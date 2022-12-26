<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPengirimanPotongStok.aspx.cs" Inherits="GRCweb1.Modul.Factory.ListPengirimanPotongStok" %>

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
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Pengiriman Belum Potong Stok</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Dari Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl1" runat="server" class="form-control" Width="150px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTgl1" Enabled="True"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtTgl2" runat="server" class="form-control" Width="150px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTgl2" Enabled="True"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12">
                                    <div class="row">
                                        <asp:Button ID="btnPreview" class="btn btn-sm btn-primary" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="contentlist" style="height: 450px" id="lst" runat="server">
                            <table style="width: 100%; border-collapse: collapse; font-size: medium" border="0"
                                id="baList">
                                <thead>
                                    <tr class="tbHeader">
                                        <th style="width: 5%" class="kotak">No.
                                        </th>
                                        <th style="width: 30%" class="kotak">Schedule No
                                        </th>
                                        <th style="width: 10%" class="kotak">Surat Jalan 
                                        </th>
                                        <th style="width: 15%" class="kotak">Tanggal Kirim
                                        </th>
                                        <th style="width: 15%" class="kotak">Item
                                        </th>
                                        <th style="width: 15%" class="kotak">Qty
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstSch" runat="server" OnItemDataBound="lstSch_DataBound">
                                        <ItemTemplate>
                                            <tr class="OddRows baris">
                                                <td class="kotak" nowrap="nowrap" style="width: 5%; font-size: small; font-weight: bolder;">
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <td class="kotak" nowrap="nowrap" style="width: 30%; font-size: small; font-weight: bolder;">
                                                    <%# Eval("ScheduleNo")%>
                                                </td>
                                                <td class="kotak" nowrap="nowrap" style="width: 10%; font-size: small; font-weight: bolder;">
                                                    <%# Eval("SuratJalanNo")%>
                                                </td>

                                                <td class="kotak" nowrap="nowrap" style="width: 15%; font-size: small; font-weight: bolder;">
                                                    <%# Eval("Scheduledate")%>
                                                </td>
                                                <td class="kotak" nowrap="nowrap" style="width: 15%; font-size: small; font-weight: bolder;">
                                                    <%# Eval("Item")%>
                                                </td>
                                                <td class="kotak" nowrap="nowrap" style="width: 15%; font-size: small; font-weight: bolder;">
                                                    <%# Eval("Qty")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>