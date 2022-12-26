<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Asimetris.aspx.cs" Inherits="GRCweb1.Modul.Factory.Asimetris" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>Pengiriman</title>

                <meta name="description" content="Common form elements and layouts" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
                <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
                <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">PROSES MUTASI ASIMETRIS </h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari0" runat="server" class="btn btn-sm btn-info" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari1" runat="server" class="btn btn-sm" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="List" />
                                    </span>

                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>PartNo</asp:ListItem>
                                            <asp:ListItem>Lokasi</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartnoC" runat="server" AutoPostBack="True"
                                            class="form-control" OnTextChanged="txtPartnoC_TextChanged" Width="224px" Font-Size="X-Small"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoC">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtPartnoC" Mask="AAA-A-99999999999AAA"
                                            MessageValidatorTip="False" MaskType="None" InputDirection="RightToLeft" Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                                            AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" runat="server"
                                            ClearMaskOnLostFocus="false" AutoComplete="False" PromptCharacter=" "></cc1:MaskedEditExtender>
                                        <asp:TextBox ID="txtLokasiC" runat="server" AutoPostBack="True"
                                            class="form-control" OnTextChanged="txtLokasiC_TextChanged" Visible="False" Width="48px"></asp:TextBox><cc1:AutoCompleteExtender
                                                ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC">
                                            </cc1:AutoCompleteExtender>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" runat="server" class="btn btn-sm btn-info" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide_CheckedChanged" Text="Tampilkan List Stock Produk" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">List Stock Produk</h2>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel3" runat="server" BackColor="#CCFFCC" ScrollBars="Vertical"
                                    Wrap="False">
                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%" OnRowCommand="GridViewtrans_RowCommand">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="GroupID" HeaderText="GroupID" />
                                            <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="lokid" HeaderText="lokid" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="PartName" HeaderText="Part Name" />
                                            <asp:BoundField DataField="Tebal" HeaderText="Tebal" />
                                            <asp:BoundField DataField="Lebar" HeaderText="Lebar" />
                                            <asp:BoundField DataField="Panjang" HeaderText="Panjang" />
                                            <asp:BoundField DataField="volume" HeaderText="Volume" />
                                            <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="qty" HeaderText="Stock" />
                                            <asp:ButtonField CommandName="Pilih" Text="Pilih" Visible="False" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Asimetris" />
                    </div>
                </div>
                <div class="row">
                    <asp:Panel ID="Panel8" runat="server">
                        <div class="col-xs-12 col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Input Transaksi </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-xs-12 col-sm-12">
                                        <span class="input-icon input-icon-right">
                                            <label for="form-field-9" style="font-size: 14px">Tgl Proses</label>
                                            <BDP:BDPLite ID="DatePicker1" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal">
                                                <TextBoxStyle Font-Size="X-Small" />
                                            </BDP:BDPLite>
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <input id="btnRefresh" runat="server" class="btn btn-sm btn-primary" onserverclick="btnRefresh_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Refresh Data" />
                                        </span>

                                        <span class="input-icon input-icon-right">
                                            <label for="form-field-9" style="font-size: 14px">Nama Mesin : </label>
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <asp:DropDownList ID="ddlMCutter" runat="server" class="form-control" Style="font-family: Calibri; font-size: x-small"
                                                Visible="true" Width="100%">
                                            </asp:DropDownList>
                                        </span>
                                    </div>

                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">LOKASI AWAL </h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True"
                                                                class="form-control" OnTextChanged="txtPartnoA_TextChanged"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                                                TargetControlID="txtPartnoA">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Ukuran</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtTebal1" size="15" Style="height: 30px" Class="form-control" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <span>X</span>
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtLebar1" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <span>X</span>
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtPanjang1" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Part Name</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPartname1" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtLokasi1" runat="server" AutoPostBack="True"
                                                                class="form-control"
                                                                OnTextChanged="txtLokasi1_TextChanged" Width="65px"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3"
                                                                UseContextKey="true" ContextKey="0"
                                                                ServicePath="AutoComplete.asmx" TargetControlID="txtLokasi1">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label for="form-field-9" style="font-size: 14px">Stok</label>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtStock1" class="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtLokasi1_TextChanged" Width="48px" disabled></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtStock1_AutoCompleteExtender" runat="server"
                                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                TargetControlID="txtStock1">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Quantity</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtQty1" runat="server"
                                                                class="form-control"
                                                                OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label for="form-field-9" style="font-size: 14px">Sisa luas</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtluas" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                Enabled="False" Height="21px" Width="48px"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtluas_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtluas">
                                                            </cc1:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-footer">
                                                    <asp:Panel ID="PanelOtomatis" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="font-size: x-small;">
                                                                    <asp:CheckBox ID="ChkConvertBS" runat="server" AutoPostBack="True" Checked="True"
                                                                        OnCheckedChanged="ChkConvertBS_CheckedChanged" Text="Auto Produk BS" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: x-small;">&nbsp;: (<asp:Label ID="LCPartnoBS1" runat="server" Text="-"></asp:Label>)
                                                                                                    <asp:Label ID="LCQtyBS1" runat="server" Text="0"></asp:Label>
                                                                    &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                    <asp:Label ID="LCLokBS1" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: x-small;">&nbsp;: (<asp:Label ID="LCPartnoBS2" runat="server" Text="-"></asp:Label>)
                                                                                                    <asp:Label ID="LCQtyBS2" runat="server" Text="0"></asp:Label>
                                                                    &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                    <asp:Label ID="LCLokBS2" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: x-small;">&nbsp;: (<asp:Label ID="LCPartnoBS3" runat="server" Text="-"></asp:Label>)
                                                                                                    <asp:Label ID="LCQtyBS3" runat="server" Text="0"></asp:Label>
                                                                    &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                    <asp:Label ID="LCLokBS3" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-size: x-small;">&nbsp;: (<asp:Label ID="LCPartnoBS4" runat="server" Text="-"></asp:Label>)
                                                                                                    <asp:Label ID="LCQtyBS4" runat="server" Text="0"></asp:Label>
                                                                    &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                    <asp:Label ID="LCLokBS4" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">LOKASI AKHIR </h3>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPartnoB" runat="server" AutoPostBack="True" Class="form-control" OnPreRender="txtPartnoB_PreRender" OnTextChanged="txtPartnoB_TextChanged"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoB">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Ukuran</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtTebal2" size="15" Style="height: 30px" Class="form-control" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <span>X</span>
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtLebar2" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <span>X</span>
                                                            <span class="input-icon input-icon-right">
                                                                <asp:TextBox ID="txtPanjang2" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                            </span>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Part Name</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPartname2" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtLokasi2" runat="server" AutoPostBack="True" class="form-control" OnTextChanged="txtLokasi2_TextChanged"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="200"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasi2">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label for="form-field-9" style="font-size: 14px">Stok</label>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtStock2" runat="server" class="form-control" AutoPostBack="True" Width="48px"
                                                                Enabled="False"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtStock2_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtStock2">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Total Quantity Asimetris</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtQty3" runat="server" class="form-control" OnTextChanged="txtQty3_TextChanged"
                                                                Width="64px"></asp:TextBox>
                                                            <asp:TextBox ID="txtQty2" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                Height="21px" OnTextChanged="txtQty2_TextChanged" Visible="False" Width="48px"></asp:TextBox>
                                                            <asp:TextBox ID="txtPengali" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                Height="21px" onfocus="this.select();" ReadOnly="True" Visible="False" Width="19px">1</asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-footer text-right">
                                                    <input id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick" class="btn btn-sm btn-primary" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Tambah" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <div class="col-xs-12 col-sm-4">
                                        <div class="panel panel-primary">
                                            <div class="panel-body">
                                                <asp:Panel ID="Panel7" runat="server" Font-Size="X-Small"
                                                    ScrollBars="Vertical" Height="173px">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Underline="True" Text="Group Marketing"></asp:Label>
                                                    <asp:RadioButtonList ID="RBList" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                        OnSelectedIndexChanged="RBList_SelectedIndexChanged">
                                                    </asp:RadioButtonList>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide0" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide0_CheckedChanged" Text="Tampilkan List Transaksi" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List transaksi</h3>
                            </div>

                            <div class="panel-body">
                                <asp:Panel ID="Panel5" runat="server" BackColor="#CCFFCC" ScrollBars="Vertical">
                                    <asp:GridView ID="GridViewTemp" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%" OnRowCommand="GridViewTemp_RowCommand">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="PartNoIn" HeaderText="PartNo In" />
                                            <asp:BoundField DataField="lokasiIn" HeaderText="Lokasi In" />
                                            <asp:BoundField DataField="qtyin" HeaderText="Qty In" />
                                            <asp:BoundField DataField="partnoOut" HeaderText="PartNo Out" />
                                            <asp:BoundField DataField="lokasiout" HeaderText="Lokasi Out" />
                                            <asp:BoundField DataField="qtyout" HeaderText="Qty Out" />
                                            <asp:BoundField DataField="luas" HeaderText="Luas" />
                                            <asp:BoundField DataField="GroupName" HeaderText="G.Marketing" />
                                            <asp:ButtonField CommandName="batal" Text="Cancel" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                            <div class="panel-footer text-right">
                                <input id="btnTansfer" runat="server" class="btn btn-sm btn-primary" onserverclick="btnTansfer_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                    type="button" value="Transfer" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">

                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Proses Asimetris</h3>
                            </div>

                            <div class="panel-body">
                                <asp:Panel ID="Panel6" runat="server" BackColor="#CCFFCC" ScrollBars="Vertical">
                                    <asp:GridView ID="GridViewAsimetris" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnRowCommand="GridViewAsimetris_RowCommand"
                                        OnRowCancelingEdit="GridViewAsimetris_RowCancelingEdit" Font-Size="X-Small" OnDataBinding="GridViewAsimetris_DataBinding">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="docno" HeaderText="Doc No"></asp:BoundField>
                                            <asp:BoundField DataField="Tgltrans" DataFormatString="{0:d}" HeaderText="Tgl. Proses">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Partnoin" HeaderText="Dari PartNo">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lokasiin" HeaderText="Dari Lok">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="qtyin" HeaderText="Qty">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Detail Proses Asimetris">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_Show" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                        CommandName="Details" Font-Size="X-Small" Height="19px" Style="margin-top: 0px"
                                                        Text="Show Details" Width="88px" OnClick="btn_Show_Click" />
                                                    <asp:Button ID="Cancel" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                        CommandName="Cancel" Font-Size="X-Small" Height="19px" Text="Hide Details" Visible="false"
                                                        Width="85px" />
                                                    <%--child gridview with bound fields --%>
                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        EnableModelValidation="True" Font-Size="X-Small" ForeColor="Black">
                                                        <Columns>
                                                            <asp:BoundField DataField="GroupName" HeaderText="G.Marketing" />
                                                            <asp:BoundField DataField="PartnoOut" HeaderText="PartNo"></asp:BoundField>
                                                            <asp:BoundField DataField="LokasiOut" HeaderText="Lokasi"></asp:BoundField>
                                                            <asp:BoundField DataField="QtyOut" HeaderText="Qty"></asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#7C6F57" />
                                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#E3EAEB" />
                                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>