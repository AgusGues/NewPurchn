<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LSiapKirim.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LSiapKirim" %>
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
                DivHR.style.width = (parseInt(width - 1)) + '%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                DivHR.style.verticalAlign = 'top';

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
    
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelAtas" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                <table style="width: 100%; font-size: x-small;">
                    <tr>
                        <td colspan="2" align="center" style="font-size: large">
                            <b>LAPORAN STOCK MARKETING</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RBPBJ" runat="server" GroupName="a" Text="" AutoPostBack="True"
                                Checked="True" OnCheckedChanged="RBPBJ_CheckedChanged" />Produk BJ
                            <asp:RadioButton ID="RBPBP" runat="server" GroupName="a" Text="" AutoPostBack="True"
                                OnCheckedChanged="RBPBP_CheckedChanged" />Produk BP
                            <asp:RadioButton ID="RBPBS" runat="server" GroupName="a" Text="" AutoPostBack="True"
                                OnCheckedChanged="RBPBS_CheckedChanged" />Produk BS
                            &nbsp;<asp:RadioButton ID="RBPEFO" runat="server" GroupName="a" Text="" AutoPostBack="True"
                                OnCheckedChanged="RBPBS_CheckedChanged" />EFO
                            <%--Tambahan--%>
                            &nbsp;<asp:RadioButton ID="RBMKT" runat="server" GroupName="a" Text=""
                                AutoPostBack="True" OnCheckedChanged="RBMKT_CheckedChanged" />Barang Marketing
                            &nbsp;<asp:RadioButton ID="RBPRM" runat="server" GroupName="a" Text=""
                                AutoPostBack="True" OnCheckedChanged="RBPRM_CheckedChanged" />Barang Promosi & Aksesoris
                            <%--Selesai Tambahan--%>
                            <%--Tambahan--%>
                            &nbsp;<asp:RadioButton ID="RBNoPrint" runat="server" GroupName="a" Text=""
                                AutoPostBack="True" OnCheckedChanged="RBNoPrint_CheckedChanged" />Barang Belum Print
                            <%--Selesai Tambahan--%>
                        </td>
                        <td align="right">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh" Height="21px" />
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Export To Excel"
                                Height="21px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelSatu" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                <table style="width: 100%; font-size: x-small;">
                    <tr>
                        <td style="height: 19px;" colspan="2">
                            <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                                <table style="width: 100%; font-size: x-small;" bgcolor="#999999">
                                    <tr>
                                        <td style="height: 23px; background-color: #808080; color: #FFFFFF;">
                                            Kriteria
                                        </td>
                                        <td style="height: 23px">
                                            Group
                                            <asp:DropDownList ID="ddlGroup" runat="server" Width="150px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="height: 23px">
                                            Tebal dari
                                            <asp:DropDownList ID="ddlTebal1" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlTebal1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;s/d
                                            <asp:DropDownList ID="ddlTebal2" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlTebal2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkTebal" runat="server" Text="" AutoPostBack="True"
                                                OnCheckedChanged="chkTebal_CheckedChanged" />ALL
                                        </td>
                                        <td align="center" style="height: 23px">
                                            Lebar dari
                                            <asp:DropDownList ID="ddlLebar1" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlLebar1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;s/d&nbsp;
                                            <asp:DropDownList ID="ddlLebar2" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlLebar2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkLebar" runat="server" Text="" AutoPostBack="True"
                                                OnCheckedChanged="chkLebar_CheckedChanged" />ALL
                                        </td>
                                        <td align="center" style="height: 23px">
                                            Panjang dari
                                            <asp:DropDownList ID="ddlPanjang1" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlPanjang1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;s/d
                                            <asp:DropDownList ID="ddlPanjang2" runat="server" Enabled="False" Width="60px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlPanjang2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:CheckBox ID="chkPanjang" runat="server" Text="" AutoPostBack="True"
                                                OnCheckedChanged="chkPanjang_CheckedChanged" />ALL
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF">
                                <table id="thr0" style="width: 100%;">
                                    <tr>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT CITEUREUP
                                        </td>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT KARAWANG
                                        </td>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT JOMBANG
                                        </td>
                                        <tr>
                                            <td colspan="3">
                                                <div id="DivRoot" align="left">
                                                    <div id="DivHeaderRow" style="overflow: hidden;">
                                                    </div>
                                                    <div id="DivMainContent" onscroll="OnScrollDiv(this)" style="overflow: scroll;">
                                                        <table id="thr" style="width: 100%;">
                                                            <tr>
                                                                <td valign="top" width="33%">
                                                                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                        PageSize="15" Width="100%">
                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                        <PagerStyle BorderStyle="Solid" />
                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td valign="top" width="33%">
                                                                    <asp:GridView ID="GrdDynamic0" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                        PageSize="15" Width="100%">
                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                        <PagerStyle BorderStyle="Solid" />
                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td valign="top" width="33%">
                                                                    <asp:GridView ID="GrdDynamic1" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                        PageSize="15" Width="100%">
                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                        <PagerStyle BorderStyle="Solid" />
                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden">
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--Tambahan--%>
            <asp:Panel ID="PanelDua" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                <table style="width: 100%; font-size: x-small;">
                    <%-- <tr>
                        <td style="height: 19px;" colspan="2">                           
                                <table style="width: 100%; font-size: x-small;">
                                    <tr>
                                        <td style="height: 23px; background-color: #808080; color: #FFFFFF;">
                                        </td>
                                    </tr>
                                </table>                           
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelMKT" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF">
                                <table id="Table1" style="width: 100%;">
                                    <tr>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT CITEUREUP
                                        </td>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT KARAWANG
                                        </td>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT JOMBANG
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="Div1" align="left">
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="Div3">
                                                    <table id="Table2" style="width: 100%;">
                                                        <tr>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicMKT" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicMKT2" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicMKT3" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelDua2" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                <table style="width: 100%; font-size: x-small;">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PanelPromo" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF">
                                <table id="Table3" style="width: 100%;">
                                    <tr>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT CITEUREUP
                                        </td>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT KARAWANG
                                        </td>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT JOMBANG
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="Div5" align="left">
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="Div7">
                                                    <table id="Table4" style="width: 100%;">
                                                        <tr>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicPromo" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicPromo2" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GrdDynamicPromo3" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="33%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--Selesai Tambahan--%>
            <asp:Panel ID="PanelTiga" runat="server" BackColor="#CCCCCC" Font-Size="X-Small">
                <table style="width: 100%; font-size: x-small;">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel4" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF">
                                <table id="Table5" style="width: 100%;">
                                    <tr>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT CITEUREUP
                                        </td>
                                        <td valign="top" width="33%" style="font-family: Calibri; font-weight: bold">
                                            PLANT KARAWANG
                                        </td>
                                        <td style="font-family: Calibri; font-weight: bold" valign="top" width="33%">
                                            PLANT JOMBANG
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="Div2" align="left">
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="Div4">
                                                    <table id="Table6" style="width: 100%;">
                                                        <tr>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GridNoPrint" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GridNoPrint2" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="100%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                            <td valign="top" width="33%">
                                                                <asp:GridView ID="GridNoPrint3" runat="server" AutoGenerateColumns="False" Font-Size="Medium"
                                                                    PageSize="15" Width="33%">
                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Gold" />
                                                                    <PagerStyle BorderStyle="Solid" />
                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
