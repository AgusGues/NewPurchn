<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormBudgetingATK.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormBudgetingATK" %>
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
<script type="text/javascript">
    function onListPopulated() {

        var completionList = $find("AutoCompleteEx").GetItemATK();
    }
    function itemSelected(ev) {
        var index = $find("AutoCompleteEx")._selectIndex;
        var dd = $find("AutoCompleteEx").GetItemATK().childNodes[index]._value;
        $find("AutoCompleteEx").GetItemATK().value = dd;
    }
    function dd() {
        var comletionList = $find("AutoCompleteEx").GetItemATK();
        for (i = 0; i < comletionList.childNodes.length; i++) {

            var _value = comletionList.childNodes[i]._value;
            comletionList.childNodes[i]._value = _value.substring(_value.lastIndexOf('-') + 1); 

            _value = _value.substring(0, _value.lastIndexOf('-'));
            comletionList.childNodes[i].innerHTML = _value.replace('-', '<br/>');

        }

    }
    function HapusData() {
        if (confirm("Yakin akan di hapus data ini?") == true) {
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');

        } else {
            return false;
        }
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>BudgetingAtk</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-primary" ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" />
                    <asp:Button class="btn btn-primary" ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                    <asp:Button class="btn btn-primary" ID="btnList" runat="server" Text="List" OnClick="btnList_Click" />
                    <asp:DropdownList ID="ddlCari" runat="server">
                    <asp:ListItem Value="BudgetNo">Budget No.</asp:ListItem></asp:DropdownList>
                    <asp:TextBox ID="txtCari" runat="server" Width="160px"></asp:TextBox>
                    <asp:Button class="btn btn-primary" ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">BudgetNo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtBudgetNo" runat="server" ReadOnly="false"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Periode</div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change" AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change" AutoPostBack="true"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_Change" AutoPostBack="true"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ItemName</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtMaterial" AutoCompleteType="Search" runat="server" AutoPostBack="true" OnTextChanged="txtMaterial_Change"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="act" BehaviorID="AutoCompleteEx" runat="server" TargetControlID="txtMaterial"
                                CompletionSetCount="10" ServiceMethod="GetItemATK" ServicePath="AutoComplete.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" MinimumPrefixLength="2" EnableCaching="true"></cc1:AutoCompleteExtender>
                                <asp:TextBox ID="txtItemID" runat="server" Visible="false"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Satuan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUnit" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtHeadID" runat="server" Visible="false"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Quantity</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQty" runat="server" CssClass="angka" OnTextChanged="txtQty_Change" AutoPostBack="true" width="100%"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <span>History Spb</span>
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr class="Line3">
                                        <td class="kotak" colspan="<%=Kolom %>">
                                            &nbsp;&bull;&nbsp;<asp:Label ID="lblItemName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="tbHeader">
                                        <%=BulanPakai %>
                                    </tr>
                                </table>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Keterangan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKeterangan" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn btn-primary" ID="btnAdditem" runat="server" Text="Add Item" OnClick="btnAddItem_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <table class="tbStandart">
                        <thead>
                            <tr class="tbHeader">
                                <th style="width:5%">No.</th>
                                <th style="width:10%">Item Code</th>
                                <th style="width:45%">Item Name</th>
                                <th style="width:5%">Satuan</th>
                                <th style="width:8%">Quantity</th>
                                <th style="width:22%">Keterangan</th>
                                <th style="width:5%">&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="lstAtk" runat="server" OnItemCommand="lstAtk_Command" OnItemDataBound="lstAtk_Databound">
                            <ItemTemplate>
                                <tr class="EvenRows baris">
                                    <td class="kotak tengah"><%# Container.ItemIndex+1  %></td>
                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                    <td class="kotak"><%# Eval("UomCode") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                    <td class="kotak"><%# Eval("Keterangan") %></td>
                                    <td class="kotak tengah">
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
