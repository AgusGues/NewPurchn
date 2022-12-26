<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HelpDeskKeluhan.aspx.cs" Inherits="GRCweb1.Modul.HelpDesk.HelpDeskKeluhan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width:100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>
                                            <td >
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <strong>INPUT KELUHAN</strong>
                                                        </td>
                                                        <td style="width: 89%">
                                                        </td>
                                                        <td style="width: 37px">
                                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                                                font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                                font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold;
                                                                font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick" />
                                                        </td>
                                                        <td style="width: 116px">
                                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                                <asp:ListItem Value="HelpDeskNo">No HelpDesk</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 70px">
                                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 70px">
                                                            <input id="btnSearch" runat="server" style="background-image: url(../images/Button_Back.gif);
                                                                background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cari"
                                                                onserverclick="btnSearch_ServerClick" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: auto; height: 100%; width: 100%;">
                                    <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td style="height: 101px; width: 100%;">
                                                <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 107%;" cellspacing="1"
                                                    cellpadding="0" border="0">
                                                    <tr>
                                                        <td style="width: 87px; height: 3px" valign="top"></td>
                                                        <td style="width: 126px; height: 3px" valign="top"></td>
                                                        <td style="height: 3px; width: 13px;" valign="top"></td>
                                                        <td style="width: 6px; height: 3px" valign="top"></td>
                                                        <td style="width: 205px; height: 3px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; No HelpDesk</span>
                                                        </td>
                                                        <td style="width: 126px; height: 6px" valign="top">
                                                            <asp:TextBox ID="txtHelpDeskNo" runat="server" BorderStyle="Groove" Width="233" BackColor="Silver"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 6px; width: 13px;" valign="top">&nbsp;
                                                        </td>
                                                        <td style="width: 6px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp;Kategori</span>
                                                        </td>
                                                        <td style="width: 205px; height: 6px" valign="top">
                                                            <asp:DropDownList ID="ddlHelpDeskCategory" runat="server" Width="233px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td rowspan="6" valign="top">&nbsp;&nbsp;&nbsp;KETERANGAN<br />
                                                            <br />
                                                            &nbsp;&nbsp;&nbsp;K1(KATEGORI 1) : &nbsp; Dapat diselesaikan dalam waktu 1 hari<br />
                                                            &nbsp;&nbsp;&nbsp;K2(KATEGORI 2) : &nbsp; Dapat diselesaikan dalam waktu < 1 minggu<br />
                                                            &nbsp;&nbsp;&nbsp;K3(KATEGORI 3) : &nbsp; Dapat diselesaikan dalam jangan waktu > 1 minggu
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; height: 19px" valign="middle">
                                                            <span style="font-size: 10pt">&nbsp; Tanggal</span>
                                                        </td>
                                                        <td rowspan="1" style="width: 126px; height: 19px;">
                                                            <asp:TextBox ID="txtHelpTgl" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 13px; height: 19px">
                                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtHelpTgl" Format="dd-MMM-yyyy"
                                                                runat="server"></cc1:CalendarExtender>
                                                        </td>
                                                        <td style="width: 6px; height: 19px">
                                                            <span style="font-size: 10pt">&nbsp;Analisa</span>
                                                        </td>
                                                        <td style="width: 205px;" rowspan="3" valign="top">
                                                            <asp:TextBox ID="txtAnalisa" runat="server" BorderStyle="Groove" Width="233px" Height="71px"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Dept ID</span>
                                                        </td>
                                                        <td style="width: 126px; height: 6px" valign="top">
                                                            <asp:TextBox ID="txtDeptID" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="height: 6px; width: 13px;" valign="top">&nbsp;
                                                        </td>
                                                        <td style="width: 6px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Departemen</span>
                                                        </td>
                                                        <td style="width: 126px; height: 6px" valign="top">
                                                            <asp:DropDownList ID="ddlDept" runat="server" Width="233px" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="height: 6px; width: 13px;" valign="top">&nbsp;
                                                        </td>
                                                        <td style="width: 6px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px;" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; PIC</span>
                                                        </td>
                                                        <td style="width: 126px;" valign="top">
                                                            <asp:TextBox ID="txtPIC" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                            <%-- <asp:TextBox ID="txtStatus" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True"
                                                                                                     Width="60px">
                                                                                                    <asp:ListItem>Open</asp:ListItem>
                                                                                                    <asp:ListItem>Progress</asp:ListItem>
                                                                                                    <asp:ListItem>Solved</asp:ListItem>
                                                                                                </asp:DropDownList>--%>
                                                        </td>
                                                        <td style="width: 13px;" valign="top">&nbsp;
                                                        </td>
                                                        <td style="width: 6px;" valign="top">
                                                            <span style="font-size: 10pt">&nbsp;Perbaikan</span>
                                                        </td>
                                                        <td style="width: 205px;" valign="top" rowspan="2">


                                                            <asp:TextBox ID="txtPerbaikan" runat="server" BorderStyle="Groove" Height="71px"
                                                                TextMode="MultiLine" Width="233px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px;" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Status</span>
                                                        </td>
                                                        <td style="width: 126px;" valign="top">
                                                            <%-- <asp:TextBox ID="txtStatus" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>--%><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True"
                                                                Width="233px">
                                                                <asp:ListItem>Open</asp:ListItem>
                                                                <asp:ListItem>Progress</asp:ListItem>
                                                                <asp:ListItem>Solved</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 13px"></td>
                                                        <td style="width: 6px">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px"><span style="font-size: 10pt">&nbsp; Keluhan</span></td>
                                                        <td style="width: 126px;" rowspan="3" valign="top">
                                                            <asp:TextBox ID="txtKeluhan" runat="server" BorderStyle="Groove" Width="233px" Height="71px"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 13px">&nbsp;</td>
                                                        <td style="width: 6px"><span style="font-size: 10pt">&nbsp;Tanggal Selesai</span></td>
                                                        <td style="width: 6px">
                                                            <asp:TextBox ID="txtTglPerbaikan" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox></td>
                                                        <td>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTglPerbaikan" Format="dd-MMM-yyyy"
                                                                runat="server"></cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px"></td>
                                                        <td style="width: 13px">&nbsp;</td>
                                                        <td style="width: 6px" valign="top"><span style="font-size: 10pt">Penyelesaian</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPenyelesaian" runat="server" AutoPostBack="True"
                                                                Width="233px">
                                                                <asp:ListItem>K1</asp:ListItem>
                                                                <asp:ListItem>K2</asp:ListItem>
                                                                <asp:ListItem>K3</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px">&nbsp;</td>
                                                        <td style="width: 13px">&nbsp;</td>
                                                        <td style="width: 6px"><span style="font-size: 10pt">CreatedBy</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox></td>

                                                    </tr>
                                                </table>
                                                <hr width="100%" size="1">
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="Table2" style="left: 0px; top: 0px; width: 95%;" cellspacing="1" cellpadding="0"
                                        border="0" height="165">
                                        <tr>
                                            <td colspan="1" style="height: 3px" valign="top" width="100"></td>
                                            <td style="height: 3px" valign="top" colspan="1">
                                                <span style="font-size: 10pt">&nbsp; <strong>List Keluhan</strong></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" valign="top" width="100">&nbsp; &nbsp;
                                            </td>
                                            <td style="width: 100%; height: 100%" valign="top">
                                                <div>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        PageSize="10" OnRowCommand="GridView1_RowCommand" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                        OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="HelpDeskNo" HeaderText="NO" />
                                                            <asp:BoundField DataField="HelpTgl" HeaderText="TANGGAL" DataFormatString="{0:d}" />
                                                            <%--<asp:BoundField DataField="CreatedBy" HeaderText="USER" />--%>
                                                            <asp:BoundField DataField="PIC" HeaderText="PIC" />
                                                            <asp:BoundField DataField="DeptName" HeaderText="DEPARTEMEN" />
                                                            <asp:BoundField DataField="Keluhan" HeaderText="KELUHAN" />
                                                            <asp:BoundField DataField="Analisa" HeaderText="ANALISA" />
                                                            <asp:BoundField DataField="Perbaikan" HeaderText="PERBAIKAN" />
                                                            <asp:BoundField DataField="HelpDeskCategoryID" HeaderText="KATEGORI" />
                                                            <asp:BoundField DataField="Status" HeaderText="STATUS" />
                                                            <asp:BoundField DataField="TglPerbaikan" HeaderText="TANGGAL PERBAIKAN" DataFormatString="{0:d}" />
                                                            <asp:ButtonField CommandName="add" Text="Pilih" />
                                                        </Columns>
                                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Dotted" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
