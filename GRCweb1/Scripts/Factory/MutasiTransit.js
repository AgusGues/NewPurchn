var isPertama = true;
var active_class = 'active';
var total = 0;
var transit = 0;
var totaltransit = {
    transit: []
};
var PaletTransit = {};

$("#tglmutasi").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#tglserah").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    $("#tglserah").hide();
    RequestListTransit();
});

$("#cektglserah").change(function () {
    if (this.checked) {
        $("#tglserah").hide();
    } else {
        $("#tglserah").show();
        
    }
});


$("#partnotujuan").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnotujuan").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnotujuan").val(ui.item.PartNo.trim());
        $("#tebal").val(ui.item.Tebal);
        $("#panjang").val(ui.item.Panjang);
        $("#lebar").val(ui.item.Lebar);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };



$("#lokasi").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "T1Adjust.aspx/GetLokasiT1",
            data: "{'lokasi':'" + $("#lokasi").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#lokasi").val(ui.item.Lokasi.trim());
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

$("#lokasitransit").on('change', function () {
    total = 0;
    transit = 0;
    totaltransit.transit = [];
    PaletTransit = [];
    RequestPartnoTransit($(this).val(), $("#tglserah").val(),0,0,0,0);
});


$("#tglserah").datepicker({
    onSelect: function (dateText) {
        total = 0;
        totaltransit.transit = [];
        PaletTransit = [];
        RequestPartnoTransit("", $("#tglserah").val(), 1, 0, 0, 0);
        $("#qtyall").val(total);
        $(this).change();
    }
}).on("change", function () {
    total = 0;
    totaltransit.transit = [];
    PaletTransit = [];
    RequestPartnoTransit("", $("#tglserah").val(), 1, 0, 0, 0);
    $("#qtyall").val(total);
});


function RequestListTransit() {
    $.ajax({
        url: "MutasiLokasiTransit.aspx/ListLokasiTransit",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#lokasitransit").append('<option value="' + value.Lokasi.trim() + '" >' + value.Lokasi.trim() + '</option>');
            });
        }
    });
}


function RequestPartnoTransit(lokasi, tglserah, range, tebal, panjang, lebar) {
    $.ajax({
        url: "MutasiLokasiTransit.aspx/GetPartnoTransit",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ lokasi: lokasi, tglserah: tglserah, range: range, tebal: tebal, panjang: panjang, lebar: lebar }),
        success: function (data) {
            if (!isPertama) {
                $("#tablelisttransit").DataTable().destroy();
                $('#tablelisttransit').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            for (var i = 0; i < datatable.length; ++i) {
                transit = {
                    ID: datatable[i].ID,
                    Produksi: datatable[i].Produksi,
                    PartnoDest: datatable[i].PartnoDest,
                    TglSerah: datatable[i].TglSerah,
                    PartnoSer: datatable[i].PartnoSer,
                    LokasiSer: datatable[i].LokasiSer,
                    QtyIn: datatable[i].QtyIn
                }
                totaltransit.transit.push(transit);

                transit = total;
                total = transit + datatable[i].QtyIn;
                $("#qtyall").val(total);
            }
            var oTblReport = $("#tablelisttransit").DataTable({
                "order": [9, 10],
                "columnDefs": [
                    {
                        "width": "10%",
                        "targets": [9, 10],
                        "orderable": false
                    }
                ],
                "pageLength": 50,
                "data": datatable,
                "columns": [
                    { "data": "Produksi", title: "Tanggal Produksi" },
                    { "data": "PartnoDest", title: "Partno Tahap 1" },
                    { "data": "TglSerah", title: "Tanggal Serah" },
                    { "data": "Tebal", title: "Tebal" },
                    { "data": "Panjang", title: "Panjang" },
                    { "data": "Lebar", title: "Lebar" },
                    { "data": "PartnoSer", title: "Partno Tahap 3" },
                    { "data": "LokasiSer", title: "Lokasi" },
                    { "data": "QtyIn", title: "Stok" },
                    {
                        "render": function (data, type, row, meta) {
                            aksi = "<td class='center'><label class='pos-rel'><input type='text' id='qty' class='txtBox' value=" + row.QtyIn + " style='width:40%' /></label></td>";
                            return aksi;
                        },
                        "defaultContent": ""
                    },
                    {
                        "render": function (data, type, row, meta) {
                            aksi = "<td class='center'><label class='pos-rel'><input type='checkbox' class='ace' checked/><span class='lbl'></span>Mutasi</label></td>";
                            return aksi;
                        },
                        "defaultContent": ""
                    },
                ]
            });

            $('#tablelisttransit tbody').on('keyup change', 'td input[type=text]', function () {
                var data = oTblReport.row($(this).closest('tr')).data();
                var qty;
                for (var i = 0; i < totaltransit.transit.length; ++i) {
                    if (totaltransit.transit[i].ID == data.ID) {
                        if ($(this).val() > data.QtyIn) {
                            $(this).val(data.QtyIn);
                            totaltransit.transit[i].QtyIn = data.QtyIn;
                            break;
                        } else {
                            if ($(this).val() == '') {
                                qty = 0;
                            } else if ($(this).val() != '') {
                                qty = parseInt($(this).val());
                            }
                            totaltransit.transit[i].QtyIn = qty;
                            break;
                        }
                    }
                }

                transit = 0;
                total = 0;
                for (var i = 0; i < totaltransit.transit.length; ++i) {
                    transit = total;
                    total = transit + totaltransit.transit[i].QtyIn;
                    $("#qtyall").val(total);
                }
            });

            $('#tablelisttransit tbody').on('click', 'td input[type=checkbox]', function () {
                var data = oTblReport.row($(this).closest('tr')).data();
                if (this.checked == true) {
                    transit = {
                        ID: data.ID,
                        Produksi: data.Produksi,
                        PartnoDest: data.PartnoDest,
                        TglSerah: data.TglSerah,
                        PartnoSer: data.PartnoSer,
                        LokasiSer: data.LokasiSer,
                        QtyIn: data.QtyIn
                    }
                    totaltransit.transit.push(transit);
                    $("#qtyall").val(parseInt($("#qtyall").val()) + data.QtyIn);
                } else {
                    for (var i = 0; i < totaltransit.transit.length; ++i) {
                        if (totaltransit.transit[i].ID == data.ID) {
                            totaltransit.transit.splice(i, 1);
                        }
                    }
                    $("#qtyall").val(parseInt($("#qtyall").val()) - data.QtyIn);
                }
            });

        }
    });
}



$("#transferpartno").click(function () {
    if ($("#partnotujuan").val() == "" || $("#lokasi").val() == "" || $("#qtyall").val() == 0 ) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Tidak Lengkap',
            theme: 'modern',
            type: 'red'
        });
    } else {
        PaletTransit = {
            PartnoTujuan: $("#partnotujuan").val(),
            LokasiTujuan: $("#lokasi").val(),
            QtyTujuan: $("#qtyall").val(),
            TglPotong: $("#tglmutasi").val()
        }
        SimpanTransit(totaltransit, PaletTransit);
    }
});


function SimpanTransit(totaltransit, PaletTransit) {
    $.ajax({
        url: "MutasiLokasiTransit.aspx/SimpanTransit",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ totaltransit: totaltransit, PaletTransit: PaletTransit }),
        success: function (data) {
            if (data.d == 1) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Proses Simpan Berhasil',
                    theme: 'modern',
                    type: 'green'
                });
                totaltransit.transit = [];
                PaletTransit = [];
            } else if (data.d == 0) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Success',
                    content: 'Proses Simpan Gagal',
                    theme: 'modern',
                    type: 'red'
                });
                totaltransit.transit = [];
                PaletTransit = [];
            }

        }
    });
}