
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAsset.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapAsset" %>

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
					<span class="the-title">Rekap Asset</span>
				</div>
				<div class="panel-body">
					<div class="row">
						<div class="col-md-6"></div>
						<div class="col-md-6 text-right">
							<label class="btn btn-primary btn-xs" id="BtnFind">Find</label>
							<label class="btn btn-primary btn-xs BtnRefresh">Clear</label>
							<div style="padding: 2px"></div>
						</div>
					</div>
					<div style="padding: 4px"></div>
					<div class="table-responsive">
						<table class="table" id=datatable style="width: 100%;">
							<thead>
								<tr>
									<th>ItemCode</th>
									<th>ItemName____________________</th>
									<th>Satuan</th>
									<th>Saldo Awal</th>
									<th>Pembelian</th>
									<th>AdjustIn</th>
									<th>AdjustOut</th>
									<th>SaldoAkhir</th>
									<th>Kategori</th>
								</tr>
							</thead>
							<tbody id="list-data"></tbody>
						</table>
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
<script src="../../Scripts/ListReport/RekapAsset.js" type="text/javascript"></script>
</asp:Content>
