<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SerahTerimaAsset.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.SerahTerimaAsset" %>

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
                                Serah Terima Asset Berkomponen
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 100%">
                                            <table class="nbTableHeader" style="width: 100%">
                                                <tr style="height: 49px">
                                                    <%--<td style="width: 30%">&nbsp;&nbsp;<span style="font-family: Calibri"><b>Serah Terima Asset Berkomponen</b></span></td>--%>
                                                    <td style="width: 60%; padding-right: 15px" align="right">
                                                        <asp:Button ID="btnSerah" runat="server" Text="Serah" Visible="false" OnClick="btnSerah_Click" Style="font-family: Calibri; font-weight: 700" />
                                                    </td>
                                                    <td style="width: 60%; padding-right: 15px" align="right">
                                                        <asp:Button ID="btnApprove" runat="server" Text="Approved" Visible="false" OnClick="btnApprove_Click" Style="font-family: Calibri; font-weight: 700" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="">
                                                <hr />
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                    <tr>
                                                        <td style="width: 15%; font-family: Calibri;">&nbsp;&nbsp; Cari Nomor Asset&nbsp;</td>
                                                        <td style="width: 55%">
                                                            <asp:TextBox ID="txtNomor0" runat="server" AutoPostBack="true" OnTextChanged="txtNomor_Change" Width="80%"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtNomor0_AutoCompleteExtender" runat="server"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="true"
                                                                MinimumPrefixLength="2" ServiceMethod="GetListProjectNo"
                                                                ServicePath="AutoComplete.asmx" TargetControlID="txtNomor0">
                                                            </cc1:AutoCompleteExtender>
                                                            <asp:HiddenField ID="txtProjectID" runat="server" />
                                                        </td>
                                                        <td style="width: 10%">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%; font-family: Calibri;">&nbsp;&nbsp;Nama Asset</td>
                                                        <td style="width: 55%">
                                                            <asp:DropDownList ID="ddlNamaProject" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlNamaProject_Change"
                                                                Style="font-family: Calibri; font-weight: 700" Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:HiddenField ID="txtID" runat="server" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri">&nbsp;&nbsp;Nomor Asset</td>
                                                        <td>
                                                            <asp:TextBox ID="txtNomor" runat="server"
                                                                Style="font-family: Calibri; font-weight: 700"></asp:TextBox></td>
                                                        <td>
                                                            <asp:HiddenField ID="txtNoImp" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="txtLevel" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri; height: 35px">&nbsp;&nbsp;Tanggal Mulai</td>
                                                        <td style="height: 35px">
                                                            <asp:TextBox ID="txtTglMulai" runat="server"
                                                                Style="font-family: Calibri"></asp:TextBox></td>
                                                        <td style="height: 35px">
                                                            <asp:HiddenField ID="txtTMulai" runat="server" />
                                                        </td>
                                                        <td style="height: 35px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri; height: 35px">&nbsp;&nbsp;Tanggal Selesai</td>
                                                        <td style="height: 35px">
                                                            <asp:TextBox ID="txtTglSelesai" runat="server"
                                                                Style="font-family: Calibri; font-size: x-small" Width="269px"></asp:TextBox></td>
                                                        <td>
                                                            <cc1:CalendarExtender ID="cal" runat="server" TargetControlID="txtTglSelesai" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri">&nbsp;&nbsp;Total Nilai Asset</td>
                                                        <td>
                                                            <asp:TextBox ID="txtBiaya" runat="server" Style="font-family: Calibri"></asp:TextBox></td>
                                                        <td>
                                                            <asp:HiddenField ID="txtHarga" runat="server" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri">&nbsp;&nbsp;Status</td>
                                                        <td>
                                                            <asp:TextBox ID="txtStatus" Width="70%" runat="server"
                                                                Style="font-family: Calibri"></asp:TextBox></td>
                                                        <td>
                                                            <asp:HiddenField ID="txtSts" runat="server" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri">&nbsp;&nbsp;Pemilik Asset</td>
                                                        <td>
                                                            <asp:TextBox ID="txtDeptPemohon" runat="server" Width="100%"
                                                                Style="font-family: Calibri"></asp:TextBox></td>
                                                        <td>
                                                            <asp:HiddenField ID="txtDeptMohon" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="txtFinish" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDID" runat="server" Width="10%" Visible="false"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri">&nbsp;&nbsp;Pembuat Asset</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPelaksana" runat="server" Width="60%"
                                                                Style="font-family: Calibri"></asp:TextBox></td>
                                                </table>

                                                &nbsp;&nbsp;<span style="font-family: Calibri"><b>List Komponen Asset</b></span>
                                                <hr />
                                                <div class="contentlist" style="height: 300px">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                        <tr class="tbHeader">
                                                            <th class="kotak" style="width: 3%; font-family: Calibri;">No.</th>
                                                            <th class="kotak" style="width: 15%; font-family: Calibri;">Kode Asset</th>
                                                            <th class="kotak" style="width: 46%; font-family: Calibri;">Nama Komponen Asset</th>
                                                            <th class="kotak" style="width: 8%; font-family: Calibri;">Unit</th>
                                                            <th class="kotak" style="width: 8%; font-family: Calibri;">Quantity</th>
                                                            <th class="kotak" style="width: 10%; font-family: Calibri;">Harga Satuan</th>
                                                            <th class="kotak" style="width: 10%; font-family: Calibri;">Total Nilai</th>
                                                            <th class="" style="background-color: Transparent">&nbsp;</th>
                                                        </tr>
                                                        <tbody style="font-family: Calibri">
                                                            <asp:Repeater ID="lstMaterial" runat="server" OnItemDataBound="lstMaterial_DataBound">
                                                                <ItemTemplate>
                                                                    <tr id="lst" runat="server" class="EvenRows baris">
                                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                                        <td class="kotak tengah"><%# Eval("UomCode") %></td>
                                                                        <td class="kotak angka"><%# Eval("QtyPakai","{0:N2}") %></td>
                                                                        <td class="kotak angka"><%# Eval("Price","{0:N2}") %></td>
                                                                        <td class="kotak angka"><%# Eval("TotalPrice","{0:N2}") %></td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <tr class="total baris" id="ftr" runat="server">
                                                                        <td class="kotak tengah" colspan="6">
                                                                            <strong>Grand Total Nilai</strong>
                                                                        </td>
                                                                        <td class="kotak bold angka"></td>
                                                                    </tr>
                                                                </FooterTemplate>
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
