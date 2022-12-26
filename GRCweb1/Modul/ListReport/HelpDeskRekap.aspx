<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelpDeskRekap.aspx.cs" Inherits="GRCweb1.Modul.ListReport.HelpDeskRekap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
    <script language="JavaScript">
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
            MyPopUpWin("../Report/Report.aspx?IdReport=LapRekapKom", 900, 800)
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP KELUHAN</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color: #B0C4DE">
                                    <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                        cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 197px; height: 3px" valign="top"></td>
                                            <td style="width: 204px; height: 3px" valign="top"></td>
                                            <td style="height: 3px; width: 169px;" valign="top"></td>
                                            <td style="width: 209px; height: 3px" valign="top"></td>
                                            <td style="width: 205px; height: 3px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Dari Periode Posting</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top"></td>
                                            <td style="width: 205px; height: 6px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Ke Periode Posting</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top"></td>
                                            <td style="width: 205px; height: 6px" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">
                                                <span style="font-size: 10pt">&nbsp; Pilih Line</span></td>
                                            <td rowspan="1" style="width: 204px; height: 19px;">
                                                <asp:DropDownList ID="ddlDeptID" runat="server" Width="233px"></asp:DropDownList></td>

                                            <td style="width: 169px; height: 19px"></td>
                                            <td style="width: 209px; height: 19px"></td>
                                            <td style="width: 205px; height: 19px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;"></td>
                                            <td style="width: 169px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Preview" />
                                            </td>
                                            <td style="width: 169px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;"></td>
                                            <td style="width: 169px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">&nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
