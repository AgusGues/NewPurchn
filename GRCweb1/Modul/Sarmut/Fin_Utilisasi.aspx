<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fin_Utilisasi.aspx.cs" Inherits="GRCweb1.Modul.SarMut.Fin_Utilisasi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        LAPORAN UTILISASI
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Refresh" OnClick="btnNew_Click" Style="font-family: Calibri" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click"
                                            Style="font-family: Calibri" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            Style="font-family: Calibri" />
                                        <asp:Button ID="btnCetak" runat="server" Text="Cetak Laporan" OnClick="btnCetak_Click"
                                            Style="font-family: Calibri" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content" style="height: 440px; width: 100%;background:#fff;">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%; font-family: Calibri; text-align: left; font-weight: 700;">
                                            Periode
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" Height="16px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Button ID="btnPreview" runat="server" Text="<< Preview >>" OnClick="btnPreview_Click"
                                                Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;"
                                                ForeColor="#000099" />
                                            <asp:Button ID="btnExport" runat="server" Text="<< Export to Excel >>" OnClick="btnExport_Click"
                                                Style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" />
                                        </td>
                                        <td style="width: 17%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                        <td style="width: 17%">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="LabelStatus" runat="server" Visible="False" Width="91%" Height="27px"
                                                Style="font-family: Calibri; font-weight: 700; color: #006600"></asp:Label>
                                        </td>
                                        <td style="height: 3px; text-align: right; width: 17%;" valign="top">
                                            <asp:RadioButton ID="RBUti" runat="server" AutoPostBack="True" OnCheckedChanged="RBUti_CheckedChanged"
                                                Style="font-family: Calibri; font-size: x-small; font-style: italic; color: #000066;
                                                text-align: right;" Text="Utilisasi Tebal 3,3.5,4 " TextAlign="Left" />
                                        </td>
                                        <td style="height: 3px; text-align: right;" valign="top">
                                            <asp:RadioButton ID="RBUtiListP" runat="server" AutoPostBack="True" OnCheckedChanged="RBUtiListP_CheckedChanged"
                                                Style="font-family: Calibri; font-size: x-small; font-style: italic; color: #000066;"
                                                Text="Utilisasi Tebal 8,9 " TextAlign="Left" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="PanelBukanListPlank" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 1000px; width: 100%; float: left;" id="lst"
                                        runat="server">
                                        <table id="tbl" runat="server">
                                            <tr>                                          
                                                <td style="width: 48%;" height="10px">
                                                    <table style="width: 100%;">                                                    
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; float: left;">
                                                                <asp:Panel ID="Panel1" runat="server" Visible="true">
                                                                    <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                        <tr id="NamaPlant" visible="false">
                                                                            <td style="font-size: small" colspan="6" align="left">
                                                                                PT. BANGUNPERKASA ADHITAMASENTRA
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td style="font-size: x-small" align="left" colspan="4">
                                                                               <%-- &nbsp;--%>
                                                                               <asp:Label ID="LabelPeriode" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                 font-size: small;"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small" colspan="6">
                                                                                II. PRODUK 3, 3.5, 4, & 4.5
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                OK & M 3.5 & 4 mm dari BP
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table border="1" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                                    font-family: Calibri;">
                                                                    <thead>
                                                                        <tr class="tbHeader">
                                                                            <th class="kotak tengah" rowspan="2" style="width: 30px" valign="middle" align="center">
                                                                                No
                                                                            </th>
                                                                            <th class="kotak tengah" rowspan="2" style="width: 175px" align="center" valign="middle">
                                                                                Partno
                                                                            </th>
                                                                            <th class="kotak tengah" colspan="2" style="width: 100px" align="center" valign="middle">
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Qty System
                                                                            </th>
                                                                            <th class="kotak tengah" colspan="1" valign="middle" align="center">
                                                                                * Qty Aktual
                                                                            </th>
                                                                            <th class="kotak tengah" rowspan="2" align="center" valign="middle">
                                                                                Keterangan
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="tbHeader">
                                                                            <th class="kotak tengah" colspan="1" style="width: 60px" align="center" valign="middle">
                                                                                &nbsp;&nbsp;&nbsp; Lembar
                                                                            </th>
                                                                            <th class="kotak tengah" colspan="1" style="width: 50px" align="center" valign="middle">
                                                                                &nbsp;&nbsp;&nbsp; M3
                                                                            </th>
                                                                            <th class="kotak tengah" colspan="1" style="width: 80px" valign="middle" align="center">
                                                                                M3
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody id="tb" runat="server" style="font-family: Calibri">
                                                                        <asp:Repeater ID="lstMatrix" runat="server" OnItemCommand="lstMatrix_ItemCommand"
                                                                            OnItemDataBound="lstMatrix_DataBound">
                                                                            <ItemTemplate>
                                                                                <tr id="lst1" runat="server" class="EvenRows baris">
                                                                                    <td class="kotak tengah" width="30px">
                                                                                        &nbsp;
                                                                                        <%# Eval("No")%>
                                                                                    </td>
                                                                                    <td class="kotak" style="white-space: nowrap" width="175px">
                                                                                        &nbsp;
                                                                                        <%# Eval("Partno")%>
                                                                                    </td>
                                                                                    <td class="kotak angka" style="width: 60px">
                                                                                        <%# Eval("QtyLembar", "{0:N0}")%>
                                                                                    </td>
                                                                                    <td class="kotak angka" style="width: 60px">
                                                                                        <%# Eval("M3", "{0:N2}")%>
                                                                                    </td>
                                                                                    <td class="kotak angka" style="width: 80px">
                                                                                        <%# Eval("M3Other", "{0:N2}")%>
                                                                                    </td>
                                                                                    <td class="kotak" style="background-color: #C0C0C0; width: 40px">
                                                                                        &nbsp;
                                                                                        <asp:Label ID="txtKeterangan" runat="server" Visible="false"></asp:Label>
                                                                                        <asp:TextBox ID="Keterangan" runat="server" AutoPostBack="true" CssClass="txtOnGrid"
                                                                                            Font-Names="calibri" OnTextChanged="Keterangan_Change"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <tr id="lstF" runat="server" class="Line3 Baris">
                                                                            <td class="kotak txtUpper" colspan="3" style="width: 210px">
                                                                            </td>
                                                                            <%--<td class="kotak angka" style="width: 60px">
                                                                </td>--%>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="LTotal" runat="server" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <%--<td class="kotak angka">
                                                                                </td>--%>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="LTotalAktual" runat="server" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <asp:Panel ID="Panel11" runat="server" Visible="true">
                                                                    <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                NB : * Aktual adalah Hasil Non Retur
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="PanelNilai" runat="server" Visible="true">
                                                                    <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="width: 100%">
                                                                            <td style="width: 10%; height: 19px;">
                                                                                <asp:Label ID="LRumusan" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold">Rumus :</asp:Label>
                                                                            </td>
                                                                            <td style="width: 50%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;" colspan="3">
                                                                                <asp:Label ID="LRumusanIsi" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold">Hasil produk OK & KW dari produk BP</asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%; height: 19px;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 20%; height: 19px;">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 10%">
                                                                                <asp:Label ID="Label1" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 50%" colspan="3">
                                                                                <asp:Label ID="Label2" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold">BP Tahap I + BP dari NC Logistik + BP dari NC Sortir </asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <table style="width: 100%; font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                        <tr style="width: 60%">
                                                                            <td style="width: 5%;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 9%;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 30%;">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 20%;">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="width: 60%">
                                                                            <td style="width: 5%; height: 19px; text-align: center;">
                                                                                <asp:Label ID="LabelSamaDengan1" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold">=</asp:Label>
                                                                            </td>
                                                                            <td style="width: 9%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;">
                                                                                <asp:Label ID="LabelM31" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 30%; height: 19px; font-family: Calibri; font-size: small; font-weight: 700;">
                                                                                &nbsp; X 100
                                                                            </td>
                                                                            <td style="width: 20%; height: 19px;">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5%">
                                                                                <asp:Label ID="Label3" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 9%; text-align: center;">
                                                                                <asp:Label ID="LabelM32" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <table style="width: 100%; font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                            <tr style="width: 60%">
                                                                                <td style="width: 5%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 9%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 30%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 20%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="width: 60%">
                                                                                <td style="width: 5%; height: 19px; text-align: center;">
                                                                                    <asp:Label ID="Label8" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold">=</asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;">
                                                                                    <asp:Label ID="LabelM31_Copy" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30%; height: 19px; font-family: Calibri; font-size: small; font-weight: 700;">
                                                                                    &nbsp; X 100
                                                                                </td>
                                                                                <td style="width: 20%; height: 19px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 5%">
                                                                                    <asp:Label ID="Label11" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%; text-align: center;">
                                                                                    <asp:Label ID="LabelM32_ttl" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 20%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 20%">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <%-- NewP --%>
                                                                            <tr style="width: 60%">
                                                                                <td style="width: 5%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 9%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 30%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 20%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="width: 60%">
                                                                                <td style="width: 5%; height: 19px; text-align: center;">
                                                                                    <asp:Label ID="Label9" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold">=</asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%; text-align: center; font-size: medium; height: 19px;">
                                                                                    <asp:Label ID="LabelNilai" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30%; height: 19px; font-family: Calibri; font-size: small; font-weight: 700;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 20%; height: 19px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 5%">
                                                                                    <asp:Label ID="Label13" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%; text-align: center;">
                                                                                    <asp:Label ID="Label14" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 20%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 20%">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table style="width: 100%; font-family: Calibri; font-size: x-small">
                                                                            <tr style="width: 60%">
                                                                                <td style="width: 30%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 10%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 9%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 4%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 10%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 30%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30%; font-size: medium; font-weight: bold;" colspan="2">
                                                                                    <asp:Label ID="LabelTotalP" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                        font-size: small; font-weight: bold" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;">
                                                                                    <asp:Label ID="LabelTotalAtas" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%; height: 19px; font-family: Calibri; font-size: small; font-weight: 700;">
                                                                                    &nbsp; X 100
                                                                                </td>
                                                                               <%-- <td style="width: 4%">
                                                                                    &nbsp; =
                                                                                </td>--%>
                                                                                <td style="width: 4%; text-align: center; font-size: medium; height: 19px;">= &nbsp;
                                                                                    <asp:Label ID="LabelNilaiAkhir" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: medium; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 40%;">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 30%" colspan="2">
                                                                                    <asp:Label ID="Label12" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 10%; text-align: center; font-size: medium; height: 19px;">
                                                                                    <asp:Label ID="LabelTotalBawah" runat="server" Visible="true" Style="font-family: Calibri;
                                                                                        font-size: x-small; font-weight: bold"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 9%">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="width: 4%">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>                                                      
                                                    </table>                                                   
                                                </td>
                                                
                                                <%--<td style="width: 1%;" valign="top">&nbsp;
                                                </td>--%>
                                                
                                                <td style="width: 20%;" valign="top">                                                   
                                                    <table style="width: 100%;">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; float: left;">
                                                                <asp:Panel ID="Panel2" runat="server" Visible="true">
                                                                    <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                    <%--<table border="0" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                                    font-family: Calibri;">--%>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                BP dari Tahap I
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table border="1" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                                    font-family: Calibri;">
                                                                    <thead>
                                                                        <tr class="tbHeader">
                                                                            <th class="kotak" rowspan="1" style="height: 37px;" align="center" valign="middle">
                                                                                No
                                                                            </th>
                                                                            <th class="kotak" rowspan="1" style="height: 37px;">
                                                                                Partno
                                                                            </th>
                                                                            <th class="kotak" rowspan="1" style="height: 37px;">
                                                                                M3
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody id="tb2" runat="server" style="font-family: Calibri">
                                                                        <asp:Repeater ID="lstMatrix2" runat="server" OnItemCommand="lstMatrix2_ItemCommand"
                                                                            OnItemDataBound="lstMatrix2_DataBound">
                                                                            <ItemTemplate>
                                                                                <tr id="lst2" runat="server" class="EvenRows baris">
                                                                                    <td class="kotak tengah" style="width: 50px">
                                                                                        &nbsp;
                                                                                        <%# Eval("No")%>
                                                                                    </td>
                                                                                    <td class="kotak">
                                                                                        &nbsp;
                                                                                        <%# Eval("Partno")%>
                                                                                    </td>
                                                                                    <td class="kotak angka">
                                                                                        <%# Eval("M3", "{0:N2}")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <tr class="Line3 Baris" id="lstF2" runat="server">
                                                                            <td colspan="2" class="kotak txtUpper" style="width: 210px">
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="LTotal2" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>                                                  
                                                </td>
                                                
                                                <td style="width: 1%;" valign="top">&nbsp;
                                                </td>
                                                
                                                <td style="width: 20%;" valign="top" align="right">                                                   
                                                    <table style="width: 100%;">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%; float: left;">
                                                                <asp:Panel ID="Panel3" runat="server" Visible="true">
                                                                    <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                     <tr>
                                                                         <td style="font-size: small" colspan="3" align="right">
                                                                             <%--Form No. PRD/K/LUBT/100/18/R0--%>
                                                                             <asp:Label ID="LabelNoForm" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                 font-size: x-small;"></asp:Label>
                                                                         </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-size: small">
                                                                                &nbsp;&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                BP dari BJ (NC dari Logistik & Sortir)
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <table border="1" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                                    font-family: Calibri;">
                                                                    <thead>
                                                                        <tr class="tbHeader">
                                                                            <th class="kotak" rowspan="1" style="height: 37px;">
                                                                                No
                                                                            </th>
                                                                            <th class="kotak" rowspan="1" style="height: 37px">
                                                                                Partno
                                                                            </th>
                                                                            <th class="kotak" rowspan="1" style="height: 37px">
                                                                                M3
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody id="tb3" runat="server" style="font-family: Calibri">
                                                                        <asp:Repeater ID="lstMatrix3" runat="server" OnItemCommand="lstMatrix3_ItemCommand"
                                                                            OnItemDataBound="lstMatrix3_DataBound">
                                                                            <ItemTemplate>
                                                                                <tr id="lst3" runat="server" class="EvenRows baris">
                                                                                    <td class="kotak tengah">
                                                                                        &nbsp;
                                                                                        <%# Eval("No")%>
                                                                                    </td>
                                                                                    <td class="kotak" style="white-space: nowrap">
                                                                                        &nbsp;
                                                                                        <%# Eval("Partno")%>
                                                                                    </td>
                                                                                    <td class="kotak angka">
                                                                                        <%# Eval("M3", "{0:N2}")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                        <tr class="Line3 Baris" id="lstF3" runat="server">
                                                                            <td colspan="2" class="kotak txtUpper" style="width: 210px">
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="LTotal3" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                    font-size: x-small; font-weight: bold"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>                                                       
                                                    </table>  
                                                                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" 
                                                    style="font-family: Calibri; font-weight: 700; font-size: small;">
                                                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Dibuat 
                                                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; DiPeriksa
                                                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Mengetahui
                                                    &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; Menyetujui
                                                </td>                                                
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="font-family: Calibri; font-weight: 700">
                                                 &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;Adm Finishing   
                                                 &emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;Staff Adm Fin
                                                 &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;Mgr Finishing
                                                 &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;Corp Plant Manager   
                                                  <%--&nbsp;&nbsp;&nbsp;    
                                                          Staff Adm Fin     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
                                                          Mgr Finishing      
                                                        Corp Plant Manager--%>
                                                </td>                                                
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                               
                                
                                <asp:Panel ID="PanelListPlank" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 600px; width: 100%; float: left;" id="Div2"
                                        runat="server">
                                        <div id="Div3" runat="server" style="width: 55%; display: inline; vertical-align: top;
                                            table-layout: fixed;">
                                            <table id="tbl2" style="width: 80%;" runat="server">
                                                <tr style="width: 100%;">
                                                    <td style="width: 100%; float: left;">
                                                        <asp:Panel ID="Panel4" runat="server" Visible="true">
                                                            <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                <tr>
                                                                    <td style="font-size: small" colspan="5" align="left">
                                                                        PT. BANGUNPERKASA ADHITAMASENTRA
                                                                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
                                                                        &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
                                                                       <%-- &emsp;&emsp;&emsp;&emsp;&emsp;--%>
                                                                        <asp:Label ID="LabelNoFormLP" runat="server" Visible="false" Style="font-family: Calibri;
                                                                                 font-size: x-small;"></asp:Label>
                                                                    </td>
                                                                </tr>                                                              
                                                                <tr>
                                                                    <td style="font-size: x-small" align="left" colspan="3">                                                                      
                                                                        <asp:Label ID="LabelPeriodeLP" runat="server" Visible="false" Style="font-family: Calibri;
                                                                            font-size: small;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size: small">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        I. LISTPLANK
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <table border="0" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                            font-family: Calibri;">
                                                            <thead>
                                                                <tr class="tbHeader" style="width: 100%;">
                                                                    <th rowspan="2" class="kotak" style="width: 20%;">
                                                                        UTILISASI LISTPLANK
                                                                    </th>
                                                                    <th colspan="2" class="kotak txtUpper" style="width: 40%;">
                                                                        PRODUK BP IN ( M 3 )
                                                                    </th>
                                                                    <th colspan="2" class="kotak txtUpper" style="width: 40%;">
                                                                        PRODUK OK ( M 3 )
                                                                    </th>
                                                                </tr>
                                                                <tr class="tbHeader">
                                                                    <th colspan="1" class="kotak">
                                                                        Lap ListPlank
                                                                    </th>
                                                                    <th colspan="1" class="kotak">
                                                                        Lap BP
                                                                    </th>
                                                                    <th colspan="1" class="kotak">
                                                                        Lap ListPlank
                                                                    </th>
                                                                    <th colspan="1" class="kotak">
                                                                        Lap OK
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="lstLP" runat="server" style="font-family: Calibri">
                                                                <asp:Repeater ID="lstListPlank" runat="server" OnItemDataBound="lstListPlank_DataBound">
                                                                    <ItemTemplate>
                                                                        <tr id="lstlsp" runat="server" class="EvenRows baris">
                                                                            <td class="kotak tengah">
                                                                                &nbsp;
                                                                                <%# Eval("Utilisasi")%>
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <%# Eval("QtyM3_LP", "{0:N2}")%>
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <%# Eval("QtyM3_BP", "{0:N2}")%>
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <%# Eval("QtyM32", "{0:N2}")%>
                                                                            </td>
                                                                            <td class="kotak angka">
                                                                                <%# Eval("QtyM3_OK", "{0:N2}")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                        <asp:Panel ID="Panel5" runat="server" Visible="true">
                                                            <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel6" runat="server" Visible="true">
                                                            <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                <tr>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 50%">
                                                                    <td style="width: 5%; height: 19px;">
                                                                        <asp:Label ID="Label4" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold">Rumus :</asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;" colspan="2">
                                                                        <asp:Label ID="Label5" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold">OK dari BP 8 & 9 mm</asp:Label>
                                                                    </td>
                                                                    <td style="width: 50%; height: 19px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 30%; height: 19px;">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 5%">
                                                                        <asp:Label ID="Label6" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 15%" colspan="3">
                                                                        <asp:Label ID="Label7" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold">BP dari Tahap I + BP dari NC Logistik + BP dari NC Sortir </asp:Label>
                                                                    </td>
                                                                    <td style="width: 50%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 10%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="font-weight: 700; font-family: Calibri; font-size: x-small">
                                                                <tr style="width: 100%">
                                                                    <td style="width: 20%; height: 19px; text-align: center;">
                                                                        <asp:Label ID="LabelLP1L" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold">=</asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: center; font-size: medium; height: 19px; border-bottom-style: groove;">
                                                                        <asp:Label ID="LabelLP1" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 30%; height: 19px;">
                                                                        &nbsp; X 100
                                                                    </td>
                                                                    <td style="width: 40%; height: 19px;">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%">
                                                                        <asp:Label ID="Label10" runat="server" Visible="false" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: center;">
                                                                        <asp:Label ID="LabelLP2" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 30%">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 40%">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 20%; height: 19px; text-align: center;">
                                                                        <asp:Label ID="LabelHasilLP" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold">=</asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left; font-size: medium; height: 19px;">
                                                                        <asp:Label ID="LabelNilaiLP" runat="server" Visible="true" Style="font-family: Calibri;
                                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 30%; height: 19px;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 40%; height: 19px;">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            
                                                            
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
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
