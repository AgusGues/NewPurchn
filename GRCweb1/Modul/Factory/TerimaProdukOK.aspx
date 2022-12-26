<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TerimaProdukOK.aspx.cs" Inherits="GRCweb1.Modul.Factory.TerimaProdukOK" %>

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
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">INPUT PENERIMAAN PRODUK</h3>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Font-Size="X-Small"
                            OnCheckedChanged="CheckBox1_CheckedChanged" Text="Tampilkan Total Transit" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical"
                            Visible="False">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewSaldo1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewSaldo1_RowCommand"
                                        PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="partnoser" HeaderText="PartNo"></asp:BoundField>
                                            <asp:BoundField DataField="LokasiSer" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="Qty" />
                                            <asp:ButtonField CommandName="pilih" Text="Pilih" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan Saldo Transit Per Tanggal" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical"
                            Visible="False">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <asp:GridView ID="GridViewSaldo" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewSaldo_RowCommand"
                                        PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="tglSerah" DataFormatString="{0:d}" HeaderText="Tgl.Serah" />
                                            <asp:BoundField DataField="tglproduksi" HeaderText="Tgl. Produksi" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="partnoser" HeaderText="PartNo"></asp:BoundField>
                                            <asp:BoundField DataField="LokasiSer" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="Qty" />
                                            <asp:ButtonField CommandName="pilih" Text="Pilih" />
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
                            <div class="panel-body">
                                <div class="row">
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Cari data berdasarkan : </label>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:RadioButton ID="RBtglserah" runat="server" AutoPostBack="True" Checked="True"
                                            GroupName="c" Text="Tgl. Serah" ToolTip="Tanggal Serah" OnCheckedChanged="RBtglserah_CheckedChanged" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtDateSerah" runat="server" AutoPostBack="True" class="form-control" OnTextChanged="txtDateSerah_TextChanged" Width="127px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateSerah_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateSerah"></cc1:CalendarExtender>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:RadioButton ID="RBtglproduksi" runat="server" GroupName="c" Text="Tgl. Produksi"
                                            AutoPostBack="True" OnCheckedChanged="RBtglproduksi_CheckedChanged" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtDateProduksi" runat="server" AutoPostBack="True"
                                            class="form-control" Enabled="False" OnTextChanged="txtDateSerah_TextChanged"
                                            Width="127px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateProduksi_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateProduksi"></cc1:CalendarExtender>
                                        <div style="padding: 2px"></div>
                                    </span>
                                </div>
                                <div class="row">
                                    <span class="input-icon input-icon-right">
                                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                                            OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Penerimaan Produk" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                </div>
                                <div class="row">
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Filter PartNo</label>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtPartNo" runat="server" AutoPostBack="True" class="form-control"
                                            Font-Bold="True" OnTextChanged="txtPartNo_TextChanged" Width="227px"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Tentukan Group Marketing</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlGroups" runat="server" AutoPostBack="True" class="form-control"
                                            OnSelectedIndexChanged="ddlGroups_SelectedIndexChanged" Width="185px">
                                        </asp:DropDownList>
                                        <div style="padding: 2px"></div>
                                    </span>
                                </div>
                                <div class="row">
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">List Penyerahan Produk Dari Lokasi : </label>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:RadioButton ID="RH99" runat="server"
                                            AutoPostBack="True" GroupName="a" OnCheckedChanged="RW28_CheckedChanged" Text="H99"
                                            Checked="True" />
                                        <asp:RadioButton ID="RW28" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RW28_CheckedChanged"
                                            Text="W28" Visible="False" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:RadioButton ID="RB99" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RP99_CheckedChanged"
                                            Text="B99" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:RadioButton ID="RC99" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RP99_CheckedChanged" Text="C99" />
                                        <asp:RadioButton ID="RP99" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RP99_CheckedChanged" Text="P99 " Visible="False" />
                                        <asp:RadioButton ID="RBLain" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RP99_CheckedChanged" Text="Lain" Visible="False" />
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Tgl. Terima</label>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtDateTrm" runat="server" AutoPostBack="True" class="form-control"
                                            OnTextChanged="txtDateTrm_TextChanged" Width="127px" Enabled="False"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateTrm_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateTrm"></cc1:CalendarExtender>
                                        <div style="padding: 2px"></div>
                                    </span>
                                </div>
                                <div class="row">
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Isi Lokasi Penerimaan dengan</label>
                                        <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtLokTrm0" runat="server" AutoPostBack="True"
                                            Font-Size="XX-Small" class="form-control" OnTextChanged="txtLokTrm0_TextChanged" Width="50px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtLokTrm0_AutoCompleteExtender" runat="server" CompletionInterval="500"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtLokTrm0">
                                        </cc1:AutoCompleteExtender>
                                        <div style="padding: 2px"></div>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Wrap="False"
                                    Font-Size="XX-Small">
                                    <asp:GridView ID="GridViewSerah" runat="server" AutoGenerateColumns="False" PageSize="9"
                                        Width="100%" OnRowDataBound="GridViewSerah_RowDataBound" ToolTip="Gunakan tombol &quot;Tab&quot; untuk perpindahan input data"
                                        Font-Size="XX-Small">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <SelectedRowStyle Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl. Prod." />
                                            <asp:BoundField DataField="Formula" HeaderText="Jenis" />
                                            <asp:BoundField DataField="PartNodest" HeaderText="PartNo T1" />
                                            <asp:BoundField DataField="TglSerah" DataFormatString="{0:d}" HeaderText="Tgl.Serah" />
                                            <asp:TemplateField HeaderText="Partno T3">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPartnoOK" runat="server" Font-Size="XX-Small" Height="20px" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LokasiSer" HeaderText="Lokasi" />
                                            <asp:BoundField DataField="Qtyin" HeaderText="Qty In" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="Qty Out" />
                                            <asp:TemplateField HeaderText="Lok Trm">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLokTrm" runat="server" Font-Size="XX-Small" Height="20px" Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="XX-Small" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty Trm">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQtyTrm" runat="server" Font-Size="XX-Small" Height="20px" Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="G.Marketing">
                                                <ItemTemplate>
                                                    <asp:Label ID="LMarketing" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <EditRowStyle Font-Size="XX-Small" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="panel-footer text-right">
                                <input id="btnTansfer" runat="server" class="btn btn-sm btn-primary" onserverclick="btnTansfer_ServerClick" onblur="btnTansfer_LostFocus"
                                    style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                    value="Transfer" />
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Penerimaan Produk</h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="GridViewTerima" runat="server" AutoGenerateColumns="False" PageSize="22"
                                    Width="100%">
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <Columns>
                                        <asp:BoundField DataField="tglSerah" HeaderText="Tgl.Serah" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="partnoser" HeaderText="DPartNo"></asp:BoundField>
                                        <asp:BoundField DataField="LokasiSer" HeaderText="DLokasi" />
                                        <asp:BoundField DataField="TglTrm" DataFormatString="{0:d}" HeaderText="Tgl. Terima" />
                                        <asp:BoundField DataField="Partnotrm" HeaderText="KPartNo" />
                                        <asp:BoundField DataField="LokasiTrm" HeaderText="KLokasi" />
                                        <asp:BoundField DataField="QtyInTrm" HeaderText="Qty" />
                                        <asp:BoundField DataField="groups" HeaderText="Group Marketing" />
                                    </Columns>
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>