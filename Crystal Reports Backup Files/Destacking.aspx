<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Destacking.aspx.cs" EnableEventValidation="false" Inherits="GRCweb1.Modul.Factory.Destacking" %>

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
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
        <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Input Destasking</h3>
                    </div>

                    <div class="panel-body">
                        <%--                        <div class="radio" id="panelbs">
                            <label>
                                <input name="panelbs" type="radio" id="jemur" class="ace" />
                                <span class="lbl">Untuk Proses Jemur</span>
                            </label>
                            <label>
                                <input name="panelbs" type="radio" id="oven" class="ace" checked="checked" />
                                <span class="lbl">Untuk Proses Oven</span>
                            </label>
                        </div>--%>
                        <div class="col-xs-12 col-sm-6 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Tanggal Produksi</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="tglproduksi" name="tgladjust" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Input Jam</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="drjam" class="form-control" style="width: 45%;" />
                                    s/d
                                    <input type="text" id="sdjam" class="form-control" style="width: 45%;" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Jenis</label>
                                </div>
                                <div class="col-md-8">
                                    <select class="form-control" id="jenis">
                                        <option value="">Pilih Jenis</option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">No. Produksi</label>
                                </div>
                                <div class="col-md-8">
                                    <select class="form-control" id="noproduk">
                                        <option value="">No. Produksi</option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Shift</label>
                                </div>
                                <div class="col-md-8">
                                    <select class="form-control" id="shift">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Group</label>
                                </div>
                                <div class="col-md-8">
                                    <select class="form-control" id="group">
                                        <option value="">Group</option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Jumlah</label>
                                </div>
                                <div class="col-md-8">
                                    <input id="jumlah" type="text" placeholder="Jumlah" class="form-control" width="50%" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer text-right">
                        <button class="btn btn-info" type="button" id="clean"><i class="fa fa-eraser bigger-110"></i>Clean</button>
                        <button class="btn btn-primary" type="button" id="transfer">
                            <i class="ace-icon fa fa-check bigger-110"></i>
                            Transfer
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="hr hr-24"></div>
        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">LIST PRODUKSI DESTACKING</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tableListDestacking" class="table table-striped table-hover responsive nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>ID </th>
                                            <th>Jenis</th>
                                            <th>Partno</th>
                                            <th>Qty</th>
                                            <th>Status</th>
                                            <th>Tgl Produksi</th>
                                            <th>Dari Jam</th>
                                            <th>Sampai Jam</th>
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
    <script src="../../assets/bootstrap-datetimepicker/js/bootstrap-datetimepicker.js"></script>
    <script src="../../assets/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    <script src="../../Scripts/Factory/Destacking.js" type="text/javascript"></script>
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