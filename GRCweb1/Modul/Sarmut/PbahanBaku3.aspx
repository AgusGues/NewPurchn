<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PbahanBaku3.aspx.cs" Inherits="GRCweb1.Modul.SarMut.PbahanBaku3" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .content {
    background-color: #ffffff;
    width: 100%;
    height: 100%;
}
    </style>
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
        //window.onload = function() {
        //    MakeStaticHeader('zib', 650, 400, 52, false)
        //}


        
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
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        PEMAKAIAN BAHAN BAKU PULP
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="Rekap" OnClick="btnList_Click" />
                                        <asp:TextBox ID="txtCari" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content" style="background:#fff;">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Tanggal
                                        </td>
                                        <td style="width: 30%">
                                            <cc1:CalendarExtender ID="ca1" runat="server" TargetControlID="txtTanggal" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                            <asp:TextBox ID="txtTanggal" runat="server" Width="50%"></asp:TextBox>&nbsp;
                                            <%--<asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>--%>
                                            <asp:Button ID="lbAddOP" runat="server" Text="Tambah" OnClick="lbAddOP_Click" />
                                            <asp:HiddenField ID="txtID" runat="server" Value="0" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 16%">
                                            <%--<asp:Button ID="lbAddOP" runat="server" Text="Tambah" OnClick="lbAddOP_Click" />&nbsp;
                                            <asp:CheckBox ID="chkHide" Text="Show Hide Kolom" runat="server" OnCheckedChanged="OnCheckedChanged"
                                                AutoPostBack="true" Checked="true" />--%>
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 550px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                        <table id="zib" style="border-collapse: collapse; font-size: xx-small; font-family: Calibri;
                                            width: 120%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" rowspan="4">
                                                        No.
                                                    </th>
                                                    <%--<%if(this.IsChecked){ %>--%>
                                                    <th class="kotak" rowspan="4">
                                                        Tanggal Produksi
                                                    </th>
                                                    <%--<%} %>--%>
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
                                                    <th class="kotak" colspan="6">
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
                                                    <th class="kotak" rowspan="3">
                                                        KV
                                                    </th>
                                                    <th class="kotak" rowspan="3">
                                                        KB
                                                    </th>
                                                    <th class="kotak" rowspan="3">
                                                        KS
                                                    </th>
                                                    <th class="kotak" rowspan="3">
                                                        GU
                                                    </th>
                                                    <th class="kotak" rowspan="3">
                                                        G2
                                                    </th>
                                                    <th class="kotak" rowspan="3">
                                                        G3
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
                                            <tbody id="tb" runat="server">
                                                <asp:Repeater ID="lstR" runat="server" OnItemCommand="lstR_Command" OnItemDataBound="lstR_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1  %>
                                                            </td>
                                                            <%-- <%if(this.IsChecked){ %>--%>
                                                            <td class="kotak tengah " style="white-space: nowrap">
                                                                <span>
                                                                    <asp:Label runat="server" ID="lbltglProd" Text='<%# Eval("Tgl_Prod","{0:d}") %>'></asp:Label></span>
                                                            </td>
                                                           <%-- <%} %>--%>
                                                            <td class="kotak tengah " style="background-color: Yellow">
                                                                <asp:Label ID="txtL1G" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1" runat="server" OnTextChanged="jmlL1G_Change" CssClass="txtOnGrid angka"
                                                                    Width="100%" AutoPostBack="true" Font-Size="XX-Small"></asp:TextBox>
                                                                <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                            </td>
                                                            <td class="kotak tengah " style="background-color: Yellow">
                                                                <asp:Label ID="txtL1Gx" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                <asp:TextBox ID="txtjmlL1G1x" runat="server" OnTextChanged="jmlL1G_Change" CssClass="txtOnGrid angka"
                                                                    Width="100%" AutoPostBack="true" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL2G2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL2G2x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL3G3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL3G3x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL4G4" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL4G4x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL5G5" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL5G5x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL6G6" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtjmlL6G6x" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small" OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="tMix" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="Frmula" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtSdL" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtSspB" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtAT" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKL" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtJenisKertasVirgin" runat="server" CssClass="txtOnGrid angka"
                                                                    Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvKg2" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKvEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBK" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaSampah" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKBimaBB" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg0" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsKg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKsEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuKg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKuEfs" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Kg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade2Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Kg" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtKgrade3Eff" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTEfis" runat="server" Font-Size="XX-Small"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKV" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKB" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"
                                                                    OnTextChanged="jmlL1G_Change" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKS" runat="server" CssClass="txtOnGrid angka" Width="100%" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKGradeUtama" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKgrade2" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                            <td class="kotak tengah" style="background-color: Yellow">
                                                                <asp:TextBox ID="txtKgrade3" runat="server" CssClass="txtOnGrid angka" Width="100%"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
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
