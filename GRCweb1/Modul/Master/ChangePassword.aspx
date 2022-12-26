<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="GRCweb1.Modul.Master.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nbTableHeader">
        <tr>

            <td style="width: 218%">
                <strong>UBAH PASSWORD DAN ALAMAT E-MAIL</strong></td>
            <td style="width: 37px"></td>

            <td>
                <table class="nbTable1">
                    <tr>
                        <td style="width: 5px"></td>
                        <td></td>
                        <td>
                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                        <td style="width: 3px">
                            <input id="btnSearch" runat="server"
                                type="button" value="Cari" style="color: #000000" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div>
        <table class="nbTable1" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
            cellpadding="0" border="0">
            <tr>
                <td style="width: 156px; height: 3px" valign="top"></td>
                <td style="width: 204px; height: 3px" valign="top"></td>
                <td style="height: 3px; width: 169px;" valign="top"></td>
                <td style="width: 209px; height: 3px" valign="top"></td>
                <td style="width: 205px; height: 3px" valign="top"></td>
            </tr>
            <tr>
                <td style="width: 156px; height: 6px" valign="top">
                    <span style="font-size: 10pt">&nbsp; User ID</span></td>
                <td style="width: 204px; height: 6px" valign="top">
                    <asp:TextBox ID="txtUserID" runat="server" BorderStyle="Groove"
                        Width="233" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 169px;" valign="top">&nbsp;</td>
                <td style="width: 209px; height: 6px" valign="top"></td>
                <td style="width: 205px; height: 6px" valign="top"></td>
            </tr>
            <tr>
                <td style="width: 156px; height: 6px" valign="top">
                    <span style="font-size: 10pt">&nbsp; User Name</span></td>
                <td style="width: 204px; height: 6px" valign="top">
                    <asp:TextBox ID="txtUserName" runat="server" BorderStyle="Groove"
                        Width="233" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="height: 6px; width: 169px; font-size: x-small;" valign="top"
                    align="right">Alamat E-Mail</td>
                <td style="width: 209px; height: 6px" valign="top">
                    <asp:TextBox ID="txtUsrEmail" runat="server" BorderStyle="Groove" Width="233"
                        AutoPostBack="True" OnTextChanged="txtUsrEmail_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 205px; height: 6px" valign="top"></td>
            </tr>
            <tr>
                <td style="width: 197px; height: 6px" valign="top">
                    <span style="font-size: 10pt">&nbsp; Password</span></td>
                <td style="height: 6px; width: 204px;" valign="top">
                    <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" Width="233"
                        TextMode="Password"></asp:TextBox></td>
                <td style="height: 6px; width: 169px; font-size: x-small;" valign="top"
                    align="right">Password E-Mail</td>
                <td style="width: 209px; height: 6px" valign="top">
                    <asp:TextBox ID="txtPassEmail" runat="server" BorderStyle="Groove"
                        TextMode="Password" Width="233"></asp:TextBox>
                </td>
                <td style="width: 205px; height: 6px" valign="top"></td>
            </tr>
            <tr>
                <td style="width: 156px; height: 6px" valign="top">
                    <input id="btnChange" runat="server"
                        style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                        type="button" value="Change" onserverclick="btnChange_ServerClick" />
                </td>
                <td style="width: 204px; height: 6px" valign="top"></td>
                <td style="width: 169px; height: 19px"></td>
                <td style="width: 209px; height: 19px"></td>
                <td style="width: 205px; height: 19px"></td>
            </tr>
            <tr>
                <td style="width: 156px; height: 6px" valign="top"></td>
                <td style="width: 204px; height: 6px" valign="top"></td>
                <td style="width: 169px; height: 19px"></td>
                <td style="width: 209px; height: 19px"></td>
                <td style="width: 205px; height: 19px"></td>
            </tr>
        </table>
    </div>
</asp:Content>
