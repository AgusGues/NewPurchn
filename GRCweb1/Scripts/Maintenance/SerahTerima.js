$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL']],'order':[[0,'desc']],
});
var isiDetail; isiDetail= { isiDtl:[] };
function ListProject(){
	$('.the-loader').show();
	$.ajax({
		url: 'SerahTerimaProject_rev1.aspx/ListProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="0">Pilihan Project</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.ProjectKode+` - `+b.ProjectName+` </option>`;
			});
			$('#NamaProject').html(row);
			$('.the-loader').hide();
		}
	})
}ListProject();
function GoRefresh(){
	$('#KodeProject').val('');
	$('#TanggalMulai').val('');
	$('#TanggalSelesai').val('');
	$('#Quantity').val('');
	$('#BiayaActual').val('');
	$('#StatusProject').val('');
	$('#DeptPemohon').val('');
	$('#datatable').DataTable().clear().draw();
	isiHead='';
}
$('#NamaProject').change(function(){
	$('.the-loader').show();
	if($('#NamaProject').val()==0){
		GoRefresh();
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'SerahTerimaProject_rev1.aspx/InfoProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				Id:$('#NamaProject').val()
			}),
			success: function (output) {
				$.each(output.d, function (i, b) {
					$('#KodeProject').val(b.ProjectKode);
					$('#TanggalMulai').val(b.FromDate);
					$('#TanggalSelesai').val(b.ToDate);
					$('#Quantity').val(b.Quantity);
					$('#BiayaActual').val(b.BiayaActual);
					$('#StatusProject').val(b.StatusProject);
					$('#DeptPemohon').val(b.DeptPemohon);
				});
				$('.the-loader').hide();
			}
		})
		$.ajax({
			url: 'SerahTerimaProject_rev1.aspx/InfoDetail',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				Id:$('#NamaProject').val()
			}),
			success: function (output) {
				var row=[];
				$.each(output.d, function (i, b) {
					row.push([b.ItemCode,b.ItemName,b.Jumlah,b.UomCode,b.Schedule]);
				});
				$('#datatable').DataTable().rows.add(row).draw();
				$('.the-loader').hide();
			}
		})
	}
});
$('#BtnApv').click(function(){
	$('.the-loader').show();
	if($('#NamaProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NamaProject Tidak boleh kosong'
		});return false;
	}
	if($('#TanggalSelesai').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TanggalSelesai Tidak boleh kosong'
		});return false;
	}
	var isiHead={
		Id:$('#NamaProject').val(),
		TanggalSelesai:$('#TanggalSelesai').val()
	}
	$.ajax({
		url: 'SerahTerimaProject_rev1.aspx/ApproveData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({isiHead:isiHead}),
		success: function(data) {
			if(data.d==''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Approve Project Berhasil'
				});
				isiHead='';
				ListProject();
				GoRefresh();
				$('.the-loader').hide();
			}else{
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Data Gagal Disimpan',
				});
			}
		}
	})
});
$('#BtnClear').click(function(){
	ListProject();
	GoRefresh();
});