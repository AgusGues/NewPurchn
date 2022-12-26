<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterMinMax.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterMinMax" %>

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
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        <strong>&nbsp;INVENTORY</strong>
                                    </td>
                                    <td style="width: 100%"></td>
                                    <td style="width: 37px">
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru"
                                            onserverclick="btnNew_ServerClick" />
                                    </td>
                                    <td style="width: 75px">
                                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Simpan"
                                            onserverclick="btnUpdate_ServerClick" />
                                    </td>
                                    <td style="width: 5px">
                                        <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Hapus"
                                            onserverclick="btnDelete_ServerClick" />
                                    </td>
                                    <td style="width: 70px">
                                        <input id="btnPrint" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cetak"
                                            onclick="Cetak()" />
                                    </td>
                                    <td style="width: 70px">
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                            <asp:ListItem Value="ItemName">Nama Item</asp:ListItem>
                                            <asp:ListItem Value="ItemCode">Kode Item</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 70px">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                    </td>
                                    <td style="width: 70px">
                                        <input id="btnSearch" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cari"
                                            onserverclick="btnSearch_ServerClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr height="100%">
                        <td height="100%" style="width: 100%">
                            <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td style="height: 101px; width: 100%;">
                                        <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                            cellpadding="0" border="0">
                                            <tr>
                                                <td style="width: 197px; height: 3px" valign="top"></td>
                                                <td style="width: 204px; height: 3px" valign="top"></td>
                                                <td style="height: 3px; width: 169px;" valign="top"></td>
                                                <td style="width: 209px; height: 3px" valign="top"></td>
                                                <td style="width: 205px; height: 3px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Cari Nama Barang</span>
                                                </td>
                                                <td style="width: 204px; height: 6px" valign="top">
                                                    <asp:TextBox ID="txtItemName" runat="server" BorderStyle="Groove" TabIndex="2" Width="233"
                                                        AutoPostBack="True"></asp:TextBox>
                                                </td>
                                                <td style="height: 6px; width: 169px;" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; </span>
                                                </td>
                                                <td style="width: 209px; height: 6px" valign="top">&nbsp;
                                                </td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Nama Item</span>
                                                </td>
                                                <td style="height: 6px;" valign="top" colspan="3">
                                                    <asp:DropDownList ID="ddlNamaBarang" runat="server" AutoPostBack="True" EnableTheming="true"
                                                        Height="16px" OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged" Width="581px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; <span style="font-size: 10pt">Kode Item</span></span>
                                                </td>
                                                <td style="width: 204px; height: 6px" valign="top">
                                                    <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        ReadOnly="true " Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 169px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; Satuan</span>
                                                </td>
                                                <td style="width: 209px; height: 6px" valign="top">
                                                    <asp:TextBox ID="txtUomCode" runat="server" BorderStyle="Groove" TabIndex="10" Width="233"
                                                        AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 205px; height: 19px"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; <span style="font-size: 10pt">Minimum Stok</span></span>
                                                </td>
                                                <td style="width: 204px; height: 19px;">
                                                    <asp:TextBox ID="txtMinStock" runat="server" BorderStyle="Groove" TabIndex="10" Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 169px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; </span>&nbsp;
                                                </td>
                                                <td style="width: 209px; height: 19px">&nbsp;
                                                </td>
                                                <td style="width: 205px; height: 19px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; <span style="font-size: 10pt"><span style="font-size: 10pt">Maximum Stok</span></span></span>
                                                </td>
                                                <td style="width: 204px; height: 19px;">
                                                    <asp:TextBox ID="txtMaxStock" runat="server" BorderStyle="Groove" TabIndex="10" Width="233"></asp:TextBox>
                                                </td>
                                                <td style="width: 169px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp; </span>&nbsp;
                                                </td>
                                                <td style="width: 209px; height: 19px">&nbsp;
                                                </td>
                                                <td style="width: 205px; height: 19px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 197px; height: 19px">
                                                    <span style="font-size: 10pt">&nbsp;<span style="font-size: 10pt"><span style="font-size: 10pt">
                                                                                                    Re-Order Point</span></span></span>
                                                </td>
                                                <td style="width: 204px; height: 19px;">
                                                    <asp:TextBox ID="txtReOrder" runat="server" BorderStyle="Groove" TabIndex="10" Width="233"></asp:TextBox>
                                                </td>
                                    </td>
                                    <td style="width: 169px; height: 19px">
                                        <span style="font-size: 10pt">&nbsp; </span>&nbsp;
                                    </td>
                                    <td style="width: 209px; height: 19px">&nbsp;
                                    </td>
                                    <td style="width: 205px; height: 19px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 197px; height: 19px">
                                        <span style="font-size: 10pt">&nbsp;</span>
                                    </td>
                                    <td style="width: 204px; height: 19px;">&nbsp;
                                    </td>
                                    <td style="width: 169px; height: 19px">
                                        <span style="font-size: 10pt">&nbsp; </span>&nbsp;
                                    </td>
                                    <td style="width: 204px; height: 19px;">&nbsp;
                                    </td>
                                    <td>
                                        <caption>
                                            &nbsp;</caption>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="Table2" style="left: 0px; top: 0px; width: 100%;" cellspacing="1" cellpadding="0" border="0" height="165" >
                    <tr>
                        <td colspan="1" style="height: 3px" valign="top" width="100"></td>
                        <td style="height: 3px" valign="top" colspan="1">
                            <span style="font-size: 10pt">&nbsp; <strong>List</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 100%" valign="top" width="100">&nbsp; &nbsp;
                        </td>
                        <td style="width: 100%; height: 100%" valign="top">
                            <div id="div2">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="true" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    TabIndex="12">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Item" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                        <asp:BoundField DataField="SupplierCode" HeaderText="Supplier" />
                                        <asp:BoundField DataField="DeptName" HeaderText="Departemen" />
                                        <asp:BoundField DataField="Jumlah" HeaderText="Q t y" />
                                        <asp:BoundField DataField="UOMDesc" HeaderText="Satuan" />
                                        <asp:BoundField DataField="Harga" HeaderText="Harga" />
                                        <asp:BoundField DataField="MinStock" HeaderText="Minim Stock" />
                                        <asp:BoundField DataField="Gudang" HeaderText="Gudang" />
                                        <asp:BoundField DataField="RakID" HeaderText="Rak" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
