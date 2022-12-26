<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BudgetingDelivery.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.BudgetingDelivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
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
            <div class="table-responsive">
                <div class="col-xs-12">
                    <div id="Div1" runat="server">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <tr valign="top">
                                <td style="height: 49px">
                                    <table class="nbTableHeader" style="font-weight: bold">
                                        <tr>
                                            <td style="width: 100%; font-family: 'Courier New', Courier, monospace; font-size: medium; color: #CCCCCC;">&nbsp;
                                        PEMANTAUAN BUDGETING DEPARTEMEN TRANSPORTASI
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <div class="content">
                                        <asp:Panel ID="PanelUtama" runat="server" Visible="true">
                                            <table style="width: 80%; font-size: x-small; border-collapse: collapse">
                                                <tr style="width: 30%">
                                                    <td style="font-family: Calibri; width: 74%;">&nbsp;
                                                    <asp:Label ID="LabelStatus" runat="server" Visible="false" Width="100%"
                                                        Font-Bold="True"></asp:Label>
                                                    </td>

                                                    <td style="width: 65%" rowspan="3">
                                                        <asp:Panel Style="width: 100%" ID="PanelKeterangan" runat="server" Visible="false"
                                                            Width="332px">
                                                            <table style="width: 100%" bgcolor="#0099CC">
                                                                <tr style="width: 100%">
                                                                    <td style="width: 60%">
                                                                        <asp:Label ID="LabelTotalStd" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: 700"
                                                                            Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%">: 
                                                                <asp:Label ID="txtTotalStd" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 60%">
                                                                        <asp:Label ID="LabelTotalPakai" runat="server" Visible="false" Style="font-family: Calibri; font-weight: 700; font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%">:
                                                                <asp:Label ID="txtTotalPakai" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 60%">
                                                                        <asp:Label ID="LabelTotalPersen" runat="server" Visible="false" Style="font-family: Calibri; font-weight: 700; font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 40%">:
                                                                <asp:Label ID="txtTotalPersen" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr style="width: 30%">
                                                    <td style="width: 74%">&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB1_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                                    Text="Bulanan" TextAlign="Left" Width="100px" />
                                                        <asp:RadioButton ID="RB2" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB2_CheckedChanged"
                                                            Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                                            Text="Tahunan" TextAlign="Left" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                        </asp:Panel>
                                        <asp:Panel ID="PanelPeriodeBulan" runat="server" Visible="false" Width="779px">
                                            <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                <tr>
                                                    <%--<td style="width:10%">&nbsp;</td>--%>
                                                    <td style="width: 40%; font-family: Calibri; font-weight: 700;">&nbsp;
                                                Pilih Periode :
                                                <asp:DropDownList AutoPostBack="True" ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_SelectedChange"
                                                    Style="font-family: Calibri; font-weight: 700">
                                                </asp:DropDownList>
                                                        <asp:DropDownList AutoPostBack="True" ID="ddlTahunBulan" runat="server" OnSelectedIndexChanged="ddlTahunBulan_SelectedChange"
                                                            Style="font-family: Calibri; font-weight: 700">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 5%">&nbsp;
                                                    </td>
                                                    <td style="width: 40%">
                                                        <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                            Style="font-family: Calibri" />
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Style="font-family: Calibri" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                            Style="font-family: Calibri" />
                                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                                                            Style="font-family: Calibri" />
                                                        <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td style="font-family: Calibri; width: 15%;">
                                                        <asp:Label ID="LabelResult" runat="server" Visible="false" Style="font-family: Arial; font-size: medium; font-weight: 300; color: #000066;"
                                                            Width="100%"
                                                            Font-Bold="True" Font-Italic="True"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelPeriodeTahun" runat="server" Visible="false" Width="778px">
                                            <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                <tr>
                                                    <td style="width: 15%; font-family: Calibri; font-weight: 700;">Periode Tahun :
                                                <asp:DropDownList AutoPostBack="True" ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_SelectedChange"
                                                    Style="font-family: Calibri; font-weight: 700">
                                                </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 40%">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnPreview2" runat="server" Text="Preview" OnClick="btnPreview2_Click"
                                                            Style="font-family: Calibri" />
                                                        <asp:Button ID="btnExport2" runat="server" Text="Export to Excel" OnClick="btnExport2_Click"
                                                            Style="font-family: Calibri" />
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelPBulan" runat="server" Visible="false" Width="779px">
                                            <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                <tr>
                                                    <td style="width: 50%">&nbsp;<asp:Label ID="LabelHeader" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: 700"
                                                        Width="100%"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelRBulanan" runat="server" Visible="false">
                                            <div class="contentlist" style="height: 410px">
                                                <div id="DivRoot" align="left">
                                                    <div id="lst" runat="server">
                                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                                        </div>
                                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                            <table style="width: 70%; border-collapse: collapse; font-size: x-small; display: block"
                                                                border="0">
                                                                <thead>
                                                                    <tr class=" tbHeader baris">
                                                                        <th class="kotak" style="width: 5%">NO.
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%">PLANT
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">JENIS UNIT
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">NO POLISI
                                                                        </th>
                                                                        <th class="kotak" style="width: 12%">BUDGET PER BULAN
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">ACTUAL
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">%
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody style="font-family: Calibri">
                                                                    <asp:Repeater ID="DataBudget" runat="server" OnItemDataBound="DataBudget_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("No") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("Plant") %>
                                                                                </td>
                                                                                <td class="kotak">&nbsp;
                                                                            <%# Eval("JenisUnit") %>
                                                                                </td>
                                                                                <td class="kotak">&nbsp;
                                                                            <%# Eval("NoPol")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("MaxBudget", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Actual", "{0:N0}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Persen", "{0:N1}")+"%"%>
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
                                        <asp:Panel ID="PanelRTahunan" runat="server" Visible="false">
                                            <div class="contentlist" style="height: 410px">
                                                <div id="lst2" runat="server">
                                                    <table style="width: 130%; border-collapse: collapse; font-size: x-small; display: block"
                                                        border="0">
                                                        <thead>
                                                            <tr class=" tbHeader baris">
                                                                <th class="kotak" rowspan="2" style="width: 3%">NO.
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 6%">PLANT
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 7%">JENIS UNIT
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 6%">NO POLISI
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 8%">BUDGET PER BULAN
                                                                </th>
                                                                <th class="kotak" colspan="12" style="width: 60%">PERIODE
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 6%">TOTAL
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 10%">%
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader">
                                                                <th class="kotak" style="width: 5%">JAN
                                                                </th>
                                                                <th class="kotak" style="width: 5%">FEB
                                                                </th>
                                                                <th class="kotak" style="width: 5%">MRT
                                                                </th>
                                                                <th class="kotak" style="width: 5%">APR
                                                                </th>
                                                                <th class="kotak" style="width: 5%">MEI
                                                                </th>
                                                                <th class="kotak" style="width: 5%">JUN
                                                                </th>
                                                                <th class="kotak" style="width: 5%">JUL
                                                                </th>
                                                                <th class="kotak" style="width: 5%">AGST
                                                                </th>
                                                                <th class="kotak" style="width: 5%">SEPT
                                                                </th>
                                                                <th class="kotak" style="width: 5%">OKT
                                                                </th>
                                                                <th class="kotak" style="width: 5%">NOV
                                                                </th>
                                                                <th class="kotak" style="width: 5%">DES
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody style="font-family: Calibri; font-size: x-small; font-weight: 700;">
                                                            <asp:Repeater ID="DataBudget2" runat="server" OnItemDataBound="DataBudget2_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("No") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("Plant") %>
                                                                        </td>
                                                                        <td class="kotak">&nbsp;
                                                                    <%# Eval("JenisUnit") %>
                                                                        </td>
                                                                        <td class="kotak">&nbsp;
                                                                    <%# Eval("NoPol")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("MaxBudget", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Jan", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Feb", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Mrt", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Apr", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Mei", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Jun", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Jul", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Agst", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Sept", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Okt", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Nov", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Des", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Total", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka" align="right">
                                                                            <%# Eval("Persen", "{0:N1}")+"%"%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelBulanan_Baru" runat="server" Visible="false">
                                            <div class="contentlist" style="height: 410px">
                                                <div id="Div2" align="left">
                                                    <div id="lstNew" runat="server">
                                                        <div style="overflow: hidden;" id="Div4">
                                                        </div>
                                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="Div5">
                                                            <table style="width: 75%; border-collapse: collapse; font-size: x-small; display: block"
                                                                border="0">
                                                                <thead>
                                                                    <tr class="tbHeader" id="hd1">
                                                                        <th class="kotak" rowspan="2" style="width: 5%">NO.
                                                                        </th>
                                                                        <th class="kotak" rowspan="2" style="width: 15%">PLANT/DEPO
                                                                        </th>
                                                                        <th class="kotak" rowspan="2" style="width: 10%">JENIS
                                                                        </th>
                                                                        <th class="kotak" rowspan="2" style="width: 10%">KENDARAAN
                                                                        </th>
                                                                        <th class="kotak" colspan="3" style="width: 25%">STANDAR BUDGET
                                                                        </th>
                                                                        <th class="kotak" rowspan="2" style="width: 10%">PEMAKAIAN
                                                                        </th>
                                                                        <th class="kotak" rowspan="2" style="width: 8%">%
                                                                        </th>
                                                                    </tr>
                                                                    <tr class="tbHeader" id="hd2">
                                                                        <th class="kotak tengah" style="width: 10%">BUDGET/KM
                                                                        </th>
                                                                        <th class="kotak tengah" style="width: 10%">KM
                                                                        </th>
                                                                        <th class="kotak tengah" style="width: 15%">TTL BUDGET STANDAR
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody style="font-family: Calibri">
                                                                    <asp:Repeater ID="DataBudgetNew" runat="server" OnItemDataBound="DataBudgetNew_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("No") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("Plant") %>
                                                                                </td>
                                                                                <td class="kotak">&nbsp;
                                                                            <%# Eval("JenisUnit") %>
                                                                                </td>
                                                                                <td class="kotak">&nbsp;
                                                                            <%# Eval("NoPol")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("MaxBudget", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Km", "{0:N0}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("TtlBudget", "{0:N1}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Actual", "{0:N0}")%>
                                                                                </td>
                                                                                <td class="kotak angka" align="right">
                                                                                    <%# Eval("Persen", "{0:N1}")+" %"%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                        <FooterTemplate>
                                                                            <tr class="total baris" id="ftr" runat="server">
                                                                                <td class="kotak tengah" colspan="6">
                                                                                    <strong>Grand Total</strong>
                                                                                </td>
                                                                                <td class="kotak bold angka"></td>
                                                                                <td class="kotak bold angka"></td>
                                                                                <td class="kotak bold angka"></td>
                                                                            </tr>
                                                                        </FooterTemplate>

                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div id="Div6" style="overflow: hidden">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
