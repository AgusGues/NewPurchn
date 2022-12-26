    $('#tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
    GetDepartemen();
    GetUser();
    GetTipeAsset();
    GetSPGroup();
    Load();
    
    document.getElementById("txtstok").disabled = true;
    document.getElementById("txtnoasset").disabled = true;
    document.getElementById("ddlsp").disabled = true;
    document.getElementById("btncancel").disabled = true;
    document.getElementById("btnprint").disabled = true;
    $('.the-loader').hide();

$(function () {
    $("form").submit(function () { return false; });
});
function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}
function Load() {
    $('.the-loader').show();
    var pakai = GetParameterValues('PakaiNo');
    var pakaino = $("#txtcari").val();
    var nopakai;
    if (pakai != undefined ) {
        nopakai = pakai;
    }else if(pakaino!=''){
        nopakai=pakaino
    }
    else {
        $('.the-loader').hide();
        return;
    }
    ClearQty();
    ClearAll();
    
    $.ajax({
        url: "FormPakaiAsset.aspx/LoadingAwal",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ NoPakai: nopakai }),
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results

            data = $.parseJSON(data);
            ListDetail();
               
            $.each(data, function (i, data) {
                $("#txtpakaino").val(data.PakaiNo);
                $("#txtkodedept").val(data.DeptCode);
                $("#ddldept").empty();
                $("#ddldept").append($("<option/>", {
                    value: (data.DeptID),
                    text: (data.DeptName)
                }));
                $("#tanggal").val((data.PakaiDate).substring(0, 10));
                $("#CreatedBy").val(data.CreatedBy);
                document.getElementById("ddldept").disabled = true;
                if (data.Status == -1){
                    document.getElementById("btncancel").disabled =true;
                }
                else {
                    document.getElementById("btncancel").disabled = false;
                }
                   
                    

                //if (pakai.Status > 0)
                if (data.ID > 0 && data.Status==2)
                {
                    document.getElementById("btntambah").disabled = true;
                    document.getElementById("btnsimpan").disabled = false;
                    document.getElementById("btnprint").disabled = true;
                    //lbAddOP.Enabled = false;
                    //btnUpdate.Disabled = true;
                    //btnPrint.Disabled = false;
                }
                else if (data.ID > 0 && data.Status == 0 || data.ID > 0 && data.Status == 1)
                {
                    document.getElementById("btntambah").disabled = true;
                    document.getElementById("btnsimpan").disabled = false;
                    document.getElementById("btnprint").disabled = false;
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = true;
                }
            });
        }
    });
    $('.the-loader').hide();
}

function GetUser() {
    $.ajax({
        url: "FormPakaiAsset.aspx/GetUser",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            $("#CreatedBy").val(data);

        }
    });
}

function GetDepartemen() {

    $.ajax({
        url: "FormPakaiAsset.aspx/GetDept",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddldept").append('<option value="' + value.ID + '" >' + value.DeptName + '</option>');
            });
        }
    });
    
}

function GetTipeAsset() {
    $.ajax({
        url: "FormPakaiAsset.aspx/GetTipe",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddltipe").append('<option value="' + value.ID + '" >' + value.GroupDescription + '</option>');
            });
        }
    });
}

var input = document.getElementById("txtNama");
input.addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        $('.the-loader').show();
        var Nama = $('#txtNama').val();
        $.ajax({
            url: "FormPakaiAsset.aspx/GetNama",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ Nama: Nama }),
            success: function (data) {
                var ddl = $("#ddlnama");
                ddl.empty();
                if (data.d == 0) {
                    ddl.append($("<option/>", {
                        value: '0',
                        text: " "
                    }));
                    $.alert({
                        icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                        content: 'Data tidak ditemukan !!'
                    });
                }
                else {
                    ddl.append($("<option/>", {
                        value: '0',
                        text: "- - Pilih Inventory - -"
                    }));
                    $.each(data.d, function (data, value) {
                        $("#ddlnama").append('<option value="' + value.ID + '" >' + value.ItemName + '</option>');
                    });
                }
                $("#txtNama").val('');
            }
        });
        $('.the-loader').hide();
    }
});

function getdept(value) {
    $('#txtkodedept').val(value);
    if (value == '4' || value == '5' || value == '18') {
        document.getElementById("ddlsp").disabled = false;
    }
    else {
        document.getElementById("ddlsp").disabled = true;
    }
}
function getkode(sel) {
    var type = $("#ddltipe").val();
    if (type == "") {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Tentukan Tipe Asset dahulu !!'
        });
        return;
    }

    obj = {};
    obj.ID = sel;
    obj.itemTypeID = type;
    obj.Tanggal = $("#tanggal").val();
    
    $.ajax({
        url: "FormPakaiAsset.aspx/GetKode",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ obj:obj }),
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results

            $(function () {
                $.each(data, function (i, data) {
                    $("#txtstok").val(data.SaldoAkhir);
                    $("#txtkodebarang").val(data.ItemCode);
                    $("#txtsatuan").val(data.UOMCode);
                    $("#txtUOMID").val(data.UOMID);
                    $("#txtGroupID").val(data.GroupID);
                    namabarang = data.ItemName;

                });
             });
        }
    });
    
}

var namabarang;
var uomid;
function statuschange(set) {
    if (set == "2") {
        document.getElementById("txtnoasset").disabled = false;
    }
    else if (set == "1") {
        document.getElementById("txtnoasset").disabled = true;
    }
}


function tambah() {
    $('.the-loader').show();
    var a = $("#txtstok").val();
    var b = $("#txtpakai").val();
    var c = $("#form-field-11").val();
    if (/\D/.test(b) || b == '') {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Quantity Pakai harus number'
        });
        $('.the-loader').hide();
        return;
    }
    if (b == '0') {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Quantity Terima tidak boleh 0'
        });
         $('.the-loader').hide();
        return;
    }
    if (/\D/.test(a) || a == '') {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Quantity Stok harus number'
        });
         $('.the-loader').hide();
        return;
    }
    if (a == '0' ) {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Quantity Stok tidak boleh 0'
        });
         $('.the-loader').hide();
        return;
    }
    if (b>a) {
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Quantity Pakai lebih besar dari pada Stok'
        });
         $('.the-loader').hide();
        return;
    }
    if(c==''){
        $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Keterangan harus diisi'
        });
         $('.the-loader').hide();
        return;
    }
    else
    {
        var dept = $("#ddldept").val();
        var Tgl = $("#tanggal").val();
        $.ajax({
            url: "FormPakaiAsset.aspx/ValidasiTanggal", type: "POST", contentType: "application/json; charset=utf-8", dataType: "json",
            data: JSON.stringify({ Dept: dept, Tanggal: Tgl }),
            success: function (output) {
                if (output.d != '') {
                    $('.the-loader').hide(); $.alert({
                        icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                        content: output.d
                    });
                     $('.the-loader').hide();
                    return ;
                    
                }
                else {
                    if ($("#ddlStatusAsset").val() == '0') {
                        $.alert({
                            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                            content: 'Status Asset Belum ditentukan'
                        });
                         $('.the-loader').hide();
                        return;
                    }
                    if ($("#ddlStatusAsset").val() == '2' && $("#txtnoasset").val() == '') {
                        $.alert({
                            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                            content: 'Nomor Asset yng akan di ganti belum di tentukan atau \\n Ganti pilihan status asset menjadi Baru'
                        });
                         $('.the-loader').hide();
                        return;
                    }
                    else {
                        obj = {};
                        obj.ItemID = $("#ddlnama").val();
                        obj.Quantity = $("#txtpakai").val();
                        obj.Keterangan = $("#form-field-11").val();
                        obj.GroupID = $("#txtGroupID").val();
                        obj.ItemCode = $("#txtkodebarang").val();
                        obj.ItemName = namabarang;
                        obj.UomID = $("#txtUOMID").val();
                        obj.UOMCode = $("#txtsatuan").val();
                        obj.DeptCode = $("#txtkodedept").val();
                        obj.SarmutID = $("#ddlsp").val();

                        $.ajax({
                            url: "FormPakaiAsset.aspx/PakaiDetail", type: "POST", contentType: "application/json; charset=utf-8", dataType: "json",
                            data: JSON.stringify({ obj: obj }),
                            success: function (output) {
                                ListDetail();
                                if (output.d != '') {
                                    $('.the-loader').hide(); $.alert({
                                        icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                                        content: output.d
                                    });
                                }
                            }
                        });
                        ClearQty();
                         $('.the-loader').hide();
                    }
                }
            }
        });
         $('.the-loader').hide();
    }
}

function ListDetail() {
    $.ajax({
        url: "FormPakaiAsset.aspx/PakaiDetailList",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#tabledetail").DataTable().destroy();
            $('#tabledetail').empty();

            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tabledetail");
            oTblReport.DataTable({
                "data": datatable,
                "responsive": true,
                "autoWidth": true,

                "columns": [
                    { "data": "ItemName", title: "Nama Barang" },
                    { "data": "ItemCode", title: "Kode Barang" },
                    { "data": "Quantity", title: "Jumlah" },
                    { "data": "UOMCode", title: "Satuan" },
                    { "data": "Keterangan", title: "Keterangan" }
                ]
            });
        }
    });
}

function GetSPGroup() {

    $.ajax({
        url: "FormPakaiAsset.aspx/GetSPGroup",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddlsp").append('<option value="' + value.ID + '" >' + value.ZonaName + '</option>');
            });
        }
    });
}

function Baru() {
    $('.the-loader').show();
    $.ajax({
        url: "FormPakaiAsset.aspx/Clear",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //window.location.replace("../Purchasing/FormPakaiAsset.aspx");
            ClearQty();
            ClearAll();
            GetUser();

            GetDepartemen();
            document.getElementById("ddldept").disabled = false;
        }
    });
    $('.the-loader').hide();
}

function Simpan() {
    $('.the-loader').show();

    obj = {};
    obj.DeptID = $("#ddldept").val();
    obj.PakaiNo = $("#txtpakaino").val();
    obj.Tanggal = $("#tanggal").val();

    $.ajax({
        url: "FormPakaiAsset.aspx/simpan",
        type: "POST",
        data: JSON.stringify({ obj: obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.length > 12) {
                $('.the-loader').hide(); $.alert({
                    icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
                    content: data.d
                })
            }
            else {
                data = data.d
                data.data = data.results
                delete data.results

                $(function () {
                    $.each(data, function (i, data) {
                        $("#txtpakaino").val(data.PakaiNo);
                    });
                });
            }
           
        }
    });
                
    $('.the-loader').hide();
}

function Cancel() {
    var pakaino = $("#txtpakaino").val();
    $('.the-loader').show();
    $.ajax({
        url: "FormPakaiAsset.aspx/Cancel",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ pakaino: pakaino }),
        success: function (data) {
            if (data.d != "") {
                if (data.d == "Cancel berhasil .....") {
                    $.alert({
                        icon: 'fa fa-check', theme: 'modern', title: 'Success', type: 'green',
                        content: data.d
                    });
                }
            }
        }
    });
    $('.the-loader').hide();
}

function Cetak() {
    $.alert({
        icon: 'fa fa-times', theme: 'modern', title: 'Ooopsss..', type: 'red',
        content: 'Error !'
    });
}

function List() {
    $.ajax({
        url: "FormPakaiAsset.aspx/List",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location.replace("../Purchasing/ListPakaiBaku.aspx?approve=" + data.d);
        }
    });
}

function ClearQty() {
    $("#txtkodebarang").val('');
    $('#txtpakai').val('');
    $('#txtstok').val('');
    $('#txtsatuan').val('');
    $('#form-field-11').val('');
    $('#txtUOMID').val('');
    $('#txtGroupID').val('');
}

function ClearAll(){
    $('#tanggal').val(new Date().toISOString().slice(0, 10));
    $('#txtpakaino').val('');
    $('#txtkodedept').val('');
    $('#ddlstatus').val('0');
    //$('#ddldept').val('0');
    $("#ddldept").empty();
    $("#ddldept").append($("<option/>", {
        value: '0',
        text: ' - - Pilih Dept - -'
    }));
    ListDetail();
    document.getElementById("btncancel").disabled = true;
}

