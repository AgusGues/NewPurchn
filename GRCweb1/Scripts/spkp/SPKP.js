$(document).ready(function () {
    Getkategori();
    GetTebal();
    GetUkuran();
    //getday();
    $('.the-loader').hide();
    GetUser();
});
function report() {
    window.location.replace("../SPKP/SPKPReport2.aspx");
}
function List() {
    window.location.replace("../SPKP/ListSPKP.aspx");
}

function days(current) {
    var week = new Array();
    // Starting Monday not Sunday 
    var first = ((current.getDate() - current.getDay()) + 1);
    for (var i = 0; i < 7; i++) {
        week.push(
            new Date(current)
        );
        current.setDate(current.getDate() + 1);
    }
    return week;
}


function getday() {
    //logika get all date next week
    //var date = new Date;
    //var nextWeekStart = date.getDate() - date.getDay() + 7;
    //var nextWeekFrom = new Date(date.setDate(nextWeekStart));
    //var nextWeekStart1 = date.getDate() - date.getDay() + 1;
    //var nextWeekFrom1 = new Date(date.setDate(nextWeekStart1));
    //var nextWeekStart2 = date.getDate() - date.getDay() + 2;
    //var nextWeekFrom2 = new Date(date.setDate(nextWeekStart2));
    //var nextWeekStart3 = date.getDate() - date.getDay() + 3;
    //var nextWeekFrom3 = new Date(date.setDate(nextWeekStart3));
    //var nextWeekStart4 = date.getDate() - date.getDay() + 4;
    //var nextWeekFrom4 = new Date(date.setDate(nextWeekStart4));
    //var nextWeekStart5 = date.getDate() - date.getDay() + 5;
    //var nextWeekFrom5 = new Date(date.setDate(nextWeekStart5));

    //var nextWeekEnd = date.getDate() - date.getDay() + 6;
    //var nextWeekTo = new Date(date.setDate(nextWeekEnd));
    //var nextWeekEnd2 = date.getDate() - date.getDay() + 7;
    //var nextWeekTo2 = new Date(date.setDate(nextWeekEnd2));

    //end logika next week

    var input = new Date($("#txttgl").val());
    var result;
    var dt = input;
    while (dt.getDay() != 2) {
        dt.setDate(dt.getDate() - 1);
    }
    
    result = days(dt);
   
    var nextWeekFrom1=result[0];
    var nextWeekFrom2=result[1];
    var nextWeekFrom3=result[2];
    var nextWeekFrom4=result[3];
    var nextWeekFrom5=result[4];
    var nextWeekTo=result[5];
    var nextWeekTo2=result[6];

    var senin = new Date(nextWeekFrom1).toISOString().slice(0, 10);
    var selasa = new Date(nextWeekFrom2).toISOString().slice(0, 10);
    var rabu = new Date(nextWeekFrom3).toISOString().slice(0, 10);
    var kamis = new Date(nextWeekFrom4).toISOString().slice(0, 10);
    var jumat = new Date(nextWeekFrom5).toISOString().slice(0, 10);
    var sabtu = new Date(nextWeekTo).toISOString().slice(0, 10);
    var minggu = new Date(nextWeekTo2).toISOString().slice(0, 10);

    $("#tanggal1").text(senin);
    $("#tanggal1b").text(senin);
    $("#tanggal1c").text(senin);
    $("#tanggal1d").text(senin);
    $("#tanggal1e").text(senin);
    $("#tanggal1f").text(senin);
    $("#tanggal2").text(selasa);
    $("#tanggal2b").text(selasa);
    $("#tanggal2c").text(selasa);
    $("#tanggal2d").text(selasa);
    $("#tanggal2e").text(selasa);
    $("#tanggal2f").text(selasa);
    $("#tanggal3").text(rabu);
    $("#tanggal3b").text(rabu);
    $("#tanggal3c").text(rabu);
    $("#tanggal3d").text(rabu);
    $("#tanggal3e").text(rabu);
    $("#tanggal3f").text(rabu);
    $("#tanggal4").text(kamis);
    $("#tanggal4b").text(kamis);
    $("#tanggal4c").text(kamis);
    $("#tanggal4d").text(kamis);
    $("#tanggal4e").text(kamis);
    $("#tanggal4f").text(kamis);
    $("#tanggal5").text(jumat);
    $("#tanggal5b").text(jumat);
    $("#tanggal5c").text(jumat);
    $("#tanggal5d").text(jumat);
    $("#tanggal5e").text(jumat);
    $("#tanggal5f").text(jumat);
    $("#tanggal6").text(sabtu);
    $("#tanggal6b").text(sabtu);
    $("#tanggal6c").text(sabtu);
    $("#tanggal6d").text(sabtu);
    $("#tanggal6e").text(sabtu);
    $("#tanggal6f").text(sabtu);
    $("#tanggal7").text(minggu);
    $("#tanggal7b").text(minggu);
    $("#tanggal7c").text(minggu);
    $("#tanggal7d").text(minggu);
    $("#tanggal7e").text(minggu);
    $("#tanggal7f").text(minggu);
}

function Getkategori() {
    $.ajax({
        url: "SPKP.aspx/Getkategori",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data.d, function (data, value) {
                $("#ddlkategori1a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori1b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori2a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori2b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori3a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori3b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori4a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori4b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori5a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori5b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori6a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori6b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori7a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori7b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori8a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori8b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori9a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori9b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori10a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori10b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori11a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori11b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori12a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori12b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori13a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori13b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori14a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori14b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori15a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori15b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori16a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori16b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori17a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori17b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori18a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori18b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori19a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori19b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori20a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori20b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori21a").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
                $("#ddlkategori21b").append('<option value="' + value.Kategori + '" >' + value.Kategori + '</option>');
            });
            $('#ddlkategori1a').trigger('chosen:updated');
            $("#ddlkategori1a").chosen({ width: "95%" });
        }
    });
}

function setkategori1a(value)
{
    $("#ddlkategori1b").val(value).change();
}

function setkategori1b(value)
{
    $("#ddlkategori2a").val(value).change();
}

function setkategori2a(value) {

    $("#ddlkategori2b").val(value).change();
}

function setkategori2b(value) {
    $("#ddlkategori3a").val(value).change();
}

function setkategori3a(value) {

    $("#ddlkategori3b").val(value).change();
}

function setkategori3b(value) {

    $("#ddlkategori4a").val(value).change();
}

function setkategori4a(value) {

    $("#ddlkategori4b").val(value).change();
}

function setkategori4b(value) {
    $("#ddlkategori5a").val(value).change();
}

function setkategori5a(value) {
    $("#ddlkategori5b").val(value).change();
}

function setkategori5b(value) {
    $("#ddlkategori6a").val(value).change();
}

function setkategori6a(value) {
    $("#ddlkategori6b").val(value).change();
}

function setkategori6b(value) {
    $("#ddlkategori7a").val(value).change();
}

function setkategori7a(value) {
    $("#ddlkategori7b").val(value).change();
}

function setkategori7b(value) {
    $("#ddlkategori8a").val(value).change();
}

function setkategori8a(value) {
    $("#ddlkategori8b").val(value).change();
}

function setkategori8b(value) {
    $("#ddlkategori9a").val(value).change();
}

function setkategori9a(value) {
    $("#ddlkategori9b").val(value).change();
}

function setkategori9b(value) {
    $("#ddlkategori10a").val(value).change();
}

function setkategori10a(value) {
    $("#ddlkategori10b").val(value).change();
}

function setkategori10b(value) {
    $("#ddlkategori11a").val(value).change();
}

function setkategori11a(value) {
    $("#ddlkategori11b").val(value).change();
}

function setkategori11b(value) {
    $("#ddlkategori12a").val(value).change();
}

function setkategori12a(value) {
    $("#ddlkategori12b").val(value).change();
}

function setkategori12b(value) {
    $("#ddlkategori13a").val(value).change();
}

function setkategori13a(value) {
    $("#ddlkategori13b").val(value).change();
}

function setkategori13b(value) {
    $("#ddlkategori14a").val(value).change();
}

function setkategori14a(value) {
    $("#ddlkategori14b").val(value).change();
}

function setkategori14b(value) {
    $("#ddlkategori15a").val(value).change();
}

function setkategori15a(value) {
    $("#ddlkategori15b").val(value).change();
}

function setkategori15b(value) {
    $("#ddlkategori16a").val(value).change();
}

function setkategori16a(value) {
    $("#ddlkategori16b").val(value).change();
}

function setkategori16b(value) {
    $("#ddlkategori17a").val(value).change();
}

function setkategori17a(value) {
    $("#ddlkategori17b").val(value).change();
}

function setkategori17b(value) {
    $("#ddlkategori18a").val(value).change();
}

function setkategori18a(value) {
    $("#ddlkategori18b").val(value).change();
}

function setkategori18b(value) {
    $("#ddlkategori19a").val(value).change();
}

function setkategori19a(value) {
    $("#ddlkategori19b").val(value).change();
}

function setkategori19b(value) {
    $("#ddlkategori20a").val(value).change();
}

function setkategori20a(value) {
    $("#ddlkategori20b").val(value).change();
}

function setkategori20b(value) {
    $("#ddlkategori21a").val(value).change();
}

function setkategori21a(value) {

    $("#ddlkategori21b").val(value).change();
}



function GetTebal() {
    $.ajax({
        url: "SPKP.aspx/GetTebal",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
           
            $.each(data.d, function (data, value) {
                $("#ddltebal1a").append('<option value="' + value.Tebal + '" >' + value.Tebal  + '</option>');
                $("#ddltebal1b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal2a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal2b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal3a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal3b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal4a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal4b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal5a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal5b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal6a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal6b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal7a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal7b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal8a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal8b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal9a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal9b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal10a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal10b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal11a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal11b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal12a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal12b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal13a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal13b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal14a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal14b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal15a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal15b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal16a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal16b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal17a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal17b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal18a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal18b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal19a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal19b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal20a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal20b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal21a").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
                $("#ddltebal21b").append('<option value="' + value.Tebal  + '" >' + value.Tebal  + '</option>');
            });
            $("#ddltebal1a").trigger("chosen:updated");
            $("#ddltebal1a").chosen({ width: "95%" });

        }
    });
}

function settebal1a(value) {
    $("#ddltebal1b").val(value).change();
}

function settebal1b(value) {
    $("#ddltebal2a").val(value).change();
}
function settebal2a(value) {
    $("#ddltebal2b").val(value).change();
}
function settebal2b(value) {
    $("#ddltebal3a").val(value).change();
}
function settebal3a(value) {
    $("#ddltebal3b").val(value).change();
}
function settebal3b(value) {
    $("#ddltebal4a").val(value).change();
}
function settebal4a(value) {
    $("#ddltebal4b").val(value).change();
}
function settebal4b(value) {
    $("#ddltebal5a").val(value).change();
}
function settebal5a(value) {
    $("#ddltebal5b").val(value).change();
}
function settebal5b(value) {
    $("#ddltebal6a").val(value).change();
}
function settebal6a(value) {
    $("#ddltebal6b").val(value).change();
}
function settebal6b(value) {
    $("#ddltebal7a").val(value).change();
}
function settebal7a(value) {
    $("#ddltebal7b").val(value).change();
}
function settebal7b(value) {
    $("#ddltebal8a").val(value).change();
}
function settebal8a(value) {
    $("#ddltebal8b").val(value).change();
}
function settebal8b(value) {
    $("#ddltebal9a").val(value).change();
}
function settebal9a(value) {
    $("#ddltebal9b").val(value).change();
}
function settebal9b(value) {
    $("#ddltebal10a").val(value).change();
}
function settebal10a(value) {
    $("#ddltebal10b").val(value).change();
}
function settebal10b(value) {
    $("#ddltebal11a").val(value).change();
}
function settebal11a(value) {
    $("#ddltebal11b").val(value).change();
}
function settebal11b(value) {
    $("#ddltebal12a").val(value).change();
}
function settebal12a(value) {
    $("#ddltebal12b").val(value).change();
}
function settebal12b(value) {
    $("#ddltebal13a").val(value).change();
}
function settebal13a(value) {
    $("#ddltebal13b").val(value).change();
}
function settebal13b(value) {
    $("#ddltebal14a").val(value).change();
}
function settebal14a(value) {
    $("#ddltebal14b").val(value).change();
}
function settebal14b(value) {
    $("#ddltebal15a").val(value).change();
}
function settebal15a(value) {
    $("#ddltebal15b").val(value).change();
}
function settebal15b(value) {
    $("#ddltebal16a").val(value).change();
}
function settebal16a(value) {
    $("#ddltebal16b").val(value).change();
}
function settebal16b(value) {
    $("#ddltebal17a").val(value).change();
}
function settebal17a(value) {
    $("#ddltebal17b").val(value).change();
}
function settebal17b(value) {
    $("#ddltebal18a").val(value).change();
}
function settebal18a(value) {
    $("#ddltebal18b").val(value).change();
}
function settebal18b(value) {
    $("#ddltebal19a").val(value).change();
}
function settebal19a(value) {
    $("#ddltebal19b").val(value).change();
}
function settebal19b(value) {
    $("#ddltebal20a").val(value).change();
}
function settebal20a(value) {
    $("#ddltebal20b").val(value).change();
}
function settebal20b(value) {
    $("#ddltebal21a").val(value).change();
}
function settebal21a(value) {
    $("#ddltebal21b").val(value).change();
}
function GetUkuran() {
    $.ajax({
        url: "SPKP.aspx/GetUkuran",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            $.each(data.d, function (data, value) {
                $("#ddlukuran1a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran1b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran2a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran2b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran3a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran3b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran4a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran4b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran5a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran5b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran6a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran6b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran7a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran7b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran8a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran8b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran9a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran9b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran10a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran10b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran11a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran11b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran12a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran12b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran13a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran13b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran14a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran14b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran15a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran15b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran16a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran16b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran17a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran17b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran18a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran18b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran19a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran19b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran20a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran20b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran21a").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
                $("#ddlukuran21b").append('<option value="' + value.Ukuran + '" >' + value.Ukuran + '</option>');
            });
            $("#ddlukuran1a").trigger("chosen:updated");
            $("#ddlukuran1a").chosen({ width: "95%" });

        }
    });
}

function setUkuran1a(value) {
    $("#ddlukuran1b").val(value).change();
}

function setUkuran1b(value) {
    $("#ddlukuran2a").val(value).change();
}
function setUkuran2a(value) {
    $("#ddlukuran2b").val(value).change();
}
function setUkuran2b(value) {
    $("#ddlukuran3a").val(value).change();
}
function setUkuran3a(value) {
    $("#ddlukuran3b").val(value).change();
}
function setUkuran3b(value) {
    $("#ddlukuran4a").val(value).change();
}
function setUkuran4a(value) {
    $("#ddlukuran4b").val(value).change();
}
function setUkuran4b(value) {
    $("#ddlukuran5a").val(value).change();
}
function setUkuran5a(value) {
    $("#ddlukuran5b").val(value).change();
}
function setUkuran5b(value) {
    $("#ddlukuran6a").val(value).change();
}
function setUkuran6a(value) {
    $("#ddlukuran6b").val(value).change();
}
function setUkuran6b(value) {
    $("#ddlukuran7a").val(value).change();
}
function setUkuran7a(value) {
    $("#ddlukuran7b").val(value).change();
}
function setUkuran7b(value) {
    $("#ddlukuran8a").val(value).change();
}
function setUkuran8a(value) {
    $("#ddlukuran8b").val(value).change();
}
function setUkuran8b(value) {
    $("#ddlukuran9a").val(value).change();
}
function setUkuran9a(value) {
    $("#ddlukuran9b").val(value).change();
}
function setUkuran9b(value) {
    $("#ddlukuran10a").val(value).change();
}
function setUkuran10a(value) {
    $("#ddlukuran10b").val(value).change();
}
function setUkuran10b(value) {
    $("#ddlukuran11a").val(value).change();
}
function setUkuran11a(value) {
    $("#ddlukuran11b").val(value).change();
}
function setUkuran11b(value) {
    $("#ddlukuran12a").val(value).change();
}
function setUkuran12a(value) {
    $("#ddlukuran12b").val(value).change();
}
function setUkuran12b(value) {
    $("#ddlukuran13a").val(value).change();
}
function setUkuran13a(value) {
    $("#ddlukuran13b").val(value).change();
}
function setUkuran13b(value) {
    $("#ddlukuran14a").val(value).change();
}
function setUkuran14a(value) {
    $("#ddlukuran14b").val(value).change();
}
function setUkuran14b(value) {
    $("#ddlukuran15a").val(value).change();
}
function setUkuran15a(value) {
    $("#ddlukuran15b").val(value).change();
}
function setUkuran15b(value) {
    $("#ddlukuran16a").val(value).change();
}
function setUkuran16a(value) {
    $("#ddlukuran16b").val(value).change();
}
function setUkuran16b(value) {
    $("#ddlukuran17a").val(value).change();
}
function setUkuran17a(value) {
    $("#ddlukuran17b").val(value).change();
}
function setUkuran17b(value) {
    $("#ddlukuran18a").val(value).change();
}
function setUkuran18a(value) {
    $("#ddlukuran18b").val(value).change();
}
function setUkuran18b(value) {
    $("#ddlukuran19a").val(value).change();
}
function setUkuran19a(value) {
    $("#ddlukuran19b").val(value).change();
}
function setUkuran19b(value) {
    $("#ddlukuran20a").val(value).change();
}
function setUkuran20a(value) {
    $("#ddlukuran20b").val(value).change();
}
function setUkuran20b(value) {
    $("#ddlukuran21a").val(value).change();
}
function setUkuran21a(value) {
    $("#ddlukuran21b").val(value).change();
}
var user;
function GetUser() {
    $.ajax({
        url: "SPKP.aspx/GetUser",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data = data.d
            data.data = data.results
            delete data.results
            user=data;

        }
    });
}
function Simpan() {
    $('.the-loader').show();
    var Tanggal;
    var Shift;
    var Kategori;
    var Tebal;
    var Ukuran;
    var Target;
    var Line;
    var NoSpkp;
    var CreatedBy;
    var dinput = {
        data: []
    
    };
    $("#tblinput tbody tr").each(function (i, element) {
        var html = $(this).html();
        if (html != "") {
            Tanggal = $(this).find(".tgl").text();
            Shift = $(this).find(".shift").text();
            Kategori = $(this).find(".kategori option:selected").text();
            Tebal = $(this).find(".tebal option:selected").text();
            Ukuran = $(this).find(".ukuran option:selected").text();
            Target = $(this).find(".target").val();
            Line = $("#ddlline").val();
            NoSpkp = $("#nospkp").val();
            CreatedBy = user;
            Ketrangan = $(this).find(".Keterangan").val();

            data = {
                Tanggal: Tanggal,
                Shift: Shift,
                Kategori: Kategori,
                Ukuran: Ukuran,
                Tebal: Tebal,
                Line: Line,
                NoSpkp: NoSpkp,
                Target: Target,
                CreatedBy: CreatedBy,
                Keterangan:Ketrangan
            }

        }
        if (Target != "") {
            dinput.data.push(data);
        }
    });
    
    obj = {};
    obj.nospkp = $("#nospkp").val();
    obj.CreatedBy = user;
    if (Line == "0") {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'warning!', type: 'red',
            content: "Pilih Line"
            //data.d
        });
        return;
    }
    if (NoSpkp == "") {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'warning!', type: 'red',
            content: "No SPKP Tidak Boleh Kosong !"
            //data.d
        });
        return;
    }
    else {
        $('.the-loader').hide();
        $.ajax({
            url: 'SPKP.aspx/Insert',
            type: 'POST',
            data: JSON.stringify({ dinput: dinput, obj: obj }),
            contentType: "application/json; charset=utf-8",
            dataType: 'text',
            success: function (data) {
                $('.the-loader').hide(); $.alert({
                    icon: 'fa fa-check', theme: 'modern', title: 'success!', type: 'green',
                    content: "data Berhasil disimpan"
                    //data.d
                });
                clear();
            }
        });
    
    }
}

function clear() {
    setkategori('');
    settebal('');
    setUkuran('');
    $("#ddlline").val('0').change();
    $("#1a").val("");
    $("#1b").val("");
    $("#2a").val("");
    $("#2b").val("");
    $("#3a").val("");
    $("#3b").val("");
    $("#4a").val("");
    $("#4b").val("");
    $("#5a").val("");
    $("#5b").val("");
    $("#6a").val("");
    $("#6b").val("");
    $("#7a").val("");
    $("#7b").val("");
    $("#8a").val("");
    $("#8b").val("");
    $("#9a").val("");
    $("#9b").val("");
    $("#10a").val("");
    $("#10b").val("");
    $("#11a").val("");
    $("#11b").val("");
    $("#12a").val("");
    $("#12b").val("");
    $("#13a").val("");
    $("#13b").val("");
    $("#14a").val("");
    $("#14b").val("");
    $("#15a").val("");
    $("#15b").val("");
    $("#16a").val("");
    $("#16b").val("");
    $("#17a").val("");
    $("#17b").val("");
    $("#18a").val("");
    $("#18b").val("");
    $("#19a").val("");
    $("#19b").val("");
    $("#20a").val("");
    $("#20b").val("");
    $("#21a").val("");
    $("#21b").val("");
}

$('#1a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#1a').val()
    $('#2a').val(a);
    $('#3a').val(a);
    $('#4a').val(a);
    $('#5a').val(a);
    $('#6a').val(a);
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#2a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#2a').val()
    $('#3a').val(a);
    $('#4a').val(a);
    $('#5a').val(a);
    $('#6a').val(a);
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#3a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#3a').val()
    $('#4a').val(a);
    $('#5a').val(a);
    $('#6a').val(a);
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#4a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#4a').val()
    $('#5a').val(a);
    $('#6a').val(a);
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#5a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#5a').val()
    $('#6a').val(a);
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#6a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#6a').val()
    $('#7a').val(a);
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#7a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#7a').val()
    $('#8a').val(a);
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#8a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#8a').val()
    $('#9a').val(a);
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#9a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#9a').val()
    $('#10a').val(a);
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#10a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#10a').val()
    $('#11a').val(a);
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#11a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#11a').val()
    $('#12a').val(a);
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#12a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#12a').val()
    $('#13a').val(a);
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#13a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#13a').val()
    $('#14a').val(a);
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#14a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#14a').val()
    $('#15a').val(a);
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#15a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#15a').val()
    $('#16a').val(a);
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#16a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#16a').val()
    $('#17a').val(a);
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#17a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#17a').val()
    $('#18a').val(a);
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#18a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#18a').val()
    $('#19a').val(a);
    $('#20a').val(a);
    $('#21a').val(a);
});
$('#20a').on('input', function () {
    //alert('Text1 changed!');
    var a = $('#20a').val()
    $('#21a').val(a)
});