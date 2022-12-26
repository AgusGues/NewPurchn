var isiDetail; isiDetail= { isiDtl:[] };
$('.select2').select2();
$('#datatable').DataTable({
	"ordering": false,"bPaginate": false,"bLengthChange": false,"bFilter": true,"bInfo": false,"bAutoWidth": false,//nonaktif sortir&hiden show entri
	dom: 'Bfrtip',buttons: ['excel', 'pdf', 'print']//export data
});
function ListAsset(){
	$('.the-loader').show();
	$.ajax({
		url: 'SerahTerimaAsset.aspx/ListAsset',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
		success: function (output) {
			var row = '<option value="">Pilihan Asset</option>';
			$.each(output.d, function (i, b) {
				row+=`<option value="`+b.ItemCode +`">`+b.ItemCode+` - `+b.ItemName+` </option>`;
			});
			$('#NamaAsset').html(row);
			$('.the-loader').hide();
		}
	})
}ListAsset();
function GoRefresh(){
	$('.the-loader').show();
	$('#KodeAsset').val('');
	$('#TanggalMulai').val('');
	$('#TanggalSelesai').val('');
	$('#DeptPemilikAsset').val('');
	$('#DeptPembuatAsset').val('');
	$('#StatusAsset').val('');
	$('#GrandTotalPrice').val(0);
	$('#BtnAprov').hide();
	$('#BtnSerah').hide();
	$('#datatable').DataTable().clear().draw();
	isiHead='';
	isiDetail= { isiDtl:[] };
	$('.the-loader').hide();
}
$('#NamaAsset').change(function(){
	$('.the-loader').show();
	if($('#NamaAsset').val()==''){
		GoRefresh();
	}else{
		$.ajax({
			url: 'SerahTerimaAsset.aspx/InfoAsset',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				ItemCode:$('#NamaAsset').val()
			}),
			success: function (output) {
				$.each(output.d, function (i, b) {
					$('#KodeAsset').val($('#NamaAsset').val());
					$('#TanggalMulai').val(b.TglMulai);
					$('#TanggalSelesai').val(b.TglSelesai);
					$('#DeptPemilikAsset').val(b.DeptPemilikAsset);
					$('#DeptPembuatAsset').val(b.DeptPembuatAsset);
					$('#StatusAsset').val(b.StatusAsset);
					$('#BtnAprov').hide();if(b.BtnAprov=='true'){$('#BtnAprov').show();}
					$('#BtnSerah').hide();if(b.BtnSerah=='true'){$('#BtnSerah').show();}
				});
				$('.the-loader').hide();
			}
		})
		$('#datatable').DataTable().clear().draw();
		$.ajax({
			url: 'SerahTerimaAsset.aspx/InfoDetail',type: 'POST',contentType: 'application/json; charset=utf-8',dataType: 'json',
			data : JSON.stringify({
				ItemCode:$('#NamaAsset').val()
			}),
			success: function (output) {
				var row=[];var GrandTotalPrice=0;isiDetail= { isiDtl:[] };
				$.each(output.d, function (i, b) {
					var Price=parseInt(b.Price).toLocaleString();
					var TotalPrice=parseInt(b.TotalPrice).toLocaleString();
					GrandTotalPrice+=parseInt(b.TotalPrice);
					row.push([b.ItemCode,b.ItemName,b.UomCode,b.QtyPakai,Price,TotalPrice]);
					isiDtl={
						ItemCode:b.ItemCode,
						ItemName:b.ItemName,
						QtyPakai:b.QtyPakai,
						Price:b.Price
					}
					isiDetail.isiDtl.push(isiDtl);
				});
				row.push(['','GrandTotalPrice','','','',parseInt(GrandTotalPrice).toLocaleString()]);
				$('#datatable').DataTable().rows.add(row).draw();
				$('#GrandTotalPrice').val(GrandTotalPrice);
				$('.the-loader').hide();
			}
		})
	}
});
$('#BtnSerah').click(function(){
	$('.the-loader').show();
	if($('#NamaAsset').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NamaAsset Tidak boleh kosong'
		});return false;
	}
	if($('#TanggalSelesai').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'TanggalSelesai Tidak boleh kosong'
		});return false;
	}
	var isiHead={
		ItemCode:$('#KodeAsset').val(),
		TanggalMulai:$('#TanggalMulai').val(),
		TanggalSelesai:$('#TanggalSelesai').val()
	}
	$.ajax({
		url: 'SerahTerimaAsset.aspx/SerahData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({isiHead:isiHead, isiDetail:isiDetail}),
		success: function(data) {
			if(data.d==''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Serah Asset Berhasil'
				});
				ListAsset();
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
$('#BtnAprov').click(function(){
	$('.the-loader').show();
	if($('#NamaAsset').val()==''){
		$('.the-loader').hide();$.alert({
			icon: 'fa fa-times',theme: 'modern',title: 'Warning!',type: 'red',
			content: 'NamaAsset Tidak boleh kosong'
		});return false;
	}
	var isiHead={
		ItemCode:$('#KodeAsset').val(),
		NilaiAsset:$('#GrandTotalPrice').val()
	}
	$.ajax({
		url: 'SerahTerimaAsset.aspx/AprovData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
		data : JSON.stringify({isiHead:isiHead}),
		success: function(data) {
			if(data.d==''){
				$.alert({
					icon: 'fa fa-check',theme: 'modern',title: 'Sukses!',type: 'green',
					content: 'Approve Asset Berhasil'
				});
				ListAsset();
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
	ListAsset();
	GoRefresh();
});
