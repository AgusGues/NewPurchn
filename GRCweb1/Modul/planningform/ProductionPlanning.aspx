<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductionPlanning.aspx.cs" Inherits="GRCweb1.Modul.planningform.ProductionPlanning" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width:100%; border-collapse:collapse">
                    <tr>
                        <td style="width:100%; height:49px">
                            <table style="width:100%; border-collapse:collapse;" class="nbTableHeader">
                                <tr>
                                    <td style="width:50%; padding-left:10px"><b>Production Planning</b></td>
                                    <td style="width:50%; padding-right:10px" align="right">
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="Planning List" OnClick="btnList_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small; margin-top:5px">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:15%">Periode</td>
                                        <td style="width:30%"><asp:DropDownList ID="ddlBulan" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Tahun</td>
                                        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTahun_Change"></asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Running Line</td>
                                        <td><asp:DropDownList ID="ddlRunning" runat="server" Width="50%">
                                            <asp:ListItem Value="1">1 Line</asp:ListItem>
                                            <asp:ListItem Value="2">2 Line</asp:ListItem>
                                            <asp:ListItem Value="3">3 Line</asp:ListItem>
                                            <asp:ListItem Value="4">4 Line</asp:ListItem>
                                            <asp:ListItem Value="5">5 Line</asp:ListItem>
                                            <asp:ListItem Value="6" Selected="True">6 Line</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Keterangan</td>
                                        <td><asp:TextBox ID="txtKeterangan" runat="server" Rows="3" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height:340px">
                                    <table>
                                        <thead>
                                            <tr class="tbHeader">
                                               <th class="kotak" style="width:5%; height: 17px;">No.</th>
                                               <th class="kotak" style="width:15%; height: 17px;">Periode</th>
                                               <th class="kotak" style="width:10%; height: 17px;">Planning</th>
                                               <th class="kotak" style="width:5%; height: 17px;">Revisi</th>
                                               <th class="kotak" style="width:10%; height: 17px;">Revisi Date</th>
                                               <th class="kotak" style="width:10%; height: 17px;">Created By</th>
                                               <th class="kotak" style="width:15%; height: 17px;">Keterangan</th>
                                               <th class="kotak" style="width:5%; height: 17px;"></th>
                                               <th class="kotak" style="width:5%; height: 17px;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstPlanning" runat="server" OnItemCommand="lstPlanning_Command" OnItemDataBound="lstPlanning_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak"><%# Eval("Periode") %></td>
                                                        <td class="kotak"><%# Eval("RunningLine") %></td>
                                                        <td class="kotak"><%# Eval("Revision") %></td>
                                                        <td class="kotak"><%# Eval("CreatedTime", "{0:d}") %></td>
                                                        <td class="kotak "><%# Eval("CreatedBy") %></td>
                                                        <td class="kotak"><%# Eval("Keterangan") %></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="hps" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>' CommandName="delete" />
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="cls" runat="server" ImageUrl="~/images/Close.gif" CommandArgument='<%# Eval("ID") %>' CommandName="closing" ToolTip="Closing Planing" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
