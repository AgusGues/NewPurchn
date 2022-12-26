<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MutasiLokasiPelarian.aspx.cs" Inherits="GRCweb1.Modul.Factory.MutasiLokasiPelarian" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title></title>

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
                </script>
            </head>

            <body class="no-skin">
                <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Mutasi Lokasi Pelarian" />
                <div class="row">
                    <asp:Panel ID="Panel8" runat="server" Width="100%">
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                            <div class="col-xs-12 col-sm-12">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">

                                    </div>
                                    <div class="panel-body">
                                        <span>
                                            <asp:CheckBox ID="ChkUkuran" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="ChkUkuran_CheckedChanged" Text="Retrieve By Ukuran" />
                                        </span>
                                        <span>
                                            <asp:DropDownList ID="ddlUkuran" runat="server" AutoPostBack="True" Height="22px"
                                                OnSelectedIndexChanged="ddlUkuran_SelectedIndexChanged" Visible="False"
                                                Width="175px">
                                            </asp:DropDownList>
                                        </span>
                                        <span>
                                            <asp:CheckBox ID="ChkTgl" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="ChkTgl_CheckedChanged" Text="Retrieve By Tgl.Pelarian" />
                                        </span>
                                        <span>
                                            <asp:TextBox ID="txtTglPelarian" runat="server" AutoPostBack="True"
                                                BorderStyle="Groove" OnTextChanged="txtTglPelarian_TextChanged" Visible="False"
                                                Width="176px"></asp:TextBox><cc1:CalendarExtender
                                                    ID="txtTglPelarian_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTglPelarian"></cc1:CalendarExtender>
                                        </span>
                                        <span>
                                            <asp:CheckBox ID="ChkPartno" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="ChkPartno_CheckedChanged"
                                                Text="Retrieve By Partno Pelarian" />
                                        </span>
                                        <span>
                                            <asp:DropDownList ID="ddlPartno" runat="server" AutoPostBack="True"
                                                Height="22px" OnSelectedIndexChanged="ddlPartno_SelectedIndexChanged"
                                                Visible="False" Width="175px">
                                            </asp:DropDownList>
                                        </span>

                                        <asp:Panel ID="Paneltransit" runat="server" ScrollBars="Both" BackColor="White"
                                            Width="100%">
                                            <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                Width="100%">
                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                    <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl.Produksi" />
                                                    <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                    <asp:BoundField DataField="Partnodest" HeaderText="PartNo Asal" />
                                                    <asp:BoundField DataField="tglserah" DataFormatString="{0:d}" HeaderText="Tgl.Serah" />
                                                    <asp:BoundField DataField="PartNoser" HeaderText="PartNo Pelarian" />
                                                    <asp:BoundField DataField="Lokasiser" HeaderText="Lok Pelarian" />
                                                    <asp:BoundField DataField="qtyin" HeaderText="Stock">
                                                        <ItemStyle ForeColor="#FF0066" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="QtyOut">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtyMutasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                Height="21px" OnTextChanged="txtQtyOut_TextChanged" Width="48px"></asp:TextBox>
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
                                        </asp:Panel>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-xs-12 col-sm-4">
                                            <span class="input-icon input-icon-right">
                                                 <label for="form-field-9" style="font-size: 12px">No. Palet Baru</label>
                                            </span>
                                            <span class="input-icon input-icon-right">
                                                <asp:TextBox ID="txtnopalet" class="form-control" runat="server" AutoPostBack="True"
                                                    Width="105px" ReadOnly="True"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </span>
                                            <span class="input-icon input-icon-right">
                                                <label for="form-field-9" style="font-size: 12px">Jumlah Potong</label>
                                            </span>
                                            <span class="input-icon input-icon-right">
                                                <asp:TextBox ID="txtQtyOut" runat="server" class="form-control" onfocus="this.select();"
                                                        OnTextChanged="txtQty1_TextChanged" Width="65px" AutoPostBack="True"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                            </span>
                                        </div>
                                        <div class="col-xs-12 col-sm-8">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label for="form-field-9" style="font-size: 12px">Tanggal Potong</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True"
                                                       class="form-control" Width="176px" OnTextChanged="txtTanggal_TextChanged"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label for="form-field-9" style="font-size: 12px">Mutasi Partno OK</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RadioButton ID="RBOK" runat="server" AutoPostBack="True" Checked="True"
                                                        GroupName="a" OnCheckedChanged="RBOK_CheckedChanged"
                                                        Text="Mutasi Partno OK " Visible="False" />
                                                    <asp:TextBox ID="txtPartnoOK" runat="server" class="form-control"
                                                        onfocus="this.select();" Width="176px" Font-Size="14px" AutoPostBack="True"
                                                        OnTextChanged="txtPartnoOK_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                        FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi"
                                                        ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoOK">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                                <div class="col-md-1">
                                                    <label for="form-field-9" style="font-size: 12px">Qty</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtQtyOK" runat="server" AutoPostBack="True" class="form-control"
                                                        OnTextChanged="txtlokOK_TextChanged" Width="65px"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="LabelOK" runat="server" Text="ke Lokasi "></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtlokOK" runat="server" class="form-control" Width="65px" AutoPostBack="True"
                                                        OnTextChanged="txtlokOK_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetLokasiStock" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtlokOK">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <label for="form-field-9" style="font-size: 12px">Mutasi Partno BP</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:RadioButton ID="RBBP" runat="server" AutoPostBack="True" GroupName="a"
                                                        OnCheckedChanged="RadioButton2_CheckedChanged" Text="Mutasi Partno BP "
                                                        Visible="False" />
                                                    <asp:TextBox ID="txtPartnoBP" runat="server" class="form-control"
                                                        onfocus="this.select();" Width="176px" Font-Size="14px" AutoPostBack="True"
                                                        OnTextChanged="txtPartnoBP_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                        FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi"
                                                        ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoBP">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                                <div class="col-md-1">
                                                    <label for="form-field-9" style="font-size: 12px">Qty</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtQtyBP" runat="server" AutoPostBack="True" class="form-control"
                                                        OnTextChanged="txtlokOK_TextChanged" Width="65px"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="LabelOK0" runat="server" Text="ke Lokasi"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLokBP" runat="server" class="form-control"
                                                        Width="65px" ></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetLokasiStock" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtLokBP">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer text-right">
                                        <input id="btnTansfer" runat="server" class="btn btn-sm btn-primary" onserverclick="btnTansfer_ServerClick"
                                                    style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Transfer" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Proses Mutasi Lokasi Pelarian</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Vertical">
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>