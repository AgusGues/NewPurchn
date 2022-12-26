<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapCustomer.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LapCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            var panelProg = $get('divImage');
            panelProg.style.display = '';
            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = false;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divImage and hide it again
            var panelProg = $get('divImage');
            panelProg.style.display = 'none';
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
                DivHR.style.width = (parseFloat(width-1.5)) + '%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = (width) + '%';
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
            <link href="../../scripts/text.css" rel="stylesheet" type="text/css" />
            <div id="div1" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 60%">
                                        <strong>&nbsp;LAPORAN CUSTOMER </strong>
                                    </td>
                                    <td style="width: 40%; padding-right: 10px" align="right">
                                        <%--<input type="button" id="cc" onclick="displayPopUP()" value="test"; />--%>
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
                                        <td style="padding-left: 10px">
                                            Periode
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true">
                                                <asp:ListItem Value="01">Januari</asp:ListItem>
                                                <asp:ListItem Value="02">Februari</asp:ListItem>
                                                <asp:ListItem Value="03">Maret</asp:ListItem>
                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                <asp:ListItem Value="05">Mei</asp:ListItem>
                                                <asp:ListItem Value="06">Juni</asp:ListItem>
                                                <asp:ListItem Value="07">Juli</asp:ListItem>
                                                <asp:ListItem Value="08">Agustus</asp:ListItem>
                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                <asp:ListItem Value="11">Nopember</asp:ListItem>
                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div style="height: 100%" id="lstr" runat="server">
                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <table id="thr" style="border-collapse: collapse; font-size: x-small; font-family: Arial">
                                                <thead>
                                                    <tr class="tbHeader" id="hd1" runat="server">
                                                        <th class="kotak tengah" rowspan="4">
                                                        Tanggal
                                                        </th>
                                                        <th class="kotak tengah" colspan="8">
                                                        RETAIL    
                                                        </th>
                                                        <th class="kotak tengah" colspan="6">
                                                            PROJECT
                                                        </th>
                                                        <th class="kotak tengah" colspan="4">
                                                            DEPO
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="3">
                                                            LISTPLANK & TIMBERPLANK
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="3">
                                                            >4mm & lainnya
                                                        </th>
                                                        <th class="kotak tengah" colspan="3" rowspan="3">
                                                            Total Customer
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="3">
                                                            Total Produksi
                                                        </th>
                                                    </tr>
                                                    
                                                    <tr class="tbHeader" id="hd2" runat="server">
                                                    <th class="kotak" colspan="2" rowspan="2" style="font-size: xx-small;" >
                                                            4mm 4X8
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;" >
                                                            4mm 1200x2400
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;">
                                                            1000x1000 dan 1000x2000
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;" >
                                                            CLB, SUB, DRG, TGR & JVB
                                                        </th>
                                                        <th class="kotak tengah" colspan="4" style="font-size: xx-small;">
                                                            Belahan
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;">
                                                            Utuhan
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;">
                                                            4mm 4X8 & 1200
                                                        </th>
                                                        <th class="kotak tengah" colspan="2" rowspan="2" style="font-size: xx-small;" >
                                                            1000x1000 dan 1000x2000
                                                        </th>
                                                    </tr>
                                                    
                                                    <tr class="tbHeader" id="hd21" runat="server">
                                                    <th class="kotak tengah" colspan="2" style="font-size: xx-small;" >
                                                            >= 3 MTR
                                                        </th>  
                                                        <th class="kotak tengah" colspan="2" style="font-size: xx-small;">
                                                           < 3 MTR
                                                        </th>
                                                       <%-- <th class="kotak" rowspan="2" style="font-size: xx-small;">
                                                           Utuhan
                                                        </th>--%>
                                                    </tr>
                                                    
                                                    <tr ID="hd3" runat="server" class="tbHeader">
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Lembar
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Konversi 4 4x8
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            Konversi 4 4x8
                                                        </th>
                                                        <th class="kotak tengah" style="font-size: xx-small;">
                                                            M3
                                                        </th>
                                                    </tr>
                                                <tbody>
                                                    <asp:Repeater ID="lst" runat="server" OnItemDataBound="lst_DataBound">
                                                        <ItemTemplate>
                                                            <tr ID="ps1" runat="server" class="EvenRows baris">
                                                                <td class="kotak tengah bold OddRows">
                                                                    <%# Eval("tglkirim", "{0:d}")%>
                                                                </td>
                                                                 <td class="kotak kanan">
                                                                    <%# Eval("K0","{0:N0}") %>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K00", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K1","{0:N0}") %>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K2", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K3", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K4", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K5", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K6", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K7", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K8", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K9", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K10", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K09","{0:N0}") %>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K010", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K11", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K12", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K13", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K14", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K15", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K16", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K17", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K18", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K19", "{0:N0}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K20", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K21", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan OddRows">
                                                                    <%# Eval("K22", "{0:N1}")%>
                                                                </td>
                                                                <td class="kotak kanan">
                                                                    <%# Eval("K23", "{0:N1}")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr ID="ftr" runat="server" class="Line3 baris">
                                                                <td class="kotak bold angka">
                                                                    Total
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                               <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak bold kanan">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                        <div ID="DivFooterRow" style="overflow: hidden">
                                        </div>
                                                    </tr>
                                                    
                                                </thead>
                                            </table>
                                        </div>
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
