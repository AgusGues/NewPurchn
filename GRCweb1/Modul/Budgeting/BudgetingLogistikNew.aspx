<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BudgetingLogistikNew.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.BudgetingLogistikNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel-body">
                <div style="width: 100%;">
                    <div class="table-responsive">
                        <table style="width: 100%; border-collapse: collapse; font-size: small">
                            <tr>
                                <td style="width: 100%; height: 49px">
                                    <table class="nbTableHeader" style="width: 100%">
                                        <tr>
                                            <td style="width: 50%; padding-left: 10px">MATRIX BUDGET LOGISTIK PRODUK JADI
                                            </td>
                                            <td style="width: 50%; text-align: right; padding-right: 10px">
                                                <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" ForeColor="Black" />
                                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                                <asp:TextBox ID="txtCari" runat="server" Width="250px" ForeColor="Black"></asp:TextBox>
                                                <asp:Button ID="btnCari" runat="server" Text="Cari" ForeColor="Black" />
                                                <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!--content--->
                                    <div class="content">

                                        <table style="width: 100%; border-collapse: collapse; font-size: small">
                                            <tr>
                                                <td style="width: 5%">&nbsp;
                                                </td>
                                                <td style="width: 15%">Periode
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:DropDownList ID="ddlBulan" OnSelectedIndexChanged="ddlBulan_Change" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTahun" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" />
                                                    <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="contentlist" style="height: 440px; overflow: auto" id="lst" runat="server"
                                            onscroll="setScrollPosition(this.scrollTop);">
                                            <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small; height: 541px;" border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th rowspan="3" class="kotak">No
                                                        </th>
                                                        <th rowspan="3" class="kotak">Pengiriman
                                                        </th>
                                                        <th rowspan="3" class="kotak">Jenis Packing
                                                        </th>
                                                        <th rowspan="3" class="kotak">Jumlah Delivery (Palet)
                                                        </th>
                                                        <th colspan="4" class="kotak txtUpper">Papan Kayu
                                                        </th>
                                                        <th colspan="4" class="kotak txtUpper">Balok Kayu
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th colspan="2" class="kotak">STD
                                                        </th>
                                                        <th colspan="2" class="kotak">Pemakaian(SPB)
                                                        </th>
                                                        <th colspan="2" class="kotak">STD
                                                        </th>
                                                        <th colspan="2" class="kotak">Pemakaian(SPB)
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak">Lbr/Palet
                                                        </th>
                                                        <th class="kotak">Jumlah(Lbr)
                                                        </th>
                                                        <th class="kotak">Jumlah(Lbr)
                                                        </th>
                                                        <th class="kotak">(%)
                                                        </th>
                                                        <th class="kotak">Lbr/Palet
                                                        </th>
                                                        <th class="kotak">Jumlah(Lbr)
                                                        </th>
                                                        <th class="kotak">Jumlah(Lbr)
                                                        </th>
                                                        <th class="kotak">(%)
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tb" runat="server">
                                                    <tr class="Line3 baris">
                                                        <td colspan="12">STANDAR PALET KIRIM
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="lstPengiriman" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="Line3 baris" id="Pengiriman" runat="server">
                                                                <td class="kotak"></td>
                                                                <td class="kotak" colspan="2">
                                                                    <%# Eval("Pengiriman")%>
                                                                </td>
                                                                <td class="kotak" colspan="9"></td>
                                                            </tr>
                                                            <asp:Repeater ID="lstMatrix" runat="server" OnItemDataBound="lstMatrix_DataBound">
                                                                <ItemTemplate>
                                                                    <tr id="lst" runat="server" class="EvenRows baris">
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("Urutan") %>
                                                                        </td>
                                                                        <td class="kotak " style="white-space: nowrap">
                                                                            <asp:Label ID="lblPengiriman" runat="server" Text='<%# Eval("Pengiriman")%>' Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td class="kotak " style="white-space: nowrap">
                                                                            <%# Eval("JenisPacking") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="background-color: Yellow">
                                                                            <asp:Label ID="txtjmlDelivery" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                            <asp:TextBox ID="jmlDelivery" runat="server" AutoPostBack="true" CssClass="txtOnGrid angka"
                                                                                Width="100%" OnTextChanged="jmlDelivery_Change"></asp:TextBox>
                                                                            <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                                        </td>
                                                                        <td class="kotak tengah OddRows">
                                                                            <%# Eval("PKA", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah OddRows">
                                                                            <%# Eval("BKA", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="Line3 Baris" id="lstF" runat="server">
                                                        <td colspan="3" class="kotak angka txtUpper">
                                                            <b>TOTAL</b>
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="Line3 baris">
                                                        <td colspan="12">STANDAR BUDGET PAPAN DAN BALOK REPAIR
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="lstPengiriman1" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="Line3 baris" id="Pengiriman1" runat="server">
                                                                <td class="kotak"></td>
                                                                <td class="kotak" colspan="2">
                                                                    <%# Eval("Pengiriman")%>
                                                                </td>
                                                                <td class="kotak" colspan="9"></td>
                                                            </tr>
                                                            <asp:Repeater ID="lstMatrix1" runat="server" OnItemDataBound="lstMatrix1_DataBound">
                                                                <ItemTemplate>
                                                                    <tr id="lst1" runat="server" class="EvenRows baris">
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("Urutan") %>
                                                                        </td>
                                                                        <td class="kotak " style="white-space: nowrap">
                                                                            <asp:Label ID="lblPengiriman" runat="server" Text='<%# Eval("Pengiriman")%>' Visible="False"></asp:Label>
                                                                        </td>
                                                                        <td class="kotak " style="white-space: nowrap">
                                                                            <%# Eval("JenisPacking") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="background-color: Yellow">
                                                                            <asp:Label ID="txtjmlDelivery" runat="server" Visible="false" Width="100%"></asp:Label>
                                                                            <asp:TextBox ID="jmlDelivery" runat="server" AutoPostBack="true" CssClass="txtOnGrid angka"
                                                                                Width="100%" OnTextChanged="jmlDelivery1_Change"></asp:TextBox>
                                                                            <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                                        </td>
                                                                        <td class="kotak tengah OddRows">
                                                                            <%# Eval("PKA", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah OddRows">
                                                                            <%# Eval("BKA", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <tr class="kotak kanan" id="ftr1" runat="server">
                                                                        <td class="kotak bold angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka" colspan="2"></td>
                                                                        <td class="kotak bold angka">&nbsp;
                                                                        </td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                        <td class="kotak bold angka"></td>
                                                                    </tr>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="Line3 Baris" id="lstPakai" runat="server">
                                                        <td colspan="3" class="kotak">
                                                            <b>PERSENTASE EFISIENSI PEMAKAIAN</b>
                                                        </td>
                                                        <td colspan="4" class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                        <td colspan="3" class="kotak angka">&nbsp;
                                                        </td>
                                                        <td class="kotak angka">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="Line3 Baris" id="lstSPB" runat="server">
                                                        <td colspan="3" class="kotak">
                                                            <b>JUMLAH PEMAKAIAN = TOTAL PEMAKAIAN PAPAN DAN BALOK (SPB) - TOTAL PALET STOCK YANG
                                                        TIDAK KEMBALI (PAPAN DAN BALOK) + TOTAL PALET KIRIM YANG DIKEMBALIKAN (PAPAN DAN
                                                        BALOK)</b>
                                                        </td>
                                                        <td colspan="9" class="kotak angka tengah">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="Line3 Baris" id="lstStandar" runat="server">
                                                        <td colspan="3" class="kotak">
                                                            <b>JUMLAH STANDAR = TOTAL STANDARD BUDGET PAPAN DAN BALOK PALET KIRIM + STANDAR BUDGET
                                                        PAPAN DAN BALOK REPAIR</b>
                                                        </td>
                                                        <td colspan="9" class="kotak angka tengah">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr class="Line3 Baris" id="lstPencapaian" runat="server">
                                                        <td colspan="3" class="kotak">
                                                            <b>% PENCAPAIAN BUDGET LOGISTIK PRODUK JADI = JUMLAH PEMAKAIAN / JUMLAH STANDAR</b>
                                                        </td>
                                                        <td colspan="9" class="kotak angka tengah">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="12">&nbsp;
                                                        </td>
                                                    </tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
