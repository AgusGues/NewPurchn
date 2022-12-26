$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestTransitLokasiT1();
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

function RequestTransitLokasiT1() {
    $.ajax({
        url: "LStockTransitT1.aspx/GetLapLokasiTransitT1",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableTransitT1(data);
            DrawTableTransitT1Print(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableTransitT1(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouplokasi = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Stock Lokasi Transit Tahap 1</h4>';
    textHTML += '<table id="tabletransitt1" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Periode Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Ukuran</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].Lokasi.trim(), grouplokasi) == -1) {
            grouplokasi.push(data[k].Lokasi.trim())
        }
    }

    for (var j = 0; j < grouplokasi.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouplokasi[j] == data[i].Lokasi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="width:200px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Periode + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Ukuran + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            } 
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td>Grand Total ' + grouplokasi[j] + ' : </td>';
        textHTML += '<td></td>';
        textHTML += '<td></td>';
        textHTML += '<td class="text-right">' + separator(totalsaldo) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; display: none;">' + grouplokasi[j] + '</td>';
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
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '<td style="display: none;">ZZZ</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtranst1").html(textHTML);
    $("#tabletransitt1").DataTable({
        "pageLength": 50,
        "order": [4, 'asc']
    });
}


function DrawTableTransitT1Print(data) {
    var textHTML = '';
    var totalsaldo = 0;
    var grouplokasi = [];
    var grandtotal = [];

    textHTML += '<h4>Laporan Stock Lokasi Transit Tahap 1</h4>';
    textHTML += '<table id="tabletransitt1print" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Periode Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Ukuran</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var k = 0; k < data.length; k++) {
        if (jQuery.inArray(data[k].Lokasi.trim(), grouplokasi) == -1) {
            grouplokasi.push(data[k].Lokasi.trim())
        }
    }

    for (var j = 0; j < grouplokasi.length; ++j) {
        totalsaldo = 0;
        for (var i = 0; i < data.length; i++) {
            if (grouplokasi[j] == data[i].Lokasi.trim()) {
                textHTML += '<tr>';
                textHTML += '<td style="width:200px">' + data[i].Partno + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Periode + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Ukuran + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].Qty) + '</td>';
                textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi + '</td>';
                textHTML += '</tr>';
                totalsaldo += data[i].Qty;
            }
        }
        grandtotal.push(totalsaldo);
        textHTML += '<tr>';
        textHTML += '<td colspan="3">Grand Total ' + grouplokasi[j] + ' : </td>';
        textHTML += '<td colspan="2">' + separator(totalsaldo) + '</td>';
        textHTML += '</tr>';
    }

    var total = 0;
    for (var l = 0; l < grandtotal.length; l++) {
        total += grandtotal[l] << 0;
    }

    textHTML += '<tr>';
    textHTML += '<td colspan="3" >Grand Total : </td>';
    textHTML += '<td colspan="2">' + separator(total) + '</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbtranst1print").html(textHTML);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tabletransitt1print');

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
    var divToPrint = document.getElementById("tbtranst1print");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}




