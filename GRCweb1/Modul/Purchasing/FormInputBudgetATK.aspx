<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputBudgetATK.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormInputBudgetATK" %>
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
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

<script language="JavaScript" type="text/javascript">
    $(document).ready(function() {
        maintainScrollPosition();
    });
    function pageLoad() {
        maintainScrollPosition();
    }
    function maintainScrollPosition() {
        $("#div2").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
    }
    function setScrollPosition(scrollValue) {
        $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
    }
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }

</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>InputBudgetAtk</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-primary" ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />
                    <asp:Button class="btn btn-primary" ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_serverClick" />
                    <asp:Button class="btn btn-primary" ID="btnList" runat="server" Text="List" OnClick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ItemName">Nama Material</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <asp:Button class="btn btn-primary" ID="btnSearch" runat="server" Text="Cari" OnClick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tahun</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlTahun" runat="server" AutoPostBack="True">
                                <asp:ListItem>-- Pliih Tahun --</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" OnSelectedIndexChanged="ddlDept_Change" AutoPostBack="True"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">CariItemName</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCariNamaBrg" runat="server" AutoPostBack="True" OnTextChanged="txtCariNamaBrg_Change"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ItemName</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Quantity</div>
                            <div class="col-md-8">
                                <asp:HiddenField ID="txtID" runat="server" Value="0" />
                                <asp:TextBox class="form-control" ID="txtBudgetQty" runat="server"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TypeBudget</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlTipeBudget" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="0">-- Pliih Tipe Budget --</asp:ListItem>
                                <asp:ListItem Value="1">Bulanan</asp:ListItem>
                                <asp:ListItem Value="6">Semester</asp:ListItem>
                                <asp:ListItem Value="12">Tahunan</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">JenisBudget</div>
                            <div class="col-md-8">
                                <asp:RadioButton GroupName="tpp" ID="txtBaru" runat="server" OnCheckedChanged="tpp_Change" AutoPostBack="true" />&nbsp;Baru &nbsp;&nbsp;
                                <asp:RadioButton GroupName="tpp" ID="txtRevisi" runat="server"  OnCheckedChanged="tpp_Change" AutoPostBack="true"/>&nbsp;Revisi
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">MasaBerlaku</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlMasaBerlaku" runat="server" width="100%"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn  btn-primary" ID="lbAddOP" runat="server" Text="Add Item" OnClick="lbAddOP_Click" />
                                <asp:Button class="btn  btn-primary" ID="lbUpdate" runat="server" Text="Update Item" OnClick="lbUpdate_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <div class="contentlist" style="height:270px" onscroll="setScrollPosition(this.scrollTop);" id="div2">
                    <table class="tbStandart">
                        <thead>
                            <tr class="tbHeader">
                                <th class="kotak" style="width: 4%"> No.</th>
                                <th class="kotak" style="width: 5%">Tahun</th>
                                <th class="kotak" style="width: 10%">Departement</th>
                                <th class="kotak" style="width: 35%">Nama Material</th>
                                <th class="kotak" style="width: 8%">Budget Quantity</th>
                                <%--<th class="kotak" style="width: 8%">Add Quantity</th>--%>
                                <th class="kotak" style="width: 6%">Tipe Budget</th>
                                <th class="kotak" style="width: 4%">&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="lstBudget" runat="server" OnItemCommand="lstBudget_Command" OnItemDataBound="lstBudget_Databound">
                            <ItemTemplate>
                                <tr class="EvenRows baris">
                                    <td class="kotak tengah"><%# Container.ItemIndex+1  %></td>
                                    <td class="kotak tengah"><%# Eval("Tahun")%></td>
                                    <td class="kotak" nowrap="nowrap"><%# Eval("DeptName")%></td>
                                    <td class="kotak"><%# Eval("ItemCode") %> - <%# Eval("ItemName") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity", "{0:N2}")%></td>
                                    <%--<td class="kotak angka"><%# Eval("Jumlah", "{0:N2}")%></td>--%>
                                    <td class="kotak"><asp:Label ID="txtBgType" runat="server"></asp:Label></td>
                                    <td class="kotak tengah" nowrap="nowrap">
                                        <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandName="edit" CommandArgument='<%# Eval("ID") %>' ToolTip="Edit Data" />
                                        <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandName="dele" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Hapus Data" />
                                        <asp:ImageButton ID="dels" runat="server" ImageUrl="~/images/Delete.png" CommandName="delet" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />
                                    </td>
                                </tr>
                            </ItemTemplate></asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
