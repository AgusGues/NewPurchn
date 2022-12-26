<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPakaiBaku.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListPakaiBaku" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 1px 1px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}
    label{font-size: 12px;}
</style>
<script type="text/javascript">
    function openWindow() {
        window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
    }
</script>  
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Daftar Pemakaian</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Form" onserverclick="btnUpdate_ServerClick" />
                    <input class="btn btn-info" id="btnlist" runat="server" type="button" value="List" onserverclick="btnlist_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="100px">
                    <asp:ListItem Value="ScheduleNo">No Pakai</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="100px"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6 col-xs-12">
                        <div class="row">
                            <div class="col-md-4 col-xs-6">
                                <asp:RadioButton ID="RBAll"
                                runat="server" AutoPostBack="True" Checked="True" GroupName="g1" OnCheckedChanged="RBAll_CheckedChanged"
                                Text="Semua Pemakaian" />
                            </div>
                            <div class="col-md-4 col-xs-6">
                                <asp:RadioButton ID="RBAll0" runat="server" AutoPostBack="True" GroupName="g1" 
                                OnCheckedChanged="RBAll0_CheckedChanged" Text="Barang siap diambil" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div2" style="width: 100%; overflow: auto; padding:10px; background-color:#fff;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                        <Columns>
                            <asp:BoundField DataField="PakaiNo" HeaderText="No Pakai" />
                            <asp:BoundField DataField="PakaiDate" HeaderText="Tanggal" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="Satuan" />
                            <asp:BoundField HeaderText="Apv" DataField="Apv" />
                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>     
</asp:Content>
