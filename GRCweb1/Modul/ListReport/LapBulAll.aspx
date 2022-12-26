<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapBulAll.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapBulAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript" type="text/javascript"> function imgChange(img) { document.LookUpCalendar.src = img; }</script>
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
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapLapBul", 900, 800)
        }
    </script>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <table>
            <tbody>
                <tr>
                    <td >
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width: 100%">
                                    <strong>&nbsp;LAPORAN BULANAN</strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td >
                        <div style="overflow: auto; height: 100%; width: 100%; background-color: #B0C4DE">
                            <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 100%;" cellspacing="1"
                                cellpadding="0" border="0">
                                <tr>
                                    <td style="width: 238px; height: 3px" valign="top"></td>
                                    <td style="width: 278px; height: 3px" valign="top">
                                        <asp:Panel ID="PanelPost" runat="server" Width="190px">
                                            <asp:Label ID="Label3" runat="server" Font-Size="X-Small" Text="Posting Saldo"></asp:Label>
                                            &nbsp;<asp:RadioButton ID="rbOn" runat="server" Checked="True" Font-Size="X-Small"
                                                GroupName="1" Text="Yes" />
                                            &nbsp;<asp:RadioButton ID="rbOff" runat="server" Font-Size="X-Small" GroupName="1"
                                                Text="No" />
                                            &nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td style="height: 3px; width: 169px;" valign="top">
                                        <asp:DropDownList ID="ddlBulan0" runat="server" Height="19px" Visible="False" Width="200px">
                                            <asp:ListItem Value="0">Pilih Bulan</asp:ListItem>
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
                                    </td>
                                    <td style="width: 209px; height: 3px" valign="top">
                                        <asp:TextBox ID="txtTahun0" runat="server" AutoPostBack="True" OnTextChanged="TxtTahun_TextChanged"
                                            Visible="False" Width="86px"></asp:TextBox>
                                    </td>
                                    <td style="width: 205px; height: 3px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px; font-size: x-small;" valign="top">&nbsp; Periode&nbsp;
                                    </td>
                                    <td style="width: 278px; height: 19px">
                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                                            Width="130px">
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
                                        &nbsp;&nbsp;
                                                <%--<asp:TextBox ID="txtTahun" runat="server" AutoPostBack="True" OnTextChanged="TxtTahun_TextChanged"
                                                    Width="86px"></asp:TextBox>--%>
                                        <asp:DropDownList ID="txtTahun" runat="server" AutoPostBack="True" OnTextChanged="TxtTahun_TextChanged"
                                            Width="86px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 169px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 209px; height: 6px" valign="top">&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 6px" valign="top">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px" valign="top">
                                        <span style="font-size: 10pt">&nbsp; Dari Periode Posting</span>
                                    </td>
                                    <td rowspan="1" style="width: 278px; height: 19px">
                                        <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                    </td>
                                    <td style="width: 169px; height: 19px">
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtFromPostingPeriod"></cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 209px; height: 6px" valign="top"></td>
                                    <td style="width: 205px; height: 6px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px" valign="top">
                                        <span style="font-size: 10pt">&nbsp; Ke Periode Posting</span>
                                    </td>
                                    <td rowspan="1" style="width: 278px; height: 19px">
                                        <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                    </td>
                                    <td style="width: 169px; height: 19px">
                                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 209px; height: 6px" valign="top"></td>
                                    <td style="width: 205px; height: 6px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; font-size: x-small;" valign="top"></td>
                                    <td style="width: 278px;">&nbsp;
                                    </td>
                                    <td style="width: 169px;"></td>
                                    <td style="width: 209px;"></td>
                                    <td style="width: 205px;"></td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px" valign="top">
                                        <span style="font-size: 10pt">&nbsp; Jenis</span>
                                    </td>
                                    <td style="width: 278px; height: 19px;">
                                        <asp:DropDownList ID="ddlJenisLapBul" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged"
                                            Width="233px">
                                            <asp:ListItem>-- Pilih Jenis Report --</asp:ListItem>
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Stock</asp:ListItem>
                                            <asp:ListItem>Non Stock</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 169px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 209px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 19px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px" valign="top">
                                        <span style="font-size: 10pt">&nbsp; Laporan</span>
                                    </td>
                                    <td style="width: 278px; height: 19px;">
                                        <asp:DropDownList ID="ddlTipeSPP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged"
                                            Width="233px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 169px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 209px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 19px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 6px" valign="top">
                                        <span style="font-size: 10pt">&nbsp; Format Print</span>
                                    </td>
                                    <td style="width: 278px; height: 19px;">
                                        <asp:DropDownList ID="ddlFormatPrint" runat="server" Width="233px">
                                            <asp:ListItem>Potrait</asp:ListItem>
                                            <asp:ListItem>Land Scape</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 169px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 209px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 19px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 19px">&nbsp;
                                    </td>
                                    <td rowspan="1" style="width: 278px; height: 19px;"></td>
                                    <td style="width: 169px; height: 19px"></td>
                                    <td style="width: 209px; height: 19px"></td>
                                    <td style="width: 205px; height: 19px"></td>
                                </tr>
                                <tr>
                                    <td style="width: 238px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 278px; height: 19px;">
                                        <%--<input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                                                    background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Preview" />--%>
                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Preview" />
                                    </td>
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
            </tbody>
        </table>
    </div>
</asp:Content>
