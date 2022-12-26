<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePakaiNew.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApprovePakaiNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 5px 4px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    textarea{color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    input,select,.form-control,select.form-control,
    select.form-group-sm .form-control{height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    td{border: 0px solid #d5d5d5}
    .table > tbody > tr > th {padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}
</style>
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

<script language="JavaScript" type="text/javascript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>ApprovePemakaian</span>
                <div class="pull-right">
                    <input class="btn btn-primary" id="btnSebelumnya" runat="server" type="button" value="Sebelumnya" onserverclick="btnSebelumnya_ServerClick" />
                    <input class="btn btn-primary" id="btnUpdate" runat="server" type="button" value="Approve" onserverclick="btnUpdate_ServerClick" />
                    <input class="btn btn-primary" id="btnSelanjutnya" runat="server" type="button" value="Selanjutnya" onserverclick="btnSesudahnya_ServerClick" />
                    <input class="btn btn-primary" id="btnList" runat="server" type="button" value="List Open Pakai" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="Pakaino">No Pakai</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-primary" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">PakaiNo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPakaiNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeDept</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKodeDept" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">NameDept</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Stock</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtStok" runat="server" AutoPostBack="True" Enabled="False" ReadOnly="True" width="100%"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                                runat="server"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DibuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCreatedBy" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Panel ID="Panel2" runat="server" BackColor="White" Font-Size="Smaller" Height="67px"
                            HorizontalAlign="Center" Width="218px" Visible="False">
                            <br />
                            <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Ada Permintaan Barang Baru...."></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="Button1" runat="server" Font-Size="XX-Small" Height="21px" OnClick="Button1_Click"
                            Text="Refresh Data" Width="93px" /></asp:Panel>
                            <div style="padding:2px;"></div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div2">
                        <span style="font-size: 11pt">&nbsp; <strong>List</strong></span>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand">
                        <%--onrowdatabound="GridView1_RowDataBound">--%>
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                            <%--<asp:ButtonField CommandName="Add" Text="Pilih" /><asp:ButtonField CommandName="AddDelete" Text="Hapus" />--%>
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
        <table style="table-layout: fixed" width="100%" border="0px">
            <tbody>
                <tr>
                    <td>
                        <div style=" width: 100%; padding:5px; background-color:#fff;">
                            <table id="Table4" style="width: 100%;" border="0px">
                                <tr>
                                    <td style="width: 204px; height: 19px;">
                                        <span style="font-size: small">
                                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                        <span style="font-size: small">
                                            <asp:Timer ID="Timer2" runat="server" Interval="1000" OnTick="Timer2_Tick" Enabled="False"></asp:Timer>
                                        </span>
                                    </td>
                                </tr>
                                <tr id="info" runat="server">
                                    <td align="right" colspan="3" style="height: 19px">
                                        <asp:Label ID="LblInfo" runat="server" Font-Size="X-Small" Text="Kirim Informasi ketersediaan barang kepada User"></asp:Label>
                                        &nbsp;
                                        <asp:CheckBox ID="ChkReady" runat="server" AutoPostBack="True" OnCheckedChanged="ChkReady_CheckedChanged"
                                        Text="Barang siap diambil" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 19px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
