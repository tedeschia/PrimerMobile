(function (dataPadron, utils) {
    var vm = function () {

        var colegio = ko.observable(),
            usuario = ko.observable(),
            id = ko.observable(''),
            cambiosPendientes = ko.observable(),
            enviandoPendientes = ko.observable(false),
            keyboard = {},
            tecladoNumericoActivo = ko.observable(utils.is_touch_device),
            messages = {
                ingresarDni: { msg: 'Ingresar DNI', status: 'info' },
                noEncontrado: { msg: 'No se encontró el DNI', status: 'danger' },
                minNumeros: { msg: 'La busqueda comienza cuando hay 3 dígitos', status: 'warning' },
                punteado: { msg: '<strong>{0}</strong> punteado!', status: 'success' },
                masDe10: { msg: 'Mas de 10 electores encontrados', status: 'info' },
                resultadosMostrados: { msg: 'Resultados encontrados', status: 'info' },
                listoParaPuntear: { msg: tecladoNumericoActivo() ? 'Listo para puntear <strong>{0}</strong>!' : 'Enter para puntear <strong>{0}</strong>!', status: 'success' },
                yaEstaPunteado: { msg:'El DNI <strong>{0}</strong> ya está punteado!', status: 'warning' },
                errorLogin: { msg: 'Error: {0}', status: 'danger' },
                cambiarColegio: { msg: 'Cambiar Colegio', status: 'info' },
                sincronizado: { msg: 'Datos enviados!', status: 'success' },
            },
            cambiandoUsuario = ko.observable(false),
            inicializandoApp = ko.observable(true),
            electores = ko.observableArray(),
            usuarioAnterior = '',
            colegioAnterior = '';

        dataPadron.init()
            .done(function() {
                inicializandoApp(false);
                if (colegio()) {
                    utils.showMessage(messages.ingresarDni);
                }
            });
        dataPadron.onDataUpdated = function (data) {
            electores(data.Padron);
            colegio(data.Colegio);
            usuario(data.Usuario);
        };
        cambiosPendientes.extend({ rateLimit: 1000 });
        dataPadron.cambiosPendientesObservable = cambiosPendientes;


        var resultadoBusqueda = ko.computed(function () {
            var dni = id(),
                resultado = [];
            if (dni && dni.length > 2) {
                resultado = electores().filter(function (elector) {
                    return elector.DNI && elector.DNI.toString().substring(0, dni.length) == dni;
                });
                if (resultado.length > 10) {
                    utils.showMessage(messages.masDe10);
                    return [];
                } else {
                    if (resultado.length > 0) {
                        utils.showMessage(messages.resultadosMostrados);
                    } else {
                        utils.showMessage(messages.noEncontrado);
                    }
                    addObservables(resultado);
                    return resultado;
                }
            } else if(dni) {
                utils.showMessage(messages.minNumeros);
            };
            return [];

        });
        resultadoBusqueda.extend({ rateLimit: 500 });

        //keyboard
        keyboard.puedeAutomarcar = ko.computed(function () {
            if (resultadoBusqueda().length == 1) {
                if (resultadoBusqueda()[0].Punteado) {
                    utils.showMessage(messages.yaEstaPunteado, [resultadoBusqueda()[0].DNI]);
                    return false;

                } else {
                    utils.showMessage(messages.listoParaPuntear, [resultadoBusqueda()[0].DNI]);
                    return true;
                }
            }
            return false;
        });
        keyboard.automarcar = function () {
            if (!keyboard.puedeAutomarcar()) return;
            var elector = ko.utils.unwrapObservable(resultadoBusqueda)[0];
            if (!elector.Punteado) {
                elector.PunteadoObservable(true);
                id('');
            }
        };


        function addObservables(array) {
            array.forEach(function (e) {
                if (!e.InSyncObservable) {
                    e.InSyncObservable = ko.observable((e.InSync == undefined) || e.InSync);
                    e.InSyncObservable.subscribe(function (value) {
                        e.InSync = value;
                    });
                    e.InSyncObservable.extend({ rateLimit: 1000 });
                    e.PunteadoObservable = ko.observable(e.Punteado);
                    e.PunteadoObservable.subscribe(function(value) {
                        e.Punteado = value;
                        if (!e.InSyncObservable()) {
                            //si no estaba sync cancelo el cambio pendiente
                            dataPadron.cancelUpdate([e]);
                        } else {
                            if (e.Punteado) {
                                utils.showMessage(messages.punteado, [e.DNI]);
                            }
                            dataPadron.update([e]);
                        }
                    });
                }
            });

        }

        function initKeyboard(elements) {
            var keyboard = $(elements).find('#keyboard'),
                ns = this;
            $(function () {
                var element = keyboard.get(0);
                FastClick.attach(element);
            });
            keyboard.find('.key-numero').click(function (e) {
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

        function login() {
            cambiandoUsuario(true);
            dataPadron.changeUser(usuario())
                .fail(function(e) {
                    utils.showMessage(messages.errorLogin, [e]);
                }).always(function() {
                    cambiandoUsuario(false);
                });
        }
        function cambiarColegio() {
            usuarioAnterior = usuario();
            colegioAnterior = colegio();
            usuario('');
            colegio('');
            utils.showMessage(messages.cambiarColegio);
        }
        function cancelarCambioColegio() {
            usuario(usuarioAnterior);
            colegio(colegioAnterior);
        }

        function enviarPendientes() {
            enviandoPendientes(true);
            dataPadron.savePendientes()
                .done(function() {
                    utils.showMessage(messages.sincronizado);
                })
                .always(function() {
                    enviandoPendientes(false);
                });
        }
        return {
            inicializandoApp: inicializandoApp,
            cambiandoUsuario: cambiandoUsuario,
            id: id,
            resultadoBusqueda: resultadoBusqueda,
            cambiosPendientes: cambiosPendientes,
            enviarPendientes: enviarPendientes,
            enviandoPendientes: enviandoPendientes,
            tecladoNumericoActivo: tecladoNumericoActivo,
            initTouchKeyboard: initKeyboard,
            keyboard: keyboard,
            login: login,
            usuario: usuario,
            colegio: colegio,
            cambiarColegio: cambiarColegio,
            cancelarCambioColegio: cancelarCambioColegio
        };
    };

    var viewModel = new vm();
    ko.applyBindings(viewModel);

    $(function () {
        //IE mantiene el valor del input si la pagina está en cache
        viewModel.id('');
    });

}(DATACONTEXT.data, APP.utils))