$('#TanggalPemakaian').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#panel-form').click(function(){
	$('.panel-form').show();$('.panel-list').hide();$('.the-title').text('Input EstimasiMaterial');
});
$('#panel-list').click(function(){
	$('.panel-list').show();$('.panel-form').hide();$('.the-title').text('List EstimasiMaterial');
});
var isiDetail; isiDetail= { isiDtl:[] };
function ListKodeProject(){
	$('.the-loader').show();
	$.ajax({
		url: 'EstimasiMaterial.aspx/ListKodeProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan KodeProject</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.Nomor+`</option>`;
			});
			$('#KodeProject').html(row);
			$('.the-loader').hide();
		}
	})
}ListKodeProject();
function ListTypeItem(){
	$('.the-loader').show();
	$.ajax({
		url: 'EstimasiMaterial.aspx/ListTypeItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan TypeItem</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.Name+`</option>`;
			});
			$('#TypeItem').html(row);
			$('.the-loader').hide();
		}
	})
}ListTypeItem();
$('#BtnCari').click(function(){
	$('.the-loader').show();
	if($('#TypeItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TypeItem Tidak boleh kosong'
		});return false;
	}
	if($('#CariNameItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'CariNameItem Tidak boleh kosong'
		});return false;
	}
	$.ajax({
		url:'EstimasiMaterial.aspx/ListNameItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data:JSON.stringify({
			type:$('#TypeItem').val(),
			name:$('#CariNameItem').val()
		}),
		success:function(output){
			var row = '<option value="">Pilihan NameItem</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.ItemName+`</option>`;
			});
			$('#NameItem').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#NameItem').change(function(){
	$('.the-loader').show();
	$.ajax({
		url:'EstimasiMaterial.aspx/InfoNameItem',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data:JSON.stringify({
			item:$('#NameItem').val(),
			type:$('#TypeItem').val()
		}),
		success:function(output){
			$.each(output.d, function (i, b) {
				$('#HargaPoTerakhir').val(b.Price);
				$('#TanggalPo').val(b.Tanggal);
			});
			$('.the-loader').hide();
		}
	})
});
$('#KodeProject').change(function(){
	ListMaterial();
	$('.the-loader').show();
	$.ajax({
		url:'EstimasiMaterial.aspx/InfoKodeProject',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data:JSON.stringify({
			Id:$('#KodeProject').val()
		}),
		success:function(output){
			$.each(output.d, function (i, b) {
				$('#NameProject').val(b.ProjectName);
			});
			$('.the-loader').hide();
		}
	})
});
function ListMaterial(){
	$('.the-loader').show();
	$.ajax({
		url:'EstimasiMaterial.aspx/ListMaterial',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data:JSON.stringify({
			Id:$('#KodeProject').val()
		}),
		success:function(output){
			var row=``;
			$.each(output.d,function(i,b){
				var aksi=`
				<label class="btn btn-primary btn-xs btn-data" 
				Id="`+b.Id+`"
				ItemId="`+b.ItemId+`"
				ItemTypeId="`+b.ItemTypeId+`"
				ItemCode="`+b.ItemCode+`"
				ItemName="`+b.ItemName+`"
				Jumlah="`+b.Jumlah+`"
				Harga="`+b.Harga+`"
				Schedule="`+b.Schedule+`"
				><i class="fa fa-edit"></i></label>
				<label class="btn btn-danger btn-xs btn-hapus" Id="`+b.Id+`"><i class="fa fa-times"></i></label>
				`
				if (b.Id==999){aksi='';}
				row+=`
				<tr>
				<td>`+aksi+`</td>
				<td>`+b.ItemCode+`</td>
				<td>`+b.ItemName+`</td>
				<td>`+b.UomCode+`</td>
				<td>`+b.Jumlah+`</td>
				<td>`+b.Harga+`</td>
				<td>`+b.TotalHarga+`</td>
				<td>`+b.Schedule+`</td>
				</tr>
				`
			});
			$('#list-dtl').html(row);
			$('.the-loader').hide();
		}
	})
}
$('#list-dtl').on('click','.btn-hapus',function(){
	$('.the-loader').show();
	$.ajax({
		url: 'EstimasiMaterial.aspx/DeleteData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			Id:$(this).attr('Id')
		}),
		success:function(data){
			if(data.d!=''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Delete Estimati Material Berhasil'
				});
				ListMaterial();
				$('.the-loader').hide();
			}else{
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Perintah Gagal Dikerjakan',
				});
			}
		}
	})
});
$('#list-dtl').on('click','.btn-data',function(){
	$('.the-loader').show();
	$('#BtnAddItem').hide();
	$('#BtnUpdateItem').show();
	$('#Id').val($(this).attr('Id'));
	$('#NameItem').html(`<option value="`+$(this).attr('ItemId')+`">`+$(this).attr('ItemCode')+` - `+$(this).attr('ItemName')+`</option>`);
	$('#Quantity').val($(this).attr('Jumlah'));
	$('#HargaPoTerakhir').val($(this).attr('Harga'));
	$('#TanggalPemakaian').val($(this).attr('Schedule'));
	$('.the-loader').hide();
	$('#NameItem').attr('disabled','disabled'); 
	$('#CariNameItem').attr('disabled','disabled'); 
	$('#BtnCari').attr('disabled','disabled'); 
});
$('#BtnUpdateItem').click(function(){
	$('.the-loader').show();
	if($('#NameItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NameItem Tidak boleh kosong'
		});return false;
	}
	if($('#Quantity').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Quantity Tidak boleh kosong'
		});return false;
	}
	if($('#TanggalPemakaian').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TanggalPemakaian Tidak boleh kosong'
		});return false;
	}
	var isi={
		Id:$('#Id').val(),
		Jumlah:$('#Quantity').val(),
		Harga:$('#HargaPoTerakhir').val(),
		TanggalPakai:$('#TanggalPemakaian').val()		
	}
	$.ajax({
		url: 'EstimasiMaterial.aspx/UpdateData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isi:isi
		}),
		success:function(data){
			if(data.d!=''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Update Estimati Material Berhasil'
				});
				$('#BtnAddItem').show();
				$('#BtnUpdateItem').hide();
				isi='';
				ListMaterial();
				$('#NameItem').removeAttr('disabled');
				$('#CariNameItem').removeAttr('disabled');
				$('#BtnCari').removeAttr('disabled');
				$('#Id').val('');
				$('.the-loader').hide();
			}else{
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Perintah Gagal Dikerjakan',
				});
			}
		}
	})
});
$('#BtnAddItem').click(function(){
	$('.the-loader').show();
	if($('#NameItem').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NameItem Tidak boleh kosong'
		});return false;
	}
	if($('#Quantity').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Quantity Tidak boleh kosong'
		});return false;
	}
	if($('#TanggalPemakaian').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TanggalPemakaian Tidak boleh kosong'
		});return false;
	}
	var Harga=$('#HargaPoTerakhir').val();if($('#HargaPoTerakhir').val()==""){Harga=0;}
	var isi={
		ItemID:$('#NameItem').val(),
		ProjectID:$('#KodeProject').val(),
		Jumlah:$('#Quantity').val(),
		Harga:Harga,
		ItemTypeId:$('#TypeItem').val(),
		TanggalPakai:$('#TanggalPemakaian').val()		
	}
	$.ajax({
		url: 'EstimasiMaterial.aspx/AddData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isi:isi
		}),
		success:function(data){
			if(data.d!=''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Add Estimati Material Berhasil'
				});
				isi='';
				ListMaterial();
				$('.the-loader').hide();
			}else{
				$('.the-loader').hide();$.alert({
					icon: 'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content: 'Perintah Gagal Dikerjakan',
				});
			}
		}
	})
});
$('.BtnRefresh').click(function(){
	ListKodeProject();
	ListTypeItem();
	$('#list-dtl').html('');
	$('#NameItem').html('');
	$('#NamaProject').val('');
	$('#CariNameItem').val('');
	$('#HargaPoTerakhir').val('');
	$('#TanggalPo').val('');
	$('#Quantity').val('');
	$('#TanggalPemakaian').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	$('#BtnAddItem').show();
	$('#BtnUpdateItem').hide();
	isi='';
	$('#NameItem').removeAttr('disabled');
	$('#CariNameItem').removeAttr('disabled');
	$('#BtnCari').removeAttr('disabled');
	$('#Id').val('');
});

function ListDeptPemohon(){
	$('.the-loader').show();
	$.ajax({
		url: 'EstimasiMaterial.aspx/ListDeptPemohon',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
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
var optionStatusProject=`
<option value="">Pilihan Status</option>
<option value="0">Open</option>
<option value="2">Release</option>
<option value="21">Finish</option>
<option value="3">Close</option>
<option value="4">Pending</option>
<option value="-1">Cancel</option>
`;$('#StatusProject-list').html(optionStatusProject);

var isPertama = true;
var isPertamadetail = true;
var id;
var oTblReport;
var ID, GroupID, ApproveDate1, ApproveDate2, ApproveDate3;
$('#BtnSearchData').click(function(){
	$('.the-loader').show();
	$.ajax({
		url: "EstimasiMaterial.aspx/ListData",type: "POST",contentType: "application/json; charset=utf-8",dataType: "json",
		data : JSON.stringify({
			status:$('#StatusProject-list').val(),
			dept:$('#DeptPemohon-list').val(),
			nomor:$('#KodeProject-list').val()
		}),
		success: function (data) {
			if (!isPertama) {
				$("#datatable").DataTable().destroy();
				$('#datatable').empty();
			} else {
				isPertama = false;
			}
			datatable = $.parseJSON(data.d);
			oTblReport = $("#datatable").DataTable({
				"data": datatable,
				"pageLength": 50,
				"columns": [
				{
					'className': 'details-control',
					'orderable': false,
					'data': null,
					'defaultContent': '',
					'render': function (data, type, full, meta) {
						var aksi = "<i class='fa fa-check'></i>";
						return aksi;
					},
					'createdCell': function (td, cellData, rowData, row, col) {
						$(td).attr('id', 'td_details' + row);
					}
				},
				{ "data": "Id", title: "Id" },
				{ "data": "KodeProject", title: "KodeProject" },
				{ "data": "ProjectName", title: "ProjectName" },
				{ "data": "Sasaran", title: "Sasaran" },
				{ "data": "FromDate", title: "FromDate" },
				{ "data": "ToDate", title: "ToDate" },
				{ "data": "EstimasiBiaya", title: "EstimasiBiaya" },
				{ "data": "BiayaActual", title: "BiayaActual" },
				{ "data": "Approval", title: "Approval" },
				{ "data": "Status", title: "Status" }
				]
			});
			oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
			$('.the-loader').hide();
		}
	});
});

$('#datatable').on('click', 'td.details-control', function () {
	var tr = $(this).closest('tr');
	var row = oTblReport.row(tr);
	if(row.child.isShown()) {
		row.child.hide();
		tr.removeClass('shown');
	}else{
		row.child(format(row.data())).show();
		tr.addClass('shown');
	}
});

function format(d) {
	$('.the-loader').show();
	var html;
	$.ajax({
		url: "EstimasiMaterial.aspx/ListDetail",type: "POST",contentType: "application/json; charset=utf-8",dataType: "json",async: false,
		data: JSON.stringify({ Id: d.Id }),
		success: function (data) {
			html=`
			<div style="width: 100%;">
			<div class="table-responsive">
			<table class="table table-bordered" style="width: 100%" >
			<tr>
			<th>ItemCode</th>
			<th>ItemName</th>
			<th>Satuan</th>
			<th>QtyPlaning</th>
			<th>HargaPlaning</th>
			<th>QtyActual</th>
			<th>HargaActual</th>
			<th>Selisih</th>
			</tr>
			<tbody>
			`;
			for (i = 0; i < data.d.length; i++) {
				html+=`
				<tr>
				<th>`+data.d[i].ItemCode+`</th>
				<th>`+data.d[i].ItemName+`</th>
				<th>`+data.d[i].UomCode+`</th>
				<th>`+data.d[i].Jumlah+`</th>
				<th>`+data.d[i].Selisih+`</th>
				<th>`+data.d[i].QtyAktual+`</th>
				<th>`+data.d[i].TotalAktual+`</th>
				<th>`+data.d[i].HargaAktual+`</th>
				</tr>`;
			}
			html+=`
			</tbody>
			</table>
			</div>
			</div>
			`;
			$('.the-loader').hide();
		}
	});
	return html;
}

$('#datatable').on('page.dt', function () {
	oTblReport.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
});

$('#BtnClearData').click(function(){
	$('#datatable').DataTable().clear().draw();
});