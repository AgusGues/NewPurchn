var isPertamaDs = true;
var status, closingstatus;
var recount, kd;
var itemid;
var username;
var plantid;

$("#tglproduksi").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#drjam").datetimepicker({ format: 'dd-mm-yyyy hh:ii' });
$("#sdjam").datetimepicker({ format: 'dd-mm-yyyy hh:ii' });
$("#tglproduksi").datepicker({
    onSelect: function (dateText) {
        $(this).change();
    }
}).on("change", function () {
    $('#drjam').val($("#tglproduksi").val() + ' 07:00');
    $('#sdjam').val($("#tglproduksi").val() + ' 15:00');
    RequestDestacking($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
});

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    $('#drjam').val($("#tglproduksi").val() + ' 07:00');
    $('#sdjam').val($("#tglproduksi").val() + ' 15:00');

    RequestDeptID();
    RequestFormula();
    RequestPlantGroup();
    RequestProduksi();
    RequestDestacking($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
});

$("#noproduk").change(function () {
    itemid = $("#noproduk").val();
});

$("#shift").change(function () {
    var shift = $("#shift option:selected").text();
    if (shift == 1) {
        $('#drjam').val($("#tglproduksi").val() + ' 07:00');
        $('#sdjam').val($("#tglproduksi").val() + ' 15:00');
    } else if (shift == 2) {
        $('#drjam').val($("#tglproduksi").val() + ' 15:00');
        $('#sdjam').val($("#tglproduksi").val() + ' 23:00');
    } else if (shift == 3) {
        $('#drjam').val($("#tglproduksi").val() + ' 23:00');
        $('#sdjam').val($("#tglproduksi").val() + ' 06:59');
    }  
});

function RequestFormula() {
    $.ajax({
        url: "Destacking.aspx/GetListFormula",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#jenis").append('<option value="' + value.ID + '" >' + value.FormulaName + '</option>');
            });
        }
    });
}

function leftPad(value, length) {
    return ('0'.repeat(length) + value).slice(-length);
};


function RequestDeptID() {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/GetUserDept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            deptid = data.d[0].DeptId;
            username = data.d[0].Username;
            if (deptid == 3) {
                RequestListMesinCutter();
            }
        },
        error: function (jqXHR, exception) {
        }
    });
}

function RequestPlantGroup() {
    $.ajax({
        url: "Destacking.aspx/GetListPlantGroup",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#group").append('<option value="' + value.ID + '" >' + value.Group + '</option>');
            });
        }
    });
}

function RequestProduksi() {
    $.ajax({
        url: "Destacking.aspx/GetListPartnoT1",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#noproduk").append('<option value="' + value.ID + '" >' + value.PartNo + '</option>');
            });
        }
    });
}


function RequestKodeCompany() {
    $.ajax({
        url: "Destacking.aspx/GetKodeCompany",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            kd = data.d;
        }
    });
}

function RequestCountInYear(tgl) {
    $.ajax({
        url: "Destacking.aspx/GetRecordCountInYear",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ tgl: tgl }),
        success: function (data) {
            recount = data.d;
        }
    });
}

function RequestDestacking(tgl) {
    $.ajax({
        url: "Destacking.aspx/GetListProduksiDestacking",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ tgl: tgl }),
        success: function (data) {
            if (!isPertamaDs) {
                $("#tableListDestacking").DataTable().destroy();
                $('#tableListDestacking').empty();
            } else {
                isPertamaDs = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListDestacking");
            oTblReport.DataTable({
                "data": datatable,
                "responsive" : true,
                "autoWidth": true,
                "columns": [
                    { "data": "ID", title: "ID" },
                    { "data": "FormulaCode", title: "Jenis" },
                    { "data": "PartNo", title: "PartNo" },
                    { "data": "Qty", title: "Qty" },
                    { "data": "Status", title: "Status" },
                    { "data": "Produksi", title: "TglProduksi" },
                    { "data": "DariJam", title: "Dari Jam" },
                    { "data": "SampaiJam", title: "Sampai Jam" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "";
                            aksi = "<button class = 'btn btn-danger' type='button' style='margin-right:5px;' onclick='CancelDestacking(" + row.ID + ")'><i class='fa fa-check'></i> Cancel </button>";
                            return aksi;
                        },
                        "defaultContent": ""
                    }
                ]
            });
        }
    });
}

function GetMonthClosingStatus(tahun, bulan) {
    $.ajax({
        url: "Destacking.aspx/GetClosingStatus",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ tahun: tahun, bulan: bulan, modul: "Produksi", modulname: "SystemClosing" }),
        success: function (data) {
            status = data.d[0].status;
            closingstatus = data.d[0].clsStat;
        }
    });
}

function GetPlantID(id) {
    $.ajax({
        url: "Destacking.aspx/GetPlantID",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ id: id }),
        success: function (data) {
            plantid = data.d;
        }
    });
}

$("#transfer").click(function () {
    var tahun = $("#tglproduksi").val().split('-')[2];
    var bulan = $("#tglproduksi").val().split('-')[1];

    GetMonthClosingStatus(tahun, bulan);
    if (status == 1 && closingstatus == 1) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Periode Bulan Sudah Closing. Transaksi Tidak Bisa Dilakukan',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }


    if ($("#jumlah").val() == null || $("#jumlah").val() == "") {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Jumlah Belum Diisi',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }
    if ($("#jenis").val() == "" || $("#jenis").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Jenis Produksi Belum Ditentukan',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    if ($("#group").val() == "" || $("#group").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Group Produksi Belum Ditentukan',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    if ($("#noproduk").val() == "" || $("#noproduk").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Produksi Belum Ditentukan',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    var id_dstk;
    var thn = $("#tglproduksi").val().split('-')[2];
    var thnbln = leftPad(thn, 2) + $("#tglproduksi").val().split('-')[1];
    GetPlantID($('#group').val());
    RequestKodeCompany();
    RequestCountInYear($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1]);
    id_destk = kd + thnbln + recount;

    destacking = {
        PlantID: plantid,
        PlantGroupID: $('#group').val(),
        FormulaID: $("#jenis").val(),
        LokasiID: 13044,
        PaletID: 2030,
        ItemID: $("#noproduk").val(),
        TglProduksi: $("#tglproduksi").val(),
        Qty: $("#jumlah").val(),
        CreatedBy: username,
        Id_dstk: id_destk,
        Shift: $('#shift').val(),
        DrJam: $('#drjam').val(),
        SdJam: $('#sdjam').val()
    };

    InsertDestacking(destacking);
});



function CancelDestacking(id) {
    $.ajax({
        url: "Destacking.aspx/CencelDestacking",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ id: id }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d == 1) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Data Berhasil Dicancel',
                    theme: 'modern',
                    type: 'green'
                });
            } else {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Partno Sudah Diserahkan, Proses Cancel Tidak Dapat Dilakukan',
                    theme: 'modern',
                    type: 'red'
                });
            }
            RequestDestacking($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

$("#clean").click(function () {
    Clearform();
});

function Clearform() {
    $("#jumlah").val(null);
    $("#group").val(null);
    $("#shift").val(1);
    $("#noproduk").val(null);
    $("#jenis").val(null);
    $('#drjam').val($("#tglproduksi").val() + ' 07:00');
    $('#sdjam').val($("#tglproduksi").val() + ' 15:00');
};

function InsertDestacking(destacking) {
    $.ajax({
        url: "Destacking.aspx/InsertDestacking",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ destacking: destacking }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            Clearform();
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Data Tersimpan',
                theme: 'modern',
                type: 'green'
            });
            RequestDestacking($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


