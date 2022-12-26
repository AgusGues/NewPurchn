<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MutasiLokasi.aspx.cs" Inherits="GRCweb1.Modul.Factory.MutasiLokasi" %>

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
                                <h3 class="panel-title pull-left">PROSES MUTASI LOKASI</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" class="btn btn-sm btn-info" runat="server" onserverclick="btnNew_ServerClick" type="button"
                                            value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>PartNo</asp:ListItem>
                                            <asp:ListItem>Lokasi</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartnoC" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtPartnoC_TextChanged" Width="200px"></asp:TextBox>
                                        <asp:TextBox ID="txtLokasiC" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtLokasiC_TextChanged" Visible="False" Width="55px"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtPartnoC" Mask="AAA-A-99999999999AAA"
                                            MessageValidatorTip="False" MaskType="None" InputDirection="RightToLeft" Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                                            AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" runat="server"
                                            ClearMaskOnLostFocus="false" AutoComplete="False" PromptCharacter=" "></cc1:MaskedEditExtender>
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
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" class="btn btn-sm" runat="server" type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Stock Produk</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel3" runat="server"
                                    Wrap="False">
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Mutasi Lokasi" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False" PageSize="22"
                            Visible="False" Width="100%">
                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="GroupID" HeaderText="GroupID" />
                                <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                <asp:BoundField DataField="lokid" HeaderText="lokid" />
                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                <asp:BoundField DataField="Tebal" HeaderText="Tebal" />
                                <asp:BoundField DataField="Lebar" HeaderText="Lebar" />
                                <asp:BoundField DataField="Panjang" HeaderText="Panjang" />
                                <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
                                <asp:BoundField DataField="qty" HeaderText="Stock" />
                                <asp:TemplateField HeaderText="QtyOut">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQtyMutasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                            Height="21px" OnTextChanged="txtQtyOut_TextChanged" Width="55px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pilih">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkMutasi" runat="server" AutoPostBack="True" OnCheckedChanged="ChkMutasi_CheckedChanged"
                                            Text="Mutasi" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BorderStyle="Solid" />
                            <AlternatingRowStyle BackColor="Gainsboro" />
                        </asp:GridView>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel8" runat="server">

                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Input Proses Mutasi Lokasi</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <span class="input-icon input-icon-right">
                                            <asp:TextBox ID="DatePicker1" runat="server" AutoPostBack="True"
                                                class="form-control"
                                                OnTextChanged="DatePicker1_SelectionChanged1" Width="200px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="DatePicker1"></cc1:CalendarExtender>
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <input id="btnRefresh" runat="server" class="btn btn-sm btn-primary" onserverclick="btnRefresh_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 91px;"
                                                type="button" value="Refresh Data" />
                                        </span>
                                        <div style="padding: 2px"></div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="col-xs-12 col-sm-8">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtPartnoA" class="form-control" runat="server" AutoPostBack="True"
                                                        OnTextChanged="txtPartnoA_TextChanged" Width="200px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
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
                                                        <asp:TextBox ID="txtTebal1" size="15" Style="height: 30px" Class="form-control" runat="server" AutoPostBack="True" ReadOnly="True" Width="65px"></asp:TextBox>
                                                    </span>
                                                    <span>X</span>
                                                    <span class="input-icon input-icon-right">
                                                        <asp:TextBox ID="txtLebar1" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="65px"></asp:TextBox>
                                                    </span>
                                                    <span>X</span>
                                                    <span class="input-icon input-icon-right">
                                                        <asp:TextBox ID="txtPanjang1" size="15" Class="form-control" Style="height: 30px" runat="server" AutoPostBack="True" ReadOnly="True" Width="65px"></asp:TextBox>
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
                                                    <label for="form-field-9" style="font-size: 14px">Dari Lokasi</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLokasi1" runat="server" AutoPostBack="True" class="form-control" OnPreRender="txtLokasi1_PreRender" OnTextChanged="txtLokasi1_TextChanged" Width="55px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetLokasiTransT3" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtLokasi1">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>

                                                <div class="col-md-1">
                                                    <label for="form-field-9" style="font-size: 14px">Stok</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStock1" class="form-control" runat="server" AutoPostBack="True"
                                                        Enabled="False" OnTextChanged="txtLokasi1_TextChanged" Width="55px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtStock1_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtStock1">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-3">
                                                    <label style="font-size: 14px">
                                                        <asp:CheckBox ID="ChkLoading" runat="server" AutoPostBack="True" OnCheckedChanged="ChkLoading_CheckedChanged" />
                                                        Dari Lokasi Loading</label>

                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 14px">Ke Lokasi</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLokasi2" runat="server" AutoPostBack="True"
                                                        class="form-control" OnPreRender="txtLokasi1_PreRender" OnTextChanged="txtLokasi2_TextChanged"
                                                        Width="55px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtLokasi2_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtLokasi2" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>

                                                <div class="col-md-1">
                                                    <label for="form-field-9" style="font-size: 14px">Quantity</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtQtyOut" class="form-control" runat="server" OnPreRender="txtQty1_PreRender"
                                                        OnTextChanged="txtQty1_TextChanged" Width="55px"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <asp:Button ID="btnTansfer" class="btn btn-sm btn-primary" runat="server" OnClick="btnTansfer_Click" Text="Transfer" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel5" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">List Proses Mutasi Lokasi</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewMutasiLok" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasi1" HeaderText="Lokasi Awal" />
                                            <asp:BoundField DataField="lokasi2" HeaderText="Lokasi Akhir" />
                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                            <asp:BoundField DataField="CreatedTime" HeaderText="Created Time" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>