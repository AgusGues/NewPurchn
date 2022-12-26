var invoice, rata;
$(document).ready(function () {
    RequestAjaxCargoJasa();
    RequestAjaxCargoAlat();
    RequestAjaxContainer();
    RequestAjaxTotal();
    RequestAjaxWaitAppVer();
    setInterval(function () {
        RequestAjaxCargoJasa();
        RequestAjaxCargoAlat();
        RequestAjaxContainer();
        RequestAjaxTotal();
        RequestAjaxWaitAppVer();
    }, 600000);
});

function RequestAjaxWaitAppVer() {
    $.ajax({
        url: "/TerminalBilling/ListWaitAppVer",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            WaitAppVer(data);
        }
    });
}

function RequestAjaxCargoJasa() {
    $.ajax({
        url: "/TerminalBilling/ListJasaTOS",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DataJasaTos(data);
        }
    });
}

function RequestAjaxCargoAlat() {
    $.ajax({
        url: "/TerminalBilling/ListAlatTOS",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        async: false,
        success: function (data) {
            DataAlatTos(data);
        }
    });
}

function RequestAjaxContainer() {
    $.ajax({
        url: "/TerminalBilling/ListSpinner",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DataSpin(data);
        }
    });
}

function RequestAjaxTotal() {
    $.ajax({
        url: "/TerminalBilling/ListTotal",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            DataTotal(data);
        },
    });
}

function DataJasaTos(data) {
    var dibawah, antara, diatas;
    for (var i = 0; i < data.length; i++) {
        if (data[i].TOTAL != 0) {
            dibawah = data[i].DIBAWAH / data[i].TOTAL * 100;
            antara = data[i].ANTARA / data[i].TOTAL * 100;
            diatas = data[i].DIATAS / data[i].TOTAL * 100;
        } else {
            dibawah = 0;
            antara = 0;
            diatas = 0;
        }
        $("#" + data[i].NAMA_TERMINAL + "d").text(dibawah.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "e").text(antara.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "c").text(diatas.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "i").text(data[i].TOTAL);
    }
}

function DataAlatTos(data) {
    var dibawah, antara, diatas;
    for (var i = 0; i < data.length; i++) {
        if (data[i].TOTAL != 0) {
            dibawah = data[i].DIBAWAH / data[i].TOTAL * 100;
            antara = data[i].ANTARA / data[i].TOTAL * 100;
            diatas = data[i].DIATAS / data[i].TOTAL * 100;
        } else {
            dibawah = 0;
            antara = 0;
            diatas = 0;
        }
        $("#" + data[i].NAMA_TERMINAL + "dt").text(dibawah.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "et").text(antara.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "ct").text(diatas.toFixed() + "%");
    }
}

function DataSpin(data) {
    var dibawah, antara, diatas;
    for (var i = 0; i < data.length; i++) {
        if (data[i].TOTAL != 0) {
            dibawah = data[i].DIBAWAH / data[i].TOTAL * 100;
            antara = data[i].ANTARA / data[i].TOTAL * 100;
            diatas = data[i].DIATAS / data[i].TOTAL * 100;
        } else {
            dibawah = 0;
            antara = 0;
            diatas = 0;
        }
        $("#" + data[i].NAMA_TERMINAL + "dc").text(dibawah.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "ec").text(antara.toFixed() + "%");
        $("#" + data[i].NAMA_TERMINAL + "cc").text(diatas.toFixed() + "%");
    }
}

function DataTotal(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_TERMINAL + "i").text(data[i].TOTAL);
        $("#" + data[i].NAMA_TERMINAL + "r").text(data[i].RATA);
    }
}

function WaitAppVer(data) {
    for (var i = 0; i < data.length; i++) {
        $("#" + data[i].NAMA_TERMINAL + "v").text(data[i].VERIFICATION_CARGO);
        $("#" + data[i].NAMA_TERMINAL + "a").text(data[i].APPROVE_JASA);
        $("#" + data[i].NAMA_TERMINAL + "at").text(data[i].APPROVE_ALAT);
        $("#" + data[i].NAMA_TERMINAL + "vc").text(data[i].VERIFICATION);
        $("#" + data[i].NAMA_TERMINAL + "ac").text(data[i].APPROVE);
    }
}