(function (dataPadron, utils) {
    var urls = {
        getVotacion: '/api/Estadisticas/getVotacion',
    }
    $(function() {
        initChart();
    });
    
    function initChart() {
        $("#chart").kendoChart({
            title: { text: 'Votos' },
            chartArea: {
                height: 1500
            },
            dataSource: {
                transport: {
                    read: {
                        url: urls.getVotacion,
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