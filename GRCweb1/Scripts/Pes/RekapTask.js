$('#DariTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('#SampaiTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
$('.select2').select2();
$('#datatable').DataTable({
	lengthMenu:[ [-1], ['ALL']],
	"targets": 'no-sort',"bSort": false,"order": [],
	dom: 'Bfrtip',buttons: ['excel', 'pdf', 'print']
});
var optionStatus=`
<option value="0">All</option>
<option value="1">Solved</option>
<option value="2">UnSolved</option>
<option value="3">Cancel</option>
`;$('#Status').html(optionStatus);
function ListDept(){
	$('.the-loader').show();
	$.ajax({
		url: 'RekapTask.aspx/ListDept',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
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
		url: 'RekapTask.aspx/ListPic',type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
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
	if($('#DariTanggal').val()=='0'){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'DariTanggal Belum Di Pilih'
		});return false;
	}
	if($('#SampaiTanggal').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'SampaiTanggal Belum Di Pilih'
		});return false;
	}
	var PicUser=$('#PicUser').val();if($('#PicUser').val()==''){PicUser=0;}
	$('#datatable').DataTable().clear().draw();
	
	$.ajax({
		url: 'RekapTask.aspx/ListData',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		data : JSON.stringify({
			dept:$('#Department').val(),
			user:PicUser
		}),
		success:function(output){
			$.each(output.d,function(i,b){
				var row=[];
				var userpes=b.UserID;
				$.ajax({
					url: 'RekapTask.aspx/ListDataDtl',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
					data : JSON.stringify({
						dept:$('#Department').val(),
						user:userpes,
						DariTanggal:$('#DariTanggal').val(),
						SampaiTanggal:$('#SampaiTanggal').val(),
						Status:$('#Status').val()
					}),
					success:function(output){
						$.each(output.d,function(i,b){
							row.push([
								b.UserName,
								b.TaskNo,
								b.TaskName,
								b.TglMulai,
								b.Target1,
								b.Target2,
								b.Target3,
								b.Target4,
								b.Target5,
								b.Target6,
								b.TglSelesai,
								b.NilaiBobot,
								b.Point,
								b.Score
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
	$('#Status').html(optionStatus);
	$('#DariTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	$('#SampaiTanggal').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', new Date());
	ListDept();
	$('#datatable').DataTable().clear().draw();
});

