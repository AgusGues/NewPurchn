<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MutasiLokasiPelarian.aspx.cs" Inherits="GRCweb1.Modul.Factory.MutasiLokasiPelarian" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Mutasi Lokasi Pelarian</title>

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
            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Proses Mutasi Lokasi Pelarian</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-6">
                                    <input type="checkbox" id="ukuran" name="Ukuran" value="1">
                                    <label for="form-field-9" style="font-size: 11px;">Retrieve By Ukuran</label>
                                </div>
                                <div class="col-md-6">
                                    <select class="form-control" name="" id="listukuran">
                                        <option value="" ></option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-6">
                                    <input type="checkbox" id="tglpelarian" name="Tanggal Pelarian" value="2">
                                    <label for="form-field-9" style="font-size: 11px;">Retrieve By Tgl Pelarian</label>
                                </div>
                                <div class="col-md-6">
                                    <input class="form-control date-picker" id="listtgl" name="tglpelarian" type="text">
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-6">
                                    <input type="checkbox" id="partnopelarian" name="Partno Pelarian" value="3">
                                    <label for="form-field-9" style="font-size: 11px;">Retrieve By Partno Pelarian</label>
                                </div>
                                <div class="col-md-6">
                                    <select class="form-control" name="" id="listpartnopelarian">
                                        <option value=""></option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tablelistpelarian" class="table table-striped table-hover display nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Tanggal Produksi</th>
                                            <th>Partno Asal</th>
                                            <th>Tanggal Serah</th>
                                            <th>Partno Pelarian</th>
                                            <th>Lokasi Pelarian</th>
                                            <th>Stock</th>
                                            <th>Qty Out</th>
                                            <th>Pilih</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Proses Pelarian</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-6 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Tanggal Potong</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="tglpotong" name="tgladjust" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Mutasi Partno</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control" type="text" id="partnook" style="text-transform:uppercase"/>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
<%--                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Mutasi Partno BP</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control" type="text" id="partnobp" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>--%>
                        </div>
                        <div class="col-xs-12 col-sm-6 form-group-sm">

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Jumlah Potong</label>
                                </div>
                                <div class="col-md-2">
                                    <input class="form-control" type="text" id="jumlahpotong" style="width: 75%" disabled/>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Qty</label>
                                </div>
                                <div class="col-md-2">
                                    <input class="form-control" type="text" id="qtyok" style="width: 75%" />
                                    <div style="padding: 2px"></div>
                                </div>
                                <div class="col-md-4">
                                    <label for="form-field-9">Lokasi</label>
                                </div>
                                <div class="col-md-2">
                                    <input class="form-control" type="text" id="lokasiok" style="width: 75%;text-transform:uppercase" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

             <%--               <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Qty</label>
                                </div>
                                <div class="col-md-2">
                                    <input class="form-control" type="text" id="qtybp" style="width: 75%" />
                                    <div style="padding: 2px"></div>
                                </div>
                                <div class="col-md-4">
                                    <label for="form-field-9">Lokasi</label>
                                </div>
                                <div class="col-md-2">
                                    <input class="form-control" type="text" id="lokasibp" style="width: 75%" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                    <div class="panel-footer padding-8 clearfix">
                        <button type='button' id="transferpartno" class="btn btn-primary pull-right">
                            <span class="bigger-110">Transfer</span>
                            <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                        </button>
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
    <script src="../../assets/jquery.mask.min.js"></script>
    <script src="../../Scripts/Factory/MutasiPelarian.js" type="text/javascript"></script>
    </html>

            <style>
.ui-datepicker {
    width: 23em;
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
            </style>
</asp:Content>