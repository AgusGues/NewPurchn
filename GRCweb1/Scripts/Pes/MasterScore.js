$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [5,10,20,50,100,-1], [5,10,20,50,100,'ALL']],'order':[[0,'desc']],
});
var optionPesType=`
<option value="">Pilihan Category</option>
<option value="1">Kpi</option>
<option value="2">Task</option>
<option value="3">Sop</option>
`;$('#PesType').html(optionPesType);
function ListDepartment(){
	$('.the-loader').show();
	$.ajax({
		url: 'MasterPESScore.aspx/ListDepartment',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
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
$('#Department').change(function(){
	$('.the-loader').show();
	$.ajax({
		url: 'MasterPESScore.aspx/ListSection',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			dept:$('#Department').val()
		}),
		success: function (output) {
			var row = '<option value="">Pilihan Section</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.BagianName+`</option>`;
			});
			$('#Section').html(row);
			$('.the-loader').hide();
		}
	})
});
function ListCategory(){
	$('.the-loader').show();
	if($('#Section').val()=='' || $('#PesType').val()==''){
		$('#Category').html('');
		$('.the-loader').hide();
	}else if($('#Section').val()=='' && $('#PesType').val()==''){
		$('#Category').html('');
		$('.the-loader').hide();
	}else{
		$.ajax({
			url: 'MasterPESScore.aspx/ListCategory',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				Section:$('#Section').val(),
				PesType:$('#PesType').val()
			}),
			success: function (output) {
				var row = '<option value="">Pilihan Category</option>';
				$.each(output.d, function (i, b) {
					row+=`<option value="`+b.CategoryID +`">`+b.Description+`</option>`;
				});
				$('#Category').html(row);
				$('.the-loader').hide();
			}
		})
	}
}
$('#Section').change(function(){
	ListCategory();
});
$('#PesType').change(function(){
	ListCategory();
});
$('#Category').change(function(){
	$('.the-loader').show();
	if($('#Category').val()==''){
		$('#datatable').DataTable().clear().draw();
		$('.the-loader').hide();
	}else{
		$('#datatable').DataTable().clear().draw();
		$.ajax({
			url: 'MasterPESScore.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				Category:$('#Category').val()
			}),
			success:function(output){
				var row=[];
				$.each(output.d,function(i,b){
					var act=`
					<label class="btn btn-primary btn-xs btn-data" 
					Id="`+b.Id+`" 
					DeptId="`+b.DeptId+`" DeptName="`+b.DeptName+`" 
					SectionId="`+b.SectionId+`" BagianName="`+b.BagianName+`" 
					PesType="`+b.PesType+`" Category="`+b.Category+`" 
					CategoryId="`+b.CategoryId+`"  Description="`+b.Description+`" 
					TargetKe="`+b.TargetKe+`" PointNilai="`+b.PointNilai+`"
					><i class="fa fa-edit"></i></label>
					`;
					row.push([act,b.DeptName,b.BagianName,b.Category,b.Description,b.TargetKe,b.PointNilai]);
				});
				$('#datatable').DataTable().rows.add(row).draw();
				$('.the-loader').hide();
			}
		})
	}
});
$('#datatable').on('click','.btn-data',function(){
	$('.the-loader').show();
	$('#Id').val($(this).attr('Id'));
	$('#PesType').html(`<option value="`+$(this).attr('PesType')+`">`+$(this).attr('Category')+`</option>`+optionPesType);
	$('#Target').val($(this).attr('TargetKe'));
	$('#Score').val($(this).attr('PointNilai'));
	var PesType=$(this).attr('PesType');
	var DeptId=$(this).attr('DeptId'); var DeptName=$(this).attr('DeptName');
	var SectionId=$(this).attr('SectionId'); var BagianName=$(this).attr('BagianName');
	var CategoryId=$(this).attr('DeptId'); var Description=$(this).attr('Description');
	$.ajax({
		url: 'MasterPESScore.aspx/ListDepartment',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
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
	$.ajax({
		url: 'MasterPESScore.aspx/ListSection',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			dept:DeptId
		}),
		success: function (output) {
			var row = `
			<option value="`+SectionId+`">`+BagianName+`</option>
			<option value="">Pilihan Section</option>
			`;
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.Id +`">`+b.BagianName+`</option>`;
			});
			$('#Section').html(row);
			$('.the-loader').hide();
		}
	})
	$.ajax({
		url: 'MasterPESScore.aspx/ListCategory',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
				Section:SectionId,
				PesType:PesType
			}),
		success: function (output) {
			var row = `
			<option value="`+CategoryId+`">`+Description+`</option>
			<option value="">Pilihan Category</option>
			`;
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.CategoryID +`">`+b.Description+`</option>`;
			});
			$('#Category').html(row);
			$('.the-loader').hide();
		}
	})
});
$('#BtnDel').click(function(){
	$('.the-loader').show();
	if($('#Id').val()!=0){
		var isi={
			Id:$('#Id').val()
		}
		$.ajax({
			url: 'MasterPESScore.aspx/DeleteData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
			data : JSON.stringify({
				isi:isi
			}),
			success:function(data){
				if(data.d!=''){
					$('.the-loader').hide();$.alert({
						icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
						content:'Delete Data MasterPESScore Berhasil'
					});
					GoRefresh();isi={};
				}else{
					$('.the-loader').hide();$.alert({
						icon:'fa fa-times',theme: 'modern',title: 'Gagal!',type: 'red',
						content:'Perintah Gagal Dikerjakan',
					});
				}
			}
		})
	}else{
		$('.the-loader').hide();
	}
});
function GoRefresh(){
	$('#Id').val(0);
	ListDepartment();
	$('#PesType').html(optionPesType);
	$('#Section').html('');
	$('#Category').html('');
	$('#Target').val('');
	$('#Score').val('');
	$('#datatable').DataTable().clear().draw();
}
$('.BtnRefresh').click(function(){
	GoRefresh();
});
$('#BtnSave').click(function(){
	$('.the-loader').show();
	if($('#Department').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Department Tidak boleh kosong'
		});return false;
	}
	if($('#PesType').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'PesType Tidak boleh kosong'
		});return false;
	}
	if($('#Section').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Section Tidak boleh kosong'
		});return false;
	}
	if($('#Category').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Category Tidak boleh kosong'
		});return false;
	}
	if($('#Target').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Target Tidak boleh kosong'
		});return false;
	}
	if($('#Score').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'Score Tidak boleh kosong'
		});return false;
	}
	var isi={
		Id:$('#Id').val(),
		PesType:$('#PesType').val(),
		CategoryId:$('#Category').val(),
		TargetKe:$('#Target').val(),
		PointNilai:$('#Score').val()
	}
	$.ajax({
		url: 'MasterPESScore.aspx/ProsesData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({
			isi:isi
		}),
		success:function(data){
			if(data.d!=''){
				if($('#Id').val()==0){
					$.alert({
						icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
						content:'Add Data MasterPESScore Berhasil'
					});
					GoRefresh();isi={};
				}else{
					$.alert({
						icon:'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
						content:'Edit Data MasterPESScore Berhasil'
					});
					GoRefresh();isi={};
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