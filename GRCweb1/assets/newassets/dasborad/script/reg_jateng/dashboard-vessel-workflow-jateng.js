var vesselflow = function () {

    var initdatareft = function () {
        GetPilotAlert();
        GetBerthAlert();
        GetSPBBelumNota();
        $.ajax({
            url: "/VesselWorkflowJateng/GetDataVesselWorkflow",
            method: "GET",
            dataType: "json",
            async: true,
            success: function (msgd) {
                setdatapie(msgd);
                reload();
            }
        });
    };

    var GetPilotAlert = function () {

        $.ajax({
            url: "/VesselWorkflowJateng/GetDataPilotAlert",
            method: "GET",
            dataType: "json",
            async: true,
            success: function (msgd) {
                ShowData(msgd);

            }
        });
    };

    var ShowData = function (data) {

        for (var i = 0; i < data.length; i++) {
            $("#" + data[i].KAWASAN + "pmhptppilot").text(data[i].ALERT_PMH_TO_PTP);
            $("#" + data[i].KAWASAN + "blmspk").text(data[i].ALERT_SPK);
            $("#" + data[i].KAWASAN + "blmohn").text(data[i].ALERT_OHN);
            $("#" + data[i].KAWASAN + "ptpreapilot").text(data[i].ALERT_PTP_TO_REA);
            $("#" + data[i].KAWASAN + 'blmverif').text(data[i].ALERT_VERIFIKASI);
            $("#" + data[i].KAWASAN + 'blmnota').text(data[i].ALERT_BILLING);
        }
    };

    var GetBerthAlert = function () {

        $.ajax({
            url: "/VesselWorkflowJateng/GetDataBerthAlert",
            method: "GET",
            dataType: "json",
            async: true,
            success: function (msgd) {
                ShowBerthAlert(msgd);

            }
        });
    };

    var ShowBerthAlert = function (data) {

        for (var i = 0; i < data.length; i++) {
            $("#" + data[i].KAWASAN + "pmhptpberth").text(data[i].ALERT_PMH_TO_PTP);
            $("#" + data[i].KAWASAN + "ptpreaberth").text(data[i].ALERT_PTP_TO_REA);
            $("#" + data[i].KAWASAN + "reabillberth").text(data[i].ALERT_REA_TO_BILLING);
        }
    };

    var GetSPBBelumNota = function () {

        $.ajax({
            url: "/VesselWorkflowJateng/SPBBelumNota",
            method: "GET",
            dataType: "json",
            async: true,
            success: function (msgd) {
                ShowSPBBelumNota(msgd);

            }
        });
    };

    var ShowSPBBelumNota = function (data) {

        for (var i = 0; i < data.length; i++) {
            $("#" + data[i].KAWASAN + "spbblmnota").text(data[i].TOTAL);
        }
    };

    var setdatapie = function (xdata) {
        $.each(xdata, function (i, items) {

            setpieitem(items.KAWASAN + (1), items.TOTAL_PMH, items.TOTAL_PMH_T, items.PMH);
            setpieitem(items.KAWASAN + (3), items.TOTAL_PTP, items.TOTAL_PTP_T, items.PTP);
            setpieitem(items.KAWASAN + (4), items.TOTAL_REA, items.TOTAL_REA_T, items.REA);
            setitembilling(items.BILLING, items.KAWASAN + (5));
            $('#' + items.KAWASAN + 'textfield').html('');
            //var xalert = '';
            //items.ALERT_PMH_TO_PTP_ARRIVAL > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PMH_TO_PTP_ARRIVAL + ' Permohonan ARRIVAL Belum Ditetapkan </h2></div>' : null;
            //items.ALERT_PMH_TO_PTP_SHIFTING > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PMH_TO_PTP_SHIFTING + ' Permohonan SHIFTING Belum Ditetapkan </h2></div>' : null;
            //items.ALERT_PMH_TO_PTP_DEPARTURE > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PMH_TO_PTP_DEPARTURE + ' Permohonan DEPARTURE Belum Ditetapkan </h2></div>' : null;
            //items.ALERT_PTP_TO_REA_ARRIVAL > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PTP_TO_REA_ARRIVAL + ' Penetapan ARRIVAL Belum Direalisasi </h2></div>' : null;
            //items.ALERT_PTP_TO_REA_SHIFTING > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PTP_TO_REA_SHIFTING + ' Penetapan SHIFTING Belum Direalisasi </h2></div>' : null;
            //items.ALERT_PTP_TO_REA_DEPARTURE > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PTP_TO_REA_DEPARTURE + ' Penetapan DEPARTURE Belum Direalisasi </h2></div>' : null;

            //items.ALERT_SPK > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_SPK + ' Belum SPK </h2></div>' : null;
            //items.ALERT_OHN > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_OHN + ' Belum OHN </h2></div>' : null;
            //items.ALERT_VERIFIKASI > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_VERIFIKASI + ' Belum Verifikasi </h2></div>' : null;
            //items.ALERT_BILLING > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_BLLING + ' Billing Belum Dinotakan </h2></div>' : null;

            //items.ALERT_PMH_TO_PRN > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PMH_TO_PRN + ' Permohonan TAMBAT Belum Direncanakan </h2></div>' : null;
            //items.ALERT_PRN_TO_PTP > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PRN_TO_PTP + ' Perencanaan TAMBAT Belum Ditetapkan </h2></div>' : null;
            //items.ALERT_PTP_TO_REA > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_PTP_TO_REA + ' Penetapan TAMBAT Belum Direalisasi </h2></div>' : null;
            //items.ALERT_REA_TO_BILLING > 0 ? xalert += '<div class="text" style="position:absolute;text-align:left;width:100%;margin-top:20px;font-family:Arial;opacity:0;"><h2>' + items.ALERT_REA_TO_BILLING + 'TAMBAT Belum Dinotakan </h2></div>' : null;
            //$('#' + items.KAWASAN + 'textfield').html(xalert);
            //alertflow(items.KAWASAN);
        });

    };
    var setpieitem = function (title, datainti, datainti2, datachart) {
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
                            color: '#cc0000'
                        }
                    },
                    {
                        value: 20,
                        name: datainti2.toString(),
                        itemStyle: {
                            color: '#00C853'
                        },
                        label: {
                            normal: {
                                position: 'inside',
                                fontSize: 30,
                                fontWeight: 700
                            }
                        }
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

    };

    var setitembilling = function (value, id) {
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
            series: [{
                name: 'chart',
                type: 'pie',
                selectedMode: 'single',
                radius: ['75%', '95%'],
                center: ['50%', '45%'],
                label: {
                    normal: {
                        show: true,
                        position: 'center',
                        fontSize: 70
                    },
                    emphasis: {
                        show: true,
                        textStyle: {
                            fontSize: '45',
                            fontWeight: 'bold'
                        }
                    }
                },
                labelLine: {
                    normal: {
                        show: true
                    }
                },
                data: [{
                    value: value,
                    name: value.toString(),
                    itemStyle: {
                        color: '#42c8f4'
                    }
                }]
            }

            ]
        };
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    };
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
    };
    var reload = function () {
        setInterval(function () {
            toastopstion();
            initdatareft();

        }, 120000);
    };

    //var alertflow = function (xvalue) {
    //    var $texts = $("#" + xvalue + "textfield .text");

    //    var textIndex = 0;

    //    function nextLine() {

    //        //Step 1: Fade in text
    //        $($texts[textIndex]).animate({ opacity: 1, "margin-top": 12 }, 500, "linear", function () {

    //            //Step 2: Fade out text
    //            setTimeout(function () {
    //                $($texts[textIndex]).animate({ opacity: 0, "margin-top": 20 }, 500, "linear", function () {
    //                    //Step 3: Reset current and call next text
    //                    $($texts[textIndex]).css({ "margin-top": 20 });
    //                    textIndex++;
    //                    textIndex %= $texts.length;
    //                    nextLine();

    //                });
    //            }, 3000);
    //        });

    //    }

    //    nextLine();
    //};



    return {
        init: function () {
            initdatareft();

        }
    };
}();


jQuery(document).ready(function () {
    vesselflow.init();
    $('#collapseOne').collapse("hide");
    $('#collapseTwo').collapse("hide");
    $('#collapseThree').collapse("hide");
});