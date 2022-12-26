$('#Tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#panel-form').click(function(){
	$('.panel-form').show();$('.panel-list').hide();$('.the-title').text('Input Receipt');
});
$('#panel-list').click(function(){
	$('.panel-list').show();$('.panel-form').hide();$('.the-title').text('List Receipt');
});
var isiDetail; isiDetail= { isiDtl:[] };
var optionTypeReceipt=`
<option value="">Pilihan Type Receipt</option>
<option value="1">Mekanik & Elektrik</option>
<option value="2">Project</option>
<option value="3">Non Grc</option>
`;$('#TypeReceipt').html(optionTypeReceipt);

$('#datatable').on('click','.btn-data',function(){
	$('.the-loader').show();
	$('#list-dtl').html('');
	var id=$(this).attr('id');
	$.ajax({
		url: 'ReceiptMRS.aspx/HeadData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({id:id}),
		success:function(output){
			$.each(output.d,function(i,b){
				$('#KodeReceipt').val(b.KodeReceipt);
				$('#Tanggal').val(b.Tanggal);
				$('#KodeReceipt').attr('disabled','disabled'); 
				$('#Tanggal').attr('disabled','disabled'); 
			});
		}
	})
	$.ajax({
		url: 'ReceiptMRS.aspx/DtlData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({id:id}),
		success:function(output){
			var row=``;
			$.each(output.d,function(i,b){
				row+=`
				<tr>
				<td>`+b.KodePO+`</td>
				<td>`+b.KodeSpp+`</td>
				<td>`+b.ItemCode+`</td>
				<td>`+b.ItemName+`</td>
				<td>`+b.Quantity+`</td>
				<td>`+b.Satuan+`</td>
				<td>`+b.Keterangan+`</td>
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
		url: 'ReceiptMRS.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			KodeReceipt:$('#KodeReceipt-list').val(),
			KodePo:$('#KodePo-list').val(),
			KodeSpp:$('#KodeSpp-list').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				var Aksi=`<label class="btn btn-primary btn-xs btn-data" id="`+b.ID+`"><i class="fa fa-edit"></i></label>`;
				var Quantity=b.Quantity+' '+b.Satuan;
				row.push([Aksi,b.KodeReceipt,b.TanggalReceipt,b.KodePO,b.KodeSpp,b.ItemCode,b.ItemName,Quantity,b.Status]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('#BtnClearData').click(function(){
	$('#datatable').DataTable().clear().draw();
});

$('#TypeReceipt').change(function(){
	$('.the-loader').show();
	if($('#TypeReceipt').val()==''){
		$('#NoPo').html('<option value=""></option>');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'ReceiptMRS.aspx/ListPo',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({type:$('#TypeReceipt').val()}),
			success: function (output) {
				var row = '<option value="">Pilihan Po</option>';
				$.each(output.d, function (i, b) {
					row+=`<option value="`+b.PoId +`">`+b.NoPo+`</option>`;
				});
				$('#NoPo').html(row);
				$('.the-loader').hide();
			}
		})
	}
});
$('#NoPo').change(function(){
	$('.the-loader').show();
	if($('#NoPo').val()==''){
		$('#ItemName').html('<option value=""></option>');
		$('.the-loader').hide();
	}else{
		listItem();
	}
});
function listItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'ReceiptMRS.aspx/CekDate',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({ID:$('#NoPo').val(),Tanggal:$('#Tanggal').val()}),
		success: function (output1) {
			if(output1.d!=''){
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
					content: 'Tanggal Po Tidak Boleh Lebih Lampau Dari Tanggal Receipt'
				});return false;
			}else{
				$.ajax({
					url: 'ReceiptMRS.aspx/ListItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
					data : JSON.stringify({Id:$('#NoPo').val()}),
					success: function (output) {
						var row = '<option value="">Pilihan Item</option>';
						$.each(output.d, function (i, b) {
							row+=`<option value="`+b.ID +`">`+b.ItemName+`</option>`;
						});
						$('#ItemName').html(row);
						$('.the-loader').hide();
					}
				})
			}
		}
	})
}
$('#ItemName').change(function(){
	$('.the-loader').show();
	if($('#ItemName').val()==''){
		$('#ItemCode').val('');
		$('#Stock').val('');
		$('#Satuan').val('');
		$('#QtyPo').val('');
		$('#NoSpp').val('');
		$('#Status').val('');
		$('#Suplier').val('');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'ReceiptMRS.aspx/InfoItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({Id:$('#ItemName').val()}),
			success: function (output) {
				$.each(output.d, function (i, b) {
					$('#ItemCode').val(b.ItemCode);
					$('#Stock').val(b.Stock);
					$('#Satuan').val(b.Satuan);
					$('#QtyPo').val(b.QtyPo);
					$('#NoSpp').val(b.NoSpp);
					var status='private';if(b.Status=='1'){status='public';}
					$('#Status').val(status);
					$('#Suplier').val(b.Suplier);
				});
				$('.the-loader').hide();
			}
		})
	}
});
$('#BtnAddItem').click(function(){
	$('.the-loader').show();
	if($('#QtyRecipt').val()=='' || $('#QtyRecipt').val()==0){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'QtyRecipt Tidak boleh kosong'
		});return false;
	}
	if($('#Keterangan').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Keterangan Tidak boleh kosong'
		});return false;
	}
	if(parseInt($('#QtyRecipt').val()) > parseInt($('#QtyPo').val())){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'QtyRecipt Tidak boleh lebih besar dari QtyPo'
		});return false;
	}
	if($('#Tanggal').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tanggal Tidak boleh kosong'
		});return false;
	}else{
		$.ajax({
			url: 'ReceiptMRS.aspx/CekClosing',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({NoPo:$('#NoPo option:selected').text(),Tanggal:$('#Tanggal').val()}),
			success: function (output) {
				if(output.d!=''){
					$('.the-loader').hide();$.alert({
						icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
						content: output.d
					});return false;
				}
			}
		})
	}
	$.ajax({
		url: 'ReceiptMRS.aspx/AddItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({item:$('#ItemName option:selected').text()}),
		success: function (output) {
			if(output.d!=''){
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
					content: output.d
				});return false;
			}else{
				$.ajax({
					url: 'ReceiptMRS.aspx/InfoDetailPo',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
					data : JSON.stringify({Id:parseInt($('#ItemName').val())}),
					success: function (output) {
						$.each(output.d, function (i, b) {
							isiDtl={
								NoPO:$('#NoPo').val(),
								ItemCode:$('#ItemCode').val(),
								ItemName:$('#ItemName option:selected').text(),
								UOMCode:$('#Satuan').val(),
								Quantity:$('#QtyRecipt').val(),
								Keterangan:$('#Keterangan').val(),
								Price:b.Price,
								ItemID:b.ItemID,
								TotalPrice:b.Price*$('#QtyRecipt').val(),
								SppID:b.SppID,
								SppNo:b.DocumentNo,
								PoID:b.PoID,
								PoNo: $('#NoPo option:selected').text(),
								UomID:b.UomID,
								PODetailID:$('#ItemName').val(),
								GroupID:b.GroupID,
								ItemTypeID:b.ItemTypeID,
								ItemID2:b.ItemID,
								ID:0,
								Bulan:0,
								Tahun:0,
								Status:0,
								TimbanganBPAS:0,
								ReceiptID:0,
								Kadarair:0,
								Disc:0,
								RowStatus:0,
								QtyTimbang:0,
								KodeTimbang:0,
								TipeAsset:0,
								DOID:0,
								ScheduleNo:0
							}
							isiDetail.isiDtl.push(isiDtl);
							var list=`
							<tr>
							<td>`+$('#NoPo option:selected').text()+`</td>
							<td>`+$('#NoSpp').val()+`</td>
							<td>`+$('#ItemCode').val()+`</td>
							<td>`+$('#ItemName option:selected').text()+`</td>
							<td>`+$('#QtyRecipt').val()+`</td>
							<td>`+$('#Satuan').val()+`</td>
							<td>`+$('#Keterangan').val()+`</td>
							<td>action</td>
							</tr>
							`;
							$('#list-dtl').append(list);

							$('#TypeReceipt').attr('disabled','disabled'); 
							$('#NoPo').attr('disabled','disabled'); 
							$('#Tanggal').attr('disabled','disabled'); 
							$('#BtnSimpan').removeAttr('disabled');
							ClearValue();
							listItem();
						});
						$('.the-loader').hide();
					}
				})
			}
		}
	})
});
function ClearValue(){
	$('#ItemCode').val('');
	$('#Satuan').val('');
	$('#NoSpp').val('');
	$('#Status').val('');
	$('#Suplier').val('');
	$('#Stock').val('');
	$('#QtyPo').val('');
	$('#QtyRecipt').val('');
	$('#Keterangan').val('');
	
}
function ClearArrayList(){
	$('.the-loader').show();
	$.ajax({
		url: 'ReceiptMRS.aspx/ClearArrayList', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success: function (data) {
			$('.the-loader').hide();
		}
	})
}ClearArrayList();
$('.BtnRefresh').click(function(){
	$('#KodeReceipt').val('');
	$('#list-dtl').html('');
	ClearValue();
	$('#TypeReceipt').removeAttr('disabled');
	$('#NoPo').removeAttr('disabled');
	$('#Tanggal').removeAttr('disabled');
	$('#BtnAddItem').removeAttr('disabled');
	$('#BtnSimpan').attr('disabled','disabled'); 
	$('#ItemName').html('<option value=""></option>');
	$('#NoPo').html('<option value=""></option>');
	$('#TypeReceipt').html(optionTypeReceipt);
	ClearArrayList()
	isiDetail:[];
	isiHead='';
});
$('#BtnSimpan').click(function(){
	$('.the-loader').show();
	var isiHead={
		ID:0,
		Tanggal:$('#Tanggal').val(),
		TypeReceipt:0,
		SupplierType:0,
		SupplierID:0,
		PoID:$('#NoPo').val(),
		NoPo:$('#NoPo option:selected').text(),
		ReceiptNo:'',
		TypeReceipt:$('#TypeReceipt').val(),
		DepoID:13,
		Status:0,
		ItemTypeID:1,
		tipeAsset:0,
		CreatedBy:'',
		AlasanCancel:'',
		LastModifiedBy:'',
		FakturPajak:'',
		Keteranganpay:'',
		InvoiceNo:'',
		KursPajak:0
	}
	$.ajax({
		url: 'ReceiptMRS.aspx/SaveData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isiHead:isiHead,
			isiDetail:isiDetail
		}),
		success: function (data) {
			if (data.d!='') {
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Transaksi Receipt Berhasil'
				});
				$('#KodeReceipt').val(data.d);
				$('#BtnSimpan').attr('disabled','disabled'); 
				$('#BtnAddItem').attr('disabled','disabled'); 
				isiDetail:[];
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

















