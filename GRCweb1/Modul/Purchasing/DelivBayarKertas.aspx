﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivBayarKertas.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivBayarKertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetKey(source, eventArgs) {
            $('#<%=txtSupplierID.ClientID %>').val(eventArgs.get_value());
    }
    </script>
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
        function CetakFrom(docno) {
            params = 'width=1024px';
            params += ', height=600px';
            params += ', top=20px, left=20px';
            params += ',scrollbars=1';
            //window.showModalDialog
            window.open("../../ModalDialog/FromBeliQA.aspx?ka=" + docno, "Preview", params);
        }
    </script>
    <script type="text/javascript">
        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function Cetak() {
            MyPopUpWin("../Report/Report.aspx?IdReport=bayarkertas", 900, 800)
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse;">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 40%; padding-left: 10px">INPUT PEMBAYARAN KERTAS</td>
                                    <td style="width: 60%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnCetak" runat="server" Text="Cetak"
                                            OnClick="btnCetak_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnList" runat="server" Text="List Pembayaran" OnClick="btnList_Click" />
                                        &nbsp;&nbsp;<asp:DropDownList ID="ddlcari" runat="server" AutoPostBack="true">
                                            <asp:ListItem>No.Document</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtCari" runat="server" Width="20%" ToolTip="Cari by No SJ"></asp:TextBox><asp:Button
                                            ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; margin-top: 5px">
                                    <tr>
                                        <td style="padding-left: 10px" colspan="3">
                                            <asp:RadioButton ID="RBayarFull" runat="server" Checked="True" GroupName="a"
                                                Text="Pembayaran Tanpa DP" AutoPostBack="True"
                                                OnCheckedChanged="RBayarFull_CheckedChanged" />
                                            &nbsp;<asp:RadioButton ID="RBayarDP" runat="server" GroupName="a"
                                                Text="Pembayaran DP" AutoPostBack="True"
                                                OnCheckedChanged="RBayarDP_CheckedChanged" Checked="True" />
                                            &nbsp;<asp:RadioButton ID="RBayarPelunasan" runat="server" AutoPostBack="True"
                                                GroupName="a" OnCheckedChanged="RBayarPelunasan_CheckedChanged"
                                                Text="Pelunasan Pembayaran" />
                                            &nbsp;</td>
                                        <td colspan="3" style="padding-left: 10px">
                                            <asp:Panel ID="Panel3" runat="server" Visible="False">
                                                No. BBK&nbsp;
                                                <asp:DropDownList ID="ddlDocNo" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDocNo_SelectedIndexChanged" Width="60%">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; padding-left: 10px">Depo
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlDepo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDepo_Change" Width="60%">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="txtID" runat="server" Value="0" />
                                            <asp:HiddenField ID="txtSupplierID" runat="server"
                                                OnValueChanged="txtBayarKpd_TextChanged" Value="0" />
                                        </td>
                                        <td style="width: 11%; padding-left: 5px">Tujuan Kirim</td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlTujuanKirim" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTujuanKirim_Change" Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>No. Document
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server" ReadOnly="true" Width="141px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">Tanggal Bayar
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTglBayar" runat="server" AutoPostBack="true" OnTextChanged="txtTglKirim_Change"
                                                Text="" Width="60%"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Ca1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglBayar"></cc1:CalendarExtender>
                                        </td>
                                        <td style="padding-left: 5px">An. BG. No</td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtAnBGNo" runat="server" AutoPostBack="false" Width="141px"></asp:TextBox>
                                        </td>
                                        <td>Tgl Jatuh Tempo</td>
                                        <td>
                                            <asp:TextBox ID="txtTglJTempo" runat="server" AutoPostBack="true"
                                                OnTextChanged="txtTglKirim_Change" Text="" Width="141px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtTglJTempo_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtTglJTempo"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; height: 12px;">Bayar Kepada
                                        </td>
                                        <td style="height: 12px">
                                            <asp:TextBox ID="txtBayarKpd" runat="server" AutoPostBack="true"
                                                Width="100%" OnTextChanged="txtBayarKpd_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="act1" runat="server" CompletionInterval="100"
                                                CompletionListCssClass="autocomplete_completionListElement"
                                                EnableCaching="true" MinimumPrefixLength="2" OnClientItemSelected="GetKey"
                                                ServiceMethod="GetSupplierKAT" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtBayarKpd">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td style="padding-left: 5px; height: 12px;">&nbsp;BG. No
                                        </td>
                                        <td align="justify" style="height: 12px; width: 25%;">
                                            <asp:TextBox ID="txtBGNo" runat="server" AutoPostBack="false" Text=""
                                                Width="141px"></asp:TextBox>
                                        </td>
                                        <td style="height: 12px">Total Bayar
                                        </td>
                                        <td style="height: 12px">
                                            <asp:TextBox ID="txtJumlahBayar" runat="server" AutoPostBack="true" OnTextChanged="txtTglKirim_Change"
                                                Text="" Width="141px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; height: 12px;">&nbsp;</td>
                                        <td style="height: 12px">&nbsp;</td>
                                        <td style="padding-left: 5px; height: 12px;">&nbsp;</td>
                                        <td align="right" style="height: 12px;" colspan="2">Pembayaran DP / Deposit DP :&nbsp; </td>
                                        <td style="height: 12px">
                                            <asp:Label ID="txtDP" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="contentlist" style="height: 150px;">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">

                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" colspan="15">List Pembayaran
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 3%">No.
                                                </th>
                                                <th class="kotak" style="width: 8%">Depo
                                                </th>
                                                <th class="kotak" style="width: 11%">Supplier
                                                </th>
                                                <th class="kotak" style="width: 10%">Item Name
                                                </th>
                                                <th class="kotak" style="width: 4%">Quantity
                                                </th>
                                                <th class="kotak" style="width: 7%">Jml. Bayar
                                                </th>
                                                <th class="kotak" style="width: 11%">Bayar Kpd
                                                </th>
                                                <th class="kotak" style="width: 6%">Tgl Bayar
                                                </th>
                                                <th class="kotak" style="width: 6%">Tgl J Tempo
                                                </th>
                                                <th class="kotak" style="width: 5%">BG. No.
                                                </th>
                                                <th class="kotak" style="width: 4%">Min.Kirim
                                                </th>
                                                <th class="kotak" style="width: 4%">Tot.Kirim
                                                </th>
                                                <th class="kotak" style="width: 4%">Harga
                                                </th>
                                                <th class="kotak" style="width: 4%">Harga +
                                                </th>
                                                <th class="kotak" style="width: 2%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstDepo" runat="server" OnItemDataBound="lstDepo_DataBound"
                                                OnItemCommand="lstDepo_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="xx" runat="server">
                                                        <td class="kotak tengah">
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("DepoName") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("SupplierName") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("Itemname") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("qty", "{0:N0}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="txttotalharga" runat="server" Text='<%#Eval("totalharga","{0:N0}") %>'></asp:Label>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("Penerima") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("Tglbayar","{0:d}") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("Jtempo", "{0:d}")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("bgno") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("MinKirim") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("TKirim") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("Harga") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("HargaT") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="edt" runat="server" ToolTip="Edit Item" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="edit" ImageUrl="~/images/folder.gif" />
                                                            <asp:ImageButton ID="hps" runat="server" ToolTip="Delete Item" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="delet" ImageUrl="~/images/trash.gif" />
                                                            <asp:ImageButton ID="del" runat="server" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="hapus" ImageUrl="~/images/Delete.png" />
                                                            <asp:ImageButton ID="sts" runat="server" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="tStatus" ImageUrl="~/images/po.png" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="contentlist" style="height: 200px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">

                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" colspan="14">List Pembelian
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 4%">No
                                                </th>
                                                <th class="kotak" style="width: 8%">Doc. No
                                                </th>
                                                <th class="kotak" style="width: 8%">No. PO
                                                </th>
                                                <th class="kotak" style="width: 12%">No. SJ
                                                </th>
                                                <th class="kotak" style="width: 8%">Tanggal
                                                </th>
                                                <th class="kotak" style="width: 15%">Supplier
                                                </th>
                                                <th class="kotak" style="width: 5%">Checker
                                                </th>
                                                <th class="kotak" style="width: 15%">ItemName
                                                </th>
                                                <th class="kotak" style="width: 5%">Gross
                                                </th>
                                                <th class="kotak" style="width: 5%">Kadar air
                                                </th>
                                                <th class="kotak" style="width: 4%">Sampah
                                                </th>
                                                <th class="kotak" style="width: 5%">Netto
                                                </th>
                                                <th class="kotak" style="width: 5%">Jml BAL
                                                </th>
                                                <th class="kotak" style="width: 4%">&nbsp;
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstKA" runat="server" OnItemDataBound="lstKA_DataBound" OnItemCommand="lstKA_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:CheckBox ID="chk" runat="server" Visible="true" ToolTip='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("DocNo") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("NoPO") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("NoSJ") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("TglCheck","{0:d}") %>
                                                        </td>
                                                        <td class="kotak" style="white-space: nowrap">
                                                            <%# Eval("SupplierName").ToString().ToUpper() %>
                                                        </td>
                                                        <td class="kotak" style="white-space: nowrap">
                                                            <%# Eval("Checker").ToString().ToUpper() %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("ItemName") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("GrossPlant","{0:N0}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("AvgKA","{0:N2}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("Sampah","{0:N2}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("NettPlant", "{0:N0}")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("JmlBAL","{0:N0}") %><asp:Label ID="sts" runat="server" Text='<%# Eval("Keputusan") %>'
                                                                Visible="false"></asp:Label></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="btnPrev" runat="server" CommandArgument='<%=Eval("ID %>' ToolTip="Click For Preview Detail"
                                                                CommandName="prev" ImageUrl="~/images/clipboard_16.png" />
                                                            <asp:ImageButton ID="btnApp" runat="server" CommandArgument='<%=Eval("ID %>' ToolTip="Click For Preview Detail"
                                                                CommandName="prev" ImageUrl="~/images/Approved_16.png" />
                                                            <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                ToolTip='<%# Eval("DocNo") %>' CommandName="prn" ImageUrl="~/images/printer_small.png" />
                                                            <asp:ImageButton ID="btnPO" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip='Kirim data pembelian'
                                                                CommandName="po" ImageUrl="~/images/editor.png" Visible="false" />
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
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function closed() {
            $('#popcontent').html('');
            $('#popup').hide();
            $('#bgr').hide();
        }
    </script>
</asp:Content>
