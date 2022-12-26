<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPostingInventory.aspx.cs" Inherits="GRCweb1.Modul.ListReport.FormPostingInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function btnPosting_onclick() {

        }

        function btnRePosting_onclick() {

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;PERIODE POSTING</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%;" class="content">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse:collapse">
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 204px; height: 3px" valign="top">
                                            </td>
                                            <td style="height: 3px; width: 169px;" valign="top">
                                            </td>
                                            <td style="width: 209px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Bulan</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                                    <asp:ListItem>Pilih Bulan</asp:ListItem>
                                                    <asp:ListItem>Januari</asp:ListItem>
                                                    <asp:ListItem>Februari</asp:ListItem>
                                                    <asp:ListItem>Maret</asp:ListItem>
                                                    <asp:ListItem>April</asp:ListItem>
                                                    <asp:ListItem>Mei</asp:ListItem>
                                                    <asp:ListItem>Juni</asp:ListItem>
                                                    <asp:ListItem>Juli</asp:ListItem>
                                                    <asp:ListItem>Agustus</asp:ListItem>
                                                    <asp:ListItem>September</asp:ListItem>
                                                    <asp:ListItem>Oktober</asp:ListItem>
                                                    <asp:ListItem>November</asp:ListItem>
                                                    <asp:ListItem>Desember</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <input id="btnPosting" runat="server" onserverclick="btnPrint_ServerClick" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                                                    background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Posting"
                                                    visible="False" onclick="return btnPosting_onclick()" />&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 6px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Tahun</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px">
                                                <asp:TextBox ID="txtTahun" runat="server" BorderStyle="Groove" Height="22px" Width="95px" Visible="false">
                                                </asp:TextBox>
                                                <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Click" AutoPostBack="true" Width="95px"></asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                &nbsp; Group&nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:DropDownList ID="ddlTipeSPP" runat="server" Width="233px" OnTextChanged="ddlTipeSPP_OnTextChanged"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTipeSPP_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                <asp:CheckBox ID="chkActive" runat="server" Font-Size="XX-Small" Text="Active" />
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px;">
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                        <tr >
                                            <td style="width: 20px; border-top:2px solid">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; border-top:2px solid">
                                                &nbsp;<%--Persiapan Data ....--%>
                                            </td>
                                            <td style="width: 204px;border-top:2px solid">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px;border-top:2px solid">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px;border-top:2px solid">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px;border-top:2px solid">
                                                <asp:TextBox ID="txtPersiapan" runat="server" AutoPostBack="False" BorderColor="White"
                                                    BorderStyle="Groove" Height="19" ReadOnly="true" Width="204" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                <%--Kumpulkan Receipt ....--%>
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" 
                                                    Text="Posting Saldo" Width="120px" />
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:TextBox ID="txtReceipt" runat="server" AutoPostBack="False" BorderColor="White"
                                                    BorderStyle="Groove" Height="19" ReadOnly="true" Width="204" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                <%--Kumpulkan Pemakaian ....--%>
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" 
                                                    Text="Reset Price" Width="120px" Visible="False" />
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:TextBox ID="txtPakai" runat="server" AutoPostBack="False" BorderColor="White"
                                                    BorderStyle="Groove" Height="19" ReadOnly="true" Width="204" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <input id="btnReset" runat="server" onserverclick="btnReset_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 120px;" type="button"
                                                    value="Reset Price" visible="False" />
                                            </td>
                                            
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                <%--Kumpulkan Adjustment ....--%>
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" 
                                                    Text="Posting Price" Width="120px" />
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                <asp:TextBox ID="txtAdjust" runat="server" AutoPostBack="False" BorderColor="White"
                                                    BorderStyle="Groove" Height="19" ReadOnly="true" Width="204" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                <input id="btnRePosting" runat="server" onserverclick="btnRePosting_ServerClick"
                                                    style="background-color: white; font-weight: bold; font-size: 11px; width: 120px;"
                                                    type="button" value="Reposting Price" visible="False" onclick="return btnRePosting_onclick()" />
                                            </td>
                                            
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                               <%-- Kumpulkan Retur ....--%>
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
                                                    Text="Posting Avg Price" Width="120px" Visible="False" />
                                            </td>
                                            
                                            <td style="width: 204px; height: 19px;">
                                                <asp:TextBox ID="txtRetur" runat="server" AutoPostBack="False" BorderColor="White"
                                                    BorderStyle="Groove" Height="19" ReadOnly="true" Width="204" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 197px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 204px; height: 19px;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height:255px">
                                    <div id="lst" runat="server" visible="false">
                                    <b>&nbsp; Log Posting</b>
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="kotak">No.</th>
                                                <th style="width:10%" class="kotak">Material Group</th>
                                                <th style="width:15%" class="kotak">Event</th>
                                                <th style="width:12%" class="kotak">Tanggal</th>
                                                <th style="width:10%" class="kotak">User</th>
                                                <th style="width:8%" class="kotak">IP Address</th>
                                                <th style="width:20%" class="kotak">Keterangan</th>
                                            </tr>
                                            <tbody>
                                                <asp:Repeater ID="lstEvent" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak"><%# Eval("ModulName") %></td>
                                                            <td class="kotak"><%# Eval("EventName") %></td>
                                                            <td class="kotak"><%# Eval("CreatedTime") %></td>
                                                            <td class="kotak"><%# Eval("CreatedBy") %></td>
                                                            <td class="kotak"><%# Eval("IPAddress") %></td>
                                                            <td class="kotak"><%# Eval("Keterangan") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        </div>
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
