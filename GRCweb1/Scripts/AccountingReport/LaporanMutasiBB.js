var thisyear = new Date().getFullYear();
var group = 0;
var itemid = 0;
new Date().getFullYear();

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestDepoID();
    RequestTipeSPP();
    for (var selecttahun = 2019; selecttahun < thisyear + 2; ++selecttahun) {
        $("#tahun").append('<option value="' + selecttahun + '" >' + selecttahun + '</option>');
    }
    $('#tahun').val(thisyear);
});

$('input[type=radio][name=rekap]').change(function () {
    if (this.value == 1) {
        $("#namabarang").hide();
        group = 0;
        itemid = 0;
        $("#caribarang").val('');
    } else if (this.value == 2) {
        $("#namabarang").show();
        group = 1;
        itemid = 0;
        $("#caribarang").val('');
    }
});

function RequestDepoID() {
    $.ajax({
        url: "LapMutasiStockBB.aspx/GetDepoID",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            depo = data.d;
        }
    });
}

function RequestTipeSPP() {
    $.ajax({
        url: "LapMutasiStockBB.aspx/GetTipeSPP",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#laporan").append('<option value="' + value.ID + '" >' + value.GroupDescription + '</option>');
            });
        }
    });
}

$("#modalwip").on("hidden.bs.modal", function () {
    $("#tbbb").html("");
});

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
});

function separator(n) {
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

$("#caribarang").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "LapMutasiStockBB.aspx/GetItems",
            data: "{'item':'" + $("#caribarang").val() + "','group':'" + $('#laporan').val() + "'}",
            dataType: "json",
            success: function (data) {
                data = data.d
                data.data = data.results
                delete data.results
                data = $.parseJSON(data);
                response(data);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#caribarang").val(ui.item.ItemName);
        itemid = ui.item.ID;
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>").append("<a>" + item.ItemName + "</a>").appendTo(ul);
    };

$("#preview").click(function () {
    var SaldoLaluQty;
    var SaldoLaluPrice;
    var NextSaldo;
    var bln = $('#bulan').val();
    var thn = $('#tahun').val();
    var tipespp = $("#laporan").val();
    if (bln == 1) {
        SaldoLaluQty = "DesQty";
        SaldoLaluPrice = "DesAvgPrice";
        NextSaldo = "JanAvgPrice";
        thn = thn - 1;
    }
    else if (bln == 2) {
        SaldoLaluQty = "JanQty";
        SaldoLaluPrice = "JanAvgPrice";
        NextSaldo = "FebAvgPrice";
    }
    else if (bln == 3) {
        SaldoLaluQty = "FebQty";
        SaldoLaluPrice = "FebAvgPrice";
        NextSaldo = "MarAvgPrice";
    }
    else if (bln == 4) {
        SaldoLaluQty = "MarQty";
        SaldoLaluPrice = "MarAvgPrice";
        NextSaldo = "AprAvgPrice";
    }
    else if (bln == 5) {
        SaldoLaluQty = "AprQty";
        SaldoLaluPrice = "AprAvgPrice";
        NextSaldo = "MeiAvgPrice";
    }
    else if (bln == 6) {
        SaldoLaluQty = "MeiQty";
        SaldoLaluPrice = "MeiAvgPrice";
        NextSaldo = "JunAvgPrice";
    }
    else if (bln == 7) {
        SaldoLaluQty = "JunQty";
        SaldoLaluPrice = "JunAvgPrice";
        NextSaldo = "JulAvgPrice";
    }
    else if (bln == 8) {
        SaldoLaluQty = "JulQty";
        SaldoLaluPrice = "JulAvgPrice";
        NextSaldo = "AguAvgPrice";
    }
    else if (bln == 9) {
        SaldoLaluQty = "AguQty";
        SaldoLaluPrice = "AguAvgPrice";
        NextSaldo = "SepAvgPrice";
    }
    else if (bln == 10) {
        SaldoLaluQty = "SepQty";
        SaldoLaluPrice = "SepAvgPrice";
        NextSaldo = "OktAvgPrice";
    }
    else if (bln == 11) {
        SaldoLaluQty = "OktQty";
        SaldoLaluPrice = "OktAvgPrice";
        NextSaldo = "NovAvgPrice";
    }
    else if (bln == 12) {
        SaldoLaluQty = "NovQty";
        SaldoLaluPrice = "NovAvgPrice";
        NextSaldo = "DesAvgPrice";
    }
    RequestBB(thn, bln, tipespp, SaldoLaluQty, SaldoLaluPrice, NextSaldo, group, itemid);
});

function RequestBB(thn, bln, tipespp, SaldoLaluQty, SaldoLaluPrice, NextSaldo, group) {
    $.ajax({
        url: "LapMutasiStockBB.aspx/GetLapMutasiBB",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thn: thn, bln: bln, tipespp: tipespp, SaldoLaluQty: SaldoLaluQty, SaldoLaluPrice: SaldoLaluPrice, NextSaldo: NextSaldo, group: group, itemid: itemid }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBB(data);
            $('#modalbb').modal('show');
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableBB(data) {
    var textHTML = '';
    var saldoawalqty = 0;
    var saldoawalhs = 0;
    var saldoawalamt = 0;
    var terimabeliqty = 0;
    var terimabelihs = 0;
    var terimabeliamt = 0;
    var terimasesuaiqty = 0;
    var terimasesuaihs = 0;
    var terimasesuaiamt = 0;
    var terimareturqty = 0;
    var terimareturhs = 0;
    var terimareturamt = 0;
    var keluarproduksiqty = 0;
    var keluarproduksihs = 0;
    var keluarproduksiamt = 0;
    var keluarsesuaiqty = 0;
    var keluarsesuaihs = 0;
    var keluarsesuaiamt = 0;
    var keluarreturqty = 0;
    var keluarreturhs = 0;
    var keluarreturamt = 0;
    var saldoakhirqty = 0;
    var saldoakhirhs = 0;
    var saldoakhiramt = 0;

    if (group == 1) {
        textHTML += '<h4>Laporan Rekap Mutasi Stock Bahan Baku, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
        textHTML += '<table id="tablebb" class="table table-striped table-bordered table-hover display nowrap" style="width: 200%" border="4">';
        textHTML += '<thead>';
        textHTML += '<tr>';
        textHTML += '<th rowspan="3" style="vertical-align: middle; text-align: center;">Tanggal</th>';
        textHTML += '<th rowspan="3" style="vertical-align: middle; text-align: center;">No. Dokumen</th>';
        textHTML += '<th colspan="9" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
        textHTML += '<th colspan="9" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
        textHTML += '<th colspan="3" rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
        textHTML += '</tr>';

        textHTML += '<tr>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Pembelian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Penyesuaian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Return Pakai</th>';

        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Produksi</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Penyesuaian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Return Supplier</th>';
        textHTML += '</tr>';

        textHTML += '<tr>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '</tr>';
        textHTML += '</thead>';
        textHTML += '<tbody>';
    } else {
        textHTML += '<h4>Laporan Rekap Mutasi Stock Bahan Baku, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
        textHTML += '<table id="tablebb" class="table table-striped table-bordered table-hover display nowrap" style="width: 200%" border="4">';
        textHTML += '<thead>';
        textHTML += '<tr>';
        textHTML += '<th rowspan="3" style="vertical-align: middle; text-align: center;">Item</th>';
        textHTML += '<th colspan="3" rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
        textHTML += '<th colspan="9" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
        textHTML += '<th colspan="9" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
        textHTML += '<th colspan="3" rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
        textHTML += '</tr>';

        textHTML += '<tr>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Pembelian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Penyesuaian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Return Pakai</th>';

        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Produksi</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Penyesuaian</th>';
        textHTML += '<th colspan="3" style="vertical-align: middle; text-align: center;">Return Supplier</th>';
        textHTML += '</tr>';

        textHTML += '<tr>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';

        textHTML += '<th style="vertical-align: middle; text-align: center;">QTY</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">HS</th>';
        textHTML += '<th style="vertical-align: middle; text-align: center;">AMT</th>';
        textHTML += '</tr>';
        textHTML += '</thead>';
        textHTML += '<tbody>';
    }

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';

        if (group == 1) {
            textHTML += '<td >' + data[i].Tanggal + '</td>';
            textHTML += '<td >' + data[i].DocNo + '</td>';
        } else {
            textHTML += '<td style="width:250px">' + data[i].ItemID + '</td>';
            textHTML += '<td style="vertical-align: middle; text-align: center;" class="AwalQty">' + separator((data[i].SaldoAwalQty).toFixed(2)) + '</td>';
            textHTML += '<td style="vertical-align: middle; text-align: center;" class="Produksi">' + separator((data[i].SaldoAwalHS).toFixed(2)) + '</td>';
            textHTML += '<td style="vertical-align: middle; text-align: center;" class="Pelarian">' + separator((data[i].SaldoAwalAMT).toFixed(2)) + '</td>';
        }

        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].BeliQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].BeliHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].BeliAMT).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].AdjustQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].AdjustHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].AdjustAMT).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].ReturnQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].ReturnHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].ReturnAMT).toFixed(2)) + '</td>';

        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].ProdQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].ProdHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].ProdAMT).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].AdjProdQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].AdjProdHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].AdjProdAMT).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator((data[i].RetSupQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator((data[i].RetSupHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator((data[i].RetSupAMT).toFixed(2)) + '</td>';

        textHTML += '<td style="vertical-align: middle; text-align: center;" class="SaldoAkhirQty">' + separator((data[i].SaldoAkhirQty).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="SaldoAkhirHS">' + separator((data[i].SaldoAkhirHS).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="SaldoAkhirAMT">' + separator((data[i].SaldoAkhirAMT).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        saldoawalqty += parseFloat(data[i].SaldoAwalQty);
        saldoawalamt += parseFloat(data[i].SaldoAwalAMT);
        terimabeliqty += parseFloat(data[i].BeliQty);
        terimabeliamt += parseFloat(data[i].BeliAMT);
        terimasesuaiqty += parseFloat(data[i].AdjustQty);
        terimasesuaiamt += parseFloat(data[i].AdjustAMT);
        terimareturqty += parseFloat(data[i].ReturnQty);
        terimareturamt += parseFloat(data[i].ReturnAMT);
        keluarproduksiqty += parseFloat(data[i].ProdQty);
        keluarproduksiamt += parseFloat(data[i].ProdAMT);
        keluarsesuaiqty += parseFloat(data[i].AdjProdQty);
        keluarsesuaiamt += parseFloat(data[i].AdjProdAMT);
        keluarreturqty += parseFloat(data[i].RetSupQty);
        keluarreturamt += parseFloat(data[i].RetSupAMT);
        saldoakhirqty += parseFloat(data[i].SaldoAkhirQty);
        saldoakhiramt += parseFloat(data[i].SaldoAkhirAMT);
    }

    if (saldoawalqty > 0) {
        saldoawalhs = (saldoawalamt / saldoawalqty).toFixed(2);
    }
    if (terimabeliqty > 0) {
        terimabelihs = (terimabeliamt / terimabeliqty).toFixed(2);
    }
    if (terimasesuaiqty > 0) {
        terimasesuaihs = (terimasesuaiamt / terimasesuaiqty).toFixed(2);
    }
    if (terimareturqty > 0) {
        terimareturhs = (terimareturamt / terimareturqty).toFixed(2);
    }
    if (keluarproduksiqty > 0) {
        keluarproduksihs = (keluarproduksiamt / keluarproduksiqty).toFixed(2);
    }
    if (keluarsesuaiqty > 0) {
        keluarsesuaihs = (keluarsesuaiamt / keluarsesuaiqty).toFixed(2);
    }
    if (keluarreturqty > 0) {
        keluarreturhs = (keluarreturamt / keluarreturqty).toFixed(2);
    }
    if (saldoakhirqty > 0) {
        saldoakhirhs = (saldoakhiramt / saldoakhirqty).toFixed(2);
    }

    if (group == 0) {
        textHTML += '<tr>';
        textHTML += '<td class="text-right">Total</td>';
        textHTML += '<td class="text-right">' + separator(saldoawalqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(saldoawalhs) + '</td>';
        textHTML += '<td class="text-right">' + separator(saldoawalamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(terimabeliqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimabelihs) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimabeliamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(terimasesuaiqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimasesuaihs) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimasesuaiamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(terimareturqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimareturhs) + '</td>';
        textHTML += '<td class="text-right">' + separator(terimareturamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(keluarproduksiqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarproduksihs) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarproduksiamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(keluarsesuaiqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarsesuaihs) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarsesuaiamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(keluarreturqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarreturhs) + '</td>';
        textHTML += '<td class="text-right">' + separator(keluarreturamt.toFixed(0)) + '</td>';

        textHTML += '<td class="text-right">' + separator(saldoakhirqty.toFixed(0)) + '</td>';
        textHTML += '<td class="text-right">' + separator(saldoakhirhs) + '</td>';
        textHTML += '<td class="text-right">' + separator(saldoakhiramt.toFixed(0)) + '</td>';
        textHTML += '</tr>';
    }


    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbbb").html(textHTML);
    $("#tablebb").DataTable({
        "pageLength": 50
    });
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablebb');

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
    var divToPrint = document.getElementById("tablebb");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}