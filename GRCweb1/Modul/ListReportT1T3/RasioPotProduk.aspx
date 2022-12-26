<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RasioPotProduk.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.RasioPotProduk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px; font-family: 'Courier New', Courier, monospace;">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 40%; font-size: medium; font-family: 'Times New Roman', Times, serif;">
                                            <asp:Label ID="labelJudul" runat="server" Visible="true"></asp:Label>
                                        </td>
                                        <td style="width: 60%; text-align: right;">
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content" style="font-family: 'Courier New', Courier, monospace">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <table width="85%" style="border-collapse: collapse; margin-top: 10px">
                                        <tr>
                                            <td style="width: 20%;">
                                                <strong>&nbsp;<span style="font-family: 'Calibri Light'; font-size: x-small">Periode
                                                    :</span><span style="font-size: x-small"> </span></strong></span>
                                                <asp:DropDownList AutoPostBack="True" ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_SelectedChange"
                                                    Style="font-family: Calibri; font-weight: 700">
                                                </asp:DropDownList>
                                                <asp:DropDownList AutoPostBack="True" ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_SelectedChange"
                                                    Style="font-family: Calibri; font-weight: 700">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50%" colspan="5">
                                                <span style="font-family: 'Courier New', Courier, monospace">
                                                    <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Style="font-family: 'Calibri Light';
                                                        font-weight: 700; font-size: x-small;" Text="Preview" />
                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Style="font-family: 'Calibri Light';
                                                        font-weight: 700; font-size: x-small;" Text="Export To Excel" />
                                                </span><strong style="color: #0000FF; font-size: x-small; font-weight: normal; font-style: italic;
                                                    font-variant: normal; text-transform: uppercase;">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <hr />
                                <div class="contentlist" style="overflow: scroll; height: 400px; width: 100%;" id="div2"
                                    runat="server">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: 'Calibri Light';
                                        height: auto;" border="0">
                                        <thead>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblheader" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblperiode" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lbl000" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lbl00" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; height: 17px;" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lbl0" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblP_BP" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblP_BP1" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtP_BP1" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtP_BP2" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblP_OK" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblP_OK1" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtP_OK1" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtP_OK2" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblPersen1" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblNP" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblNP0" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblNP_BP" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblNP_BP0" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtNP_BPm3" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtNP_BPlb" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblNP_OK" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblNP_OK0" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtNP_OKm3" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtNP_OKlb" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblPersenNP1" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblNP01" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblNP001" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>   --%>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblNP_BP2" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblNP_BP20" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtNP_BP2m3" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtNP_BP2lb" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">
                                                    <asp:Label ID="lblNP_OK2" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:Label ID="lblNP_OK20" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                                <td style="width: 7%; text-align: right;">
                                                    <asp:Label ID="txtNP_OK2m3" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 14%; text-align: left;">
                                                    <asp:Label ID="txtNP_OK2lb" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 60%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblPersenNP2" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    <asp:Label ID="lblPersenTotal" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="5">
                                                     <asp:Label ID="lblinfo" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="width: 100%" colspan="5">
                                                     <asp:Label ID="lblinfo1" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="width: 100%" colspan="5">
                                                     <asp:Label ID="lblinfo2" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td style="width: 100%" colspan="5">
                                                     <asp:Label ID="lblinfo3" runat="server" Visible="true">&nbsp;</asp:Label>
                                                </td>
                                            </tr>
                                        </thead>
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
