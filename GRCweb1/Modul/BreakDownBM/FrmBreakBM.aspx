<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmBreakBM.aspx.cs" Inherits="GRCweb1.Modul.BreakDownBM.FrmBreakBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
            <div id="Div1" runat="server" class="table-responsive" >
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;INPUT BREAKDOWN TIME</strong><asp:HiddenField ID="hfScrollPosition"
                                                Value="0" runat="server" />
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <%-- <td style="width: 5px">
                                            <asp:Button ID="btnList" runat="server" Style="background-color: white; font-weight: bold;
                                                font-size: 11px;" Text="List" OnClick="btnList_ServerClick" />
                                        </td>--%>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnDelete" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                Text="Hapus" OnClick="btnDelete_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="BreakDownNo">No Breakdown</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                        <td style="width: 3px">&nbsp
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
                                            <td style="width: 249px;"></td>
                                            <td style="width: 204px;"></td>
                                            <td style="width: 102px;"></td>
                                            <td style="width: 209px;"></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;No Breakdown</td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtBreakDownNo" runat="server" Width="192" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px;">&nbsp;Nama Line&nbsp;&nbsp;</td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlLine" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList>
                                            </td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; height: 19px" valign="top">&nbsp;Tanggal</td>
                                            <td style="width: 200px; height: 19px" valign="top">
                                                <asp:TextBox ID="txtTglBreak" runat="server" BorderStyle="Groove" Width="192px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTglBreak" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                                &nbsp;Keterangan &nbsp;&nbsp;</td>
                                            <td style="width: 169px;" rowspan="2" valign="top">
                                                <asp:TextBox ID="txtKet"
                                                    runat="server" BorderStyle="Groove" Width="192px" Height="48px"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;Breakdown Time</td>
                                            <td style="width: 204px;">Start&nbsp;&nbsp;<asp:TextBox ID="txtStartBD" runat="server" BorderStyle="Groove" Width="47px" AutoPostBack="True"
                                                ToolTip="Ex : 00:00" MaxLength="5"></asp:TextBox>&nbsp; Finish &nbsp;</span><asp:TextBox ID="txtFinishBD" runat="server" BorderStyle="Groove" Width="47px"
                                                    OnTextChanged="txtFinishBD_TextChanged" AutoPostBack="True" ToolTip="Ex : 00:00" MaxLength="5"></asp:TextBox></td>
                                            <td style="width: 102px;"></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;BD Time</td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtBDTime" runat="server" BorderStyle="Groove" Width="100px" AutoPostBack="True"></asp:TextBox></td>
                                            <td style="width: 102px;">&nbsp;Pinalti&nbsp;&nbsp;</td>
                                            <td style="width: 209px;">
                                                <asp:TextBox ID="txtPinalti" runat="server" BorderStyle="Groove" Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;Syarat Breakdown</td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtSyarat" runat="server" BorderStyle="Groove" Width="192px" AutoPostBack="true" ReadOnly="False"
                                                    OnTextChanged="txtSyarat_TextChanged" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox></td>
                                            <td style="width: 102px;">&nbsp;Lokasi Problem&nbsp;&nbsp;</td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlProblem" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;Nama Group</td>
                                            <td style="width: 204px;">
                                                <asp:DropDownList ID="ddlGrup" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList></td>
                                            <td style="width: 102px;">&nbsp;Lokasi Charge&nbsp;&nbsp;</td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlCharge" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;Ketebalan</td>
                                            <td style="width: 204px;">
                                                <asp:DropDownList ID="ddlKetebalan" runat="server" Width="192px"></asp:DropDownList></td>
                                            <td style="width: 102px;">&nbsp;Group Off&nbsp;&nbsp;</td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlgrupline" runat="server" Width="192px"></asp:DropDownList></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;"></td>
                                            <td style="width: 204px;"></td>
                                            <td style="width: 102px;"></td>
                                            <td style="width: 209px;"></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;"></td>
                                            <td style="width: 204px;"></td>
                                            <td style="width: 102px;"></td>
                                            <td style="width: 209px;"></td>
                                            <td style="width: 205px;"></td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <%-- <tr>
                                             <td style="width: 249px;"></td>
                                             <td style="width: 204px;"></td>
                                             <td style="width: 102px;"></td>
                                             <td style="width: 209px;"><input id="btnUpdate1" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Update" onserverclick="btnUpdate1_ServerClick" /></td>
                                             <td style="width: 205px;"> </td>   
                                              <td style="width: 205px;"> </td>  
                                         </tr>--%>
                                        <tr>
                                            <td colspan="6">
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="right">
                                        <%-- <asp:CheckBox ID="ChkByLine" runat="server" AutoPostBack="True" OnCheckedChanged="ChkByLine_CheckedChanged"
                                            Text="Sort By Line" />--%>
                                            View By Line &nbsp;<asp:DropDownList ID="ddlLineNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLineNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>&nbsp;
                                    </div>
                                    <div id="div2" style="height: 350px;" class="contentlist" onscroll="setScrollPosition(this.scrollTop);">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                            Width="100%" OnRowCommand="GridView1_RowCommand" AllowPaging="True"
                                            OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnRowDataBound="GridView1_RowDataBound" PageSize="600">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                <%--<asp:BoundField DataField="BreakDownNo" HeaderText="NO BREAKDOWN" />  --%>
                                                <asp:BoundField DataField="TglBreak" HeaderText="TANGGAL" DataFormatString="{0:d}" />
                                                <%-- <asp:BoundField DataField="OperationalSche" HeaderText="TTL PROD" />--%>
                                                <asp:BoundField DataField="StartBD" HeaderText="START" DataFormatString="{0:HH:mm}" />
                                                <asp:BoundField DataField="FinishBD" HeaderText="FINISH" DataFormatString="{0:HH:mm}" />
                                                <asp:BoundField DataField="BDTime" HeaderText="BREAKTIME" DataFormatString="{0:HH:mm:ss}" />
                                                <%--<asp:BoundField DataField="FrekBD" HeaderText="FREK" />       --%>
                                                <asp:BoundField DataField="Syarat" HeaderText="SYARAT" />
                                                <asp:BoundField DataField="BM_plantGroupID" HeaderText="GROUP" />
                                                <asp:BoundField DataField="BM_PlantID" HeaderText="LINE" />
                                                <%--<asp:BoundField DataField="OperationalTime" HeaderText="OP TIME" />         --%>
                                                <asp:BoundField DataField="Ket" HeaderText="KETERANGAN" />
                                                <asp:BoundField DataField="Pinalti" HeaderText="PINALTI ( % )" />
                                                <asp:BoundField DataField="BreakBM_MasterProblemID" HeaderText="LOKASI PROBLEM" />
                                                <asp:BoundField DataField="BreakBM_MasterChargeID" HeaderText="LOKASI CHARGE" />
                                                <asp:BoundField DataField="GroupOff" HeaderText="GROUP OFF" />
                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
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
