<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormTitipanMss.aspx.cs" Inherits="GRCweb1.Modul.Factory.FormPenerimaanTitipanMss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>BDT Armada</title>
                <script type="text/javascript">
                    function confirmation() {
                        if (confirm('Yakin mau hapus data ?')) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                </script>
                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>

                <link href="../../assets/css/jquery.datetimepicker.css" rel="stylesheet" />

            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">PENERIMAAN BARANG TITIPAN MSS</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Baru" onserverclick="ClearForm" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSimpan" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Simpan" onserverclick="Simpan" />
                                    </span>

                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True">
                                            <asp:ListItem Value="No. Order">No. Order</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <asp:TextBox ID="txtCari" runat="server" BorderStyle="Groove" Height="20px" Width="208px"></asp:TextBox>
                                    &nbsp;<input id="btnCari" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" onserverclick="klikcari" value="Cari" />

                                </div>
                            </div>
                            <div class="panel panel-body">

                                <div class="col-xs-12 col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading clearfix">
                                            <h3 class="panel-title pull-left">List  Paket</h3>
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <label for="form-field-9" style="font-size: 12px">Tanggal Terima</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtTglTerima" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 12px">No. Order</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtNoOrder" runat="server" class="form-control" ClientIDMode="Static" AutoPostBack="True" OnTextChanged="txtNoOrder_TextChanged"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 12px">Supplier</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtSupplier" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-3">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9" style="font-size: 12px">Jumlah Paket</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtJumlah" runat="server" class="form-control" ClientIDMode="Static" OnTextChanged="txtJumlah_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel-body">
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;" border="0" id="expbaList">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak tengah">No</th>
                                                        <th class="kotak tengah">Nama Paket</th>
                                                        <th class="kotak tengah">Tebal (Cm)</th>
                                                        <th class="kotak tengah">Panjang (Cm)</th>
                                                        <th class="kotak tengah">Lebar (Cm)</th>
                                                        <th class="kotak tengah">M3</th>
                                                        <th class="kotak tengah">Berat (kg)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstPaket" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris" id="ps1" runat="server">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width=2%;"><%# Eval("No")%></td>
                                                                <td class="kotak tengah" style="width=5%;">
                                                                    <asp:TextBox ID="ltxtNama"  CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("NamaPaket").ToString() %>'
                                                                        Width="100%" AutoPostBack="True" OnTextChanged="lsttxtNama_TextChanged" onfocus="this.select()" ToolTip='<%# Container.ItemIndex %>'></asp:TextBox>
                                                                </td>
                                                                <td class="kotak " nowrap="nowrap" style="width=10%;">
                                                                    <asp:TextBox ID="ltxtTebal" CssClass="txtongrid"  runat="server" Visible="true" Text='<%# Eval("Tebal").ToString() %>'
                                                                        Width="100%" AutoPostBack="True" OnTextChanged="lsttxtTebal_TextChanged" onfocus="this.select()" ToolTip='<%# Container.ItemIndex %>'></asp:TextBox>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width=10%;">
                                                                    <asp:TextBox ID="ltxtPanjang" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Panjang").ToString() %>'
                                                                        Width="100%" AutoPostBack="True" OnTextChanged="lsttxtPanjang_TextChanged" onfocus="this.select()" ToolTip='<%# Container.ItemIndex %>'></asp:TextBox>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width=10%;">
                                                                    <asp:TextBox ID="ltxtLebar" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Lebar").ToString() %>'
                                                                        Width="100%" AutoPostBack="True" OnTextChanged="lsttxtLebar_TextChanged" onfocus="this.select()" ToolTip='<%# Container.ItemIndex %>'></asp:TextBox>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width=10%;">
                                                                    <asp:TextBox ID="ltxtM3" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("M3").ToString() %>'
                                                                        Width="100%" AutoPostBack="True" onfocus="this.select()" ReadOnly="false"></asp:TextBox>
                                                                </td>
                                                                <td class="kotak" style="width=10%;">
                                                                    <asp:TextBox ID="ltxtBerat" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Berat").ToString() %>'
                                                                        Width="100%"  AutoPostBack="True" OnTextChanged="lsttxtBerat_TextChanged" onfocus="this.select()" ToolTip='<%# Container.ItemIndex %>'></asp:TextBox></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-6">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="form-field-9" style="font-size: 12px">Total Dimensi</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 12px">Tebal</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtTebal" runat="server" class="form-control" ClientIDMode="Static" AutoPostBack="True" OnTextChanged="txtTebal_TextChanged" ReadOnly="false"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                            <div class="col-md-1 pull-left">
                                                &nbsp;Cm
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 12px">Panjang</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtPanjang" runat="server" class="form-control" ClientIDMode="Static" AutoPostBack="True" OnTextChanged="txtPanjang_TextChanged" ReadOnly="false"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                            <div class="col-md-1 pull-left">
                                                Cm
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 12px">Lebar</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtLebar" runat="server" class="form-control" ClientIDMode="Static" AutoPostBack="True" OnTextChanged="txtLebar_TextChanged"  ReadOnly="false"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                            <div class="col-md-1 pull-left">
                                                Cm
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="form-field-9" style="font-size: 12px">M3</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtm3" runat="server" class="form-control" ClientIDMode="Static" ReadOnly="false"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                         <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 12px">Berat</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtBerat" runat="server" class="form-control" ClientIDMode="Static" ReadOnly="false"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <h3 class="panel-title center">Info Pengiriman</h3>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server" BorderColor="Black">
                                                <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading clearfix">
                                            <h3 class="panel-title pull-left">List Item Barang</h3>
                                        </div>
                                        <div class="panel-body">
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                                border="0" id="baList" bgcolor="#999999">

                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak tengah">No</th>
                                                        <th class="kotak tengah">ItemID</th>
                                                        <th class="kotak tengah">Nama Barang</th>
                                                        <th class="kotak tengah">Tebal</th>
                                                        <th class="kotak tengah">Panjang</th>
                                                        <th class="kotak tengah">Lebar</th>
                                                        <th class="kotak tengah">M3</th>
                                                        <th class="kotak tengah">Berat</th>
                                                        <th class="kotak tengah">Quantity</th>
                                                        <th class="kotak tengah">Keterangan</th>
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstTitipan" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris" id="ps1" runat="server">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 2%;"><%# Eval("No")%></td>
                                                                <td class="kotak tengah" style="width: 5%;"><%# Eval("ItemID")%></td>
                                                                <td class="kotak " nowrap="nowrap" style="width: 10%;"><%# Eval("Description")%></td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 10%;"><%# Eval("Tebal","{0:N0}")%></td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 10%;"><%# Eval("Panjang","{0:N0}")%></td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 10%;"><%# Eval("Lebar","{0:N0}")%></td>
                                                                <td class="kotak" style="width: 7%;"><%# Eval("M3","{0:N4}")%></td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 10%;"><%# Eval("Berat","{0:N2}")%></td>
                                                                <td class="kotak" style="width: 10%;"><%# Eval("Quantity","{0:N0}")%></td>
                                                                <td class="kotak" style="width: 20%;"><%# Eval("Keterangan")%></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                            <div id="lst2" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </body>
            <script src="../../assets/js/jquery-ui.min.js"></script>
            <script src="../../assets/js/jquery.datetimepicker.full.js"></script>
            <script src="../../Scripts/Factory/titipanmss.js" type="text/javascript"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
