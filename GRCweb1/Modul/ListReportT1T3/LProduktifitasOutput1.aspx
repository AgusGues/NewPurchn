<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LProduktifitasOutput1.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LProduktifitasOutput1" %>
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


        function btnHitung_onclick() {

        }

    </script>

    <table style="width: 100%; font-size: x-small;" bgcolor="#CCCCCC">
        <tr>
            <td colspan="2">
                PENCAPAIAN OUTPUT
            </td>
        </tr>
        <tr>
            <td>
                Bulan
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" Width="132px">
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
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Edit" />
            </td>
            <td align="right">
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Reset" ToolTip="Hitung Ulang" />
                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtdrtanggal_TextChanged" Visible="False"></asp:TextBox>
                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal">
                </cc1:CalendarExtender>
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtdrtanggal_TextChanged" Visible="False"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#CCCCFF" Wrap="False"
                    HorizontalAlign="Center" Visible="False">
                    <table style="overflow: hidden; background-color: #FFFFFF; color: #FFFFFF; width: 100%;">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Simpan" />
                            </td>
                        </tr>
                        <tr>
                            <div class="contentlist" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                    id="baList">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th rowspan="1" style="width: 5%" class="kotak">
                                                ID
                                            </th>
                                            <th rowspan="1" style="width: 5%" class="kotak">
                                                Line
                                            </th>
                                            <th style="width: 10%" class="kotak">
                                                GP
                                            </th>
                                            <th style="width: 10%"  class="kotak">
                                                Waktu(Menit)
                                            </th>
                                            <th rowspan="1" style="width: 10%" class="kotak">
                                                Ketebalan
                                            </th>
                                            <th rowspan="1" style="width: 10%" class="kotak">
                                                Target(M3)
                                            </th>
                                            <th rowspan="1" style="width: 10%" class="kotak">
                                                Output(M3)
                                            </th>
                                            <th colspan="1" style="width: 10%" class="kotak">
                                                Output(M3/Shift)
                                            </th>
                                            <th rowspan="1" style="width: 10%">
                                                konversi
                                            </th>
                                            <th rowspan="1" style="width: 10%">
                                                Persen
                                            </th>
                                            <th rowspan="1" style="width: 10%">
                                                stdTarget
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstPrs" runat="server" OnItemDataBound="lstPrs_DataBound" >
                                            <ItemTemplate>
                                                <tr class="OddRows baris" style="font-weight: bold">
                                                    <td class="kotak" nowrap="nowrap" style="width: 5%">
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID", "{0:N}").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td class="kotak" nowrap="nowrap" style="width: 5%">
                                                        <%# Eval("Line")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("GP").ToString() %>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <asp:TextBox ID="txtWaktu" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("WaktuMenit", "{0:N0}").ToString()%>'
                                                            AutoPostBack="True" OnTextChanged="txWaktu_TextChanged" Width="100%" onfocus="this.select()"></asp:TextBox>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("Ketebalan")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("TargetM3")%>
                                                    </td>
                                                    <td class="kotak" style="width: 10%">
                                                        <%# Eval("OutputM3")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("OutputM3Shift")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("konversi")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("Persen", "{0:n}")%>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 10%">
                                                        <%# Eval("stdTarget")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridEdit" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowDataBound="GridEdit_RowDataBound" Visible="False">
                                    <Columns>
                                        <asp:BoundField DataField="Line" HeaderText="Line" />
                                        <asp:BoundField DataField="GP" HeaderText="GP" />
                                        <asp:TemplateField HeaderText="Waktu(Menit)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWaktu" runat="server" AutoPostBack="True" Height="21px" Width="100%"
                                                    Visible="True" OnTextChanged="txtGridTo_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ketebalan" HeaderText="Ketebalan" />
                                        <asp:BoundField DataField="Target(M3)" HeaderText="Target(M3)" />
                                        <asp:BoundField DataField="Produktifitas(M3)" HeaderText="Produktifitas(M3)" />
                                        <asp:BoundField DataField="Produktifitas(M3/Shift)" HeaderText="Produktifitas(M3/Shift)" />
                                        <asp:BoundField DataField="konversi" HeaderText="konversi" />
                                        <asp:BoundField DataField="Persen" DataFormatString="{0:n}" HeaderText="Persen" />
                                        <asp:BoundField DataField="stdTarget" HeaderText="Target" />
                                    </Columns>
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCFFFF" Wrap="False"
                    HorizontalAlign="Center">
                    REKAP OUTPUT LINE
                    <asp:Label ID="LblLine" runat="server"></asp:Label>
                    <br />
                    Periode &nbsp;:
                    <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                    <br />
                    <div id="DivRoot" align="Center">
                        <div style="overflow: hidden; background-color: #FFFFFF; color: #FFFFFF;" id="DivHeaderRow">
                        </div>
                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <table style="overflow: hidden; background-color: #FFFFFF; color: #FFFFFF; width: 100%;">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                            HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                            Style="margin-right: 0px" Width="98%" OnRowDataBound="GrdDynamic_RowDataBound">
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma"
                                                Font-Size="XX-Small" BackColor="#66CCFF" />
                                            <PagerStyle BorderStyle="Solid" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel2" runat="server" Font-Size="X-Small" Visible="False">
                                <table style="width: 100%; font-size: x-small;">
                                    <tr>
                                        <td style="height: 19px" width="10%">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px" width="10%">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td colspan="6" style="height: 19px">
                                            Karawang,
                                            <asp:Label ID="LblTgl6" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px" width="10%">
                                            &nbsp; Grand Total
                                        </td>
                                        <td align="right" style="height: 19px">
                                            <asp:Label ID="LblTgl2" runat="server"></asp:Label>
                                        </td>
                                        <td style="height: 19px">
                                            mm
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td style="height: 19px" colspan="4">
                                            Dibuat :
                                        </td>
                                        <td style="height: 19px">
                                            &nbsp;
                                        </td>
                                        <td colspan="4" style="height: 19px">
                                            Disetujui :
                                        </td>
                                        <td colspan="2" style="height: 19px" width="20%">
                                            &nbsp; Mengetahui :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp; Total Hari Kerja
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="LblTgl3" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            hari
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
                                            &nbsp; Rata2 / hari
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="LblTgl4" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            mm/hari
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
                                            &nbsp; Grand Total
                                        </td>
                                        <td align="right">
                                            &nbsp;<asp:Label ID="LblTgl5" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            m3
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="4">
                                            Maulana Yusuf
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="4">
                                            Linda Kusumastutie
                                        </td>
                                        <td colspan="2">
                                            &nbsp; Ir. Makruf N
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <div id="DivFooterRow" style="overflow: hidden">
                            &nbsp;</div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
