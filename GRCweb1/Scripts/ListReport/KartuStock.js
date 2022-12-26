$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
var year = new Date().getFullYear();
$('#Tahun').val(parseInt(year));
var optionStatusItem=`
<option value="1">Hanya Item Aktif</option>
<option value="0">Termasuk Item Non Aktif</option>
`;$('#StatusItem').html(optionStatusItem);
var optionRangePeriode=`
<option value="1">1 Bulan</option>
<option value="6">6 Bulan</option>
<option value="12">12 Bulan</option>
`;$('#RangePeriode').html(optionRangePeriode);
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
$('#BtnCari').click(function(){
	$('.the-loader').show();
	$.ajax({
		url: 'KartuStock.aspx/ListItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			StatusItem:$('#StatusItem').val(),
			ItemName:$('#CariItemName').val()
		}),
		success:function(data){
			var row = '<option value="">Pilihan Item</option>';
			$.each(data.d, function (i, b) {
				var id=b.ItemTypeID.toString()+b.ID.toString();
				row+=`<option value="`+id +`">`+b.ItemName+` (`+b.ItemCode+`)</option>`;
			});
			$('#ItemName').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#RangePeriode').change(function(){
	if($('#RangePeriode').val()=='1'){
		$('#row-Bulan').show();
		$('#row-Bulan2').hide();
		$('#row-Tahun2').hide();
	}else if($('#RangePeriode').val()=='6'){
		$('#row-Bulan').show();
		$('#row-Bulan2').show();
		$('#row-Tahun2').show();
		sampaibulan();
	}else{
		$('#row-Bulan').hide();
		$('#row-Bulan2').hide();
		$('#row-Tahun2').hide();
	}
});
$('#Bulan').change(function(){
	sampaibulan();
});
function sampaibulan(){
	var bulan=parseInt($('#Bulan').val()),q=0;
	if(bulan<=6){
		q=bulan+5;
		$('#Tahun2').val($('#Tahun').val());
	}else{
		q=bulan-7;
		$('#Tahun2').val(parseInt($('#Tahun').val())+1);
	}
	var bulan='Desember';
	if(q==1){bulan='Januari';}
	if(q==2){bulan='Februari';}
	if(q==3){bulan='Maret';}
	if(q==4){bulan='April';}
	if(q==5){bulan='Mei';}
	if(q==6){bulan='Juni';}
	if(q==7){bulan='Juli';}
	if(q==8){bulan='Agustus';}
	if(q==9){bulan='September';}
	if(q==10){bulan='Oktober';}
	if(q==11){bulan='November';}
	var row=`<option value="`+q.toString()+`">`+bulan+`</option>`;
	$('#Bulan2').html(row);
}
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#ItemName').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Name Item Belum Di Pilih'
		});return false;
	}
	if($('#Tahun').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tahun Belum Di Isi'
		});return false;
	}
	var bulan2='';if($('#Bulan2').val()==null){bulan2='';}else{bulan2=$('#Bulan2').val();}
	$.ajax({
		url: 'KartuStock.aspx/ListData',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			ItemId:parseInt($('#ItemName').val()),
			RangePeriode:parseInt($('#RangePeriode').val()),
			bulan:$('#Bulan').val().toString(),
			tahun:$('#Tahun').val().toString(),
			bulan2:bulan2,
			tahun2:$('#Tahun2').val().toString()
		}),
		success:function(data){
			var row = '';
			$.each(data.d, function (i, b) {
				row+=`
				<tr>
				<td>`+b.Tanggal+`</td>
				<td>`+b.Faktur+`</td>
				<td>`+b.Keterangan+`</td>
				<td>`+b.Masuk+`</td>
				<td>`+b.Keluar+`</td>
				<td>`+b.Saldo+`</td>
				</tr>
				`;
			});
			$('#list-data').html(row);
			$('.the-loader').hide();
		}
	})
	$.ajax({
		url: 'KartuStock.aspx/InfoItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			ItemId:parseInt($('#ItemName').val()),
		}),
		success:function(data){
			var row = '';
			$.each(data.d, function (i, b) {
				$('#info-ItemCode').text(b.ItemCode);
				$('#info-ItemName').text(b.ItemName);
				$('#info-UOMDesc').text(b.UOMDesc);
				$('#info-MinStock').text(b.MinStock);
				$('#info-MaxStock').text(b.MaxStock);
				$('#info-ReOrder').text(b.ReOrder);
				row+=`
				<tr>
				<td>`+b.ItemCode+`</td>
				<td>`+b.ItemName+`</td>
				<td>`+b.UOMDesc+`</td>
				<td>`+b.MinStock+`</td>
				<td>`+b.MaxStock+`</td>
				<td>`+b.ReOrder+`</td>
				</tr>
				`;
			});
			$('#info-item').html(row);
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#Tahun').val(parseInt(year));
	$('#StatusItem').html(optionStatusItem);
	$('#RangePeriode').html(optionRangePeriode);
	$('#row-Bulan').show();
	$('#row-Bulan2').hide();
	$('#row-Tahun2').hide();
	$('#list-data').html('');
});