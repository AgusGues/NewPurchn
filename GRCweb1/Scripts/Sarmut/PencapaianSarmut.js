var isPertamaCtrp = true;
var isPertamaKrwg = true;
var isPertamaJmng = true;
var tahun;

var date = new Date();
var year = date.getFullYear();
$(document).ready(function () {
    $("#citereup").prop('checked', true);
    $("#panelkrwg").hide();
    $("#paneljmng").hide();
    $("#tahun").val(year);
    tahun = $("#tahun option:selected").text();
    RequestCtrp(tahun);
});

$("#tahun").change(function () {
    tahun = $("#tahun option:selected").text();
    if ($("#citereup").prop("checked") == true && $("#karawang").prop("checked") == true && $("#jombang").prop("checked") == true) {
        RequestCtrp(tahun);
        RequestKrwg(tahun);
        RequestJmng(tahun);
    } else if ($("#citereup").prop("checked") == true && $("#karawang").prop("checked") == true && $("#jombang").prop("checked") == false) {
        RequestCtrp(tahun);
        RequestKrwg(tahun);
    } else if ($("#citereup").prop("checked") == true && $("#karawang").prop("checked") == false && $("#jombang").prop("checked") == true) {
        RequestCtrp(tahun);
        RequestJmng(tahun);
    } else if ($("#citereup").prop("checked") == false && $("#karawang").prop("checked") == true && $("#jombang").prop("checked") == true) {
        RequestKrwg(tahun);
        RequestJmng(tahun);
    } else if ($("#citereup").prop("checked") == true && $("#karawang").prop("checked") == false && $("#jombang").prop("checked") == false) {
        RequestCtrp(tahun);
    } else if ($("#citereup").prop("checked") == false && $("#karawang").prop("checked") == true && $("#jombang").prop("checked") == false) {
        RequestKrwg(tahun);
    } else if ($("#citereup").prop("checked") == false && $("#karawang").prop("checked") == false && $("#jombang").prop("checked") == true) {
        RequestJmng(tahun);
    }
});

$("#citereup").change(function () {
    if (this.checked) {
        $("#panelctrp").show(1000);
        RequestCtrp(tahun);
    } else {
        $("#panelctrp").hide(1000);
    }
});

$("#karawang").change(function () {
    if (this.checked) {
        $("#panelkrwg").show(1000);
        RequestKrwg(tahun);
    } else {
        $("#panelkrwg").hide(1000);
    }
});

$("#jombang").change(function () {
    if (this.checked) {
        $("#paneljmng").show(1000);
        RequestJmng(tahun);
    } else {
        $("#paneljmng").hide(1000);
    }
});


function RequestCtrp(tahun) {
    $.ajax({
        url: "PencapaianSarmut.aspx/PlantCtrp",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Tahun: tahun }),
        success: function (data) {
            if (!isPertamaCtrp) {
                $("#tablesarmutctrp").DataTable().destroy();
                $('#tablesarmutctrp').empty();
            } else {
                isPertamaCtrp = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablesarmutctrp");
            oTblReport.DataTable({
                "data": datatable,
                "paging": false,
                "scrollY": 300,
                "scrollX": true,
                "order": [[0, "asc"]],
                "columns": [
                    { "data": "Urutan", title: "Urutan", visible: false},
                    { "data": "NO", title: "No" },
                    { "data": "Dimensi", title: "Dimensi" },
                    { "data": "SarMutPerusahaan", title: "SarMut Perusahaan" },
                    { "data": "Dept", title: "Dept" },
                    { "data": "SarMutDepartemen", title: "SarMut Departemen" },
                    { "data": "ParameterTerukur", title: "Parameter Terukur" },
                    { "data": "Jan", title: "Jan" },
                    { "data": "Feb", title: "Feb" },
                    { "data": "Mar", title: "Mar" },
                    { "data": "Apr", title: "Apr" },
                    { "data": "Mei", title: "Mei" },
                    { "data": "Jun", title: "Jun" },
                    { "data": "SMI", title: "SM I" },
                    { "data": "Jul", title: "Jul" },
                    { "data": "Agu", title: "Agu" },
                    { "data": "Sep", title: "Sep" },
                    { "data": "Okt", title: "Okt" },
                    { "data": "Nov", title: "Nov" },
                    { "data": "Des", title: "Des" },
                    { "data": "SMII", title: "SM II" }
                ]
            });
        }
    });
}


function RequestKrwg(tahun) {
    $.ajax({
        url: "PencapaianSarmut.aspx/PlantKrwg",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Tahun: tahun }),
        success: function (data) {
            if (!isPertamaKrwg) {
                $("#tablesarmutkrwg").DataTable().destroy();
                $('#tablesarmutkrwg').empty();
            } else {
                isPertamaKrwg = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablesarmutkrwg");
            oTblReport.DataTable({
                "data": datatable,
                "paging": false,
                "scrollY": 300,
                "scrollX": true,
                "order": [[0, "asc"]],
                "columns": [
                    { "data": "Urutan", title: "Urutan", visible: false },
                    { "data": "NO", title: "No" },
                    { "data": "Dimensi", title: "Dimensi" },
                    { "data": "SarMutPerusahaan", title: "SarMut Perusahaan" },
                    { "data": "Dept", title: "Dept" },
                    { "data": "SarMutDepartemen", title: "SarMut Departemen" },
                    { "data": "ParameterTerukur", title: "Parameter Terukur" },
                    { "data": "Jan", title: "Jan" },
                    { "data": "Feb", title: "Feb" },
                    { "data": "Mar", title: "Mar" },
                    { "data": "Apr", title: "Apr" },
                    { "data": "Mei", title: "Mei" },
                    { "data": "Jun", title: "Jun" },
                    { "data": "SMI", title: "SM I" },
                    { "data": "Jul", title: "Jul" },
                    { "data": "Agu", title: "Agu" },
                    { "data": "Sep", title: "Sep" },
                    { "data": "Okt", title: "Okt" },
                    { "data": "Nov", title: "Nov" },
                    { "data": "Des", title: "Des" },
                    { "data": "SMII", title: "SM II" }
                ]
            });
        }
    });
}



function RequestJmng(tahun) {
    $.ajax({
        url: "PencapaianSarmut.aspx/PlantJmng",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Tahun: tahun }),
        success: function (data) {
            if (!isPertamaJmng) {
                $("#tablesarmutjmng").DataTable().destroy();
                $('#tablesarmutjmng').empty();
            } else {
                isPertamaJmng = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablesarmutjmng");
            oTblReport.DataTable({
                "data": datatable,
                "paging": false,
                "scrollY": 300,
                "scrollX": true,
                "order": [[0, "asc"]],
                "columns": [
                    { "data": "Urutan", title: "Urutan", visible: false },
                    { "data": "NO", title: "No" },
                    { "data": "Dimensi", title: "Dimensi" },
                    { "data": "SarMutPerusahaan", title: "SarMut Perusahaan" },
                    { "data": "Dept", title: "Dept" },
                    { "data": "SarMutDepartemen", title: "SarMut Departemen" },
                    { "data": "ParameterTerukur", title: "Parameter Terukur" },
                    { "data": "Jan", title: "Jan" },
                    { "data": "Feb", title: "Feb" },
                    { "data": "Mar", title: "Mar" },
                    { "data": "Apr", title: "Apr" },
                    { "data": "Mei", title: "Mei" },
                    { "data": "Jun", title: "Jun" },
                    { "data": "SMI", title: "SM I" },
                    { "data": "Jul", title: "Jul" },
                    { "data": "Agu", title: "Agu" },
                    { "data": "Sep", title: "Sep" },
                    { "data": "Okt", title: "Okt" },
                    { "data": "Nov", title: "Nov" },
                    { "data": "Des", title: "Des" },
                    { "data": "SMII", title: "SM II" }
                ]
            });
        }
    });
}