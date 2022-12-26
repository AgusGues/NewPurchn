<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPES.aspx.cs" Inherits="GRCweb1.Modul.ISO.LapPES" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label,td,span{font-size:12px;}
        table,tr,td{background-color: #fff;}
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <link href="../../Scripts/text.css" rel="stylesheet" type="text/css" />
            <table style="width: 100%; font-size: x-small; height: 100%;">
                <tbody>
                    <tr valign="top">
                        <td style="height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        &nbsp;&nbsp;REKAP
                                        <%=(Request.QueryString["p"]==null)?"KPI":Request.QueryString["p"].ToString() %>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" valign="top">
                            <div style="width: 100%;" class="content">
                                <table id="Table1" style="width: 100%; font-size: x-small; border-collapse: collapse"
                                    rule="all">
                                    <tr>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Departement
                                        </td>
                                        <td style="width: 30%">
                                            <asp:DropDownList AutoPostBack="true" ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedChange">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Periode
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
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
                                            PIC
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPIC" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp
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
                                            <asp:Button ID="btnTest" runat="server" Text="Scroll" Visible="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 100%" id="lst" runat="server">
                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                display: block" border="0">
                                                <thead>
                                                    <tr class=" tbHeader baris">
                                                        <th class="kotak" rowspan="2" style="width: 4%">
                                                            No.
                                                        </th>
                                                        <th class="kotak" rowspan="2" style="width: 30%">
                                                            <%=Request.QueryString["p"].ToString()%>
                                                            Description
                                                        </th>
                                                        <th class="kotak" rowspan="2" style="width: 5%">
                                                            Bobot
                                                        </th>
                                                        <th class="kotak" rowspan="2" style="width: 15%">
                                                            Target
                                                        </th>
                                                        <th class="kotak" colspan="3" id="bln">
                                                            <%=Bulan %>
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader baris">
                                                        <th class="kotak" style="width: 8%">
                                                            Pencapaian
                                                        </th>
                                                        <th class="kotak" style="width: 5%">
                                                            Score
                                                        </th>
                                                        <th class="kotak" style="width: 10%">
                                                            Point
                                                        </th>
                                                    </tr>
                    </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="lstH" runat="server" OnItemDataBound="lstH_DataBound">
                            <ItemTemplate>
                                <tr class="OddRows baris">
                                    <td class="kotak tengah">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td class="kotak" colspan="6">
                                        <b>
                                            <%# Eval("PIC") %>
                                            -
                                            <%# Eval("BagianName") %></b>
                                    </td>
                                </tr>
                                <asp:Repeater ID="lstRkp" runat="server" OnItemDataBound="lstPES1_DataBound">
                                    <ItemTemplate>
                                        <tr class=" EvenRows baris">
                                            <td class="kotak angka">
                                                <%# Container.ItemIndex+1 %>&nbsp;
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("SOPName") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("BobotNilai", "{0:N0}")%>%
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("Target") %>
                                            </td>
                                            <td class="kotak tengah ">
                                                <%# Eval("Pencapaian")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="sc" runat="server" Text='<%# Eval("Score", "{0:N0}")%>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Nilai", "{0:N2}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="lstTot" runat="server">
                                    <ItemTemplate>
                                        <tr class="total">
                                            <td colspan="2" class="angka kotak">
                                                <b>Total</b>&nbsp;
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TotalBobot","{0:N0}") %>%
                                            </td>
                                            <td class="kotak" colspan="3">
                                                &nbsp;
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TotalNilai","{0:N2}") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
            </table>
            </div>
            <div id="DivFooterRow" style="overflow: hidden">
            </div>
            </div> </div> </div> </td> </tr> </tbody> </table>
            <%--</div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
