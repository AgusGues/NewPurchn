<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPakaiNew.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapPakaiNew" %>
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

            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=LapRekapPakai", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

    </script>

    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

    </script>

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = '98.5%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                //***DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + '%';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '0';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width)) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="table-layout: fixed" width="100%">
                <tbody>
                    <tr>
                        <td style="height: 20px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        <strong>&nbsp;REKAP PEMAKAIAN</strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                        <asp:RadioButton ID="RBDetail" runat="server" Checked="True" GroupName="a" Text="Detail" />
                                        <asp:RadioButton ID="RBRekap" runat="server" GroupName="a" Text="Rekap" />
                                        &nbsp; &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Dari Tanggal"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Department
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" CssClass="MyImageButton"
                                            OnDataBound="ddlDeptName_DatabOund" Width="233px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTgl1">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="s/d Tanggal"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Group
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipeSPP" runat="server" AutoPostBack="True" Width="233px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTgl2">
                                        </cc1:CalendarExtender>
                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Preview" />
                                        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                                        <asp:Button ID="btnPrint0" runat="server" OnClick="btnPrint_ServerClick" Text="Cetak" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFCC" BorderColor="#CCFFCC" Wrap="False"
                                            Height="500px" HorizontalAlign="Right" ScrollBars="Auto">
                                            REKAP PEMAKAIAN
                                            <br />
                                            Periode &nbsp;:
                                            <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                                            <br />
                                            
                                                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                        HorizontalAlign="Center" PageSize="20" Style="margin-right: 0px" Width="98%"
                                                        OnRowDataBound="GrdDynamic_RowDataBound">
                                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma" Font-Size="XX-Small"
                                                            BackColor="#CCCCCC" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                    </asp:GridView>
                                                
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
