<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapSarmut.aspx.cs" Inherits="GRCweb1.Modul.Mtc.RekapSarmut" %>
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
             <table style="table-layout: fixed" width="100%">
                 <tbody>
                     <tr>
                         <td style="width: 100%; height: 49px">
                             <table class="nbTableHeader" style="width: 100%">
                                 <tr>
                                     <td style="width: 100%">
                                         <strong>&nbsp;REKAP SARMUT MAINTENANCE</strong>
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                     <tr>
                         <td height="100%" style="width: 100%">
                             <div style="width: 100%" class="content">
                                 <table width="100%" style="border-collapse: collapse">
                                     <tr>
                                         <td colspan="4">
                                             &nbsp;
                                         </td>
                                     </tr>
                                     <tr>
                                         <td width="15%">
                                             &nbsp;&nbsp;Dari Tanggal
                                         </td>
                                         <td width="35%">
                                             <asp:TextBox ID="txtDariTgl" runat="server"></asp:TextBox>
                                         </td>
                                         <td width="20%">
                                         <cc1:CalendarExtender runat="server" ID="txtDariCE" TargetControlID="txtDariTgl" Format="dd-MMM-yyyy">
                                         </cc1:CalendarExtender>
                                         </td>
                                         <td width="30%">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td>
                                             &nbsp;&nbsp;Sampai Tanggal
                                         </td>
                                         <td>
                                             <asp:TextBox ID="txtSampaiTgl" runat="server"></asp:TextBox>
                                         </td>
                                         <cc1:CalendarExtender runat="server" ID="txtSampaiCE" TargetControlID="txtSampaiTgl" Format="dd-MMM-yyyy">
                                         </cc1:CalendarExtender>
                                         <td>
                                         </td>
                                         <td>
                                         </td>
                                     </tr>
                                     <tr>
                                        <td >&nbsp;&nbsp;Departemen</td>
                                        <td colspan="3"><asp:DropDownList ID="ddlDeptName" runat="server">
                                            <asp:ListItem Value="">All</asp:ListItem>
                                            <asp:ListItem Value="4">Mekanik</asp:ListItem>
                                            <asp:ListItem Value="5">Electrik</asp:ListItem>
                                            <asp:ListItem Value="18">Utility</asp:ListItem>
                                        </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                        <td>&nbsp;&nbsp;Zona</td>
                                        <td colspan="3"><asp:DropDownList ID="ddlZona" runat="server">
                                            
                                        </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="3">&nbsp;</td>
                                     </tr>
                                     <tr>
                                         <td>
                                             &nbsp;
                                         </td>
                                         <td align="left">
                                             <asp:Button ID="btnPreview" runat="server" Text="Preview" onclick="btnPreview_Click" />&nbsp;
                                             <asp:Button ID="btnExport" runat="server" Text="Export to Excel" onclick="ExportToExcel" />
                                             <asp:Button ID="btnExportpdf" runat="server" Text="Export to Pdf" onclick="btnExport_Click" Visible="false" />
                                         </td>
                                         <td>
                                         </td>
                                         <td>
                                         </td>
                                     </tr>
                                                                          
                                 </table>
                                 <div style="width: 100%; height: 340px; overflow: auto" class="contentlist">
                                <table style="border-collapse:collapse; width:100%; font-size:smaller">
                                <asp:Repeater ID="lstSarmut" runat="server" OnItemDataBound="lstSarmut_DataBound">
                                 <HeaderTemplate>
                                        <tr class="tbHeader tengah">
                                            <th class="kotak" style="width:4%">#</th>
                                            <th class="kotak" style="width:8%">Tgl SPB</th>
                                            <th class="kotak" style="width:10%">Item Code</th>
                                            <th class="kotak" style="width:20%">Item Name</th>
                                            <th class="kotak" style="width:5%">Unit</th>
                                            <th class="kotak" style="width:6%">Jumlah</th>
                                            <th class="kotak" style="width:10%">Harga</th>
                                            <th class="kotak" style="width:12%">Total</th>
                                            <th class="kotak" style="width:10%">Zona</th>
                                            <th class="kotak" style="width:12%">Keterangan</th>
                                        </tr>
                                    </HeaderTemplate>
                                     <ItemTemplate>
                                                <tr class="baris EvenRows">
                                                    <td class="tengah kotak"><%# Container.ItemIndex + 1 %></td>
                                                    <td class="kotak" colspan="3" style="font-size:medium; font-weight:bold;">&nbsp;<%# Eval("ZonaName") %></td>
                                                    <%--<td class="kotak">&nbsp;</td>--%>
                                                    <td class="kotak">&nbsp;</td>
                                                    <td class="kotak">&nbsp;</td>
                                                    <td class="kotak">&nbsp;</td>
                                                    <td class="kotak">&nbsp;</td>
                                                    <td class="kotak">&nbsp;</td>
                                                    <td class="kotak">&nbsp;</td>
                                                    <asp:Repeater ID="dtlSarmut" runat="server" EnableViewState="true">
                                                        <ItemTemplate>
                                                            <tr class="baris OddRows">
                                                                <td class="kotak" style="width:3%">&nbsp;</td>
                                                                <td class="kotak tengah" style="width:6%"><%#Eval("SPBDate", "{0:d}")%></td>
                                                                <td class="kotak" style="width:10%"><span style="color:White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                <td class="kotak" style="width:20%" title='<%# Eval("NoPol") %>'><%#Eval("SarmutName") %></td>
                                                                <td class="kotak" style="width:5%"><%#Eval("SPBID") %></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("Qty", "{0:N2}")%></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("avgPrice", "{0:N2}")%></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("Total","{0:N2}") %></td>
                                                                <td class="kotak tengah" style="width:10%"><%#Eval("ZonaMTC") %></td>
                                                                <td class="kotak" style="width:12%"><%#Eval("keterangan") %></td>
                                                            </tr>
                                                     </ItemTemplate>
                                                     <AlternatingItemTemplate>
                                                        <tr class="baris EvenRows">
                                                                <td class="kotak" style="width:3%">&nbsp;</td>
                                                                <td class="kotak tengah" style="width:6%"><%#Eval("SPBDate", "{0:d}")%></td>
                                                                <td class="kotak" style="width:10%"><span style="color:White">'</span><%# string.Format(Eval("SarmutCode").ToString()) %></td>
                                                                <td class="kotak" style="width:20%" title='<%# Eval("NoPol") %>'><%#Eval("SarmutName") %></td>
                                                                <td class="kotak" style="width:5%"><%#Eval("SPBID") %></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("Qty", "{0:N2}")%></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("avgPrice", "{0:N2}")%></td>
                                                                <td class="angka kotak" style="width:10%"><%#Eval("Total","{0:N2}") %></td>
                                                                <td class="kotak tengah" style="width:10%"><%#Eval("ZonaMTC") %></td>
                                                                <td class="kotak" style="width:12%"><%#Eval("keterangan") %></td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                                <tr class="Line3" style="font-size:small; font-weight:bold;">
                                                    <td colspan="7" class="kotak angka">&nbsp;<%# Eval("ZonaName") %>Total</td>
                                                    <td class="kotak angka"><asp:Label ID="txtTotalSarmut" runat="server"></asp:Label></td>
                                                    <td class="kotak" colspan="2">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                   <FooterTemplate>
                                    <tr class="total" style="font-size:small; font-weight:bold">
                                        <td colspan="7" class="kotak angka"><%=ddlDeptName.SelectedItem.Text %> Total</td>
                                        <td class="kotak angka"><asp:Label ID="txtGrandTotal" runat="server"></asp:Label><%=GrandTotal.ToString("###,##0.#0")%></td>
                                        <td class="kotak" colspan="2">&nbsp;</td>
                                    </tr>
                                   </FooterTemplate>
                                   </asp:Repeater>
                                </table>
                                 </div>
                             </div>
                         </td>
                     </tr>
                 </tbody>
             </table>
         </div>
</asp:Content>
