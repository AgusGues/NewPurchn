$('.select2').select2();
$('#datatable').DataTable({
    lengthMenu: [[5, 10, 20, 50, 100, -1], [5, 10, 20, 50, 100, 'ALL']], 'order': [[0, 'desc']],
});
var optionUserGroup = "<option value='0'>Pilihan UserGroup</option><option value='200'>Yang Di Aproval</option><option value='100'>Yang Aproval</option>";
$('#UserGroup').html(optionUserGroup);
var optionBulan = "<option value='0'>Pilihan Bulan</option><option value='1'>Januari</option><option value='2'>Februari</option><option value='3'>Maret</option><option value='4'>April</option><option value='5'>Mei</option><option value='6'>Juni</option><option value='7'>Juli</option><option value='8'>Agustus</option><option value='9'>September</option><option value='10>Oktober</option><option value='11'>November</option><option value='12'>Desember</option>";
$('#BerlakuBulan').html(optionBulan);

function ListDepartment() {

    $.ajax({
        url: 'MasterDeptSection.aspx/ListDepartment',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (output) {
            var row = '<option value="">Pilihan Department</option>';
            $.each(output.d, function (i, b) {
                row += "<option value=" + b.Id + ">" + b.DeptName + "</option>";
            });
            $('#Department').html(row);
        }
    })
} ListDepartment();
$('#Department').change(function () {
    $('.the-loader').show();
    if ($('#Department').val() == '') {
        $('#datatable').DataTable().clear().draw();
        $('.the-loader').hide();
    } else {
        $('#datatable').DataTable().clear().draw();
        $.ajax({
            url: 'MasterDeptSection.aspx/ListData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            data: JSON.stringify({
                dept: $('#Department').val()
            }),
            success: function (output) {
                var row = [];
                $.each(output.d, function (i, b) {
                    var act = "<label class='btn btn-primary btn-xs btn-data Id='"+ b.Id + "' BagianName='" + b.BagianName + 
					"' DeptId='"+ b.DeptId + "' DeptName='" + b.DeptName + "' UserGroupId="+ b.UserGroupId + "' UserGroupName='" + b.UserGroupName + 
					"' BobotKpi='" + b.BobotKpi + "' BobotSop='" + b.BobotSop + "' BobotTask= '"+ b.BobotTask + "' BobotDisiplin= '" + b.BobotDisiplin + 
					"' BerlakuBulan= '"+ b.BerlakuBulan + "' BerlakuBulanName= '"+ b.BerlakuBulanName +  "' BerlakuTahun= '"+ b.BerlakuTahun + 
					"' ><i class ='fa fa-edit'></i></label>";
                    row.push([act, b.Id, b.BagianName, b.UserGroupName, b.BobotKpi + ' %', b.BobotSop + ' %', b.BobotTask + ' %', b.BobotDisiplin + ' %', b.BerlakuBulanName, b.BerlakuTahun]);
                });
                $('#datatable').DataTable().rows.add(row).draw();
                $('.the-loader').hide();
            }
        })
    }
});
$('#datatable').on('click', '.btn-data', function () {
    $('#Id').val($(this).attr('Id'));
    $('#SectionName').val($(this).attr('BagianName'));
    $('#UserGroup').html("<option value=' " + $(this).attr('UserGroupId') + "'>" + $(this).attr('UserGroupName') + "</option>" + optionUserGroup);
    $('#BobotKpi').val($(this).attr('BobotKpi'));
    $('#BobotSop').val($(this).attr('BobotSop'));
    $('#BobotTask').val($(this).attr('BobotTask'));
    $('#BobotDisiplin').val($(this).attr('BobotDisiplin'));
    $('#BerlakuBulan').html("<option value=' " + $(this).attr('BerlakuBulan') + "'>" + $(this).attr('BerlakuBulanName') + "</option>" + optionBulan);
    $('#BerlakuTahun').val($(this).attr('BerlakuTahun'));
    var DeptId = $(this).attr('DeptId'); var DeptName = $(this).attr('DeptName');
    $.ajax({
        url: 'MasterDeptSection.aspx/ListDepartment', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        success: function (output) {
            var row = "<option value=' "+ DeptId + "'>" + DeptName + "</option> <option value='>Pilihan Department</option>";
            $.each(output.d, function (i, b) {row += "<option value=' " + b.Id + "'>" + b.DeptName + "</option>";
            });
            $('#Department').html(row);
            $('.the-loader').hide();
        }
    })
});
$('#BtnDel').click(function () {
    $('.the-loader').show();
    if ($('#Id').val() != 0) {
        var isi = {
            Id: $('#Id').val()
        }
        $.ajax({
            url: 'MasterDeptSection.aspx/DeleteData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
            data: JSON.stringify({
                isi: isi
            }),
            success: function (data) {
                if (data.d != '') {
                    $('.the-loader').hide(); $.alert({
                        icon: 'fa fa-check', theme: 'modern', title: 'Sukses!', type: 'green',
                        content: 'Delete Data MasterDeptSection Berhasil'
                    });
                    GoRefresh(); isi = {};
                } else {
                    $('.the-loader').hide(); $.alert({
                        icon: 'fa fa-times', theme: 'modern', title: 'Gagal!', type: 'red',
                        content: 'Perintah Gagal Dikerjakan',
                    });
                }
            }
        })
    } else {
        $('.the-loader').hide();
    }
});
$('#BtnSave').click(function () {
    $('.the-loader').show();
    if ($('#Department').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Department Tidak boleh kosong'
        }); return false;
    }
    if ($('#SectionName').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'SectionName Tidak boleh kosong'
        }); return false;
    }
    if ($('#UserGroup').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'UserGroup Tidak boleh kosong'
        }); return false;
    }
    if ($('#BobotKpi').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BobotKpi Tidak boleh kosong'
        }); return false;
    }
    if ($('#BobotSop').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BobotSop Tidak boleh kosong'
        }); return false;
    }
    if ($('#BobotTask').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BobotTask Tidak boleh kosong'
        }); return false;
    }
    if ($('#BobotDisiplin').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BobotDisiplin Tidak boleh kosong'
        }); return false;
    }
    if ($('#BerlakuBulan').val() == 0) {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BerlakuBulan Tidak boleh kosong'
        }); return false;
    }
    if ($('#BerlakuTahun').val() == '') {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'BerlakuTahun Tidak boleh kosong'
        }); return false;
    }
    var bobot = parseInt($('#BobotKpi').val()) + parseInt($('#BobotSop').val()) + parseInt($('#BobotTask').val()) + parseInt($('#BobotDisiplin').val());
    if (bobot != 100) {
        $('.the-loader').hide(); $.alert({
            icon: 'fa fa-times', theme: 'modern', title: 'Warning!', type: 'red',
            content: 'Total Bobot harus 100'
        }); return false;
    }
    var isi = {
        Id: $('#Id').val(),
        DeptId: $('#Department').val(),
        BagianName: $('#SectionName').val(),
        UserGroupId: $('#UserGroup').val(),
        BobotKpi: ($('#BobotKpi').val() * 0.01).toFixed(2),
        BobotSop: ($('#BobotSop').val() * 0.01).toFixed(2),
        BobotTask: ($('#BobotTask').val() * 0.01).toFixed(2),
        BobotDisiplin: ($('#BobotDisiplin').val() * 0.01).toFixed(2),
        BerlakuBulan: $('#BerlakuBulan').val(),
        BerlakuTahun: $('#BerlakuTahun').val()
    }
    $.ajax({
        url: 'MasterDeptSection.aspx/ProsesData', type: 'POST', contentType: 'application/json; charset=utf-8', dataType: 'json',
        data: JSON.stringify({
            isi: isi
        }),
        success: function (data) {
            if (data.d != '') {
                if ($('#Id').val() == 0) {
                    $.alert({
                        icon: 'fa fa-check', theme: 'modern', title: 'Sukses!', type: 'green',
                        content: 'Add Data MasterDeptSection Berhasil'
                    });
                    GoRefresh(); isi = {};
                } else {
                    $.alert({
                        icon: 'fa fa-check', theme: 'modern', title: 'Sukses!', type: 'green',
                        content: 'Edit Data MasterDeptSection Berhasil'
                    });
                    GoRefresh(); isi = {};
                }
            } else {
                $('.the-loader').hide(); $.alert({
                    icon: 'fa fa-times', theme: 'modern', title: 'Gagal!', type: 'red',
                    content: 'Perintah Gagal Dikerjakan',
                });
            }
            $('.the-loader').hide();
        }
    })
});
function GoRefresh() {
    $('#Id').val(0);
    $('#UserGroup').html(optionUserGroup);
    ListDepartment();
    $('#SectionName').val('');
    $('#BobotKpi').val('');
    $('#BobotSop').val('');
    $('#BobotTask').val('');
    $('#BobotDisiplin').val('');
    $('#BerlakuBulan').html(optionBulan);
    $('#BerlakuTahun').val('');
    $('#datatable').DataTable().clear().draw();
}
$('.BtnRefresh').click(function () {
    GoRefresh();
});
