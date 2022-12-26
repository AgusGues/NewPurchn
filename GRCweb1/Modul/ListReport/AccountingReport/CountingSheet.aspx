<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CountingSheet.aspx.cs" Inherits="GRCweb1.Modul.ListReport.AccountingReport.CountingSheet" %>

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
                            var proses;
                            if ($('input[id*=Curing]').is(":checked")) {
                                proses = "Curing";
                            } else if ($('input[id*=Jemur]').is(":checked")) {
                                proses = "Jemur";
                            } else if ($('input[id*=Transit]').is(":checked")) {
                                proses = "Transit";
                            } else if ($('input[id*=Pjadi]').is(":checked")) {
                                proses = "Pjadi";
                            }
                            MyPopUpWin("../AccountingReport/Report.aspx?IdReport=CountSheet&tp="+proses+"", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">WIP BreakDown</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Counting Sheet :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <span>
                                                <asp:RadioButton ID="Curing" runat="server" GroupName="Cs" Text="Curing"
                                                    OnCheckedChanged="Curing_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="Jemur" runat="server" GroupName="Cs" Text="Jemur"
                                                    OnCheckedChanged="Jemur_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="Transit" runat="server" GroupName="Cs" Text="Transit Area"
                                                    OnCheckedChanged="Transit_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="Pjadi" runat="server" GroupName="Cs" Text="Logistic"
                                                    OnCheckedChanged="Pjadi_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <asp:Button ID="printsheet" runat="server" Text="Print" Enabled="false"
                                            OnClick="printsheet_Click" Width="67px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div id="pnCuring" runat="server" style="background-color: White; padding: 3px; border: 3px solid ActiveCaption; height: 430px; overflow: auto">
                                    <table style="width: 100%; border-collapse: collapse" border="1">
                                        <tr style="background-color: Silver">
                                            <th style="width: 5%">No.</th>
                                            <th style="width: 8%">Tgl Produksi</th>
                                            <th style="width: 15%">Part No.</th>
                                            <th style="width: 5%">Lokasi</th>
                                            <th style="width: 5%">Nomor</th>
                                            <th style="width: 5%">No. Palet</th>
                                            <th style="width: 8%">Jumlah</th>
                                            <th style="width: 10%">Counting</th>
                                            <th style="width: 8%">Balance</th>
                                        </tr>
                                        <asp:Repeater ID="lstCuring" runat="server">
                                            <ItemTemplate>
                                                <tr align="center">
                                                    <td><%#Eval("Nom") %></td>
                                                    <td><%#Eval("Tanggal","{0:d}") %></td>
                                                    <td align="left"><%#Eval("PartNo") %></td>
                                                    <td><%#Eval("Lokasi") %></td>
                                                    <td><%#Eval("Rak") %></td>
                                                    <td><%#Eval("PaletNo") %></td>
                                                    <td align="right"><%#Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr align="center" style="background-color: #E0E8F0">
                                                    <td><%#Eval("Nom") %></td>
                                                    <td><%#Eval("Tanggal", "{0:d}")%></td>
                                                    <td align="left"><%#Eval("PartNo") %></td>
                                                    <td><%#Eval("Lokasi") %></td>
                                                    <td><%#Eval("Rak") %></td>
                                                    <td><%#Eval("PaletNo") %></td>
                                                    <td align="right"><%#Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div id="pnJemur" runat="server" style="background-color: White; padding: 3px; border: 3px solid ActiveCaption; height: 430px; overflow: auto" visible="false">
                                    <table style="width: 100%; border-collapse: collapse" border="1">
                                        <tr style="background-color: Silver">
                                            <th style="width: 5%">No.</th>
                                            <th style="width: 8%">Tgl Produksi</th>
                                            <th style="width: 15%">Part No.</th>
                                            <th style="width: 5%">PaletNo</th>
                                            <th style="width: 5%">Rak</th>
                                            <th style="width: 5%">Nomor</th>
                                            <th style="width: 8%">Jumlah</th>
                                            <th style="width: 10%">Counting</th>
                                            <th style="width: 8%">Balance</th>
                                        </tr>
                                        <asp:Repeater ID="lstJemur" runat="server">
                                            <ItemTemplate>
                                                <tr align="center">
                                                    <td><%#Eval("Nom") %></td>
                                                    <td><%#Eval("ToPartNo") %></td>
                                                    <td align="left"><%#Eval("PartNo") %></td>
                                                    <td><%#Eval("PaletNo") %></td>
                                                    <td><%#Eval("Lokasi") %></td>
                                                    <td><%#Eval("Rak") %></td>
                                                    <td align="right"><%#Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr align="center" style="background-color: #E0E8F0">
                                                    <td><%#Eval("Nom") %></td>
                                                    <td><%#Eval("ToPartNo")%></td>
                                                    <td align="left"><%#Eval("PartNo") %></td>
                                                    <td><%#Eval("PaletNo") %></td>
                                                    <td><%#Eval("Lokasi") %></td>
                                                    <td><%#Eval("Rak") %></td>
                                                    <td align="right"><%#Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div id="pnTransit" runat="server" style="background-color: White; padding: 3px; border: 3px solid ActiveCaption; height: 430px; overflow: auto">
                                    <table style="border-collapse: collapse" width="100%" border="1">
                                        <tr align="center" style="background-color: #A0B0E0">
                                            <th style="width: 5%">No.</th>
                                            <th style="width: 20%">Part No.</th>
                                            <th style="width: 20%">Ukuran</th>
                                            <th style="width: 15%" colspan="2">Lokasi</th>
                                            <th style="width: 10%">Quantity</th>
                                            <th style="width: 20%">Counting</th>
                                            <th style="width: 10%">Balance</th>
                                        </tr>
                                        <asp:Repeater ID="lstTransit" runat="server">
                                            <ItemTemplate>
                                                <tr align="center">
                                                    <td><%# Eval("Nom") %></td>
                                                    <td align="left"><%# Eval("PartNo") %></td>
                                                    <td align="left"><%# Eval("PaletNo") %></td>
                                                    <td align="right"><%# Eval("Lokasi") %></td>
                                                    <td align="left"><%# Eval("Rak") %></td>
                                                    <td align="right"><%# Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr align="center" style="background-color: #E0E8F0">
                                                    <td><%# Eval("Nom") %></td>
                                                    <td align="left"><%# Eval("PartNo") %></td>
                                                    <td align="left"><%# Eval("PaletNo") %></td>
                                                    <td align="right"><%# Eval("Lokasi") %></td>
                                                    <td align="left"><%# Eval("Rak") %></td>
                                                    <td align="right"><%# Eval("QtyIn","{0:N0}") %></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>