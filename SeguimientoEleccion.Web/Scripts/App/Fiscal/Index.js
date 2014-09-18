(function (dataPadron, utils) {
    var vm = function() {

        var colegio = ko.observable(),
            id = ko.observable(''),
            settings = {},
            keyboard = {
            },
            message = ko.observable(''),
            messageStatus = ko.observable('info'),
            messages = {
                ingresarDni: { msg:'Ingresar DNI', status:'info' },
                noEncontrado: { msg: 'No se encontró el DNI', status: 'danger' },
                minNumeros: { msg: 'La busqueda comienza cuando hay 3 dígitos', status: 'warning' },
                punteado: { msg: 'Punteado!', status: 'success' },
                masDe10: { msg:'Mas de 10 electores encontrados' ,status:'info'},
                resultadosMostrados: { msg:'Resultados encontrados' ,status:'info'},
                listoParaPuntear: { msg:'Listo para puntear!' ,status:'success'}
            },
            cargaFinalizada = ko.observable(false),
            electores = ko.observableArray();

        var resultadoBusqueda = ko.computed(function() {
            var dni = id(),
                resultado = [];
            if (dni && dni.length > 2) {
                resultado = electores().filter(function(elector) {
                    return elector.DNI && elector.DNI.substring(0, dni.length) == dni;
                });
                if (resultado.length > 10) {
                    showMessage(messages.masDe10);
                    return [];
                } else {
                    if (resultado.length > 0) {
                        showMessage(messages.resultadosMostrados);
                    } else {
                        showMessage(messages.noEncontrado);
                    }
                    addObservables(resultado);
                    return resultado;
                }
            } else {
                showMessage(messages.minNumeros);
                return [];
            };
            
        });
        resultadoBusqueda.extend({rateLimit:500});

        //keyboard
        keyboard.puedeAutomarcar = ko.computed(function () {
            var resultado = resultadoBusqueda().length == 1;
            if (resultado) {
                showMessage(messages.listoParaPuntear);
            }
            return resultado;
        });
        keyboard.automarcar = function () {
            var elector = ko.utils.unwrapObservable(resultadoBusqueda)[0];
            elector.PunteadoObservable(true);
            //elector.Punteado = true;
            //$('#punteado_' + elector.Id).prop('checked', true);
            //guardarPuntear(elector);
            showMessage(messages.punteado);

        };

        dataPadron.ready
            .done(function () {
                electores(dataPadron.getData());
                cargaFinalizada(true);
            });
        dataPadron.onDataUpdated = function (data) {
            electores(data);
        };
        dataPadron.onDataSync = function (data) {
            //var target = ko.utils.unwrapObservable(electores);
            data.forEach(function(e) {
                //var toUpdate = target.filter(function (t) { return t.Id == e.Id; });
                //if (toUpdate) {
                    if (e.InSyncObservable) {
                        e.InSyncObservable(true);
                    } else {
                        e.InSync = true;
                    }
                //}
            });
        };

        function addObservables(array) {
            array.forEach(function (e) {
                if (!e.InSyncObservable) {
                    e.InSyncObservable = ko.observable((e.InSync == undefined) || e.InSync);
                    e.InSyncObservable.subscribe(function (value) {
                        e.InSync = value;
                    });
                    e.PunteadoObservable = ko.observable(e.Punteado);
                    e.PunteadoObservable.subscribe(function (value) {
                        e.Punteado = value;
                        //si estaba pendiente y se volvió al estado de punteado anterior lo saco de pendiente
                        e.InSyncObservable(!e.InSyncObservable());
                        dataPadron.update([e]);
                    });
                }
            });

        }
        //function puntear(elector, event) {
        //    //elector.Punteado = $(event.target).prop('checked');
        //    guardarPuntear(elector);
        //    return true;
        //}
        //function guardarPuntear(elector) {
        //    setSync(elector,false);
        //    var pendientes = electores.filter(function (p) { return p.InSync != undefined && !p.InSync; });
        //    dataPadron.update(pendientes)
        //        .done(function() {
        //            pendientes.forEach(function (p) { setSync(p, true); });
        //        });
        //}
        
        //function setSync(entity, status) {
        //    if (!entity.InSyncObservable) {
        //        entity.InSyncObservable = ko.computed({
        //            read: function() {
        //                return this.InSync == undefined || this.InSync;
        //            },
        //            write: function(value) {
        //                this.InSync = value;
        //            }
        //        }, entity);
        //    }
        //    entity.InSyncObservable(status);
        //}

        function initKeyboard(elements) {
            var keyboard = $(elements).find('#keyboard'),
                ns = this;
            $(function () {
                var element = keyboard.get(0);
                FastClick.attach(element);
            });
            keyboard.find('.key-numero').click(function(e) {
                var btn = $(e.target),
                    numero = btn.attr('value');
                var dni = id() || '';
                id(dni + '' + numero);
            });

            keyboard.find('.key-back').click(function (e) {
                if (!id() || id().length == 0) return;
                id(id().substring(0, id().length - 1));
            });
            keyboard.find('.key-remove').click(function (e) {
                id('');
            });
        };
        
        //mensajes
        showMessage(messages.ingresarDni);
        
        function showMessage(msg) {
            message(msg.msg);
            messageStatus(msg.status);
        }

        return {
            cargaFinalizada: cargaFinalizada,
            id: id,
            resultadoBusqueda: resultadoBusqueda,
            //puntear: puntear,
            touchDevice: utils.is_touch_device(),
            initTouchKeyboard: initKeyboard,
            keyboard: keyboard,
            message: message,
            messageStatus: messageStatus
        };
    };

    var viewModel = new vm();
    ko.applyBindings(viewModel);

    $(function () {
        //IE mantiene el valor del input si la pagina está en cache
        viewModel.id('');
    });

}(DATACONTEXT.data, APP.utils))