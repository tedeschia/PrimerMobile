(function (dataPadron, utils) {
    $(function() {
        initChart();
    });

    var dataSource = {
        transport: {
            read: {
                url: '/api/Estadisticas/getVotacion',
                dataType: "json"
            }
        },
        sort: {
            field: "Colegio",
            dir: "asc"
        }
    };

    
    function initChart() {
        $("#chart").kendoChart({
            theme: 'bootstrap',
            title: { text: 'Votos' },
            legend: { position: 'top' },
            chartArea: {
                height: 1500
            },
            dataSource: dataSource,
            seriesDefaults: {
                type: "bar",
            },
            series: [
                {
                    name: 'Punteado/Voto',
                    field: 'PunteadoVoto',
                    stack: true
                },
                {
                    name: 'Punteado/No Voto',
                    field: 'PunteadoNoVoto',
                },
                {
                    name: 'No Punteado/Voto',
                    field: 'NoPunteadoVoto',
                },
                {
                    name: 'No Punteado/No Voto',
                    field: 'NoPunteadoNoVoto',
                },
            ],
            categoryAxis: {
                field: 'Colegio',
                axisCrossingValues: [0, 1000],
                labels: {
                    font: '10px sans-serif',
                    template: '#=value.substring(0,15)#'
                }
            },
            valueAxes: [{
                    name: 'top'
                },
                {
                    name: 'bottom'
                }],
            tooltip: {
                visible: true,
                template: "#= series.name #: : #= value #"
            },
        });
        
        $(window).on("resize", function () {
            kendo.resize($("#chart"));
        });
    }
}())