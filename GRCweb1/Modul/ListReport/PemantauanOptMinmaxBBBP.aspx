<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanOptMinmaxBBBP.aspx.cs" Inherits="GRCweb1.Modul.ListReport.PemantauanOptMinmaxBBBP" %>

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
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%;" >
                    <tbody>
                        <tr>
                            <td style="height: 20px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;PEMANTAUAN OPTIMUM MIN-MAX BAHAN BAKU DAN BAHAN PENUNJANG</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="6">Bulan&nbsp; &nbsp;
                                        <asp:DropDownList ID="ddlBulan" runat="server" Height="22px"
                                            Width="132px">
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
                                            &nbsp; &nbsp;
                                        Tahun &nbsp; &nbsp;
                                        <asp:DropDownList ID="ddTahun" runat="server">
                                            <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">&nbsp;
                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click"
                                            Text="Preview" />
                                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                                Text="Export To Excel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFCC" BorderColor="#CCFFCC" Wrap="True"
                                                 HorizontalAlign="Center" ScrollBars="Auto">
                                                PEMANTAUAN OPTIMUM MIN-MAX BAHAN BAKU DAN BAHAN PENUNJANG
                                            <br />
                                                Periode &nbsp;:
                                            <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                                                <br />
                                                <div id="DivRoot" align="left">
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                        <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                            HorizontalAlign="Center" PageSize="20" Style="margin-right: 0px" Width="98%"
                                                            OnRowDataBound="GrdDynamic_RowDataBound">
                                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma" Font-Size="XX-Small"
                                                                BackColor="#CCCCCC" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden">
                                                    </div>
                                                </div>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="80%" align="right">Total Item Min-Max</td>
                                                        <td>&nbsp;=&nbsp;<asp:Label ID="Lbl_0" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="80%">Total Standard Nilai perbulan</td>
                                                        <td>&nbsp;=&nbsp;<asp:Label ID="Lbl_1" runat="server">0</asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="80%">Prosentase Pencapaiaan Min Max ( % )</td>
                                                        <td>&nbsp;=&nbsp;<asp:Label ID="Lbl_3" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
