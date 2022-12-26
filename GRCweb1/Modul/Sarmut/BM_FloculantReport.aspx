<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM_FloculantReport.aspx.cs" Inherits="GRCweb1.Modul.SarMut.BM_FloculantReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>--%>
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


        function btnHitung_onclick() {

        }

    </script>
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">PEMANTAUAN EFISIENSI FLOCULANT
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Refresh" OnClick="btnNew_Click" Style="font-family: Calibri" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click"
                                            Style="font-family: Calibri" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            Style="font-family: Calibri" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td style="width: 15%; font-family: Calibri; text-align: left; font-weight: 700;">Periode
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 65%">
                                            <asp:Button ID="BtnPreview" runat="server" Text="<< Preview >>" OnClick="BtnPreview_Click"
                                                Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;"
                                                ForeColor="#000099" />
                                            <asp:Button ID="btnLihat" runat="server" Visible="false" Text="<< Cetak Laporan >>"
                                                OnClick="btnLihat_Click" Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" />
                                            <asp:Button ID="btnExport" runat="server" Visible="true" Text="<< Export to Excel >>"
                                                OnClick="btnExport_Click" Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabelStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="PanelCiteureup" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 350px; overflow: auto" id="lst" runat="server">
                                        <table style="width: 150%; border-collapse: collapse; font-size: x-small; font-family: Calibri; table-layout: fixed; overflow: scroll;"
                                            border="0">
                                            <thead>
                                                <tr class="tbHeader" style="width: 100%;">
                                                    <th rowspan="3" class="kotak" style="width: 10%;">Tanggal
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 19%;">Line 1
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 19%;">Line 2
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 19%;">Line 3
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 19%;">Line 4
                                                    </th>
                                                    <th rowspan="3" class="kotak" style="width: 7%;">Keterangan
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian Flo
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian Flo
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian Flo
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian Flo
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="tb" runat="server" style="font-family: Calibri">
                                                <asp:Repeater ID="lstMatrix" runat="server" OnItemDataBound="lstMatrix_DataBound"
                                                    OnItemCommand="lstMatrix_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr id="lst2" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Eval("Tanggal", "{0:d}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L1" , "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPB_BBL1", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL1", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL1", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML1", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L2", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPB_BBL2", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL2", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL2", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L3", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPB_BBL3", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL3", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL3", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML3", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L4", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPB_BBL4", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL4", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL4", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML4", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak" style="background-color: #FF99FF;">
                                                                <asp:Label ID="txtKeterangan" runat="server" Visible="false" Width="100%">&nbsp;&nbsp;</asp:Label>
                                                                <asp:TextBox ID="Keterangan" runat="server" AutoPostBack="true" CssClass="txtOnGrid"
                                                                    Width="100%" Font-Names="calibri"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelKarawang" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 440px; overflow: auto" id="lstK" runat="server">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                <table style="width: 150%;  border-collapse: collapse; font-size: x-small; font-family: Calibri; overflow: scroll;"
                                                    border="0" id="tblKrw">
                                                    <thead>
                                                        <tr class="tbHeader" style="width: 100%;">
                                                            <th rowspan="3" class="kotak" style="width: 6%;">Tanggal
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 1
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 2
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 3
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 4
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 5
                                                            </th>
                                                            <th colspan="5" class="kotak txtUpper" style="width: 14%;">Line 6
                                                            </th>
                                                            <th rowspan="3" class="kotak" style="width: 10%;">Keterangan
                                                            </th>
                                                        </tr>
                                                        <tr class="tbHeader">
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                            <th colspan="1" class="kotak">Output
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian BB
                                                            </th>
                                                            <th colspan="1" class="kotak">Pemakaian Flo
                                                            </th>
                                                            <th colspan="1" class="kotak">Efesiensi
                                                            </th>
                                                            <th colspan="1" class="kotak">PPM
                                                            </th>
                                                        </tr>
                                                        <tr class="tbHeader">
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                            <th class="kotak">(M3)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg)
                                                            </th>
                                                            <th class="kotak">(Kg/M3)
                                                            </th>
                                                            <th class="kotak"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tb2" runat="server" style="font-family: Calibri">
                                                        <asp:Repeater ID="lstMatrixK2" runat="server" OnItemDataBound="lstMatrixK2_DataBound"
                                                            OnItemCommand="lstMatrixK2_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr id="lstK2" runat="server" class="EvenRows baris">
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("Tanggal", "{0:d}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L1" , "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL1", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL1", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL1", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML1", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L2", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL2", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL2", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL2", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML2", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L3", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL3", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL3", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL3", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML3", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L4", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL4", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL4", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL4", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML4", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L5", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL5", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL5", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL5", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML5", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="white-space: nowrap">
                                                                        <%# Eval("OutM3L6", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPB_BBL6", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtySPBL6", "{0:N1}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("EfesiensiL6", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PPML6", "{0:N0}")%>
                                                                    </td>
                                                                    <td class="kotak" style="background-color: #FF99FF;">
                                                                        <asp:Label ID="txtKeterangan" runat="server" Visible="false" Width="100%">&nbsp;&nbsp;</asp:Label>
                                                                        <asp:TextBox ID="Keterangan" runat="server" AutoPostBack="true" CssClass="txtOnGrid"
                                                                            Width="100%" Font-Names="calibri"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <%-- <tr class="Line3 Baris" id="lstK2F" runat="server">
                                                    <td colspan="1" class="kotak txtUpper">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                    <td class="kotak angka">
                                                        &nbsp;
                                                    </td>
                                                </tr>--%>
                                                        <%--  <tr>
                                                    <td colspan="22">
                                                        &nbsp;
                                                    </td>
                                                </tr>--%>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr>
                                                            <td colspan="18" style="margin-left: 10px">
                                                                <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td colspan="4" style="margin-left: 10px" valign="top">
                                                                <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center">DiSetujui:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 20%; text-align: center;" valign="middle">
                                                                            <asp:Label ID="LabelPMK" runat="server" Style="text-decoration: underline"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 50%; text-align: center; font-weight: 700;">Plant Manager
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <td style="margin-left: 10px; width: 48px;" valign="top">
                                                                    <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    </table>
                                                                </td>
                                                            <td colspan="4" style="margin-left: 10px" valign="top">
                                                                <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center">Mengetahui:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 20%; text-align: center;" valign="middle">
                                                                            <asp:Label ID="LabelManagerK" runat="server" Style="text-decoration: underline"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 50%; text-align: center; font-weight: 700;">Manager BoardMill
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <td style="margin-left: 10px; width: 48px;" valign="top">
                                                                    <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    </table>
                                                                </td>
                                                            <td colspan="4" style="margin-left: 10px" valign="top">
                                                                <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                    <tr>
                                                                        <td colspan="4" style="width: 20%; text-align: center;" valign="middle">
                                                                            <asp:Label ID="LabelTanggalK" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="text-align: center">DiBuat:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <%--<tr>                                                            
                                                            <td colspan="4" style="width:20%; text-align: center;" valign="middle">
                                                                <asp:Label ID="LabelTandaTanganAdmK" runat="server" style="text-decoration: underline"></asp:Label></td>
                                                            
                                                        </tr>--%>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 20%; text-align: center;" valign="middle">
                                                                            <asp:Label ID="LabelAdminK" runat="server" Style="text-decoration: underline"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="width: 50%; text-align: center; font-weight: 700;">Adm Staff BoardMill
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="margin-left: 10px" valign="top">
                                                                <table style="border-collapse: collapse; font-size: x-small; width: 100%">
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>

                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="PanelLaporanKarawang" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 440px; overflow: auto" id="lstLaporanK" runat="server">
                                        <table style="width: 150%; border-collapse: collapse; font-size: x-small; font-family: Calibri; table-layout: fixed; overflow: scroll; visibility: collapse;"
                                            border="0">
                                            <thead>
                                                <tr class="tbHeader" style="width: 100%;">
                                                    <th rowspan="3" class="kotak" style="width: 4%;">Tanggal
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 1
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 2
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 3
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 4
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 5
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 15%; height: 17px;">Line 6
                                                    </th>
                                                    <th rowspan="3" class="kotak" style="width: 6%;">Keterangan
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak" style="width: 48px">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak" style="width: 48px">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak" style="width: 48px">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody1" runat="server" style="font-family: Calibri">
                                                <asp:Repeater ID="lstMatrixLapK" runat="server" OnItemDataBound="lstMatrixLapK_DataBound">
                                                    <ItemTemplate>
                                                        <tr id="lstLK" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Eval("Tanggal", "{0:d}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("OutM3L1")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL1")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL1", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL1", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML1", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L2")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL2")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL2", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL2", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L3")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL3")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL3", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL3", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML3", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L4")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL4")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL4", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL4", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML4", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L5")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL5")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL5", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL5", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML5", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka" style="white-space: nowrap">
                                                                <%# Eval("OutM3L6")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL6")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL6", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL6", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML6", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak">
                                                                <%# Eval("Keterangan")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="Line3 Baris" id="lstFLK" runat="server">
                                                    <td colspan="1" class="kotak txtUpper"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelLaporanCiteureup" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 440px; overflow: auto" id="lstLaporanC" runat="server">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri; table-layout: fixed; overflow: scroll; visibility: collapse;"
                                            border="0">
                                            <thead>
                                                <tr class="tbHeader" style="width: 100%;">
                                                    <th rowspan="3" class="kotak" style="width: 7%;">Tanggal
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 20%;">Line 1
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 20%;">Line 2
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 20%;">Line 3
                                                    </th>
                                                    <th colspan="5" class="kotak txtUpper" style="width: 20%;">Line 4
                                                    </th>
                                                    <th rowspan="3" class="kotak" style="width: 13%;">Keterangan
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                    <th colspan="1" class="kotak">Output
                                                    </th>
                                                    <th colspan="1" class="kotak">Pemakaian BB
                                                    </th>
                                                    <th colspan="1" class="kotak" style="width: 48px">Pemakaian
                                                    </th>
                                                    <th colspan="1" class="kotak">Efesiensi
                                                    </th>
                                                    <th colspan="1" class="kotak">PPM
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                    <th class="kotak">(M3)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg)
                                                    </th>
                                                    <th class="kotak">(Kg/M3)
                                                    </th>
                                                    <th class="kotak"></th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody2" runat="server" style="font-family: Calibri">
                                                <asp:Repeater ID="lstMatrixLapC" runat="server" OnItemDataBound="lstMatrixLapC_DataBound">
                                                    <ItemTemplate>
                                                        <tr id="lstLC" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Eval("Tanggal", "{0:d}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("OutM3L1")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL1")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL1", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL1", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML1", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("OutM3L2")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL2")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL2", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL2", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("OutM3L3")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL3")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL3", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL3", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML3", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("OutM3L4")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtyPakaiL4")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("QtySPBL4", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("EfesiensiL4", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("PPML4", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak">
                                                                <%# Eval("Keterangan")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="Line3 Baris" id="lstFLC" runat="server">
                                                    <td colspan="1" class="kotak txtUpper"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka"></td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                    <td class="kotak angka">&nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
