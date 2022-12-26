<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapSmtPelarian.aspx.cs" Inherits="GRCweb1.Modul.SarMut.LapSmtPelarian" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
  // fix for deprecated method in Chrome 37 / js untuk bantu view modal dialog
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
    <div class="table-responsive" style="width:100%">
     <table >
            <tr>
                <td >
                Bulan
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" Width="132px" 
                        AutoPostBack="True" onselectedindexchanged="ddlBulan_SelectedIndexChanged">
                    <asp:ListItem Value="1">Januari</asp:ListItem>
                    <asp:ListItem Value="2">Februari</asp:ListItem>
                    <asp:ListItem Value="3">Maret</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">Mei</asp:ListItem>
                    <asp:ListItem Value="6">Juni</asp:ListItem>
                    <asp:ListItem Value="7">Juli</asp:ListItem>
                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                    <asp:ListItem Value="12">Desember</asp:ListItem>
                </asp:DropDownList>
                 &nbsp;&nbsp; Tahun
                <asp:DropDownList ID="ddTahun" runat="server">
                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Line
                <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlLine_SelectedIndexChanged">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;Group Produksi
                <asp:DropDownList ID="ddlGroup" runat="server">
                    </asp:DropDownList>
                &nbsp;&nbsp;Ukuran
                    <asp:DropDownList ID="ddlUkuran" runat="server">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold;
                                                font-size: 11px;" Text="Preview" OnClick="Priview" /></td>
            </tr>
        <tr>
            <td>
                
            </td>
        </tr>
        </table>
        </div>
</asp:Content>
