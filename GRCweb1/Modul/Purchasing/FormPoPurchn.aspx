<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPOPurchn.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPOPurchn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
    </script>
    <script type="text/javascript">
        function openWindow() {
            window.showModalDialog("../../ModalDialog/FormPOPurchn.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
        }

        function onCancel() {

        }
        function Cetak() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=POPurchn", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }
        function CetakToFax() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=POPurchn2", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        function confirm_batal() {
            if (confirm("Anda yakin untuk Batal ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
        function confirm_revisi() {
            if (confirm("Anda yakin PO ini akan di revisi ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx?j=Revisi', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
        function confirm_close() {
            if (confirm("Anda yakin untuk Close ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonClose.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }

        function input_uang_muka() {
            window.showModalDialog('../../ModalDialog/InputUangMukaOP.aspx', '', 'resizable:yes;dialogHeight: 350px; dialogWidth: 600px;scrollbars=yes');
        }


        function btnClose_onclick() {

        }
        function inputKadarAir(supplier) {
            var net = window.showModalDialog('../../ModalDialog/KadarAirPO.aspx?id=' + supplier, '', 'status=0;toolbar=0;resizable:0;dialogHeight: 300px; dialogWidth: 520px;scrollbars=no;addressbar=0');
        }
        function GetKey(source, eventArgs) {
            //document.getElementById("LblKey").innerText = eventArgs.get_value();
            //alert(eventArgs.get_value());
            $('#<%=txtSuppID.ClientID %>').val(eventArgs.get_value());
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-xs-12 col-sm-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading clearfix">
                            <h3 class="panel-title pull-left">INPUT PO</h3>
                            <div class="pull-right">
                                <span class="input-icon input-icon-right">
                                    <input id="btnNew" class="btn-info" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnUpdate" class="btn-info" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:Button ID="btnCancel" class="btn-info" runat="server" Style="font-weight: bold; font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:Button ID="btnClose" class="btn-info" runat="server" OnClick="btnClose_ServerClick" Style="font-weight: bold; font-size: 11px;" Text="Close PO" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnPrint" class="btn-info" onclick="Cetak()" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="Print" visible="False" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnPrintFax" class="btn-info" onclick="CetakToFax()" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="Print For Fax" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnList" class="btn-info" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="ListPO" onserverclick="btnList_ServerClick" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnListSPP" class="btn-info" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="ListSPP" onserverclick="btnListSPP_ServerClick" visible="false" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:Button ID="btnRevisi" class="btn-info" runat="server" Style="font-weight: bold; font-size: 11px;" type="button" Text="Revisi" OnClick="btnRevisi_Click" Visible="false" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:Button ID="btnUangMuka" class="btn-info" runat="server" Style="font-weight: bold; font-size: 11px;" Text="Uang Muka" OnClick="btnUangMuka_ServerClick" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:Button ID="btnKirimEmail" class="btn-info" runat="server" Style="font-weight: bold; font-size: 11px;" Text="Kirim Email" OnClick="btnKirimEmail_Click" />
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="70px">
                                        <asp:ListItem Value="NoPO">No. PO</asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                                <span class="input-icon input-icon-right">
                                    <asp:TextBox ID="txtSearch" runat="server" Width="100px"></asp:TextBox>
                                </span>
                                <span class="input-icon input-icon-right">
                                    <input id="btnSearch" class="btn-info" runat="server" style="font-weight: bold; font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                </span>
                            </div>
                        </div>
                        <div class="panel panel-body">
                            <div class="col-xs-12 col-sm-12" hidden="hidden">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtSPRNo" runat="server" AutoPostBack="True"
                                            Height="22px" Visible="False" Width="233px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="None"
                                            AutoComplete="False" ClearMaskOnLostFocus="false" ClipboardEnabled="False" DisplayMoney="None"
                                            Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" InputDirection="RightToLeft"
                                            Mask="999/99-99/AAAA" MaskType="None" MessageValidatorTip="False" PromptCharacter=" "
                                            TargetControlID="txtSPRNo"></cc1:MaskedEditExtender>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtIDBiaya" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtItemCode" runat="server" ReadOnly="True" Visible="False" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <span id="revInfo" runat="server" style="color: Blue; font-size: x-small; position: relative; float: left; background-color: Highlight">Info</span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; No. PO
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtNoPO" runat="server" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Label ID="stPO" runat="server" CssClass="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Tgl. PO
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtDate" runat="server" Width="100%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Label ID="txtInfoRevisi" runat="server" CssClass="cursor" ForeColor="Red"></asp:Label>
                                    </div>

                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; No. SPP
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSPP" runat="server" Width="100%" AutoPostBack="True" OnTextChanged="txtSPP_TextChanged" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                        <span>
                                            <asp:ImageButton Visible="false" ID="nSPP" runat="server" ImageUrl="~/images/feed.jpg" AlternateText="SPP" ToolTip="Click for Close SPP by Reason" /></span>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Item SPP
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlItemSPP" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlItemSPP_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Cari Supplier
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtCariSupplier" runat="server" AutoPostBack="True" Width="100%" OnTextChanged="txtCariSupplier_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Tipe SPP
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlTipeSPP" runat="server" Width="100%" AutoPostBack="False" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5">
                                        <span id="tipeSpp" runat="server" style="font-size: 10pt"></span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Nama Supplier
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"
                                            Width="233px" Visible="false">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSup" runat="server" Width="100%" AutoPostBack="true" OnTextChanged="txtSup_Change"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="at1" runat="server" TargetControlID="txtSup" ServiceMethod="GetSupplier"
                                            ServicePath="AutoComplete.asmx" MinimumPrefixLength="3" CompletionSetCount="15"
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionInterval="100" EnableCaching="true"
                                            OnClientItemSelected="GetKey">
                                        </cc1:AutoCompleteExtender>
                                        <asp:HiddenField ID="txtSuppID" runat="server"></asp:HiddenField>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Kode Supplier
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtKodeSupplier" Width="100%" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:HiddenField ID="txtSubCompanyID" runat="server" />
                                        <cc1:ComboBox ID="ddlSupPurch" runat="server" AppendDataBoundItems="false" MaxLength="0" AutoCompleteMode="Suggest" RenderMode="Inline" CssClass="fontKecil" Visible="false">
                                        </cc1:ComboBox>
                                    </div>
                                    <div class="col-md-5">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; UP Supplier
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtUp" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        &nbsp; Telp Supplier
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtTelepon" runat="server" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Fax Supplier
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtFax" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                    </div>

                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Nama Barang
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtNamaBarang" runat="server" ReadOnly="True" Width="82%"></asp:TextBox>
                                        <asp:TextBox ID="txtSatuan" runat="server" ReadOnly="True" Width="17%"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Q t y
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtQty" runat="server" OnUnload="txtQty_TextChanged" Width="60%" AutoPostBack="True" ReadOnly="False"></asp:TextBox>
                                        <asp:TextBox ID="txtKts" runat="server" Visible="false" OnTextChanged="txtKts_Change"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Mata Uang
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlMataUang" runat="server" AutoPostBack="true" Width="" OnSelectedIndexChanged="ddlMatauang_Change">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <span id="nk" runat="server" visible="false">Nilai Kurs</span>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="nilaiKurs" runat="server" Visible="false" Width="100%" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Harga
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtHarga" runat="server" Width="60%" ReadOnly="False"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Term Of Pay
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlTermOfPay" runat="server" AutoPostBack="True" ToolTip="Term Of Payment/Syarat-syarat Pembayaran"
                                            Width="100%" OnSelectedIndexChanged="ddlTermOfPay_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtTermOfPay" runat="server"
                                            ToolTip="Delivery/Shipment(Diharapkan Sampai di Gudang" Visible="False"
                                            Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Discount
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtDiscount" runat="server" Width="60%" ReadOnly="False"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Term Of Delivery
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtDelivery" runat="server" Width="23%"
                                            ToolTip="Delivery/Shipment(Diharapkan Sampai di Gudang" Visible="False"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDelivery" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDelivery_SelectedIndexChanged" Width="233px">
                                            <asp:ListItem>-</asp:ListItem>
                                            <asp:ListItem>LOCO</asp:ListItem>
                                            <asp:ListItem>FRANCO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Delivery Date
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtDate0" runat="server" Width="50%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate0_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDate0"></cc1:CalendarExtender>
                                        &nbsp;<span id="leadTime" runat="server" style="font-size: 10pt"></span>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; P P N
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtPPN" runat="server" ReadOnly="False" Width="50%"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:RadioButton ID="rbCash" runat="server" Checked="True" Font-Size="XX-Small" GroupName="A" Text="Cash" AutoPostBack="True" />
                                        <asp:RadioButton ID="rbCredit" runat="server" Font-Size="XX-Small" GroupName="A" Text="Kredit" AutoPostBack="True" />
                                    </div>

                                    <div class="col-md-2">
                                        P P H  &nbsp;<asp:TextBox ID="txtPPH" runat="server" ReadOnly="False" Width="50%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; Ongkos Kirim
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtOngkos" runat="server" ReadOnly="False"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-12">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp; 
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp;Total Price
                                    </div>
                                    <div class="col-md-9">
                                        <asp:Label ID="Label3" runat="server" Font-Italic="True" Font-Size="X-Small" Text="Label" Visible="False"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Font-Italic="True" Font-Size="X-Small" Text="Label" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtTotalPrice" runat="server" Font-Bold="True"
                                            Font-Italic="True" ReadOnly="False" Width="44%"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-6">
                                        &nbsp; Barang&nbsp;
                                    
                                        <asp:RadioButton ID="rbLocal" runat="server" Checked="True" Font-Size="XX-Small" GroupName="B" Text="Local" />
                                        &nbsp;
                                                    <asp:RadioButton ID="rbImport" runat="server" Font-Size="XX-Small" GroupName="B" Text="Import" />
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp;Remark
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" ToolTip="Delivery/Shipment(Diharapkan Sampai di Gudang"
                                            Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">
                                        &nbsp;&nbsp;
                                         <asp:CheckBox ID="ChkIndent" runat="server" AutoPostBack="true" Checked="False" Font-Size="XX-Small"
                                             GroupName="B" OnCheckedChanged="ChkIndent_Changed" Text="Indent ?" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlIndent" runat="server" AutoPostBack="False" Enabled="false"
                                            ToolTip="Tenggang Waktu Barang Indent" Width="100%">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="lbAddItem" runat="server" Font-Size="10pt" OnClick="lbAddItem_Click" Text="Add Item" />
                                        <asp:Button ID="btnUpdHeader" runat="server" OnClick="btnUpdHeader_Click" Text="Update Header" Visible="false" />
                                        <asp:Button ID="lbEdit" runat="server" Font-Size="10pt" OnClick="lbAddItem0_Click" Text="Edit Header" Visible="false" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="bthHD" runat="server" OnClick="btnHd_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>


                            <div id="frmKA" runat="server" visible="false" class="kotak OddRows">
                                <!-- input kadar air kertas -->
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" class="kotak">
                                    <tr>
                                        <td colspan="2" class="Line3 kotak"><b>&nbsp;INPUT KADAR AIR</b></td>
                                    </tr>
                                    <tr>
                                        <td>No. SJDepo</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSJDepo" runat="server" Width="100%" ToolTip="Wajib di isi untuk kiriman dari depo" OnSelectedIndexChanged="ddlSJDepo_Click" AutoPostBack="true"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>No. Mobil</td>
                                        <td>
                                            <asp:TextBox ID="txtNoPOL" runat="server" class="txtUpper" OnTextChanged="txtNoPOL_Change" Width="100%" AutoPostBack="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">StdKA</td>
                                        <td style="width: 65%">
                                            <asp:TextBox ID="txtStdKAPlant" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Gross</td>
                                        <td>
                                            <asp:TextBox ID="txtGrossPlant" runat="server" CssClass="angka" Width="100%" AutoPostBack="true" OnTextChanged="txtGrossPlant_Change"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Kadar Air</td>
                                        <td>
                                            <asp:TextBox ID="txtKadarAirPlant" runat="server" CssClass="angka" Width="100%" Text="0" AutoPostBack="true" OnTextChanged="txtKadarAir_Change"></asp:TextBox>
                                            <span style="display: none">
                                                <asp:TextBox ID="txtSelisih" runat="server" class="txtongrid angka" ReadOnly="true"></asp:TextBox></span>
                                        </td>
                                    </tr>
                                    <tr id="rSampah" runat="server">
                                        <td>Sampah</td>
                                        <td>
                                            <asp:TextBox ID="txtSampah" runat="server" CssClass="angka" Width="100%" AutoPostBack="true" OnTextChanged="txtSampah_Change" Text="0"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Netto</td>
                                        <td>
                                            <asp:TextBox ID="txtNettoPlant" runat="server" CssClass="angka" ReadOnly="true" Width="100%" AutoPostBack="false" OnTextChanged="txtNetto_Change"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <cc1:MaskedEditExtender ID="mmsk" runat="server" MaskType="none" ClearMaskOnLostFocus="false" TargetControlID="txtNOPOL" Mask="$$-9999-$$$"></cc1:MaskedEditExtender>
                                <!-- end of kadar air kertas-->
                            </div>
                            <div class="contentlist" style="height: 200px;">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                                    OnRowDataBound="GridView2_RowDataBound" Width="91%" Visible="false">
                                    <Columns>
                                        <%--<asp:BoundField DataField="SPPDetailID" HeaderText="ID" /> --%>
                                        <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                        <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                        <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                        <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                        <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="H a r g a" ItemStyle-HorizontalAlign="Right" />
                                        <%--<asp:ButtonField CommandName="Add" Text="Pilih" />
                                      <asp:ButtonField CommandName="AddDelete" Text="Hapus" />--%><asp:BoundField DataField="DlvDate"
                                          DataFormatString="{0:d}" HeaderText="DlvDate" />
                                        <asp:ButtonField CommandName="batal" Text="Cancel" />
                                        <asp:ButtonField CommandName="Close" Text="Close" />
                                        <asp:ButtonField CommandName="Hapus" Text="Hapus" />
                                        <asp:BoundField DataField="status" ShowHeader="False">
                                            <ControlStyle Width="0px" />
                                            <FooterStyle Width="0px" />
                                            <HeaderStyle Width="0px" />
                                            <ItemStyle BackColor="Black" Width="0px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound" Visible="False" Width="91%">
                                    <Columns>
                                        <%--<asp:BoundField DataField="SPPDetailID" HeaderText="ID" /> --%>
                                        <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                        <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                        <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                        <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                        <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="H a r g a" />
                                        <%--<asp:ButtonField CommandName="Add" Text="Pilih" />
                                      <asp:ButtonField CommandName="AddDelete" Text="Hapus" />--%><asp:TemplateField HeaderText="DlvDate">
                                          <ItemTemplate>
                                              <asp:TextBox ID="txtdlvdate" runat="server" Font-Size="XX-Small"
                                                  Height="19px" Width="73px"></asp:TextBox>
                                              <cc1:CalendarExtender ID="txtdlvdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                  TargetControlID="txtdlvdate"></cc1:CalendarExtender>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                                <table id="tblPO" width="100%" style="width: 100%; border-collapse: collapse; font-size: x-small; position: inherit">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th style="width: 4%" class="kotak">#</th>
                                            <th style="width: 10%" class="kotak">NoSPP</th>
                                            <th style="width: 10%" class="kotak">Item Code</th>
                                            <th style="width: 32%" class="kotak">ItemName</th>
                                            <th style="width: 8%" class="kotak">Quantity</th>
                                            <th style="width: 5%" class="kotak">Satuan</th>
                                            <th style="width: 10%" class="kotak">Harga</th>
                                            <th style="width: 10%" class="kotak">Dlv.Date</th>
                                            <th style="width: 5%" class="kotak">&nbsp;</th>
                                        </tr>
                                        <asp:Repeater ID="lstPO" runat="server" OnItemDataBound="lstPO_DataBound">
                                            <ItemTemplate>
                                                <tr class=" EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex +1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah">
                                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                            <tr>
                                                                <td style="width: 90%" align="center"><%# Eval("ItemCode") %></td>
                                                                <td style="width: 9%;" align="center">
                                                                    <sup>
                                                                        <asp:Label ID="lblRevInfo" Width="100%" runat="server" Text="1" BackColor="Red" ForeColor="White" ToolTip="Klik for revision info"></asp:Label>
                                                                    </sup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="kotak"><%# Eval("NamaBarang")%></td>
                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                    <td class="kotak angka xx"><%#Eval("Price","{0:N3}") %></td>
                                                    <td class="kotak tengah"><%# Eval("DlvDate","{0:d}") %></td>
                                                    <td class="kotak tengah">
                                                        <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                        <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="OddRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex +1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah">
                                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                            <tr>
                                                                <td style="width: 90%" align="center"><%# Eval("ItemCode") %></td>
                                                                <td style="width: 9%;" align="center">
                                                                    <sup>
                                                                        <asp:Label ID="lblRevInfo" Width="100%" runat="server" Text="1" BackColor="Red" ForeColor="White" ToolTip="Klik for revision info"></asp:Label></sup></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="kotak "><%# Eval("NamaBarang")%></td>
                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                    <td class="kotak angka"><%#Eval("Price","{0:N3}") %></td>
                                                    <td class="kotak tengah"><%# Eval("DlvDate","{0:d}") %></td>
                                                    <td class="kotak tengah">
                                                        <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                        <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </thead>
                                </table>
                            </div>
                            <div id="Div1" runat="server">
                                <table style="table-layout: fixed; width: 100%; border-collapse: collapse">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                        </tr>
                                </table>
                            </div>
                            <%--<div id="bgPanel" runat="server" class="modalBackground" style="width:100%; height:100%; display:none">--%>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none; left: 150px; top: 110px">
                                <div class="nbTableHeader" style="width: 100%; height: 25px; overflow: hidden">
                                    <b>INPUT KADAR AIR</b>
                                </div>
                                <div class="content" style="height: 130px">
                                    <hr />
                                    <table style="width: 100%; border-collapse: collapse;">
                                        <tr>
                                            <td style="width: 10%">&nbsp;</td>
                                            <td style="width: 25%">Standart Kadar Air</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtStdKA" runat="server" ReadOnly="true" Width="60%"></asp:TextBox></td>
                                            <td style="width: 15%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Berat Kotor</td>
                                            <td>
                                                <asp:TextBox ID="txtGross" runat="server" CssClass="angka" Width="75%" AutoPostBack="true" OnTextChanged="txtGross_Change"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Kadar Air</td>
                                            <td>
                                                <asp:TextBox ID="txtKadarAir" runat="server" CssClass="angka" Width="50%" AutoPostBack="true" OnTextChanged="txtKadarAir_Change"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Berat Bersih</td>
                                            <td>
                                                <asp:TextBox ID="txtNetto" runat="server" CssClass="angka" ReadOnly="true" Width="75%"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

                                </div>
                                <div class="angka total" style="width: 99%; overflow: hidden; height: 30px; padding: 2px; padding-right: 5px">
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" />
                                    <asp:Button ID="btnBatal" runat="server" Text="Batal" OnClick="btnBatal_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <%--</div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
