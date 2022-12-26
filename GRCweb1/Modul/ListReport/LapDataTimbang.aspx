<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapDataTimbang.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapDataTimbang" %>
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
        label{font-size:12px;}
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="div1" runat="server">
            <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;LAP REKAP RECEIPT</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <div class="content" style="background:#fff;">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <tr>
                                            <td style="width:10%">&nbsp;</td>
                                            <td style="width:10%">Periode</td>
                                            <td style="width:35%">
                                                <asp:TextBox ID="txtDrTgl" runat="server"></asp:TextBox> s/d
                                                <asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <cc1:CalendarExtender ID="Ca1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="Ca2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Supplier</td>
                                            <td><asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr><td colspan="4">&nbsp;</td></tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height:400px" id="div2" runat="server">
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                            <tr class="baris tbHeader">
                                                <th class="kotak" style="width:5%">No</th>
                                                <th class="kotak" style="width:10%">ReceiptNo</th>
                                                <th class="kotak" style="width:8%">ReceiptDate</th>
                                                <th class="kotak" style="width:10%">ItemCode</th>
                                                <th class="kotak" style="width:28%">ItemName</th>
                                                <th class="kotak" style="width:5%">Unit</th>
                                                <th class="kotak" style="width:5%">Qty Terima</th>
                                                <th class="kotak" style="width:5%">Qty BPAS</th>
                                                <th class="kotak" style="width:5%">Selisih</th>
                                                <th class="kotak" style="width:5%">Persentase</th>
                                                <th class="kotak" style="width:7%">PO No</th>
                                                <th class="kotak" style="width:20%">Keterangan</th>
                                            </tr>
                                            <tbody>
                                                <asp:Repeater ID="lstSupplier" runat="server" OnItemDataBound="lstSupplier_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="NestedRow baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak" colspan="11"><%# Eval("SupplierName") %></td>
                                                        </tr>
                                                        <asp:Repeater ID="lstTimbang" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="OddRows baris">
                                                                    <td class="kotak angka"></td>
                                                                    <td class="kotak tengah"><%# Eval("ReceiptNo") %></td>
                                                                    <td class="kotak tengah"><%# Eval("ReceiptDate","{0:d}") %></td>
                                                                    <td class="kotak tengah"><%# Eval("ItemCode") %>.</td>
                                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                                    <td class="kotak tengah"><%# Eval("Unit") %></td>
                                                                    <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                                    <td class="kotak angka"><%# Eval("QtyTimbang", "{0:N0}")%></td>
                                                                    <td class="kotak angka"><%# Eval("Selisih", "{0:N0}")%></td>
                                                                    <td class="kotak angka"><%# Eval("Persentase", "{0:N5}")%></td>
                                                                    <td class="kotak tengah"><%# Eval("PONo") %></td>
                                                                    <td class="kotak tengah"><%# Eval("keterangan") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                             <tr class="EvenRows baris">
                                                                    <td class="kotak angka"></td>
                                                                    <td class="kotak tengah"><%# Eval("ReceiptNo") %></td>
                                                                    <td class="kotak tengah"><%# Eval("ReceiptDate","{0:d}") %></td>
                                                                    <td class="kotak tengah"><%# Eval("ItemCode") %>.</td>
                                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                                    <td class="kotak tengah"><%# Eval("Unit") %></td>
                                                                    <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                                    <td class="kotak angka"><%# Eval("QtyTimbang", "{0:N0}")%></td>
                                                                    <td class="kotak angka"><%# Eval("Selisih", "{0:N0}")%></td>
                                                                    <td class="kotak angka"><%# Eval("Persentase", "{0:N5}")%></td>
                                                                    <td class="kotak tengah"><%# Eval("PONo") %></td>
                                                                    <td class="kotak tengah"><%# Eval("keterangan") %></td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
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
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
