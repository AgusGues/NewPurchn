<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T3_BA_Approval.aspx.cs" Inherits="GRCweb1.Modul.Factory.T3_BA_Approval" %>

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
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">APPROVAL BERITA ACARA TAHAP 3</h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSebelumnya" runat="server" class="btn btn-sm btn-info" onserverclick="btnSebelumnya_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                            value="Sebelumnya" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnUpdate" runat="server" class="btn btn-sm btn-info" onserverclick="btnUpdate_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Approve" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSelanjutnya" runat="server" class="btn btn-sm btn-info" onserverclick="btnSesudahnya_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                            value="Selanjutnya" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-12">
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">No. Berita Acara</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtNoBA" class="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Tgl. Berita Acara</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtTglBA" class="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTglBA"></cc1:CalendarExtender>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9">Type Berita Acara</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlKeterangan" class="form-control" runat="server" Enabled="False">
                                            <asp:ListItem>PERMINTAAN PRODUK</asp:ListItem>
                                            <asp:ListItem>PRODUK MASUK</asp:ListItem>
                                            <asp:ListItem>KONSENSI</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-9">
                                    <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                            <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                                <div class="col-xs-12 col-sm-3">
                                    <label for="form-field-9">List Lampiran</label>
                                    <div class="row">
                                        <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td>
                                                        <%# Eval("FileName") %>
                                                    </td>
                                                    <td align="right" width="25%">
                                                        <asp:ImageButton ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                            CommandName="pre" CssClass='<%# Eval("ID") %>' ImageUrl="~/img/Logo_Download.png"
                                                            ToolTip="Click to Preview Document" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
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