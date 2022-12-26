var isPertama = true;
var active_class = 'active';
var total = 0;
var pelarian = 0;
var totalpelarian = {
    lari: []
};
var PaletPelarian = {};

$(document).ready(function () { 
    $("#loading").hide($.unblockUI());
    $("#listukuran").hide();
    $("#listtgl").hide();
    $("#listpartnopelarian").hide();
    RequestListUkuran();
    RequestListPelarian();
});


$("#ukuran").change(function () {
    if (this.checked) {
        total = 0;
        $("#listukuran").show();
        RequestPartnoPelarian("", "", "");
    } else {
        totalpelarian.lari = [];
        PaletPelarian = [];
        $("#listukuran").hide();
        $('#tablelistpelarian').DataTable().clear().draw();
        $("#jumlahpotong").val("");
    }
});

$("#tglpelarian").change(function () {
    if (this.checked) {
        total = 0;
        $("#listtgl").show();
        RequestPartnoPelarian("", "", "");
    } else {
        totalpelarian.lari = [];
        PaletPelarian = [];
        $("#listtgl").hide();
        $('#tablelistpelarian').DataTable().clear().draw();
        $("#jumlahpotong").val("");
    }
});

$("#partnopelarian").change(function () {
    if (this.checked) {
        total = 0;
        $("#listpartnopelarian").show();
        RequestPartnoPelarian("","","");
    } else {
        totalpelarian.lari = [];
        PaletPelarian = [];
        $("#listpartnopelarian").hide();
        $('#tablelistpelarian').DataTable().clear().draw();
        $("#jumlahpotong").val("");
    }
});


$("#listpartnopelarian").on('change', function () {
    total = 0;
    totalpelarian.lari = [];
    PaletPelarian = [];
    RequestPartnoPelarian($(this).val(), "", "");
    $("#jumlahpotong").val(total);
});


$("#listukuran").on('change', function () {
    total = 0;
    totalpelarian.lari = [];
    PaletPelarian = [];
    RequestPartnoPelarian("", "", $(this).val().replace(" X ", ""));
    $("#jumlahpotong").val(total);
});

$("#listtgl").datepicker({
    onSelect: function (dateText) {
        total = 0;
        totalpelarian.lari = [];
        PaletPelarian = [];
        RequestPartnoPelarian("", $("#listtgl").val(), "");
        $("#jumlahpotong").val(total);
        $(this).change();
    }
}).on("change", function () {
    total = 0;
    totalpelarian.lari = [];
    PaletPelarian = [];
    RequestPartnoPelarian("", $("#listtgl").val(), "");
    $("#jumlahpotong").val(total);
});


$("#tglpotong").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$("#partnook").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnook").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnook").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };



$("#lokasiok").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "T1Adjust.aspx/GetLokasiT1",
            data: "{'lokasi':'" + $("#lokasiok").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#lokasiok").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

function RequestListPelarian() {
    $.ajax({
        url: "MutasiLokasiPelarian.aspx/ListPartnoPelarian",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#listpartnopelarian").append('<option value="' + value.PartNo.trim() + '" >' + value.PartNo.trim() + '</option>');
            });
        }
    });
}

function RequestListUkuran() {
    $.ajax({
        url: "MutasiLokasiPelarian.aspx/ListPartnoPelarianUkuran",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#listukuran").append('<option value="' + value.Ukuran + '" >' + value.Ukuran.trim() + '</option>');
            });
        }
    });
}

$('#qtyok').on('input', function () {
    if ($("#qtyok").val() > $("#jumlahpotong").val()) {
        $("#qtyok").val($("#jumlahpotong").val())
    }
});




function RequestPartnoPelarian(partno, tgl, ukuran) {
    $.ajax({
        url: "MutasiLokasiPelarian.aspx/GetPartnoPelarian",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ partno: partno, tgl: tgl, ukuran:ukuran }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d != '[]') {
                if (!isPertama) {
                    $("#tablelistpelarian").DataTable().destroy();
                    $('#tablelistpelarian').empty();
                } else {
                    isPertama = false;
                }
                datatable = $.parseJSON(data.d);
                for (var i = 0; i < datatable.length; ++i) {
                    lari = {
                        ID: datatable[i].ID,
                        Produksi: datatable[i].Produksi,
                        PartnoDest: datatable[i].PartnoDest,
                        TglSerah: datatable[i].TglSerah,
                        PartnoSer: datatable[i].PartnoSer,
                        LokasiSer: datatable[i].LokasiSer,
                        QtyIn: datatable[i].QtyIn
                    }
                    totalpelarian.lari.push(lari);

                    pelarian = total;
                    total = pelarian + datatable[i].QtyIn;
                    $("#jumlahpotong").val(total);
                }
                var oTblReport = $("#tablelistpelarian").DataTable({
                    "order": [6, 7],
                    "columnDefs": [
                        {
                            "width": "10%",
                            "targets": [6, 7],
                            "orderable": false
                        }
                    ],
                    "pageLength": 50,
                    "data": datatable,
                    "columns": [
                        { "data": "Produksi", title: "Tgl Produksi" },
                        { "data": "PartnoDest", title: "Partno Asal" },
                        { "data": "TglSerah", title: "Tgl Serah" },
                        { "data": "PartnoSer", title: "Partno Pelarian" },
                        { "data": "LokasiSer", title: "Lokasi Pelarian" },
                        { "data": "QtyIn", title: "Stock" },
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

                $('#tablelistpelarian tbody').on('keyup change', 'td input[type=text]', function () {
                    var data = oTblReport.row($(this).closest('tr')).data();
                    var qty;
                    for (var i = 0; i < totalpelarian.lari.length; ++i) {
                        if (totalpelarian.lari[i].ID == data.ID) {
                            if ($(this).val() > data.QtyIn) {
                                $(this).val(data.QtyIn);
                                totalpelarian.lari[i].QtyIn = data.QtyIn;
                                break;
                            } else {
                                if ($(this).val() == '') {
                                    qty = 0;
                                } else if ($(this).val() != '') {
                                    qty = parseInt($(this).val());
                                }
                                totalpelarian.lari[i].QtyIn = qty;
                                break;
                            }
                        }
                    }

                    pelarian = 0;
                    total = 0;
                    for (var i = 0; i < totalpelarian.lari.length; ++i) {
                        pelarian = total;
                        total = pelarian + totalpelarian.lari[i].QtyIn;
                        $("#jumlahpotong").val(total);
                    }
                });

                $('#tablelistpelarian tbody').on('click', 'td input[type=checkbox]', function () {
                    var data = oTblReport.row($(this).closest('tr')).data();
                    if (this.checked == true) {
                        lari = {
                            ID: data.ID,
                            Produksi: data.Produksi,
                            PartnoDest: data.PartnoDest,
                            TglSerah: data.TglSerah,
                            PartnoSer: data.PartnoSer,
                            LokasiSer: data.LokasiSer,
                            QtyIn: data.QtyIn
                        }
                        totalpelarian.lari.push(lari);
                        $("#jumlahpotong").val(parseInt($("#jumlahpotong").val()) + data.QtyIn);
                    } else {
                        for (var i = 0; i < totalpelarian.lari.length; ++i) {
                            if (totalpelarian.lari[i].ID == data.ID) {
                                totalpelarian.lari.splice(i, 1);
                            }
                        }
                        $("#jumlahpotong").val(parseInt($("#jumlahpotong").val()) - data.QtyIn);
                    }
                });
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


$("#transferpartno").click(function () {
    if ($("#partnook").val() == "" || $("#lokasiok").val() == "" || $("#jumlahpotong").val() == "" || $("#jumlahpotong").val() == 0 || $("#qtyok").val() == 0 || $("#qtyok").val() == "") {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Tidak Lengkap',
            theme: 'modern',
            type: 'red'
        });
    } else {
        PaletPelarian = {
            PartnoAsal: $('#partnopelarian').find(":selected").text(),
            PartnoTujuan: $("#partnook").val(),
            LokasiTujuan: $("#lokasiok").val(),
            QtyTujuan: $("#qtyok").val(),
            TglPotong: $("#tglpotong").val()
        }
        SimpanPelarian(totalpelarian, PaletPelarian);
    }

});


function SimpanPelarian(totalpelarian, PaletPelarian) {
    $.ajax({
        url: "MutasiLokasiPelarian.aspx/SimpanPelarian",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ totalpelarian: totalpelarian, PaletPelarian: PaletPelarian }),
        success: function (data) {
            if (data.d == 1) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Proses Simpan Berhasil',
                    theme: 'modern',
                    type: 'green'
                });
                totalpelarian.lari = [];
                PaletPelarian = [];
            } else if (data.d == 0) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Success',
                    content: 'Proses Simpan Gagal',
                    theme: 'modern',
                    type: 'red'
                });
                totalpelarian.lari = [];
                PaletPelarian = [];
            }

        }
    });
}

