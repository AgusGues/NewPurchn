<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiBaku.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiBaku" %>

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
    .contentlist {border: 0px solid #B0C4DE;}.boxShadow{box-shadow: 5px 0px 10px rgba(0,0,0,0.4);}label{font-size: 12px;}
</style>
<script language="javascript" type="text/javascript" src="../../Script/calendar.js"></script>
<script language="javascript" type="text/javascript" src="../../Script/jquery-1.2.6-vsdoc.js"></script>
<script language="javaScript" type="text/javascript">

    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel() { }

    function Cetak() {
        var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipPakaiBaku", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }

    function confirm_del() {
        if (confirm("Yakin data ini akan dihapus?") == true) {
            document.getElementById('<%= hps.ClientID %>').value = "ya";

        } else {
            document.getElementById('<%= hps.ClientID %>').value = "no";


        }
    }
    function Resethps() {
        document.getElementById('<%=hps.ClientID %>').value = "";
    }

    $(document).ready(function () {
        $("input.org").click(function () {
            $(this).val('ok');
        })
        $('#GridView1').click(function () {
            alert($(this).attr("ID"));
        })
    })
    function confirm_del() {
        if (confirm("Yakin data ini akan dihapus?") == true) {
            $('input.org').val('ya');
        } else {
            $('input.org').val('no');
        }
    }
    function confirm_hapus() {
        if (confirm("Anda yakin untuk akan dihapus ?") == true) {
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
        } else {
            return;
        }
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
    ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel"
    ConfirmOnFormSubmit="false" />
    <div id="Div1" runat="server">
        <div class="panel panel-primary boxShadow">
            <div class="panel-heading">
                <span>Bahan Baku Drawing Slip</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <input id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                    <asp:Button class="btn btn-info" ID="btnUnlok" runat="server" Text="UnLock" ToolTip="Click Unlock to Edit Data by user"
                    OnClick="btnUnlock_ServerClick" Visible="false" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">BBDS No</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div id="Div5" runat="server" class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtTanggal" runat="server" AutoPostBack="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                                runat="server"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kode BBDS</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtPakaiNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <input id="hpuse" runat="server" class="org" type="text" value="" visible="false" />
                                <asp:TextBox ID="hps" runat="server" Visible="false" AutoPostBack="True" OnTextChanged="hps_TextChanged"
                                CssClass="tst"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" width="100%" ID="ddlDeptName" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KodeDept</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtKodeDept" runat="server" AutoPostBack="True"
                                ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ProduksiLine</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" width="100%" ID="ddlProdLine" runat="server" Enabled="false"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Shift</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlShift" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                <asp:ListItem>Shift 1</asp:ListItem>
                                <asp:ListItem>Shift 2</asp:ListItem>
                                <asp:ListItem>Shift 3</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;background-color: transparent;">
                                    <tr id="press" runat="server" visible="true">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-4">KetPressing</div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList class="form-control" ID="ddlTebal" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">CariNamaBarang</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCariNamaBrg" runat="server" AutoPostBack="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NamaBarang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlItemName" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged"></asp:DropDownList>
                                <asp:CheckBox ID="stk" runat="server" Text="Stocked" Checked="true" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ItemCode</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtItemCode" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Stock</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtStok" runat="server" AutoPostBack="True" Enabled="False" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DiBuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtCreatedBy" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Status</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtStatus" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtUomID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                ReadOnly="True" Style="margin-top: 0px" Visible="False" Width="52px"></asp:TextBox>
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
                                <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" Text="Tambah" OnClick="lbAddOP_Click" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="Panel2" runat="server" BackColor="White" Font-Size="Smaller" Height="41px"
                        HorizontalAlign="Center" Width="174px">
                        <div style="padding:2px;"></div>
                        <asp:Button ID="Button1" runat="server" Height="25px" OnClick="Button1_Click" Text="Matikan Alarm ReOrder 1 menit"
                        Width="162px" Font-Size="XX-Small" /></asp:Panel>
                        <span style="font-size: 10pt"><asp:Timer ID="Timer2" runat="server" Enabled="False" Interval="1000" OnTick="Timer2_Tick"></asp:Timer></span>
                        <span style="font-size: 10pt"><asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" OnTick="Timer1_Tick"></asp:Timer></span>
                    </div>
                </div>
                <div class="table-responsive">
                    <div class="boxShadow">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                        Width="100%" Visible="true">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:BoundField DataField="LineNo" HeaderText="Line" />
                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                            <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="true" />
                            <asp:BoundField DataField="ID" Visible="false" />
                        </Columns>
                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                        <table width="100%" style="border-collapse: collapse; font-size: smaller; display: none" visible="true">
                            <tr class="tbHeader tengah">
                                <th class="kotak" style="width: 5%">#</th>
                                <th class="kotak" style="width: 10%">Kode Barang</th>
                                <th class="kotak" style="width: 30%">Nama Barang</th>
                                <th class="kotak" style="width: 12%">Jumlah</th>
                                <th class="kotak" style="width: 8%">UOM</th>
                                <th class="kotak" style="width: 5%">Line</th>
                                <th class="kotak" style="width: 25%">Keterangan</th>
                                <th class="kotak" style="width: 5%">&nbsp;</th>
                            </tr>
                            <asp:Repeater ID="lstSPB" runat="server" OnItemDataBound="lstSPB_ItemDataBound">
                            <ItemTemplate>
                                <tr class="baris" style="background-color: #DCDCDC">
                                    <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                    <td class="kotak kiri"><%# Eval("ItemName") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                    <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                    <td class="kotak tengah"><%# Eval("LineNo") %></td>
                                    <td class="kotak kiri"><%#Eval("Keterangan") %></td>
                                    <td class="kotak tengah">
                                        <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus"
                                        OnClientClick="confirm_hapus()"
                                        CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="baris">
                                    <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                    <td class="kotak kiri"><%# Eval("ItemName") %></td>
                                    <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                    <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                    <td class="kotak tengah"><%# Eval("LineNo") %></td>
                                    <td class="kotak kiri"><%#Eval("Keterangan") %></td>
                                    <td class="kotak tengah">
                                        <asp:Label ID="Act" runat="server">
                                        <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus"
                                        OnClientClick="confirm_hapus();"
                                        CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" /></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate></asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
