<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApprovePO" %>

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
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">LIST PO</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tableListopenpo" class="table table-striped table-hover table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>ID</th>
                                            <th>No PO</th>
                                            <th>Supplier</th>
                                            <th>UP Supplier</th>
                                            <th>Term Of Payment</th>
                                            <th>Term Of Delivery</th>
                                            <th>PPN</th>
                                            <th>PPH</th>
                                            <th>Mata Uang</th>
                                            <th>Uang Muka</th>
                                            <th>Discount</th>
                                            <th>Remark</th>
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

    <script src="../../Scripts/Purchasing/ApprovePO.js" type="text/javascript"></script>
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
    </style>
</asp:Content>