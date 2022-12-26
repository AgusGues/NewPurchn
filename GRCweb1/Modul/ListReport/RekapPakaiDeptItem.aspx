<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPakaiDeptItem.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapPakaiDeptItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function Cetak() {

            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=LapRekapPakai", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

    </script>

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
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapPakaiDeptItem", 900, 800)
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server"  class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP PEMAKAIAN PER ITEM</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <div style="width: 100%; background-color: #B0C4DE">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 31px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 144px">
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 31px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 144px">
                                                <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl1">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 31px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 144px">
                                                <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl2">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 31px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 144px">
                                                Department
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="233px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Tipe Barang
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipeBarang" runat="server" AutoPostBack="True" 
                                                    Width="233px" onselectedindexchanged="ddlTipeBarang_SelectedIndexChanged">
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
                                                Pencarian Nama Barang
                                            </td>
                                            <td style="width: 204px; height: 19px" colspan="2">
                                                <asp:TextBox ID="txtCari" runat="server" BorderStyle="Groove" AutoPostBack="true"
                                                    ReadOnly="False" Width="233" OnTextChanged="txtCari_TextChanged" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Nama Item
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="True" Width="600px">
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
                                            <td style="width: 31px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 144px">
                                                <%--<input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-image: url('../../../../GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                                                    background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cetak" />--%>
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Preview" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
