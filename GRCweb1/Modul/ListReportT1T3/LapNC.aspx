<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapNC.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LapNC" %>

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
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LNCHandling", 900, 800);
                        }
                        function Cetak2() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LNCSortir", 900, 800);
                        }
                        function Cetak3() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LNCHandlingB", 900, 800);
                        }
                        function Cetak4() {
                            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LNCSortirB", 900, 800);
                        }
                    </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">LAPORAN NON CONFORMITY PRODUK</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <span>
                                            <asp:RadioButton ID="RB_Rekap" runat="server" Style="margin: 10px" Checked="True" GroupName="a" Text="NC Handling" />
                                        </span>
                                        <span>
                                            <asp:RadioButton ID="RB_Detail" runat="server" GroupName="a" Text="NC Sortir" />
                                        </span>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddlBulan" class="form-control" runat="server" Width="132px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
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
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddTahun" class="form-control" runat="server" Width="132px">
                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <span>
                                            <asp:RadioButton ID="RB_Harian" runat="server" Style="margin: 10px" Checked="True" GroupName="b" Text="Harian" />
                                        </span>
                                        <span>
                                            <asp:RadioButton ID="RBBulanan" runat="server" GroupName="b" Text="Bulanan" />
                                        </span>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btnPrint" runat="server" class="btn btn-sm btn-primary" OnClick="btnPrint_Click" Text="Print"
                                            Style="font-family: 'Calibri Light'; font-weight: 700" />
                                        <asp:Button ID="btnP" runat="server" class="btn btn-sm btn-info" OnClick="btnP_Click" Text="Preview"
                                            Style="font-family: 'Calibri Light'; font-weight: 700" />
                                        <asp:Button ID="btnExport" runat="server" class="btn btn-sm btn-info" OnClick="btnExport_Click" Text="Export To Excel"
                                            Style="font-family: 'Calibri Light'; font-weight: 700;" />
                                        <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <asp:Panel ID="PaneLH" runat="server" Visible="false">
                            <div class="contentlist" style="overflow: scroll; height: 400px; width: 100%;" id="div2" runat="server">
                                <%--<asp:Panel ID="PaneLH" runat="server" Visible="false" >--%>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="4" style="font-family: 'Calibri Light'; font-size: medium; font-weight: bold;">
                                            <asp:Label ID="lblbpas"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblJdl1"
                                                runat="server" Style="font-size: x-small; text-align: center;" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblJdl2"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold;">
                                            <asp:Label ID="lblperiode"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                </table>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: 'Calibri Light'; height: auto;"
                                    border="0">
                                    <thead>
                                        <tr class="tbHeader" id="hd1">
                                            <th class="kotak" style="width: 3%">NO.
                                            </th>
                                            <th class="kotak" style="width: 25%">PARTNO
                                            </th>
                                            <th class="kotak" style="width: 15%">NC HANDLING (LEMBAR)
                                            </th>
                                            <th class="kotak" style="width: 15%">NC HANDLING (M3)
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lst" runat="server" OnItemDataBound="lst_DataBound">
                                            <ItemTemplate>
                                                <tr class="Line3 baris" id="isi" runat="server">
                                                    <td class="kotak tengah">
                                                        <%# Eval("No")%>
                                                    </td>
                                                    <td class="kotak">&nbsp;<%# Eval("PartnoAkhir")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("LembarAkhir")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("KubikAkhir", "{0:N2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr class="Line3 baris" id="ftr" runat="server">
                                                    <td class="kotak bold" colspan="2">&nbsp; Total
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr class="Line3 baris" id="ftr1" runat="server">
                                            <td class="kotak bold" colspan="2">&nbsp; Total Potong OK
                                            </td>
                                            <td class="kotak angka bold">
                                                <asp:Label ID="lblLembar" runat="server" /></strong>
                                            </td>
                                            <td class="kotak angka bold">&nbsp;
                                                                    <asp:Label ID="lblKubik" runat="server" /></strong>
                                            </td>
                                        </tr>
                                        <tr class="Line3 baris" id="ftr2" runat="server">
                                            <td class="kotak bold" colspan="2">&nbsp; % NC Handling
                                            </td>
                                            <td class="kotak angka bold" colspan="2">
                                                <asp:Label ID="lblPersen" runat="server" /></strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table style="width: 60%;">
                                    <tr>
                                        <td colspan="4" style="font-family: 'Calibri Light';">
                                            <asp:Label ID="lblplant"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <%-- <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>--%>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Dibuat
                                        </td>
                                        <%-- <td>&nbsp;</td>--%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Diperiksa
                                        </td>
                                        <%-- <td>&nbsp;</td>  --%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Mengetahui
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <%--  <td>&nbsp;</td>--%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <%-- <td>&nbsp;</td>  --%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <%--</asp:Panel>--%>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PaneLS" runat="server" Visible="false">
                            <div class="contentlist" style="overflow: scroll; height: 700px; width: 100%;" id="div3"
                                runat="server">
                                <%--<asp:Panel ID="PaneLS" runat="server" Visible="false" >--%>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="6" style="font-family: 'Calibri Light'; font-size: medium; font-weight: bold;">
                                            <asp:Label ID="lblbpas01"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblJdl01"
                                                runat="server" Style="font-size: x-small; text-align: center;" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold; text-align: center;">
                                            <asp:Label ID="lblJdl02"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"
                                            style="font-family: 'Calibri Light'; font-size: small; font-weight: bold;">
                                            <asp:Label ID="lblPeriode01"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                    </tr>
                                </table>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: 'Calibri Light'; height: auto;"
                                    border="0">
                                    <thead>
                                        <tr class="tbHeader" id="Tr1">
                                            <th class="kotak" style="width: 3%" rowspan="2">NO.
                                            </th>
                                            <th class="kotak" style="width: 15%" rowspan="2">PARTNO
                                            </th>
                                            <th class="kotak" style="width: 25%" colspan="2">NC SORTIR (LEMBAR)
                                            </th>
                                            <th class="kotak" style="width: 25%" colspan="2">NC SORTIR (M3)
                                            </th>
                                        </tr>
                                        <tr class="tbHeader" id="hd2">
                                            <th class="kotak tengah" style="width: 10%">EFO
                                            </th>
                                            <th class="kotak tengah" style="width: 10%">STANDAR
                                            </th>
                                            <th class="kotak tengah" style="width: 10%">EFO
                                            </th>
                                            <th class="kotak tengah" style="width: 10%">STANDAR
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lst2" runat="server" OnItemDataBound="lst2_DataBound">
                                            <ItemTemplate>
                                                <tr class="Line3 baris" id="isi2" runat="server">
                                                    <td class="kotak tengah">
                                                        <%# Eval("No")%>
                                                    </td>
                                                    <td class="kotak">&nbsp;<%# Eval("PartnoAkhir")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("QtyAkhirSE")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("QtyAkhirSS")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("M3AkhirSE", "{0:N2}")%>
                                                    </td>
                                                    <td class="kotak angka">&nbsp;<%# Eval("M3AkhirSS", "{0:N2}")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr class="Line3 baris" id="ftr" runat="server">
                                                    <td class="kotak bold" colspan="2">&nbsp; Total
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                    <td class="kotak angka bold">&nbsp;
                                                    </td>
                                                </tr>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <tr class="Line3 baris" id="Tr2" runat="server">
                                            <td class="kotak bold" colspan="2">&nbsp; Total Potong OK
                                            </td>
                                            <td class="kotak angka bold" colspan="2">
                                                <asp:Label ID="lbl1" runat="server" />
                                                </strong>
                                            </td>
                                            <td class="kotak angka bold" colspan="2">&nbsp;
                                                                    <asp:Label ID="lbl2" runat="server" />
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr id="Tr3" runat="server" class="Line3 baris">
                                            <td class="kotak bold" colspan="2">&nbsp; % NC Sortir
                                            </td>
                                            <td class="kotak angka bold" colspan="4">
                                                <asp:Label ID="lbl3" runat="server" />
                                                </strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table style="width: 60%;">
                                    <tr>
                                        <td colspan="4" style="font-family: 'Calibri Light';">
                                            <asp:Label ID="lblplant01"
                                                runat="server" Style="font-size: x-small" /></strong></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <%-- <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>--%>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Dibuat
                                        </td>
                                        <%-- <td>&nbsp;</td>--%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Diperiksa
                                        </td>
                                        <%-- <td>&nbsp;</td>  --%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">&nbsp;Mengetahui
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <%--  <td>&nbsp;</td>--%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <%-- <td>&nbsp;</td>  --%>
                                        <td style="font-family: 'Calibri Light'; font-size: x-small; text-align: center;">(_________________)
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <%--</asp:Panel>--%>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>