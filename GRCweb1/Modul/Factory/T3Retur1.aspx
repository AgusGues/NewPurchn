<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T3Retur1.aspx.cs" Inherits="GRCweb1.Modul.Factory.T3Retur1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>T3 Retur</title>

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
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">PROSES RETUR PENJUALAN</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCetak" runat="server" class="btn btn-sm btn-info" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cetak" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="NoRetur">No. Retur</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartnoC" class="form-control" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtPartnoC_TextChanged" Width="208px" Visible="False"
                                            Wrap="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCariReturNo" class="form-control" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtCariReturNo_TextChanged" Width="208px"></asp:TextBox>
                                        <asp:TextBox ID="txtLokasiC" class="form-control" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtLokasiC_TextChanged" Visible="False" Width="48px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtCariReturNo_AutoCompleteExtender" runat="server"
                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetNoBA" ServicePath="AutoComplete.asmx"
                                            TargetControlID="txtCariReturNo">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoC">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender
                                            ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC">
                                        </cc1:AutoCompleteExtender>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk"
                            Style="font-family: Calibri" />

                        <asp:Panel ID="Panel3" runat="server" Visible="False">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">LIST STOCK PRODUK</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewtrans_RowCommand"
                                        PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
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
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Retur"
                            Style="font-family: Calibri" />

                        <asp:Panel ID="Panel8" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">INPUT RETUR</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-2">
                                            <h5 class="panel-title">Type Retur</h5>
                                        </div>
                                        <div class="col-xs-12 col-sm-10">
                                            <asp:RadioButton ID="RBType1" runat="server" AutoPostBack="True" Checked="True" GroupName="b"
                                                Text="Type1" Style="font-family: Calibri" />
                                            <asp:RadioButton ID="RBType2" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                GroupName="b" Text="Type2" Style="font-family: Calibri" />
                                            <asp:RadioButton ID="RBType3" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                GroupName="b" Text="Type3" Style="font-family: Calibri" />
                                            &nbsp;<asp:RadioButton ID="RBType4" runat="server" AutoPostBack="True"
                                                Font-Size="X-Small" GroupName="b" Style="font-family: Calibri" Text="Type3" />
                                            <asp:RadioButton ID="RBType5" runat="server" AutoPostBack="True"
                                                Font-Size="X-Small" GroupName="b" Style="font-family: Calibri" Text="Type3" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label for="form-field-9" style="font-size: 14px">No.Retur</label>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtReturNo" class="form-control" runat="server" ReadOnly="True"
                                                        ToolTip="Otomatis dibuat saat disimpan" Width="242px" Style="font-family: Calibri"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Tgl. Retur </label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtTglRetur" class="form-control" runat="server" AutoPostBack="True"
                                                        Width="150px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtTglRetur"></cc1:CalendarExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">No. Surat Jalan</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtSJNo" runat="server" AutoPostBack="True" class="form-control"
                                                        Width="182px" OnTextChanged="txtSJNo_TextChanged" Style="font-family: Calibri"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetSJNo" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtSJNo" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">No. OP</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtOPNo" runat="server" class="form-control" Width="182px"
                                                        Style="font-family: Calibri"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Customer</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtCustomer" runat="server" class="form-control" Width="244px"
                                                        Style="font-family: Calibri"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCustomer" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtCustomer" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Expedisi</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtExpedisi" runat="server" class="form-control" Width="244px"
                                                        Style="font-family: Calibri"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Keterangan</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtKeterangan" runat="server" class="form-control"
                                                        Width="244px" Style="font-family: Calibri"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Tgl. Produksi</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtTglProduksi" runat="server" AutoPostBack="True"
                                                        class="form-control" Width="150px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtTglProduksi_CalendarExtender" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtTglProduksi"></cc1:CalendarExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Jenis Defect</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlDefect" runat="server" Enabled="false" class="form-control"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Group Prod.</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlGP" class="form-control" runat="server" Enabled="false"
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
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
                                                                class="form-control" OnPreRender="txtLokasi1_PreRender"
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
                                                            <label for="form-field-9" style="font-size: 14px">Quantity Adjust</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtQty1" runat="server"
                                                                class="form-control" OnPreRender="txtQty1_PreRender"
                                                                OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-footer text-right">
                                                    <input id="btnTansfer" runat="server" class="btn btn-sm btn-success" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Add Item" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer text-right">
                                    <input id="btnNew" runat="server" class="btn btn-sm" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Baru" />
                                    <input id="btnSimpan" runat="server" class="btn btn-sm btn-primary" onserverclick="btnSimpan_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 63px;"
                                        type="button" value="Simpan" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                            PageSize="22" Width="100%">
                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="ItemIDser" HeaderText="ItemID" />
                                <asp:BoundField DataField="Tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                <asp:BoundField DataField="SJNo" HeaderText="SJNo" />
                                <asp:BoundField DataField="OPNo" HeaderText="OPNo" />
                                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                                <asp:BoundField DataField="Expedisi" HeaderText="Expedisi" />
                                <asp:BoundField DataField="PartNortr" HeaderText="PartNo" />
                                <asp:BoundField DataField="lokasirtr" HeaderText="Lokasi" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                <asp:ButtonField CommandName="hapus" Text="Cancel" Visible="False" />
                            </Columns>
                            <PagerStyle BorderStyle="Solid" />
                            <AlternatingRowStyle BackColor="Gainsboro" />
                        </asp:GridView>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Item Retur By </h3>
                            </div>
                            <div class="panel-body">
                                <asp:RadioButton ID="RBTanggal" runat="server" AutoPostBack="True" Checked="True"
                                    Font-Size="X-Small" GroupName="B" OnCheckedChanged="RBTanggal_CheckedChanged"
                                    Text="Tanggal" Visible="False" Style="font-family: Calibri" />
                                &nbsp;&nbsp;<asp:RadioButton ID="RBApproval" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                    GroupName="B" OnCheckedChanged="RBApproval_CheckedChanged" Text="Approval" Visible="False"
                                    Style="font-family: Calibri" />
                                <asp:Panel ID="PanelApprove" runat="server" Visible="False" Width="309px">
                                    <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        OnCheckedChanged="ChkAll_CheckedChanged" Text="Approve All" Style="font-family: Calibri" />
                                    &nbsp;&nbsp;
                                                                        <input id="btnApprove" runat="server" onserverclick="btnApprove_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 63px; height: 22px;"
                                                                            type="button" value="Approve" visible="False" />
                                </asp:Panel>

                                <asp:Panel ID="Panel5" runat="server">
                                    <asp:GridView ID="GridItem" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%" OnRowCommand="GridItem_RowCommand">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="Tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                            <asp:BoundField DataField="SJNo" HeaderText="SJNo" />
                                            <asp:BoundField DataField="PartNortr" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasirtr" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="deletebtn" runat="server" CommandName="hapus" OnClientClick="return confirm('Yakin mau cancel data retur??');"
                                                        Text="Cancel" CommandArgument='<%# Container.DataItemIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <asp:GridView ID="GridApprove" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="Approve">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkapv" runat="server" Text="Approve" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="adjustdate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                            <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                            <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                            <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasi" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                            <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
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