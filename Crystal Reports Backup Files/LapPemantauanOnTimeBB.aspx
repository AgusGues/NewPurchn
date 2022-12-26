<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantauanOnTimeBB.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantauanOnTimeBB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;LAPORAN ON TIME PEMBELIAN BAHAN BAKU DAN PENUNJANG</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                             <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Periode
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDrTgl" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtSdTgl" runat="server" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlTahun" runat="server" Visible="true">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="onlyParDe" runat="server" Text="Only Parsial Delivery Material" />
                                            </td>
                                            <td>
                                            
                                                <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy"
                                                    EnableViewState="true">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy"
                                                    EnableViewState="true">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="preview" runat="server" Text="Preview" OnClick="preview_Click" />
                                                <asp:Button ID="ExportXls" runat="server" Text="Export to Excel" OnClick="ExportToExcel" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <%--table style="width:100%; margin-top:-6px; margin-bottom:-2px; margin-right:1px; margin-left:1px; border-collapse:collapse">
                                        <tr style="height:30px">
                                            <td style="width:15%; background-image:url(Box.png) repeat-x;"  align="center">List Data</td>
                                            <td class="JudulListRight"></td>
                                            <td style="background-color:White; border-right:3px solid #B0C4DE; width:100%"></td>
                                        </tr>
                                    </table>--%>
                                    
                                    <div class="contentlist" style="height:430px;" id="lst" runat="server">
                                    <table style="width:150%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="tbHeader baris">
                                                <th class="kotak" rowspan="2" style="width:3%">No.</th>
                                                <th class="kotak" rowspan="2" style="width:5%">Tanggal</th>
                                                <th class="kotak" rowspan="2" style="width:10%">Supplier</th>
                                                <th class="kotak" rowspan="2" style="width:12%">ItemName</th>
                                                <th class="kotak" colspan="4">Schedule Kedatangan</th>
                                                <th class="kotak" colspan="3">Actual Kedatangan</th>
                                                <th class="kotak" colspan="2">On Time</th>
                                                <th class="kotak" colspan="2">Kesesuaian</th>
                                                <th class="kotak" rowspan="2" style="width:12%">Keterangan</th>
                                               
                                            </tr>
                                            <tr class="tbHeader baris">
                                                <th class="kotak" style="width:5%">No.SPP</th>
                                                <th class="kotak" style="width:5%">No.PO</th>
                                                <th class="kotak" style="width:5%">Tanggal</th>
                                                <th class="kotak" style="width:5%">Qty</th>
                                                <th class="kotak" style="width:5%">No.RMS</th>
                                                <th class="kotak" style="width:5%">Tanggal</th>
                                                <th class="kotak" style="width:5%">Qty</th>
                                                <th class="kotak" style="width:3%">Ok</th>
                                                <th class="kotak" style="width:5%">Tidak</th>
                                                <th class="kotak" style="width:5%">Sesuai</th>
                                                <th class="kotak" style="width:5%">Tidak</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstRepeater" runat="server" OnItemDataBound="lstRepeater_DataBound" OnItemCommand="lst_Repeater_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="trr" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex + 1%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("SchDates","{0:d}")%></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;"><%# Eval("SupplierName")%></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;"><%# Eval("ItemName")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("NoSPP")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("NoPO")%></td>
                                                        <%--<td class="kotak tengah" nowrap="nowrap"><%# Eval("ScheduleNo")%></td>--%>
                                                        <td class="kotak tengah"><%# Eval("SchDates","{0:d}")%></td>
                                                        <td class="kotak angka"><%# Eval("QtySCH", "{0:N2}")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("ReceiptNo")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("TglReceipt")%></td>
                                                        <td class="kotak angka"><%# Eval("Quantity", "{0:N2}")%></td>
                                                        <td class="kotak tengah"><asp:Label ID="ok" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="no" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label1" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label2" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;">
                                                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                                                <tr>
                                                                    <td style="width:90%; white-space:nowrap; overflow:hidden;"><asp:Label ID="lblKet" runat="server" Text='<%# Eval("Keterangan") %>'></asp:Label></td>
                                                                    <td style="width:10%">
                                                                        <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="EvenRows baris" id="trr" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex + 1%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("SchDates","{0:d}")%></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;"><%# Eval("SupplierName")%></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;"><%# Eval("ItemName")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("NoSPP")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("NoPO")%></td>
                                                        <%--<td class="kotak tengah" nowrap="nowrap"><%# Eval("ScheduleNo")%></td>--%>
                                                        <td class="kotak tengah"><%# Eval("SchDates","{0:d}")%></td>
                                                        <td class="kotak angka"><%# Eval("QtySCH", "{0:N2}")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("ReceiptNo")%></td>
                                                        <td class="kotak tengah" style="white-space:nowrap; overflow:hidden;"><%# Eval("TglReceipt")%></td>
                                                        <td class="kotak angka"><%# Eval("Quantity", "{0:N2}")%></td>
                                                        <td class="kotak tengah"><asp:Label ID="ok" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="no" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label1" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label2" runat="server" Text="X"></asp:Label></td>
                                                        <td class="kotak" style="white-space:nowrap; overflow:hidden;">
                                                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                                                <tr>
                                                                    <td style="width:90%; white-space:nowrap; overflow:hidden;"><asp:Label ID="lblKet" runat="server" Text='<%# Eval("Keterangan") %>'></asp:Label></td>
                                                                    <td style="width:10%">
                                                                        <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    <tr class="total" id="trfoot" runat="server">
                                                        <td colspan="11" class="kotak"> &nbsp;</td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                    </tr>
                                                    <tr class="line" id="tr1" runat="server" style="height:50px">
                                                        <td colspan="11" class="kotak"> &nbsp;</td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                        <td class="kotak tengah"></td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
</asp:Content>
