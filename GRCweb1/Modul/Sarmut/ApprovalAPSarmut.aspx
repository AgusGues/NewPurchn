<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalAPSarmut.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.ApprovalAPSarmut" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/calendar.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;<%-- <%=Judul %>--%>Approval Analisa dan Pemantauan </b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" OnClick="btnPrev_Click" Visible="false" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Visible="false" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Visible="false" />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                        <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                        <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_Click" Visible="false" />
                                        <asp:HiddenField ID="anNo" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 10%"></td>
                                        <td style="width: 15%"></td>
                                        <td style="width: 35%"></td>
                                        <td>
                                            <asp:Panel runat="server" ID="IsoOnly">
                                                &nbsp; Departemen :
                                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                    Width="204px">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <asp:Panel ID="PanelUtama" runat="server" Visible="true">
                                    <div class="contentlist" style="height: 200px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                            id="baList">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th rowspan="1" class="kotak tengah" nowrap="nowrap" style="width: 4%">#
                                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange" />
                                                    </th>
                                                    <th rowspan="1" style="width: 7%" class="kotak">No Analisa
                                                    </th>
                                                    <th style="width: 5%" class="kotak">Tanggal
                                                    </th>
                                                    <th rowspan="1" style="width: 10%" class="kotak">SarMutPerusahaan
                                                    </th>
                                                    <th rowspan="1" style="width: 10%" class="kotak">SarmutDepartemen
                                                    </th>
                                                    <th rowspan="1" style="width: 5%" class="kotak">Target
                                                    </th>
                                                    <th rowspan="1" style="width: 5%" class="kotak">Actual
                                                    </th>
                                                    <th rowspan="1" style="width: 5%" class="kotak">Pencapaian
                                                    </th>
                                                    <th rowspan="1" style="width: 5%" class="kotak">Status
                                                    </th>
                                                    <th rowspan="1" style="width: 5%" class="kotak">Detail
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstAppAP" runat="server" OnItemDataBound="lstAppAP_DataBound" OnItemCommand="lstAppAP_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td class="kotak tengah" nowrap="nowrap" style="width: 4%">
                                                                <span class="angka">
                                                                    <%-- <%# Container.ItemIndex+1 %>--%></span>
                                                                <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                    OnCheckedChanged="chk_CheckedChangePrs" /></span>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("AnNo")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("TglAnalisa", "{0:d}")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("SarMutPerusahaan")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("SarmutDepartemen")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("TargetVID")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("Actual")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("Pencapaian")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("Approval")%>
                                                            </td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/clipboard_16.png" CommandArgument='<%# Eval("AnNo") %>'
                                                                    CommandName="edit" ToolTip='<%# Eval("ID") %>' />
                                                                <%--<asp:ImageButton ID="edt1" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("AnNo") %>'
                                                                    CommandName="edit1" ToolTip='<%# Eval("ID") %>' />--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <div class="contentlist" style="height: 400px" id="Div2" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td>
                                                <div style="width: 100%; padding: 5px">
                                                    <table style="width: 100%; font-size: x-small">
                                                        <%--<tr>
                                                            <td style="width: 197px; height: 6px" valign="top">
                                                                <span style="font-size: 10pt">&nbsp; No. Analisa Data </span>
                                                            </td>
                                                            <td style="width: 204px; height: 6px" valign="top">
                                                                <asp:TextBox ID="txtNoAnalisa" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: x-small"></asp:TextBox>
                                                            </td>
                                                            <td style="height: 6px; width: 169px;" valign="top">
                                                                <span style="font-size: 10pt">&nbsp; Tgl. Analisa</span>
                                                            </td>
                                                            <td style="width: 209px; height: 6px" valign="top">
                                                                <asp:TextBox ID="txtDate" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: x-small"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtID" runat="server" BorderStyle="Groove" Width="25" ReadOnly="True"
                                                                    Visible="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 197px; height: 6px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 204px; height: 19px" valign="top">
                                                                <asp:TextBox ID="txtDeptF" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: x-small" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 197px; height: 6px" valign="top">
                                                                <span style="font-size: 10pt">&nbsp; Status Aproval </span>
                                                            </td>
                                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                                <asp:TextBox ID="txtApv" runat="server" BorderStyle="Groove" Width="100" ReadOnly="True"
                                                                    Style="font-family: Arial, Helvetica, sans-serif; font-size: x-small">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 245px;">
                                                                <asp:Label ID="resultMailSucc" runat="server" BackColor="White" class="result_done"
                                                                    Font-Size="X-Small" ForeColor="Lime" Visible="False"></asp:Label>
                                                                <asp:Label ID="resultMailFail" runat="server" class="result_fail" ForeColor="Red"
                                                                    Height="20px" Visible="False"></asp:Label>
                                                            </td>
                                                            <td style="width: 205px; height: 6px" valign="top">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 197px; height: 6px" valign="top">
                                                                <span style="font-size: 10pt">&nbsp; Sarmut Perusahaan </span>
                                                            </td>
                                                            <td style="height: 19px" colspan="3">
                                                                <asp:TextBox ID="txtSarmutPerusahaanx" runat="server" BorderStyle="Groove" TextMode="MultiLine"
                                                                    Height="100px" Font-Size="X-Small" Width="100%" Style="font-family: Arial, Helvetica, sans-serif;
                                                                    font-size: x-small"></asp:TextBox>
                                                            </td>
                                                            <td style="height: 50px" valign="bottom">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 197px; height: 6px">
                                                                <span style="font-size: 10pt">&nbsp; Sarmut Departemen </span>
                                                            </td>
                                                            <td style="height: 19px" colspan="3">
                                                                <asp:TextBox ID="txtSarmutDeptx" runat="server" BorderStyle="Groove" Font-Size="X-Small"
                                                                    Width="100%" Style="font-family: Arial, Helvetica, sans-serif; font-size: x-small"></asp:TextBox>
                                                            </td>
                                                            <td style="height: 50px" valign="middle">
                                                                <asp:CheckBox ID="chkList" runat="server" AutoPostBack="True" OnCheckedChanged="chkList_CheckedChanged"
                                                                    Text="List Analisa" />
                                                                <asp:CheckBox ID="chkDetail" runat="server" AutoPostBack="True" Text="Detail" OnCheckedChanged="chkDetail_CheckedChanged" />
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td colspan="5" style="height: 6px" valign="top">
                                                                <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" Width="100%"
                                                                    Visible="false">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Left"
                                                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                                        OnRowDataBound="GridView1_RowDataBound" PageSize="5" Style="margin-right: 40px; margin-bottom: 0px; font-family: Arial, Helvetica, sans-serif; font-size: small;"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                            <asp:BoundField DataField="AnNo" HeaderText="No.Analisa">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="TglAnalisa" DataFormatString="{0:d}" HeaderText="Tgl Analisa">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="SarMutPerusahaan" HeaderText="Sarmut Perusahaan">
                                                                                <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="SarmutDepartemen" HeaderText="Sarmut Departemen">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="TargetVID" HeaderText="Target">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Actual" HeaderText="Actual">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Pencapaian" HeaderText="Pencapaian">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Approval" HeaderText="Approval">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                            </asp:BoundField>
                                                                            <asp:ButtonField CommandName="Add" Text="Pilih">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                <ItemStyle ForeColor="#3399FF" />
                                                                            </asp:ButtonField>
                                                                        </Columns>
                                                                        <RowStyle BackColor="White" Font-Names="tahoma" Font-Size="X-Small" ForeColor="Black" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <HeaderStyle BackColor="#666666" BorderColor="Black" BorderStyle="Groove" BorderWidth="1px"
                                                                            Font-Bold="false" Font-Names="Arial" Font-Size="Smaller" ForeColor="White" Wrap="True" />
                                                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerStyle BackColor="#99CCCC" BorderStyle="Solid" ForeColor="#003399" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <caption>
                                                            <br />
                                                            <tr>
                                                                <td colspan="5" style="height: 6px" valign="top">
                                                                    <asp:Panel ID="PanelFormDetail" runat="server" BackColor="White" ScrollBars="Auto" Visible="true">
                                                                        <table id="Table2" class="tblForm" style="width: 100%; border-collapse: collapse;">
                                                                            <tr>
                                                                                <td style="width: 169px" valign="top"><span style="font-size: 10pt"></span></td>
                                                                                <td style="width: 204px;"></td>
                                                                                <td style="width: 169px"></td>
                                                                                <td style="width: 209px"></td>
                                                                                <td style="width: 205px">&nbsp; </td>
                                                                            </tr>
                                                                            <hr />
                                                                            <tr>
                                                                                <td colspan="5" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">PT.BANGUNPERKASA ADHITAMASENTRA </span>&nbsp; </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="5" valign="top">&nbsp;&nbsp;<span style="font-size: 12pt">ANALISA DATA PENCAPAIAN SASARAN MUTU / PEMANTAUAN DEPARTEMEN </span>&nbsp; </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" valign="top">
                                                                                    <table id="Table1" class="tblForm" style="width: 100%; border-collapse: collapse">
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top"><span style="font-size: 10pt"></span></td>
                                                                                            <td align="center" style="width: 5%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">Analisa No.</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="sarmutnotxt" runat="server" Enabled="false" Width="50%"></asp:TextBox>
                                                                                                <asp:Label ID="IDa" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">Tanggal</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">
                                                                                                <bdp:BDPLite ID="txtAsarmut_Date0" runat="server" CssClass="style2" Enabled="false" ToolTip="klik icon untuk merubah tanggal" Width="95%">
                                                                                                </bdp:BDPLite>
                                                                                            </td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%; height: 21px;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">1.&nbsp;Departemen / Bagian</span> </td>
                                                                                            <td align="center" style="width: 5%; height: 21px;">: </td>
                                                                                            <td style="width: 5%; height: 21px;">
                                                                                                <asp:Label ID="txtNamaDept" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%; height: 21px;"></td>
                                                                                            <td style="width: 25%; height: 21px;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">2.&nbsp;Sasaran Mutu Perusahaan</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 35%;">
                                                                                                <asp:Label ID="txtSasaranMutuPerusahaan" runat="server"></asp:Label>
                                                                                                &nbsp;<asp:Label ID="typeID" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">3.&nbsp;Sasaran Mutu Dept / Bagian</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="txtSarmutDepartemen" runat="server"></asp:Label>
                                                                                                <asp:Label ID="IDx" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:Label ID="xxx" runat="server" Visible="true"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">4.&nbsp;Pemantauan Dept / Bagian</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">- </td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">5.&nbsp;Target</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="txtTargetx" runat="server">&nbsp;&nbsp;</asp:Label>
                                                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                                (<asp:Label ID="txtParam" runat="server"></asp:Label>
                                                                                                )<%--<asp:TextBox ID="txtTargetx" runat="server" OnTextChanged="Target_Change" CssClass="txtOnGrid"
                                                                        Width="50%"  AutoPostBack="true" ReadOnly="true" ></asp:TextBox>--%></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">6.&nbsp;Monitoring Pencapaian</span> </td>
                                                                                            <td align="center" style="width: 5%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;6.1&nbsp;Periode</span> </td>
                                                                                            <td align="center" style="width: 5%;">: </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="txtBulan1" runat="server"></asp:Label>
                                                                                                <asp:Label ID="txtTahun1" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="txttahun" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:Label ID="txtbulan" runat="server" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%; height: 21px;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;6.2&nbsp;Hasil Pencapaian</span> </td>
                                                                                            <td align="center" style="width: 5%; height: 21px;">: </td>
                                                                                            <td style="width: 25%; height: 21px;">
                                                                                                <asp:Label ID="txtActual" runat="server"></asp:Label>
                                                                                                <asp:Label ID="txtSatuan" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%; height: 21px;"></td>
                                                                                            <td style="width: 25%; height: 21px;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;6.3&nbsp;Grafik Pencapaian</span> </td>
                                                                                            <td align="center" style="width: 5%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" valign="top"><span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;
                                                                                                 <table id="Table3" class="tblForm" style="width: 100%; border-collapse: collapse">
                                                                                                     <tr>
                                                                                                         <td rowspan="2" style="width: 70%;" valign="top">
                                                                                                             <cc2:Chart ID="Chart1" runat="server" BorderlineColor="Blue" Height="320px" Width="380px">
                                                                                                                 <Titles>
                                                                                                                     <cc2:Title Font="Arial,12pt,style=Bold" Name="title" ShadowOffset="3">
                                                                                                                     </cc2:Title>
                                                                                                                 </Titles>
                                                                                                                 <ChartAreas>
                                                                                                                     <cc2:ChartArea Name="Area1">
                                                                                                                     </cc2:ChartArea>
                                                                                                                 </ChartAreas>
                                                                                                             </cc2:Chart>
                                                                                                             <%--<img alt="" src="../../images/exampleChart.JPG" />--%></td>
                                                                                                         <td style="width: 2%;" valign="top">
                                                                                                             <asp:CheckBox ID="chkTercapai" runat="server" Text="Tercapai" />
                                                                                                             <br />
                                                                                                             <asp:CheckBox ID="chkTTercapai" runat="server" Text="Tidak Tercapai" />
                                                                                                         </td>
                                                                                                     </tr>
                                                                                                     <tr>
                                                                                                         <td style="width: 20%;" valign="top"></td>
                                                                                                     </tr>
                                                                                                 </table>
                                                                                            </span></td>
                                                                                            <td colspan="2" valign="top">
                                                                                                <div id="tbldata" runat="server" visible="false">
                                                                                                    <table border="0" style="width: 30%; border-collapse: collapse; font-size: x-small;">
                                                                                                        <thead>
                                                                                                            <tr>
                                                                                                                <th class="kotak">ID </th>
                                                                                                                <th class="kotak">Tahun </th>
                                                                                                                <th class="kotak">Bulan </th>
                                                                                                                <th class="kotak">Sarmut Dept </th>
                                                                                                                <th class="kotak">Actual </th>
                                                                                                                <th class="kotak">Target </th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <asp:Repeater ID="lstdt" runat="server" OnItemDataBound="lstdt_DataBound">
                                                                                                                <ItemTemplate>
                                                                                                                    <tr class="EvenRows baris kotak">
                                                                                                                        <td class="kotak tengah"><%# Eval("ID") %></td>
                                                                                                                        <td class="kotak tengah"><%# Eval("Tahun") %></td>
                                                                                                                        <td class="kotak tengah"><%# Eval("Bulan") %></td>
                                                                                                                        <td class="kotak tengah"><%# Eval("SarmutDepartemen")%></td>
                                                                                                                        <td class="kotak tengah"><%# Eval("Actual")%></td>
                                                                                                                        <td class="kotak tengah"><%# Eval("Target")%></td>
                                                                                                                    </tr>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top">&nbsp;&nbsp;<span style="font-size: 10pt">7.&nbsp;Analisa Penyebab</span> </td>
                                                                                            <td align="center" style="width: 5%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 23%;" valign="top"></td>
                                                                                            <td align="center" style="width: 5%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;"></td>
                                                                                            <td style="width: 25%;">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" valign="top">
                                                                                                <table style="width: 100%;">
                                                                                                    <tr>
                                                                                                        <td colspan="7"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="7"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:CheckBox ID="chkManusia" runat="server" AutoPostBack="True" Text="Manusia" />
                                                                                                        </span></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:CheckBox ID="chkMesin" runat="server" AutoPostBack="True" Text="Mesin" />
                                                                                                        </span></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="center" style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:Image ID="Image4" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg" Width="41px" />
                                                                                                        </span></td>
                                                                                                        <td align="center" style="height: 25px" width="10%"></td>
                                                                                                        <td align="center" style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg" Width="41px" />
                                                                                                        </span></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" valign="middle" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:CheckBox ID="chkLingkungan" runat="server" AutoPostBack="True" Text="Lingkungan" />
                                                                                                        </span></td>
                                                                                                        <td colspan="4" style="height: 25px" valign="middle">
                                                                                                            <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/images/Panah Lurus.jpg" Width="93%" />
                                                                                                        </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%">
                                                                                                            <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg" Width="41px" />
                                                                                                        </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%">
                                                                                                            <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg" Width="41px" />
                                                                                                        </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:CheckBox ID="chkMaterial" runat="server" AutoPostBack="True" Text="Material" />
                                                                                                        </span></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%"><span style="font-size: 10pt">
                                                                                                            <asp:CheckBox ID="chkMetode" runat="server" AutoPostBack="True" Text="Metode" />
                                                                                                        </span></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%"></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td align="right" style="height: 25px" width="10%"></td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                        <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp;&nbsp; <span style="font-size: 10pt">Mesin </span></td>
                                                                                                        <td align="right" colspan="6" style="height: 25px">
                                                                                                            <asp:TextBox ID="txtMesin" runat="server" Enabled="False" Height="44px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp;&nbsp; <span style="font-size: 10pt">Manusia</span> </td>
                                                                                                        <td align="right" colspan="6" style="height: 25px">
                                                                                                            <asp:TextBox ID="txtManusia" runat="server" Enabled="False" Height="44px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp;&nbsp; <span style="font-size: 10pt">Material</span> </td>
                                                                                                        <td align="right" colspan="6" style="height: 25px">
                                                                                                            <asp:TextBox ID="txtMaterial" runat="server" Enabled="False" Height="44px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp;<span style="font-size: 10pt">Metode</span> </td>
                                                                                                        <td align="right" colspan="6" style="height: 25px">
                                                                                                            <asp:TextBox ID="txtMetode" runat="server" Enabled="False" Height="44px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    
                                                                                                    <tr>
                                                                                                        <td style="height: 25px" width="10%">&nbsp;&nbsp; <span style="font-size: 10pt">Lingkungan</span> </td>
                                                                                                        <td align="right" colspan="6" style="height: 25px">
                                                                                                            <asp:TextBox ID="txtLingkungan" runat="server" Enabled="False" Height="44px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" valign="top"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 25px; width: 23%;"></td>
                                                                                            <td style="height: 25px; width: 5%;">&nbsp; </td>
                                                                                            <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                            <td style="height: 25px" width="10%">&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" style="height: 25px" width="10%">&nbsp;&nbsp; <span style="font-size: 10pt">8.&nbsp;Tindakan Perbaikan &amp; Pencegahan</span><span style="font-size: 10pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;[ Tindakan Perbaikan ] </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                 <div id="tablePerbaikan" runat="server">
                                                                                                     &nbsp;&nbsp;<table border="0" style="width: 100%; border-collapse: collapse; font-size: x-small;">
                                                                                                         <thead>
                                                                                                             <tr>
                                                                                                                 <th class="kotak">ID </th>
                                                                                                                 <th class="kotak">Tindakan </th>
                                                                                                                 <th class="kotak">Pelaku </th>
                                                                                                                 <th class="kotak">Jadwal Selesai </th>
                                                                                                                 <th class="kotak">Aktual Selesai </th>
                                                                                                                 <th class="kotak">Verifikasi </th>
                                                                                                                 <th class="kotak">Tgl Verifikasi </th>
                                                                                                             </tr>
                                                                                                         </thead>
                                                                                                         <tbody>
                                                                                                             <asp:Repeater ID="Repeater1" runat="server">
                                                                                                                 <ItemTemplate>
                                                                                                                     <tr class="EvenRows baris kotak">
                                                                                                                         <td class="kotak tengah"><%--<%# Eval("IDPer")%>--%><%# Container.ItemIndex+1 %></td>
                                                                                                                         <td class="kotak"><%# Eval("Tindakan") %></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Pelaku") %></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Jadwal_Selesai","{0:d}")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Aktual_Selesai","{0:d}")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("VerifikasiKet")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("tglVerifikasi", "{0:d}")%></td>
                                                                                                                     </tr>
                                                                                                                 </ItemTemplate>
                                                                                                             </asp:Repeater>
                                                                                                         </tbody>
                                                                                                     </table>
                                                                                                 </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;[ Tindakan Pecegahan ] </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 25px">&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                 <div id="Div3" runat="server">
                                                                                                     &nbsp;&nbsp;<table border="0" style="width: 100%; border-collapse: collapse; font-size: x-small;">
                                                                                                         <thead>
                                                                                                             <tr>
                                                                                                                 <th class="kotak">ID </th>
                                                                                                                 <th class="kotak">Tindakan </th>
                                                                                                                 <th class="kotak">Pelaku </th>
                                                                                                                 <th class="kotak">Jadwal Selesai </th>
                                                                                                                 <th class="kotak">Aktual Selesai </th>
                                                                                                                 <th class="kotak">Verifikasi </th>
                                                                                                                 <th class="kotak">Tgl Verifikasi </th>
                                                                                                             </tr>
                                                                                                         </thead>
                                                                                                         <tbody>
                                                                                                             <asp:Repeater ID="Repeater2" runat="server">
                                                                                                                 <ItemTemplate>
                                                                                                                     <tr class="EvenRows baris kotak">
                                                                                                                         <td class="kotak tengah"><%--<%# Eval("IDPer")%>--%><%# Container.ItemIndex+1 %></td>
                                                                                                                         <td class="kotak"><%# Eval("Tindakan") %></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Pelaku") %></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Jadwal_Selesai","{0:d}")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("Aktual_Selesai","{0:d}")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("VerifikasiKet")%></td>
                                                                                                                         <td class="kotak tengah"><%# Eval("tglVerifikasi", "{0:d}")%></td>
                                                                                                                     </tr>
                                                                                                                 </ItemTemplate>
                                                                                                             </asp:Repeater>
                                                                                                         </tbody>
                                                                                                     </table>
                                                                                                 </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </caption>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
