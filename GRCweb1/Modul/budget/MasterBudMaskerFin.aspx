<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterBudMaskerFin.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.MasterBudMaskerFin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%; height:49px">
                            <table class="nbTableHeader" style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr>
                                    <td style="width:50%; padding-left:10px">
                                        <b>BUDGET FINISHING UNTUK PEMAKAIAN INVENTORY</b>
                                    </td>
                                    <td style="width:50%; padding-right:10px" align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnBaru_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small; margin-top:3px">
                                    <tr>
                                        <td style="width:5%">&nbsp;</td>
                                        <td style="width:10%">Periode</td>
                                        <td style="width:20%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlTahun_Change" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change" ></asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td><asp:Button ID="btnPrevie" runat="server" Text="Preview" Visible="true" OnClick="btnPreview_Click" /></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height:400px">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" rowspan="2" style="width:4%">No</th>
                                            <th class="kotak" rowspan="2" style="width:10%">ItemCode</th>
                                            <th class="kotak" rowspan="2" style="width:25%">ItemName</th>
                                            <th class="kotak" colspan="3">Budget</th>
                                            <th class="kotak" rowspan="2" style="width:5%"></th>
                                            <th class="transparant">&nbsp;</th>
                                        </tr>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:10%">Satuan </th>
                                            <th class="kotak" style="width:12%">Lembar</th>
                                            <th class="kotak" style="width:12%">Rupiah</th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstBudget" runat="server" OnItemDataBound="lstBudget_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        <td class="kotak" style="white-space:nowrap"><%# Eval("ItemName") %></td>
                                                        <td class="kotak tengah"><asp:TextBox ID="txtBarang" runat="server" CssClass="txtOngrid angka" Width="100%" Text='<%# Eval("Barang","{0:N2}") %>'></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ID="txtLembar" runat="server" CssClass="txtOngrid angka" Width="100%" Text='<%# Eval("Lembar","{0:N0}") %>'></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ID="txtRupiah" runat="server" CssClass="txtOngrid angka" Width="100%" Text='<%# Eval("RupiahPerBln","{0:N0}") %>'></asp:TextBox></td>
                                                        <td class="kotak tengah">
                                                            <asp:HiddenField ID="txtMcID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:HiddenField ID="txtID" runat="server" Value='<%# Eval("MaterialCCID") %>' />
                                                            <asp:ImageButton Visible="false" ID="btnEdit" runat="server" CommandArgument='<%# Eval("MaterialCCID") %>' CommandName="Edit" ImageUrl="~/images/folder_16.png" />
                                                        </td>
                                                        <th class="transparant">&nbsp;</th>
                                                    </tr>
                                                </ItemTemplate>
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
