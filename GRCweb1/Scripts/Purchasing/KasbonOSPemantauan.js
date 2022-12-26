$(document).ready(function () {
    $('.input-daterange').datepicker({ autoclose: true });
    RequestListPIC();
    //InitDropdownprog();
});

var drTgl;
var sdTgl;
var Criteria="";

function ShowHide() {
    var x = document.getElementById("Tbldetail");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {

    }
    Getkasbon();
}

function GetKasbon() {
    tgl = $("#Start").val();
    tgl2 = $("#End").val();
    ddlCriteria = $("#ddlpic").val();

    if (tgl > tgl2) {
        alert("Tanggal Periode Salah");
    }
    else {
        //window.alert("Bisa")
        var x = document.getElementById("Tbldetail");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {

        }

        //var tgl = data;
        var thna = tgl.substring(6, 10);
        var blna = tgl.substring(0, 2);
        var tgla = tgl.substring(3, 5);
        drTgl = thna + blna + tgla;

        var thnb = tgl2.substring(6, 10);
        var blnb = tgl2.substring(0, 2);
        var tglb = tgl2.substring(3, 5);
        sdTgl = thnb + blnb + tglb;
        
        if (ddlCriteria != "") {
            Criteria = "and k.Pic=" + "'" + ddlCriteria + "'"
        }
        
        $.ajax({
            url: "KasbonOSPemantauan.aspx/GetKasbon",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ drTgl: drTgl, sdTgl: sdTgl, Criteria: Criteria }),
            success: function (data) {

                $("#tableLap").DataTable().destroy();
                $('#tableLap').empty();

                datatable = $.parseJSON(data.d);
                var oTblReport = $("#tableLap");
                oTblReport.DataTable({
                    "data": datatable,
                    "responsive": true,
                    "autoWidth": true,
                    dom: 'Blfrtip',
                    buttons: [
                        { extend: 'copy' },
                        { extend: 'csv' },
                        { extend: 'excel', title: 'PEMANTAUAN OUTSTANDING KASBON', message: 'periode ' + tgl + ' sampai ' + tgl2 + ' ' },
                        { extend: 'pdf', title: 'PEMANTAUAN OUTSTANDING KASBON',  message: 'periode ' + tgl + ' sampai ' + tgl2 + ' '},

                        {
                            extend: 'print', title: 'PEMANTAUAN OUTSTANDING KASBON',
                            message: 'periode '+tgl+' sampai '+tgl2+' ',
                            //customize: function (win) {
                            //    $(win.document.body).addClass('white-bg');
                            //    $(win.document.body).css('font-size', '10px');

                            //    $(win.document.body).find('table')
                            //            .addClass('compact')
                            //            .css('font-size', 'inherit');
                            //}
                        }],
                    "columns": [
                        { "data": "NoPengajuan", title: "No Pengajuan" },
                        { "data": "KasbonNo", title: "No Kasbon" },
                        {
                            "className": '',
                            "data": "TglKasbon",
                            title: "Tanggal",
                            "render": function (data, type, row) {
                                var tgl = data;
                                var thn = tgl.substring(0, 4);
                                var bln = tgl.substring(5, 7);
                                var tgla = tgl.substring(8, 10);
                                return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                            }
                        },
                        { "data": "NoSPP", title: "No SPP" },
                        { "data": "ItemName", title: "Item Name" },
                        { "data": "UOMDesc", title: "Satuan" },
                        { "data": "Qty", title: "Qty" },
                        { "data": "EstimasiKasbon", title: "Estimasi" },
                        { "data": "Total", title: "Total" }
                    ]
                });
            }
        });
    }
}

function RequestListPIC() {
    $.ajax({
        url: "KasbonOSPemantauan.aspx/GetPIC",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddlpic").append('<option value="' + value.UserName+ '" >' + value.UserName + '</option>');
            });
        }
    });
}
