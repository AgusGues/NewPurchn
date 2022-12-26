<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapMasterDok.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.LapMasterDok" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
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

   <%-- <script language="JavaScript">
    
    void Page_Load()
    {
        string[] cookies = Request.Cookies.AllKeys;
        foreach (string cookie in cookies)
        {
            BulletedList1.Items.Add("Deleting " + cookie);
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }
    }
    
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }     
          
    </script>--%>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>
                                            <td style="height: 49px">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <strong>&nbsp;LAPORAN MASTER DOKUMEN ISO</strong> &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td height="100%" style="width: 100%">
                                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
                                    <tbody>
                                        <tr class="treeRow1" valign="top">
                                            <td>
                                                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" border="1" height="100%"
                                                    width="100%">
                                                    <tbody>
                                                        <tr style="width: 100%; height: 100%">
                                                            <td style="width: 100%; height: 100%;">
                                                                <div style="height: 100%; width: 100%;">
                                                                    <div>
                                                                        <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td style="height: 101px; width: 100%;">
                                                                                    <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                                                                        cellpadding="0" border="0">
                                                                                        <tr>
                                                                                            <td style="width: 112px; height: 6px; font-size: x-small; font-family: Calibri;"
                                                                                                valign="top">
                                                                                                <b>&nbsp; DEPARTMENT</b>
                                                                                            </td>
                                                                                            <td rowspan="1" style="width: 196px; height: 19px">
                                                                                                <asp:DropDownList ID="ddlDept" runat="server" Height="22px" Width="140px" Style="font-family: Calibri;
                                                                                                    font-size: x-small">
                                                                                                    <asp:ListItem Value="0">--- Pilih Department ---</asp:ListItem>
                                                                                                    <asp:ListItem Value="100">Direksi</asp:ListItem>
                                                                                                    <asp:ListItem Value="101">Plant Manager</asp:ListItem>
                                                                                                    <%--<asp:ListItem Value="3">MR</asp:ListItem>--%>
                                                                                                    <asp:ListItem Value="23">ISO</asp:ListItem>
                                                                                                    <asp:ListItem Value="9">Quality Control</asp:ListItem>                                                                                                    
                                                                                                    <asp:ListItem Value="2">BoardMill</asp:ListItem>
                                                                                                    <asp:ListItem Value="3">Finishing</asp:ListItem>
                                                                                                    <asp:ListItem Value="7">HRD $ GA</asp:ListItem>
                                                                                                    <asp:ListItem Value="11">PPIC</asp:ListItem>
                                                                                                    <asp:ListItem Value="6">Log Produk Jadi</asp:ListItem>
                                                                                                    <asp:ListItem Value="10">Log SparePart</asp:ListItem>
                                                                                                    <asp:ListItem Value="14">IT</asp:ListItem>
                                                                                                    <asp:ListItem Value="4">Maintenance</asp:ListItem>
                                                                                                    <asp:ListItem Value="26">Transportation</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 112px; height: 19px; font-size: x-small; font-family: Calibri;"
                                                                                                valign="top">
                                                                                                <b>&nbsp; MASTER DOKUMEN</b>
                                                                                            </td>
                                                                                            <td rowspan="1" style="width: 196px; height: 19px">
                                                                                                <asp:DropDownList ID="ddlMaster" runat="server" Height="21px" Style="font-family: Calibri;
                                                                                                    font-size: x-small" Width="162px">
                                                                                                    <asp:ListItem Value="0">---Pilih Master Dokumen---</asp:ListItem>
                                                                                                    <asp:ListItem Value="1">Pedoman Mutu</asp:ListItem>
                                                                                                    <asp:ListItem Value="2">Instruksi Kerja</asp:ListItem>
                                                                                                    <asp:ListItem Value="3">Form</asp:ListItem>
                                                                                                    <asp:ListItem Value="4">Prosedure</asp:ListItem>
                                                                                                    <asp:ListItem Value="5">Rencana Mutu</asp:ListItem>
                                                                                                    <asp:ListItem Value="6">Standar</asp:ListItem>
                                                                                                    <asp:ListItem Value="7">Dokumen Eksternal</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 92px; height: 19px">
                                                                                            </td>
                                                                                            <td style="width: 209px; height: 19px" valign="top">
                                                                                            </td>
                                                                                            <td style="width: 205px; height: 19px" valign="top">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 112px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 112px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td style="width: 196px; height: 19px;">
                                                                                                <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white;
                                                                                                    font-weight: bold; font-size: 11px; height: 22px;" type="button" value="Preview" />
                                                                                            </td>
                                                                                            <td style="width: 92px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td style="width: 209px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td style="width: 205px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <caption>
                                                                                            <hr size="1" width="100%"></hr>
                                                                                        </caption>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
