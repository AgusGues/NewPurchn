<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapRetur.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.RekapRetur" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title></title>

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
                </script>

                    <script type="text/javascript">
                        function MyPopUpWin(url, width, height) {
                            var leftPosition, topPosition;
                            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
                            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
                            window.open(url, "Window2",
                                "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
                                + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
                                + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
                        }

                        function Cetak() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=PemantauanRetur", 900, 800);
                        }
                        function Cetak1() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=RekapRetur", 900, 800);
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Rekap Retur</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Periode Report : </label>
                                        </div>
                                        <div class="col-md-9">
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Dari Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl1" runat="server" class="form-control"
                                                Width="126px"></asp:TextBox>
                                            <cc1:calendarextender id="CalendarExtender1" runat="server"
                                                format="dd-MMM-yyyy" targetcontrolid="txtTgl1">
                                            </cc1:calendarextender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl2" runat="server" class="form-control"
                                                Width="126px"></asp:TextBox>
                                            <cc1:calendarextender id="CalendarExtender2" runat="server"
                                                format="dd-MMM-yyyy" targetcontrolid="txtTgl2">
                                            </cc1:calendarextender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <asp:Button ID="btnPrev" class="btn btn-sm btn-primary" runat="server" OnClick="btnPreview_Click"
                                            Text="Preview" />
                                        <asp:Button ID="btnPrint0" class="btn btn-sm btn-info" runat="server" OnClick="btnPrint0_Click"
                                            Text="Print Rekap Return" Width="159px" />
                                        <asp:Button ID="btnPrint" class="btn btn-sm" runat="server" OnClick="btnPrint_ServerClick"
                                            Text="Print Pemantauan Return" Width="159px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <table style="width:100%; border-collapse:collapse; font-size:small">
                                                    <thead>
                                                        <tr class="tbHeader baris">
                                                            <th class="kotak" style="width:5%">No.</th>
                                                            <th class="kotak" style="width:10%"><asp:Button ID="tglTrans" runat="server" CssClass="txtbox tbHeader img" Width="100%"  OnClick="SortBy_tgltrans" Text="Tanggal" /></th>
                                                            <th class="kotak" style="width:20%"><asp:Button ID="Customer" runat="server" CssClass="txtbox tbHeader img" Width="100%"  OnClick="SortBy_Customer" Text="Customer"/></th>
                                                            <th class="kotak" style="width:15%">SJ No</th>
                                                            <th class="kotak" style="width:10%">OP No</th>
                                                            <th class="kotak" style="width:20%">PartNo</th>
                                                            <th class="kotak" style="width:10%">Quantity</th>
                                                            <th class="kotak" style="width:15%">Type</th> 
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstRetur" runat="server">
                                                            <ItemTemplate>
                                                                <tr class=" EvenRows baris">
                                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                    <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                                    <td class="kotak"><%# Eval("Customer") %></td>
                                                                    <td class="kotak"><%# Eval("SJNo") %></td>
                                                                    <td class="kotak"><%# Eval("OPNo") %></td>
                                                                    <td class="kotak"><%# Eval("PartNo") %></td>
                                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                                    <td class="kotak"><%# Eval("typeR") %></td>
                                                                    <%--<td class="kotak tengah">&nbsp;</td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                            <tr class=" OddRows baris">
                                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                    <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                                    <td class="kotak"><%# Eval("Customer") %></td>
                                                                    <td class="kotak"><%# Eval("SJNo") %></td>
                                                                    <td class="kotak"><%# Eval("OPNo") %></td>
                                                                    <td class="kotak"><%# Eval("PartNo") %></td>
                                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                                    <td class="kotak"><%# Eval("typeR") %></td>
                                                                    <%--<td class="kotak tengah">&nbsp;</td>--%>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                        
                                                    </tbody>
                                                    <asp:Repeater ID="lstTotal" runat="server">
                                                        <ItemTemplate>
                                                        <tr class="content">
                                                        <td colspan="6" class="kotak angka">Total Retur</td>
                                                        <td class="kotak angka"><%# Eval("Total","{0:N2}") %></td>
                                                        <td class="kotak"></td>
                                                        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>