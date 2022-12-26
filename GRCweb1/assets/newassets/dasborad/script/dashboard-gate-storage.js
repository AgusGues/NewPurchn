$(document).ready(function () {
    RequestAjaxProgContainer();
    RequestAjaxProgCargo();
    RequestAjaxGateInOut();
    RequestAjaxEquipment();
    setInterval(function () {
        RequestAjaxProgContainer();
        RequestAjaxProgCargo();
        RequestAjaxGateInOut();
        RequestAjaxEquipment();
    }, 600000);
});

function RequestAjaxProgContainer() {
    $.ajax({
        url: "/Gate/ProgContainer",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            ProgContainer(data);
        }
    });
}

function RequestAjaxProgCargo() {
    $.ajax({
        url: "/Gate/ProgCargo",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            ProgCargo(data);
        }
    });
}

function RequestAjaxGateInOut() {
    $.ajax({
        url: "/Gate/GateInOut",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            GateInOut(data);
        }
    });
}

function RequestAjaxEquipment() {
    $.ajax({
        url: "/Gate/GetDataEquipment",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            Equipment(data);
        }
    });
}

function ProgContainer(data) {
    var Percent, text, ContainerUsed, ContainerReady;
    for (var i = 0; i < data.length; i++) {
        if (data[i].USED != 0) {
            ContainerReady = data[i].READY / data[i].TOTAL * 100;
            ContainerUsed = data[i].USED / data[i].TOTAL * 100;
            Percent = ContainerUsed.toFixed() + "%";
        }
        else {
            ContainerReady = 0;
            ContainerUsed = 0;
            Percent = 0 + "%";
        }
        text = '<div class="progress-bar progress-bar-striped progress-bar-animated bg-danger" role="progressbar" aria-valuenow="' + data[i].USED + '" aria-valuemin="0" aria-valuemax="' + data[i].TOTAL + '" style="width: ' + Percent + ';"></div>';
        $("#" + data[i].NAMA_TERMINAL + "coa").text(ContainerUsed.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "cob").text(ContainerReady.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "co").html(text);
    }
}

function ProgCargo(data) {
    var CargoReady, CargoUsed, CargoLReady, CargoLUsed, PercentCargo, PercentCargoL, Kapasitas, KapasitasL;
    for (var i = 0; i < data.length; i++) {
        if (data[i].MGLAP_JENIS == "GUDANG") {
            if (data[i].JML_TPK != 0) {
                CargoReady = (data[i].TOTAL_KAPASITAS - data[i].JML_TPK) / data[i].TOTAL_KAPASITAS * 100;
                CargoUsed = data[i].JML_TPK / data[i].TOTAL_KAPASITAS * 100;
                PercentCargo = CargoUsed + "%";
            } else {
                CargoReady = 0;
                CargoUsed = 0;
                PercentCargo = 0 + "%";
            }
            textc = '<div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="' + CargoUsed + '" aria-valuemin="0" aria-valuemax="' + data[i].TOTAL_KAPASITAS + '" style="width: ' + PercentCargo + ';"></div>';
            $("#" + data[i].NAMA_TERMINAL + "caa").text(CargoUsed.toFixed() + "%");
            $("#" + data[i].NAMA_TERMINAL + "cab").text(CargoReady.toFixed() + "%");
            $("#" + data[i].NAMA_TERMINAL + "ca").html(textc);
        } else if (data[i].MGLAP_JENIS == "LAPANGAN") {
            CargoLReady = (data[i].TOTAL_KAPASITAS - data[i].JML_TPK) / data[i].TOTAL_KAPASITAS * 100;
            CargoLUsed = data[i].JML_TPK / data[i].TOTAL_KAPASITAS * 100;
            PercentCargoL = CargoLUsed + "%";
            textl = '<div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuenow="' + CargoLUsed + '5" aria-valuemin="0" aria-valuemax="' + data[i].TOTAL_KAPASITAS + '" style="width:' + PercentCargoL + ';"></div>';
            $("#" + data[i].NAMA_TERMINAL + "caya").text(CargoLUsed.toFixed() + "%");
            $("#" + data[i].NAMA_TERMINAL + "cayb").text(CargoLReady.toFixed() + "%");
            $("#" + data[i].NAMA_TERMINAL + "cay").html(textl);
        }
    }
}

function GateInOut(data) {
    var Gatein, Working, Gateout;
    for (var i = 0; i < data.length; i++) {
        Gatein = data[i].GATE_IN;
        Gateout = data[i].GATE_OUT;
        Working = data[i].WORKING;
        $("#" + data[i].NAMA_TERMINAL + "i").text(Gatein);
        $("#" + data[i].NAMA_TERMINAL + "o").text(Gateout);
        $("#" + data[i].NAMA_TERMINAL + "w").text(Working);
    }
}

function Equipment(data) {
    var Down, Work, Standby;
    for (var i = 0; i < data.length; i++) {
        Down = data[i].DOWN;
        Standby = data[i].STAND_BY;
        Work = data[i].WORK;
        $("#" + data[i].NAMA_TERMINAL + "down").text(Down);
        $("#" + data[i].NAMA_TERMINAL + "work").text(Work);
        $("#" + data[i].NAMA_TERMINAL + "standby").text(Standby);
    }
}