<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSaldoLokasi.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LSaldoLokasi" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LSaldoLokasi", 900, 800);
                        }
                    </script>
                    <style>
        .AutoExtender {
            font-family: Verdana,Helvetica,sans-serif;
            font-size: 1em;
            font-weight: lighter;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 0px;
            width: 45px;
            display: block;
            background-color: White;
            border-radius: 0.25rem;
            position: absolute;
        }

        .AutoExtenderList {
            display: block;
            elevation: higher;
            border-bottom: solid 1px #006699;
            cursor: pointer;
            color: black;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
    </style>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN SALDO LOKASI</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                            <asp:RadioButton ID="RBAllPart" runat="server" Checked="True" GroupName="a" Text="Semua Part"
                                                AutoPostBack="True" OnCheckedChanged="RBAllPart_CheckedChanged" />
                                        </div>
                                        <asp:Panel ID="Panel3" runat="server" BackColor="#99CCFF">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:RadioButton ID="RbA1" runat="server" Checked="True" GroupName="1" Text="Semua Lokasi"
                                                        AutoPostBack="True" OnCheckedChanged="RbA1_CheckedChanged" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:RadioButton ID="RbA2" runat="server" GroupName="1" Text="Satu Lokasi" AutoPostBack="True"
                                                        OnCheckedChanged="RbA2_CheckedChanged" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtLokasi2" class="form-control" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        Width="93px" Visible="False"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="200"
                                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetLokasiStock" ServicePath="../Factory/AutoComplete.asmx" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtLokasi2">
                                                        </cc1:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="pull-right">
                                                        <input id="btnPrint1" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint1_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); margin: 5px; background-color: white; font-weight: bold; font-size: 11px;"
                                                            type="button"
                                                            value="Preview" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                            <asp:RadioButton ID="RBSatuPart" runat="server" GroupName="a" Text="Satu Part" AutoPostBack="True"
                                                OnCheckedChanged="RBSatuPart_CheckedChanged" />
                                        </div>
                                        <asp:Panel ID="Panel4" runat="server" BackColor="#99CCFF" Visible="False">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <label for="form-field-9" style="font-size: 14px">Partno</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPartnoA" runat="server" class="form-control" Width="182px"
                                                            OnTextChanged="txtPartnoA_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetNoProduk" ServicePath="../Factory/AutoComplete.asmx" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtPartnoA">
                                                        </cc1:AutoCompleteExtender>
                                                        <div style="padding: 2px"></div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtLokasi1" runat="server" AutoPostBack="True" class="form-control" Width="88px" OnPreRender="txtLokasi1_PreRender" OnTextChanged="txtLokasi1_TextChanged"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1" 
                                                            ServiceMethod="GetLokasiStock" ServicePath="../Factory/AutoComplete.asmx" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtLokasi1">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="pull-right">
                                                        <input id="btnPrint0" runat="server" class="btn btn-sm btn-primary" onserverclick="btnPrint0_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); margin: 5px; background-color: white; font-weight: bold; font-size: 11px;"
                                                            type="button"
                                                            value="Preview" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                            <asp:RadioButton ID="RBLokasi" runat="server" GroupName="a" Text="Semua Lokasi" AutoPostBack="True"
                                                OnCheckedChanged="RBLokasi_CheckedChanged" />
                                        </div>
                                        <asp:Panel ID="Panel5" runat="server" BackColor="#99CCFF" Visible="False">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <label for="form-field-9" style="font-size: 14px">Partno</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPartnoA0" runat="server" AutoPostBack="True" class="form-control" Width="182px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtPartnoA0_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetNoProduk" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServicePath="../Factory/AutoComplete.asmx" TargetControlID="txtPartnoA0">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="pull-right">
                                                        <input id="btnPrint" runat="server" align="right" class="btn btn-sm btn-primary" onserverclick="btnPrint_ServerClick"
                                                            style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); margin: 5px; background-color: white; font-weight: bold; font-size: 11px;"
                                                            type="button"
                                                            value="Preview" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                            <asp:RadioButton ID="RBrpt1" runat="server" Checked="True" GroupName="rpt" Text="Report By Lokasi" />
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <div class="row">
                                            <asp:RadioButton ID="RBrpt2" runat="server" GroupName="rpt" Text="Report ALL" />
                                        </div>
                                    </div>
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