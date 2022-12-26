<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputSuratJalanToCpd.aspx.cs" Inherits="GRCweb1.Modul.Factory.InputSuratJalanToCpd" %>

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
                            MyPopUpWin("../Report/Report.aspx?IdReport=SuratJalanTO", 900, 800);
                        }

                        function CetakLgsg() {

                            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SuratJalanTO", 900, 800);
                        }

                        function onCancel() {

                        }
                        function confirm_turunTO() {
                            if (confirm("Anda yakin ingin menurunkan Status Surat Jalan TO?") == true)
                                window.showModalDialog('../ModalDialog/ReasonCancel.aspx?judul=TurunStatusTO', 900, 800);
                            else
                                return false;
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">SURAT JALAN OP</h3>
                            </div>
                            <div class="panel-body">
                                <span>
                                    <input id="btnNew" runat="server" class="btn btn-sm btn-success" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Baru"
                                        onserverclick="btnNew_ServerClick" />
                                </span>
                                <span>
                                    <input id="btnSimpan" runat="server" class="btn btn-sm btn-success" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Simpan"
                                        onserverclick="btnSimpan_ServerClick" />
                                </span>
                                <span>
                                    <input id="btnPostingReceipt" class="btn btn-sm btn-primary" runat="server" onserverclick="btnPostingReceipt_ServerClick"
                                        style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                        value="Posting Receipt" visible="True" />
                                </span>
                                <span>
                                    <asp:Button ID="btnCancel" class="btn btn-sm btn-danger" runat="server" Style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                        Text="Cancel SJ"
                                        OnClick="btnCancel_ServerClick" />
                                </span>
                                <span>
                                    <input id="btnPrintLgsg" runat="server" class="btn btn-sm btn-primary" onserverclick="CetakServerClick" style="background-color: White; font-weight: bold; font-size: 11px;"
                                        type="button" value="Cetak SJ" />
                                </span>
                                <span>
                                    <input id="btnList" class="btn btn-sm btn-info" runat="server" style="background-color: White; font-weight: bold; font-size: 11px;"
                                        type="button" value="List SJ" onserverclick="btnList_ServerClick" visible="True" />
                                </span>
                                <span>
                                    <input id="btnListOP" runat="server" class="btn btn-sm btn-info" style="background-color: White; font-weight: bold; font-size: 11px;"
                                        type="button" value="List OP" onserverclick="btnListTO_ServerClick" visible="True" />
                                </span>
                                <span>
                                    <asp:Button ID="btnTurunStatus" class="btn btn-sm btn-warning" runat="server" Style="background-color: White; font-weight: bold; font-size: 11px;"
                                        Text="Turun Status SJ" OnClick="btnTurunStatus_ServerClick" />
                                </span>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" Style="background-color: white; font-weight: bold; font-size: 11px; margin-left: 0px;">
                                            <asp:ListItem Value="SuratJalanNo">No SJ</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" Width="165px"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSearch" runat="server"  class="btn btn-sm btn-primary" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Search"
                                            onserverclick="btnSearch_ServerClick" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Surat Jalan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSuratJalanNo" class="form-control" runat="server" Width="233"
                                                ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No TO</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTransferOrderNo" class="form-control" runat="server"  Width="233"
                                                AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Schedule</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtScheduleNo" class="form-control" runat="server" AutoPostBack="True"
                                                Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Mobil</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtNoMobil" class="form-control" runat="server" Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Nama Sopir</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtDriverName" runat="server" class="form-control" Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tgl Create</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCreateDate" runat="server" class="form-control" Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">User</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCreatedBy" runat="server" class="form-control" Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Dari Alamat </label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtFromAddress" runat="server" class="form-control" Height="70px"
                                                Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Ke Alamat</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtToAddress" runat="server" class="form-control" Height="70px"
                                                Width="233"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                        <asp:BoundField DataField="Qty" HeaderText="Jumlah" />
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
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