$(document).ready(function () {
    GetArmada();
    GetUser();
});
$('#txtStart').datetimepicker({
    timepicker: true,
    format: 'd-M-Y H:i',
    formatDate: 'd-M-Y',
});

$('#txtFinish').datetimepicker({
    timepicker: true,
    format: 'd-M-Y H:i',
    formatDate: 'd-M-Y'
});


function durasi() {
    var sta = $("#txtStart").val();
    var fin = $("#txtFinish").val();
    var start = sta.substr(sta.length - 5);
    var end = fin.substr(fin.length - 5);
    start = start.split(":");
    end = end.split(":");
    var starth = sta.substring(0, sta.indexOf(' '));
    var endh = fin.substring(0, fin.indexOf(' '));
    starth = starth.split("-");
    endh = endh.split("-");
    var sbln = 0;
    var fbln = 0;
    switch (starth[1]) {
        case "Jan": sbln = 0; break; case "Feb": sbln = 1; break; case "Mar": sbln = 2; break; case "Apr": sbln = 3; break; case "Mei": sbln = 4; break; case "Jun": sbln = 6; break;
        case "Jul": sbln = 6; break; case "Aug": sbln = 7; break; case "Sep": sbln = 8; break; case "Okt": sbln = 9; break; case "Nov": sbln = 10; break; case "Des": sbln = 12; break;
    }
    switch (endh[1]) {
        case "Jan": fbln = 0; break; case "Feb": fbln = 1; break; case "Mar": fbln = 2; break; case "Apr": fbln = 3; break; case "Mei": fbln = 4; break; case "Jun": fbln = 6; break;
        case "Jul": fbln = 6; break; case "Aug": fbln = 7; break; case "Sep": fbln = 8; break; case "Okt": fbln = 9; break; case "Nov": fbln = 10; break; case "Des": fbln = 12; break;
    }
    var startDate = new Date(starth[2], sbln, starth[0], start[0], start[1], 0);
    var endDate = new Date(endh[2], fbln , endh[0], end[0], end[1], 0);

    if (startDate > endDate) {
        alert("Tanggal dan jam mulai tidak boleh lebih besar dari tanggal dan jam selesai !");
        $("#txtFinish").val("");
    } else {
        var diff = endDate.getTime() - startDate.getTime();
        var hours = Math.floor(diff / 1000 / 60 / 60);
        //diff -= hours * 1000 * 60 * 60;
        var minutes = Math.floor(diff / 1000 / 60);
        $("#txtTotal").val(minutes);
    }
    //var total = finish - start;
}

function GetArmada() {
    $.ajax({
        url: "BDTArmada.aspx/GetArmada",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddlArmada").append('<option value="' + value.NamaKendaraan + '" >' + value.NamaKendaraan + '</option>');
            });
        }
    });
}
function GetUser() {
    $.ajax({
        url: "BDTArmada.aspx/GetUser",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            user = data;

        }
    });
}
function Simpan() {
    obj = {};
    obj.Tanggal = $("#txtTglBA").val();
    obj.NamaUnit = $("#ddlArmada").val();
    obj.TglStart = $("#txtStart").val();
    obj.TglFinish = $("#txtFinish").val();
    obj.TotalTime = $("#txtTotal").val();
    obj.Kendala = $('#txtKendala').val();
    obj.Perbaikan = $('#txtPerbaikan').val();
    obj.Keterangan = $('#txtKeterangan').val();
    obj.CreatedBy = user;
    if (obj.Tanggal == "" || obj.NamaUnit == "" || obj.TglStart == "" || obj.TotalTime == "" || obj.TotalTime == "") {
        alert("Tidak boleh kosong !");
    }
    $.ajax({
        url: 'BDTArmada.aspx/Simpan',
        type: 'POST',
        data: JSON.stringify({ obj: obj }),
        contentType: "application/json; charset=utf-8",
        dataType: 'text',
        success: function (data) {
            console.log(data);
            alert("Data Berhasil disimpan");
            window.location.reload();
        },
        error: function (errorText) {
            alert("Data Gagal disimpan");
        }
    });

}