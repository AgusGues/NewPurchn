$(document).ready(function () {
    GetHargaBarang();
});

var strField;
var strValue;


function GetHargaBarang() {
    $.ajax({
        url: "FormHistoryHargaBarang.aspx/GetHargaBarang",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //data: JSON.stringify({ strField: strField, strValue: strValue }),
        success: function (data) {

            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListHarga");
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,
                "columns": [
                    { "data": "ItemCode", title: "Kode Barang" },
                    { "data": "ItemName", title: "Nama Barang" },
                    { "data": "Price", title: "Harga" },
                    { "data": "SupplierName", title: "Supplier" }

                ]
            });
        }
    });
}

function GetHargaBarangByCriteria() {
    strField = $("#ddlbarang").val();
    strValue = $("#txtcari").val();
    $.ajax({
        url: "FormHistoryHargaBarang.aspx/GetHargaBarangByCriteria",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ strField: strField, strValue: strValue }),
        success: function (data) {

                $("#tableListHarga").DataTable().destroy();
                $('#tableListHarga').empty();
           
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListHarga");
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,
                "columns": [
                    { "data": "ItemCode", title: "Kode Barang" },
                    { "data": "ItemName", title: "Nama Barang" },
                    { "data": "Price", title: "Harga" },
                    { "data": "SupplierName", title: "Supplier" }
                    
                ]
            });
        }
    });
}