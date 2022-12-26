<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormReceiptMRS.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormReceiptMRS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

<script language="javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

<script language="javaScript" type="text/javascript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel()
    { }

    function Cetak() {
        var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipMRS", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>


    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
    ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel" ConfirmOnFormSubmit="false" />

    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Material Receipt Slip</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold;
                    font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <input class="btn btn-info" id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">MRS No</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick"/>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">JenisReceipt</div>
                            <div class="col-md-8">
                                <asp:RadioButton ID="RadioME" runat="server" Checked="True" Font-Size="X-Small" GroupName="g1" Text="Mekanik dan Elektrik" />
                                &nbsp;<asp:RadioButton ID="RadioP" runat="server" Font-Size="X-Small" GroupName="g1" Text="Projek" />
                                &nbsp;<asp:RadioButton ID="RadioN" runat="server" Font-Size="X-Small" GroupName="g1" 
                                Text="Non GRC" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeRms</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtMrsNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">CariPo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtCariOP" runat="server" AutoPostBack="True"></asp:TextBox>
                                <cc1:AutoCompleteExtender runat="server" ID="autoCariPO" CompletionListCssClass="autocomplete_completionListElement" 
                                TargetControlID="txtCariOP" ServiceMethod="GetNoPO" ServicePath="AutoComplete.asmx" EnableCaching="true" 
                                CompletionInterval="100" CompletionSetCount="10" MinimumPrefixLength="6"></cc1:AutoCompleteExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodePo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPONo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Supplier</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtSupplier" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">NamaBarang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeBarang</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtItemCode" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>               
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">KodeSpp</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoSpp" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Stok</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtStok" runat="server" AutoPostBack="True" Enabled="False" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyPo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQty" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtPrice" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Satuan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUom" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyTerima</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQtyTerima" runat="server"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Keterangan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKeterangan" runat="server"></asp:TextBox>
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
                        <div class="row">
                            <div class="col-md-4">Status</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="stsSPPmg" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" Text="Add Item" OnClick="lbAddOP_Click"/>
                            </div>
                            <div style="padding:2px;"></div>
                        </div>
                    </div>
                </div>
                <div style="padding:2px;"></div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" Width="100%" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="PONo" HeaderText="No PO" />
                        <asp:BoundField DataField="SPPNo" HeaderText="No SPP" />
                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                        <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                        <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                        <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                    </Columns>
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                    <table width="100%" style="border-collapse:collapse; font-size:x-small">
                        <thead>
                            <tr class="tbHeader tengah">
                                <th class="kotak" style="width:4%">No.</th>
                                <th class="kotak" style="width:8%">No.PO</th>
                                <th class="kotak" style="width:8%">No.SPP</th>
                                <th class="kotak" style="width:10%">ItemCode</th>
                                <th class="kotak" style="width:20%">ItemName</th>
                                <th class="kotak" style="width:10%">Jumlah</th>
                                <th class="kotak" style="width:5%">Unit</th>
                                <th class="kotak" style="width:15%">Keterangan</th>
                                <th class="kotak" style="width:5%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="lstRMS" runat="server" OnItemDataBound="lstRMS_DataBound" OnItemCommand="lstRMS_ItemCommand">
                            <ItemTemplate>
                                <tr class="baris EvenRows">
                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                    <td class="kotak tengah"><%# Eval("PONo") %></td>
                                    <td class="kotak tengah"><%# Eval("SPPNo") %></td>
                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                    <td class="kotak " nowrap="nowrap"><%# Eval("ItemName") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %>&nbsp;</td>
                                    <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                    <td class="kotak "><%# Eval("Keterangan") %></td>
                                    <td class="kotak tengah">
                                        <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                        <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del"  onclientclick="javascript:return confirm('Are you sure to delete record?')" />
                                        <asp:ImageButton ID="lstLock" ImageUrl="~/images/lock_closed.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="unlock" />
                                        <asp:ImageButton ID="lstUnLock" ImageUrl="~/images/lock_open.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="lock" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="baris OddRows">
                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                    <td class="kotak tengah"><%# Eval("PONo") %></td>
                                    <td class="kotak tengah"><%# Eval("SPPNo") %></td>
                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                    <td class="kotak " nowrap="nowrap"><%# Eval("ItemName") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %>&nbsp;</td>
                                    <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                    <td class="kotak "><%# Eval("Keterangan") %></td>
                                    <td class="kotak tengah">
                                        <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                        <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del"  onclientclick="javascript:return confirm('Are you sure to delete record?')" />
                                        <asp:ImageButton ID="lstLock" ImageUrl="~/images/lock_closed.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="unlock" />
                                        <asp:ImageButton ID="lstUnLock" ImageUrl="~/images/lock_open.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="lock" />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate></asp:Repeater>
                        </tbody>
                    </table>  
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
