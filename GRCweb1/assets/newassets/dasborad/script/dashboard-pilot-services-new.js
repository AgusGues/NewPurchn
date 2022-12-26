$(document).ready(function () {
    RequestTuggingServices();
    RequestPilotWork();
    RequestVesselProg();
    RequestWTP();
    RequestPilotWeek();
    RequestTugging();
    RequestVessel();
    setInterval(function () {
        RequestTuggingServices();
        RequestPilotWork();
        RequestVesselProg();
        RequestWTP();
        RequestPilotWeek();
        RequestTugging();
        RequestVessel();
    }, 600000);
});

var mTuggingService = [];
var all = 0;

function RequestTuggingServices() {
    $.ajax({
        url: "/PilotServices/listtuggingservices",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawData(data);
        }
    });
}

function RequestVessel() {
    $.ajax({
        url: "/PilotServices/Vessel",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            $("#vessel").text(data);
        }
    });
}

function RequestVesselProg() {
    $.ajax({
        url: "/PilotServices/vesselprog",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawVesselProgress(data);
        }
    });
}

function RequestVesselProgNew() {
    $.ajax({
        url: "/PilotServices/vesselprogNew",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            $("#blm-yesterday").text(data[0].BELUM_PENETAPAN_YESTERDAY);
            $("#blm-tomorow").text(data[0].BELUM_PENETAPAN_TOMORROW);
        }
    });
}

function RequestPilotWork() {
    $.ajax({
        url: "/PilotServices/pilotwork",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawGerakanPandu(data);
        }
    });
}

function RequestWTP() {
    $.ajax({
        url: "/PilotServices/wtpservices",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawWTP(data);
        }
    });
}

function RequestPilotWeek() {
    $.ajax({
        url: "/PilotServices/pilotofweekitem",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawPanduWeek(data);
        }
    });
}

function RequestTugging() {
    $.ajax({
        url: "/PilotServices/Pilotservices",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            FadeOut();
            DrawTugging(data);
        }
    });
}

function DrawData(data) {
    localStorage.removeItem('dataTuggingServices');
    localStorage.setItem('dataTuggingServices', data);
    var total = 0;
    mTuggingService = [];
    for (var i = 0; i < data.length; i++) {
        var kawasan = data[i].NAMA_KAWASAN;
        total += data[i].TOTAL;
        $("#avalibeltugging").text(total);
        $("#" + data[i].NAMA_KAWASAN + "od").text(data[i].ON_DUTY);
        $("#" + data[i].NAMA_KAWASAN + "s").text(data[i].STAND_BY);
        $("#" + data[i].NAMA_KAWASAN + "of").text(data[i].OFF_DUTY);
        $.ajax({
            url: "/PilotServices/GetTglOff",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: { NAMA_KAWASAN: kawasan },
            success: function (res) {
                mTuggingService.push(res);
            }
        });
    }
}

function toDateFromJson(src) {
    return new Date(parseInt(src.substr(6)));
}



setInterval(function () {
    for (var i = 0; i < mTuggingService.length; i++) {
        drawCountDown(mTuggingService[i]);
    }
}, 1000);

function drawCountDown(param) {
    listdata = param;

    var strRes = '';
    var kawasan = '';

    for (var i = 0; i < listdata.length; i++) {
        var data = listdata[i];
        var hour = execCountDown(data);
        if (hour > 0) {
            strRes = strRes + hour;
            if (listdata.length - 1 != i)
                strRes = strRes + ", ";
        }
        kawasan = data.NAMA_KAWASAN;
    }

    $('#' + kawasan + "cd").html(strRes);
}

function execCountDown(data) {
    var tglSkrg = new Date();
    var tglMulai = toDateFromJson(data.TGL_MULAI);
    var tglSelesai = toDateFromJson(data.TGL_SELESAI);

    var hour = 0;
    var mili = tglSelesai - tglSkrg;
    mili = mili / 1000;

    while (true) {
        if (mili >= 3600) {
            hour++;
            mili = mili - 3600;
        } else if (mili > 60) {
            hour++;
            break;
        } else {
            break;
        }
    }

    return hour;
}

function DrawGerakanPandu(data) {
    localStorage.removeItem('dataPilotWork');
    localStorage.setItem('dataPilotWork', data);
    var total = 0;
    all = 0;
    for (var i = 0; i < data.length; i++) {
        all = all + data[i].TOTAL;
        $("#" + data[i].AREA_NAME + "total").text(data[i].TOTAL);
        $("#" + data[i].AREA_NAME + "arr").text(data[i].ARRIVAL);
        $("#" + data[i].AREA_NAME + "shif").text(data[i].SHIFTING);
        $("#" + data[i].AREA_NAME + "dep").text(data[i].DEPARTURE);
        $("#" + data[i].AREA_NAME + 'avajamuang').text(data[i].AVAILABLE_JAMUANG);
        $("#" + data[i].AREA_NAME + 'ava').text(data[i].AVAILABLE);
    }
    $('#allpilot').text(all);
}

function DrawVesselProgress(data) {
    localStorage.removeItem('dataVesselProg');
    localStorage.setItem('dataVesselProg', data);
    $("#blm").text(data[0].BELUM_PENETAPAN);
    $("#approve").text(data[0].APPROVE);
    $("#spk").text(data[0].SPK);
    $("#ohn").text(data[0].OHN);
    $("#pob").text(data[0].POB);
}

function DrawTugging(data) {
    localStorage.removeItem('dataTugging');
    localStorage.setItem('dataTugging', data);
    var listrow = "";
    var rows = "";
    $.each(data.header, function (i, item) {
        $.each(data.data, function (s, items) {
            if (items.KD_LOKASI == item.KD_LOKASI) {
                rows += '<div class="col"> ' +
                    '<label class="fs-20 fw-700">' + items.STATUS_NAMA + '</label>' +
                    '<p class="fs-35 fw-700 p-2">' + items.JUMLAH + '</p>' +
                    '</div>';
            }
        });
        rows += '</div>';
        listrow += rows;
    });

    $('#content-tugging').html(listrow);
    Tugg(data.tugg);
    FadeIn();
}

function Tugg(data) {
    localStorage.removeItem('dataTugging');
    localStorage.setItem('dataTugging', data);
    var header = '<thead class="fs-30">' +
        '<tr>' +
        ' <th class="my-auto" style="/*vertical-align: center;line-height: 90px*/">Jam</th>';
    $.each(data.header, function (i, itmss) {
        header += '<th>' + itmss.NAMA_KAWASAN + '</th>'
    });
    header += '</tr> </thead>';
    var tbody = '<tbody class="fs-40 ">';
    $.each(data.jam, function (x, itms2) {
        tbody += '<tr>' + '<td>' + itms2 + '</td>';

        $.each(data.data, function (c, citems) {
            tbody += '<td><span class="badge badge-pill ' + rederwarnig(citems[x].BLINK) + '">' + citems[x].JUMLAH_KPL_TUNDA + '</span></td>';
        });
        tbody += '</tr>';
    });
    tbody += '</tbody>';

    $('#tug-data').html(header + tbody);

    function rederwarnig(c) {
        if (c == 1) {
            return 'badge-danger  blink'
        } else {
            return 'badge-success';
        }
    }
}

function FadeIn() {
    FadeOut();
    setTimeout(function () {
        $('.animation-demo').dreyanim({
            animationType: 'wipeInVertical',
            animationTime: 2000
        });
    }, 2000);
}

function FadeOut() {
    $('.animation-demo').dreyanim({
        animationType: 'wipeOutVertical',
        animationTime: 2000
    });
}

function DrawWTP(data) {
    localStorage.removeItem('dataWTP');
    localStorage.setItem('dataWTP', data);
    var dom = document.getElementById("psf");
    var myChart = echarts.init(dom);
    var app = {};
    option = null;
    option = {
        tooltip: {
            formatter: "{a} <br/>{b} : {c}%"
        },
        series: [{
            name: 'Pilot Service Performance',
            type: 'gauge',
            detail: {
                formatter: '{value} minutes'
            },
            data: [{
                value: data.WTP,
            }],
            startAngle: 180,
            endAngle: 0,
            axisLine: {
                lineStyle: {
                    width: 110,
                    color: [
                        [(data.EXCELLENT / 100), '#50D850'],
                        [(data.FAIR / 100), '#00C8FF'],
                        [(data.POOR / 100), '#ED4D49']
                    ]
                }
            },
            axisTick: {
                splitNumber: 10,
                length: 5
            },
            axisLabel: {
                formatter: function (a) {
                    switch (a + "") {
                        case "10":
                            return "EXCELLENT";
                        case "50":
                            return "FAIR";
                        case "90":
                            return "POOR";
                        default:
                            return ""
                    }
                },
                textStyle: {
                    color: "#fff",
                    fontSize: 12,
                    fontWeight: "bold"
                }
            },
            pointer: {
                width: 30,
                length: "70%",
                color: "#FFF"
            },
            detail: {
                show: true,
                borderWidth: 0,
                fontSize: 25,
                formatter: "{value} minutes",
                offsetCenter: [5, -40],
            },
            itemStyle: {
                color: true,
                opacity: 0.8
            },
            radius: '180%',
            center: ['50%', '100%']
        }]
    };

    if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
}

function DrawPanduWeek(data) {
    localStorage.removeItem('dataPilotWeek');
    localStorage.setItem('dataPilotWeek', data);
    $('#namep').html(data.NAMA_PETUGAS);
    $('#nipp').html(data.NIP_PANDU);
    $('#util').html(data.UTILITAS);
    $('#gerakan').html(data.GERAKAN);
}