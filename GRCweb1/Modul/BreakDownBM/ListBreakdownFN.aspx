<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListBreakdownFN.aspx.cs" Inherits="GRCweb1.Modul.BreakDownBM.ListBreakdownFN" %>

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

        <script type="text/javascript">
            $(document).ready(function () {
                maintainScrollPosition();
            });
            function pageLoad() {
                maintainScrollPosition();
            }
            function maintainScrollPosition() {
                $("#<%=lst.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }

        </script>

    </head>

    <body class="no-skin">




        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        LIST BREAKDOWN FINISHING
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>
                    <div id="div1" runat="server" class="table-responsive" style="width:100%">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <tr>
                                <td style="width: 100%; height: 49px">
                                    <table class="nbTableHeader" style="width: 100%">
                                        <tr>
                                            <td style="width: 50%">
                                                <%--<b>&nbsp;&nbsp; List Breakdown Finishing</b>--%>
                                            </td>
                                            <td style="width: 50%; padding-right: 10px" align="right">
                                                <asp:Button ID="btnBack" runat="server" Text="Form" OnClick="btnBack_Click" />
                                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="content">
                                        <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                            <tr>
                                                <td style="width: 10%">&nbsp;
                                                </td>
                                                <td style="width: 15%">Periode :
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlBulan" runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">&nbsp;
                                                </td>
                                                <td style="width: 15%">Pilih Oven :
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlNamaOven" runat="server" AutoPostBack="true" Width="50%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                                </td>

                                            </tr>
                            </tr>
                            <tr>
                                <td style="width: 10%">&nbsp;
                                </td>
                                <td style="width: 15%">Kategori Uraian :
                                </td>
                                <td style="width: 35%">
                                    <asp:DropDownList ID="ddlCategoriUraian" runat="server" AutoPostBack="true" Width="50%" OnSelectedIndexChanged="ddlCategoriUraian_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>

                            </tr>
                            <tr>
                                <td style="width: 10%">&nbsp;
                                </td>
                                <td style="width: 15%">Pemotong Output Oven:
                                </td>
                                <td style="width: 35%">
                                    <asp:DropDownList ID="ddlPemotongOutputOven" runat="server" AutoPostBack="true" Width="50%" OnSelectedIndexChanged="ddlPemotongOutputOven_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Pilih--</asp:ListItem>
                                        <asp:ListItem Value="18,20,21,22">Oven Mati, PLN Mati, Batu Bara Habis, Produk yang akan di oven habis</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>

                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                            <table style="width: 70%; border-collapse: collapse; font-size: x-small" border="0" id="bdtList">
                                <thead>
                                    <tr class="tbHeader">

                                        <th style="width: 7%" class="kotak">
                                            <b>Tanggal</b>
                                        </th>
                                        <th style="width: 3%" class="kotak">Shift / Group
                                        </th>
                                        <th class="kotak" colspan="2" style="width: 30%">Uraian
                                        </th>
                                        <th style="width: 3%" class="kotak">Frek. ( X )
                                        </th>
                                        <th style="width: 5%" class="kotak">Waktu ( Menit )
                                        </th>
                                        <th style="width: 3%" class="kotak">Code
                                        </th>
                                        <th style="width: 8%" class="kotak">Ket
                                        </th>
                                        <th style="width: 8%" class="kotak"></th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstListBdt" runat="server" OnItemDataBound="lstListBdt_DataBound"
                                        OnItemCommand="lstListBdt_Command">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                               
                                                <td class="kotak" nowrap="nowrap" colspan="9">
                                                    <b><span style="width: 30%">&nbsp;[
                                                        <asp:Label runat="server" ID="TglBreak" Text='<%# Eval("NamaOven") %>'></asp:Label>
                                                        ]</span></b>
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="lstListBdt2" runat="server" OnItemDataBound="lstListBdt2_ItemDataBound"
                                                OnItemCommand="lstListBdt2_ItemCommand">

                                                <ItemTemplate>
                                                    <tr class="Line3" runat="server" id="t">

                                                        <td class="kotak" nowrap="nowrap">&nbsp;&nbsp;<asp:Label runat="server" ID="TglBreak" Text='<%# Eval("TanggalBreak1") %>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("NamaGroupOven")%>
                                                        </td>
                                                        <td class="kotak tengah" style="width: 1%">*
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("Uraian")%>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <asp:Label ID="lblFrex" runat="server" Text='<%# Eval("Frek")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <asp:Label ID="lblWaktu" runat="server" Text='<%# Eval("Waktu")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <asp:Label ID="NmMasterCatID" runat="server" Text='<%# Eval("NmMasterCatID")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <asp:Label ID="lblUraianCat" runat="server" Text='<%# Eval("UraianCat")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap" id="hapusgrid">
                                                            <span id="ext" runat="server" style="width: 10%; text-align: right">
                                                                <asp:ImageButton ID="updSts" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="upd"
                                                                    ImageUrl="~/images/Delete.png" ToolTip="Click For Delete" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                               
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    </td>
                    </tr>
                </table>
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
