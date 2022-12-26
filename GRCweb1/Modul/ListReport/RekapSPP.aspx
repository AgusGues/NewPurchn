<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapSPP.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapSPP" %>
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
     <script type="text/javascript">

        function Cetak() {

            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=RekapSPP", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

    </script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
        <script language="JavaScript"  type="text/javascript">
            function imgChange(img) {
                document.LookUpCalendar.src = img;
            }

        </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
            <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>
                                            <td style="width: 90%; height: 49px">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <strong>Rekap SPP</strong>
                                                        </td>
                                                        <%--<td style="width: 75px">
                                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                                font-size: 11px;" type="button" value="Form SPP" onserverclick="btnUpdate_ServerClick" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                                <asp:ListItem Value="ScheduleNo">No SPP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 3px">
                                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <%--<hr />
                    <span style="font-size: 10pt">&nbsp; <strong>List</strong></span> --%>
                <hr />
                <table style="width: 100%; border-collapse:collapse">
                    <tr>
                        <td colspan="4">&nbsp;&nbsp;Periode Report :</td>
                    </tr>
                    <tr>
                        <td style="width: 31px">
                            &nbsp;
                        </td>
                        <td style="width: 144px">
                            <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                        </td>
                        <td style="width: 180px; height: 19px" valign="top">
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTgl1">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 31px">
                            &nbsp;
                        </td>
                        <td style="width: 144px">
                            <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                        </td>
                        <td style="width: 180px; height: 19px" valign="top">
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTgl2">
                            </cc1:CalendarExtender>
                            <input ID="btnPrint" runat="server" onserverclick="BtnPrint_ServerClick" type="button" value="Cetak" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 31px">&nbsp;
                        </td>
                        <td style="width: 144px">
                            
                        </td>
                        <td>
                            &nbsp;</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
