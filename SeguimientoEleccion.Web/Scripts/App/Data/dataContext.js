DATACONTEXT = function () {

    var DataSourcePadron = function() {
        var ns = this,
            dataKey = 'eleccion2014',
            database = {},
            urls = {
                getAll: 'api/Padron',
                update: 'api/Padron/puntear'
            },
            ready = false,
            readyDeferred = $.Deferred(),
            actualizandoDatos;

        function update(entities) {
            //si estoy en proceso de traer datos del server no mando actualizaciones
            if (actualizandoDatos) return;
            var ids = [];

            entities.forEach(function(e) {
                //var toUpdate = attachOrGet(e);
                //toUpdate.Punteado = e.Punteado;
                //e.InSyncObservable(false);
                ids.push({ Key: e.Id, Value: e.Punteado });
            });
            saveLocal();
            $.ajax(urls.update, {
                type: 'PUT',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(ids)
            }).done(function (data) {
                saveLocal();
                if (typeof ns.onDataSync == 'function') {
                    ns.onDataSync(entities);
                }
            });
        }

        loadData();
        
        function loadData() {
            database = readLocal();
            if (database.Padron.length > 0) {
                readyDeferred.resolve();
            }

            var fechaUltimaActualizacion = (database && database.FechaUltimaActualizacion) ? database.FechaUltimaActualizacion : '';

            actualizandoDatos = true;
            $.get(urls.getAll + '/' + fechaUltimaActualizacion)
                .done(function (data) {
                    //vino null porque no hubieron modificaciones. CACHE MANUAL "CASERITO!!"
                    if (data) {
                        var pending = [];
                        if (database.Padron) {
                            pending = database.Padron.filter(function (p) { return p.InSync != undefined && !p.InSync; });
                        }
                        database = data;
                        pending.forEach(function (p) {
                            var toUpdate = attachOrGet(p);
                            toUpdate.InSync = p.InSync;
                            toUpdate.Punteado = p.Punteado;
                        });
                        saveLocal();
                        raiseDataUpdated(database.Padron);
                    }

                }).always(function () {
                    actualizandoDatos = false;
                    savePendientes();
                    if (readyDeferred.state()=="pending") {
                        readyDeferred.resolve();
                    }
                });
        }

        function raiseDataUpdated(padron) {
            if (typeof ns.onDataUpdated === "function") {
                //setTimeout(ns.onDataUpdated(padron), 1);
                ns.onDataUpdated(padron);
            }
        }
        
        function savePendientes() {
            var pendientes = database.Padron.filter(function (p) { return p.InSync != undefined && !p.InSync; });
            if (pendientes.length > 0) {
                update(pendientes);
            }
        }

        function addObservables(padron) {
            padron.forEach(function(e) {
                e.InSyncObservable = ko.observable((e.InSync == undefined) || e.InSync);
                e.InSyncObservable.subscribe(function (value) {
                    e.InSync = value;
                });
                e.PunteadoObservable = ko.observable(e.Punteado);
                e.PunteadoObservable.subscribe(function (value) {
                    e.Punteado = value;
                    //si estaba pendiente y se volvió al estado de punteado anterior lo saco de pendiente
                    e.InSyncObservable(!e.InSyncObservable());
                    update([e]);
                });
            });
        }

        function saveLocal() {
            //var data = JSON.stringify(database);
            //localStorage.setItem(dataKey+".Padron",database.Padron);
        }

        function readLocal() {
            var padron = [];
            //try {
            //    var data = JSON.parse(localStorage.getItem(dataKey+".Padron"));
            //    if (!data) throw 'Invalid data in storage';
            //    padron = data;
            //} catch(e) {
            //    //return {
            //    //    Padron: [],
            //    //    FechaUltimaActualizacion: ''
            //    //};
            //}
            return {
                Padron: padron,
                FechaUltimaActualizacion: ''
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
            var result = database.Padron.filter(function(p) { return p.Id == id; });
            if (result.length > 0) {
                return result[0];
            }
            return null;
        }

        ns.ready = readyDeferred
            .done(function() {
                ready = true;
            }).promise();
        ns.getData = function() {
            return database.Padron;
        };
        ns.savePendientes = savePendientes;
        ns.update = update;
        ns.onDataUpdated = undefined;
        ns.onDataSync = undefined;
    };

    var DataSourceSettings = function () {
        var ns = this,
            urls = {
                get: 'api/settings/servicio',
                set: 'api/settings/servicio',
            },
            ready = false;

        ns.set = function (settings) {

            return $.ajax(urls.set, {
                type: 'PUT',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(settings)
            }).fail(function (data) {
                console.log(data.message);
            });
        };

        ns.get = function () {
            return $.get(urls.get)
                .fail(function (data) {
                    console.log(data.responseText);
                });
        };
    };

    var ns = this;
    ns.data = new DataSourcePadron();
    ns.settings = new DataSourceSettings();
    
    return ns;
}();



