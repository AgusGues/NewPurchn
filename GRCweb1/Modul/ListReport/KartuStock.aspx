<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KartuStock.aspx.cs" Inherits="GRCweb1.Modul.ListReport.KartuStock" %>
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
        label,td,span{font-size:12px;}
    </style>
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

    <script type="text/javascript" src="../../Scripts/jquery-1.6.2.min.js"></script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
    <script language="javascript" type="text/javascript" src="../../Scripts/jquery.fixedheadertable.js"></script>

    <script type="text/javascript">
        $(document).ready(function(){
           
        });
        function Cetak() {

            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=LapRekapPakai", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

    
            function imgChange(img) {
                document.LookUpCalendar.src = img;
            }

        function fixHead()
        {
             jQuery('#listStk').fixedHeaderTable();
        }
    </script>
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="div1" runat="server">
        <table style="table-layout:fixed; width:100%; height:100%; border-collapse:collapse; font-size:x-small">
            <tr>
                <td style="width:100%; height:49px">
                    <table class="nbTableHeader">
                        <td style="width:100%; padding-left:10px">
                            KARTU STOCK
                        </td>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content" style="background:#fff;">
                        <table style="width: 100%; font-size: x-small;">
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:10%">
                                    &nbsp;
                                </td>
                                <td style="width:20%">Pencarian Nama Barang</td>
                                <td style="width:40%">
                                    <asp:TextBox ID="txtCari" runat="server" AutoPostBack="true" Width="100%" 
                                        OnTextChanged="txtCari_TextChanged" onkeyup="this.value=this.value.toUpperCase()">
                                    </asp:TextBox>
                                    
                                </td>
                                <td style="width:20%">
                                    <asp:CheckBox ID="ChkNonAktif" runat="server" Text="Termasuk barang Non Aktif" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Nama Item</td>
                                <td><asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="true" Width="100%"
                                        OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td>&nbsp</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Periode</td>
                                <td><asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" Checked="true"
                                    GroupName="g1" OnCheckedChanged="RadioButton1_CheckedChanged" Text="1 Bulan" />
                                    &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="true" GroupName="g1"
                                        OnCheckedChanged="RadioButton2_CheckedChanged" Text="6 Bulan" />
                                    &nbsp;<asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="true" GroupName="g1"
                                        Text="1 Tahun" OnCheckedChanged="RadioButton3_CheckedChanged" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lblBulan" runat="server" Text="Dari "></asp:Label> :&nbsp;
                                    <asp:DropDownList ID="ddlBulan1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan1_SelectedIndexChanged">
                                        <asp:ListItem Value="01">Januari</asp:ListItem>
                                        <asp:ListItem Value="02">Februari</asp:ListItem>
                                        <asp:ListItem Value="03">Maret</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">Mei</asp:ListItem>
                                        <asp:ListItem Value="06">Juni</asp:ListItem>
                                        <asp:ListItem Value="07">Juli</asp:ListItem>
                                        <asp:ListItem Value="08">Agustus</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">Oktober</asp:ListItem>
                                        <asp:ListItem Value="11">Nopember</asp:ListItem>
                                        <asp:ListItem Value="12">Desember</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtTahun1" runat="server" AutoPostBack="true"
                                        ReadOnly="false" Width="47px" onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtTahun1_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Label ID="Label3" runat="server" Text="s/d" Visible="false"></asp:Label>
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlBulan2" runat="server" Enabled="false" Visible="false">
                                        <asp:ListItem Value="01">Januari</asp:ListItem>
                                        <asp:ListItem Value="02">Februari</asp:ListItem>
                                        <asp:ListItem Value="03">Maret</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">Mei</asp:ListItem>
                                        <asp:ListItem Value="06">Juni</asp:ListItem>
                                        <asp:ListItem Value="07">Juli</asp:ListItem>
                                        <asp:ListItem Value="08">Agustus</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">Oktober</asp:ListItem>
                                        <asp:ListItem Value="11">Nopember</asp:ListItem>
                                        <asp:ListItem Value="12">Desember</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtTahun2" runat="server" AutoPostBack="true"
                                        ReadOnly="true" Width="47px" OnTextChanged="txtCari_TextChanged" onkeyup="this.value=this.value.toUpperCase()"
                                        Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" Visible="true" />&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Cetak" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="contentlist" style="height: 340px">
                        <div id="lstPrev" runat="server"  visible="false">
                            <table id="listStk" style="width:100%; border-collapse:collapse; font-size:x-small; display:block;">
                                <tr valign="top">
                                    <td style="width:80%" class="kotak">
                                        <table style="width:100%; border-collapse:collapse;font-size:x-small">
                                            <tr class="tbHeader">
                                                <th style="width:8%" class="kotak">Tanggal</th>
                                                <th style="width:10%" class="kotak">Bon No.</th>
                                                <th style="width:30%" class="kotak">Keterangan</th>
                                                <th style="width:10%" class="kotak">Masuk</th>
                                                <th style="width:10%" class="kotak">Keluar</th>
                                                <th style="width:10%" class="kotak">Saldo</th>
                                            </tr>
                                            <asp:Repeater ID="lstTrans" runat="server" OnItemDataBound="lstTrans_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label1" runat="server" Text='<%# Eval("BonNo")%>'></asp:Label></td>
                                                        <td class="kotak"><%# Eval("Uraian") %><asp:Label ID="txtKet" runat="server"></asp:Label></td>
                                                        <td class="kotak angka"><%# Eval("Masuk","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Keluar", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("Saldo", "{0:N2}")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                <tr class="OddRows baris">
                                                        <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                        <td class="kotak tengah"><asp:Label ID="Label1" runat="server" Text='<%# Eval("BonNo")%>'></asp:Label></td>
                                                        <td class="kotak"><%# Eval("Uraian") %><asp:Label ID="txtKet" runat="server"></asp:Label></td>
                                                        <td class="kotak angka"><%# Eval("Masuk","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Keluar", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("Saldo", "{0:N2}")%></td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                    <td style="width:100%" class="content">
                                        <table style="width:100%; border-collapse:collapse; font-family:Courier New; font-size:x-small">
                                            <tr>
                                                <td colspan="2" class="line3 kotak tengah">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Material Info &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">Type :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtMatType" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">ItemCode :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtItemCode" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">ItemName :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtItemName" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">Unit :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtUnit" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><hr /></td>
                                            </tr>
                                             <tr>
                                                <td colspan="2" class="kotak tengah line3">Data Stock</td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">MinStock :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtMin" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">ReOrder :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtRe" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" class="kotak content angka">MaxStock :</td>
                                                <td style="width:60%" class="Kotak"><asp:Label ID="txtMax" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
