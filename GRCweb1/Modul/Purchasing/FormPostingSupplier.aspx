<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPostingSupplier.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPostingSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server"  class="table-responsive" style="width:100%">
                <table style="height="100%" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td >

                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        <strong>&nbsp;PERIODE POSTING</strong></td>
                                    <td style="width: 100%"></td>
                                    <td style="width: 37px">&nbsp;</td>
                                    <td style="width: 75px">&nbsp;</td>
                                    <td style="width: 5px">&nbsp;</td>
                                    <td style="width: 70px">&nbsp;</td>
                                    <td style="width: 70px">&nbsp;</td>
                                    <td style="width: 70px">&nbsp;</td>
                                    <td style="width: 70px">&nbsp;</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="1" cellpadding="0" cellspacing="0" class="nbTable1" cols="1" height="100%" width="100%">
                                <tr>
                                    <td>

                                        <table id="Table4" border="0" cellpadding="0" cellspacing="1" class="tblForm" style="left: 0px; top: 0px; width: 103%;">
                                            <tr>
                                                <td style="width: 197px; height: 3px" valign="top"></td>
                                                <td style="width: 204px; height: 3px" valign="top"></td>
                                                <td style="height: 3px; width: 169px;" valign="top"></td>
                                                <td style="width: 209px; height: 3px" valign="top"></td>
                                                <td style="width: 205px; height: 3px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 6px" valign="top"><span style="font-size: 10pt">&nbsp; Bulan</span></td>
                                                <td rowspan="1" style="width: 204px; height: 19px">
                                                    <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px">
                                                        <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                        <asp:ListItem>Januari</asp:ListItem>
                                                        <asp:ListItem>Februari</asp:ListItem>
                                                        <asp:ListItem>Maret</asp:ListItem>
                                                        <asp:ListItem>April</asp:ListItem>
                                                        <asp:ListItem>Mei</asp:ListItem>
                                                        <asp:ListItem>Juni</asp:ListItem>
                                                        <asp:ListItem>Juli</asp:ListItem>
                                                        <asp:ListItem>Agustus</asp:ListItem>
                                                        <asp:ListItem>September</asp:ListItem>
                                                        <asp:ListItem>Oktober</asp:ListItem>
                                                        <asp:ListItem>November</asp:ListItem>
                                                        <asp:ListItem>Desember</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 169px; height: 19px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <input id="btnPosting" runat="server" onserverclick="btnPrint_ServerClick"
                                                                            style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; font-style: italic;"
                                                                            type="button" value="Posting" />
                                                    &nbsp;</td>
                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 6px" valign="top"><span style="font-size: 10pt">&nbsp; Tahun</span></td>
                                                <td rowspan="1" style="width: 204px; height: 19px">
                                                    <asp:TextBox ID="txtTahun" runat="server" BorderStyle="Groove" Height="22px" Width="95px">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 169px; height: 19px">&nbsp;</td>
                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">&nbsp; &nbsp;</td>
                                                <td style="width: 204px; height: 19px;">&nbsp;</td>
                                                <td style="width: 169px; height: 19px">&nbsp;</td>
                                                <td style="width: 209px; height: 19px">&nbsp;</td>
                                                <td style="width: 205px; height: 19px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">
                                                    <asp:CheckBox ID="chkActive" runat="server" Font-Size="XX-Small" Text="Active" />
                                                </td>
                                                <td rowspan="1" style="width: 204px; height: 19px;"></td>
                                                <td style="width: 169px; height: 19px"></td>
                                                <td style="width: 209px; height: 19px"></td>
                                                <td style="width: 205px; height: 19px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">&nbsp;</td>
                                                <td style="width: 204px; height: 19px;">&nbsp;</td>
                                                <td style="width: 169px; height: 19px">&nbsp;</td>
                                                <td style="width: 209px; height: 19px">&nbsp;</td>
                                                <td style="width: 205px; height: 19px">&nbsp;</td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
