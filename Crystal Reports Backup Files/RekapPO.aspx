<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.RekapPO" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Rekap PO</title>

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
        <script src="../../assets/html2pdf/dist/html2pdf.bundle.min.js" type="text/javascript"></script>
        <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    </head>

    <body class="no-skin">
        <script type="text/javascript">
            function OpenModal() {
                $("#MyPopup").modal("show");
            }
        </script>
        <div class="row">
            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Rekap PO</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="input-group">
                                <input class="form-control date-picker" id="tglawal" name="tglawal" type="text">
                                <span class="input-group-addon">
                                    <i class="fa fa-calendar bigger-110"></i>
                                </span>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-4 form-group-sm">
                            <div class="input-group">
                                <input class="form-control date-picker" id="tglakhir" name="tglakhir" type="text">
                                <span class="input-group-addon">
                                    <i class="fa fa-calendar bigger-110"></i>
                                </span>
                            </div>
                        </div>
                        <button class="btn btn-info btn-sm" type="button" id="preview" style="margin-left: 50px">
                            <i class="ace-icon fa fa-check bigger-110"></i>
                            Preview
                        </button>
                        <div>
                            <input type="text" id="idrekap" name="idrekap" style="display: none" />
                        </div>

                        <asp:Button runat="server" ID="btnOpen" OnClick="btnOpen_Click" Text="Show Popup" CssClass="btn btn-primary" />
                </div>
            </div>
            <asp:Panel ID="PanelView" runat="server">
                <div class="col-xs-12 col-sm-12 form-group-sm">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">List Rekap PO</h3>
                        </div>
                        <div class="panel-body">
                            <div style="width: 100%;">
                                <div class="table-responsive">
                                    <table id="tablerekappo" class="table table-striped table-hover display nowrap" style="width: 180%">
                                        <thead>
                                            <tr>
                                                <th>Action</th>
                                                <th>No PO</th>
                                                <th>Supplier Name</th>
                                                <th>Approval</th>

                                                <th>PPN</th>
                                                <th>PPH</th>
                                                <th>Mata Uang</th>

                                                <th>Item Name</th>
                                                <th>No SPP</th>
                                                <th>Qty</th>
                                                <th>Satuan</th>

                                                <th>Disc</th>
                                                <th>Price</th>
                                                <th>Total</th>

                                                <th>TOT2</th>
                                                <th>PO Purchn Date</th>
                                                <th>Group Desc</th>

                                                <th>Cetak</th>
                                                
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

<%--        <div >
            <div id="invoice">
                <div class="invoice-box">
                    <table cellpadding="0" cellspacing="0">
                        <tr class="top">
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td>PT BANGUNPERKASA ADHITAMA SENTRA<br />
                                            GRAHA GRC BOARD Lt.3<br />
                                            Jl. S. PARMAN kav.64 Slipi Palmerah<br />
                                            Jakarta 11410 - Indonesia
                                            Telp. (62-21) 53666800<br />
                                            Fax. (62-21) 53666720
                                        </td>

                                        <td>NO. KP12124324
                                        </td>
                                    </tr>
                                </table>

                                                                <table>
                                    <tr>
                                        <td>
                                            Vendor  : TEST <br />
                                                      UP. BP tets<br />
                                                      telp.<br />
                                                      Fax.

                                        </td>

                                        <td>
                                            PT BANGUNPERKASA ADHITAMA SENTRA<br />
                                            GRAHA GRC BOARD Lt.3<br />
                                            Jl. S. PARMAN kav.64 Slipi Palmerah<br />
                                            Jakarta 11410 - Indonesia
                                                    Telp. (62-21) 53666800<br />
                                            Fax. (62-21) 53666720
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr class="information">
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td>
                                            Please supply items listed below in accordance with condition & instruction as agreed:<br />
                                            Harap kirim barang-barang tersebut dibawah ini sesuai dengan syarat-syarat dan perintah yang telah disetujui

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr class="heading">
                            <td>No</td>
                            <td>Item Code</td>
                            <td>Delivery Date</td>
                            <td>Qty</td>
                            <td>Unit Price</td>
                            <td>Amount</td>
                        </tr>

                        <tr class="details">
                            <td>Check</td>
                            <td>1000</td>
                            <td>1000</td>
                            <td>1000</td>
                            <td>1000</td>
                            <td>1000</td>
                        </tr>

                        <tr class="heading">
                            <td>Item</td>

                            <td>Price</td>
                        </tr>

                        <tr class="item">
                            <td>Website design</td>

                            <td>$300.00</td>
                        </tr>

                        <tr class="item">
                            <td>Hosting (3 months)</td>

                            <td>$75.00</td>
                        </tr>

                        <tr class="item last">
                            <td>Domain name (1 year)</td>

                            <td>$10.00</td>
                        </tr>

                        <tr class="total">
                            <td></td>

                            <td>Total: $385.00</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>--%>

        
        <div id="invoice" class="hide">
            <div id="invoice2">

        </div>
        </div>
        <asp:Panel ID="PanelReport" Visible="false"  runat="server">
        <div id="MyPopup">
            <div>
                <!-- Modal Header -->
                <div class="bg-default-gradient">
                    <h4 >Report</h4>
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <asp:Button ID="Button1" runat="server" Text="Close" OnClick="Button1_Click" />
                </div>
                <!-- Modal body -->
                <div class="card-body bg-default-gradient">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" style="overflow: scroll; width: 100%; height: 100%">
                                <div id="dvReportFinal">
                                    <CR:CrystalReportViewer ID="crRekapPo" runat="server" AutoDataBind="true"  
            HasToggleGroupTreeButton="false" DisplayToolbar="True" EnableDatabaseLogonPrompt="False"
            EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bg-default-gradient">
                </div>
            </div>
        </div>
        </asp:Panel>
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
    <script src="../../assets/terbilang.js"></script>


    <script src="../../Scripts/Purchasing/RekapPO.js" type="text/javascript"></script>
    </html>
    <style>
                .ui-datepicker {
            width: 23em;
        }
        .hide {
            display: none;
        }

        .br {
            display: block;
            margin-bottom: 0em;
        }

        .invoice-box {
            max-width: 900px;
            margin: auto;
            padding: 10px;
            border: 1px solid #eee;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
            font-size: 9px;
            line-height: 24px;
            font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
            color: #555;
        }

            .invoice-box table {
                width: 100%;
                line-height: inherit;
                text-align: left;
            }

                .invoice-box table td {
                    padding: 5px;
                    vertical-align: top;
                    padding-bottom: 5px;
                }

           /*     .invoice-box table tr td:nth-child(2) {
                    width: 150px;
                }*/


                .invoice-box table tr.top table td {
                    padding-bottom: 20px;
                }

                    .invoice-box table tr.top table td.title {
                        font-size: 45px;
                        line-height: 45px;
                        color: #333;
                    }

                .invoice-box table tr.information table td {
                    padding-bottom: 40px;
                }

                .invoice-box table tr.heading td {
                    background: #eee;
                    border-bottom: 1px solid #ddd;
                    font-weight: bold;
                }

                .invoice-box table tr.details td {
                    padding-bottom: 20px;
                }

                .invoice-box table tr.item td {
                    border-bottom: 1px solid #eee;
                }

                .invoice-box table tr.item.last td {
                    border-bottom: none;
                }

                .invoice-box table tr.total td:nth-child(2) {
                    border-top: 2px solid #eee;
                    font-weight: bold;
                }

        @media only screen and (max-width: 700px) {
            .invoice-box table tr.top table td {
                width: 100%;
                display: block;
                text-align: center;
            }

            .invoice-box table tr.information table td {
                width: 100%;
                display: block;
                text-align: center;
            }
        }

        /** RTL **/
        .invoice-box.rtl {
            direction: rtl;
            font-family: Tahoma, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
        }

            .invoice-box.rtl table {
                text-align: right;
            }

                .invoice-box.rtl table tr td:nth-child(2) {
                    text-align: left;
                }
    </style>
</asp:Content>