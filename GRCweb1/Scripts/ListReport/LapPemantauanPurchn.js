$(document).ready(function () {
    $('.input-daterange').datepicker({ autoclose: true });
});


var drtgl;
var sdtgl;

function GetLapPurchn() {
    tgl = $("#Start").val();
    tgl2 = $("#End").val();
    
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
        drtgl = thna + blna + tgla;

        var thnb = tgl2.substring(6, 10);
        var blnb = tgl2.substring(0, 2);
        var tglb = tgl2.substring(3, 5);
        sdtgl = thnb + blnb + tglb;


        $.ajax({
            url: "LapPemantauanPurchn.aspx/GetLapPurchn",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ drtgl: drtgl, sdtgl: sdtgl }),
            success: function (data) {

                $("#tableLap").DataTable().destroy();
                $('#tableLap').empty();

                datatable = $.parseJSON(data.d);
                var oTblReport = $("#tableLap");
                oTblReport.DataTable({
                    "data": datatable,
                    "language": {
                        "emptyTable": "Data tidak ditemukan"
                    },
                    //"responsive": true,
                    //"autoWidth": true,
                    dom: 'Bfrtip',
                    buttons: [
                        { extend: 'copy' },
                        { extend: 'csv' },
                        { extend: 'excel', title: 'Laporan Pemantauan Purchn' },
                        { extend: 'pdf', title: 'Laporan Pemantauan Purchn' },

                        {
                            extend: 'print', title: 'Laporan Pemantauan Purchn',
                            message: 'periode '+tgl+' sampai '+tgl2+' ',
                            customize: function (win) {
                                $(win.document.body).addClass('white-bg');
                                $(win.document.body).css('font-size', '10px');

                                $(win.document.body).find('table')
                                        .addClass('compact')
                                        .css('font-size', 'inherit');
                            }
                        }],
                    "columns": [
                        { "data": "NoSPP", title: "No SPP" },
                        {
                            "className": '',
                            "data": "TglSPP",
                            title: "Tanggal SPP",
                            "render": function (data, type, row) {
                                var tgl = data;
                                var thn = tgl.substring(0, 4);
                                var bln = tgl.substring(5, 7);
                                var tgla = tgl.substring(8, 10);
                                return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                            }
                        },
                        { "data": "ApprovalSPP", title: "ApprovalSPP" },
                        { "data": "NoSPP", title: "NoSPP" },
                        { "data": "NamaBarang", title: "Nama Barang" },
                        { "data": "KodeBarang", title: "Kode Barang" },
                        { "data": "Satuan", title: "Satuan" },
                        { "data": "JumlahSPP", title: "JumlahSPP" },
                        { "data": "UserName", title: "UserName" },
                        { "data": "NoPO", title: "No PO" },
                        { "data": "TglPO", title: "TglPO" },
                        { "data": "TglApprovalPO", title: "Tgl Approval PO" },
                        { "data": "JumlahPO", title: "Jumlah PO" },
                        { "data": "SisaSPP", title: "Sisa SPP" },
                        { "data": "Indent", title: "Indent" },
                        { "data": "NoReceipt", title: "NoReceipt" },
                        { "data": "StatusReceipt", title: "Status Receipt" }

                    ]
                });
            }
        });
    }
}