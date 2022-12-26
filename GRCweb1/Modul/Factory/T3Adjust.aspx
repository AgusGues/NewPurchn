<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T3Adjust.aspx.cs" Inherits="GRCweb1.Modul.Factory.T3Adjust" %>

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
                <title>T3 Adjust</title>

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
                <script type="text/javascript">
                    function confirmation() {
                        if (confirm('Yakin mau hapus data ?')) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">PROSES ADJUSTMENT </h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" class="form-control" Style="margin: 10px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>PartNo</asp:ListItem>
                                            <asp:ListItem>Lokasi</asp:ListItem>
                                            <asp:ListItem Value="NoBA">No. BA</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtLokasiC" class="form-control" Style="margin: 10px" runat="server" AutoPostBack="True" OnTextChanged="txtLokasiC_TextChanged" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtPartnoC" class="form-control" Style="margin: 10px" runat="server" AutoPostBack="True" OnTextChanged="txtPartnoC_TextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtCariBA" class="form-control" Style="margin: 10px" runat="server" AutoPostBack="True" OnTextChanged="txtCariBA_TextChanged" Visible="False"></asp:TextBox>
                                        <cc1:AutoCompleteExtender
                                            ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetLokasiStock"
                                            ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtCariBA_AutoCompleteExtender" runat="server"
                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoBA" ServicePath="AutoComplete.asmx"
                                            TargetControlID="txtCariBA">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server"
                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                            TargetControlID="txtPartnoC">
                                        </cc1:AutoCompleteExtender>
                                    </span>

                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" class="btn btn-sm btn-info" runat="server" style="background-color: white; font-weight: bold; margin: 10px" type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Font-Size="X-Small" OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" />
                        <asp:Panel ID="Panel3" runat="server" Visible="False">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">LIST STOCK PRODUK</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False"
                                        OnRowCommand="GridViewtrans_RowCommand" PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                            BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                            ForeColor="Gold" />
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
                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Adjustment" />
                        <asp:Panel ID="Panel8" runat="server">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">INPUT PROSES ADJUSTMENT</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-xs-12 col-sm-6 form-group-sm">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="form-field-9">No Adjust</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtAdjustNo" class="form-control" runat="server" ReadOnly="True" ToolTip="Otomatis dibuat saat disimpan" Width="244px"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="form-field-9">Tanggal Adjust</label>
                                            </div>
                                            <div class="col-md-8">

                                                <BDP:BDPLite ID="DatePicker1" CssClass="form-control" runat="server"
                                                    ToolTip="klik icon untuk merubah tanggal, kemudian klik Refresh Data"
                                                    YOffset="0">
                                                    <TextBoxStyle Font-Size="X-Small" />
                                                </BDP:BDPLite>

                                                <input id="btnRefresh" runat="server" class="btn btn-sm btn-info" onserverclick="btnRefresh_ServerClick"
                                                    style="background-color: white; font-weight: bold; margin: 5px"
                                                    type="button" value="Refresh Data" /></td>
                                            <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="form-field-9">No Berita Acara</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtBA" class="form-control" runat="server"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="form-field-9">Keterangan</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtKeterangan" class="form-control" runat="server"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-6 form-group-sm">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Type Adjustment :
                                                    <asp:RadioButton ID="RBIn" runat="server" Style="margin: 7px;" AutoPostBack="True" Checked="True" GroupName="A" Text=" Adjust In" />
                                                    <asp:RadioButton ID="RBOut" runat="server" AutoPostBack="True" GroupName="A" Text=" Adjust Out" />
                                                </h3>
                                            </div>
                                            <div class="panel-body">

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="form-field-9">PartNo</label>
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
                                                        <label for="form-field-9">Ukuran</label>
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
                                                        <label for="form-field-9">Part Name</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtPartname1" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
                                                        <div style="padding: 2px"></div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <label for="form-field-9">Lokasi</label>
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
                                                        <label for="form-field-9">Stok</label>
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
                                                        <label for="form-field-9">Quantity Adjust</label>
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
                                                    type="button" value="Add Item" /></td>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-footer text-right">
                                    <input id="btnNew" runat="server" class="btn btn-sm btn-info" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Baru" />
                                    <input id="btnSimpan" runat="server" class="btn btn-sm btn-primary" onserverclick="btnSimpan_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 63px;"
                                        type="button" value="Simpan" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                            PageSize="22" Width="100%">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="AdjustDate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                                <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                                <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Process" HeaderText="Process" />
                                                <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                                <asp:ButtonField CommandName="hapus" Text="Cancel" />
                                            </Columns>
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Item Adjustment by
                                    <asp:RadioButton ID="RBTanggal" Style="margin: 7px;" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" GroupName="B" OnCheckedChanged="RBTanggal_CheckedChanged" Text="Tanggal" />
                                    <asp:RadioButton ID="RBNoBA" Style="margin: 7px;" runat="server" AutoPostBack="True" Font-Size="X-Small" GroupName="B" OnCheckedChanged="RBAdjustNo_CheckedChanged" Text="No. Berita Acara" />
                                    <asp:RadioButton ID="RBApproval" runat="server" AutoPostBack="True" Font-Size="X-Small" GroupName="B" OnCheckedChanged="RBApproval_CheckedChanged" Text="Approval" />
                                </h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="PanelApprove" runat="server" Visible="False">
                                    <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        OnCheckedChanged="ChkAll_CheckedChanged" Text="Approve All" />
                                    &nbsp;<input id="btnApprove" runat="server" onserverclick="btnApprove_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 63px; height: 23px;"
                                        type="button" value="Approve" />
                                </asp:Panel>

                                <asp:Panel ID="Panel5" runat="server">
                                    <asp:GridView ID="GridItem" runat="server" AutoGenerateColumns="False"
                                        PageSize="22" Width="100%" OnRowCommand="GridItem_RowCommand">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                            BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                            ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="AdjustDate" DataFormatString="{0:d}"
                                                HeaderText="Tanggal" />
                                            <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                            <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                            <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasi" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                            <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                            <asp:ButtonField CommandName="hapus" Text="Cancel" Visible="False" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <asp:GridView ID="GridApprove" runat="server" AutoGenerateColumns="False"
                                        PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                            BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                            ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="Approve">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkapv" runat="server" Text="Approve" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="adjustdate" DataFormatString="{0:d}"
                                                HeaderText="Tanggal" />
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