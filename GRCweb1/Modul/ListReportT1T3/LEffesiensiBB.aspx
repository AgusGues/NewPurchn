<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LEffesiensiBB.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LEffesiensiBB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = '99%';
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


        function btnHitung_onclick() {

        }

    </script>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
        <table style="width: 100%; font-size: x-small;" bgcolor="#CCCCCC">
            <tr>
                <td colspan="2">PEMANTAUAN EFFESIENSI BAHAN BAKU
                </td>
            </tr>
            <tr>
                <td>Bulan
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                    Width="132px">
                    <%--<asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>--%>
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
                <asp:DropDownList ID="ddlLine" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                </asp:DropDownList>
                    &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" />
                </td>
                <td align="right">
                    <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                        OnTextChanged="txtdrtanggal_TextChanged" Visible="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                        TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                    <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                        OnTextChanged="txtdrtanggal_TextChanged" Visible="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCFFFF"
                        Wrap="False" Height="500px" HorizontalAlign="Center">
                        PEMANTAUAN EFFESIENSI BAHAN BAKU LINE
                    <asp:Label ID="LblLine" runat="server"></asp:Label>
                        <br />
                        Periode &nbsp;:
                    <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                        <br />
                        <div id="DivRoot" align="left">
                            <div style="overflow: hidden; background-color: #FFFFFF; color: #FFFFFF;"
                                id="DivHeaderRow">
                            </div>
                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                    HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                    Style="margin-right: 0px" Width="98%"
                                    OnRowDataBound="GrdDynamic_RowDataBound">
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma"
                                        Font-Size="XX-Small" />
                                    <PagerStyle BorderStyle="Solid" />
                                </asp:GridView>
                                Rekap Efisiensi
                            <asp:GridView ID="GrdDynamic1" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                HorizontalAlign="Center" OnRowCreated="grv1MergeHeader_RowCreated" PageSize="20"
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma"
                                    Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>

                                <asp:Panel ID="Panel2" runat="server" Font-Size="X-Small" Visible="False">
                                    <table style="width: 100%; font-size: x-small;">

                                        <tr>
                                            <td style="height: 19px" width="10%">&nbsp;</td>
                                            <td style="height: 19px" width="10%">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td colspan="6" style="height: 19px">Karawang,
                                            <asp:Label ID="LblTgl6" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 19px" width="10%">&nbsp; Grand Total</td>
                                            <td align="right" style="height: 19px">
                                                <asp:Label ID="LblTgl2" runat="server"></asp:Label>
                                            </td>
                                            <td style="height: 19px">mm</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td style="height: 19px" colspan="4">Dibuat :</td>
                                            <td style="height: 19px">&nbsp;</td>
                                            <td colspan="4" style="height: 19px">Disetujui :</td>
                                            <td colspan="2" style="height: 19px" width="20%">&nbsp; Mengetahui :</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; Total Hari Kerja</td>
                                            <td align="right">
                                                <asp:Label ID="LblTgl3" runat="server"></asp:Label>
                                            </td>
                                            <td>hari</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; Rata2 / hari</td>
                                            <td align="right">
                                                <asp:Label ID="LblTgl4" runat="server"></asp:Label>
                                            </td>
                                            <td>mm/hari</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; Grand Total</td>
                                            <td align="right">&nbsp;<asp:Label ID="LblTgl5" runat="server"></asp:Label></td>
                                            <td>m3</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="4">Maulana Yusuf</td>
                                            <td>&nbsp;</td>
                                            <td colspan="4">Linda Kusumastutie</td>
                                            <td colspan="2">&nbsp; Plant Manager</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div id="DivFooterRow" style="overflow: hidden">
                                &nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
