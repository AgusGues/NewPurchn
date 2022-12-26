<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPOPending.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListPOPending" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 24px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>ListPendingPo</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Form Approval PO" onserverclick="btnUpdate_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">No PO</asp:ListItem> </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <div>
                        <asp:HiddenField ID="txtGroup" runat="server" />
                        <div style="height: 450px; overflow: auto">
                            <asp:GridView ID="lstPO" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="lstPO_DataBound"
                            DataKeyNames="NoPO" Visible="false">
                            <Columns>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" style="cursor:pointer" src="../../images/next.gif" />
                                    <asp:Panel ID="pnlOrder" runat="server" Style="display: block">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="30">
                                    <Columns>
                                        <asp:BoundField DataField="NoPO" HeaderText="No. PO" />
                                        <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                        <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                        <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                        <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                        <asp:BoundField DataField="Price" HeaderText="H a r g a" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" /> </asp:GridView>
                                </asp:Panel>  </ItemTemplate>  </asp:TemplateField>
                                <asp:BoundField ItemStyle-Width="10%" DataField="NoPO" HeaderText="No.PO" />
                                <asp:BoundField ItemStyle-Width="25%" DataField="SupplierName" HeaderText="SupplierName" />
                                <asp:BoundField ItemStyle-Width="10%" DataField="PoPurchnDate" HeaderText="Tanggal" />
                                <asp:BoundField ItemStyle-Width="10%" DataField="Delivery" HeaderText="Term Of Del" />
                                <asp:BoundField ItemStyle-Width="10%" DataField="Approval" HeaderText="Approval" /> </Columns>  </asp:GridView>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="GridView1_RowCommand" 
                                OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="30">
                                <Columns>
                                    <asp:BoundField DataField="NoPO" HeaderText="No. PO" />
                                    <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                    <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                    <asp:BoundField DataField="Qty" HeaderText="Q t y" DataFormatString="{0:N2}" >
                                    <ItemStyle HorizontalAlign="Right" />  </asp:BoundField>
                                    <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                    <asp:BoundField DataField="Price" HeaderText="H a r g a" 
                                    DataFormatString="{0:N3}" >
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" /> </asp:BoundField>
                                    <asp:BoundField DataField="Approval" HeaderText="App" />
                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                </Columns>
                                <RowStyle Font-Names="tahoma" Font-Size="X-Small" BackColor="WhiteSmoke" />
                                <HeaderStyle Font-Names="tahoma" Font-Size="X-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="white" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />  </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>    
</asp:Content>
