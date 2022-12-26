$(document).ready(function () {
    Getforklift();
    GetUser();
    let today = new Date().toLocaleDateString()
    $('#txtTanggal').datepicker('setDate', today);
    //$('#txtTanggal').datepicker({ dateformat: 'dd-mm-yyyy' }); // format to show

});
$('#txtStart').datetimepicker({
    timepicker: true,
    format: 'd/m/Y H:i',
    formatDate: 'd/m/Y',
});

$('#txtFinish').datetimepicker({
    timepicker: true,
    format: 'd/m/Y H:i',
    formatDate: 'd/m/Y'
});

var user;
function GetUser() {
    $.ajax({
        url: "BreakdownForklift.aspx/GetUser",
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
function durasi() {
    var sta = $("#txtStart").val();
    var fin = $("#txtFinish").val();
    var start = sta.substr(sta.length - 5);
    var end = fin.substr(fin.length - 5);
    start = start.split(":");
    end = end.split(":");
    var starth = sta.substring(0, sta.indexOf(' '));
    var endh = fin.substring(0, fin.indexOf(' '));
    starth = starth.split("/");
    endh = endh.split("/");
    var startDate = new Date(starth[2],starth[1],starth[0], start[0], start[1], 0);
    var endDate = new Date(endh[2],endh[1],endh[0], end[0], end[1], 0);
    
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

function Getforklift() {
    $.ajax({
        url: "BreakdownForklift.aspx/GetForklift",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddlforklift").append('<option value="' + value.Forklift + '" >' + value.Forklift + '</option>');
            });
        }
    });
}

function Simpan() {
    obj = {};
    obj.Tanggal = $("#txtTanggal").val();
    obj.Forklift = $("#ddlforklift").val();
    obj.Start = $("#txtStart").val();
    obj.Finish = $("#txtFinish").val();
    obj.Total = $("#txtTotal").val();
    obj.Kendala = $("#txtkendala").val();
    obj.Perbaikan = $("#txtperbaikan").val();
    obj.Keterangan = $("#txtketerangan").val();
    obj.Users = user;
    if (obj.Tanggal == "" || obj.Forklift == "" || obj.Start == "" || obj.Finish == "" || obj.Total == ""||obj.Kendala=="") {
        alert("Tidak boleh kosong !");
    }
    $.ajax({
        url: 'BreakdownForklift.aspx/Simpan',
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
