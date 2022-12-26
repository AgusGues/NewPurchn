<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KasbonOSPemantauan.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KasbonOSPemantauan" %>
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
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

        <link rel="stylesheet" href="../../assets/css/daterangepicker.min.css" />
		
    </head>

    <body class="no-skin">
        <div class="row">
             <div class="col-xs-7 col-sm-7 form-group-sm">
                <div class="panel panel-primary">
                     <div class="panel-heading">
                        <h3 class="panel-title">Pemantauan OS Kasbon</h3>
                    </div>
                    <div class="panel-body">
                        <div class="input-daterange input-group">
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    Periode
                                </div>
                                <div class="col-lg-4">
                                    <input type="text" class="input-sm form-control" name="start" id="Start"/>
                                </div>
                                <div class="col-lg-4">
                                    <input type="text" class="input-sm form-control" name="end" id="End" />
                                </div>
                            </div>
                            &nbsp
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                   PIC
                                </div>
                                <div class="col-lg-4">
                                   <select class=" form-control" id="ddlpic">
                                        <option value=""> - - All PIC - - </option>
                                        
                                    </select>
                                </div>
                            </div>
                            &nbsp
                            <div class="col-lg-12">
                                &nbsp
                            </div>
                            &nbsp
                            <div class="col-lg-12">
                                <div class="col-lg-5">

                                </div>
                                <div>
                                    <button class="btn btn-sm btn-primary" type="button" onclick="GetKasbon()">
                                        <i class="ace-icon fa fa-search bigger-110"></i>Preview
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 form-group-sm" id="Tbldetail" style="display:none">
                <div class="panel panel-primary">
                    <!--<div class="panel-heading">
                        <h3 class="panel-title">List PO</h3>
                    </div>-->
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tableLap" class="table table-striped table-bordered table-hover" >
                            <!--<table id="tableListPO" class="table table-striped table-hover responsive nowrap" style="width: 100%">-->
                                 <thead>
                                    <tr>
                                        <th>No Pengajuan</th>
                                        <th>No Kasbon</th>
                                        <th>tanggal</th>
                                        <th>No SPP</th>
                                        <th>Item Name</th>
                                        <th>Satuan</th>
                                        <th>Qty</th>
                                        <th>Estimasi</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
        <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    
    <script src="../../Scripts/purchasing/KasbonOSPemantauan.js" type="text/javascript"></script>
    <script src="../../assets/js/daterangepicker.min.js"></script>

</html>

</asp:Content>
