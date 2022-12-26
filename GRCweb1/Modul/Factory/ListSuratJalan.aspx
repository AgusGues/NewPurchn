<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSuratJalan.aspx.cs" Inherits="GRCweb1.Modul.Factory.ListSuratJalan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">DAFTAR SURAT JALAN</h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" class="btn btn-sm btn-info" type="button" value="Form Surat Jalan" onserverclick="btnNew_ServerClick" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" runat="server" class="form-control" Width="120px">
                                            <asp:ListItem Value="SuratJalanNo">No Surat Jalan</asp:ListItem>
                                            <asp:ListItem Value="ScheduleNo">No Schedule</asp:ListItem>
                                            <asp:ListItem Value="OPNo">No OP</asp:ListItem>
                                            <asp:ListItem Value="Status">Status</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSearch" class="btn btn-sm btn-info" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Per Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" class="form-control" Width="150"
                                                AutoPostBack="True"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromPostingPeriod"
                                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" AutoPostBack="True" class="form-control" Width="150"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToPostingPeriod"
                                                Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <input id="btnPrint" class="btn btn-sm btn-primary" runat="server" onserverclick="btnPrint_ServerClick" type="button"
                                            value="Preview" />
                                        <input id="Button1" class="btn btn-sm btn-success" runat="server" onserverclick="btnExp_ServerClick" type="button"
                                            value="Export To Excel" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div id="div2" style="overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Height="100%">
                                        <Columns>
                                            <asp:BoundField DataField="SuratJalanNo" HeaderText="No Surat Jalan" />
                                            <asp:BoundField DataField="CreatedTime" HeaderText="Tanggal" />
                                            <asp:BoundField DataField="OPNo" HeaderText="No OP" />
                                            <asp:BoundField DataField="ScheduleNo" HeaderText="No Schedule" />
                                            <asp:BoundField DataField="PoliceCarNo" HeaderText="No Mobil" />
                                            <asp:BoundField DataField="DriverName" HeaderText="Nama Sopir" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Dibuat oleh" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                        </Columns>
                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <asp:Panel ID="Panel1" runat="server" Font-Size="X-Small" Visible="False">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                            Height="100%" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="SuratJalanNo" HeaderText="No Surat Jalan" />
                                                <asp:BoundField DataField="CreatedTime" HeaderText="Tanggal" />
                                                <asp:BoundField DataField="OPNo" HeaderText="No OP" />
                                                <asp:BoundField DataField="ScheduleNo" HeaderText="No Schedule" />
                                                <asp:BoundField DataField="PoliceCarNo" HeaderText="No Mobil" />
                                                <asp:BoundField DataField="DriverName" HeaderText="Nama Sopir" />
                                                <asp:BoundField DataField="CreatedBy" HeaderText="Dibuat oleh" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>