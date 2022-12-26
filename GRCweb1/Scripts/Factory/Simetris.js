var isPertama = true;
var isPertamaSm = true;
var kali;
var partnoawal, tebalawal, panjangawal, lebarawal, stokawal, lokasiawal, idawal, itemidawal, lokidawal, groupidawal;
var partnoakhir, tebalakhir, panjangakhir, lebarakhir, stokakhir, lokasiakhir, idakhir, itemidakhir, lokidakhir, groupidakhir;
var deptid, username;
var luas1, luas2;
var id, lock;
var status, closingstatus;
var lokasi, partnobs;
var idpartnobsauto, groupidbsauto;
var idlokasif, idlokasibuangan;
var partnoBS1;
var partnoBS2;
var partnoBS3;
var partnoBS4;
var textHTML = '';
var textHTMLMC = '';
$("#tglproduksi").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", new Date());

//Turn Off Postback
$("#quantity, #partnoakhir, #caripartno, #lokasiakhir").keypress(function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
    }
});

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestDeptID();
    RequestListGroupMarketing();
    RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
    $("#caripartno").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Simetris.aspx/GetNoProdukStok",
                data: "{'partno':'" + $("#caripartno").val() + "','groupid':'" + $('#groupmarketing').val() + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (jqXHR, exception) {
                }
            });
        },
        select: function (event, ui) {
            $("#caripartno").val(ui.item.PartNo);
            RequestPartNoStock(ui.item.PartNo);
            return false;
        }
    })
        .data("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<a>" + item.PartNo + "</a>")
                .appendTo(ul);
        };

    $("#partnoakhir").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Simetris.aspx/GetNoProdukJadi",
                data: "{'partno':'" + $("#partnoakhir").val() + "','groupid':'" + $('#groupmarketing').val() + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (jqXHR, exception) {
                }
            });
        },
        select: function (event, ui) {
            partnoakhir = ui.item.PartNo;
            tebalakhir = ui.item.Tebal;
            panjangakhir = ui.item.Panjang;
            lebarakhir = ui.item.Lebar;
            itemidakhir = ui.item.ID;
            $("#partnoakhir").val(ui.item.PartNo);
            $("#tebalakhir").val(ui.item.Tebal);
            $("#panjangakhir").val(ui.item.Panjang);
            $("#lebarakhir").val(ui.item.Lebar);
            $("#partnameakhir").val(ui.item.PartName);
            CekPartNoAkhir(ui.item.PartNo, ui.item.Panjang, ui.item.Lebar, ui.item.Tebal);
            AutoBS();
            return false;
        }
    })
        .data("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<a>" + item.PartNo + "</a>")
                .appendTo(ul);
        };

    $("#lokasiakhir").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Simetris.aspx/GetListLokasi",
                data: "{'lokasi':'" + $("#lokasiakhir").val() + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (jqXHR, exception) {
                }
            });
        },
        select: function (event, ui) {
            $("#lokasiakhir").val(ui.item.Lokasi);
            lokidakhir = ui.item.ID;
            CekStokLokasiAkhir(ui.item.Lokasi, $("#partnoakhir").val());
            return false;
        }
    })
        .data("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<a>" + item.Lokasi + "</a>")
                .appendTo(ul);
        };
});

function RequestPartNoStock(partno) {
    $.ajax({
        url: "Simetris.aspx/GetListPartNo",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ partno: partno }),
        success: function (data) {
            if (!isPertama) {
                $("#tableListProdukStok").DataTable().destroy();
                $('#tableListProdukStok').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListProdukStok");
            oTblReport.DataTable({
                "data": datatable,
                "columns": [
                    { "data": "PartNo", title: "Part No" },
                    { "data": "Lokasi", title: "Lokasi" },
                    { "data": "Qty", title: "Stok" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-primary btn-sm' type='button' style='margin-right:5px;' onclick='FillDataLokasiAwal(\"" + row.PartNo + "\"," + row.Tebal + ", " + row.Panjang + "," + row.Lebar + ",\"" + row.Lokasi + "\", " + row.Qty + ",\"" + row.PartName + "\"," + row.GroupID + "," + row.ItemID + "," + row.ID + "," + row.LokID + ")'><i class='fa fa-check'></i> Pilih </button>";
                            return aksi;
                        }
                    }
                ]
            });
        }
    });
}

function FillDataLokasiAwal(partno, tebal, panjang, lebar, lokasi, stok, partname, groupid, itemid, id, lokid) {
    partnoawal = partno;
    tebalawal = tebal;
    panjangawal = panjang;
    lebarawal = lebar;
    lokasiawal = lokasi;
    stokawal = stok;
    itemidawal = itemid;
    idawal = id;
    lokidawal = lokid;
    groupidawal = groupid;
    luasawal = lebarawal * panjangawal * tebalawal;
    $("#partno").val(partno);
    $("#tebal").val(tebal);
    $("#panjang").val(panjang);
    $("#lebar").val(lebar);
    $("#lokasi").val(lokasi);
    $("#partname").val(partname);
    $("#stok").val(stok);
    $('#groupmarketing').val(groupid);
}

function RequestListGroupMarketing() {
    $.ajax({
        url: "Simetris.aspx/GetListGroupMarketing",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#groupmarketing").append('<option value="' + value.ID + '" >' + value.Groups + '</option>');
            });
        }
    });
}

function CekPartNoAkhir(partno, panjang, lebar, tebal) {
    luasakhir = lebar * panjang * tebal;
    var partnoawal = $("#partno").val();

    if (partno.indexOf('-P-') !== -1) {
        RequestListDefect();
    }
    if (partno.indexOf('-P-') !== -1 || partno.indexOf('-S-') !== -1 && partnoawal.indexOf('-3-') !== -1 || partnoawal.indexOf('-W-') !== -1 || partnoawal.indexOf('-M-') !== -1) {
        RequestPanelNC();
    }
    if (partno.indexOf('-S-') !== -1) {
        $(".showhiddenbs").css("visibility", "visible");
    }
    if (parseInt($("#panjang").val()) < panjang) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Panjang Partno Tujuan Tidak Mencukupi',
            theme: 'modern',
            type: 'red'
        });
        $("#partnoakhir").val(null);
        $("#tebalakhir").val(null);
        $("#panjangakhir").val(null);
        $("#lebarakhir").val(null);
        $("#partnameakhir").val(null);
        $("#quantityakhir").val(null);
        return false;
    } else if (parseInt($("#tebal").val()) !== tebal) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Ketebalan Tidak Sama',
            theme: 'modern',
            type: 'red'
        });
        $("#partnoakhir").val(null);
        $("#tebalakhir").val(null);
        $("#panjangakhir").val(null);
        $("#lebarakhir").val(null);
        $("#partnameakhir").val(null);
        $("#quantityakhir").val(null);
        return false;
    }else if ($("#quantity").val() == '') {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Stok Awal Belum Diisi',
            theme: 'modern',
            type: 'red'
        });
        $("#partnoakhir").val(null);
        $("#tebalakhir").val(null);
        $("#panjangakhir").val(null);
        $("#lebarakhir").val(null);
        $("#partnameakhir").val(null);
        $("#quantityakhir").val(null);
        return false;

    }
    else if (partno == partnoawal) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Partno Awal Tidak Boleh Sama Dengan Partno Tujuan, Gunakan Menu Mutasi Untuk Melakukan Proses Ini',
            theme: 'modern',
            type: 'red'
        });
        $("#partnoakhir").val(null);
        $("#tebalakhir").val(null);
        $("#panjangakhir").val(null);
        $("#lebarakhir").val(null);
        $("#partnameakhir").val(null);
        $("#quantityakhir").val(null);
        return false;
    } else {
        var qtyakhir = (Math.round(lebarawal / lebarakhir) * Math.round(panjangawal / panjangakhir)) * $("#quantity").val();
        $("#quantityakhir").val(qtyakhir);
    }
}

function RequestListDefect() {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/GetListDefect",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            textHTML += '<select class=" form-control" id="defect">';
            textHTML += '<option value="">Pilih Jenis Defect</option>';
            textHTML += '</select>';
            $("#selectdefect").html(textHTML);

            $.each(data.d, function (data, value) {
                $("#defect").append('<option value="' + value.JenisDefect + '" >' + value.JenisDefect + '</option>');
            });
        },
        error: function (jqXHR, exception) {
        }
    });
}


function RequestListMesinCutter() {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/GetListMesinCutter",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            textHTMLMC += '<select class=" form-control" id="cutter">';
            textHTMLMC += '<option value="">Pilih Mesin Cutter</option>';
            textHTMLMC += '</select>';
            $("#selectmesincutter").html(textHTMLMC);

            $.each(data.d, function (data, value) {
                $("#cutter").append('<option value="' + value.NamaMesin + '" >' + value.NamaMesin + '</option>');
            });
        },
        error: function (jqXHR, exception) {
        }
    });
}

function CekStokLokasiAkhir(lokasi, partno) {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/GetStokLokasiAkhir",
        contentType: "application/json",
        data: JSON.stringify({ lokasi: lokasi, partno: partno }),
        success: function (data) {
            if (data.d == null) {
                $("#stokakhir").val(0);
            } else {
                $("#stokakhir").val(data.d);
            }
        },
        error: function (jqXHR, exception) {
        }
    });
}

function RequestPanelNC() {
    if (deptid == 6 || deptid == 10) {
        $(".showhidden").css("visibility", "visible");
    }
}

String.prototype.replaceAt = function (index, replacement) {
    return this.substr(0, index) + replacement + this.substr(index + replacement.length);
};


function leftPad(value, length) {
    return ('0'.repeat(length) + value).slice(-length);
};

function AutoBS() {
    
    kali = (Math.floor(lebarawal / lebarakhir) * Math.floor(panjangawal/panjangakhir));

    var cekluas = true;
    var luasisa;
    var lebarBS1;
    var panjangBS1;
    var lebarBS2;
    var panjangBS2;
    var lebarBS3;
    var panjangBS3;
    var lebarBS4;
    var panjangBS4;

    //radio button
    if ($("#potong1").prop("checked")) {
        ClearAutoBS();
        luasisa = (lebarawal * panjangawal) - (lebarakhir * panjangakhir * Math.floor(kali));
        lebarBS1 = 20;
        panjangBS1 = panjangawal;
        lebarBS2 = lebarawal - (lebarakhir * Math.floor(lebarawal / lebarakhir)) - lebarBS1;
        panjangBS2 = panjangawal;
        lebarBS3 = 40;
        panjangBS3 = (lebarakhir * Math.floor(lebarawal / lebarakhir));
        lebarBS4 = panjangawal - 40 - (panjangakhir * Math.floor(panjangawal / panjangakhir));
        if (lebarBS4 > 400) {
            lebarBS4 = 400;
            lebarBS3 = panjangawal - lebarBS4 - (panjangakhir * Math.floor(panjangawal / panjangakhir));
        }
        panjangBS4 = panjangBS3;

        if (luasisa <= 0) {
            cekluas = false;
        }
        if (cekluas == true) {
            if (panjangBS1 > 0 && lebarBS1 > 0) {
                partnoBS1 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS1, 4) + leftPad(panjangBS1, 4) + partnoakhir.substring(17, 19));
            }

            if (deptid == 28) {
                $("#lcpartnobs1").text('(' +partnoBS1 + "-" + "KAT)");
            }
            else if (partnoBS1 != null) {
                $("#lcpartnobs1").text('(' + partnoBS1 + ')');
            } else {
                $("#lcpartnobs1").text("(-)");
            }

            if (partnoBS1 != null) {
                $("#lcqtybs1").text($("#quantity").val());
                if (lebarBS1 > 0) {
                    if (lebarBS1 < 400) {
                        $("#lclokasibs1").text("BSAUTO");
                    } else {
                        $("#lclokasibs1").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs1").text("(-)");
            }
            luasisa = luasisa - (lebarBS1 * panjangBS1);
        }

        if (luasisa <= 0) {
            cekluas = false;
        }

        if (cekluas == true) {
            if (panjangBS2 > 0 && lebarBS2 > 0) {
                partnoBS2 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS2, 4) + leftPad(panjangBS2, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs2").text('(' +partnoBS2 + "-" + "KAT)");
            }
            else if (partnoBS2 != null) {
                $("#lcpartnobs2").text('(' + partnoBS2 + ')');
            } else {
                $("#lcpartnobs2").text("(-)");
            }

            if (partnoBS2 != null) {
                $("#lcqtybs2").text($("#quantity").val());
                if (lebarBS2 > 0) {
                    if (lebarBS1 < 400) {
                        $("#lclokasibs2").text("BSAUTO");
                    } else {
                        $("#lclokasibs2").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs2").text("(-)");
            }
            luasisa = luasisa - (lebarBS2 * panjangBS2);
        }

        if (luasisa <= 0) {
            cekluas = false;
        }

        if (cekluas == true) {
            if (panjangBS3 > 0 && lebarBS3 > 0) {
                partnoBS3 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS3, 4) + leftPad(panjangBS3, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs3").text('(' +partnoBS3 + "-" + "KAT)");
            }
            else if (partnoBS3 != null) {
                $("#lcpartnobs3").text('(' + partnoBS3 + ')');
            } else {
                $("#lcpartnobs3").text("(-)");
            }

            if (partnoBS3 != null) {
                $("#lcqtybs3").text($("#quantity").val());
                if (lebarBS3 > 0) {
                    if (lebarBS3 < 400) {
                        $("#lclokasibs3").text("BSAUTO");
                    } else {
                        $("#lclokasibs3").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs3").text("(-)");
            }
            luasisa = luasisa - (lebarBS3 * panjangBS3);
        }


        if (luasisa <= 0) {
            cekluas = false;
        }
        if (cekluas == true) {
            if (panjangBS4 > 0 && lebarBS4 > 0) {
                partnoBS4 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS4, 4) + leftPad(panjangBS4, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs4").text('(' +partnoBS4 + "-" + "KAT)");
            }
            else if (partnoBS4 != null) {
                $("#lcpartnobs4").text('(' + partnoBS4 + ')');
            } else {
                $("#lcpartnobs4").text("(-)");
            }

            if (partnoBS4 != null) {
                $("#lcqtybs4").text($("#quantity").val());
                if (lebarBS4 > 0) {
                    if (lebarBS4 < 400) {
                        $("#lclokasibs4").text("BSAUTO");
                    } else {
                        $("#lclokasibs4").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs4").text("(-)");
            } 
            luasisa = luasisa - (lebarBS4 * panjangBS3);
        }
    } else {
        ClearAutoBS();
        luasisa = (lebarawal * panjangawal) - (lebarakhir * panjangakhir * Math.floor(kali));
        lebarBS1 = 20;
        panjangBS1 = panjangawal;
        lebarBS2 = lebarawal - (lebarakhir * Math.floor(lebarawal / lebarakhir)) - lebarBS1;
        panjangBS2 = panjangawal;
        lebarBS3 = 200;
        panjangBS3 = lebarakhir;
        lebarBS4 = panjangawal - (panjangakhir * kali) - lebarBS3;
        panjangBS4 = panjangBS3;

        if (luasisa <= 0) {
            cekluas = false;
        }
        if (cekluas == true) {
            if (panjangBS1 > 0 && lebarBS1 > 0) {
                partnoBS1 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS1, 4) + leftPad(panjangBS1, 4) + partnoakhir.substring(17, 19));
            }

            if (deptid == 28) {
                $("#lcpartnobs1").text('(' +partnoBS1 + "-" + "KAT)");
            }
            else if (partnoBS1 != null) {
                $("#lcpartnobs1").text('(' + partnoBS1 + ')');
            } else {
                $("#lcpartnobs1").text("(-)");
            }

            if (partnoBS1 != null) {
                $("#lcqtybs1").text($("#quantity").val());
                if (lebarBS1 > 0) {
                    if (lebarBS1 < 400) {
                        $("#lclokasibs1").text("BSAUTO");
                    } else {
                        $("#lclokasibs1").text("BSAUTO");
                    }
                }
            }
            else {
                $("#lclokasibs1").text("(-)");
            }
            luasisa = luasisa - ((lebarBS1 * panjangBS1) * 2);
        }


        if (luasisa <= 0) {
            cekluas = false;
        }

        if (cekluas == true) {
            if (panjangBS2 > 0 && lebarBS2 > 0) {
                partnoBS2 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS2, 4) + leftPad(panjangBS2, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs2").text('(' +partnoBS2 + "-" + "KAT)");
            }
            else if (partnoBS2 != null) {
                $("#lcpartnobs2").text('(' + partnoBS2 + ')');
            } else {
                $("#lcpartnobs2").text("(-)");
            }

            if (partnoBS2 != null) {
                $("#lcqtybs2").text($("#quantity").val());
                if (lebarBS2 > 0) {
                    if (lebarBS1 < 400) {
                        $("#lclokasibs2").text("BSAUTO");
                    } else {
                        $("#lclokasibs2").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs2").text("(-)");
            }
            luasisa = luasisa - (lebarBS2 * panjangBS2);
        }

        if (luasisa <= 0) {
            cekluas = false;
        }

        if (cekluas == true) {
            if (panjangBS3 > 0 && lebarBS3 > 0) {
                partnoBS3 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS3, 4) + leftPad(panjangBS3, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs3").text('(' +partnoBS3 + "-" + "KAT)");
            }
            else if (partnoBS3 != null) {
                $("#lcpartnobs3").text('(' + partnoBS3 + ')');
            } else {
                $("#lcpartnobs3").text("(-)");
            }

            if (partnoBS3 != null) {
                $("#lcqtybs3").text($("#quantity").val());
                if (lebarBS3 > 0) {
                    if (lebarBS3 < 400) {
                        $("#lclokasibs3").text("BSAUTO");
                    } else {
                        $("#lclokasibs3").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs3").text("(-)");
            }
            luasisa = luasisa - (lebarBS3 * panjangBS3);
        }


        if (luasisa <= 0) {
            cekluas = false;
        }
        if (cekluas == true) {
            if (panjangBS4 > 0 && lebarBS4 > 0) {
                partnoBS4 = partnoakhir.replaceAt(3, "-S-" + leftPad((tebalakhir * 10), 3) + leftPad(lebarBS4, 4) + leftPad(panjangBS4, 4) + partnoakhir.substring(17, 19));
            }

            /** Ditambahkan Base On WO Accounting **/
            if (deptid == 28) {
                $("#lcpartnobs4").text('(' + partnoBS4 + "-" + "KAT)");
            }
            else if (partnoBS4 != null) {
                $("#lcpartnobs4").text('(' + partnoBS4 + ')');
            } else {
                $("#lcpartnobs4").text("(-)");
            }

            if (partnoBS4 != null) {
                $("#lcqtybs4").text($("#quantity").val());
                if (lebarBS4 > 0) {
                    if (lebarBS4 < 400) {
                        $("#lclokasibs4").text("BSAUTO");
                    } else {
                        $("#lclokasibs4").text("BSAUTO");
                    }
                }
            } else {
                $("#lclokasibs4").text("(-)");
            }
        }
    }
   
}

$('#carapotong input').on('change', function () {
    ClearAutoBS();
    AutoBS();
});

$('#panelnc input').on('change', function () {
    var panelvalue = $('input[name=panel]:checked', '#panelnc').val();
    if (panelvalue == "nchandling") {
        $(".showhiddenpanel").css("visibility", "hidden");
    } else if (panelvalue == "ncsortir") {
        $(".showhiddenpanel").css("visibility", "visible");
    } else if (panelvalue == "nonnc") {
        $(".showhiddenpanel").css("visibility", "visible");
    }
});

function ClearAutoBS() {
    partnoBS1 = null;
    partnoBS2 = null;
    partnoBS3 = null;
    partnoBS4 = null;
    $("#lcpartnobs1").text('(-)');
    $("#lcpartnobs2").text('(-)');
    $("#lcpartnobs3").text('(-)');
    $("#lcpartnobs4").text('(-)');

    $("#lclokasibs1").text(0);
    $("#lclokasibs2").text(0);
    $("#lclokasibs3").text(0);
    $("#lclokasibs4").text(0);

    $("#lcqtybs1").text('');
    $("#lcqtybs2").text('');
    $("#lcqtybs3").text('');
    $("#lcqtybs4").text('');
}

function ClearForm() {
    partnoBS1 = null;
    partnoBS2 = null;
    partnoBS3 = null;
    partnoBS4 = null;
    $("#lcpartnobs1").text('(-)');
    $("#lcpartnobs2").text('(-)');
    $("#lcpartnobs3").text('(-)');
    $("#lcpartnobs4").text('(-)');

    $("#lclokasibs1").text(0);
    $("#lclokasibs2").text(0);
    $("#lclokasibs3").text(0);
    $("#lclokasibs4").text(0);

    $("#lcqtybs1").text('');
    $("#lcqtybs2").text('');
    $("#lcqtybs3").text('');
    $("#lcqtybs4").text('');


    
    $("#caripartno").val(null);
    $('#groupmarketing').val(null);

    $("#partno").val(null);
    $("#tebal").val(null);
    $("#panjang").val(null);
    $("#lebar").val(null);
    $("#lokasi").val(null);
    $("#partname").val(null);
    $("#stok").val(null);
    $("#quantity").val(null);
    $("#lokasiakhir").val(null);
    
    $("#partnoakhir").val(null);
    $("#tebalakhir").val(null);
    $("#panjangakhir").val(null);
    $("#lebarakhir").val(null);
    $("#partnameakhir").val(null);
    $("#stokakhir").val(null);
    $("#quantityakhir").val(null);

    $('#tableListProdukStok').empty();
    $("#selectdefect").html('');
    $("#selectmesincutter").html('');
}

function RequestDeptID() {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/GetUserDept",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            deptid = data.d[0].DeptId;
            username = data.d[0].Username;
            if (deptid == 3) {
                RequestListMesinCutter();
            }
        },
        error: function (jqXHR, exception) {
        }
    });
}


$("#transfer").click(function () {
    if (partnoawal == '' || partnoakhir == '' || lokasiakhir == '' || $("#quantity").val() == '' || $("#lokasiakhir").val() == '') {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Belum Lengkap !!!',
            theme: 'modern',
            type: 'red'
        });
        return false;
    } else if (deptid == 3 && $('#cutter').val() == 0) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Anda Belum Menentukan Pilihan Mesin Cutter !!',
            theme: 'modern',
            type: 'red'
        });
        return false;
    } else if ((partnoawal.indexOf('-3-') !== -1 || partnoawal.indexOf('-M-') != -1) && partnoakhir.indexOf('-P-') !== -1 && $('#defect').val() == 0) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Anda Belum Menentukan Jenis Defect !!',
            theme: 'modern',
            type: 'red'
        });
        return false;
    } else if ($("#quantity").val() == '' || $("#quantityakhir").val() == '' || $('#groupmarketing').val() == 0) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Data Belum Lengkap"',
            theme: 'modern',
            type: 'red'
        });
        return false;
    } else if (partnoakhir.indexOf('-1-') !== -1) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Partno Tahap Satu Tidak Di Ijinkan',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    //verifikasi closing status
    var tahun = $("#tglproduksi").val().split('-')[2];
    var bulan = $("#tglproduksi").val().split('-')[1];

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


    var lastAvgHPP1 = 0;
    var laststock1 = stokawal;
    var lastAvgHPP2 = 0;
    var laststock2 = stokakhir;

    if (luasawal < luasakhir) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Ukuran Tidak Mencukupi',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    if (panjangawal < panjangakhir) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Panjang Partno Tujuan Tidak Mencukupi',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    if ((stokawal - $("#quantity").val()) < 0) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Stock "' + $("#lokasi").val() + '" Tidak Mencukupi, Proses Dibatalkan !',
            theme: 'modern',
            type: 'red'
        });
        return false;
    }

    //Retrieve Partno bukan Listplank added 08 Agustus 2019 By Beny (citeureup)
    RequestPartnoBukanListplank(tebalakhir, lebarakhir, panjangakhir);

    if ((tebalawal >= 8 && tebalawal <= 9 && panjangawal >= 2200 && lebarawal > 1000 && lock > 0 && id > 0)
        || (tebalawal >= 8 && tebalawal <= 9 && panjangawal >= 2200 && lebarawal > 1000 && id == 0)) {
        if (lebarakhir <= 300 && lebarakhir >= 100) {
            $.alert({
                icon: 'fa fa-times',
                title: 'Warning!',
                content: 'Lakukan Proses Ini Pada Inputan Proses Listplank',
                theme: 'modern',
                type: 'red'
            });
        }
        return false;
    }

    var t3serahK;//Asal Partno
    var t3serahT;//terima Partno
    var rekapK;//asal partno qtyout
    var rekapT;//terima partno qtyin
    var awalID = 0;

    t3serahK = {
        Flag: "Kurang",
        ItemID: itemidawal,
        GroupID: groupidawal,
        ID: idawal,
        LokID: lokidawal,
        Qty: $("#quantity").val(),
        CreatedBy: username
    };

    rekapK = {
             DestID: 0,
            SerahID: idawal,
            T1serahID: 0,
            GroupID: groupidawal,
            LokasiID: lokidawal,
            ItemIDSer: itemidawal,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: 0,
            QtyOutTrm: $("#quantity").val(),
            HPP: lastAvgHPP1,
            Keterangan: partnoakhir,
            SA: stokawal,
            Process: "Simetris",
            CreatedBy: username
    };

    //proses lokasi akhir
    t3serahT = {
        t3serahTambah: [{
            Flag: "Tambah",
            ItemID: itemidakhir,
            GroupID: $('#groupmarketing').val(),
            ID: idawal,
            LokID: lokidakhir,
            Qty: $("#quantityakhir").val(),
            HPP: 0,
            CreatedBy: username
        }]
    };

    rekapT = {
        rekapTambah: [{
            DestID: 0,
            SerahID: idawal,
            T1serahID: 0,
            GroupID: $('#groupmarketing').val(),
            LokasiID: lokidakhir,
            ItemIDSer: itemidakhir,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: $("#quantityakhir").val(),
            QtyOutTrm: 0,
            HPP: 0,
            Keterangan: partnoawal,
            SA: $("#stokakhir").val(),
            Process: "Simetris",
            CreatedBy: username
        }]
    };


    var newitemID = 0;
    //cek lokasi finishing (Z99)
    CekLokasi('Z99');
    if (idlokasif == null) {
        InsertLokasi(3, 'Z99');
    }
    //cek lokasi buangan (BSAUTO)
    CekLokasiBuangan('BSAUTO');
    if (idlokasibuangan == null) {
        InsertLokasi(3, 'BSAUTO');
    }

    //AutoBS1
    var cekpartno;
    var item;
    var LokasiBSID = 0;
    if ($("#lcpartnobs1").text() != '(-)' && $("#lcpartnobs1").text().length > 15) {
        CekPartno(partnoBS1);
        if (partnobs == null) {
            item = {
                ItemTypeID: 3,
                Kode: partnoBS1.substring(0, 3),
                Tebal: partnoBS1.substring(7, 8),
                Lebar: partnoBS1.substring(11, 13),
                Panjang: partnoBS1.substring(13, 17),
                Volume: (partnoBS1.substring(7, 8)) / 100 * (partnoBS1.substring(11, 13)) / 1000 * (partnoBS1.substring(13, 17)) / 1000,
                Partno: partnoBS1,
                ItemDesc: "Sisa Potong",
                GroupID: 0
            };
            InsertPartno(item);
            GetPartNo(partnoBS1);
            newitemID = idpartnobsauto;
        }
        else {
            GetPartNo(partnoBS1);
            newitemID = idpartnobsauto;
        }

        LokasiBSID = 0;
        LokasiBSID = idlokasibuangan;

        if (newitemID > 0) {
            t3serahTambah = {
                Flag: "Tambah",
                ItemID: newitemID,
                GroupID: groupidbsauto,
                LokID: LokasiBSID,
                Qty: $("#lcqtybs1").text(),
                CreatedBy: username
            };
            t3serahT.t3serahTambah.push(t3serahTambah);
        }
        CekStokPartno(LokasiBSID, newitemID);
        rekapTambah = {
            DestID: 0,
            SerahID: idawal,
            GroupID: $('#groupmarketing').val(),
            T1serahID: 0,
            LokasiID: LokasiBSID,
            ItemIDSer: newitemID,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: $("#lcqtybs1").text(),
            QtyOutTrm: 0,
            HPP: 0,
            Keterangan: partnoawal,
            SA: stokpartno,
            Process: "Simetris",
            CreatedBy: username
        };
        rekapT.rekapTambah.push(rekapTambah);
    }

    if ($("#lcpartnobs2").text() != '(-)' && $("#lcpartnobs2").text().length > 15) {
        CekPartno(partnoBS2);
        if (partnobs == null) {
            item = {
                ItemTypeID: 3,
                Kode: partnoBS2.substring(0, 3),
                Tebal: partnoBS2.substring(7, 8),
                Lebar: partnoBS2.substring(11, 13),
                Panjang: partnoBS2.substring(13, 17),
                Volume: (partnoBS2.substring(7, 8)) / 100 * (partnoBS2.substring(11, 13)) / 1000 * (partnoBS2.substring(13, 17)) / 1000,
                Partno: partnoBS2,
                ItemDesc: "Sisa Potong",
                GroupID: 0
            };
            InsertPartno(item);
            GetPartNo(partnoBS2);
            newitemID = idpartnobsauto;
        }
        else {
            GetPartNo(partnoBS2);
            newitemID = idpartnobsauto;
        }

        LokasiBSID = 0;
        LokasiBSID = idlokasibuangan;

        if (newitemID > 0) {
            t3serahTambah = {
                Flag: "Tambah",
                ItemID: newitemID,
                GroupID: groupidbsauto,
                LokID: LokasiBSID,
                Qty: $("#lcqtybs2").text(),
                CreatedBy: username
            };
            t3serahT.t3serahTambah.push(t3serahTambah);
        }
        CekStokPartno(LokasiBSID, newitemID);
        rekapTambah = {
            DestID: 0,
            SerahID: idawal,
            GroupID: $('#groupmarketing').val(),
            T1serahID: 0,
            LokasiID: LokasiBSID,
            ItemIDSer: newitemID,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: $("#lcqtybs2").text(),
            QtyOutTrm: 0,
            HPP: 0,
            Keterangan: partnoawal,
            SA: stokpartno,
            Process: "Simetris",
            CreatedBy: username
        };
        rekapT.rekapTambah.push(rekapTambah);
    }


    if ($("#lcpartnobs3").text() != '(-)' && $("#lcpartnobs3").text().length > 15) {
        CekPartno(partnoBS3);
        if (partnobs == null) {
            item = {
                ItemTypeID: 3,
                Kode: partnoBS3.substring(0, 3),
                Tebal: partnoBS3.substring(7, 8),
                Lebar: partnoBS3.substring(11, 13),
                Panjang: partnoBS3.substring(13, 17),
                Volume: (partnoBS3.substring(7, 8)) / 100 * (partnoBS3.substring(11, 13)) / 1000 * (partnoBS3.substring(13, 17)) / 1000,
                Partno: partnoBS3,
                ItemDesc: "Sisa Potong",
                GroupID: 0
            };
            InsertPartno(item);
            GetPartNo(partnoBS2);
            newitemID = idpartnobsauto;
        }
        else {
            GetPartNo(partnoBS3);
            newitemID = idpartnobsauto;
        }

        LokasiBSID = 0;
        LokasiBSID = idlokasibuangan;

        if (newitemID > 0) {
            t3serahTambah = {
                Flag: "Tambah",
                ItemID: newitemID,
                GroupID: groupidbsauto,
                LokID: LokasiBSID,
                Qty: $("#lcqtybs3").text(),
                CreatedBy: username
            };
            t3serahT.t3serahTambah.push(t3serahTambah);
        }
        CekStokPartno(LokasiBSID, newitemID);
        rekapTambah = {
            DestID: 0,
            SerahID: idawal,
            GroupID: $('#groupmarketing').val(),
            T1serahID: 0,
            LokasiID: LokasiBSID,
            ItemIDSer: newitemID,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: $("#lcqtybs3").text(),
            QtyOutTrm: 0,
            HPP: 0,
            Keterangan: partnoawal,
            SA: stokpartno,
            Process: "Simetris",
            CreatedBy: username
        };
        rekapT.rekapTambah.push(rekapTambah);
    }

    if ($("#lcpartnobs4").text() != '(-)' && $("#lcpartnobs4").text().length > 15) {
        CekPartno(partnoBS4);
        if (partnobs == null) {
            item = {
                ItemTypeID: 3,
                Kode: partnoBS4.substring(0, 3),
                Tebal: partnoBS4.substring(7, 8),
                Lebar: partnoBS4.substring(11, 13),
                Panjang: partnoBS4.substring(13, 17),
                Volume: (partnoBS4.substring(7, 8)) / 100 * (partnoBS4.substring(11, 13)) / 1000 * (partnoBS4.substring(13, 17)) / 1000,
                Partno: partnoBS4,
                ItemDesc: "Sisa Potong",
                GroupID: 0
            };
            InsertPartno(item);
            GetPartNo(partnoBS4);
            newitemID = idpartnobsauto;
        }
        else {
            GetPartNo(partnoBS2);
            newitemID = idpartnobsauto;
        }

        LokasiBSID = 0;
        LokasiBSID = idlokasibuangan;

        if (newitemID > 0) {
            t3serahTambah = {
                Flag: "Tambah",
                ItemID: newitemID,
                GroupID: groupidbsauto,
                LokID: LokasiBSID,
                Qty: $("#lcqtybs4").text(),
                CreatedBy: username
            };
            t3serahT.t3serahTambah.push(t3serahTambah);
        }

        CekStokPartno(LokasiBSID, newitemID);
        rekapTambah = {
            DestID: 0,
            SerahID: idawal,
            GroupID: $('#groupmarketing').val(),
            T1serahID: 0,
            LokasiID: LokasiBSID,
            ItemIDSer: newitemID,
            TglTrm: $("#tglproduksi").val(),
            QtyInTrm: $("#lcqtybs4").text(),
            QtyOutTrm: 0,
            HPP: 0,
            Keterangan: partnoawal,
            SA: stokpartno,
            Process: "Simetris",
            CreatedBy: username
        };
        rekapT.rekapTambah.push(rekapTambah);
    }

    //rekam table simetris
    var cutter;
    var nch;
    var ncss;
    var ncse;
    var bs;
    var defect;

    if (deptid == 3) {
       cutter = $('#cutter').val();
    } else {
        cutter = "-";
    }

    if ($("#nonnc").prop("checked")) {
        nch = 0;
        ncss = 0;
        ncse = 0;
    }
    else if ($("#nchendling").prop("checked")) {
        nch = 1;
        ncss = 0;
        ncse = 0;
    }
    else if ($("#ncsortir").prop("checked") && $("#RBStd").prop("checked")) {
        nch = 0;
        ncss = 1;
        ncse = 0;
    }
    else if ($("#ncsortir").prop("checked") && $("#RBEfo").prop("checked")) {
        nch = 0;
        ncss = 0;
        ncse = 1;
    }
    else {
        nch = 0;
        ncss = 0;
        ncse = 0;
    }
    if ($(".showhiddenbs").css('visibility') == 'visible') {
        if ($("#finishing").prop("checked")) {
            bs = "FIN";
        } else if ($("#logistik").prop("checked")) {
            bs = "LOG";
        } else {
            bs = "";
        }
    } else {
        bs = "";
    }

    if ($('#defect').val() == null) {
        defect = 0;
    }
    else {
        defect = $('#defect').val();
    }

    simetris = {
        RekapID: 0,
        SerahID: idawal,
        LokasiID: lokidakhir,
        TglSm: $("#tglproduksi").val(),
        ItemID: itemidakhir,
        QtyInSm: $("#quantity").val(),
        QtyOutSm: $("#quantityakhir").val(),
        GroupID: $('#groupmarketing').val(),
        MCutter: cutter,
        NCH : nch,
        NCSS : ncss,
        NCSE: ncse,
        BS: bs,
        Defect: defect,
        CreatedBy: username
    };

    InsertRekapSerah(t3serahK, t3serahT, rekapK, rekapT, simetris);
    RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
});



function RequestPartnoBukanListplank(tebalakhir, lebarakhir, panjangakhir) {
    $.ajax({
        url: "Simetris.aspx/GetPartnoBukanListplank",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ tebal: tebalakhir, lebar: lebarakhir, panjang: panjangakhir }),
        success: function (data) {
            id = data.d.id;
            lock = data.d.lock;
        }
    });
}

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

function CekLokasi (lokasi) {
    $.ajax({
        url: "Simetris.aspx/CekLokasi",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ lokasi: lokasi }),
        success: function (data) {
            if (data.d.length != 0 && data.d != null) {
                idlokasif = data.d[0].ID;
            } else {
                idlokasif = null;
            }
            
        }
    });
}

function CekLokasiBuangan(lokasi) {
    $.ajax({
        url: "Simetris.aspx/CekLokasi",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ lokasi: lokasi }),
        success: function (data) {
            if (data.d.length != 0 && data.d != null) {
                idlokasibuangan = data.d[0].ID;
            } else {
                idlokasibuangan = null;
            }

        }
    });
}


function InsertLokasi(id, lokasi) {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/InsertLokasi",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ id: id, lokasi: lokasi }),
        dataType: "json",
        success: function (data) {

        },
        error: function (jqXHR, exception) {

        }
    });
}

function CekPartno(partno) {
    $.ajax({
        url: "Simetris.aspx/CekPartno",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ partno: partno }),
        success: function (data) {
            if (data.d.length != 0 && data.d != null) {
                partnobs = data.d[0].ID;
            } else  {
                partnobs = null;
            }  
        },
        error: function (jqXHR, exception) {

        }
    });
}

function InsertPartno(item) {
    $.ajax({
        url: "Simetris.aspx/InsertPartno",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ item: item }),
        dataType: "json",
        beforeSend: function () {
            // Show image container
            $("#loading").show();
        },
        success: function (data) {
            ClearForm();
        },
        complete: function (data) {
            // Hide image container
            $("#loading").hide();
        },
        error: function (jqXHR, exception) {

        }
    });
}

function GetPartNo(partno) {
    $.ajax({
        url: "Simetris.aspx/CekPartno",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ partno: partno }),
        success: function (data) {
            idpartnobsauto = data.d[0].ID;
            groupidbsauto = data.d[0].GroupID;
        }
    });
}

function CekStokPartno(lokid, itemid) {
    $.ajax({
        url: "Simetris.aspx/GetStock",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ lokid: lokid, itemid: itemid }),
        success: function (data) {
            if (data.d.length != 0 && data.d != null) {
                stokpartno = data.d;
            } else {
                stokpartno = 0;
            }
        }
    });
}

function CancelSimetris(id, partnosm, lokasism, qtyoutsm) {
    var stoklokasi;
    $.ajax({
        url: "Simetris.aspx/GetStokLokasi",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ partno: partnosm, lokasi: lokasism }),
        success: function (data) {
            stoklokasi = data.d;
        }
    });

    if (qtyoutsm > stoklokasi) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Cancel Simetris Tidak Bisa Dilakukan, Karena Stock Partno: ' + partnosm + ' Di Lokasi : ' + lokasism +' Tidak Mencukupi',
            theme: 'modern',
            type: 'red'
        });
    } else {
        $.ajax({
            url: "Simetris.aspx/CancelSimetris",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ id: id }),
            beforeSend: function () {
                $("#loading").show($.blockUI({ message: null }));
            },
            success: function (data) {
                RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
            },
            complete: function (data) {
                $("#loading").hide($.unblockUI());
            }
        });
    }


 
}

function InsertRekapSerah(t3serahK, t3serahT, rekapK, rekapT, simetris) {
    $.ajax({
        type: "POST",
        url: "Simetris.aspx/InsertRekapSerah",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ t3serahK: t3serahK, rekapK: rekapK, t3serahT: t3serahT, rekapT: rekapT, simetris: simetris }),
        dataType: "json",
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (data.d == "" || data.d == null) {
                $.alert({
                    icon: 'fa fa-check',
                    title: 'Success',
                    content: 'Data Tersimpan',
                    theme: 'modern',
                    type: 'green'
                });
                RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
            } else {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Data Gagal Disimpan',
                    theme: 'modern',
                    type: 'red'
                });
            }
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        },
        error: function (jqXHR, exception) {

        }
    });
}

//load table list simetris saat tgl diganti
$(".date-picker").datepicker({
    onSelect: function (dateText) {
        RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
        $(this).change();
    }
}).on("change", function () {
    RequestSimetris($("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0]);
});

function RequestSimetris(tgl) {
    $.ajax({
        url: "Simetris.aspx/GetSimetris",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ tgl: tgl}),
        success: function (data) {
            if (!isPertamaSm) {
                $("#tableListSimetris").DataTable().destroy();
                $('#tableListSimetris').empty();
            } else {
                isPertamaSm = false;
            }
            ClearForm();
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListSimetris");
            oTblReport.DataTable({
                "data": datatable,
                "responsive" : true,
                "columns": [
                    { "data": "PartnoSer", title: "Partno Awal" },
                    { "data": "LokasiSer", title: "Lokasi" },
                    { "data": "QtyInSm", title: "Qty" },
                    { "data": "Groups", title: "Groups" },
                    { "data": "PartnoSm", title: "Partno Akhir" },
                    { "data": "LokasiSm", title: "Lokasi" },
                    { "data": "QtyOutSm", title: "Qty" },
                    { "data": "MCutter", title: "Mesin" },
                    { "data": "CreatedBy", title: "Users" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-danger btn-sm' type='button' style='margin-right:5px;' onclick='CancelSimetris(" + row.ID + ",\"" + row.PartnoSm + "\",\"" + row.LokasiSm + "\"," + row.QtyOutSm + ")'><i class='fa fa-check'></i> Cancel </button>";
                            return aksi;
                        },
                        "defaultContent": ""
                    }
                ]
            });
        }
    });
}