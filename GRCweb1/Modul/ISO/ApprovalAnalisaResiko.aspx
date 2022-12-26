<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalAnalisaResiko.aspx.cs" Inherits="GRCweb1.Modul.ISO.ApprovalAnalisaResiko" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#div2").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 30%; padding-left: 5px">
                                            <strong>&nbsp; Approval Analisa Resiko </strong>
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                        <td style="width: 70%; padding-right: 5px" align="right">
                                            <%--<asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />--%>
                                            <%--<asp:Button ID="btnForm" runat="server" Text="Form" OnClick="btnForm_serverClick" />--%>
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                            <asp:HiddenField ID="ArID" runat="server" />
                                            <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                            <%--   <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClick="btnPrint_ServerClick"
                                                Visible="false" />--%>
                                            <%--<asp:Button ID="btnList" runat="server" Text="List" />--%>
                                            <%--<asp:Button ID="rekapList" runat="server" Text="Rekap" OnClick="btnRekap_ServerClick" />--%>
                                            <%--<asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="NoSJ">No Surat Jalan</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" Text="Cari" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                                <div class="content">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp; Periode :
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp;<asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="True" Width="25%"
                                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Pilih --</asp:ListItem>
                                                    <asp:ListItem Value="1">Semester I</asp:ListItem>
                                                    <asp:ListItem Value="2">Semester II</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged"
                                                    Width="20%">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp<asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="45%"
                                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" Enabled="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%;">
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <div class="contentlist" style="height: 700px; overflow: auto" id="lst" runat="server"
                                        onscroll="setScrollPosition(this.scrollTop);">
                                        <table id="Table1" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                            width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak " rowspan="2">
                                                        No.
                                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange"
                                                            />
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Departemen
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Klasifikasi Resiko
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Activities/ Kegiatan
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Risk
                                                    </th>
                                                    <th class="kotak " colspan="2">
                                                        Issue (Internal / External )
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Opportunity / Peluang
                                                    </th>
                                                    <th rowspan="2">
                                                        Level Kemungkinan
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Level Dampak
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Level Resiko
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Treatment / Mitigation
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Due Date
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Approval
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        Status
                                                    </th>
                                                   <%-- <th class="kotak " rowspan="2">
                                                        Lampiran
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        #
                                                    </th>
                                                    <th class="kotak " rowspan="2">
                                                        UploadFile
                                                    </th>--%>
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak ">
                                                        Internal
                                                    </th>
                                                    <th class="kotak ">
                                                        External
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tb" runat="server">
                                                <asp:Repeater ID="lstPMX" runat="server" OnItemCommand="lstPMX_Command" OnItemDataBound="lstPMX_Databound">
                                                    <ItemTemplate>
                                                        <tr id="lst" runat="server" class="EvenRows baris">
                                                            <td class="kotak tengah" style="width: 2%">
                                                                <%# Container.ItemIndex+1 %></span>
                                                                <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                    OnCheckedChanged="chk_CheckedChangePrs" />
                                                            </td>
                                                             <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("Dept")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("AnalisaResiko")%> &nbsp;- <%# Eval("Ket")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("Aktivitas")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("Risk")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("IssueInternal1")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("IssueEkternal1")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("Peluang1")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 1%">
                                                                <%# Eval("LvlKemungkinan")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 1%">
                                                                <%# Eval("LvlDampak")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 3%">
                                                                <%# Eval("LvlResiko")%>
                                                                &nbsp; (
                                                                <%# Eval("LvlResiko1")%>
                                                                )
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <%# Eval("Treatment1")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 4%">
                                                                <%# Eval("DueDate")%>/<%# Eval("Bulan")%>/<%# Eval("Tahun")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 4%">
                                                                <%# Eval("Approval")%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 4%">
                                                                <%# Eval("StatusX")%>
                                                            </td>
                                                            <%--<td class="kotak angka" style="width: 10%">
                                                                <%# Eval("FileName")%>
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                            CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                            ImageUrl="~/images/Logo_Download.png" />
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 10%">
                                                                <asp:ImageButton ToolTip="Upload Attachment" ID="attPrs" runat="server" CssClass='<%# Eval("ID") %>'
                                                                    CommandArgument='<%# Container.ItemIndex %>' CommandName="attachPrs" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr class="Line3 Baris" id="lstF" runat="server">
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                    <td class="kotak angka">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
