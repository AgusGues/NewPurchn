$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL']],'order':[[1,'desc']],
	dom: 'Bfrtip',
	buttons: [
	'excel', 'pdf', 'print'
	]
});
var optionDepartment=`
<option value="0">Pilihan Department</option>
<option value="7">HRD & GA</option>
<option value="19">MAINTENANCE</option>
<option value="14">IT</option>
`;$('#Department').html(optionDepartment);
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
/*
RB1=Pemantauan WO Masuk
RBK=Pemantauan WO Keluar 				1
RB2=Pemantauan WO per Bulan 			2
RBPO=Semua WO ( Status Open - Closed ) 	3
*/
function HakAkses(){
	$('.the-loader').show();
	var optionTypeLapoaran='<option value="0" >Pilihan Laporan</option>';
	$.ajax({
		url: 'LapWorkOrder.aspx/HakAkses',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		success:function(data){
			$.each(data.d, function (i, b) {
				// Untuk HRD PES - ISO
				if(b.StatusReport==5){
					$('#row-Department').show();
					optionTypeLapoaran+=`
					<option value="1">WO Keluar</option>
					<option value="2">WO per Bulan</option>
					<option value="3">Semua WO ( Status Open - Closed )</option>
					`;
				}
				// Untuk Manager Penerima WO
				else if(b.StatusReport==0 && b.StatusApv==3 || b.StatusReport==0 && b.StatusApv==3){
					$('#row-Department').hide();
					optionTypeLapoaran+=`
					<option value="1">WO Keluar</option>
					<option value="2">WO per Bulan</option>
					<option value="3">Semua WO ( Status Open - Closed )</option>
					`;
				}
				// Untuk User Pembuat WO
				else{
					$('#row-Department').hide();
					optionTypeLapoaran+=`
					<option value="1">WO Keluar</option>
					`;
				}
				$('#TypeLapoaran').html(optionTypeLapoaran);
			});
			$('.the-loader').hide();
		}
	})
};HakAkses();
function ListTahun(){
	$('.the-loader').show();
	$.ajax({
		url: 'LapWorkOrder.aspx/ListTahun',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
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
$('#BtnFind').click(function(){
	$('.the-loader').show();
	if($('#TypeLapoaran').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TypeLapoaran Tidak Boleh Kosong'
		});return false;
	}
	if($('#Department').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Department Tidak Boleh Kosong'
		});return false;
	}
	if($('#Bulan').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Bulan Tidak Boleh Kosong'
		});return false;
	}
	if($('#Tahun').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tahun Belum Di Pilih'
		});return false;
	}
	clearVal();
	$.ajax({
		url: 'LapWorkOrder.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			TypeLapoaran:$('#TypeLapoaran').val(),
			Department:$('#Department').val(),
			Bulan:$('#Bulan').val(),
			Tahun:$('#Tahun').val()
		}),
		success:function(output){
			var dept='';
			if(output.d!=''){
				$.each(output.d,function(i,b1){
					dept=b1.DeptName;
					$.ajax({
						url: 'LapWorkOrder.aspx/PencapaianNilai',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
						data : JSON.stringify({
							DeptFrom:dept,
							DeptTo:$('#Department').val(),
							Bulan1:$('#Bulan').val(),
							Tahun1:$('#Tahun').val()
						}),
						success:function(data){
							$.each(data.d, function (i, b2) {
								$('#TotalWO').text(b2.txtTotal);
								$('#Tercapai').text(b2.txtTarget);
								$('#PersenPencapaian').text(b2.txtPersen);
								$('#TotalWO1').text(b2.LabelTotalNilai+' '+b2.KetTotal);
								$('#Tercapai1').text(b2.LabelTargetNilai+' '+b2.KetTarget);
								$('#PersenPencapaian1').text(b2.LabelPersenNilai+' '+b2.KetPersen);
							});
						}
					})
					var row=[];
					$.ajax({
						url: 'LapWorkOrder.aspx/ListDataWo',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
						data : JSON.stringify({
							DeptFrom:dept,
							DeptTo:$('#Department').val(),
							Bulan:$('#Bulan').val(),
							Tahun:$('#Tahun').val()
						}),
						success:function(output){
							$.each(output.d,function(i,b){
								row.push([
									b.UsersWO,
									b.NoWO,
									b.UraianPekerjaan,
									b.AreaWO,
									b.CreatedTime,
									b.ApvMgr,
									b.Waktu2,
									b.DueDateWO,
									b.FinishDate2,
									b.Pelaksana,
									b.StatusApv,
									b.SisaHari+' hari',
									b.StatusWO,
									b.CreatedBy,
									b.Selisih
									]);
							});
							$('#datatable').DataTable().rows.add(row).draw();
							$('.the-loader').hide();
						}
					})
				});
			}else{
				$('.the-loader').hide();
			}
		}
	})
});
$('.BtnRefresh').click(function(){
	$('#Department').html(optionDepartment);
	$('#Bulan').html(optionBulan);
	HakAkses();
	ListTahun();
	clearVal();
});
function clearVal(){
	$('#datatable').DataTable().clear().draw();
	$('#TotalWO').text('');
	$('#Tercapai').text('');
	$('#PersenPencapaian').text('');
	$('#TotalWO1').text('');
	$('#Tercapai1').text('');
	$('#PersenPencapaian1').text('');
	$('#datatable').DataTable().clear().draw();
}

