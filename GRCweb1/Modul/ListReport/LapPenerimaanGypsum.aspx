<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPenerimaanGypsum.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPenerimaanGypsum" %>

<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>


        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>



    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>

        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        REKAP PENERIMAAN GYPSUM CURAH
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>
                    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="width: 100%; border-collapse: collapse">

                            <tr>
                                <td>
                                    <div class="content">
                                        <table class="tbStandart">
                                            <tr>
                                                <td colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%">&nbsp;</td>
                                                <td style="width: 10%">Periode</td>
                                                <td style="width: 20%">
                                                    <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <div id="lst" runat="server" class="contentlist" style="height: 420px">
                                            <table class="tbStandart" border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" rowspan="2" style="width: 4%">No.</th>
                                                        <th class="kotak" rowspan="2" style="width: 8%">Tanggal Ambil</th>
                                                        <th class="kotak" rowspan="2" style="width: 8%">PO BPAS</th>
                                                        <th class="kotak" rowspan="2" style="width: 15%">Supplier</th>
                                                        <th class="kotak" rowspan="2" style="width: 10%">No. SJ</th>
                                                        <th class="kotak" colspan="2">Timbangan</th>
                                                        <th class="kotak" style="width: 5%">Kadar Air</th>
                                                        <th class="kotak" style="width: 5%">Selisih KA</th>
                                                        <th class="kotak" style="width: 10%">RMS</th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" style="width: 5%">SUPPLIER</th>
                                                        <th class="kotak" style="width: 5%">BPAS</th>
                                                        <th class="kotak">(%)</th>
                                                        <th class="kotak">>28%</th>
                                                        <th class="kotak">(KG)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstRMS" runat="server" OnItemDataBound="lstRMS_DataBound">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                <td class="kotak tengah"><%# Eval("ReceiptDate","{0:d}") %></td>
                                                                <td class="kotak tengah"><%# Eval("PONo") %></td>
                                                                <td class="kotak"><%# Eval("SupplierName") %></td>
                                                                <td class="kotak"><%# Eval("Keterangan1") %></td>
                                                                <td class="kotak angka"><%# Eval("QtyTimbang","{0:N2}") %></td>
                                                                <td class="kotak angka"><%# Eval("QtyBPAS","{0:N2}") %></td>
                                                                <td class="kotak tengah"><%# Eval("KadarAir","{0:N2}") %></td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="selisih" runat="server"></asp:Label></td>
                                                                <td class="kotak angka"><%# Eval("Quantity","{0:n2}") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>


                </div>
            </div>
        </div>
        <script src="../../assets/jquery.js" type="text/javascript"></script>
        <script src="../../assets/js/jquery-ui.min.js"></script>
        <script src="../../assets/select2.js"></script>
        <script src="../../assets/datatable.js"></script>
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>
</asp:Content>
