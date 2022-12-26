var T1AdjustAll = {
    T1AdjustDetail: []
}
var T1Adjust = {};
var status, closingstatus;
var isPertamaSm = true;
var isPertama = true;
var apvcekbox = [];
var active_class = 'active';
var thnbln = $("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1];
var thnbln0 = $("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] - 1;
var allPages;

$("#tglproduksi").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());
$("#tgladjust").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    $("#listapv").hide();
    $("#listadjust").show();

    $("#approve").hide();
    RequestListT1Adjust($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);

    $("#partno").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "T1Adjust.aspx/GetPartonoT1",
                data: "{'partno':'" + $("#partno").val() + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (jqXHR, exception) {
                }
            });
        },
        select: function (event, ui) {
            $("#partno").val(ui.item.PartNo);
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
            RequestLokasiStock($("#partno").val(), ui.item.Lokasi, thnbln, thnbln0);
            return false;
        }
    })
        .data("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<a>" + item.Lokasi + "</a>")
                .appendTo(ul);
        };
});


function RequestListT1Adjust(tgl) {
    $.ajax({
        url: "T1Adjust.aspx/ListT1Adjust",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ tgl: tgl, param: 1 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d != '[]') {
                if (!isPertamaSm) {
                    $("#tablelistadjust").DataTable().destroy();
                    $('#tablelistadjust').empty();
                } else {
                    isPertamaSm = false;
                }
                datatable = $.parseJSON(data.d);
                var oTblReport = $("#tablelistadjust").DataTable({
                    "data": datatable,
                    "responsive": true,
                    "columns": [
                        { "data": "DateAdjust", title: "Tanggal Adjust" },
                        { "data": "AdjustNo", title: "Adjust No" },
                        { "data": "NoBA", title: "No BA" },
                        { "data": "AdjustType", title: "Type" },
                        { "data": "Partno", title: "Partno" },
                        { "data": "ProduksiDate", title: "Tgl Produksi" },
                        { "data": "Lokasi", title: "Lokasi" },
                        { "data": "QtyIn", title: "Qty In" },
                        { "data": "QtyOut", title: "Qty Out" }
                    ]
                });
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function RequestListApv() {
    $.ajax({
        url: "T1Adjust.aspx/ListT1Adjust",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ tgl: '', param: 2 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d != null || data.d != 0) {
                if (!isPertama) {
                    $("#tablelistapv").DataTable().destroy();
                    $('#tablelistapv').empty();
                } else {
                    isPertama = false;
                }
                datatable = $.parseJSON(data.d);
                var oTblReport = $("#tablelistapv").DataTable({
                    "order": [0],
                    "columnDefs": [
                        {
                            "targets": 0,
                            "orderable": false
                        }
                    ],
                    "pageLength": 50,
                    "data": datatable,
                    "responsive": true,
                    "columns": [
                        {
                            "render": function (data, type, row, meta) {
                                aksi = "<td class='center'><label class='pos-rel'><input type='checkbox' class='ace' /><span class='lbl'></span></label></td>";
                                return aksi;
                            },
                            "defaultContent": ""
                        },
                        { "data": "DateAdjust", title: "Tgl Adjust" },
                        { "data": "AdjustNo", title: "Adjust No" },
                        { "data": "NoBA", title: "No BA" },
                        { "data": "AdjustType", title: "Type" },
                        { "data": "Partno", title: "Partno" },
                        { "data": "ProduksiDate", title: "Tgl Produksi" },
                        { "data": "Lokasi", title: "Lokasi" },
                        { "data": "QtyIn", title: "Qty In" },
                        { "data": "QtyOut", title: "Qty Out" },
                        { "data": "Approval", title: "Approval" }
                    ]
                });

                $('#tablelistapv tbody').on('click', 'td input[type=checkbox]', function () {
                    var data = oTblReport.row($(this).closest('tr')).data();
                    if (this.checked == true) {
                        T1AdjustDetail = {
                            ID: data.ID,
                            Partno: data.Partno,
                            Lokasi: data.Lokasi,
                            AdjustType: data.AdjustType,
                            AdjustDate: data.AdjustDate,
                            QtyIn: data.QtyIn,
                            QtyOut: data.QtyOut
                        }
                        T1AdjustAll.T1AdjustDetail.push(T1AdjustDetail);
                    } else {
                        T1AdjustAll.T1AdjustDetail.splice($.inArray(data.ID, T1AdjustAll.T1AdjustDetail), 1);
                    }
                });


                $('#tablelistapv > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
                    var th_checked = this.checked;
                    var alldata = oTblReport.rows().data();

                    if (th_checked == true) {
                        for (i = 0; i < alldata.length; ++i) {
                            T1AdjustDetail = {
                                ID: alldata[i].ID,
                                Partno: alldata[i].Partno,
                                Lokasi: alldata[i].Lokasi,
                                AdjustType: alldata[i].AdjustType,
                                AdjustDate: alldata[i].AdjustDate,
                                QtyIn: alldata[i].QtyIn,
                                QtyOut: alldata[i].QtyOut
                            }
                            T1AdjustAll.T1AdjustDetail.push(T1AdjustDetail);
                        }
                    } else {
                        T1Adjust = [];
                        T1AdjustAll.T1AdjustDetail = [];
                    }
                    $(this).closest('table').find('tbody > tr').each(function () {
                        var row = this;
                        if (th_checked) {
                            $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
                        } else {
                            $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
                        }
                    });
                });   
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}

function RequestLokasiStock(partno, lokasi, thnbln, thnbln0) {
    $.ajax({
        url: "T1Adjust.aspx/GetStokLokasiT1",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ partno: partno, lokasi: lokasi, thnbln: thnbln, thnbln0: thnbln0 }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d == null || data.d == 0) {
                $("#qtyprod").val(0);
            } else {
                $("#qtyprod").val(data.d[0].Saldo);
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


$('input[type=radio][name=listtable]').change(function () {
    if (this.value == 1) {
        $("#listapv").hide();
        $("#listadjust").show();
        $("#approve").hide();
        RequestListT1Adjust($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
    } else if (this.value == 2) {
        $("#listadjust").hide();
        $("#listapv").show();
        $("#approve").show();
        RequestListApv();
    }
});

$("#addpartno").click(function () {
    if ($("#partno").val() == "" || $("#lokasi").val() == "" || $("#qtyadjust").val() == "" || $("#noba").val() == "") {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Tidak Lengkap',
            theme: 'modern',
            type: 'red'
        });
    } else {
        var qtyin = 0;
        var qtyout = 0;
        if ($('input[name="adjust"]:checked').val() == 'In') {
            qtyin = $("#qtyadjust").val();
            qtyout = 0;
        } else if ($('input[name="adjust"]:checked').val() == 'Out') {
            qtyin = 0;
            qtyout = $("#qtyadjust").val();
        }

        T1AdjustDetail = {
            Partno: $("#partno").val(),
            Lokasi: $("#lokasi").val(),
            AdjustType: $('input[name="adjust"]:checked').val(),
            DateProduksi: $("#tglproduksi").val(),
            QtyIn: qtyin,
            QtyOut: qtyout
        }
        T1AdjustAll.T1AdjustDetail.push(T1AdjustDetail);

        T1Adjust = {
            TglAdjust: $("#tgladjust").val(),
            NoBA: $("#noba").val(),
            Keterangan: $("#keterangan").val()
        }

        var list = `<tr><td>` + $("#tgladjust").val() + `</td>
                <td>` + $("#noba").val() + `</td>
                <td>` + $('input[name="adjust"]:checked').val() + `</td>
                <td>` + $("#partno").val() + `</td>
                <td>` + $("#tglproduksi").val() + `</td>
                <td>` + $("#lokasi").val() + `</td>
                <td>` + qtyin + `</td>
                <td>` + qtyout + `</td>
                <td><button class="btn btn-danger btnDelete">Delete</button></td></tr>`;
        $('#list-dtl').append(list);


        $("#partno").val('');
        $("#lokasi").val('');
        $("#qtyadjust").val('');
        $("#qtyprod").val('');
    }
});

$("#tablereadd").on('click', '.btnDelete', function () {
    var row_index = $(this).closest("tr").index();
    T1AdjustAll.T1AdjustDetail.splice(row_index, 1);
    $(this).closest('tr').remove(); 
});


$("#simpanpartno").click(function () {
    if (T1Adjust.length == 0 || T1AdjustAll.T1AdjustDetail.length == 0) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Tidak Lengkap',
            theme: 'modern',
            type: 'red'
        });
    } else {
        var tahun = $("#tgladjust").val().split('-')[2];
        var bulan = $("#tgladjust").val().split('-')[1];

        GetMonthClosingStatus(tahun, bulan);
        if (status == 1 && closingstatus == 1) {
            $.alert({
                icon: 'fa fa-times',
                title: 'Warning!',
                content: 'Periode Bulan Sudah Closing. Transaksi Tidak Bisa Dilakukan',
                theme: 'modern',
                type: 'red'
            });
            return false;
        }

        SimpanPartno(T1Adjust, T1AdjustAll);

        $("#noba").val('');
        $("#keterangan").val('');
        $("#partno").val('');
        $("#lokasi").val('');
        $("#qtyadjust").val('');
        $("#qtyprod").val('');

        $('#list-dtl').empty();

        T1Adjust = [];
        T1AdjustAll.T1AdjustDetail = [];
    }
    RequestListApv();
});


$("#approve").click(function () {
    ApproveAdjust(T1AdjustAll);
});

function GetMonthClosingStatus(tahun, bulan) {
    $.ajax({
        url: "Simetris.aspx/GetClosingStatus",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ tahun: tahun, bulan: bulan, modul: "Produksi", modulname: "SystemClosing" }),
        success: function (data) {
            status = data.d[0].status;
            closingstatus = data.d[0].clsStat;
        }
    });
}

function ApproveAdjust(T1AdjustAll) {
    $.ajax({
        url: "T1Adjust.aspx/Approve",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ T1AdjustAll: T1AdjustAll }),
        success: function (data) {
            if (data.d == 1) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Proses Approve Berhasil',
                    theme: 'modern',
                    type: 'green'
                });
                T1Adjust = [];
                T1AdjustAll.T1AdjustDetail = [];
                
            } else if (data.d == 0) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Success',
                    content: 'Proses Approve Gagal',
                    theme: 'modern',
                    type: 'red'
                });
                T1Adjust = [];
                T1AdjustAll.T1AdjustDetail = [];
            }
            RequestListApv();
        }
    });
}

function SimpanPartno(T1Adjust, T1AdjustAll) {
    $.ajax({
        url: "T1Adjust.aspx/SimpanAdjust",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ T1Adjust: T1Adjust, T1AdjustAll: T1AdjustAll }),
        success: function (data) {
            if (data.d == 1) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Proses Adjust Berhasil',
                    theme: 'modern',
                    type: 'green'
                });
                RequestListT1Adjust($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
            } else if (data.d == 0) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Success',
                    content: 'Proses Adjust Gagal',
                    theme: 'modern',
                    type: 'red'
                });
            }
        }
    });
}


$("#tglproduksi").datepicker({
    onSelect: function (dateText) {
        RequestListT1Adjust($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
        $(this).change();
    }
}).on("change", function () {
    RequestListT1Adjust($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
});


$("#tgladjust").datepicker({
    onSelect: function (dateText) {
        RequestListT1Adjust($("#tgladjust").val().split('-')[2] + $("#tgladjust").val().split('-')[1] + $("#tgladjust").val().split('-')[0]);
        $(this).change();
    }
}).on("change", function () {
    RequestListT1Adjust($("#tgladjust").val().split('-')[2] + $("#tgladjust").val().split('-')[1] + $("#tgladjust").val().split('-')[0]);
});


$('#tablelistapv > thead > tr > th input[type=checkbox]').eq(0).on('click', function () {
    var th_checked = this.checked;

    $(this).closest('table').find('tbody > tr').each(function () {
        var row = this;
        if (th_checked) $(row).addClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', true);
        else $(row).removeClass(active_class).find('input[type=checkbox]').eq(0).prop('checked', false);
    });
});

$('#selectAll').click(function () {
    if ($(this).hasClass('allChecked')) {
        $(allPages).find('input[type="checkbox"]').prop('checked', false);
    } else {
        $(allPages).find('input[type="checkbox"]').prop('checked', true);
    }
    $(this).toggleClass('allChecked');
})

$('#tablelistapv').on('click', 'td input[type=checkbox]', function () {
    var $row = $(this).closest('tr');
    if (this.checked) {
        $row.addClass(active_class);
    }
    else {
        $row.removeClass(active_class);
    }
});