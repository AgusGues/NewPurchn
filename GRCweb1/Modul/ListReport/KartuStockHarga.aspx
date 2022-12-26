<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KartuStockHarga.aspx.cs" Inherits="GRCweb1.Modul.ListReport.KartuStockHarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function Cetak() {

            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=LapRekapPakai", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

    </script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
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
            MyPopUpWin("../Report/Report.aspx?IdReport=kartustockharga", 900, 800)
        }
    </script>
    <div  runat="server" class="table-responsive" style="width:100%">
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td style="width: 31px">&nbsp;</td>
            <td style="width: 184px">&nbsp;</td>
            <td style="width: 354px">&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 184px; font-size: x-small;">Pencarian Nama Barang</td>
            <td style="width: 354px; height: 19px">
                <asp:TextBox ID="txtCari" runat="server" BorderStyle="Groove" AutoPostBack="true"
                    ReadOnly="False" Width="599px" OnTextChanged="txtCari_TextChanged"
                    onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 184px">Nama Item</td>
            <td style="width: 354px">
                <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="True"
                    Width="600px" OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>

        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 184px">Periode</td>
            <td style="width: 354px">
                <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="True"
                    Checked="True" GroupName="g1" OnCheckedChanged="RadioButton1_CheckedChanged"
                    Text="1 Bulan" />
                &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="True" GroupName="g1"
                    OnCheckedChanged="RadioButton2_CheckedChanged" Text="6 Bulan" />
                &nbsp;<asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="True" GroupName="g1"
                    Text="1 Tahun" OnCheckedChanged="RadioButton3_CheckedChanged" />
            </td>
            <td>&nbsp;</td>

        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 184px">&nbsp;</td>
            <td style="width: 354px">
                <asp:Label ID="lblBulan" runat="server" Text="Dari "></asp:Label>
                <asp:DropDownList ID="ddlBulan1" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlBulan1_SelectedIndexChanged">
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
                &nbsp;<asp:TextBox ID="txtTahun1" runat="server" BorderStyle="Groove" AutoPostBack="true"
                    ReadOnly="False" Width="47px"
                    onkeyup="this.value=this.value.toUpperCase()"
                    OnTextChanged="txtTahun1_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="s/d" Visible="False"></asp:Label>
                &nbsp;&nbsp;
                <asp:DropDownList ID="ddlBulan2" runat="server" Enabled="False" Visible="False">
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
                <asp:TextBox ID="txtTahun2" runat="server" BorderStyle="Groove" AutoPostBack="true"
                    ReadOnly="True" Width="47px" OnTextChanged="txtCari_TextChanged"
                    onkeyup="this.value=this.value.toUpperCase()" Visible="False"></asp:TextBox></td>
            <td>&nbsp;</td>

        </tr>
        <tr>
            <td style="width: 31px">&nbsp;</td>
            <td style="width: 184px">
                <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cetak" />
                </td>
            <td style="width: 354px">&nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>
