<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanAnalisaResiko.aspx.cs" Inherits="GRCweb1.Modul.ISO.PemantauanAnalisaResiko" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="table-responsive" style="width:100%">
     <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
        <tr>
            <td style="width: 10%;">
                &nbsp
            </td>
            <td style="width: 25%">
                &nbsp
            </td>
            <td style="width: 10%;">
                &nbsp
            </td>
            <td style="width: 20%;">
                &nbsp
            </td>
            <td style="width: 10%;">
                &nbsp
            </td>
        </tr>
        <tr>
            <td style="width: 10%;">
                &nbsp; Periode :
            </td>
            <td style="width: 25%">
                &nbsp;<asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="True" Width="30%">
                    <asp:ListItem Value="0">-- Pilih --</asp:ListItem>
                    <asp:ListItem Value="1">Semester I</asp:ListItem>
                    <asp:ListItem Value="2">Semester II</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" Width="17%">
                </asp:DropDownList>
            </td>
            <td style="width: 20%;">
                &nbsp<asp:DropDownList ID="ddlDepartemen" runat="server" AutoPostBack="True" Width="45%"
                    Enabled="true">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="btnCetak" runat="server" Text="Preview" OnClick="btnPrint_ServerClick" />
            </td>
            <td style="width: 10%;">
                <asp:Label ID="idArisk" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
        </div>
</asp:Content>
