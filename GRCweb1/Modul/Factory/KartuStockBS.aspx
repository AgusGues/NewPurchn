<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KartuStockBS.aspx.cs" Inherits="GRCweb1.Modul.Factory.KartuStockBS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>Destacking</title>

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
                            <div class="panel-heading">
                                <h3 class="panel-title">KARTU STOCK BS</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-3 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9">Tahun</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTahun" runat="server" class="form-control"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-3 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9">Bulan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlBulan" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                                                Width="156px">
                                                <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                <asp:ListItem>Januari</asp:ListItem>
                                                <asp:ListItem>Februari</asp:ListItem>
                                                <asp:ListItem>Maret</asp:ListItem>
                                                <asp:ListItem>April</asp:ListItem>
                                                <asp:ListItem>Mei</asp:ListItem>
                                                <asp:ListItem>Juni</asp:ListItem>
                                                <asp:ListItem>Juli</asp:ListItem>
                                                <asp:ListItem>Agustus</asp:ListItem>
                                                <asp:ListItem>September</asp:ListItem>
                                                <asp:ListItem>Oktober</asp:ListItem>
                                                <asp:ListItem>November</asp:ListItem>
                                                <asp:ListItem>Desember</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlCari" class="form-control" runat="server" AutoPostBack="True">
                                                <asp:ListItem>PartNo</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPartnoC" runat="server" AutoPostBack="True"
                                                class="form-control" OnTextChanged="txtPartnoC_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtPartnoC">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel3" runat="server" Height="500px" ScrollBars="Auto"
                                                Width="314px">
                                                List Items<asp:GridView ID="GridViewItems" runat="server"
                                                    AutoGenerateColumns="False"
                                                    OnSelectedIndexChanged="GridViewItems_SelectedIndexChanged" PageSize="22"
                                                    Width="100%" OnRowCommand="GridViewItems_RowCommand" AllowPaging="True"
                                                    OnPageIndexChanging="GridViewItems_PageIndexChanging">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                        ForeColor="Gold" />
                                                    <Columns>
                                                        <asp:BoundField DataField="partno" HeaderText="Part No" />
                                                        <asp:BoundField DataField="tebal" HeaderText="Tebal" />
                                                        <asp:BoundField DataField="lebar" HeaderText="Lebar" />
                                                        <asp:BoundField DataField="panjang" HeaderText="Panjang" />
                                                        <asp:ButtonField CommandName="pilih" Text="Detail" />
                                                        <asp:ButtonField CommandName="rekap" Text="Rekap" />
                                                    </Columns>
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>

                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel4" runat="server" Visible="false" BackColor="White" Height="500px"
                                                ScrollBars="Auto" Width="100%">
                                                List Kartu Stock :
                                                                                                                    <asp:Label ID="LPartno" runat="server"></asp:Label>
                                                <asp:Button ID="Linkbtn" runat="server" Style="margin: 5px" class="btn btn-sm btn-info" OnClick="ExportToExcel"
                                                    Text="Export to excel" />
                                                <asp:GridView ID="GridViewKStock1" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="3" EnableModelValidation="True" Font-Size="X-Small"
                                                    OnRowCancelingEdit="GridViewKStock1_RowCancelingEdit1"
                                                    OnRowCommand="GridView1_RowCommand"
                                                    OnSelectedIndexChanged="GridViewKStock1_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:BoundField DataField="Tanggal" DataFormatString="{0:d}"
                                                            HeaderText="Tanggal">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                        <asp:BoundField DataField="Awal" HeaderText="Awal">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Penerimaan" HeaderText="Terima">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pengeluaran" HeaderText="Keluar">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" />
                                                        <asp:BoundField DataField="HPP" HeaderText="HPP" DataFormatString="{0:0.##}" />
                                                        <asp:BoundField DataField="Process" HeaderText="Process" />
                                                        <asp:TemplateField HeaderText="Detail Kartu Stock" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_Show" runat="server"
                                                                    CommandArgument="<%# Container.DataItemIndex%>" CommandName="Details"
                                                                    Font-Size="X-Small" Height="19px" OnClick="btn_Show_Click"
                                                                    Style="margin-top: 0px" Text="Show Details" Width="88px" />
                                                                <asp:Button ID="Cancel" runat="server"
                                                                    CommandArgument="<%# Container.DataItemIndex%>" CommandName="Cancel"
                                                                    Font-Size="X-Small" Height="19px" Text="Hide Details" Visible="false"
                                                                    Width="85px" />
                                                                <%--child gridview with bound fields --%>
                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                                                                    EnableModelValidation="True" Font-Size="X-Small" ForeColor="Black">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Lokasitrm" HeaderText="Lokasi" />
                                                                        <asp:BoundField DataField="Qtyintrm" HeaderText="Qty-In" />
                                                                        <asp:BoundField DataField="qtyouttrm" HeaderText="Qty-Out" />
                                                                        <asp:BoundField DataField="Process" HeaderText="Process" />
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
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>

                                            <asp:Panel ID="PanelRekap" runat="server" BackColor="White" Height="500px"
                                                ScrollBars="Auto" Width="100%" Visible="false">
                                                List Kartu Stock :
                                                                                                                    <asp:Label ID="LPartno2" runat="server"></asp:Label>
                                                <asp:Button ID="Linkbtn2" runat="server" Style="margin: 5px" class="btn btn-sm btn-info" OnClick="ExportToExcel2"
                                                    Text="Export to excel" />
                                                <asp:GridView ID="GridViewRekap" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="3" EnableModelValidation="True" Font-Size="X-Small"
                                                    OnRowCommand="GridViewRekap_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                        <asp:BoundField DataField="Awal" HeaderText="Awal">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Penerimaan" HeaderText="Terima">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Pengeluaran" HeaderText="Keluar">
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" />
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
