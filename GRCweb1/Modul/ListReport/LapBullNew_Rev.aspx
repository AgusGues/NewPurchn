<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapBullNew_Rev.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapBullNew_Rev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function PreviewPDF(ID) {
            //params = 'dialogWidth:1000px';
            //params += '; dialogHeight:600px'
            //params += '; top=0, left=0'
            //params += '; resizable:yes'
            //params += ';scrollbars:yes';
            //window.showModalDialog("../../ModalDialog/PDFPreviewLapBul.aspx?grp=" + ID, "Preview", params);
            //return false;
            MyPopUpWin("../../ModalDialog/PDFPreviewLapBul.aspx?grp=" + ID, 900, 800)
        };
    </script>
    <script type="text/javascript">
        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function Cetak() {
            MyPopUpWin("../Report/Report.aspx?IdReport=RekapLapBul", 900, 800)
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <%--<table style="width: 100%" cellpadding="0" cellspacing="2" border="0">--%>
                <table width="100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 90%; font-family: Calibri; font-size: medium; background-color: #C0C0C0; color: #0000FF; font-style: normal; font-weight: bold;"
                                        colspan="4" height="100%">&nbsp; <b>E-LAPORAN BULANAN INVENTORY</b>
                                    </td>
                                    <td style="width: 5%">
                                        <asp:Button ID="btnReleaseUlang" runat="server" OnClick="btnReleaseUlang_ServerClick" Text="Release Ulang" Enabled="true"
                                            Style="font-family: Calibri; font-weight: 700" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:Button ID="btnEmail" runat="server" OnClick="btnEmail_ServerClick" Text="Sent Email"
                                            Style="font-family: Calibri; font-weight: 700" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Panel Periode Normal --%>
                            <asp:Panel ID="PanelPeriode" runat="server" Visible="false" Width="100%">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <td style="font-family: Calibri; width: 8%; font-size: x-small;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span><span style="font-size: small"><b>PERIODE
                                            :</b></span>
                                        </td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtPeriode" runat="server" AutoPostBack="True" Height="30px" Width="100%"
                                                Style="background-color: #FFFFFF; font-family: Calibri; font-weight: 700; font-size: small; text-align: center;"
                                                ReadOnly="True" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:TextBox ID="txtTahun" runat="server" AutoPostBack="True" Height="30px" Width="100%"
                                                Style="background-color: #FFFFFF; font-family: Calibri; font-weight: 700; font-size: small; text-align: center;"
                                                ReadOnly="True" Enabled="False" BorderStyle="NotSet"></asp:TextBox>
                                        </td>
                                        <td style="width: 67%;">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- End Panel Periode Normal --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Panel Periode Back Month --%>
                            <asp:Panel ID="PanelPeriodeBack" runat="server" Visible="false" Width="100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-family: Calibri; font-size: x-small;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span><span style="font-size: small"><b>PERIODE
                                            :</b></span>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px"
                                                Style="font-family: Calibri">
                                                <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                <asp:ListItem Value="01">JANUARI</asp:ListItem>
                                                <asp:ListItem Value="02">FEBRUARI</asp:ListItem>
                                                <asp:ListItem Value="03">MARET</asp:ListItem>
                                                <asp:ListItem Value="04">APRIL</asp:ListItem>
                                                <asp:ListItem Value="05">MEI</asp:ListItem>
                                                <asp:ListItem Value="06">JUNI</asp:ListItem>
                                                <asp:ListItem Value="07">JULI</asp:ListItem>
                                                <asp:ListItem Value="08">AGUSTUS</asp:ListItem>
                                                <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                                <asp:ListItem Value="10">OKTOBER</asp:ListItem>
                                                <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                                <asp:ListItem Value="12">DESEMBER</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlTahun" runat="server" Height="19px" Width="100px"
                                                Style="font-family: Calibri">
                                                <asp:ListItem>Pilih Tahun</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                                <asp:ListItem Value="2023">2023</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_ServerClick" Text="Refresh"
                                                Style="font-family: Calibri; font-weight: 700" />
                                        </td>

                                        <td></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- End Panel Periode Back Month --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Header --%>
                            <asp:Panel ID="PanelFormHeader" runat="server" Visible="false" Width="100%">
                                <table style="width: 100%; font-size: x-small" border="0">
                                    <tr>
                                        <td style="font-family: Calibri; width: 20%;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span><span style="font-size: x-small">Group
                                                Purchn</span>
                                        </td>

                                        <%--<td style="width: 50%; height: 1px" valign="top">
                                            <asp:Label ID="LabelGroup" runat="server" Visible="false" Style="font-family: Calibri;
                                                font-size: x-small;" Font-Bold="True">&nbsp; Group Purchn</asp:Label>
                                        </td>--%>

                                        <td >
                                            <asp:TextBox ID="txtGroup" runat="server" BorderStyle="Dotted" Width="233" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="IDLap" runat="server" BorderStyle="Dotted" Width="10%" Visible="False"
                                                Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="IDGroup" runat="server" BorderStyle="Dotted" Width="10%" Visible="False"
                                                Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-family: Calibri; width: 10%;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;
                                            </span><span style="font-size: x-small">Periode Laporan</span>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtLap" runat="server" BorderStyle="Dotted" Width="233" Style="font-family: Calibri; font-size: x-small"
                                                Font-Bold="True"></asp:TextBox>
                                        </td>
                                        <td rowspan="1" visible="false">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Dotted" Width="233"
                                                Style="font-family: Calibri; font-size: x-small" Visible="False"></asp:TextBox>
                                        </td>
                                        <td rowspan="1" visible="false">
                                            <asp:TextBox ID="txtToPostingPeriod" runat="server" BorderStyle="Dotted" Width="233"
                                                Style="font-family: Calibri; font-size: x-small" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- End Header --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Panel Release --%>
                            <asp:Panel ID="PanelFormRelease" runat="server" Visible="false" Width="100%">
                                <div id="lst" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                    Font-Bold="True">&nbsp; ..:: LIST ::..</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Style="font-family: Calibri">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                            <ControlStyle Width="5%" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="" >
                                                            <ControlStyle />
                                                            <ItemTemplate >
                                                                <asp:CheckBox ID="Check" runat="server" Font-Size="Smaller"  />
                                                                Pilih
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="XX-Small" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="GroupID" HeaderText="" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="GroupDescription" HeaderText="Laporan Bulanan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Periode" HeaderText="Periode">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35%" />
                                                        </asp:BoundField>

                                                        <asp:ButtonField HeaderText="" CommandName="Add" Text="Pilih" ControlStyle-Width="10%"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                            <ControlStyle Width="10%" />
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>

                                                        <asp:ButtonField HeaderText="" CommandName="Add2" Text="Hapus" ControlStyle-Width="10%"
                                                            ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                            <ControlStyle Width="10%" />
                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>

                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" Width="5%" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="btnRelease" runat="server" type="button" value="Release" visible="false"
                                                    onserverclick="btnRelease_ServerClick" style="font-family: Calibri; font-weight: 700;" />
                                                <asp:Button ID="btnCetak" runat="server" OnClick="btnCetak_ServerClick" Text="Print"
                                                    Visible="false" Style="font-family: Calibri; font-weight: 700" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <table style="width: 100%">
                                        <%-- List Laporan Bulanan format PDF --%>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                    Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;" border="0">
                                                    <thead>
                                                        <tr class="tbHeader">
                                                            <th style="width: 5%" class="kotak">No</th>
                                                            <th style="width: 45%" class="kotak">Accounting Report</th>
                                                            <th style="width: 45%" class="kotak">Nama File</th>
                                                            <th style="width: 5%" class="kotak">View PDF</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstBA3" runat="server" OnItemDataBound="lstBA_DataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="kotak tengah">&nbsp;<%# Eval("ID") %></td>
                                                                    <td class="kotak tengah">&nbsp;<%# Eval("GroupDescription")%></td>
                                                                    <td class="kotak">&nbsp;<%# Eval("FileName")%></td>
                                                                    <td class="kotak tengah" style="padding-right: 1px">
                                                                        <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("ID") %>'
                                                                            CommandName="viewpdf" ToolTip="View Attachment" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <%-- END Release --%>
                                
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Panel Approval Mgr --%>
                            <asp:Panel ID="PanelFormApvMgrLog" runat="server" Visible="false" Width="100%">
                                <div id="lst2" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td >
                                                <asp:Label ID="Label2" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                    Font-Bold="True">&nbsp; ..:: LIST APPROVAL LAPORAN BULANAN ::..</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridViewApv1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDataBound="GridViewApv1_RowDataBound" Style="font-family: Calibri">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                            <ControlStyle Width="5%" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="" >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="check2" runat="server" Enabled="true" />
                                                                Pilih
                                                            </ItemTemplate>
                                                            <ControlStyle />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="GroupDescription" HeaderText="Laporan Bulanan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Periode" HeaderText="Periode">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Status" HeaderText="STATUS" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="TanggalBuat" HeaderText="Tanggal Masuk" Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" Width="5%" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="btnApv1" runat="server" type="button" value="Approval" onserverclick="btnApv1_ServerClick"
                                                    style="font-family: Calibri" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%">

                                    <tr>
                                        <td >
                                            <asp:Label ID="Label3" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                                border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th style="width: 5%" class="kotak">No</th>
                                                        <th style="width: 45%" class="kotak">Laporan Bulanan</th>
                                                        <th style="width: 45%" class="kotak">Nama File</th>
                                                        <th style="width: 5%" class="kotak">View PDF</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstBA2" runat="server" OnItemDataBound="lstBA_DataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="kotak tengah">&nbsp;<%# Eval("ID") %></td>
                                                                <td class="kotak tengah">&nbsp;<%# Eval("GroupDescription")%></td>
                                                                <td class="kotak">&nbsp;<%# Eval("FileName")%></td>
                                                                <td class="kotak tengah" style="padding-right: 1px">
                                                                    <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="viewpdf" ToolTip="View Attachment" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- END Approval Mgr --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- Panel Approval PM --%>
                            <asp:Panel ID="PanelFormApprovalPM" runat="server" Visible="false" Width="100%">
                                <div id="lst21" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td >
                                                <asp:Label ID="Label1" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                    Font-Bold="True">&nbsp; ..:: LIST APPROVAL LAPORAN BULANAN ::..</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridViewApv2" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridViewApv2_RowDataBound" Style="font-family: Calibri">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                            <ControlStyle Width="5%" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="" >
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="check4" runat="server" Enabled="true" />
                                                                Pilih
                                                            </ItemTemplate>
                                                            <ControlStyle />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="GroupDescription" HeaderText="Laporan Bulanan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Periode" HeaderText="Periode">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="TanggalBuat" HeaderText="Tanggal Masuk" Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                                ForeColor="White" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" Width="5%" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input id="btnApv2" runat="server" type="button" value="Approval" onserverclick="btnApv2_ServerClick"
                                                    style="font-family: Calibri" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelListJudul" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                                border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th style="width: 5%" class="kotak">No</th>
                                                        <th style="width: 45%" class="kotak">Laporan Bulanan</th>
                                                        <th style="width: 45%" class="kotak">Nama File</th>
                                                        <th style="width: 5%" class="kotak">View PDF</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstBA1" runat="server" OnItemDataBound="lstBA_DataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="kotak tengah">&nbsp;<%# Eval("ID") %></td>
                                                                <td class="kotak tengah">&nbsp;<%# Eval("GroupDescription")%></td>
                                                                <td class="kotak">&nbsp;<%# Eval("FileName")%></td>
                                                                <td class="kotak tengah" style="padding-right: 1px">
                                                                    <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="viewpdf" ToolTip="View Attachment" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- END Approval PM --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--Panel sent Email GRID--%>
                            <asp:Panel ID="PanelFormSentEmail" runat="server" Visible="false" Width="100%">
                                <table style="width: 100%; font-size: x-small" border="0" bgcolor="#99CCFF">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkAll3" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                OnCheckedChanged="ChkAll3_CheckedChanged" Style="font-family: Calibri; font-weight: 700; font-size: 9pt;"
                                                Text="Pilih semua" TextAlign="Left" />
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound"
                                                Width="100%" Style="font-family: Calibri">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                        <ControlStyle Width="5%" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="" >
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="check3" runat="server" Enabled="true" />
                                                            Pilih
                                                        </ItemTemplate>
                                                        <ControlStyle />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="GroupID" HeaderText="" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Font-Bold="True"
                                                ForeColor="White" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:BoundField>--%>
                                                    <asp:BoundField DataField="GroupDescription" HeaderText="GROUP INVENTORY">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FileName" HeaderText="NAMA FILE">
                                                        <HeaderStyle Width="30%" Font-Bold="True" ForeColor="White" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="keterangan" HeaderText="STATUS">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TglKirim" HeaderText="TANGGAL KIRIM">
                                                        <HeaderStyle Width="15%" Font-Bold="True" ForeColor="White" />
                                                        <ItemStyle Width="25%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" Width="5%" />
                                                <PagerStyle BorderStyle="Solid" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="btnSent" runat="server" type="button" value="Sent Email" onserverclick="btnSent_ServerClick"
                                                style="font-family: Calibri" />
                                        </td>
                                        <asp:Label ID="lblError" runat="server" Text="" Style="color: red;"></asp:Label>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--END Panel sent Email GRID--%>
                        </td>
                    </tr>

                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
