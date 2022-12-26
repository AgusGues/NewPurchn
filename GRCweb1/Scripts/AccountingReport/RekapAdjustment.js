$("#drtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#sdtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$("#modaladjust").on("hidden.bs.modal", function () {
    $("#tbadjust").html("");
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
    var fromdate = $("#drtgl").val().split('-')[2] + $("#drtgl").val().split('-')[1] + $("#drtgl").val().split('-')[0];
    var todate = $("#sdtgl").val().split('-')[2] + $("#sdtgl").val().split('-')[1] + $("#sdtgl").val().split('-')[0];
    var tipe = $("#tipe").val();
    RequestAdjust(fromdate, todate, tipe);
});


function RequestAdjust(fromdate, todate, tipe) {
    $.ajax({
        url: "RekapAdjustmentT1T3.aspx/GetAdjutment",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ fromdate: fromdate, todate: todate, tipe: tipe }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableAdjust(data);
            //DrawTableBJPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableAdjust(data) {
    $('#modaladjust').modal('show');
    var textHTML = '';

    textHTML += '<h4>Rekap Adjustment</h4>';
    textHTML += '<table id="tableadjust" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Tanggal</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">No Adjust</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">No BA</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Kode Barang</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Nama Barang</th>';
    textHTML += '<th colspan="2" style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Satuan</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust In</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust Out</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:10%">' + data[i].DateAdjust + '</td>';
        textHTML += '<td style="width:15%">' + data[i].AdjustNo + '</td>';
        textHTML += '<td style="width:20%">' + data[i].Keterangan + '</td>';
        textHTML += '<td style="width:15%">' + data[i].ItemCode + '</td>';
        textHTML += '<td style="width:20%">' + data[i].ItemName + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustIn) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + separator(data[i].AdjustOut) + '</td>';
        textHTML += '<td>' + data[i].Unit + '</td>';
        textHTML += '</tr>';
    }
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbadjust").html(textHTML);
    $("#tableadjust").DataTable({
        "pageLength": 50
    });
}

function printData() {
    var divToPrint = document.getElementById("tableadjust");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}


function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tableadjust');

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
