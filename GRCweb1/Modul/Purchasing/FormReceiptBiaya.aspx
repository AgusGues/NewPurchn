<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormReceiptBiaya.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormReceiptBiaya" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
<script language="JavaScript" type="text/javascript">
    function imgChange(img) { document.LookUpCalendar.src = img;}
    function onCancel() { }
    function Cetak() {
        var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipBiaya", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }
    function confirm_delete() {
        (confirm("Anda yakin untuk Cancel ?") == true) ?
        window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes') :
        '';
    }
    function infoupdate() {

        window.showModalDialog('../ModalDialog/InfoUpdate.aspx', '', 'resizable:yes;dialogheight: 500px; dialogWidth: 750px;scrollbars=yes; toolbar:no');
    }
    function Autospb()
    {
        window.showModalDialog('../ModalDialog/AutoSPB.aspx','','resizable:yes;dialogheight: 500px; dialogWidth: 750px;scrollbars=yes; toolbar:no');
    }
</script>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
    ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel"
    ConfirmOnFormSubmit="false" />
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Biaya Receipt Slip</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnPrint" runat="server" Text="Cetak" OnClientClick="Cetak()" />
                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="List" OnClick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">BRS No</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                    <asp:Button class="btn btn-info" ID="btnSearch" runat="server" Text="Cari" OnClick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True" width="100%"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" 
                                TargetControlID="txtTanggal" 
                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kode Brs</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtMrsNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">CariPo</div>
                            <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtCariOP" runat="server" AutoPostBack="True" width="100%"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"><span id="xref" runat="server">&nbsp;No.Reff.</span></div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoReff" runat="server" Visible="false"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodePo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPONo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">NamaBarang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlItemName" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ItemCode</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtItemCode" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="bItemID" runat="server" Visible="false"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeSpp</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoSpp" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">NamaDept</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">JenisBiaya</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlJenisBiaya" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlJenisBiaya_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-4">
                                <span ID="spZona" runat="server" style="font-size: 10pt" visible="false">&nbsp; Zona Maintenance</span>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlZona" runat="server" Visible="false"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <span ID="x1" runat="server" style="font-size: 10pt" visible="false">&nbsp; No. Kendaraan</span>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlNoPolisi" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlNoPolisi_SelectedChange" Visible="false"></asp:DropDownList>
                                &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" 
                                OnCheckedChanged="CheckBox1_CheckedChanged" Text="Plant" Visible="false" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"><span id="frk" runat="server" style="font-size: 10pt">&nbsp;&nbsp;</span></div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlForklif" runat="server" CssClass="gambar" Visible="false"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">SpGroup</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="spGroup" runat="server" 
                                OnSelectedIndexChanged="spGroup_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DiBuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCreatedBy" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Stock</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtStok" runat="server" AutoPostBack="True" ReadOnly="True" Enabled="false"></asp:TextBox>
                                &nbsp;<asp:CheckBox ID="chkAutoSPB" runat="server" Text="Auto SPB" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyPo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQty" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtPrice" runat="server" ReadOnly="True" Visible="False" Width="80%"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Satuan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUom" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyTerima</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQtyTerima" runat="server"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Keterangan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKeterangan" runat="server"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">MtcProject</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="DdlMtcProject" runat="server"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn-info" ID="lbAddOP" runat="server" OnClick="lbAddOP_Click" Text="Tambah" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <!-- <div id="div2" style="height: 200px; overflow: auto" class="contentlist"> -->
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="PONo" HeaderText="No PO" />
                            <asp:BoundField DataField="SPPNo" HeaderText="No SPP" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                    <!-- </div> -->
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
