<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivBayarKertasList.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivBayarKertasList" %>

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
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px;">
                            <table class="nbTableHeader" style="width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="width: 40%; padding-left: 10px;">
                                        <b>LIST PEMBAYARAN KERTAS</b>
                                    </td>
                                    <td style="width: 60%; padding-right: 10px" align="right">
                                        <asp:HiddenField ID="txtSupplierID" runat="server" Value="0" />
                                        <asp:Button ID="btnBack" runat="server" Text="Form Input" OnClick="btnBack_Click" />
                                        <asp:Label ID="lblFind" runat="server" Text="Find by Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupplier" runat="server" AutoPostBack="true" Width="300px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="act1" runat="server" CompletionInterval="100"
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            EnableCaching="true" MinimumPrefixLength="2" OnClientItemSelected="GetKey"
                                            ServiceMethod="GetSupplierKertas" ServicePath="AutoComplete.asmx"
                                            TargetControlID="txtSupplier">
                                        </cc1:AutoCompleteExtender>
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
                                <p></p>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 5%">&nbsp;</td>
                                        <td style="width: 10%">Periode</td>
                                        <td style="width: 30%">
                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            <asp:HiddenField ID="txtID" runat="server" />
                                        </td>
                                        <td style="width: 40%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Lokasi QA</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPlant" runat="server"></asp:DropDownList></td>
                                        <td>
                                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="Preview" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 400px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 4%">No</th>
                                                <th class="kotak" style="width: 8%">Doc. No</th>
                                                <th class="kotak" style="width: 8%">Tanggal<br />
                                                    Bayar</th>
                                                <th class="kotak" style="width: 8%">Tanggal<br />
                                                    JTempo</th>
                                                <th class="kotak" style="width: 12%">Supplier Name</th>
                                                <th class="kotak" style="width: 15%">ItemName</th>
                                                <th class="kotak" style="width: 5%">Qty</th>
                                                <th class="kotak" style="width: 5%">Harga</th>
                                                <th class="kotak" style="width: 4%">Total Harga</th>
                                                <th class="kotak" style="width: 5%">BG No.</th>
                                                <th class="kotak" style="width: 5%">An BG No.</th>
                                                <%--<th class="kotak" style="width:4%">&nbsp</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstKA" runat="server" OnItemDataBound="lstKA_DataBound" OnItemCommand="lstKA_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah"><%# Eval("DocNo") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglBayar","{0:d}") %></td>
                                                        <td class="kotak tengah"><%# Eval("Jtempo","{0:d}") %></td>
                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("SupplierName").ToString().ToUpper() %></td>
                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                        <td class="kotak angka"><%# Eval("Qty","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("Harga","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("TotalHarga","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("BGNo")%></td>
                                                        <td class="kotak tengah"><%# Eval("AnBGNo") %>
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
</asp:Content>
