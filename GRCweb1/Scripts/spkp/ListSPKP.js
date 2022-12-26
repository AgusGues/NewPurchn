$(document).ready(function () {
    $('.the-loader').hide();
    load();
});

function Form() {
    window.location.replace("../SPKP/SPKP.aspx");
}

function load() {
    $.ajax({
        url: "ListSPKP.aspx/GetList",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ }),
        success: function (data) {

            $("#tablelist").DataTable().destroy();
            $('#tablelist').empty();

            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablelist");
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,
                dom: 'Blfrtip',
                
                "columns": [
                    
                    { "data": "No", title: "No " },
                    { "data": "NoSpkp", title: "No SPKP" },
                    {
                        "data": "ID",title:"Action", "render": function (id) {
                            return '<a class="btn btn-primary" style="margin-left:30px"  onclick="getspkp(' + id + ')">Pilih</a>'
                        }
                    }
                ]
            });
        }
    });
}

function getspkp(id) {
    $.ajax({
        url: "ListSPKP.aspx/Getnospkp",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ id: id }),
        success: function (data) {
            let text = data.d;
            let result = text.substr(1, text.length - 2);

            $('#nospkp').val(result);
        }
    });
    $('#modallistspkp').modal({
        show: 'true'
    });
}

function loadspkp() {
    var nospkp = $("#nospkp").val();
    var line = $("#ddlline").val();
    $.ajax({
        url: "ListSPKP.aspx/Getspkp",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ nospkp: nospkp, line: line }),
        success: function (data) {

            $("#tablespkp").DataTable().destroy();
            $('#tablespkp').empty();

            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablespkp");
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,
                dom: 'Blfrtip',
                "columns": [
                    //{ "data": "Tanggal", title: "Tanggal" },
                    {
                        "className": '',
                        "data": "Tanggal",
                        title: "Tanggal",
                        "render": function (data, type, row) {
                            var tgl = data;
                            var thn = tgl.substring(6, 10);
                            var bln = tgl.substring(3, 5);
                            var tgla = tgl.substring(0, 2);
                            return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                        }
                    },
                    { "data": "Shift", title: "Shift" },
                    { "data": "Kategori",title:"Kategori"},
                    { "data": "Tebal",title:"Tebal"},
                    { "data": "Ukuran",title:"Ukuran"},
                    { "data": "Target",title:"Target"},
                    { "data": "Keterangan",title:"Keterangan"},
                    {
                        "data": "id", title: "Action", "render": function (id) {
                            return '<a class="btn btn-primary" style="margin-left:30px"  onclick="getspkpdetail(' + id + ')">update</a>'
                        }
                    }
                ]
            });
        }
    });
}

function getspkpdetail(id) {
    $.ajax({
        url: "ListSPKP.aspx/Getspkpdetail",
       type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ id: id }),
        success: function (data) {
            var rspns = eval(data.d);
            $.each(rspns, function () {
                $('#ID').val(this.id);
                $('#Tanggal').val(this.Tanggal);
                $('#Shift').val(this.Shift);
                $('#Kategori').val(this.Kategori);
                $('#Tebal').val(this.Tebal);
                $('#Ukuran').val(this.Ukuran);
                $('#Target').val(this.Target);
                $('#Keterangan').val(this.Keterangan);
            });
        }
    });
    $('#modalupdatespkp').modal({
        show: 'true'
    });
}

function update() {
    obj = {};
    obj.id = $("#ID").val();
    obj.Kategori = $("#Kategori").val();
    obj.Keterangan = $("#Keterangan").val();
    obj.Target = $("#Target").val();
    obj.Ukuran = $("#Ukuran").val();
    obj.Tebal = $("#Tebal").val();
    $.ajax({
        url: "ListSPKP.aspx/update",
        type: "POST",
        contentType: "application/json; charset=utf-8", 
        dataType: "json",
        data: JSON.stringify({ obj: obj }),
        success: function (output) {
            $('.the-loader').hide(); $.alert({
                icon: 'fa fa-check', theme: 'modern', title: 'success!', type: 'green',
                content: "data Berhasil diubah"
                //data.d
            });
            $('#modalupdatespkp').modal('hide');
            loadspkp();
        }
    });
}
