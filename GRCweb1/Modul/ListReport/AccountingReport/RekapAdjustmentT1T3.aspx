<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAdjustmentT1T3.aspx.cs" Inherits="GRCweb1.Modul.ListReport.AccountingReport.RekapAdjustmentT1T3" %>

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
                            MyPopUpWin("../AccountingReport/Report.aspx?IdReport=AdjustmentT1T3", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Rekap Adjustment</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Dari Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl1" runat="server" class="form-control" Width="150px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTgl1"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl2" runat="server" class="form-control" Width="150px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTgl2"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Proses</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlProses" class="form-control" runat="server" Width="132px">
                                                <asp:ListItem Value="T0">Inventory</asp:ListItem>
                                                <asp:ListItem Value="T1">Tahap 1</asp:ListItem>
                                                <asp:ListItem Value="T3" Selected="True">Tahap 3</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btnPrint" runat="server" class="btn btn-sm btn-primary" OnClick="btnPrint_ServerClick" Text="Preview" />
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