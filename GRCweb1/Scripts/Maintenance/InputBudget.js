$(document).ready(function () {
    let tahun = new Date().getFullYear()
    $("#txttahun").val(tahun.toString());
    let bulan = new Date().getMonth();
    var bln;
    if (bulan >= 6) {
        bln = 'II'
    }
    else
        bln = 'I'
    $("#ddlsmt").val(bln);
    Load();
});

function Load() {
    $.ajax({
        url: "InputBudget.aspx/LoadList",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#tablelist").DataTable().destroy();
            $('#tablelist').empty();

            datatable = $.parseJSON(data.d);

            var oTblReport = $("#tablelist" );
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,
                
                "columns": [
                    { "data": "ID", title: "ID" },
                    { "data": "Tahun", title: "Tahun" },
                    { "data": "Smt", title: "Semester" },
                    { "data": "Jumlah", title: "Jumlah" },
                    { "data": "LastModifiedTime", title: "Created" },
                    {
                        "data": "ID",title: "Edit",
                        render: function (data, type, row) {
                            return '<td><center><button type="button" class="btn btn-info waves-effect" onclick="detail(' + data + ');return false" >Edit</button></center></td>';
                        }
                    }
                ]
            });
        }
    });
}


function detail(id) {
    var id = id;
    $.ajax({
        url: "InputBudget.aspx/Loaddetail",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ id: id }),
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results

            data = $.parseJSON(data);

            $.each(data, function (i, data) {
                    $("#txttahun").val(data.Tahun);
                    $("#txttotal").val(data.Jumlah);
                    $("#ddlsmt").val(data.Smt);
                    $("#ID").val(data.ID);

                });
        }
    });
}
function baru(){
    let tahun = new Date().getFullYear()
    $("#txttahun").val(tahun.toString());
    let bulan = new Date().getMonth();
    var bln;
    if (bulan>7){
        bln='II'
    }
    else
        bln='I'
    $("#ddlsmt").val(bln);
    $("#txttotal").val('');
    $("#ID").val('');
               
}

function simpan() {
    var id = 0;
    if ($("#ID").val() != "") {
        id = $("#ID").val();
    }

    obj = {};
    obj.Tahun  = $("#txttahun").val();
    obj.Jumlah  = $("#txttotal").val();
    obj.Smt    = $("#ddlsmt").val();
    obj.ID = id;

    $.ajax({
        url: "InputBudget.aspx/simpan",
        type: "POST",
        data: JSON.stringify({ obj: obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.alert({
                icon: 'fa fa-check', theme: 'modern', title: 'Succes!', type: 'green',
                content: ''
            });
            baru();
            Load();
        }
    });

}