<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM_Pulp.aspx.cs" Inherits="GRCweb1.Modul.SarMut.BM_Pulp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        input[readonly] {
            color: black;
            background: #f5f5f5 !important;
            cursor: default;
            border: 0px solid #fff;
        }
    </style>
    <%-- <script type="text/javascript">
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
    </script>--%>
    <%-- razib--%>
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">PEMANTAUAN PEMAKAIAN PULP
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Refresh" Style="font-family: Calibri" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="font-family: Calibri" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--Content--->
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td style="width: 15%; font-family: Calibri; text-align: left;">Periode
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 65%">
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" Style="font-family: Calibri"
                                                OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnCalc" runat="server" Text="Proses" Style="font-family: Calibri"
                                                OnClick="btnCalc_Click" Visible="false" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Style="font-family: Calibri"
                                                OnClick="btnEdit_Click" Visible="false" />
                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Style="font-family: Calibri"
                                                Visible="false" OnClick="btnSimpan_Click" />
                                            <asp:Button ID="btnSimpan2" runat="server" Text="Update" Style="font-family: Calibri"
                                                Visible="false" OnClick="btnSimpan2_Click" />
                                            <asp:Button ID="btnLihat" runat="server" Text="Cetak Laporan" Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" />
                                            <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to Excel"
                                                Style="font-family: Calibri;" OnClick="btnExport_Click" />
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
                                <hr />
                                <asp:Panel ID="PanelKarawang" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="lst" runat="server">
                                        <table id="zib" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 150%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="5">No.
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="5">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="2" rowspan="3">
                                                        <asp:Label ID="line1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">
                                                        <asp:Label ID="line2" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="3">
                                                        <asp:Label ID="line3" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">
                                                        <asp:Label ID="line4" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">
                                                        <asp:Label ID="line5" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">
                                                        <asp:Label ID="line6" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="5">Total mix
                                                    </th>
                                                    <th class="kotak" rowspan="5">Formula
                                                    </th>
                                                    <th class="kotak" rowspan="5">Suplay dari logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5">Saldo stok proses bm
                                                    </th>
                                                    <th style="width: 59px" rowspan="5">Area Transit
                                                    </th>
                                                    <th class="kotak" rowspan="5">Keluar Logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5">Efisiensi %
                                                    </th>
                                                    <th class="kotak" rowspan="5">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="10">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="5">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" colspan="6">Standart Komposisi Pulp
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3" rowspan="2">KV
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="2">Kertas Bima
                                                    </th>
                                                    <th class="kotak" rowspan="2">Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="6">GRADE 3
                                                    </th>
                                                    <th class="kotak" rowspan="4">KV
                                                    </th>
                                                    <th class="kotak" rowspan="4">KB
                                                    </th>
                                                    <th class="kotak" rowspan="4">KS
                                                    </th>
                                                    <th class="kotak" rowspan="4">GU
                                                    </th>
                                                    <th class="kotak" rowspan="4">G2
                                                    </th>
                                                    <th class="kotak" rowspan="4">G3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3">Kraft Mentah Plastik
                                                    </th>
                                                    <th class="kotak">Grade 3
                                                    </th>
                                                    <th class="kotak" colspan="2">Total Grade 3
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
                                                        <asp:Label ID="lblF3" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF4" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF5" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF6" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF7" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF8" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF9" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF10" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF11" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF12" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF13" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF16" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF14" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF15" runat="server"></asp:Label>
                                                    </th>

                                                    <th class="kotak" rowspan="2">Kg (SPB)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Sampah &nbsp;&nbsp;
                                                        <asp:Label ID="PersenKBima" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Bersih (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Sampah&nbsp;&nbsp;
                                                        <asp:Label ID="txtPerKraftSmpahPlastik" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Bersih(Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
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
                                                        <asp:Label ID="frmula2b" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3a1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4a1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula5" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula5a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6a1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6a" runat="server"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tb" runat="server">
                                                <asp:Repeater ID="lstR" runat="server" OnItemCommand="lstR_Command" OnItemDataBound="lstR_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1  %>
                                                            </td>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod","{0:d}") %>'></asp:Label></span>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1G" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1" runat="server" OnTextChanged="jmlL1G_Change" CssClass="txtOnGrid angka"
                                                                    Width="100%" AutoPostBack="true" Font-Size="XX-Small"></asp:TextBox>
                                                                <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>

                                                            <td class="kotak tengah">
                                                                <asp:Label ID="tMix" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="Frmula" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSdL" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSspB" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtAT" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKL" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtJenisKertasVirgin" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyPulpVirgin", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg2" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBK" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyKertasBima", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaSampah" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBB" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg0" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyKertasSemen", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuKg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGradeUtama", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuEfs" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGrade2", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikktr" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyKraftPlastik", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikSmph" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikBrsh" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGrade3", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg2" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKV" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKB" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKS" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKGradeUtama" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelKarawang2" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div2" runat="server">
                                        <table id="zib2" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 150%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="5" style="width: 3%">#
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="5">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="2" rowspan="3">
                                                        <asp:Label ID="line1a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">
                                                        <asp:Label ID="line2a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="3">
                                                        <asp:Label ID="line3a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">
                                                        <asp:Label ID="line4a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">
                                                        <asp:Label ID="line5a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">
                                                        <asp:Label ID="line6a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="5">Total mix
                                                    </th>
                                                    <th class="kotak" rowspan="5">Formula
                                                    </th>
                                                    <th class="kotak" rowspan="5">Suplay dari logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5">Saldo stok proses bm
                                                    </th>
                                                    <th style="width: 59px" rowspan="5">Area Transit
                                                    </th>
                                                    <th class="kotak" rowspan="5">Keluar Logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5">Efisiensi %
                                                    </th>
                                                    <th class="kotak" rowspan="5">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="10">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="5">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" colspan="6">Standart Komposisi Pulp
                                                    </th>
                                                    <%--<th class="kotak" rowspan="5">
                                                        #
                                                    </th>--%>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3" rowspan="2">KV
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="2">Kertas Bima
                                                    </th>
                                                    <th class="kotak" rowspan="2">Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="6">GRADE 3
                                                    </th>
                                                    <th class="kotak" rowspan="4">KV
                                                    </th>
                                                    <th class="kotak" rowspan="4">KB
                                                    </th>
                                                    <th class="kotak" rowspan="4">KS
                                                    </th>
                                                    <th class="kotak" rowspan="4">GU
                                                    </th>
                                                    <th class="kotak" rowspan="4">G2
                                                    </th>
                                                    <th class="kotak" rowspan="4">G3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3">Kraft Mentah Plastik
                                                    </th>
                                                    <th class="kotak">Grade 3
                                                    </th>
                                                    <th class="kotak" colspan="2">Total Grade 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF1a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF2a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF3a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF4a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF5a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF6a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF7a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF8a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF9a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF10a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF11a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF12a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF13a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF14a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF15a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblF16a" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="2">Kg (SPB)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Sampah &nbsp;&nbsp;
                                                        <asp:Label ID="PersenKBima0" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Bersih (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">Sampah&nbsp;&nbsp;
                                                        <asp:Label ID="txtPerKraftSmpahPlastik0" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2">Berat Bersih(Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2">Eff (%)
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula1x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula1ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula2x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula2ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula2bx" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula3a1x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula4a1x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula5x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula5ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6x" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6ax" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="frmula6ax1" runat="server"></asp:Label>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tb1" runat="server">
                                                <asp:Repeater ID="lstR2" runat="server" OnItemCommand="lstR2_Command" OnItemDataBound="lstR2_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1x" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="Refresh" ToolTip="Refresh Data" runat="server" ImageUrl="~/images/icons8-refresh-16.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="refresh" Visible="true" />
                                                                <asp:ImageButton ID="edit" ToolTip="Edit Data" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" Visible="true" />
                                                                <asp:ImageButton ID="simpan" ToolTip="Simpan Perubahan" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Save" Visible="false" />
                                                            </td>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod2") %>'></asp:Label></span>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1G" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1","{0:N0}") %>'
                                                                    ReadOnly="true"> </asp:TextBox>
                                                                <%--<asp:HiddenField ID="txtNilaiID" runat="server" runat="server" ToolTip='<%# Eval("ID") %>' /> --%>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L2","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L2x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L2x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L3","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L3x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L5","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L5x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL5G5x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L5x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <%-- <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6x1x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L6x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>--%>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L6","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L6x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL6G6x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L6x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="tMix" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("TMix","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="Frmula" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Formula","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSdL" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("SdL","{0:N0}") %>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSspB" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("SspB","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtAT" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("AT","{0:N0}") %>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKL" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KL","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Efis","{0:N1}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtJenisKertasVirgin" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("JKertasVirgin") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvKg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKvKg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KvKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg2" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvKg2", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvEfis", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBK" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("BkBimaKg", "{0:N0}")%>'></asp:Label>
                                                                <%-- <asp:TextBox ID="txtKBimaBK" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("BkBimaKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaSampah" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("SampahBima", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBB" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("BbBimaKg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg0" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KsKg0", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKsKg0" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KsKg0", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KsKg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KsEfis", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KuKg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKuKg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KuKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuEfs" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KuEfs", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Kg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Kgrade2Kg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKgrade2Kg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade2Kg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox> --%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Eff" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Kgrade2Eff", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikktr" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KraftBkKg", "{0:N0}")%>'></asp:Label>
                                                                <%-- <asp:TextBox ID="txtKgKraftPlastikktr" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KraftBkKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox> --%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikSmph" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("SampahKraft", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgKraftPlastikBrsh" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KraftBbKg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("Kgrade3Kg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKgrade3Kg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade3Kg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox> --%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg2" runat="server" Font-Size="XX-Small" Text='<%# Eval("Kgrade3xKg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Eff" runat="server" Font-Size="XX-Small" Text='<%# Eval("Kgrade3Eff", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTEfis" runat="server" Font-Size="XX-Small" Text='<%# Eval("TEfis", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKV" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kv", "{0:N0}")%>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKB" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kb", "{0:N0}")%>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKS" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Ks", "{0:N0}")%>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKGradeUtama" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KGradeUtama", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade2", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade3", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <%--<td class="kotak tengah" style="background-color: #68f2e1">
                                                                
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelKarawangExport" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div3" runat="server">
                                        <table id="tblExport" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <%--<th class="kotak" rowspan="5">
                                                        No.
                                                    </th>
                                                    <%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="2" rowspan="3">Line 1
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">Line 2
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="3">Line 3
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">Line 4
                                                    </th>
                                                    <th class="kotak" rowspan="3" colspan="3">Line 5
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="3">Line 6
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Total mix
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Formula
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Suplay dari logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Saldo stok proses bm
                                                    </th>
                                                    <th style="width: 2%" rowspan="5">Area Transit
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Keluar Logistik
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Efisiensi %
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="10">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="5" style="width: 2%">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" rowspan="5">Standart Komposisi Pulp
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3" rowspan="2">KV
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="2">Kertas Bima
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="6">GRADE 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3">Kraft Mentah Plastik
                                                    </th>
                                                    <th class="kotak" style="width: 2%">Grade 3
                                                    </th>
                                                    <th class="kotak" colspan="2">Total Grade 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" style="width: 2%">G185G 189
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G185G 189
                                                    </th>
                                                    <th class="kotak" style="width: 2%">C 007
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206/207
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G185G 189
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G185/G201
                                                    </th>
                                                    <th class="kotak" style="width: 2%">C 007
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206/G207
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G185
                                                    </th>
                                                    <th class="kotak" style="width: 2%">C 007
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206
                                                    </th>
                                                    <th class="kotak" style="width: 2%">C 007
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G206 / G 207
                                                    </th>
                                                    <th class="kotak" style="width: 2%">G 211 / P 004
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Kg (SPB)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Sampah &nbsp;&nbsp;
                                                        <asp:Label ID="Label22" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Berat Bersih (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Eff (%)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Berat Kotor (Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Sampah&nbsp;&nbsp;
                                                        <asp:Label ID="Label23" runat="server"></asp:Label>%
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Berat Bersih(Kg)
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">KG
                                                    </th>
                                                    <th class="kotak" rowspan="2" style="width: 2%">Eff (%)
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" style="width: 2%">90
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                    <th class="kotak" style="width: 2%">90
                                                    </th>
                                                    <th class="kotak" style="width: 2%">85
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                    <th class="kotak" style="width: 2%">90
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                    <th class="kotak" style="width: 2%">90
                                                    </th>
                                                    <th class="kotak" style="width: 2%">85
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                    <th class="kotak" style="width: 2%">90
                                                    </th>
                                                    <th class="kotak" style="width: 2%">85
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                    <th class="kotak" style="width: 2%">85
                                                    </th>
                                                    <th class="kotak" style="width: 2%">79
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody1" runat="server">
                                                <asp:Repeater ID="rptExport" runat="server">
                                                    <ItemTemplate>
                                                        <tr id="Tr1x" runat="server" class="EvenRows baris">
                                                            <%-- <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1  %>
                                                            </td>--%>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <%# Eval("Tgl_Prod2") %>
                                                            </td>
                                                            <td class="kotak tengah ">
                                                                <%# Eval("L1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah ">
                                                                <%# Eval("L1x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L2","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L2x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L2x1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L3","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L3x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L4","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L4x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L4x1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L5","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L5x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" s>
                                                                <%# Eval("L5x1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L6","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L6x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("L6x1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TMix","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Formula","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SdL","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SspB","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("AT","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KL","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Efis","{0:N1}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("JKertasVirgin") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvKg2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvEfis", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("BkBimaKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SampahBima", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("BbBimaKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KsKg0", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KsKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KsEfis", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KuKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KuEfs", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade2Kg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade2Eff", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KraftBkKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SampahKraft", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KraftBkKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade3Kg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade3xKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade3Eff", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TEfis", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">KV=<%# Eval("Kv", "{0:N0}")%>%,KB=<%# Eval("Kb", "{0:N0}")%>%,KS=<%# Eval("Ks", "{0:N0}")%>%,
                                                                KU=<%# Eval("KGradeUtama", "{0:N0}")%>%,K2=<%# Eval("Kgrade2", "{0:N0}")%>%,K3=<%# Eval("kgrade3", "{0:N0}")%>%
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCiteureup" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div4" runat="server">
                                        <table id="Table1" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="3">No.
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="3">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="4" rowspan="2">
                                                        <asp:Label ID="LineC1" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="2">
                                                        <asp:Label ID="LineC2" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="2">
                                                        <asp:Label ID="LineC3" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="3" rowspan="2">
                                                        <asp:Label ID="LineC4" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total mix
                                                    </th>
                                                    <th class="kotak" rowspan="3">Formula
                                                    </th>
                                                    <th class="kotak" rowspan="3">Suplay dari logistik
                                                    </th>
                                                    <th class="kotak" rowspan="3">Saldo stok proses bm
                                                    </th>
                                                    <th rowspan="3" style="width: 59px">Area Transit
                                                    </th>
                                                    <th rowspan="3" class="kotak">Keluar Logistik
                                                    </th>
                                                    <th rowspan="3" class="kotak">Efisiensi %
                                                    </th>
                                                    <th rowspan="3" class="kotak">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="2">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="5">Standart Komposisi Pulp
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3" style="height: 15px">KV
                                                    </th>
                                                    <th class="kotak" colspan="2" style="height: 15px">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2" style="height: 15px">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2" style="height: 15px">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="2" style="height: 15px">GRADE 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1x" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1a" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1ax" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1a0" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1ax0" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1a01" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1ax01" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc2" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc2x" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc2a" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc2ax" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc3" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc3x" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc3a" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc3ax" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc4x" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4a1" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc4ax1" runat="server" Visible="true"></asp:Label></th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4a" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc4ax" runat="server" Visible="true"></asp:Label>
                                                    </th>

                                                    <th class="kotak">Kg (SPB)
                                                    </th>
                                                    <th class="kotak">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff(%)
                                                    </th>
                                                    <th class="kotak">KV
                                                    </th>
                                                    <th class="kotak">KS
                                                    </th>
                                                    <th class="kotak">G1
                                                    </th>
                                                    <th class="kotak">G2
                                                    </th>
                                                    <th class="kotak">G3
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody2" runat="server">
                                                <asp:Repeater ID="C1" runat="server" OnItemCommand="C1_Command" OnItemDataBound="C1_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1c" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1  %>
                                                            </td>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod","{0:d}") %>'></asp:Label></span>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1G" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1" runat="server" OnTextChanged="jmlL1G_Change" CssClass="txtOnGrid angka"
                                                                    Width="100%" AutoPostBack="true" Font-Size="XX-Small"></asp:TextBox>
                                                                <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx1" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx12" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x12" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="tMix" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="Frmula" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSdL" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSspB" runat="server" CssClass="txtOnGrid angka" Font-Size="XX-Small"
                                                                    Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtAT" runat="server" CssClass="txtOnGrid angka" Font-Size="XX-Small"
                                                                    Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKL" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtJenisKertasVirgin" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyPulpVirgin", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg2" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyKertasSemen", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuKg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGradeUtama", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuEfs" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGrade2", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("QtyGrade3", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKV" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKS" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKGradeUtama" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCiteureup2" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div5" runat="server">
                                        <table id="Table2" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 110%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="3" style="width: 44px">#
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="3">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="4" rowspan="2">
                                                        <asp:Label ID="LineC12" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">
                                                        <asp:Label ID="LineC21" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="2">
                                                        <asp:Label ID="LineC31" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="3">
                                                        <asp:Label ID="LineC41" runat="server"></asp:Label>
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total mix
                                                    </th>
                                                    <th class="kotak" rowspan="3">Formula
                                                    </th>
                                                    <th rowspan="3" class="kotak">Suplay dari logistik
                                                    </th>
                                                    <th rowspan="3" class="kotak">Saldo stok proses bm
                                                    </th>
                                                    <th rowspan="3" style="width: 59px">Area Transit
                                                    </th>
                                                    <th class="kotak" rowspan="3">Keluar Logistik
                                                    </th>
                                                    <th class="kotak" rowspan="3">Efisiensi %
                                                    </th>
                                                    <th class="kotak" rowspan="3">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="2">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" colspan="5" rowspan="2">Standart Komposisi Pulp
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3">KV
                                                    </th>
                                                    <th class="kotak" colspan="2">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1z" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1zf" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1za" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1zf1" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1zb" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1zf1b" runat="server" Visible="true"></asp:Label></th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc1zc" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc1zf1c" runat="server" Visible="true"></asp:Label></th>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc2z" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc2zf" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc2za" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc2zf1" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc3z" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc3zf" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc3za" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc3zaf1" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4z" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblfc4zf" runat="server" Visible="true"></asp:Label>
                                                    </th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4za1" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblfc4zaf12" runat="server" Visible="true"></asp:Label></th>
                                                    <th class="kotak">
                                                        <asp:Label ID="lblfc4za" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblfc4zaf1" runat="server" Visible="true"></asp:Label>
                                                    </th>

                                                    <th class="kotak">Kg (SPB)
                                                    </th>
                                                    <th class="kotak">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff(%)
                                                    </th>
                                                    <th class="kotak">KV
                                                    </th>
                                                    <th class="kotak">KS
                                                    </th>
                                                    <th class="kotak">G1
                                                    </th>
                                                    <th class="kotak">G2
                                                    </th>
                                                    <th class="kotak">G3
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody3" runat="server">
                                                <asp:Repeater ID="C2" runat="server" OnItemCommand="C2_Command" OnItemDataBound="C2_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1c2" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="Refreshx" ToolTip="Refresh Data" runat="server" ImageUrl="~/images/icons8-refresh-16.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="refreshx" Visible="true" />
                                                                <asp:ImageButton ID="edit" ToolTip="Ubah Data" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" Visible="true" />
                                                                <asp:ImageButton ID="simpan" ToolTip="Simpan Data" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Save" Visible="false" />
                                                            </td>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod2","{0:d}") %>'></asp:Label></span>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1G" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1","{0:N0}") %>'
                                                                    ReadOnly="true"> </asp:TextBox>
                                                                <%--<asp:HiddenField ID="txtNilaiID" runat="server" runat="server" ToolTip='<%# Eval("ID") %>' /> --%>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx1" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <asp:Label ID="txtL1Gx12" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x12" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L1x2","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L2","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL2G2x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L2x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L3","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL3G3x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L3x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4x","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtjmlL4G4x1" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("L4x1","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="tMix" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("TMix","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="Frmula" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Formula","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSdL" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("SdL","{0:N0}") %>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtSspB" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("SspB","{0:N0}") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtAT" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("AT","{0:N0}") %>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKL" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KL","{0:N0}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Efis","{0:N1}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtJenisKertasVirgin" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("JKertasVirgin") %>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvKg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKvKg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KvKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg2" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvKg2", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KvEfis", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KsKg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsEfis" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KsEfis", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuKg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KuKg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKuKg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KuKg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>--%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuEfs" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("KuEfs", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Kg" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Kgrade2Kg", "{0:N0}")%>'></asp:Label>
                                                                <%--<asp:TextBox ID="txtKgrade2Kg" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade2Kg", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox> --%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Eff" runat="server" Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>'
                                                                    Text='<%# Eval("Kgrade2Eff", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg" runat="server" Font-Size="XX-Small" Text='<%# Eval("Kgrade3Kg", "{0:N0}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Eff" runat="server" Font-Size="XX-Small" Text='<%# Eval("Kgrade3Eff", "{0:N2}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTEfis" runat="server" Font-Size="XX-Small" Text='<%# Eval("TEfis", "{0:N1}")%>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKV" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kv", "{0:N0}")%>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKS" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Ks", "{0:N0}")%>' ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKGradeUtama" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("KGradeUtama", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade2", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <asp:TextBox ID="txtKgrade3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" ToolTip='<%# Eval("ID") %>' Text='<%# Eval("Kgrade3", "{0:N0}")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCiteureupExport" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div6" runat="server">
                                        <table id="Table3" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri; width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="3">No.
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="3">Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
                                                    <th class="kotak" colspan="4" rowspan="2">Line 1
                                                    </th>
                                                    <th class="kotak" colspan="2" rowspan="2">Line 2
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="2">Line 3
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="3">Line 4
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total mix </th>
                                                    <th class="kotak" rowspan="3">Formula
                                                    </th>
                                                    <th class="kotak" rowspan="3">Suplay dari logistik
                                                    </th>
                                                    <th class="kotak" rowspan="3">Saldo stok proses bm
                                                    </th>
                                                    <th rowspan="3" style="width: 59px">Area Transit
                                                    </th>
                                                    <th rowspan="3" class="kotak">Keluar Logistik
                                                    </th>
                                                    <th class="kotak" rowspan="3">Efisiensi %
                                                    </th>
                                                    <th class="kotak" rowspan="3">Jenis Kertas Virgin
                                                    </th>
                                                    <th class="kotak" colspan="3">KERTAS VIRGIN (KV)
                                                    </th>
                                                    <th class="kotak" colspan="2">KERTAS SEMEN (KS)
                                                    </th>
                                                    <th class="kotak" colspan="6">KERTAS KRAFT
                                                    </th>
                                                    <th class="kotak" rowspan="3">Total Eff (% )
                                                    </th>
                                                    <th class="kotak" rowspan="2" colspan="5">Standart Komposisi Pulp
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak" colspan="3">KV
                                                    </th>
                                                    <th class="kotak" colspan="2">Total Kertas Semen
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE UTAMA
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE 2
                                                    </th>
                                                    <th class="kotak" colspan="2">GRADE 3
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak">G185<br />
                                                        75
                                                    </th>
                                                    <th class="kotak">G206<br />
                                                        66
                                                    </th>
                                                    <th class="kotak">G207<br />
                                                        66</th>
                                                    <th class="kotak">P003<br />
                                                        84</th>
                                                    <th class="kotak">G185<br />
                                                        90
                                                    </th>
                                                    <th class="kotak">G206<br />
                                                        80
                                                    </th>
                                                    <th class="kotak">G185<br />
                                                        90
                                                    </th>
                                                    <th class="kotak">G206<br />
                                                        80
                                                    </th>
                                                    <th class="kotak">G185<br />
                                                        90
                                                    </th>
                                                    <th class="kotak">G206<br />
                                                        80
                                                    </th>
                                                    <th class="kotak">G207<br />
                                                        66</th>
                                                    <th class="kotak">Kg (SPB)
                                                    </th>
                                                    <th class="kotak">Kg (Eff K)
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff (%)
                                                    </th>
                                                    <th class="kotak">KG
                                                    </th>
                                                    <th class="kotak">Eff(%)
                                                    </th>
                                                    <th class="kotak">KV
                                                    </th>
                                                    <th class="kotak">KS
                                                    </th>
                                                    <th class="kotak">G1
                                                    </th>
                                                    <th class="kotak">G2
                                                    </th>
                                                    <th class="kotak">G3
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="Tbody4" runat="server">
                                                <asp:Repeater ID="RepeaterCeXport" runat="server">
                                                    <ItemTemplate>
                                                        <tr id="Tr1c2" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1  %>
                                                            </td>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod2","{0:d}") %>'></asp:Label></span>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <%# Eval("L1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <%# Eval("L1x","{0:N0}") %>'
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <%# Eval("L1x1","{0:N0}") %>'
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: #68f2e1">
                                                                <%# Eval("L1x2","{0:N0}") %>'
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L2","{0:N0}") %>'
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L2x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L3","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L3x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L4","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L4x","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("L4x1","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TMix","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Formula","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("SdL","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("SspB","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("AT","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KL","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Efis","{0:N1}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("JKertasVirgin") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvKg2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KvEfis", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KsKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KsEfis", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KuKg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("KuEfs", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade2Kg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade2Eff", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade3Kg", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Kgrade3Eff", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TEfis", "{0:N1}")%>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("Kv", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("Ks", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("KGradeUtama", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("Kgrade2", "{0:N0}")%>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: #68f2e1">
                                                                <%# Eval("Kgrade3", "{0:N0}")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                            <!--End Content--->
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
