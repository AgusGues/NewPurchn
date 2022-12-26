<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListRountineTest.aspx.cs" Inherits="GRCweb1.Modul.Boardmill.ListRountineTest" %>

<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>


        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>



    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>

        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        LIST ROUTINE TEST
                    </div>
                    <div style="padding: 2px"></div>



                    <%--copy source design di sini--%>
                    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="5" style="height: 49px">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td style="width: 100%; height: 49px">
                                                <table class="nbTableHeader" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 50%"></td>
                                                        <td style="width: 50%; padding-right: 10px" align="right">
                                                            <asp:Button ID="btnBack" runat="server" Text="Form" OnClick="btnBack_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" valign="top" align="left">
                                                <div id="div2" class="">
                                                    <table id="headerPO" width="100%" style="border-collapse: collapse;" visible="true"
                                                        runat="server">
                                                        <tr>
                                                            <td colspan="5">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%" align="right" style="height: 24px">Periode &nbsp;
                                                            </td>
                                                            <td width="25%" style="height: 24px">
                                                                <asp:TextBox ID="txtDrTgl" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy"
                                                                    EnableViewState="true"></cc1:CalendarExtender>
                                                                <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy"
                                                                    EnableViewState="true"></cc1:CalendarExtender>
                                                            </td>
                                                            <td width="5%" style="height: 24px"></td>
                                                            <td width="10%" align="right" style="height: 24px"></td>
                                                            <td width="25%" style="height: 24px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%" align="right">Line&nbsp;
                                                            </td>
                                                            <td width="20%" colspan="2" style="width: 25%">
                                                                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" Width="76%">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="15%" align="right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%" align="right">Formula&nbsp;
                                                            </td>
                                                            <td width="20%" colspan="2" style="width: 25%">
                                                                <asp:DropDownList ID="ddlFormula" runat="server" AutoPostBack="true" Width="76%">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="15%" align="right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%" align="right">&nbsp;
                                                            </td>
                                                            <td width="20%" colspan="2" style="width: 25%">
                                                                <asp:Button ID="Preview" runat="server" Text="Preview" OnClick="Preview_Click" />
                                                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                                            </td>
                                                            <td width="15%" align="right">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <hr />
                                                    <div id="lst" runat="server" class="contentlist" style="height: 380px">
                                                        <table width="100%" style="border-collapse: collapse; font-size: x-small" id="lst1"
                                                            border="1px;">
                                                            <thead>
                                                                <tr class="tbHeader">
                                                                    <th class="kotak" rowspan="2" style="width: 4%">No.
                                                                    </th>
                                                                    <th class="kotak" rowspan="2" style="width: 4%">ID
                                                                    </th>
                                                                    <th class="kotak" rowspan="2" style="width: 6%">Prod. Date
                                                                    </th>

                                                                    <th class="kotak" rowspan="2" style="width: 4%">Formula
                                                                    </th>
                                                                    <th class="kotak" colspan="6">Denisty
                                                                    </th>
                                                                    <th class="kotak" colspan="3">Water Content
                                                                    </th>
                                                                    <th class="kotak" colspan="3">Water Absorption
                                                                    </th>
                                                                    <th class="kotak" colspan="2">LB
                                                                    </th>
                                                                    <th class="kotak" colspan="2">LK
                                                                    </th>
                                                                    <th class="kotak" colspan="2">Dimention Changes(%)
                                                                    </th>
                                                                </tr>
                                                                <tr class="tbHeader">
                                                                    <th class="kotak" style="width: 3%">BK
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">t
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">l
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">p
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">V
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">Denisty
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">BA
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">BK
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">WC
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">BB
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">BK
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">WA
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">C
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">L
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">C
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">L
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">C
                                                                    </th>
                                                                    <th class="kotak" style="width: 3%">L
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <asp:Repeater ID="lstBendingStrength" runat="server" OnItemDataBound="lstBendingStrength_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr align="center" valign="top">
                                                                        <td align="center" border="1px;">
                                                                            <%#Eval("Nom") %>
                                                                        </td>
                                                                        <td align="center" valign="top" border="1px;">
                                                                            <%# Eval("ID") %>
                                                                        </td>
                                                                        <td align="left" border="1px;">
                                                                            <%# Eval("Tanggal", "{0:d}")%>
                                                                        </td>

                                                                        <td border="1px;">
                                                                            <%# Eval("Formula") %>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("BK", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("T", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("L", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("P", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("V", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("Denisty", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("BA", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("BK2", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("WC", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("BB", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("BK3", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("WA", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("LBC", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("LBL", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("LKC", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("LKL", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("DimentioC", "{0:N2}")%>
                                                                        </td>
                                                                        <td border="1px;">
                                                                            <%# Eval("DimentioL", "{0:N2}")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>
        </div>
        <script src="../../assets/jquery.js" type="text/javascript"></script>
        <script src="../../assets/js/jquery-ui.min.js"></script>
        <script src="../../assets/select2.js"></script>
        <script src="../../assets/datatable.js"></script>
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>
</asp:Content>
