var isPertamaspp = true;
var isPertamasppdetail = true;
var id;
var oTblReport;
var ID, GroupID, ApproveDate1, ApproveDate2, ApproveDate3;
$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestSPPOpen();
});

function RequestSPPOpen() {
    $.ajax({
        url: "ApproveSPP.aspx/GetListSPPOpen",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (!isPertamaspp) {
                $("#tableListsppopen").DataTable().destroy();
                $('#tableListsppopen').empty();
            } else {
                isPertamaspp = false;
            }
            datatable = $.parseJSON(data.d);
            oTblReport = $("#tableListsppopen").DataTable({
                "data": datatable,
                "pageLength": 5,
                "fixedHeader": {
                    header:true
                },
                "columnDefs": [
                    { "targets": 2, "visible": false },
                    {
                        "targets": 3,
                        "render": function (data, type, full, meta) {
                            return type === 'display' ? '<div title="' + full.ID + '">' + data : data;
                        }
                    }
                ],
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
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-xs btn-success' type='button' onclick='ApproveSPP(" + row.ID + "," + row.GroupID + ",\"" + row.ApproveDate1 + "\",\"" + row.ApproveDate2 + "\",\"" + row.ApproveDate3 + "\")'><i class='fa fa-check'></i> Approve </button> " +
                                "<button class = 'btn btn-xs btn-danger' type='button' onclick='InputAlasan(" + row.ID + ",\"" + row.NoSPP + "\"," + row.GroupID + ",\"" + row.ApproveDate1 + "\",\"" + row.ApproveDate2 + "\",\"" + row.ApproveDate3 + "\")'><i class='fa fa-times'></i> Not Approve </button> ";
                            return aksi;
                        }
                    },
                    { "data": "ID", title: "ID" },
                    { "data": "NoSPP", title: "No SPP" },
                    { "data": "GroupDescription", title: "Group" },
                    { "data": "TypeDescription", title: "Jenis Barang" },
                    { "data": "CreatedBy", title: "Created By" },
                    { "data": "TanggalMinta", title: "Tanggal Minta" }
     
                ]
            });
             //Proses agar child table expand all saat load
            oTblReport.rows().every(function (rowIdx, tableLoop, rowLoop) {
                this.child(format(this.data())).show();
                this.nodes().to$().addClass('shown');
            });
           
       
        }
    });
}


//$('#tableListsppopen').on('click', 'td.details-control', function () {
//    var tr = $(this).closest('tr');
//    var row = oTblReport.row(tr);
//    row.child(format(row.data())).show();
//    tr.addClass('shown');
//});

//Load More
//$('#load-more').on('click', function () {
//    var i = oTblReport.page.len() + 5; // Jumlah Page.
//    oTblReport.page.len(i).draw();
//    oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
//});

//$('#tableListsppopen').on('page.dt', function () {
//    oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
//});

function format(d) {
    var html;
    $.ajax({
        url: "ApproveSPP.aspx/DetailSPP",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: d.ID }),
        success: function (data) {

            //Html expand child datatable
            html = '<div style="width: 100%;">' +
                '<div class="table-responsive">' +
                '<table table id="tableListsp" class="table table-hover table-bordered mytable" >' +
                '<thead>' +
                '<tr>' +
                '<th>ID</th>' +
                '<th>Item Code</th>' +
                '<th>Item Name</th>' +
                '<th>Quantity</th>' +
                '<th>Satuan</th>' +
                '<th>TglKirim</th>' +
                '</tr>' +
                '</thead>' +
                '<tbody>';

            for (i = 0; i < data.d.length; i++) {
                html = html +
                    '<tr>' +
                    '<th>' + data.d[i].ID + '</th>' +
                    '<th>' + data.d[i].ItemCode + '</th>' +
                    '<th>' + data.d[i].ItemName + '</th>' +
                    '<th>' + data.d[i].Quantity + '</th>' +
                    '<th>' + data.d[i].Satuan + '</th>' +
                    '<th>' + data.d[i].TanggalKirim + '</th>' +
                    '</tr>';
            }

            html = html + '</tbody>' +
                '</table>' +
                '</div>' +
                '</div>';
        }
    });

    return html;
}

function InputAlasan(id, nospp, groupid, approvedate1, approvedate2, approvedate3) {
    $("#modaldetailtitle").text("Detail SPP " + nospp + "");
    $('#modaldetail').modal('show');

    ID = id;
    GroupID = groupid;
    ApproveDate1 = approvedate1;
    ApproveDate2 = approvedate2;
    ApproveDate3 = approvedate3;

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
        spp = {
            ID: ID,
            GroupID: GroupID,
            AlasanBatal: $("#alasannotapprove").val(),
            ApproveDate1: ApproveDate1,
            ApproveDate2: ApproveDate2,
            ApproveDate3: ApproveDate3
        };
        NotApproveSpp(spp);
    }
});

function ApproveSPP(id, groupid, approvedate1, approvedate2, approvedate3) {
    spp = {
        ID: id,
        GroupID: groupid,
        AlasanBatal: "", 
        ApproveDate1: approvedate1,
        ApproveDate2: approvedate2,
        ApproveDate3: approvedate3
    };
    ApproveSpp(spp);
}

function ApproveSpp(spp) {
    $.ajax({
        url: "ApproveSPP.aspx/Approve",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ spp: spp }),
        success: function (data) {
            RequestSPPOpen();
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'SPP Telah Di Approve',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}


function NotApproveSpp(spp) {
    $.ajax({
        url: "ApproveSPP.aspx/NotApprove",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ spp: spp }),
        success: function (data) {
            RequestSPPOpen();
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'SPP Tidak Di Approve',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}