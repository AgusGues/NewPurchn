$('#TanggalPemakaian').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL']],'order':[[0,'desc']],
});
var optionTypeUnitKerja=`
<option value="0">Pilihan TypeUnitKerja</option>
<option value="1">Distributor</option>
<option value="2">Depo</option>
`;$('#TypeUnitKerja').html(optionTypeUnitKerja);
$('#UserName').autocomplete({
	source: function (request, response) {
		$.ajax({
			url: 'MasterUserISO.aspx/ListUserName', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
			data : JSON.stringify({
				UserName:$('#UserName').val()
			}),
			success: function (data) {
				response(data.d);
			},
			error: function (jqXHR, exception) {
			}
		});
	},
	select: function (event, ui) {
		$('#UserName').val(ui.item.UserName);
		if(ui.item.Id!=0){$('#UserId').val(ui.item.Id);}else{$('#UserId').val(-1);}
		return false;
	}
}).data('ui-autocomplete')._renderItem = function (ul, item) {
	return $("<li>").append("<a>" + item.UserName + "</a></li>").appendTo(ul);
};
function ListCompany(){
	$('.the-loader').show();
	$.ajax({
		url: 'MasterUserISO.aspx/ListCompany',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan Company</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.Nama+`</option>`;
			});
			$('#Company').html(row);
			$('.the-loader').hide();
		}
	})
}ListCompany();
function ListDepartment(){
	$('.the-loader').show();
	$.ajax({
		url: 'MasterUserISO.aspx/ListDepartment',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan Department</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.DeptName+`</option>`;
			});
			$('#Department').html(row);
			$('.the-loader').hide();
		}
	})
}ListDepartment();
$('#TypeUnitKerja').change(function(){
	$('.the-loader').show();
	if($('#TypeUnitKerja').val()==''){
		$('#UnitKerja').html('<option value=""></option>');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'MasterUserISO.aspx/ListUnitKerja',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({type:$('#TypeUnitKerja').val()}),
			success: function (output) {
				var row = '<option value="">Pilihan UnitKerja</option>';
				$.each(output.d, function (i, b) {
					row+=`<option value="`+b.Id +`">`+b.Name+`</option>`;
				});
				$('#UnitKerja').html(row);
				$('.the-loader').hide();
			}
		})
	}
});
$('#Department').change(function(){
	$('.the-loader').show();
	if($('#Department').val()==''){
		$('#Jabatan').html('<option value=""></option>');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'MasterUserISO.aspx/ListJabatan',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({dept:$('#Department').val()}),
			success: function (output) {
				var row = '<option value="">Pilihan Jabatan</option>';
				$.each(output.d, function (i, b) {
					row+=`<option value="`+b.Id +`">`+b.BagianName+`</option>`;
				});
				$('#Jabatan').html(row);
				$('.the-loader').hide();
			}
		})
	}
});

function ListData(){
	$('.the-loader').show();
	$('#datatable').DataTable().clear().draw();
	$.ajax({
		url: 'MasterUserISO.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success:function(output){
			var row=[];
			$.each(output.d,function(i,b){
				var act=`
				<div class="btn-group">
				<label class="btn btn-primary btn-xs btn-data" 
				Id="`+b.Id+`"
				UserID="`+b.UserId+`" UserName="`+b.UserName+`" Nik="`+b.Nik+`"
				TypeUnitKerjaId="`+b.TypeUnitKerjaId+`" TypeUnitKerjaName="`+b.TypeUnitKerjaName+`"
				UnitKerjaId="`+b.UnitKerjaId+`" UnitKerjaName="`+b.UnitKerjaName+`"
				CompanyId="`+b.CompanyId+`" CompanyName="`+b.CompanyName+`"
				DeptId="`+b.DeptId+`" DeptName="`+b.DeptName+`"
				BagianId="`+b.BagianId+`" BagianName="`+b.BagianName+`"
				><i class="fa fa-edit"></i></label>
				<label class="btn btn-danger btn-xs btn-hapus" Id="`+b.Id+`"><i class="fa fa-times-circle-o"></i></label>
				</div>`;
				row.push([act,b.UserName,b.Nik,b.TypeUnitKerjaName,b.UnitKerjaName,b.CompanyName,b.DeptName,b.BagianName]);
			});
			$('#datatable').DataTable().rows.add(row).draw();
			$('.the-loader').hide();
		}
	})
}ListData();
$('#datatable').on('click','.btn-data',function(){
	$('.the-loader').show();
	$('#Id').val($(this).attr('Id'));
	$('#UserId').val($(this).attr('UserId'));$('#UserName').val($(this).attr('UserName'));
	$('#Nik').val($(this).attr('Nik'));
	var TypeUnitKerja=`<option value="`+$(this).attr('TypeUnitKerjaId')+`">`+$(this).attr('TypeUnitKerjaName')+`</option>`+optionTypeUnitKerja;
	$('#TypeUnitKerja').html(TypeUnitKerja);
	var UnitKerja=`<option value="`+$(this).attr('UnitKerjaId')+`">`+$(this).attr('UnitKerjaName')+`</option>`;
	$('#UnitKerja').html(UnitKerja);
	var Jabatan=`<option value="`+$(this).attr('BagianId')+`">`+$(this).attr('BagianName')+`</option>`;
	$('#Jabatan').html(Jabatan);
	var CompanyId=$(this).attr('CompanyId'); var CompanyName=$(this).attr('CompanyName');
	$.ajax({
		url: 'MasterUserISO.aspx/ListCompany',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = `
			<option value="`+CompanyId+`">`+CompanyName+`</option>
			<option value="">Pilihan Company</option>
			`;
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.Nama+`</option>`;
			});
			$('#Company').html(row);
			$('.the-loader').hide();
		}
	})
	var DeptId=$(this).attr('DeptId'); var DeptName=$(this).attr('DeptName');
	$.ajax({
		url: 'MasterUserISO.aspx/ListDepartment',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = `
			<option value="`+DeptId+`">`+DeptName+`</option>
			<option value="">Pilihan Department</option>
			`;
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.DeptName+`</option>`;
			});
			$('#Department').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#datatable').on('click','.btn-hapus',function(){
	$('.the-loader').show();
	var isi={
		Id:$(this).attr('Id')
	}
	$.ajax({
		url: 'MasterUserISO.aspx/DeleteData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isi:isi
		}),
		success:function(data){
			if(data.d!=''){
				$.alert({
					icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content:'Hapus Data UserPes Berhasil'
				});
				GoRefresh();isi={};
			}else{
				$('.the-loader').hide();$.alert({
					icon:'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content:'Perintah Gagal Dikerjakan',
				});
			}
			$('.the-loader').hide();
		}
	})
});
$('#BtnSave').click(function(){
	$('.the-loader').show();
	if($('#UserName').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'UserName Tidak boleh kosong'
		});return false;
	}
	if($('#Nik').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Nik Tidak boleh kosong'
		});return false;
	}
	if($('#Company').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Company Tidak boleh kosong'
		});return false;
	}
	if($('#TypeUnitKerja').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TypeUnitKerja Tidak boleh kosong'
		});return false;
	}
	if($('#UnitKerja').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'UnitKerja Tidak boleh kosong'
		});return false;
	}
	if($('#Department').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Department Tidak boleh kosong'
		});return false;
	}
	if($('#UnitKerja').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TypeUnitKerja Tidak boleh kosong'
		});return false;
	}
	if($('#Jabatan').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Jabatan Tidak boleh kosong'
		});return false;
	}
	var isi={
		Id:$('#Id').val(),
		UserName:$('#UserName').val(),
		UserID:$('#UserId').val(),
		Nik:$('#Nik').val(),
		Company:$('#Company').val(),
		TypeUnitKerja:$('#TypeUnitKerja').val(),
		UnitKerja:$('#UnitKerja').val(),
		Department:$('#Department').val(),
		DepartmentName:$('#Department option:selected').text(),
		Jabatan:$('#Jabatan').val()
	}
	$.ajax({
		url: 'MasterUserISO.aspx/ProsesData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isi:isi
		}),
		success:function(data){
			if(data.d!=''){
				if($('#Id').val()==0){
					$.alert({
						icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
						content:'Add Data UserPes Berhasil'
					});
					GoRefresh();isi={};
				}else{
					$.alert({
						icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
						content:'Edit Data UserPes Berhasil'
					});
				}
			}else{
				$('.the-loader').hide();$.alert({
					icon:'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
					content:'Perintah Gagal Dikerjakan',
				});
			}
			$('.the-loader').hide();
		}
	})
});
function GoRefresh(){
	$('#Id').val(0);
	$('#UserId').val(-1);
	$('#UserName').val('');
	$('#Nik').val('');
	ListCompany();
	ListDepartment();
	ListData();
	$('#TypeUnitKerja').html(optionTypeUnitKerja);
	$('#UnitKerja').html('<option value=""></option>');
	$('#Jabatan').html('<option value=""></option>');
}
$('.BtnRefresh').click(function(){
	GoRefresh();
});




