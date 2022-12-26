<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HargaKertas.aspx.cs" Inherits="GRCweb1.Modul.Master.FrmHargaKertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed; width: 103%;" height="100%" cellspacing="0" cellpadding="0">
                    <tbody>

                        <tr>
                            <td>
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 216%">
                                            <strong>INPUT HARGA KERTAS</strong></td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 55px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" /></td>
                                        <td>
                                            <input id="btnHapus" runat="server" onserverclick="btnHapus_ServerClick"
                                                style="background-color: white; font-weight: bold; font-size: 11px; width: 55px;"
                                                type="button" value="Hapus" /></td>
                                        <td style="width: 50px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Height="16px" Width="115px">
                                                <asp:ListItem Value="SupplierName">Supplier Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5px">
                                            <asp:TextBox ID="txtCari" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                Width="181px"></asp:TextBox>
                                        </td>
                                        <td style="width: 10px">
                                            <input id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick"
                                                style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" /></td>

                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="width: 3px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
                    <tbody>
                        <tr class="treeRow1" valign="top">
                            <td>
                                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" border="1" height="100%" width="100%">
                                    <tr style="width: 100%; height: 100%">
                                        <td style="width: 100%; height: 100%;">
                                            <table id="TblIsi" cellspacing="0" cellpadding="0" border="0" style="width: 97%">
                                                <tr>
                                                    <td style="width: 116px; height: 6px">&nbsp;Company</td>
                                                    <td style="width: 16px; height: 19px" valign="top">
                                                        <asp:DropDownList ID="ddlSubCompany"
                                                            runat="server" Width="150px" Enabled="True" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlSubCompany_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="BPAS"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="CGI"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="KAT"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="SBR"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="NPS"></asp:ListItem>
                                                            <asp:ListItem Value="100" Text="CPD"></asp:ListItem>

                                                        </asp:DropDownList></td>
                                                    <td style="width: 72px; height: 6px" valign="top">&nbsp;</td>
                                                    <td colspan="2" style="height: 19px" valign="top">&nbsp;</td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 6px">
                                                        <span style="font-size: 10pt">&nbsp; Cari Supplier</span></td>
                                                    <td style="height: 19px; width: 16px;" valign="top">
                                                        <asp:TextBox ID="txtCariSupplier" runat="server" AutoPostBack="True"
                                                            BorderStyle="Groove" OnTextChanged="txtCariSupplier_TextChanged" Width="233"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 72px; height: 6px" valign="top">&nbsp;</td>
                                                    <td style="height: 19px" valign="top" colspan="2">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 6px;">
                                                        <span style="font-size: 10pt">&nbsp; Nama Supplier</span></td>
                                                    <td colspan="4" style="height: 19px" valign="top">
                                                        <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True"
                                                            Height="16px" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"
                                                            Width="506px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 18px; font-size: x-small;">&nbsp;&nbsp;Cari Nama Brg</td>
                                                    <td colspan="4" style="height: 18px" valign="top">
                                                        <asp:TextBox ID="txtCariNamaBrg" runat="server" AutoPostBack="True"
                                                            BorderStyle="Groove" OnTextChanged="txtCariNamaBrg_TextChanged"
                                                            Style="height: 22px" Width="232px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 6px">
                                                        <span style="font-size: 10pt">&nbsp; Nama Barang</span></td>
                                                    <td colspan="4" style="height: 19px" valign="top">
                                                        <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="True"
                                                            Height="21px" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"
                                                            Width="507px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 6px">
                                                        <span style="font-size: 10pt">&nbsp; Harga</span></td>
                                                    <td style="width: 16px; height: 19px" valign="top">
                                                        <asp:TextBox ID="txtHarga" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                            Width="211px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 72px; height: 6px" valign="top">
                                                        <span style="font-size: 10pt">&nbsp;&nbsp;Price List</span></td>
                                                    <td style="width: 152px; height: 19px" valign="top">
                                                        <asp:TextBox ID="txtPriceList" runat="server" BorderStyle="Groove"
                                                            ReadOnly="False" Width="211px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 212px; height: 19px" valign="top">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px; height: 6px">
                                                        <span style="font-size: 10pt">&nbsp; Kadar Air</span></td>
                                                    <td rowspan="1" style="width: 16px; height: 19px">
                                                        <asp:TextBox ID="txtKadarAir" runat="server" BorderStyle="Groove"
                                                            ReadOnly="False" Width="211px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 72px; height: 6px" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">Min Price</span></td>
                                                    <td style="width: 152px; height: 19px" valign="top">
                                                        <asp:TextBox ID="txtMinPrice" runat="server" BorderStyle="Groove"
                                                            ReadOnly="False" Width="211px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 212px; height: 19px" valign="top">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px;">&nbsp;</td>
                                                    <td style="width: 16px;" valign="top">&nbsp;</td>
                                                    <td align="left" style="width: 72px;">
                                                        <span style="font-size: 10pt" visible="false">&nbsp;&nbsp;Add Price</span></td>
                                                    <td style="width: 152px;" valign="top">
                                                        <asp:TextBox ID="txtAddPrice" runat="server" BorderStyle="Groove"
                                                            ReadOnly="False" Visible="False" Width="211px"></asp:TextBox>
                                                        <td align="right" style="width: 212px;" valign="top"></td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 116px;">&nbsp;</td>
                                                    <td style="width: 16px;" valign="top">&nbsp;</td>
                                                    <td style="width: 72px; height: 6px" valign="top">
                                                        <span style="font-size: 10pt">&nbsp;&nbsp;Price Beli</span></td>
                                                    <td style="width: 152px;" valign="top">
                                                        <asp:TextBox ID="txtPriceBeli" runat="server" BorderStyle="Groove"
                                                            ReadOnly="False" Width="211px"></asp:TextBox>
                                                        <td align="right" style="width: 212px;" valign="top">&nbsp;</td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="height: 6px">&nbsp;<span style="font-size: 10pt">&nbsp; <strong>List</strong></span>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="height: 6px">
                                                        <div id="div2" style="width: 784px; height: 320px; overflow: auto">
                                                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                                                                AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                                OnRowCommand="GridView1_RowCommand" PageSize="20" Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" />
                                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                    <asp:BoundField DataField="Harga" HeaderText="Harga" />
                                                                    <asp:BoundField DataField="PriceBeli" HeaderText="Price Beli" />
                                                                    <asp:BoundField DataField="PriceList" HeaderText="Price List" />
                                                                    <asp:BoundField DataField="MinPrice" HeaderText="Min Price" />
                                                                    <asp:BoundField DataField="KadarAir" HeaderText="Kadar Air" />
                                                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                </Columns>
                                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                                    BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                                    ForeColor="Gold" />
                                                                <PagerStyle BorderStyle="Solid" />
                                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
