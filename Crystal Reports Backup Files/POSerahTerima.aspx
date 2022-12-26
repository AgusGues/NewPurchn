<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POSerahTerima.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.POSerahTerima" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />

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
                       <span class="the-title">Serah Terima Document</span>
                        <div class="pull-right">
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" OnClick="btnBack_Click" CssClass="btn btn-outline-secondary" />
                            <asp:Button ID="btnList" runat="server" Text="List Serah Terima" Visible="true" OnClick="lstData_Click" CssClass="btn btn-outline-secondary" />
                        </div>
                    </div>
                        
                        
                        
                    <div class="panel-body" style="max-height: 5;">

                        <div style="padding: 2px"></div>
                        <div class="row">
                            <div class="col-md-12">
                                &nbsp;<div class="badge bg-primary text-wrap" style="width: 10rem;">
                                    Priode &nbsp;&nbsp; 
                                </div>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>

                            </div>

                        </div>
                        <div style="padding: 2px"></div>

                        <div id="frm10" runat="server">
                        
                        <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Visible="false" OnClick="btnExport_Click" />
                        <div style="padding: 2px"></div>
                        
                        </div>

                       

                        <div class="contentlist" style="height:485px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                         
                        <table class="table table-responsive table-bordered" style="display: block; height: 500px; overflow: auto;">
                            <tr class="tbHeader">
                                <th class="kotak" style="width: 4%">No.</th>
                                <th class="kotak" style="width: 8%">Tgl PO</th>
                                <th class="kotak" style="width: 15%">Supplier</th>
                                <th class="kotak" style="width: 8%">No.RMS</th>
                                <th class="kotak" style="width: 8%">No.PO</th>
                                <th class="kotak" style="width: 8%">No.SPP</th>
                                <th class="kotak" style="width: 10%">Tgl Kirim</th>
                                <th class="kotak" style="width: 4%">Selisih</th>
                                <th class="kotak" style="width: 10%">Tgl Terima</th>
                                <th class="kotak" style="width: 4%">Sesuai</th>
                                <th class="kotak" style="width: 4%">Tidak</th>
                                <th class="kotak" style="width: 15%">Keterangan</th>
                                <th class="kotak" style="width: 5%"></th>
                            </tr>
                            <tbody>
                                <asp:Repeater ID="lstSerah" runat="server" OnItemCommand="lstSerah_Command" OnItemDataBound="lstSerah_DataBound">
                                    <ItemTemplate>
                                        <tr class="EvenRows baris">
                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                            <td class="kotak tengah"><%# Eval("POPurchnDate","{0:d}") %></td>
                                            <td class="kotak"><%# Eval("SupplierName") %></td>
                                            <td class="kotak tengah"><%# Eval("ReceiptNo") %></td>
                                            <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                            <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtTglKirim" runat="server" Text='<%# Eval("TglKirim","{0:dd/MM/yyyy HH:mm}") %>'></asp:Label></td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtSelisih" runat="server" Text=''></asp:Label></td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtTglterima" runat="server" Text='<%# Eval("TglTerima", "{0:dd/MM/yyyy HH:mm}")%>'></asp:Label></td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtSesuai" CssClass="tengah" runat="server" Text='<%# Eval("Sesuai") %>'></asp:Label></td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtTidak" CssClass="tengah" runat="server" Text='<%# Eval("Tidak") %>'></asp:Label></td>
                                            <td class="kotak">
                                                <asp:HiddenField ID="txtKetOri" runat="server" Value='<%# Eval("Keterangan") %>' />
                                                <asp:TextBox ID="txtKet" runat="server" CssClass="txtongrid" Width="100%" Text='<%# Eval("Keterangan") %>' OnTextChanged="txtKet_Change"></asp:TextBox>
                                                <asp:Label ID="exKeterangan" runat="server" Text='<%# Eval("Keterangan") %>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:ImageButton ID="btnSimpan" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="simpan" ImageUrl="~/img/Save_blue.png" />
                                                <asp:ImageButton ID="btnKirim" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName='kirim' ImageUrl="~/img/delivery.png" />
                                                <asp:ImageButton ID="btnterima" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName='terima' ImageUrl="~/img/clipboard_16.png" />
                                                <asp:ImageButton ID="btnUpd" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="upd" ImageUrl="~/img/disk.jpg" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr class="line3 baris">
                                            <td class="kotak angka" colspan="8">Total Document</td>
                                            <td class="kotak angka">
                                                <asp:Label ID="sTotalData" runat="server"></asp:Label></td>
                                            <td colspan="4" style="background-color: White">&nbsp;</td>
                                        </tr>
                                        <tr class="line3 baris">
                                            <td class="kotak angka" colspan="8">Total Tidak OK</td>
                                            <td class="kotak angka">
                                                <asp:Label ID="stxtTidak" runat="server"></asp:Label></td>
                                            <td colspan="4" style="background-color: White">&nbsp;</td>
                                        </tr>
                                        <tr class="line3 baris">
                                            <td class="kotak angka" colspan="8">Pencapaian(%)</td>
                                            <td class="kotak angka">
                                                <asp:Label ID="stxtSesuai" runat="server"></asp:Label></td>
                                            <td colspan="4" style="background-color: White">&nbsp;</td>
                                        </tr>

                                    </FooterTemplate>
                                </asp:Repeater>

                            </tbody>
                        </table>
                        </div>

                        



                    </div>
                </div>
            </div>
    </body>
</asp:Content>
