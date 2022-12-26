<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterPESScore.aspx.cs" Inherits="GRCweb1.Modul.ISO.MasterPESScore" %>
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
<asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Master Score</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-primary" ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" />
                    <asp:Button class="btn btn-primary" ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                    <asp:Button class="btn btn-primary" ID="btnList" runat="server" Text="List" onclick="btnList_Click" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_Change"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Section</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_Change"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">PesType</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlPesType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPesType_Click">
                                <asp:ListItem Value="1">KPI</asp:ListItem>
                                <asp:ListItem Value="2">TASK</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">SOP</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Deskripsi</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlCategori" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategori_Change"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Target</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTarget" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Score</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtScore" runat="server"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Label ID="txtMsg" runat="server" Text=""></asp:Label>
                                <asp:Button class="btn btn-primary" ID="btnAddItem" runat="server" Text="Add Score" OnClick="btnAddItem_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div class="contentlist" style="height:310px";>
                        <table style="width:100%; font-size:x-small; border-collapse:collapse">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width:4%">No.</th>
                                    <th class="kotak" style="width:5%">PES</th>
                                    <th class="kotak" style="width:30%">Category Description</th>
                                    <th class="kotak" style="width:15%">Target Pencapaian</th>
                                    <th class="kotak" style="width:10%">Score</th>
                                    <th class="kotak" style="width:5%">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstScore" runat="server" OnItemCommand="lstScore_ItemCommand" OnItemDataBound="lstScore_DataBound">
                                <ItemTemplate>
                                    <tr class="EvenRows baris" id="br" runat="server">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak tengah"><%# Eval("PESName") %></td>
                                        <td class="kotak"><%# Eval("CategoryDescription")%></td>
                                        <td class="kotak tengah"><%# Eval("Target") %></td>
                                        <td class="kotak tengah"><%# Eval("Nilai") %></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="btnExt" runat="server" ImageUrl="~/images/trash.gif" CommandArgument='<%# Eval("ID") %>' CommandName="nonaktif" />
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/images/Delete.png"
                                            CommandArgument='<%# Container.ItemIndex %>' CommandName="del" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="OddRows baris" id="br" runat="server">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak tengah"><%# Eval("PESName") %></td>
                                        <td class="kotak"><%# Eval("CategoryDescription")%></td>
                                        <td class="kotak tengah"><%# Eval("Target") %></td>
                                        <td class="kotak tengah"><%# Eval("Nilai") %></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="btnExt" runat="server" ImageUrl="~/images/trash.gif" CommandArgument='<%# Eval("ID") %>' CommandName="nonaktif" />
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/images/Delete.png"
                                            CommandArgument='<%# Container.ItemIndex %>' CommandName="del" />
                                        </td>
                                    </tr></AlternatingItemTemplate>
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
