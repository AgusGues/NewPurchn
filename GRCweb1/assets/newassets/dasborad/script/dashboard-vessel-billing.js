$(document).ready(function () {
    RequestAjaxData();
    RequestAjaxPlan();
    RequestAjaxOutstanding();
    setInterval(function () {
        RequestAjaxData();
        RequestAjaxPlan();
        RequestAjaxOutstanding();
    }, 600000);
});

function RequestAjaxData() {
    $.ajax({
        url: "/VesselBilling/List",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawData(data);
        }
    });
}

function RequestAjaxOutstanding() {
    $.ajax({
        url: "/VesselBilling/Outstanding",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (outstanding) {
            DrawOutstanding(outstanding);
        }
    });
}

function DrawOutstanding(outstanding) {
    for (var i = 0; i < outstanding.length; i++) {
        $("#" + outstanding[i].KAWASAN + "o").text(outstanding[i].TOTAL);
    }
}

function RequestAjaxPlan() {
    $.ajax({
        url: "/VesselBilling/Plan",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (plan) {
            DrawPlan(plan);
        }
    });
}

function DrawPlan(plan) {
    for (var i = 0; i < plan.length; i++) {
        $("#" + plan[i].NAMA_KAWASAN + "t").text(plan[i].TODAY);
        $("#" + plan[i].NAMA_KAWASAN + "n").text(plan[i].TOMORROW);
    }
}

function DrawData(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_CABANG + "i").text(data[i].TOTAL);
        //$("#" + data[i].KODE_CABANG + "d").text((data[i].RATA / 24).toFixed(2));
        $("#" + data[i].NAMA_CABANG + "d").text(data[i].RATA);
        var dom = document.getElementById("chart" + data[i].NAMA_CABANG + "");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        var dibawah = ((data[i].DIBAWAH).toFixed(2));
        var antara = ((data[i].ANTARA).toFixed(2));
        var diatas = ((data[i].DIATAS).toFixed(2));
        app.title = 'piechart';

        option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                //data: [(data[i].DIATAS) + '%', (data[i].ANTARA) + '%', (data[i].DIBAWAH) + '%'],
                data: ['> 3   : ' + diatas + '%', '1 - 3 : ' + antara + '%', '< 1   : ' + dibawah + '%'],
                textStyle: {
                    fontSize: 28,
                    fontWeight: 500,
                    color : 'white'
                }
            },
            series: [{
                name: 'piechart',
                type: 'pie',
                radius: ['50%', '70%'],
                center: ['50%', '53%'],
                avoidLabelOverlap: false,
                label: {
                    normal: {
                        show: false,
                        position: 'center'
                    },
                    emphasis: {
                        show: true,
                        textStyle: {
                            fontSize: '35',
                            fontWeight: 'bold'
                        }
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [{
                    value: diatas,
                    name: '> 3   : ' + diatas + '%',
                    //name: diatas + '%',
                    itemStyle: {
                        color: '#ED4D49'
                    }
                },
                {
                    value: antara,
                    name: '1 - 3 : '+ antara + '%',
                    //name: antara + '%',
                    itemStyle: {
                        color: '#00C8FF'
                    }
                },
                {
                    value: dibawah,
                    name: '< 1   : '+dibawah + '%',
                    //name: dibawah + '%',
                    itemStyle: {
                        color: '#50D850'
                    }
                }
                ]
            }]
        };;;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    }
}