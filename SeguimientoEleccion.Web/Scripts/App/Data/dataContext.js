DATACONTEXT = function (utils) {

    var DataSourcePadron = function () {
        var ns = this,
            dataKey = 'eleccion2014',
            database = {},
            urls = {
                getAll: 'api/Padron',
                update: 'api/Padron/puntear'
            },
            actualizandoDatos,
            messages = {
                descargandoDatos: { msg: 'Descargando datos (puede demorar) ...', status: 'info' },
                preparandoDatos: { msg: 'Datos descargados, preparando ...', status: 'info' },
                datosActualizados: { msg: 'Datos actualizados', status: 'info' },
            },
            cantidadCambiosPendientes = 0;


        function init() {
            var initDeferred = $.Deferred();
            setTimeout(function() {
                database = readLocal();
                if (database.Colegio) {
                    var cambiosPendientes = database.Padron.filter(function (p) { return p.InSync != undefined && !p.InSync; });
                    setCantidadCambiosPendientes(cambiosPendientes.length);

                    raiseDataUpdated(database);
                    refreshData(database.Usuario);
                }
                initDeferred.resolve();
            }, 1);

            return initDeferred.promise();

        }

        function update(entities) {
            //cuento los nuevos pendientes y notifico
            var pendientesNuevos = entities.filter(function (e) {
                return e.InSync == undefined || e.InSync;
            }).length;
            addCantidadCambiosPendientes(pendientesNuevos);
            //marco todos como pendiente
            entities.forEach(function (e) { setInSync(e, false); });

            saveLocal();

            //si estoy en proceso de traer datos del server no mando actualizaciones
            if (actualizandoDatos) return;

            var ids = entities.map(function (e) {
                return { Key: e.Id, Value: e.Voto };
            });
            return $.ajax(urls.update, {
                type: 'PUT',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(ids)
            }).done(function(data) {
                entities.forEach(function(e) { setInSync(e, true); });
                saveLocal();
                addCantidadCambiosPendientes(-entities.length);
            });
        }
        function cancelUpdate(entities) {
            entities.forEach(function (e) { setInSync(e, true); });
            addCantidadCambiosPendientes(-entities.length);
            saveLocal();
        }
        function setInSync(e,value) {
            if (e.InSyncObservable) {
                e.InSyncObservable(value);
            } else {
                e.InSync = value;
            }
        }
        function refreshData(usuario) {
            actualizandoDatos = true;
            utils.showMessage(messages.descargandoDatos);


            return $.get(urls.getAll + '/' + usuario)
                .done(function (data) {
                    utils.showMessage(messages.preparandoDatos);
                    
                    var cambiosPendientes = database.Padron ? database.Padron.filter(function(p) { return p.InSync != undefined && !p.InSync; }) : [];
                    setCantidadCambiosPendientes(cambiosPendientes.length);
                    database = data;
                    cambiosPendientes.forEach(function(p) {
                        var toUpdate = attachOrGet(p);
                        toUpdate.InSync = p.InSync;
                        toUpdate.Voto = p.Voto;
                    });
                    saveLocal();
                    raiseDataUpdated(database);

                }).always(function () {
                    actualizandoDatos = false;
                    savePendientes();
                    utils.showMessage(messages.datosActualizados);
                });
        }

        function changeUser(usuario) {
            var deferred = $.Deferred();
            refreshData(usuario)
                .done(function() {
                    if (!database.Usuario) {
                        deferred.reject('Usuario inválido');
                    } else {
                        deferred.resolve();
                    }
                }).fail(function(e) {
                    deferred.reject('No se pudo ingresar, posiblemente por fallas de conexión');
                });
            return deferred.promise();
        }
        function raiseDataUpdated(padron) {
            if (typeof ns.onDataUpdated === "function") {
                //setTimeout(ns.onDataUpdated(padron), 1);
                ns.onDataUpdated(padron);
            }
        }

        function savePendientes() {
            var deferred = $.Deferred();
            var cambiosPendientes = database.Padron ? database.Padron.filter(function (p) { return p.InSync != undefined && !p.InSync; }) : [];
            if (cambiosPendientes.length > 0) {
                update(cambiosPendientes)
                    .done(function(d) { deferred.resolve(d); })
                    .fail(function(d) { deferred.reject(d); });
            }
            return deferred.promise();
        }

        function saveLocal() {
            var data = JSON.stringify(database);
            localStorage.setItem(dataKey, data);
        }

        function readLocal() {
            var padron = [],
                colegio = '',
                usuario = '';
            try {
                var data = JSON.parse(localStorage.getItem(dataKey));
                if (data && data.Padron && data.Colegio && data.Usuario) {
                    padron = data.Padron;
                    colegio = data.Colegio;
                    usuario = data.Usuario;
                }
            } catch (e) {
            }
            return {
                Padron: padron,
                Colegio: colegio,
                Usuario: usuario
            };
        }

        function attachOrGet(entity) {
            var current = find(entity.Id);
            if (!current) {
                current = entity;
                database.Padron.push(entity);
            }
            return current;
        }

        function find(id) {
            var result = database.Padron.filter(function (p) { return p.Id == id; });
            if (result.length > 0) {
                return result[0];
            }
            return null;
        }
        
        function setCantidadCambiosPendientes(cantidad) {
            cantidadCambiosPendientes = cantidad;
            addCantidadCambiosPendientes(0);
        }
        function addCantidadCambiosPendientes(delta) {
            cantidadCambiosPendientes += delta;
            if (typeof ns.cambiosPendientesObservable === "function") {
                ns.cambiosPendientesObservable(cantidadCambiosPendientes);
            }
        };

        ns.getData = function () {
            return database.Padron;
        };
        ns.savePendientes = savePendientes;
        ns.update = update;
        ns.cancelUpdate = cancelUpdate;
        ns.onDataUpdated = undefined;
        ns.changeUser = changeUser;
        ns.init = init;
        ns.cambiosPendientesObservable = undefined;
    };

    var ns = this;
    ns.data = new DataSourcePadron();

    return ns;
}(APP.utils);



