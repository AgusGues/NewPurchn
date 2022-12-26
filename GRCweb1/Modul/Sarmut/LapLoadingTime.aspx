<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapLoadingTime.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.LapLoadingTime" %>

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
                        Laporan Loading Time
                    </div>
                    <div style="padding: 2px"></div>



                    <%--copy source design di sini--%>
                    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="table-layout: fixed; height: 100%" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%; height: 49px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" valign="top">
                                        <div class="content">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 31px">&nbsp;
                                                    </td>
                                                    <td style="width: 114px">&nbsp;
                                                    </td>
                                                    <td style="width: 562px">&nbsp;
                                                    </td>
                                                    <td rowspan="5">
                                                        <table style="width: 100%">
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri; text-decoration: underline">
                                                                    <strong>&nbsp;PENGATURAN DAFTAR WAKTU MUAT :</strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri; font-weight: 700; font-style: italic;">
                                                                    <strong>&nbsp;<span style="background-color: #CCCCCC">Pengiriman Dalam Pulau</span></strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri">
                                                                    <strong>&nbsp;Senin - Jumat : 06:30 - 21:00 WIB </strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri">
                                                                    <strong>&nbsp;Sabtu : 06:30 - 15:00 WIB </strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri; font-style: italic; font-weight: 700;">
                                                                    <strong>&nbsp;<span style="background-color: #CCCCCC">Pengiriman Luar Pulau</span></strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri">
                                                                    <strong>&nbsp;Senin - Jumat : 06:30 - 18:00 WIB </strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: x-small; font-family: Calibri">
                                                                    <strong>&nbsp;Sabtu : 06:30 - 12:00 WIB </strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: xx-small; font-family: Calibri">
                                                                    <strong>&nbsp;<span style="background-color: #00FF00">Jika pendaftaran di luar waktu 
                                                            yg sdh di tetapkan maka perhitungan waktu loadingnya akan dimulai</span></strong>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 75%">
                                                                <td style="font-size: xx-small; font-family: Calibri">
                                                                    <strong>&nbsp;<span style="background-color: #66FF66">pukul 06:30 - waktu selesai 
                                                            loading</span></strong>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 31px">&nbsp;
                                                    </td>
                                                    <td style="width: 114px">
                                                        <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                                                    </td>
                                                    <td style="width: 562px">
                                                        <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Width="140px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                            TargetControlID="txtTgl1" Enabled="True"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 31px">&nbsp;
                                                    </td>
                                                    <td style="width: 114px">
                                                        <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                                                    </td>
                                                    <td style="width: 562px">
                                                        <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Width="140px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                            TargetControlID="txtTgl2" Enabled="True"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 31px">&nbsp;
                                                    </td>
                                                    <td style="width: 114px">&nbsp;
                                                    </td>
                                                    <td style="width: 562px">
                                                        <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Print" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="contentlist" style="height: 360px;">
                                                <div id="lst" runat="server">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                        <tr class="tbHeader">
                                                            <th class="kotak" style="width: 4%">No.</th>
                                                            <th class="kotak" style="width: 6%">No.Polisi</th>
                                                            <th class="kotak" style="width: 5%">No.Urut</th>
                                                            <th class="kotak" style="width: 24%">Jenis Mobil</th>
                                                            <th class="kotak" style="width: 4%">Mobil</th>
                                                            <th class="kotak" style="width: 8%">Time Daftar</th>
                                                            <th class="kotak" style="width: 8%">Time In</th>
                                                            <th class="kotak" style="width: 8%">Time Out</th>
                                                            <th class="kotak" style="width: 5%">Target</th>
                                                            <th class="kotak" style="width: 6%">Waktu Loading</th>
                                                            <th class="kotak" style="width: 7%">Tujuan</th>
                                                            <th class="kotak" style="width: 6%">Status</th>
                                                            <th class="kotak" style="width: 9%">Keterangan</th>
                                                        </tr>
                                                        <tbody>
                                                            <asp:Repeater ID="lstLoading" runat="server" OnItemDataBound="lstLoading_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" id="ps1" runat="server">
                                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                        <td class="kotak tengah"><%# Eval("NoPolisi") %></td>
                                                                        <td class="kotak"><%# Eval("NoUrut") %></td>
                                                                        <td class="kotak"><%# Eval("JenisMobil") %></td>
                                                                        <td class="kotak"><%# Eval("Keterangan")%></td>
                                                                        <td class="kotak tengah"><%# Eval("TimeDaftar") %></td>
                                                                        <td class="kotak tengah"><%# Eval("TimeIn") %></td>
                                                                        <td class="kotak tengah"><%# Eval("TimeOut") %></td>
                                                                        <td class="kotak tengah"><%# Eval("Target") %></td>
                                                                        <td class="kotak tengah"><%# Eval("Status2") %></td>
                                                                        <td class="kotak tengah"><%# Eval("Tujuan2") %></td>
                                                                        <td class="kotak tengah"><%# Eval("LoadingNo") %></td>
                                                                        <td class="kotak tengah"><%# Eval("Noted")%></td>
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
