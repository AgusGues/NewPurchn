<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoryPO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.HistoryPO" %>
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
    </head>

    <body class="no-skin">
        <div class="row">
             <div class="col-xs-7 col-sm-7 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div>
                            <div class="col-lg-2">
                                Search By
                            </div>
                            <div class="col-lg-4">
                                <select id="ddlbarang">
                                    <option value="popurchn.nopo like">No. PO</option>
                                    <option value="SPP.nospp like">No. SPP</option>
                                    <option value="POPurchnDetail.ItemID in(select ID from Inventory where ItemCode like">ITEM CODE</option>
                                    <option value="popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like">ITEM NAME (INVENTORY)</option>
                                    <option value="popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like">ITEM NAME (ASSET)</option>
                                    <option value="3">ITEM NAME (BIAYA)</option>
                                    <option value="SuppPurch.SupplierName like">SUPPLIER</option>
                                </select>
                            </div>
                           
                            <div class="col-lg-4">
                                <input type="text" id="txtcari" class="col-sm-12 col-lg-12" />
                            </div>
                            <div class="col-lg-2">
                                <button class="btn btn-sm btn-primary" type="button" onclick="ShowHide()">
                                    <i class="ace-icon fa fa-search bigger-110"></i>Cari
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>  
            <div class="col-xs-12 col-sm-12 form-group-sm" id="Tbldetail" style="display:none">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">List PO</h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tableListPO" class="table table-striped table-bordered table-hover" >
                            <!--<table id="tableListPO" class="table table-striped table-hover responsive nowrap" style="width: 100%">-->
                                 <thead>
                                    <tr>
                                        <th>PO IssueDate</th>
                                        <th>delivery Date</th>
                                        <th>NoPo</th>
                                        <th>NoSPP</th>
                                        <th>ItemCode</th>
                                        <th>ItemName</th>
                                        <th>Satuan</th>
                                        <th>Price</th>
                                        <th>CRC</th>
                                        <th>Qty</th>
                                        <th>SupplierName</th>
                                        <th>Term of Payment</th>
                                        <th>Term of Delivery</th>
                                        <th>Disc</th>
                                        <th>PPN</th>
                                        <th>Receipt No</th>
                                        <th>Receipt Date</th>
                                        <th>Quantity</th>
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
    
        <script src="../../Scripts/Purchasing/HistPO.js" type="text/javascript"></script>
    
    </html>

</asp:Content>
