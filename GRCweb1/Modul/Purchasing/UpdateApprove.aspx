<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateApprove.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.UpdateApprove" %>

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


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                REVISI PO
                            </div>
                            <div style="padding: 2px"></div>

                            <div id="div1" runat="server">
                                <table style="table-layout: fixed; width: 100%;">
                                    <%--<tr>
                                        <td colspan="5" style="height: 49px">
                                            <!--header tabel-->
                                            <table class="nbTableHeader" width="100%">
                                                <tr>
                                                    <td style="width: 40%"><strong>&nbsp;REVISI PO</strong></td>
                                                    <td style="width: 60%"></td>
                                                </tr>
                                            </table>
                                            <!-- end of header-->
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="5" valign="top" align="left">

                                            <div id="div2" class="">
                                                <table id="headerPO" width="100%" style="border-collapse: collapse;" visible="true" runat="server">
                                                    <tr>
                                                        <td width="10%" align="right">No. PO</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="NoPO" runat="server" OnTextChanged="NoPO_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                        <td width="5%"></td>
                                                        <td width="10%" align="right">PO Date</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="POPurchDate" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%" align="right">Supplier</td>
                                                        <td width="20%" colspan="2" style="width: 25%">
                                                            <asp:DropDownList ID="ddlSupplierID" runat="server"></asp:DropDownList></td>
                                                        <td width="10%" align="right">Termin</td>
                                                        <td width="20%">
                                                            <asp:DropDownList ID="ddlTermin" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlTermin_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtTermOfPay" runat="server"
                                                                ToolTip="Delivery/Shipment(Diharapkan Sampai di Gudang" Visible="False"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%" align="right">PPN</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="ppn" runat="server" Width="62px"></asp:TextBox></td>
                                                        <td width="5%"></td>
                                                        <td width="10%" align="right">PPH</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="pph" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Mata Uang</td>
                                                        <td width="20%">
                                                            <asp:DropDownList ID="ddlMataUang" runat="server"></asp:DropDownList>
                                                        </td>
                                                        <td width="5%">&nbsp;</td>
                                                        <td align="right" width="15%">Nilai Kurs</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="kurs" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Discount</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="Disc" runat="server"></asp:TextBox></td>
                                                        <td width="5%">&nbsp;</td>
                                                        <td align="right" width="15%">Ongkos Kirim</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="ongkos" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Term Of Delivery</td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="TermOfDelivery" runat="server" Width="70%"></asp:TextBox></td>
                                                        <td align="right">Total Price</td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="totalPrice" ReadOnly="true"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Remarks</td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="rmak" runat="server" Width="70%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Keterangan</td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="ket" runat="server" Width="70%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">Approval Status</td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="appstat" runat="server" Width="50px" ReadOnly="true" Visible="true"></asp:TextBox>
                                                            <asp:DropDownList ID="AprovalStatus" runat="server" Visible="false">
                                                                <asp:ListItem Value="0">Admin</asp:ListItem>
                                                                <asp:ListItem Value="1">Head</asp:ListItem>
                                                                <asp:ListItem Value="2">Manager</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="5%">
                                                            <asp:HiddenField ID="txtStatus" runat="server" />
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <asp:TextBox ID="HeadID" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="itemTypeID" runat="server" Visible="false"></asp:TextBox></td>
                                                        <td width="20%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <asp:Button ID="simpan" runat="server" Text="Simpan" OnClick="simpan_Click" />
                                                            <asp:Button ID="delete" runat="server" Text="Hapus" OnClick="delete_Click" /></td>
                                                        <td align="left" colspan="2">
                                                            <asp:Button ID="Batal" runat="server" Text="Batal" Visible="false" /></td>
                                                    </tr>
                                                </table>
                                                <hr />
                                                <!-- Detail form-->

                                                <table id="txtDetailPO" width="100%" style="border-collapse: collapse;" runat="server" visible="false">
                                                    <tr>
                                                        <td width='15%'>&nbsp;&nbsp;Nama Barang</td>
                                                        <td width='35%'>
                                                            <asp:TextBox ID="nmbarang" runat="server" Width="100%"
                                                                OnTextChanged="nmbarang_TextChanged"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="itemID" runat="server" Visible="false"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width='15%'>Pilih Nama Barang</td>
                                                        <td width='15%'>
                                                            <asp:DropDownList ID="KodeBarang" runat="server" Width="250px"></asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox ID="DetailID" runat="server" Visible="false"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width='15%'>Satuan</td>
                                                        <td width='15%'>
                                                            <asp:DropDownList ID="ddlSatuan" runat="server"></asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox ID="itemTpID" runat="server" Visible="false"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width='15%'>Quantity</td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="Qty" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <span id="txt" runat="server" style="font-style: italic; color: Red; font-size: smaller"></span></td>
                                                    </tr>
                                                    <tr>
                                                        <td width='10%'>Price</td>
                                                        <td width='15%'>
                                                            <asp:TextBox ID="Price" runat="server"></asp:TextBox></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td width='10%'>Delivery Date</td>
                                                        <td width='15%'>
                                                            <asp:TextBox ID="DelDate" runat="server"></asp:TextBox></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2">
                                                            <asp:Button ID="DetailUp" runat="server" Text="Simpan" OnClick="DetailUp_Click" />
                                                            <asp:Button ID="DetailCn" runat="server" Text="Batal" OnClick="DetailCn_Click" /></td>
                                                        <td align="left"></td>
                                                    </tr>
                                                </table>
                                                <div class="contentlist" style="height: 215px">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                        <thead>
                                                            <tr class="tbHeader">
                                                                <th width="5%" style="border: 1px solid;">ID</th>
                                                                <th width="10%" style="border: 1px solid;">ItemCode</th>
                                                                <th width="35%" style="border: 1px solid;">ItemName</th>
                                                                <th width="10%" style="border: 1px solid;">UoM</th>
                                                                <th width="10%" style="border: 1px solid;">PO. Qty</th>
                                                                <th width="10%" style="border: 1px solid;">Price</th>
                                                                <th width="10%" style="border: 1px solid;">Dlv.Date</th>
                                                                <th width="5%" style="border: 1px solid;">&nbsp;</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="detailPO" runat="server" OnItemCommand="detailPO_ItemCommand" OnItemDataBound="detailPO_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr style="background-color: #B0C0E0">
                                                                        <td align="center" style="border: 1px solid;"><%# Eval("ID")%></td>
                                                                        <td align="center" colspan="0" style="border: 1px solid;"><%# Eval("ItemCode")%></td>
                                                                        <td align="left" style="border: 1px solid;"><%# Eval("NamaBarang")%> </td>
                                                                        <td align="left" style="border: 1px solid;"><%# Eval("Satuan")%></td>
                                                                        <td align="right" style="border: 1px solid;"><%# Eval("Qty", "{0:#,##0.00}")%></td>
                                                                        <td align="right" style="border: 1px solid;"><%# Eval("Price", "{0:#,##0.000}")%></td>
                                                                        <td align="center" style="border: 1px solid;"><%# ((int)Eval("Status") >-1)? Eval("DlvDate", "{0:d}"): Eval("Status")%></td>
                                                                        <td style="border: 1px solid;" align="center">
                                                                            <asp:Label ID="Act" runat="server">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/images/folder.gif" ID="btnedit" CommandArgument='<%# Eval("ID")%>' CommandName="Edit" />
                                                                                <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus" CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" /></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr style="background-color: #FFF0D0">
                                                                        <td align="center" style="border: 1px solid;"><%# Eval("ID")%></td>
                                                                        <td align="center" colspan="0" style="border: 1px solid;"><%# Eval("ItemCode")%></td>
                                                                        <td align="left" style="border: 1px solid;"><%# Eval("NamaBarang")%> </td>
                                                                        <td align="left" style="border: 1px solid;"><%# Eval("Satuan")%></td>
                                                                        <td align="right" style="border: 1px solid;"><%# Eval("Qty", "{0:#,##0.00}")%></td>
                                                                        <td align="right" style="border: 1px solid;"><%# Eval("Price", "{0:#,##0.000}")%></td>
                                                                        <td align="center" style="border: 1px solid;"><%# ((int)Eval("Status") >-1)? Eval("DlvDate", "{0:d}"): Eval("Status")%></td>
                                                                        <td style="border: 1px solid;" align="center">
                                                                            <asp:Label ID="Act" runat="server">
                                                                                <asp:ImageButton runat="server" ImageUrl="~/images/folder.gif" ID="btnedit" CommandArgument='<%# Eval("ID")%>' CommandName="Edit" />
                                                                                <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus" CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" />
                                                                            </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>

                                                            </asp:Repeater>

                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>

                                </table>

                                <%--extender 1--%>
                            <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
                            <cc1:ModalPopupExtender ID="mpePopUp" runat="server"
                                TargetControlID="lblHidden"
                                PopupControlID="panEdit"
                                CancelControlID="btnUpdateClose"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>


                            <asp:Panel ID="panEdit" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                <table style="table-layout: fixed; height: 100%" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; height: 49px" bgcolor="gray">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Label ID="Label3" runat="server" Text="ALASAN REVISI" Font-Bold="True" Font-Names="Verdana"
                                                                Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                        </td>

                                                        <td style="width: 37px">
                                                            <input id="btnUpdateClose" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose_ServerClick" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdateAlasan" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan_ServerClick" />
                                                        </td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="100%" style="width: 100%">
                                                <div style="overflow: hidden; height: 100%; width: 100%;" class="content">
                                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="height: 6px; width: 100%;" valign="top">
                                                                <asp:TextBox ID="txtAlasanCancel" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>

                            <%-- extender 2--%>


                            <asp:Label ID="lblHidden1" runat="server" Text=""></asp:Label>
                            <cc1:ModalPopupExtender ID="mpePopUp1" runat="server"
                                TargetControlID="lblHidden1"
                                PopupControlID="panEdit1"
                                CancelControlID="btnUpdateClose1"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>


                            <asp:Panel ID="panEdit1" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                <table style="table-layout: fixed; height: 100%" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; height: 49px" bgcolor="gray">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Label ID="Label2" runat="server" Text="ALASAN REVISI" Font-Bold="True" Font-Names="Verdana"
                                                                Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                        </td>

                                                        <td style="width: 37px">
                                                            <input id="btnUpdateClose1" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose1_ServerClick" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdateAlasan1" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan1_ServerClick" />
                                                        </td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="100%" style="width: 100%">
                                                <div style="overflow: hidden; height: 100%; width: 100%;" class="content">
                                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="height: 6px; width: 100%;" valign="top">
                                                                <asp:TextBox ID="txtAlasanCancel1" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>

                            <%--extender 3--%>
                            <asp:Label ID="lblHidden2" runat="server" Text=""></asp:Label>
                            <cc1:ModalPopupExtender ID="mpePopUp2" runat="server"
                                TargetControlID="lblHidden2"
                                PopupControlID="panEdit2"
                                CancelControlID="btnUpdateClose2"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>


                            <asp:Panel ID="panEdit2" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                <table style="table-layout: fixed; height: 100%" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; height: 49px" bgcolor="gray">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Label ID="Label4" runat="server" Text="ALASAN REVISI" Font-Bold="True" Font-Names="Verdana"
                                                                Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                        </td>

                                                        <td style="width: 37px">
                                                            <input id="btnUpdateClose2" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose2_ServerClick" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdateAlasan2" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan2_ServerClick" />
                                                        </td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="100%" style="width: 100%">
                                                <div style="overflow: hidden; height: 100%; width: 100%;" class="content">
                                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="height: 6px; width: 100%;" valign="top">
                                                                <asp:TextBox ID="txtAlasanCancel2" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>

                            <%--extender 4--%>
                            <asp:Label ID="lblHidden3" runat="server" Text=""></asp:Label>
                            <cc1:ModalPopupExtender ID="mpePopUp3" runat="server"
                                TargetControlID="lblHidden3"
                                PopupControlID="panEdit3"
                                CancelControlID="btnUpdateClose3"
                                BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>


                            <asp:Panel ID="panEdit3" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                <table style="table-layout: fixed; height: 100%" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; height: 49px" bgcolor="gray">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Label ID="Label5" runat="server" Text="ALASAN REVISI" Font-Bold="True" Font-Names="Verdana"
                                                                Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                        </td>

                                                        <td style="width: 37px">
                                                            <input id="btnUpdateClose3" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose3_ServerClick" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdateAlasan3" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan3_ServerClick" />
                                                        </td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="100%" style="width: 100%">
                                                <div style="overflow: hidden; height: 100%; width: 100%;" class="content">
                                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td style="height: 6px; width: 100%;" valign="top">
                                                                <asp:TextBox ID="txtAlasanCancel3" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <hr />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>


                            </div>

                            

                        </div>

                        <script src="../../assets/jquery.js" type="text/javascript"></script>
                        <script src="../../assets/js/jquery-ui.min.js"></script>
                        <script src="../../assets/select2.js"></script>
                        <script src="../../assets/datatable.js"></script>
                        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
                </body>
                </html>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

