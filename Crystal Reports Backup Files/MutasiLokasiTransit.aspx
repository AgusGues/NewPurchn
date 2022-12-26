<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MutasiLokasiTransit.aspx.cs" Inherits="GRCweb1.Modul.Factory.MutasiLokasiTransit" %>

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

        <title>Mutasi Lokasi Transit</title>

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
                        <h3 class="panel-title">Proses Mutasi Lokasi Transit Finishing</h3>
                    </div>
                    <div class="panel-body">
<%--                                                <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9" >Tanggal Mutasi</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control date-picker" id="tglmutasi" name="tglmutasi" type="text">
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>--%>

                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9" >Lokasi Transit</label>
                                </div>
                                <div class="col-md-8">
                                    <select class="form-control" name="" id="lokasitransit">
                                        <option value="" ></option>
                                    </select>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <input type="checkbox" id="cektglserah" name="Tgl Serah" value="0" checked>
                                    <label for="form-field-9">Tanggal Serah</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control date-picker" id="tglserah" name="tglserah" type="text">
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                        <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Proses Mutasi Lokasi</h3>
                    </div>
                    <div class="panel-body">

                        <div class="col-xs-12 col-sm-4 form-group-sm">                        
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Partno Tujuan</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control" type="text" id="partnotujuan" style="text-transform:uppercase"/>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                                   <div class="col-md-4">
                                    <label for="form-field-9">Ukuran</label>
                                </div>
                                <div>
                                    <input type="text" id="tebal"  size="5" disabled/>
                                    X
                                    <input type="text" id="lebar" size="5"  disabled/>
                                    X
                                    <input type="text" id="panjang" size="5"  disabled/>
                                </div>
                        </div>
                        
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Lokasi</label>
                                </div>
                                <div class="col-md-8">
                                    <input class="form-control" type="text" id="lokasi" style="text-transform:uppercase" />
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
                                <table id="tablelisttransit" class="table table-striped table-hover display nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Tanggal Produksi</th>
                                            <th>Partno Tahap 1</th>
                                            <th>Tanggal Serah</th>
                                            <th>Tebal</th>
                                            <th>Panjang</th>
                                            <th>Lebar</th>
                                            <th>Partno Tahap 3</th>
                                            <th>Lokasi</th>
                                            <th>Stok</th>
                                            <th>Qty Out</th>
                                            <th>Pilih</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                                        <div class="panel-footer padding-8 clearfix">
  <label for="form-field-9" style="padding-right:10px">Quantity</label>

                                     <input type="text" id="qtyall" disabled/>
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
    <script src="../../Scripts/Factory/MutasiTransit.js"></script>

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
