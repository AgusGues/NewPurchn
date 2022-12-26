
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransaksiSPP3.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.TransaksiSPP3" %>

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

	<style>
		.panelbox {background-color: #efeded;padding: 2px;}
		html,body,button{font-size: 12px;}
		.input-group-addon{background: white;}
		.fz11{font-size: 11px;}
		.fz10{font-size: 10px;}
		.the-loader{
			position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
			text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
		}
		.input-xs{font-size: 11px;height: 11px;width: 100%;}
		.btn-data{padding:1px 1px;line-height:0.1;border-radius:3px;}
		.ui-datepicker {
			width: 23em;
		}
	</style>

</head>
<body class="no-skin">
	<div class="row">
		<div class="col-md-12">
			<div class="panel panel-primary">
				<div class=panel-heading>
					<span class="the-title">Input Spp</span>
					<div class="pull-right">
						<label class="btn btn-primary btn-xs BtnRefresh">NewInput</label>
						<label class="btn btn-primary btn-xs" id="panel-form">Form</label>
						<label class="btn btn-primary btn-xs" id="panel-list">List</label>
					</div>
				</div>
				<div class="panel-body panel-list" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">NoSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="list-NoSpp">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">User SPP</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="list-CreatedBy">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-12">
							<div class="row">
								<div class="col-md-12 text-right">
									<label class="btn btn-primary btn-xs" id="BtnClearData">Clear</label>
									<label class="btn btn-primary btn-xs" id="BtnSearchData">Cari</label>
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
									<th>NoSpp</th>
									<th>Tanggal</th>
									<th>ItemCode</th>
									<th>ItemName_________</th>
									<th>Qty</th>
									<th>Keterangan</th>
									<th>Status</th>
									<th>ApvLevel</th>
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
								<div class="col-md-4">No Spp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="NoSpp" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Tanggal</div>
								<div class="col-md-8">
									<div class="input-group">
										<input class="form-control input-sm date-picker" id="Tanggal" type="text" disabled="">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Dept User</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="DeptUser" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Type Minta</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeMinta" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Type Item</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeItem" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Type Spp</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeSpp" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-AssetItem" style="display: none;">
								<div class="col-md-4">Asset Item</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="AssetItem" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-NameItem">
								<div class="col-md-4">Search Item</div>
								<div class="col-md-8">
									<div class="input-group">
										<input type="text" class="form-control input-sm" id="NameItem">
										<div class="input-group-btn">
											<label class="btn btn-primary btn-xs" id="BtnSearchItem">Cari</label>
										</div>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Name Item</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="ItemID" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-12 fz10" id="info-budget">
									<div class="panel panel-primary" style="padding-bottom: -10px;">
										<div class="panel-heading" style="padding: 8px 12px;">Info Item</div>
										<div class="panel-body" style="margin: -10px;">
											<div class="row">
												<div class="col-md-4 col-xs-6">ItemCode</div>
												<div class="col-md-8 col-xs-6">
													<input type="text" class="form-control input-xs" id="ItemCode" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Stock Item</div>
												<div class="col-md-8 col-xs-6">
													<input type="number" class="form-control input-xs" id="StockItem" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Max Stock</div>
												<div class="col-md-8 col-xs-6">
													<input type="number" class="form-control input-xs" id="MaxStock" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Jenis Stock</div>
												<div class="col-md-8 col-xs-6">
													<input type="text" class="form-control input-xs" id="JenisStock" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Satuan</div>
												<div class="col-md-8 col-xs-6">
													<div class="row">
														<div class="col-xs-8">
															<input type="text" class="form-control input-xs" id="Satuan" disabled="" style="font-size: 10px;">
														</div>
														<div class="col-xs-4">
															<input type="number" class="form-control input-xs" id="UOMID" disabled="" style="font-size: 10px;">
														</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Multi Gudang</div>
												<div class="col-md-8 col-xs-6">
													<div class="row">
														<div class="col-xs-8">
															<input type="text" class="form-control input-xs" id="MultiGudangName" disabled="" style="font-size: 10px;">
														</div>
														<div class="col-xs-4">
															<input type="number" class="form-control input-xs" id="MultiGudang" disabled="" style="font-size: 10px;">
														</div>
													</div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Last Po</div>
												<div class="col-md-8 col-xs-6">
													<input type="text" class="form-control input-xs" id="LastPo" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Last Rms</div>
												<div class="col-md-8 col-xs-6">
													<input type="text" class="form-control input-xs" id="LastRms" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Lead Time</div>
												<div class="col-md-8 col-xs-6">
													<input type="number" class="form-control input-xs" id="LeadTime" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row">
												<div class="col-md-4 col-xs-6">Tanggal Kirim</div>
												<div class="col-md-8 col-xs-6">
													<input type="text" class="form-control input-xs" id="TanggalKirim" disabled="" style="font-size: 10px;">
												</div>
											</div>
											<div class="row" style="display: none;">
												<div class="col-md-4 col-xs-6">Biaya ID</div>
												<div class="col-md-8 col-xs-6">
													<input type="number" class="form-control input-xs" id="BiayaID" disabled="" style="font-size: 10px;">
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>

					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Keterangan</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Keterangan">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-KetBiaya" style="display: none;">
								<div class="col-md-4">Keterangan Biaya</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KetBiaya">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Quantity</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="Qty">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row" id="row-TypeBiaya" style="display: none;">
								<div class="col-md-4">Type Biaya</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TypeBiaya" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-GroupSarmut">
								<div class="col-md-4">GroupSarmut</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="GroupSarmut" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-Forklif" style="display: none;">
								<div class="col-md-4">Forklif</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="Forklif" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-GroupAsset" style="display: none;">
								<div class="col-md-4">Group Asset</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="GroupAsset" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-KelasAsset" style="display: none;">
								<div class="col-md-4">Kelas Asset</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="KelasAsset" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-SubKelasAsset" style="display: none;">
								<div class="col-md-4">SubKelas Asset</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="SubKelasAsset" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-LokasiAsset" style="display: none;">
								<div class="col-md-4">Lokasi Asset</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="LokasiAsset" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-UmurEkonomis" style="display: none;">
								<div class="col-md-4">Umur Ekonomis</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="UmurEkonomis" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-GroupEfisien">
								<div class="col-md-4">Group Efisien</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="GroupEfisien" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="row-GroupEfisien">
								<div class="col-md-4">NoPol</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="NoPol" style="width: 100%;">
										<option value="0"></option>
									</select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-3 col-xs-6">
							<div class="row">
								<div class="col-md-4">CreateUser</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-xs" id="CreateUser" disabled="" style="font-size: 10px;">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-xs-6">
							<div class="row">
								<div class="col-md-4">HeadUser</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-xs" id="HeadUser" disabled="" style="font-size: 10px;">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-xs-6">
							<div class="row">
								<div class="col-md-4">StatusSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-xs" id="StatusSpp" disabled=""  style="font-size: 10px;">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-3 col-xs-6">
							<div class="row">
								<div class="col-md-4">ApprovalSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-xs" id="ApprovalSpp" disabled=""  style="font-size: 10px;">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-8 col-xs-12">
							<table class="table table-bordered" id="table-dead" style="width: 100%;display: none;">
								<thead>
									<tr>
										<th>Plant</th>
										<th>ItemCode</th>
										<th>Quantity</th>
									</tr>
								</thead>
								<tbody id="list-dead"></tbody>
							</table>
						</div>
						<div class="col-md-4 col-xs-12 text-right">
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
									<th>CodeItem</th>
									<th>NameItem</th>
									<th>Quantity</th>
									<th>Satuan</th>
									<th>Keterangan</th>
									<th>TanggalKirim</th>
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
<script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
<script src="../../assets/select2.js"></script>
<script src="../../assets/datatable.js"></script>
<script type="text/javascript">
	(function(factory){
		if(typeof define==='function' && define.amd){
			define(['jquery', 'datatables.net'],function($){
				return factory($,window,document);
			});
		}else if(typeof exports==='object'){
			module.exports=function (root, $){
				if(!root){
					root=window;
				}
				if(!$ || !$.fn.dataTable){
					$=require('datatables.net')(root,$).$;
				}
				return factory($,root,root.document);
			};
		}else{
			factory(jQuery,window,document);
		}
	}
	(function($,window,document,undefined){
		'use strict';
		var DataTable = $.fn.dataTable;
		$.extend( DataTable.ext.classes,{
			sWrapper:"dataTables_wrapper form-inline dt-bootstrap",
			sFilterInput:"form-control input-sm ",
			sLengthSelect:"form-control input-sm",
			sProcessing:"dataTables_processing panel panel-default",
			oLanguage: { "sSearch": "" } 
		});
		return DataTable;
	})
	);
</script>
<script src="../../Scripts/Purchasing/TransaksiSPP3.js" type="text/javascript"></script>
</html>
</asp:Content>
