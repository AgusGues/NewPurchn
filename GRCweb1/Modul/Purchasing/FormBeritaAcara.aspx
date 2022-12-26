<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormBeritaAcara.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormBeritaAcara" %>
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
    </style>
    <script type="text/javascript">
    function lostFocus(id) {
        if (id == '') {
            return alert('Subject Tidak boleh Kosong');
        }
    }
    function _print_prev(id) {
        window.showModalDialog('BeritaAcaraPrint.aspx?id='+id, '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1024px;scrollbars=yes');
        return;
    }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                 <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <tr valign="top">
                    <td style="height:49px">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width:20%">&nbsp;&nbsp;Berita Acara</td>
                                <td style="width:70%; padding-right:10px" align="right">
                                    <asp:Button ID="btnNew" runat="server" Text="New BA" OnClick="btnNew_Click" />
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_Click" />
                                    <asp:Button ID="btnPrint" runat="server" Text="Print BA" />
                                    <asp:TextBox ID="txtCari" runat="server" Width="250px" Text="Find by BA Number" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                    <cc1:AutoCompleteExtender ID="autoex" runat="server" TargetControlID="txtCari" 
                                    ServicePath="~/Modul/Purchasing/AutoComplete.asmx" ServiceMethod="GetBANum" 
                                    CompletionListCssClass="autocomplete_completionListElement" MinimumPrefixLength="2" EnableCaching="true"></cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <div id="ctn" runat="server" class="content">
                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr>
                                    <td style="width:5%">&nbsp;</td>
                                    <td style="width:10%">Tanggal</td>
                                    <td style="width:25%"><asp:TextBox ID="txtTanggal" runat="server" Width="80%"></asp:TextBox></td>
                                    <td style="width:5%"><cc1:CalendarExtender ID="CalEx" runat="server" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td>
                                    <td style="width:10%">From Dept</td>
                                    <td style="width:25%"><asp:DropDownList ID="ddlfromDept" runat="server" Width="100%"></asp:DropDownList></td>
                                    <td style="width:10%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>To Dept</td>
                                    <td><asp:DropDownList ID="ddlToDept" runat="server" Width="100%"></asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                    <td>Attn</td>
                                    <td><asp:TextBox ID="txtAttn" runat="server" Width="100%"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp</td>
                                     <td>Subject</td>
                                     <td><asp:TextBox ID="txtSubject" runat="server" AutoPostBack="true" Width="100%"  OnTextChanged="txtSubject_Change"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                    <td>BA Number</td>
                                    <td><asp:TextBox ID="txtBANum" runat="server" Width="80%" ReadOnly="true"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Jenis Barang</td>
                                    <td colspan="4"><asp:RadioButtonList ID="chkJenisBarang" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList></td>
                                    <td>&nbsp</td>
                                </tr>
                                <tr id="kertas1" runat="server" >
                                    <td>&nbsp;</td>
                                    <td>Cari Nama Barang</td>
                                    <td><asp:TextBox ID="txtCariNamaBarang" runat="server" Width="100%"></asp:TextBox></td>
                                    <td colspan="3">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Nama Barang</td>
                                    <td colspan="2"><asp:DropDownList ID="ddlNamaBarang" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlNamaBarang_Change"></asp:DropDownList></td>
                                    <%--<td>&nbsp;</td>--%>
                                    <td>&nbsp;&nbsp;Depo</td>
                                    <td><asp:DropDownList ID="ddlDepo" runat="server"></asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr id="kertas" runat="server">
                                    <td colspan="7">
                                    <hr />
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                            <tr>
                                                <td style="width:5%">&nbsp;</td>
                                                <td style="width:10%">Receipt No</td>
                                                <td style="width:25%">
                                                    <asp:DropDownList ID="ddlNoRMS" runat="server" Width="60%" AutoPostBack="true" OnSelectedIndexChanged="ddlNoRMS_Change"></asp:DropDownList>
                                                    <asp:TextBox ID="txtNoRMS" runat="server" Width="60%" AutoPostBack="true" OnTextChanged="txtNoRMS_Change" Visible="false"></asp:TextBox>
                                                    </td>
                                                <td style="width:5%">&nbsp;</td>
                                                <td style="width:10%">Receipt Date</td>
                                                <td style="width:25%"><asp:TextBox ID="txtReceiptDate" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                <td style="width:10%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Agen/Supplier</td>
                                                <td><asp:TextBox ID="txtDepo" runat="server" Width="100%" ReadOnly="true"></asp:TextBox></td>
                                                <td><asp:HiddenField ID="txtDepoID" runat="server" /></td>
                                                <td>No Surat Jalan</td>
                                                <td><asp:TextBox ID="txtSJNo" runat="server" Width="100%"></asp:TextBox></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Berat Kotor</td>
                                                <td><asp:TextBox ID="txtQtySup" runat="server" Width="50%"></asp:TextBox></td>
                                                <td>Kadar Air</td>
                                                <td><asp:TextBox ID="txtKadarAir" runat="server" Width="60%"></asp:TextBox></td>
                                                <td>Jml (Bal) &nbsp;&nbsp;<asp:TextBox ID="txtJmlBal" Text="0" runat="server" Width="30%"></asp:TextBox></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Berat Bersih</td>
                                                <td><asp:TextBox ID="txtNetto" runat="server" Width="50%"></asp:TextBox></td>
                                                <td>Qty RMS</td>
                                                <td><asp:TextBox ID="txtQtyRms" runat="server" Width="60%"></asp:TextBox></td>
                                                <td align="right" style="padding-right:10px"><asp:Button ID="btnAddItem" runat="server" Text="AddItem" OnClick="btnAddItem_Click" Height="25px" /></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                     <hr />
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <tr>
                                            <td style="width:5%">&nbsp;</td>
                                            <td style="width:10%">Total Qty Supplier</td>
                                            <td style="width:20%"><asp:TextBox ID="txtTotQtySup" runat="server" Width="50%" Text="0" ReadOnly="true"></asp:TextBox>
                                            <td style="width:10%">Timbangan BPAS </td>
                                            <td style="width:20%"><asp:TextBox ID="txtQtyBPAS" runat="server" Width="80%"  Text="0" AutoPostBack="true" OnTextChanged="txtQtyBPAS_Change"></asp:TextBox></td>
                                            <td style="width:15%" align="right">Total (Bal)&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtTotalBal" Width="50%">0</asp:TextBox></td>
                                            <td style="width:5%">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;<asp:HiddenField ID="txtStdKA" runat="server" /></td>
                                            <td>Kadar Air BPAS(%)</td>
                                            <td><asp:TextBox ID="txtKABPAS" runat="server" Width="30%" Text="0" AutoPostBack="true" OnTextChanged="txtKABPAS_Change"></asp:TextBox> &nbsp;&nbsp;
                                            Sampah(%) &nbsp;:&nbsp;<asp:TextBox ID="txtSampah" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtSampah_Change" Width="30%"></asp:TextBox><td>Berat Bersih BPAS</td>
                                            <td><asp:TextBox ID="txtNetBPAS" runat="server" Width="80%" Text="0" ReadOnly="true"></asp:TextBox></td>
                                            <td align="right">Selisih&nbsp;&nbsp;<asp:TextBox ID="txtSelisih" runat="server" Width="50%"></asp:TextBox></td>
                                           <td><asp:TextBox ID="txtProsKA" runat="server" width="106%" CssClass="txtongrid" 
                                                   ReadOnly="true"></asp:TextBox></td>
                                        </tr>
                                       
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <div class="contentlist" style="height:190px">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:4%">No.</th>
                                            <th class="kotak" style="width:8%">ReceiptNo</th>
                                            <th class="kotak" style="width:8%">Receipt Date</th>
                                            <th class="kotak" style="width:15%">Depo</th>
                                            <th class="kotak" style="width:10%">Qty Gross</th>
                                            <th class="kotak" style="width:10%">Qty Netto</th>
                                            <th class="kotak" style="width:8%">Kadar Air</th>
                                            <th class="kotak" style="width:5%">Jml(Bal)</th>
                                            <th class="kotak" style="width:5%">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah"><%# Eval("ReceiptNo").ToString().ToUpper() %></td>
                                                    <td class="kotak tengah"><%# Eval("ReceiptDate","{0:d}") %></td>
                                                    <td class="kotak"><%# Eval("SupplierName") %></td>
                                                    <td class="kotak angka"><%# Eval("Gross","{0:N2}") %></td>
                                                    <td class="kotak angka"><%# Eval("Netto", "{0:N2}")%></td>
                                                    <td class="kotak tengah"><%# Eval("KadarAir", "{0:N2}")%></td>
                                                    <td class="kotak angka"><%# Eval("JmlBal", "{0:N2}")%></td>
                                                    <td class="kotak">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="Line3">
                                            <td colspan="4" class="kotak angka"></td>
                                            <td class="kotak angka"><asp:Label ID="totA" runat="server"></asp:Label></td>
                                            <td class="kotak angka"><asp:Label ID="totB" runat="server"></asp:Label></td>
                                            <td class="kotak angka"><asp:Label ID="Ka" runat="server"></asp:Label></td>
                                            <td class="kotak angka"><asp:Label ID="totC" runat="server"></asp:Label></td>
                                            <td class="kotak">&nbsp;</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                </table>
                <span id="errMsg" runat="server"></span>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSubject" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
