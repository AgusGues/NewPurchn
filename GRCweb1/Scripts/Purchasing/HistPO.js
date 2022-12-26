var strField;
var strValue;

function ShowHide() {
    var x = document.getElementById("Tbldetail");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
           
        }
        GetHistPO();
}

function GetHistPO() {
    strField = $("#ddlbarang").val();
    strValue = $("#txtcari").val();
    
    $.ajax({
        url: "HistoryPO.aspx/GetHistPO",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ strField: strField, strValue: strValue }),
        success: function (data) {
            
            $("#tableListPO").DataTable().destroy();
                $('#tableListPO').empty();
           
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListPO");
            oTblReport.DataTable({
                "data": datatable,
                //"responsive": true,
                "autoWidth": true,
                dom: 'Blfrtip',
                buttons: [
                    { extend: 'copy' },
                    { extend: 'csv' },
                    { extend: 'excel', title: 'History PO' },
                    { extend: 'pdf', title: 'History PO' },

                    {
                        extend: 'print',title :'History PO',
                        orientation: 'landscape',
                        pageSize: 'A3',
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');

                            $(win.document.body).find('table')
                                    .addClass('compact')
                                    .css('font-size', 'inherit');
                        }
                    }],
                "columns": [
                    {
                        "className": '',
                        "data": "POPurchnDate",
                        title: "PO IssueDate",
                        "render": function (data, type, row) {
                            var tgl = data;
                            var thn = tgl.substring(0, 4);
                            var bln = tgl.substring(5, 7);
                            var tgla = tgl.substring(8, 10);
                            return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                        }
                    },
                    {
                        "className": '',
                        "data": "DlvDate",
                        title: "Delivery Date",
                        "render": function (data, type, row) {
                            var tgl = data;
                            var thn = tgl.substring(0, 4);
                            var bln = tgl.substring(5, 7);
                            var tgla = tgl.substring(8, 10);
                            return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                        }
                    },
                    //{ "data": "POPurchnDate", title: "PO IssueDate" },
                    //{ "data": "DlvDate", title: "Delivery Date" },
                    { "data": "NoPO", title: "NoPO" },
                    { "data": "NoSPP", title: "NoSPP" },
                    { "data": "ItemCode", title: "ItemCode" },
                    { "data": "ItemName", title: "ItemName" },
                    { "data": "Satuan", title: "Satuan" },
                    { "data": "Price", title: "price" },
                    { "data": "CRC", title: "CRC" },
                    { "data": "Qty", title: "Qty" },
                    { "data": "SupplierName", title: "SupplierName" },
                    { "data": "Termin", title: "Term 0f Payment" },
                    { "data": "Delivery", title: "term of Delivery" },
                    { "data": "Disc", title: "Disc" },
                    { "data": "PPN", title: "PPN" },
                    { "data": "receiptno", title:"Reeceipt No"},
                    {
                        "className": '',
                        "data": "receiptdate",
                        title: "Receipt Date",
                        "render": function (data, type, row) {
                            var tgl = data;
                            var thn = tgl.substring(0, 4);
                            var bln = tgl.substring(5, 7);
                            var tgla = tgl.substring(8, 10);
                            return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                        }
                    },
                    { "data": "quantity", title: "Quantity"}
                ]
            });
        }
    });
}