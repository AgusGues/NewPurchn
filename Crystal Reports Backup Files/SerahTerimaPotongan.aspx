<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SerahTerimaPotongan.aspx.cs" Inherits="GRCweb1.Modul.Factory.SerahTerimaPotongan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Destacking</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    </head>
    <body class="no-skin">
        <div class="row">
            <div class="col-sm-12 form-group-sm">
                <div class="widget-box transparent" id="recent-box">
                    <div class="widget-header">
                        <h4 class="widget-title lighter smaller">
                            <i class="ace-icon fa fa-handshake-o blue"></i>SERAH TERIMA DAN PENERIMAAN PRODUK
                        </h4>

                        <div class="widget-toolbar no-border">
                            <ul class="nav nav-tabs" id="recent-tab">
                                <li id="89" class="active">
                                    <a data-toggle="tab" href="#89mm" aria-expanded="true" style="font-size: large">8/9MM</a>
                                </li>

                                <li id="4" class="">
                                    <a data-toggle="tab" href="#4mm" aria-expanded="false" style="font-size: large">4MM/Lainnya</a>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="widget-body">
                        <div class="widget-main padding-4">
                            <div class="tab-content padding-8">
                                <div class="col-xs-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"></h3>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group form-group-sm">
                                                <div class="col-sm-12">
                                                    <span class="help-inline col-xs-6">
                                                        <label class="middle" style="padding-bottom: 7px;">
                                                            <input class="ace" type="checkbox" id="semua">
                                                            <span class="lbl">Semua </span>
                                                        </label>
                                                        <div>
                                                            <label class="control-label no-padding-right">Tgl Produksi : </label>
                                                        </div>

                                                        <div class="input-group">
                                                            <input class="form-control date-picker" id="tglproduksi" type="text">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar bigger-110"></i>
                                                            </span>
                                                        </div>
                                                    </span>

                                                    <span class="help-inline col-xs-6">
                                                        <label class="middle">

                                                            <select class=" form-control" id="groupcutter">
                                                                <option value="-"></option>
                                                            </select>
                                                        </label>
                                                        <div>
                                                            <label class="control-label no-padding-right">Tgl Potong : </label>
                                                        </div>

                                                        <div class="input-group">
                                                            <input class="form-control date-picker" id="tglpotong" type="text">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar bigger-110"></i>
                                                            </span>
                                                        </div>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12 form-group-sm">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">LIST JEMUR</h3>
                                        </div>
                                        <div class="panel-body">
                                            <div style="width: 100%;">
                                                <div class="table-responsive">
                                                    <table id="tableListjemur" class="table table-striped table-hover display nowrap" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Tanggal Produksi</th>
                                                                <th>Partno</th>
                                                                <th>Jenis</th>
                                                                <th>Tanggal Jemur</th>
                                                                <th>Qty In</th>
                                                                <th>Qty Out</th>
                                                                <th>Sisa</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div id="89mm" class="tab-pane active">
                                    <div class="col-xs-12">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title"></h4>

                                                <div class="widget-toolbar">
                                                </div>
                                            </div>

                                            <div class="widget-body" style="display: block;">
                                                <div class="widget-main">
                                                    <label class="control-label no-padding-right">Serah : </label>
                                                    
                                                    <span class="input-icon">
                                                        <input type="text" id="8serah" class="form-control">
                                                        
                                                    </span>
                                                    <label class="control-label no-padding-right">Oven : </label>
                                                    <span class="input-icon input-icon-right">
                                                        <select class=" form-control" id="oven8">
                                                            <option value="0"></option>
                                                            <option value="1">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                        </select>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 form-group-sm">

                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title"></h3>
                                            </div>

                                            <div class="panel-body">
                                                <div class="tabbable">
                                                    <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                        <li class="active">
                                                            <a data-toggle="tab" href="#superpanel">SuperPanel</a>
                                                        </li>
                                                        <li>
                                                            <a data-toggle="tab" href="#listplank">ListPlank</a>
                                                        </li>
                                                    </ul>

                                                    <div class="tab-content">
                                                        <div id="superpanel" class="tab-pane in active">
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Partno OK</h5>
                                                                    </div>
                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnospok" type="text" size="40"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnosplokok" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnospqtyok" type="text" size="2"  class="form-control" disabled />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6 form-group-sm">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Partno BP</h5>
                                                                    </div>
                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnospbp" type="text" size="40"  class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnosplokbp" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnospqtybp" type="text" size="2"  class="form-control" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <div class="control-group">
                                                                                    <div class="radio">
                                                                                        <label>
                                                                                            <input name="form-field-radio" type="radio" class="ace" id="rev1">
                                                                                            <span class="lbl" style="font-size: small;">Diteruskan Listplank Renovasi I  </span>
                                                                                        </label>
                                                                                        <label>
                                                                                            <input name="form-field-radio" type="radio" class="ace" id ="b99">
                                                                                            <span class="lbl" style="font-size: small;">Diteruskan ke Lokasi B99 </span>
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">ListPlank</h5>
                                                                        <div class="widget-toolbar no-border">
                                                                            <label>
                                                                                <select class=" form-control" id="lstplnkjenis" style="background-color: gray; color: white">
                                                                                </select>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnosplp" type="text" size="40"  class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnosploklp" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnospqtylp" type="text" size="2"  class="form-control" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Partno BP UnFinish</h5>
                                                                    </div>

                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnospunf" type="text" size="40"  class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnosplokunf" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnospqtyunf" type="text" size="2"  class="form-control" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Sample</h5>
                                                                    </div>

                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnospsmp" type="text" size="40"  class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnosploksmp" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnospqtysmp" type="text" size="2"  class="form-control"/>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Pelarian</h5>
                                                                        <div class="widget-toolbar no-border">
                                                                            <button type='button' id="addpelariansp" class="btn btn-xs btn-success">
                                                                                <i class="ace-icon fa fa fa-plus"></i>
                                                                                <span class="bigger-110">Add </span>
                                                                            </button>
                                                                        </div>
                                                                    </div>

                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table id="tablepelariansp" class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnoplrnsp" type="text" size="40"  class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnoplrnloksp" type="text" size="2"  class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnoplrnqtysp" class="form-control qty" type="text" size="2"/>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div id="listplank" class="tab-pane">
                                                            <div class="col-xs-12">
                                                                <label class="control-label no-padding-right">Listplank : </label>
                                                                <span class="input-icon input-icon-right">
                                                                    <select class=" form-control" id="cblistplank">
                                                                        <option value="0">Partno Asal</option>
                                                                        <option value="1">1220 X 2440</option>
                                                                        <option value="2">1221 X 2440</option>
                                                                        <option value="3">1233 X 2440</option>
                                                                        <option value="4">1230 X 2440</option>
                                                                        <option value="5">1240 X 2460</option>
                                                                        <option value="6">1230 X 3600</option>
                                                                        <option value="7">1244 X 3600</option>
                                                                        <option value="8">1244 X 3600</option>
                                                                    </select>
                                                                </span>
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Partno OK</h5>
                                                                    </div>
                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 90%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnolpok" type="text" size="80" class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnolplokok" type="text" size="2" class="form-control" style="text-transform:uppercase" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnolpqtyok" type="text" size="2" class="form-control" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Sample</h5>
                                                                    </div>

                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnosmplp" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnosmploklp" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnosmpqtylp" type="text" size="2" class="form-control"/>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-6">
                                                                <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                                    <div class="widget-header">
                                                                        <h5 class="widget-title smaller">Pelarian</h5>
                                                                        <div class="widget-toolbar no-border">
                                                                            <button type='button' id="addpelarianlp" class="btn btn-xs btn-success">
                                                                                <i class="ace-icon fa fa fa-plus"></i>
                                                                                <span class="bigger-110">Add </span>
                                                                            </button>
                                                                        </div>
                                                                    </div>

                                                                    <div class="widget-body">
                                                                        <div class="widget-main padding-6">
                                                                            <div class="table-responsive">
                                                                                <table id="tablepelarianlp" class="table table-bordered table-striped">
                                                                                    <thead class="thin-border-bottom">
                                                                                        <tr>
                                                                                            <th style="width: 35%">Partno
                                                                                            </th>
                                                                                            <th style="width: 5%">Lokasi
                                                                                            </th>
                                                                                            <th style="width: 5%">Qty
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input id="partnoplrlp" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                            <td>
                                                                                                <input id="partnoplrloklp" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input id="partnoplrqtylp" class="form-control qty8" type="text" size="2" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-footer padding-8 clearfix">
                                                <button type='button' id="transfer8mm" class="btn btn-primary pull-right">
                                                    <span class="bigger-110">Transfer</span>

                                                    <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="4mm" class="tab-pane">
                                    <div class="col-xs-12">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title"></h4>

                                                <div class="widget-toolbar">
                                                </div>
                                            </div>

                                            <div class="widget-body" style="display: block;">
                                                <div class="widget-main">
                                                    <label class="control-label no-padding-right">Serah : </label>

                                                    <span class="input-icon">
                                                        
                                                        <input type="text" id="4serah" class="form-control">
                                                    </span>
                                                    <label class="control-label no-padding-right">Oven : </label>
                                                    <span class="input-icon input-icon-right">
                                                        <select class=" form-control" id="oven4">
                                                            <option value="0"></option>
                                                            <option value="1">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                        </select>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">

                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title"></h3>
                                            </div>

                                            <div class="panel-body">
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Partno OK</h5>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 35%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4ok" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partnolok4ok" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partnoqty4ok" type="text" size="2" class="form-control" disabled />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Partno KW</h5>
                                                            <div class="widget-toolbar no-border">
                                                                <label>
                                                                    <select class=" form-control" id="lstpljenis4" style="background-color: gray; color: white">
                                                                    </select>
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 35%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4kw" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4lokkw" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4qtykw" type="text" size="2" class="form-control"/>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Partno BP</h5>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 35%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4bp" type="text" size="35" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4lokbp" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4qtybp" type="text" size="2" class="form-control"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4lok2bp" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Partno BP UnFinish</h5>
                                                        </div>

                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 25%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                                <th style="width: 25%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4unf" type="text" size="15" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4lokunf" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4qtyunf" type="text" size="2" class="form-control"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4unf2" type="text" size="15" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4lokunf2" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Sample</h5>
                                                        </div>

                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 35%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4smp" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4loksmp" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4qtysmp" type="text" size="2" class="form-control"/>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="widget-box widget-color-dark light-border ui-sortable-handle" style="opacity: 1;">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title smaller">Pelarian</h5>
                                                            <div class="widget-toolbar no-border">
                                                                <button type='button' id="addpelarian4" class="btn btn-xs btn-success">
                                                                    <i class="ace-icon fa fa fa-plus"></i>
                                                                    <span class="bigger-110">Add </span>
                                                                </button>
                                                            </div>
                                                        </div>

                                                        <div class="widget-body">
                                                            <div class="widget-main padding-6">
                                                                <div class="table-responsive">
                                                                    <table id="tablepelarian4" class="table table-bordered table-striped">
                                                                        <thead class="thin-border-bottom">
                                                                            <tr>
                                                                                <th style="width: 35%">Partno
                                                                                </th>
                                                                                <th style="width: 5%">Lokasi
                                                                                </th>
                                                                                <th style="width: 5%">Qty
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <input id="partno4plrn" type="text" size="40" class="form-control" style="text-transform:uppercase"/></td>
                                                                                <td>
                                                                                    <input id="partno4lokplrn" type="text" size="2" class="form-control" style="text-transform:uppercase"/>
                                                                                </td>
                                                                                <td>
                                                                                    <input id="partno4qtyplrn" class="form-control qty4" type="text" size="2"  />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-footer padding-8 clearfix">
                                                <button type='button' id="transfer4mm" class="btn btn-primary pull-right">
                                                    <span class="bigger-110">Transfer</span>

                                                    <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 form-group-sm">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">LIST SERAH</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div style="width: 100%;">
                                                    <div class="table-responsive">
                                                        <table id="tableListserah" class="table table-striped table-hover table-bordered" style="width: 100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Tanggal Produksi</th>
                                                                    <th>Tanggal Serah</th>
                                                                    <th>Partno Awal</th>
                                                                    <th>Partno Akhir</th>
                                                                    <th>Lokasi Akhir</th>
                                                                    <th>Qty In</th>
                                                                    <th>Qty Out</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="loader" id="loading"></div>
    </body>

    <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.min.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>

    <script src="../../Scripts/Factory/SerahTerima.js" type="text/javascript"></script>
    </html>
    <style>
        .loader {
            z-index: 9999;
            position: fixed;
            top: 50%;
            left: 50%;
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 100px;
            height: 100px;
            animation: spin 2s linear infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        .mytable > thead > tr > th {
            background-color: gray !important;
            color: white !important;
        }

        html, body, .form-control, btn {
            font-size: 11px;
        }

        table.dataTable tbody td {
            vertical-align: middle;
        }

        .ui-datepicker {
            width: 23em;
        }
    </style>
</asp:Content>