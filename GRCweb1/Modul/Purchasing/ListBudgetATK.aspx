<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListBudgetATK.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListBudgetATK" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 5px 4px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    textarea{color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control,
    td{height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th {padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}
</style>

<script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        maintainScrollPosition();
    });
    function pageLoad() {
        maintainScrollPosition();
    }
    function maintainScrollPosition() {
        $("#<%=ctn.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        $("#<%=ToSPP.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
    }
    function setScrollPosition(scrollValue) {
        $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
    }    
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>ListBudgetAtk</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-primary" ID="btnBack" runat="server" Text="Budget Form" OnClick="btnBack_Click" />
                    <asp:Button class="btn btn-primary" ID="btnApproval" runat="server" Text="Approved" OnClick="btnApproval_Click" />
                    <asp:Button class="btn btn-primary" ID="btnToSPP" runat="server" Text="Create SPP" OnClick="btnToSPP_Click" />
                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row" id="filter" runat="server">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Periode</div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList> &nbsp;
                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn btn-primary" ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                <asp:Button class="btn btn-primary" ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="flToSPP" runat="server">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Periode</div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBulan1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList> &nbsp;
                                <asp:DropDownList ID="ddlTahun1" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6"></div>
                </div>
            </div>
            <div class="table-responsive">
                <div class="contentlist" id="ctn" runat="server" style="height:450px" onscroll="setScrollPosition(this.scrollTop);">
                    <div id="approv" runat="server">
                        <table class="tbStandart" border="0">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width:4%">No.</th>
                                    <th class="kotak" style="width:10%">Item Code</th>
                                    <th class="kotak" style="width:25%">Item Name</th>
                                    <th class="kotak" style="width:5%">Unit</th>
                                    <th class="kotak" style="width:8%">Quantity</th>
                                    <th class="kotak" style="width:8%">Qty Apv</th>
                                    <th class="kotak" style="width:8%">Approval</th>
                                    <th class="kotak" style="width:5%">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstBulan" runat="server" OnItemDataBound="lstBulan_DataBound">
                                <ItemTemplate>
                                    <tr class="Line3">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak" colspan="7"><b><%# Eval("ItemName") %> <%# Eval("Tahun") %></b></td>
                                    </tr>
                                    <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                    <ItemTemplate>
                                        <tr class="total">
                                            <td class="kotak">
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" ToolTip='<%#((RepeaterItem)Container.Parent.Parent).ItemIndex %>' OnCheckedChanged="ChkAll_Checked" />
                                            </td>
                                            <td class="kotak" colspan="3"><%# Eval("DeptName") %></td>
                                            <td class="kotak">&nbsp;</td>
                                            <td class="kotak">&nbsp;</td>
                                            <td class="kotak">&nbsp;</td>
                                            <td class="kotak">&nbsp;</td>
                                        </tr>
                                        <asp:Repeater ID="lstATK" runat="server" OnItemDataBound="lstATK_DataBound" OnItemCommand="lstATK_Command">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak angka">
                                                    <asp:HiddenField ID="txtApv" runat="server" Value='<%# Eval("Approval") %>' />
                                                    <asp:CheckBox ID="chk" runat="server" ToolTip='<%# Eval("BudgetID") %>' AutoPostBack="true" OnCheckedChanged="chk_Checked" />
                                                </td>

                                                <%--<td class="kotak tengah"><%# Eval("ItemCode") %></td>--%>
                                                <td class="kotak tengah">
                                                    <%--<asp:TextBox ID="ItemCode" runat="server" Text='<%# Eval("ItemCode") %>' ></asp:TextBox>
                                                </td>--%>

                                                <asp:TextBox ID="ItemCode" runat="server" Width="100%" BorderStyle="None" BackColor="Transparent" 
                                                Text='<%# Eval("ItemCode") %>' ReadOnly="true"  Font-Size="X-Small">
                                            </asp:TextBox></td>

                                            <td class="kotak"><%# Eval("ItemName") %></td>
                                            <td class="kotak tengah"><%# Eval("UomCode") %></td>

                                            <td class="kotak angka"><asp:Label ID="lblQty" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtQty" runat="server" Width="100%" BorderStyle="None" BackColor="Transparent" 
                                                Text='<%# Eval("Quantity","{0:N2}") %>' ReadOnly="true" CssClass="angka" Font-Size="X-Small">
                                            </asp:TextBox></td>

                                            <td class="kotak angka"><asp:Label ID="lblApv" runat="server" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtApvQty" runat="server"  Width="100%" BorderStyle="none" ToolTip='<%# Eval("ItemID") %>' 
                                                Text='<%# Eval("AppvQty", "{0:N2}")%>' CssClass="angka" Font-Size="X-Small" 
                                                BackColor="Transparent" AutoPostBack="true" OnTextChanged="txtApvQty_Change"></asp:TextBox>
                                                <asp:Label ID="txtDept" runat="server" Text='<%# Eval("DeptID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="txtThn" runat="server" Text='<%# Eval("Tahun") %>' Visible="false"></asp:Label>
                                            </td>

                                            <td class="kotak" nowrap="nowrap"><%# Eval("ApprovalBy") %>
                                                <asp:TextBox ID="txtDetailID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:TextBox></td>
                                                <td class="kotak tengah">
                                                    <asp:ImageButton ID="edit" runat="server" ImageUrl="~/images/editor.png" CommandName="edit" CommandArgument='<%# Eval("ID")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div id="ToSPP" runat="server"  onscroll="setScrollPosition(this.scrollTop);">
            <table class="tbStandart">
                <thead>
                    <tr class="tbHeader">
                        <th style="width:4%">No.</th>
                        <th style="width:10%">ItemCode</th>
                        <th style="width:25%">ItemName</th>
                        <th style="width:5%">Unit</th>
                        <th style="width:8%">Quantity</th>
                        <th style="width:8%">Stock</th>
                        <th style="width:4%"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="lsttoSPP" runat="server" OnItemCommand="lsttoSPP_ItemCommand" OnItemDataBound="lstSPP_DataBind">
                    <ItemTemplate>
                        <tr class="EvenRows baris">
                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                            <td class="kotak tengah"><asp:TextBox Width="100%" ID="txtItemCode" runat="server" CssClass="txtongrid tengah" Text='<%# Eval("ItemCode") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak"><asp:TextBox Width="100%" ID="txtItemName" runat="server" CssClass="txtongrid" Text='<%# Eval("ItemName") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak"><asp:TextBox Width="100%" ID="txtUomCode" runat="server" CssClass="txtongrid" Text='<%# Eval("UOMCode") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak angka"><asp:TextBox Width="100%" ID="txtQty" runat="server" Text='<%# Eval("Quantity","{0:N0}") %>' CssClass="txtongrid angka"></asp:TextBox></td>
                            <td class="kotak angka"><asp:Label Width="100%" ID="txtStk" runat="server" Text='<%# Eval("Stock","{0:N0}") %>' CssClass='angka'></asp:Label></td>
                            <td class="kotak tengah">
                                <asp:ImageButton ID="detail" runat="server" ImageUrl="~/images/clipboard_16.png" CommandArgument='<%# Eval("ItemID") %>' CommandName="infone" />
                                <asp:TextBox ID="txtItemID" runat="server" CssClass="txtongrid" Text='<%# Eval("ItemID") %>' ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtUomID" runat="server" CssClass="txtongrid" Text='<%# Eval("UomID") %>' ReadOnly="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="OddRows baris">
                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                            <td class="kotak tengah"><asp:TextBox Width="100%" ID="txtItemCode" runat="server" CssClass="txtongrid tengah" Text='<%# Eval("ItemCode") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak"><asp:TextBox Width="100%" ID="txtItemName" runat="server" CssClass="txtongrid" Text='<%# Eval("ItemName") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak"><asp:TextBox Width="100%" ID="txtUomCode" runat="server" CssClass="txtongrid" Text='<%# Eval("UOMCode") %>' ReadOnly="true"></asp:TextBox></td>
                            <td class="kotak angka"><asp:TextBox Width="100%" ID="txtQty" runat="server" Text='<%# Eval("Quantity","{0:N0}") %>' CssClass="txtongrid angka"></asp:TextBox></td>
                            <td class="kotak angka"><asp:Label Width="100%" ID="txtStk" runat="server" Text='<%# Eval("Stock","{0:N0}") %>' CssClass='angka'></asp:Label></td>
                            <td class="kotak tengah">
                                <asp:ImageButton ID="detail" runat="server" ImageUrl="~/images/clipboard_16.png" CommandArgument='<%# Eval("ItemID") %>' CommandName="infone" />
                                <asp:TextBox ID="txtItemID" runat="server" CssClass="txtongrid" Text='<%# Eval("ItemID") %>' ReadOnly="true" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtUomID" runat="server" CssClass="txtongrid" Text='<%# Eval("UomID") %>' ReadOnly="true" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</div>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
