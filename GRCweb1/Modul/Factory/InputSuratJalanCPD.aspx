<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputSuratJalanCPD.aspx.cs" Inherits="GRCweb1.Modul.Factory.InputSuratJalanCPD" %>

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
                            MyPopUpWin("../Report/Report.aspx?IdReport=SuratJalan", 900, 800);
                        }

                        function CetakLgsg() {
                            MyPopUpWin("../Report/Report.aspx?IdReport=SuratJalan", 900, 800);
                        }
                        function CetakKwitansi() {
                            MyPopUpWin("../Report/Report4.aspx?IdReport=CetakKwitansi", 900, 800);
                        }

                        function onCancel() {
                        }

                        function openWindow() {
                            MyPopUpWin("../ModalDialog/ExpedisiDetail.aspx", 900, 800);
                        }
                        function confirm_delete() {
                            if (confirm("Anda yakin untuk Cancel Surat Jalan?") == true)
                                MyPopUpWin('../ModalDialog/ReasonCancel.aspx?judul=AlasanCancel', 900, 800);
                            else
                                return false;
                        }
                        function confirm_turun() {
                            if (confirm("Anda yakin untuk Turunin Status Surat Jalan?") == true)
                                MyPopUpWin('../ModalDialog/ReasonCancel.aspx?judul=TurunStatus', 900, 800);
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
                                    <input id="btnSave" runat="server" class="btn btn-sm btn-success" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Simpan" onserverclick="btnSave_ServerClick" />
                                </span>
                                <span>
                                    <input id="btnPostingReceipt" runat="server" class="btn btn-sm btn-success" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Posting Receipt" visible="True" onserverclick="btnPostingReceipt_ServerClick" />
                                </span>
                                <span>
                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-sm btn-danger" Style="background-color: White; font-weight: bold; font-size: 11px;"
                                        Text="Cancel SJ" OnClick="btnCancel_ServerClick" />
                                </span>
                                <span>
                                    <asp:Button ID="btnCancelReceive" runat="server" class="btn btn-sm btn-danger" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                        Text="Cancel Receipt" OnClick="btnCancelReceive_ServerClick" />
                                </span>
                                <span>
                                    <input id="btnPrintLgsg" class="btn btn-sm btn-primary" onserverclick="CetakServerClick" runat="server" style="background-color: White; font-weight: bold; font-size: 11px;"
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
                                        <input id="btnSearch" runat="server" class="btn btn-sm btn-primary" onserverclick="btnSearch_ServerClick" style="background-color: White; font-weight: bold; font-size: 11px;" type="button" value="Cari" /><input id="btnAutoSJ" runat="server" style="background-color: White; font-weight: bold; font-size: xx-small;" type="button" value="Auto SJ" visible="False" onserverclick="btnAutoSJ_ServerClick" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-8">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Surat Jalan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSuratJalanNo" runat="server" class="form-control" Width="233"
                                                ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No OP</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSalesOrderNo" runat="server" class="form-control" Width="233"
                                                AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Schedule</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtScheduleNo" runat="server" AutoPostBack="True" class="form-control"
                                                Width="233" ReadOnly="True"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">No Mobil</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtNoMobil" runat="server" class="form-control" Width="233"></asp:TextBox>
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
                                            <label for="form-field-9" style="font-size: 14px">Alamat</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtAlamat" runat="server" class="form-control" Height="50px" Width="100%"
                                                ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Keterangan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtKeterangan" runat="server" class="form-control" Height="41px"
                                                Width="100%" TextMode="MultiLine"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tgl Kirim Actual</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtActualKirim" runat="server" class="form-control" Width="233"
                                                AutoPostBack="True" OnTextChanged="txtActualKirim_TextChanged"></asp:TextBox>
                                            <asp:TextBox ID="txtScheduleDate" runat="server" class="form-control" OnTextChanged="txtActualKirim_TextChanged"
                                                Width="150px" Visible="False"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtScheduleDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtScheduleDate"></cc1:CalendarExtender>
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

                                    <div class="row">
                                        <asp:LinkButton ID="lbUpdateTglAktualKirim" runat="server" Font-Size="10pt" Font-Italic="true"
                                            OnClick="lbUpdateTglAktualKirim_Click">Update Tgl Kirim Actual</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">

                                        <span>
                                            <asp:RadioButton ID="RBAntrian" Style="margin: 5px" runat="server" AutoPostBack="True" Checked="True"
                                                Font-Size="X-Small" GroupName="A" OnCheckedChanged="RBAntrian_CheckedChanged" />
                                            <label for="form-field-9" style="font-size: 14px">Sort By No. Antrian</label>
                                        </span>
                                        <span>
                                            <asp:RadioButton ID="RBSchedule" Style="margin: 5px" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                GroupName="A" OnCheckedChanged="RBSchedule_CheckedChanged" />
                                            <label for="form-field-9" style="font-size: 14px">Sort By No. Schedule</label>
                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="panel panel-primary">
                                            <div class="panel-body">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtActualKirim" Format="dd-MMM-yyyy"
                                                    runat="server"></cc1:CalendarExtender>
                                                <asp:GridView ID="GridAntrian" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowCommand="GridAntrian_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="noantrian" HeaderText="No. Antrian" />
                                                        <asp:BoundField DataField="NoSchedule" HeaderText="No. Schedule" />
                                                        <asp:BoundField DataField="NoOP" HeaderText="No. OP" />
                                                        <asp:BoundField DataField="suratjalanNo" HeaderText="No.Surat Jalan" />
                                                        <asp:BoundField DataField="ExpedisiName" HeaderText="Expedisi" />
                                                        <asp:ButtonField CommandName="pilih" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <asp:LinkButton ID="lbUpdateTglAktualKirim0" runat="server" Font-Size="10pt" Font-Italic="true"
                                            OnClick="lbUpdateTglAktualKirim0_Click">Refresh List Antrian Loading</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
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