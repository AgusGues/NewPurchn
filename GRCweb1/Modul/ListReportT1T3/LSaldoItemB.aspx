<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSaldoItemB.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LSaldoItemB" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LSaldoItemB", 600, 1200)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN SALDO ITEM BULANAN</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlBulan" class="form-control" runat="server" Width="132px">
                                                <asp:ListItem Value="janqty">Januari</asp:ListItem>
                                                <asp:ListItem Value="Febqty">Februari</asp:ListItem>
                                                <asp:ListItem Value="Marqty">Maret</asp:ListItem>
                                                <asp:ListItem Value="Aprqty">April</asp:ListItem>
                                                <asp:ListItem Value="Meiqty">Mei</asp:ListItem>
                                                <asp:ListItem Value="Junqty">Juni</asp:ListItem>
                                                <asp:ListItem Value="Julqty">Juli</asp:ListItem>
                                                <asp:ListItem Value="Aguqty">Agustus</asp:ListItem>
                                                <asp:ListItem Value="Sepqty">September</asp:ListItem>
                                                <asp:ListItem Value="Oktqty">Oktober</asp:ListItem>
                                                <asp:ListItem Value="Novqty">Nopember</asp:ListItem>
                                                <asp:ListItem Value="Desqty">Desember</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9"  style="font-size: 14px">Tahun</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTahun" runat="server" AutoPostBack="True"
                                                class="form-control" Width="84px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <div class="row">
                                            <span>
                                                <asp:RadioButton ID="RB1" Style="margin:10px" runat="server" Checked="True" Font-Size="X-Small"
                                                    GroupName="I" Text="Tahap III" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="RB2" Style="margin:10px" runat="server" Font-Size="X-Small" GroupName="I"
                                                    Text="BP" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="RB3" Style="margin:10px" runat="server" Font-Size="X-Small" GroupName="I"
                                                    Text="KW II" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="RB4" runat="server" Font-Size="X-Small" GroupName="I"
                                                    Text="BS" />
                                            </span>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </asp:Panel>
                                    <div class="row">
                                        <input id="btnPrint" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Preview" />
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