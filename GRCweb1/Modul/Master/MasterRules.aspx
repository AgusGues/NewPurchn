<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterRules.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">

                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;RULES</strong>
                                        </td>
                                        <td style="width: 100%"></td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru"
                                                onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan"
                                                onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <input id="btnDelete" runat="server" style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Hapus"
                                                onserverclick="btnDelete_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnPrint" runat="server" style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cetak"
                                                onclick="Cetak()" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="RuleName">Nama Rule</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari"
                                                onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                <div>
                                    <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td style="height: 101px; width: 100%;">
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
                                                            <span style="font-size: 10pt">&nbsp; Nama Rule</span>
                                                        </td>
                                                        <td style="width: 204px; height: 6px" valign="top">
                                                            <asp:TextBox ID="txtRuleName" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 6px; width: 169px;" valign="top"></td>
                                                        <td style="width: 209px; height: 6px" valign="top"></td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 19px"></td>
                                                        <td rowspan="1" style="width: 204px; height: 19px;"></td>
                                                        <td style="width: 169px; height: 19px"></td>
                                                        <td style="width: 209px; height: 19px"></td>
                                                        <td style="width: 205px; height: 19px"></td>
                                                    </tr>
                                                </table>
                                                <hr width="100%" size="1">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table id="Table2" style="left: 0px; top: 0px; width: 95%;" cellspacing="1" cellpadding="0"
                                    border="0" height="165">
                                    <tr>
                                        <td colspan="1" style="height: 3px" valign="top" width="100"></td>
                                        <td style="height: 3px" valign="top" colspan="1">
                                            <span style="font-size: 10pt">&nbsp; <strong>List</strong></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 100%" valign="top" width="100">&nbsp; &nbsp;
                                        </td>
                                        <td style="width: 100%; height: 100%" valign="top">
                                            <div id="div2" style="width: 770px; height: 320px; overflow: auto">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowCommand="GridView1_RowCommand" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                        <asp:BoundField DataField="RuleName" HeaderText="Nama Rule" />
                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
