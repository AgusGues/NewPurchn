$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [-1], ['ALL']],
	"targets": 'no-sort',"bSort": false,"order": [],
	dom: 'Bfrtip',buttons: ['excel', 'pdf', 'print']
});
var optionBulan=`
<option value="0">Pilihan Bulan</option>
<option value="1">Januari</option>
<option value="2">Februari</option>
<option value="3">Maret</option>
<option value="4">April</option>
<option value="5">Mei</option>
<option value="6">Juni</option>
<option value="7">Juli</option>
<option value="8">Agustus</option>
<option value="9">September</option>
<option value="10">Oktober</option>
<option value="11">November</option>
<option value="12">Desember</option>
`;$('#Bulan').html(optionBulan);
function ListTahun(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapSop.aspx/ListTahun',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Tahun</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.Tahun +`">`+b.Tahun+`</option>`;
			});
			$('#Tahun').html(row);
			$('.the-loader').hide();
		}
	})
};ListTahun();
function ListDept(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapSop.aspx/ListDept',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			var row = '<option value="">Pilihan Department</option>';
			if(data.d==null){
				$('.the-loader').hide();
			}else{
				$.each(data.d, function (i, b) {
					row+=`<option value="`+b.Id +`">`+b.Alias+`</option>`;
				});
				$('#Department').html(row);
				$('.the-loader').hide();
			}
		}
	})
};ListDept();
$('#Department').change(function(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapSop.aspx/ListPic',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			dept:$('#Department').val()
		}),
		success:function(data){
			var row = '<option value="">Pilihan PicUser</option>';
			$.each(data.d, function (i, b) {
				row+=`<option value="`+b.UserID +`">`+b.UserName+`</option>`;
			});
			$('#PicUser').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#Department').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Department Belum Di Pilih'
		});return false;
	}
	if($('#Bulan').val()=='0'){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Bulan Belum Di Pilih'
		});return false;
	}
	if($('#Tahun').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tahun Belum Di Pilih'
		});return false;
	}
	var PicUser=$('#PicUser').val();if($('#PicUser').val()==''){PicUser=0;}
	$('#datatable').DataTable().clear().draw();
	
	$.ajax({
		url: 'RekapSop.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			dept:$('#Department').val(),
			user:PicUser
		}),
		success:function(output){
			$.each(output.d,function(i,b){
				var row=[];
				var userpes=b.UserID;
				$.ajax({
					url: 'RekapSop.aspx/ListDataDtl',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
					data : JSON.stringify({
						dept:$('#Department').val(),
						user:userpes,
						bulan:$('#Bulan').val(),
						tahun:$('#Tahun').val()
					}),
					success:function(output){
						$.each(output.d,function(i,b){
							row.push([
								b.UserName,
								b.Description,
								b.Bobot+ '%',
								b.Target,
								b.Pencapaian,
								b.Score,
								b.Point
								]);
						});
						$('#datatable').DataTable().rows.add(row).draw();
						$('.the-loader').hide();
					}
				})
			});
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#PicUser').html('');
	$('#Bulan').html(optionBulan);
	ListTahun();
	ListDept();
	$('#datatable').DataTable().clear().draw();
});

