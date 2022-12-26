var isPertamapo = true;
var oTblReport;
var total, dis, ppn, grandtotal;
var ID;
$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestPOOpen();
});
function RequestPOOpen() {
    $.ajax({
        url: "ApprovePO.aspx/GetListOpenPO",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (!isPertamapo) {
                $("#tableListopenpo").DataTable().destroy();
                $('#tableListopenpo').empty();
            } else {
                isPertamapo = false;
            }
            datatable = $.parseJSON(data.d);
            oTblReport = $("#tableListopenpo").DataTable({
                "data": datatable,
                "pageLength": 5,
                "scrollX": true,
                "fixedHeader": true,
                "columns": [

                    {
                        'className': 'details-control',
                        'orderable': false,
                        'data': null,
                        'defaultContent': '',
                        'render': function (data, type, full, meta) {
                            var aksi = "<i class='fa fa-check'></i>";
                            return aksi;
                        },
                        'createdCell': function (td, cellData, rowData, row, col) {
                            $(td).attr('id', 'td_details' + row);
                        }
                    },
                    { "data": "ID", title: "ID" },
                    { "data": "NoPO", title: "No PO" },
                    { "data": "SupplierName", title: "Supplier" },
                    { "data": "UP", title: "UP Supplier" },
                    { "data": "Termin", title: "Term Of Payment" },
                    { "data": "Delivery", title: "Term Of Delivery" },
                    { "data": "PPN", title: "PPN" },
                    { "data": "PPH", title: "PPH" },
                    { "data": "MataUang", title: "Mata Uang" },
                    { "data": "UangMuka", title: "Uang Muka" },
                    { "data": "Disc", title: "Discount" },
                    { "data": "Remark", title: "Remark" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-xs btn-success' type='button' onclick='ApprovePO(" + row.ID + ")'><i class='fa fa-check'></i> Approve </button> " +
                                "<button class = 'btn btn-xs btn-danger' type='button' onclick='InputAlasan(" + row.ID + ",\"" + row.NoPO + "\")'><i class='fa fa-times'></i> Not Approve </button> ";
                            return aksi;
                        }
                    }
                ]
            });

            //Proses agar child table expand all saat load
            oTblReport.rows().every(function (rowIdx, tableLoop, rowLoop) {
                this.child(format(this.data())).show();
                this.nodes().to$().addClass('shown');
            });
            
            //Proses agar child table expand all saat load
            //oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
            //$('selector', oTblReport.rows().nodes());
            //oTblReport.rows().nodes().to$().find('selector');

        }
    });
}

//$('#tableListopenpo').on('click', 'td.details-control', function () {
//    var tr = $(this).closest('tr');
//    var row = oTblReport.row(tr);

//    if (row.child.isShown()) {
//        //Proses kalau expand terbuka maka tutup expand
//        row.child.hide();
//        tr.removeClass('shown');
//    }
//    else {
//        //Proses buka expand
//        row.child(format(row.data())).show();
//        tr.addClass('shown');
//    }
//});

function format(d) {
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'IDR',
        minimumFractionDigits: 2
    });
    var html;
    var arrtotal = [];
    $.ajax({
        url: "ApprovePO.aspx/DetailPO",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: d.ID }),
        success: function (data) {
            //Html expand child datatable
            html = '<div style="width: 100%;">' +
                '<div class="table-responsive">' +
                '<table table id="tableListsp" class="table table-hover table-bordered mytable">' +
                '<thead>' +
                '<tr >' +
                '<th>NO</th>' +
                '<th>NO SPP</th>' +
                '<th>Item Code</th>' +
                '<th>Item Name</th>' +
                '<th>Unit</th>' +
                '<th>Quantity</th>' +
                '<th>Price</th>' +
                '<th>Total Price</th>' +
                '<th>Deliv Date</th>' +
                '</tr>' +
                '</thead>' +

                '<tbody>';
            for (i = 0; i < data.d.length; i++) {
                var totalprice = data.d[i].Price * data.d[i].Qty;
                var no = i + 1;
                arrtotal.push(totalprice);
                html = html +
                    '<tr>' +
                    '<th>' + no + '</th>' +
                    '<th>' + data.d[i].DocumentNo + '</th>' +
                    '<th>' + data.d[i].ItemCode + '</th>' +
                    '<th>' + data.d[i].ItemName + '</th>' +
                    '<th>' + data.d[i].UOMCode + '</th>' +
                    '<th>' + data.d[i].Qty + '</th>' +
                    '<th>' + formatter.format(data.d[i].Price) + '</th>' +
                    '<th>' + formatter.format(totalprice) + '</th>' +
                    '<th>' + data.d[i].DelivDate + '</th>' +
                    '</tr>';
                    
            }
            total = arrtotal.reduce((a, b) => a + b, 0)
            dis = (total * (d.Disc / 100));
            ppn = ((total - dis) * (d.PPN / 100));
            grandtotal = total + ppn;
            html = html + '<tr >' +
                '<td colspan="5"  rowspan="3" ></td > ' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold">Total : </td>' +
                '<td style="background-color:lightgray; font-weight: bold">' + formatter.format(total) +'</td>' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold"></td>' +
                '</tr >' +
                '<tr>' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold">PPN 10% : </td>' +
                '<td style="background-color:lightgray; font-weight: bold">' + formatter.format(ppn) +'</td>' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold"></td>' +
                '</tr >' +
                '<tr>' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold">Grand Total : </td>' +
                '<td style="background-color:lightgray; font-weight: bold">' + formatter.format(grandtotal) +'</td>' +
                '<td colspan="2" style="background-color:lightgray; font-weight: bold"></td>' +
                '</tr > ';


            html = html + '</tbody>' +
                '</table>' +
                '</div>' +
                '</div>';
        }
    });

    return html;
}

function ApprovePO(id) {
    po = {
        ID: id
    };

    $.ajax({
        url: "ApprovePO.aspx/Approve",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ po: po }),
        success: function (data) {
            RequestPOOpen();
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'PO Telah Di Approve',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}

function InputAlasan(id, nopo) {
    $("#modaldetailtitle").text("Detail PO " + nopo + "");
    $('#modaldetail').modal('show');

    ID = id;
}

$("#btnproses").click(function () {
    $('#modaldetail').modal('hide');
    if ($("#alasannotapprove").val() == '' || $("#alasannotapprove").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Alasan Not Approve Tidak Boleh Kosong',
            theme: 'modern',
            type: 'red'
        });
    }
    else {
        po = {
            ID: ID,
            AlasanNotApproval: $("#alasannotapprove").val()
        };
        NotApprovePO(po);
    }
});


function NotApprovePO(po) {
    $.ajax({
        url: "ApprovePO.aspx/NotApprove",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ po: po }),
        success: function (data) {
            RequestPOOpen();
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'PO Tidak Di Approve',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}
