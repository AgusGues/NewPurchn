<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSPPnew.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListSPPnew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }

        label {
            font-size: 12px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table class="nbTableHeader" style="width: 100%">
                    <tr>
                        <td><strong>&nbsp;&nbsp;&bull;&nbsp;LIST SPP READY TO PO</strong></td>
                        <td>
                            <asp:Button ID="btnForm" runat="server" Text="Form PO" OnClick="btnFormPO_Click" /></td>
                        <td>
                            <asp:DropDownList ID="ddlSearch" runat="server">
                                <asp:ListItem Value="NoSPP">SPP NO</asp:ListItem>
                                <asp:ListItem Value="ItemName">Item Name</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCari" runat="server" Width="200px"></asp:TextBox><input type="hidden" id="hidValue" runat="server" /></td>
                    </tr>
                </table>

                <div class="content" style="height: 500px; overflow: auto; padding: 5px;">
                    <table style="border-collapse: collapse; font-size: smaller;width: 100%">
                        <thead style="">
                            <tr class="tbHeader">
                                <th class="kotak" rowspan="2" >No.</th>
                                <th class="kotak" >No.SPP</th>
                                <th class="kotak" >Tanggal</th>
                                <th class="kotak" >Tipe SPP</th>
                                <th class="kotak" colspan="2" >Head</th>
                                <th class="kotak" >App.Date</th>
                                <th class="kotak" colspan="4" >&nbsp;</th>
                            </tr>
                            <tr class="tbHeader">
                                <th class="kotak">ItemCode</th>
                                <th class="kotak" colspan="3" >ItemName</th>
                                <th class="kotak" >Unit</th>
                                <th class="kotak" >QtySPP</th>
                                <th class="kotak" >QtyPO</th>
                                <th class="kotak" >Delivery</th>
                                <th class="kotak" >LeadTime</th>
                                <th class="kotak" >&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="lstSPP" runat="server" OnItemDataBound="lstSPP_ItemDataBound">
                                <ItemTemplate>
                                    <tr style="background-color: #CCFF99; height: 24px">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak"><a href='FormPOPurchn.aspx?NoSPP=<%# Eval("NoSPP") %>'><%# Eval("NoSPP") %></a></td>
                                        <td class="kotak tengah"><%# Eval("Minta","{0:d}") %></td>
                                        <td class="kotak"><%#Eval("Gudang").ToString().ToUpper()%></td>
                                        <td class="kotak" colspan="2"><%# Eval("HeadName").ToString().ToUpper()%></td>
                                        <td class="kotak"><%# Eval("ApproveDate3","{0:d}") %></td>
                                        <td class="kotak angka" colspan="4" style="padding-right: 5px">
                                            <%--<asp:ImageButton ID="cPO" runat="server" CommandArgument='<%# Eval("NoSPP")%>' ImageUrl="~/images/folder.gif" ToolTip="Create PO"/>
                                                    <asp:ImageButton ID="PO" runat="server" ImageUrl="~/images/Delete.png" />--%>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="lstDetailSPP" runat="server" OnItemDataBound="lsd_ItemDataBound" OnItemCommand="lsd_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                <td class="kotak" colspan="3"><%# Eval("ItemName")  %></td>
                                                <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                                <td class="kotak angka"><%# Eval("QtyPO","{0:N2}") %></td>
                                                <td class="kotak tengah"><%# Eval("TglKirim","{0:d}") %></td>
                                                <td class="kotak tengah"><%# Eval("ItemTypeID") %></td>
                                                <td class="kotak tengah">
                                                    <asp:ImageButton ID="pndSPP" CommandName="unlock" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/lock_closed.png" ToolTip="set Pending SPP" />
                                                    <asp:ImageButton ID="clsSPP" CommandName="lock" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/lock_open.png" ToolTip="set Pending SPP" Visible="false" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="OddRows baris">
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                <td class="kotak" colspan="3"><%# Eval("ItemName")  %></td>
                                                <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                                <td class="kotak angka"><%# Eval("QtyPO", "{0:N2}")%></td>
                                                <td class="kotak tengah"><%# Eval("TglKirim","{0:d}") %></td>
                                                <td class="kotak tengah"><%# Eval("ItemTypeID") %></td>
                                                <td class="kotak tengah">
                                                    <asp:ImageButton ID="pndSPP" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="unlock" ImageUrl="~/images/lock_closed.png" ToolTip="set Pending SPP" />
                                                    <asp:ImageButton ID="clsSPP" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="lock" ImageUrl="~/images/lock_open.png" ToolTip="set Pending SPP" Visible="false" />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>

                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
