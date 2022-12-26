
$(document).ready(function () {
    window.setInterval(function () {
        //getInfoUser();
        getSPPInfo();
        getPoInfo();
        getBAInfo();
        getTaskInfo();
    }, 60000);
    $("marquee").hover(function () {
        this.stop();
    }, function () {
        this.start();
    });
    setInterval("$('#tsInfo').fadeOut().fadeIn();", 2000)
});
function searchSearch(strVal) {
    var url = 'SearchAll.ashx';
    var usr = $('#<%=usrlgn.ClientID %>').val();
    var psn = $('#usrmsg').val();//.split(',');
    var ss = psn.indexOf('All');
    //alert(ss);
    $.post(url, { 'id': '1', 'Status': strVal }, function (response) {

        if (psn.indexOf(usr) > -1) {
            $('#msgs').attr('visible', 'true');
            $('#msgs').html(response);
        } else if (psn.indexOf('ALL') > -1) {
            $('#msgs').attr('visible', 'true');
            $('#msgs').html(response);
        } else {
            $('#msgs').attr('visible', 'false');
        }
    });
}
function getInfoUser() {
    var url = 'SearchAll.ashx'
    $.post(url, { 'id': '2', 'Status': '' }, function (response) {
        $('#usrmsg').val(response);
        $('#usrmsg').val($('#usrmsg').val().toUpperCase())
        searchSearch('pesan');
    });
}
function getSPPInfo() {
    var url = 'SearchAll.ashx';
    $.post(url, { 'id': '3', 'Status': '' }, function (response) {
        $('#usrmsg').val('');
        if (response != '') {
            $('#msgSPP').attr('visible', 'true');
            $('#msgSPP').html(response);
        } else {
            $('#msgSPP').attr('visible', 'true');
        }
    });
} /**/
function getPoInfo() {
    var url = 'SearchAll.ashx';
    $.post(url, { 'id': '4', 'Status': '' }, function (response) {
        $('#usrmsg').val('');
        if (response != '') {
            $('#msgSPP').attr('visible', 'true');
            $('#msgSPP').html(response);
        } else {
            $('#msgSPP').attr('visible', 'true');
        }
    });
} /**/
function getBAInfo() {
    var url = 'SearchAll.ashx';
    $.post(url, { 'id': '5', 'Approval': '' }, function (response) {
        $('#usrmsg').val('');
        if (response != '') {
            $('#msgSPP').attr('visible', 'true');
            $('#msgSPP').html(response);
        } else {
            $('#msgSPP').attr('visible', 'true');
        }
    });
}
function getTaskInfo() {
    var url = 'SearchAll.ashx';
    /*$.post(url, { 'id': '6', 'Approval': '' }, function(response) {
        var d=JSON.parse(response);
        $('#usrmsg').val('');
        $('#msgSPP').html(d[0].TaskName);
    });*/
    $.ajax({
        type: 'POST',
        url: url,
        data: { id: '6' },
        dataType: 'json',
        success: function (datax) {
            if (datax.length > 0) {

                var title = '=>' + datax[0].PIC + ' - ' + datax[0].TaskName + '\n\r';
                $('#tsInfo')
                    .html(datax.length + " Task harus di update")
                    .attr("title", title);

            }
        }
    });
}