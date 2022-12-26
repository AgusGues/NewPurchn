<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MutasiLokasiTransit.aspx.cs" Inherits="GRCweb1.Modul.Factory.MutasiLokasiTransit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
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
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">PROSES MUTASI LOKASI TRANSIT FINISHING</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" class="btn btn-sm btn-info" onserverclick="btnNew_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True"
                                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                            meta:resourcekey="ddlCariResource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource1">PartNo</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource2">Lokasi</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartnoC" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtPartnoC_TextChanged" Width="208px"
                                            meta:resourcekey="txtPartnoCResource1"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtPartnoC" Mask="AAA-A-99999999999AAA"
                                            MessageValidatorTip="False" InputDirection="RightToLeft"
                                            Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                                            ErrorTooltipEnabled="True" runat="server"
                                            ClearMaskOnLostFocus="False" AutoComplete="False" PromptCharacter=" "
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"></cc1:MaskedEditExtender>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server"
                                            CompletionInterval="200" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                            TargetControlID="txtPartnoC" DelimiterCharacters="" Enabled="True">
                                        </cc1:AutoCompleteExtender>
                                        <asp:TextBox ID="txtLokasiC" runat="server" AutoPostBack="True"
                                            class="form-control" OnTextChanged="txtLokasiC_TextChanged"
                                            Visible="False" Width="48px" meta:resourcekey="txtLokasiCResource1"></asp:TextBox><cc1:AutoCompleteExtender
                                                ID="txtLokasiC_AutoCompleteExtender" runat="server"
                                                CompletionInterval="200" FirstRowSelected="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetLokasiStock"
                                                ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC"
                                                DelimiterCharacters="" Enabled="True">
                                            </cc1:AutoCompleteExtender>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" runat="server" class="btn btn-sm btn-info"
                                            style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Font-Size="X-Small" OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" meta:resourcekey="ChkHide2Resource1" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel3" runat="server"
                            ScrollBars="Vertical" Wrap="False" Visible="False"
                            meta:resourcekey="Panel3Resource1">
                            <div class="panel panel-primary">
                                <div class="panel-heading ">
                                    <h3 class="panel-title">List Stock Produk</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False"
                                        OnRowCommand="GridViewtrans_RowCommand" PageSize="22" Width="100%"
                                        meta:resourcekey="GridViewtransResource1">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                            BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                            ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ItemID" HeaderText="ItemID"
                                                meta:resourcekey="BoundFieldResource1" />
                                            <asp:BoundField DataField="ID" HeaderText="ID"
                                                meta:resourcekey="BoundFieldResource2" />
                                            <asp:BoundField DataField="lokid" HeaderText="lokid"
                                                meta:resourcekey="BoundFieldResource3" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo"
                                                meta:resourcekey="BoundFieldResource4" />
                                            <asp:BoundField DataField="PartName" HeaderText="Part Name"
                                                meta:resourcekey="BoundFieldResource5" />
                                            <asp:BoundField DataField="Tebal" HeaderText="Tebal"
                                                meta:resourcekey="BoundFieldResource6" />
                                            <asp:BoundField DataField="Lebar" HeaderText="Lebar"
                                                meta:resourcekey="BoundFieldResource7" />
                                            <asp:BoundField DataField="Panjang" HeaderText="Panjang"
                                                meta:resourcekey="BoundFieldResource8" />
                                            <asp:BoundField DataField="volume" HeaderText="Volume"
                                                meta:resourcekey="BoundFieldResource9" />
                                            <asp:BoundField DataField="Lokasi" HeaderText="Lokasi"
                                                meta:resourcekey="BoundFieldResource10" />
                                            <asp:BoundField DataField="qty" HeaderText="Stock"
                                                meta:resourcekey="BoundFieldResource11" />
                                            <asp:ButtonField CommandName="Pilih" Text="Pilih" Visible="False"
                                                meta:resourcekey="ButtonFieldResource1" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Proses Mutasi Lokasi" meta:resourcekey="ChkHide1Resource1" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <span>
                            <label for="form-field-9" style="font-size: 14px">Tgl. Mutasi</label>
                        </span>
                        <span>
                            <BDP:BDPLite ID="TglMutasi" runat="server" CssClass="style2"
                                ToolTip="klik icon untuk merubah tanggal"
                                meta:resourcekey="TglMutasiResource1">
                                <TextBoxStyle Font-Size="X-Small" />
                            </BDP:BDPLite>
                        </span>
                        <span>
                            <input id="btnRefresh" runat="server" class="btn btn-sm btn-success" onserverclick="btnRefresh_ServerClick"
                                style="background-color: white; font-weight: bold; font-size: 11px; width: 91px;"
                                type="button" value="Refresh Data" />
                            <div style="padding: 2px"></div>
                        </span>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Stock Produk</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel8" runat="server" meta:resourcekey="Panel8Resource1">
                                    <asp:Panel ID="Panel2" runat="server" meta:resourcekey="Panel2Resource1">
                                        <div class="panel panel-primary">
                                            <div class="panel-body">
                                                <div class="col-xs-12 col-sm-12">
                                                    <span class="input-icon input-icon-right">
                                                        <asp:DropDownList ID="ddlLokasi" class="form-control" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlLokasi_SelectedIndexChanged"
                                                            meta:resourcekey="ddlLokasiResource1">
                                                        </asp:DropDownList>
                                                        <div style="padding: 2px"></div>
                                                    </span>
                                                    <span class="input-icon input-icon-right">
                                                        <label for="form-field-9" style="font-size: 14px">Tgl. Serah</label>
                                                        <div style="padding: 2px"></div>
                                                    </span>
                                                    <span class="input-icon input-icon-right">
                                                        <BDP:BDPLite ID="TglSerah" runat="server" Style="color: black"
                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False"
                                                            meta:resourcekey="TglSerahResource1">
                                                            <TextBoxStyle Font-Size="X-Small" />
                                                        </BDP:BDPLite>
                                                        <div style="padding: 2px"></div>
                                                    </span>
                                                    <span class="input-icon input-icon-right">
                                                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Checked="True"
                                                            OnCheckedChanged="ChkAll_CheckedChanged" Text="ALL"
                                                            meta:resourcekey="ChkAllResource1" />
                                                        <div style="padding: 2px"></div>
                                                    </span>
                                                </div>

                                                <div class="col-xs-12 col-sm-12">
                                                    <div class="col-md-1">
                                                        <label for="form-field-9" style="font-size: 14px">No. Palet</label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtnopalet" runat="server" AutoPostBack="True" class="form-control"
                                                            onblur="change_color3()" onfocus="change_color2()" Width="72px"
                                                            meta:resourcekey="txtnopaletResource1"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtNoPalet_AutoCompleteExtender" runat="server"
                                                            BehaviorID="AutoCompleteEx2" CompletionInterval="200"
                                                            CompletionSetCount="20" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetPaletBM" ServicePath="AutoComplete.asmx"
                                                            ShowOnlyCurrentWordInCompletionListItem="True"
                                                            TargetControlID="txtNoPalet" DelimiterCharacters="" Enabled="True">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label for="form-field-9" style="font-size: 12px">PartNo Tujuan</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True"
                                                            class="form-control" OnTextChanged="txtPartnoA_TextChanged"
                                                            Width="195px" meta:resourcekey="txtPartnoAResource1"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                            CompletionInterval="200"
                                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetNoProduk" ServicePath="AutoComplete.asmx"
                                                            TargetControlID="txtPartnoA" DelimiterCharacters="" Enabled="True">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label for="form-field-9" style="font-size: 14px">Ukuran</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="input-icon input-icon-right">
                                                            <asp:TextBox ID="txtTebal1" size="15" Style="height: 30px; padding-right: 0px" Class="form-control" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                        </span>
                                                        <span>X</span>
                                                        <span class="input-icon input-icon-right">
                                                            <asp:TextBox ID="txtLebar1" size="15" Class="form-control" Style="height: 30px; padding-right: 0px" runat="server" AutoPostBack="True" ReadOnly="True" Width="55px"></asp:TextBox>
                                                        </span>
                                                        <span>X</span>
                                                        <span class="input-icon input-icon-right">
                                                            <asp:TextBox ID="txtPanjang1" size="15" Class="form-control" Style="height: 30px; padding-right: 0px" runat="server" AutoPostBack="True" padding-right="0px" ReadOnly="True" Width="55px"></asp:TextBox>
                                                        </span>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label for="form-field-9" style="font-size: 14px">Ke Lokasi</label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtLokasi2" runat="server" AutoPostBack="True"
                                                            class="form-control" OnPreRender="txtLokasi1_PreRender"
                                                            OnTextChanged="txtLokasi2_TextChanged" Width="55px"
                                                            meta:resourcekey="txtLokasi2Resource1"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtLokasi2_AutoCompleteExtender" runat="server"
                                                            CompletionInterval="200" ContextKey="0" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                            TargetControlID="txtLokasi2" UseContextKey="True" DelimiterCharacters=""
                                                            Enabled="True">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12">
                                                    <asp:Panel ID="Paneltransit" runat="server" Height="313px" ScrollBars="Both"
                                                        meta:resourcekey="PaneltransitResource1">
                                                        <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False"
                                                            PageSize="22" Width="100%" meta:resourcekey="GridViewtrans0Resource1">
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                                BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                                ForeColor="Gold" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ID" HeaderText="ID"
                                                                    meta:resourcekey="BoundFieldResource12" />
                                                                <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}"
                                                                    HeaderText="Tgl.Produksi" meta:resourcekey="BoundFieldResource13" />
                                                                <asp:BoundField DataField="Palet" HeaderText="Palet"
                                                                    meta:resourcekey="BoundFieldResource14" />
                                                                <asp:BoundField DataField="Partnodest" HeaderText="PartNoT1"
                                                                    meta:resourcekey="BoundFieldResource15" />
                                                                <asp:BoundField DataField="tglserah" DataFormatString="{0:d}"
                                                                    HeaderText="Tgl.Serah" meta:resourcekey="BoundFieldResource16" />
                                                                <asp:BoundField DataField="Tebal" HeaderText="Tebal"
                                                                    meta:resourcekey="BoundFieldResource17" />
                                                                <asp:BoundField DataField="Lebar" HeaderText="Lebar"
                                                                    meta:resourcekey="BoundFieldResource18" />
                                                                <asp:BoundField DataField="Panjang" HeaderText="Panjang"
                                                                    meta:resourcekey="BoundFieldResource19" />
                                                                <asp:BoundField DataField="PartNoser" HeaderText="PartNoT3"
                                                                    meta:resourcekey="BoundFieldResource20" />
                                                                <asp:BoundField DataField="Lokasiser" HeaderText="Lokasi"
                                                                    meta:resourcekey="BoundFieldResource21" />
                                                                <asp:BoundField DataField="qtyin" HeaderText="Stock"
                                                                    meta:resourcekey="BoundFieldResource22">
                                                                    <ItemStyle ForeColor="#FF0066" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="QtyOut"
                                                                    meta:resourcekey="TemplateFieldResource1">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQtyMutasi" runat="server" AutoPostBack="True"
                                                                            BorderStyle="Groove" Height="21px" OnTextChanged="txtQtyOut_TextChanged"
                                                                            Width="48px" meta:resourcekey="txtQtyMutasiResource1"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pilih" meta:resourcekey="TemplateFieldResource2">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkMutasi" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="ChkMutasi_CheckedChanged" Text="Mutasi"
                                                                            meta:resourcekey="ChkMutasiResource1" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="panel-footer">
                                                <span class="input-icon input-icon-right">
                                                    <label for="form-field-9">Quantity</label>
                                                </span>
                                                <span class="input-icon input-icon-right">
                                                    <asp:TextBox ID="txtQtyOut" runat="server" class="form-control"
                                                        OnPreRender="txtQty1_PreRender" OnTextChanged="txtQty1_TextChanged"
                                                        Width="65px" meta:resourcekey="txtQtyOutResource1"></asp:TextBox>
                                                </span>
                                                <span class="input-icon input-icon-right">
                                                    <asp:Button ID="btnTansfer" class="btn btn-sm btn-primary" runat="server" OnClick="btnTansfer_Click"
                                                        Text="Transfer" meta:resourcekey="btnTansferResource1" />
                                                </span>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading ">
                                <h3 class="panel-title">List Proses Mutasi Lokasi</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel5" runat="server"
                                    ScrollBars="Vertical" meta:resourcekey="Panel5Resource1">
                                    <asp:GridView ID="GridViewMutasiLok" runat="server" AutoGenerateColumns="False"
                                        PageSize="22" Width="100%" meta:resourcekey="GridViewMutasiLokResource1">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                            BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                            ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo"
                                                meta:resourcekey="BoundFieldResource23" />
                                            <asp:BoundField DataField="lokasi1" HeaderText="Lokasi Awal"
                                                meta:resourcekey="BoundFieldResource24" />
                                            <asp:BoundField DataField="lokasi2" HeaderText="Lokasi Akhir"
                                                meta:resourcekey="BoundFieldResource25" />
                                            <asp:BoundField DataField="qty" HeaderText="Qty"
                                                meta:resourcekey="BoundFieldResource26" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By"
                                                meta:resourcekey="BoundFieldResource27" />
                                            <asp:BoundField DataField="CreatedTime" HeaderText="Created Time"
                                                meta:resourcekey="BoundFieldResource28" />
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