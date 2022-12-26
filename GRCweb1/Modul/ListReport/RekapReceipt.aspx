<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapReceipt.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapReceipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function Cetak() {
            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=RekapReceipt", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
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
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapReceipt", 900, 800)
        }
        function CetakExcel() {
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapReceiptExcel", 900, 800)
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
                                            <strong>&nbsp;LAP REKAP RECEIPT</strong>
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
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                                            </td>
                                            <td style="width: 215px">
                                                <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Width="164px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl1">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                                            </td>
                                            <td style="width: 215px">
                                                <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Width="164px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl2">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label1" runat="server" Text="Group Receipt By"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="group1" OnCheckedChanged="RadioButton1_CheckedChanged" Text="Group Purchn" />
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" GroupName="group1"
                                                    OnCheckedChanged="RadioButton2_CheckedChanged" Text="Doc No Prefix" />
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="True" GroupName="group1"
                                                    OnCheckedChanged="RadioButton3_CheckedChanged" Text="Item Code" />
                                                &nbsp;
                                                <asp:RadioButton ID="RadioButton4" runat="server" AutoPostBack="True" GroupName="group1"
                                                    OnCheckedChanged="RadioButton4_CheckedChanged" Text="Supplier" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlGroup" runat="server" Width="233px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtInput" runat="server" Visible="False" Width="282px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label4" runat="server" Text="Print Out"></asp:Label>
                                            </td>
                                            <td style="width: 215px">
                                                <asp:DropDownList ID="ddlPrint" runat="server" Width="233px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlPrint_SelectedIndexChanged">
                                                    <asp:ListItem>Potrait</asp:ListItem>
                                                    <asp:ListItem>Landscape Excel</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 215px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                            </td>
                                            <td style="height: 19px" colspan="2">
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
