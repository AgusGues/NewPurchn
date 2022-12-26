<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantauanSPP.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantauanSPP" %>

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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                LAPORAN PEMANTAUAN SPP, PO DAN RECEIPT
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>
                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="table-layout: fixed; width: 100%;">

                                    <tr>
                                        <td colspan="5" valign="top" align="left">
                                            <div id="div2" class="content">
                                                <table id="headerPO" width="100%" style="border-collapse: collapse;" visible="true" runat="server">
                                                    <tr>
                                                        <td colspan="5">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" align="right">Periode &nbsp;</td>
                                                        <td width="20%">
                                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;
                        <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList></td>
                                                        <td width="5%"></td>
                                                        <td width="10%" align="right">Pencarian</td>
                                                        <td width="25%">
                                                            <asp:TextBox ID="txtDrTgl" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                                            <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                        <td width="15%" align="right">Bulan&nbsp;</td>
                        <td width="20%">
                           </td>
                        <td width="5%"></td>
                        <td width="10%" align="right">
                        </td>
                        <td width="25%"></td>
                    </tr>--%>
                                                    <tr>
                                                        <td width="15%" align="right">Tipe Barang&nbsp;</td>
                                                        <td width="20%" colspan="2" style="width: 25%">
                                                            <asp:DropDownList ID="ddlTipeBarang" runat="server" OnSelectedIndexChanged="ddlTipeBarang_SelectedIndexChanged" AutoPostBack="true" Width="120px">
                                                                <asp:ListItem Value="0">--Pilih--</asp:ListItem>
                                                                <asp:ListItem Value="1">Inventory</asp:ListItem>
                                                                <asp:ListItem Value="2">Asset</asp:ListItem>
                                                                <asp:ListItem Value="3">Biaya</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="15%" align="right">&nbsp;</td>
                                                        <td width="20%">
                                                            <asp:DropDownList ID="ddlTipe" runat="server" OnSelectedIndexChanged="ddlTipe_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">--Tipe Barang--</asp:ListItem>
                                                                <asp:ListItem Value="1">Inventory</asp:ListItem>
                                                                <asp:ListItem Value="2">Asset</asp:ListItem>
                                                                <asp:ListItem Value="3">Biaya</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlCriteria" runat="server">
                                                                <asp:ListItem Value="0">--Criteria--</asp:ListItem>
                                                                <asp:ListItem Value="1">ItemCode</asp:ListItem>
                                                                <asp:ListItem Value="2">ItemName</asp:ListItem>
                                                                <asp:ListItem Value="3">NoPO</asp:ListItem>
                                                                <asp:ListItem Value="4">NoSPP</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" align="right">Purchn Group&nbsp;</td>
                                                        <td width="20%" colspan="2" style="width: 25%">
                                                            <asp:DropDownList ID="ddlPurchn" runat="server" Height="20px"
                                                                OnSelectedIndexChanged="ddlPurchn_SelectedIndexChanged" Width="120px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="15%" align="right">&nbsp;</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txtMasukan" runat="server" Width="245px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" align="right">&nbsp;</td>
                                                        <td width="20%" colspan="2" style="width: 25%">
                                                            <asp:Button ID="Preview" runat="server" Text="Preview" OnClick="Preview_Click" />
                                                            <asp:Button ID="Print" runat="server" Text="Print Out" OnClick="Print_Click" Visible="true" /></td>
                                                        <td width="15%" align="right">&nbsp;</td>
                                                        <td width="20%">
                                                            <asp:Button ID="btnCari" runat="server" Text="Search" OnClick="btnCari_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="lstSP" runat="server" style="width: 100%; height: 350px; background-color: White; border: 2px solid #B0C4DE; padding: 3px; overflow: auto">
                                                    <table width="200%" style="border-collapse: collapse; font-size: x-small" id="lst1">
                                                        <thead>
                                                            <tr align="center" style="background-color: #E0D8E0;">
                                                                <th rowspan="2" style="width: 2%; border: 1px solid grey">No.</th>
                                                                <th rowspan="2" style="width: 4%; border: 1px solid grey">ItemCode</th>
                                                                <th rowspan="2" style="width: 12%; border: 1px solid grey;">Item Name</th>
                                                                <th rowspan="2" style="width: 3%; border: 1px solid grey">Uom</th>
                                                                <th colspan="15" style="background-color: #B0C4DE; border: 1px solid grey">SPP</th>
                                                                <th style="background-color: #F0E890; border: 1px solid grey">PO</th>
                                                                <th style="background-color: #B0C4DE; border: 1px solid grey">Receipt</th>
                                                            </tr>
                                                            <tr align="center" style="background-color: #E0D8E0">
                                                                <th style="width: 4%; border: 1px solid grey">Tgl SPP</th>
                                                                <th style="width: 6%; border: 1px solid grey">User</th>
                                                                <th style="width: 6%; border: 1px solid grey">Dept</th>
                                                                <th style="width: 4%; border: 1px solid grey">No SPP</th>
                                                                <th style="width: 3%; border: 1px solid grey">Apv</th>
                                                                <th style="width: 4%; border: 1px solid grey">Apv Date</th>
                                                                <th style="width: 2%; border: 1px solid grey">Qty</th>
                                                                <th style="width: 4%; border: 1px solid grey">Harga</th>
                                                                <th style="width: 3%; border: 1px solid grey">Tgl PO</th>
                                                                <th style="width: 3%; border: 1px solid grey">No PO</th>
                                                                <th style="width: 2%; border: 1px solid grey">Apv</th>
                                                                <th style="width: 2%; border: 1px solid grey">Qty</th>
                                                                <th style="width: 4%; border: 1px solid grey">Tgl Receipt</th>
                                                                <th style="width: 4%; border: 1px solid grey">No Receipt</th>
                                                                <th style="width: 4%; border: 1px solid grey">Qty Receipt</th>
                                                                <th style="border: 1px solid grey; width: 15%">
                                                                    <table style="border-collapse: collapse; font-size: x-small" width="100%">
                                                                        <tr align="center">
                                                                            <td style="width: 20%; border-right: 1px solid grey;">Tgl PO</td>
                                                                            <td style="width: 20%; border-right: 1px solid grey;">No PO</td>
                                                                            <td style="width: 20%; border-right: 1px solid grey;">Apv</td>
                                                                            <td style="width: 20%;" align="right">Qty PO</td>
                                                                        </tr>
                                                                    </table>
                                                                </th>

                                                                <th style="width: 50%; border: 1px solid grey">
                                                                    <table style="border-collapse: collapse; font-size: x-small" width="100%">
                                                                        <tr align="center">
                                                                            <td style="width: 20%; border-right: 1px solid grey;">Tgl</td>
                                                                            <td style="width: 20%; border-right: 1px solid grey;">No.Receipt</td>
                                                                            <td style="width: 20%; border-right: 1px solid grey;">Qty</td>
                                                                    </table>
                                                                </th>

                                                            </tr>
                                                        </thead>

                                                        <asp:Repeater ID="lstSPP" runat="server" OnItemDataBound="lstSPP_DataBound">
                                                            <ItemTemplate>
                                                                <tr align="center" style="background-color: #FFF0F0" valign="top">
                                                                    <td align="center" style="border: 1px solid grey;"><%#Eval("Nom") %></td>
                                                                    <td align="center" valign="top" style="border: 1px solid grey;"><%# Eval("ItemCode") %></td>
                                                                    <td align="left" style="border: 1px solid grey;"><%# Eval("ItemName") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("Uom") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("TglSpp","{0:d}") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("UserAlias") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("DeptName") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("NoSpp") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("ApvStatus") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("ApvDate") %></td>
                                                                    <td align="right" style="border: 1px solid grey; padding-right: 5px"><%# Eval("QtySpp", "{0:N0}")%></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("HargaPO","{0:N0}") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("PurchnDate", "{0:d}")%></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("NopemantauanPO")%></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("ApprovalPo")%></td>
                                                                    <td align="right" style="border: 1px solid grey; padding-right: 5px"><%# Eval("QtyPO2", "{0:N0}")%></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("TglReceipt","{0:d}") %></td>
                                                                    <td style="border: 1px solid grey;"><%# Eval("NoReceipt") %></td>
                                                                    <td align="right" style="border: 1px solid grey; padding-right: 5px"><%# Eval("QtyReceipt", "{0:N0}")%></td>
                                                                    <td style="border: 1px solid grey;">
                                                                        <table style="border-collapse: collapse; font-size: x-small" width="100%">
                                                                            <asp:Repeater ID="lstPO" runat="server" EnableViewState="true">
                                                                                <ItemTemplate>
                                                                                    <tr align="center">
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("ApvDate")%></td>
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("NoPO") %></td>
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("ApvStatus") %></td>
                                                                                        <td style="width: 20%;" align="right"><%# Eval("Qty","{0:N0}") %></td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <AlternatingItemTemplate>
                                                                                    <tr align="center" style="background-color: #FFF8D0">
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("ApvDate")%></td>
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("NoPO") %></td>
                                                                                        <td style="width: 20%; border-right: 1px solid grey"><%# Eval("ApvStatus") %></td>
                                                                                        <td style="width: 20%;" align="right"><%# Eval("Qty", "{0:N0}")%></td>
                                                                                    </tr>
                                                                                </AlternatingItemTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
                                                                    </td>

                                                                    <td style="border: 1px solid grey;">
                                                                        <table style="border-collapse: collapse; font-size: x-small" width="100%">
                                                                            <asp:Repeater ID="lstRMS" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr align="center">
                                                                                        <td style="width: 20%; border-right: 1px solid grey;"><%# Eval("ApvDate") %></td>
                                                                                        <td style="width: 20%; border-right: 1px solid grey;"><%# Eval("NoPO")%></td>
                                                                                        <td align="right" style="width: 20%; border-right: 1px solid grey;"><%# Eval("Qty", "{0:N0}")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
