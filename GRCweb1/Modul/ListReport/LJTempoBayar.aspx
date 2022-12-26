<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LJTempoBayar.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LJTempoBayar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }     
          
    </script>  
    <script type="text/javascript">
        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function Cetak() {
            MyPopUpWin("../Report/Report.aspx?IdReport=LJTempoBayar", 900, 800)
        }
        function CetakPPn() {
            MyPopUpWin("../Report/Report.aspx?IdReport=LPPnBM", 900, 800)
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed; width:100%;">
                    <tbody>
                        <tr>
                            <td style="height: 49px; width: 100%">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="">
                                            <strong>&nbsp;LAPORAN JATUH TEMPO PEMBAYARAN</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color: #B0C4DE">
                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                        <tr>
                                            <td style="height: 3px" valign="top" colspan="4">
                                                <span style="font-size: 10pt">&nbsp; Kriteria pengambilan Data&nbsp;&nbsp; </span>
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" Font-Size="X-Small"
                                                    GroupName="a" Text="Tanggal Receipt" />
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton2" runat="server" Font-Size="X-Small" GroupName="a"
                                                    Text="Tanggal PO" />
                                                &nbsp;<asp:RadioButton ID="RadioButton3" runat="server" Font-Size="X-Small" GroupName="a"
                                                    Text="Tanggal Aprooval Plant" />
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton4" runat="server" Font-Size="X-Small" GroupName="a"
                                                    Text="PPnBM" />
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 19px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Per Tanggal</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 209px; height: 19px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; S/D Tanggal</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px;">
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" 
                                                    style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Preview" />
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
