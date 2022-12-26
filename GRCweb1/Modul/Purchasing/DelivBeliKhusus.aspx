<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivBeliKhusus.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivBeliKhusus" %>

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
            function GetKey(source, eventArgs) {
                $('#<%=txtSupplierID.ClientID %>').val(eventArgs.get_value());
        }
        </script>

        <script type="text/javascript">
            function SetFocus() {
                return false;
            }


        </script>


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
                                INPUT PEMBELIAN KERTAS (TEAM KHUSUS) 
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="height: 49px; width: 100%;">
                                            <table class="nbTableHeader" style="width: 100%; border-collapse: collapse;">
                                                <tr>

                                                    <td style="width: 60%; padding-right: 10px" align="right">
                                                        <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" cssclass="form-control input-sm" type="button" value="Baru" /><input id="btnSimpan"
                                                            runat="server" onserverclick="btnSimpan_ServerClick" cssclass="form-control input-sm" type="button" value="Simpan" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="content">
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                    <tr>
                                                        <td style="width: 10%" class="left">No.SJ
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtNoSJ" runat="server" AutoPostBack="false" CssClass="txtUpper"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 4%">&nbsp;
                                                        </td>
                                                        <td style="width: 10%" class="left">Document No.
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtDocNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 5%">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="width: 10%; height: 24px;">No.Mobil
                                                        </td>
                                                        <td style="width: 20%; height: 24px;">
                                                            <asp:TextBox ID="txtNOPOL" runat="server" AutoPostBack="false" CssClass="txtUpper"
                                                                Width="120px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="mms2" runat="server" ClearMaskOnLostFocus="false" Mask="$$-9999-$$$"
                                                                MaskType="None" TargetControlID="txtNOPOL"></cc1:MaskedEditExtender>
                                                        </td>
                                                        <td style="width: 4%; height: 24px;">&nbsp;
                                                        </td>
                                                        <td class="left" style="width: 10%; height: 24px;">&nbsp;
                                                        </td>
                                                        <td style="width: 20%; height: 24px;">&nbsp;
                                                        </td>
                                                        <td style="width: 5%; height: 24px;">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="width: 10%">Ekspedisi
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:DropDownList ID="ddlExpedisi" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 4%">&nbsp;
                                                        </td>
                                                        <td class="left" style="width: 10%">&nbsp;
                                                        </td>
                                                        <td style="width: 20%">&nbsp;
                                                        </td>
                                                        <td style="width: 5%">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="width: 10%">Nama Team Khusus
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:DropDownList ID="ddlDepo" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 4%">&nbsp;
                                                        </td>
                                                        <td class="left" style="width: 10%">&nbsp;
                                                        </td>
                                                        <td style="width: 20%">&nbsp;
                                                        </td>
                                                        <td style="width: 5%">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="width: 10%">Supplier
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnTextChanged="txtSupplier_TextChanged"
                                                                Width="100%" onfocus="this.select()"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="act1" runat="server" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                                EnableCaching="true" MinimumPrefixLength="2" OnClientItemSelected="GetKey" ServiceMethod="GetSupplierKAT"
                                                                ServicePath="AutoComplete.asmx" TargetControlID="txtSupplier">
                                                            </cc1:AutoCompleteExtender>
                                                        </td>
                                                        <td style="width: 4%">&nbsp;
                                                        </td>
                                                        <td class="left" style="width: 10%">&nbsp;
                                                        </td>
                                                        <td style="width: 20%">&nbsp;
                                                        </td>
                                                        <td style="width: 5%">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="width: 10%">Tanggal Kirim
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:TextBox ID="txtTanggal" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txtSupplier_TextChanged"
                                                                ontxtchanged="txtTanggal_TextChanged" Width="70%" onfocus="this.select()"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="ca1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width: 4%">&nbsp;
                                                        </td>
                                                        <td class="left" style="width: 10%">&nbsp;
                                                        </td>
                                                        <td style="width: 20%">&nbsp;
                                                        </td>
                                                        <td style="width: 5%">&nbsp;
                                            <asp:HiddenField ID="txtSupplierID" runat="server"
                                                OnValueChanged="txtSupplier_Change" Value="0" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">Jenis Kertas
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNamaBarang" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlNamaBarang_Change"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td class="left">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="txtItemCode" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="height: 24px">Hasil Timbangan(kg)
                                                        </td>
                                                        <td style="height: 24px">
                                                            <asp:TextBox ID="txtJmlTimbangan" CssClass="form-control input-sm" Style="text-align: right" runat="server" AutoPostBack="false"
                                                                OnTextChanged="txtJmlTimbangan_Change" Text="0" onfocus="this.select()"> </asp:TextBox>
                                                        </td>
                                                        <td style="height: 24px"></td>
                                                        <td class="left" style="height: 24px"></td>
                                                        <td style="height: 24px"></td>
                                                        <td style="height: 24px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">Harga / Kg
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHarga" CssClass="form-control input-sm" Style="text-align: right" runat="server" AutoPostBack="True"
                                                                OnTextChanged="txtHarga_TextChanged" Text="0" onfocus="this.select()"></asp:TextBox>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td class="left">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left" style="height: 24px">Total Bayar
                                                        </td>
                                                        <td style="height: 24px">
                                                            <asp:TextBox ID="txtBayar" runat="server" CssClass="form-control input-sm" AutoPostBack="false"
                                                                onfocus="this.select()"
                                                                Style="text-align: right" Text="0" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 24px">&nbsp;
                                                        </td>
                                                        <td class="left" style="height: 24px">&nbsp;
                                                        </td>
                                                        <td style="height: 24px">&nbsp;
                                                        </td>
                                                        <td style="height: 24px">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="left">Tujuan Kirim
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTujuanKirim" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTujuanKirim_Change"
                                                                Width="60%">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td class="left">&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
