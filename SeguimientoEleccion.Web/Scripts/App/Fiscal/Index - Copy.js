(function (remoteContext, localContext) {
    var vm = function() {

        var electores = ko.observableArray([]),
            colegio = ko.observable(),
            mesa=ko.observable(),
            editedElector=ko.observable();

        var loadElectores= ko.computed(function() {
            var query = localContext.Electores;
                //.filter(function(elector) {
                //    return elector.Colegio == this.colegio;
                //    //        (!this.mesa || elector.Mesa == this.mesa);
                //}, { colegio: colegio() || '' });
            query.toArray(electores);
        });

        function change(elector) {
            localContext.attach(elector.innerInstance);
            elector.InSync(false);
        }
        function save() {
            localContext.saveChanges(synchronizeData);
            //remoteContext.saveChanges();
            return true;
        }

        function synchronizeData() {
            localContext
                .Electores
                .filter("it.InSync === false")
                .toArray(function(electores) {
                    electores.forEach(function(e) {
                        e.InSync = true;
                        remoteContext.attach(e, $data.EntityAttachMode.AllChanged);
                    });
                    remoteContext.saveChanges(function () {
                        //luego del save en el online actualizo la versión local como sincronizada
                        //si esto no se termina de hacer no pasa nada, como mucho volverá a enviar los datos para guardar la próxima vez
                        electores.forEach(function (e) {
                            localContext.attach(e, $data.EntityAttachMode.AllChanged);
                            localContext.saveChanges();
                            localContext.Electores.detach(e);
                        });
                    });
                });
        }
        return {
            electores: electores,
            colegio: colegio,
            change: change,
            save: save
    };
    };

    DATACONTEXT.ready
        .always(function () {
            var viewModel = new vm();
            ko.applyBindings(viewModel);
        });

}(DATACONTEXT.online, DATACONTEXT.offline))