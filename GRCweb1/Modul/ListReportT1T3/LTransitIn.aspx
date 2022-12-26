<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LTransitIn.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LTransitIn1" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LTransitIn", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN TRANSIT IN</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-3">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label for="form-field-9" style="font-size: 14px"></label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label for="form-field-9" style="font-size: 14px">Per Tanggal</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-3">
                                    <div class="row">
                                        <span>
                                            <asp:RadioButton ID="RBTglProduksi" runat="server" Checked="True" Font-Size="X-Small"
                                                GroupName="a" Text="Tgl. Produksi" Visible="False" />
                                            <asp:RadioButton ID="RBTglSerah" runat="server" Font-Size="X-Small" GroupName="a"
                                                Text="Tgl. Serah" Visible="False" />
                                            <asp:CheckBox ID="ChkTglProduksi" runat="server" AutoPostBack="True" Checked="True"
                                                OnCheckedChanged="ChkTglProduksi_CheckedChanged" Text="Tgl. Produksi" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="233"
                                                AutoPostBack="True" OnTextChanged="txtFromPostingPeriod_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromPostingPeriod"
                                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"
                                                AutoPostBack="True"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtToPostingPeriod"
                                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtPartno" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Height="20px" Width="208px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtPartno_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetNoProduk" ServicePath="AutoComplete.asmx" TargetControlID="txtPartno">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-3">
                                    <div class="row">
                                        <span>
                                            <asp:CheckBox ID="ChkTglSerah" runat="server" AutoPostBack="True" OnCheckedChanged="ChkTglSerah_CheckedChanged"
                                                Text="Tgl. Serah" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFromPostingPeriod0" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                OnTextChanged="txtFromPostingPeriod0_TextChanged" Visible="False" Width="233"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromPostingPeriod0_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFromPostingPeriod0"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtToPostingPeriod0" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Visible="False" Width="233"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtToPostingPeriod0_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtToPostingPeriod0"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-3">
                                    <div class="row">
                                        <span>
                                            <asp:CheckBox ID="ChkTglInput" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="ChkTglInput_CheckedChanged" Text="Tgl. Transaksi" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtFromPostingPeriod1" runat="server" AutoPostBack="True"
                                                BorderStyle="Groove" OnTextChanged="txtFromPostingPeriod0_TextChanged"
                                                Visible="False" Width="233"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromPostingPeriod1_CalendarExtender"
                                                runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFromPostingPeriod1"></cc1:CalendarExtender>

                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtToPostingPeriod1" runat="server" AutoPostBack="True"
                                                BorderStyle="Groove" Visible="False" Width="233"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtToPostingPeriod1_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtToPostingPeriod1"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12">
                                    <div class="row">
                                        <input id="btnPrint" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint_ServerClick" type="button" value="Cetak" />
                                        <input id="btnPrint0" runat="server" class="btn btn-sm btn-info" onserverclick="btnPrint0_ServerClick"
                                            type="button" value="Preview" visible="False" />
                                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Visible="False">Export
                                                To Excel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:GridView ID="GrdDynamic" runat="server" ScrollBars="Vertical" AutoGenerateColumns="False"
                                    PageSize="15" Width="100%">
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                        ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>