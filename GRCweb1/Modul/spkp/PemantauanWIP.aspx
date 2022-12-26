<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanWIP.aspx.cs" Inherits="GRCweb1.Modul.spkp.PemantauanWIP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function _copyHeader() {
            $('#tbhd tbody').html('');
            $('#tbhd thead').html($('#tblst thead').html());
            if (parseInt($('#tblst').height()) > 380) {
                $('#tbhd tbody').html($('#tblst tbody').html());
            }
        }
    </script>
    <asp:UpdatePanel ID="UppdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server" style="height: 100%">
                <table class="tbStandart">
                    <tr valign="top">
                        <td style="height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">&nbsp;&nbsp;PEMANTAUAN WIP : MASA CURING DAN JEMUR</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td style="width: 100%">
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">&nbsp;</td>
                                        <td style="width: 10%">Periode</td>
                                        <td style="width: 50%">
                                            <asp:TextBox ID="txtTanggal" runat="server"></asp:TextBox></td>
                                        <td>
                                            <cc1:calendarextender id="caEx" runat="server" targetcontrolid="txtTanggal" format="dd-MMM-yyyy"></cc1:calendarextender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="0">
                                            <asp:RadioButton ID="masaCuring" runat="server" GroupName="masa" Text="Masa Curing &gt;= 15 Hari" />
                                            &nbsp;
                                        <asp:RadioButton ID="masaJemur" runat="server" GroupName="masa" Text="Masa Jemur &gt;= 10 Hari" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="0">
                                            <asp:RadioButton ID="viewRkp" runat="server" GroupName="view" Text="Rekap" />
                                            &nbsp;
                                        <asp:RadioButton ID="viewRdt" runat="server" GroupName="view" Text="Detail" />
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist">
                                    <div style="width: 100%; height: 30px; overflow: auto" id='ctndiv'>
                                        <table id='tbhd' class='tbStandart'>
                                            <thead>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                    <div id="lste" runat="server" style="height: 380px; overflow: auto">
                                        <table class='tbStandart' border="0" id='tblst' style='margin-top: -29px'>
                                            <thead>
                                                <tr class='tbHeader' style="height: 26px">
                                                    <th class="kotak" style="width: 4%">No.</th>
                                                    <th class="kotak" style="width: 15%">PartNo</th>
                                                    <th class="kotak" style="width: 10%">Tanggal</th>
                                                    <th class="kotak" style="width: 8%">Palet No</th>
                                                    <th class="kotak" style="width: 8%">Lokasi</th>
                                                    <th class="kotak" style="width: 10%">Quantity</th>
                                                    <th class="kotak" style="width: 5%">&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lst" runat="server" OnItemDataBound="lst_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak" colspan="4"><%# Eval("PartNo") %></td>
                                                            <td class="kotak angka"><%# Eval("Total","{0:N2}") %></td>
                                                            <td class="kotak">&nbsp;</td>
                                                        </tr>
                                                        <asp:Repeater ID="lstDetail" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="OddRows baris">
                                                                    <td class="kotak angka" colspan="2"><%# Container.ItemIndex +1 %>&nbsp;&nbsp;</td>
                                                                    <td class="kotak tengah"><%# Eval("TglProduksi", "{0:d}")%></td>
                                                                    <td class="kotak tengah"><%# Eval("PaletNo")%></td>
                                                                    <td class="kotak tengah"><%# Eval("Rak") %></td>
                                                                    <td class="kotak angka"><%# Eval("Total","{0:N2}") %></td>
                                                                    <td class="kotak">&nbsp;</td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
