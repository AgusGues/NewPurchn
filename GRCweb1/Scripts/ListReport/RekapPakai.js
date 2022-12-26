
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#DariTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('#SampaiTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
var optionLaporan=`
<option value="Detail">Detail</option>
<option value="Rekap">Rekap</option>
`;$('#TypeLaporan').html(optionLaporan);
function ListDept(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapPakai.aspx/ListDept',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">All</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.Alias +`">`+b.Alias+`</option>`;
			});
			$('#DeptName').html(row);
			$('.the-loader').hide();
		}
	})
};ListDept();
function ListGroupItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapPakai.aspx/ListGroupItem',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="0">All</option>';
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
	$('#list-dataDetail').html('');
	$('#list-dataRekap').html('');
	if($('#TypeLaporan').val()=='Detail'){
		$('#table-detail').show();
		$('#table-rekap').hide();
		$.ajax({
			url: 'RekapPakai.aspx/ListDataDetail',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				DariTanggal:$('#DariTanggal').val(),
				SampaiTanggal:$('#SampaiTanggal').val(),
				DeptName:$('#DeptName').val(),
				TypeItem:$('#GroupItem').val()
			}),
			success:function(output){
				var row='';
				$.each(output.d,function(i,b){
					var DeptName=b.DeptName;if(b.DeptName==null){DeptName='';}
					var GroupDescription=b.GroupDescription;if(b.GroupDescription==null){GroupDescription='';}
					var PakaiNo=b.PakaiNo;if(b.PakaiNo==null){PakaiNo='';}
					var PakaiDate=b.PakaiDate;if(b.PakaiDate==null){PakaiDate='';}
					var ItemCode=b.ItemCode;if(b.ItemCode==null){ItemCode='';}
					var ItemName=b.ItemName;if(b.ItemName==null){ItemName='';}
					var UomCode=b.UomCode;if(b.UomCode==null){UomCode='';}
					var Jumlah=b.Jumlah;if(b.Jumlah==null){Jumlah='';}
					var Harga=b.Harga;if(b.Harga==null){Harga='';}
					var Total=b.Total;if(b.Total==null){Total='';}
					var Keterangan=b.Keterangan;if(b.Keterangan==null){Keterangan='';}
					var Status=b.Status;if(b.Status==null){Status='';}
					var NoPol=b.NoPol;if(b.NoPol==null){NoPol='';}
					row+=`
					<tr>
					<td>`+DeptName+`</td>
					<td>`+GroupDescription+`</td>
					<td>`+PakaiNo+`</td>
					<td>`+PakaiDate+`</td>
					<td>`+ItemCode+`</td>
					<td>`+ItemName+`</td>
					<td>`+UomCode+`</td>
					<td>`+Jumlah+`</td>
					<td>`+Harga+`</td>
					<td>`+Total+`</td>
					<td>`+Keterangan+`</td>
					<td>`+Status+`</td>
					<td>`+NoPol+`</td>
					</tr>
					`;			
				});
				$('#list-dataDetail').html(row);
				$('.the-loader').hide();
			}
		})
	}else{
		$('#table-detail').hide();
		$('#table-rekap').show();
		$.ajax({
			url: 'RekapPakai.aspx/ListDataRekap',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				DariTanggal:$('#DariTanggal').val(),
				SampaiTanggal:$('#SampaiTanggal').val(),
				DeptName:$('#DeptName').val(),
				TypeItem:$('#GroupItem').val()
			}),
			success:function(output){
				var row='';
				$.each(output.d,function(i,b){
					var DeptName=b.DeptName;if(b.DeptName==null){DeptName='';}
					var GroupDescription=b.GroupDescription;if(b.GroupDescription==null){GroupDescription='';}
					var ItemCode=b.ItemCode;if(b.ItemCode==null){ItemCode='';}
					var ItemName=b.ItemName;if(b.ItemName==null){ItemName='';}
					var UomCode=b.UomCode;if(b.UomCode==null){UomCode='';}
					var Jumlah=b.Jumlah;if(b.Jumlah==null){Jumlah='';}
					var Harga=b.Harga;if(b.Harga==null){Harga='';}
					var Total=b.Total;if(b.Total==null){Total='';}
					row+=`
					<tr>
					<td>`+b.DeptName+`</td>
					<td>`+b.GroupDescription+`</td>
					<td>`+b.ItemCode+`</td>
					<td>`+b.ItemName+`</td>
					<td>`+b.UomCode+`</td>
					<td>`+b.Jumlah+`</td>
					<td>`+b.Harga+`</td>
					<td>`+b.Total+`</td>
					</tr>
					`;	
				});
				$('#list-dataRekap').html(row);
				$('.the-loader').hide();
			}
		})
	}
});
$('.BtnRefresh').click(function(){
	$('#DariTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	$('#SampaiTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	$('#TypeLaporan').html(optionLaporan);
	$('#list-dataDetail').html('');
	$('#list-dataRekap').html('');
	$('#table-detail').show();
	$('#table-rekap').hide();
	ListDept();
	ListGroupItem();

});

