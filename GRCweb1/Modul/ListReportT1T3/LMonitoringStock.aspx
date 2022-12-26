<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LMonitoringStock.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LMonitoringStock" %>
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
                DivFR.style.width = width + '%';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '95%';
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
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive">
                <table width="100%" align="center">
                    <tr>
                        <td align="left" colspan="2">
                            <strong>MONITORING STOCK PRODUK</strong>
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small; font-weight: bold;" align="center" colspan="3">
                            Periode&nbsp;
                            <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" 
                                Width="151px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                            Format="dd-MMM-yyyy" TargetControlID="txtdrtanggal">
                        </cc1:CalendarExtender>
                            &nbsp;s/d&nbsp;
                        <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" 
                                Width="151px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" 
                            Format="dd-MMM-yyyy" TargetControlID="txtsdtanggal">
                        </cc1:CalendarExtender>
                        &nbsp;<asp:Button ID="Button1" runat="server" Height="21px" onclick="Button1_Click" 
                                Text="Refresh" />
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small; font-weight: bold;">
                            Rekap Monitoring</td>
                        <td style="font-size: x-small; font-weight: bold;">
                            <asp:Label ID="LUpdate" runat="server"></asp:Label>
                        </td>
                        <td style="font-size: x-small; font-weight: bold;" align="right">
                            <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">Export  To Excel</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small; font-weight: bold;" colspan="3">
                            <div style="overflow: scroll; height: 150px;" id="DivRekapContent">
                                <asp:GridView ID="GrdDynamic0" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                    HorizontalAlign="Center" OnRowCreated="GrdDynamic0_RowCreated" PageSize="20"
                                    Style="margin-right: 0px" Width="98%" 
                                    onrowdatabound="GrdDynamic0_RowDataBound">
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                        Font-Names="tahoma" Font-Size="XX-Small" />
                                    <PagerStyle BorderStyle="Solid" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small; font-weight: bold;" colspan="2">
                            List Monitoring Detail
                            <asp:RadioButton ID="RBJenis0" runat="server" AutoPostBack="True" 
                                Checked="True" GroupName="a" oncheckedchanged="RBJenis0_CheckedChanged" 
                                Text="WIP" />
                            &nbsp;<asp:RadioButton ID="RBJenis1" runat="server" AutoPostBack="True" 
                                GroupName="a" oncheckedchanged="RBJenis1_CheckedChanged" Text="OK" />
                            &nbsp;<asp:RadioButton ID="RBJenis2" runat="server" AutoPostBack="True" 
                                GroupName="a" oncheckedchanged="RBJenis2_CheckedChanged" Text="M" />
                            &nbsp;<asp:RadioButton ID="RBJenis3" runat="server" AutoPostBack="True" 
                                GroupName="a" oncheckedchanged="RBJenis3_CheckedChanged" Text="BP" />
                            &nbsp;<asp:RadioButton ID="RBJenis4" runat="server" AutoPostBack="True" 
                                GroupName="a" oncheckedchanged="RBJenis4_CheckedChanged" Text="BS" />
                            &nbsp;<asp:RadioButton ID="RBJenis5" runat="server" AutoPostBack="True" 
                                GroupName="a" oncheckedchanged="RBJenis5_CheckedChanged" Text="EFO" />
                        </td>
                        <td style="font-size: x-small; font-weight: bold;" align="right">
                            <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">Export  To Excel</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="DivRoot" align="left">
                                <div style="overflow: hidden; background-color: #FFFFFF; color: #FFFFFF;" 
                                    id="DivHeaderRow">
                                </div>
                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                        HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated"
                                        PageSize="20" Style="margin-right: 0px" Width="98%" 
                                        onrowdatabound="GrdDynamic_RowDataBound">
                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                            Font-Names="tahoma" Font-Size="XX-Small" />
                                        <PagerStyle BorderStyle="Solid" />
                                    </asp:GridView>
                                </div>
                                <div id="DivFooterRow" style="overflow: hidden">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
