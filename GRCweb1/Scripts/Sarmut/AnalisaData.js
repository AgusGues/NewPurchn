////var checkboxes = document.querySelectorAll('input[type="checkbox"]');

////// On document ready event, set the initial states of the checkboxes
////document.addEventListener('DOMContentLoaded', function () {
////    checkboxes.forEach(function (checkbox) {
////        this.checked = window.localStorage.getItem(checkbox.id) || false;
////    });
////});

////// When checkbox state is changed, save it to the localStorage
////checkboxes.forEach(function (checkbox) {
////    checkbox.addEventListener('change', function () {
////        window.localStorage.setItem(this.id, this.value);
////    });
////});

$(document).ready(function () {
    var checkbox;
    if ($('#chkTTercapai').is(':checked') == true) {
        $("#chkTTercapai").prop("checked", true);
        checkbox = document.getElementById('chkTTercapai');
        localStorage.setItem('chkTTercapai', checkbox.checked);
    } else {
        $("#chkTercapai").prop("checked", true);
        checkbox = document.getElementById('chkTercapai');
        localStorage.setItem('chkTercapai', checkbox.checked);
    }
});

function save() {
    var checkbox;
    if ($('#chkTTercapai').is(':checked') == true) {
        $("#chkTTercapai").prop("checked", true);
        checkbox = document.getElementById('chkTTercapai');
        localStorage.setItem('chkTTercapai', checkbox.checked);
    } else {
        $("#chkTercapai").prop("checked", true);
        checkbox = document.getElementById('chkTercapai');
        localStorage.setItem('chkTercapai', checkbox.checked);
    }
}

function load() {
    var checked = JSON.parse(localStorage.getItem('chkTTercapai'));
    document.getElementById("chkTTercapai") == checked;
}

function clear() {
    location.reload();
    localStorage.clear()

}

load();