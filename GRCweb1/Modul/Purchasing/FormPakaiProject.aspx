<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiProject.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiProject" %>
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
<script language="JavaScript" type="text/javascript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }
    function onCancel()
    { }

    function Cetak() {
        var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipPakaiProject", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
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
</script>
<script type="text/javascript">
    function MyPopUpWin(url, width, height) {
        var leftPosition, topPosition;
        leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
        topPosition = (window.screen.height / 2) - ((height / 2) + 50);
        window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
    }
    function Cetak() {
        MyPopUpWin("../Report/Report.aspx?IdReport=SlipPakaiProject", 900, 800)
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
    ConfirmText="Anda yakin untuk Cancel " OnClientCancel="onCancel" ConfirmOnFormSubmit="false" />
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Project Drawing Slip</span>
                <div class="pull-right">
                    <input class="bnt btn-info" id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                    <input class="bnt btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                    <asp:Button class="bnt btn-info" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                    <input class="bnt btn-info" id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                    <input class="bnt btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">PDS No</asp:ListItem></asp:DropDownList>
                    <asp:TextBox  ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="bnt btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
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
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kode Pds</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPakaiNo" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">NamaDept</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"></asp:DropDownList>
                                <asp:TextBox ID="txtKodeDept" runat="server" AutoPostBack="True"
                                BorderStyle="Groove" ReadOnly="True" Visible="false" Width="79px"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;">
                                    <tr id="projectNew" runat="server">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-4">NamaProject</div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList class="form-control" ID="ddlProjectName" runat="server"
                                                    OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">ProjectProgres</div>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <asp:TextBox class="form-control" ID="txtProgres" runat="server" ReadOnly="true"></asp:TextBox>
                                                        <span class="input-group-addon" style="height: 18px;padding:2px 2px;">%</span>
                                                    </div>
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
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">SpGroup</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlSpGroup" runat="server" width="100%"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Stock</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtStok" runat="server" AutoPostBack="True" Enabled="False" ReadOnly="True" width="100%"></asp:TextBox>
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
                                ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <asp:TextBox ID="txtItemTypeID" runat="server" AutoPostBack="True"
                                BorderStyle="Groove" ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <asp:TextBox ID="txtAvgPrice" runat="server" AutoPostBack="True"
                                BorderStyle="Groove" ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DiBuatOleh</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtCreatedBy" runat="server" ReadOnly="True"></asp:TextBox>
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
                                <asp:Button class="btn btn-info" ID="lbAddOP" runat="server" OnClick="lbAddOP_Click" Text="Tambah" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div2" style=" width: 100%; height: 250px; overflow: auto; border: 2px solid #B0C4DE; background-color: White">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                            <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                            <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="False" />
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
