<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanAnalisaData.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.PemantauanAnalisaData" %>

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
          <script type="text/javascript">
        // fix for deprecated method in Chrome / js untuk bantu view modal dialog
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {
                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";
                // get the modal specs
                var mdattrs = arg3.split(";");
                for (i = 0; i < mdattrs.length; i++) {
                    var mdattr = mdattrs[i].split(":");
                    var n = mdattr[0];
                    var v = mdattr[1];
                    if (n) { n = n.trim().toLowerCase(); }
                    if (v) { v = v.trim().toLowerCase(); }
                    if (n == "dialogheight") {
                        h = v.replace("px", "");
                    } else if (n == "dialogwidth") {
                        w = v.replace("px", "");
                    } else if (n == "resizable") {
                        resizable = v;
                    } else if (n == "scroll") {
                        scroll = v;
                    } else if (n == "status") {
                        status = v;
                    }
                }
                var left = window.screenX + (window.outerWidth / 2) - (w / 2);
                var top = window.screenY + (window.outerHeight / 2) - (h / 2);
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
    </script>
        <script type="text/javascript">
        function Cetak() {
            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=RekapReceipt", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }
    </script>

    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>
        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">    
		<ContentTemplate> --%>
        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        PEMANTAUAN ANALISA DATA
                    </div>
                    <div style="padding: 2px"></div>

                     <%--copy source design di sini--%>

                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                      
                        <tr>
                            <td style="width: 100%;">
                                <div style="width: 100%; background-color: #B0C4DE">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                                            </td>
                                            <td style="width: 303px">
                                                <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Width="164px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl1">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                                            </td>
                                            <td style="width: 303px">
                                                <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Width="164px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl2">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                                <asp:Label ID="Label1" runat="server" Text="Departemen"></asp:Label>
                                            </td>
                                            <td style="width: 303px">
                                                <asp:DropDownList ID="ddlDepartemen" runat="server" Height="16px" Width="164px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                            </td>
                                            <td style="width: 303px">
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 116px">
                                            </td>
                                            <td style="width: 303px">
                                                <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClick="BtnPreview_Click" />
                                                <asp:Button ID="btnLihat" runat="server" Text="Preview" OnClick="BtnPreview2_Click" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height: 550px; overflow: auto" id="Div6" runat="server">
                                        <table id="zibz" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                            width: 200%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" style="width: 5%">
                                                        No Analisa
                                                    </th>
                                                    <th class="kotak" style="width: 5%">
                                                        Tgl Analisa
                                                    </th>
                                                    <th class="kotak" style="width: 5%">
                                                        Departement
                                                    </th>
                                                    <th class="kotak" style="width: 10%">
                                                        Sarmut Perusahaan
                                                    </th>
                                                    <th class="kotak" style="width: 10%">
                                                        Sarmut Departement
                                                    </th>
                                                    <th class="kotak" style="width: 10%">
                                                        Target
                                                    </th>
                                                    <th class="kotak" style="width: 4%">
                                                        Parameter
                                                    </th>
                                                    <th class="kotak" style="width: 4%">
                                                        Satuan
                                                    </th>
                                                    <th class="kotak" style="width: 5%">
                                                        Jenis
                                                    </th>
                                                    <th class="kotak" style="width: 20%">
                                                        Analisa Permasalahan
                                                    </th>
                                                    <th class="kotak" style="width: 4%">
                                                        Faktor
                                                    </th>
                                                    <th class="kotak" style="width: 15%">
                                                        Tindakan / Perbaikan
                                                    </th>
                                                    <th class="kotak" style="width: 15%">
                                                        Tanggal
                                                    </th>
                                                    <th class="kotak" style="width: 4%">
                                                        Jenis
                                                    </th>
                                                    <th class="kotak" style="width: 4%">
                                                        Status
                                                    </th>
                                                    <%--<th class="kotak" style="width: 4%">
                                                        Status
                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="tb1" runat="server">
                                                <asp:Repeater ID="lstR2" runat="server" OnItemCommand="lstR2_Command" OnItemDataBound="lstR2_Databound">
                                                    <ItemTemplate>
                                                        <tr id="Tr1" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah">
                                                                <%# Eval("AnNo") %>
                                                                <asp:ImageButton ID="attPrs" runat="server" CssClass='<%# Eval("ID") %>' CommandArgument='<%# Container.ItemIndex %>'
                                                                    CommandName="attachPrs" ImageUrl="~/TreeIcons/Icons/BookY.gif" Visible="false" />
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TglAnalisa","{0:d}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Dept") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SarmutPerusahaan") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SarmutDepartemen") %>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <%# Eval("TargetVID", "{0:N2}")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("ParamID") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("SatuanID") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("Jenis") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <table width="100%" style="font-size: x-small">
                                                                    <thead>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Repeater ID="attachPrs" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris">
                                                                                            <td>
                                                                                                <%# Eval("Uraian")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <table width="100%" style="font-size: x-small">
                                                                    <thead>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Repeater ID="attachPrs2" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris" valign="top">
                                                                                            <td>
                                                                                                <%# Eval("Penyebab")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <table width="100%" style="font-size: x-small">
                                                                    <thead>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Repeater ID="attachPrs3" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris" valign="top">
                                                                                            <td>
                                                                                                <%# Eval("Tindakan")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <table width="100%" style="font-size: x-small">
                                                                    <thead>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Repeater ID="attachPrs4" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris" valign="top">
                                                                                            <td>
                                                                                                <%# Eval("Tanggal", "{0:d}")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </td>
                                                             <td class="kotak tengah">
                                                                <table width="100%" style="font-size: x-small">
                                                                    <thead>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Repeater ID="attachPrs5" runat="server">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris" valign="top">
                                                                                            <td>
                                                                                                <%# Eval("Jenis")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </td>
                                                             <td class="kotak tengah">
                                                                <%# Eval("Status") %>
                                                            </td>
                                                        </tr>
                                                        <%-- <asp:Repeater ID="lstR21" runat="server">
                                                            <HeaderTemplate>
                                                                <tr class="Line3">
                                                                    <td class="kotak" colspan="2">
                                                                        <b>Analisa Permasalahan</b>
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                    <td class="kotak">
                                                                    </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak">
                                                                        <%# Eval("Uraian") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>--%>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
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
