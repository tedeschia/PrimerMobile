(function (dataPadron, utils) {
    var urls = {
        getPunteoColegio: '/api/Estadisticas/getPunteoColegio'
    }
    $(function() {
        initChart();
    });
    
    function initChart() {
        $("#chart").kendoChart({
            title: { text: 'Punteo por Colegio' },
            chartArea: {
                height: 1500
            },
            dataSource: {
                transport: {
                    read: {
                        url: urls.getPunteoColegio,
                        dataType: "json"
                    }
                },
                sort: {
                    field: "Colegio",
                    dir: "asc"
                }
            },
            seriesDefaults: {
                type: "bar",
            },
            series: [
                {
                    name: 'Votos punteados',
                    field: 'Punteado',
                    stack: true
                },
                {
                    name: 'Votos no punteados',
                    field: 'NoPunteado',
                }
            ],
            categoryAxis: {
                field: 'Colegio'
            }
        });
    }
}())