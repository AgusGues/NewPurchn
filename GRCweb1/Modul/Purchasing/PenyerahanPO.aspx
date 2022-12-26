<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PenyerahanPO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.PenyerahanPO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%">
                            <table  class="nbTableHeader">
                                <tr style="height:49px">
                                    <td style="width:30%; padding-left:10px">
                                        <strong> Serah Terima PO ke Accounting</strong>
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
                                <div id="frm11" runat="server">
                                    <table style="width:100%; border-collapse:collapse; padding-top:10px;">
                                        <tr id="bulan" runat="server" visible="false">
                                            <td style="width:14%; padding-left:10px">
                                                Periode Bulanan &nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBulan0" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlBulan0_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlTahun0" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlTahun0_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr id="harian" runat="server" visible="true">                                            
                                            <td style="width: 16%; padding-left: 10px; font-family: Arial; font-size: x-small;">
                                                Periode Harian &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTgl01" runat="server" BorderStyle="Groove" ReadOnly="false" Visible="true"
                                                    Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl01">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width:14%; padding-left:10px">
                                                &nbsp;</td>
                                            <td>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTgl1">
                                                </cc1:CalendarExtender>
                                                <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" ReadOnly="True" 
                                                    Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:14%; padding-left:10px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTgl2">
                                                </cc1:CalendarExtender>
                                                <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" ReadOnly="True" 
                                                    Visible="False" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width:14%; padding-left:10px">
                                                <asp:Button ID="btnPrint"  OnClick="btnPrint_ServerClick" runat="server" Text="Preview" />
                                            </td>
                                            <td style="width: 100%; padding-left: 10px">
                                                <asp:RadioButton ID="RBulan" runat="server" AutoPostBack="True" Checked="false" OnCheckedChanged="RBulan_CheckedChanged"
                                                    Style="font-family: Arial; font-size: xx-small; text-align: left;" Text="&nbsp; Periode Bulanan"
                                                    TextAlign="Left" Width="109px" />
                                                <asp:RadioButton ID="RHarian" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="RHarian_CheckedChanged"
                                                    Style="font-family: Arial; font-size: xx-small; text-align: left;" Text="&nbsp; Periode Harian"
                                                    TextAlign="Left" Width="100px" />
                                                <asp:RadioButton ID="RPONoSerah" runat="server" AutoPostBack="True" Checked="false"
                                                    OnCheckedChanged="RPONoSerah_CheckedChanged" Style="font-family: Arial; font-size: xx-small;
                                                    text-align: left;" Text="&nbsp; PO blm Serah Purchasing" TextAlign="Left" Width="150px" />
                                                <asp:RadioButton ID="RPOSerah" runat="server" AutoPostBack="True" Checked="true"
                                                    OnCheckedChanged="RPOSerah_CheckedChanged" Style="font-family: Arial; font-size: xx-small;
                                                    text-align: left;" Text="&nbsp; PO sdh Serah Purchasing" TextAlign="Left" Width="150px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
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
                                <div class="contentlist" style="height:485px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:4%">No.</th>
                                            <%--<th class="kotak" style="width:8%">No.RMS</th>--%>
                                            <%--<th class="kotak" style="width:8%">No.SPP</th>--%>
                                            <th class="kotak" style="width:8%">Tgl PO</th>
                                            <th class="kotak" style="width:8%">No.PO</th>
                                            <th class="kotak" style="width:15%">Nama Supplier</th>
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
                                                        <%--<td class="kotak tengah"><%# Eval("ReceiptNo") %></td>--%>
                                                        <%--<td class="kotak tengah"><%# Eval("NoSPP") %></td>--%>
                                                        <td class="kotak tengah"><%# Eval("POPurchnDate","{0:d}") %></td>
                                                        <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                        <td class="kotak"><%# Eval("SupplierName") %></td>
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
                                                            <asp:ImageButton ID="hapus" ToolTip="Kembalikan ke awal Accounting sblm terima" runat="server"
                                                                CommandArgument='<%# Eval("ID") %>' CommandName="hps"
                                                                ImageUrl="~/images/Delete.png" />
                                                            <asp:ImageButton ID="hapus2" ToolTip="Kembalikan ke Purchasing" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="hps2" ImageUrl="~/images/hps3.jpg" />
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
