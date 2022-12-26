<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MinMaxdeliveryKertas.aspx.cs" Inherits="GRCweb1.Modul.Master.MinMaxdeliveryKertas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%;" >
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader">
                                <tr>
                                    <td >
                                        <strong>MASTER DATA MINMAX TOLERANSI 30% PENGIRIMAN KERTAS</strong>
                                    </td>
                                    <td style="width: 37px">
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru"
                                            onserverclick="btnNew_ServerClick" />
                                    </td>
                                    <td style="width: 75px">
                                        <input id="btnSimpan" runat="server" onserverclick="btnUpdate_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Simpan" />
                                    </td>
                                    <td>
                                        <input id="btnHapus" runat="server" onserverclick="btnHapus_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Hapus" />
                                    </td>
                                    <td style="width: 50px">
                                        <asp:DropDownList ID="ddlSearch" runat="server" Height="16px" Width="115px">
                                            <asp:ListItem Value="SupplierName">Supplier Name</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5px">
                                        <asp:TextBox ID="txtCari" runat="server" BorderStyle="Groove" ReadOnly="False" Width="181px"></asp:TextBox>
                                    </td>
                                    <td style="width: 10px">
                                        <input id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" type="button" value="Cari" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="width: 3px">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div style="overflow: auto; height: 100%; width: 100%;">
                                <table id="TblIsi" cellspacing="0" cellpadding="0" border="0" style="width: 97%">
                                    <tr>
                                        <td >&nbsp;Plant
                                        </td>
                                        <td style="height: 19px" valign="top">
                                            <asp:DropDownList ID="ddlPlant" runat="server" AutoPostBack="True" Height="16px"
                                                Width="250px" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-</asp:ListItem>
                                                <asp:ListItem Value="1">Citeureup</asp:ListItem>
                                                <asp:ListItem Value="7">Karawang</asp:ListItem>
                                                <asp:ListItem Value="13">Jombang</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RbEdit" runat="server" Checked="True" GroupName="a"
                                                    Text="Edit Data" />
                                            &nbsp;<asp:RadioButton ID="RbEdit0" runat="server" GroupName="a"
                                                Text="Tambah Data" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px; height: 6px">&nbsp;Nama Group
                                        </td>
                                        <td style="height: 19px;" valign="top">
                                            <asp:TextBox ID="txtGroup" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                OnTextChanged="txtCariSupplier_TextChanged" Width="506px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px; height: 6px;">
                                            <span style="font-size: 10pt">&nbsp; Cari Supplier</span>
                                        </td>
                                        <td style="height: 19px" valign="top">
                                            <asp:TextBox ID="txtCariSupplier" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                OnTextChanged="txtCariSupplier_TextChanged" Width="506px"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 142px; height: 6px">
                                            <span style="font-size: 10pt">&nbsp; Nama Supplier</span>
                                        </td>
                                        <td style="height: 19px" valign="top">
                                            <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" Height="16px"
                                                OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" Width="506px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px; height: 19px">
                                            <span style="font-size: 10pt">&nbsp; Minimum Pengiriman</span>
                                        </td>
                                        <td style="height: 19px" valign="top">
                                            <asp:TextBox ID="txtMin" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                Width="211px"></asp:TextBox>
                                            &nbsp;Kg
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 142px; height: 6px">
                                            <span style="font-size: 10pt">&nbsp; Maximum Toleransi</span>
                                        </td>
                                        <td style="height: 19px">
                                            <asp:TextBox ID="txtMax" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                Width="211px"></asp:TextBox>
                                            &nbsp;Kg
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 6px" bgcolor="#9999FF">
                                            <span style="font-size: 10pt"><strong>List</strong></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 6px">
                                            <div id="div2" style="overflow: auto; height: 250px;">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    PageSize="20" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                        <asp:BoundField DataField="PlantID" HeaderText="PlantID" />
                                                        <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" />
                                                        <asp:BoundField DataField="Plant" HeaderText="Plant" />
                                                        <asp:BoundField DataField="GroupName" HeaderText="GroupName" />
                                                        <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" />
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="Min30" HeaderText="Min30" />
                                                        <asp:BoundField DataField="Max30" HeaderText="Max30" />
                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
