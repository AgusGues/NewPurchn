
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputProject.aspx.cs" Inherits="GRCweb1.Modul.Maintenance.InputProject" %>

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
	<style>
		.panelbox {background-color: #efeded;padding: 2px;}
		html,body,.form-control,button{font-size: 12px;}
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
					<span class="the-title">Input Project</span>
					<div class="pull-right">
						<label class="btn btn-primary btn-xs BtnRefresh">NewInput</label>
						<label class="btn btn-primary btn-xs" id="panel-form">Form</label>
						<label class="btn btn-primary btn-xs" id="panel-list">ListData</label>
					</div>
				</div>
				<div class="panel-body panel-list" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">KodeProject</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodeProject-list">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">StatusProject</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="StatusProject-list" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">DeptPemohon</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="DeptPemohon-list" style="width: 100%;"></select>
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
						<table id="datatable" class="table display nowrap" style="width: 100%;">
							<thead>
								<tr>
									<th>KodeProject</th>
									<th>ProjectName</th>
									<th>Sasaran</th>
									<th>FromDate</th>
									<th>ToDate</th>
									<th>EstimasiBiaya</th>
									<th>BiayaActual</th>
									<th>Approval</th>
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
								<div class="col-md-4">KodeProject</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KodeProject" disabled="">
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
								<div class="col-md-4">DeptPenerima</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="DeptPenerima" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">HeadName</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="HeadName" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">GroupProject</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="GroupProject" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">NamaProject</div>
								<div class="col-md-8">
									<textarea class="form-control input-sm" id="NamaProject" rows="2"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">SasaranProject</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="SasaranProject" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">DeptPemohon</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="DeptPemohon" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">AreaProject</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="AreaProject" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">SubArea</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="SubArea" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Satuan</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="Satuan" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">TujuanProject</div>
								<div class="col-md-8">
									<textarea class="form-control input-sm" id="TujuanProject" rows="2"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Quantity</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="BatasEstimasi" style="display: none;">
									<input type="number" class="form-control input-sm" id="Quantity">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12 text-right">
									<label class="btn btn-primary btn-xs" id="BtnSimpan">Save</label>
									<label class="btn btn-primary btn-xs BtnRefresh">Clear</label>
									<div style="padding: 2px"></div>
								</div>
							</div>
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
<!-- <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script> -->
<script src="../../assets/jquery.js"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
<script src="../../assets/select2.js"></script>
<script src="../../assets/datatable.js"></script>
<script src="../../assets/fixedHeader.js"></script>
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
<script src="../../Scripts/Maintenance/InputProject.js" type="text/javascript"></script>
</asp:Content>
