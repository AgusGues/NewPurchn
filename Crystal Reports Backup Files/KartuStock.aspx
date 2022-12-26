
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KartuStock.aspx.cs" Inherits="GRCweb1.Modul.ListReport.KartuStock" %>

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
	</style>
</head>
<body class="no-skin">
	<div class="row">
		<div class="col-md-12">
			<div class="panel panel-primary">
				<div class=panel-heading>
					<span class="the-title">Kartu Stock</span>
				</div>
				<div class="panel-body">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Status Item</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="StatusItem" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Cari ItemName</div>
								<div class="col-md-8">
									<div class="input-group">
										<input type="text" class="form-control input-sm" id="CariItemName">
										<div class="input-group-btn">
											<label class="btn btn-primary btn-xs" id="BtnCari">Cari</label>
										</div>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">ItemName</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="ItemName" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">RangePeriode</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="RangePeriode" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6">
									<div class="row" id="row-Bulan">
										<div class="col-md-4">Bulan</div>										
										<div class="col-md-8">
											<select class="form-control input-sm select2" id="Bulan" style="width: 100%;"></select>
											<div style="padding: 2px"></div>
										</div>
									</div>
								</div>
								<div class="col-md-6">
									<div class="row" id="row-Tahun">
										<div class="col-md-4">Tahun</div>
										<div class="col-md-8">
											<input type="number" class="form-control input-sm" id="Tahun">
											<div style="padding: 2px"></div>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6">
									<div class="row" id="row-Bulan2" style="display: none;">
										<div class="col-md-4">Bulan 2</div>										
										<div class="col-md-8">
											<select class="form-control input-sm select2" id="Bulan2" style="width: 100%;"></select>
											<div style="padding: 2px"></div>
										</div>
									</div>
								</div>
								<div class="col-md-6">
									<div class="row" id="row-Tahun2" style="display: none;">
										<div class="col-md-4">Tahun 2</div>
										<div class="col-md-8">
											<input type="number" class="form-control input-sm" id="Tahun2" di>
											<div style="padding: 2px"></div>
										</div>
									</div>
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
					<div class="row">
						<div class="col-md-6">
							<div class="panel panel-default" style="margin-bottom: -8px;">
								<div class="panel-body" style="padding: 6px 5px;">
									<div class="row">
										<div class="col-xs-6">ItemCode</div>
										<div class="col-xs-6"><span id="info-ItemCode"></span></div>
									</div>
									<div class="row">
										<div class="col-xs-6">ItemName</div>
										<div class="col-xs-6"><span id="info-ItemName"></span></div>
									</div>
									<div class="row">
										<div class="col-xs-6">UOMDesc</div>
										<div class="col-xs-6"><span id="info-UOMDesc"></span></div>
									</div>
								</div>
							</div>
							<div style="padding: 2px"></div>
						</div>
						<div class="col-md-6">
							<div class="panel panel-default" style="margin-bottom: -8px;">
								<div class="panel-body" style="padding: 6px 5px;">
									<div class="row">
										<div class="col-xs-6">MinStock</div>
										<div class="col-xs-6"><span id="info-MinStock"></span></div>
									</div>
									<div class="row">
										<div class="col-xs-6">MaxStock</div>
										<div class="col-xs-6"><span id="info-MaxStock"></span></div>
									</div>
									<div class="row">
										<div class="col-xs-6">ReOrder</div>
										<div class="col-xs-6"><span id="info-ReOrder"></span></div>
									</div>
								</div>
							</div>
						</div>
						<div style="padding: 2px"></div>
					</div>
					<div class="table-responsive">
					<table class="table" style="width: 100%;">
						<thead>
							<tr>
								<th>ItemCode</th>
								<th>ItemName</th>
								<th>UOMDesc</th>
								<th>MinStock</th>
								<th>MaxStock</th>
								<th>ReOrder</th>
							</tr>
						</thead>
						<tbody id="info-item"></tbody>
					</table>
				</div>
				<div style="padding: 4px"></div>
				<div class="table-responsive">
					<table class="table" style="width: 100%;">
						<thead>
							<tr>
								<th>Tanggal</th>
								<th>Bon No</th>
								<th>Keterangan</th>
								<th>Masuk</th>
								<th>Keluar</th>
								<th>Saldo</th>
							</tr>
						</thead>
						<tbody id="list-data"></tbody>
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
<script src="../../assets/Datatables/datatables.js"></script>
<script src="../../Scripts/ListReport/KartuStock.js" type="text/javascript"></script>
</asp:Content>
