var isPertama = true;
var status;
var sno = [];
var load;
var no_rows = 0;
var row;
var mainArr = [];
var tmpArr = [];
var totalserah;
var totalsisa;
var plrnpartno = '';
var totalplr = 0;
var unitkerja;
var mm = 9;
var idglobal;
var dest;
var lok;
var partnoawal;
var ItemIDDest;
var pelarian = {
    lari: []
};
var pelarian8 = {
    lari: []
};

var pelarianlp = {
    lari: []
};
var remove = 0;

$("#tglproduksi").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", -1);
$("#tglpotong").datepicker({ dateFormat: 'dd-mm-yy' }).datepicker("setDate", -1);

//auto complete 8/9mm
$("#partnospok").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnospok").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnospok").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partnospbp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnospbp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnospbp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };

$("#partnosplp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnosplp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosplp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };

$("#partnospunf").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnospunf").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnospunf").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partnospsmp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnospsmp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnospsmp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partnoplrnsp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partnoplrnsp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnoplrnsp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partnosplokok").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnosplokok").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosplokok").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partnosplokbp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnosplokbp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosplokbp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partnosploklp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnosploklp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosploklp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

$("#partnosplokunf").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnosplokunf").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosplokunf").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partnosploksmp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnosploksmp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnosploksmp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partnoplrnloksp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnoplrnloksp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnoplrnloksp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

//auto complete 4mm
$("#partno4ok").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4ok").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4ok").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partno4kw").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4kw").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4kw").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };

$("#partno4bp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4bp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4bp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };

$("#partno4unf").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4unf").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4unf").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partno4unf2").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4unf2").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4unf2").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partno4smp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4smp").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4smp").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };

$("#partno4plrn").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetNoProdukStok",
            data: "{'partno':'" + $("#partno4plrn").val() + "','groupid':''}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4plrn").val(ui.item.PartNo);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.PartNo + "</a>")
            .appendTo(ul);
    };


$("#partnolok4ok").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partnolok4ok").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partnolok4ok").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4lokkw").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lokkw").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lokkw").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4lokbp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lokbp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lokbp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

$("#partno4lok2bp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lok2bp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lok2bp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4lokunf").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lokunf").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lokunf").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4lokunf2").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lokunf2").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lokunf2").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4loksmp").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4loksmp").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4loksmp").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };


$("#partno4lokplrn").autocomplete({
    source: function (request, response) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Simetris.aspx/GetListLokasi",
            data: "{'lokasi':'" + $("#partno4lokplrn").val() + "'}",
            dataType: "json",
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $("#partno4lokplrn").val(ui.item.Lokasi);
        return false;
    }
})
    .data("ui-autocomplete")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<a>" + item.Lokasi + "</a>")
            .appendTo(ul);
    };

$(document).ready(function () {
    $("#loading").hide($.unblockUI());
    RequestUnitKerja();
    RequestGroupCutter();
    RequestListJemur(0, 9);
    RequestListSerah();
});

$("#addpelariansp").click(function () {
    row = "<tr class='deleteall' id=tasklist>" +
        "<td><input class='partno' type=text size=40 value=" + plrnpartno+"></input></td>" +
        "<td><input class='lok' type=text size=2 value=P99></input></td>" +
        "<td><input class='qty' type=text size=2 value=0></input></td>" +
        "<td><button type='button'  class='RemoveTr btn btn-xs btn-danger'>Remove</button></td></tr > ";
    $("#tablepelariansp > tbody").append(row);
    ++no_rows;
});

$("#addpelarianlp").click(function () {
    row = "<tr class='deleteall' id=tasklist>" +
        "<td><input class='partno' type=text size=40 value=" + plrnpartno + "></input></td>" +
        "<td><input class='lok' type=text size=2 value=P99></input></td>" +
        "<td><input class='qty8' type=text size=2 value=0></input></td>" +
        "<td><button type='button'  class='RemoveTrlp btn btn-xs btn-danger'>Remove</button></td></tr > ";
    $("#tablepelarianlp > tbody").append(row);
    ++no_rows;
});

$("#addpelarian4").click(function () {
    row = "<tr class='deleteall' id=tasklist>" +
        "<td><input type=text size=40 value=" + plrnpartno + "></input></td>" +
        "<td><input type=text size=2 value=P99></input></td>" +
        "<td><input class='qty4' type=text size=2 value=0></input></td>" +
        "<td><button type='button'  class='RemoveTr4 btn btn-xs btn-danger'>Remove</button></td></tr > ";
    $("#tablepelarian4 > tbody").append(row);
    ++no_rows;
});


$(".date-picker").datepicker({
    onSelect: function (dateText) {
        if (mm == 9) {
            RequestListJemur(0, 9);
            RequestListSerah();
        } else if (mm == 4) {
            RequestListJemur(0, 4);
            RequestListSerah();
        }

        $(this).change();
    }
}).on("change", function () {
    if (mm == 9) {
        RequestListJemur(0, 9);
        RequestListSerah();
    } else if (mm == 4) {
        RequestListJemur(0, 4);
        RequestListSerah();
    }
});


$("#tablepelariansp tbody").on("keyup change", function () {
    calc();
});

$("#tablepelarianlp tbody").on("keyup change", function () {
    calc8();
});


$("#tablepelarian4 tbody").on("keyup change", function () {
    calc4();
});

function calc() {
    var plrqty = [];
    var qty;
    $("#tablepelariansp tbody tr").each(function (i, element) {
        var html = $(this).html();
        if (html != "") {
            qty = $(this).find(".qty").val();
            if (qty != "" || qty != 0) {
                plrqty.push(qty);
            }
        }
        totalplr = 0;
        for (var i = 0; i < plrqty.length; i++) {
            totalplr += plrqty[i] << 0;
        }
        var qtyok = $("#partnospqtyok").val();
        var qtybp = $("#partnospqtybp").val();
        var qtylp = $("#partnospqtylp").val();
        var qtyunf = $("#partnospqtyunf").val();
        var qtysmp = $("#partnospqtysmp").val();
        var qtylist = $("#partnolpqtyok").val();
        var qtysmplist = $("#partnosmpqtylp").val();

        $("#partnospqtyok").val(totalserah - qtybp - qtylist - qtysmplist - qtylp - qtyunf - qtysmp - totalplr);
    });
}

function calc8() {
    var plrqty = [];
    $("#tablepelarianlp tbody tr").each(function (i, element) {
        var html = $(this).html();
        if (html != "") {
            var qty = $(this).find(".qty8").val();
            if (qty != "" || qty != 0) {
                plrqty.push(qty);
            }
        }
        totalplr = 0;
        for (var i = 0; i < plrqty.length; i++) {
            totalplr += plrqty[i] << 0;
        }
        var qtyok = $("#partnospqtyok").val();
        var qtybp = $("#partnospqtybp").val();
        var qtylp = $("#partnospqtylp").val();
        var qtyunf = $("#partnospqtyunf").val();
        var qtysmp = $("#partnospqtysmp").val();
        var qtylist = $("#partnolpqtyok").val();
        var qtysmplist = $("#partnosmpqtylp").val();

        $("#partnospqtyok").val(totalserah - qtybp - qtylp - qtylist - qtysmplist - qtyunf - qtysmp - totalplr);
    });
}

function calc4() {
    var plrqty = [];
    $("#tablepelarian4 tbody tr").each(function (i, element) {
        var html = $(this).html();
        if (html != "") {
            var qty = $(this).find(".qty4").val();
            if (qty != "" || qty != 0) {
                plrqty.push(qty);
            }
        }
        totalplr = 0;
        for (var i = 0; i < plrqty.length; i++) {
            totalplr += plrqty[i] << 0;
        }
        var qtyok = $("#partnoqty4ok").val();
        var qtykw = $("#partno4qtykw").val();
        var qtybp = $("#partno4qtybp").val();
        var qtyunf = $("#partno4qtyunf").val();
        var qtysmp = $("#partno4qtysmp").val();

        $("#partnoqty4ok").val(totalserah - qtykw - qtybp - qtyunf - qtysmp - totalplr);
    });
}

$(document).on('click', '.RemoveTr', function () {
    var qty = $(this).closest("tr").find(".qty").val();
    var qtyok = $("#partnospqtyok").val();
    $("#partnospqtyok").val(parseInt(qtyok) + parseInt(qty));
    $(this).closest('tr').remove();
});

$(document).on('click', '.RemoveTrlp', function () {
    var qty = $(this).closest("tr").find(".qty8").val();
    var qtyok = $("#partnospqtyok").val();
    $("#partnospqtyok").val(parseInt(qtyok) + parseInt(qty));
    $(this).closest('tr').remove();
});

$(document).on('click', '.RemoveTr4', function () {
    var qty = $(this).closest("tr").find(".qty4").val();
    var qtyok = $("#partnoqty4ok").val();
    $("#partnoqty4ok").val(parseInt(qtyok) + parseInt(qty));
    $(this).closest('tr').remove();
});

function loadValues() {
    var mainTable = $('#tablepelariansp');
    var tr = mainTable.find('tbody tr');
    tmpArr = [];
    tr.each(function () {
        
        $(this).find('td').each(function () {
            var values = $(this).find('input, select').val();
            tmpArr.push(values);
        });
        lari = {
            PartnoSer: tmpArr[0],
            PartnoDest: partnoawal,
            QtyIn: tmpArr[2],
            LokasiSer: tmpArr[1],
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "lari",
            Oven: $("#oven8 option:selected").text()
        };
        pelarian8.lari.push(lari);
        tmpArr = [];
    });
}


function loadValues8() {
    var mainTable = $('#tablepelarianlp');
    var tr = mainTable.find('tbody tr');
    tmpArr = [];
    tr.each(function () {

        $(this).find('td').each(function () {
            var values = $(this).find('input, select').val();
            tmpArr.push(values);
        });
        lari = {
            PartnoSer: tmpArr[0],
            PartnoDest: partnoawal,
            QtyIn: tmpArr[2],
            LokasiSer: tmpArr[1],
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "lari",
            Oven: $("#oven8 option:selected").text()
        };
        pelarianlp.lari.push(lari);
        tmpArr = [];
    });
}

function loadValues4() {
    var mainTable = $('#tablepelarian4');
    var tr = mainTable.find('tbody tr');
    tmpArr = [];
    tr.each(function () {

        $(this).find('td').each(function () {
            var values = $(this).find('input, select').val();
            tmpArr.push(values);
        });
        lari = {
            PartnoSer: tmpArr[0],
            PartnoDest: partnoawal,
            QtyIn: tmpArr[2],
            LokasiSer: tmpArr[1],
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "lari",
            Oven: $("#oven4 option:selected").text()
        };
        pelarian.lari.push(lari);
        tmpArr = [];
    });
}

$("#semua").change(function () {
    if (this.checked) {
        if (mm == 9) {
            RequestListJemur(1, 9);
        } else if (mm == 4) {
            RequestListJemur(1, 4);
        }
    } else {
        if (mm == 9) {
            RequestListJemur(1, 9);
        } else if (mm == 4) {
            RequestListJemur(1, 4);
        }
    }
});

function RequestUnitKerja() {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/GetUnitKerja",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            unitkerja = data.d;
        }
    });
}

function RequestGroupCutter() {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/GetGroupCutter",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#groupcutter").append('<option value="' + value.ID + '" >' + value.GroupCutCode + '</option>');
            });
        }
    });
}

function RequestJenis() {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/GetListJenis",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#lstplnkjenis").append('<option value="' + value.Groups + '" >' + value.Groups + '</option>');
            });
            $("#lstplnkjenis option[value=INT]").attr('selected', 'selected');

            $.each(data.d, function (data, value) {
                $("#lstpljenis4").append('<option value="' + value.Groups + '" >' + value.Groups + '</option>');
            });
            $("#lstpljenis4 option[value=INT]").attr('selected', 'selected');
        }
    });
}

function RequestListJemur(param, prod) {
    var tglprod = $("#tglproduksi").val().split('-')[2] + $("#tglproduksi").val().split('-')[1] + $("#tglproduksi").val().split('-')[0];
    $.ajax({
        url: "SerahTerimaPotongan.aspx/GetListJemur",
        type: "POST",
        contentType: "application/json",
        async:false,
        data: JSON.stringify({ tgl: tglprod, param: param, prod: prod }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (!isPertama) {
                $("#tableListjemur").DataTable().destroy();
                $('#tableListjemur').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListjemur");
            oTblReport.DataTable({
                "data": datatable,
                "columns": [
                    { "data": "Produksi", title: "Tanggal Produksi" },
                    { "data": "PartNo", title: "Partno" },
                    { "data": "FormulaCode", title: "Jenis" },
                    { "data": "Potong", title: "Tanggal Potong" },
                    { "data": "QtyIn", title: "QtyIn" },
                    { "data": "QtyOut", title: "QtyOut" },
                    { "data": "Sisa", title: "Sisa" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-primary' type='button' style='margin-right:5px;' onclick='FillData(" + row.ID + ",\"" + row.PartNo + "\"," + row.Sisa + ", \"" + row.FormulaCode + "\"," + row.Oven + "," + row.DestID + "," + row.ItemID + ")'><i class='fa fa-check'></i> Pilih </button>";
                            return aksi;
                        }
                    }
                ]
            });
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function RequestListSerah() {
    var tglprod = $("#tglpotong").val().split('-')[2] + $("#tglpotong").val().split('-')[1] + $("#tglpotong").val().split('-')[0];
    $.ajax({
        url: "SerahTerimaPotongan.aspx/GetListSerah",
        type: "POST",
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ tgl: tglprod }),
        beforeSend: function () {
            $("#loading").show($.blockUI({ message: null }));
        },
        success: function (data) {
            if (!isPertama) {
                $("#tableListserah").DataTable().destroy();
                $('#tableListserah').empty();
            } else {
                isPertama = false;
            }
            datatable = $.parseJSON(data.d);
            var oTblReport = $("#tableListserah");
            oTblReport.DataTable({
                "data": datatable,
                "columns": [
                    { "data": "Produksi", title: "Tanggal Produksi" },
                    { "data": "TglSerah", title: "Tanggal Serah" },
                    { "data": "PartnoDest", title: "Partno Awal" },
                    { "data": "PartnoSer", title: "Partno Akhir" },
                    { "data": "LokasiSer", title: "Lokasi Akhir" },
                    { "data": "QtyIn", title: "QtyIn" },
                    { "data": "QtyOut", title: "QtyOut" },
                    {
                        "render": function (data, type, row, meta) {
                            var aksi = "<button class = 'btn btn-danger' type='button' style='margin-right:5px;' onclick='CancelSerah(" + row.ID + ")'><i class='fa fa-times'></i> Cancel </button>";
                            return aksi;
                        }
                    }
                ]
            });
        },
        complete: function (data) {
            $("#loading").hide($.unblockUI());
        }
    });
}


function CancelSerah(id) {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/CancelSerah",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: id }),
        dataType: "json",
        async: false,
        success: function (data) {
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Cancel Serah Berhasil',
                theme: 'modern',
                type: 'green'
            });
        }
    });
    RequestListJemur(0, 4);
    RequestListJemur(0, 9);
    RequestListSerah();
}

function FillData(id, partno, sisa, formula, oven, destid, itemid) {
    idglobal = id;
    dest = destid;
    partnoawal = partno;
    ItemIDDest = itemid;
    if (oven == null) {
        $("#oven8").val("");
        $("#oven4").val("");
    } else {
        $("#oven4").val(oven);
        $("#oven8").val(oven);
    }

    totalplr = 0;
    $('.deleteall').remove();
    row = "";
    plrnpartno = partno.trim();
    var part = partno.trim();
    var form = formula.trim();
    RequestJenis();
    totalserah = sisa;
    totalsisa = sisa;
    $("#8serah").val(sisa);
    $("#4serah").val(sisa);
    var kode = partno.substring(0, 3);
    var A = partno.substring(4, 7);
    var T = partno.substring(6, 9);
    var B = partno.substring(9, 13);
    var C = partno.substring(13, 17);
    var panjang = parseInt(C);
    var tebal = parseInt(T) / 10;
    var lebar = parseInt(B);
    var kodeKW = $("#lstplnkjenis option:selected").text();
    if (kodeKW == "") {
        kodeKW = kode
    }
    $("#partnoplrnsp").val(partno.trim());
    $("#partnoplrnloksp").val("P99");
    $("#partnoplrnqtysp").val(0);

    $("#partnoplrlp").val(partno.trim());
    $("#partnoplrloklp").val("P99");
    $("#partnoplrqtylp").val(0);

    $("#partno4plrn").val(partno.trim());
    $("#partno4lokplrn").val("P99");
    $("#partno4qtyplrn").val(0);
    switch (form.length) {
        case 3:
            //8 dan 9mm
            $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
            $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

            $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
            $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

            //4mm
            $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
            $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
            $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
            $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
            break;
        case 4:
            if (lebar == 1230) {

                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (lebar == 1200) {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (panjang > 2460) {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (partno.substring(4, 5) == "1") {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                if (lebar == 1220 && panjang == 2440) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    if (kode != "PNK") {
                        $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    } else {
                        $("#partnosplp").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    }
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    if (kode != "PNK") {
                        $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    } else {
                        $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    }
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1230 && panjang == 2440) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1230 && panjang == 3600) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1000 && panjang == 2000) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4lok2bp").val("E99");
                    break;
                }
            }
            else {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4bp").val(kodeKW + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            break;
        case 5:
            if (lebar == 1230) {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (lebar == 1200) {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (panjang > 2460) {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
            if (partno.substring(4, 5) == "1") {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                if (lebar == 1220 && panjang == 2440) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    if (kode != "PNK") {
                        $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    } else {
                        $("#partnosplp").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    }
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    if (kode != "PNK") {
                        $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    } else {
                        $("#partno4kw").val(kode + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    }
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1230 && panjang == 2440) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1230 && panjang == 3600) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 10).toString() + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    break;
                }
                if (lebar == 1000 && panjang == 2000) {
                    $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                    $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                    $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17) + "SE");
                    $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                    $("#partno4lok2bp").val("E99");
                    break;
                }
                break;
            }
            else {
                $("#partnospok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnosplp").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospbp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partnospunf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnospsmp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));

                $("#partnolpok").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partnosmplp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));


                $("#partno4ok").val(kode + "-3-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4kw").val(kodeKW + "-M-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4bp").val(kode + "-P-" + partno.substring(6, 9) + (parseInt(partno.substring(9, 13)) - 20).toString() + (parseInt(partno.substring(13, 17)) - 20).toString() + "SE");
                $("#partno4unf").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4unf2").val(kode + "-P-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                $("#partno4smp").val(kode + "-1-" + partno.substring(6, 9) + partno.substring(9, 13) + partno.substring(13, 17));
                break;
            }
    }
    $("#partnosplokok").val("H99");
    $("#partnosplokbp").val("B99");
    $("#partnosploklp").val("H99");
    $("#partnosplokunf").val("C99"); 
    $("#partnosploksmp").val("Q99");

    $("#partnolplokok").val("I99");
    $("#partnosmploklp").val("Q99");

    $("#partnolok4ok").val("H99");
    $("#partno4lokkw").val("H99");
    $("#partno4lokbp").val("B99");
    $("#partno4lokunf").val("C99");
    $("#partno4loksmp").val("Q99");

    $("#partnospqtyok").val(sisa);
    $("#partnospqtybp").val(0);
    $("#partnospqtylp").val(0);
    $("#partnospqtyunf").val(0);
    $("#partnospqtysmp").val(0);

    $("#partnolpqtyok").val(0);
    $("#partnosmpqtylp").val(0);

    $("#partnoqty4ok").val(sisa);
    $("#partno4qtybp").val(0);
    $("#partno4qtykw").val(0);
    $("#partno4qtyunf").val(0);
    $("#partno4qtysmp").val(0);

    if (unitkerja == 1) {
        $("#partno4lok2bp").val("FN02");
        $("#partno4lokunf2").val("FN01");
    } else {
        $("#partno4lok2bp").val("D99");
        $("#partno4lokunf2").val("D99");
    }

    if (mm == 4) {
        if (unitkerja != 1) {
            $("#partno4lok2bp").val("A99");
            $("#partno4lokunf2").val("E14");
            if (lebar == 1000 && panjang == 2000)
                $("#partno4lok2bp").val("E99");
        }
    }
}


$("#lstplnkjenis").change(function () {
    partnolp = $("#partnosplp").val();
    $("#partnosplp").val(this.value + "-M-" + partnolp.substring(6));
});

$("#cblistplank").change(function () {
    //$("#partnolpok").val(this.value);
    var lebar = $("#cblistplank option:selected").text().substring(0, 4);
    var panjang = $("#cblistplank option:selected").text().substring(7); 

    var lebarlp = $("#partnolpok").val().substring(9, 13);
    var panjanglp = $("#partnolpok").val().substring(13, 17);

    if (parseInt(lebarlp) < parseInt(lebar) && parseInt(panjanglp) < parseInt(panjang)) {
        $("#partnolpok").val(partnoawal);
    } else {
        $("#partnolpok").val($("#partnolpok").val().substring(0, 9) + lebar + panjang);
    }
});


$("#lstpljenis4").change(function () {
    partnolp = $("#partno4kw").val();
    $("#partno4kw").val(this.value + "-M-" + partnolp.substring(6));
});

$("#oven8").change(function () {
    var oven = this.value;
    $.ajax({
        url: "SerahTerimaPotongan.aspx/UpdateOven",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ID: idglobal, oven: oven  }),
        dataType: "json",
        async: false,
        success: function (data) {
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Update Oven Berhasil',
                theme: 'modern',
                type: 'green'
            });
        }
    });
    RequestListJemur(1, 9);
});

$('#partnospqtybp').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();

    $("#partnospqtyok").val(totalserah - qtylist - qtybp - qtysmplist - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partnospqtylp').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();

    $("#partnospqtyok").val(totalserah - qtylist - qtybp - qtysmplist - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partnospqtyunf').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();


    $("#partnospqtyok").val(totalserah - qtybp - qtylist - qtysmplist - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partnospqtysmp').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();


    $("#partnospqtyok").val(totalserah - qtybp - qtylist - qtysmplist - qtylp - qtyunf - qtysmp - totalplr);
});


$('#partnolpqtyok').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();

    $("#partnospqtyok").val(totalserah - qtylist - qtysmplist - qtybp - qtylp - qtyunf - qtysmp - totalplr);
});


$('#partnosmpqtylp').on('input', function (e) {
    var qtyok = $("#partnospqtyok").val();
    var qtybp = $("#partnospqtybp").val();
    var qtylp = $("#partnospqtylp").val();
    var qtyunf = $("#partnospqtyunf").val();
    var qtysmp = $("#partnospqtysmp").val();
    var qtylist = $("#partnolpqtyok").val();
    var qtysmplist = $("#partnosmpqtylp").val();

    $("#partnospqtyok").val(totalserah - qtylist - qtysmplist- qtybp - qtylp - qtyunf - qtysmp - totalplr);
});



$('#partno4qtybp').on('input', function (e) {
    var qtyok = $("#partnoqty4ok").val();
    var qtybp = $("#partno4qtybp").val();
    var qtylp = $("#partno4qtykw").val();
    var qtyunf = $("#partno4qtyunf").val();
    var qtysmp = $("#partno4qtysmp").val();

    $("#partnoqty4ok").val(totalserah - qtybp - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partno4qtykw').on('input', function (e) {
    var qtyok = $("#partnoqty4ok").val();
    var qtybp = $("#partno4qtybp").val();
    var qtylp = $("#partno4qtykw").val();
    var qtyunf = $("#partno4qtyunf").val();
    var qtysmp = $("#partno4qtysmp").val();

    $("#partnoqty4ok").val(totalserah - qtybp - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partno4qtyunf').on('input', function (e) {
    var qtyok = $("#partnoqty4ok").val();
    var qtybp = $("#partno4qtybp").val();
    var qtylp = $("#partno4qtykw").val();
    var qtyunf = $("#partno4qtyunf").val();
    var qtysmp = $("#partno4qtysmp").val();

    $("#partnoqty4ok").val(totalserah - qtybp - qtylp - qtyunf - qtysmp - totalplr);
});

$('#partno4qtysmp').on('input', function (e) {
    var qtyok = $("#partnoqty4ok").val();
    var qtybp = $("#partno4qtybp").val();
    var qtylp = $("#partno4qtykw").val();
    var qtyunf = $("#partno4qtyunf").val();
    var qtysmp = $("#partno4qtysmp").val();

    $("#partnoqty4ok").val(totalserah - qtybp - qtylp - qtyunf - qtysmp - totalplr);
});

$('#8serah').on('input', function (e) {
    if ($("#8serah").val() > totalsisa) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Jumlah Serah Lebih Besar Dari Stok',
            theme: 'modern',
            type: 'red'
        });
        $("#8serah").val(totalsisa)
        $("#partnospqtyok").val(totalsisa);
        $("#partnospqtybp").val(0);
        $("#partnospqtylp").val(0);
        $("#partnospqtyunf").val(0);
        $("#partnospqtysmp").val(0);
    } else {
        $("#partnospqtyok").val($("#8serah").val());
        $("#partnospqtybp").val(0);
        $("#partnospqtylp").val(0);
        $("#partnospqtyunf").val(0);
        $("#partnospqtysmp").val(0);
        totalserah = $("#8serah").val();
    }
});

$('#4serah').on('input', function (e) {
    if ($("#4serah").val() > totalsisa) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Jumlah Serah Lebih Besar Dari Stok',
            theme: 'modern',
            type: 'red'
        });
        $("#4serah").val(totalsisa)
        $("#partnoqty4ok").val(totalsisa);
    } else {
        $("#partnoqty4ok").val($("#4serah").val());
        totalserah = $("#4serah").val();
    }
});

$('#89').click(function () {
    mm = 89;
    if ($("#semua").prop('checked') == true) {
        RequestListJemur(1,9);
    } else {
        RequestListJemur(0,9);
    }
});

$('#4').click(function () {
    mm = 4;
    if ($("#semua").prop('checked') == true) {
        RequestListJemur(1,4);
    } else {
        RequestListJemur(0,4);
    }
});

$('#transfer8mm').click(function () {

    if ($("#oven8").val() == 0 || $("#oven8").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Oven Belum Dipilih',
            theme: 'modern',
            type: 'red'
        });
        return false;
    };

    if ($('#partnoplrnqtysp').val() > 0) {
        loadValues();
    }
    if ($('#partnoplrqtylp').val() > 0) {
        loadValues8();
    }

    if ($("#partnospqtyok").val() == 0) {
        $("#partnospqtyok").val(0)
    };
    if ($("#partnospqtybp").val() == 0) {
        $("#partnospqtybp").val(0)
    };
    if ($("#partnospqtylp").val() == 0) {
        $("#partnospqtylp").val(0)
    };
    if ($("#partnospqtyunf").val() == 0) {
        $("#partnospqtyunf").val(0)
    };
    if ($("#partnospqtysmp").val() == 0) {
        $("#partnospqtysmp").val(0)
    };
    if ($("#partnolpqtyok").val() == 0) {
        $("#partnolpqtyok").val(0)
    };
    if ($("#partnosmpqtylp").val() == 0) {
        $("#partnosmpqtylp").val(0)
    };
    var serah = {
        tambah: []
    };

    //proses itemid, createdby dan lokid dilakukan dibackend
    if ($("#partnospqtyok").val() != 0 && $("#partno4ok").val() != null) {
        tambah = {
            PartnoSer: $("#partnospok").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtyok").val(),
            LokasiSer: $("#partnosplokok").val(),
            LokasiID: 0,
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text()
        };
        serah.tambah.push(tambah);
    }

    if ($("#partnospqtybp").val() != 0 && $("#partnospbp").val() != null && $('#rev1').is(':checked')) {
        tambah = {
            PartnoSer: $("#partnospbp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtybp").val(),
            LokasiSer: $("#partnosplokbp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text(),
            Parameter: 1
        };
        serah.tambah.push(tambah);
    }


    if ($("#partnospqtybp").val() != 0 && $("#partnospbp").val() != null && $('#b99').is(':checked')) {
        tambah = {
            PartnoSer: $("#partnospbp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtybp").val(),
            LokasiSer: $("#partnosplokbp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text(),
            Parameter: 2
        };
        serah.tambah.push(tambah);
    }

    if ($("#partnospqtylp").val() != 0 && $("#partnosplp").val() != null) {
        tambah = {
            PartnoSer: $("#partnosplp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtylp").val(),
            LokasiSer: $("#partnosploklp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text()
        };
        serah.tambah.push(tambah);
    }

    if ($("#partnospqtyunf").val() != 0 && $("#partnospunf").val() != null) {
        tambah = {
            PartnoSer: $("#partnospunf").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtyunf").val(),
            LokasiSer: $("#partnosplokunf").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text()
        };
        serah.tambah.push(tambah);
    }


    if ($("#partnospqtysmp").val() != 0 && $("#partnospsmp").val() != null) {
        tambah = {
            PartnoSer: $("#partnospsmp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnospqtysmp").val(),
            LokasiSer: $("#partnosploksmp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text()
        };
        serah.tambah.push(tambah);
    }


    if ($("#partnosmpqtylp").val() != 0 && $("#partnosmplp").val() != null) {
        tambah = {
            PartnoSer: $("#partnosmplp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnosmpqtylp").val(),
            LokasiSer: $("#partnosmploklp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text()
        };
        serah.tambah.push(tambah);
    }

    if ($("#partnolpqtyok").val() != 0 && $("#partnolpok").val() != null) {
        tambah = {
            PartnoSer: $("#partnolpok").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnolpqtyok").val(),
            LokasiSer: $("#partnolplokok").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven8 option:selected").text(),
            Parameter:3
        };
        serah.tambah.push(tambah);
    }

    if (pelarian8.lari.length > 0) {
        for (var i = 0; i < pelarian8.lari.length; i++) {
            tambah = {
                PartnoSer: pelarian8.lari[i].PartnoSer,
                PartnoDest: pelarian8.lari[i].PartnoDest,
                QtyIn: pelarian8.lari[i].QtyIn,
                LokasiSer: pelarian8.lari[i].LokasiSer,
                LokasiID: '0',
                DestID: pelarian8.lari[i].DestID,
                ItemIDDest: pelarian8.lari[i].ItemIDDest,
                HPP: 0,
                JemurID: idglobal,
                TglSerah: pelarian8.lari[i].TglSerah,
                SFrom: pelarian8.lari[i].SFrom,
                Oven: pelarian8.lari[i].Oven
            };
            serah.tambah.push(tambah);
        }
    };


    if (pelarianlp.lari.length > 0) {
        for (var i = 0; i < pelarianlp.lari.length; i++) {
            tambah = {
                PartnoSer: pelarianlp.lari[i].PartnoSer,
                PartnoDest: pelarianlp.lari[i].PartnoDest,
                QtyIn: pelarianlp.lari[i].QtyIn,
                LokasiSer: pelarianlp.lari[i].LokasiSer,
                LokasiID: '0',
                DestID: pelarianlp.lari[i].DestID,
                ItemIDDest: pelarianlp.lari[i].ItemIDDest,
                HPP: 0,
                JemurID: idglobal,
                TglSerah: pelarianlp.lari[i].TglSerah,
                SFrom: pelarianlp.lari[i].SFrom,
                Oven: pelarianlp.lari[i].Oven
            };
            serah.tambah.push(tambah);
        }
    };
    SimpanSerah8(serah);
    ClaerForm4();
    ClaerForm8();
    if ($('#semua').is(':checked')) {
        $('#semua').prop('checked', false);
    };
    RequestListJemur(0, 9);
    RequestListSerah();
});

$('#transfer4mm').click(function () {

    if ($("#oven4").val() == 0 || $("#oven4").val() == null) {
        $.alert({
            icon: 'fa fa-times',
            title: 'Warning!',
            content: 'Oven Belum Dipilih',
            theme: 'modern',
            type: 'red'
        });
        return false;
    };

    if ($('#partno4qtyplrn').val() > 0) {
        loadValues4();
    }

    if ($("#partnoqty4ok").val() == 0) {
        $("#partnoqty4ok").val(0)
    };
    if ($("#partno4qtykw").val() == 0) {
        $("#partno4qtykw").val(0)
    };
    if ($("#partno4qtybp").val() == 0) {
        $("#partno4qtybp").val(0)
    };
    if ($("#partno4qtyunf").val() == 0) {
        $("#partno4qtyunf").val(0)
    };
    if ($("#partno4qtysmp").val() == 0) {
        $("#partno4qtysmp").val(0)
    };
    
    var serah = {
        tambah: []
    };

    //proses itemid, createdby dan lokid dilakukan dibackend
    if ($("#partnoqty4ok").val() != 0 && $("#partno4ok").val() != null) {
        tambah = {
            PartnoSer: $("#partno4ok").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partnoqty4ok").val(),
            LokasiSer: $("#partnolok4ok").val(),
            LokasiID:0,
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven4 option:selected").text()
        };
        serah.tambah.push(tambah);

    } 

    if ($("#partno4qtykw").val() != 0 && $("#partno4kw").val() != null) {
        tambah = {
            PartnoSer: $("#partno4kw").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partno4qtykw").val(),
            LokasiSer: $("#partno4lokkw").val(),
            LokasiID:'0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven4 option:selected").text()
        };
        serah.tambah.push(tambah);

    } 

    if ($("#partno4qtybp").val() != 0 && $("#partno4bp").val() != null) {
        tambah = {
            PartnoSer: $("#partno4bp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partno4qtybp").val(),
            LokasiSer: $("#partno4lok2bp").val(),
            LokasiID: $("#partno4lokbp").val(), 
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven4 option:selected").text()
        };
        serah.tambah.push(tambah);
    } 

    if ($("#partno4qtyunf").val() != 0 && $("#partno4unf").val() != null) {
        tambah = {
            PartnoSer: $("#partno4unf").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partno4qtyunf").val(),
            LokasiSer: $("#partno4lokunf").val(),
            LokasiID: $("#partno4lokunf").val(),
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven4 option:selected").text()
        };
        serah.tambah.push(tambah);
    }

    if ($("#partno4qtysmp").val() != 0 && $("#partno4smp").val() != null) {
        tambah = {
            PartnoSer: $("#partno4smp").val(),
            PartnoDest: partnoawal,
            QtyIn: $("#partno4qtysmp").val(),
            LokasiSer: $("#partno4loksmp").val(),
            LokasiID: '0',
            DestID: dest,
            ItemIDDest: ItemIDDest,
            HPP: 0,
            JemurID: idglobal,
            TglSerah: $("#tglpotong").val(),
            SFrom: "jemur",
            Oven: $("#oven4 option:selected").text()
        };
        serah.tambah.push(tambah);
    }

    if (pelarian.lari.length > 0) {
        for (var i = 0; i < pelarian.lari.length; i++) {
            if (pelarian.lari[i].QtyIn > 0) {
                tambah = {
                    PartnoSer: pelarian.lari[i].PartnoSer,
                    PartnoDest: pelarian.lari[i].PartnoDest,
                    QtyIn: pelarian.lari[i].QtyIn,
                    LokasiSer: pelarian.lari[i].LokasiSer,
                    LokasiID: '0',
                    DestID: pelarian.lari[i].DestID,
                    ItemIDDest: pelarian.lari[i].ItemIDDest,
                    HPP: 0,
                    JemurID: idglobal,
                    TglSerah: pelarian.lari[i].TglSerah,
                    SFrom: pelarian.lari[i].SFrom,
                    Oven: pelarian.lari[i].Oven
                };
            }
            serah.tambah.push(tambah);
        }
    };
    SimpanSerah(serah);
    ClaerForm4();
    ClaerForm8();
    if ($('#semua').is(':checked')) {
        $('#semua').prop('checked', false);
    };
    RequestListJemur(0, 4);
    RequestListSerah();
});

function SimpanSerah(serah) {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/Insert4mm",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ serah: serah }),
        success: function (data) {
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Data Tersimpan',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}


function SimpanSerah8(serah) {
    $.ajax({
        url: "SerahTerimaPotongan.aspx/Insert8mm",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ serah: serah }),
        success: function (data) {
            $.alert({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Data Tersimpan',
                theme: 'modern',
                type: 'green'
            });
        }
    });
}



function ClaerForm8() {
    pelarian8 = {
        lari: []
    };
    pelarianlp = {
        lari: []
    };
    row = "";
    $("#8serah").val('');
    $("#oven8").val(0);

    //Partno OK
    $("#partnospok").val('');
    $("#partnosplokok").val('');
    $("#partnospqtyok").val('');

    $("#partnolpok").val('');
    $("#partnolplokok").val('');
    $("#partnolpqtyok").val('');

    //Partno BP
    $("#partnospbp").val('');
    $("#partnosplokbp").val('');
    $("#partnospqtybp").val('');

    //Partno LP
    $("#partnosplp").val('');
    $("#partnosploklp").val('');
    $("#partnospqtylp").val('');

    //Partno Unfinish
    $("#partnospunf").val('');
    $("#partnosplokunf").val('');
    $("#partnospqtyunf").val('');


    //Partno Sample
    $("#partnospsmp").val('');
    $("#partnosploksmp").val('');
    $("#partnospqtysmp").val('');

    $("#partnosmplp").val('');
    $("#partnosmploklp").val('');
    $("#partnosmpqtylp").val('');

    //Partno Pelarian
    $("#partnoplrnsp").val('')
    $("#partnoplrnloksp").val('')
    $("#partnoplrnqtysp").val('')

    $("#partnoplrlp").val('')
    $("#partnoplrloklp").val('')
    $("#partnoplrqtylp").val('')
}


function ClaerForm4() {
    pelarian = { lari: [] };
    row = "";
    $("#4serah").val('');
    $("#oven4").val(0);

    //Partno OK
    $("#partno4ok").val('');
    $("#partnolok4ok").val('');
    $("#partnoqty4ok").val('');

    //Partno KW
    $("#partno4kw").val('');
    $("#partno4lokkw").val('');
    $("#partno4qtykw").val('');

    //Partno BP
    $("#partno4bp").val('');
    $("#partno4lokbp").val('');
    $("#partno4qtybp").val('');
    $("#partno4lok2bp").val('');

    //Partno Unfinish
    $("#partno4unf").val('');
    $("#partno4lokunf").val('');
    $("#partno4qtyunf").val('');
    $("#partno4unf2").val('');
    $("#partno4lokunf2").val('');

    //Partno Sample
    $("#partno4smp").val('');
    $("#partno4loksmp").val('');
    $("#partno4qtysmp").val('');

    //Partno Pelarian
    $("#partno4plrn").val('');
    $("#partno4lokplrn").val('');
    $("#partno4qtyplrn").val('');
}
