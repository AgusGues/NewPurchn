<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ISO_UPDreport.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.ISO_UPDreport" %>
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
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

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
                                                            <strong>&nbsp;REKAP UDB , UPD , DAN PEMUSNAHAN DOKUMEN ISO</strong> &nbsp;
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
                                                                                        </caption>
                                                                                        <tr>
                                                                                            <td style="width: 303px; height: 6px" valign="top">
                                                                                                <span style="font-size: 10pt">&nbsp;<b> </b><span style="font-family: Calibri; font-weight: bold;">
                                                                                                    Dari Periode :</span></span>
                                                                                            </td>
                                                                                            <td rowspan="1" style="width: 278px; height: 19px">
                                                                                                <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="233px"
                                                                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 169px; height: 19px">
                                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                                                    TargetControlID="txtToPostingPeriod">
                                                                                                </cc1:CalendarExtender>
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
                                                                                            <td style="width: 303px; height: 6px" valign="top">
                                                                                                <span style="font-size: 10pt">&nbsp;<b> </b><span style="font-family: Calibri; font-weight: bold;">
                                                                                                    Sampai Periode :</span></span>
                                                                                            </td>
                                                                                            <td rowspan="1" style="width: 278px; height: 19px">
                                                                                                <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Groove" Width="233"
                                                                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 169px; height: 19px">
                                                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                                                                    TargetControlID="txtFromPostingPeriod">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 209px; height: 6px" valign="top">
                                                                                            </td>
                                                                                            <td style="width: 205px; height: 6px" valign="top">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 303px; height: 6px" valign="top">
                                                                                                <span style="font-size: 10pt">&nbsp; </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 303px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 303px; height: 19px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td style="width: 278px; height: 19px;">
                                                                                                <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white;
                                                                                                    font-weight: bold; font-size: 11px; height: 22px;" type="button" value="Preview" />
                                                                                            </td>
                                                                                        </tr>
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
