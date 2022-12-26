<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaterialCCMonitoring.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.MaterialCCMonitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="table-responsive">
                <div class="col-xs-12">
                    <table style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 50%; padding-left: 10px">COST MONITORING</td>
                                        <td style="width: 50%; text-align: right; padding-right: 10px"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="content">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; margin-top: 5px">
                                        <tr>
                                            <td style="width: 5%">&nbsp;</td>
                                            <td style="width: 15%">Periode</td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Department</td>
                                            <td>
                                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnPerview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="List Aktual Budget BM" />
                                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Execl" />
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />

                                    <div class="contentlist" style="height: 370px;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="vertical-align: top">
                                                    <div id="lstRekap" runat="server" style="display: inline;">
                                                        <table style="border-collapse: collapse; font-size: x-small">
                                                            <thead>
                                                                <tr class="tbHeader">
                                                                    <th rowspan="2" style="width: 5%" class="kotak">No.</th>
                                                                    <th rowspan="2" style="width: 15%" class="kotak">Material Group</th>
                                                                    <th rowspan="2" style="width: 5%" class="kotak">Prod Line</th>
                                                                    <th colspan="2" class="kotak">Cost Value</th>
                                                                    <th rowspan="2" style="width: 5%" class="kotak">%</th>
                                                                    <th rowspan="2" style="width: 1%; background-color: Transparent">&nbsp;</th>
                                                                </tr>
                                                                <tr class="tbHeader">
                                                                    <th style="width: 10%" class="kotak">Budget</th>
                                                                    <th style="width: 10%" class="kotak">Actual</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="lstMonitor" runat="server" OnItemDataBound="lstMonitor_DataBound" OnItemCommand="lstMonitor_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris" id="xx" runat="server" title='<%# Eval("GroupName") %>'>
                                                                            <td class="kotak tengah"><%# Container.ItemIndex + 1%></td>
                                                                            <td class="kotak">
                                                                                <asp:LinkButton ID="grp" runat="server" Text='<%# Eval("GroupName") %>' CommandName="pilih" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton></td>
                                                                            <td class="kotak tengah"><%# Eval("RunningLine") %></td>
                                                                            <td class="kotak angka"><%# Eval("CostBudget","{0:N0}") %></td>
                                                                            <td class="kotak angka"><%# Eval("CostActual", "{0:N0}")%></td>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="prs" runat="server" Text='<%# Eval("Prosen","{0:N2}") %>'></asp:Label></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                                <td style="vertical-align: top">
                                                    <div id="lstDtl" runat="server" visible="false" style="overflow: auto; height: 350px; padding: 5px; display: inline; background-color: #B0C4DE; vertical-align: top">
                                                        <table style="border-collapse: collapse; font-size: x-small">
                                                            <thead>

                                                                <asp:Label ID="txtHeader" runat="server"></asp:Label>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="lstDetail" runat="server" OnItemDataBound="lstDetailDataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="OddRows baris" id="xDetail" runat="server" title='<%# Eval("ItemName") %>'>
                                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                            <td class="kotak"><%# "'" +Eval("ItemCode") %></td>
                                                                            <td class="kotak"><%# Eval("ItemName") %></td>
                                                                            <td class="kotak angka"><%# Eval("QtyBudget","{0:N2}") %></td>
                                                                            <td class="kotak angka"><%# Eval("CostBudget","{0:N2}") %></td>
                                                                            <td class="kotak angka"><%# Eval("CostActual","{0:N0}") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                                <asp:Repeater ID="lstMtc" runat="server" OnItemDataBound="lstDetailDataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="OddRows baris" id="xDetail" runat="server" title='<%# Eval("ItemName") %>'>
                                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                            <td class="kotak">
                                                                                <asp:LinkButton ID="txtItem" runat="server" Text='<%# Eval("ItemName") %>' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton></td>
                                                                            <td class="kotak angka"><%# Eval("CostBudget","{0:N0}") %></td>
                                                                            <td class="kotak angka"><%# Eval("CostActual","{0:N2}") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>


                                    </div>

                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
