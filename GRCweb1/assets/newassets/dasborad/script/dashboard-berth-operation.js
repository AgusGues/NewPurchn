
$(document).ready(function () {
    RequestAjaxData();
    RequestAjaxWTB();
    RequestAjaxFairPoor();
    setInterval(function () {
        RequestAjaxData();
        RequestAjaxWTB();
        RequestAjaxFairPoor();
    }, 600000);
});

function RequestAjaxData() {
    $.ajax({
        url: "/Berth/berthopertionchart",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawData(data);
        }
    });
}


function RequestAjaxWTB() {
    $.ajax({
        url: "/Berth/berthopertionwtb",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            WTB(data);
        }
    });
}

function RequestAjaxFairPoor() {
    $.ajax({
        url: "/Berth/berthopertionfairpoor",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            FairPoor(data);
        }
    });
}

function FairPoor(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_TERMINAL + "f").text(data[i].FAIR);
        $("#" + data[i].NAMA_TERMINAL + "l").text(data[i].LATE);
        $("#" + data[i].NAMA_TERMINAL + "s").text(data[i].STOP);
    }
}

function WTB(data) {
    var NilamRata, MirahRata, Total;
    for (var i = 0; i < data.length; i++) {
        if (data[i].NAMA_TERMINAL == 'NILAM' || data[i].NAMA_TERMINAL == 'MIRAH') {
            for (var j = 0; j < data.length; j++) {
                if (data[j].NAMA_TERMINAL == 'NILAM') {
                    NilamRata = data[j].RATA;
                } else if (data[j].NAMA_TERMINAL == 'MIRAH') {
                    MirahRata = data[j].RATA;
                }

            }
            total = ((NilamRata + MirahRata) / 2).toFixed(2);
            $("#NM").text(total);
        } else {
            $("#" + data[i].NAMA_TERMINAL + "").text((data[i].RATA).toFixed(2))
        }
    }
}


function DrawData(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_TERMINAL + "ocar").text(data[i].ON_SCHEDULE_CAR);
        $("#" + data[i].NAMA_TERMINAL + "ocon").text(data[i].ON_SCHEDULE_CON);
        $("#" + data[i].NAMA_TERMINAL + "scar").text(data[i].SHORTEN_CAR);
        $("#" + data[i].NAMA_TERMINAL + "scon").text(data[i].SHORTEN_CON);
        $("#" + data[i].NAMA_TERMINAL + "ecar").text(data[i].EXTENSION_CAR);
        $("#" + data[i].NAMA_TERMINAL + "econ").text(data[i].EXTENSION_CON);
        $("#" + data[i].NAMA_TERMINAL + "tcar").text(data[i].TOTAL_CAR);
        $("#" + data[i].NAMA_TERMINAL + "tcon").text(data[i].TOTAL_CON);

        $("#" + data[i].KODE_CABANG + "i").text(data[i].TOTAL);
        var dom = document.getElementById("chart" + data[i].NAMA_TERMINAL + "");
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        app.title = 'chart';

        option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            series: [{
                name: 'chart',
                type: 'pie',
                selectedMode: 'single',
                radius: [0, '40%'],
                center: ['50%', '45%'],

                label: {
                    normal: {
                        position: 'inner'
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [{
                    value: data[i].TOTAL_CAR,
                    name: data[i].TOTAL_CAR,
                    selected: true,
                    label: {
                        normal: {
                            position: 'inner',
                            fontSize: 30,
                            fontWeight: 700
                        }
                    },
                    itemStyle: {
                        color: '#AA00FF',
                    },
                },
                {
                    value: data[i].TOTAL_CON,
                    name: data[i].TOTAL_CON,
                    itemStyle: {
                        color: '#00C853'
                    },
                    label: {
                        normal: {
                            position: 'inside',
                            fontSize: 30,
                            fontWeight: 700
                        }
                    },

                }
                ]
            },
            {
                name: 'chartlagi',
                type: 'pie',
                radius: ['85%', '55%'],
                center: ['50%', '45%'],
                data: [{
                    value: data[i].ON_SCHEDULE_CAR,
                    name: data[i].ON_SCHEDULE_CAR,
                    itemStyle: {
                        color: '#D500F9'
                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },
                    selected: true
                },
                {
                    value: data[i].SHORTEN_CAR,
                    name: data[i].SHORTEN_CAR,
                    selected: true,
                    itemStyle: {
                        color: '#9f17b7'

                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },
                },
                {
                    value: data[i].EXTENSION_CAR,
                    name: data[i].EXTENSION_CAR,
                    selected: true,
                    itemStyle: {
                        color: '#7c00c7'

                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },

                },
                {
                    value: data[i].ON_SCHEDULE_CON,
                    name: data[i].ON_SCHEDULE_CON,
                    itemStyle: {
                        color: '#00C853'
                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },
                },
                {
                    value: data[i].SHORTEN_CON,
                    name: data[i].SHORTEN_CON,
                    itemStyle: {
                        color: '#119e5a'
                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },
                },
                {
                    value: data[i].EXTENSION_CON,
                    name: data[i].EXTENSION_CON,
                    itemStyle: {
                        color: '#00773e'
                    },
                    label: {
                        normal: {
                            position: 'inside',
                            color: '#fff',
                            fontSize: '24',
                            fontWeight: '600'
                        }
                    },
                },
                ]
            }
            ]
        };
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

    }

} 