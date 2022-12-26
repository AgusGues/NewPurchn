<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormMemoHarian.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormMemoHarian" %>

<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
  // fix for deprecated method in Chrome / js untuk bantu view modal dialog
  if (!window.showModalDialog) {
     window.showModalDialog = function (arg1, arg2, arg3) {
        var w;
        var h;
        var resizable = "no";
        var scroll = "no";
        var status = "no";
        // get the modal specs
        var mdattrs = arg3.split(";");
        for (i = 0; i < mdattrs.length; i++) {
           var mdattr = mdattrs[i].split(":");
           var n = mdattr[0];
           var v = mdattr[1];
           if (n) { n = n.trim().toLowerCase(); }
           if (v) { v = v.trim().toLowerCase(); }
           if (n == "dialogheight") {
              h = v.replace("px", "");
           } else if (n == "dialogwidth") {
              w = v.replace("px", "");
           } else if (n == "resizable") {
              resizable = v;
           } else if (n == "scroll") {
              scroll = v;
           } else if (n == "status") {
              status = v;
           }
        }
        var left = window.screenX + (window.outerWidth / 2) - (w / 2);
        var top = window.screenY + (window.outerHeight / 2) - (h / 2);
        var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        targetWin.focus();
     };
  }
</script>

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


        <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

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

        <script type="text/javascript">
            function openWindow(id) {
                window.showModalDialog("../../ModalDialog/POParsialSch.aspx?p=" + id, "PO Defined", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
            }
            function updateDO(id) {
                window.showModalDialog("../../ModalDialog/PODOEdit.aspx?p=" + id, "DO Update", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
            }
            function UpdateMobil(id) {
                window.showModalDialog("../../ModalDialog/ArmadaEdit.aspx?p=" + id, "Mobil Update", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
            }
            function openWindow2(id) {
                window.showModalDialog("../../ModalDialog/POParsialArmada.aspx?p=" + id, "", "resizable:yes;dialogHeight: 520px; dialogWidth: 867px;scrollbars:no;addressbar:yes");
            }
            function Cetak() {
                var wn = window.showModalDialog("../Report/Report.aspx?IdReport=DO", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
            }
            function HapusData() {
                if (confirm("Yakin akan di hapus data ini?") == true) {
                    window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');

                } else {
                    return false;
                }
            }
            function updateSch(id) {
                window.showModalDialog("../../ModalDialog/POParsialUpdate.aspx?p=" + id, "SCH Update", "resizable:yes;dialogHeight: 250px; dialogWidth: 860px;scrollbars:no;");
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
                                PARSIAL DELIVERY SCHEDULE 
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="table-layout: fixed; width: 100%; height: 100%">
                                    <tbody>
                                        <tr>
                                            <td style="height: 49px; width: 100%">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <%-- <td style="width: 30%">
                                                            <strong>&nbsp;PARSIAL DELIVERY SCHEDULE</strong>
                                                        </td>--%>
                                                        <td style="width: 70%; padding-right: 5px; text-align: right">
                                                            <asp:Button ID="btnNew" runat="server" Text="New Schedule" OnClick="btnNew_Click" />
                                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                            <asp:Button ID="btnListSch" runat="server" Text="List" OnClick="btnListSch_Click" />
                                                            <asp:DropDownList ID="ddlCari" runat="server">
                                                                <asp:ListItem Value="ItemName">ItemName</asp:ListItem>
                                                                <asp:ListItem Value="SupplierName">Supplier Name</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtCari" runat="server" Width="200px"></asp:TextBox>
                                                            <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 100%" valign="top">
                                                <div class="content">
                                                    <asp:Panel ID="formInput" runat="server">
                                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                            <tr>
                                                                <td style="width: 10%">&nbsp;
                                                                </td>
                                                                <td style="width: 10%">Schedule No.
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:TextBox ID="txtSchNo" runat="server" Enabled="false"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlPoNo" runat="server" Width="70%" OnTextChanged="ddlPoNo_Click"
                                                                        AutoPostBack="true" Visible="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 4%">&nbsp;
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                                <td style="width: 20%"></td>
                                                                <td style="width: 10%">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td></td>
                                                                <td>Supplier Name
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSupplier" runat="server" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>Item Name
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMaterial" runat="server" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td></td>
                                                                <td>Estimasi Qty
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOutPO" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>Jumlah Mobil
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="true" OnTextChanged="txt_onChange"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                                <td>Delivery Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSchDate" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td valign="top">Keterangan
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtKeterangan" runat="server" Rows="3" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <cc1:CalendarExtender ID="CA1" runat="server" TargetControlID="txtSchDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Tambah" OnClick="btnAdd_Click" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                                                        Visible="false" />
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                        <hr />
                                                        <div class="contentlist" style="height: 365px">
                                                            <table style="border-collapse: collapse; width: 100%; font-size: x-small">
                                                                <thead>
                                                                    <tr class="tbHeader baris">
                                                                        <th class="kotak" style="width: 5%">No
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%; display: none">No.PO
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%; display: none">Supplier
                                                                        </th>
                                                                        <th class="kotak" style="width: 35%">Item Name
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">Jml Mobil
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">Quantity
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">Sch Delivery
                                                                        </th>
                                                                        <th class="kotak" style="width: 25%">Keterangan
                                                                        </th>
                                                                        <th class="kotak" style="width: 5%">&nbsp;
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="lstSch" runat="server" OnItemDataBound="lstSch_DataBound" OnItemCommand="lstSch_Command">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris">
                                                                                <td class="kotak tengah">
                                                                                    <%# Container.ItemIndex+1 %>
                                                                                </td>
                                                                                <td class="kotak tengah" style="display: none">
                                                                                    <%# Eval("NoPO") %>
                                                                                </td>
                                                                                <td class="kotak" style="display: none">
                                                                                    <%# Eval("SupplierName") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("ItemName") %>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                    <%# Eval("Qty","{0:N2}") %>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                    <%# Eval("EstQty","{0:N2}") %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("DlvDate","{0:d}") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("Keterangan") %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                                        CommandName="edit" />
                                                                                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>'
                                                                                        CommandName="delx" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="lstMemo" runat="server" Visible="false">
                                                        <table style="width: 100%; border-collapse: collapse; margin-top: 5px">
                                                            <tr>
                                                                <td style="width: 15%"></td>
                                                                <td style="width: 10%">Periode
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtDariTgl" runat="server"></asp:TextBox>
                                                                    s/d
                                                    <asp:TextBox ID="txtSampaiTgl" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                                    <cc1:CalendarExtender ID="caex1" runat="server" TargetControlID="txtDariTgl" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                                    <cc1:CalendarExtender ID="caex2" runat="server" TargetControlID="txtSampaiTgl" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                                                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <hr />
                                                        <div class="contentlist" style="height: 470px; padding: 5px; overflow: auto" id="lst"
                                                            runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                                            <table style="border-collapse: collapse; width: 100%; font-size: x-small" border="0">
                                                                <thead>
                                                                    <tr class="tbHeader baris">
                                                                        <th class="kotak" style="width: 3%">No
                                                                        </th>
                                                                        <th class="kotak" style="width: 25%">Item Name
                                                                        </th>
                                                                        <th class="kotak" style="width: 7%">JmlMobil
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">Quantity
                                                                        </th>
                                                                        <th class="kotak" style="width: 5%">Schedule
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">Delivery
                                                                        </th>
                                                                        <th class="kotak" style="width: 18%">Keterangan
                                                                        </th>
                                                                        <th class="kotak" style="width: 5%">&nbsp;
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="lstMemoHarian" runat="server" OnItemDataBound="lstMemoHarian_DataBound"
                                                                        OnItemCommand="lstMemoHarian_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <!--list dari logistic-->
                                                                            <tr class="EvenRows baris" id="xx" runat="server">
                                                                                <td class="kotak tengah">
                                                                                    <%# Container.ItemIndex+1 %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("Itemname") %>
                                                                                </td>
                                                                                <td class="kotak ">
                                                                                    <%# Eval("Qty","{0:N2}") %>
                                                                                    <table style="border-collapse: collapse; width: 100%; font-size: x-small; display: none">
                                                                                        <tr>
                                                                                            <td style="width: 90%">
                                                                                                <asp:TextBox ID="txtQtyMinta" Width="100%" runat="server" Text='<%# Eval("Qty","{0:N2}") %>'
                                                                                                    CssClass="txtongrid tengah" ReadOnly="true"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                                <asp:ImageButton ID="qtyUpd" ImageUrl="~/images/folder.gif" runat="server" CommandName="updQty"
                                                                                                    CommandArgument='<%# Eval("ID") %>' />
                                                                                                <asp:ImageButton ID="qtySim" Visible="false" ImageUrl="~/images/disk.jpg" runat="server"
                                                                                                    CommandName="simQty" CommandArgument='<%# Eval("ID") %>' />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td class="kotak ">
                                                                                    <%# Eval("EstQty","{0:N2}") %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("DlvDate","{0:d}") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <asp:Panel ID="noArmada" runat="server">
                                                                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                                                            <tr>
                                                                                                <td style="width: 85%">
                                                                                                    <%# Eval("Armada") %>
                                                                                                </td>
                                                                                                <td style="width: 15%" class="tengah">
                                                                                                    <asp:ImageButton ID="editArm" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                                                        CommandName="add" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="Armada" runat="server" Visible="false">
                                                                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                                                            <tr>
                                                                                                <td style="width: 85%">
                                                                                                    <asp:CheckBoxList ID="ddlArmada" runat="server" Width="100%">
                                                                                                    </asp:CheckBoxList>
                                                                                                </td>
                                                                                                <td style="width: 15%" class="tengah">
                                                                                                    <asp:ImageButton ID="saveArm" runat="server" ImageUrl="~/images/disk.jpg" CommandArgument='<%# Eval("ID") %>'
                                                                                                        CommandName="save" />
                                                                                                    <asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/images/Close.gif" CommandArgument='<%# Eval("ID") %>'
                                                                                                        CommandName="batal" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("Keterangan") %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <asp:ImageButton ID="mmEdit" runat="server" ImageUrl="~/images/editor.png" CommandArgument='<%# Eval("ID") %>'
                                                                                        CommandName="scEdit" />
                                                                                    <asp:ImageButton ID="schEdit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                                        CommandName="edit" />
                                                                                    <asp:ImageButton ID="schDel" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>'
                                                                                        CommandName="del" />
                                                                                </td>
                                                                            </tr>
                                                                            <!--PO by Purchasing dan pengaturan mobil Mobil by Armada-->
                                                                            <asp:Repeater ID="lstPOne" runat="server" OnItemDataBound="lstPOne_DataBound" OnItemCommand="lstPOne_Command">
                                                                                <ItemTemplate>
                                                                                    <tr class="OddRows baris">
                                                                                        <td class="kotak">&nbsp;
                                                                                        </td>
                                                                                        <td class="kotak angka">
                                                                                            <%# Eval("NoPO") %>
                                                                            -
                                                                            <%# Eval("SupplierName") %>&nbsp;&nbsp;
                                                                                        </td>
                                                                                        <td class="kotak angka">
                                                                                            <%# Eval("Qty") %>
                                                                                        </td>
                                                                                        <td class="kotak angka">
                                                                                            <%# Eval("EstQty","{0:N2}") %>
                                                                                        </td>
                                                                                        <td class="kotak"></td>
                                                                                        <td class="kotak" style="white-space: nowrap">
                                                                                            <%# Eval("Delivery").ToString().ToUpper() %>
                                                                                        </td>
                                                                                        <td class="kotak">
                                                                                            <%# Eval("Keterangan") %>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <asp:ImageButton ID="EditPO" runat="server" ToolTip="EditPO" ImageUrl="~/images/clipboard_16.png"
                                                                                                CommandArgument='<%# Eval("ID") %>' CommandName="editPO" />
                                                                                            <asp:ImageButton ID="armAdd" runat="server" ToolTip="Tentukan Mobil" ImageUrl="~/images/delivery.png"
                                                                                                CommandArgument='<%# Eval("ID") %>' CommandName="addArm" />
                                                                                            <asp:ImageButton ID="armDel" runat="server" ToolTip="Delete data PO" ImageUrl="~/images/Delete.png"
                                                                                                CommandArgument='<%# Eval("ID") %>' CommandName="delArm" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <asp:Repeater ID="lstArmada" runat="server" OnItemDataBound="lstArmada_DataBound"
                                                                                        OnItemCommand="lstArmada_ItemCommand">
                                                                                        <ItemTemplate>
                                                                                            <tr class="Line3 baris" style="color: #00008B">
                                                                                                <td class="kotak">&nbsp;
                                                                                                </td>
                                                                                                <td class="kotak angka">DO Number :
                                                                                    <%# Eval("DoNum") %>&nbsp;
                                                                                                </td>
                                                                                                <td class="kotak">
                                                                                                    <%# Eval("NoPol") %>
                                                                                                </td>
                                                                                                <td class="kotak">Rit Ke :
                                                                                    <%# Eval("Ritase") %>
                                                                                                </td>
                                                                                                <td class="kotak tengah">
                                                                                                    <%# Eval("Jam") %>
                                                                                                </td>
                                                                                                <td class="kotak">
                                                                                                    <%# Eval("Driver") %>
                                                                                                </td>
                                                                                                <td class="kotak">
                                                                                                    <%# Eval("Keterangan") %>
                                                                                                </td>
                                                                                                <td class="kotak tengah">
                                                                                                    <asp:ImageButton ID="armEdit" runat="server" ToolTip="Edit data" ImageUrl="~/images/folder_16.png"
                                                                                                        CommandArgument='<%# Eval("ID") %>' CommandName="editarm" />
                                                                                                    <asp:ImageButton ID="armDelet" runat="server" ToolTip="Delete DO" ImageUrl="~/images/Delete.png"
                                                                                                        CommandArgument='<%# Eval("ID") %>' CommandName="deldlv" />
                                                                                                    <asp:ImageButton ID="armPrint" runat="server" ToolTip="Print DO" ImageUrl="~/images/printer_small.png"
                                                                                                        CommandArgument='<%# Eval("ID") %>' CommandName="print" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
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
