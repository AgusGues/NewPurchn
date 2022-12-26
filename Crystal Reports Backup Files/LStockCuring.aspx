<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LStockCuring.aspx.cs" MasterPageFile="~/Site.Master" Inherits="GRCweb1.Modul.ListReportT1T3.LStockCuring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Laporan Stock Curing Dan Jemur</title>

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
                        <h3 class="panel-title">LAPORAN STOCK CURING DAN JEMUR</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-10 form-group-sm">
                            <input type="radio" id="perlokasi" name="list" value="1" checked>
                            <label for="Bulanan" style="padding: 5px">Rekap Pemetaan per Lokasi</label>
                            <input type="radio" id="curingdetail" name="list" value="2">
                            <label for="Harian" style="padding: 5px">Stock Curing Detail</label>
                            <input type="radio" id="Jemurdetail" name="list" value="3">
                            <label for="Bulanan" style="padding: 5px">Stock Jemur Detail</label>
                            <input type="radio" id="harian" name="list" value="4">
                            <label for="Harian" style="padding: 5px">Pemantauan Produksi Harian</label>
                        </div>
                        <div class="col-xs-12 col-sm-6 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Per Tanggal</label>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="awaltgl" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">s/d Tanggal</label>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="akhirtgl" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row" id="hidepartno" hidden>
                                <div class="col-md-4">
                                    <label for="form-field-9">Partno</label>
                                </div>
                                <div class="col-md-6">
                                    <input type="text" id="caripartno" class="form-control" style="width: 100%;" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row" id="hidelokasi" hidden>
                                <div class="col-md-4">
                                    <label for="form-field-9">Lokasi</label>
                                </div>
                                <div class="col-md-4">
                                    <input type="text" id="carilokasi" class="form-control" style="width: 100%;" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div>
                                <button type='button' id="preview" class="btn btn-primary">
                                    <span class="bigger-110">Preview</span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalcuring" class="modal fade">
            <div class="modal-dialog1">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="smaller lighter blue no-margin">Laporan Stock Curing Dan Jemur</h3>
                    </div>
                    <div class="modal-body">
                        <button type='button' class="btn btn-sm btn-primary" id="exportexcel">
                            <i class="ace-icon fa fa-file-excel-o"></i>
                            Export Excel
                        </button>

                        <button type='button' class="btn btn-sm btn-default" id="print">
                            <i class="ace-icon fa fa-print"></i>
                            Print
                        </button>
                        <div class="table-responsive">
                            <div id="tbcj"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                            <i class="ace-icon fa fa-times"></i>
                            Exit
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div id="tbcjp" hidden></div>
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
    <script src="../../Scripts/ReportT1T3/LaporanStockCuringJemur.js"></script>
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

        .paging-nav {
            text-align: right;
            padding-top: 2px;
        }

            .paging-nav a {
                margin: auto 1px;
                text-decoration: none;
                display: inline-block;
                padding: 1px 7px;
                background: #91b9e6;
                color: white;
                border-radius: 3px;
            }

            .paging-nav .selected-page {
                background: #187ed5;
                font-weight: bold;
            }
    </style>
</asp:Content>