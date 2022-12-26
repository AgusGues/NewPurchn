var isPertama = true;
var isPertamadetail = true;
var id;
var oTblReport;
var ID, GroupID, ApproveDate1, ApproveDate2, ApproveDate3;

function ListData() {
    $('.the-loader').show();
    $.ajax({
        url: "ApprovePakaiNew.aspx/ListData",type: "POST",contentType: "application/json; charset=utf-8",dataType: "json",
        success: function (data) {
            if (!isPertama) {
                $("#datatable").DataTable().destroy();
                $('#datatable').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            oTblReport = $("#datatable").DataTable({
                "data": datatable,
                "pageLength": 50,
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
                { "data": "PakaiNo", title: "PakaiNo" },
                { "data": "PakaiDate", title: "PakaiDate" },
                { "data": "CreatedBy", title: "CreatedBy" },
                {
                    "render": function (data, type, row, meta) {
                        var aksi=`
                        <button class ="btn btn-xs btn-success" type="button" onclick="Approve('`+row.PakaiNo+`')">
                        <i class=fa fa-check></i> Approve 
                        </button>`;
                        return aksi;
                    }
                }
                ]
            });
            oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
            $('.the-loader').hide();
        }
    });
}ListData();

$('#datatable').on('click', 'td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = oTblReport.row(tr);
    if(row.child.isShown()) {
        row.child.hide();
        tr.removeClass('shown');
    }else{
        row.child(format(row.data())).show();
        tr.addClass('shown');
    }
});

function format(d) {
    $('.the-loader').show();
    var html;
    $.ajax({
        url: "ApprovePakaiNew.aspx/ListDetail",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: d.ID }),
        success: function (data) {
            html=`
            <div style="width: 100%;">
            <div class="table-responsive">
            <table class="table table-bordered" style="width: 100%" >
            <tr>
            <th>ID</th>
            <th>Item Code</th>
            <th>Item Name</th>
            <th>Quantity</th>
            <th>Satuan</th>
            <th>Keterangan</th>
            </tr>
            <tbody>
            `;
            for (i = 0; i < data.d.length; i++) {
               html+=`
               <tr>
               <th>`+data.d[i].ID+`</th>
               <th>`+data.d[i].ItemCode+`</th>
               <th>`+data.d[i].ItemName+`</th>
               <th>`+data.d[i].Quantity+`</th>
               <th>`+data.d[i].Satuan+`</th>
               <th>`+data.d[i].Keterangan+`</th>
               </tr>`;
           }
           html+=`
           </tbody>
           </table>
           </div>
           </div>
           `;
           $('.the-loader').hide();
       }
   });
    return html;
}

$('#datatable').on('page.dt', function () {
    oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
});

function Approve(PakaiNo) {
    $('.the-loader').show();
    $.ajax({
        url: 'ApprovePakaiNew.aspx/Approve',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data: JSON.stringify({ PakaiNo: PakaiNo }),
        success: function (data) {
            $.each(data.d, function (i, b) {
                if(b.MsgAprov!=''){
                    $.alert({
                        icon: 'fa fa-check',title: 'Success', theme: 'modern',type: 'green',
                        content: b.MsgAprov
                    });
                }else{
                    $.alert({
                        icon: 'fa fa-check',title: 'Success', theme: 'modern',type: 'green',
                        content: 'Pemakaian Kode : '+PakaiNo+ ' Berhasil Di Approve'
                    });
                    ListData();
                }
            })
            $('.the-loader').hide();
        }
    });
}
