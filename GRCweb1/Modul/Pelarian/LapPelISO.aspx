<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPelISO.aspx.cs" Inherits="GRCweb1.Modul.Pelarian.LapPelISO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>

                                            <td style="width: 100%">
                                                <strong>&nbsp;Laporan Pelarian Format ISO</strong>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td height="100%" style="width: 100%">
                                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
                                    <tbody>
                                        <tr class="treeRow1" valign="top">

                                            <td style="height: 101px; width: 100%;">
                                                <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                                    cellpadding="0" border="0">
                                                   
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
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Dari Periode</span>
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
                                                            <span style="font-size: 10pt">&nbsp; Ke Periode</span>
                                                        </td>
                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 169px; height: 19px">
                                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod"
                                                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Pilih Line</span>
                                                        </td>
                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                            <asp:DropDownList ID="ddlPlanID" runat="server" Width="233px" AutoPostBack="True">
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
                                                        <td style="width: 197px; height: 19px">&nbsp;
                                                        </td>
                                                        <td rowspan="1" style="width: 204px; height: 19px;"></td>
                                                        <td style="width: 169px; height: 19px"></td>
                                                        <td style="width: 209px; height: 19px"></td>
                                                        <td style="width: 205px; height: 19px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 19px">&nbsp;
                                                        </td>
                                                        <td style="width: 204px; height: 19px;">
                                                            <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Preview" />
                                                        </td>
                                                        <td style="width: 169px; height: 19px">&nbsp;
                                                        </td>
                                                        <td style="width: 209px; height: 19px">&nbsp;
                                                        </td>
                                                        <td style="width: 205px; height: 19px">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr width="100%" size="1">
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
