<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LKirim.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LKirim" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LKirim", 900, 800)
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN PENGIRIMAN</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-10">
                                    <div class="row">
                                        <span>
                                            <asp:RadioButton ID="RBTglProduksi" runat="server" Style="margin: 10px" Checked="True" Font-Size="X-Small"
                                                GroupName="a" Text="Tgl. Transaksi" />
                                        </span>
                                        <span>
                                            <asp:RadioButton ID="RBTglSerah" runat="server" Style="margin: 10px" Font-Size="X-Small" GroupName="a"
                                                Text="Tgl. Kirim" />
                                        </span>
                                        <span>
                                            <asp:CheckBox ID="ChkSJ" runat="server" Checked="True" Font-Size="X-Small"
                                                Text="Tanpa Informasi Schedule Surat Jalan" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Per Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" class="form-control" Width="150"
                                                AutoPostBack="True"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server">
                                                </cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" class="form-control" Width="150"
                                                AutoPostBack="True"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod"
                                                    Format="dd-MMM-yyyy" runat="server">
                                                </cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">PartNo</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtPartno" runat="server" AutoPostBack="True" class="form-control" Width="208px"></asp:TextBox>
                                            <cc1:autocompleteextender id="txtPartno_AutoCompleteExtender" runat="server" completioninterval="200"
                                                completionsetcount="10" enablecaching="true" firstrowselected="True" minimumprefixlength="1"
                                                servicemethod="GetNoProdukStock" servicepath="AutoComplete.asmx" targetcontrolid="txtPartno">
                                            </cc1:autocompleteextender>
                                            <cc1:maskededitextender id="MaskedEditExtender1" runat="server" acceptnegative="None"
                                                autocomplete="False" clearmaskonlostfocus="false" displaymoney="None" errortooltipenabled="True"
                                                filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" inputdirection="RightToLeft"
                                                mask="AAA-A-99999999999AAA" masktype="None" messagevalidatortip="False" promptcharacter=" "
                                                targetcontrolid="txtPartno">
                                            </cc1:maskededitextender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12">
                                    <div class="row">
                                        <input id="btnPrint" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint_ServerClick" type="button" value="Cetak" />
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