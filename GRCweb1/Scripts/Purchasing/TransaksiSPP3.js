$('#Tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
    lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#panel-form').click(function(){
    $('.panel-form').show();$('.panel-list').hide();$('.the-title').text('Input Spp');
});
$('#panel-list').click(function(){
    $('.panel-list').show();$('.panel-form').hide();$('.the-title').text('List Spp');
});
var optionTypeMinta=`
<option value="1">Biasa</option>
<option value="2">Sesuai Schedule</option>
<option value="0">Top Urgent</option>
`;$('#TypeMinta').html(optionTypeMinta);

var optionTypeBiaya=`
<option value="">None</option>
<option value="1">Kompensasi</option>
<option value="2">Buat</option>
`;$('#TypeBiaya').html(optionTypeBiaya);

var isiDetail; isiDetail= { isiDtl:[] }

$('#datatable').on('click','.btn-data',function(){
    $('.the-loader').show();
    $('#list-dtl').html('');
    var id=$(this).attr('id');
    $.ajax({
        url: 'TransaksiSPP3.aspx/HeadData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({id:id}),
        success:function(output){
            $.each(output.d,function(i,b){
                $('#CreateUser').val(b.CreateUser);
                $('#HeadUser').val(b.HeadUser);
                $('#StatusSpp').val(b.StatusSpp);
                $('#ApprovalSpp').val(b.ApprovalSpp);
                $('#NoSpp').val(b.NoSpp);
                $('#Tanggal').val(b.Tanggal);
                $('#MultiGudangName').val(b.MultiGudangName);
                $('#TypeMinta').html(`<option value="">`+b.TypeMinta+`</option>`);
                $('#TypeItem').html(`<option value="">`+b.TypeItem+`</option>`);
                $('#TypeMinta').attr('disabled','disabled'); 
                $('#TypeItem').attr('disabled','disabled'); 
            });
        }
    })
    $.ajax({
        url: 'TransaksiSPP3.aspx/DtlData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({id:id}),
        success:function(output){
            var row=``;
            $.each(output.d,function(i,b){
                row+=`
                <tr>
                <td>`+b.ItemCode+`</td>
                <td>`+b.ItemName+`</td>
                <td>`+b.Quantity+`</td>
                <td>`+b.Satuan+`</td>
                <td>`+b.Keterangan+`</td>
                <td>`+b.TglKirim+`</td>
                <td>Action</td>
                </tr>
                `
            });
            $('#list-dtl').html(row);
            $('.the-loader').hide();
        }
    })
    $('.panel-form').show();$('.panel-list').hide();
});


$('#BtnSearchData').click(function(){
    $('.the-loader').show();
    $('#datatable').DataTable().clear().draw();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({
            NoSPP:$('#list-NoSpp').val(),
            CreatedBy:$('#list-CreatedBy').val()
        }),
        success:function(output){
            var row=[];
            $.each(output.d,function(i,b){
                var Status='Open';
                if(b.Status==1){Status='Parsial';}
                if(b.Status==2){Status='FullPo';}
                var Approval='Admin';
                if(b.Approval==1){Approval='Head';}
                if(b.Approval==2){Approval='Manager';}
                if(b.Approval==3){Approval='Pm';}
                var Aksi=`<label class="btn btn-primary btn-xs btn-data" id="`+b.ID+`"><i class="fa fa-edit"></i></label>`;
                var Qty=b.Quantity+' '+b.UOMDesc;
                row.push([Aksi,b.NoSPP,b.Minta,b.ItemCode,b.ItemName,Qty,b.Keterangan,Status,Approval]);
            });
            $('#datatable').DataTable().rows.add(row).draw();
            $('.the-loader').hide();
        }
    })
});
$('#BtnClearData').click(function(){
    $('#datatable').DataTable().clear().draw();
});
function InfoUser() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/InfoUser',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.DeptName+`</option>`;
            });
            $('#DeptUser').html(row);
            $('.the-loader').hide();
        }
    })
}InfoUser();
function ListGroupSarmut() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListGroupSarmut',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Group Sarmut</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.ZonaName+`</option>`;
            });
            $('#GroupSarmut').html(row);
            $('.the-loader').hide();
        }
    })
}ListGroupSarmut();
function ListGroupAsset() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListGroupAsset',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Group Asset</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.NamaGroup+`</option>`;
            });
            $('#GroupAsset').html(row);
            $('.the-loader').hide();
        }
    })
}ListGroupAsset();
function ListForklif() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListForklif',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Forklif</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.Forklift+`</option>`;
            });
            $('#Forklif').html(row);
            $('.the-loader').hide();
        }
    })
}ListForklif();
function ListGroupEfisien() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListGroupEfisien',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Group Efisien</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.GroupName+`</option>`;
            });
            $('#GroupEfisien').html(row);
            $('.the-loader').hide();
        }
    })
}ListGroupEfisien();
function ListTypeItem() {
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListTypeItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        success: function (uotput) {
            var row = '<option value="0">pilihan Type Item</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.TypeDescription+`</option>`;
            });
            $('#TypeItem').html(row);
            $('.the-loader').hide();
        }
    })
}ListTypeItem();
$('#GroupAsset').change(function(){
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListKelasAsset',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({GroupAsset:$('#GroupAsset').val()}),
        success: function (uotput) {
            var row = '<option value="0">pilihan Kelas Asset</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.NamaClass+`</option>`;
            });
            $('#KelasAsset').html(row);
            $('.the-loader').hide();
        }
    })
});
$('#TypeItem').change(function(){
    $('.the-loader').show();
    if($('#TypeItem').val()==2){
        $('#row-GroupSarmut').hide();
        $('#row-GroupEfisien').hide();
    }else{
        $('#row-GroupSarmut').show();
        $('#row-GroupEfisien').show();
    }
    if($('#TypeItem').val()==3){
        $('#row-TypeBiaya').show();
        $('#row-KetBiaya').show();
        $('#NameItem').attr('disabled','disabled');$('#BtnSearchItem').attr('disabled','disabled'); 
        if($('#DeptUser').val()==26){
            $.ajax({
                url: 'TransaksiSPP3.aspx/ListNoPol',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
                success: function (uotput) {
                    var row = '<option value="0">pilihan NoPol</option>';
                    $.each(uotput.d, function (i, b) {
                        row+=`<option value="`+b.ID +`">`+b.KendaraanNo+` - `+b.Type+`</option>`;
                    });
                    $('#NoPol').html(row);
                    $('.the-loader').hide();
                }
            })
        }else{
            $('#NoPol').html('<option value="0"></option>');
        }
    }else{
        $('#row-TypeBiaya').hide();
        $('#row-KetBiaya').hide();
        $('#NameItem').removeAttr('disabled');$('#BtnSearchItem').removeAttr('disabled');
    }
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListTypeSpp',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({ItemTypeID:$('#TypeItem').val()}),
        success: function (uotput) {
            var row = '<option value="0">pilihan Type Spp</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID +`">`+b.GroupDescription+`</option>`;
            });
            $('#TypeSpp').html(row);
            $('.the-loader').hide();
        }
    })
});
function ListAsset(TypeSpp){
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListAsset',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({TypeSpp:TypeSpp}),
        success: function (uotput) {
            var row = '<option value="0">pilihan Asset Item</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.KodeProjectAsset +`">`+b.NamaProjectAsset+`</option>`;
            });
            $('#AssetItem').html(row);
        }
    })
}
$('#TypeSpp').change(function(){
    if($('#TypeSpp')!=''){
        $('#TypeMinta').attr('disabled','disabled'); 
        $('#TypeItem').attr('disabled','disabled');
    }
    var TypeSpp=$('#TypeSpp').val();
    var DeptUser=$('#DeptUser').val();
    if(TypeSpp==4 && DeptUser==22 || TypeSpp==4 && DeptUser==30){
        $('#row-AssetItem').show();
        ListAsset(TypeSpp);
        $('#NameItem').attr('disabled','disabled');$('#BtnSearchItem').attr('disabled','disabled'); 
    }else if(TypeSpp==12){
        $('#row-AssetItem').show();
        ListAsset(TypeSpp);
        $('#NameItem').attr('disabled','disabled');$('#BtnSearchItem').attr('disabled','disabled');
    }else if(TypeSpp==5){
        $('#row-AssetItem').hide();
        $('#NameItem').attr('disabled','disabled');$('#BtnSearchItem').attr('disabled','disabled');
    }else{
        $('#row-AssetItem').hide();
        $('#AssetItem').html('');
        $('#NameItem').removeAttr('disabled');$('#BtnSearchItem').removeAttr('disabled');
    }
    if($('#TypeItem').val()==3){
        $.ajax({
            url: 'TransaksiSPP3.aspx/DataBiaya',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
            data : JSON.stringify({TypeSpp:TypeSpp}),
            success: function (uotput) {
                var row = '<option value="0">pilihan Biaya Item</option>';
                $.each(uotput.d, function (i, b) {
                    row+=`<option value="`+b.ID+`">`+b.ItemName+` (`+b.ItemCode+`)</option>`;
                });
                $('#ItemID').html(row);
            }
        })
    }
});
$('#AssetItem').change(function(){
    $('.the-loader').show();
    if($('#AssetItem')!=''){
        $('#row-NameItem').hide();
        $.ajax({
            url: 'TransaksiSPP3.aspx/ListAssetKomponen',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
            data : JSON.stringify({AssetItem:$('#AssetItem').val(), TypeSpp:$('#TypeSpp').val()}),
            success: function (uotput) {
                var row = '<option value="0">pilihan Name Item</option>';
                $.each(uotput.d, function (i, b) {
                    row+=`<option value="`+b.ID +`">`+b.ItemName+`</option>`;
                });
                $('#ItemID').html(row);
                $('.the-loader').hide();
            }
        })
    }else{$('#row-NameItem').show();}
});
$('#BtnSearchItem').click(function(){
    $('.the-loader').show();
    if($('#TypeItem').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Type Itemnya'
        });return false;
    }
    if($('#TypeSpp').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Type Sppnya'
        });return false;
    }
    $.ajax({
        url: 'TransaksiSPP3.aspx/ListItem', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({NameItem:$("#NameItem").val(), TypeItem:$('#TypeItem').val(), TypeSpp:$('#TypeSpp').val()}),
        success: function (uotput) {
            var row = '<option value="0">pilhan Name Item</option>';
            $.each(uotput.d, function (i, b) {
                row+=`<option value="`+b.ID+`">`+b.ItemName+` (`+b.ItemCode+`)</option>`;
            });
            $('#ItemID').html(row);
            $('.the-loader').hide();
        }
    })
});
$('#ItemID').change(function(){
    $('.the-loader').show();
    $('#list-dead').html('');
    $('#table-dead').hide();
    $.ajax({
        url: 'TransaksiSPP3.aspx/LoadDeadStockLocal', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({
            item:$("#ItemID").val(), 
            Tanggal:$('#Tanggal').val()
        }),
        success: function (uotput) {
            var row='';
            $.each(uotput.d, function (i, b) {
                row=`
                <tr>
                <td>`+b.Plant+`</td>
                <td>`+b.ItemCode+`</td>
                <td>`+b.Stock+`</td>
                </tr>
                `;
                $('#list-dead').append(row);
                if(b.Plant!=''){$('#table-dead').show();}
            });
            $('.the-loader').hide();
        }
    })
    $.ajax({
        url: 'TransaksiSPP3.aspx/LoadDeadStock1', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({
            item:$("#ItemID").val(), 
            Tanggal:$('#Tanggal').val()
        }),
        success: function (uotput) {
            var row='';
            $.each(uotput.d, function (i, b) {
                row=`
                <tr>
                <td>`+b.Plant+`</td>
                <td>`+b.ItemCode+`</td>
                <td>`+b.Stock+`</td>
                </tr>
                `;
                $('#list-dead').append(row);
                if(b.Plant!=''){$('#table-dead').show();}
            });
            $('.the-loader').hide();
        }
    })
    $.ajax({
        url: 'TransaksiSPP3.aspx/LoadDeadStock2', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({
            item:$("#ItemID").val(), 
            Tanggal:$('#Tanggal').val()
        }),
        success: function (uotput) {
            var row='';
            $.each(uotput.d, function (i, b) {
                row=`
                <tr>
                <td>`+b.Plant+`</td>
                <td>`+b.ItemCode+`</td>
                <td>`+b.Stock+`</td>
                </tr>
                `;
                $('#list-dead').append(row);
                if(b.Plant!=''){$('#table-dead').show();}
            });
            $('.the-loader').hide();
        }
    })
    var Qty=$('#Qty').val();if($('#Qty').val()==''){Qty=0;}
    $.ajax({
        url: 'TransaksiSPP3.aspx/InfoItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({
            ItemID:$('#ItemID').val(),
            ItemName:$('#ItemID option:selected').text(),
            TypeItem:$('#TypeItem').val(), 
            TypeItemName:$('#TypeItem option:selected').text(),
            TypeSpp:$('#TypeSpp').val(), 
            TypeMinta:$('#TypeMinta').val(), 
            NameItem:$("#NameItem").val(),
            Qty:Qty,
            Keterangan:$('#Keterangan').val(),
            Tanggal:$('#Tanggal').val()
        }),
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                if(b.Msg!=''){
                    $.alert({
                        icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                        content: b.Msg
                    });return false;
                }else{
                    $('#ItemCode').val(b.ItemCode);
                    $('#StockItem').val(b.StockItem);
                    $('#MaxStock').val(b.MaxStock);
                    $('#Satuan').val(b.Satuan);
                    $('#UOMID').val(b.UOMID);
                    $('#JenisStock').val(b.JenisStock);
                    var MultiGudangName='Private'; if(b.MultiGudang==1){MultiGudangName='Public';}
                    $('#MultiGudang').val(b.MultiGudang);
                    $('#MultiGudangName').val(MultiGudangName);
                    $('#LastPo').val(b.LastPo);
                    $('#LastRms').val(b.LastRms);
                    $('#TanggalKirim').val(b.TanggalKirim);
                    $('#LeadTime').val(b.LeadTime);
                }
            });
            $('.the-loader').hide();
        }
    })
});
$('#Keterangan').autocomplete({
    source: function (request, response) {
        $('#BiayaID').val('');
        var Qty=$('#Qty').val();if($('#Qty').val()==''){Qty=0;}
        $.ajax({
            url: 'TransaksiSPP3.aspx/ListBiaya', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            data : JSON.stringify({
                ItemID:$('#ItemID').val(), 
                ItemName:$('#ItemID option:selected').text(), 
                TypeItem:$('#TypeItem').val(),
                Qty:Qty,
                Keterangan:$('#Keterangan').val()
            }),
            success: function (data) {
                response(data.d);
            },
            error: function (jqXHR, exception) {
            }
        });
    },
    select: function (event, ui) {
        $('#Keterangan').val(ui.item.ItemName);
        $('#BiayaID').val(ui.item.ID);
        if(ui.item.ID!='' || ui.item.ID!=null){ItemBiaya(ui.item.ID);}
        return false;
    }
}).data('ui-autocomplete')._renderItem = function (ul, item) {
    return $("<li>").append("<a>" + item.ItemName + "</a></li>").appendTo(ul);
};
function ItemBiaya(ID) {
    $.ajax({
        url: 'TransaksiSPP3.aspx/ItemBiaya', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({ItemID:ID, TypeMinta:$('#TypeMinta').val(), Tanggal:$('#Tanggal').val()}),
        success: function (uotput) {
            $.each(uotput.d, function (i, b) {
                $('#StockItem').val(b.StockItem);
                $('#MaxStock').val(b.MaxStock);
                $('#Satuan').val(b.Satuan);
                $('#UOMID').val(b.SatuanID);
                $('#TanggalKirim').val(b.TanggalKirim);
            });
        }
    });
}
$('#GroupSarmut').change(function(){
    $('.the-loader').show();
    if($('#GroupSarmut').val()==14){
        $('#row-Forklif').show();
    }else{
        $('#row-Forklif').hide();
    }
    $('.the-loader').hide();
});
$('#BtnAddItem').click(function(){
    $('.the-loader').show();
    if($('#table-dead').is(':visible')){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Tidak bisa SPP karena item termasuk barang dead stock'
        });return false;
    }
    if($('#TypeItem').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Type Itemnya'
        });return false;
    }
    if($('#TypeSpp').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Pilih Dulu Type Sppnya'
        });return false;
    }
    if($('#Qty').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Quantity Tidak Boleh Kosong'
        });return false;
    }
    if($('#Keterangan').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'Keterangan Tidak Boleh Kosong'
        });return false;
    }
    if($('#ItemID').val()==''){
        $('.the-loader').hide();$.alert({
            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
            content: 'ItemName Tidak Boleh Kosong'
        });return false;
    }
    if($('#TypeItem').val()==3){
        if($('#Keterangan').val()==''){
            $('.the-loader').hide();$.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Untuk SPP type Biaya keterangan harus di isi dengan uraian penggunaan biaya'
            });return false;
        }
        if($('#KetBiaya').val()==''){
            $('.the-loader').hide();$.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Keterangan Biaya Harus Di isi'
            });return false;
        }
        if($('#DeptUser').val()==26 && $('#Nopol').val()==0){
            $('.the-loader').hide();$.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Nomor Polisi Harus Di isi'
            });return false;
        }
        $.ajax({
            url: 'TransaksiSPP3.aspx/PurchnTools', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            success: function (uotput) {
                $.each(uotput.d, function (i, b) {
                    var AutoSave=b.Status;
                    if($('#BiayaID').val()=='' && AutoSave==0){
                        $('.the-loader').hide();$.alert({
                            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                            content: 'Keterangan Biaya belum terdaftar di sistem'
                        });return false;
                    }else if($('#BiayaID').val()=='' && AutoSave==1){
                        $.ajax({
                            url: 'TransaksiSPP3.aspx/InputBiaya', type: 'POST', 
                            contentType: 'application/json; charset=utf-8', dataType: 'json',
                            data : JSON.stringify({
                                Keterangan:$('#Keterangan').val(),
                                ItemCode:$('#ItemCode').val()
                            }),
                            success: function (uotput1) {
                                $.each(uotput1.d, function (i1, b1) {
                                    if(b1.Msg!=''){
                                        $.alert({
                                            icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                                            content: b1.Msg
                                        });return false;
                                    }
                                })
                            }
                        })
                    }
                })
            }
        })
        if($('#GroupSarmut').val()==0){
            $('.the-loader').hide();$.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Group Sarmut Tidak Boleh Kosong'
            });return false;
        }
        if($('#GroupEfisien').val()==0){
            $('.the-loader').hide();$.alert({
                icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                content: 'Group Efisien Tidak Boleh Kosong'
            });return false;
        }
        if($('#GroupSarmut').val()==14){
            $('#row-Forklif').show();
            if($('#Forklif').val()==''){
                $('.the-loader').hide();$.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: 'Forklif Tidak Boleh Kosong'
                });return false;
            }
        }else{
            $('#row-Forklif').hide();
        }
    }
    var KelasAsset=$('#KelasAsset').val();if($('#KelasAsset').val()==null){KelasAsset=0;}
    var SubKelasAsset=$('#SubKelasAsset').val();if($('#SubKelasAsset').val()==null){SubKelasAsset=0;}
    var LokasiAsset=$('#LokasiAsset').val();if($('#LokasiAsset').val()==null){LokasiAsset=0;}
    var UmurEkonomis=$('#UmurEkonomis').val();if($('#UmurEkonomis').val()==null){UmurEkonomis=0;}
    var Qty=$('#Qty').val();if($('#Qty').val()==''){Qty=0;}
    var Forklif=$('#Forklif').val();if($('#Forklif').val()==null){Forklif=0;}
    var TypeBiaya=$('#TypeBiaya').val();if($('#TypeBiaya').val()==null){TypeBiaya='';}
    $.ajax({
        url: 'TransaksiSPP3.aspx/CekAddItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
        data : JSON.stringify({item:$('#ItemID').val()}),
        success: function (output) {
            if(output.d!=''){
                $('.the-loader').hide();$.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                    content: output.d
                });return false;
            }else{
                $.ajax({
                    url: 'TransaksiSPP3.aspx/AddItem', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
                    data : JSON.stringify({
                        TypeItem:$('#TypeItem').val(),
                        TypeSpp:$('#TypeSpp').val(),
                        ItemID:$('#ItemID').val(),
                        ItemName:$('#ItemID option:selected').text(),
                        Qty:Qty,
                        Keterangan:$('#Keterangan').val(),
                        UomID:$('#UOMID').val(),
                        NoPol:$('#NoPol').val(),
                        NoPolName:$('#NoPol option:selected').text(),
                        Forklif:Forklif,
                        TanggalKirim:$('#TanggalKirim').val(),
                        TypeBiaya:TypeBiaya,
                        KetBiaya:$('#KetBiaya').val(),
                        ItemCode:$('#ItemCode').val(),
                        AssetItemName:$('#AssetItem option:selected').text(),
                        GroupAsset:$('#GroupAsset').val(),
                        KelasAsset:KelasAsset,
                        SubKelasAsset:SubKelasAsset,
                        LokasiAsset:LokasiAsset,
                        GroupSarmut:$('#GroupSarmut').val(),
                        GroupEfisien:$('#GroupEfisien').val(),
                        UmurEkonomis:UmurEkonomis
                    }),
                    success: function (uotput) {
                        $.each(uotput.d, function (i, b) {
                            if(b.Msg!=''){
                                $.alert({
                                    icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
                                    content: b.Msg
                                });return false;
                            }else{
                                isiDtl={
                                    NoPol:b.NoPol,
                                    ItemID:b.ItemID,
                                    GroupID:b.GroupID,
                                    Quantity:b.Quantity,
                                    ItemTypeID:b.ItemTypeID,
                                    UOMID:b.UOMID,
                                    ItemCode:b.ItemCode,
                                    ItemName:b.ItemName,
                                    Satuan:b.Satuan,
                                    QtyPO:b.QtyPO,
                                    Keterangan:b.Keterangan,
                                    TglKirim:b.TglKirim1,
                                    TypeBiaya:b.TypeBiaya,
                                    Keterangan1:b.Keterangan1,
                                    AmGroupID:b.AmGroupID,
                                    AmClassID:b.AmClassID,
                                    AmSubClassID:b.AmSubClassID,
                                    AmLokasiID:b.AmLokasiID,
                                    MTCGroupSarmutID:b.MTCGroupSarmutID,
                                    MaterialMTCGroupID:b.MaterialMTCGroupID,
                                    UmurEkonomis:b.UmurEkonomis
                                }
                                isiDetail.isiDtl.push(isiDtl);
                                var list=`
                                <tr>
                                <td>`+b.ItemCode+`</td>
                                <td>`+b.ItemName+`</td>
                                <td>`+b.Quantity+`</td>
                                <td>`+b.Satuan+`</td>
                                <td>`+b.Keterangan+`</td>
                                <td>`+b.TglKirim1+`</td>
                                <td>action</td>
                                </tr>
                                `;
                                $('#list-dtl').append(list);
                                $('#DeptUser').attr('disabled','disabled'); 
                                $('#Tanggal').attr('disabled','disabled'); 
                                $('#BtnSimpan').removeAttr('disabled');
                                ClearValue();
                                $('#TypeMinta').attr('disabled','disabled'); 
                                $('#TypeItem').attr('disabled','disabled'); 
                                $('#TypeSpp').attr('disabled','disabled'); 
                                $('.the-loader').hide();
                            }
                        });
                    }
                })
}
}
})
});
function ClearArrayList(){
    $('.the-loader').show();
    $.ajax({
        url: 'TransaksiSPP3.aspx/ClearArrayList', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        success: function (data) {
            $('.the-loader').hide();
        }
    })
}ClearArrayList();
$('.BtnRefresh').click(function(){
    GoRefresh();
    ClearArrayList();
    isiDetail= { isiDtl:[] }
});
function GoRefresh(){
    $('#TypeMinta').html(optionTypeMinta);
    $('#TypeBiaya').html(optionTypeBiaya);
    InfoUser();
    ListTypeItem();

    $('#row-GroupSarmut').show();
    $('#row-GroupEfisien').show();
    $('#row-NameItem').show();
    $('#row-TypeBiaya').hide();
    $('#row-KetBiaya').hide();
    $('#row-AssetItem').hide();
    $('#row-Forklif').hide();

    $('#NameItem').removeAttr('disabled');$('#BtnSearchItem').removeAttr('disabled');
    $('#TypeMinta').removeAttr('disabled');
    $('#TypeItem').removeAttr('disabled');
    $('#DeptUser').removeAttr('disabled');
    $('#TypeMinta').removeAttr('disabled');
    $('#TypeItem').removeAttr('disabled');
    $('#TypeSpp').removeAttr('disabled');
    $('#BtnAddItem').removeAttr('disabled');

    $('#BtnSimpan').attr('disabled','disabled');
    $('#Tanggal').attr('disabled','disabled');

    $('#KelasAsset').html('');
    $('#TypeSpp').html('');
    $('#AssetItem').html('');
    $('#ItemID').html('');
    $('#list-dtl').html('');

    ClearValue();
}
function ClearValue(){
    ListGroupSarmut();
    ListGroupAsset();
    ListForklif();
    ListGroupEfisien();
    ListAsset($('#TypeSpp').val());
    $('#NoSpp').val('');
    $('#NameItem').val('');
    $('#ItemCode').val('');
    $('#StockItem').val('');
    $('#MaxStock').val('');
    $('#JenisStock').val('');
    $('#Satuan').val('');
    $('#UOMID').val('');
    $('#MultiGudangName').val('');
    $('#LastPo').val('');
    $('#LastRms').val('');
    $('#LeadTime').val('');
    $('#TanggalKirim').val('');
    $('#BiayaID').val('');
    $('#Keterangan').val('');
    $('#KetBiaya').val('');
    $('#Qty').val('');
    $('#ItemID').html('');
    if($('#TypeItem').val()==3){
        $.ajax({
            url: 'TransaksiSPP3.aspx/DataBiaya',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
            data : JSON.stringify({TypeSpp:$('#TypeSpp').val()}),
            success: function (uotput) {
                var row = '<option value="0">pilihan Biaya Item</option>';
                $.each(uotput.d, function (i, b) {
                    row+=`<option value="`+b.ID+`">`+b.ItemName+` (`+b.ItemCode+`)</option>`;
                });
                $('#ItemID').html(row);
            }
        })
    }

    $('#CreateUser').val('');
    $('#HeadUser').val('');
    $('#StatusSpp').val('');
    $('#ApprovalSpp').val('');
}
$('#BtnSimpan').click(function(){
    $('.the-loader').show();
    var isiHead={
        TanggalInput:$('#Tanggal').val(),
        TypeItem:$('#TypeItem').val(),
        TypeSpp:$('#TypeSpp').val(),
        MultiGudang:$('#MultiGudang').val(),
        TypeMinta:$('#TypeMinta').val()
    }
    $.ajax({
        url: 'TransaksiSPP3.aspx/SaveData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data : JSON.stringify({isiHead:isiHead,isiDetail:isiDetail}),
        success: function (data) {
            if (data.d!='') {
                $.alert({
                    icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
                    content: 'Transaksi Spp Berhasil'
                });
                $('#NoSpp').val(data.d);
                $('#DllDept').removeAttr('disabled');
                $('#BtnSimpan').attr('disabled','disabled'); 
                $('#BtnAddItem').attr('disabled','disabled'); 
                isiDetail= { isiDtl:[] }
                isiHead='';
                $('.the-loader').hide();
            } else {
                $('.the-loader').hide();$.alert({
                    icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
                    content: 'Data Gagal Disimpan',

                });
            }
        }
    })
});
