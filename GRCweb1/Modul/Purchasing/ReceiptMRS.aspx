<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiptMRS.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ReceiptMRS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
		.the-loader{
			position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
			text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
		}
		.input-xs{
			font-size: 11px;height: 11px;width: 100%;
		}
		.modal-footer{
			padding-left: : 1px;padding-top: 2px;padding-right: 1px;padding-bottom: 2px;
		}
		.btn-data{padding:1px 1px;font-size:8px;line-height:0.5;border-radius:3px;}
	</style>
</head>
<body class="no-skin">
	<div class="row">
		<div class="col-md-12">
			<div class="panel panel-primary">
				<div class=panel-heading>
					<span class="the-title">Input Receipt</span>
					<div class="pull-right">
						<label class="btn btn-primary btn-xs BtnRefresh">NewInput</label>
						<label class="btn btn-primary btn-xs" id="panel-form">Form</label>
						<label class="btn btn-primary btn-xs" id="panel-list">ListReceipt</label>
					</div>
				</div>
				<div class="panel-body panel-list" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">KodeReceipt</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodeReceipt-list">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">KodePo</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodePo-list">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">KodeSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodeSpp-list">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12 text-right">
									<label class="btn btn-primary btn-xs" id="BtnClearData">Clear</label>
									<label class="btn btn-primary btn-xs" id="BtnSearchData">Find</label>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="table-responsive">
						<table id="datatable" class="table" style="width: 100%;">
							<thead>
								<tr>
									<th>.</th>
									<th>KodeReceipt</th>
									<th>TanggalReceipt</th>
									<th>KodePO</th>
									<th>KodeSpp</th>
									<th>ItemCode</th>
									<th>ItemName____</th>
									<th>Quantity</th>
									<th>Status</th>
								</tr>
							</thead>
							<tbody></tbody>
						</table>
					</div>
				</div>
				<div class="panel-body panel-form">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">KodeReceipt</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodeReceipt" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Tanggal</div>
								<div class="col-md-8">
									<div class="input-group">
										<input class="form-control input-sm date-picker" id="Tanggal" type="text">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">TypeReceipt</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeReceipt" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">NoPo</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="NoPo" style="width: 100%;"></select>
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
							<div class="row">
								<div class="col-md-4">ItemCode</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="ItemCode" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Satuan</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Satuan" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">NoSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="NoSpp" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Status</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Status" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Suplier</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Suplier" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Stock</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="Stock" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">QtyPo</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="QtyPo" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">QtyRecipt</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="QtyRecipt">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Keterangan">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-6"></div>
						<div class="col-md-6 text-right">
							<label class="btn btn-primary btn-xs" id="BtnAddItem">AddItem</label>
							<label class="btn btn-primary btn-xs" id="BtnSimpan" disabled="">Save</label>
							<label class="btn btn-primary btn-xs BtnRefresh">Clear</label>
							<div style="padding: 2px"></div>
						</div>
					</div>
					<div class="table-responsive">
						<table class="table" style="width: 100%;">
							<thead>
								<tr>
									<th>NoPo</th>
									<th>NoSpp</th>
									<th>ItemCode</th>
									<th>ItemName</th>
									<th>Quantity</th>
									<th>Satuan</th>
									<th>Keterangan</th>
									<th>Action</th>
								</tr>
							</thead>
							<tbody id="list-dtl"></tbody>
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
<script src="../../Scripts/Purchasing/ReceiptMRS.js" type="text/javascript"></script>
</asp:Content>