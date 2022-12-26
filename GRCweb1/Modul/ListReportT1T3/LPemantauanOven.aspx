<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LPemantauanOven.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LPemantauanOven" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;PEMANTAUAN HASIL OVEN</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content">
                                <table width="100%" style="border-collapse: collapse; margin-top: 10px">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                <table style="width: 136%; font-size: x-small;">
                                                    <tr>
                                                        <td rowspan="1" style="width: 103px; height: 19px;">
                                                            &nbsp; Bulan
                                                        </td>
                                                        <td style="height: 19px">
                                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                                                                Width="132px" AutoPostBack="True">
                                                                <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
                                                                <asp:ListItem Value="01">Januari</asp:ListItem>
                                                                <asp:ListItem Value="02">Februari</asp:ListItem>
                                                                <asp:ListItem Value="03">Maret</asp:ListItem>
                                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                                <asp:ListItem Value="05">Mei</asp:ListItem>
                                                                <asp:ListItem Value="06">Juni</asp:ListItem>
                                                                <asp:ListItem Value="07">Juli</asp:ListItem>
                                                                <asp:ListItem Value="08">Agustus</asp:ListItem>
                                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                                <asp:ListItem Value="11">Nopember</asp:ListItem>
                                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="1" style="width: 103px; height: 19px;">
                                                            &nbsp; Tahun
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddTahun" runat="server" Width="132px" AutoPostBack="True" 
                                                                onselectedindexchanged="ddTahun_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 103px; height: 19px;">
                                                            &nbsp; Produk&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProduk" runat="server" Width="50%" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlProduk_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 12%">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" 
                                                OnClick="btnPreview_Click" Visible="False" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Execl" OnClick="btnExport_Click" />
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 450px" id="div2" runat="server">
                                    <table style="width: 50%; border-collapse: collapse; font-size: x-small" border="0">
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width: 15%" rowspan="2">
                                                Tgl Oven
                                            </th>
                                            <th class="kotak" style="width: 15%">
                                                Total Oven
                                            </th>
                                            <th class="kotak" style="width: 15%" rowspan="2">
                                                OK
                                            </th>
                                            <th class="kotak" style="width: 15%" rowspan="2">
                                                %
                                            </th>
                                            <th class="kotak" style="width: 15%" rowspan="2">
                                                BP
                                            </th>
                                            <th class="kotak" style="width: 10%" rowspan="2">
                                                %
                                            </th>
                                        </tr>
                                        <tbody>
                                            <asp:Repeater ID="ListProduk" runat="server" 
                                                onitemdatabound="ListProduk_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="OddRows baris" id="ps1" runat="server">
                                                        <td class="kotak tengah">
                                                            <%# Eval("tglSerah","{0:d}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("TPotong")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("OK")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("PersenOK")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("BP")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("PersenBP")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr class="Line3 baris" id="ftr" runat="server">
                                                        <td class="kotak bold tengah">
                                                            Total
                                                        </td>
                                                        <td class="kotak bold angka">
                                                            &nbsp;
                                                        </td>
                                                        <td class="kotak bold angka" >
                                                        </td>
                                                        <td class="kotak bold angka" >
                                                        </td><td class="kotak bold angka" >
                                                        </td>
                                                        <td class="kotak bold angka" >
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <br />
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
