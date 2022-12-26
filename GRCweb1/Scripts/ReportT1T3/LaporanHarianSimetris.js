var param = 1;
var autobs = 0;
$("#awaltgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#akhirtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$('input[type=radio][name=list]').change(function () {
    if (this.value == 1) {
        param = 1;
    } else if (this.value == 2) {
        param = 2;
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

function separatorcoma(n) {
    if (typeof n === 'string') {
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

$("#preview").click(function () {
    var thnblnhariawal = $("#awaltgl").val().split('-')[2] + $("#awaltgl").val().split('-')[1] + $("#awaltgl").val().split('-')[0];
    var thnblnhariakhir = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1] + $("#akhirtgl").val().split('-')[0];
    if ($('#autobs').is(':checked')) {
        autobs = 1;
    } else {
        autobs = 0;
    }
    RequestHarianSimetris(thnblnhariawal, thnblnhariakhir, param, autobs);
});

function RequestHarianSimetris(thnblnhariawal, thnblnhariakhir, param, autobs) {
    $.ajax({
        url: "LSimetris.aspx/GetLapHarianSimetris",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnblnhariawal: thnblnhariawal, thnblnhariakhir: thnblnhariakhir, param: param, autobs: autobs }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            $('#modalhariansimetris').modal('show');
            if ($('#autobs').is(':checked')) {
                DrawTableHarianSimetrisAutoBS(data);
                DrawTableHarianSimetrisAutoBSPrint(data);
            } else {
                DrawTableHarianSimetris(data);
                DrawTableHarianSimetrisPrint(data);
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function DrawTableHarianSimetris(data) {
    var textHTML = '';
    var qtyin = 0;
    var qtyout = 0;
    var qtyinm3 = 0;
    var qtyoutm3 = 0;
    textHTML += '<h4>Laporan Harian Simetris, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablehariansimetris" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Mesin</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglInput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyInSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].MCutter + '</td>';
        textHTML += '</tr>';

        qtyin += data[i].QtyInSm;
        qtyout += data[i].QtyOutSm;

        qtyinm3 += parseFloat(data[i].QtyInSm * data[i].V1);
        qtyoutm3 += parseFloat(data[i].QtyOutSm * data[i].V2);
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Lembar</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyin) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyout) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Meter Kubik</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separatorcoma(qtyinm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separatorcoma(qtyoutm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbs").html(textHTML);
    $("#tablehariansimetris").DataTable({
        "pageLength": 50
    });
}


function DrawTableHarianSimetrisAutoBS(data) {
    var textHTML = '';
    var qtyin = 0;
    var qtyout = 0;
    var qtybp = 0;
    var qtybs = 0;

    var partnoser ="";
    var lokasiser ="";
    var qtyser = "";
    var tglinput = "";
    var tgltrans = "";

    textHTML += '<h4>Laporan Harian Simetris, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablehariansimetris" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno OK/KW</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        if (data[i].PartnoSer == null) {
            partnoser = "";
        } else {
            partnoser = data[i].PartnoSer;
        }
        if (data[i].LokasiSer == null) {
            lokasiser = "";
        } else {
            lokasiser = data[i].LokasiSer;
        }
        if (data[i].QtyInSm == null) {
            data[i].QtyInSm  = "";
        } else {
            qtyser = data[i].QtyInSm;
        }
        if (data[i].TglInput == "01/01/0001") {
            tglinput = "";
        } else {
            tglinput = data[i].TglInput;
        }
        if (data[i].TglTransaksi == "01/01/0001") {
            tgltrans = "";
        } else {
            tgltrans = data[i].TglTransaksi;
        }

        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tglinput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tgltrans + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + partnoser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + lokasiser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + qtyser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoBS + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiBS + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutBS + '</td>';
        textHTML += '</tr>';

        qtyin += data[i].QtyInSm;
        qtyout += data[i].QtyOutSm;
        qtybp += data[i].QtyOutBP;
        qtybs += data[i].QtyOutBS;
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Lembar</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyin) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyout) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtybp) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtybs) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbs").html(textHTML);
    $("#tablehariansimetris").DataTable({
        "pageLength": 50
    });
}

function DrawTableHarianSimetrisPrint(data) {
    var textHTML = '';
    var qtyin = 0;
    var qtyout = 0;
    var qtyinm3 = 0;
    var qtyoutm3 = 0;
    textHTML += '<h4>Laporan Harian Simetris, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablehariansimetrisp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Mesin</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglInput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:160px;">' + data[i].PartnoSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyInSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:160px;">' + data[i].PartnoSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].MCutter + '</td>';
        textHTML += '</tr>';

        qtyin += data[i].QtyInSm;
        qtyout += data[i].QtyOutSm;

        qtyinm3 += parseFloat(data[i].QtyInSm * data[i].V1);
        qtyoutm3 += parseFloat(data[i].QtyOutSm * data[i].V2);
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Lembar</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyin) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyout) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Meter Kubik</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separatorcoma(qtyinm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separatorcoma(qtyoutm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbsp").html(textHTML);
}


function DrawTableHarianSimetrisAutoBSPrint(data) {
    var textHTML = '';
    var qtyin = 0;
    var qtyout = 0;
    var qtybp = 0;
    var qtybs = 0;

    var partnoser = "";
    var lokasiser = "";
    var qtyser = "";
    var tglinput = "";
    var tgltrans = "";

    textHTML += '<h4>Laporan Harian Simetris, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablehariansimetrisp" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno OK/KW</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        if (data[i].PartnoSer == null) {
            partnoser = "";
        } else {
            partnoser = data[i].PartnoSer;
        }
        if (data[i].LokasiSer == null) {
            lokasiser = "";
        } else {
            lokasiser = data[i].LokasiSer;
        }
        if (data[i].QtyInSm == null) {
            data[i].QtyInSm = "";
        } else {
            qtyser = data[i].QtyInSm;
        }
        if (data[i].TglInput == "01/01/0001") {
            tglinput = "";
        } else {
            tglinput = data[i].TglInput;
        }
        if (data[i].TglTransaksi == "01/01/0001") {
            tgltrans = "";
        } else {
            tgltrans = data[i].TglTransaksi;
        }

        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tglinput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tgltrans + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:200px;">' + partnoser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + lokasiser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + qtyser + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:200px;">' + data[i].PartnoSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:200px;">' + data[i].PartnoBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutBP + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:200px;">' + data[i].PartnoBS + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiBS + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].QtyOutBS + '</td>';
        textHTML += '</tr>';

        qtyin += data[i].QtyInSm;
        qtyout += data[i].QtyOutSm;
        qtybp += data[i].QtyOutBP;
        qtybs += data[i].QtyOutBS;
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Lembar</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyin) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtyout) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtybp) + '</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(qtybs) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbsp").html(textHTML);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablehariansimetrisp');

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
    var divToPrint = document.getElementById("tablehariansimetrisp");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}