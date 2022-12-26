var index = 0;
var item;
var list;
var isPertamam = true;
var levelapp;
var CekEstimasiMaterial;
var maxbiaya;
var statuscancel;
var userapv;
var userdept;
var biaya;

$("#finished").datepicker({ dateFormat: 'dd-mm-yy' });

$('#biaya').on('click', function () {
    $('#biaya').val('');
});

$(document).ready(function () {
    $("#notapprove").hide();
    $("#reschedule").hide();
    $("#barang").hide();
    $("#notapproveest").hide();
    
    $("#approve").attr("disabled", false);
    RequestProject();
});

function RequestProject() {
    $.ajax({
        url: "ApprovalProject.aspx/LoadOpenImprovement",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            list = data.d[0].ListProject
            item = data.d[0].ListProject[index];

            levelapp = data.d[0].LevelApp;
            maxbiaya = data.d[0].MaxBiaya;

            //Hidden/Unhide Button
            if (levelapp < 0) // Manager Dept Pemohon
            {
                $("#finished").attr("disabled", true);
                $("#biaya").attr("disabled", true);
                $("#notapprove").show();
            }
            else if (levelapp == 3) //PM
            {
                $("#finished").attr("disabled", true);
                $("#biaya").attr("disabled", true);
                $("#notapprove").show();
            }
            else if (levelapp == 1)//Head Enginering
            {
                $("#finished").attr("disabled", false);
                $("#biaya").attr("disabled", false);
            }
            else if (levelapp == 0)//Head GA
            {
                $("#finished").attr("disabled", false);
                $("#biaya").attr("disabled", false);
            }
            else if (levelapp == 2)//Mgr MTN
            {
                $("#finished").attr("disabled", false);
                $("#biaya").attr("disabled", false);
            }
            else if (levelapp == 5)//Mgr GA
            {
                $("#finished").attr("disabled", false);
                $("#biaya").attr("disabled", false);
            }
            else if (levelapp == 4)//Direksi
            {
                $("#finished").attr("disabled", true);
                $("#biaya").attr("disabled", true);
            }
            else if (levelapp < 0 && userapv > 0 && userdept == 5 || levelapp < 0 && userapv > 0 && userdept == 18 || levelapp < 0 && userapv > 0 && userdept == 4) // Head MTN
            {
                $("#finished").attr("disabled", true);
                $("#biaya").attr("disabled", true);
            }

            displayItem(item);
        }
    });
}

function RequestMaterial(namaproject) {
    $.ajax({
        url: "ApprovalProject.aspx/LoadMaterial",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ improve: namaproject }),
        dataType: "json",
        success: function (data) {
            if (!isPertamam) {
                $("#tableListmaterial").DataTable().destroy();
                $('#tableListmaterial').empty();
            } else {
                isPertamam = false;
            }
            datatable = $.parseJSON(data.d);
            oTblReport = $("#tableListmaterial").DataTable({
                "data": datatable,
                "pageLength": 10,
                "fixedHeader": true,
                "columns": [

                    {
                        'className': 'details-control',
                        'orderable': false,
                        'data': null,
                        'defaultContent': '',
                        'render': function (data, type, full, meta) {
                            var aksi = "<i class='fa fa-check'></i>";
                            return aksi;
                        },
                        'createdCell': function (td, cellData, rowData, row, col) {
                            $(td).attr('id', 'td_details' + row);
                        }
                    },
                    { "data": "ItemCode", title: "Item Code" },
                    { "data": "ItemName", title: "Item Name" },
                    { "data": "UomCode", title: "Unit" },
                    { "data": "Jumlah", title: "Quantity" },
                    { "data": "Harga", title: "Harga" },
                    { "data": "SchedulePakai", title: "Schedule Pakai" }
                ]
            });
        }
    });
}

function CekMaterial(namaproject) {
    $.ajax({
        url: "ApprovalProject.aspx/CekEstimasiMaterial",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ improve: namaproject }),
        dataType: "json",
        async: false,
        success: function (data) {
            CekEstimasiMaterial = data.d
        }
    });
}

prev.addEventListener('click', function () {
    displayItem(list[--index]);
});

next.addEventListener('click', function () {
    displayItem(list[++index]);
});

function displayItem(item) {
    //format currency
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'IDR',
        minimumFractionDigits: 2
    });

    CekMaterial(item.Nomor);

    //Jika Biaya Masih Kosong
    if (item.Biaya == 0) {
        $("#id").val(item.ID);
        $("#noimprove").val(item.Nomor);
        $("#pemohon").val(item.DeptName);

        //Approval Manager Dept. Pemohon
        if (levelapp < 0 || levelapp == 2 || levelapp == 5) {
            if (item.ToDeptID == 19 || item.ToDeptID == 0) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7 && item.Approval == 1 && item.Status == 0 && item.RowStatus == 0) {
                $("#penerima").val('HRD GA');
                $("#info").show();
                $("#info").text('IMPROVMENT di tujukan ke HRD GA');
                $("#danger").hide();
            }
            else if (item.ToDeptID == 19 && item.Approval == 1 && item.Status == 0 && mitem.RowStatus == 0) {
                $("#penerima").val('MAINTENANCE');
                $("#info").show();
                $("#info").text('IMPROVMENT di tujukan ke MAINTENANCE');
                $("#danger").hide();
            }

            if (item.Biaya > 0 && item.Target == "11/11/2000 00:00:00") {
                $("#reschedule").show();
                $("#notapprove").hide();
            }
            else if (item.Biaya == 0 && item.Target == "11/11/2000 00:00:00") {
                $("#reschedule").show();
                $("#notapprove").hide();
            }

            if (item.Approval == 1 && item.Status == 0 && item.RowStatus == 0) {
                $("#status").val('Open - Dibuat Oleh : ' + item.NamaHead);
            }
        }

        //Approval Head HRDGA & Engineering
        else if (levelapp == 1 || levelapp == 0) {
            $("#barang").show();

            if (item.ToDeptID == 19 || item.ToDeptID == 0) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7) {
                $("#penerima").val('HRD GA');
            }

            if (item.Approval == 1 && item.Status == 0 && item.RowStatus == 1 && item.VerPM == 1 && item.VerDate == 0) {
                $("#status").val('Approved PM Pertama - Oleh : ' + item.LastModifiedBy);
            }
            else if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerDate == -2) {
                $("#status").val('DiTolak - Oleh : ' + item.LastModifiedBy);

            }
        }

        //PM Filter I Approval
        else if (levelapp == 3) {
            $("#notapproveest").hide();
            $("#notapprove").show();

            $("#status").val(item.Approval);

            if (item.ToDeptID == 19 || item.ToDeptID == 0) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7) {
                $("#penerima").val('HRD GA');
            }
        }

        $("#improve").val(item.ProjectName);
        $("#qty").val(item.Quantity);
        $("#sasaran").val(item.Sasaran);
        $("#target").val(item.DetailSasaran);
        $("#dibuat").val(item.NamaHead);
        $("#tanggal").val(item.Tanggal);
        $("#satuan").val(item.UomCode);
        $("#group").val(item.NamaGroup);
        $("#biaya").val(formatter.format(item.Biaya));
        biaya = item.Biaya;

        $("#hidestatus").val(item.Status);
        $("#hiderowstatus").val(item.RowStatus);
        $("#hideapv").val(item.Approval);
        $("#hidedeptid").val(item.DeptID);
        $('#hideapvpm').val(item.VerPM);

        if (item.ProdLine == 99) {
            $("#area").val('General');
        }
        else if (item.ProdLine == 97) {
            $("#area").val('Material Preparation');
        }
        else if (item.ProdLine == 98) {
            $("#area").val('WTP');
        }
        else if (item.ProdLine == 1) {
            $("#area").val('Zona 1 - ' + item.Zona);
        }
        else if (item.ProdLine == 2) {
            $("#area").val('Zona 2 - ' + item.Zona);
        }
        else if (item.ProdLine == 3) {
            $("#area").val('Zona 3 - ' + item.Zona);
        }
        else if (item.ProdLine == 4) {
            $("#area").val('Zona 4');
        }

        if (item.Target == "11/11/2000 00:00:00") {
            $("#finished").val('Target belum ditentukan');
        }
        else if (item.Target != "11/11/2000 00:00:00" || item.Target == "11/11/2000 00.00.00") {
            $("#finished").val(item.Target);
        }
    }

    //Biaya Sudah Di Isi
    else if (item.Biaya > 0) {
        $("#noimprove").val(item.Nomor);
        $("#pemohon").val(item.DeptName);
        $("#id").val(item.ID);
        //Manager Dept. Pemohon
        if (levelapp < 0) {
            if (item.ToDeptID == 19) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7 && item.Approval == 1 && item.Status == 0 && item.RowStatus == 0) {
                $("#penerima").val('HRD GA');
            }
            else if (item.ToDeptID == 19 && item.Approval == 1 && item.Status == 0 && mitem.RowStatus == 0) {
                $("#penerima").val('MAINTENANCE');
            }

            if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerDate == 0) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#info").text('Target Finished Date Sudah Ditentukan');
            }

            if (item.Biaya > 0 && item.Target != "11/11/2000 0:00:00") {
                $("#notapprove").hide();
                $("#reschedule").show();
            }
        }

        //Head Engineering
        else if (levelapp == 1) {
            $("#barang").show();

            if (item.ToDeptID == 19 || item.ToDeptID == 0) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7) {
                $("#penerima").val('HRD GA');
            }

            if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == -2) {
                $("#status").val('Not Approved - Oleh : ' + item.LastModifiedBy);
                $("#danger").show();
                $("#danger").text('Target Finished Date Harap di Rubah !!');
            }
            else if (item.Approval == 2 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == -1 && item.VerUser == 1) {
                $("#status").val('Not Approved - Oleh : ' + item.LastModifiedBy);
                $("#danger").text('Estimasi Material dan Biaya harus di Rubah !!');
            }
            else if (item.Approval == 2 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == -2 && item.VerUser == 1) {
                $("#status").val('Not Approved - Oleh : ' + item.LastModifiedBy);
                $("#danger").show();
                $("#danger").text('Target dan tujuan tidak tepat !!');
                $("#notapprove").show();
            }
        }

        //Manager MTN
        else if (levelapp == 2 || levelapp == 5) {
            if (item.ToDeptID == 19 || item.ToDeptID == 0) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7) {
                $("#penerima").val('HRD GA');
            }

            if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == 1 && CekEstimasiMaterial == 0) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").show();
                $("#info").text('Target Finished Date Sudah Disetujui !!');
                $("#danger").text('Head Enginering belum membuat Estimasi Material !!');
                $("#approve").attr("disabled", true);
                //$("#approve").hide();
            }
            else if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == 1 && CekEstimasiMaterial > 0) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").hide();
                $("#info").text('Target Finished Date Sudah Disetujui !!');
                $("#approve").attr("disabled", false);
                //$("#approve").show();

            }
            else if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == 0 && CekEstimasiMaterial > 0) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").hide();
                $("#info").text('Target Finished Date blm Disetujui Mgr Dept. Peminta !!');
                $("#approve").attr("disabled", false);
                //$("#approve").hide();
            }
            else if (item.Approval == 1 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == 0 && CekEstimasiMaterial == 0) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").show();
                $("#info").text('Target Finished Date Belum di Approved oleh Manager Peminta!!');
                $("#danger").text('Head Enginering belum membuat Estimasi Material !!');
                $("#approve").attr("disabled", true);
                //$("#approve").hide();
            }
            else {
                $("#info").hide();
                $("#danger").hide();
            }

            if (item.VerDate == 0 && item.ProjectID != 0 && item.DeptID != 4 && item.DeptID != 5 && item.DeptID != 18
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 4 && item.Approval == 1 && item.Status == 1 && item.RowStatus == 1
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 5 && item.Approval == 1 && item.Status == 1 && item.RowStatus == 1
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 18 && item.Approval == 1 && item.Status == 1 && item.RowStatus == 1
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID != 4 && item.DeptID != 5 && item.DeptID != 18
                || item.VerDate == 1 && item.ProjectID == 0) {
                $("#approve").attr("disabled", true);
            }
            else if (item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 4 && item.Approval == 1 && item.Status == 0 && item.RowStatus == 0 && CekEstimasiMaterial > 0
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 5 && item.Approval == 1 && item.Status == 0 && item.RowStatus == 0 && CekEstimasiMaterial > 0
                || item.VerDate == 0 && item.ProjectID == 0 && item.DeptID == 18 && item.Approval == 1 && item.Status == 0 && item.RowStatus == 0 && CekEstimasiMaterial > 0
                || (item.VerDate == 0 && item.ProjectID != 0 && item.DeptID == 18 || item.DeptID == 5 || item.DeptID == 4) && CekEstimasiMaterial > 0
                || item.VerDate == 0 && item.ProjectID != 0 && item.DeptID == 4 && CekEstimasiMaterial > 0
                || item.VerDate == 0 && item.ProjectID != 0 && item.DeptID == 5 && CekEstimasiMaterial > 0
                || item.VerDate == 1 && item.ProjectID != 0 && CekEstimasiMaterial > 0) {
                $("#approve").attr("disabled", false);
            }
        }

        //PM
        else if (levelapp == 3) {
            $("#notapprove").hide();
            $("#notapproveest").show();
            if (item.Approval == 2 && item.Status == 1 && item.RowStatus == 1 && item.VerPM == 1 && item.VerUser == 1) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").hide();
                $("#danger").hide();
            }

            else if (item.Approval == 4 && item.Status == 2 && item.RowStatus == 2 && item.VerPM == 2 && item.VerUser == 1
                || item.Approval == 3 && item.Status == 2 && item.RowStatus == 2 && item.VerPM == 2 && item.VerUser == 1) {
                $("#status").val('Approved Tahap 2 - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").hide();
                $("#info").text('Pekerjaan sudah di Serah Terimakan Ke Manager Peminta');
            }

            if (item.ToDeptID == 19) {
                $("#penerima").val('MAINTENANCE');
            }
            else if (item.ToDeptID == 7) {
                $("#penerima").val('HRD GA');
            }
        }

        //Direksi
        else if (item.Biaya > maxbiaya && levelapp == 4) {
            $("#notapprove").hide();
            $("#notapproveest").show();
            if (item.Approval == 2 && item.Status == 2 && item.RowStatus == 0 && item.VerPM == 3 && item.VerUser == 1) {
                $("#status").val('Approved - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").hide();
            }

            else if (item.Approval == 4 && item.Status == 2 && item.RowStatus == 2 && item.VerPM == 2 && item.VerUser == 1
                || item.Approval == 3 && item.Status == 2 && item.RowStatus == 2 && item.VerPM == 2 && item.VerUser == 1) {
                $("#status").val('Approved Tahap 2 - Oleh : ' + item.LastModifiedBy);
                $("#info").show();
                $("#danger").hide();
                $("#info").text('Pekerjaan sudah di Serah Terimakan Ke Manager Peminta');
            }
        }

        $("#improve").val(item.ProjectName);
        $("#qty").val(item.Quantity);
        $("#sasaran").val(item.Sasaran);
        $("#target").val(item.DetailSasaran);
        $("#dibuat").val(item.NamaHead);
        $("#tanggal").val(item.Tanggal);
        $("#satuan").val(item.UomCode);
        $("#group").val(item.NamaGroup);
        $("#biaya").val(formatter.format(item.Biaya));
        biaya = item.Biaya;

        $("#hidestatus").val(item.Status);
        $("#hiderowstatus").val(item.RowStatus);
        $("#hideapv").val(item.Approval);
        $("#hidedeptid").val(item.DeptID);
        $('#hideapvpm').val(item.VerPM);

        if (item.ProdLine == 99) {
            $("#area").val('General');
        }
        else if (item.ProdLine == 97) {
            $("#area").val('Material Preparation');
        }
        else if (item.ProdLine == 98) {
            $("#area").val('WTP');
        }
        else if (item.ProdLine == 1) {
            $("#area").val('Zona 1 - ' + item.Zona);
        }
        else if (item.ProdLine == 2) {
            $("#area").val('Zona 2 - ' + item.Zona);
        }
        else if (item.ProdLine == 3) {
            $("#area").val('Zona 3 - ' + item.Zona);
        }
        else if (item.ProdLine == 4) {
            $("#area").val('Zona 4');
        }

        if (item.Target == "11/11/2000 00:00:00") {
            $("#finished").val('Target belum ditentukan');
        } else {
            $("#finished").val(item.Target);
        }
    }
    //Sudah Ada Dan Sudah Approval
    else if (item.Approval > 0 && item.Status > 0 && item.RowStatus > 0 && item.Biaya == 0) {
        $("#id").val(item.ID);
        $("#improve").val(item.ProjectName);
        $("#qty").val(item.Quantity);
        $("#sasaran").val(item.Sasaran);
        $("#target").val(item.DetailSasaran);
        $("#dibuat").val(item.NamaHead);
        $("#tanggal").val(item.Tanggal);
        $("#satuan").val(item.UomCode);
        $("#group").val(item.NamaGroup);
        $("#biaya").val(formatter.format(item.Biaya));
        biaya = item.Biaya;

        $("#hidestatus").val(item.Status);
        $("#hiderowstatus").val(item.RowStatus);
        $("#hideapv").val(item.Approval);
        $("#hidedeptid").val(item.DeptID);
        $('#hideapvpm').val(item.VerPM);

        if (item.ProdLine == 99) {
            $("#area").val('General');
        }
        else if (item.ProdLine == 97) {
            $("#area").val('Material Preparation');
        }
        else if (item.ProdLine == 98) {
            $("#area").val('WTP');
        }
        else if (item.ProdLine == 1) {
            $("#area").val('Zona 1 - ' + item.Zona);
        }
        else if (item.ProdLine == 2) {
            $("#area").val('Zona 2 - ' + item.Zona);
        }
        else if (item.ProdLine == 3) {
            $("#area").val('Zona 3 - ' + item.Zona);
        }
        else if (item.ProdLine == 4) {
            $("#area").val('Zona 4');
        }

        $("#finished").val('FromDate2');

        if (item.ToDeptID == 7) {
            $("#penerima").val('HRD GA');
        }

        if (item.Approval == 1 && item.Status == 0 && item.RowStatus == 0) {
            $("#status").val('Dibuat - Oleh : ' + item.NamaHead);
            $("#info").show();
            $("#danger").hide();
            $("#info").text('IMPROVMENT KE HRD GA');
        }
        else if (item.Biaya == 0 && item.ToDate.ToString() != "11/11/2000 0:00:00" && item.Noted1 == 1) {
            $("#info").show();
            $("#danger").hide();
            $("#info").text('Biaya Tidak Ada Karena Menggunakan Barang Bekas !!');
        }
    }

    RequestMaterial(item.Nomor);

    prev.disabled = index <= 0;
    next.disabled = index >= list.length - 1;
}

$("#approve").click(function () {
    var hideapv = $('#hideapv').val();
    var hidestatus = $('#hidestatus').val();
    var hiderowstatus = $('#hiderowstatus').val();
    var hidedeptid = $('#hidedeptid').val();
    var hideverpm = $('#hideapvpm').val();
    //var biaya = parseInt($('#biaya').val().replace("IDR", "").trim());
    var hidebiaya = $('#biaya').val().replace("IDR", "").trim();

    var rowstatus = 0;
    var approval = 0;
    var status = 0;
    var verdate = 0;
    var finishdate = 0;
    var apvdireksi = 0;
    var noted = 0;
    var flag = 0;
    var verpm = 0;

    if (hideapv != "0") {
        if (hidestatus == "0" && hiderowstatus == "0" && hideapv == "1" && (hidedeptid == "4" || hidedeptid == "5" || hidedeptid == "18" || hidedeptid == "19")) {
            rowstatus = 1;
            approval = 1;
            status = 0;
        }

        else if (hidestatus == "0" && hiderowstatus == "0" && hideapv == "2" && (hidedeptid == "4" || hidedeptid == "5" || hidedeptid == "18" || hidedeptid == "19")) {
            rowstatus = 1;
            approval = 2;
            status = 1;
        }

        // Approval Manager Dept
        else if (hidestatus == "0" && hiderowstatus == "0" && hideapv == "1") {
            rowstatus = 1;
            approval = hideapv;
        }

        // Approval Manager Dept , Deal target selesai
        else if (hidestatus == "1" && hiderowstatus == "1" && hideapv == "1") {
            if (levelapp == 1 || levelapp == 0) {
                rowstatus = 1;
                approval = 1;
                status = 1;
                verpm = hideverpm;
                verdate = 0;
                apvdireksi = 0;
            }
            else if (levelapp < 0) {
                rowstatus = 1;
                approval = 1;
                status = 1;
                verpm = hideverpm;
                verdate = 1;
                apvdireksi = 0;
            }
            else if (levelapp == 2 || levelapp == 5)

                if (hideverpm == -1) {
                    rowstatus = 2;
                    approval = 2;
                    status = 2;
                    verpm = 1;
                    verdate = 2;
                    apvdireksi = 0;
                    finishdate = $("#finished").val();
                }
                else if (hideverpm == -2) {
                    rowstatus = 2;
                    approval = 2;
                    status = 2;
                    verpm = 2;
                    verdate = 2;
                    apvdireksi = 0;
                    finishdate = $("#finished").val();
                }
                else if (hideverpm == 1) {
                    rowstatus = 1;
                    approval = 2;
                    status = 1;
                    verpm = 1;
                    verdate = 1;
                    apvdireksi = 0;
                    finishdate = $("#finished").val();
                }
                else {
                    rowstatus = 2;
                    approval = 2;
                    status = 2;
                    verpm = hideverpm;
                    verdate = 1;
                    apvdireksi = 0;
                    finishdate = $("#finished").val();
                }
        }

        else if (hidestatus == "1" && hiderowstatus == "1" && hideapv == "2" && hideverpm == "-1") {
            rowstatus = 1;
            approval = 2;
            status = 1;
            verpm = 1;
            verdate = 1;
            apvdireksi = 0;
            finishdate = $("#finished").val();
        }

        else if (hidestatus == "1" && hiderowstatus == "1" && hideapv == "2" && hideverpm == "-2") {
            rowstatus = 1;
            approval = 2;
            status = 1;
            verpm = 2;
            verdate = 1;
            apvdireksi = 0;
            finishdate = $("#finished").val();
        }

        // Apv PM Baru Filter Pertama
        else if (hidestatus == "0" && hiderowstatus == "1" && hideapv == "1" && hideverpm == "0") {
            rowstatus = 1;
            approval = 1;
            status = 0;
            verpm = 1;
            apvdireksi = 0;
        }

        // Apv PM Baru Filter Kedua ( Realese )
        else if (hidestatus == "1" && hiderowstatus == "1" && hideapv == "2" && hideverpm == "1") {
            if (biaya > maxbiaya) {
                rowstatus = 2;
                approval = 2;
                status = 2;
                verpm = 2;
                verdate = 1;
                apvdireksi = 0;
                finishdate = $("#finished").val();
                flag = 0;
            }
            else if (biaya < maxbiaya) {
                rowstatus = 2;
                approval = 2;
                status = 2;
                verpm = 2;
                verdate = 1;
                apvdireksi = 0;
                finishdate = $("#finished").val();
                flag = 1;
            }
        }

        // Apv PM Baru Filter Ketiga ( Closed )
        else if (hidestatus == "2" && hiderowstatus == "2" && hideapv == "4" && hideverpm == "2"
            || hidestatus == "2" && hiderowstatus == "2" && hideapv == "3" && hideverpm == "2") {
            if (parseInt(biaya) > maxbiaya) {
                apvdireksi = 1;
            }
            else {
                apvdireksi = 0;
            }
            rowstatus = 2;
            approval = 5;
            status = 2;
            verpm = 3;
            verdate = 1;
            finishdate = $("#finished").val();
            flag = 1;
        }

        // Apv Direksi
        else if (hidestatus == "2" && hiderowstatus == "2" && hideapv == "2" && hideverpm == "2") {
            if (parseInt(biaya) > maxbiaya) {
                rowstatus = 2;
                approval = 2;
                status = 2;
                verpm = 2;
                verdate = 1;
                finishdate = $("#finished").val();
                flag = 1;
                apvdireksi = 1;
            }
        }

        // Apv Head Eng
        else if (hidestatus == "0" && hiderowstatus == "1" && hideapv == "1" && hideverpm != "0") {
            if (hidebiaya == "0.00" && $("#finished").val() == "Target belum ditentukan" && $("#barangbekas").prop('checked') == false) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Estimasi Biaya dan Target Finish Date harus diisi !!',
                    theme: 'modern',
                    type: 'red'
                });
                return false;
            }

            else if ($("#finished").val() != "Target belum ditentukan" && hidebiaya == "0.00" && $("#barangbekas").prop('checked') == false) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Estimasi Biaya harus di isi !!',
                    theme: 'modern',
                    type: 'red'
                });
                return false;
            }
            else if ($("#finished").val() == "Target belum ditentukan" || hidebiaya == "0.00" && $("#barangbekas").prop('checked') == false) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Target Finish Date harus di isi !!',
                    theme: 'modern',
                    type: 'red'
                });
                return false;
            }
            else if ($("#finished").val() == "Target belum ditentukan" && hidebiaya == "0.00" && $("#barangbekas").prop('checked') == true) {
                $.alert({
                    icon: 'fa fa-times',
                    title: 'Warning!',
                    content: 'Target Finish Date harus di isi !!',
                    theme: 'modern',
                    type: 'red'
                });
                return false;
            }

            rowstatus = hiderowstatus;
            approval = hideapv;
            status = 1;
            verpm = hideverpm;
            verdate = 0;
            apvdireksi = 0;
            biaya = $("#biaya").val();
        }

        else if (biaya > maxbiaya) {
            rowstatus = 2;
            approval = 2;
            status = 2;
            verdate = 1;
        }

        else {
            rowstatus = 2;
            approval = 2;
            status = 2;
            apvdireksi = 0;
        }
    }

    if (approval == 1 && rowstatus == 1 && status == 1 && verpm == 1 && verdate == 0) {
        finishdate = $("#finished").val();
    }
    else if (approval == 1 && rowstatus == 1 && status == 1 && verpm == 1 && verdate == 1) {
        finishdate = $("#finished").val();
    }
    else if (approval == 2 && rowstatus == 1 && status == 1 && verpm == 1 || approval == 2 && rowstatus == 2 && status == 2 && verpm == 2 || approval == 5 && rowstatus == 2 && status == 2 && verpm == 3) {
        finishdate = $("#finished").val();
    }
    else {
        finishdate = "11/11/2000 00:00:00";
    }

    if ($("#barangbekas").prop('checked') == true) {
        noted = 1;
    }
    else if ($("#barangbekas").prop('checked') == false) {
        noted = 0;
    }

    approve = {
        ID: $("#id").val(),
        Status: status,
        Approval: approval,
        RowStatus: rowstatus,
        Biaya: biaya,
        VerDate: verdate,
        VerPM: verpm,
        Flag: flag,
        ApvDireksi: apvdireksi,
        Noted1: noted,
        FinishDate: finishdate
    };
    ApproveProject(approve);
});

$("#reschedule").click(function () {
    ReScheduleProject($("#id").val());
});

$("#notapprove").click(function () {
    CancelProject($("#id").val());
});

$("#notapproveest").click(function () {
    CancelProjectEst($("#id").val());
});

function search(nameKey, myArray) {
    for (var i = 0; i < myArray.length; i++) {
        if (myArray[i].Nomor === nameKey) {
            return myArray[i];
        }
    }
}

$("#btncari").click(function () {
    var resultObject = search($("#cari").val(), list);

    //Hidden/Unhide Button
    if (levelapp < 0) // Manager Dept Pemohon
    {
        $("#finished").attr("disabled", true);
        $("#biaya").attr("disabled", true);
        $("#notapprove").show();
    }
    else if (levelapp == 3) //PM
    {
        $("#finished").attr("disabled", true);
        $("#biaya").attr("disabled", true);
        $("#notapprove").show();
    }
    else if (levelapp == 1)//Head Enginering
    {
        $("#finished").attr("disabled", false);
        $("#biaya").attr("disabled", false);
    }
    else if (levelapp == 0)//Head GA
    {
        $("#finished").attr("disabled", false);
        $("#biaya").attr("disabled", false);
    }
    else if (levelapp == 2)//Mgr MTN
    {
        $("#finished").attr("disabled", false);
        $("#biaya").attr("disabled", false);
    }
    else if (levelapp == 5)//Mgr GA
    {
        $("#finished").attr("disabled", false);
        $("#biaya").attr("disabled", false);
    }
    else if (levelapp == 4)//Direksi
    {
        $("#finished").attr("disabled", true);
        $("#biaya").attr("disabled", true);
    }
    else if (levelapp < 0 && userapv > 0 && userdept == 5 || levelapp < 0 && userapv > 0 && userdept == 18 || levelapp < 0 && userapv > 0 && userdept == 4) // Head MTN
    {
        $("#finished").attr("disabled", true);
        $("#biaya").attr("disabled", true);
    }

    displayItem(resultObject);
});



function ApproveProject(approve) {
    $.ajax({
        url: "ApprovalProject.aspx/ApproveProject",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ approve: approve }),
        success: function (data) {
            $.confirm({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Project Telah Di Approve',
                theme: 'modern',
                type: 'green',
                buttons: {
                    OK: function () {
                        location.reload();
                    }
                }
            });
        }
    });
}

function ReScheduleProject(ID) {
    $.ajax({
        url: "ApprovalProject.aspx/Reschedule",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: ID }),
        success: function (data) {
            $.confirm({
                icon: 'fa fa-check',
                title: 'Success',
                content: 'Permintaan ReSchedule Sudah Dikirim',
                theme: 'modern',
                type: 'green',
                buttons: {
                    OK: function () {
                        location.reload();
                    }
                }
            });
        }
    });
}

function CancelProject(ID) {
    $.ajax({
        url: "ApprovalProject.aspx/CancelProject",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: ID }),
        success: function (data) {
            $.confirm({
                icon: 'fa fa-times',
                title: 'Warning!',
                content: 'Project Tidak DiSetujui',
                theme: 'modern',
                type: 'red',
                buttons: {
                    OK: function () {
                        location.reload();
                    }
                }
            });
        }
    });
}


function CancelProjectEst(ID) {
    $.ajax({
        url: "ApprovalProject.aspx/CancelProjectEst",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify({ ID: ID }),
        success: function (data) {
            $.confirm({
                icon: 'fa fa-times',
                title: 'Warning!',
                content: 'Estimasi Project Tidak DiSetujui',
                theme: 'modern',
                type: 'red',
                buttons: {
                    OK: function () {
                        location.reload();
                    }
                }
            });
        }
    });
}