<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterBudMaskerFin.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.MasterBudMaskerFin1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        <b>BUDGET FINISHING UNTUK PEMAKAIAN SARUNG TANGAN DAN MASKER</b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" Height="25px"
                                            OnClick="btnNew_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <input id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick"
                                            style="background-color: #CCCCCC; font-weight: bold; font-size: 11px; height: 25px;"
                                            type="button" value="Simpan" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; margin-top: 3px">
                                    <tr>
                                        <td style="width: 5%; height: 24px;"></td>
                                        <td style="width: 20%; height: 24px;">Periode</td>
                                        <td style="width: 20%; height: 24px;">
                                            <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="height: 24px"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Total Budget Sarung tangan B5</td>
                                        <td>
                                            <asp:TextBox ID="txtST" runat="server" Width="95px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Total Budget Sarung tangan B8</td>
                                        <td>
                                            <asp:TextBox ID="txtSTB8" runat="server" Width="95px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Total Budget Masker (Face Mask)&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtMS" runat="server" Width="95px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Total Budget Masker (Kain Tali)&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtMSKTL" runat="server" Width="95px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="LID" runat="server" Text="0" Visible="false"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 400px">

                                    <asp:GridView ID="GridView1" runat="server"
                                        AutoGenerateColumns="False" Width="50%"
                                        OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                            <asp:BoundField DataField="Tahun" HeaderText="Tahun" />
                                            <asp:BoundField DataField="Bulan" HeaderText="Bulan" />
                                            <asp:BoundField DataField="Sarung_Tangan" HeaderText="Qty ST B5" />
                                            <asp:BoundField DataField="B8" HeaderText="Qty ST B8" />
                                            <asp:BoundField DataField="Masker" HeaderText="Qty Masker (Face Mask)" />
                                            <asp:BoundField DataField="TL" HeaderText="Qty Masker (Kain Tali)" />
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
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
