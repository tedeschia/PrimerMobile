(function (dataPadron, utils) {
    var urls = {
        getPunteoColegio: '/api/Estadisticas/getPunteoColegio'
    }
    $(function() {
        initChart();
    });

    var dataSource = {
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
    };

    
    function initChart() {
        $("#chart").kendoChart({
            theme: 'bootstrap',
            title: { text: 'Punteo por Colegio' },
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
            }
        });
        $(window).on("resize", function() {
            kendo.resize($("#chart"));
        });

    }
}())