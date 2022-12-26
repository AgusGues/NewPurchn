<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapStockInvetory.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapStockInvetory" %>

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




        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        LAPORAN STOCK INVENTORY
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>
                    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="table-layout: fixed;" width="100%">
                            <tbody>

                                <tr valign="top">
                                    <td style="width: 100%" valign="top">
                                        <%--form input --%>
                                        <div style="background-color: #B0C4DE; vertical-align: top; padding: 3px;">
                                            <b>Select Criteria :</b>
                                            <table class="tblForm" id="Table4" style="width: 99%;" border="0">
                                                <tr>
                                                    <td style="width: 128px;" valign="top"></td>
                                                    <td style="width: 204px;" valign="top"></td>
                                                    <td style="width: 102px;" valign="top"></td>
                                                    <td style="width: 209px;" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 128px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Type Barang</span>
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:DropDownList ID="ddlTipeBarang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeBarang_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 102px;" valign="top"></td>
                                                    <td style="width: 209px;" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 128px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Group Purchn</span>
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:DropDownList ID="GroupID" runat="server" OnSelectedIndexChanged="GroupID_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Value="">--Pilih Group--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 102px;" valign="top"></td>
                                                    <td style="width: 209px;" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 128px;" valign="top">&nbsp; Criteria&nbsp;
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:DropDownList ID="ddlCriteria" runat="server">
                                                            <asp:ListItem Value=" and aktif=1">Aktif</asp:ListItem>
                                                            <asp:ListItem Value=" and Jumlah > 0"> Jumlah > 0</asp:ListItem>
                                                            <asp:ListItem Value=" and Aktif=0"> Non Aktif</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td valign="top" colspan="2">&nbsp;
                                                    </td>
                                                    <td style="width: 205px;" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="width: 128px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Short By</span>
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:DropDownList ID="ddlOrderby" runat="server">
                                                            <asp:ListItem Value="ItemCode">Item Code</asp:ListItem>
                                                            <asp:ListItem Value="ItemName">Item Name</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td colspan="2" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 128px;" valign="top"></td>
                                                    <td style="width: 204px;" valign="top"></td>
                                                    <td style="width: 102px;" valign="top"></td>
                                                    <td style="width: 209px;" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 128px;" valign="top"></td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:Button ID="cari" runat="server" Text="Search" OnClick="cari_Click" />
                                                    </td>
                                                    <td style="width: 102px;" valign="top"></td>
                                                    <td style="width: 209px;" valign="top"></td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <%--end of form input --%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div id="lst" style="width: 100%; padding: 5px; height: 320px; overflow: auto">
                        <%--List stock--%>
                        <table width="100%" style="border-collapse: collapse;" border="1">
                            <thead>
                                <tr align="center" style="background-color: #FFA000">
                                    <th width="5%">#
                                    </th>
                                    <th width="10%">Item Code
                                    </th>
                                    <th width="25%">Item Name
                                    </th>
                                    <th width="5%">Unit
                                    </th>
                                    <th width="10%">Stock Item
                                    </th>
                                    <th width="10%">Jml Transit
                                    </th>
                                    <%--<th width="10%">
                                Dept
                            </th>--%>
                                </tr>
                            </thead>
                            <tbody style="font-size: x-small">
                                <asp:Repeater ID="ListStock" runat="server">
                                    <ItemTemplate>
                                        <tr style="cursor: pointer">
                                            <td align="center"><%# Eval("ID") %></td>
                                            <td align="center"><%# Eval("ItemCode") %></td>
                                            <td align="left"><%# Eval("ItemName") %></td>
                                            <td align="center"><%# Eval("UOMCode")%></td>
                                            <td align="right"><%# Eval("Jumlah", "{0:#,##0.00}")%></td>
                                            <td align="right"><%# Eval("Harga", "{0:#,##0.00}")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color: #E0E8F0; cursor: pointer">
                                            <td align="center"><%# Eval("ID") %></td>
                                            <td align="center"><%# Eval("ItemCode") %></td>
                                            <td align="left"><%# Eval("ItemName") %></td>
                                            <td align="center"><%# Eval("UOMCode")%></td>
                                            <td align="right"><%# Eval("Jumlah", "{0:#,##0.00}")%></td>
                                            <td align="right"><%# Eval("Harga", "{0:#,##0.00}")%></td>

                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <%-- end of list stock --%>
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
