<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ElapBulT1T3.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.ElapBulT1T3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        function PreviewPDF(ID) {
            params = 'dialogWidth:1000px';
            params += '; dialogHeight:600px'
            params += '; top=0, left=0'
            params += '; resizable:yes'
            params += ';scrollbars:yes';
            window.showModalDialog("../../ModalDialog/PDFPreviewLapBulT13.aspx?grp=" + ID, "Preview", params);
            return false;
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <%--<table class="nbTableHeader">--%>
                <table class="tblForm" style="width: 100%">
                    <tr>
                        <td>
                            <table class="nbTableHeader" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                <tr>
                                    <td style="font-family: Calibri; font-size: medium; font-style: normal; font-weight: bold;" colspan="4">&nbsp; 
                                        <span style="font-family: 'Courier New', Courier, monospace; color: #000099"><b>E-LAPORAN BULANAN TAHAP I dan III</b></span>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnReleaseUlang" runat="server" OnClick="btnReleaseUlang_ServerClick"
                                            Text="Release Ulang" Enabled="true" Style="font-family: Calibri; font-weight: 700" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEmail" runat="server" OnClick="btnEmail_ServerClick" Text="Sent Email"
                                            Style="font-family: Calibri; font-weight: 700" />
                                    </td>
                                </tr>
                            </table>
                            <%-- Panel Periode Normal --%>
                            <asp:Panel ID="PanelPeriode" runat="server" Visible="true">
                                <hr />
                                <table>
                                    <tr>
                                        <td style="font-family: Calibri; font-size: x-small;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span><span style="font-size: small"><b>PERIODE
                                                :</b></span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPeriode" runat="server" AutoPostBack="True" Height="30px" Width="100%"
                                                Style="background-color: #FFFFFF; font-family: Calibri; font-weight: 700; font-size: small; text-align: center;"
                                                ReadOnly="True" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTahun" runat="server" AutoPostBack="True" Height="30px" Width="100%"
                                                Style="background-color: #FFFFFF; font-family: Calibri; font-weight: 700; font-size: small; text-align: center;"
                                                ReadOnly="True" Enabled="False" BorderStyle="NotSet"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <hr size="1" width="400%" />
                            </asp:Panel>
                            <%-- End Panel Periode Normal --%>
                            <%-- Panel Periode Back Month --%>
                            <asp:Panel ID="PanelPeriodeBack" runat="server" Visible="false">
                                <hr />
                                <table>
                                    <tr>
                                        <td style="font-family: Calibri; width: 10%; font-size: x-small;">&nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span><span style="font-size: small"><b>PERIODE
                                                :</b></span>
                                        </td>
                                        <td rowspan="1">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px" Style="font-family: Calibri">
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
                                        <td rowspan="1">
                                            <asp:DropDownList ID="ddlTahun" runat="server" Height="19px" Width="100px" Style="font-family: Calibri">
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
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <hr size="1" width="100%" />
                            </asp:Panel>
                            <%-- End Panel Periode Back Month --%>
                            <%-- Panel Release --%>
                            <asp:Panel ID="PanelFormRelease" runat="server" Visible="false">
                                <div id="lst" runat="server">
                                    <tr>
                                        <td >
                                            <asp:Label ID="Label5" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                Font-Bold="True">&nbsp; ..:: LIST ::..0</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Style="font-family: Calibri">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID"  ItemStyle-VerticalAlign="Middle">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="check" runat="server" OnCheckedChanged="chk_change" Checked="false"
                                                                Enabled="true" Text="" />Pilih
                                                        </ItemTemplate>
                                                        <ControlStyle />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GroupID" HeaderText="" Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GroupName" HeaderText="Accounting Report">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Periode" HeaderText="Periode">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Font-Bold="True"
                                                            ForeColor="White" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField HeaderText="" CommandName="Add" Text="Pilih" 
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ControlStyle />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField HeaderText="" CommandName="Add2" Text="Hapus" 
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ControlStyle  />
                                                        <HeaderStyle  HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:ButtonField>
                                                </Columns>
                                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
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
                                </div>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <%-- List Laporan Bulanan format PDF --%>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                            Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..1</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                            border="0">
                                            <headertemplate>                                        
                                        <tr class="tbHeader">
                                            <th style="width: 5%" class="kotak">No</th>
                                            <th style="width: 45%" class="kotak">Accounting Report</th>
                                            <th style="width: 45%" class="kotak">Nama File</th>                                            
                                            <th style="width: 5%" class="kotak">View PDF</th>
                                        </tr>
                                    </headertemplate>
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
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <%-- END Release --%>
                            <%-- Panel Approval Mgr --%>
                            <asp:Panel ID="PanelFormApvMgrLog" runat="server" Visible="false">
                                <div id="lst2" runat="server">
                                    <tr>
                                        <td style="width: 100%" colspan="0" rowspan="0">
                                            <asp:Label ID="Label2" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                                Font-Bold="True">&nbsp; ..:: LIST APPROVAL LAPORAN BULANAN ::..2</asp:Label>
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
                                                    <asp:TemplateField HeaderText="" ControlStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="check2" runat="server" Enabled="true" Text="Pilih" ControlStyle-Width="5%" />
                                                        </ItemTemplate>
                                                        <ControlStyle Width="5%" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GroupDescription" HeaderText="Accounting Report">
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
                                </div>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="0" rowspan="0">
                                        <asp:Label ID="Label3" runat="server" Visible="True" Style="font-family: Calibri; font-size: x-small;"
                                            Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..3</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                            border="0">
                                            <headertemplate>                                        
                                        <tr class="tbHeader">
                                            <th style="width: 5%" class="kotak">No</th>
                                            <th style="width: 45%" class="kotak">Accounting Report</th>
                                            <th style="width: 45%" class="kotak">Nama File</th>                                            
                                            <th style="width: 5%" class="kotak">View PDF</th>
                                        </tr>
                                    </headertemplate>
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
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <%-- END Approval Mgr --%>
                            <%-- Panel Approval PM --%>
                            <asp:Panel ID="PanelFormApprovalPM" runat="server" Visible="false">
                                <div id="lst21" runat="server">
                                    <tr bgcolor="Silver">
                                        <td style="width: 100%">
                                            <hr>
                                            <asp:Label ID="Label1" runat="server" Visible="True" Style="font-family: 'Courier New', Courier, monospace; font-size: medium; text-decoration: underline;"
                                                Font-Bold="True">LIST APPROVAL</asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;<asp:CheckBox ID="ChkAllApv2" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                            OnCheckedChanged="ChkAllApv2_CheckedChanged" Style="font-family: Calibri; font-weight: 700; font-size: 9pt;"
                                            Text="Pilih semua" TextAlign="Left"  />

                                            <asp:GridView ID="GridViewApv2" runat="server" AutoGenerateColumns="False" Width="100%"
                                                OnRowDataBound="GridViewApv2_RowDataBound"
                                                Style="font-family: Calibri; font-size: x-small;" Font-Size="X-Small">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                        <ControlStyle Width="5%" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" Font-Size="XX-Small" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="" ControlStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="check4" runat="server" Enabled="true" Text="Pilih" ControlStyle-Width="5%" />
                                                        </ItemTemplate>
                                                        <ControlStyle Width="5%" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"
                                                            BackColor="#336699" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GroupDescription" HeaderText="Accounting Report">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" Font-Size="XX-Small" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Periode" HeaderText="Periode">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TanggalBuat" HeaderText="Tanggal Masuk" Visible="true">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%" Font-Bold="True"
                                                            ForeColor="White" BackColor="#336699" />
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
                                            <input id="btnApv2" runat="server" type="button" value="Approval" onserverclick="btnApv2_ServerClick"
                                                style="font-family: Calibri" />
                                        </td>
                                    </tr>
                                </div>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="width: 100%" colspan="0" rowspan="0">
                                        <asp:Label ID="LabelListJudul" runat="server" Visible="True" Style="font-family: Calibri;
                                            font-size: x-small;" Font-Bold="True">&nbsp; ..:: LIST LAPORAN BULANAN ::..</asp:Label>
                                    </td>
                                </tr>--%>
                                <tr bgcolor="Silver">
                                    <td style="width: 100%">
                                        <hr>
                                        <span style="font-family: 'Agency FB'">
                                            <asp:Label ID="LabelListJudul" runat="server" Font-Bold="True" Style="font-family: 'Courier New', Courier, monospace; font-size: medium; text-decoration: underline;"
                                                Visible="True">LIST LAPORAN BULANAN</asp:Label>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                            border="0">
                                            <headertemplate>                                        
                                        <tr class="tbHeader">
                                            <th style="width: 5%; color: #FFFFFF; background-color: #336699;" class="kotak">No</th>
                                            <th style="width: 45%; color: #FFFFFF; background-color: #336699;" 
                                                class="kotak">Accounting Report</th>
                                            <th style="width: 45%; color: #FFFFFF; background-color: #336699;" 
                                                class="kotak">Nama File</th>                                            
                                            <th style="width: 5%; color: #FFFFFF; background-color: #336699;" class="kotak">View PDF</th>
                                        </tr>
                                    </headertemplate>
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
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="PanelFormSentEmail" runat="server" Visible="false">
                                <table style="width: 100%; font-size: x-small" border="0">
                                    <asp:CheckBox ID="ChkAll3" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        OnCheckedChanged="ChkAll3_CheckedChanged" Style="font-family: Calibri; font-weight: 700; font-size: 9pt; text-align: left;"
                                        Text="Pilih semua" TextAlign="Left" />
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound"
                                        Style="font-family: Calibri">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" ItemStyle-VerticalAlign="Middle">
                                                <ControlStyle Width="5%" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" Font-Bold="True"
                                                    ForeColor="White" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="" ControlStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="check3" runat="server" Enabled="true" Text="Pilih" ControlStyle-Width="5%" />
                                                </ItemTemplate>
                                                <ControlStyle Width="5%" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                            </asp:TemplateField>

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
                                    <tr>
                                        <td>
                                            <input id="btnSent" runat="server" type="button" value="Sent Email" onserverclick="btnSent_ServerClick"
                                                style="font-family: Calibri" />
                                        </td>
                                        <asp:Label ID="lblError" runat="server" Text="" Style="color: red;"></asp:Label>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
