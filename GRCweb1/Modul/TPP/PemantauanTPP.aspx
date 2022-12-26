<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanTPP.aspx.cs" Inherits="GRCweb1.Modul.TPP.PemantauanTPP" %>

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

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        PEMANTAUAN TPP
                    </div>
                    <div style="padding: 2px"></div>


                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                    <table style="width: 100%; font-size: x-small;">
                        <tr>
                            <td style="width: 82px">Dari Tanggal
                            </td>
                            <td style="width: 158px">
                                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                            </td>
                            <td style="width: 78px">s/d Tanggal
                            </td>
                            <td style="width: 151px">
                                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                            </td>
                            <td style="width: 220px" valign="middle">&nbsp;
                <asp:Label ID="LblDept" runat="server" Text="Departemen"></asp:Label>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlDepartemen" runat="server" Height="16px" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td>Asal Masalah
                <asp:DropDownList ID="ddlMasalah" runat="server" AutoPostBack="True" Width="225px">
                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 82px; height: 19px;"></td>
                            <td style="width: 158px; height: 19px;"></td>
                            <td style="width: 78px; height: 19px;"></td>
                            <td style="width: 151px; height: 19px;"></td>
                            <td colspan="2" style="height: 19px"></td>
                        </tr>
                        <tr>
                            <td style="width: 82px">&nbsp;
                            </td>
                            <td style="width: 158px">&nbsp;
                            </td>
                            <td style="width: 78px">&nbsp;
                            </td>
                            <td align="right" style="width: 151px">&nbsp;
                            </td>
                            <td align="center" style="width: 220px">
                                <asp:Button ID="BtnPreview" runat="server" Height="20px" Text="Cetak" Width="66px"
                                    OnClick="BtnPreview_Click" Style="font-family: Calibri" />
                               
                                <asp:Button ID="BtnPreview2" runat="server" Height="20px" Text="Preview" Width="66px"
                                    OnClick="BtnPreview2_Click" Style="font-family: Calibri" />
                                <asp:Button ID="BtnExport" runat="server" Height="20px" Text="Ke Excel" Width="72px"
                                    OnClick="BtnExport_Click" Style="font-family: Calibri" />
                            </td>
                            <td align="right" style="width: 151px">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <div class="contentlist" style="height: 410px">
                        <div id="DivRoot" align="left">
                            <div id="lst" runat="server">
                                <div style="overflow: hidden;" id="DivHeaderRow">
                                </div>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; display: block; font-family: Calibri;"
                                    border="0">
                                    <thead>
                                        <tr class=" tbHeader baris">
                                            <th class="kotak" style="width: 11%">No. TPP
                                            </th>
                                            <th class="kotak" style="width: 8%">Tgl. TPP
                                            </th>
                                            <th class="kotak" style="width: 8%">Tgl. Kejadian
                                            </th>
                                            <th class="kotak" style="width: 10%">Departemen
                                            </th>
                                            <th class="kotak" style="width: 6%">PIC
                                            </th>
                                            <th class="kotak" style="width: 10%">Keterangan
                                            </th>
                                            <th class="kotak" style="width: 23%">Ketidaksesuaian
                                            </th>
                                            <th class="kotak" style="width: 14%">Uraian
                                            </th>
                                            <th class="kotak" style="width: 10%">Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody style="font-family: Calibri">
                                        <asp:Repeater ID="DataTPP" runat="server" OnItemDataBound="DataTPP_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                    <td class="kotak tengah">&nbsp;
                                        <%# Eval("Laporan_No")%>
                                                    </td>
                                                    <td class="kotak">&nbsp;
                                        <%# Eval("CreatedTime")%>
                                                    </td>
                                                    <td class="kotak">&nbsp;
                                        <%# Eval("TPP_Date")%>
                                                    </td>
                                                    <td class="kotak">&nbsp;
                                        <%# Eval("Departemen")%>
                                                    </td>
                                                    <td class="kotak" align="left">&nbsp;
                                        <%# Eval("PIC")%>
                                                    </td>
                                                    <td class="kotak" align="left">

                                                        <%# Eval("Keterangan")%>
                                                    </td>
                                                    <td class="kotak" align="left">

                                                        <%# Eval("Uraian")%>
                                                    </td>
                                                    <td class="kotak" align="left">

                                                        <%# Eval("Ketidaksesuaian")%>
                                                    </td>
                                                    <td class="kotak" align="left">&nbsp;
                                        <%# Eval("Status")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
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

   
</asp:Content>
