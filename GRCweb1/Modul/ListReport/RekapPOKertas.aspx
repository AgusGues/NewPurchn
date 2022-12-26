<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPOKertas.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapPOKertas" %>

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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                PEMANTAUAN PO KERTAS SESUAI DATA PLANT
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>
                            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                                <table style="table-layout: fixed; height: 100%" width="100%">
                                    <tbody>
                                        
                                        <tr>
                                            <td style="width: 100%" valign="top">
                                                <div class="">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                        <tr>
                                                            <td class="style4" style="width: 10%"></td>
                                                            <td style="width: 20%">&nbsp;
                                                            </td>
                                                            <td style="width: 20%">&nbsp;
                                                            </td>
                                                            <td style="width: 30%" colspan="2">
                                                                <asp:TextBox ID="txtSearch" runat="server" Visible="false"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="" valign="top">Periode</td>

                                                            <td>
                                                                <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Lokasi Plant</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCompany" runat="server" Width="100%"></asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5"></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" />
                                                            </td>
                                                            <td>Total Pencapaian (Tonase):
                                                <asp:Label ID="LblPencapaian" runat="server" Text="0"></asp:Label>
                                                                &nbsp;</td>
                                                            <td>Total Pencapaian (Persen):
                                                                <asp:Label ID="LblPencapaian0" runat="server" Text="0"></asp:Label>
                                                                &nbsp;&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <hr>
                                                    <div class="contentlist" style="height: 380px;">
                                                        <div id="lst" runat="server">
                                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                                <tr class="tbHeader">
                                                                    <th class="kotak" style="width: 2%">No.</th>
                                                                    <th class="kotak" style="width: 5%">Agen</th>
                                                                    <th class="kotak" style="width: 5%">Tgl RMS</th>
                                                                    <th class="kotak" style="width: 5%">RMS No</th>
                                                                    <th class="kotak" style="width: 5%">NoPO</th>
                                                                    <th class="kotak" style="width: 12%">Supplier</th>
                                                                    <%-- <th class="kotak" style="width:3%">ItemCode</th>--%>
                                                                    <th class="kotak" style="width: 10%">ItemName</th>
                                                                    <%--<th class="kotak" style="width:1%">Unit</th>--%>
                                                                    <th class="kotak" style="width: 8%">BK</th>
                                                                    <th class="kotak" style="width: 8%">KA</th>
                                                                    <th class="kotak" style="width: 8%">SAMPAH(%)</th>
                                                                    <th class="kotak" style="width: 8%">POTONGAN(KG)</th>
                                                                    <th class="kotak" style="width: 5%">BB</th>
                                                                    <th class="kotak" style="width: 5%">RMS</th>
                                                                </tr>
                                                                <tbody>
                                                                    <asp:Repeater ID="Rba" runat="server" OnItemDataBound="Rba_DataBound" OnItemCommand="Rba_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" id="lstR" runat="server">
                                                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                                <td class="kotak " style="white-space: nowrap"><%# Eval("AgenName")%></td>
                                                                                <td class="kotak tengah" style="white-space: nowrap"><%# Eval("TglReceipt", "{0:d}")%></td>
                                                                                <td class="kotak tengah" style="white-space: nowrap"><%# Eval("NoRMS")%></td>
                                                                                <td class="kotak tengah" style="white-space: nowrap"><%# Eval("NoPO")%></td>
                                                                                <td class="kotak " style="white-space: nowrap"><%# Eval("SupplierName")%></td>
                                                                                <%--<td class="kotak "><asp:Label ID="xx" runat="server" Text="'" ForeColor="White"></asp:Label>
                                                            <%# Eval("ItemCode")%></td>--%>
                                                                                <td class="kotak " style="white-space: nowrap"><%# Eval("ItemName")%></td>
                                                                                <%--<td class="kotak tengah"><%# Eval("Unit")%></td>--%>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("GrossPlant", "{0:N0}")%></td>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("KAPlant", "{0:n2}")%></td>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("Sampah", "{0:N2}")%></td>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("Potongan", "{0:N0}")%></td>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("NettPlant", "{0:N0}")%></td>
                                                                                <td class="kotak angka" style="white-space: nowrap"><%# Eval("QtyRMS", "{0:N2}")%></td>
                                                                            </tr>

                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
