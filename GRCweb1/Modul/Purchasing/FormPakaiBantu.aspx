<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiBantu.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiBantu" %>
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
<script language="Javascript" type="text/javascript" src="../../Script/calendar.js"></script>  
<script language="JavaScript" type="text/javascript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel() { }
    function Cetak() {
        var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipPakaiBantu", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
    function infoupdate() {
        window.showModalDialog('../ModalDialog/InfoUpdate.aspx', '', 'resizable:yes;dialogheight: 500px; dialogWidth: 750px;scrollbars=yes; toolbar:no');
    }
    function confirm_hapus() {
        if (confirm("Anda yakin untuk akan dihapus ?") == true) {
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
        } else {
            return false;
        }
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
<ContentTemplate>    
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
    ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel" ConfirmOnFormSubmit="false" />    
    <div id="Div1" runat="server" class="table-responsive" >
        <div class="panel panel-primary boxShadow">
            <div class="panel-heading">
                <span>Bahan Bantu Drawing Slip</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <input class="btn btn-info" id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                    <asp:Button class="btn btn-info" ID="btnUnlok" runat="server" Text="UnLock" Visible="false" OnClick="btnUnlok_Click" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">BTDS No</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick"/>
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
                            <div class="col-md-4">No Btds</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtPakaiNo" runat="server" ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox class="form-control" width="100%" ID="txtKodeDept" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
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
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;background-color: transparent;">
                                    <tr id="shift" runat="server" visible="false">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-4">Shift</div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList class="form-control" ID="ddlShift" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                    <asp:ListItem>Shift 1</asp:ListItem>
                                                    <asp:ListItem>Shift 2</asp:ListItem>
                                                    <asp:ListItem>Shift 3</asp:ListItem></asp:DropDownList>
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;background-color: transparent;">
                                    <tr id="press" runat="server" visible="false">
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
                                <asp:CheckBox ID="stk" runat="server" Text="Stocked"
                                Checked="true" />
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
                        <div class="row">
                            <div class="col-md-4">Satuan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUom" runat="server" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtUomID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                ReadOnly="True" Style="margin-top: 0px" Visible="False" Width="52px"></asp:TextBox>
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
                                <asp:TextBox class="form-control" width="100%" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">QtyPakai</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtQtyPakai" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtAvgPrice" runat="server" Visible="False" Width="104px"></asp:TextBox>
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
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">SpGroup</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" width="100%" ID="spGroup" runat="server" OnSelectedIndexChanged="spGroup_SelectedIndexChanged"
                                AutoPostBack="true"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <span id="spZona" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-4">Zona</div>
                                        <div class="col-md-8">
                                            <asp:DropDownList class="form-control" ID="ddlZona" runat="server"></asp:DropDownList>
                                            <div style="padding:2px;"></div>
                                        </div>
                                    </div>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"><span id="x1" runat="server" visible="true">&nbsp;No. Kendaraan</span></div>
                            <div class="col-md-8">
                                <span id="x2" runat="server" visible="true">
                                    <asp:DropDownList class="form-control" ID="ddlNoPolisi" runat="server" OnSelectedIndexChanged="ddlNoPolisi_SelectedIndexChanged"
                                    AutoPostBack="true"></asp:DropDownList>
                                    &nbsp;
                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Plant" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/>
                                </span>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"><span id="prj" runat="server" style="font-size: 10pt" visible="false">&nbsp;&nbsp;Project Name</span></div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlProjectName" runat="server" Visible="false"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"><span id="frk" runat="server" style="font-size: 10pt">&nbsp;</span></div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlForklif" runat="server" Visible="false"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-right">
                        <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" OnClick="lbAddOP_Click" Text="Tambah" />
                        <div style="padding:2px;"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="Panel2" runat="server" BackColor="LightSteelBlue" Font-Size="Smaller"
                        Height="35px" HorizontalAlign="Center" Width="174px">
                        <asp:Button ID="Button1" runat="server" Font-Size="XX-Small" Height="30px" OnClick="Button1_Click"
                        Text="Matikan Alarm ReOrder 1 menit" Width="162px" /></asp:Panel>
                        <span style="font-size: 10pt">
                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                        </span>
                        <span style="font-size: 10pt">
                            <asp:Timer ID="Timer2" runat="server" Interval="1000" OnTick="Timer2_Tick"></asp:Timer>
                        </span>
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
                            <asp:BoundField DataField="LineNo" HeaderText="Line" />
                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                            <asp:ButtonField CommandName="AddDelete"  Text="Hapus" Visible="False" />
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
