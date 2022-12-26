<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListReceiptMRS.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListReceiptMRS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 1px 1px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control,
    td{height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th {padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}
    label{font-size: 12px;}
    .scroll-y{overflow-y: scroll;height: 450px;}
</style>
<script language="JavaScript" type="text/jscript">
    function openWindow() {
    window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
}
</script>  

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>List Receipt</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Form" onserverclick="btnUpdate_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server">
                    <asp:ListItem Value="ScheduleNo">No Receipt</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div id="div2" class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowCommand="GridView1_RowCommand" 
                    OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="17">
                    <Columns>
                        <asp:BoundField DataField="ReceiptNo" HeaderText="No MRS" />
                        <asp:BoundField DataField="ReceiptDate" HeaderText="Tanggal" />
                        <asp:BoundField DataField="PONo" HeaderText="No PO" />
                        <asp:BoundField DataField="SPPNo" HeaderText="No SPP" />
                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                        <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                        <asp:BoundField DataField="Quantity" DataFormatString="{0:N0}" HeaderText="Jumlah" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                    </Columns>
                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" />
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>   
</asp:Content>
