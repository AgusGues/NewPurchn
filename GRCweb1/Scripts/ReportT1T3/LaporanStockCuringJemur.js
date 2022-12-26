var param = 1;
$("#awaltgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#akhirtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$("#modalcuring").on("hidden.bs.modal", function () {
    $("#tbcj").html("");
});

$('input[type=radio][name=list]').change(function () {
    if (this.value == 1) {
        param = 1;
        $("#hidepartno").hide();
        $("#hidelokasi").hide();
    } else if (this.value == 2) {
        param = 2;
        $("#hidepartno").hide();
        $("#hidelokasi").hide();
    } else if (this.value == 3) {
        param = 3;
        $("#hidepartno").hide();
        $("#hidelokasi").hide();
    } else if (this.value == 4) {
        param = 4;
        $("#hidepartno").show();
        $("#hidelokasi").show();
    }
});

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
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
            url: "LStockCuring.aspx/GetPartno",
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


$("#carilokasi").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "LStockCuring.aspx/GetLokasi",
            data: "{'lokasi':'" + $("#carilokasi").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#carilokasi").val(ui.item.Lokasi);
        itemid = ui.item.ID;
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>").append("<a>" + item.Lokasi + "</a>").appendTo(ul);
    };

$("#preview").click(function () {
    var thnblnhariawal = $("#awaltgl").val().split('-')[2] + $("#awaltgl").val().split('-')[1] + $("#awaltgl").val().split('-')[0];
    var thnblnhari = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1] + $("#akhirtgl").val().split('-')[0];
    var partno = $("#caripartno").val();
    var lokasi = $("#carilokasi").val();

    RequestPerLokasi(thnblnhariawal, thnblnhari, partno, lokasi, param);
});

function RequestPerLokasi(thnblnhariawal, thnblnhari, partno, lokasi, param) {
    $.ajax({
        url: "LStockCuring.aspx/GetLapCuringJemur",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnblnhariawal: thnblnhariawal, thnblnhari: thnblnhari, partno: partno, lokasi: lokasi, param: param }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            $('#modalcuring').modal('show');
            if (param == 1) {
                DrawTablePerLokasi(data);
                DrawTablePerLokasiPrint(data);
            } else if (param == 2 || param == 3) {
                DrawTableCuringdetail(data);
                DrawTableCuringdetailPrint(data);
            } else if (param == 4) {
                DrawTableProduksiHarian(data);
                DrawTableProduksiHarianPrint(data);
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function DrawTablePerLokasi(data) {
    var textHTML = '';
    var tortalsaldo = 0;
    var totalp99 = 0;

    textHTML += '<h4>Laporan Rekap Per Lokasi, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tableperlokasi" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">P99</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AwalQty">' + data[i].Lokasi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Produksi">' + separator(data[i].Saldo) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Pelarian">' + separator(data[i].P99) + '</td>';
        textHTML += '</tr>';

        tortalsaldo += data[i].Saldo;
        totalp99 += data[i].P99;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(tortalsaldo) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalp99) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcj").html(textHTML);
    $("#tableperlokasi").DataTable({
        "pageLength": 50,
        "order": []
    });
}

function DrawTableCuringdetail(data) {
    var textHTML = '';
    var subtotal = 0;


    textHTML += '<h4>Laporan Jemur Detail, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablecuringjemur" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tgl Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tgl Jemur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Keterangan</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + data[i].TglProduksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + data[i].TglJemur + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + separator(data[i].Saldo) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >Curing Oven</td>';
        textHTML += '</tr>';

        subtotal += data[i].Saldo;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(subtotal) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcj").html(textHTML);
    $("#tablecuringjemur").DataTable({
        "pageLength": 50,
        "order": []
    });
}

function DrawTableProduksiHarian(data) {
    var textHTML = '';
    var totalawal = 0;
    var totalproduksi = 0;
    var totaladjustin = 0;
    var totalpenyerahan = 0;
    var totaladjustout = 0;
    var totalsaldo = 0;

    textHTML += '<h4>Laporan Stock Tahap 1, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tableproduksiharian" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th colspan="2" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
    textHTML += '<th colspan="2" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust In</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Penyerahan</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust Out</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Awal) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Penerimaan) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustIn) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Pengeluaran) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustOut) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Saldo) + '</td>';
        textHTML += '</tr>';

        totalawal += data[i].Awal;
        totalproduksi += data[i].Penerimaan;
        totaladjustin += data[i].AdjustIn;
        totalpenyerahan += data[i].Pengeluaran;
        totaladjustout += data[i].AdjustOut;
        totalsaldo += data[i].Saldo;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td class="text-right">' + separator(totalawal) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalproduksi) + '</td>';
    textHTML += '<td class="text-right">' + separator(totaladjustin) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalpenyerahan) + '</td>';
    textHTML += '<td class="text-right">' + separator(totaladjustout) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcj").html(textHTML);
    $("#tableproduksiharian").DataTable({
        "pageLength": 50,
        "order": []
    });
}


function DrawTablePerLokasiPrint(data) {
    var textHTML = '';
    var tortalsaldo = 0;
    var totalp99 = 0;

    textHTML += '<h4>Laporan Rekap Per Lokasi, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tableperlokasip" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">P99</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AwalQty">' + data[i].Lokasi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Produksi">' + separator(data[i].Saldo) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Pelarian">' + separator(data[i].P99) + '</td>';
        textHTML += '</tr>';

        tortalsaldo += data[i].Saldo;
        totalp99 += data[i].P99;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(tortalsaldo) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalp99) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcjp").html(textHTML);
}

function DrawTableCuringdetailPrint(data) {
    var textHTML = '';
    var subtotal = 0;

    textHTML += '<h4>Laporan Jemur Detail, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablecuringjemurp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tgl Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tgl Jemur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Keterangan</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + data[i].TglProduksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + data[i].TglJemur + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >' + separator(data[i].Saldo) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" >Curing Oven</td>';
        textHTML += '</tr>';

        subtotal += data[i].Saldo;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td></td>';
    textHTML += '<td></td>';
    textHTML += '<td class="text-right">' + separator(subtotal) + '</td>';
    textHTML += '<td></td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcjp").html(textHTML);
}

function DrawTableProduksiHarianPrint(data) {
    var textHTML = '';
    var totalawal = 0;
    var totalproduksi = 0;
    var totaladjustin = 0;
    var totalpenyerahan = 0;
    var totaladjustout = 0;
    var totalsaldo = 0;

    textHTML += '<h4>Laporan Stock Tahap 1, Periode : ' + $("#awaltgl").val() + ' s/d ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tableproduksiharianp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th colspan="2" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
    textHTML += '<th colspan="2" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust In</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Penyerahan</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust Out</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Awal) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Penerimaan) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustIn) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Pengeluaran) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustOut) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Saldo) + '</td>';
        textHTML += '</tr>';

        totalawal += data[i].Awal;
        totalproduksi += data[i].Penerimaan;
        totaladjustin += data[i].AdjustIn;
        totalpenyerahan += data[i].Pengeluaran;
        totaladjustout += data[i].AdjustOut;
        totalsaldo += data[i].Saldo;
    }

    textHTML += '<tr>';
    textHTML += '<td>Grand Total : </td>';
    textHTML += '<td class="text-right">' + separator(totalawal) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalproduksi) + '</td>';
    textHTML += '<td class="text-right">' + separator(totaladjustin) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalpenyerahan) + '</td>';
    textHTML += '<td class="text-right">' + separator(totaladjustout) + '</td>';
    textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbcjp").html(textHTML);
}


function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;

    if (param == 1) {
        tab = document.getElementById('tableperlokasip');
    } else if (param == 2 || param ==3 ) {
        tab = document.getElementById('tablecuringjemurp');
    } else if (param == 4) {
        tab = document.getElementById('tableproduksiharianp');
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
        divToPrint = document.getElementById("tableperlokasip");
    } else if (param == 2 || param == 3) {
        divToPrint = document.getElementById("tablecuringjemurp");
    } else if (param == 4) {
        divToPrint = document.getElementById("tableproduksiharianp");
    }

    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}