<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Simetris.aspx.cs" Inherits="GRCweb1.Modul.Factory.Simetris" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Simetris</title>

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
            <div class="col-xs-12 col-sm-4 form-group-sm">
                <div class="panel panel-primary">
                         <div class="panel-heading">
                        <h3 class="panel-title">Group Marketing</h3>
                    </div>
                    <div class="panel-body">
                               <div>
                                <select class=" form-control" id="groupmarketing">
                                    <option value="">Pilih Group Marketing</option>
                                </select>
                            </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4 form-group-sm">
                <label for="form-field-8">Cari PartNo Awal : </label>
                <div>
                    <input id="caripartno" class="form-control" type="text" placeholder="PartNo...." size="50" style="text-transform:uppercase"/>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4 form-group-sm">
                <label for="form-field-8">Tgl Proses : </label>
                <div class="input-group">
                    <input class="form-control date-picker" id="tglproduksi" type="text">
                    <span class="input-group-addon">
                        <i class="fa fa-calendar bigger-110"></i>
                    </span>
                </div>
            </div>
        </div>
 
        <div class="row">
            <div class="col-xs-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">List Stock Produk</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tableListProdukStok" class="table table-striped table-hover" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Part No</th>
                                            <th>Lokasi</th>
                                            <th>Stok</th>
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
        <div class="row">
            <div class="col-xs-12 col-sm-4 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Lokasi Awal</h3>
                    </div>
                    <div class="panel-body">
                            <div>
                                <label for="form-field-8">Partno</label>

                                <div>
                                    <input id="partno" class="form-control" type="text" placeholder="Partno" size="50" disabled />
                                </div>
                            </div>

                            <div>
                                <label for="form-field-9">Ukuran</label>
                                <div>
                                    <input type="text" id="tebal" placeholder="Tebal" size="10" style="height: 30px" disabled />
                                    X
                                    <input type="text" id="lebar" placeholder="lebar" size="10" style="height: 30px" disabled />
                                    X
                                    <input type="text" id="panjang" placeholder="Panjang" size="10" style="height: 30px" disabled />
                                </div>
                            </div>

                            <div>
                                <label for="form-field-11">Part Name</label>

                                <div>
                                    <input type="text" class="form-control" id="partname" placeholder="Part Name" size="50" disabled />
                                </div>
                            </div>

                            <div class="col-lg-12" style="margin-left: -22px;">
                                <div class="col-lg-6">
                                    <label for="form-field-11">Lokasi</label>

                                    <div>
                                        <input type="text" class="form-control" id="lokasi" placeholder="Lokasi" size="17" disabled />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label for="form-field-11">Stok</label>

                                    <div>
                                        <input type="text" class="form-control" id="stok" placeholder="Stok" size="17" disabled />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <label for="form-field-11">Quantity</label>

                                <div>
                                    <input type="text" class="form-control" id="quantity" placeholder="Quantity" size="17" onsubmit="return false;" />
                                </div>
                            </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-4 form-group-sm">

                <div class="panel panel-primary">
                      <div class="panel-heading">
                        <h3 class="panel-title">Lokasi Akhir</h3>
                    </div>
                    <div class="panel-body">
                           <div>
                                <label for="form-field-8">Partno</label>

                                <div>
                                    <input id="partnoakhir" type="text" class="form-control" placeholder="Partno" size="50" style="text-transform:uppercase" />
                                </div>
                            </div>

                            <div>
                                <label for="form-field-9">Ukuran</label>
                                <div>
                                    <input type="text" id="tebalakhir" placeholder="Tebal" size="10" style="height: 30px" disabled />
                                    X
                                    <input type="text" id="lebarakhir" placeholder="lebar" size="10" style="height: 30px"disabled />
                                    X
                                    <input type="text" id="panjangakhir" placeholder="Panjang" size="10" style="height: 30px" disabled />
                                </div>
                            </div>

                            <div>
                                <label for="form-field-11">Part Name</label>

                                <div>
                                    <input type="text" class="form-control" id="partnameakhir" placeholder="Part Name" size="50" disabled />
                                </div>
                            </div>

                            <div class="col-lg-12" style="margin-left: -22px;">
                                <div class="col-lg-6">
                                    <label for="form-field-11">Lokasi</label>

                                    <div>
                                        <input type="text" class="form-control" id="lokasiakhir" placeholder="Lokasi" size="17" style="text-transform:uppercase" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <label for="form-field-11">Stok</label>

                                    <div>
                                        <input type="text" class="form-control" id="stokakhir" placeholder="Stok" size="17" disabled />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <label for="form-field-11">Quantity</label>

                                <div>
                                    <input type="text" id="quantityakhir" placeholder="Quantity" size="17" style="height: 30px" disabled />
                                    <button class="btn btn-info btn-sm" type="button" id="transfer" style="margin-left: 50px">
                                        <i class="ace-icon fa fa-check bigger-110"></i>
                                        Transfer
                                    </button>
                                </div>
                            </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-4 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-body">
                              <div id="selectdefect">
                            </div>
                            <br />
                            <div id="selectmesincutter">
                            </div>
                            <br />
                            <div class="showhidden">

                                <div class="panelbox">
                                    <div class="radio" id="panelnc">
                                        <label>
                                            <input type="radio" name="panel" id="nchendling" class="ace" value="nchandling" checked/>
                                            <span class="lbl" style="font-size:14px;">NC Handling</span>
                                        </label>
                                        <label>
                                            <input type="radio" name="panel" id="ncsortir" class="ace" value="ncsortir" />
                                            <span class="lbl" style="font-size:14px;">NC Sortir</span>
                                        </label>
                                        <label>
                                            <input type="radio" name="panel" id="nonnc" class="ace" value="nonnc" />
                                            <span class="lbl" style="font-size:14px;">Non NC</span>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <br />

                            <div class="showhiddenpanel">

                                <div class="panelbox">
                                    <div class="radio" id="panelncsortir">
                                        <label>
                                            <input type="radio" name="panelsortir" id="RBStd" class="ace" value="Standar" checked />
                                            <span class="lbl" style="font-size:14px;">Standar</span>
                                        </label>
                                        <label>
                                            <input type="radio" name="panelsortir" id="RBEfo" class="ace" value="EFO" />
                                            <span class="lbl" style="font-size:14px;">EFO</span>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="showhiddenbs">
                                <div class="radio" id="panelbs">
                                    <label>
                                        <input name="panelbs" type="radio" id="logistik" class="ace" />
                                        <span class="lbl" style="font-size:14px;">BS Logistik</span>
                                    </label>
                                    <label>
                                        <input name="panelbs" type="radio" id="finishing" class="ace" />
                                        <span class="lbl" style="font-size:14px;">BS Finishing</span>
                                    </label>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 form-group-sm">
                <table class="table table-striped table-bordered table-hover">
                    <thead >
                        <tr>
                            <th style="width: 50%;">
                                <label>Auto Produk BS</label></th>
                            <th style="width: 50%;">
                                <div class="radio" id="carapotong">
                                    <label>
                                        <input type="radio" name="potong" id="potong1" class="ace" value="cara potong 1" />
                                        <span class="lbl">Cara Potong 1</span>
                                    </label>
                                    <label>
                                        <input type="radio" name="potong" id="potong2" class="ace" value="cara potong 2" checked="checked" />
                                        <span class="lbl">Cara Potong 2</span>
                                    </label>
                                </div>
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <label for="form-field-11">: </label>
                            <label for="form-field-11" id="lcpartnobs1">(-) </label>
                            <label for="form-field-11" id="lcqtybs1"></label>
                            <label for="form-field-11">Lembar, ke lokasi : </label>
                            <label for="form-field-11" id="lclokasibs1">0</label></td>
                        <td>
                            <label for="form-field-11">: </label>
                            <label for="form-field-11" id="lcpartnobs3">(-) </label>
                            <label for="form-field-11" id="lcqtybs3"></label>
                            <label for="form-field-11">Lembar, ke lokasi : </label>
                            <label for="form-field-11" id="lclokasibs3">0</label></td>
                    </tr>
                    <tr>
                        <td>
                            <label for="form-field-11">: </label>
                            <label for="form-field-11" id="lcpartnobs2">(-) </label>
                            <label for="form-field-11" id="lcqtybs2"></label>
                            <label for="form-field-11">Lembar, ke lokasi : </label>
                            <label for="form-field-11" id="lclokasibs2">0</label></td>
                        <td>
                            <label for="form-field-11">: </label>
                            <label for="form-field-11" id="lcpartnobs4">(-) </label>
                            <label for="form-field-11" id="lcqtybs4"></label>
                            <label for="form-field-11">Lembar, ke lokasi : </label>
                            <label for="form-field-11" id="lclokasibs4">0</label></td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">List Stock Produk</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tableListSimetris" class="table table-striped table-hover" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Partno Awal</th>
                                            <th>Lokasi</th>
                                            <th>Qty</th>

                                            <th>Group</th>
                                            <th>Partno Akhir</th>
                                            <th>Lokasi</th>

                                            <th>Qty</th>
                                            <th>Mesin</th>
                                            <th>Users</th>
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

    <script src="../../Scripts/Factory/Simetris.js" type="text/javascript"></script>
    </html>
    <style>
		html,body,.form-control,btn{
            font-size: 11px;
            
		}

.ui-datepicker {
    width: 23em;
}

.zoom{
    zoom:80%
}



        .showhidden {
            visibility: hidden;
        }

        .showhiddenpanel {
            visibility: hidden;
        }

        .showhiddenbs {
            visibility: hidden;
        }

        .panelbox {
            background-color: #efeded;
            padding: 2px;
        }

        .scrollmenu {
            overflow: auto;
            white-space: nowrap;
            margin-left: 3px;
            margin-right: 3px;
        }

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

        table.dataTable tbody td {
  vertical-align: middle;
}

    </style>
</asp:Content>