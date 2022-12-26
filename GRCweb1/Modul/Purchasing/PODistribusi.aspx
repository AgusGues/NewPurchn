<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PODistribusi.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.PODistribusi" %>

<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            function openWindow(id) {
                window.showModalDialog("../../ModalDialog/PODistrribusiDialog.aspx?p=" + id, "", "resizable:yes;dialogHeight: 500px; dialogWidth: 900px;scrollbars=no");
            }
        </script>

    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>

        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        PEMANTAUAN DISTRIBUSI PO
                    </div>
                    <div style="padding: 2px"></div>



                    <%--copy source design di sini--%>
                    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="width: 100%; height: 100%; font-size: x-small; border-collapse: collapse">

                            <tr valign="top">
                                <td>
                                    <div class="content">
                                        <table style="width: 100%; border-collapse: collapse; font-size: small">
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">&nbsp;</td>
                                                <td style="width: 10%">Periode</td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                    <asp:Button ID="btnPrint" runat="server" Text="Export to Excel" OnClick="btnPrint_onClick" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="contentlist" id="lstRkp" runat="server" style="height: 420px">
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" rowspan="2" style="width: 5%">Tanggal</th>
                                                        <th class="kotak" rowspan="2" style="width: 6%">No.PO</th>
                                                        <th class="kotak" rowspan="2" style="width: 12%">Supplier</th>
                                                        <th class="kotak" colspan="6">Konfirmasi ke Supplier</th>
                                                        <th class="kotak" rowspan="2" style="width: 15%">Keterangan</th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" style="width: 8%">Kirim via</th>
                                                        <th class="kotak" style="width: 5%">Tgl Kirim</th>
                                                        <th class="kotak" style="width: 8%">Diterima Oleh</th>
                                                        <th class="kotak" style="width: 5%">Tgl Terima</th>
                                                        <th class="kotak" style="width: 5%">Est Dlv</th>
                                                        <th class="kotak" style="width: 5%">Actual Dlv</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstPO" runat="server" OnItemCommand="lstPO_ItemCommand" OnItemDataBound="lstPO_DataBound">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <%--<td class="kotak"><%# Container.ItemIndex+1 %></td>--%>
                                                                <td class="kotak tengah"><%# Eval("POPurchnDate","{0:d}") %></td>
                                                                <td class="kotak tengah"><%# Eval("NoPO")%></td>
                                                                <td class="kotak"><%# Eval("SupplierName") %></td>
                                                                <td class="kotak">&nbsp;<asp:Label ID="ddlKirim" runat="server"></asp:Label></td>
                                                                <td class="kotak tengah">&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label></td>
                                                                <td class="kotak">&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label></td>
                                                                <td class="kotak tengah">&nbsp;<asp:Label ID="Label3" runat="server"></asp:Label></td>
                                                                <td class="kotak tengah">&nbsp;<asp:Label ID="Label4" runat="server"></asp:Label></td>
                                                                <td class="kotak tengah">&nbsp;<asp:Label ID="Label5" runat="server"></asp:Label></td>
                                                                <td class="kotak">
                                                                    <asp:Label ID="Keterangan" runat="server" Width="85%"></asp:Label>&nbsp;
                                                        <asp:ImageButton ID="ActDlv" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="act" />
                                                                </td>
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
