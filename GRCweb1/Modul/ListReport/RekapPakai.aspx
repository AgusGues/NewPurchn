
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPakai.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapPakai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
	<meta charset="utf-8" />
	<title>Widgets - Ace Admin</title>
	<meta name="description" content="Common form elements and layouts" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
	<link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
	<link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />

	<link rel="stylesheet" href="../../assets/select2.css" />
	<link rel="stylesheet" href="../../assets/datatable.css" />
	<style>
		.panelbox {background-color: #efeded;padding: 2px;}
		html,body,.form-control,button{font-size: 11px;}
		.input-group-addon{background: white;}
		.fz11{font-size: 11px;}
		.fz10{font-size: 10px;}
		.the-loader{
			position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
			text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
		}
		.input-xs{
			font-size: 11px;height: 11px;width: 100%;
		}
		.table-scroll{position: relative;max-height: 400px;overflow:auto;width: 100%;display: block;}
	</style>
</head>
<body class="no-skin">
	<div class="row">
		<div class="col-md-12">
			<div class="panel panel-primary">
				<div class=panel-heading>
					<span class="the-title">Laporan Pemakaian</span>
				</div>
				<div class="panel-body">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Type Laporan</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeLaporan" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Group Item</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="GroupItem" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Department</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="DeptName" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Dari Tanggal</div>
								<div class="col-md-8">
									<div class="input-group">
										<input class="form-control input-sm date-picker" id="DariTanggal" type="text">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Sampai Tanggal</div>
								<div class="col-md-8">
									<div class="input-group">
										<input class="form-control input-sm date-picker" id="SampaiTanggal" type="text">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6"></div>
								<div class="col-md-6 text-right">
									<label class="btn btn-primary btn-xs" id="BtnFind">Find</label>
									<label class="btn btn-primary btn-xs BtnRefresh">Clear</label>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div style="padding: 4px"></div>
					<div class="table-responsive table-scroll">
						<table class="table" id="table-detail" style="width: 100%;;">
							<thead>
								<tr>
									<th>DeptName__________</th>
									<th>GroupDescription</th>
									<th>PakaiNo_____________</th>
									<th>PakaiDate</th>
									<th>ItemCode</th>
									<th>ItemName____________________</th>
									<th>UOMCode</th>
									<th>Jumlah</th>
									<th>Harga</th>
									<th>Total</th>
									<th>Keterangan____________</th>
									<th>Status</th>
									<th>NoPol</th>
								</tr>
							</thead>
							<tbody id="list-dataDetail"></tbody>
						</table>
						<table class="table" id="table-rekap" style="width: 100%;display: none;">
							<thead>
								<tr>
									<th>DeptName__________</th>
									<th>GroupDescription</th>
									<th>ItemCode</th>
									<th>ItemName____________________</th>
									<th>UOMCode</th>
									<th>Jumlah</th>
									<th>Harga</th>
									<th>Total</th>
								</tr>
							</thead>
							<tbody id="list-dataRekap"></tbody>
						</table>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
<div class="panel-body boxShadow the-loader" style="display: none;">
	<div style="padding:7%;"></div>
	<i class="fa fa-spinner fa-spin"></i>
</div>
</body>
</html>
<script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
<script src="../../assets/select2.js"></script>
<script src="../../assets/datatable.js"></script>
<script src="../../Scripts/ListReport/RekapPakai.js" type="text/javascript"></script>
</asp:Content>
