<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T3BAApproval.aspx.cs" Inherits="GRCweb1.Modul.Factory.T3BAApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .main-container {
            display: block;
            content: "";
            position: absolute;
            z-index: -2;
            width: 100%;
            max-width: inherit;
            bottom: 0;
            top: 0;
            background-color: #FFF;
            overflow-x: auto;
        }
        label {
            font-weight: 400;
            font-size: 10px;
        }
    </style>
    <script language="Javascript" type="text/javascript" src="../../Script/calendar.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table cellpadding="0" cellspacing="0" style="table-layout: fixed" width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader">
                                <tr>
                                    <td >
                                        <strong>APPROVAL BERITA ACARA TAHAP 3</strong>
                                    </td>
                                    <td style="width: 75px">
                                        <input id="btnSebelumnya" runat="server" onserverclick="btnSebelumnya_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                            value="Sebelumnya" />
                                    </td>
                                    <td style="width: 5px">
                                        <input id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px;" type="button" value="Approve" />
                                    </td>
                                    <td style="width: 5px">
                                        <input id="btnSelanjutnya" runat="server" onserverclick="btnSesudahnya_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                            value="Selanjutnya" />
                                    </td>
                                    <td align="right">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="font-size: x-small; font-weight: bold;">
                                    </td>
                                    <td align="right" style="font-size: x-small;">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table id="tabelInput" align="right" style="width: 100%; font-size: x-small;">
                                                <tr>
                                                    <td align="left" colspan="2" style="height: 19px">
                                                        No. Berita Acara
                                                        <asp:TextBox ID="txtNoBA" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Height="20px"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tgl.&nbsp;
                                                        Berita Acara
                                                        <asp:TextBox ID="txtTglBA" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                            TargetControlID="txtTglBA">
                                                        </cc1:CalendarExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Type&nbsp;
                                                        Berita Acara
                                                        <asp:DropDownList ID="ddlKeterangan" runat="server" Enabled="False">
                                                            <asp:ListItem>PERMINTAAN PRODUK</asp:ListItem>
                                                            <asp:ListItem>PRODUK MASUK</asp:ListItem>
                                                            <asp:ListItem>KONSENSI</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <hr />
                                                </tr>
                                                <tr>
                                                    <td style="font-size: x-small; font-weight: bold;" valign="top">
                                                        <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" PageSize="22" Width="100%">
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <Columns>
                                                                <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                                                <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                                                <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                                            </Columns>
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td style="font-size: x-small; font-weight: bold;" valign="top">
                                                        <table style="font-size: x-small" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <td bgcolor="#CCCCCC" rowspan="2">
                                                                        List Lampiran
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#CCCCFF">
                                                                        <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="EvenRows baris">
                                                                                    <td>
                                                                                        <%# Eval("FileName") %>
                                                                                    </td>
                                                                                    <td align="right" width="25%">
                                                                                        <asp:ImageButton ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                                                            CommandName="pre" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Logo_Download.png"
                                                                                            ToolTip="Click to Preview Document" />
                                                                                        <%--<asp:ImageButton ID="hapus" runat="server" AlternateText='<%# Eval("BAID") %>' CommandArgument="<%# Container.ItemIndex %>"
                                                                                            CommandName="hps" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Delete.png"
                                                                                            ToolTip="Click for delete attachment" />--%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: x-small; font-weight: bold;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: x-small; font-weight: bold;" valign="top">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
