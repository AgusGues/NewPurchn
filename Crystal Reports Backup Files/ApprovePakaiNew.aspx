<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePakaiNew.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApprovePakaiNew" %>

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
                    Approval Pemakaian
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <table id="datatable" class="table" style="width: 100%;">
                            <thead>
                                <tr>
                                    <th> </th>
                                    <th>ID</th>
                                    <th>NoPakai</th>
                                    <th>PakaiDate</th>
                                    <th>CreatedBy</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
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
<script src="../../Scripts/Purchasing/ApvPakai.js" type="text/javascript"></script>    
</html>
</asp:Content>
