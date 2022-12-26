<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BudgetingFinishing.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.BudgetingFinishing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel-body">
                <div style="width: 100%;">
                    <div class="table-responsive">
                        <table style="width: 100%; border-collapse: collapse; font-size: small">
                            <tr>
                                <td style="width: 100%; height: 49px">
                                    <table class="nbTableHeader" style="width: 100%">
                                        <tr>
                                            <td style="width: 50%; padding-left: 10px">PEMANTAUAN BUDGET FINISHING
                                            </td>
                                            <td style="width: 50%; text-align: right; padding-right: 10px">
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
                                                    <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTahun" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click"
                                                Text="Preview" />
                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                                        Text="Export to Excel" />
                                                    <asp:Button ID="btnSimpan" runat="server" OnClick="btnSimpan_Click"
                                                        Text="Simpan Data" Visible="False" />
                                                    <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <div class="contentlist" style="overflow: auto" id="lst" runat="server">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                        <thead>
                                                            <tr class="tbHeader">
                                                                <th class="kotak" rowspan="2" style="width: 4%">No
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 16%">Spesifikasi Barang
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 5%">Satuan
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 15%">Harga / Satuan
                                                                </th>
                                                                <th class="kotak" colspan="3">Budget
                                                                </th>
                                                                <th class="kotak" colspan="6">Aktual Pemakaian
                                                                </th>
                                                                <th class="kotak" rowspan="2">Program Checking
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader">
                                                                <th class="kotak" style="width: 5%">Barang(Satuan)
                                                                </th>
                                                                <th class="kotak" style="width: 7%">Lembar/ Satuan Barang
                                                                </th>
                                                                <th class="kotak" style="width: 10%">Rupiah / Bulan
                                                                </th>
                                                                <th class="kotak" style="width: 5%">Barang(Satuan)
                                                                </th>
                                                                <th class="kotak" style="width: 7%">Lembar/ Satuan Barang
                                                                </th>
                                                                <th class="kotak" style="width: 7%">Output Produk (Lembar)
                                                                </th>
                                                                <th class="kotak" style="width: 10%">Rupiah / Bulan
                                                                </th>
                                                                <th class="kotak" style="width: 5%">Prosen
                                                                </th>
                                                                <th class="kotak" style="width: 8%">Rupiah/ Lembar
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="lstFin" runat="server" OnItemDataBound="lstFin_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                                        <td class="kotak tengah 0"></td>
                                                                        <td class="kotak 1" style="white-space: nowrap">
                                                                            <%# Eval("ItemName") %>
                                                                            <asp:HiddenField ID="txtItemID" runat="server" Value='<%# Eval("ItemID") %>' />
                                                                            <asp:HiddenField ID="txtBarang" runat="server" Value='<%# Eval("ItemName") %>' />
                                                                            <asp:HiddenField ID="txtStatus" runat="server" Value='<%# Eval("RowStatus") %>' />
                                                                            <asp:HiddenField ID="txtMCCID" runat="server" Value='<%# Eval("ID") %>' />
                                                                        </td>
                                                                        <td class="kotak tengah 2">
                                                                            <%# Eval("Unit") %>
                                                                        </td>
                                                                        <td class="kotak angka 3">
                                                                            <asp:TextBox ID="txtPrice" runat="server" ToolTip='<%# Container.ItemIndex %>'
                                                                                Width="50px" OnTextChanged="txtPrice_Change" AutoPostBack="true" Font-Size="XX-Small" CssClass="txtOnGrid angka" BackColor="transparent" BorderColor="Transparent"></asp:TextBox>
                                                                            <asp:HiddenField ID="txtSimpanID" runat="server" Value='0' />
                                                                        </td>
                                                                        <td class="kotak angka 4">
                                                                            <%# Eval("Barang", "{{0:0.00}}")%>
                                                                        </td>
                                                                        <td class="kotak angka 5">
                                                                            <%# Eval("Lembar", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka 6">
                                                                            <%# Eval("RupiahPerBln", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka 7">
                                                                            <%# Eval("Quantity","{0:N0}") %>
                                                                        </td>
                                                                        <td class="kotak angka 8">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka 9">
                                                                            <%# (Eval("Quantity").ToString()=="0")?"": Eval("ProdukOut", "{0:N0}")%>
                                                                        </td>
                                                                        <td class="kotak angka 10">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka 11" style="white-space: nowrap">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka 12">&nbsp;
                                                                        </td>
                                                                        <td class="kotak angka 13">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr class="Line3 baris">
                                                                <td colspan="6" class="kotak tengah">JUMLAH TOTAL (Rp.)
                                                                </td>
                                                                <td class="kotak angka bold" id="rpBudget" runat="server"></td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak angka bold" id="rpAktual" runat="server">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr class="Line3 baris">
                                                                <td colspan="6" class="kotak tengah">PROSEN (%)
                                                                </td>
                                                                <td class="kotak angka">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak angka bold" id="prosene" runat="server">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </div>
                                                <div id="DivFooterRow" style="overflow: hidden">
                                                </div>
                                            </div>
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
