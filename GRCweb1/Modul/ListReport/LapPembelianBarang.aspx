<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPembelianBarang.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPembelianBarang" %>
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
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }       
</script>  


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 49px; width: 100%">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;Laporan Pembelian Barang</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color: #B0C4DE">
                                    <table class="tblForm" id="Table4" style="width: 103%;" >
                                        <tr>
                                            <td style="width: 197px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 204px; height: 3px" valign="top">
                                            </td>
                                            <td style="height: 3px; width: 169px;" valign="top">
                                            </td>
                                            <td style="width: 209px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; </span>Bulan
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px">
                                                    <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                    <asp:ListItem>Januari</asp:ListItem>
                                                    <asp:ListItem>Februari</asp:ListItem>
                                                    <asp:ListItem>Maret</asp:ListItem>
                                                    <asp:ListItem>April</asp:ListItem>
                                                    <asp:ListItem>Mei</asp:ListItem>
                                                    <asp:ListItem>Juni</asp:ListItem>
                                                    <asp:ListItem>Juli</asp:ListItem>
                                                    <asp:ListItem>Agustus</asp:ListItem>
                                                    <asp:ListItem>September</asp:ListItem>
                                                    <asp:ListItem>Oktober</asp:ListItem>
                                                    <asp:ListItem>November</asp:ListItem>
                                                    <asp:ListItem>Desember</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; </span>Tahun
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <%--<asp:TextBox ID="txtTahun" runat="server" BorderStyle="Groove" Height="22px" Width="95px"> </asp:TextBox>--%>
                                                <asp:DropDownList ID="txtTahun" runat="server" ></asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Laporan</span>
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:DropDownList ID="ddlTipeSPP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged"
                                                    Width="233px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px;">
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <%--<input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                                                    background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Preview" />--%>
                                                 <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_ServerClick" Text="Preveiw" />
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
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
