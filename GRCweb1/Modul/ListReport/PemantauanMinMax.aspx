<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanMinMax.aspx.cs" Inherits="GRCweb1.Modul.ListReport.PemantauanMinMax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label{font-size:12px;}
    </style>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
        <div id="Div1" runat="server">
            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <tr valign="top">
                    <td style="height:49px">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width:100%">&nbsp;&nbsp;PEMANTAUAN MIN MAX (SPARE PART )</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div class="content" style="background:#fff;">
                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr>
                                    <td style="width:10%">&nbsp;</td>
                                    <td style="width:10%">Periode</td>
                                    <td style="width:35%">
                                        <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                    </td>
                                    <td style="width:45%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Material Group</td>
                                    <td><asp:DropDownList ID="ddlGroup" runat="server">
                                        <asp:ListItem Value="8">Electric</asp:ListItem>
                                        <asp:ListItem Value="9">Mekanik</asp:ListItem>
                                        <asp:ListItem Value="2">Bahan Pembantu</asp:ListItem>
                                        <asp:ListItem Value="7">Marketing</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
                            <div id="lst" runat="server" class="contentlist" style="height:435px">
                                <table style="width:100%; font-size:x-small; border-collapse:collapse" border="0">
                                    <tr class="tbHeader baris">
                                        <th class="kotak" style="width:3%">No</th>
                                        <th class="kotak" style="width:6%">Tanggal</th>
                                        <th class="kotak" style="width:8%">ItemCode</th>
                                        <th class="kotak" style="width:35%">ItemName</th>
                                        <th class="kotak" style="width:5%">MinStock</th>
                                        <th class="kotak" style="width:5%">MaxStock</th>
                                        <th class="kotak" style="width:5%">SaldoAwal</th>
                                        <th class="kotak" style="width:5%">Transaksi</th>
                                        <th class="kotak" style="width:5%">SaldoAkhir</th>
                                        <th class="kotak" style="width:4%">< Min</th>
                                        <th class="kotak" style="width:4%">> Max</th>
                                    </tr>
                                    <tbody>
                                        <asp:Repeater ID="lstMinMax" runat="server">
                                            <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                <td class="kotak tengah xx"><%# Eval("ItemCode") %></td>
                                                <td class="kotak"><%# Eval("ItemName") %></td>
                                                <td class="kotak angka"><%# Eval("MinStock","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("MaxStock","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("SaldoAwal","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("Transaksi","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("SaldoAkhir","{0:N0}") %></td>
                                                <td class="kotak tengah"><%# Eval("inMin","{0:N0}") %></td>
                                                <td class="kotak tengah"><%# Eval("inMax","{0:N0}") %></td>
                                            </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                            <tr class="OddRows baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                <td class="kotak tengah xx"><%# Eval("ItemCode") %></td>
                                                <td class="kotak"><%# Eval("ItemName") %></td>
                                                <td class="kotak angka"><%# Eval("MinStock","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("MaxStock","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("SaldoAwal","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("Transaksi","{0:N0}") %></td>
                                                <td class="kotak angka"><%# Eval("SaldoAkhir","{0:N0}") %></td>
                                                <td class="kotak tengah"><%# Eval("inMin","{0:N0}") %></td>
                                                <td class="kotak tengah"><%# Eval("inMax","{0:N0}") %></td>
                                            </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
             </table>
        </div>
     </ContentTemplate>
  </asp:UpdatePanel>    
</asp:Content>
