<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplyListPlank.aspx.cs" Inherits="GRCweb1.Modul.Factory.SupplyListPlank" %>

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
                        <div class="panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">PROSES SUPPLY LISTPLANK</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" class="btn btn-sm" runat="server" onserverclick="btnNew_ServerClick" type="button"
                                            value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>PartNo</asp:ListItem>
                                            <asp:ListItem>Lokasi</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartnoC" runat="server" class="form-control" AutoPostBack="True"
                                            OnTextChanged="txtPartnoC_TextChanged" Width="208px"></asp:TextBox>
                                        <asp:TextBox ID="txtLokasiC" runat="server" AutoPostBack="True" class="form-control" OnTextChanged="txtLokasiC_TextChanged" Visible="False" Width="48px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender
                                            ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtPartnoC" Mask="AAA-A-99999999999AAA"
                                            MessageValidatorTip="False" MaskType="None" InputDirection="RightToLeft" Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                                            AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" runat="server"
                                            ClearMaskOnLostFocus="false" AutoComplete="False" PromptCharacter=" "></cc1:MaskedEditExtender>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoC">
                                        </cc1:AutoCompleteExtender>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" runat="server" class="btn btn-sm btn-info" type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" />
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">List Stock Produk</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False"
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
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 16px">Tgl. Proses</label>
                                        </div>
                                        <div class="col-md-8">
                                            <BDP:BDPLite ID="DatePicker1" runat="server" CssClass="style2"
                                                ToolTip="klik icon untuk merubah tanggal" Width="182px">
                                                <TextBoxStyle Font-Size="X-Small" />
                                            </BDP:BDPLite>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 15px">Partno</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True" class="form-control" OnTextChanged="txtPartnoA_TextChanged"
                                                Width="200px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtPartnoA">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Dari Lokasi</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtLokasi1" class="form-control" runat="server" AutoPostBack="True"
                                                OnPreRender="txtLokasi1_PreRender"
                                                OnTextChanged="txtLokasi1_TextChanged" Width="75px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" ContextKey="0"
                                                EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetLokasiStockP" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtLokasi1" UseContextKey="true">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>

                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Stock</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStock1" runat="server" AutoPostBack="True"
                                                Enabled="False" class="form-control"
                                                OnTextChanged="txtLokasi1_TextChanged" Width="70px"></asp:TextBox>
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
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Ke Lokasi</label>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Renovasi</label>
                                            <div style="padding: 2px"></div>
                                        </div>

                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Quantity</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtQtyOut" runat="server" class="form-control"
                                                OnPreRender="txtQty1_PreRender" OnTextChanged="txtQty1_TextChanged"
                                                Width="48px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <asp:Button ID="btnTansfer" runat="server" class="btn btn-sm btn-primary"
                                    OnClick="btnTansfer_Click" Text="Transfer" />
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Proses Supply Listplank</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel5" runat="server"
                                    ScrollBars="Vertical">
                                    <asp:GridView ID="GridViewSupply" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="TglTrans" HeaderText="Tanggal" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasi" HeaderText="Dari Lokasi" />
                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                            <asp:BoundField DataField="CreatedTime" HeaderText="Created Time" />
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