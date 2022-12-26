function clearText()
{
   $('#nopo').val("");
   $('#alasanreprint').val("");
}


function addNew()
{
    var nopo = $('#nopo').val();
    var alasanreprint = $('#alasanreprint').val();

    var empObj = {
        nopo: $('#nopo').val(),
        alasanreprint: $('#alasanreprint').val()

    };

    if (nopo == "") {

        window.alert("Nomor Po Tidak Boleh Kosong");

    } else if (alasanreprint == "")
    {
        window.alert("Alasan Reprint Tidak Boleh Kosong");
    } else
    {
        $.ajax({
            url: "RePrintPO.aspx/ReprintPO",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",

            success: function (result) {

                
                clearText();

                
            }
        });
        $.ajax({
            url: "RePrintPO.aspx/CetakPOulang2",
            data: JSON.stringify({ nopo1: nopo }),
            type: "POST",
            contentType: "application/json;charset=utf-8",

            success: function (result) {


                clearText();


            }
        });
        window.alert("Reprin PO Berhasil");
    }

    
}

