<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivBeliKA0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivBeliKA0" %>

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

        function CetakFrom(docno) {
            params = 'width=1024px';
            params += ', heigh=600px'
            params += ', top=20px, left=20px'
            window.open("../../ModalDialog/FromBeliQA.aspx?ka=" + docno, "Preview", params);
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="height: 49px; width: 100%;">
                            <table class="nbTableHeader" style="width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="width: 40%; padding-left: 10px;">
                                        <b>INPUT PEMBELIAN KERTAS</b>
                                    </td>
                                    <td style="width: 60%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnCetak" runat="server" Text="Cetak" Visible="true" OnClick="btnCetak_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="List Kadar Air" OnClick="btnList_Clik" />
                                        <asp:HiddenField ID="__LASTFOCUS" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 10%" class="left">Depo</td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlDepo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDepo_Change" Width="60%" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 4%">&nbsp;</td>
                                        <td style="width: 10%" class="left">Document No.</td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%"></td>
                                    </tr>
                                    <tr>
                                        <td class="left" style="width: 10%">Checker</td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlChecker" runat="server" Width="60%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 4%">&nbsp;</td>
                                        <td class="left" style="width: 10%">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="width: 5%"></td>
                                    </tr>
                                    <tr>
                                        <td class="left">Jenis Barang</td>
                                        <td>
                                            <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlNamaBarang_Change" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="left">Tanggal</td>
                                        <td>
                                            <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True"
                                                ontxtchanged="txtTanggal_TextChanged" Width="70%"
                                                OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="ca1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="txtItemCode" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="left">Tujuan Kirim</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTujuanKirim" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTujuanKirim_Change" Width="60%">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="txtSupplierID" runat="server"
                                                OnValueChanged="txtSupplier_Change" Value="0" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td class="left">Supplier</td>
                                        <td>
                                            <asp:TextBox ID="txtSupplier" runat="server" AutoPostBack="true" Width="100%"
                                                OnTextChanged="txtSupplier_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="act1" runat="server" CompletionInterval="100"
                                                CompletionListCssClass="autocomplete_completionListElement"
                                                EnableCaching="true" MinimumPrefixLength="2" OnClientItemSelected="GetKey"
                                                ServiceMethod="GetSupplierKAT" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtSupplier">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="left">Estimasi Berat Kotor(kg)</td>
                                        <td>
                                            <asp:TextBox ID="txtJmlTimbangan" runat="server" AutoPostBack="false" OnTextChanged="txtJmlTimbangan_Change"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td class="left">Jumlah Bal</td>
                                        <td>
                                            <asp:TextBox ID="txtjmlBal" runat="server"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="left" colspan="5">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>Total kirim periode
                                                            <asp:Label ID="LPeriode1" runat="server"></asp:Label>
                                                            &nbsp;s/d
                                                            <asp:Label ID="LPeriode2" runat="server"></asp:Label>
                                                            &nbsp;=
                                                            <asp:Label ID="LTotalKirim" runat="server" Text="0"></asp:Label>
                                                            &nbsp;Kg.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Total kirim Kertas Semen periode
                                                            <asp:Label ID="LPeriode1S" runat="server"></asp:Label>
                                                            &nbsp;s/d
                                                            <asp:Label ID="LPeriode2S" runat="server"></asp:Label>
                                                            &nbsp;=
                                                            <asp:Label ID="LTotalKirimS" runat="server" Text="0"></asp:Label>
                                                            &nbsp;Kg.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Minimal toleransi kadar air 30% =
                                                            <asp:Label ID="LTotalKirim0" runat="server" Text="0"></asp:Label>
                                                            &nbsp;Kg.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Maksimal toleransi kadar air 30% =
                                                            <asp:Label ID="LTotalKirim1" runat="server" Text="0"></asp:Label>
                                                            &nbsp;Kg.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Akumulasi toleransi kadar air 30% bulan ini =
                                                            <asp:Label ID="LTotalKirim2" runat="server" Text="0"></asp:Label>
                                                            &nbsp;Kg.
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 3px;" class="total kotak"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td colspan="3" style="width: 50%; max-height: 150px; overflow: auto" class="kotak" valign="top">
                                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="1">
                                                                <thead>
                                                                    <tr class="tbHeader">
                                                                        <th colspan="5" class="tengah kotak">Hasil Tusukan</th>
                                                                    </tr>
                                                                    <tr class="tbHeader">
                                                                        <th style="width: 12%" class="kotak">Bal Ke</th>
                                                                        <th style="width: 12%" class="kotak">No Bal</th>
                                                                        <th style="width: 12%" class="kotak">Titik 1</th>
                                                                        <th style="width: 12%" class="kotak">Titik 2</th>
                                                                        <th style="width: 15%" class="kotak">Rata-rata KA(%)</th>
                                                                        <th style="width: 5%" class="kotak">Test</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr class="OddRows baris">
                                                                        <td class="kotak tengah">
                                                                            <asp:HiddenField ID="txtID" runat="server" />
                                                                            <asp:TextBox ID="txtBalke" runat="server" ReadOnly="true" class="txtongrid tengah" Width="100%" Text="0"></asp:TextBox></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="txtBall" runat="server" class="tengah" Width="100%" AutoPostBack="false"></asp:TextBox></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="txtTusuk1" runat="server" class="tengah" Width="100%" AutoPostBack="false" OnTextChanged="txtTusuk1_Change"></asp:TextBox></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="txtTusuk2" runat="server" class="tengah" Width="100%" AutoPostBack="true" OnTextChanged="txtTusuk2_Change"></asp:TextBox></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="txtAvgKA" runat="server" class="txtongrid tengah" AutoPostBack="false" OnTextChanged="txtAvgKA_Change" Width="100%"></asp:TextBox></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:ImageButton ID="btnAdd" runat="server" ToolTip="Simpan Data" OnClick="btnAdd_Click" ImageUrl="~/images/Save_blue.png" /></td>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstKA" runat="server" OnItemCommand="lstKA_Command" OnItemDataBound="lstKA_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" id="trs" runat="server">
                                                                                <td class="kotak tengah"><%# Eval("BALKe") %></td>
                                                                                <td class="kotak tengah"><%# Eval("NoBall") %></td>
                                                                                <td class="kotak tengah"><%# Eval("Tusuk1") %></td>
                                                                                <td class="kotak tengah"><%# Eval("Tusuk2") %></td>
                                                                                <td class="kotak tengah"><%# Eval("AvgTusuk") %></td>
                                                                                <td class="kotak tengah">
                                                                                    <asp:ImageButton ID="btndel" runat="server" ToolTip='<%# Container.ItemIndex %>' ImageUrl="~/images/trash.gif" CommandArgument='<%# Container.ItemIndex %>' CommandName="hapus" />
                                                                                    <asp:ImageButton ID="btnEdt" runat="server" ToolTip="Edit Raw" ImageUrl="~/images/folder.gif" CommandArgument='<%# Container.ItemIndex %>' CommandName="edit" />
                                                                                </td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>

                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td colspan="3" style="width: 40%; padding-left: 10px;" class="kotak">
                                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" class="kotak">
                                                                <tr>
                                                                    <td class="Line3 kotak" colspan="3">Perhitungan Berat Bersih</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 30%; padding-left: 5px">Berat Kotor</td>
                                                                    <td style="width: 30%">
                                                                        <asp:TextBox ID="txtBK" runat="server" ReadOnly="true"></asp:TextBox></td>
                                                                    <td style="width: 40%" rowspan="4">
                                                                        <!-- inputan sampah-->
                                                                        <span class="line3" style="width: 100%">Data Sampah</span>
                                                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                                            <tr>
                                                                                <td>Std. Kadar Air</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="stKA" runat="server" Width="100%" ReadOnly="true"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 60%; padding-left: 3px">Berat Sample</td>
                                                                                <td style="width: 40%">
                                                                                    <asp:TextBox ID="txtBeratSample" runat="server" Width="100%"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 3px">Berat Sampah</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtBeratSampah" runat="server" Width="100%" OnTextChanged="txtBS_Change" AutoPostBack="true"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 3px">Prosentase Sampah(%)</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtprosSampah" runat="server" Width="100%"></asp:TextBox></td>
                                                                            </tr>
                                                                        </table>
                                                                        <!--end of inptan sampah-->
                                                                    </td>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td style="padding-left: 5px">Jumlah Sample</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtJmlSample" runat="server"></asp:TextBox></td>

                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td style="padding-left: 5px">Jumlah Sample Basah</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtJmlSampleBasah" runat="server"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr style="display: none">
                                                                    <td style="padding-left: 5px">Pros.Sample Basah</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtProsSB" runat="server"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr style="display: block">
                                                                    <td style="padding-left: 5px">Rata2 Kadar Air(%)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAvgKadarAir" runat="server"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr style="display: block">
                                                                    <td style="padding-left: 5px">Sampah (%)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSampah" runat="server" OnTextChanged="txtSampah_Change" AutoPostBack="true"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 5px">Berat Yang Dipotong</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtberatDiPotong" runat="server" ToolTip="Berat Kotor x Pros. Sample Basah (%)"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 5px">Berat Potongan</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBeratPotongan" runat="server" ToolTip="Berat Kotor x ((Rata2 Kadar Air - StdKA)+ Sampah))"></asp:TextBox></td>
                                                                    <%--<td style="width:20%">&nbsp;</td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left: 5px">Berat Bersih</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtBeratBersih" runat="server" ToolTip="Berat Kotor - Berat Potongan"></asp:TextBox></td>
                                                                    <td style="width: 20%">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="left kotak" valign="top"><b>Keputusan Akhir</b></td>
                                                                    <td class="kotak">
                                                                        <asp:RadioButton GroupName="kpts" ID="chkKA" value="1" Checked="true" runat="server" Text="Diterima (Berat Bersih)" /><br />
                                                                        <asp:RadioButton GroupName="kpts" ID="chkKA1" value="2" runat="server" Text="Diterima Setelah Sortir" /><br />
                                                                        <asp:RadioButton GroupName="kpts" ID="chkKA2" value="-1" runat="server" Text="Tidak Di terima" />
                                                                    </td>
                                                                    <td colspan="">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <small><i>(*) Hasil tusukan berdasarkan angka yang muncul pada Moisture Tester 
                                                        dengan pengambilan sample 25% - 30% dari jumlah total bal dalam 1x pengiriman</i></small>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height: 3px;"></td>
                                    </tr>

                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
