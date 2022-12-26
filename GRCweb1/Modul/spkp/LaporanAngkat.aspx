<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LaporanAngkat.aspx.cs" Inherits="GRCweb1.Modul.spkp.LaporanAngkat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table style="border-collapse: collapse; width: 100%; height: 100%">
                    <tr>
                        <td>
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">Laporan WIP Siap Potong</td>
                                    <td style="width: 5px">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="Table4" style="width: 100%; border-collapse: collapse; font-size:x-small" border="0">
                                <tr>
                                    <td >Tanggal :</td>
                                    <td >
                                        <asp:TextBox ID="txtTgljemur" runat="server"></asp:TextBox></td>
                                    <td >&nbsp;</td>
                                    <td >
                                        <cc1:CalendarExtender ID="exCal" runat="server" TargetControlID="txtTglJemur" Format="dd-MM-yyyy"></cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Part No</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtPartNo" runat="server" CssClass="txtUpper"></asp:TextBox></td>
                                    <td>
                                        <cc1:AutoCompleteExtender ID="autoExtender" TargetControlID="txtPartno" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionInterval="100" CompletionSetCount="10" MinimumPrefixLength="3" ServiceMethod="GetNoProdukBM"
                                            ServicePath="~/Modul/Purchasing/AutoComplete.asmx" runat="server">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp</td>
                                    <td colspan="3">
                                        <asp:Button ID="btnPreview" runat="server" Text="Preview"
                                            OnClick="btnPreview_Click" />&nbsp<asp:Button ID="btnPrint" runat="server"
                                                Text="Export To Excel" Enabled="false" OnClick="btnPrint_Click" />
                                        &nbsp;&nbsp;
                                                <asp:RadioButton GroupName="sts" ID="lstRekap" runat="server" Text="View Rekap" Checked="true" AutoPostBack="true" OnCheckedChanged="lstRekap_Checked" />
                                        <asp:RadioButton GroupName="sts" ID="lstDetail" runat="server" Text="View Detail" AutoPostBack="true" OnCheckedChanged="lstDetail_Checked" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <div >
                            <asp:Label ID="lst" runat="server"></asp:Label>
                            <table id="lstR" style="border-collapse: collapse; width: 100%; font-size: x-small">
                                <thead>
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width: 5%">No</th>
                                        <th class="kotak" style="width: 15%">Part Number</th>
                                        <th class="kotak" style="width: 10%">Tgl. Produksi</th>
                                        <th class="kotak" style="width: 5%">No. Palet</th>
                                        <th class="kotak" style="width: 5%">Rak No.</th>
                                        <th class="kotak" style="width: 10%">Tgl Jemur</th>
                                        <th class="kotak" style="width: 10%">Quantity</th>
                                        <th class="kotak" style="width: 5%">&nbsp</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstPartNo" runat="server" OnItemDataBound="lstPartNo_DataBound">
                                        <ItemTemplate>
                                            <tr class="baris total">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak" colspan="5"><%# Eval("PartNo") %></td>
                                                <td class="kotak angka"><%# Eval("Total","{0:N2}") %></td>
                                                <td class="kotak">&nbsp;</td>
                                            </tr>

                                            <asp:Repeater ID="lstAngkat" runat="server">
                                                <ItemTemplate>
                                                    <tr class="baris EvenRows" id='r_<%#Eval("PartNo")%>'>
                                                        <td class="kotak angka" colspan="2"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak tengah"><%# Eval("TglProduksi","{0:d}") %></td>
                                                        <td class="kotak"><%# Eval("PaletNo") %></td>
                                                        <td class="kotak"><%# Eval("Rak") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglJemur","{0:d}") %></td>
                                                        <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                        <td class="kotak">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="baris OddRows" id='r_<%# Eval("PartNo") %>'>
                                                        <td class="kotak angka" colspan="2"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak tengah"><%# Eval("TglProduksi","{0:d}") %></td>
                                                        <td class="kotak"><%# Eval("PaletNo") %></td>
                                                        <td class="kotak"><%# Eval("Rak") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglJemur","{0:d}") %></td>
                                                        <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                        <td class="kotak">&nbsp;</td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                </tfoot>
                            </table>

                        </div>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
