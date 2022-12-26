var month;
var isPertama = true;
$("#filtertgl").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
var thisyear = $("#filtertgl").val().split('-')[2];
var depo = 0;
var bln;
var thn;

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    month = new Date().getMonth();
    if (month.constructor.length > 1) {
        $('#bulan').val(month + 1);
    } else {
        $('#bulan').val("0" + (month + 1));
    }
    RequestDepoID();
    for (var selecttahun = 2019; selecttahun < thisyear + 2; ++selecttahun) {
        $("#tahun").append('<option value="' + selecttahun + '" >' + selecttahun + '</option>');
    }
    $('#tahun').val(thisyear);
});

$("#modalbj").on("hidden.bs.modal", function () {
    $("#tbj").html("");
});

$("#modalbjm3").on("hidden.bs.modal", function () {
    $("#tbjm3").html("");
});

function RequestDepoID() {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetDepoID",
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
    var strQtyMonth;
    var strQtyLastMonth;
    var thnbln;
    var thnblnhari
    bln = $('#bulan').val();
    thn = $('#tahun').val();

    if ($('#harian').is(':checked')) {
        thnbln = $("#filtertgl").val().split('-')[2] + $("#filtertgl").val().split('-')[1];
        thnblnhari = $("#filtertgl").val().split('-')[2] + $("#filtertgl").val().split('-')[1] + $("#filtertgl").val().split('-')[0];
        bln = $("#filtertgl").val().split('-')[1];
        thn = $("#filtertgl").val().split('-')[2];
    } else {
        bln = $('#bulan').val();
        thn = $('#tahun').val();
        thnbln = thn + bln;
    }

    if (bln == "01") {
        strQtyLastMonth = "DesQty";
        strQtyMonth = "JanQty";
        thn = thn - 1;
    }
    else if (bln == 2) {
        strQtyMonth = "FebQty";
        strQtyLastMonth = "JanQty";
    }
    else if (bln == 3) {
        strQtyMonth = "MarQty";
        strQtyLastMonth = "FebQty";
    }
    else if (bln == 4) {
        strQtyMonth = "AprQty";
        strQtyLastMonth = "MarQty";
    }
    else if (bln == 5) {
        strQtyMonth = "MeiQty";
        strQtyLastMonth = "AprQty";
    }
    else if (bln == 6) {
        strQtyMonth = "JunQty";
        strQtyLastMonth = "MeiQty";
    }
    else if (bln == 7) {
        strQtyMonth = "JulQty";
        strQtyLastMonth = "JunQty";
    }
    else if (bln == 8) {
        strQtyMonth = "AguQty";
        strQtyLastMonth = "JulQty";
    }
    else if (bln == 9) {
        strQtyMonth = "SepQty";
        strQtyLastMonth = "AguQty";
    }
    else if (bln == 10) {
        strQtyMonth = "OktQty";
        strQtyLastMonth = "SepQty";
    }
    else if (bln == 11) {
        strQtyMonth = "NovQty";
        strQtyLastMonth = "OktQty";
    }
    else if (bln == 12) {
        strQtyMonth = "DesQty";
        strQtyLastMonth = "NovQty";
    }

    if ($('#harian').is(':checked')) {
        if ($('#meterkubik').is(':checked')) {
            if ($('#utuh').is(':checked')) {
                $('#modalbjm3').modal('show');
                RequestBJM3Harian(thnbln, strQtyLastMonth, thn, thnblnhari)
            }
            else {
                $('#modalbjm3').modal('show');
                RequestBJM3Harian(thnbln, strQtyLastMonth, thn, thnblnhari)
            }
        }
        else {
            if ($('#utuh').is(':checked')) {
                $('#modalbj').modal('show');
                RequestBJKonversi(thnbln, strQtyLastMonth, thn)
            } else {
                $('#modalbj').modal('show');
                RequestBJHarian(thnbln, strQtyLastMonth, thn, thnblnhari)
            }
        }
    } else {
        if ($('#meterkubik').is(':checked')) {
            if ($('#utuh').is(':checked')) {
                $('#modalbjm3').modal('show');
                RequestBJM3(thnbln, strQtyLastMonth, thn)
            } else {
                $('#modalbjm3').modal('show');
                RequestBJM3(thnbln, strQtyLastMonth, thn)
            }
        }
        else {
            if ($('#utuh').is(':checked')) {
                $('#modalbj').modal('show');
                RequestBJKonversi(thnbln, strQtyLastMonth, thn)
            } else {
                $('#modalbj').modal('show');
                RequestBJ(thnbln, strQtyLastMonth, thn)
            }
        }
    }
});


function RequestBJ(thnbln, lastmonth, thn) {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetLapMutasiBJ",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastmonth: lastmonth, thn: thn }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBJ(data);
            DrawTableBJPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestBJHarian(thnbln, lastmonth, thn, thnbln0) {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetLapMutasiBJHarian",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastmonth: lastmonth, thn: thn, thnbln0: thnbln0 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBJ(data);
            DrawTableBJPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function RequestBJM3(thnbln, lastmonth, thn) {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetLapMutasiBJ",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastmonth: lastmonth, thn: thn }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBJM3(data);
            DrawTableBJM3Print(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestBJM3Harian(thnbln, lastmonth, thn, thnbln0) {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetLapMutasiBJHarian",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastmonth: lastmonth, thn: thn, thnbln0: thnbln0 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBJM3(data);
            DrawTableBJM3Print(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestBJKonversi(thnbln, lastmonth, thn) {
    $.ajax({
        url: "LapMutasiStockBJ.aspx/GetLapMutasiBJKonversi",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ thnbln: thnbln, lastmonth: lastmonth, thn: thn }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            data = $.parseJSON(data);
            DrawTableBJ(data);
            DrawTableBJPrint(data);
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function DrawTableBJ(data) {
    var textHTML = '';
    var insaldoawal = 0;
    var inwip = 0;
    var inbp = 0;
    var inbj = 0;
    var inbs = 0;
    var inretur = 0;
    var inadjust = 0;
    var outcustomer = 0;
    var outbp = 0;
    var outbj = 0;
    var outsample = 0;
    var outbs = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;

    var insaldoawalm3 = 0;
    var inwipm3 = 0;
    var inbpm3 = 0;
    var inbjm3 = 0;
    var inbsm3 = 0;
    var inreturm3 = 0;
    var inadjustm3 = 0;
    var outcustomerm3 = 0;
    var outbpm3 = 0;
    var outbjm3 = 0;
    var outsamplem3 = 0;
    var outbsm3 = 0;
    var outadjustm3 = 0;

    textHTML += '<h4>Laporan Rekap Mutasi Stock Barang Jadi, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
    textHTML += '<table id="tablebj" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center;">Penerimaan Dari</th>';
    textHTML += '<th colspan="6" style="vertical-align: middle; text-align: center;">Pengeluaran Ke</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Meter Kubik</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">WIP T1</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Retur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Customer</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Sample</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno+'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Awal">' + separator(data[i].Awal) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TWIP">' + separator(data[i].TWIP) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBP">' + separator(data[i].TBP) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBJ">' + separator(data[i].TBJ) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBS">' + separator(data[i].TBS) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TRETUR">' + separator(data[i].TRETUR) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TAdjust">' + separator(data[i].TAdjust) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KKirim">' + separator(data[i].KKirim) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBP">' + separator(data[i].KBP) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBJ">' + separator(data[i].KBJ) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KSample">' + separator(data[i].KSample) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBS">' + separator(data[i].KBS) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KAdjust">' + separator(data[i].KAdjust) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Total">' + separator(data[i].Total) +'</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="M3">' + separatorcoma((data[i].Total * data[i].Volume).toFixed(2)) +'</td>';
        textHTML += '</tr>';

        insaldoawal += data[i].Awal;
        inwip += data[i].TWIP;
        inbp += data[i].TBP;
        inbj += data[i].TBJ;
        inbs += data[i].TBS;
        inretur += data[i].TRETUR;
        inadjust += data[i].TAdjust;
        outcustomer += data[i].KKirim;
        outbp += data[i].KBP;
        outbj += data[i].KBJ;
        outsample += data[i].KSample;
        outbs += data[i].KBS;
        outadjust += data[i].KAdjust;
        saldoakhir += data[i].Total;
        m3 += data[i].Total * data[i].Volume;

        insaldoawalm3 += parseFloat(data[i].Awal * data[i].Volume);
        inwipm3 += parseFloat(data[i].TWIP * data[i].Volume);
        inbpm3 += parseFloat(data[i].TBP * data[i].Volume);
        inbjm3 += parseFloat(data[i].TBJ * data[i].Volume);
        inbsm3 += parseFloat(data[i].TBS * data[i].Volume);
        inreturm3 += parseFloat(data[i].TRETUR * data[i].Volume);
        inadjustm3 += parseFloat(data[i].TAdjust * data[i].Volume);
        outcustomerm3 += parseFloat(data[i].KKirim * data[i].Volume);
        outbpm3 += parseFloat(data[i].KBP * data[i].Volume);
        outbjm3 += parseFloat(data[i].KBJ * data[i].Volume);
        outsamplem3 += parseFloat(data[i].KSample * data[i].Volume);
        outbsm3 += parseFloat(data[i].KBS * data[i].Volume);
        outadjustm3 += parseFloat(data[i].KAdjust * data[i].Volume);
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total</td>';
    textHTML += '<td class="text-right" id="insaldoawal">' + separator(insaldoawal) +'</td>';
    textHTML += '<td class="text-right" id="inwip">' + separator(inwip) +'</td>';
    textHTML += '<td class="text-right" id="inbp">' + separator(inbp) +'</td>';
    textHTML += '<td class="text-right" id="inbj">' + separator(inbj) +'</td>';
    textHTML += '<td class="text-right" id="inbs">' + separator(inbs) +'</td>';
    textHTML += '<td class="text-right" id="inretur">' + separator(inretur) +'</td>';
    textHTML += '<td class="text-right" id="inadjust">' + separator(inadjust) +'</td>';
    textHTML += '<td class="text-right" id="outcustomer">' + separator(outcustomer) +'</td>';
    textHTML += '<td class="text-right" id="outbp">' + separator(outbp) +'</td>';
    textHTML += '<td class="text-right" id="outbj">' + separator(outbj) +'</td>';
    textHTML += '<td class="text-right" id="outsample">' + separator(outsample) +'</td>';
    textHTML += '<td class="text-right" id="outbs">' + separator(outbs) +'</td>';
    textHTML += '<td class="text-right" id="outadjust">' + separator(outadjust) +'</td>';
    textHTML += '<td class="text-right" id="saldoakhir">' + separator(saldoakhir) +'</td>';
    textHTML += '<td class="text-right" id="m3">' + separatorcoma(m3.toFixed(2)) +'</td>';
    textHTML += '</tr>';


    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total Meter Kubik</td>';
    textHTML += '<td class="text-right" id="insaldoawal">' + separatorcoma((insaldoawalm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inwip">' + separatorcoma((inwipm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbp">' + separatorcoma((inbpm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbj">' + separatorcoma((inbjm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbs">' + separatorcoma((inbsm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inretur">' + separatorcoma((inreturm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inadjust">' + separatorcoma((inadjustm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outcustomer">' + separatorcoma((outcustomerm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbp">' + separatorcoma((outbpm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbj">' + separatorcoma((outbjm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outsample">' + separatorcoma((outsamplem3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbs">' + separatorcoma((outbsm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outadjust">' + separatorcoma((outadjustm3).toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="saldoakhir">-</td>';
    textHTML += '<td class="text-right" id="m3">-</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbj").html(textHTML);
    $("#tablebj").DataTable({
        "pageLength": 50
    });
}

function DrawTableBJPrint(data) {
    var textHTML = '';
    var insaldoawal = 0;
    var inwip = 0;
    var inbp = 0;
    var inbj = 0;
    var inbs = 0;
    var inretur = 0;
    var inadjust = 0;
    var outcustomer = 0;
    var outbp = 0;
    var outbj = 0;
    var outsample = 0;
    var outbs = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;

    var insaldoawalm3 = 0;
    var inwipm3 = 0;
    var inbpm3 = 0;
    var inbjm3 = 0;
    var inbsm3 = 0;
    var inreturm3 = 0;
    var inadjustm3 = 0;
    var outcustomerm3 = 0;
    var outbpm3 = 0;
    var outbjm3 = 0;
    var outsamplem3 = 0;
    var outbsm3 = 0;
    var outadjustm3 = 0;

    textHTML += '<table id="tablebjprint" class="table"  style="width: 100%; border-collapse: collapse; border: thin solid black;">';

    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<td colspan="16" style="vertical-align: middle; text-align: center; border: none;"><h4>Laporan Rekap Mutasi Stock Barang Jadi, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4></th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Partno</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center; border: thin solid black;">Penerimaan Dari</th>';
    textHTML += '<th colspan="6" style="vertical-align: middle; text-align: center; border: thin solid black;">Pengeluaran Ke</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Akhir</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Meter Kubik</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">WIP T1</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Retur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Customer</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Sample</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:180px; border-bottom: 1px dotted;">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Awal">' + separator(data[i].Awal) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TWIP">' + separator(data[i].TWIP) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBP">' + separator(data[i].TBP) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBJ">' + separator(data[i].TBJ) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBS">' + separator(data[i].TBS) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TRETUR">' + separator(data[i].TRETUR) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TAdjust">' + separator(data[i].TAdjust) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KKirim">' + separator(data[i].KKirim) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBP">' + separator(data[i].KBP) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBJ">' + separator(data[i].KBJ) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KSample">' + separator(data[i].KSample) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBS">' + separator(data[i].KBS) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KAdjust">' + separator(data[i].KAdjust) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Total">' + separator(data[i].Total) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="M3">' + separatorcoma((data[i].Total * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        insaldoawal += data[i].Awal;
        inwip += data[i].TWIP;
        inbp += data[i].TBP;
        inbj += data[i].TBJ;
        inbs += data[i].TBS;
        inretur += data[i].TRETUR;
        inadjust += data[i].TAdjust;
        outcustomer += data[i].KKirim;
        outbp += data[i].KBP;
        outbj += data[i].KBJ;
        outsample += data[i].KSample;
        outbs += data[i].KBS;
        outadjust += data[i].KAdjust;
        saldoakhir += data[i].Total;
        m3 += data[i].Total * data[i].Volume;

        insaldoawalm3 += parseFloat(data[i].Awal * data[i].Volume);
        inwipm3 += parseFloat(data[i].TWIP * data[i].Volume);
        inbpm3 += parseFloat(data[i].TBP * data[i].Volume);
        inbjm3 += parseFloat(data[i].TBJ * data[i].Volume);
        inbsm3 += parseFloat(data[i].TBS * data[i].Volume);
        inreturm3 += parseFloat(data[i].TRETUR * data[i].Volume);
        inadjustm3 += parseFloat(data[i].TAdjust * data[i].Volume);
        outcustomerm3 += parseFloat(data[i].KKirim * data[i].Volume);
        outbpm3 += parseFloat(data[i].KBP * data[i].Volume);
        outbjm3 += parseFloat(data[i].KBJ * data[i].Volume);
        outsamplem3 += parseFloat(data[i].KSample * data[i].Volume);
        outbsm3 += parseFloat(data[i].KBS * data[i].Volume);
        outadjustm3 += parseFloat(data[i].KAdjust * data[i].Volume);
    }

    textHTML += '<tr>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">Total</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(insaldoawal) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inwip) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inbp) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inbj) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inbs) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inretur) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(inadjust) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outcustomer) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outbp) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outbj) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outsample) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outbs) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(outadjust) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separator(saldoakhir) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(m3.toFixed(2)) + '</td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">Total Meter Kubik</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((insaldoawalm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inwipm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inbpm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inbjm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inbsm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inreturm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((inadjustm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outcustomerm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outbpm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outbjm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outsamplem3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outbsm3).toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma((outadjustm3).toFixed(2)) + '</td>';
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

    $("#tbjprint").html(textHTML);
}

function DrawTableBJM3(data) {
    var textHTML = '';
    var insaldoawal = 0;
    var inwip = 0;
    var inbp = 0;
    var inbj = 0;
    var inbs = 0;
    var inretur = 0;
    var inadjust = 0;
    var outcustomer = 0;
    var outbp = 0;
    var outbj = 0;
    var outsample = 0;
    var outbs = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;

    textHTML += '<h4>Laporan Rekap Mutasi Stock Barang Jadi, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4>';
    textHTML += '<table id="tablebjm3" class="table table-striped table-bordered table-hover display nowrap" style="width: 100%" border="4">';
    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Partno</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center;">Penerimaan Dari</th>';
    textHTML += '<th colspan="6" style="vertical-align: middle; text-align: center;">Pengeluaran Ke</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Saldo Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">WIP T1</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Retur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Customer</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Sample</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:150px">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Awal">' + separatorcoma((data[i].Awal * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TWIP">' + separatorcoma((data[i].TWIP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBP">' + separatorcoma((data[i].TBP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBJ">' + separatorcoma((data[i].TBJ * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TBS">' + separatorcoma((data[i].TBS * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TRETUR">' + separatorcoma((data[i].TRETUR * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="TAdjust">' + separatorcoma((data[i].TAdjust * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KKirim">' + separatorcoma((data[i].KKirim * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBP">' + separatorcoma((data[i].KBP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBJ">' + separatorcoma((data[i].KBJ * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KSample">' + separatorcoma((data[i].KSample * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KBS">' + separatorcoma((data[i].KBS * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="KAdjust">' + separatorcoma((data[i].KAdjust * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center;" class="Total">' + separatorcoma((data[i].Total * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        insaldoawal += parseFloat((data[i].Awal * data[i].Volume));
        inwip += parseFloat((data[i].TWIP * data[i].Volume));
        inbp += parseFloat((data[i].TBP * data[i].Volume));
        inbj += parseFloat((data[i].TBJ * data[i].Volume));
        inbs += parseFloat((data[i].TBS * data[i].Volume));
        inretur += parseFloat((data[i].TRETUR * data[i].Volume));
        inadjust += parseFloat((data[i].TAdjust * data[i].Volume));
        outcustomer += parseFloat((data[i].KKirim * data[i].Volume));
        outbp += parseFloat((data[i].KBP * data[i].Volume));
        outbj += parseFloat((data[i].KBJ * data[i].Volume));
        outsample += parseFloat((data[i].KSample * data[i].Volume));
        outbs += parseFloat((data[i].KBS * data[i].Volume));
        outadjust += parseFloat((data[i].KAdjust * data[i].Volume));
        saldoakhir += parseFloat((data[i].Total * data[i].Volume));
    }

    textHTML += '<tr>';
    textHTML += '<td class="text-right">Total</td>';
    textHTML += '<td class="text-right" id="insaldoawalm3">' + separatorcoma(insaldoawal.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inwipm3">' + separatorcoma(inwip.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbpm3">' + separatorcoma(inbp.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbjm3">' + separatorcoma(inbj.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inbsm3">' + separatorcoma(inbs.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inreturm3">' + separatorcoma(inretur.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="inadjustm3">' + separatorcoma(inadjust.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outcustomerm3">' + separatorcoma(outcustomer.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbpm3">' + separatorcoma(outbp.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbjm3">' + separatorcoma(outbj.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outsamplem3">' + separatorcoma(outsample.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outbsm3">' + separatorcoma(outbs.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="outadjustm3">' + separatorcoma(outadjust.toFixed(2)) + '</td>';
    textHTML += '<td class="text-right" id="saldoakhirm3">' + separatorcoma(saldoakhir.toFixed(2)) + '</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';
    textHTML += '</table>';

    $("#tbjm3").html(textHTML);
    $("#tablebjm3").DataTable({
        "pageLength": 50
    });
}

function DrawTableBJM3Print(data) {
    var textHTML = '';
    var insaldoawal = 0;
    var inwip = 0;
    var inbp = 0;
    var inbj = 0;
    var inbs = 0;
    var inretur = 0;
    var inadjust = 0;
    var outcustomer = 0;
    var outbp = 0;
    var outbj = 0;
    var outsample = 0;
    var outbs = 0;
    var outadjust = 0;
    var saldoakhir = 0;
    var m3 = 0;
    textHTML += '<table id="tablebjm3print" class="table" style="width: 100%; border-collapse: collapse; border: thin solid black;">';

    textHTML += '<thead>';
    textHTML += '<tr>';
    textHTML += '<td colspan="15" style="vertical-align: middle; text-align: center; border: none;"><h4>Laporan Rekap Mutasi Stock Barang, Periode : ' + $("#bulan option:selected").text() + ' ' + $('#tahun').val() + '</h4></th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Partno</th>';
    textHTML += '<th colspan="7" style="vertical-align: middle; text-align: center; border: thin solid black;">Penerimaan Dari</th>';
    textHTML += '<th colspan="6" style="vertical-align: middle; text-align: center; border: thin solid black;">Pengeluaran Ke</th>';
    textHTML += '<th rowspan="2" style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Akhir</th>';
    textHTML += '</tr>';
    textHTML += '<tr>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Saldo Awal</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">WIP T1</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Retur</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Customer</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BP</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BJ</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Sample</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">BS</th>';
    textHTML += '<th style="vertical-align: middle; text-align: center; border: thin solid black;">Adjust</th>';
    textHTML += '</tr>';
    textHTML += '</thead>';
    textHTML += '<tbody>';

    for (var i = 0; i < data.length; i++) {
        textHTML += '<tr>';
        textHTML += '<td style="width:180px; border-bottom: 1px dotted;">' + data[i].Partno + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Awal">' + separatorcoma((data[i].Awal * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TWIP">' + separatorcoma((data[i].TWIP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBP">' + separatorcoma((data[i].TBP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBJ">' + separatorcoma((data[i].TBJ * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TBS">' + separatorcoma((data[i].TBS * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TRETUR">' + separatorcoma((data[i].TRETUR * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="TAdjust">' + separatorcoma((data[i].TAdjust * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KKirim">' + separatorcoma((data[i].KKirim * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBP">' + separatorcoma((data[i].KBP * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBJ">' + separatorcoma((data[i].KBJ * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KSample">' + separatorcoma((data[i].KSample * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KBS">' + separatorcoma((data[i].KBS * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="KAdjust">' + separatorcoma((data[i].KAdjust * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px dotted;" class="Total">' + separatorcoma((data[i].Total * data[i].Volume).toFixed(2)) + '</td>';
        textHTML += '</tr>';

        insaldoawal += parseFloat((data[i].Awal * data[i].Volume));
        inwip += parseFloat((data[i].TWIP * data[i].Volume));
        inbp += parseFloat((data[i].TBP * data[i].Volume));
        inbj += parseFloat((data[i].TBJ * data[i].Volume));
        inbs += parseFloat((data[i].TBS * data[i].Volume));
        inretur += parseFloat((data[i].TRETUR * data[i].Volume));
        inadjust += parseFloat((data[i].TAdjust * data[i].Volume));
        outcustomer += parseFloat((data[i].KKirim * data[i].Volume));
        outbp += parseFloat((data[i].KBP * data[i].Volume));
        outbj += parseFloat((data[i].KBJ * data[i].Volume));
        outsample += parseFloat((data[i].KSample * data[i].Volume));
        outbs += parseFloat((data[i].KBS * data[i].Volume));
        outadjust += parseFloat((data[i].KAdjust * data[i].Volume));
        saldoakhir += parseFloat((data[i].Total * data[i].Volume));
    }

    textHTML += '<tr>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">Total</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(insaldoawal.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inwip.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inbp.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inbj.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inbs.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inretur.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(inadjust.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outcustomer.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outbp.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outbj.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outsample.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outbs.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(outadjust.toFixed(2)) + '</td>';
    textHTML += '<td style="vertical-align: middle; text-align: center; border-bottom: 1px solid black;">' + separatorcoma(saldoakhir.toFixed(2)) + '</td>';
    textHTML += '</tr>';
    textHTML += '</tbody>';

    textHTML += '<tr>';
    if (depo == 1) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Citeureup, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 7) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Karawang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    } else if (depo == 13) {
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"></th>';
        textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;"><b>Jombang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></th>'
    }
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Diketahui</th>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Disetujui</th>';
    textHTML += '<td colspan="5" style="vertical-align: middle; text-align: center; border: none;">Dibuat</th>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; border: none;"></td>';
    textHTML += '</tr>';

    textHTML += '<tr>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '<td  colspan="5" style="vertical-align: middle; text-align: center; padding-top: 70px; border: none;">(______________)</td>';
    textHTML += '</tr>';

    textHTML += '</table>';

    $("#tbjm3print").html(textHTML);
}

function printData() {
    var divToPrint = document.getElementById("tablebjprint");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}


function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablebjprint');

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

function fnExcelReportm3() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablebjm3print');

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
    var divToPrint = document.getElementById("tablebjm3print");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}
