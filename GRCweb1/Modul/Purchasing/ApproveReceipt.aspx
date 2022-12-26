<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveReceipt.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApproveReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
<script language="JavaScript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approve Receipt</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnSebelumnya" runat="server" type="button" value="Sebelumnya" onserverclick="btnSebelumnya_ServerClick" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Approve" onserverclick="btnUpdate_ServerClick" />
                    <input class="btn btn-info" id="btnSelanjutnya" runat="server" type="button" value="Selanjutnya" onserverclick="btnSesudahnya_ServerClick" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List Open Receipt" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="110px">
                    <asp:ListItem Value="ReceiptNo">No Receipt</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">KodeReceipt</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoReceipt" runat="server"
                                ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeSpp</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtNoSPP" runat="server" AutoPostBack="True"
                                Enabled="False" Font-Bold="True"
                                ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodePo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPONo" runat="server" ReadOnly="True"
                                ></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True"
                                ></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DiBuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCreatedBy" runat="server" AutoPostBack="True"
                                ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div2">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        Width="100%" OnRowCommand="GridView1_RowCommand"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="PONo" HeaderText="No PO" />
                            <asp:BoundField DataField="SPPNo" HeaderText="No SPP" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />

                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
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
