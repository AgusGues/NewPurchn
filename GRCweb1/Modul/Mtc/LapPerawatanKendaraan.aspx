<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPerawatanKendaraan.aspx.cs" Inherits="GRCweb1.Modul.Mtc.LapPerawatanKendaraan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
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
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <table style="table-layout: fixed; width: 100%;">
            <tr>
                <td colspan="5" style="height: 49px">
                    <!--header tabel-->
                    <table class="nbTableHeader" width="100%">
                        <tr>
                            <td style="width: 40%"><strong>&nbsp;LAPORAN PERAWATAN KENDARAAN</strong></td>
                            <td style="width: 60%"></td>
                        </tr>
                    </table>
                    <!-- end of header-->
                </td>
            </tr>
            <tr>
                <td colspan="5" valign="top" align="left">
                    <div id="div2" style="width: 100%;" class="content">
                        <table id="headerPO" width="100%" style="border-collapse: collapse;" visible="true" runat="server">
                            <tr>
                                <td align="right">&nbsp;Laporan</td>
                                <td>
                                    <asp:RadioButton ID="rkpArmada" runat="server" Text="Rekap" GroupName="cr"
                                        Checked="true" AutoPostBack="true"
                                        OnCheckedChanged="rkpArmada_CheckedChanged" />&nbsp;
                            <asp:RadioButton ID="lsArmada" runat="server" Text="Detail" GroupName="cr"
                                AutoPostBack="true" OnCheckedChanged="lsArmada_CheckedChanged" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="15%" align="right">Dari Tanggal &nbsp;</td>
                                <td width="20%">
                                    <asp:TextBox ID="fromDate" runat="server"></asp:TextBox></td>
                                <td width="5%"></td>
                                <td width="10%" align="right">
                                    <cc1:calendarextender id="CalendarExtender1" targetcontrolid="fromDate" format="dd-MMM-yyyy" runat="server">
                        </cc1:calendarextender>
                                </td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td width="15%" align="right">Sampai Tanggal&nbsp;</td>
                                <td width="20%">
                                    <asp:TextBox ID="toDate" runat="server"></asp:TextBox></td>
                                <td width="5%"></td>
                                <td width="10%" align="right">
                                    <cc1:calendarextender id="CalendarExtender2" targetcontrolid="toDate" format="dd-MMM-yyyy" runat="server">
                        </cc1:calendarextender>
                                </td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td width="15%" align="right">No. Kendaraan&nbsp;</td>
                                <td width="20%" colspan="2" style="width: 25%">
                                    <asp:DropDownList ID="ddlNoPolisi" runat="server" AutoPostBack="true"
                                        Height="16px" OnSelectedIndexChanged="ddlNoPolisi_SelectedIndexChanged"
                                        Width="192px">
                                    </asp:DropDownList>
                                </td>
                                <td width="15%" align="right">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="15%" align="right">&nbsp;</td>
                                <td width="20%" colspan="2" style="width: 25%">
                                    <asp:Button ID="Preview" runat="server" Text="Preview" OnClick="Preview_Click" />
                                    <asp:Button ID="Print" runat="server" Text="Print Out" OnClick="Print_Click" /></td>
                                <td width="15%" align="right">&nbsp;</td>
                                <td width="20%">&nbsp;</td>
                            </tr>
                        </table>
                        <div style="width: 100%; height: 340px; overflow: auto" class="contentlist">
                            <div id="rekapArmada" runat="server" visible="true">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td width="70%">
                                            <table width="100%" style="border-collapse: collapse; font-size: x-small" border="1">
                                                <thead>
                                                    <tr align="center" style="background-color: Olive;">
                                                        <th width="10%" style="border-bottom: 1px solid">No.</th>
                                                        <th width="15%" style="border-bottom: 1px solid">No.Kendaraan</th>
                                                        <th width="45%" style="border-bottom: 1px solid">Jenis Kendaraan</th>
                                                        <th width="20%" style="border-bottom: 1px solid">Value</th>
                                                        <th width="20%" style="border-bottom: 1px solid">%</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstSarmut" runat="server">
                                                        <ItemTemplate>
                                                            <tr style="cursor: pointer; background-color: #E0D8E0">
                                                                <td align="center" style="border-bottom: 1px solid"><%# Eval("ID") %></td>
                                                                <td style="border-bottom: 1px solid"><%# Eval("NoPol")%></td>
                                                                <td align="left" style="border-bottom: 1px solid"><%# Eval("SPBNo")%></td>
                                                                <td align="right" style="border-bottom: 1px solid"><%# Eval("AvgPrice","{0:#,#00.00}") %></td>
                                                                <td align="center" style="border-bottom: 1px solid"><%# Eval("Quantity","{0:#,#00.00}") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr style="cursor: pointer">
                                                                <td align="center" style="border-bottom: 1px solid"><%# Eval("ID") %></td>
                                                                <td style="border-bottom: 1px solid"><%# Eval("NoPol") %></td>
                                                                <td align="left" style="border-bottom: 1px solid"><%# Eval("SPBNo")%></td>
                                                                <td align="right" style="border-bottom: 1px solid"><%# Eval("AvgPrice","{0:#,#00.00}") %></td>
                                                                <td align="center" style="border-bottom: 1px solid"><%# Eval("Quantity","{0:#,#00.00}") %></td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <%--<asp:Repeater ID="sTotal" runat="server">
                                <ItemTemplate>--%>
                                                    <tr style="background-color: Silver">
                                                        <td colspan="3" align="right">
                                                            <b>TOTAL</b>
                                                        </td>
                                                        <td align="right" id="ttl" runat="server">
                                                            <%--<b><span id="ttl" runat="server"><%#Eval("TotalS","{0:N2}") %></span></b>--%>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <%--</ItemTemplate>
                            </asp:Repeater>--%>
                                                </tfoot>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; width: 30%">
                                            <%--<asp:ImageMap ID="chart" runat="server"></asp:ImageMap>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="detailArmada" runat="server" visible="false" class="contentlist">
                                <table style="border-collapse: collapse; font-size: x-small" width="100%" border="1">
                                    <thead>
                                        <tr style="background-color: #FFD800">
                                            <th style="width: 5%">#</th>
                                            <th style="width: 8%">Tanggal</th>
                                            <th style="width: 10%">Item Code</th>
                                            <th style="width: 30%">Item Name</th>
                                            <th style="width: 5%">Unit</th>
                                            <th style="width: 5%">Quantity</th>
                                            <th style="width: 8%">Value</th>
                                            <th style="width: 8%">Total</th>
                                            <th style="width: 20%">keterangan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="dtlArmada" runat="server">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td align="center"><%# Eval("ID") %></td>
                                                    <td align="center"><%# Eval("SPBDate","{0:d}") %></td>
                                                    <td><%# Eval("ItemCode") %></td>
                                                    <td><%# Eval("ItemName") %></td>
                                                    <td align="center"><%# Eval("Satuan") %></td>
                                                    <td align="right"><%# Eval("Quantity","{0:N2}") %></td>
                                                    <td align="right"><%# Eval("AvgPrice", "{0:N2}")%></td>
                                                    <td align="right"><%# Eval("Total","{0:N2}") %></td>
                                                    <td align="left"><%# Eval("DeptName") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="OddRows baris">
                                                    <td align="center"><%# Eval("ID") %></td>
                                                    <td align="center"><%# Eval("SPBDate","{0:d}") %></td>
                                                    <td><%# Eval("ItemCode") %></td>
                                                    <td><%# Eval("ItemName") %></td>
                                                    <td align="center"><%# Eval("Satuan") %></td>
                                                    <td align="right"><%# Eval("Quantity","{0:N2}") %></td>
                                                    <td align="right"><%# Eval("AvgPrice", "{0:N2}")%></td>
                                                    <td align="right"><%# Eval("Total","{0:N2}") %></td>
                                                    <td align="left"><%# Eval("DeptName") %></td>
                                                </tr>
                                            </AlternatingItemTemplate>

                                        </asp:Repeater>

                                        <tr class="Line3 baris">
                                            <td align="right" colspan="6">Total</td>
                                            <td align="right" id="ta" runat="server"></td>
                                            <td align="right" id="tt" runat="server"></td>
                                            <td align="right">&nbsp;</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                            <div id="detailAllArmada" runat="server" visible="false">
                                <table style="border-collapse: collapse; font-size: x-small" width="100%" border="1">
                                    <thead>
                                        <tr style="background-color: #FFD800">
                                            <th style="width: 5%">#</th>
                                            <th style="width: 8%">Tanggal</th>
                                            <th style="width: 10%">Item Code</th>
                                            <th style="width: 30%">Item Name</th>
                                            <th style="width: 5%">Unit</th>
                                            <th style="width: 5%">Quantity</th>
                                            <th style="width: 8%">Value</th>
                                            <th style="width: 8%">Total</th>
                                            <th style="width: 20%">keterangan </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstNopol" runat="server" OnItemDataBound="lstNopol_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="Line3 baris">
                                                    <td align="center"><%# Eval("ID") %></td>
                                                    <td colspan="7"><%# Eval("NoPol") %></td>
                                                    <td align="center"><span style="display: none"><%# Eval("IDKendaraan") %></span></td>
                                                </tr>
                                                <asp:Repeater ID="dtlLstArmada" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td align="right"><%# Eval("ID") %>&nbsp;</td>
                                                            <td align="center"><%# Eval("SPBDate","{0:d}") %></td>
                                                            <td><%# Eval("ItemCode") %></td>
                                                            <td><%# Eval("ItemName") %></td>
                                                            <td align="center"><%# Eval("Satuan") %></td>
                                                            <td align="right"><%# Eval("Quantity","{0:N2}") %></td>
                                                            <td align="right"><%# Eval("AvgPrice", "{0:N2}")%></td>
                                                            <td align="right"><%# Eval("Total","{0:N2}") %></td>
                                                            <td align="left"><%# Eval("DeptName") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td align="right"><%# Eval("ID") %>&nbsp;</td>
                                                            <td align="center"><%# Eval("SPBDate","{0:d}") %></td>
                                                            <td><%# Eval("ItemCode") %></td>
                                                            <td><%# Eval("ItemName") %></td>
                                                            <td align="center"><%# Eval("Satuan") %></td>
                                                            <td align="right"><%# Eval("Quantity","{0:N2}") %></td>
                                                            <td align="right"><%# Eval("AvgPrice", "{0:N2}")%></td>
                                                            <td align="right"><%# Eval("Total","{0:N2}") %></td>
                                                            <td align="left"><%# Eval("DeptName") %></td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                            <%--<tr style="background-color:#A0A8A0">
                                    <td align="right" colspan="6">Total</td>
                                    <td align="right" id="tal" runat="server"></td>
                                    <td align="right" id="tdl" runat="server"></td>
                                    <td align="right">&nbsp;</td>
                                    </tr>--%>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <%--end of tabel content--%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
