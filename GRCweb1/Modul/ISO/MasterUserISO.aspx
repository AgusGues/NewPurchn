<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterUserISO.aspx.cs" Inherits="GRCweb1.Modul.ISO.MasterUserISO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 5px 4px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
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
            <span>Master User Pes</span>
                <div class="pull-right">
                    <asp:TextBox ID="ID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="pwd" TextMode="Password" runat="server" Visible="false"></asp:TextBox>
                    <input class="btn btn-primary" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-primary" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick"/>
                    <input class="btn btn-primary" id="btnDelete" runat="server" type="button" 
                    value="Hapus" onserverclick="btnDelete_ServerClick"/> 
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="UserName">Nama User</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="btn btn-primary" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick"/>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">&nbsp;</div>
                            <div class="col-md-8">
                                &nbsp;
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Id</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="userid" runat="server" AutoPostBack="false"
                                ontextchanged="userid_TextChanged" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">UserName</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="username" runat="server" AutoPostBack="true" 
                                ontextchanged="username_TextChanged"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Nik</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNIK" runat="server" 
                                ontextchanged="username_TextChanged"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TypeUnitKerja</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID='TypeUnitKerjaID' runat="server" AutoPostBack="true" onselectedindexchanged="TypeUnitKerjaID_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Pilih Tipe Unit Kerja --</asp:ListItem>
                                <asp:ListItem Value="1">Distributor</asp:ListItem>
                                <asp:ListItem Value="2">Depo</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">UnitKerja</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID='UnitKerjaID' runat="server" AutoPostBack="true">
                                <asp:ListItem Value="0">--Pilih Unit Kerja--</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">CompanyId</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="CompanyID" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">PlanId</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="PlantID" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="DeptID" runat="server" AutoPostBack="true"
                                OnTextChanged="DeptID_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Jabatan</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="BagianID" runat="server"><asp:ListItem Value="">--Pilih Bagian--</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div3" style="background-color:Scrollbar; margin-left:5px; padding:10px; width:100%; vertical-align:top">
                        <asp:GridView ID="ListUserISO" runat="server" BackColor="White" AutoGenerateColumns="False" 
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        GridLines="Horizontal" Width="100%" Font-Size="Smaller" AllowPaging="true" 
                        PagerSettings-Mode="Numeric" 
                        OnRowCommand="ListUserISO_RowCommand"
                        OnRowEditing="ListUserISO_RowEditing"
                        onpageindexchanging="GridView1_PageIndexChanging">
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundField DataField="Nom" HeaderText="#" />
                            <asp:BoundField DataField="ID"  HeaderText="User ID" />
                            <asp:BoundField DataField="UserName"  HeaderText="User Name" />
                            <asp:BoundField DataField="TypeUnitKerjaName"  HeaderText="Type Unit Kerja" />
                            <asp:BoundField DataField="UnitKerjaName"  HeaderText="Unit Kerja" />
                            <asp:BoundField DataField="DeptName" HeaderText="Departement" />
                            <asp:BoundField DataField="bagianname"  HeaderText="Bagian" />
                            <asp:ButtonField CommandName="Add" HeaderText="Pilih" Text="Pilih" />
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </div>  
            </div>
        </div>
    </div>
    
    </div>
</ContentTemplate>
</asp:UpdatePanel>
<script language="javascript" type="text/javascript">
    function Cetak() {
        return false;
    }
</script>
</asp:Content>
