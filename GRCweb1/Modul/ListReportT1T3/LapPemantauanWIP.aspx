<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantauanWIP.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LapPemantauanWIP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>T3 Retur</title>

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
                                <h3 class="panel-title">WIP BreakDown</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tanggal Produksi</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="tglProduksi" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Extender_tglProduksi" TargetControlID="tglProduksi" FirstDayOfWeek="Default" EnableViewState="true" runat="server" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">View Style</label>
                                        </div>
                                        <div class="col-md-9">
                                            <span>
                                                <asp:RadioButton ID="list" runat="server" GroupName="view" Text="List View" Checked="true" OnCheckedChanged="list_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <span>
                                                <asp:RadioButton ID="tri" runat="server" GroupName="view" Text="TreeView" OnCheckedChanged="tri_CheckedChanged" AutoPostBack="true" />
                                            </span>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Filter Criteria</label>
                                        </div>
                                        <div class="col-md-9">
                                            <table style="width: 80%; border-collapse: collapse; font-size: x-small; background-color: #A0B0E0" id="criteria" runat="server">
                                                <tr>
                                                    <td style="width: 10%">&nbsp;&nbsp;Part. No
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:TextBox ID="txtPartNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 4%">&nbsp</td>
                                                    <td style="width: 10%">&nbsp;Palet No</td>
                                                    <td style="width: 10%">
                                                        <asp:TextBox ID="txtPaletNo" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 4%">&nbsp</td>
                                                    <td style="width: 10%">&nbsp;Lokasi</td>
                                                    <td style="width: 10%">
                                                        <asp:TextBox ID="TxtLokasi" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 4%">&nbsp</td>
                                                    <td style="width: 10%">&nbsp;Rak No</td>
                                                    <td style="width: 10%">
                                                        <asp:TextBox ID="txtRakNo" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 40%">&nbsp;
                                    <!--Autocomplete PartNo-->
                                                        <cc1:AutoCompleteExtender ID="AutoComplete_txtPartNo" runat="server" TargetControlID="txtPartNo"
                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetNoProdukBM" ServicePath="~/Modul/Factory/AutoComplete.asmx"
                                                            CompletionListCssClass="autocomplete_completionListElement">
                                                        </cc1:AutoCompleteExtender>
                                                        <!--Autocomplete PelatNo-->
                                                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPaletNo"
                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetNoProdukBM" ServicePath="~/Modul/Factory/AutoComplete.asmx"
                                            CompletionListCssClass="autocomplete_completionListElement">
                                        </cc1:AutoCompleteExtender>--%>
                                                        <!--Autocomplete Lokasi-->
                                                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtLokasi"
                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetNoProdukBM" ServicePath="~/Modul/Factory/AutoComplete.asmx"
                                            CompletionListCssClass="autocomplete_completionListElement">
                                        </cc1:AutoCompleteExtender>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="preview" class="btn btn-sm btn-primary" runat="server" Text="Preview" 
                                    onclick="preview_Click" />
                                <asp:Button ID="toExcel" runat="server" Text="Export to Excel" Visible="false"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div id="tabled" runat="server" style="background-color: White; border: 2px solid #B0C4DE; padding: 5px; height: 370px; overflow: auto" visible="false">
                                    <table width="100%" style="border-collapse: collapse;">
                                        <thead>
                                            <tr style="border: 1px solid grey" align="center">
                                                <th rowspan="2" style="width: 4%; background-color: #A0A8A0; border: 1px solid grey">No.</th>
                                                <th rowspan="2" style="width: 15%; background-color: #A0A8A0; border: 1px solid grey">Part No.</th>
                                                <th colspan="3" style="background-color: #FFE0B0; border: 1px solid grey">Curing</th>
                                                <th colspan="4" style="background-color: #E0FFFF; border: 1px solid grey">Jemur</th>
                                                <th colspan="5" style="background-color: #FFF8D0; border: 1px solid grey">Transit</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                            <tr style="background-color: #A0A8A0;" align="center">
                                                <th style="width: 8%; border: 1px solid grey">Lokasi</th>
                                                <th style="width: 8%; border: 1px solid grey">Palet</th>
                                                <th style="width: 6%; border: 1px solid grey">Qty</th>
                                                <th style="width: 9%; border: 1px solid grey">Tgl Jemur</th>
                                                <th style="width: 8%; border: 1px solid grey">Rak No</th>
                                                <th style="width: 6%; border: 1px solid grey">QtyIn</th>
                                                <th style="width: 6%; border: 1px solid grey">QtyOut</th>
                                                <th style="width: 9%; border: 1px solid grey">Tgl Serah</th>
                                                <th style="width: 15%; border: 1px solid grey">PartNo</th>
                                                <th style="width: 8%; border: 1px solid grey">Lokasi</th>
                                                <th style="width: 6%; border: 1px solid grey">QtyIn</th>
                                                <th style="width: 6%; border: 1px solid grey">QtyOut</th>
                                                <th style="width: 1%; background-color: white">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="Pantau" runat="server" OnItemDataBound="Pantau_DataBound">
                                                <ItemTemplate>
                                                    <tr style="background-color: #F0F0F0; border: 1px solid grey; cursor: pointer">
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("ID") %></td>
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("PartNo") %></td>
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("Lokasi") %></td>
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("Paletno") %></td>
                                                        <td align="right" style="border: 1px solid grey"><%# Eval("Qty","{0:N2}") %>&nbsp;&nbsp;</td>
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("NoPart") %></td>
                                                        <td align="center" style="border: 1px solid grey"><%# Eval("Rak") %></td>
                                                        <td align="right" style="border: 1px solid grey"><%# Eval("QtyIn","{0:N2}") %></td>
                                                        <td align="right" style="border: 1px solid grey"><%# Eval("QtyOut","{0:N2}") %></td>
                                                        <td align="center" colspan="5" style="border: 1px solid grey">&nbsp;</td>
                                                        <td runat="server"><span id="Span2" runat="server" visible="false"><%#Eval("DestackID")%></span></td>
                                                    </tr>

                                                    <asp:Repeater runat="server" ID="isSerah">
                                                        <ItemTemplate>
                                                            <tr style="">
                                                                <td align="center" colspan="9" style="border-left: 1px solid grey">&nbsp;</td>
                                                                <td align="center" style="border: 1px solid grey; background-color: InfoBackground"><%# Eval("NoPart") %></td>
                                                                <td align="left" style="border: 1px solid grey; background-color: InfoBackground"><%# Eval("PartNo")%></td>
                                                                <td align="left" style="border: 1px solid grey; background-color: InfoBackground"><%# Eval("Lokasi") %></td>
                                                                <td align="right" style="border: 1px solid grey; background-color: InfoBackground"><%# Eval("sQtyIn","{0:N2}") %></td>
                                                                <td align="right" style="border: 1px solid grey; background-color: InfoBackground"><%# Eval("sQtyOut","{0:N2}") %></td>
                                                                <td><span runat="server" visible="false"><%#Eval("DestackID")%></span></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr>
                                                                <td align="center" colspan="9" style="border-left: 1px solid grey">&nbsp;</td>
                                                                <td align="center" style="border: 1px solid grey"><%# Eval("NoPart") %></td>
                                                                <td align="left" style="border: 1px solid grey"><%# Eval("PartNo")%></td>
                                                                <td align="left" style="border: 1px solid grey"><%# Eval("Lokasi") %></td>
                                                                <td align="right" style="border: 1px solid grey"><%# Eval("sQtyIn","{0:N2}") %></td>
                                                                <td align="right" style="border: 1px solid grey"><%# Eval("sQtyOut","{0:N2}") %></td>
                                                                <td><span runat="server" visible="false"><%#Eval("DestackID")%></span></td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater ID="sumSerah" runat="server" EnableViewState="true">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center" colspan="9" style="border-left: 1px solid grey">&nbsp;</td>
                                                                <td colspan="3" align="right" style="border: 1px solid grey; background-color: #B0C4DE">Total &nbsp;&nbsp;</td>
                                                                <td align="right" style="border: 1px solid grey; background-color: #B0C4DE"><%#Eval("QtyIn", "{0:N2}")%></td>
                                                                <td align="right" style="border: 1px solid grey; background-color: #B0C4DE"><%#Eval("QtyOut","{0:N2}")%></td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="nofound" runat="server" visible="false">
                                                <td colspan="8">&nbsp;&nbsp;Data not found...</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div style="width: 100%; padding: 5px; height: 370px; background-color: White; overflow: auto" id="trev" runat="server">
                                    <asp:TreeView ID="tahap1" runat="server" ShowLines="true" Width="100%" OnSelectedNodeChanged="tahap1_SelectedNodeChanged">
                                    </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>