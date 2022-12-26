<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiSparePart.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiSparePart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>Widgets - Ace Admin</title>
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <link rel="stylesheet" href="../../assets/select2.css" />
    <link rel="stylesheet" href="../../assets/datatable.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
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
            font-size: 11px;height: 11px;
        }
    </style>
</head>
<body class="no-skin">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class=panel-heading>
                    Pemakaian Spare Part 
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
                                <div class="col-md-4">PakaiNO</div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="list-PakaiNo">
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4">ItemName</div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="list-ItemName">
                                </div>
                            </div><div style="padding: 2px"></div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">User SPB</div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="list-CreatedBy">
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <label class="btn btn-primary btn-xs" id="BtnClearData">Clear</label>
                                    <label class="btn btn-primary btn-xs" id="BtnCariData">Cari</label>
                                </div>
                            </div><div style="padding: 2px"></div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table id="datatable" class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>NoPakai</th>
                                    <th>Tanggal</th>
                                    <th>KodeBarang</th>
                                    <th>NamaBarang</th>
                                    <th>Qty</th>
                                    <th>Uom</th>
                                    <th>Keterangan</th>
                                    <th>AprovLevel</th>
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
                                <div class="col-md-4">No. Pakai</div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="PakaiNo" disabled="">
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4">Tanggal</div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input class="form-control input-sm date-picker" id="Tanggal" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4">Nama Dept</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="DllDept" style="width: 100%;"></select>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-KodeProject" style="display: none;">
                                <div class="col-md-4">Kode Project</div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input type="text" class="form-control input-sm" id="KodeProject">
                                        <div class="input-group-btn">
                                            <label class="btn btn-primary btn-xs" id="BtnCariProject">Cari</label>
                                        </div>
                                    </div>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-NameProject" style="display: none;">
                                <div class="col-md-4">Name Project</div>
                                <div class="col-md-8">
                                    <span id="NameProject"></span>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-TextBarang">
                                <div class="col-md-4">Cari Nama Barang</div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input type="text" class="form-control input-sm" id="TextBarang">
                                        <div class="input-group-btn">
                                            <label class="btn btn-primary btn-xs" id="BtnCariBarang">Cari</label>
                                        </div>
                                    </div>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4">Nama Barang</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="ddlItem" style="width: 100%;"></select>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" style="display: none;">
                                <div class="col-lg-12">
                                    ItemID<input type="number" class="form-control input-sm" id="ItemID">
                                    UOMID<input type="number" class="form-control input-sm" id="UOMID">
                                    GroupID<input type="number" class="form-control input-sm" id="GroupID">
                                    ItemTypeID<input type="number" class="form-control input-sm" id="ItemTypeID">
                                    AvgPrice<input type="number" class="form-control input-sm" id="AvgPrice" value="0">
                                    Jumlah<input type="number" class="form-control input-sm" id="Jumlah">
                                    ReOrder<input type="number" class="form-control input-sm" id="ReOrder">
                                    StockOtherDept<input type="number" class="form-control input-sm" id="StockOtherDept">
                                    ShowStock<input type="number" class="form-control input-sm" id="ShowStock">
                                    PendingSPB<input type="number" class="form-control input-sm" id="PendingSPB">
                                    StockAkhir<input type="number" class="form-control input-sm" id="StockAkhir">
                                    Planning<input type="number" class="form-control input-sm" id="Planning">
                                    CheckCost<input type="number" class="form-control input-sm" id="CheckCost">
                                    MaxSPB<input type="number" class="form-control input-sm" id="MaxSPB">
                                    AddBudget<input type="number" class="form-control input-sm" id="AddBudget">
                                    RuleBudget<input type="number" class="form-control input-sm" id="RuleBudget">
                                    TotalSPB<input type="number" class="form-control input-sm" id="TotalSPB">
                                    ItemIDKhusus<input type="number" class="form-control input-sm" id="ItemIDKhusus">
                                    JumlahMaterial<input type="number" class="form-control input-sm" id="JumlahMaterial">
                                    TotalSPBPrj<input type="number" class="form-control input-sm" id="TotalSPBPrj">
                                    DeptCode<input type="text" class="form-control input-sm" id="DeptCode">
                                    ProjectID<input type="number" class="form-control input-sm" id="ProjectID">
                                </div>
                                <div class="col-xs-6">
                                    <div class="input-group">
                                        <span class="input-group-addon fz11">ItemCode</span>
                                        <input type="text" class="form-control input-sm" id="ItemCode" readonly="">
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="input-group">
                                        <span class="input-group-addon fz11">UomDesc</span>
                                        <input type="text" class="form-control input-sm" id="UOMDesc" readonly="">
                                    </div>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4">Qty</div>
                                <div class="col-md-8">
                                    <input type="number" class="form-control input-sm" id="Quantity">
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-Keterangan">
                                <div class="col-md-4">Keterangan</div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="Keterangan">
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-Palet" style="display: none;">
                                <div class="col-md-4">Keterangan Untuk</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="Palet" style="width: 100%;">

                                    </select>
                                </div>
                            </div><div style="padding: 2px"></div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-xs-6">
                                    <div class="input-group">
                                        <span class="input-group-addon fz11">Stock</span>
                                        <input type="number" class="form-control input-sm" id="Stock" readonly="">
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="input-group">
                                        <span class="input-group-addon fz11">Blocked</span>
                                        <input type="number" class="form-control input-sm" id="Blocked" readonly="">
                                    </div>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-12 fz10" id="info-budget">
                                    <div class="panel panel-primary" style="padding-bottom: -10px;">
                                        <div class="panel-heading">Info Budget</div>
                                        <div class="panel-body" style="margin: -10px;">
                                            <div class="row">
                                                <div class="col-xs-6">PeriodeBudget</div>
                                                <div class="col-xs-6">
                                                    <input type="text" class="input-xs" id="PeriodeBudget" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6">TotalBudget</div>
                                                <div class="col-xs-6">
                                                    <input type="number" class="input-xs" id="TotalBudget" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6">TambahanBudget</div>
                                                <div class="col-xs-6">
                                                    <input type="number" class="input-xs" id="TambahanBudget" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6">TotalJumlahSPB</div>
                                                <div class="col-xs-6">
                                                    <input type="number" class="input-xs" id="TotalJumlahSPB" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6">SisaBudget</div>
                                                <div class="col-xs-6">
                                                    <input type="number" class="input-xs" id="SisaBudget" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-6">TypeBudget</div>
                                                <div class="col-xs-6">
                                                    <input type="text" class="input-xs" id="TypeBudget" disabled="" style="font-size: 10px;">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="row-SpGroup" style="display: none;">
                                <div class="col-md-4">Sp Group</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="SpGroup" style="width: 100%;"></select>         
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-Zona" style="display: none;">
                                <div class="col-md-4">Name Zona</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="ZonaMtc" style="width: 100%;">
                                        <option value="0">Pilih Zona</option>
                                        <option value="Zona 1">Zona 1</option>
                                        <option value="Zona 2">Zona 2</option>
                                        <option value="Zona 3">Zona 3</option>
                                        <option value="Zona 4">Zona 4</option>
                                        <option value="Oven Dryer">Oven Dryer</option>
                                        <option value="General">General</option>
                                    </select>
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row" id="row-Forklift" style="display: none;">
                                <div class="col-md-4">Forklift</div>
                                <div class="col-md-8">
                                    <select class="form-control input-sm select2" id="Forklift" style="width: 100%;">
                                        <option value="0"></option>
                                    </select>         
                                </div>
                            </div><div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-6"></div>
                                <div class="col-md-6 text-right">
                                    <label class="btn btn-primary btn-xs" id="BtnAddItem">Add Item</label>
                                    <label class="btn btn-primary btn-xs" id="BtnSimpan" disabled="">Save</label>
                                    <label class="btn btn-primary btn-xs BtnRefresh">Refresh</label>
                                </div>
                            </div>
                        </div>
                    </div><div style="padding: 2px"></div>
                    <div class="table-responsive ">
                        <table class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th>Kode Barang</th>
                                    <th>Nama Barang</th>
                                    <th>Jumlah</th>
                                    <th>Uom</th>
                                    <th>Keterangan</th>
                                    <th>action</th>
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
<script src="../../assets/jquery.js" type="text/javascript"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../assets/select2.js"></script>
<script src="../../assets/datatable.js"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
<script src="../../Scripts/Purchasing/FormPakaiSparePart.js" type="text/javascript"></script>
</html>
</asp:Content>