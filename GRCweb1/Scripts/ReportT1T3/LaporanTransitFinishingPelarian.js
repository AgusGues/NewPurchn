var param = 1;
$("#awaltgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#akhirtgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
});

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
});

$("#caripartno").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            url: "LTransitinPelarian.aspx/GetPartno",
            contentType: "application/json; charset=utf-8",
            data: "{'partno':'" + $("#caripartno").val() + "'}",
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

$('input[type=radio][name=list]').change(function () {
    if (this.value == 1) {
        param = 1;
    } else if (this.value == 2) {
        param = 2;
    } else if (this.value == 3) {
        param = 3;
    } else if (this.value == 4) {
        param = 4;
    }
});

$("#preview").click(function () {
    var thnblnhariawal = $("#awaltgl").val().split('-')[2] + $("#awaltgl").val().split('-')[1] + $("#awaltgl").val().split('-')[0];
    var thnblnhariakhir = $("#akhirtgl").val().split('-')[2] + $("#akhirtgl").val().split('-')[1] + $("#akhirtgl").val().split('-')[0];
    var partno = $("#caripartno").val();

    RequestTransitFinPelarian(thnblnhariawal, thnblnhariakhir, partno, param);
});

function RequestTransitFinPelarian(thnblnhariawal, thnblnhariakhir, partno, param) {
    $.ajax({
        url: "LTransitinPelarian.aspx/GetLapTransitFinisihingPelarian",
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
            DrawTableTransitFinPelarian(data);
            DrawTableTransitFinPelarianPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function DrawTableTransitFinPelarian(data) {
    var textHTML = '';
    var totalpelarian = 0;
    var sisa = 0;
    var totalserah = 0;
    textHTML += '<h4>Laporan Transaksi Finishing Kelokasi Transit Pelarian, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablePelarian" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Group</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Jemur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">M3 Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">M3 Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno3;
        var lokasi3;
        var tglserah;
        if (data[i].Partno3 == null) {
            partno3 = "";
        } else {
            partno3 = data[i].Partno3;
        }
        if (data[i].Lokasi3 == null) {
            lokasi3 = "";
        } else {
            lokasi3 = data[i].Lokasi3;
        }
        if (data[i].TglSerah == "01/01/0001") {
            tglserah = "";
        } else {
            tglserah = data[i].TglSerah;
        }
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Group + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglJemur + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + data[i].Partno2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Qty + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].M3_2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tglserah + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:150px;">' + partno3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + lokasi3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Qty2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].M3_3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
        textHTML += '</tr>';

        totalpelarian += data[i].Qty;
        totalserah += data[i].Qty2;
        sisa += data[i].Sisa;
    }

    textHTML += '</tbody>';
    textHTML += '</table>';

    textHTML += '<div class="col-xs-12 col-sm-10 form-group-sm">';
    textHTML += '<label for="produksi" style="padding: 5px">Total Pelarian : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + totalpelarian + '</label>';
    textHTML += '<label for="produksi" style="padding: 5px">Total Serah : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + totalserah + '</label>';
    textHTML += '<label for="produksi" style="padding: 5px">Sisa : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + sisa + '</label>';
    textHTML += '</div>'


    $("#tbtfpp").html(textHTML);
    $("#tablePelarian").DataTable({
        "pageLength": 50
    });
}


function DrawTableTransitFinPelarianPrint(data) {
    var textHTML = '';
    var totalpelarian = 0;
    var sisa = 0;
    var totalserah = 0;
    textHTML += '<h4>Laporan Transaksi Finishing Kelokasi Transit Pelarian, Periode : ' + $("#awaltgl").val() + ' - ' + $("#akhirtgl").val() + '</h4>';
    textHTML += '<table id="tablePelarianprint" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Input</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Group</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Jemur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">M3 Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Tanggal Serah</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Partno Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Lokasi Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Qty Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">M3 Akhir</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Oven</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno3;
        var lokasi3;
        var tglserah;
        if (data[i].Partno3 == null) {
            partno3 = "";
        } else {
            partno3 = data[i].Partno3;
        }
        if (data[i].Lokasi3 == null) {
            lokasi3 = "";
        } else {
            lokasi3 = data[i].Lokasi3;
        }
        if (data[i].TglSerah == "01/01/0001") {
            tglserah = "";
        } else {
            tglserah = data[i].TglSerah;
        }
        textHTML += '<tr>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglTransaksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglProduksi + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Group + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:160px;">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].TglJemur + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:160px;">' + data[i].Partno2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Lokasi2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Qty + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].M3_2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + tglserah + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; width:160px;">' + partno3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + lokasi3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Qty2 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].M3_3 + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;">' + data[i].Oven + '</td>';
        textHTML += '</tr>';

        totalpelarian += data[i].Qty;
        totalserah += data[i].Qty2;
        sisa += data[i].sisa;
    }

    textHTML += '</tbody>';
    textHTML += '</table>';

    textHTML += '<div class="col-xs-12 col-sm-10 form-group-sm">';
    textHTML += '<label for="produksi" style="padding: 5px">Total Pelarian : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + totalpelarian + '</label>';
    textHTML += '<label for="produksi" style="padding: 5px">Total Serah : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + totalserah + '</label>';
    textHTML += '<label for="produksi" style="padding: 5px">Sisa : </label>';
    textHTML += '<label for="produksi" style="padding: 5px">' + sisa + '</label>';
    textHTML += '</div>'
    
    $("#tbtfp").html(textHTML);
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablePelarianprint');

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
    var divToPrint = document.getElementById("tablePelarianprint");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}