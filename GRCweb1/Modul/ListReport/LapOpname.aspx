<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapOpname.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapOpname" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
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
            MyPopUpWin("../Report/Report.aspx?IdReport=LapOpname", 900, 800)
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed; height: 100%; width: 100%">
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%">
                                                <strong>&nbsp;LEMBAR PEMANTAUAN KETIDAKSESUAIAN STOCK INVENTORY</strong> &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">

                                <table class="tblForm" id="Table4" style="width: 100%; font-size: x-small;">
                                    <tr>
                                        <td>&nbsp; Periode Opname</td>
                                        <td>
                                            <asp:DropDownList ID="ddlBulan1" runat="server">
                                                <asp:ListItem Value="0">---Silahkan Pilih---</asp:ListItem>
                                                <asp:ListItem Value="JANUARI">Januari</asp:ListItem>
                                                <asp:ListItem Value="FEBRUARI">Februari</asp:ListItem>
                                                <asp:ListItem Value="MARET">Maret</asp:ListItem>
                                                <asp:ListItem Value="APRIL">April</asp:ListItem>
                                                <asp:ListItem Value="MEI">Mei</asp:ListItem>
                                                <asp:ListItem Value="JUNI">Juni</asp:ListItem>
                                                <asp:ListItem Value="JULI">Juli</asp:ListItem>
                                                <asp:ListItem Value="AGUSTUS">Agustus</asp:ListItem>
                                                <asp:ListItem Value="SEPTEMBER">September</asp:ListItem>
                                                <asp:ListItem Value="OKTOBER">Oktober</asp:ListItem>
                                                <asp:ListItem Value="NOVEMBER">November</asp:ListItem>
                                                <asp:ListItem Value="DESEMBER">Desember</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 250px;">&nbsp; Bulan</td>
                                        <td rowspan="1" style="width: 250px;">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Width="140px">
                                                <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
                                                <asp:ListItem Value="DesQty">Januari</asp:ListItem>
                                                <asp:ListItem Value="JanQty">Februari</asp:ListItem>
                                                <asp:ListItem Value="FebQty">Maret</asp:ListItem>
                                                <asp:ListItem Value="MarQty">April</asp:ListItem>
                                                <asp:ListItem Value="AprQty">Mei</asp:ListItem>
                                                <asp:ListItem Value="MeiQty">Juni</asp:ListItem>
                                                <asp:ListItem Value="JunQty">Juli</asp:ListItem>
                                                <asp:ListItem Value="JulQty">Agustus</asp:ListItem>
                                                <asp:ListItem Value="AguQty">September</asp:ListItem>
                                                <asp:ListItem Value="SepQty">Oktober</asp:ListItem>
                                                <asp:ListItem Value="OktQty">November</asp:ListItem>
                                                <asp:ListItem Value="NovQty">Desember</asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 92px; font-weight: normal; font-size: x-small;">&nbsp;<span style="font-weight: bold; display: none"> Tahun</span>
                                        </td>
                                        <td rowspan="1" style="width: 655px;">
                                            <asp:TextBox ID="txtTahun" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Width="84px" Style="font-size: x-small" Visible="false"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="color: #0033CC;"><b>&nbsp; Periode Cutt Off&nbsp;</b></td>
                                        <td style="width: 254px;"></td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right"><span style="font-weight: bold;">Awal</span>&nbsp;&nbsp;</td>
                                        <td rowspan="1" style="width: 278px;">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="200px"
                                                Style="font-size: x-small"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtToPostingPeriod"></cc1:CalendarExtender>
                                        </td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td align="right"><span style="font-weight: bold;">Akhir</span>&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"
                                                Style="font-size: x-small"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtFromPostingPeriod"></cc1:CalendarExtender>
                                        </td>
                                        <td>&nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp; Group Purchn</td>
                                        <td>
                                            <asp:DropDownList ID="ddlGroup" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <%--<td>&nbsp;&nbsp;</td>--%>

                                        <td style="height: 3px; width: 50px;" valign="top" visible="false">
                                            <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB1_CheckedChanged"
                                                Style="font-family: Calibri; font-size: x-small; text-align: left;" Text="Oil"
                                                TextAlign="Left" Width="300px" />
                                        </td>

                                        <td>
                                                &nbsp;&nbsp;</td>
                                        <td>&nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp; Tanggal Opname</td>
                                        <td>
                                            <asp:TextBox ID="txtFromPostingPeriod1" runat="server" Width="132px"></asp:TextBox></td>
                                        <td>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtFromPostingPeriod1"></cc1:CalendarExtender>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Preview" />
                                            <%--<input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white;
                                                    font-weight: bold; font-size: 11px; height: 22px;" type="button" value="Preview" />--%>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
