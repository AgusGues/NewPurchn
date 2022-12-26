$('#TanggalPo').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#data-pending').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#panel-form').click(function(){
	$('.panel-form').show();$('.panel-list').hide();$('.panel-uangmuka').hide();$('.panel-spp').hide();$('.panel-pending').hide();
	$('.the-title').text('Input Po');
});
$('#panel-list').click(function(){
	$('.panel-list').show();$('.panel-form').hide();$('.panel-uangmuka').hide();$('.panel-spp').hide();$('.panel-pending').hide();
	$('.the-title').text('List Po');
});
$('#panel-uangmuka').click(function(){
	$('.panel-uangmuka').show();$('.panel-form').hide();$('.panel-list').hide();$('.panel-spp').hide();$('.panel-pending').hide();
	$('.the-title').text('Uang Muka');
});
$('#panel-spp').click(function(){
	$('.panel-spp').show();$('.panel-form').hide();$('.panel-list').hide();$('.panel-uangmuka').hide();$('.panel-pending').hide();
	$('.the-title').text('List Spp');
});
$('#panel-pending').click(function(){
	$('.panel-pending').show();$('.panel-form').hide();$('.panel-list').hide();$('.panel-uangmuka').hide();$('.panel-spp').hide();
	$('.the-title').text('List Pending');
});
$('#NoSj').html('<option value=""></option>');
var optionTermOfDelivery=`
<option value="-">-</option>
<option value="FRANCO">FRANCO</option>
<option value="LOCO">LOCO</option>
`;$('#TermOfDelivery').html(optionTermOfDelivery);
var optionBayarCash=`
<option value="0">Cash</option>
<option value="1">Kredit</option>
`;
var optionBayarKredit=`
<option value="1">Kredit</option>
<option value="0">Cash</option>
`;$('#Bayar').html(optionBayarCash);
var optionBarang=`
<option value="0">Local</option>
<option value="1">Import</option>
`;$('#Barang').html(optionBarang);

var isiDetail; isiDetail= { isiDtl:[] }; isiKadar= { isiKdr:[] };

$('#datatable').on('click','.btn-data',function(){
	$('.the-loader').show();
	$('#list-dtl').html('');
	var id=$(this).attr('id');
	$.ajax({
		url: 'FormPoPurchn.aspx/HeadData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({id:id}),
		success:function(output){
			$.each(output.d,function(i,b){
				$('#NoPo').val(b.NoPo);
				$('#TermOfPay').html('<option value="">'+b.TermOfPay+'</option>');
				$('#Bayar').html('<option value="">'+b.Bayar+'</option>');
				$('#TermOfDelivery').html('<option value="">'+b.TermOfDelivery+'</option>');
				$('#TanggalPo').val(b.TanggalPo);
				$('#SupplierName').val(b.SupplierName);
				$('#UPSupplier').text(b.UPSupplier);
				$('#TelpSupplier').text(b.TelpSupplier);
				$('#FaxSupplier').text(b.FaxSupplier);
				$('#MataUang').html('<option value="">'+b.MataUang+'</option>');
				$('#PPN').val(b.PPN);
				$('#PPH').val(b.PPH);
				$('#Discount').val(b.Discount);
				$('#OngkosKirim').val(b.OngkosKirim);

				$('#NoPo').attr('disabled','disabled'); 
				$('#TermOfPay').attr('disabled','disabled'); 
				$('#Bayar').attr('disabled','disabled'); 
				$('#TermOfDelivery').attr('disabled','disabled'); 
				$('#TanggalPo').attr('disabled','disabled'); 
				$('#SupplierName').attr('disabled','disabled'); 
				$('#UPSupplier').attr('disabled','disabled'); 
				$('#TelpSupplier').attr('disabled','disabled'); 
				$('#FaxSupplier').attr('disabled','disabled'); 
				$('#MataUang').attr('disabled','disabled'); 
				$('#PPN').attr('disabled','disabled'); 
				$('#PPH').attr('disabled','disabled'); 
				$('#Discount').attr('disabled','disabled'); 
				$('#OngkosKirim').attr('disabled','disabled'); 
			});
		}
	})
	$.ajax({
		url: 'FormPoPurchn.aspx/GettotalPrice',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({id:id}),
		success:function(output){
			$('#TotalPrice').val(output.d);
			$('#TotalPrice').attr('disabled','disabled'); 
		}
	})
	$.ajax({
		url: 'FormPoPurchn.aspx/DtlData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({id:id}),
		success:function(output){
			var row=``;
			$.each(output.d,function(i,b){
				row+=`
				<tr>
				<td>`+b.NoSpp+`</td>
				<td>`+b.ItemCode+`</td>
				<td>`+b.ItemName+`</td>
				<td>`+b.Quantity+`</td>
				<td>`+b.Satuan+`</td>
				<td>`+b.Harga+`</td>
				<td>`+b.DlvDate+`</td>
				<td>Action</td>
				</tr>
				`
			});
			$('#list-dtl').html(row);
			$('.the-loader').hide();
		}
	})
	$('.panel-form').show();$('.panel-list').hide();$('.panel-uangmuka').hide();$('.panel-spp').hide();
});

$('#BtnSearchData').click(function(){
	$('.the-loader').show();
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			NoPo:$('#list-NoPo').val(),
			NoSpp:$('#list-NoSpp').val(),
			CreatedBy:$('#list-CreatedBy').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				var Aksi=`<label class="btn btn-primary btn-xs btn-data" id="`+b.ID+`"><i class="fa fa-edit"></i></label>`;
				var Qty=b.Qty+' '+b.UomDesc;
				row.push([Aksi,b.NoPo,b.NoSpp,b.TanggalPo,b.ItemCode,b.ItemName,Qty,b.Price]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('#BtnClearData').click(function(){
	$('#datatable').DataTable().clear().draw();
});
function ListNoSpp() {
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListNoSpp',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (uotput) {
			var row = '<option value="">Pilihan NoSpp</option>';
			$.each(uotput.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.NoSpp+`</option>`;
			});
			$('#NoSpp').html(row);
			$('.the-loader').hide();
		}
	})
}ListNoSpp();
$('#NoSpp').change(function(){
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListSppDetailBySppId',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({SppId:$('#NoSpp').val()}),
		success: function (uotput) {
			var row = '<option value="">Pilihan ItemSpp</option>';
			$.each(uotput.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.ItemName+`</option>`;
			});
			$('#ItemSpp').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#ItemSpp').change(function(){
	$('.the-loader').show();
	if($('#ItemSpp').val()!=''){
		$.ajax({
			url: 'FormPoPurchn.aspx/InfoItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				SppId:$('#NoSpp').val(),
				Id:$('#ItemSpp').val(),
				IdName:$('#ItemSpp option:selected').text()
			}),
			success: function (uotput) {
				$.each(uotput.d, function (i, b) {
					$('#ItemTypeID').val(b.ItemTypeID);
					$('#ItemID').val(b.ItemID);
					$('#ItemCode').text(b.ItemCode);
					$('#ItemName').text($('#ItemSpp option:selected').text());
					$('#GroupID').val(b.GroupID);
					$('#Harga').val(b.Harga);
					$('#Qty').val(b.Qty);
					$('#LeadTime').text(b.LeadTime);
					$('#DeliveryDate').text(b.DeliveryDate);
					$('#UOMID').val(b.UOMID);
					$('#Satuan').text(b.SatuanName);
					$('#HargaTerendah').text(b.Msg);
				});
				$('.the-loader').hide();
			}
		})
	}	
});
$('#SupplierName').autocomplete({
	source: function (request, response) {
		$('#SupplierID').val('');
		$.ajax({
			url: 'FormPoPurchn.aspx/ListSup', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
			data : JSON.stringify({SupplierName:$('#SupplierName').val()}),
			success: function (data) {
				response(data.d);
			},
			error: function (jqXHR, exception) {
			}
		});
	},
	select: function (event, ui) {
		$('#SupplierName').val(ui.item.SupplierName);
		$('#SupplierID').val(ui.item.ID);
		if(ui.item.ID!='' || ui.item.ID!=null){InfoSup(ui.item.ID,ui.item.SupplierName);}
		return false;
	}
}).data('ui-autocomplete')._renderItem = function (ul, item) {
	return $("<li>").append("<a>" + item.SupplierName + "</a></li>").appendTo(ul);
};
function DisableInfoKadarAir(){
	$('#NoSj').attr('disabled','disabled');
	$('#NoMobil').attr('disabled','disabled');
	$('#StdKa').attr('disabled','disabled');
	$('#Gross').attr('disabled','disabled');
	$('#KadarAir').attr('disabled','disabled');
	$('#Sampah').attr('disabled','disabled');
	$('#Netto').attr('disabled','disabled');
}DisableInfoKadarAir();
function EnableInfoKadarAir(){
	$('#NoSj').removeAttr('disabled');
	$('#NoMobil').removeAttr('disabled');
	$('#StdKa').removeAttr('disabled');
	$('#Gross').removeAttr('disabled');
	$('#KadarAir').removeAttr('disabled');
	$('#Sampah').removeAttr('disabled');
	$('#Netto').removeAttr('disabled');
}
function ValueDefaultInfoKadarAir(){
	$('#NoMobil').val('');
	$('#StdKa').val(0);
	$('#Gross').val(0);
	$('#KadarAir').val(0);
	$('#Sampah').val(0);
	$('#Netto').val(0);
}ValueDefaultInfoKadarAir();
function InfoSup(ID,SupplierName) {
	$('.the-loader').show();
	DisableInfoKadarAir();
	ValueDefaultInfoKadarAir();
	var SupId=ID;
	$.ajax({
		url: 'FormPoPurchn.aspx/InfoSup', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({ID:SupId,SupplierName:SupplierName,ItemCode:$('#ItemCode').text()}),
		success: function (uotput) {
			$.each(uotput.d, function (i, b) {
				$('#UPSupplier').text(b.UPSupplier);
				$('#TelpSupplier').text(b.TelpSupplier);
				$('#FaxSupplier').text(b.FaxSupplier);
				$('#FaxSupplier').text(b.FaxSupplier);
				$('#Harga').val(b.Harga);
				$('#PPN').val(b.PPN);
				if(b.PPN==0){$('#PPN').removeAttr('disabled');}else{$('#PPN').attr('disabled','disabled');}
				$('#SubCompanyID').val(b.SubCompanyID);
				var optionMataUang=`<option value="`+b.MataUang +`">`+b.MataUangName+`</option>`;
				var optionTermOfPay=`<option value="`+b.TermOfPayID +`">`+b.TermOfPay+`</option>`;
				if(b.MataUang!=''){
					$.ajax({
						url: 'FormPoPurchn.aspx/ListMataUang',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
						data : JSON.stringify({ID:b.MataUang}),
						success: function (uotput1) {
							$.each(uotput1.d, function (i1, b1) {
								optionMataUang+=`<option value="`+b1.ID +`">`+b1.Lambang+`</option>`;
							});
							$('#MataUang').html(optionMataUang);
						}
					})
				}
				if(b.TermOfPayID!=''){
					$.ajax({
						url: 'FormPoPurchn.aspx/ListTermOfPay',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
						data : JSON.stringify({ID:b.TermOfPayID}),
						success: function (uotput1) {
							$.each(uotput1.d, function (i1, b1) {
								optionTermOfPay+=`<option value="`+b1.ID +`">`+b1.TermPay+`</option>`;
							});
							$('#TermOfPay').html(optionTermOfPay);
						}
					})
				}
				if(b.TermOfPayID==null ||b.TermOfPayID==0 || b.TermOfPayID==6 || b.TermOfPayID==8){
					$('#Bayar').html(optionBayarCash);
				}else{
					$('#Bayar').html(optionBayarKredit);
				}
				if(b.PosKertas > -1){
					if(b.SubCompanyID==5 || b.ForDK==5){
						$('#NoSj').removeAttr('disabled');
						$.ajax({
							url: 'FormPoPurchn.aspx/ListDlvKertas',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
							data : JSON.stringify({ItemCode: $('#ItemCode').text(),SupId: SupId}),
							success: function (uotput1) {
								var row='<option value="">Pilihan SjDepo</option>';						
								$.each(uotput1.d, function (i1, b1) {
									row+=`<option value="`+b1.ID +`">`+b1.NoSj+`</option>`;
								});
								$('#NoSj').html(row);
							}
						})
					}
				}
			});
			$('.the-loader').hide();
		}
	});
}
$('#NoSj').change(function(){
	if($('#NoSj').val()!=''){
		EnableInfoKadarAir();
		$.ajax({
			url: 'FormPoPurchn.aspx/InfoKadarAir',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({Id:$('#NoSj').val()}),
			success: function (uotput1) {
				$.each(uotput1.d, function (i1, b1) {
					$('#StdKa').val(b.StdKa);
					$('#Gross').val(b.Gross);
					$('#KadarAir').val(b.KadarAir);
					$('#Sampah').val(b.Sampah);
					$('#Netto').val(b.Netto);
				});
			}
		})
	}else{
		DisableInfoKadarAir();
		ValueDefaultInfoKadarAir();
	}
});
function ListMataUang() {
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListMataUang',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({ID:0}),
		success: function (uotput) {
			var row = '';
			$.each(uotput.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.Lambang+`</option>`;
			});
			$('#MataUang').html(row);
			$('.the-loader').hide();
		}
	})
}ListMataUang();
function ListTermOfPay() {
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListTermOfPay',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({ID:0}),
		success: function (uotput) {
			var row = '<option value="">Pilihan TermOfPay</option>';
			$.each(uotput.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.TermPay+`</option>`;
			});
			$('#TermOfPay').html(row);
			$('.the-loader').hide();
		}
	})
}ListTermOfPay();
function ListIndent() {
	$('.the-loader').show();
	$.ajax({
        url: 'FormPoPurchn.aspx/ListIndent',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (uotput) {
			var row = '<option value="">None Indent</option>';
			$.each(uotput.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.Tenggang+`</option>`;
			});
			$('#Indent').html(row);
			$('.the-loader').hide();
		}
	})
}ListIndent();
$('#MataUang').change(function(){
	if($('#MataUang').val()=='' || $('#MataUang').val()==1){
		$('#NilaiKurs').attr('disabled','disabled');
	}else{
		$('#NilaiKurs').removeAttr('disabled');
	}
});
$('#TermOfPay').change(function(){
	if($('#TermOfPay').val()=='' || $('#TermOfPay').val()==6 || $('#TermOfPay').val()==8){
		$('#Bayar').html(optionBayarCash);
	}else{
		$('#Bayar').html(optionBayarKredit);
	}
	if($('#TermOfPay').val()==10){
		$('#KetTermOfPay').removeAttr('disabled');
	}else{
		$('#KetTermOfPay').attr('disabled','disabled');
	}
});
$('#BtnAddItem').click(function(){
	$('.the-loader').show();
	if($('#NoSpp').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NoSpp Belum Di Pilih'
		});return false;
	}
	if($('#ItemSpp').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'ItemSpp Belum Di Pilih'
		});return false;
	}
	if($('#SupplierName').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'SupplierName Belum Di Isi'
		});return false;
	}
	if($('#Qty').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Qty Belum Di Isi'
		});return false;
	}
	if($('#Qty').val()==0){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Qty Tidak Boleh 0'
		});return false;
	}
	if($('#Harga').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Harga Belum Di Isi'
		});return false;
	}
	if($('#Harga').val()==0){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Harga Tidak Boleh 0'
		});return false;
	}
	if($('#Qty').val()>0){
		$.ajax({
            url: 'FormPoPurchn.aspx/CekQtyPo',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({Id:$('#ItemSpp').val()}),
			success: function (uotput) {
				$.each(uotput.d, function (i, b) {
					if(b.QtyPo+$('#Qty').val() > b.Quantity){
						$('.the-loader').hide();$.alert({
							icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
							content: 'Qty PO Tidak Boleh melebihi Qty SPP'
						});return false;
					}
					if(b.QtySudahPo+$('#Qty').val() > b.Quantity){
						$('.the-loader').hide();$.alert({
							icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
							content: 'Qty PO Tidak Boleh melebihi Qty SPP'
						});return false;
					}
				});
			}
		})
	}
	if($('#TermOfPay').val()==10 && $('#KetTermOfPay').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Keterangan TermOfPay Belum Di Isi'
		});return false;
	}
	//if($('#Indent').val()==''){
	//	$('.the-loader').hide();$.alert({
	//		icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
	//		content: 'Indent Belum Di Isi'
	//	});return false;
	//}
	var Disc=0;if($('#Discount').val()!=''){Disc=$('#Discount').val();}
	$.ajax({
        url: 'FormPoPurchn.aspx/CekAddItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({item:$('#ItemName').text()}),
		success: function (output) {
			if(output.d!=''){
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
					content: output.d
				});return false;
			}else{
				$.ajax({
                    url: 'FormPoPurchn.aspx/AddItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
					data : JSON.stringify({
						ItemCode:$('#ItemCode').text(),
						SupId:$('#SupplierID').val()
					}),
					success: function (uotput) {
						$.each(uotput.d, function (i, b) {
							isiDtl={
								SppID:$('#NoSpp').val(),
								DocumentNo:$('#NoSpp option:selected').text(),
								SppDetailID:$('#ItemSpp').val(),
								GroupID:$('#GroupID').val(),
								ItemID:$('#ItemID').val(),
								Price:$('#Harga').val(),
								Qty:$('#Qty').val(),
								ItemTypeID:$('#ItemTypeID').val(),
								UOMID:$('#UOMID').val(),
								Disc:Disc,
								DeliveryDate:$('#DeliveryDate').text(),
								Price2:b.HargaKertas
							}
							isiDetail.isiDtl.push(isiDtl);
							if($('#NoSj').val()!=''){
								var NoSj=$('#NoSj').val();if($('#NoSj').val()==''){NoSj=0;}
								isiKdr={
									SchID:NoSj,
									SchNo:$('#NoSj option:selected').text(),
									NoPol:$('#NoMobil').val(),
									StdKa:$('#StdKa').val(),
									Gross:$('#Gross').val(),
									AktualKA:$('#KadarAir').val(),
									Sampah:$('#Sampah').val(),
									Netto:$('#Netto').val()
								}
								isiKadar.isiKdr.push(isiKdr);
							}
							var list=`
							<tr>
							<td>`+$('#NoSpp option:selected').text()+`</td>
							<td>`+$('#ItemCode').text()+`</td>
							<td>`+$('#ItemName').text()+`</td>
							<td>`+$('#Qty').val()+`</td>
							<td>`+$('#Satuan').text()+`</td>
							<td>`+$('#Harga').val()+`</td>
							<td>`+$('#DeliveryDate').text()+`</td>
							<td>action</td>
							</tr>
							`;
							$('#list-dtl').append(list);

							var TotalPrice1=0;if($('#TotalPrice').val()!=''){TotalPrice1=parseFloat($('#TotalPrice').val());}
							var Qty1=0;if($('#Qty').val()!=''){parseFloat(Qty1=$('#Qty').val());}
							var Harga1=0;if($('#Harga').val()!=''){parseFloat(Harga1=$('#Harga').val());}
							var PPH1=0;if($('#PPH').val()!=''){parseFloat(PPH=$('#PPH1').val());}
							var PPN1=0;if($('#PPN').val()!=''){parseFloat(PPN1=$('#PPN').val());}
							var Discount1=0;if($('#Discount').val()!=''){parseFloat(Discount1=$('#Discount').val());}

							var TotalPrice=TotalPrice1 + (Qty1*Harga1);
							TotalPrice=TotalPrice + ((PPH1/100)*TotalPrice) + ((PPN1/100)*TotalPrice) - ((Discount1/100)*TotalPrice);

							$('#TotalPrice').val(TotalPrice);

							$('#TanggalPo').attr('disabled','disabled'); 
							$('#SupplierName').attr('disabled','disabled'); 
							$('#MataUang').attr('disabled','disabled'); 
							$('#BtnSimpan').removeAttr('disabled');
							ClearValue();
							$('.the-loader').hide();
						});
					}
				})
			}
		}
	})
});
function ClearValue(){
	ListNoSpp();
	$('#ItemSpp').html('');
	$('#ItemCode').text('');
	$('#ItemName').text('');
	$('#Satuan').text('');
	$('#DeliveryDate').text('');
	$('#LeadTime').text('');
	$('#HargaTerendah').text('');
	//$('#UOMID').val('');
	//$('#ItemID').val('');
	//$('#GroupID').val('');
	//$('#ItemTypeID').val('');
	$('#Qty').val('');
	$('#Harga').val('');
}
function ClearArrayList(){
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/ClearArrayList', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success: function (data) {
			$('.the-loader').hide();
		}
	})
}ClearArrayList();
$('.BtnRefresh').click(function(){
	$('#NoPo').val('');
	$('#SupplierName').val('');
	$('#SupplierID').val('');
	$('#SubCompanyID').val('');
	$('#TotalPrice').val('');
	$('#PPN').val('');
	$('#PPH').val('');
	$('#Discount').val('');
	$('#OngkosKirim').val('');
	$('#list-dtl').html('');
	ClearValue();
	$('#BtnAddItem').removeAttr('disabled');
	$('#SupplierName').removeAttr('disabled');
	$('#MataUang').removeAttr('disabled');

	$('#TermOfPay').removeAttr('disabled');
	$('#Bayar').removeAttr('disabled');
	$('#TermOfDelivery').removeAttr('disabled');
	$('#TanggalPo').removeAttr('disabled');
	$('#SupplierName').removeAttr('disabled');
	$('#UPSupplier').removeAttr('disabled');
	$('#TelpSupplier').removeAttr('disabled');
	$('#FaxSupplier').removeAttr('disabled');
	$('#PPN').removeAttr('disabled');
	$('#PPH').removeAttr('disabled');
	$('#Discount').removeAttr('disabled');
	$('#OngkosKirim').removeAttr('disabled');
	$('#TermOfDelivery').html(optionTermOfDelivery);
	$('#Bayar').html(optionBayarCash);
	$('#Barang').html(optionBarang);
	ClearArrayList();
	isiDetail:[];
	isiKadar:[];
	isiHead='';
	ListIndent();
});
$('#BtnSimpan').click(function(){
	$('.the-loader').show();
	var Disc=0;if($('#Discount').val()!=''){Disc=$('#Discount').val();}
	var PPN=0;if($('#PPN').val()!=''){PPN=$('#PPN').val();}
	var OngkosKirim=0;if($('#OngkosKirim').val()!=''){OngkosKirim=$('#OngkosKirim').val();}
	var NilaiKurs=0;if($('#NilaiKurs').val()!=''){NilaiKurs=$('#NilaiKurs').val();}
	var SubCompanyID=0;if($('#SubCompanyID').val()!=''){SubCompanyID=$('#SubCompanyID').val();}
	var isiHead={
		TanggalPo:$('#TanggalPo').val(),
		SupplierID:$('#SupplierID').val(),
		TermOpPay:$('#TermOpPay').val(),
		TermOpPayName:$('#TermOpPay option:selected').text(),
		KetTermOfPay:$('#KetTermOfPay').val(),
		Indent:$('#Indent').val(),
		Termin:$('#Indent option:selected').text(),
		Delivery:$('#TermOfDelivery').val(),
		MataUang:$('#MataUang').val(),
		Keterangan:'',
		PPN:PPN,
		Discount:Disc,
		OngkosKirim:OngkosKirim, 
		Remark:$('#Remark').val(),
		NilaiKurs:NilaiKurs,
		Barang:$('#Barang').val(),
		GroupID:$('#GroupID').val(),
		TotalPrice:$('#TotalPrice').val(),
		SubCompanyID:SubCompanyID
	}
	$.ajax({
		url: 'FormPoPurchn.aspx/SaveData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isiHead:isiHead,
			isiDetail:isiDetail,
			isiKadar:isiKadar
		}),
		success: function (data) {
			if (data.d!='') {
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Transaksi Po Berhasil'
				});
				$('#NoPo').val(data.d);
				$('#BtnSimpan').attr('disabled','disabled'); 
				$('#BtnAddItem').attr('disabled','disabled'); 
				isiDetail:[];
				isiKadar:[];
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
$('#BtnSave-uangmuka').click(function(){
	$('.the-loader').show();
	if($('#uangmuka-po').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Kode Po Belum Di Isi'
		});return false;
	}
	if($('#uangmuka-termin1').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Termin 1 Belum Di Isi'
		});return false;
	}
	if($('#uangmuka-termin2').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Termin 2 Belum Di Isi'
		});return false;
	}
	if($('#uangmuka-po').val()!=''){
		$.ajax({
			url: 'FormPoPurchn.aspx/cekPo',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({Po:$('#uangmuka-po').val()}),
			success: function (uotput) {
				if (uotput.d=='') {
					$('.the-loader').hide();$.alert({
						icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
						content: 'Kode Po Tidak Ada'
					});return false;
				}else{

				}
			}
		})
	}
	Termin1=$('#uangmuka-termin1').val();if($('#uangmuka-termin1').val()==''){Termin1=0;}
	Termin2=$('#uangmuka-termin2').val();if($('#uangmuka-termin2').val()==''){Termin2=0;}
	Termin3=$('#uangmuka-termin3').val();if($('#uangmuka-termin3').val()==''){Termin3=0;}
	Termin4=$('#uangmuka-termin4').val();if($('#uangmuka-termin4').val()==''){Termin4=0;}
	$.ajax({
		url: 'FormPoPurchn.aspx/SaveUangMuka', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			Termin1:Termin1,
			Termin2:Termin2,
			Termin3:Termin3,
			Termin4:Termin4,
			Keterangan1:$('#uangmuka-ket1').val(),
			Keterangan2:$('#uangmuka-ket2').val(),
			Keterangan3:$('#uangmuka-ket3').val(),
			Keterangan4:$('#uangmuka-ket4').val(),
			Po:$('#uangmuka-po').val()		
		}),
		success: function (data) {
			if (data.d!='') {
				$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Uang Muka Berhasil'
				});
				clearUangMuka();
			}else{
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Data Gagal Disimpan',

				});
			}
			$('.the-loader').hide();
		}
	})
});
$('#BtnClear-uangmuka').click(function(){
	clearUangMuka();
});
function clearUangMuka(){
	$('#uangmuka-termin1').val('');
	$('#uangmuka-termin2').val('');
	$('#uangmuka-termin3').val('');
	$('#uangmuka-termin4').val('');
	$('#uangmuka-ket1').val('');
	$('#uangmuka-ket2').val('');
	$('#uangmuka-ket3').val('');
	$('#uangmuka-ket4').val('');
	$('#uangmuka-po').val('');
}

var isPertama = true;
var isPertamadetail = true;
var id;
var oTblReport;
var ID, GroupID, ApproveDate1, ApproveDate2, ApproveDate3;
$('#BtnSearch-spp').click(function(){
	$('.the-loader').show();
	$.ajax({
		url: "FormPoPurchn.aspx/ListDataSpp",type: "POST",contentType: "application/json; charset=utf-8",dataType: "json",
		data : JSON.stringify({
			NoSpp:$('#spp-NoSpp').val(),	
			UserHead:$('#spp-CreatedBy').val()		
		}),
		success: function (data) {
			if (!isPertama) {
				$("#data-spp").DataTable().destroy();
				$('#data-spp').empty();
			} else {
				isPertama = false;
			}
			datatable = $.parseJSON(data.d);
			oTblReport = $("#data-spp").DataTable({
				"data": datatable,
				"pageLength": 50,
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
				{ "data": "ID", title: "ID" },
				{ "data": "KodeSpp", title: "KodeSpp" },
				{ "data": "TanggalSpp", title: "TanggalSpp" },
				{ "data": "TypeSpp", title: "TypeSpp" },
				{ "data": "UserHead", title: "UserHead" },
				{ "data": "ApvDate", title: "ApvDate" }
				]
			});
			oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
			$('.the-loader').hide();
		}
	});
});
$('#data-spp').on('click', 'td.details-control', function () {
	var tr = $(this).closest('tr');
	var row = oTblReport.row(tr);
	if(row.child.isShown()) {
		row.child.hide();
		tr.removeClass('shown');
	}else{
		row.child(format(row.data())).show();
		tr.addClass('shown');
	}
});
function format(d) {
	$('.the-loader').show();
	var html;
	$.ajax({
		url: "FormPoPurchn.aspx/ListDetailSpp",type: "POST",contentType: "application/json; charset=utf-8",dataType: "json",
		async: false,
		data: JSON.stringify({ ID: d.ID }),
		success: function (data) {
			html=`
			<div style="width: 100%;">
			<div class="table-responsive">
			<table class="table table-bordered" style="width: 100%" >
			<tr>
			<th>Aksi</th>
			<th>ItemCode</th>
			<th>ItemName</th>
			<th>Satuan</th>
			<th>QtySpp</th>
			<th>QtyPo</th>
			<th>DeliveryDate</th>
			<th>LeadTime</th>
			</tr>
			<tbody>
			`;
			for (i = 0; i < data.d.length; i++) {
				var ID=data.d[i].ID;
				var KodeSpp=data.d[i].KodeSpp;
				var Item=data.d[i].ItemName;
				html+=`
				<tr>
				<td>
				<button class ="btn btn-data btn-primary" onclick="ShowModal('`+ID+`','`+KodeSpp+`','`+Item+`')">Pending</button>
				</td>
				<td>`+data.d[i].ItemCode+`</td>
				<td>`+data.d[i].ItemName+`</td>
				<td>`+data.d[i].Satuan+`</td>
				<td>`+data.d[i].QtySpp+`</td>
				<td>`+data.d[i].QtyPo+`</td>
				<td>`+data.d[i].DeliveryDate+`</td>
				<td>`+data.d[i].LeadTime+`</td>
				</tr>`;
			}
			html+=`
			</tbody>
			</table>
			</div>
			</div>
			`;
			$('.the-loader').hide();
		}
	});
	return html;
}
$('#data-spp').on('page.dt', function () {
	oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
});
function ShowModal(ID, KodeSpp, Item) {
	$('.modal-spp').modal('show');
	$('#spp-kode').text(KodeSpp);
	$('#spp-itemname').text(Item);
	$('#spp-id').val(ID);
}
$('#BtnSave-spp').click(function(){
	$('.the-loader').show();
	if($('#spp-alasan').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Alasan Harus Di Isi'
		});return false;
	}
	$.ajax({
		url: 'FormPoPurchn.aspx/SaveSpp', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			ID:$('#spp-id').val(),
			Alasan:$('#spp-alasan').val()
		}),
		success: function (data) {
			if (data.d!='') {
				$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Pending Spp Berhasil'
				});
				$('.modal-spp').modal('hide');
			} else {
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Data Gagal Disimpan',
				});
			}
			$('.the-loader').hide();
		}
	})
});
function Pending(){
	$('.the-loader').show();
	$('#data-pending').DataTable().clear().draw();
	$.ajax({
		url: 'FormPoPurchn.aspx/ListPendingSpp',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				var aksi=`
				<button class ="btn btn-data btn-primary" onclick="NonPending('`+b.ID+`')">NonPending</button>
				`;
				row.push([aksi,b.KodeSpp,b.ItemCode,b.ItemName,b.Satuan,b.QtySpp,b.LeadTime,b.AlasanPending]);
			});
			$('#data-pending').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
}Pending();
function NonPending(ID){
	$('.the-loader').show();
	$.ajax({
		url: 'FormPoPurchn.aspx/SaveSpp', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			ID:ID,
			Alasan:''
		}),
		success: function (data) {
			if (data.d!='') {
				$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'NonPending Spp Berhasil'
				});
			} else {
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Data Gagal Disimpan',
				});
			}
			$('.the-loader').hide();
		}
	})
}