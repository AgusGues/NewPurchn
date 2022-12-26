<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostingCOGS.aspx.cs" Inherits="GRCweb1.Modul.Factory.PostingCOGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader" style="width: 622px">
                                    <tr>
                                        <td style="width: 97%">
                                            <strong>&nbsp;POSTING COGS</strong></td>
                                        <td style="width: 103%"></td>
                                        <td style="width: 37px">&nbsp;</td>
                                        <td style="width: 75px">&nbsp;</td>
                                        <td style="width: 5px">&nbsp;</td>
                                        <td style="width: 70px">&nbsp;</td>
                                        <td style="width: 70px">&nbsp;</td>
                                        <td style="width: 70px">&nbsp;</td>
                                        <td style="width: 70px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td height="100%" style="width: 100%">
                                <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                    cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 176px; height: 19px">&nbsp; Tahun&nbsp;</td>
                                        <td style="width: 169px; height: 19px">
                                            <asp:TextBox ID="txtTahun" runat="server" Width="98px" AutoPostBack="True"
                                                OnTextChanged="txtTahun_TextChanged"></asp:TextBox>
                                        </td>
                                        <td style="font-size: x-small; color: #0000FF; font-style: italic;" colspan="2"
                                            rowspan="2">*) Keterangan :<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp; Format file excel harus sama dengan format List Biaya</td>
                                        <td style="width: 205px; height: 19px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 176px; height: 19px">&nbsp; Bulan&nbsp;</td>
                                        <td style="width: 169px; height: 19px">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                                <asp:ListItem>Pilih Bulan</asp:ListItem>
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
                                        </td>
                                        <td style="width: 205px; height: 19px"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 176px; height: 19px">&nbsp; Source Data Biaya&nbsp;(Excel)</td>
                                        <td style="height: 19px" colspan="3">
                                            <strong>
                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="562px" />
                                            </strong>
                                        </td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 176px; height: 19px">&nbsp;</td>
                                        <td align="center">
                                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click"
                                                Text="Upload File" />
                                        </td>
                                        <td style="width: 169px; height: 19px">&nbsp;</td>
                                        <td align="right">
                                            <input id="btnPosting" runat="server" onserverclick="btnUpdate_ServerClick"
                                                style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 120px;"
                                                type="button" value="Posting" /></td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#669999" colspan="4"
                                            style="height: 19px; font-weight: bold; color: #FFFFFF;">&nbsp; List Biaya</td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="height: 19px">
                                            <asp:Panel ID="Panel3" runat="server" BackColor="#CCFFCC" Height="350px"
                                                ScrollBars="Vertical">
                                                <asp:GridView ID="GridViewBiaya" runat="server" AutoGenerateColumns="False"
                                                    PageSize="22" Width="100%">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                        ForeColor="Gold" />
                                                    <Columns>
                                                        <asp:BoundField DataField="coa" HeaderText="COA" />
                                                        <asp:BoundField DataField="AccName" HeaderText="AccName" />
                                                        <asp:BoundField DataField="biaya" HeaderText="Biaya" />
                                                    </Columns>
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="font-weight: bold; font-size: x-small;">&nbsp; Total Biaya : &nbsp;<asp:Label ID="LblBiaya" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
