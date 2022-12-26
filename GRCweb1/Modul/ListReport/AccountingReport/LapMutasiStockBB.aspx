<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapMutasiStockBB.aspx.cs" Inherits="GRCweb1.Modul.ListReport.AccountingReport.LapMutasiStockBB" %>

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

                    <script type="text/javascript">
                        function MyPopUpWin(url, width, height) {
                            var leftPosition, topPosition;
                            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
                            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
                            window.open(url, "Window2",
                                "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
                                + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
                                + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
                        }

                        function Cetak() {
                            MyPopUpWin("../AccountingReport/Report.aspx?IdReport=LMutasiTransStock", 900, 800)
                        }
                        function Cetak2() {
                            MyPopUpWin("../AccountingReport/Report.aspx?IdReport=LMutasiStock", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN MUTASI STOCK INVENTORY</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <span>
                                            <asp:RadioButton ID="RB_Rekap" runat="server" Style="margin:10px" GroupName="a" Text="Rekap" Checked="True" />
                                        </span>
                                        <span>
                                            <asp:RadioButton ID="RB_Detail" runat="server" GroupName="a" Text="Detail" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlBulan" runat="server" class="form-control" Width="132px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Januari</asp:ListItem>
                                                    <asp:ListItem Value="2">Februari</asp:ListItem>
                                                    <asp:ListItem Value="3">Maret</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">Mei</asp:ListItem>
                                                    <asp:ListItem Value="6">Juni</asp:ListItem>
                                                    <asp:ListItem Value="7">Juli</asp:ListItem>
                                                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddTahun" runat="server" class="form-control" OnSelectedIndexChanged="ddTahun_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                                </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Laporan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="jStock" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Nama Barang</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtKode" class="form-control" runat="server" AutoPostBack="true" Width="350px" 
                                                    ontextchanged="txtKode_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtKode_AutoCompleteExtender" runat="server" CompletionSetCount="20"
                                                    EnableCaching="true" Enabled="true" FirstRowSelected="true" MinimumPrefixLength="3"
                                                    ServiceMethod="GetItemList" ServicePath="AutoComplete.asmx" TargetControlID="txtKode"
                                                    CompletionListCssClass="autocomplete_completionListElement">
                                                </cc1:AutoCompleteExtender>
                                            <asp:TextBox ID="txtItemID" runat="server" Visible="false"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btnPrint" class="btn btn-sm btn-primary" runat="server" OnClick="btnPrint_Click" Text="Print" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <table style="border-collapse:collapse; font-size:x-small" id="prv" runat="server" visible="false">
                                        <thead>
                                            <tr  style="background-color:#FFE4B5">
                                                <th style="width:2%" align="center" class="kotak" rowspan="3">#</th>
                                                <th style="width:15%" align="center" class="kotak" rowspan="3">ItemName</th>
                                                <th style="" align="center" class="kotak" colspan="3" rowspan="2">SaldoAwal</th>
                                                <th style="" align="center" class="kotak" colspan="9">Penerimaan</th>
                                                <th style="" align="center" class="kotak" colspan="9">Pengeluaran</th>
                                                <th style="" align="center" class="kotak" colspan="3" rowspan="2">Saldo Akhir</th>
                                            </tr>
                                            <tr style="background-color:#FFE4B5">
                                                <th style="" align="center" class="kotak" colspan="3">Pembelian</th>
                                                <th style="" align="center" class="kotak" colspan="3">Penyesuaian</th>
                                                <th style="" align="center" class="kotak" colspan="3">Return Pakai</th>
                                                <th style="" align="center" class="kotak" colspan="3">Produksi</th>
                                                <th style="" align="center" class="kotak" colspan="3">Penyesuaian</th>
                                                <th style="" align="center" class="kotak" colspan="3">Return Supplier</th>
                                            </tr>
                                            <tr style="background-color:#FFE4B5">
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                                <th style="width:5%" align="center" class="kotak">Qty</th>
                                                <th style="width:5%" align="center" class="kotak">HS</th>
                                                <th style="width:5%" align="center" class="kotak">Amt</th>
                                            </tr>
                                           </thead>
                                          <%--<tbody>
                                          <asp:Repeater ID="lstMutasi" runat="server">
                                                <ItemTemplate>
                                                    <tr class="baris EvenRows">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak kiri" style="white-space:nowrap"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                           
                                          </tbody>--%> 
                                           
                                     </table>
                        <div id="mess" runat="server" style="display:none"></div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
