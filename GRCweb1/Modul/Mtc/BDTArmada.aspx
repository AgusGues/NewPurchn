<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BDTArmada.aspx.cs" Inherits="GRCweb1.Modul.Mtc.BDTArmada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 8px 8px 8px;
            overflow-x: auto;
            min-height: .01%;
        }

        .btn {
            font-style: normal;
            border: 1px solid transparent;
            padding: 2px 4px;
            font-size: 11px;
            height: 24px;
            border-radius: 4px;
        }

        input, select, .form-control, select.form-control, select.form-group-sm .form-control {
            height: 24px;
            color: #000;
            padding: 2px 4px;
            font-size: 12px;
            border: 1px solid #d5d5d5;
            border-radius: 4px;
        }

        .table > tbody > tr > th, .table > tbody > tr > td {
            border: 0px solid #fff;
            padding: 2px 4px;
            font-size: 12px;
            color: #fff;
            font-family: sans-serif;
        }

        .contentlist {
            border: 0px solid #B0C4DE;
        }

        label {
            font-size: 12px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>BDT Armada</title>
                <script type="text/javascript">
                    function confirmation() {
                        if (confirm('Yakin mau hapus data ?')) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                </script>
                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>

                <link href="../../assets/css/jquery.datetimepicker.css" rel="stylesheet" />

            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">BREAKDOWN TIME ARMADA</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" class="btn btn-sm" onserverclick="btnNew_ServerClick"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSimpan" runat="server" class="btn btn-sm" onclick="Simpan()"
                                            type="button" value="Simpan" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnHapus" runat="server" class="btn btn-sm" type="button" value="Hapus" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel panel-body">
                                <%--<div class="col-xs-12  col-md-1">
                                    <div class="col-md-2">
                                        <label for="form-field-9" style="font-size: 12px">ID</label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtID" runat="server" class="form-control" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>--%>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 12px">Tanggal BDT</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtTglBA" runat="server"  class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglBA"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 12px">Breakdown Time</label>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    Start
                                                </div>
                                                <div class="col-md-6">
                                                    <%--<input type="text" id="txtStart" class="input-sm form-control" autocomplete="off" />--%>
                                                    <asp:TextBox ID="txtStart" runat="server"  class="form-control" onchange="durasi()" ClientIDMode="Static"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    Finish
                                                </div>
                                                <div class="col-md-6">
                                                    <%--<input type="text" id="txtFinish" class="input-sm form-control" onchange="durasi()" autocomplete="off" />--%>
                                                     <asp:TextBox ID="txtFinish" runat="server"  class="form-control" onchange="durasi()" ClientIDMode="Static"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label for="form-field-9" style="font-size: 12px">Total BD Time</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <%--<input type="text" id="txtTotal" class="input-sm form-control" autocomplete="off" />--%>
                                                    <asp:TextBox ID="txtTotal" runat="server"  class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                                <div class="col-md-3">Menit</div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 12px">Nama Unit</label>
                                        </div>
                                        <div class="col-md-10">
                                            <select id="ddlArmada" class="input-sm form-control">
                                                <option value="">Pilih Armada</option>
                                            </select>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 12px">Kendala</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtKendala" runat="server" class="form-control" ClientIDMode="Static" TextMode="SingleLine"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 12px">Perbaikan</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtPerbaikan" runat="server" ClientIDMode="Static" class="form-control" TextMode="SingleLine"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="form-field-9" style="font-size: 12px">Keterangan</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtKeterangan" runat="server" ClientIDMode="Static" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading clearfix">
                                    <h3 class="panel-title pull-left">List Transaksi Breakdown Time Armada</h3>
                                    <div class="pull-right">
                                        <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" OnTextChanged="txtdrtanggal_TextChanged" ForeColor="Black"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                                        s/d Tanggal
                                        <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" OnTextChanged="txtsdtanggal_TextChanged" ForeColor="Black"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                                          <span class="input-icon input-icon-right">
                                         <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"   Text="Export Pemantauan to Excel" />
                                    </span>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="0" id="baList" bgcolor="#999999">

                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak tengah" rowspan="2">No</th>
                                                <th class="kotak tengah" rowspan="2">Tanggal</th>
                                                <th class="kotak tengah" rowspan="2">Nama Armada</th>
                                                <th class="kotak tengah" colspan="2">Jam</th>
                                                <th class="kotak tengah" rowspan="2">Breakdown (Menit)</th>
                                                <th class="kotak tengah" rowspan="2">Kendala</th>
                                                <th class="kotak tengah" rowspan="2">Perbaikan</th>
                                                <th class="kotak tengah" rowspan="2">Keterangan</th>
                                                <th class="kotak tengah" rowspan="2"></th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak tengah">Stop</th>
                                                <th class="kotak tengah">Selesai</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_ItemDataBound" OnItemCommand="lstBA_ItemCommand" >
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris"  id="ps1" runat="server">
                                                        <td class="kotak tengah" nowrap="nowrap" style="width:2%;">  </td>
                                                        <td class="kotak tengah" style="width:5%;"> <%# Eval("Tanggal","{0: dd MMM yyyy}")%>  </td>
                                                        <td class="kotak " nowrap="nowrap" style="width:10%;"> <%# Eval("NamaUnit")%>  </td>
                                                        <td class="kotak tengah" nowrap="nowrap" style="width:7%;"> <%# Eval("TglStart","{0: dd MMM yyyy HH:mm}")%>  </td>
                                                        <td class="kotak tengah" nowrap="nowrap" style="width:5%;"> <%# Eval("TglFinish","{0: dd MMM yyyy HH:mm}")%>  </td>
                                                        <td class="kotak tengah" nowrap="nowrap" style="width:3%;"> <%# Eval("TotalTime")%>  </td>
                                                        <td class="kotak"  style="width:20%;"> <%# Eval("Kendala")%>  </td>
                                                        <td class="kotak"  style="width:20%;"> <%# Eval("Perbaikan")%>  </td>
                                                        <td class="kotak"  style="width:20%;"> <%# Eval("Keterangan")%>  </td>
                                                         <td class="kotak tengah" style="width:2%;">
                                                            <asp:ImageButton ID="Hapus" runat="server"  CssClass='<%# Eval("ID") %>' ToolTip="Click For Dellete"
                                                                CommandName="Hps" ImageUrl="~/images/Delete.png" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr class="Line3 baris" id="ftr" runat="server">
                                                        <td class="kotak bold angka " colspan="2">Total
                                                        </td>
                                                        <td class="kotak bold angka tengah" ></td>
                                                        <td class="kotak bold angka tengah" colspan="2"></td>
                                                        <td class="kotak bold angka tengah" ></td>
                                                        <td class="kotak tengah" colspan="3">&nbsp;
                                                        </td>
                                                    </tr>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <div  id="lst2" runat="server" style="display:none">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;" border="0" id="expbaList" >

                                            <thead>
                                                <tr>
                                                    <th colspan="9" class="bold tengah">PEMANTAUAN BREAKDOWN TIME ARMADA</th>
                                                </tr>
                                                <tr>
                                                    <th colspan="9" class="bold tengah">Periode :&nbsp; 
                                                         <asp:Label ID="lblTgl" runat="server" Text="Label"></asp:Label>,
                                                    </th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak tengah" rowspan="2">No</th>
                                                    <th class="kotak tengah" rowspan="2">Tanggal</th>
                                                    <th class="kotak tengah" rowspan="2">Nama Armada</th>
                                                    <th class="kotak tengah" colspan="2">Jam</th>
                                                    <th class="kotak tengah" rowspan="2">Breakdown (Menit)</th>
                                                    <th class="kotak tengah" rowspan="2">Kendala</th>
                                                    <th class="kotak tengah" rowspan="2">Perbaikan</th>
                                                    <th class="kotak tengah" rowspan="2">Keterangan</th>
                                                    <th class="kotak tengah" rowspan="2"></th>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak tengah">Stop</th>
                                                    <th class="kotak tengah">Selesai</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstExpBA" runat="server" OnItemDataBound="lstExpBA_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="ps1" runat="server">
                                                            <td class="kotak tengah" nowrap="nowrap" style="width=2%;"></td>
                                                            <td class="kotak tengah" style="width=5%;"><%# Eval("Tanggal","{0: dd MMM yyyy}")%>  </td>
                                                            <td class="kotak " nowrap="nowrap" style="width=10%;"><%# Eval("NamaUnit")%>  </td>
                                                            <td class="kotak tengah" nowrap="nowrap" style="width=10%;"><%# Eval("TglStart","{0: dd MMM yyyy HH:mm}")%>  </td>
                                                            <td class="kotak tengah" nowrap="nowrap" style="width=3%;"><%# Eval("TglFinish","{0: dd MMM yyyy HH:mm}")%>  </td>
                                                            <td class="kotak tengah" nowrap="nowrap" style="width=18%;"><%# Eval("TotalTime")%>  </td>
                                                            <td class="kotak" style="width=20%;"><%# Eval("Kendala")%>  </td>
                                                            <td class="kotak" style="width=20%;"><%# Eval("Perbaikan")%>  </td>
                                                            <td class="kotak" style="width=2%;"><%# Eval("Keterangan")%>  </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr class="Line3 baris" id="ftr" runat="server">
                                                            <td class="kotak bold angka " colspan="2">Total
                                                        </td>
                                                        <td class="kotak bold angka tengah" ></td>
                                                        <td class="kotak bold angka tengah" colspan="2"></td>
                                                        <td class="kotak bold angka tengah" ></td>
                                                        <td class="kotak tengah" colspan="3">&nbsp;
                                                        </td>
                                                        </tr>

                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td class="tengah" colspan="3"></td>
                                                    <td colspan="4">&nbsp;</td>
                                                    <td colspan="2">
                                                        <asp:Label ID="LblPlant" runat="server" Text="Label"></asp:Label>,
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tengah" colspan="3">Mengetahui,</td>
                                                    <td colspan="4">&nbsp;</td>
                                                    <td class="tengah" colspan="2">Dibuat oleh,</td>
                                                </tr>
                                                <tr>
                                                    <td class="tengah" colspan="3">&nbsp;</td>
                                                    <td colspan="4">&nbsp;</td>
                                                    <td class="tengah" colspan="2">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="tengah" colspan="3">(Maintenance Dept.)</td>
                                                    <td colspan="4">&nbsp;</td>
                                                    <td class="tengah" colspan="2">(Armada Dept.)</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </body>
            <script src="../../assets/js/jquery-ui.min.js"></script>
            <script src="../../assets/js/jquery.datetimepicker.full.js"></script>
            <script src="../../Scripts/Maintenance/BreakArmada.js" type="text/javascript"></script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
