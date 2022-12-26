<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterTaskCategory.aspx.cs" Inherits="GRCweb1.Modul.ISO.MasterTaskCategory" %>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Master Kategory</span>
                <div class="pull-right">
                    <span id="txtMsg" runat="server"></span>
                    <asp:TextBox ID="IdKat" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                    <input class="btn btn-primary" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-primary" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick"/>
                    <input class="btn btn-primary" id="btnDelete" runat="server" type="button" value="Hapus" onserverclick="btnDelete_ServerClick"/>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_Change" AutoPostBack="true"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kategory</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="KategoriID" runat="server" OnSelectedIndexChanged="Kategori_Change"  AutoPostBack="true">
                                <asp:ListItem Value="1">KPI</asp:ListItem>
                                <asp:ListItem Value="2">Task</asp:ListItem>
                                <asp:ListItem Value="3">SOP</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Target</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTarget" runat="server"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Deskripsi</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="Deskripsi" runat="server" TextMode="MultiLine" Rows="3" ></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ProgramChecking</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtChecking" runat="server"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div5" class="contentlist" style="height:320px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" Width="100%" Font-Size="Smaller" AllowPaging="True" GridLines="Horizontal" 
                        onRowCommand="GridView1_RowCommand"
                        OnRowEditing="GridView1_RowEditing"
                        OnPageIndexChanging="GridView1_PageIndexChanging" Visible="false">
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" >
                            <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                            <asp:BoundField DataField="Task" HeaderText="Kategori" />
                            <asp:BoundField DataField="Desk" HeaderText="Deskripsi" />
                            <asp:BoundField DataField="Bobots" HeaderText="Bobot" >
                            <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingRowStyle BackColor="#F7F7F7" /></asp:GridView>
                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                            <thead>
                                <tr class="tbHeader baris">
                                    <th class="kotak" style="width:4%">No.</th>
                                    <th class="kotak" style="width:5%">Type</th>
                                    <th class="kotak" style="width:28%">Description</th>
                                    <th class="kotak" style="width:12%">Program Checking</th>
                                    <th class="kotak" style="width:15%">Target</th>
                                    <th class="kotak" style="width:5%">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstCat" runat="server" OnItemDataBound="lstCat_DataBound" OnItemCommand="lstCat_ItemCommand">
                                <ItemTemplate>
                                    <tr class="EvenRows baris">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak"><%# Eval("Task") %></td>
                                        <td class="kotak"><%# Eval("Desk")%></td>
                                        <td class="kotak"><%# Eval("Checking") %></td>
                                        <td class="kotak"><%# Eval("Target") %></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="OddRows baris">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak"><%# Eval("Task") %></td>
                                        <td class="kotak"><%# Eval("Desk")%></td>
                                        <td class="kotak"><%# Eval("Checking") %></td>
                                        <td class="kotak"><%# Eval("Target") %></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate></asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <table style="table-layout:fixed; width:100%; height:100%">
            <tr>
                <td colspan="5" style="height: 49px">
                    <table class="nbTableHeader" width="100%">
                      <tr>
                          <td style="width: 100%">                            
                              <strong>&nbsp;PES KATEGORI</strong></td>
                              <td style="width: 37px">

                              </td>
                              <td style="width: 75px">

                              </td>
                              <td style="width: 5px">

                              </td>                      
                              <td style="width:5px">&nbsp;</td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr >
                <td colspan="5" valign="top" align="left">
                    <div id="div2" class="content">
                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                            <tr>
                                <td>&nbsp;Departemen</td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width='10%'>&nbsp; Kategori</td>
                                <td width='15%'>
                                </td>
                                <td colspan='3'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width='10%' valign="top">&nbsp; Deskripsi</td>
                                <td colspan='2' width="30%">
                                </td>
                                <td colspan='2'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width='10%'>&nbsp; Target</td>
                                <td width='15%'>
                                </td>
                                <td colspan='3'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td width='10%'>&nbsp; Program Checking</td>
                                <td width='15%'>
                                </td>
                                <td colspan='3'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="4"></td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
