<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputBreakdownFN.aspx.cs" Inherits="GRCweb1.Modul.BreakDownBM.InputBreakdownFN" %>

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


        <script type="text/javascript" src="../../Scripts/calendar.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                maintainScrollPosition();
            });
            function pageLoad() {
                maintainScrollPosition();
            }
            function maintainScrollPosition() {
                $("#div2").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        
        </script>


    </head>

    <body class="no-skin">




        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        INPUT BREAKDOWN FINISHING 
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>

                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">

                        <table style="table-layout: fixed" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%; height: 49px">
                                        <table class="nbTableHeader">
                                            <tr>
                                                <td style="width: 30%; padding-left: 5px">
                                                    <%--<strong>&nbsp; INPUT BREAKDOWN FINISHING </strong>--%>
                                                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                                </td>
                                                <td style="width: 70%; padding-right: 5px" align="right">
                                                    <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_serverClick" />
                                                    <%--<asp:Button ID="btnCetak" runat="server" OnClientClick="Cetak();" Text="Cetak" />--%>
                                                    <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_ServerClick" />
                                                    <asp:Button ID="rekapList" runat="server" Text="Rekap" OnClick="btnRekap_ServerClick" />
                                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                        <asp:ListItem Value="BreakNo">No Breakdown</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Cari" OnClick="btnSearch_ServerClick" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="100%" style="width: 100%">
                                        <div class="content">
                                            <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                                <tr>
                                                    <td style="width: 15%;">&nbsp
                                                    </td>
                                                    <td style="width: 25%">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Tanggal&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTglBreak" runat="server" Width="192px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTglBreak" Format="dd-MMM-yyyy"
                                                            runat="server"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;No Break&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNoBreak" runat="server" BorderStyle="Groove" Width="80%" BackColor="#afacac"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Nama Oven&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlNamaOven" runat="server" AutoPostBack="true" Width="50%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Shift / Group&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlsGroup" runat="server" AutoPostBack="true" Width="50%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%" valign="top">&nbsp;Uraian&nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtUraian" runat="server" BorderStyle="Groove" Width="80%" Rows="3"
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Frek&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFrek" runat="server" Width="100px"></asp:TextBox>
                                                    </td>
                                                    <td></td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Waktu&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtWaktu" runat="server" Width="100px"></asp:TextBox>
                                                        <i>Menit</i>
                                                    </td>
                                                    <td></td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Kategori&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="true" Width="50%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td style="width: 20%;">&nbsp<asp:Button ID="lbAddOP" runat="server" Text="Add Item" OnClick="lbAddOP_Click" />
                                                    </td>
                                                    <td style="width: 10%;">&nbsp<asp:HiddenField ID="txtID" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%;">&nbsp;Waktu Operasional&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOps" runat="server" Width="100px"></asp:TextBox>
                                                        <i>Menit</i>
                                                    </td>
                                                    <td></td>
                                                    <td style="width: 20%;">&nbsp
                                                    </td>
                                                    <td style="width: 10%;">&nbsp
                                                    </td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="False"
                                                Width="100%" OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="NamaOven" HeaderText="Nama Oven" />
                                                    <asp:BoundField DataField="NamaGroupOven" HeaderText="Shift / Group" />
                                                    <asp:BoundField DataField="Uraian" HeaderText="Uraian" />
                                                    <asp:BoundField DataField="Frek" HeaderText="Frek" />
                                                    <asp:BoundField DataField="Waktu" HeaderText="Waktu" />
                                                    <asp:BoundField DataField="UraianCat" HeaderText="Kategori" />
                                                    <asp:BoundField DataField="WaktuOprsnl" HeaderText="Waktu Operasional" />
                                                </Columns>
                                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                <PagerStyle BorderStyle="Solid" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                            <div class="contentlist" style="height: 200px" onscroll="setScrollPosition(this.scrollTop);"
                                                id="div2">
                                                <table class="tbStandart">
                                                    <thead>
                                                        <tr class="tbHeader" style="height: 25px">
                                                            <th class="kotak" style="width: 2%">No.
                                                            </th>
                                                            <th class="kotak " style="width: 5%">Nama Oven
                                                            </th>
                                                            <th class="kotak" style="width: 5%">Shift / Group
                                                            </th>
                                                            <th class="kotak" style="width: 30%">Uraian
                                                            </th>
                                                            <th class="kotak" style="width: 3%">Frek
                                                            </th>
                                                            <th class="kotak" style="width: 3%">Waktu
                                                            </th>
                                                            <th class="kotak" style="width: 10%">Kategori
                                                            </th>
                                                            <th class="kotak" style="width: 10%">Waktu Opersional
                                                            </th>
                                                            <th class="kotak" style="width: 3%">&nbsp;
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstBdtFn" runat="server" OnItemCommand="lstBdtFn_Command" OnItemDataBound="lstBdtFn_Databound">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris" id="trd" runat="server">
                                                                    <td class="kotak tengah">
                                                                        <%# Container.ItemIndex+1  %>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("NamaOven")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("NamaGroupOven")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("Uraian")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("Frek")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("Waktu")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("UraianCat")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("WaktuOprsnl")%>
                                                                    </td>
                                                                    <td class="kotak tengah" nowrap="nowrap">
                                                                        <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandName="edit"
                                                                            CommandArgument='<%# Eval("ID") %>' ToolTip="Edit Data" />
                                                                        <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandName="dele"
                                                                            CommandArgument='<%# Container.ItemIndex %>' ToolTip="Hapus Data" />
                                                                        <%-- <asp:ImageButton ID="dels" runat="server" ImageUrl="~/images/Delete.png" CommandName="delet" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
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
