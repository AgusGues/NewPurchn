$('#Tanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL'] ],'order':[[0,'desc']]
});
$('#panel-form').click(function(){
	$('.panel-form').show();$('.panel-list').hide();$('.the-title').text('Input Project');
});
$('#panel-list').click(function(){
	$('.panel-list').show();$('.panel-form').hide();$('.the-title').text('List Project');
});
var isiDetail; isiDetail= { isiDtl:[] };
var optionSasaranProject=`
<option value="">Pilihan Sasaran</option>
<option value="Kualitas">Kualitas</option>
<option value="Produktivitas">Produktivitas</option>
<option value="Keselamatan Kerja">Keselamatan Kerja</option>
`;$('#SasaranProject').html(optionSasaranProject);
var optionDeptPenerima=`
<option value="">Pilihan Dept</option>
<option value="7">Hrd & Ga</option>
<option value="19">mainTenance</option>
`;$('#DeptPenerima').html(optionDeptPenerima);
var optionStatusProject=`
<option value="">Pilihan Status</option>
<option value="0">Open</option>
<option value="2">Release</option>
<option value="21">Finish</option>
<option value="3">Close</option>
<option value="4">Pending</option>
<option value="-1">Cancel</option>
`;$('#StatusProject-list').html(optionStatusProject);
$('#BtnSearchData').click(function(){
	$('.the-loader').show();
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'InputProject.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			status:$('#StatusProject-list').val(),
			dept:$('#DeptPemohon-list').val(),
			nomor:$('#KodeProject-list').val()
		}),
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				var Approval='';
				if(b.Approval==0){Approval='Admin';}
				if(b.Approval==1){Approval='Pm';}
				if(b.Approval==2){
					if(b.Biaya>50000000){Approval='Direksi';}
					else{Approval='Pm';}
				}
				var Status='Cancel';
				if(b.Status==0 || b.Status==1){Status='Open';}
				if(b.Status==3){Status='Close';}
				if(b.Status==4){Status='Pending';}
				if(b.Status==2){
					if(b.RowStatus==1){Status='Finish';}
					else if(b.RowStatus==2){Status='Hand Over';}
					else if(b.RowStatus==3){Status='Close';}
					else{Status='Release';}
				}
				row.push([b.Nomor,b.ProjectName,b.Sasaran,b.FromDate,b.ToDate,b.Biaya,b.BiayaActual,Approval,Status]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
});
$('#BtnClearData').click(function(){
	$('#datatable').DataTable().clear().draw();
});
function ListDeptPemohon(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/ListDeptPemohon',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="0">Pilihan Departmen</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.DeptName+`</option>`;
			});
			$('#DeptPemohon-list').html(row);
			$('.the-loader').hide();
		}
	})
}ListDeptPemohon();
function ListGroupProject(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/ListGroupProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan Group</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.NamaGroup+`</option>`;
			});
			$('#GroupProject').html(row);
			$('.the-loader').hide();
		}
	})
}ListGroupProject();
function GetDeptPemohon(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/GetDeptPemohon',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.DeptName+`</option>`;
			});
			$('#DeptPemohon').html(row);
			$('.the-loader').hide();
		}
	})
}GetDeptPemohon();
function ListAreaProject(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/ListAreaProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = `
			<option value="">Pilihan Area</option>
			<option value="98">WTP</option>
			<option value="97">Material Preparation</option>
			<option value="99">General</option>
			`;
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.ZonaName+`</option>`;
			});
			$('#AreaProject').html(row);
			$('.the-loader').hide();
		}
	})
}ListAreaProject();
function ListSatuan(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/ListSatuan',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.UomDesc+`</option>`;
			});
			$('#Satuan').html(row);
			$('.the-loader').hide();
		}
	})
}ListSatuan();
function ListHeadName(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/ListHeadName',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.NamaHead+`">`+b.NamaHead+`</option>`;
			});
			$('#HeadName').html(row);
			$('.the-loader').hide();
		}
	})
}ListHeadName();
function GetBatasEstimasi(){
	$('.the-loader').show();
	$.ajax({
		url: 'InputProject.aspx/GetBatasEstimasi',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			$('#BatasEstimasi').val(output.d);
			$('.the-loader').hide();
		}
	})
}GetBatasEstimasi();
$('#AreaProject').change(function(){
	$('.the-loader').show();
	if($('#AreaProject').val()==''){
		$('#SubArea').html('<option value=""></option>');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'InputProject.aspx/ListSubArea',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({AreaProject:$('#AreaProject option:selected').text()}),
			success: function (output) {
				var row = '<option value="">Pilihan Sub Area</option>';
				$.each(output.d, function (i, b) {
					row+=`<option value="`+b.Id +`">`+b.AreaName+`</option>`;
				});
				$('#SubArea').html(row);
				$('.the-loader').hide();
			}
		})
	}
});
$('#BtnSimpan').click(function(){
	$('.the-loader').show();
	if($('#DeptPenerima').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'DeptPenerima Tidak boleh kosong'
		});return false;
	}
	if($('#Tanggal').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Tanggal Tidak boleh kosong'
		});return false;
	}
	if($('#HeadName').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'HeadName Tidak boleh kosong'
		});return false;
	}
	if($('#GroupProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'GroupProject Tidak boleh kosong'
		});return false;
	}
	if($('#NamaProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NamaProject Tidak boleh kosong'
		});return false;
	}
	if($('#SasaranProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'SasaranProject Tidak boleh kosong'
		});return false;
	}
	if($('#DeptPemohon').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'DeptPemohon Tidak boleh kosong'
		});return false;
	}
	if($('#AreaProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'AreaProject Tidak boleh kosong'
		});return false;
	}
	if($('#Satuan').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Satuan Tidak boleh kosong'
		});return false;
	}
	if($('#TujuanProject').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TujuanProject Tidak boleh kosong'
		});return false;
	}
	if($('#Quantity').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Quantity Tidak boleh kosong'
		});return false;
	}
	var Zona=$('#SubArea option:selected').text();if($('#SubArea option:selected').text()==null){Zona="";}
	var isiHead={
		NamaProject:$('#NamaProject').val(),
		Tanggal:$('#Tanggal').val(),
		Sasaran:$('#SasaranProject').val(),
		GroupID:$('#GroupProject').val(),
		ProdLine:$('#AreaProject').val(),
		CreatedBy:'',
		Quantity:$('#Quantity').val(),
		UOMID:$('#Satuan').val(),
		Nomor:'',
		Approval:0,
		DetailSasaran:$('#TujuanProject').val(),
		Zona:$('#SubArea option:selected').text(),
		ToDept:$('#DeptPemohon').val(),
		NamaHead:$('#HeadName').val()		 	
	}
	$.ajax({
		url: 'InputProject.aspx/SaveData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({isiHead:isiHead}),
		success: function(data) {
			if(data.d!=''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Input Project Berhasil'
				});
				$('#KodeProject').val(data.d);
				isiHead='';
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
$('.BtnRefresh').click(function(){
	ListGroupProject();
	GetDeptPemohon();
	ListAreaProject();
	ListSatuan();
	ListHeadName();
	GetBatasEstimasi();
	$('#Tanggal').val('');
	$('#NamaProject').val('');
	$('#SubArea').html('');
	$('#TujuanProject').val('');
	$('#Quantity').html('');
	isiHead='';
});
