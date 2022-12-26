$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
var year = new Date().getFullYear();
$('#Tahun').val(parseInt(year));
var optionBulan=`
<option value="01">januari</option>
<option value="02">Februari</option>
<option value="03">Maret</option>
<option value="04">April</option>
<option value="05">Mei</option>
<option value="06">Juni</option>
<option value="07">Juli</option>
<option value="08">Agustus</option>
<option value="09">September</option>
<option value="10">Oktober</option>
<option value="11">November</option>
<option value="12">Desember</option>
`;$('#Bulan').html(optionBulan);
function ListLaporanItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'LapPembelianBarang.aspx/ListLaporanItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Laporan Item</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.ID +`">`+b.GroupDescription+`</option>`;
			});
			$('#LaporanItem').html(row);
			$('.the-loader').hide();
		}
	})
};ListLaporanItem();
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#LaporanItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Laporan Item Belum Di Pilih'
		});return false;
	}
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'LapPembelianBarang.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			TypeSpp:$('#LaporanItem').val(),
			Bulan:$('#Bulan').val(),
			Tahun:$('#Tahun').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				row.push([
					b.ItemCode,
					b.ItemName,
					b.UOMCode,
					b.QtySaldo,
					b.HppSaldo,
					b.TotSaldo,
					b.QtyMasuk,
					b.AvgHargaBeli,
					b.AvgTotBeli,
					b.QtyPakai,
					b.HppSaldoPakai,
					b.TotHppSaldoPakai,
					b.QtyAdjustTambah,
					b.HppSaldoAdjustTambah,
					b.TotHppSaldoQtyAdjustTambah,
					b.QtyAdjsutKurang,
					b.QtyAdjustKurang,
					b.HppSaldoAdjustKurang,
					b.TotHppSaldoQtyAdjustKurang,
					b.QtyRetur,
					b.TotHppSaldoQtyRetur,
					b.EndStock,
					b.AvgPrice,
					b.TotAvgPrice,
					b.FakturPajak
					]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#Tahun').val(parseInt(year));
	$('#Bulan').html(optionBulan);
	ListLaporanItem();
	$('#datatable').DataTable().clear().draw();
});

