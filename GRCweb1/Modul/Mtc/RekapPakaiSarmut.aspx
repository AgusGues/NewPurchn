<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPakaiSarmut.aspx.cs" Inherits="GRCweb1.Modul.Mtc.RekapPakaiSarmut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"  Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                                            <strong>&nbsp;REKAP PAKAI MAINTENANCE</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                                <div  style="width: 100%" class="content">
                                    <table width="100%" style="border-collapse: collapse">
                                        <tr>
                                            <td style="height: 49px">
                                                &nbsp;<asp:Label ID="LBulan" runat="server" Text="Bulan"></asp:Label>
                                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBulan" runat="server">
                                                    <%--<asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>--%>
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
                                                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;Tahun
                                                <asp:DropDownList ID="ddTahun" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RbBulan" runat="server" Checked="True" GroupName="a" 
                                                    Text="Detail per Bulan" AutoPostBack="True" 
                                                    oncheckedchanged="RbBulan_CheckedChanged" />
                                                &nbsp;<asp:RadioButton ID="RbTahun" runat="server" GroupName="a" 
                                                    Text="Rekap per Tahun" AutoPostBack="True" 
                                                    oncheckedchanged="RbTahun_CheckedChanged" />
                                                <asp:RadioButton ID="RbTahun0" runat="server" AutoPostBack="True" GroupName="a" 
                                                    oncheckedchanged="RbTahun0_CheckedChanged" Text="Chart Regular " />
                                                <asp:RadioButton ID="RbTahun1" runat="server" AutoPostBack="True" GroupName="a" 
                                                    oncheckedchanged="RbTahun1_CheckedChanged" Text="Chart Non Regular " 
                                                    Visible="False" />
                                                <asp:TextBox ID="txtSampaiTgl" runat="server" Visible="False"></asp:TextBox>
                                                <asp:TextBox ID="txtDariTgl" runat="server" Visible="False"></asp:TextBox>
                                                <asp:Label ID="txtGtotal" runat="server" Text="Bulan" Visible="False"></asp:Label>
                                            </td>
                                            <td style="height: 49px">
                                                <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                &nbsp;<asp:Button ID="btnExport" runat="server" OnClick="ExportToExcel" Text="Export to Excel" />
                                                &nbsp;<asp:Button ID="btnExportpdf" runat="server" OnClick="btnExport_Click" Text="Export to Pdf"
                                                    Visible="false" />
                                                <asp:DropDownList ID="ddlZona" runat="server" Visible="False">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlDeptName" runat="server" Visible="False">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value="4">Mekanik</asp:ListItem>
                                                    <asp:ListItem Value="5">Electrik</asp:ListItem>
                                                    <asp:ListItem Value="18">Utility</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <div id="div1" style="width: 100%; height: 340px; overflow: auto" class="contentlist">
                                            <table style="border-collapse: collapse; width: 100%; font-size: smaller">
                                                <asp:Repeater ID="lstSarmut" runat="server" OnItemDataBound="lstSarmut_DataBound">
                                                    <HeaderTemplate>
                                                        <tr class="tbHeader tengah">
                                                            <th class="kotak" style="width: 4%">
                                                                #
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                Tgl SPB
                                                            </th>
                                                            <th class="kotak" style="width: 10%">
                                                                Item Code
                                                            </th>
                                                            <th class="kotak" style="width: 20%">
                                                                Item Name
                                                            </th>
                                                            <th class="kotak" style="width: 5%">
                                                                Unit
                                                            </th>
                                                            <th class="kotak" style="width: 6%">
                                                                Jumlah
                                                            </th>
                                                            <th class="kotak" style="width: 10%">
                                                                Harga
                                                            </th>
                                                            <th class="kotak" style="width: 12%">
                                                                Total
                                                            </th>
                                                            <th class="kotak" style="width: 10%">
                                                                Zona
                                                            </th>
                                                            <th class="kotak" style="width: 12%">
                                                                Keterangan
                                                            </th>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baris EvenRows">
                                                            <td class="tengah kotak" style="font-size: medium; font-weight: bold;">
                                                                <%# Container.ItemIndex + 1 %>
                                                            </td>
                                                            <td class="kotak" colspan="3" style="font-size: medium; font-weight: bold;">
                                                                &nbsp;<%# Eval("ZonaName") %></td>
                                                            <%--<td class="kotak">&nbsp;</td>--%>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;
                                                            </td>
                                                            <asp:Repeater ID="dtlSarmutReg" runat="server" EnableViewState="true">
                                                                <ItemTemplate>
                                                                    <tr class="baris OddRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="baris EvenRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                        <tr class="Line3" style="font-size: small; font-weight: bold;">
                                                            <td class="kotak"  style="font-size: medium; font-weight: bold;">
                                                            </td>
                                                            <td colspan="6" class="kotak angka">
                                                                &nbsp;SubTotal&nbsp;<%# Eval("ZonaName") %>&nbsp;Regular
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtTotalSarmutReg" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="baris EvenRows">
                                                            <asp:Repeater ID="dtlSarmutNonReg" runat="server" EnableViewState="true">
                                                                <ItemTemplate>
                                                                    <tr class="baris OddRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="baris EvenRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                        <tr class="Line3" style="font-size: small; font-weight: bold;">
                                                         <td class="kotak"  style="font-size: medium; font-weight: bold;">
                                                            </td>
                                                            <td colspan="6" class="kotak angka">
                                                                &nbsp;SubTotal&nbsp;<%# Eval("ZonaName") %>&nbsp;NonRegular
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtTotalSarmutNonReg" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="baris EvenRows">
                                                            <asp:Repeater ID="dtlSarmutLain" runat="server" EnableViewState="true">
                                                                <ItemTemplate>
                                                                    <tr class="baris OddRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="baris EvenRows">
                                                                        <td class="kotak" style="width: 3%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 6%">
                                                                            <%#Eval("SPBDate", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak" style="width: 10%">
                                                                            <span style="color: White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                        <td class="kotak" style="width: 20%" title='<%# Eval("NoPol") %>'>
                                                                            <%#Eval("SarmutName") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 5%">
                                                                            <%#Eval("SPBID") %>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Qty", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("avgPrice", "{0:N2}")%>
                                                                        </td>
                                                                        <td class="angka kotak" style="width: 10%">
                                                                            <%#Eval("Total","{0:N2}") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 10%">
                                                                            <%#Eval("ZonaMTC") %>
                                                                        </td>
                                                                        <td class="kotak" style="width: 12%">
                                                                            <%#Eval("keterangan") %>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                        <tr class="Line3" style="font-size: small; font-weight: bold;">
                                                         <td class="kotak"  style="font-size: medium; font-weight: bold;">
                                                            </td>
                                                            <td colspan="6" class="kotak angka">
                                                                &nbsp;SubTotal&nbsp;<%# Eval("ZonaName") %>&nbsp;Lain-lain
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtTotalSarmutLain" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="Line3" style="font-size: small; font-weight: bold;">
                                                             <td class="kotak"  style="font-size: medium; font-weight: bold;">
                                                            </td>
                                                            <td colspan="6" class="kotak angka">
                                                                <%# Eval("ZonaName") %>&nbsp;Total
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtTotalSarmut" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr class="total" style="font-size: small; font-weight: bold">
                                                            <td class="kotak"  style="font-size: medium; font-weight: bold;">
                                                            </td>
                                                            <td colspan="6" class="kotak angka">
                                                                <%=ddlDeptName.SelectedItem.Text %>
                                                                Total
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtGrandTotal" runat="server"></asp:Label><%=txtGtotal.Text%>
                                                            </td>
                                                            <td class="kotak" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                                        <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" 
                                            PageSize="15" Width="100%">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Visible="False">
                                    <table>
                                    <tr>
                                    <td width="25%">
                                    <asp:Panel ID="PanelOption" runat="server" HorizontalAlign="Left" BackColor="#FFFFCC">
                                        <asp:CheckBoxList ID="ChkList" runat="server" Font-Size="XX-Small" 
                                            RepeatDirection="Vertical" RepeatLayout="Flow" AutoPostBack="True" 
                                            onselectedindexchanged="ChkList_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                    </td>
                                    <td width="75%">
                                    <asp:Panel ID="PanelChart" runat="server" HorizontalAlign="Center">
                                    <cc2:Chart ID="Chart1" runat="server" Height="400px" Width="900px" BorderlineColor="Blue"
                                            Visible="true">
                                            <Titles>
                                                <cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold">
                                                </cc2:Title>
                                            </Titles>
                                            <ChartAreas>
                                                <cc2:ChartArea Name="Area1">
                                                </cc2:ChartArea>
                                            </ChartAreas>
                                        </cc2:Chart>
                                    </asp:Panel>
                                    </td>
                                    </tr>
                                    </table>
                                    
                                        
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
