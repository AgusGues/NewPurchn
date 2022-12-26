<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POPurchnSendMail.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.POPurchnSendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" runat="server">
                <table style="table-layout: fixed; width: 100%;">
                    <tr>
                        <td style="height: 49px; width: 100%">
                            <!--header tabel-->
                            <table class="nbTableHeader" width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <strong>&nbsp;KIRIM EMAIL PO KE SUPPLIER</strong></td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="BtnFormMail" runat="server" Text="Kirim Email" OnClick="btnMainForm_ServerClick" Visible="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnReports" runat="server" Text="List Report" OnClick="btnReport_ServerClick" />&nbsp;&nbsp;
                        <asp:Button ID="btnNew" runat="server" Text="Form PO" OnClick="btnFormPO_ServerClick" />
                                    </td>
                                </tr>
                            </table>
                            <!-- end of header-->
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <div id="div3" class="content">
                                <asp:Panel ID="Panel1" runat="server" Height="270px">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; No.PO</td>
                                            <td style="width: 50%" class="">
                                                <asp:TextBox ID="txtNoPO" runat="server" Width="40%" AutoPostBack="true" OnTextChanged="txtNoPO_Change"></asp:TextBox></td>
                                            <td rowspan="8" class="">&nbsp;</td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp; Supplier</td>
                                            <td style="width: 41%">
                                                <asp:TextBox ID="txtCariSupplier" runat="server" Width="100%"></asp:TextBox></td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp; Email</td>
                                            <td style="width: 41%">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox></td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp;</td>
                                            <td style="width: 41%">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Enabled="true" />
                                                <!--<asp:Label ID="lblMessage" ForeColor="Green" runat="server" />-->
                                            </td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%' valign="top">&nbsp; Message</td>
                                            <td style="width: 41%">
                                                <asp:TextBox ID="txtMessage" runat="server" Width="100%"
                                                    TextMode="MultiLine" Height="87px" Enabled="true">Terlampir Purchase Order mohon agar segera proses.</asp:TextBox></td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp;</td>
                                            <td style="width: 41%">
                                                <asp:Label ID="lbmsg" runat="server"></asp:Label></td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp;</td>
                                            <td style="width: 41%">&nbsp;</td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width='10%'>&nbsp;</td>
                                            <td style="width: 41%">
                                                <asp:Button ID="btnSend" runat="server" Text="Kirim Email" OnClick="btnKirim_ServerClick" />&nbsp;&nbsp;&nbsp;
                            <!--<input id="btnReport" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="R e p o r t" onserverclick="btnReport_ServerClick" />-->
                                            </td>
                                            <td style="width: 5%">&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="peroideArea" runat="server" ScrollBars="Vertical" Visible="true">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                        <tr>
                                            <td style="width: 10%">&nbsp; Periode</td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="2">
                                                <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
                                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <div id="div5" class="contentlist">
                                    <div id="rmail" runat="server" style="height: 180px">
                                        <%--<asp:Panel ID="TblPanel" runat="server" Height="260px" ScrollBars="Vertical" Visible="false">--%>
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width: 2%">No.</th>
                                                <th class="kotak" style="width: 7%">Tanggal Kirim</th>
                                                <th class="kotak" style="width: 5%">No.PO</th>
                                                <th class="kotak" style="width: 15%">Supplier Name</th>
                                                <th class="kotak" style="width: 12%">Email</th>
                                                <th class="kotak" style="width: 12%">Report</th>
                                            </tr>
                                            <tbody>
                                                <asp:Repeater ID="ReportMailPo" runat="server" OnItemCommand="ReportMailPo_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah"><%# Eval("TglKirim","{0:d}") %></td>
                                                            <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                            <td class="kotak tengah"><%# Eval("SupplierName") %></td>
                                                            <td class="kotak tengah"><%# Eval("Email") %></td>
                                                            <td class="kotak tengah"><%# Eval("Report") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <%-- </asp:Panel>--%>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSend" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
