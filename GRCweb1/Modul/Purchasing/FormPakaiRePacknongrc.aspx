<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiRePacknongrc.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiRePacknongrc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<script type="text/javascript">
    if (!window.showModalDialog) {
        window.showModalDialog = function (arg1, arg2, arg3) {
            var w;
            var h;
            var resizable = "no";
            var scroll = "no";
            var status = "no";
            var mdattrs = arg3.split(";");
            for (i = 0; i < mdattrs.length; i++) {
                var mdattr = mdattrs[i].split(":");
                var n = mdattr[0];
                var v = mdattr[1];
                if (n) { n = n.trim().toLowerCase(); }
                if (v) { v = v.trim().toLowerCase(); }
                if (n == "dialogheight") {
                    h = v.replace("px", "");
                } else if (n == "dialogwidth") {
                    w = v.replace("px", "");
                } else if (n == "resizable") {
                    resizable = v;
                } else if (n == "scroll") {
                    scroll = v;
                } else if (n == "status") {
                    status = v;
                }
            }
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.focus();
        };
    }
</script>
<script type="text/javascript" src="../../Scripts/calendar.js"></script>
<script type="text/javascript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel()
    { }

    function Cetak() {
        var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipPakaiRepack", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
</script>
</head>

<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel"
ConfirmOnFormSubmit="false" />
<div id="Div1" runat="server" class="table-responsive" style="width:100%">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <span>Pemakaian Re-Pack Non GRC</span>
            <div class="pull-right">
                <input class="btn btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                <input class="btn btn-info" id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                <asp:ListItem Value="ScheduleNo">RPDS No</asp:ListItem></asp:DropDownList>
                <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
            </div>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">Tanggal</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                            runat="server"></cc1:CalendarExtender>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">No Mds</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtPakaiNo" runat="server" ReadOnly="True" ></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">KodeDept</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtKodeDept" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">NamaDept</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">CariNamaBarang</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtCariNamaBrg" runat="server" AutoPostBack="True"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">NamaBarang</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlItemName" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">KodeBarang</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtItemCode" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">DiBuatOleh</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtCreatedBy" runat="server" ReadOnly="True"
                            ></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">Stock</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" width="100%" ID="txtStok" runat="server" AutoPostBack="True" Enabled="False" ReadOnly="True"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">Satuan</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtUom" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                            <asp:TextBox ID="txtUomID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                            ReadOnly="True" Visible="False" Width="52px" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                            ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">QtyPakai</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtQtyPakai" runat="server"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">Keterangan</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtKeterangan" runat="server"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" OnClick="lbAddOP_Click"
                            Text="Tambah" />
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <div id="div2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                        <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                        <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                        <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="False" />
                    </Columns>
                    <RowStyle Font-Names="tahoma" Font-Size="X-Small" BackColor="WhiteSmoke" />
                    <HeaderStyle Font-Names="tahoma" Font-Size="X-Small" BackColor="RoyalBlue" BorderColor="#404040"
                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="white" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
