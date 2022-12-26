$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[1,'asc']]
});
$('#BtnFind').click(function(){
	$('.the-loader').show();
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'RekapAsset.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				row.push([
					b.ItemCode,
					b.ItemName,
					b.Unit,
					b.SaldoAwal,
					b.Pembelian,
					b.AdjustIn,
					b.AdjustOut,
					b.SaldoAkhir,
					b.Kategori
					]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#datatable').DataTable().clear().draw();
});