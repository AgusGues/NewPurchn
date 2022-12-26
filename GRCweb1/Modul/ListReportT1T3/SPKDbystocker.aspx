<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SPKDbystocker.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.SPKDbystocker" %>
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
                DivHR.style.width = '98.5%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                //***DivHR.style.verticalAlign = 'top';

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
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td colspan="5">
                REKAP</td>
        </tr>
        <tr>
            <td style="width: 82px">
                Tanggal
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" 
                    Width="151px" ontextchanged="txtdrtanggal_TextChanged"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 82px">
                Stocker</td>
            <td colspan="3">
                                            <asp:DropDownList ID="ddlStocker" runat="server" 
                    AutoPostBack="true" Width="60%">
                                            </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 82px; height: 19px;">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" />
            </td>
            <td style="width: 158px; height: 19px;">
                &nbsp;</td>
            <td style="width: 78px; height: 19px;">
            </td>
            <td style="width: 151px; height: 19px;">
                &nbsp;</td>
            <td style="height: 19px" align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" ScrollBars="Vertical"
                    Wrap="False" Height="500px">
                    Rekap SPKD per Stocker<br />
                    Tanggal &nbsp;:
                            <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                    <br />
                    <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow">
                           
                        </div>
                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">
                                <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />
                            </asp:GridView>
                        </div>
                        <div id="DivFooterRow" style="overflow: hidden">
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
