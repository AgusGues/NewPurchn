var vesselflow = function () {

    var initdata = function () {
        initdatareft();
    }
    var initdatareft = function () {

        $.ajax({
            url: "/TerminalFlow/GetDataTerminalFlow",
            method: "GET",
            dataType: "json",
            async: true,
            success: function (msgd) {
                setdatapie(msgd);
                localStorage.removeItem("data");
                localStorage.setItem("data", JSON.stringify(msgd));

            }
        });
    }

    var setdatapie = function (xdata) {
        $.each(xdata, function (i, items) {
            setpieitem(items.NAMA_CABANG + (1), items.TOTAL_PMH_CON, items.TOTAL_PMH_CAR, items.PMH);
            setpieitem(items.NAMA_CABANG + (2), items.TOTAL_PRN_CON, items.TOTAL_PRN_CAR, items.PRN);
            setpieitem(items.NAMA_CABANG + (3), items.TOTAL_PTP_CON, items.TOTAL_PTP_CAR, items.PTP);
            setpieitem(items.NAMA_CABANG + (4), items.TOTAL_REA_CON, items.TOTAL_REA_CAR, items.REA);
            setitembilling(items.BILLING_CON, items.BILLING_CAR, items.NAMA_CABANG + (5));
            $('#' + items.NAMA_CABANG + 'textfield').html('');
            var xalert = '';
            items.ALERT_PMH_STACK_CAR > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PMH_STACK_CAR+' Permohonan Penumpukan Cargo Melebihi Batas Waktu </h2></div>' : null;
            items.ALERT_PRN_STACK_CAR > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PRN_STACK_CAR + ' Perencanaan Penumpukan Cargo Melebihi Batas Waktu </h2></div>' : null;
            items.ALERT_START_WORK_STACK_CAR > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_START_WORK_STACK_CAR + ' Realisasi Mulai Penumpukan Cargo Melebihi Batas Waktu  </h2></div>' : null;
            items.ALERT_FINISH_STACK_CAR > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_FINISH_STACK_CAR + ' Selesai Penumpukan Cargo Melebihi Batas Waktu </h2></div>' : null;
            items.ALERT_START_STACK_CON > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_START_STACK_CON + ' Mulai Penumpukan Container Melebihi Batas Waktu </h2></div>' : null;
            items.ALERT_STACK_CON_FINISH > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_STACK_CON_FINISH + ' Selesai Penumpukan Container Melebihi Batas Waktu </h2></div>' : null;
            items.ALERT_CON_FINISHING > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_CON_FINISHING + ' Penumpukan dan Loosing Container Melebihi Batas Waktu </h2></div>' : null;
            $('#' + items.NAMA_CABANG + 'textfield').html(xalert);
            alertflow(items.NAMA_CABANG);
        });  
    }
    var setpieitem = function (title,datainti,datainti2,datachart) {
        var dom = document.getElementById(title);
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
                data: [
                    {
                        value: 10,
                        name: datainti.toString(),
                        selected: true,
                        label: {
                            normal: {
                                position: 'inside',
                                fontSize: 30,
                                fontWeight: 700
                            }
                        },
                        itemStyle: {
                            color: '#0276f2',
                        },
                    },
                    {
                        value: 20,
                        name: datainti2.toString(),
                        itemStyle: {
                            color: '#f49d41'
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
                    radius: ['93%', '55%'],
                    center: ['50%', '45%'],
                    data: datachart
                }
            ]
        };
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }

    }

    var setitembilling = function (value1,value2, id) {
        var dom = document.getElementById(id);
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        app.title = 'chart';

        option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b}: {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                // y: 'bottom',
                data: ['a', 'b', 'c', 'd', 'e', 'f']
            },
            series: [{
                name: 'chart',
                type: 'pie',
                selectedMode: 'single',
                radius: [0, '90%'],
                center: ['50%', '45%'],
                label: {
                    normal: {
                        position: 'center'
                    }
                },
                labelLine: {
                    normal: {
                        show: false
                    }
                },
                data: [
                    {
                        value: value1,
                        name: value1.toString(),
                        itemStyle: {
                            color: '#0276f2'
                        },
                        selected: true,
                        label: {
                            normal: {
                                position: 'inside',
                                fontSize: 20,
                                fontWeight: 700
                            }
                        },
                    },
                    {
                        value: value2,
                        name: value2.toString(),
                        itemStyle: {
                            color: '#f49d41'
                        },
                        label: {
                            normal: {
                                position: 'inside',
                                fontSize: 20,
                                fontWeight: 700
                            }
                        },
                    }

                ]
            },

            ]
        };
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    }
    var toastopstion = function () {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "90000",
            "hideDuration": "90000",
            "timeOut": "90000",
            "extendedTimeOut": "90000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }
    var reload = function () {
        setInterval(function () {
            toastopstion();
            initdatareft();

        }, 120000);
    }

    var alertflow = function (xvalue) {
        var $texts = $("#"+xvalue+"textfield .text");

        var textIndex = 0;

        function nextLine() {

            //Step 1: Fade in text
            $($texts[textIndex]).animate({ opacity: 1, "margin-top": 12 }, 500, "linear", function () {

                //Step 2: Fade out text
                setTimeout(function () {
                    $($texts[textIndex]).animate({ opacity: 0, "margin-top": 20 }, 500, "linear", function () {
                        //Step 3: Reset current and call next text
                        $($texts[textIndex]).css({ "margin-top": 20 });
                        textIndex++;
                        textIndex %= $texts.length;
                        nextLine();

                    });
                }, 3000);
            });

        }

        nextLine();
    }



    return {
        init: function () {
            initdata();

        }
    };
}();


jQuery(document).ready(function () {
    vesselflow.init();
});