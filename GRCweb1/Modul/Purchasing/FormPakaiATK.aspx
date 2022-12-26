<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiATK.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiATK" %>
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

    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel()
    { }

    function Cetak() {
        var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipPakaiATK", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 200px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
<ContentTemplate>    
    <div id="Div1" runat="server">  
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>ATK Drawing Slip</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <input class="btn btn-info" id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">ADS No</asp:ListItem></asp:DropDownList>
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
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NamaDept</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True" width="100%" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Kode Ads</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPakaiNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;">
                                    <tr id="forbudget" runat="server">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-4">Budget Periode</div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlBulanBdg" runat="server" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlBulanBdg_Click">
                                                    <asp:ListItem>--Pilih Bulan--</asp:ListItem></asp:DropDownList>&nbsp;
                                                    <asp:DropDownList ID="ddlTahunBdg" runat="server">
                                                    <asp:ListItem>--Pilih Tahun</asp:ListItem></asp:DropDownList> 
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Cari NamaBarang</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCariNamaBrg" runat="server" AutoPostBack="True"></asp:TextBox>
                                <asp:CheckBox ID="stk" runat="server" Text="Stocked" Checked="true"/>
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
                                <asp:TextBox class="form-control" ID="txtItemCode" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeDept</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKodeDept" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">DibuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtCreatedBy" runat="server" ReadOnly="True" Width="140"></asp:TextBox>
                                <asp:TextBox ID="txtmati" runat="server" ReadOnly="True" Width="140"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Stock</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtStok" runat="server" Enabled="False" ReadOnly="True" width="100%"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Satuan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUom" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtUomID" runat="server" ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <asp:TextBox ID="txtGroupID" runat="server" ReadOnly="True" Style="margin-top: 0px" Visible="False" Width="52px"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyPakai</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQtyPakai" runat="server"></asp:TextBox>
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
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" Text="Add Item" OnClick="lbAddOP_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div2" style="height: 210px;" onscroll="setScrollPosition(this.scrollTop);">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                            <asp:BoundField DataField="matikan" HeaderText="Status" />
                            <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="False" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                        <table class="tbStandart">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width:4%">No.</th>
                                    <th class="kotak" style="width:10%">ItemCode</th>
                                    <th class="kotak" style="width:20%">Item Name</th>
                                    <th class="kotak" style="width:8%">Jumlah</th>
                                    <th class="kotak" style="width:6%">UOM</th>
                                    <th class="kotak" style="width:25%">Keterangan</th>
                                    <th class="kotak" style="width:5%">Status</th>
                                    <th class="kotak" style="width:5%">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstATK" runat="server" OnItemDataBound="lstATK_DataBound" OnItemCommand="lstATK_Command">
                                <ItemTemplate>
                                    <tr class="EvenRows baris">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %>&nbsp;
                                            <asp:CheckBox ID="chk" AutoPostBack="true" runat="server" OnCheckedChanged="chk_change" ToolTip='<%# Container.ItemIndex %>' />
                                        </td>
                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                        <td class="kotak angka"><asp:TextBox ID="txtQty" runat="server" CssClass="txtongrid angka" Text='<%# Eval("Quantity","{0:N2}")%>'></asp:TextBox></td>
                                        <td class="kotak"><%# Eval("UOMCode")%></td>
                                        <td class="kotak"><%# Eval("Keterangan")%></td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtmatikan" runat="server" CssClass="txtongrid angka" Text='<%# Eval("Matikan","{0:N2}")%>'></asp:TextBox>
                                            <%--<asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Container.ItemIndex+1 %>' CommandName="edit" />
                                            <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Container.ItemIndex+1 %>' CommandName="del" />
                                            <asp:ImageButton ID="edt1" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID")%>' CommandName="edit2" />
                                            <asp:ImageButton ID="del1" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%#  Eval("ID") %>' CommandName="del2" />
                                            --%>
                                        </td>
                                        <td class="kotak"></td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="OddRows baris">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %>&nbsp;
                                            <asp:CheckBox ID="chk" AutoPostBack="true" runat="server" OnCheckedChanged="chk_change" ToolTip='<%# Container.ItemIndex %>' />
                                        </td>
                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                        <td class="kotak angka"><asp:TextBox ID="txtQty" runat="server" CssClass="txtongrid angka" Text='<%# Eval("Quantity","{0:N2}")%>'></asp:TextBox></td>
                                        <td class="kotak"><%# Eval("UOMCode")%></td>
                                        <td class="kotak"><%# Eval("Keterangan")%></td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtmatikan" runat="server" CssClass="txtongrid angka" Text='<%# Eval("Matikan","{0:N2}")%>'></asp:TextBox>
                                            <%--<asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Container.ItemIndex+1 %>' CommandName="edit" />
                                            <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Container.ItemIndex+1 %>' CommandName="del" />
                                            <asp:ImageButton ID="edt1" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID")%>' CommandName="edit2" />
                                            <asp:ImageButton ID="del1" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%#  Eval("ID") %>' CommandName="del2" />
                                            --%>
                                        </td>
                                        <td class="kotak"></td>
                                    </tr>
                                </AlternatingItemTemplate></asp:Repeater>
                                <tr id="notFound" runat="server" visible="false">
                                    <td class="kotak" colspan="7">&nbsp;&nbsp;&bull;&nbsp;Data not found</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>        
    </div>     
</ContentTemplate>     
</asp:UpdatePanel>
</asp:Content>
