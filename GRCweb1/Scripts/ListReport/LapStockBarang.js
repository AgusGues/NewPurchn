$('.select2').select2();
$('#datatable').DataTable({
	dom: 'Bfrtip',
	buttons: [
	'excel', 'pdf', 'print'
	]
});
var optionStock=`
<option value="0">All</option>
<option value="1">Stock</option>
<option value="2">Non Stock</option>
`;$('#TypeStock').html(optionStock);
var optionStatus=`
<option value="0">All</option>
<option value="1">Aktif</option>
<option value="2">Non Aktifk</option>
`;$('#TypeStatus').html(optionStatus);
function ListTypeItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'LapStockBarang.aspx/ListTypeItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Type Item</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.TypeDescription+`</option>`;
			});
			$('#TypeItem').html(row);
			$('.the-loader').hide();
		}
	})
};ListTypeItem();
function ListGroupItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'LapStockBarang.aspx/ListGroupItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Group Item</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.GroupDescription+`</option>`;
			});
			$('#GroupItem').html(row);
			$('.the-loader').hide();
		}
	})
};ListGroupItem();
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#TypeItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TypeItem Belum Di Pilih'
		});return false;
	}
	if($('#GroupItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'GroupItem Belum Di Pilih'
		});return false;
	}
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'LapStockBarang.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			TypeItem:$('#TypeItem').val(),
			GroupItem:$('#GroupItem').val(),
			TypeStock:$('#TypeStock').val(),
			TypeStatus:$('#TypeStatus').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				row.push([
					b.GroupDescription,
					b.ItemCode,
					b.ItemName,
					b.UomCode,
					b.Jumlah,
					b.StockGudang,
					b.StockMax,
					b.StockMin,
					b.ReOrder
					]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#TypeStock').val(parseInt(optionStock));
	$('#TypeStatus').html(optionStatus);
	ListTypeItem();
	ListGroupItem();
	$('#datatable').DataTable().clear().draw();
});

