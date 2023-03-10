<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapStockMKubik.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LapStockMKubik" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=RekapStockTriwulan", 900, 800);
                        }
                        function Cetak1() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=RekapRetur", 900, 800);
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">REKAP STOCK PER TRIWULAN</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">

                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddTahun" runat="server"
                                                OnSelectedIndexChanged="ddTahun_SelectedIndexChanged" class="form-control" AutoPostBack="True">
                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlBulan" class="form-control" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                                                Width="132px">
                                                <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
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
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tahun</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddTahun0" class="form-control" runat="server" Enabled="False"
                                                OnSelectedIndexChanged="ddTahun_SelectedIndexChanged">
                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlBulan0" class="form-control" runat="server" Enabled="False"
                                                OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged" Width="132px">
                                                <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
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
                                        <asp:Button ID="btnPrint" class="btn btn-sm btn-primary" runat="server" OnClick="btnPrint_Click" Text="Print" Width="62px" />
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