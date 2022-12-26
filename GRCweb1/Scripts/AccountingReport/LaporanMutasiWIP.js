var month;
var isPertama = true;
var bln;
var thn;
$("#filtertgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
var thisyear = $("#filtertgl").val().split('-')[2];
var depo = 0;

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    month = new Date().getMonth();
    if (month.constructor.length > 1) {
        $('#bulan').val(month + 1);
    } else {
        $('#bulan').val("0" + (month + 1));
    }
    RequestDepoID();
    for (var selecttahun = 2019; selecttahun < thisyear+2; ++selecttahun) {
        $("#tahun").append('<option value="' + selecttahun + '" >' + selecttahun + '</option>');
    }
    $('#tahun').val(thisyear);
    
});

$("#modalwip").on("hidden.bs.modal", function () {
    $("#tbwip").html("");
});

$("#modalwipm3").on("hidden.bs.modal", function () {
    $("#tbwipm3").html("");
});

$("#exportexcel").click(function () {
    fnExcelReport();
});

$("#print").click(function () {
    printData();
});

$("#exportexcelm3").click(function () {
    fnExcelReportm3();
});

$("#printm3").click(function () {
    printDatam3();
});


function RequestDepoID() {
    $.ajax({
        url: "LMutasiWIP.aspx/GetDepoID",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            depo = data.d;
        }
    });
}

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

$('input[type=radio][name=listtime]').change(function () {
    if (this.value == 1) {
        $("#showhidebulan").show();
        $("#showhidetahun").show();
        $("#showhidetgl").hide();
    } else if (this.value == 2) {
        $("#showhidebulan").hide();
        $("#showhidetahun").hide();
        $("#showhidetgl").show();    
    }
});

$("#preview").click(function () {
    var thnblnhr = $("#filtertgl").val().split('-')[2] + $("#filtertgl").val().split('-')[1] + $("#filtertgl").val().split('-')[0];
    bln = $('#bulan').val();
    thn = $('#tahun').val();
    var lastperiode;
    var thnbln = thn + bln;

    if (bln == "01") {
        thn = thn - 1;
        lastperiode = thn + "12";
    } else {
        lastperiode = thnbln - 1;
    }

    if ($('#harian').is(':checked')) {
        if ($('#meterkubik').is(':checked')) {
            $('#modalwipm3').modal('show');
            RequestWIPM3Harian(thnbln, lastperiode, thnblnhr);
        }
        else {
            $('#modalwip').modal('show');
            RequestWIPHarian(thnbln, lastperiode, thnblnhr);
        }
    } else {
        if ($('#meterkubik').is(':checked')) {
            $('#modalwipm3').modal('show');
            RequestWIPM3(thnbln, lastperiode);
        }
        else {
            $('#modalwip').modal('show');
            RequestWIP(thnbln, lastperiode);
        }
    }
});

function RequestWIP(thnbln, lastpriode) {
    $.ajax({
        url: "LMutasiWIP.aspx/GetLapMutasiWIP",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastpriode: lastpriode }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableWIP(data);
            DrawTableWIPPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestWIPHarian(thnbln, lastpriode, thnbln0) {
    $.ajax({
        url: "LMutasiWIP.aspx/GetLapMutasiWIPHarian",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastpriode: lastpriode, thnbln0: thnbln0 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableWIP(data);
            DrawTableWIPPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function RequestWIPM3(thnbln, lastpriode) {
    $.ajax({
        url: "LMutasiWIP.aspx/GetLapMutasiWIP",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastpriode: lastpriode }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableWIPM3(data);
            DrawTableWIPM3Print(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestWIPM3Harian(thnbln, lastpriode, thnbln0) {
    $.ajax({
        url: "LMutasiWIP.aspx/GetLapMutasiWIPHarian",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastpriode: lastpriode, thnbln0: thnbln0 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableWIPM3(data);
            DrawTableWIPM3Print(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableWIP(data) {
    var textHTML = '';
    var saldoawal = 0;
    var inproduksi = 0;
    var inpelarian = 0;
    var inadjust = 0;
    var inlistplank = 0;
    var ok = 0;
    var outlistplank = 0;
    var bp = 0;
    var khusus = 0;
    var sample = 0;
    var outpelarian = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;
    var m3produksi = 0;

    var saldoawalm3 = 0;
    var inproduksim3 = 0;
    var inpelarianm3 = 0;
    var inadjustm3 = 0;
    var inlistplankm3 = 0;
    var okm3 = 0;
    var outlistplankm3 = 0;
    var bpm3 = 0;
    var khususm3 = 0;
    var samplem3 = 0;
    var outpelarianm3 = 0;
    var outadjustm3 = 0;
    var saldoakhirm3 = 0;
    textHTML += '<h4>Laporan Rekap Mutasi Stock WIP, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
    textHTML += '<table id="tablewip" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th colspan="4" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Meter Kubik</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Mili Meter Produksi</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">OK (H99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP Finish (B99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Ukuran Khusus</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Sample (Q99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno;
        if (data[i].NoDocument.charAt(0) == 'A') {
            partno = data[i].NoDocument.replace('A', '');
        } else {
            partno = data[i].NoDocument;
        };
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AwalQty">' + separator(data[i].AwalQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Produksi">' + separator(data[i].InProdQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Pelarian">' + separator(data[i].InP99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separator(data[i].InAdjustQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separator(data[i].InI99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separator(data[i].H99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="I99">' + separator(data[i].I99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="BPFinish(B99)">' + separator(data[i].B99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="UkuranKhusus">' + separator(data[i].C99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Sample(Q99)">' + separator(data[i].Q99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OutPelarian">' + separator(data[i].OutP99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OutAdjust">' + separator(data[i].OutAdjustQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AkhirQty">' + separator(data[i].AkhirQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="M3">' + separatorcoma(data[i].M3) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="M3Produksi">' + separator(data[i].M3Produksi) + '</td>';
        textHTML += '</tr>';

        saldoawal += data[i].AwalQty;
        inproduksi += data[i].InProdQty;
        inpelarian += data[i].InP99;
        inadjust += data[i].InAdjustQty;
        inlistplank += data[i].InI99;
        ok += data[i].H99;
        outlistplank += data[i].I99;
        bp += data[i].B99;
        khusus += data[i].C99;
        sample += data[i].Q99;
        outpelarian += data[i].OutP99;
        outadjust += data[i].OutAdjustQty;
        saldoakhir += data[i].AkhirQty;
        m3 += data[i].M3;
        m3produksi += data[i].M3Produksi;

        saldoawalm3 += parseFloat(data[i].AwalQty * data[i].Volume);
        inproduksim3 += parseFloat(data[i].InProdQty * data[i].Volume);
        inpelarianm3 += parseFloat(data[i].InP99 * data[i].Volume);
        inadjustm3 += parseFloat(data[i].InAdjustQty * data[i].Volume);
        inlistplankm3 += parseFloat(data[i].InI99 * data[i].Volume);
        okm3 += parseFloat(data[i].H99 * data[i].Volume);
        outlistplankm3 += parseFloat(data[i].I99 * data[i].Volume);
        bpm3 += parseFloat(data[i].B99 * data[i].Volume);
        khususm3 += parseFloat(data[i].C99 * data[i].Volume);
        samplem3 += parseFloat(data[i].Q99 * data[i].Volume);
        outpelarianm3 += parseFloat(data[i].OutP99 * data[i].Volume);
        outadjustm3 += parseFloat(data[i].OutAdjustQty * data[i].Volume);
        saldoakhirm3 += parseFloat(data[i].AkhirQty * data[i].Volume);
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total</td>';
    textHTML += '<td class="text-right">' + separator(saldoawal) + '</td>';
    textHTML += '<td class="text-right">' + separator(inproduksi) + '</td>';
    textHTML += '<td class="text-right">' + separator(inpelarian) + '</td>';
    textHTML += '<td class="text-right">' + separator(inadjust) + '</td>';
    textHTML += '<td class="text-right">' + separator(inlistplank) + '</td>';
    textHTML += '<td class="text-right">' + separator(ok) + '</td>';
    textHTML += '<td class="text-right">' + separator(outlistplank) + '</td>';
    textHTML += '<td class="text-right">' + separator(bp) + '</td>';
    textHTML += '<td class="text-right">' + separator(khusus) + '</td>';
    textHTML += '<td class="text-right">' + separator(sample) + '</td>';
    textHTML += '<td class="text-right">' + separator(outpelarian) + '</td>';
    textHTML += '<td class="text-right">' + separator(outadjust) + '</td>';
    textHTML += '<td class="text-right">' + separator(saldoakhir) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(m3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separator(m3produksi) + '</td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Meter Kubik</td>';
    textHTML += '<td class="text-right">' + separatorcoma(saldoawalm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inproduksim3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inpelarianm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inadjustm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inlistplankm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(okm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outlistplankm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(bpm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(khususm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(samplem3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outpelarianm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outadjustm3.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">-</td>';
    textHTML += '<td class="text-right">-</td>';
    textHTML += '<td class="text-right">-</td>';
    textHTML += '</tr>';

    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbwip").html(textHTML);
    $("#tablewip").DataTable({
        "pageLength": 50
    });
}

function DrawTableWIPPrint(data) {
    var textHTML = '';
    var saldoawal = 0;
    var inproduksi = 0;
    var inpelarian = 0;
    var inadjust = 0;
    var inlistplank = 0;
    var ok = 0;
    var outlistplank = 0;
    var bp = 0;
    var khusus = 0;
    var sample = 0;
    var outpelarian = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;
    var m3produksi = 0;

    var saldoawalm3 = 0;
    var inproduksim3 = 0;
    var inpelarianm3 = 0;
    var inadjustm3 = 0;
    var inlistplankm3 = 0;
    var okm3 = 0;
    var outlistplankm3 = 0;
    var bpm3 = 0;
    var khususm3 = 0;
    var samplem3 = 0;
    var outpelarianm3 = 0;
    var outadjustm3 = 0;

    textHTML += '<table id="tablewipprint" class="table" style="width: 100%; border-collapse: collapse; border: thin solid black;">';

    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<td colspan="16" style="vertical-align: middle; text-align: center; border: none;"><h4>Laporan Rekap Mutasi Stock Bahan Jadi Tahap 1, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4></th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Awal</th>';
    textHTML += '<th colspan="4" style="vertical-align: middle; text-align: center; border: thin solid black;">Penerimaan</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center; border: thin solid black;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Akhir</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Meter Kubik</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Mili Meter Produksi</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">OK (H99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP Finish (B99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Ukuran Khusus</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Sample (Q99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno;
        if (data[i].NoDocument.charAt(0) == 'A') {
            partno = data[i].NoDocument.replace('A', '');
        } else {
            partno = data[i].NoDocument;
        };
        textHTML += '<tr>';
        textHTML += '<td style="width:180px; border-bottom: 1px dotted;">' + partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="AwalQty">' + separator(data[i].AwalQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Produksi">' + separator(data[i].InProdQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Pelarian">' + separator(data[i].InP99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Adjust">' + separator(data[i].InAdjustQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="(I99)">' + separator(data[i].InI99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OK(H99)">' + separator(data[i].H99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="I99">' + separator(data[i].I99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="BPFinish(B99)">' + separator(data[i].B99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="UkuranKhusus">' + separator(data[i].C99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Sample(Q99)">' + separator(data[i].Q99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OutPelarian">' + separator(data[i].OutP99) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OutAdjust">' + separator(data[i].OutAdjustQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="AkhirQty">' + separator(data[i].AkhirQty) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="M3">' + separatorcoma(data[i].M3) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="M3Produksi">' + separator(data[i].M3Produksi) + '</td>';
        textHTML += '</tr>';

        saldoawal += data[i].AwalQty;
        inproduksi += data[i].InProdQty;
        inpelarian += data[i].InP99;
        inadjust += data[i].InAdjustQty;
        inlistplank += data[i].InI99;
        ok += data[i].H99;
        outlistplank += data[i].I99;
        bp += data[i].B99;
        khusus += data[i].C99;
        sample += data[i].Q99;
        outpelarian += data[i].OutP99;
        outadjust += data[i].OutAdjustQty;
        saldoakhir += data[i].AkhirQty;
        m3 += data[i].M3;
        m3produksi += data[i].M3Produksi;


        saldoawalm3 += parseFloat(data[i].AwalQty * data[i].Volume);
        inproduksim3 += parseFloat(data[i].InProdQty * data[i].Volume);
        inpelarianm3 += parseFloat(data[i].InP99 * data[i].Volume);
        inadjustm3 += parseFloat(data[i].InAdjustQty * data[i].Volume);
        inlistplankm3 += parseFloat(data[i].InI99 * data[i].Volume);
        okm3 += parseFloat(data[i].H99 * data[i].Volume);
        outlistplankm3 += parseFloat(data[i].I99 * data[i].Volume);
        bpm3 += parseFloat(data[i].B99 * data[i].Volume);
        khususm3 += parseFloat(data[i].C99 * data[i].Volume);
        samplem3 += parseFloat(data[i].Q99 * data[i].Volume);
        outpelarianm3 += parseFloat(data[i].OutP99 * data[i].Volume);
        outadjustm3 += parseFloat(data[i].OutAdjustQty * data[i].Volume);
    }

    textHTML += '<tr>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">Total</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(saldoawal) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inproduksi) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inpelarian) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inadjust) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inlistplank) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(ok) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outlistplank) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(bp) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(khusus) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(sample) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outpelarian) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outadjust) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(saldoakhir) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(m3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(m3produksi) + '</td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">Total Meter Kubik</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(saldoawalm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inproduksim3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inpelarianm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inadjustm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inlistplankm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(okm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outlistplankm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(bpm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(khususm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(samplem3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outpelarianm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outadjustm3.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">-</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">-</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">-</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';

    textHTML += '<tr>';
    if (depo == 1) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="6" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Citeureup, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 7) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="6" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Karawang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 13) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="6" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Jombang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    }
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Diketahui</th>';
    textHTML += '<td colspan="6" style="vertical-align: middle; text-align: center; border: none;">Disetujui</th>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Dibuat</th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="6" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="6" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="6" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="6" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '</tr>';

    textHTML += '</table>';

    $("#tbwipprint").html(textHTML);
}


function DrawTableWIPM3(data) {
    var textHTML = '';
    var saldoawal = 0;
    var inproduksi = 0;
    var inpelarian = 0;
    var inadjust = 0;
    var inlistplank = 0;
    var ok = 0;
    var outlistplank = 0;
    var bp = 0;
    var khusus = 0;
    var sample = 0;
    var outpelarian = 0;
    var outadjust = 0;
    var saldoakhir = 0;

    textHTML += '<h4>Laporan Rekap Mutasi Stock WIP, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
    textHTML += '<table id="tablewipm3" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th colspan="4" style="vertical-align: middle; text-align: center;">Penerimaan</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">OK (H99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP Finish (B99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Ukuran Khusus</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Sample (Q99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno;
        if (data[i].NoDocument.charAt(0) == 'A') {
            partno = data[i].NoDocument.replace('A', '');
        } else {
            partno = data[i].NoDocument;
        };
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AwalQty">' + separatorcoma((data[i].AwalQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Produksi">' + separatorcoma((data[i].InProdQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Pelarian">' + separatorcoma((data[i].InP99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Adjust">' + separatorcoma((data[i].InAdjustQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="(I99)">' + separatorcoma((data[i].InI99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OK(H99)">' + separatorcoma((data[i].H99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="I99">' + separatorcoma((data[i].I99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="BPFinish(B99)">' + separatorcoma((data[i].B99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="UkuranKhusus">' + separatorcoma((data[i].C99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Sample(Q99)">' + separatorcoma((data[i].Q99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OutPelarian">' + separatorcoma((data[i].OutP99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="OutAdjust">' + separatorcoma((data[i].OutAdjustQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="AkhirQty">' + separatorcoma((data[i].AkhirQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        saldoawal += parseFloat(data[i].AwalQty * data[i].Volume);
        inproduksi += parseFloat(data[i].InProdQty * data[i].Volume);
        inpelarian += parseFloat(data[i].InP99 * data[i].Volume);
        inadjust += parseFloat(data[i].InAdjustQty * data[i].Volume);
        inlistplank += parseFloat(data[i].InI99 * data[i].Volume);
        ok += parseFloat(data[i].H99 * data[i].Volume);
        outlistplank += parseFloat(data[i].I99 * data[i].Volume);
        bp += parseFloat(data[i].B99 * data[i].Volume);
        khusus += parseFloat(data[i].C99 * data[i].Volume);
        sample += parseFloat(data[i].Q99 * data[i].Volume);
        outpelarian += parseFloat(data[i].OutP99 * data[i].Volume);
        outadjust += parseFloat(data[i].OutAdjustQty * data[i].Volume);
        saldoakhir += parseFloat(data[i].AkhirQty * data[i].Volume);
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total</td>';
    textHTML += '<td class="text-right">' + separatorcoma(saldoawal.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inproduksi.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inpelarian.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inadjust.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(inlistplank.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(ok.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outlistplank.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(bp.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(khusus.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(sample.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outpelarian.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(outadjust.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right">' + separatorcoma(saldoakhir.toFixed(2)) + '</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbwipm3").html(textHTML);
    $("#tablewipm3").DataTable({
        "pageLength": 50
    });
}

function DrawTableWIPM3Print(data) {
    var textHTML = '';
    var saldoawal = 0;
    var inproduksi = 0;
    var inpelarian = 0;
    var inadjust = 0;
    var inlistplank = 0;
    var ok = 0;
    var outlistplank = 0;
    var bp = 0;
    var khusus = 0;
    var sample = 0;
    var outpelarian = 0;
    var outadjust = 0;
    var saldoakhir = 0;

    textHTML += '<table id="tablewipm3print" class="table" style="width: 100%; border-collapse: collapse; border: thin solid black;" >';

    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<td colspan="14" style="vertical-align: middle; text-align: center; border: none;"><h4>Laporan Rekap Mutasi Stock Bahan Jadi Tahap 1, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4></th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Partno</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Awal</th>';
    textHTML += '<th colspan="4" style="vertical-align: middle; text-align: center; border: thin solid black;">Penerimaan</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center; border: thin solid black;">Pengeluaran</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Produksi</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">OK (H99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">(I99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP Finish (B99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Ukuran Khusus</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Sample (Q99)</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Pelarian</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        var partno;
        if (data[i].NoDocument.charAt(0) == 'A') {
            partno = data[i].NoDocument.replace('A', '');
        } else {
            partno = data[i].NoDocument;
        };
        textHTML += '<tr>';
        textHTML += '<td style="width:180px; border-bottom: 2px dotted;"">' + partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="AwalQty">' + separatorcoma((data[i].AwalQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Produksi">' + separatorcoma((data[i].InProdQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Pelarian">' + separatorcoma((data[i].InP99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Adjust">' + separatorcoma((data[i].InAdjustQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="(I99)">' + separatorcoma((data[i].InI99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OK(H99)">' + separatorcoma((data[i].H99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="I99">' + separatorcoma((data[i].I99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="BPFinish(B99)">' + separatorcoma((data[i].B99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="UkuranKhusus">' + separatorcoma((data[i].C99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Sample(Q99)">' + separatorcoma((data[i].Q99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OutPelarian">' + separatorcoma((data[i].OutP99 * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="OutAdjust">' + separatorcoma((data[i].OutAdjustQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="AkhirQty">' + separatorcoma((data[i].AkhirQty * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        saldoawal += parseFloat(data[i].AwalQty * data[i].Volume);
        inproduksi += parseFloat(data[i].InProdQty * data[i].Volume);
        inpelarian += parseFloat(data[i].InP99 * data[i].Volume);
        inadjust += parseFloat(data[i].InAdjustQty * data[i].Volume);
        inlistplank += parseFloat(data[i].InI99 * data[i].Volume);
        ok += parseFloat(data[i].H99 * data[i].Volume);
        outlistplank += parseFloat(data[i].I99 * data[i].Volume);
        bp += parseFloat(data[i].B99 * data[i].Volume);
        khusus += parseFloat(data[i].C99 * data[i].Volume);
        sample += parseFloat(data[i].Q99 * data[i].Volume);
        outpelarian += parseFloat(data[i].OutP99 * data[i].Volume);
        outadjust += parseFloat(data[i].OutAdjustQty * data[i].Volume);
        saldoakhir += parseFloat(data[i].AkhirQty * data[i].Volume);
    }
    textHTML += '</tbody>';

    textHTML += '<tr>';
    textHTML += '<td style="border: thin solid black;">Total</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(saldoawal.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(inproduksi.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(inpelarian.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(inadjust.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(inlistplank.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(ok.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(outlistplank.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(bp.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(khusus.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(sample.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(outpelarian.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(outadjust.toFixed(2)) + '</td>';
    textHTML += '<td style="border: thin solid black;" class="text-right">' + separatorcoma(saldoakhir.toFixed(2)) + '</td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    if (depo == 1) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="4" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Citeureup, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 7) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="4" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Karawang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 13) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="4" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Jombang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    }
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Diketahui</th>';
    textHTML += '<td colspan="4" style="vertical-align: middle; text-align: center; border: none;">Disetujui</th>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Dibuat</th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="4" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="4" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="4" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="4" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '</tr>';

    textHTML += '</table>';

    $("#tbwipm3print").html(textHTML);
}


function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablewipprint');

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
    tab_text = tab_text.replace(/<img[^>]*>/gi, "");
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))
    {
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
    var divToPrint = document.getElementById("tablewipprint");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}


function fnExcelReportm3() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablewipm3print');

    for (j = 0; j < tab.rows.length; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");
    tab_text = tab_text.replace(/<img[^>]*>/gi, "");
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, "");

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))
    {
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

function printDatam3() {
    var divToPrint = document.getElementById("tablewipm3print");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}