<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAsset_New.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapAsset_New" %>
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
        label,td,span{font-size:12px;}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%;">
                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr class="nbTableHeader" style="height:49px">
                                    <td style="width:50%; padding-left:10px">
                                        <strong>REKAP ASSET</strong>
                                    </td>
                                    <td style="width:50%; padding-right:10px" align="right">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                   <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <tr style="display:none">
                                            <td style="width:10%;">Periode :</td>
                                            <td style="width:25%">
                                                <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Periode :</td>
                                            <td> 2010  s/d <%= DateTime.Now.Year.ToString() %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                            </td>
                                        </tr>
                                   </table>
                                <div class="contentlist" style="height:400px" id="lst" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" rowspan="2" style="width:4%">No.</th>
                                                <th class="kotak" rowspan="2" style="width:10%">ItemCode</th>
                                                <th class="kotak" rowspan="2" style="width:35%">ItemName</th>
                                                <th class="kotak" rowspan="2" style="width:4%">Satuan</th>
                                                <th class="kotak" rowspan="2" style="width:6%">Saldo Awal</th>
                                                <th class="kotak" rowspan="2" style="width:6%">Pembelian</th>
                                                <th class="kotak" colspan="2">Adjustment</th>
                                                <%--
                                                <th style="display:none" class="kotak" colspan="2">Mutasi Antar Dept</th>
                                               --%>
                                                <th class="kotak" rowspan="2" style="width:6%">Saldo Akhir</th>
                                                <th class="kotak" rowspan="2" style="width:6%">Kategori</th>
                                               <%--
                                                <th style="display:none" class="kotak" rowspan="2" style="width:6%">SPB</th>
                                                <th style="display:none" class="kotak" rowspan="2" style="width:6%">Stock Gudang</th>
                                              --%>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:8%">IN</th>
                                                <th class="kotak" style="width:8%">OUT / RettOff</th>
                                                
                                                
                                              <%--
                                                <th style="display:none" class="kotak" style="width:8%">IN</th>
                                                <th style="display:none" class="kotak" style="width:8%">Out</th>
                                              --%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstAsset" runat="server" OnItemDataBound="lstAsset_Databound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak tengah" style="white-space:nowrap"><%# Eval("ItemCode") %></td>
                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                        <td class="kotak tengah"><%# Eval("Unit") %></td>
                                                        <td class="kotak angka"><%# Eval("SaldoAwal","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("Pembelian","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AdjustIN","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AdjustOut","{0:N0}") %></td>
                                                      <%--
                                                        <td style="display:none" class="kotak angka"><%# Eval("MutasiIN","{0:N0}") %></td>
                                                        <td style="display:none" class="kotak angka"><%# Eval("MutasiOut","{0:N0}") %></td>
                                                     --%>
                                                        <td class="kotak angka"><%# Eval("SaldoAkhir","{0:N0}") %></td>
                                                        <td class="kotak"><%# Eval("Kategori") %></td>
                                                       <%-- 
                                                        <td style="display:none" class="kotak angka"><%# Eval("SPB","{0:N0}") %></td>
                                                        <td style="display:none" class="kotak angka"><%# Eval("StockGudang","{0:N0}") %></td>
                                                      --%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="Line3 baris total" id="total" runat="server">
                                                <td colspan="4" class="kotak angka">Total</td>
                                                <td class="kotak angka 1"></td>
                                                <td class="kotak angka 2"></td>
                                                <td class="kotak angka"></td>
                                                <td class="kotak angka"></td>
                                               <%--
                                                <td style="display:none;" class="kotak angka"></td>
                                                <td style="display:none;" class="kotak angka"></td>
                                                --%>
                                             
                                                <td style="display:;" class="kotak angka"></td>
                                                <%--
                                                <td style="display:none;" class="kotak angka"></td>
                                                <td style="display:none;" class="kotak angka"></td>
                                               --%>
                                            </tr>
                                        </tfoot>
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
