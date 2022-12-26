<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PantauAssetKomponen.aspx.cs" Inherits="GRCweb1.Modul.Asset_Management.PantauAssetKomponen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width - 1)) + '%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + '%';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '0';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width)) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

    </script>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr valign:"top">
                        <td style="height: 49px">
                            <table class="nbTableHeader" style="font-weight: bold">
                                <tr>
                                    <td style="width: 100%; font-family: 'Courier New', Courier, monospace; font-size: medium;
                                        color: #CCCCCC;">
                                        &nbsp; PEMANTAUAN ASSET BERKOMPONEN
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign:"top">
                        <td style="width: 100%">
                            <div class="content">
                                <asp:Panel ID="PanelUtama" runat="server" Visible="true">
                                    <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                        <tr style="width: 30%">
                                            <td style="width: 14%">
                                                <asp:Label ID="LabelAsset" runat="server" Visible="true" Style="font-family: Calibri;
                                                    font-size: x-small; font-weight: bold">&nbsp; Asset Utama</asp:Label>
                                            </td>
                                            <td style="width: 30%" colspan="2">
                                                <asp:DropDownList ID="ddlAsset" runat="server" Visible="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlAsset_SelectedIndexChanged" Height="16px" Style="font-family: Calibri;
                                                    font-size: x-small" Width="90%">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 7%">
                                                <asp:Button ID="btn" runat="server" Text="Lihat" OnClick="btn_ServerClick" Style="font-family: Calibri;
                                                    font-size: x-small; font-weight: 700" Width="87%" />
                                            </td>
                                            <td style="width: 7%">
                                                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_ServerClick"
                                                    Style="font-family: Calibri; font-size: x-small; font-weight: 700" Width="87%" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <table ID="noted" runat="server" style="width: 100%; font-size: x-small; border-collapse: collapse;" visible="false">
                                    <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                            <asp:Label ID="Label1" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp; Catatan :</asp:Label>
                                        </td>
                                        <td style="width: 1%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label5" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small;">&nbsp;</asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                            <asp:Label ID="Label2" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">&nbsp; 1. Kolom [Adj Out]</asp:Label>
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label6" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">Pengurangan dari sisa stok barang</asp:Label>
                                        </td>
                                    </tr>
                                     <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                           
                                        </td>
                                        <td style="width: 1%">
                                            
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label10" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic;">( Mempengaruhi Lap. Bulanan Asset Komponen )</asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                            <asp:Label ID="Label3" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">&nbsp; 2. Kolom [Adj Out2]</asp:Label>
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label7" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">Pengurangan dari SPB</asp:Label>
                                        </td>
                                    </tr>
                                     <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                           
                                        </td>
                                        <td style="width: 1%">
                                            
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label9" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic;">( Tidak Mempengaruhi Lap. Bulanan Asset Komponen )</asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="width: 30%">
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 46%">
                                        </td>
                                        <td style="width: 12%">
                                            <asp:Label ID="Label4" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">&nbsp; 3. Kolom [Sisa]</asp:Label>
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td style="width: 61%">
                                            <asp:Label ID="Label8" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; ">(Receipt + AdjustIN) - AdjustOut - Pakai </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="PanelGrid" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 410px">
                                        <div id="DivRoot" align="left">
                                            <div id="lst" runat="server">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <table style="width: 150%; border-collapse: collapse; font-size: x-small; display: block"
                                                        border="0">
                                                        <thead>
                                                            <tr class=" tbHeader baris">
                                                                <th class="kotak" style="width: 6%">
                                                                    Tgl SPP / Adjust
                                                                </th>
                                                                <th class="kotak" style="width: 5%">
                                                                    No. SPP / Adjust
                                                                </th>
                                                                <th class="kotak" style="width: 7%">
                                                                    Kode Asset
                                                                </th>
                                                                <th class="kotak" style="width: 20%">
                                                                    Nama Asset
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Qty SPP
                                                                </th>
                                                                <th class="kotak" style="width: 5%">
                                                                    No. PO
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Qty PO
                                                                </th>
                                                                <th class="kotak" style="width: 5%">
                                                                    No. Receipt
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Qty Receipt
                                                                </th>
                                                               <%-- <th class="kotak" style="width: 3%">
                                                                    Qty Pakai
                                                                </th>--%>
                                                                <th class="kotak" style="width: 3%">
                                                                    Adj In
                                                                </th>
                                                                 <th class="kotak" style="width: 3%">
                                                                    Qty Pakai
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Adj Out
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Adj Out2
                                                                </th>
                                                                <th class="kotak" style="width: 3%">
                                                                    Sisa
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody style="font-family: Calibri">
                                                            <asp:Repeater ID="lstAsset" runat="server" OnItemDataBound="lstAsset_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("TglSPP") %>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("NoSPP") %>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            &nbsp;
                                                                            <%# Eval("KodeAsset") %>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            &nbsp;
                                                                            <%# Eval("NamaAsset")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtySPP", "{0:N1}")%>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            &nbsp;
                                                                            <%# Eval("NoPO")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtyPO", "{0:N1}")%>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            &nbsp;
                                                                            <%# Eval("ReceiptNo")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtyReceipt", "{0:N1}")%>
                                                                        </td>
                                                                        <%--<td class="kotak angka" align="right">
                                                                            <%# Eval("QtySPB", "{0:N1}")%>
                                                                        </td>--%>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtyAdjustIn", "{0:N1}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtySPB", "{0:N1}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtyAdjustOut", "{0:N1}")%>
                                                                        </td>
                                                                        <%--<td class="kotak angka" align="right">
                                                                            <%# Eval("QtyAdjustOut", "{0:N1}")%>
                                                                        </td>--%>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("QtyAdjustOutNonStok", "{0:N1}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Sisa", "{0:N1}")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div id="DivFooterRow" style="overflow: hidden">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
