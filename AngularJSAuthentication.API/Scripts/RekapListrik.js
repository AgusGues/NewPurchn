$(document).ready(function () {
    let tahun = new Date().getFullYear()
    $("#txttahun").val(tahun.toString());
    $("#txttahun2").val(tahun.toString());
});

$('#modalpemakaian ').on('hidden.bs.modal', function () {
    location.reload();
})
var bulan;
var tahun;
var bulan2;
var tahun2;

function RekapPemakaian() {
    tahun = $("#txttahun").val();
    bulan = $("#ddlbulan").val();
    var tgl = tahun + bulan;
    if (bulan == "") {
        alert("Pilih Bulan !");
    }
    else {
        $.ajax({
            url: "InputPemakaianListrik.aspx/GetPemakaian",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ tgl: tgl }),
            success: function (data) {
                data = data.d
                data.data = data.results
                delete data.results

                data = $.parseJSON(data);

                $(function () {
                    $.each(data, function (i, data) {
                        var $tr = $('<tr>').append(
                            $('<td>').text(data.Tanggal.substring(0, 10)),
                            $('<td class="kwhpjt">').text(data.kWhPJT),
                            $('<td class="result1">').text(data.Result1),
                            $('<td class="kvarhpjt">').text(data.kVarhPJT),
                            $('<td class="result2">').text(data.Result2),
                            $('<td class="kwhpln">').text(data.kWhPLN),
                            $('<td class="result3">').text(data.Result3),
                            $('<td class="kvarhpln">').text(data.kVarhPLN),
                            $('<td class="result4">').text(data.Result4),
                            $('<td class="totalkwh">').text(data.TotalkWh),
                            $('<td class="totalkvarh">').text(data.TotalkVarh)
                        ).appendTo('#tablepemakaian');
                    });
                });
                hitung();
            }
        });
    
        $('#modalpemakaian').modal({
            show: 'true'
        });
    }
}

function hitung() {
    var tds = document.getElementById('tablepemakaian').getElementsByTagName('td');
    var result1 = 0;
    var result2 = 0;
    var result3 = 0;
    var result4 = 0;
    var totalkwh = 0;
    var totalkvarh = 0;
    
    for (var i = 0; i < tds.length; i++) {
        if (tds[i].className == 'result1') {
            result1 += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }
        if (tds[i].className == 'result2') {
            result2 += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }
        if (tds[i].className == 'result3') {
            result3 += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }
        if (tds[i].className == 'result4') {
            result4 += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }
        if (tds[i].className == 'totalkwh') {
            totalkwh += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }
        if (tds[i].className == 'totalkvarh') {
            totalkvarh += isNaN(tds[i].innerHTML) ? 0 : parseFloat(tds[i].innerHTML);
        }

    }
    document.getElementById("result1").innerHTML = result1;
    document.getElementById("result2").innerHTML = result2;
    document.getElementById("result3").innerHTML = result3;
    document.getElementById("result4").innerHTML = result4;
    document.getElementById("totalkwh").innerHTML = totalkwh;
    document.getElementById("totalkvarh").innerHTML = totalkvarh;
}

function RekapEfisiensi() {
    tahun2 = $("#txttahun2").val();
    bulan2 = $("#ddlbulan2").val();
    var tgl = tahun2 + bulan2;
    
    if (bulan2 == "") {
        alert("Pilih Bulan !");
    }
    else {
        $.ajax({
            url: "InputPemakaianListrik.aspx/GetEfisiensi",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ tgl: tgl }),
            success: function (data) {
                $("#tableEfisiensi").DataTable().destroy();
                $('#tableEfisiensi').empty();
                $("#tableEfisiensi").append('<tfoot><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tfoot>');

                datatable = $.parseJSON(data.d);
                var oTblReport = $("#tableEfisiensi");
                oTblReport.DataTable({
                    "data": datatable,
                    //"responsive": true,
                    "autoWidth": true,
                    fixedHeader: {
                        header: true,
                        footer: true
                    },
                    dom: 'Blfrtip',
                    buttons: [
                        { extend: 'copy', footer: true },
                        { extend: 'csv', footer: true },
                        { extend: 'excel', title: 'Lembar Pemantauan Efisiensi Pemakaian Listrik', footer: true },
                        {
                            extend: 'pdf', title: 'Lembar Pemantauan Efisiensi Pemakaian Listrik',
                            orientation: 'landscape', footer: true,
                        },
                        {
                            extend: 'print', title: 'Lembar Pemantauan Efisiensi Pemakaian Listrik',
                            message: 'PERIODE ' + bulan2 + ' ' + tahun2 + ' ',
                            orientation: 'landscape',
                            pageSize: 'A3', footer: true,
                            customize: function (win) {
                                $(win.document.body).addClass('white-bg');
                                $(win.document.body).css('font-size', '10px');
                                $(win.document.body).find('table')
                                        .addClass('compact')
                                        .css('font-size', 'inherit');
                            }
                        }],
                    "columns": [
                        {
                            "className": '',
                            "data": "Tanggal",
                            title: "Tanggal",
                            rowspan: "3",
                            "render": function (data, type, row) {
                                var tgl = data;
                                var thn = tgl.substring(0, 4);
                                var bln = tgl.substring(5, 7);
                                var tgla = tgl.substring(8, 10);
                                return '<td>' + tgla + "/" + bln + "/" + thn + '</td>';
                            }
                        },
                        { "data": "TotalkWh", title: "kWh Terpakai" },
                        { "data": "TotalkVarh", title: "kVarh Terpakai" },
                        { "data": "Output", title: "Output Produksi (M3)" },
                        { "data": "kWhM3", title: "kWh/M3" },
                        { "data": "Prosentase", title: "Prosentase (%)" },
                        { "data": "Keterangan", title: "Keterangan" }
                    ],

                    "footerCallback": function ( row, data, start, end, display ) {
                        var api = this.api(), data;
 
                        // converting to interger to find total
                        var intVal = function ( i ) {
                            return typeof i === 'string' ?
                                i.replace(/[\$,]/g, '')*1 :
                                typeof i === 'number' ?
                                i : 0;
                        };
 
                        // computing column Total of the complete result 
                        var kwh = api
                            .column( 1 )
                            .data()
                            .reduce( function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0 );
				
                        var kvarh = api
                                .column( 2 )
                                .data()
                                .reduce( function (a, b) {
                                    return intVal(a) + intVal(b);
                                }, 0 );
				
                        var output = api
                            .column( 3 )
                            .data()
                            .reduce( function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0 );
				
                        var kwhm3 = kwh/output;
				
                        var prosentase = kwhm3
			
				
                        // Update footer by showing the total with the reference of the column index 
                        $( api.column( 0 ).footer() ).html('Total');
                        $(api.column(1).footer()).html(kwh);
                        $(api.column(2).footer()).html(kvarh);
                        $(api.column(3).footer()).html(output);
                        $(api.column(4).footer()).html(kwhm3.toFixed(2));
                        $(api.column(5).footer()).html(prosentase.toFixed(2));
                    },
                    //"processing": true,
                    //"serverSide": true
                    
                });
               
            }
        });
    
        $('#modalefisiensi').modal({
            show: 'true'
        });
    }
}

function InputPemakaian() {
    obj = {};
    obj.Tanggal = $("#txttgl").val();
    obj.Line = $("#txtline").val();
    obj.kWhPJT = $("#txtkwhpjt").val();
    obj.kVarhPJT = $("#txtkvarhpjt").val();
    obj.kWhPLN = $("#txtkwhpln").val();
    obj.kVarhPLN = $("#txtkvarhpln").val();
    if (obj.Tanggal == "" || obj.Line == "" || obj.kWhPJT == "" || obj.kVarhPJT == "" || obj.kWhPLN == "" || obj.kVarhPLN == "")
    {
        alert("Tidak Boleh Kosong !");
    }
    else
    {
        $.ajax({
            url: 'InputPemakaianListrik.aspx/InputPemakaian',
            type: 'POST',
            data: JSON.stringify({ obj: obj }),
            contentType: "application/json; charset=utf-8",
            dataType: 'text',
            success: function (data) {
                console.log(data);
                alert("Data Berhasil Ditambahkan");
                window.location.reload();
            },
            error: function (errorText) {
                alert("Data Gagal Ditambahkan");
            }
        });
    }
}

function UpdatePemakaian() {
    var a = $("#txtkwhpjt2").val();
    var b = $("#txtkvarhpjt2").val();
    var c = $("#txtkwhpln2").val();
    var d = $("#txtkvarhpln2").val();
    var e = $("#txtline").val();
    var f = $("#txtketerangan").val();
    var g = $("#txttgl").val();

    if (g == "") {
        alert("Tanggal tidak boleh kosong !");
    }
    else
    {

    
    obj = {};
    obj.Tanggal = $("#txttgl").val();

    if (e == "") {
        obj.Line = '0';
    } else {
        obj.Line = $("#txtline").val();
    }
    if (a == "") {
        obj.kWhPJT = '0';
    } else {
        obj.kWhPJT = $("#txtkwhpjt2").val();
    }
    if (b == "") {
        obj.kVarhPJT = '0';
    } else {
        obj.kVarhPJT = $("#txtkvarhpjt2").val();
    }
    if (c == "") {
        obj.kWhPLN = '0';
    } else {
        obj.kWhPLN = $("#txtkwhpln2").val();
    }
    if (d == "") {
        obj.kVarhPLN = '0';
    } else {
        obj.kVarhPLN = $("#txtkvarhpln2").val();
    } if (f == "") {
        obj.Keterangan = 'a';
    } else {
        obj.Keterangan = $("#txtketerangan").val();
    }
    
    $.ajax({
        url: 'InputPemakaianListrik.aspx/UpdatePemakaian',
        type: 'POST',
        data: JSON.stringify({ obj: obj }),
        contentType: "application/json; charset=utf-8",
        dataType: 'text',
        success: function (data) {
            console.log(data);
            alert("Data Berhasil Diubah");
            window.location.reload();
        },
        error: function (errorText) {
            alert("Data Gagal Diuabh");
        }
    });
    }
}

function fnExcelReport() {
    var tab_text = "<table border='2px'><tr>";
    var textRange; var j = 0;
    tab = document.getElementById('tablepemakaian'); // id of table

    for (j = 0 ; j < tab.rows.length ; j++) {
        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
        //tab_text=tab_text+"</tr>";
    }

    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}

function printData() {
    var divToPrint = document.getElementById("tablepemakaian");
    newWin = window.open("");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}