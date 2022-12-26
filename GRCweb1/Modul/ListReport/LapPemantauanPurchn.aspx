<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantauanPurchn.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantauanPurchn" %>
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
                        <h3 class="panel-title">Lap Pemantauan Purchasing</h3>
                    </div>
                    <div class="panel-body">
                        <div class="input-daterange input-group">
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    Dari Periode
                                </div>
                                <div class="col-lg-4">
                                    <input type="text" class="input-sm form-control" name="start" id="Start"/>
                                </div>
                            </div>
                            &nbsp
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    Ke Periode
                                </div>
                                <div class="col-lg-4">
                                    <input type="text" class="input-sm form-control" name="end" id="End" />
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
                                    <button class="btn btn-sm btn-primary" type="button" onclick="GetLapPurchn()">
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
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tableLap" class="table table-striped table-bordered table-hover" >
                               <thead>
                                    <tr>
                                        <th>No SPP</th>
                                        <th>Tgl SPP</th>
                                        <th>Approval SPP</th>
                                        <th>Nama Barang</th>
                                        <th>Kode Barang</th>
                                        <th>Satuan</th>
                                        <th>Jumlah SPP</th>
                                        <th>UserName</th>
                                        <th>No PO</th>
                                        <th>Tgl PO</th>
                                        <th>Tgl Approval PO</th>
                                        <th>Jumlah PO</th>
                                        <th>Sisa SPP</th>
                                        <th>Indent</th>
                                        <th>No Receipt</th>
                                        <th>Status Receipt</th>
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
    
    <script src="../../Scripts/ListReport/LapPemantauanPurchn.js" type="text/javascript"></script>
    <script src="../../assets/js/daterangepicker.min.js"></script>

</html>

</asp:Content>
