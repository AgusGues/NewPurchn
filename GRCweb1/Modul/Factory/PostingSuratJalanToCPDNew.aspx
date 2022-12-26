<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostingSuratJalanToCPDNew.aspx.cs" Inherits="GRCweb1.Modul.Factory.PostingSuratJalanToCPDNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <div class="panel-heading clearfix">
                                    <h2 class="panel-title pull-left">POSTING SHIPMENT SURAT JALAN TO</h2>
                                    <div class="pull-right">
                                        <span class="input-icon input-icon-right">
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <input id="btnUpdate" runat="server" class="btn btn-sm btn-info" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Posting" visible="True" onserverclick="btnUpdate_ServerClick" />
                                            <asp:Button ID="btnCancel" runat="server" Style="background-color: White; font-weight: bold; font-size: 11px;"
                                                Text="Cancel SJ" OnClick="btnCancel_ServerClick" Visible="False" />
                                            <input id="btnPrintLgsg" onclick="CetakLgsg()" runat="server"
                                                style="background-color: White; font-weight: bold; font-size: 11px;" type="button"
                                                value="Cetak SJ" visible="False" />
                                            <input id="btnSimpan" runat="server" onserverclick="btnSimpan_ServerClick" style="background-color: white;
                                                    font-weight: bold; font-size: 11px;" type="button" value="Simpan" visible="False" />
                                        </span>

                                        <span>
                                            <input id="btnList" runat="server" class="btn btn-sm btn-info" style="background-color: White; font-weight: bold; font-size: 11px;"
                                                type="button" value="List SJ" onserverclick="btnList_ServerClick" />
                                            <input id="btnListTO" runat="server" class="btn btn-sm btn-info" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="List TO" onserverclick="btnListTO_ServerClick" />
                                            <asp:Button ID="btnTurunStatus" runat="server" Style="background-color: White; font-weight: bold; font-size: 11px;"
                                                Text="Turun Status SJ" OnClick="btnTurunStatus_ServerClick"
                                                Visible="False" />
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" Style="background-color: white; font-weight: bold; font-size: 11px; margin-left: 0px;">
                                                <asp:ListItem Value="SuratJalanNo">No SJ</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <asp:TextBox ID="txtSearch" runat="server" class="form-control" Width="165px"></asp:TextBox>
                                        </span>
                                        <span class="input-icon input-icon-right">
                                            <input id="btnSearch" runat="server" class="btn btn-sm btn-info" onserverclick="btnSearch_ServerClick" style="background-color: White; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">No Surat Jalan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSuratJalanNo" class="form-control" runat="server" Width="233"
                                                ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">No TO</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTransferOrderNo" runat="server" class="form-control" Width="233"
                                                AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">No Schedule </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtScheduleNo" runat="server" AutoPostBack="True" class="form-control"
                                                Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">No Mobil </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNoMobil" runat="server" class="form-control" Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Nama Sopir </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDriverName" runat="server" class="form-control" Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Tgl Create</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtCreateDate" runat="server" class="form-control" Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Dari Alamat</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtFromAddress" runat="server" class="form-control" Height="66px"
                                                Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Ke Alamat</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtToAddress" runat="server" class="form-control" Height="66px"
                                                Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Tgl Kirim Actual</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtActualKirim" runat="server" AutoPostBack="True" class="form-control"
                                                OnTextChanged="txtActualKirim_TextChanged" Width="233"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtActualKirim"></cc1:CalendarExtender>
                                            <asp:TextBox ID="txtScheduleDate" runat="server" BorderStyle="Groove" OnTextChanged="txtActualKirim_TextChanged"
                                                                                    Visible="False" Width="81px"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="txtScheduleDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                                                    TargetControlID="txtScheduleDate">
                                                                                </cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">User </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtCreatedBy" runat="server" class="form-control" Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="pull-right">
                            <span class="input-icon input-icon-right">
                                <input id="Button1" runat="server" class="btn btn-sm btn-info" style="background-color: white; font-weight: bold; font-size: 11px;"
                                    type="button" value="rePosting stock" visible="True" onserverclick="btnPosting_ServerClick" />
                                <input id="btnPostingReceipt" runat="server" onserverclick="btnPostingReceipt_ServerClick"
                                                style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                                value="Posting Receipt" visible="False" />
                            </span>
                            <span class="input-icon input-icon-right">
                                <input id="btnCancelKirim" runat="server" class="btn btn-sm btn-info" style="background-color: white; font-weight: bold; font-size: 11px;"
                                    type="button" value="Cancel Kirim" visible="True" onserverclick="btnCancelKirim_ServerClick" />
                            </span>
                            <span class="input-icon input-icon-right">
                                <asp:CheckBox ID="chkDeco" runat="server" Checked="True" OnCheckedChanged="chkDeco_CheckedChanged"
                                    Text="Konversi DecoStone" AutoPostBack="True" Font-Size="X-Small" />
                            </span>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <div class="panel-heading">
                                    <h3 class="panel-title pull-left">List</h3>
                                </div>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panelmap" runat="server" Width="100%" Font-Size="X-Small" Visible="false">
                                    <table style="font-size: x-small">
                                        <tr>
                                            <td>ID
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblIDSJ" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="20px" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>Item Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="LblItemName" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="20px" Width="229px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>Partno
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="20px" Width="182px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td>Stocker
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStocker" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <input id="btnMap" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                    type="button" value="Simpan" visible="True" onserverclick="btnMap_ServerClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <div id="div2" style="width: 100%; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                            <asp:BoundField DataField="Qty" HeaderText="Jumlah" />
                                            <asp:TemplateField HeaderText="Potong Stock Pabrik">
                                                <ItemTemplate>
                                                    <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                        Width="100%">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="ItemIdser" HeaderText="ItemId" />
                                                            <asp:BoundField DataField="tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                            <asp:BoundField DataField="groupdesc" HeaderText="Group desc" />
                                                            <asp:BoundField DataField="partnokrm" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="lokasiser" HeaderText="dr Lokasi" />
                                                            <asp:BoundField DataField="lokasikrm" HeaderText="Lokasi" Visible="False" />
                                                            <asp:BoundField DataField="qty" HeaderText="Qty Penyiapan">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Qty Kirim">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQtyKirim" runat="server" Font-Size="X-Small" Height="20px" Width="64px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Size="X-Small" Height="19px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pengiriman">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlPengiriman" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPengiriman_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Size="X-Small" Height="19px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jenis Palet">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlJenisPalet" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Size="X-Small" Height="19px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Jumlah">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtJumlah" runat="server" Width="50px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Size="X-Small" Height="19px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                        Width="100%" OnRowCommand="GridViewtrans0_RowCommand">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                            <asp:BoundField DataField="partnokrm" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="lokasiser" HeaderText="dr Lokasi" />
                                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                                            <asp:BoundField DataField="Pengiriman" HeaderText="Pengiriman" />
                                                            <asp:BoundField DataField="JenisPalet" HeaderText="JenisPalet" />
                                                            <asp:BoundField DataField="JmlPalet" HeaderText="JmlPalet" />
                                                            <asp:ButtonField ButtonType="Button" CommandName="tambah" Text="Edit Palet">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Button" CommandName="tambah" Text="Add Partno">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <asp:Panel ID="PanelPalet" runat="server" BackColor="#CCCCFF" Visible="False" Font-Size="X-Small">
                                        <table style="font-size: x-small">
                                            <tr>
                                                <td colspan="6">ID :
                                                    <asp:Label ID="lblid" runat="server" Text="0"></asp:Label>
                                                    <asp:Label ID="lbltmpjmlkirim" runat="server" Text="0" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <input id="btnSave0" runat="server" onserverclick="btnSave0_ServerClick" style="background-color: White; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Simpan" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Pengiriman :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpengiriman" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td>Jenis Palet :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbljenispalet" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td>Jumlah Kirim :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbljmlkirim" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td>Jumlah Palet :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbljmlpalet" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Pengiriman :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPengirimanE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPengirimanE_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Jenis Palet :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlJenisPaletE" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Jumlah Kirim :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtjmlkirim" runat="server" Width="50px" AutoPostBack="True"
                                                        OnTextChanged="txtjmlkirim_TextChanged">0</asp:TextBox>
                                                </td>
                                                <td>Jumlah Palet :&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJumlahE" runat="server" Width="50px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Data Loading Time</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Label ID="lbKoneksi" runat="server" Visible="False"></asp:Label><asp:TextBox
                                    ID="txtCardID" runat="server" AutoPostBack="True" BorderStyle="Groove" onkeyup="this.value=this.value.toUpperCase()"
                                    TabIndex="1" Visible="False" Width="130px"></asp:TextBox>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">No Urutan Masuk</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtUrutanNo" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtUrutanNo_TextChanged"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Tanggal Masuk</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTglIn" runat="server" class="form-control" AutoPostBack="True" BackColor="#FF99FF"
                                                onkeyup="this.value=this.value.toUpperCase()" TabIndex="1"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">No Urutan Keluar</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtUrutanOut" Text='<%=Plant %>' class="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Tanggal Keluar</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTglOut" runat="server" class="form-control" AutoPostBack="True" BackColor="#FF99FF"
                                                onkeyup="this.value=this.value.toUpperCase()" TabIndex="1"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtTglOut" Format="dd-MMM-yyyy 00:00"
                                                runat="server"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Jenis Kendaraan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlKendaraan" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlKendaraan_SelectedIndexChanged"
                                                Width="100%">
                                                <asp:ListItem>-- Pliih Jenis Kendaraan --</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Tipe Kendaraan</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="rbMblSendiri" runat="server" GroupName="MS" />
                                            <label for="form-field-9" style="font-size: 12px">Mobil Sendiri</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="rbMblLuar" runat="server" GroupName="MS" />
                                            <label for="form-field-9" style="font-size: 12px">Mobil Luar</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Tujuan Kirim </label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RbDalam" runat="server" AutoPostBack="True" OnCheckedChanged="RbDalam_CheckedChanged" Checked="true" />
                                            <label for="form-field-9" style="font-size: 12px">Dalam Pulau</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RbLuar" runat="server" AutoPostBack="True" OnCheckedChanged="RbLuar_CheckedChanged" />
                                            <label for="form-field-9" style="font-size: 12px">Luar Pulau</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input id="btnUpdate0" runat="server" class="btn btn-sm btn-primary" onserverclick="btnUpdate0_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="100%"
                                    OnPageIndexChanging="GridView2_PageIndexChanging" OnRowCommand="GridView2_RowCommand"
                                    OnRowDataBound="GridView2_RowDataBound" PageSize="5" TabIndex="12" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="NoUrut" HeaderText="No Kartu" />
                                        <asp:BoundField DataField="TimeIn" HeaderText="Jam Masuk" />
                                        <asp:BoundField DataField="TimeOut" HeaderText="Jam Keluar" />
                                        <asp:BoundField DataField="JenisMobil" HeaderText="Jenis Kendaraan" />
                                        <asp:BoundField DataField="MobilSendiri" HeaderText="Mobil" />
                                        <asp:BoundField DataField="NoPolisi" HeaderText="No Polisi" />
                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                    </Columns>
                                    <RowStyle BackColor="White" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="#003399" />
                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <PagerStyle BorderStyle="Solid" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>