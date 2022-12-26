$(document).ready(function () {
    RequestAjaxTerminal();
    setInterval(function () {
        RequestAjaxTerminal();
    }, 600000);
});

function RequestAjaxTerminal() {
    $.ajax({
        url: "/TerminalFlowAccumulate/TerminalList",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DrawDataTerminal(data);
        }
    });
}

function DrawDataTerminal(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_CABANG + "ContainerPermohonanS").text(data[i].PMH_CONTAINER_STACK);
        $("#" + data[i].NAMA_CABANG + "ContainerPermohonanL").text(data[i].PMH_CONTAINER_LOOS);
        $("#" + data[i].NAMA_CABANG + "ContainerPenetapanS").text(data[i].PTP_CONTAINER_STACK);
        $("#" + data[i].NAMA_CABANG + "ContainerPenetapanL").text(data[i].PTP_CONTAINER_LOOS);
        $("#" + data[i].NAMA_CABANG + "ContainerRealisasiS").text(data[i].REA_CONTAINER_STACK);
        $("#" + data[i].NAMA_CABANG + "ContainerRealisasiL").text(data[i].REA_CONTAINER_LOOS);
        $("#" + data[i].NAMA_CABANG + "ContainerPermohonanT").text(data[i].TOTAL_PMH_CONTAINER);
        $("#" + data[i].NAMA_CABANG + "ContainerPenetapanT").text(data[i].TOTAL_PTP_CONTAINER);
        $("#" + data[i].NAMA_CABANG + "ContainerRealisasiT").text(data[i].TOTAL_REA_CONTAINER);

        $("#" + data[i].NAMA_CABANG + "CargoPermohonanS").text(data[i].PMH_CARGO_STACK);
        $("#" + data[i].NAMA_CABANG + "CargoPermohonanL").text(data[i].PMH_CARGO_LOOS);
        $("#" + data[i].NAMA_CABANG + "CargoPenetapanS").text(data[i].PTP_CARGO_STACK);
        $("#" + data[i].NAMA_CABANG + "CargoPenetapanL").text(data[i].PTP_CARGO_LOOS);
        $("#" + data[i].NAMA_CABANG + "CargoRealisasiS").text(data[i].REA_CARGO_STACK);
        $("#" + data[i].NAMA_CABANG + "CargoRealisasiL").text(data[i].REA_CARGO_LOOS);
        $("#" + data[i].NAMA_CABANG + "CargoPermohonanT").text(data[i].TOTAL_PMH_CARGO);
        $("#" + data[i].NAMA_CABANG + "CargoPenetapanT").text(data[i].TOTAL_PTP_CARGO);
        $("#" + data[i].NAMA_CABANG + "CargoRealisasiT").text(data[i].TOTAL_REA_CARGO);

        $("#" + data[i].NAMA_CABANG + "Billing").text(data[i].NOTA);
    }
}