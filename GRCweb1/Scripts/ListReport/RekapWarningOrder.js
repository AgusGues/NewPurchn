$('#Tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
    'order': [[0, 'asc']],
    dom: 'Bfrtip',
    buttons: [
	'excel', 'pdf', 'print'
    ]
});
function ListTypeItem(){
	$('.the-loader').show();
	$.ajax({
	    url: 'FormWarnOrder.aspx/ListTypeItem', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Type Item</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.GroupDescription+`</option>`;
			});
			$('#TypeItem').html(row);
			$('.the-loader').hide();
		}
	})
};ListTypeItem();
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#TypeItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Type Item Belum Di Pilih'
		});return false;
	}
	if($('#Tanggal').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tanggal Belum Di Isi'
		});return false;
	}
	$('#datatable').DataTable().clear().draw();
	$.ajax({
	    url: 'FormWarnOrder.aspx/ListData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			ItemId:$('#TypeItem').val(),
			Tanggal:$('#Tanggal').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				row.push([
					b.ItemCode,
					b.ItemName,
					b.UomCode,
					b.MinStock,
					b.EndingStok,
					b.ReOrder
					]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#Tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	ListTypeItem();
	$('#datatable').DataTable().clear().draw();
});

