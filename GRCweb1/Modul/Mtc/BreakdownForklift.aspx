<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BreakdownForklift.aspx.cs" Inherits="GRCweb1.Modul.Mtc.BreakdownForklift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Simetris</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>

        <link href="../../assets/css/jquery.datetimepicker.css" rel="stylesheet" />

    </head>
    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary" id="panelinput" runat="server">
                    <div class="panel-heading">
                        BREAKDOWN FORKLIFT
                         <div class="pull-right">
                             <div class="col-md-12">
                                 <%--<button class="btn-xs btn-success" type="button" onclick="Baru()">Baru</button>--%>
                                 <asp:Button ID="btnnew" runat="server" CssClass="btn-xs btn-success" Text="Baru" OnClick="btnnew_Click" />
                                 &nbsp
                                <button class="btn-xs btn-success" type="button" onclick="Simpan()">Simpan</button>
                                 &nbsp
                                <%--<button class="btn-xs btn-success" type="button" onclick="List()">Rekap</button>--%>
                                 <asp:Button ID="btnrekap" runat="server" CssClass="btn-xs btn-success" Text="Rekap" OnClick="btnrekap_Click" />
                                 &nbsp
                                 <%--<button class="btn-xs btn-success" type="button" onclick="report()" id="btnreport">Input Operasional</button>--%>
                                 <asp:Button ID="btnprodinput" runat="server" CssClass="btn-xs btn-success" Text="Input Operasional" OnClick="btnprodinput_Click" />
                                 &nbsp
                             </div>
                         </div>
                    </div>
                    <div class="panel-body" id="input">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-4">Tanggal</div>
                                            <div class="col-md-8">
                                                <%--<input type="text" id="txtTanggal" class="input-sm form-control" data-date-format="dd-mm-yyyy" autocomplete="off" />--%>
                                                <asp:TextBox ID="txtTanggal" runat="server" CssClass="input-sm form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender
                                                    ID="CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttanggal"
                                                    Enabled="True"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div style="padding: 3px"></div>

                                        <div class="row">
                                            <div class="col-md-4">Start</div>
                                            <div class="col-md-8">
                                                <%--<input type="text" id="txtStart" class="input-sm form-control" autocomplete="off" />--%>
                                                <asp:TextBox ID="txtStart" runat="server" ClientIDMode="Static" CssClass="input-sm form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="padding: 3px"></div>

                                        <div class="row">
                                            <div class="col-md-4">Finish</div>
                                            <div class="col-md-8">
                                                <%--<input type="text" id="txtFinish" class="input-sm form-control" onchange="durasi()" autocomplete="off" />--%>
                                                <asp:TextBox ID="txtFinish" runat="server" ClientIDMode="Static" CssClass="input-sm form-control" onchange="durasi()" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="padding: 3px"></div>
                                        <div class="row">
                                            <div class="col-md-4">Kendala</div>
                                            <div class="col-md-8">
                                                <textarea id="txtkendala" class="autosize-transition form-control" style="overflow: hidden; overflow-wrap: break-word; resize: horizontal; height: 132px;" autocomplete="off"></textarea>
                                            </div>
                                        </div>
                                        <div style="padding: 3px"></div>
                                        <div class="row">
                                            <div class="col-md-4">Keterangan</div>
                                            <div class="col-md-8">
                                                <textarea id="txtketerangan" class="autosize-transition form-control" style="overflow: hidden; overflow-wrap: break-word; resize: horizontal; height: 132px;" autocomplete="off"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-4">Nama Unit</div>
                                            <div class="col-md-8">
                                                <select id="ddlforklift" class="input-sm form-control">
                                                    <option value="">Pilih Forklift</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div style="padding: 3px"></div>
                                        <div class="row">
                                            <div class="col-md-4">Total BD Time</div>
                                            <div class="col-md-8">
                                                <%--<input type="text" id="txtTotal" class="input-sm form-control" autocomplete="off" />--%>
                                                <asp:TextBox ID="txtTotal" runat="server" ClientIDMode="Static" CssClass="input-sm form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div style="padding: 21px"></div>
                                        <div class="row">
                                            <div class="col-md-4">Perbaikan</div>
                                            <div class="col-md-8">
                                                <textarea id="txtperbaikan" class="autosize-transition form-control" style="overflow: hidden; overflow-wrap: break-word; resize: horizontal; height: 132px;" autocomplete="off"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary" id="panelOperasional" runat="server">
                    <div class="panel-heading">
                        PRODUKTIVITAS FORKLIFT
                        <div class="pull-right">
                            <div class="col-md-12">
                                <asp:Button ID="btnnewprod" runat="server" Text="New" CssClass="btn-xs btn-success" OnClick="btnnewprod_Click" />
                                &nbsp
                                <asp:Button ID="btnsaveprod" runat="server" Text="Simpan" CssClass="btn-xs btn-success" OnClick="btnsaveprod_Click" />
                                &nbsp
                                <asp:Button ID="btninput" runat="server" Text="Input BD" CssClass="btn-xs btn-success" OnClick="btninput_Click" />
                                &nbsp
                                <asp:Button ID="btnrekap1" runat="server" Text="Rekap" CssClass="btn-xs btn-success" OnClick="btnrekap1_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-3">Tanggal</div>
                                    <div class="col-md-3">
                                        <%--<asp:TextBox id="txtTgl" class="input-sm form-control date-picker" runat="server" autocomplete="off" /></asp:TextBox>--%>
                                        <%--<asp:TextBox runat="server" ID="txtTgl" CssClass="input-sm form-control date-picker" AutoComplete="off"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtTgl" runat="server" class="input-sm form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender
                                            ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTgl" Enabled="True"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div style="padding: 3px"></div>
                                <div class="row">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnprevprod" class="btn-xs btn-success" runat="server" Text="preview" OnClick="btnprevprod_Click" />
                                    </div>

                                </div>
                                <div style="padding: 3px"></div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table id="tableforklift" class="table table-striped table-bordered table-hover" border="1">
                                            <thead>
                                                <tr>
                                                    <th class="text-center">No</th>
                                                    <th class="text-center">Forklift</th>
                                                    <th class="text-center">Shift</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstOperasional" runat="server"
                                                    OnItemDataBound="lstOperasional_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="ps1" runat="server">
                                                            <td>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("prodForklift") %>
                                                                <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTarget" runat="server" Visible="true" Text='<%# Eval("Total").ToString()%>'
                                                                    AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                               
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary" id="panelrekap" runat="server">
                    <div class="panel-heading">
                        LEMBAR PEMANTAUAN BREAKDOWN TIME FORKLIFT
                        <div class="pull-right">
                            <div class="col-md-12">
                                <asp:Button ID="btninputBD2" runat="server" CssClass="btn-xs btn-success" Text="Input BD" OnClick="btninputBD2_Click" />
                                &nbsp
                                <asp:Button ID="btninputprod2" runat="server" CssClass="btn-xs btn-success" Text="Input Operasional" OnClick="btninputprod2_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-2">Periode</div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="false" class="input-sm form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" class="input-sm form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div style="padding: 3px"></div>
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnprevRekap" class="btn-xs btn-success" runat="server" Text="preview" OnClick="btnprevRekap_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnexport" Class="btn-xs btn-success" runat="server" Text="Export Excel" OnClick="btnexport_Click" />
                                    </div>
                                </div>
                                <div style="padding: 3px"></div>
                                <div class="row">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12" id="rekap" runat="server">
                                                <table id="tablerekap" class="table table-striped table-bordered table-hover" border="1">
                                                    <thead>
                                                        <tr>
                                                            <th rowspan="2" class="text-center">No</th>
                                                            <th rowspan="2" class="text-center">Tanggal</th>
                                                            <th rowspan="2" class="text-center">Nama Forklift</th>
                                                            <th colspan="2" class="text-center">Jam</th>
                                                            <th colspan="1" class="text-center">Breakdown</th>
                                                            <th rowspan="2" class="text-center">Kendala</th>
                                                            <th rowspan="2" class="text-center">Perbaikan</th>
                                                            <th rowspan="2" class="text-center">Keterangan</th>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Stop</th>
                                                            <th class="text-center">Selesai</th>
                                                            <th class="text-center">(menit)</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstrekap" runat="server"
                                                            OnItemDataBound="lstrekap_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="ps2" runat="server">
                                                                    <td>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Tanggal", "{0: dd/MM/yyyy}") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Forklift") %>
                                                                        <asp:Label ID="lblid2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Start") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("finish") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("total") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("kendala") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Perbaikan") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Keterangan") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td colspan="5" class="text-center">Total</td>
                                                            <td>
                                                                <asp:Label ID="lbltotalrekap" runat="server"></asp:Label></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                        <div style="padding: 20px"></div>
                                        <div class="row">
                                            <div class="col-md-5">
                                                <div class="widget-box">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">Total Operasional</div>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lbltotalop" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">Menit</div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">Total Breakdown</div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <span class="border border-secondary"></span>
                                                        <div class="col-md-12">
                                                            <div class="col-md-3">Presentase</div>
                                                            <div class="col-md-4">
                                                                (
                                                                <asp:Label ID="lbltotalbd" runat="server"></asp:Label>/
                                                                <asp:Label ID="lbltotalop1" runat="server"></asp:Label>)
                                                            </div>
                                                            <div class="col-md-3">X 100%</div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="col-md-3"></div>
                                                            <div class="col-md-4">

                                                                <asp:Label ID="lbltotalall" runat="server"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
    <script src="../../assets/js/jquery-ui.min.js"></script>

    <script src="../../assets/js/jquery.datetimepicker.full.js"></script>

    <script src="../../Scripts/Maintenance/BreakForklift.js" type="text/javascript"></script>
    </html>
</asp:Content>
