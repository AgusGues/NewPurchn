<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POSerahTerima.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.POSerahTerima" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label{font-size:12px;}
        @media (min-width: 992px) {
			.modal-xl {
				width: 95%;
			}
		}
    </style>
    <script type="text/javascript">
    $(document).ready(function() {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%">
                            <table  class="nbTableHeader">
                                <tr style="height:49px">
                                    <td style="width:30%; padding-left:10px">
                                        <strong> Serah Terima Document</strong>
                                    </td>
                                    <td align="right" style="width:65%; padding-right:10px">
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" OnClick="btnBack_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="List Serah Terima" Visible="true" OnClick="lstData_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <div id="frm10" runat="server">
                                    <table style="width:100%; border-collapse:collapse; padding-top:10px;">
                                        <tr>
                                            <td style="width:10%; padding-left:10px;">Periode</td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr style="height:45px" valign="bottom">
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Visible="false"  OnClick="btnExport_Click"/>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                                <hr />
                                <div class="contentlist" style="height:485px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:4%">No.</th>
                                            <th class="kotak" style="width:8%">Tgl PO</th>
                                            <th class="kotak" style="width:15%">Supplier</th>
                                            <th class="kotak" style="width:8%">No.RMS</th>
                                            <th class="kotak" style="width:8%">No.PO</th>
                                            <th class="kotak" style="width:8%">No.SPP</th>
                                            <th class="kotak" style="width:10%">Tgl Kirim</th>
                                            <th class="kotak" style="width:4%">Selisih</th>
                                            <th class="kotak" style="width:10%">Tgl Terima</th>
                                            <th class="kotak" style="width:4%">Sesuai</th>
                                            <th class="kotak" style="width:4%">Tidak</th>
                                            <th class="kotak" style="width:15%">Keterangan</th>
                                            <th class="kotak" style="width:5%"></th>
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
                                                        <td class="kotak tengah"><asp:Label ID="txtTglKirim" runat="server" Text='<%# Eval("TglKirim","{0:dd/MM/yyyy HH:mm}") %>'></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="txtSelisih" runat="server" Text=''></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="txtTglterima" runat="server" Text='<%# Eval("TglTerima", "{0:dd/MM/yyyy HH:mm}")%>'></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="txtSesuai" CssClass="tengah" runat="server" Text='<%# Eval("Sesuai") %>'></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="txtTidak" CssClass="tengah" runat="server" Text='<%# Eval("Tidak") %>'></asp:Label></td>
                                                        <td class="kotak">
                                                            <asp:HiddenField ID="txtKetOri" runat="server" Value='<%# Eval("Keterangan") %>' />
                                                            <asp:TextBox ID="txtKet" runat="server" CssClass="txtongrid" Width="100%" Text='<%# Eval("Keterangan") %>'  OnTextChanged="txtKet_Change" ></asp:TextBox>
                                                            <asp:Label ID="exKeterangan" runat="server" Text='<%# Eval("Keterangan") %>'></asp:Label>
                                                       </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="btnSimpan" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="simpan" ImageUrl="~/images/Save_blue.png" />
                                                            <asp:ImageButton ID="btnKirim" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName='kirim' ImageUrl="~/images/delivery.png" />
                                                            <asp:ImageButton ID="btnterima" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName='terima' ImageUrl="~/images/clipboard_16.png" />
                                                            <asp:ImageButton ID="btnUpd" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="upd" ImageUrl="~/images/disk.jpg" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr class="line3 baris">
                                                        <td class="kotak angka" colspan="8">Total Document</td>
                                                        <td class="kotak angka"><asp:Label ID="sTotalData" runat="server"></asp:Label></td>
                                                        <td colspan="4" style="background-color:White">&nbsp;</td>
                                                    </tr>
                                                    <tr class="line3 baris">
                                                        <td class="kotak angka" colspan="8">Total Tidak OK</td>
                                                        <td class="kotak angka"><asp:Label ID="stxtTidak" runat="server"></asp:Label></td>
                                                        <td colspan="4" style="background-color:White" >&nbsp;</td>
                                                    </tr>
                                                    <tr class="line3 baris">
                                                        <td class="kotak angka" colspan="8">Pencapaian(%)</td>
                                                        <td class="kotak angka"><asp:Label ID="stxtSesuai" runat="server"></asp:Label></td>
                                                        <td colspan="4" style="background-color:White">&nbsp;</td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
