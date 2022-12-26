<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivKirimKertas0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivKirimKertas0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
     function confirm_Delete() {
         if (confirm("Anda yakin ingin menghapus Surat Jalan ?") !=true)
             return false;
         
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
                                    <td style="width: 40%; padding-left: 10px">INPUT PENGIRIMAN KERTAS
                                    </td>
                                    <td style="width: 60%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="List Pengiriman" OnClick="btnList_Click" />
                                        &nbsp;<asp:Button ID="btnHapus" runat="server" OnClick="btnHapus_ServerClick" Text="Hapus" />
                                        <asp:TextBox ID="txtCari" runat="server" Width="40%" ToolTip="Cari by No SJ"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
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
                                        <td style="width: 10%; padding-left: 10px">Depo
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlDepo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDepo_Change" Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%; padding-left: 5px">Item Name</td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>

                                            <asp:HiddenField ID="txtSampah" runat="server" Value="0" />
                                        </td>
                                        <td>

                                            <asp:HiddenField ID="txtID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; padding-left: 10px">Tujuan Kirim</td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlTujuanKirim" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTujuanKirim_Change" Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%; padding-left: 5px">Jenis Mobil</td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlTypeMobil" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTujuanKirim_Change" Width="60%">
                                                <asp:ListItem>Fuso</asp:ListItem>
                                                <asp:ListItem>Tronton</asp:ListItem>
                                                <asp:ListItem>COLT DIESEL</asp:ListItem>
                                                <asp:ListItem>CARRY L 300</asp:ListItem>
                                                <asp:ListItem>GRAND MAX</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>No Document</td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">&nbsp;Lokasi Muat</td>
                                        <td>
                                            <asp:DropDownList ID="ddlLokasiMuat" runat="server" AutoPostBack="true"
                                                Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding-left: 5px">Tanggal Kirim
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtTglKirim" runat="server" AutoPostBack="true"
                                                OnTextChanged="txtTglKirim_Change" Text="" Width="120px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Ca1" runat="server" TargetControlID="txtTglKirim" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                        </td>
                                        <td>Estimasi Tiba</td>
                                        <td>
                                            <asp:TextBox ID="txtETA" runat="server" Width="120px" AutoPostBack="True"
                                                OnTextChanged="txtETA_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtETA" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; height: 12px;">No. SJ Depo</td>
                                        <td style="height: 12px">
                                            <asp:TextBox ID="txtNoSJ" runat="server" AutoPostBack="false"
                                                CssClass="txtUpper" OnTextChanged="txtNoSJ_Change" Width="100%"></asp:TextBox>
                                        </td>
                                        <td style="padding-left: 5px; height: 12px;">Expedisi</td>
                                        <td align="justify" style="height: 12px; width: 25%;">
                                            <asp:DropDownList ID="ddlExpedisi" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlExpedisi_SelectedIndexChanged" Width="100%">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtExpedisi" runat="server" AutoPostBack="false"
                                                CssClass="txtUpper" OnTextChanged="txtExpedisi_Change" Width="100%"
                                                Visible="False"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="act2" runat="server" CompletionInterval="100"
                                                CompletionListCssClass="autocomplete_completionListElement"
                                                EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetExpedisi"
                                                ServicePath="AutoComplete.asmx" TargetControlID="txtExpedisi">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td style="height: 12px">Plat Mobil</td>
                                        <td style="height: 12px">
                                            <asp:TextBox ID="txtNOPOL" runat="server" AutoPostBack="false"
                                                CssClass="txtUpper" OnTextChanged="txtNOPOL_Change" Width="120px"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="mms2" runat="server" ClearMaskOnLostFocus="false"
                                                Mask="$$-9999-$$$" MaskType="None" TargetControlID="txtNOPOL"></cc1:MaskedEditExtender>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="contentlist" style="height: 150px;">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" colspan="16">List Pengiriman</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 3%">No.
                                                </th>
                                                <th class="kotak" style="width: 8%">Depo
                                                </th>
                                                <th class="kotak" style="width: 11%">Supplier
                                                </th>
                                                <th class="kotak" style="width: 7%">Checker
                                                </th>
                                                <th class="kotak" style="width: 6%">Tgl Kirim
                                                </th>
                                                <th class="kotak" style="width: 6%">Estimasi Tiba
                                                </th>
                                                <th class="kotak" style="width: 5%">Tujuan
                                                </th>
                                                <th class="kotak" style="width: 10%">No SJ
                                                </th>
                                                <th class="kotak" style="width: 9%">Expedisi
                                                </th>
                                                <th class="kotak" style="width: 7%">PlatMobil
                                                </th>
                                                <th class="kotak" style="width: 6%">Gross
                                                </th>
                                                <th class="kotak" style="width: 6%">KadarAir
                                                </th>
                                                <th class="kotak" style="width: 6%">Sampah
                                                </th>
                                                <th class="kotak" style="width: 6%">Netto
                                                </th>
                                                <th class="kotak" style="width: 6%">Jml Bal
                                                </th>
                                                <th class="kotak" style="width: 8%">&nbsp;
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstDepo" runat="server" OnItemDataBound="lstDepo_DataBound" OnItemCommand="lstDepo_Command">
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
                                                            <%# Eval("Checker") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("TglKirim","{0:d}") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("TglETA", "{0:d}")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("KirimVia") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("NoSJ") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("Expedisi") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <asp:TextBox ID="txtnopol" runat="server" ToolTip='<%# Container.ItemIndex %>'
                                                                CssClass="txtongrid" Width="100%" AutoPostBack="false"></asp:TextBox>
                                                            <%-- <%# Eval("NoPOL") %>--%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("GrossDepo","{0:N0}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("KADepo", "{0:N2}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("Sampah", "{0:N2}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("NettDepo", "{0:N0}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:TextBox ID="txtjmlbal" runat="server" ToolTip='<%# Container.ItemIndex %>'
                                                                CssClass="txtongrid" Width="100%" AutoPostBack="false"></asp:TextBox>
                                                            <%--<%# Eval("JmlBAL", "{0:N0}")%>--%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="edt" runat="server" ToolTip="Edit Item" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="edit" ImageUrl="~/images/folder.gif" />
                                                            <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("nosj") %>'
                                                                CommandName="Save" Visible="false" ToolTip="SImpan Item" />
                                                            <asp:ImageButton ID="del" runat="server" CommandArgument='<%# Eval("IDBeli") %>'
                                                                CommandName="hapus" ImageUrl="~/images/Delete.png" />
                                                            <asp:ImageButton ID="sts" runat="server" CommandArgument='<%# Eval("IDBeli") %>' CommandName="tStatus"
                                                                ImageUrl="~/images/po.png" />
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
                                                <th class="kotak" colspan="12">List Pembelian</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 4%">No</th>
                                                <th class="kotak" style="width: 8%">Doc. No</th>
                                                <th class="kotak" style="width: 8%">Tanggal</th>
                                                <th class="kotak" style="width: 12%">Supplier</th>
                                                <th class="kotak" style="width: 12%">Checker</th>
                                                <th class="kotak" style="width: 15%">ItemName</th>
                                                <th class="kotak" style="width: 5%">Gross</th>
                                                <th class="kotak" style="width: 5%">Kadar air</th>
                                                <th class="kotak" style="width: 4%">Sampah</th>
                                                <th class="kotak" style="width: 5%">Netto</th>
                                                <th class="kotak" style="width: 5%">Jml BAL</th>
                                                <th class="kotak" style="width: 4%">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstKA" runat="server" OnItemDataBound="lstKA_DataBound" OnItemCommand="lstKA_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:CheckBox ID="chk" runat="server" Visible="true" ToolTip='<%# Eval("ID") %>' /></td>
                                                        <td class="kotak tengah"><%# Eval("DocNo") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglCheck","{0:d}") %></td>
                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("SupplierName").ToString().ToUpper() %></td>
                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("Checker").ToString().ToUpper() %></td>
                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                        <td class="kotak angka"><%# Eval("GrossPlant","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AvgKA","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Sampah","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant", "{0:N0}")%></td>
                                                        <td class="kotak tengah"><%# Eval("JmlBAL","{0:N0}") %><asp:Label ID="sts" runat="server" Text='<%# Eval("Keputusan") %>' Visible="false"></asp:Label></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="btnPrev" runat="server" CommandArgument='<%=Eval("ID %>' ToolTip="Click For Preview Detail" CommandName="prev" ImageUrl="~/images/clipboard_16.png" />
                                                            <asp:ImageButton ID="btnApp" runat="server" CommandArgument='<%=Eval("ID %>' ToolTip="Click For Preview Detail" CommandName="prev" ImageUrl="~/images/Approved_16.png" />
                                                            <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("DocNo") %>' CommandName="prn" ImageUrl="~/images/printer_small.png" />
                                                            <asp:ImageButton ID="btnPO" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip='Kirim data pembelian' CommandName="po" ImageUrl="~/images/editor.png" Visible="false" />
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
