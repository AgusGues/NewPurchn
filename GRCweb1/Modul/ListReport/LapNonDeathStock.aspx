<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapNonDeathStock.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapNonDeathStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
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
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapDeathStock", 900, 800)
        }
    </script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

    </script>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">
                    <asp:Label ID="Label2" runat="server" Text="Dari Tanggal" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Height="22px"
                        Width="95px" Enabled="False" Visible="False"></asp:TextBox>
                </td>
                <td style="width: 169px; height: 19px" valign="top">
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                        Format="dd-MMM-yyyy" TargetControlID="txtTgl1"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">
                    <asp:Label ID="Label3" runat="server" Text="s/d Tanggal" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Height="22px"
                        Width="95px" Enabled="False" Visible="False"></asp:TextBox>
                    &nbsp;&nbsp;
                <asp:RadioButton ID="RB3Bulan" runat="server" AutoPostBack="True"
                    Checked="True" GroupName="1" OnCheckedChanged="RB3Bulan_CheckedChanged"
                    Text="3 Bulan" Visible="False" />
                    &nbsp;<asp:RadioButton ID="RB6Bulan" runat="server" AutoPostBack="True" GroupName="1"
                        OnCheckedChanged="RB6Bulan_CheckedChanged" Text="6 Bulan"
                        Visible="False" />
                </td>
                <td style="width: 169px; height: 19px" valign="top">
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                        Format="dd-MMM-yyyy" TargetControlID="txtTgl2"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">Group </td>
                <td>
                    <asp:DropDownList ID="ddlTipeSPP" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged" Width="233px">
                    </asp:DropDownList>
                </td>
                <td style="width: 169px; height: 19px" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlJenisLapBul" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged" Width="233px"
                        Enabled="False">
                        <asp:ListItem>Non Stok</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 169px; height: 19px" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 31px">&nbsp;</td>
                <td style="width: 144px">
                    <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick"
                        style="background-color: white; font-weight: bold; font-size: 11px;"
                        type="button" value="Cetak" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
