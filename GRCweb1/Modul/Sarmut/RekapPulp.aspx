<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPulp.aspx.cs" Inherits="GRCweb1.Modul.SarMut.RekapPulp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        $(document).ready(function() {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#<%=lst.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        
    </script>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        REKAP PEMAKAIAN BAHAN BAKU KERTAS
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Form Kertas" OnClick="btnForm_Click" />
                                        <asp:TextBox ID="txtCari" runat="server" Width="250px"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--content--->
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Periode
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
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
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 550px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <%--<div id="DivRoot" align="left">--%>
                                        <%-- <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>--%>
                                       <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <table id="zib" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri;
                                                width: 150%" border="1">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <%--<th class="kotak" rowspan="4">
                                                            No.
                                                        </th>--%>
                                                        <th class="kotak" rowspan="4">
                                                            Tanggal Produksi
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line1" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line2" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line3" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line4" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line5" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" colspan="2" rowspan="2">
                                                            <asp:Label ID="line6" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Total mix
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Formula
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Suplay dari logistik
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Saldo stok proses bm
                                                        </th>
                                                        <th style="width: 59px" rowspan="4">
                                                            Area Transit
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Keluar Logistik
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Efisiensi %
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Jenis Kertas Virgin
                                                        </th>
                                                        <th class="kotak" colspan="3">
                                                            KERTAS VIRGIN (KV)
                                                        </th>
                                                        <th class="kotak" colspan="6">
                                                            KERTAS SEMEN (KS)
                                                        </th>
                                                        <th class="kotak" colspan="6">
                                                            KERTAS KRAFT
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Total Eff (% )
                                                        </th>
                                                        <th class="kotak" rowspan="4">
                                                            Standart Komposisi Pulp
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" colspan="3">
                                                            KV
                                                        </th>
                                                        <th class="kotak" colspan="3">
                                                            Kertas Bima
                                                        </th>
                                                        <th class="kotak">
                                                            Kertas Semen
                                                        </th>
                                                        <th class="kotak" colspan="2">
                                                            Kertas Semen
                                                        </th>
                                                        <th class="kotak" colspan="2">
                                                            GRADE UTAMA
                                                        </th>
                                                        <th class="kotak" colspan="2">
                                                            GRADE 2
                                                        </th>
                                                        <th class="kotak" colspan="2">
                                                            GRADE 3
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak">
                                                            <asp:Label ID="lblF1" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                             <asp:Label ID="lblF2" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            G185
                                                        </th>
                                                        <th class="kotak">
                                                            G206/207
                                                        </th>
                                                        <th class="kotak">
                                                            G185
                                                        </th>
                                                        <th class="kotak">
                                                            G206
                                                        </th>
                                                        <th class="kotak">
                                                            G185/G201
                                                        </th>
                                                        <th class="kotak">
                                                            G206/G207
                                                        </th>
                                                        <th class="kotak">
                                                            G185
                                                        </th>
                                                        <th class="kotak">
                                                            G206
                                                        </th>
                                                        <th class="kotak">
                                                            G185
                                                        </th>
                                                        <th class="kotak">
                                                            G206/G207
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Kg (SPB)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Kg (Eff K)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Eff (%)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Berat Kotor (Kg)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Sampah (3,5%)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Berat Bersih (Kg)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            KG
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            KG
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Eff (%)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            KG
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Eff (%)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            KG
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Eff (%)
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            KG
                                                        </th>
                                                        <th class="kotak" rowspan="2">
                                                            Eff (%)
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula1" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula1a" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula2" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula2a" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula3" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula3a" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula4" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula4a" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula5" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula5a" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula6" runat="server"></asp:Label>
                                                        </th>
                                                        <th class="kotak">
                                                            <asp:Label ID="frmula6a" runat="server"></asp:Label>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                    <asp:Repeater ID="lstPMX" runat="server" OnItemCommand="lstPMX_Command" OnItemDataBound="lstPMX_Databound">
                                                        <ItemTemplate>
                                                            <tr id="Tr1" runat="server" class="EvenRows baris">
                                                                <%-- <td class="kotak tengah">
                                                                    <%# Container.ItemIndex+1  %>
                                                                </td>--%>
                                                                <td class="kotak tengah " style="white-space: nowrap">
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod2","{0:d}") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL1" runat="server" Width="100%" Text='<%# Eval("L1", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL1x" runat="server" Width="100%" Text='<%# Eval("L1x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL2" runat="server" Width="100%" Text='<%# Eval("L2", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL2x" runat="server" Width="100%" Text='<%# Eval("L2x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL3" runat="server" Width="100%" Text='<%# Eval("L3", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL3x" runat="server" Width="100%" Text='<%# Eval("L3x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL4" runat="server" Width="100%" Text='<%# Eval("L4", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL4x" runat="server" Width="100%" Text='<%# Eval("L4x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL5" runat="server" Width="100%" Text='<%# Eval("L5", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL5x" runat="server" Width="100%" Text='<%# Eval("L5x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL6" runat="server" Width="100%" Text='<%# Eval("L6", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <asp:Label ID="txtL6x" runat="server" Width="100%" Text='<%# Eval("L6x", "{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <asp:Label ID="txtTMix" runat="server" Width="100%" Text='<%# Eval("TMix","{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <asp:Label ID="txtFormula" runat="server" Width="100%" Text='<%# Eval("Formula","{0:N0}")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <%# Eval("SdL", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <%# Eval("SspB", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka " style="background-color: Yellow">
                                                                    <%# Eval("AT", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("KL","{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Efis", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak tengah " style="background-color: Yellow">
                                                                    <%# Eval("JKertasVirgin", "{0:N0}")%>
                                                                    
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KvKg", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KvKg2", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KvEfis", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("BkBimaKg", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("SampahBima", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("BbBimaKg", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KsKg0", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KsKg", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KsEfis", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KuKg", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("KuEfs", "{0:N1}")%>
                                                                </td>
                                                                 <td class="kotak angka ">
                                                                    <%# Eval("Kgrade2Kg", "{0:N1}")%>
                                                                </td>
                                                                 <td class="kotak angka ">
                                                                    <%# Eval("Kgrade2Eff", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("Kgrade3Kg", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("Kgrade3Eff", "{0:N2}")%>
                                                                </td>
                                                                <td class="kotak angka ">
                                                                    <%# Eval("Tefis", "{0:N1}")%>
                                                                </td>
                                                               <td class="kotak tengah" >
                                                                   KV=<%# Eval("Kv", "{0:N0}")%>%,KB=<%# Eval("Kb", "{0:N0}")%>%,KS=<%# Eval("Ks", "{0:N0}")%>%,
                                                                   KU=<%# Eval("KGradeUtama", "{0:N0}")%>%,K2=<%# Eval("Kgrade2", "{0:N0}")%>%,K3=<%# Eval("kgrade3", "{0:N0}")%>%
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        <%-- </div>--%>
                                        <%-- <div id="DivFooterRow" style="overflow: hidden">
                                        </div>--%>
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
