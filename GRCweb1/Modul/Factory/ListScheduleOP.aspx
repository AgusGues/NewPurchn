<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListScheduleOP.aspx.cs" Inherits="GRCweb1.Modul.Factory.ListScheduleOP" %>

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
                                <h2 class="panel-title pull-left">DAFTAR SCHEDULE OP</h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" class="form-control" runat="server" Width="120px">
                                            <asp:ListItem Value="OpNo">No OP</asp:ListItem>
                                            <asp:ListItem Value="ScheduleNo">Schedule No</asp:ListItem>
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

                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div id="div2" style="width: 100%; height: 400px; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                        OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ScheduleNo" HeaderText="No Schedule" />
                                            <asp:BoundField DataField="ScheduleDate" HeaderText="Tgl Schedule" />
                                            <asp:BoundField DataField="OPNo" HeaderText="No OP" />
                                            <asp:BoundField DataField="OPDate" HeaderText="Tgl OP" />
                                            <asp:BoundField DataField="Address" HeaderText="Alamat" />
                                            <asp:BoundField DataField="AlamatLain" HeaderText="Alamat Lain" />
                                            <asp:BoundField DataField="CustomerType" HeaderText="Tipe Customer" />
                                            <%--<asp:BoundField DataField="CustomerID" HeaderText="Nama Customer" /> --%>
                                            <asp:BoundField DataField="TokoCustName" HeaderText="Nama Customer" />
                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>