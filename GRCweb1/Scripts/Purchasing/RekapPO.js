var isPertama = true;
$("#tglawal").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#tglakhir").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    RequestRekapPO($("#tglawal").val().split('-')[2] + $("#tglawal").val().split('-')[1] + $("#tglawal").val().split('-')[0],
        $("#tglakhir").val().split('-')[2] + $("#tglakhir").val().split('-')[1] + $("#tglakhir").val().split('-')[0]);
});

$("#preview").click(function () {
    RequestRekapPO($("#tglawal").val().split('-')[2] + $("#tglawal").val().split('-')[1] + $("#tglawal").val().split('-')[0],
        $("#tglakhir").val().split('-')[2] + $("#tglakhir").val().split('-')[1] + $("#tglakhir").val().split('-')[0]);
});

function OpenModal() {
    $("#MyPopup").modal("show");
}

$("#cetak").click(function () {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "RekapPO.aspx/Open",
        dataType: "json",
        success: function (data) {
            $("#MyPopup").modal("show");
        },
        error: function (jqXHR, exception) {
        }
    });
});


function RequestRekapPO(tglawal,tglakhir) {
    $.ajax({
        url: "RekapPO.aspx/GetListRekapPO",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ awal: tglawal, akhir: tglakhir  }),
        success: function (data) {
            if (!isPertama) {
                $("#tablerekappo").DataTable().destroy();
                $('#tablerekappo').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tablerekappo");
            oTblReport.DataTable({
                "data": datatable,
                "scrollX": true,
                "scrollCollapse": true,
                "columnDefs": [
                    { "width": "200", "targets": 7 }
                ],
                "order": [1, "asc"],
                "dom": 'lBfrtip',
                "buttons": [
                    {
                        extend: 'excelHtml5',
                        title: 'Rekap PO',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        title: 'Rekap PO',
                        orientation: 'landscape',
                        pageSize: 'A3',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]
                        }
                    },
                    {
                        extend: 'print',
                        customize: function (win) {
                            $(win.document.body)
                                .css('font-size', '10pt');

                            $(win.document.body).find('table')
                                .addClass('compact')
                                .css('font-size', 'inherit');
                        },
                        orientation: 'landscape',
                        pageSize: 'A3',
                        exportOptions: {
                            columns: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16]
                        }
                    }
                ],
                "columns": [
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "";
                            if (row.Apv == 2 && row.Cetak == 0) {
                                aksi = "<button class = 'btn btn-info btn-sm' type='button' style='margin-right:5px;' onclick='DetailPO(\"" + row.ID + "\")'><i class='fa fa-check'></i> CETAK </button>";
                            }
                            return aksi;
                        },
                        "defaultContent": ""
                    },
                    { "data": "NoPO", title: "No PO" },
                    { "data": "SupplierName", title: "Supplier Name" },
                    { "data": "Approval", title: "Approval" },
                    { "data": "PPN", title: "PPN" },
                    { "data": "PPH", title: "PPH" },
                    { "data": "M_Uang", title: "Mata Uang" },
                    { "data": "ItemName", title: "Item Name" },
                    { "data": "NoSPP", title: "No SPP" },
                    { "data": "Qty", title: "Qty" },
                    { "data": "Satuan", title: "Satuan" },
                    { "data": "Disc", title: "Disc" },
                    { "data": "Price", title: "Price" },
                    { "data": "Total", title: "Total" },
                    { "data": "Tot2", title: "Tot2" },
                    { "data": "PoDate", title: "PO Purchn Date" },
                    { "data": "groupdesc", title: "Group Desc" },
                    { "data": "Cetak", title: "Cetak" }
                ]
            });
        }
    });
}
function fncsave() {
    $('#<%=savebtn.ClientID%>').click();
    document.getElementById('<%= savebtn.ClientID %>').click();
}

function addPeriod(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    return x1 + x2;
}
function MyPopUpWin(url, width, height) {
    var leftPosition, topPosition;
    leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
    topPosition = (window.screen.height / 2) - ((height / 2) + 50);
    window.open(url, "Window2",
    "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
    + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
    + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
}
function DetailPO(ID) {

    //var origin = window.location.origin;
    //var url = origin + "/Modul/Report/Report.aspx?IdReport=POPurchn&ID=" + ID;
    //window.Open(url, '_blank');
    MyPopUpWin("../Report/Report.aspx?IdReport=POPurchn&ID=" + ID, 900, 800)
}
//function DetailPO(nopo) {
//    //$("#idrekap").val(id);
//    //$('#MainContent_savebtn').click();

//    $.ajax({
//        url: "RekapPO.aspx/GetDetailPO",
//        type: "POST",
//        contentType: "application/json",
//        data: JSON.stringify({ nopo: nopo }),
//        success: function (data) {
//            jumlah = [];
//            var textHTMLViewAll = '';
//            textHTMLViewAll += '<div class="invoice-box">';
//            textHTMLViewAll += '<h5><b>PT BANGUNPERKASA ADHITAMASENTRA<br /></b></h5>'
//            textHTMLViewAll += '<table>'
//            textHTMLViewAll += '<tr>'
            
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:10px>GRAHA GRC BOARD Lt.3</p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Jl. S. PARMAN kav.64 Slipi Palmerah </p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Jakarta 11410 - Indonesia </p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Telp. (62-21) 53666800 </p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Fax. (62-21) 53666720 </p>'
//            textHTMLViewAll += '</td>'

//            textHTMLViewAll += '<td><h5><b>PURCHASING ORDER</b></h5>'
//            textHTMLViewAll += '</td>'

//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px> NO. <b>' + nopo +'</b></p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'

//            textHTMLViewAll += '</br>'

//            textHTMLViewAll += '<table style=table-layout:fixed;border:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:10px>VENDOR : ' + data.d[0].SupplierName +'</p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px> &nbsp &nbsp &nbsp &nbsp &nbsp UP : ' + data.d[0].UP+'</p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px> &nbsp &nbsp &nbsp &nbsp &nbsp Telp. ' + data.d[0].Telepon+'</p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px> &nbsp &nbsp &nbsp &nbsp &nbsp Fax. ' + data.d[0].Fax+'</p>'
//            textHTMLViewAll += '</td>'


//            if (data.d[0].DepoID == 1) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:10px><b> SHIP TO/KIRIM KE : </b></p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>PT BANGUNPERKASA ADHITAMASENTRA-CITEUREUP </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Komp. Industri Brata Mulia </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Telp : 8756773 </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Fax : 8756774 </p>'
//                textHTMLViewAll += '</td>'
//            } else if (data.d[0].DepoID == 7) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:10px><b> SHIP TO/KIRIM KE : </b></p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>PT BANGUNPERKASA ADHITAMASENTRA-KARAWANG </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Kp. Krajan Dua Klari - Karawang 41371 </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Telp : (62-267)8615519 </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Fax : (62-267)8615523 </p>'
//                textHTMLViewAll += '</td>'
//            } else if (data.d[0].DepoID == 13) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:10px><b> SHIP TO/KIRIM KE : </b></p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>PT BANGUNPERKASA ADHITAMASENTRA-JOMBANG </p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Jalan Raya Ploso - Babat</p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Desa Pengampon - Kec. Kabuh</p>'
//                textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:10px>Kab. Jombang - 61455</p>'
//                textHTMLViewAll += '</td>'
//            }

//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'

//            textHTMLViewAll += '</br>'

//            textHTMLViewAll += '<table style=border:solid;border-collapse:collapse;>';
//            textHTMLViewAll += '<tr class="heading">';
//            textHTMLViewAll += '<td>No</td>';
//            textHTMLViewAll += '<td>Item Code</td>';
//            textHTMLViewAll += '<td>Item Name</td>';
//            textHTMLViewAll += '<td>Delivery Date</td>';
//            textHTMLViewAll += '<td>Qty</td>';
//            textHTMLViewAll += '<td>Unit Price</td>';
//            textHTMLViewAll += '<td>Amount</td>';
//            textHTMLViewAll += '</tr>';


//            for (var i = 0; i < data.d.length; i++) {
//                var money = addPeriod(data.d[i].Price);
//                var total = addPeriod((data.d[i].Qty * data.d[i].Price));
//                textHTMLViewAll += '<tr class="details">';
//                textHTMLViewAll += '<td style=line-height:5px;>'+(i+1) +'</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].ItemCode +'</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].ItemName +'</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].DeliveryDate +'</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].Qty +'</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].Lambang +' '+ money + '</td>';
//                textHTMLViewAll += '<td style=line-height:5px;>' + data.d[i].Lambang + ' ' + total + '</td>';
//                textHTMLViewAll += '</tr>';
//                jumlah.push(data.d[i].Qty * data.d[i].Price);
//            }
//            var biaya = addPeriod(jumlah.reduce((a, b) => a + b, 0));
//            var ppn;
//            var pph;
//            if (data.d[0].PPN > 0) {
//                ppn = data.d[0].PPN / 100 * jumlah.reduce((a, b) => a + b, 0);
//            } else {
//                ppn = 0;
//            }

//            if (data.d[0].PPH > 0) {
//                pph = data.d[0].PPH / 100 * jumlah.reduce((a, b) => a + b, 0);

//            } else {
//                pph = 0;
//            }
//            var totalbiaya = addPeriod(jumlah.reduce((a, b) => a + b, 0) + ppn + pph);
//            textHTMLViewAll += '<tr class="details">';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;>Total : </td>';
//            textHTMLViewAll += '<td style=line-height:5px;>' + data.d[0].Lambang + ' ' + biaya +'</td>';
//            textHTMLViewAll += '</tr>';

//            textHTMLViewAll += '<tr class="details">';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;>PPN : ' + data.d[0].PPN +' %</td>';
//            textHTMLViewAll += '<td style=line-height:5px;>' + data.d[0].Lambang + ' ' + addPeriod(ppn) + '</td>';
//            textHTMLViewAll += '</tr>';

//            textHTMLViewAll += '<tr class="details">';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;></td>';
//            textHTMLViewAll += '<td style=line-height:5px;>PPH : ' + data.d[0].PPH+' %</td>';
//            textHTMLViewAll += '<td style=line-height:5px;>' + data.d[0].Lambang + ' ' + addPeriod(pph) + '</td>';
//            textHTMLViewAll += '</tr>';
//            textHTMLViewAll += '</table>';

//            textHTMLViewAll += '<table style=table-layout:fixed;text-align:right;border-left:solid;border-right:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b>TOTAL </b></p>'
//            textHTMLViewAll += '</td>'

//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b> ' + data.d[0].Lambang + ' ' + totalbiaya + ' </b></p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'

//            var input = totalbiaya.replace(/\./g, "");
//            var bilang = terbilang(input).replace(/  +/g, ' ');

//            textHTMLViewAll += '<table style=table-layout:fixed;border:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b>Terbilang : ' + bilang + ' </b></p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'
//            textHTMLViewAll += '</br>'

//            textHTMLViewAll += '</table>';
//            textHTMLViewAll += '<table style=table-layout:fixed;text-align:center;border-left:solid;border-right:solid;border-top:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:13px>NPWP NO. 01.601.413.6-052.000</p>'
//            textHTMLViewAll += '</td>'

//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:13px> SPPKP NO. PEM-00107/WPJ.07/KP.0203/2008</p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'

//            textHTMLViewAll += '<table style=table-layout:fixed;text-align:center;border:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:13px>Term Of Payment</p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:13px>' + data.d[0].Termin + '</p>'
//            textHTMLViewAll += '</td>'

//            textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:13px> Term Of Delivery </p>'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:13px>' + data.d[0].Delivery + '</p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'

//            textHTMLViewAll += '<table style=table-layout:fixed;text-align:right;border-left:solid;border-right:solid;border-bottom:solid;border-collapse:collapse;>'
//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '</tr>'

//            textHTMLViewAll += '<tr>'
//            textHTMLViewAll += '<td>'
//            textHTMLViewAll += '</td>'

//            if (data.d[0].DepoID == 1) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b>Citeureup, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></p >'
//            } else if (data.d[0].DepoID == 7) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b>Karawang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></p >'
//            } else if (data.d[0].DepoID == 13) {
//                textHTMLViewAll += '<td><p style=margin:0;padding:0;line-height:15px;font-size:12px><b>Jombang, ' + $.datepicker.formatDate('dd-mm-yy', new Date()); +' </b></p >'
//            }
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:12px;padding-right:25px;>Approved By</p >'
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:12px> <img src="../../img/aying.JPG" /></p > '
//            textHTMLViewAll += '<p style=margin:0;padding:0;line-height:15px;font-size:12px;padding-right:25px;><b>Tuty/Aying</b></p>'
//            textHTMLViewAll += '</td>'
//            textHTMLViewAll += '</tr>'
//            textHTMLViewAll += '</table>'


//            textHTMLViewAll += ''
//            textHTMLViewAll += '</div>';
//            $("#invoice2").html(textHTMLViewAll);


//            const element = document.getElementById('invoice2');
//            var opt = {
//                margin: 1,
//                filename: 'RekapPo.pdf',
//                image: { type: 'jpeg', quality: 0.98 },
//                html2canvas: { scale: 2 },
//                jsPDF: { unit: 'in', format: 'A4', orientation: 'portrait' }
//            };

//            //download file pdf
//            //html2pdf().set(opt).from(element).save();

//            //open file pdf
//            html2pdf().set(opt).from(element).toPdf().get('pdf').then(function (pdf) {
//                window.open(pdf.output('bloburl'), '_blank');
//            });
//        },
//        error: function (jqXHR, exception) {
//        }
//    });



    

