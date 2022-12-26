var param = 1;
var isPertamaDs = true;
$("#awaltgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#akhirtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$("#modaltransitfin").on("hidden.bs.modal", function () {
    $("#tbtf").html("");
});

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
});

$('input[type=radio][name=list]').change(function () {
    if (this.value == 1) {
        param = 1;
    } else if (this.value == 2) {
        param = 2;
    } else if (this.value == 3) {
        param = 3;
    }
});

function separator(n) {
    if (typeof n === 'number') {
        n += '';
        var x = n.split(',');
        var x1 = x[0];
        var x2 = x.length > 1 ? ',' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    } else {
        return n;
    }
}

$("#caripartno").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "LTransitIn.aspx/GetPartno",
            data: "{'partno':'" + $("#caripartno").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#caripartno").val(ui.item.PartNo);
        itemid = ui.item.ID;
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>").append("<a>" + item.PartNo + "</a>").appendTo(ul);
    };


$("#preview").click(function () {
    var thnblnhariawal = $("#awaltgl").val().split('-')[2] + $("#awaltgl").val().split('-')[1] + $("#awaltgl").val().split('-')[0];
    var thnblnhariakhir = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1] + $("#akhirtgl").val().split('-')[0];
    var partno = $("#caripartno").val();

    RequestTransitFin(thnblnhariawal, thnblnhariakhir, partno, param);
});

$("#previewlist").click(function () {
    var thnbln = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1];
    RequestTransitFinishing(thnbln);
});

function RequestTransitFin(thnblnhariawal, thnblnhariakhir, partno,  param) {
    $.ajax({
        url: "LTransitIn.aspx/GetLapTransitFinisihing",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnblnhariawal: thnblnhariawal, thnblnhariakhir: thnblnhariakhir, partno: partno, param: param }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            $('#modaltransitfin').modal('show');
            if (param == 1) {
                DrawTableTransitFinProd(data);
                DrawTableTransitFinProdPrint(data);
            } else if (param == 2) {
                DrawTableTransitFinSerah(data);
                DrawTableTransitFinSerahPrint(data);
            } else if (param == 3) {
                DrawTableTransitFinTrans(data);
                DrawTableTransitFinTransPrint(data);
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function RequestTransitFinishing(thnbln) {
    $.ajax({
        url: "LTransitIn.aspx/GetLapTransitFin",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            datatable = $.parseJSON(data);
            if (!isPertamaDs) {
                $("#tableListtransitfin").DataTable().destroy();
                $('#tableListtransitfin').empty();
            } else {
                isPertamaDs = false;
            }
            var oTblReport = $("#tableListtransitfin");
            oTblReport.DataTable({
                "data": datatable,
                "dom": 'Bfrtip',
                "buttons": [
                    'excelHtml5'
                ],
                "responsive": true,
                "autoWidth": true,
                "columns": [
                    { "data": "TglProduksi", title: "Tanggal Produksi" },
                    { "data": "TglSerah", title: "Tanggal Serah" },
                    { "data": "TglTransaksi", title: "Tanggal Transaksi" },
                    { "data": "Partno", title: "Partno Awal" },
                    { "data": "Lokasi", title: "Lokasi Awal" },
                    { "data": "Partno2", title: "Partno Akhir" },
                    { "data": "Lokasi2", title: "Lokasi Akhir" },
                    { "data": "Qty", title: "Qty" },
                    { "data": "Oven", title: "Oven" },
                    { "data": "M3", title: "M3" },
                    { "data": "Line", title: "Line" },
                    { "data": "Group", title: "Group" }
                ]
            });
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function DrawTableTransitFinProd(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfinprod" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglProduksi.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglProduksi.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglProduksi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td style="display: none;"> 99/99/9999 </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtf").html(textHTML);
    $("#tabletransitfinprod").DataTable({
        "pageLength": 50,
        "order": [1, 'asc']
    });
}


function DrawTableTransitFinSerah(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfinserah" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglSerah.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglSerah.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglSerah.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td style="display: none;"> 99/99/9999 </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtf").html(textHTML);
    $("#tabletransitfinserah").DataTable({
        "pageLength": 50,
        "order": [1, 'asc']
    });
}


function DrawTableTransitFinTrans(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfintrans" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglTransaksi.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglTransaksi.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglTransaksi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td style="display: none;"> 99/99/9999 </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtf").html(textHTML);
    $("#tabletransitfintrans").DataTable({
        "pageLength": 50,
        "order": [1, 'asc']
    });
}


function DrawTableTransitFinProdPrint(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfinprodp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglProduksi.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglProduksi.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglProduksi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtfp").html(textHTML);
}


function DrawTableTransitFinSerahPrint(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfinserahp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglSerah.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglSerah.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglSerah.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtfp").html(textHTML);
}


function DrawTableTransitFinTransPrint(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouptgl = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Transit Finishing</h4>';
    textHTML += '<table id="tabletransitfintransp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].TglTransaksi.trim(), grouptgl) == -1) {
            grouptgl.push(data[k].TglTransaksi.trim())
        }
    }

    for (var j = 0; j < grouptgl.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouptgl[j] == data[i].TglTransaksi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglSerah + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '<td style="width:180px">' + data[i].Partno2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Sub Total : </td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + grouptgl[j] + '</td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td></td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtfp").html(textHTML);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;

    if (param == 1) {
        tab = document.getElementById('tabletransitfinprodp');
    } else if (param == 2) {
        tab = document.getElementById('tabletransitfinserahp');
    } else if (param == 3) {
        tab = document.getElementById('tabletransitfintransp');
    }

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
    tab_text = tab_text.replace(/<img[^>]*>/gi, "");
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    }
    else
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}

function printData() {
    var divToPrint = "";
    if (param == 1) {
        divToPrint = document.getElementById('tabletransitfinprodp');
    } else if (param == 2) {
        divToPrint = document.getElementById('tabletransitfinserahp');
    } else if (param == 3) {
        divToPrint = document.getElementById('tabletransitfintransp');
    }

    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}