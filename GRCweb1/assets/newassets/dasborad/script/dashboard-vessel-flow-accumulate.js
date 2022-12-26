$(document).ready(function () {
    RequestAjaxPilot();
    RequestAjaxBerth();
    RequestAjaxBilling();
    setInterval(function () {
        RequestAjaxPilot();
        RequestAjaxBerth();
        RequestAjaxBilling();
    }, 600000);
});

function RequestAjaxPilot() {
    $.ajax({
        url: "/VesselFlowAccumulate/PilotList",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawDataPilot(data);
        }
    });
}

function RequestAjaxBerth() {
    $.ajax({
        url: "/VesselFlowAccumulate/BerthList",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawDataBerth(data);
        }
    });
}

function RequestAjaxBilling() {
    $.ajax({
        url: "/VesselFlowAccumulate/Billing",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawDataBilling(data);
        }
    });
}

function DrawDataPilot(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].KAWASAN + "PilotPermohonanA").text(data[i].PERMOHONAN_ARRIVAL);
        $("#" + data[i].KAWASAN + "PilotPermohonanS").text(data[i].PERMOHONAN_SHIFTING);
        $("#" + data[i].KAWASAN + "PilotPermohonanD").text(data[i].PERMOHONAN_DEPARTURE);
        $("#" + data[i].KAWASAN + "PilotPenetapanA").text(data[i].PENETAPAN_ARRIVAL);
        $("#" + data[i].KAWASAN + "PilotPenetapanS").text(data[i].PENETAPAN_SHIFTING);
        $("#" + data[i].KAWASAN + "PilotPenetapanD").text(data[i].PENETAPAN_DEPARTURE);
        $("#" + data[i].KAWASAN + "PilotRealisasiA").text(data[i].REALISASI_ARRIVAL);
        $("#" + data[i].KAWASAN + "PilotRealisasiS").text(data[i].REALISASI_SHIFTING);
        $("#" + data[i].KAWASAN + "PilotRealisasiD").text(data[i].REALISASI_DEPARTURE);
        $("#" + data[i].KAWASAN + "PilotPermohonanT").text(data[i].TOTAL_PERMOHONAN);
        $("#" + data[i].KAWASAN + "PilotPenetapanT").text(data[i].TOTAL_PENETAPAN);
        $("#" + data[i].KAWASAN + "PilotRealisasiT").text(data[i].TOTAL_REALISASI);
    }
}

function DrawDataBerth(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].KAWASAN + "BerthPermohonanB").text(data[i].PERMOHONAN_BERTH);
        $("#" + data[i].KAWASAN + "BerthPermohonanE").text(data[i].PERMOHONAN_EXTENSION);
        $("#" + data[i].KAWASAN + "BerthPenetapanB").text(data[i].PENETAPAN_BERTH);
        $("#" + data[i].KAWASAN + "BerthPenetapanE").text(data[i].PENETAPAN_EXTENSION);
        $("#" + data[i].KAWASAN + "BerthRealisasiB").text(data[i].REALISASI_BERTH);
        $("#" + data[i].KAWASAN + "BerthRealisasiE").text(data[i].REALISASI_EXTENSION);
        $("#" + data[i].KAWASAN + "BerthPermohonanT").text(data[i].TOTAL_PERMOHONAN);
        $("#" + data[i].KAWASAN + "BerthPenetapanT").text(data[i].TOTAL_PENETAPAN);
        $("#" + data[i].KAWASAN + "BerthRealisasiT").text(data[i].TOTAL_REALISASI);
    }
}

function DrawDataBilling(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].KAWASAN + "Billing").text(data[i].BILLING);
    }
}