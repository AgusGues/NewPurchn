<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LStockTransitT1.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LStockTransitT11" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LSaldoTransit_T1", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN STOCK LOKASI TRANSIT TAHAP1</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-12">
                                    <asp:Panel ID="Panel3" runat="server" Width="280px" Visible="false">
                                        <div class="row">

                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">Per Tanggal</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtFromPostingPeriod" runat="server" AutoPostBack="True" class="form-control"
                                                    Width="200px" OnTextChanged="txtFromPostingPeriod_TextChanged1"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtFromPostingPeriod"></cc1:CalendarExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtToPostingPeriod" runat="server" AutoPostBack="True" class="form-control"
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtToPostingPeriod"></cc1:CalendarExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtNoProduksi" runat="server" AutoPostBack="True" class="form-control"
                                                    OnTextChanged="txtNoProduksi_TextChanged" Width="155px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtNoProduksi_AutoCompleteExtender" runat="server"
                                                    BehaviorID="AutoCompleteEx0" CompletionInterval="200" CompletionSetCount="20"
                                                    EnableCaching="true" Enabled="True" FirstRowSelected="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                    TargetControlID="txtNoProduksi">
                                                </cc1:AutoCompleteExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtLokasi" runat="server" AutoPostBack="True" class="form-control" Width="72px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtLokasiAutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx1"
                                                    CompletionInterval="500" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiBM" ServicePath="AutoComplete.asmx"
                                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtLokasi">
                                                </cc1:AutoCompleteExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel4" runat="server" Visible="False" Width="280px">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlBulan" runat="server" class="form-control" Width="200px">
                                                    <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                    <asp:ListItem>Januari</asp:ListItem>
                                                    <asp:ListItem>Februari</asp:ListItem>
                                                    <asp:ListItem>Maret</asp:ListItem>
                                                    <asp:ListItem>April</asp:ListItem>
                                                    <asp:ListItem>Mei</asp:ListItem>
                                                    <asp:ListItem>Juni</asp:ListItem>
                                                    <asp:ListItem>Juli</asp:ListItem>
                                                    <asp:ListItem>Agustus</asp:ListItem>
                                                    <asp:ListItem>September</asp:ListItem>
                                                    <asp:ListItem>Oktober</asp:ListItem>
                                                    <asp:ListItem>November</asp:ListItem>
                                                    <asp:ListItem>Desember</asp:ListItem>
                                                </asp:DropDownList>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtTahun" runat="server" class="form-control" Width="84px"></asp:TextBox>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtNoProduksi0" runat="server" AutoPostBack="True" class="form-control"
                                                    OnTextChanged="txtNoProduksi_TextChanged" Width="155px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtNoProduksi0_AutoCompleteExtender" runat="server"
                                                    BehaviorID="AutoCompleteEx0" CompletionInterval="200" CompletionSetCount="20"
                                                    EnableCaching="true" Enabled="True" FirstRowSelected="True" MinimumPrefixLength="1"
                                                    ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                    TargetControlID="txtNoProduksi0">
                                                </cc1:AutoCompleteExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtLokasi0" runat="server" AutoPostBack="True" class="form-control" Width="72px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="txtLokasi0_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx1"
                                                    CompletionInterval="500" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiBM" ServicePath="AutoComplete.asmx"
                                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtLokasi0">
                                                </cc1:AutoCompleteExtender>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <div class="row">
                                        <input id="btnPrint" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Preview" />
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
