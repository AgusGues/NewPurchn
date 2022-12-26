
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PoPurchn.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.PoPurchn" %>

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
	<link rel="stylesheet" href="../../assets/Datatables/datatables.css" />
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
		.modal-footer{
			padding-left: : 1px;padding-top: 2px;padding-right: 1px;padding-bottom: 2px;
		}
		.btn-data{padding:1px 1px;font-size:8px;line-height:0.5;border-radius:3px;}
	</style>
</head>
<body class="no-skin">
	<div class="row">
		<div class="col-md-12">
			<div class="modal fade modal-spp">
				<div class="modal-dialog modal-sm">
					<div class="modal-content">
						<div class="modal-header">
							<span>Alasan Pending KodeSpp : <span id="spp-kode"></span></span>
							<div class="pull-right">
								<span data-dismiss="modal">x</span>
							</div>
						</div>
						<div class="modal-body">
							<span>ItemName  <span id="spp-itemname"></span></span>
							<div style="padding: 2px"></div>
							Alasan
							<input type="text" class="form-control input-sm" id="spp-id" style="display: none;">
							<textarea rows="3" class="form-control input-sm" id="spp-alasan"></textarea>
						</div>
						<div class="modal-footer">
							<label class="btn btn-primary btn-xs" id="BtnSave-spp">Save</label>
						</div>
					</div>
				</div>
			</div>
			<div class="panel panel-primary">
				<div class=panel-heading>
					<span class="the-title">Input PO</span>
					<div class="pull-right">
						<label class="btn btn-primary btn-xs BtnRefresh">NewInput</label>
						<label class="btn btn-primary btn-xs" id="panel-form">Form</label>
						<label class="btn btn-primary btn-xs" id="panel-list">ListPo</label>
						<label class="btn btn-primary btn-xs" id="panel-uangmuka">UangMuka</label>
						<label class="btn btn-primary btn-xs" id="panel-spp">ListSpp</label>
						<label class="btn btn-primary btn-xs" id="panel-pending">ListPending</label>
					</div>
				</div>
				<div class="panel-body panel-pending" style="display: none;">
					<div class="table-responsive">
						<table id="data-pending" class="table" style="width: 100%;">
							<thead>
								<tr>
									<th>Aksi</th>
									<th>KodeSpp</th>
									<th>ItemCode</th>
									<th>ItemName</th>
									<th>Satuan</th>
									<th>QtySpp</th>
									<th>LeadTime</th>
									<th>AlasanPending</th>
								</tr>
							</thead>
							<tbody></tbody>
						</table>
					</div>
				</div>
				<div class="panel-body panel-spp" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">NoSpp</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="spp-NoSpp">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">User SPP</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="spp-CreatedBy">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6"></div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-12 text-right">
									<label class="btn btn-primary btn-xs" id="BtnClear-spp">Clear</label>
									<label class="btn btn-primary btn-xs" id="BtnSearch-spp">Find</label>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="table-responsive">
						<table id="data-spp" class="table" style="width: 100%;">
							<thead>
								<tr>
									<th></th>
									<th>ID</th>
									<th>KodeSpp</th>
									<th>TanggalSpp</th>
									<th>TypeSpp</th>
									<th>UserHead</th>
									<th>ApvDate</th>
								</tr>
							</thead>
							<tbody></tbody>
						</table>
					</div>
				</div>
				<div class="panel-body panel-uangmuka" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Kode Po</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="uangmuka-po">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<input type="text" class="form-control input-sm" disabled="">
							<div style="padding: 2px"></div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Termin 1</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="uangmuka-termin1">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan 1</div>
								<div class="col-md-8">
									<textarea rows="1" class="form-control input-sm" id="uangmuka-ket1"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Termin 2</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="uangmuka-termin2">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan 2</div>
								<div class="col-md-8">
									<textarea rows="1" class="form-control input-sm" id="uangmuka-ket2"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Termin 3</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="uangmuka-termin3">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan 3</div>
								<div class="col-md-8">
									<textarea rows="1" class="form-control input-sm" id="uangmuka-ket3"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Termin 4</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="uangmuka-termin4">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan 4</div>
								<div class="col-md-8">
									<textarea rows="1" class="form-control input-sm" id="uangmuka-ket4"></textarea>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<div class="row">
								<div class="col-md-12 text-right">
									<label class="btn btn-primary btn-xs" id="BtnClear-uangmuka">Clear</label>
									<label class="btn btn-primary btn-xs" id="BtnSave-uangmuka">Save</label>
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="panel-body panel-list" style="display: none;">
					<div class="row">
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">NoPo</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="list-NoPo">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
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
						<div class="col-md-6">
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
									<th>NoPo__</th>
									<th>NoSpp__</th>
									<th>TanggalPo</th>
									<th>ItemCode</th>
									<th>ItemName____</th>
									<th>Qty</th>
									<th>Price</th>
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
								<div class="col-md-4">NoPo</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="NoPo" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">TanggalPo</div>
								<div class="col-md-8">
									<div class="input-group">
										<input class="form-control input-sm date-picker" id="TanggalPo" type="text">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
									</div>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">NoSpp</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="NoSpp" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">ItemSpp</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="ItemSpp" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12" style="display: none;">
									<input type="number" class="form-control input-sm" id="UOMID">
									<input type="number" class="form-control input-sm" id="ItemID">
									<input type="number" class="form-control input-sm" id="GroupID">
									<input type="number" class="form-control input-sm" id="ItemTypeID">
								</div>
								<div class="col-md-12">
									<div class="panel panel-default" style="margin-bottom: -8px;">
										<div class="panel-body" style="padding: 6px 5px;">
											<div class="row">
												<div class="col-xs-6">ItemCode : <span id="ItemCode"></span></div>
												<div class="col-xs-6 text-right">ItemName : <span id="ItemName"></span></div>
												<div class="col-xs-4">Satuan : <span id="Satuan"></span></div>
												<div class="col-xs-4">DeliveryDate : <span id="DeliveryDate"></span></div>
												<div class="col-xs-4 text-right">LeadTime : <span id="LeadTime"></span></div>
												<div class="col-md-12">
													<div style="padding: 2px"></div>
													<span id="HargaTerendah"></span>
												</div>
											</div>
										</div>
									</div>
									<div style="padding: 6px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12" style="display: none;">
									<input type="number" class="form-control input-sm" id="SupplierID">
									<input type="number" class="form-control input-sm" id="SubCompanyID">
								</div>
								<div class="col-md-4">Supplier Name</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="SupplierName">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12">
									<div class="panel panel-default" style="margin-bottom: -8px;">
										<div class="panel-body" style="padding: 6px 5px;">
											<div class="row">
												<div class="col-xs-4">UP Supplier : <span id="UPSupplier"></span></div>
												<div class="col-xs-4">Telp Supplier : <span id="TelpSupplier"></span></div>
												<div class="col-xs-4 text-right">Fax Supplier : <span id="FaxSupplier"></span></div>
											</div>
										</div>
									</div>
									<div style="padding: 6px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Qty</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="Qty">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Harga</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="Harga">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-2 col-xs-4">Mata Uang</div>
								<div class="col-md-4 col-xs-8">
									<select class="form-control input-sm select2" id="MataUang" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
								<div class="col-md-2 col-xs-4">Nilai Kurs</div>
								<div class="col-md-4 col-xs-8">
									<input type="number" class="form-control input-sm" id="NilaiKurs" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Term Of Pay</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TermOfPay" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Keterangan Term Of Pay</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="KetTermOfPay" disabled="">
									<div style="padding: 2px"></div>
								</div>
							</div>
						</div>
						<div class="col-md-6">
							<div class="row">
								<div class="col-md-4">Term Of Delivery</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="TermOfDelivery" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-2 col-xs-4">PPN</div>
								<div class="col-md-2 col-xs-8">
									<input type="number" class="form-control input-sm" id="PPN">
									<div style="padding: 2px"></div>
								</div>
								<div class="col-md-2 col-xs-4">PPH</div>
								<div class="col-md-2 col-xs-8">
									<input type="number" class="form-control input-sm" id="PPH">
									<div style="padding: 2px"></div>
								</div>
								<div class="col-md-2 col-xs-4">Discount</div>
								<div class="col-md-2 col-xs-8">
									<input type="number" class="form-control input-sm" id="Discount">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-2 col-xs-4">Bayar</div>
								<div class="col-md-4 col-xs-8">
									<select class="form-control input-sm select2" id="Bayar" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
								<div class="col-md-2 col-xs-4">Barang</div>
								<div class="col-md-4 col-xs-8">
									<select class="form-control input-sm select2" id="Barang" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Ongkos Kirim</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="OngkosKirim">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Remark</div>
								<div class="col-md-8">
									<input type="text" class="form-control input-sm" id="Remark">
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">Indent</div>
								<div class="col-md-8">
									<select class="form-control input-sm select2" id="Indent" style="width: 100%;"></select>
									<div style="padding: 2px"></div>
								</div>
							</div>
							<div class="row" id="info-KadarAir">
								<div class="col-md-12">
									<div class="panel panel-primary" style="margin-bottom: 4px;">
										<div class="panel-heading" style="padding: 8px 12px;">Info KadarAir</div>
										<div class="panel-body" style="margin: -10px;">
											<div class="row">
												<div class="col-md-4">NoSj</div>
												<div class="col-md-8">
													<select class="form-control input-sm select2" id="NoSj" style="width: 100%;"></select>
													<div style="padding: 2px"></div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-2 col-xs-4">NoMobil</div>
												<div class="col-md-4 col-xs-8">
													<input type="text" class="form-control input-sm" id="NoMobil">
													<div style="padding: 2px"></div>
												</div>
												<div class="col-md-2 col-xs-4">StdKa</div>
												<div class="col-md-4 col-xs-8">
													<input type="number" class="form-control input-sm" id="StdKa">
													<div style="padding: 2px"></div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-2 col-xs-4">Gross</div>
												<div class="col-md-4 col-xs-8">
													<input type="number" class="form-control input-sm" id="Gross">
													<div style="padding: 2px"></div>
												</div>
												<div class="col-md-2 col-xs-4">KadarAir</div>
												<div class="col-md-4 col-xs-8">
													<input type="number" class="form-control input-sm" id="KadarAir">
													<div style="padding: 2px"></div>
												</div>
											</div>
											<div class="row">
												<div class="col-md-2 col-xs-4">Sampah</div>
												<div class="col-md-4 col-xs-8">
													<input type="number" class="form-control input-sm" id="Sampah">
													<div style="padding: 2px"></div>
												</div>
												<div class="col-md-2 col-xs-4">Netto</div>
												<div class="col-md-4 col-xs-8">
													<input type="number" class="form-control input-sm" id="Netto">
													<div style="padding: 2px"></div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-4">TotalPrice</div>
								<div class="col-md-8">
									<input type="number" class="form-control input-sm" id="TotalPrice" value="0">
									<div style="padding: 2px"></div>
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
						</div>
					</div>
					<div class="table-responsive">
						<table class="table" style="width: 100%;">
							<thead>
								<tr>
									<th>NoSpp________</th>
									<th>ItemCode</th>
									<th>ItemName</th>
									<th>Quantity</th>
									<th>Satuan</th>
									<th>Harga</th>
									<th>DlvDate</th>
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
<script src="../../assets/Datatables/datatables.js"></script>
<script src="../../Scripts/Purchasing/PoPurchn.js" type="text/javascript"></script>
</asp:Content>
