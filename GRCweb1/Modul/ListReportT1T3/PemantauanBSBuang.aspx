<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanBSBuang.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.PemantauanBSBuang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function updateDO(id) {
            window.showModalDialog("../../ModalDialog/RMMEdit.aspx?p=" + id, "RMM Update", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
        }
    </script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px; font-family: 'Courier New', Courier, monospace;">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 40%; font-size: medium; font-family: 'Times New Roman', Times, serif;">
                                            <%-- <span style="font-family: 'Courier New', Courier, monospace">
                                            <strong>&nbsp;PEMANTAUAN 
                                            BS BUANG</strong> </span>--%>
                                            <asp:Label ID="labelJudul" runat="server" Visible="true"></asp:Label>
                                        </td>
                                        <td style="width: 60%; text-align: right;">

                                            <asp:RadioButton ID="RBLiat" runat="server" AutoPostBack="True" OnCheckedChanged="RBLiat_CheckedChanged"
                                                Style="font-family: 'Calibri Light'; font-size: x-small; text-align: left; color: #FFFFFF; font-weight: 700;"
                                                Text="&nbsp; Show Laporan" TextAlign="Left" Width="150px" />


                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                                                Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                Text="Release" Visible="False" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                Text="Cancel" Visible="False" />
                                            <asp:Button ID="btnApv" runat="server" OnClick="btnApv_Click"
                                                Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                Text="Approval" Visible="False" />
                                            <asp:Button ID="btnPrev" runat="server" OnClick="btnPrev_Click"
                                                Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                Text="<< Prev" Visible="False" />
                                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click"
                                                Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                Text="Next >>" Visible="False" />
                                            </span>    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content"
                                style="font-family: 'Courier New', Courier, monospace">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <table width="85%" style="border-collapse: collapse; margin-top: 10px">
                                        <tr>
                                            <td style="width: 16%;">
                                                <strong>&nbsp;<span style="font-family: 'Calibri Light'; font-size: x-small">Periode
                                                    Tanggal :</span><span style="font-size: x-small"> </span></strong></span>
                                            </td>
                                            <td colspan="3" style="text-align: left; font-family: 'Calibri Light'; font-size: small;"
                                                width="100%">
                                                <span style="font-family: 'Brush Script MT'; font-size: medium">Dari Tanggal</span><span
                                                    style="font-family: 'Courier New', Courier, monospace"><asp:TextBox ID="txtdrtanggal"
                                                        runat="server" AutoPostBack="True" Height="22px" meta:resourcekey="txtdrtanggalResource1"
                                                        Style="text-align: center; margin-left: 16px; font-family: Calibri;" Width="116px"></asp:TextBox></span>
                                                <cc1:calendarextender
                                                    id="CalendarExtender1" runat="server" format="dd-MMM-yyyy" targetcontrolid="txtdrtanggal"
                                                    enabled="True">
                                                        </cc1:calendarextender>
                                                <span style="font-family: 'Brush Script MT'; font-size: medium">s/d Tanggal</span>&nbsp;&nbsp;&nbsp;
                                                <span style="font-family: 'Courier New', Courier, monospace">
                                                    <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" meta:resourcekey="txtsdtanggalResource1"
                                                        Width="116px" Style="font-family: Calibri; margin-left: 0px"></asp:TextBox>
                                                </span>
                                                <cc1:calendarextender id="txtsdtanggal_CalendarExtender" runat="server" format="dd-MMM-yyyy"
                                                    targetcontrolid="txtsdtanggal" enabled="True">
                                                </cc1:calendarextender>
                                            </td>
                                        </tr>
                                    </table>
                                    <%--</asp:Panel>--%>
                                    <table width="85%" style="border-collapse: collapse; margin-top: 10px">
                                        <tr>
                                            <td style="width: 16%;"></td>
                                            <td style="width: 50%" colspan="5">
                                                <span style="font-family: 'Courier New', Courier, monospace">
                                                    <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                        Text="Preview" />
                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Style="font-family: 'Calibri Light'; font-weight: 700; font-size: x-small;"
                                                        Text="Export To Excel" />
                                                </span><strong style="color: #0000FF; font-size: x-small; font-weight: normal; font-style: italic; font-variant: normal; text-transform: uppercase;">
                                                    <asp:Label ID="labelsave" runat="server" Visible="false" /></strong>
                                                <asp:RadioButton ID="RBLogistik" runat="server" AutoPostBack="True" OnCheckedChanged="RBLogistik_CheckedChanged" Style="font-family: 'Calibri Light'; font-size: x-small; text-align: left; color: #000000; font-weight: 700;"
                                                    Text="&nbsp; Logistik"
                                                    TextAlign="Left" Width="80px" />
                                                <asp:RadioButton ID="RBFinishing" runat="server" AutoPostBack="True" OnCheckedChanged="RBFinishing_CheckedChanged" Style="font-family: 'Calibri Light'; font-size: x-small; text-align: left; color: #000000; font-weight: 700;"
                                                    Text="&nbsp; Finishing"
                                                    TextAlign="Left" Width="90px" />
                                            </td>
                                            <%-- <td align="right"></td>--%>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <div class="contentlist" style="overflow: scroll; height: 400px; width: 100%;" id="div2" runat="server">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: 'Calibri Light'; height: auto;" border="0">
                                        <thead>
                                            <tr class="tbHeader" id="hd1">
                                                <th class="kotak" rowspan="3" style="width: 3%">No.
                                                </th>
                                                <th class="kotak" rowspan="3" style="width: 5%">Tanggal
                                                </th>
                                                <th class="kotak" rowspan="3" style="width: 10%">Partno
                                                </th>
                                                <th class="kotak" colspan="2" style="width: 15%">Jumlah
                                                </th>
                                                <th class="kotak" rowspan="3" style="width: 5%">PIC
                                                </th>
                                                <th class="kotak" colspan="3" style="width: 52%">Paraf
                                                </th>
                                                <th class="kotak" rowspan="3" style="width: 10%">Keterangan
                                                </th>
                                            </tr>
                                            <tr class="tbHeader" id="hd2">
                                                <th class="kotak tengah" rowspan="2" style="width: 5%">Lembar
                                                </th>
                                                <th class="kotak tengah" rowspan="2" style="width: 5%">M3
                                                </th>
                                                <th class="kotak tengah" colspan="2" style="width: 5%">Manager
                                                </th>
                                                <th class="kotak tengah" rowspan="2" style="width: 5%">ACC
                                                </th>
                                            </tr>
                                            <tr class="tbHeader" id="hd3">
                                                <th class="kotak tengah" style="width: 5%">
                                                    <asp:Label ID="labeldept" runat="server" Visible="true"></asp:Label>
                                                </th>
                                                <th class="kotak tengah" style="width: 5%">QA
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstTglP" runat="server" OnItemDataBound="lstTglP_DataBound">
                                                <ItemTemplate>
                                                    <%--<tr class="Line3 baris" id="tglpotong" runat="server">--%>
                                                    <tr class="Line3 baris" id="tglpotong" runat="server">
                                                        <td class="kotak tengah"><%# Eval("No")%>
                                                        </td>
                                                        <td class="kotak" colspan="9">&nbsp;<%# Eval("TglPotong")%></td>
                                                    </tr>
                                                    <asp:Repeater ID="ListBS" runat="server" OnItemDataBound="ListBS_DataBound" OnItemCommand="ListBS_Command">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris" id="ps1" runat="server">
                                                                <td class="kotak angka" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                   
                                                                   
                                                                        <%# Eval("Partno") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Qty") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("M3") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("PIC") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("ApvDept")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("ApvQA") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("ApvAcc") %>
                                                                </td>
                                                                <%-- <td class="kotak tengah">
                                                                        <%# Eval("Ket")  %>
                                                                    </td>--%>
                                                                <td class="kotak" style="background-color: #FF99FF;">
                                                                    <asp:Label ID="txtKeterangan" runat="server" Visible="false" Width="100%">&nbsp;&nbsp;</asp:Label>
                                                                    <asp:TextBox ID="Keterangan" runat="server" CssClass="txtOnGrid"
                                                                        Width="100%" Font-Names="calibri"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr class="total baris" id="ftr" runat="server">
                                                                <td class="kotak tengah" colspan="3">
                                                                    <strong>Sub Total</strong>
                                                                </td>
                                                                <td class="kotak bold angka">&nbsp;
                                                                </td>
                                                                <td class="kotak bold angka"></td>
                                                                <td class="kotak tengah" colspan="5">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                            <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">&nbsp;
                                                        <strong>Grand Total:</strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                        <asp:Label ID="lblGrandTotal" runat="server" /></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                        <asp:Label ID="lblGrandTotalM3" runat="server" /></strong>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <%--</div>--%>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
