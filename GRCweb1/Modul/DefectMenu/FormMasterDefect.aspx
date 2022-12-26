<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormMasterDefect.aspx.cs" Inherits="GRCweb1.Modul.DefectMenu.FormMasterDefect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }       
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                <table >
                    <tr>
                        <td >
                            <table >
                                <tr>
                                    <td >
                                        <strong>&nbsp;Master Defect</strong>
                                    </td>
                                    <td >
                                    </td>
                                    <td >
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                            font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                    </td>
                                    <td>
                                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                            font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                    </td>
                                    <td >
                                        <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold;
                                            font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick" />
                                    </td>
                                    <td >
                                        <input id="btnPrint" runat="server" style="background-color: white; font-weight: bold;
                                            font-size: 11px;" type="button" value="Cetak" onclick="Cetak()" />
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                            <asp:ListItem Value="DefectName">Nama Defect</asp:ListItem>
                                            <asp:ListItem Value="DefectCode">Kode Defect</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                    </td>
                                    <td >
                                        <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                            font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr >
                        <td>
                            <table width="100%">
                                <tr>
                                    <td >
                                    </td>
                                    <td >
                                    </td>
                                    <td >
                                    </td>
                                    <td >
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <span style="font-size: 10pt">&nbsp; Kode Defect</span>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtItemCode" runat="server" BorderStyle="Groove" Width="233" TabIndex="1"
                                            AutoPostBack="True" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                    </td>
                                    <td >
                                        <span style="font-size: 10pt">&nbsp;<b> </b><span style="color: #FF0000; font-weight: bold">
                                        </span></span>
                                    </td>
                                    <td >
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <span style="font-size: 10pt">&nbsp; Nama Defect</span>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtItemName" runat="server" BorderStyle="Groove" Width="233" AutoPostBack="True"></asp:TextBox>
                                    </td>
                                    <td >
                                        &nbsp;
                                    </td>
                                    <td >
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                            OnPageIndexChanging="GridView1_PageIndexChanging" 
                                            OnRowCommand="GridView1_RowCommand" TabIndex="12" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                <asp:BoundField DataField="DefCode" HeaderText="Kode Defect" />
                                                <asp:BoundField DataField="DefName" HeaderText="Nama Defect" />
                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                            </Columns>
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
