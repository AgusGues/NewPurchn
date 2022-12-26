<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapImprovement_Rev1.aspx.cs" Inherits="GRCweb1.Modul.Mtc.LapImprovement_Rev1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <asp:Panel ID="PanelIMP" runat="server" Visible="true" Style="font-family: Calibri">
                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                        <tr>
                            <td style="width: 100%">
                                <table class="nbTableHeader" width="100%'">
                                    <tr style="height: 49px">
                                        <td style="width: 50%">
                                            <strong>&nbsp;List Improvement</strong>
                                        </td>
                                        <td style="width: 50%; padding-right: 10px" align="right">
                                            <asp:Button ID="btnToForm" runat="server" Text="Form Input" OnClick="btnToForm_Click"
                                                Style="font-family: Calibri" />
                                            <asp:TextBox ID="txtCari" Width="200px" Text="Find by Improvement" onfocus="if(this.value==this.defaultValue)this.value='';"
                                                onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Improvement"
                                                Style="font-family: Calibri"></asp:TextBox>
                                            <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" Style="font-family: Calibri" />
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" bgcolor="#3333FF">
                                    <tr style="width: 100%">
                                        <td>
                                            <asp:RadioButton ID="RBM" runat="server" AutoPostBack="True" OnCheckedChanged="RBM_CheckedChanged"
                                                Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic; "
                                                Text="Laporan Project Masuk" TextAlign="Left" />
                                            <asp:RadioButton ID="RBK" runat="server" AutoPostBack="True" OnCheckedChanged="RBK_CheckedChanged"
                                                Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic; "
                                                Text="Laporan Project Keluar" TextAlign="Left" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="content">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td style="width: 3%">
                                                &nbsp;</td>
                                            <td style="width: 10%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 3%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                                <asp:HiddenField ID="txtUrl" runat="server" />
                                            </td>
                                            <td style="font-family: Calibri">
                                                Dept. Penerima
                                            </td>
                                            <td colspan ="5">
                                                <asp:DropDownList ID="ddlP" runat="server" Style="font-family: Calibri" Visible="true">
                                                    <asp:ListItem Value="">-- Pilih Dept --</asp:ListItem>
                                                    <asp:ListItem Value="19">MAINTENANCE</asp:ListItem>
                                                    <asp:ListItem Value="7">HRD GA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="font-family: Calibri">
                                                Status Project
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Style="font-family: Calibri"
                                                    OnTextChanged="ddlStatus_change">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value="0">Open</asp:ListItem>
                                                    <asp:ListItem Value="4">Tgl Target</asp:ListItem>
                                                    <asp:ListItem Value="2">Release</asp:ListItem>
                                                    <asp:ListItem Value="21">Finish</asp:ListItem>
                                                    <asp:ListItem Value="22">Serah Terima Mgr Peminta</asp:ListItem>
                                                    <asp:ListItem Value="3">Close</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
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
                                            <td>
                                                <asp:Label ID="LabelPeriode" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="5"> 
                                                <asp:DropDownList ID="ddlBulan" runat="server" Style="font-family: Calibri" Visible="false">
                                                    <asp:ListItem Value="">-- Pilih Bulan --</asp:ListItem>
                                                    <asp:ListItem Value="1">Januari</asp:ListItem>
                                                    <asp:ListItem Value="2">Februari</asp:ListItem>
                                                    <asp:ListItem Value="3">Maret</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">Mei</asp:ListItem>
                                                    <asp:ListItem Value="6">Juni</asp:ListItem>
                                                    <asp:ListItem Value="7">Juli</asp:ListItem>
                                                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlTahun" runat="server" Style="font-family: Calibri" Visible="false">
                                                    <asp:ListItem Value="">-- Pilih Tahun --</asp:ListItem>
                                                    <asp:ListItem Value="2018">2018</asp:ListItem>
                                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022">2022</asp:ListItem>
                                                    <asp:ListItem Value="2023">2023</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                    <%--<asp:ListItem Value="-1">Cancel</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td style="font-family: Calibri">
                                                Dept. Pemohon
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDeptName" runat="server" Style="font-family: Calibri">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td style="font-family: Calibri">
                                                Tanggal Mulai</td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" 
                                                    BorderStyle="Groove" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                    Format="dd-MMM-yyyy" TargetControlID="txtTanggal">
                                                </cc1:CalendarExtender>
                                                &nbsp;s/d
                                                <asp:TextBox ID="txtTanggal0" runat="server" AutoPostBack="True" 
                                                    BorderStyle="Groove" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTanggal0_CalendarExtender" runat="server" 
                                                    Format="dd-MMM-yyyy" TargetControlID="txtTanggal0">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                    Style="font-family: Calibri" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"
                                                    Style="font-family: Calibri" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height: 440px; overflow: auto" id="lst" runat="server"
                                        onscroll="setScrollPosition(this.scrollTop);">
                                        <table style="width: 150%; border-collapse: collapse; font-size: x-small" border="0">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" style="width: 2%; font-family: Calibri;">
                                                        No.
                                                    </th>
                                                    <th class="kotak" style="width: 4%; font-family: Calibri;">
                                                        Nomor
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Tgl Mulai
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Target Selesai
                                                    </th>
                                                    <th class="kotak" style="width: 14%; font-family: Calibri;">
                                                        Nama Improvement
                                                    </th>
                                                    <th class="kotak" style="width: 18%; font-family: Calibri;">
                                                        Target / Tujuan
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Area Improvment
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Estimasi Biaya
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Biaya Aktual
                                                    </th>
                                                    <th class="kotak" style="width: 5%; font-family: Calibri;">
                                                        Apv
                                                    </th>
                                                    <th class="kotak" style="width: 8%; font-family: Calibri;">
                                                        Status
                                                    </th>
                                                    <th class="kotak" style="width: 6%; font-family: Calibri;">
                                                        Sasaran
                                                    </th>
                                                    <th class="kotak" style="width: 8%; font-family: Calibri;">
                                                        Pemohon
                                                    </th>
                                                    <%--<th class="kotak"style="width:4%"></th>--%>
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: Calibri">
                                                <asp:Repeater ID="lstProject" runat="server" OnItemDataBound="lstProjcet_DataBound"
                                                    OnItemCommand="lstProject_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="brs" runat="server" title='<%# Eval("ID") %>'>
                                                            <td class="kotak tengah">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("Nomor") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("FromDate2","{0:d}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%--<%# Eval("ToDate2", "{0:d}")%>--%>
                                                                <asp:Label ID="txtDate" runat="server" Text='<%# Eval("ToDate2", "{0:d}") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                <asp:Label ID="dsc" runat="server" Visible="false" Text='<%# Eval("NamaProject") %>'></asp:Label>
                                                                <asp:LinkButton ID="lnk" runat="server" Text='<%# Eval("NamaProject") %>' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                            </td>
                                                            <td class="kotak">
                                                                <%# Eval("DetailSasaran") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("AreaImprove" )+ " - " + Eval("Zona") %>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("Biaya","{0:N0}") %>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtAktual" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtApv" runat="server" Text='<%# Eval("Approval") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtSts" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Sasaran") %>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("DeptName") %>
                                                            </td>
                                                            <%--<td class="kotak tengah">
                                                    </td>--%>
                                                        </tr>
                                                        <!--Estimasi Material-->
                                                        <asp:Repeater ID="lstEstimasi" runat="server">
                                                            <HeaderTemplate>
                                                                <tr style="height: 3px" class="total">
                                                                    <th colspan="11" class="kotak">
                                                                    </th>
                                                                </tr>
                                                                <tr class="EvenRows">
                                                                    <th class="kotak" rowspan="2">
                                                                        #
                                                                    </th>
                                                                    <th class="Kotak" rowspan="2">
                                                                        ItemCode
                                                                    </th>
                                                                    <th class="kotak" rowspan="2" colspan="4">
                                                                        ItemName
                                                                    </th>
                                                                    <th class="kotak" colspan="2">
                                                                        Planing
                                                                    </th>
                                                                    <th class="kotak" colspan="2">
                                                                        Actual
                                                                    </th>
                                                                    <th class="kotak" rowspan="2">
                                                                        Selisih
                                                                    </th>
                                                                </tr>
                                                                <tr class="EvenRows">
                                                                    <th class="kotak">
                                                                        Qty
                                                                    </th>
                                                                    <th class="kotak">
                                                                        Total Harga
                                                                    </th>
                                                                    <th class="Kotak">
                                                                        Qty
                                                                    </th>
                                                                    <th class="kotak">
                                                                        Total Harga
                                                                    </th>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="OddRows baris">
                                                                    <td class="kotak angka">
                                                                        <%# Container.ItemIndex+1 %>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        '<%# Eval("ItemCode") %></td>
                                                                    <td class="kotak" colspan="4">
                                                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                                            <tr>
                                                                                <td style="width: 75%">
                                                                                    <%# Eval("ItemName") %>
                                                                                </td>
                                                                                <td style="width: 20%; text-align: right;" class="angka">
                                                                                    <%# Eval("UomCode") %>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("Jumlah","{0:N2}") %>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("PricePlanning","{0:N2}") %>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("QtyAktual","{0:N2}") %>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("Avgprice", "{0:N2}")%>
                                                                    </td>
                                                                    <td class="kotak angka">
                                                                        <%# Eval("Harga", "{0:N2}")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <tr class="total" style="height: 5px">
                                                                    <td colspan="11">
                                                                    </td>
                                                                </tr>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
