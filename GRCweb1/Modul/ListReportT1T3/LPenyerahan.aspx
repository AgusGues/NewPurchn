<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LPenyerahan.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LPenyerahan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>Pemetaan Tahap 1</title>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LPenyerahan", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN PENYERAHAN PRODUK KE CURING</h3>
                            </div>
                            <div class="panel-body">
                                
                                    <div class="col-xs-12 col-sm-6">
                                        <div class="row">
                                            <span>
                                                <asp:RadioButton ID="RBTgl" runat="server" Style="margin:5px" Checked="True" Font-Size="X-Small" GroupName="a"
                                                    Text="Per Tanggal Produksi" AutoPostBack="True" OnCheckedChanged="RBTgl_CheckedChanged" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="RBBln" runat="server" Font-Size="X-Small" GroupName="a"
                                                    Text="Bulanan " AutoPostBack="True" OnCheckedChanged="RBBln_CheckedChanged" />
                                            </span>
                                        </div>
                                        <asp:Panel ID="Panel3" runat="server">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 14px">Tanggal Produksi</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtFromPostingPeriod" runat="server" AutoPostBack="True" class="form-control"
                                                    Width="233" OnTextChanged="txtFromPostingPeriod_TextChanged1"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtFromPostingPeriod"></cc1:CalendarExtender>

                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 14px"></label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtToPostingPeriod" runat="server" AutoPostBack="True" class="form-control"
                                                    Width="233" Visible="False"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtToPostingPeriod"></cc1:CalendarExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
   
                                                                            </asp:Panel>
                                        <asp:Panel ID="Panel4" Visible="False" runat="server">
                                     <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlBulan" class="form-control" runat="server" Height="19px" Width="200px">
                                                    <%--<asp:ListItem Value="0">Pilih Bulan</asp:ListItem>--%>
                                                    <asp:ListItem Value="1">Januari</asp:ListItem>
                                                    <asp:ListItem Value="2">Februari</asp:ListItem>
                                                    <asp:ListItem Value="3">Maret</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">Mei</asp:ListItem>
                                                    <asp:ListItem Value="6">Juni</asp:ListItem>
                                                    <asp:ListItem Value="7">Juli</asp:ListItem>
                                                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtTahun" runat="server" class="form-control" AutoPostBack="True"
                                                    Width="84px"></asp:TextBox>
                                            </div>
                                        </div>
                                </asp:Panel>
                                        <div class="row">
                                            <input id="btnPrint" runat="server" class="btn btn-sm btn-info" onserverclick="btnPrint_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cetak" />
                                            <input id="btnLihat" runat="server" class="btn btn-sm btn-primary" onserverclick="btnLihat_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Preview" />
                                            <input id="btnRelease" runat="server" class="btn btn-sm" onserverclick="btnRelease_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Release" />
                                            <input id="btnApv" runat="server" class="btn btn-sm btn-primary" onserverclick="btnApv_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Approval" />
                                            <input id="btnCancel" runat="server" class="btn btn-sm btn-danger" onserverclick="btnCancel_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cancel" />
                                        </div>
                                    </div>
                                                                     
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; display: block"
                                                                border="0">
                                                                <thead>
                                                                    <tr class=" tbHeader baris">
                                                                        <th class="kotak" style="width: 5%">
                                                                            No.
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%">
                                                                            Tanggal Produksi
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%">
                                                                            Partno
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            Jumlah
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            M3
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            MiliMeter Produksi
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            Konversi 4x8
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            Status
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%">
                                                                            Approved By
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody style="font-family: Calibri">
                                                                    <asp:Repeater ID="DataSerah" runat="server" OnItemDataBound="DataSerah_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                                <td class="kotak tengah">
                                                                                    &nbsp;<%# Eval("No") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    &nbsp;<%# Convert.ToDateTime(Eval("TglProduksi")).ToString("dd-MMM-yyyy")%>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    &nbsp;
                                                                                    <%# Eval("Partno") %>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                   
                                                                                    <%# Eval("Jumlah", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("M3", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("MM_Produksi", "{0:N0}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Konversi", "{0:N1}") + ""%>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    &nbsp;<%# Eval("Status") %>
                                                                                </td>
                                                                                  <td class="kotak">
                                                                                    &nbsp;<%# Eval("LastApv") %>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>