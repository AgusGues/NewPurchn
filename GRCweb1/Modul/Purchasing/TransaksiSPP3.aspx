<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransaksiSPP3.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.TransaksiSPP3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 8px 8px 8px;
            overflow-x: auto;
            min-height: .01%;
        }

        .btn {
            font-style: normal;
            border: 1px solid transparent;
            padding: 2px 4px;
            font-size: 11px;
            height: 24px;
            border-radius: 4px;
        }

        input, select, .form-control, select.form-control, select.form-group-sm .form-control {
            height: 24px;
            color: #000;
            padding: 2px 4px;
            font-size: 12px;
            border: 1px solid #d5d5d5;
            border-radius: 4px;
        }

        .table > tbody > tr > th, .table > tbody > tr > td {
            border: 0px solid #fff;
            padding: 2px 4px;
            font-size: 12px;
            color: #fff;
            font-family: sans-serif;
        }

        .contentlist {
            border: 0px solid #B0C4DE;
        }

        label {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        // fix for deprecated method in Chrome / js untuk bantu view modal dialog
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {
                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";
                // get the modal specs
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
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable +
                    ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
    </script>
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

        function Cetak() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipSPP", "", "resizable:yes;dialogheight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        function confirm_batal() {
            if (confirm("Anda yakin untuk Batal ?") == true)
                window.showModalDialog('../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
        function confirm_close() {
            if (confirm("Anda yakin untuk Close SPP ini ?") == true) {
                window.showModalDialog('../ModalDialog/ReasonClose.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
            } else {
                return false;
            }
        }
        function confirm_hapus() {
            if (confirm("Anda yakin untuk akan dihapus ?") == true)
                window.showModalDialog('../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
        function ShowHist(id) {
            alert(id);
        }
        function ShowInfos() {
            window.showModalDialog('../ModalDialog/InfoUpdate.aspx', '', 'resizable:yes;dialogheight: 500px; dialogWidth: 700px;scrollbars=yes; toolbar:no');
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div10" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td>
                                <!--panel header-->
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;INPUT SPP</strong>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                Text="Cancel" OnClick="btnCancel_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnClose" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                Text="Close" OnClick="btnClose_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <input id="btnCetak" onclick="Cetak()" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cetak" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnList" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="List" onserverclick="btnList_ServerClick" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="NoSPP">No SPP</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                        </td>
                                        <td style="width: 3px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- end panel header-->
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <div>
                                    <%--panel cek stok--%>
                                    <table id="table4" style="width: 100%; font-size: smaller; border: 1px solid #A0B0E0; border-collapse: collapse">
                                        <tr>
                                            <td colspan="5" align="right" style="background-color: #A0B0E0">
                                                <asp:TextBox ID="alasan" runat="server" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblCheck" runat="server" Text="Label"></asp:Label>
                                                &nbsp;&nbsp;<asp:RadioButton ID="rbOn" runat="server" GroupName="1" Text="ON" />
                                                &nbsp;<asp:RadioButton ID="rbOff" runat="server" GroupName="1" Text="OFF" Checked="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Height="101px" ScrollBars="Vertical" Visible="false">
                                                    <asp:Label ID="LblStock" runat="server" Font-Bold="true" Text="Stock "></asp:Label>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                            <asp:BoundField DataField="Jumlah" HeaderText="Stock" />
                                                        </Columns>
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="true" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--end panel cek stok --%>
                                    <div id="Div5" runat="server" style="font-size: smaller; background-color: #B0C4DE">
                                        <table id="TblIsi" runat="server" border="0" style="border-collapse: collapse; width: 100%">
                                            <tr style="display: none">
                                                <td colspan="5" style="border-bottom: 2px solid">&nbsp
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 151px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Tanggal Input </span>
                                                </td>
                                                <td style="width: 204px;" valign="top">
                                                    <asp:TextBox ID="txtTglInput" runat="server" BorderStyle="Groove" ReadOnly="true"
                                                        Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 134px;" valign="top">&nbsp;<span style="font-size: 10pt">&nbsp;No SPP</span>
                                                </td>
                                                <td style="width: 205px;" valign="top">
                                                    <asp:TextBox ID="txtSPP" runat="server" BorderStyle="Groove" ReadOnly="true" Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 205px; border-left: 2px solid; padding: 3px" valign="top" rowspan="9">
                                                    <font style="font-size: x-small; text-align: justify">Multi Gudang:<br />
                                                        &nbsp;&bull;&nbsp;Public :Barang boleh diambil oleh semua Departemen<br />
                                                        &nbsp;&bull;&nbsp;Private:Barang hanya boleh diambil oleh Departemen pembuat SPP
                                                        <hr />
                                                        Info :<br />
                                                        &nbsp;&bull;&nbsp;Last PO.No :<asp:Label ID="LastPO" runat="server"></asp:Label><br />
                                                        &nbsp;&bull;&nbsp;Last RMS.No :<asp:Label ID="LastRMS" runat="server"></asp:Label><hr />
                                                        LeadTime :
                                                        <asp:Label ID="ldTime" runat="server"></asp:Label>
                                                    </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 151px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Tipe Barang</span>
                                                </td>
                                                <td style="width: 204px;" valign="top">
                                                    <asp:DropDownList ID="ddlTipeBarang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeBarang_SelectedIndexChanged"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 134px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Jumlah Stok</span>
                                                </td>
                                                <td style="width: 205px;" valign="top" colspan="2">
                                                    <asp:TextBox ID="txtStok" runat="server" BorderStyle="Groove" ReadOnly="true" Width="233"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 151px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Permintaan</span>
                                                </td>
                                                <td rowspan="1" style="width: 204px;">
                                                    <asp:DropDownList ID="ddlMinta" runat="server" Width="233px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlMinta_SelectedIndexChanged">
                                                        <asp:ListItem>Top Urgent</asp:ListItem>
                                                        <asp:ListItem>Biasa</asp:ListItem>
                                                        <asp:ListItem>Sesuai Schedule</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 134px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Jumlah Max Stock</span>
                                                </td>
                                                <td style="width: 205px;" valign="top" colspan="2">
                                                    <asp:TextBox ID="txtJmlMax" runat="server" BorderStyle="Groove" ReadOnly="true" Width="233"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 151px;">
                                                    <span style="font-size: 10pt">&nbsp; Tipe SPP </span>
                                                </td>
                                                <td rowspan="1" style="width: 204px;">
                                                    <asp:DropDownList ID="ddlTipeSPP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 134px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Status</span>
                                                </td>
                                                <td rowspan="1" style="width: 204px;" colspan="2">
                                                    <asp:TextBox ID="txtStatus" runat="server" BorderStyle="Groove" ReadOnly="true" Width="233"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp; Dibuat Oleh&nbsp;
                                                </td>
                                                <td rowspan="1" style="width: 204px;">
                                                    <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" ReadOnly="true"
                                                        Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 134px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Nama Head </span>
                                                </td>
                                                <td rowspan="1" style="width: 204px;" colspan="2">
                                                    <asp:TextBox ID="txtNamaHead" runat="server" BorderStyle="Groove" onkeyup="this.value=this.value.toUpperCase()"
                                                        ReadOnly="true" Width="233"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <%-- Munculin pilihan nama asset 30 Juni 2019 --%>
                                            <tr id="trNamaAsset" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Nama Asset
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="ddlNamaAsset" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" OnTextChanged="ddlNamaAsset_OnTextChange" ToolTip="Select Group Asset"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;&nbsp;
                                                </td>
                                                <td nowrap="nowrap" style="border-right: 0px solid;" colspan="2">&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Pencarian Nama Barang &nbsp;
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:TextBox ID="txtCari" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="25px" OnTextChanged="txtCari_TextChanged2" Width="232px"></asp:TextBox>
                                                </td>
                                                <td style="width: 134px;">
                                                    <span style="font-size: 10pt">&nbsp; Status Approval&nbsp;
                                                </td>
                                                <td style="width: 204px;" colspan="2">
                                                    <asp:TextBox ID="txtApproval" runat="server" BorderStyle="Groove" ReadOnly="true"
                                                        Width="233"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoSatuan" runat="server" CompletionInterval="100"
                                                        CompletionSetCount="20" EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetSatuan"
                                                        ServicePath="AutoComplete.asmx" TargetControlID="txtSatuan" CompletionListCssClass="autocomplete_completionListElement">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp; Nama Barang &nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="true" EnableTheming="true" OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged" Width="647px">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp; Kode Barang
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:TextBox ID="txtKodeBarang" runat="server" BorderStyle="Groove" ReadOnly="true"
                                                        Width="233"></asp:TextBox>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">
                                                    <span style="font-size: 10pt">&nbsp; Satuan &nbsp;
                                                </td>
                                                <td nowrap="nowrap" colspan="2">
                                                    <asp:TextBox ID="txtSatuan" runat="server" BorderStyle="Groove" ReadOnly="true" Width="60px"
                                                        AutoPostBack="true" OnTextChanged="txtSatuan_OnTextChange"></asp:TextBox>
                                                    <span style="font-size: 10pt">&nbsp;Stock/NonStk</span> &nbsp;<asp:TextBox ID="StokNonStok"
                                                        runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp; Keterangan&nbsp;
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                        Width="647px" OnTextChanged="txtKeterangan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="biayaAutoComplete" runat="server" TargetControlID="txtKeterangan"
                                                        ServiceMethod="GetItemBiaya" ServicePath="~/Modul/Assetmanagement/AutoComplete.asmx"
                                                        CompletionSetCount="20" FirstRowSelected="true" EnableCaching="true" CompletionListCssClass="autocomplete_completionListElement"
                                                        MinimumPrefixLength="3">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:AutoCompleteExtender ID="AutoCmplt" runat="server" TargetControlID="txtKeterangan"
                                                        Enabled="false" ServiceMethod="GetProjectName" ServicePath="AutoComplete.asmx"
                                                        MinimumPrefixLength="2" CompletionSetCount="10" FirstRowSelected="true" EnableCaching="true"
                                                        CompletionListCssClass="autocomplete_completionListElement">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp; Jumlah &nbsp;
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:TextBox ID="txtQty" runat="server" BorderStyle="Groove" ReadOnly="False" Width="233"></asp:TextBox>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp; Type Biaya &nbsp;
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="ddlTypeBiaya" runat="server" Enabled="false">
                                                        <asp:ListItem Value="">&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="KOMPENSASI">Kompensasi</asp:ListItem>
                                                        <asp:ListItem Value="BUAT">Buat</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" style="border-left: 2px solid; border-bottom: 1px solid">
                                                    <asp:TextBox ID="BiayaID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtLeadTime" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="sppDetailID" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <!-- penambahan field untuk keterangan tambahan spp biaya-->
                                            <tr style="" id="rowBiaya" runat="server">
                                                <td style="font-size: 10pt;">&nbsp; Katerangan Biaya&nbsp;
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtKetBiaya" BorderStyle="Groove" runat="server" Width="233"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;<cc1:AutoCompleteExtender ID="autoComplete" runat="server" TargetControlID="txtKetBiaya"
                                                    ServiceMethod="GetKetBiaya" ServicePath="AutoComplete.asmx" MinimumPrefixLength="3">
                                                </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <!-- end of keterangan tambahan-->
                                            <tr>
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp; Multi Gudang
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="ddlMultiGudang" runat="server" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged"
                                                        ToolTip="Multi Gudang &quot;Public&quot; : Barang boleh diambil oleh semua Departemen. Multi Gudang &quot;Private&quot; : Barang hanya boleh diambil oleh Departemen pembuat SPP"
                                                        Width="233px">
                                                        <asp:ListItem Value="0">---Pilih Multi Gudang---</asp:ListItem>
                                                        <asp:ListItem Value="1">Public</asp:ListItem>
                                                        <asp:ListItem Value="2">Private</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;Tanggal Kirim
                                                </td>
                                                <td nowrap="nowrap" colspan="2" style="border-right: 0px solid;">
                                                    <asp:TextBox ID="txtTglKirim" runat="server" BorderStyle="Groove" Width="120px" Enabled="False"
                                                        EnableTheming="true"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtTglKirim"></cc1:CalendarExtender>
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="lbAddItem" runat="server" OnClick="lbAddItem_Click" Text="AddItem" />
                                                    <asp:Button ID="lbUpdItem" runat="server" OnClick="lbUpdItem_Click" Text="UpdItem" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr id="trAsset1" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Groups Asset
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="GroupID" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" OnTextChanged="GroupID_OnTextChange" ToolTip="Select Group Asset"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;&nbsp;Kelas Asset
                                                </td>
                                                <td colspan="2" nowrap="nowrap" style="border-right: 0px solid;">
                                                    <asp:DropDownList ID="ClassID" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" OnTextChanged="ClassID_OnTextChange" Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trAsset2" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Sub Kelas Asset
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="SbClassID" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" OnTextChanged="SbClassID_OnTextChange" Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;
                                                </td>
                                                <td colspan="2" nowrap="nowrap" style="border-right: 0px solid;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr id="trLokasi" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;&nbsp;Lokasi Asset
                                                </td>
                                                <td colspan="2" nowrap="nowrap" style="border-right: 0px solid;">
                                                    <asp:DropDownList ID="LokasiID1" runat="server" CausesValidation="True" Height="24px"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;
                                                </td>
                                                <td colspan="2" nowrap="nowrap" style="border-right: 0px solid;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr id="trAsset3" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Umur Ekonomis
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="ddlUmurEko" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" Width="233px">
                                                        <asp:ListItem Value="0">---Pilih Umur Ekonomis---</asp:ListItem>
                                                        <asp:ListItem Value="1">60</asp:ListItem>
                                                        <asp:ListItem Value="2">120</asp:ListItem>
                                                        <asp:ListItem Value="3">240</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 204px;"></td>
                                                <td colspan="2" style="width: 204px;"></td>
                                            </tr>
                                            <tr id="trGroupSarMut1" runat="server" visible="true">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Group SarMut
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="ddlGroupSarmut" runat="server" AutoPostBack="true" BorderStyle="Groove"
                                                        Height="24px" Width="233px" OnSelectedIndexChanged="ddlGroupSarmut_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;&nbsp;Group Efesien Mesin
                                                </td>
                                                <td colspan="2" nowrap="nowrap" style="border-right: 0px solid;">
                                                    <asp:DropDownList ID="ddlGroupEfesien" runat="server" CausesValidation="True" Height="24px"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="trasset_lokasi" runat="server" visible="false">
                                                <td style="font-size: 10pt; width: 151px;">&nbsp;&nbsp;Lokasi Asset
                                                </td>
                                                <td style="width: 204px;">
                                                    <asp:DropDownList ID="LokasiID" runat="server" CausesValidation="True" Height="24px"
                                                        Width="233px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: 10pt; width: 134px;">&nbsp;&nbsp;
                                                </td>
                                                <td colspan="2" style="font-size: 10pt; width: 233px;">&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;&nbsp;<span id="frk" runat="server" style="font-size: 10pt">&nbsp;</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlForklif" runat="server" Visible="false" CssClass="gambar">
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="font-size: 10pt;" valign="middle">
                                                    <%--<font style="font-style: italic">Ket. Multi Gudang &quot;Public&quot; : Barang boleh
                                                        diambil oleh semua Departemen. Multi Gudang &quot;Private&quot; : Barang hanya boleh
                                                        diambil oleh Departemen pembuat SPP</font>--%>
                                                    <asp:TextBox ID="txtSatuanID" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="PaneLNoPol" runat="server" Visible="false" Style="font-family: Calibri">
                                            <table id="Table10" runat="server" width="98%" border="0" style="border-collapse: collapse">
                                                <tr>
                                                    <td style="font-size: 10pt; width: 88px;">
                                                        <asp:Label ID="LabelNoPol" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small;">&nbsp; Nomor Polisi</asp:Label>
                                                    </td>
                                                    <td style="width: 204px;">
                                                        <asp:DropDownList ID="ddlNoPol" runat="server" Visible="false" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlNoPol_SelectedIndexChanged" Height="16px" Style="font-family: Calibri; font-size: x-small"
                                                            Width="84%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 134px;"></td>
                                                    <td style="width: 233px;"></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PaneLDeadstock" runat="server" Visible="false" Style="font-family: Calibri">
                                            <table id="Table1" runat="server" width="98%" border="0" style="border-collapse: collapse">
                                                <tr>
                                                    <td style="font-size: 10pt; font-weight: bold;" colspan="3">Informasi Dead Stock
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 10pt;">
                                                        <asp:GridView ID="GrdDynamic1" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                            HorizontalAlign="Center" PageSize="20"
                                                            Style="margin-right: 0px" Width="98%">
                                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="GrdDynamic2" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                            HorizontalAlign="Center" PageSize="20"
                                                            Style="margin-right: 0px" Width="98%">
                                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="GrdDynamic3" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                            HorizontalAlign="Center" PageSize="20"
                                                            Style="margin-right: 0px" Width="98%">
                                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <div id="nonbiaya" runat="server" visible="true">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                OnRowDataBound="GridView1_RowDataBound" Width="100%" Visible="false">
                                                <Columns>
                                                    <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                    <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Q t y" />
                                                    <%--<asp:ButtonField CommandName="Add" Text="Pilih" />--%>
                                                    <asp:BoundField DataField="TglKirim" DataFormatString="{0:d}" HeaderText="Tgl Kirim" />
                                                    <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                                </Columns>
                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                    Font-Bold="true" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                <PagerStyle BorderStyle="Solid" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" style="width: 4%">#
                                                        </th>
                                                        <th class="kotak" style="width: 10%">Kode Barang
                                                        </th>
                                                        <th class="kotak" style="width: 25%">Nama Barang
                                                        </th>
                                                        <th class="kotak" style="width: 5%">Satuan
                                                        </th>
                                                        <th class="kotak" style="width: 10%">Qty
                                                        </th>
                                                        <th class="kotak" style="width: 11%">Tgl Kirim
                                                        </th>
                                                        <th class="kotak" style="width: 5%">&nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstSPP" runat="server" OnItemDataBound="lstSPP_DataBound" OnItemCommand="lstSPP_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="baris">
                                                                <td class="kotak tengah">
                                                                    <%# Container.ItemIndex+1 %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("ItemCode") %>
                                                                </td>
                                                                <td class="kotak left">
                                                                    <a title="Click untuk lihat History" href="#" onclick='ShowHist("<%# Eval("ItemName") %>");'>
                                                                        <%# Eval("ItemName").ToString().ToUpper() %></a>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Satuan") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Quantity", "{0:N2}")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("tglKirim","{0:d}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="del" OnClientClick="confirm_hapus();" />
                                                                    <asp:ImageButton ID="lstPnd" ImageUrl="~/images/pending.jpg" runat="server" CommandArgument='<%# Eval("AlasanPending") %>'
                                                                        CommandName="info" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="baris" style="background-color: #c1cdcd">
                                                                <td class="kotak tengah">
                                                                    <%# Container.ItemIndex+1 %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("ItemCode") %>
                                                                </td>
                                                                <td class="kotak left">
                                                                    <a title="Click untuk lihat History" href="#" onclick='ShowHist("<%# Eval("ItemName") %>");'>
                                                                        <%# Eval("ItemName").ToString().ToUpper() %></a>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Satuan") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Quantity", "{0:N2}")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("tglKirim","{0:d}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="del" OnClientClick="confirm_hapus();" />
                                                                    <asp:ImageButton ID="lstPnd" ImageUrl="~/images/pending.jpg" runat="server" CommandArgument='<%# Eval("AlasanPending") %>'
                                                                        CommandName="info" />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                        <!--grid view for biaya-->
                                        <div id="isBiaya" runat="server" visible="false">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView3_RowCommand"
                                                OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nama Biaya">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%# Eval("ItemCode") %>' runat="server"></asp:Label>
                                                            <asp:Label Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="ItemName" HeaderText="Nama Biaya" />--%>
                                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                    <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Q t y" />
                                                    <%--<asp:ButtonField CommandName="Add" Text="Pilih" />--%>
                                                    <asp:BoundField DataField="TglKirim" DataFormatString="{0:d}" HeaderText="Tgl Kirim" />
                                                    <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                                </Columns>
                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                    Font-Bold="true" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                <PagerStyle BorderStyle="Solid" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
