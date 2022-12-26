var param = 0;
var autobs = 0;
$("#awaltgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#akhirtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$('input[type=radio][name=list]').change(function () {
    if (this.value == 1) {
        param = 0;
    } else if (this.value == 2) {
        param = 1;
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

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
});


$("#preview").click(function () {
    var thnblnhariawal = $("#awaltgl").val().split('-')[2] + $("#awaltgl").val().split('-')[1] + $("#awaltgl").val().split('-')[0];
    var thnblnhariakhir = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1] + $("#akhirtgl").val().split('-')[0];

    RequestMutasiLokasi(thnblnhariawal, thnblnhariakhir, param);
});

function RequestMutasiLokasi(thnblnhariawal, thnblnhariakhir, param) {
    $.ajax({
        url: "LMutasiLokasi.aspx/GetLapTransitFinisihing",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnblnhariawal: thnblnhariawal, thnblnhariakhir: thnblnhariakhir, param: param }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            $('#modalmutasilokasi').modal('show');
            DrawTableMutasiLokasi(data);
            DrawTableMutasiLokasiPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableMutasiLokasi(data) {
    var textHTML = '';
    var total = 0;

    textHTML += '<h4>Laporan Mutasi Lokasi, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablemutasilokasi" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Asal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglInput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].QtyOutSm + '</td>';
        textHTML += '</tr>';

        total += data[i].QtyOutSm;
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Grand Total</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbml").html(textHTML);
    $("#tablemutasilokasi").DataTable({
        "pageLength": 50
    });
}


function DrawTableMutasiLokasiPrint(data) {
    var textHTML = '';
    var total = 0;

    textHTML += '<h4>Laporan Mutasi Lokasi, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablemutasilokasip" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Transaksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Potong</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Asal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglInput + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].PartnoSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSer + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].LokasiSm + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].QtyOutSm + '</td>';
        textHTML += '</tr>';

        total += data[i].QtyOutSm;
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Grand Total</td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right"></td>';
    textHTML += '<td class="text-right">' + separator(total) + '</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbmlp").html(textHTML);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablemutasilokasip');

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
    var divToPrint = document.getElementById("tablemutasilokasip");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}