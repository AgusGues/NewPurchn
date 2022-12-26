$('#Tanggal').datepicker({ dateFormat: 'dd-mm-yy' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
    lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#panel-form').click(function(){
    $('.panel-form').show();$('.panel-list').hide();
});
$('#panel-list').click(function(){
    $('.panel-list').show();$('.panel-form').hide();
});
var optionPalet=`
<option value="">PIlih Palet</option>
<option value="Palet Packing">Palet Packing</option>
<option value="Palet Stock">Palet Stock</option>
`;
$('#Palet').html(optionPalet);
$('#BtnCariData').click(function(){
    $('.the-loader').show();
    var PakaiNo=$('#list-PakaiNo').val();
    var ItemName=$('#list-ItemName').val();
    var CreatedBy=$('#list-CreatedBy').val();
    $('#datatable').DataTable().clear().draw();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({PakaiNo:PakaiNo,ItemName:ItemName,CreatedBy:CreatedBy}),
        success:function(output){
            var row=[];
            $.each(output.d,function(i,b){
                var Status='Open';
                if(b.Status==1){Status='Head';}
                if(b.Status==2){Status='Manager';}
                if(b.Status==3){Status='Gudang';}
                row.push([b.ID,b.PakaiNo,b.CreatedTime,b.ItemCode,b.ItemName,b.Quantity,b.UOMDesc,b.Keterangan,Status]);
            });
            $('#datatable').DataTable().rows.add(row).draw();
            $('.the-loader').hide();
        }
    });
});
$('#BtnClearData').click(function(){
    $('#datatable').DataTable().clear().draw();
});
function CekPendingSPB(){
    $('.the-loader').show();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RetrieveOpenStatus',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success:function(output){
            var row='';
            if(output.d!=''){
                $.each(output.d,function(i,b){
                    var Sts='Open';
                    if(b.Status==1){Sts='Head';}
                    if(b.Status==2){Sts='Manager';}
                    row+=b.PakaiNo+' - '+'Status Masih ='+Sts+'<br>';                        
                });
                if(row!=''){
                    $.alert({
                        icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                        content: 'Tidak boleh SPB, Sebab ada SPB yang belum diAprove oleh Orang Gudang <br>'+row
                    });
                    $('.panel-list').show();$('.panel-form').hide();
                    $('#panel-form').attr('disabled','disabled'); 
                }
            }
            $('.the-loader').hide();
        }
    });
};CekPendingSPB();
function LoadListLockSPP() {
    $('.the-loader').show();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/LoadListLockSPP',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {}
    });
}LoadListLockSPP();
function ListDept() {
    $('.the-loader').show();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/ListDept',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="">pilihan Departmen</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.DeptName+`</option>`;
            });
            $('#DllDept').html(row);
            $('.the-loader').hide();
        }
    });
}ListDept();
$('#DllDept').change(function(){
    $('.the-loader').show();
    clearValue();
    $('#row-KodeProject').hide();
    $('#row-NameProject').hide();
    $('#row-Zona').hide();
    $('#row-SpGroup').hide();
    $('#row-TextBarang').show();
    var DllDept=$('#DllDept').val();
    if(DllDept==19){
        $('#row-KodeProject').show();
        $('#row-NameProject').show();
        $('#row-Zona').show();
        $('#row-SpGroup').hide();
        $('#row-TextBarang').hide();
    }
    if(DllDept==4 || DllDept==5 || DllDept==18){
        $('#row-KodeProject').hide();
        $('#row-NameProject').hide();
        $('#row-Zona').show();
        $('#row-SpGroup').show();
        $('#row-TextBarang').show();
    }
    $.ajax({
        url: 'FormPakaiSparePart.aspx/GetDeptCode', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept}),
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#DeptCode').val(b.DeptCode);
            });
            $('.the-loader').hide();
        }
    })
    $('.the-loader').hide();
});
$('#BtnCariProject').click(function(){
    $('.the-loader').show();
    var KodeProject=$('#KodeProject').val();
    $('#NameProject').text('');
    $.ajax({
        url: 'FormPakaiSparePart.aspx/ListProject', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({KodeProject:KodeProject}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#NameProject').text(b.ProjectName);
                $('#ProjectID').val(b.ProjectID);
            });
            $('.the-loader').hide();
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RetrieveEstimasiMaterialList', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({KodeProject:KodeProject}),
        async: false,
        success: function (uotput) {
            var row = '<option value="0">pilhan Item</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.ItemName+`=`+b.Jumlah+`</option>`;
            });
            $('#ddlItem').html(row);
            $('.the-loader').hide();
        }
    })
});
function ListGroup() {
    $('.the-loader').show();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RetrieveSpGroup',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Group</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.ZonaName+`</option>`;
            });
            $('#SpGroup').html(row);
            $('.the-loader').hide();
        }
    });
}ListGroup();
$('#BtnCariBarang').click(function(){
    $('.the-loader').show();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/ListBarang', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({TextBarang:$("#TextBarang").val()}),
        success: function (uotput) {
            var row = '<option value="0">pilhan Item</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID+`">`+b.ItemName+` (`+b.ItemCode+`)</option>`;
            });
            $('#ddlItem').html(row);$('.the-loader').hide();
        }
    })
});
$('#Palet').change(function(){
    $('#Keterangan').val($('#Palet').val());
});
$('#ddlItem').change(function(){
    $('.the-loader').show(); 
    if($('#DllDept').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Departmennya'
        });return false;
    }
    var itemtext=$('#ddlItem option:selected').text();
    itemtext=itemtext.substring(0,5);
    $('#row-Keterangan').show();$('#row-Palet').hide();
    if($('#DllDept').val()==6 || $('#DllDept').val()==10 && itemtext=='PAPAN' || itemtext=='BALOK'){
        $('#row-Keterangan').hide();$('#row-Palet').show();
    }
    var ItemTypeID=0;
    var DllDept=$('#DllDept').val();
    var ddlItem=$('#ddlItem').val();
    var Tanggal=$('#Tanggal').val();
    var tahun=$("#Tanggal").val().split('-')[2];
    var bulan=$("#Tanggal").val().split('-')[1];
    var Jumlah=0;
    var ReOrder=0;
    var StockOtherDept=0;
    var ShowStock=0;
    var PendingSPB=0
    var StockAkhir=0;
    var Planning=0;
    var CheckCost=0;
    var MaxSPB=0;
    var AddBudget=0;
    var Head=0;
    var RuleBudget=0;
    var TotalSPB=0;
    var ItemIDKhusus=0;
    var JumlahMaterial=0;
    var TotalSPBPrj=0;
    var KodeProject=$('#KodeProject').val();
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RetrieveById', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#ItemID').val(b.ID); 
                $('#ItemCode').val(b.ItemCode); 
                $('#UOMID').val(b.UOMID);
                $('#UOMDesc').val(b.UOMDesc);
                $('#GroupID').val(b.GroupID);
                $('#ItemTypeID').val(b.ItemTypeID);ItemTypeID=b.ItemTypeID;
                $('#UOMDesc').val(b.UOMDesc);
                $('#Head').val(b.Head);Head=b.Head;
                $('#Jumlah').val(b.Jumlah);Jumlah=b.Jumlah;
                $('#ReOrder').val(b.ReOrder);ReOrder=b.ReOrder;
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/GetPrice', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#AvgPrice').val(b.MonthAvgPrice); 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RetrieveByStock', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept,ddlItem:ddlItem, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#StockOtherDept').val(b.StockOtherDept);StockOtherDept=b.StockOtherDept; 
            });
        }
    })
    if((Jumlah-StockOtherDept)>0){ShowStock=Jumlah-StockOtherDept;$('#ShowStock').val(ShowStock);}
    $.ajax({
        url: 'FormPakaiSparePart.aspx/GetPendingSPB', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#PendingSPB').val(b.PendingSPB);PendingSPB=b.PendingSPB; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/GetStockAkhir', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#StockAkhir').val(b.StockAkhir);StockAkhir=b.StockAkhir; 
            });
        }
    })
    if((StockAkhir-StockOtherDept-PendingSPB)>0){$('#Stock').val(StockAkhir-StockOtherDept-PendingSPB);}else{$('#Stock').val(0);}
    $('#Blocked').val(PendingSPB+StockOtherDept);
    $.ajax({
        url: 'FormPakaiSparePart.aspx/PlanningProd', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#Planning').val(b.Planning);Planning=b.Planning; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/MaterialBudgetBM', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem, Planning:Planning}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#CheckCost').val(b.CheckCost);CheckCost=b.CheckCost; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/MaxQtySPB', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept, ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#MaxSPB').val(b.MaxSPB);MaxSPB=b.MaxSPB; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/AddQtyBudget', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept, ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#AddBudget').val(b.AddBudget);AddBudget=b.AddBudget; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/RuleCalc', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept, ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#RuleBudget').val(b.RuleBudget);RuleBudget=b.RuleBudget; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/TotalQtySPB', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept, ddlItem:ddlItem, Tanggal:Tanggal, ItemTypeID:ItemTypeID, RuleBudget:RuleBudget}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#TotalSPB').val(b.TotalSPB);TotalSPB=b.TotalSPB; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/IsMaterialBudgetKhusus', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({DllDept:DllDept, ddlItem:ddlItem}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#ItemIDKhusus').val(b.ItemIDKhusus);ItemIDKhusus=b.ItemIDKhusus; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/MaterialBudgetPrj', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ddlItem:ddlItem, KodeProject:KodeProject}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#JumlahMaterial').val(b.JumlahMaterial);JumlahMaterial=b.JumlahMaterial; 
            });
        }
    })
    $.ajax({
        url: 'FormPakaiSparePart.aspx/TotalQtySPBPrj', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({KodeProject:KodeProject,ddlItem:ddlItem, ItemTypeID:ItemTypeID}),
        async: false,
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#TotalSPBPrj').val(b.TotalSPBPrj);TotalSPBPrj=b.TotalSPBPrj; 
            });
            $('.the-loader').hide();
        }
    })
    if(Head==5 || CheckCost>0){
        var TypeBudget='Bulanan';
        if(RuleBudget==6){TypeBudget='Semesteran';}
        if(RuleBudget==12){TypeBudget='Tahunan';}
        var TotalBudget=AddBudget;
        if(MaxSPB>0){TotalBudget=MaxSPB-AddBudget;}
        $('#PeriodeBudget').val(bulan+'-'+tahun);
        $('#TotalBudget').val(TotalBudget);
        $('#TotalJumlahSPB').val(TotalSPB);
        $('#TambahanBudget').val(AddBudget);
        $('#SisaBudget').val(MaxSPB-TotalSPB);
        $('#TypeBudget').val(TypeBudget);
        if($('#DllDept').val()==2){
            TotalBudget=$('#CheckCost').val();
            $('#PeriodeBudget').val(bulan+'-'+tahun);
            $('#TotalBudget').val(TotalBudget);
            $('#TotalJumlahSPB').val(TotalSPB);
            $('#TambahanBudget').val(AddBudget);
            $('#SisaBudget').val(TotalBudget-TotalSPB);
            $('#TypeBudget').val('RunLine : '+$('#Planning').val());
        }
        if($('#DllDept').val()==19){
            $('#PeriodeBudget').val(bulan+'-'+tahun);
            $('#TotalBudget').val(JumlahMaterial);
            $('#TotalJumlahSPB').val(TotalSPBPrj);
            $('#TambahanBudget').val('');
            $('#SisaBudget').val(JumlahMaterial-TotalSPBPrj);
            $('#TypeBudget').val(TypeBudget);
        }
    }
    $('.the-loader').hide(); 
});

$('#SpGroup').change(function(){
    $('.the-loader').show();
    $('#Forklift').hide();$('#Forklift').html('');
    if($('#SpGroup').val()=='14'){
        $('#Forklift').show();
        $.ajax({
            url: 'FormPakaiSparePart.aspx/ListForklift',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
            async: false,
            success: function (uotput) {
                var row = '<option value="0">pilihan Forklift</option>';
                $.each(uotput.d, function (i, b) {
                    row+=`<option value="`+b.ID +`">`+b.Forklift+`</option>`;
                });
                $('#Forklift').html(row);
            }
        });
    }
    $('.the-loader').hide();
});

var isiDetail; isiDetail= { isiDetail1:[] }

$('#BtnAddItem').click(function(){
    $('.the-loader').show();
    if($('#DllDept').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Departmennya'
        });return false;
    }
    if($('#DllDept').val()==4 || $('#DllDept').val()==5 || $('#DllDept').val()==18 || $('#DllDept').val()==19){
        if($('#SpGroup').val()==''){
            $.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'SP Group harus di pilih!'
            });return false;
        }
        if($('#ZonaMtc').val()==''){
            $.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Zona harus di pilih!'
            });return false;
        }
    }
    if($('#ddlItem').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Nama Barangnya'
        });return false;
    }
    if($('#Quantity').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Isi Dulu Quantitinya'
        });return false;
    }
    if($('#UOMDesc').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Isi Dulu Satuannya'
        });return false;
    }
    if($('#Keterangan').val()==''){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Isi Dulu Keterangannya'
        });return false;
    }
    if($('#Quantity').val()<=0){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Quantity tidak boleh 0'
        });return false;
    }
    if($('#Stock').val()==0){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Stock tidak boleh 0'
        });return false;
    }
    if($('#Stock').val()<$('#Quantity').val()){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Quantity tidak boleh lebih dari Stock'
        });return false;
    }
    if($('#SpGroup').val()==0 && $('#DllDept').val()==4||$('#DllDept').val()==5||$('#DllDept').val()==18){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Spare Part Group Harus di pilih'
        });return false;
    }
    if($('#SpGroup').val()==13 || $('#DllDept').val()==26){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Nomor Kendaraan harus di pilih!'
        });return false;
    }
    if($('#SpGroup').val()!=''){
        if($('#Forklift').val()==''){
            $.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Forklift harus diisi'
            });return false;
        }
    }
    if($('#Tanggal').val()!=''){
        var StatusPurchn=0;
        var Tanggal=$('#Tanggal').val();
        var tahun = $("#Tanggal").val().split('-')[2];
        var bulan = $("#Tanggal").val().split('-')[1];
        $.ajax({
            url: 'FormPakaiSparePart.aspx/RetrieveByStatus', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            data : JSON.stringify({Tanggal:Tanggal}),
            success: function (uotput) {
                $.each(uotput.d, function (i, b) {
                    StatusPurchn=b.Status; 
                    if(StatusPurchn==1){
                        $.alert({
                            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                            content: 'Periode '+bulan+'-'+tahun+' sudah di close by Accounting'
                        });return false;
                    }
                });
            }
        })
        $.ajax({
            url: 'FormPakaiSparePart.aspx/CekClosing', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            data : JSON.stringify({Tanggal:Tanggal, DllDept:$('#DllDept').val()}),
            success: function (uotput) {
                $.each(uotput.d, function (i, b) {
                    var nowTgl = b.nowTgl;
                    var lastTgl = b.lastTgl;
                    var clsBlnStatus = b.clsBlnStatus;
                    var clsBulan = b.clsBulan;
                    var clsTahun = b.clsTahun;
                    if(clsBlnStatus==1){
                        $.alert({
                            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                            content: 'Periode '+clsBulan+'-'+clsTahun+' sudah di close by Accounting'
                        });return false;
                    }else{
                        if(nowTgl<lastTgl){
                           $.alert({
                            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                            content: 'Anda melewati Tanggal input terakhir'
                        });return false;
                       }
                   }
               });
            }
        })
    }
    if($('#CheckCost').val()>0 && $('#DllDept').val()==2){
        if($('#ItemIDKhusus').val()!=''){
            var TotalSPB=$('#TotalJumlahSPB').val();
            var RuleBudget=$('#RuleBudget').val();
            var MaxQtySPB=$('#TotalBudget').val();
            var AddBudget=$('#TambahanBudget').val();
            var Quantity=$('#Quantity').val();
            var ItemIDKhusus=$('#ItemIDKhusus').val();
            if((TotalSPB + Quantity) <= MaxQtySPB){
                var sisabd=0;
                var SisaSPB=MaxQtySPB - (TotalSPB + AddBudget);
                var Planning=$('#Planning').val();
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: 'Sisa SPB untuk bulan ini tinggal : '+sisabd+'lagi <br> Max SPB : '+MaxQtySPB+'Untuk Running '+Planning+'Line, nSilahkan Hubungi Logistik Material'
                });return false;
            }
        }else{
            if((TotalSPB + Quantity) <= (MaxQtySPB + AddBudget)){
                var sisabd=0;
                var SisaSPB=MaxQtySPB - TotalSPB + AddBudget;
                if(SisaSPB<=0){sisabd=0}else{sisabd=SisaSPB;}
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: 'Sisa SPB untuk bulan ini tinggal : '+sisabd+'lagi <br> Max SPB : '+MaxQtySPB+'Untuk Running '+Planning+'Line, nSilahkan Hubungi Logistik Material'
                });return false;
            }
        }
    }else{
        if($('#DllDept').val()==19){
            var sisa=parseInt($('#SisaBudget').text());
            if(sisa-Quantity<0){
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: 'Sisa SPB untuk RAB ini tinggal : '+sisa+' lagi <br> Max SPB : '+$('#TotalJumlahSPB').text()+' Silahkan review / update RAB'
                });return false;
            }
        }else{
            if((TotalSPB + Quantity) <= (MaxQtySPB + AddBudget) && ItemIDKhusus!=''){
                var sisabd=0;
                var SisaSPB=MaxQtySPB - TotalSPB + AddBudget;
                if(SisaSPB<=0){sisabd=0}else{sisabd=SisaSPB;}
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: 'Sisa SPB untuk bulan ini tinggal : '+sisa+' lagi <br> Max SPB : '+MaxQtySPB+' Silahkan Hubungi Logistik Material'
                });return false;
            }
        }
    }
    if($('#Jumlah').val()-$('#Quantity').val()<=$('#ReOrder').val()){
        $.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Jumlah Pemakaian Barang di stock sudah melampaui Reorder Point = '+$('#ReOrder').val()
        });return false;
    }

    var list=`
    <tr>
    <td>`+$('#ItemCode').val()+`</td>
    <td>`+$('#ddlItem option:selected').text()+`</td>
    <td>`+$('#Quantity').val()+`</td>
    <td>`+$('#UOMDesc').val()+`</td>
    <td>`+$('#Keterangan').val()+`</td>
    <td>action</td>
    </tr>
    `;
    if($('#ProjectID').val()==''){$('#ProjectID').val(0);}
    if($('#AvgPrice').val()==''){$('#AvgPrice').val(0);}
    if($('#TotalBudget').val()==''){$('#TotalBudget').val(0);}
    var Ket=$('#Keterangan').val();
    if($('#Forklift').val()!=0){Ket=Ket+' ['+$('#Forklift option:selected').text()+']';}
    isiDetail1={
        ItemID : parseInt($('#ItemID').val()),
        Quantity : parseInt($('#Quantity').val()),
        Keterangan : Ket,
        UomID : parseInt($('#UOMID').val()),
        GroupID : parseInt($('#GroupID').val()),
        ItemTypeID : parseInt($('#ItemTypeID').val()),
        AvgPrice : parseInt($('#AvgPrice').val()),
        ItemCode : $('#ItemCode').val(),
        ItemName : $('#ddlItem option:selected').text(),
        SarmutID : parseInt($('#SpGroup').val()),
        DeptCode : $('#DeptCode').val(),
        KartuStock : 0,
        BudgetQty : parseInt($('#TotalBudget').val()),
        NoPol : '',
        IDKendaraan : 0,
        ProjectID : parseInt($('#ProjectID').val()),
        ProjectName : $('#ProjectName').val(),
        Zona : $('#ZonaMtc').val()
    }
    isiDetail.isiDetail1.push(isiDetail1);
    clearValue();
    $('#list-dtl').append(list);
    $('.the-loader').hide();
    $('#DllDept').attr('disabled','disabled'); 
    $('#Tanggal').attr('disabled','disabled'); 
    $('#BtnSimpan').removeAttr('disabled');
    $('.the-loader').hide();
});
function clearValue(){
    $('#TextBarang').val('');
    $('#ddlItem').html('');
    $('#Quantity').val('');
    $('#UOMDesc').val('');
    $('#ItemCode').val('');
    $('#Keterangan').val('');
    $('#Blocked').val('');
    $('#Stock').val('');

    $('#ItemID').val('');
    $('#UOMID').val('');
    $('#ItemTypeID').val('');
    $('#AvgPrice').val('');
    $('#Jumlah').val('');
    $('#ReOrder').val('');
    $('#StockOtherDept').val('');
    $('#ShowStock').val('');
    $('#PendingSPB').val('');
    $('#StockAkhir').val('');
    $('#Planning').val('');
    $('#CheckCost').val('');
    $('#MaxSPB').val('');
    $('#AddBudget').val('');
    $('#RuleBudget').val('');
    $('#TotalSPB').val('');
    $('#ItemIDKhusus').val('');
    $('#JumlahMaterial').val('');
    $('#TotalSPBPrj').val('');
    $('#DeptCode').val('');
    $('#ProjectID').val('');

    $('#PeriodeBudget').val('');
    $('#TotalBudget').val('');
    $('#TambahanBudget').val('');
    $('#TotalJumlahSPB').val('');
    $('#SisaBudget').val('');
    $('#TypeBudget').val('');
}
$('#BtnSimpan').click(function(){
    $('.the-loader').show();
    var isiHead={
        PakaiNo :'',
        PakaiDate :$('#Tanggal').val(),
        DeptID : parseInt($('#DllDept').val()),
        DepoID : 7,
        Status : 0,
        ApproveDate : '',
        ApproveBy : '',
        AlasanCansel : '',
        CreatedBy : '',
        CreatedTime : '',
        LastModifiedBy : '',
        LastModifiedTime : '',
        PakaiTipe : parseInt($('#GroupID').val()),
        ItemTypeID : 1,
        Ready : 0,
        JenisBiaya : 0,
        PlanningID : 0,
        CompanyID : 0
    }
    $.ajax({
        url: 'FormPakaiSparePart.aspx/InsertPakai', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({isiHead:isiHead,isiDetail:isiDetail}),
        success: function (data) {
            if (data.d != "" || data.d != null) {
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Sukses!',type: 'green',
                    content: 'Transaksi Spp Berhasil'
                });
                $('#PakaiNo').val(data.d);
                $('#DllDept').removeAttr('disabled');
                $('#Tanggal').removeAttr('disabled');
                $('#BtnSimpan').attr('disabled','disabled'); 
                isiDetail:[];
                isiHead='';
                $('.the-loader').hide();
            } else {
                $.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'green',
                    content: 'Data Gagal Disimpan',

                });
            }
        }
    })
});
function GoRefresh(){
 $('#DllDept').removeAttr('disabled');
 $('#Tanggal').removeAttr('disabled');
 $('#BtnSimpan').attr('disabled','disabled'); 
}
$('.BtnRefresh').click(function(){
    GoRefresh()
    clearValue();
    $('#list-dtl').html('');
});