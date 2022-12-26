<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FCItems.aspx.cs" Inherits="GRCweb1.Modul.Factory.FCItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function confirmation() {
            if (confirm('Yakin mau hapus data ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table class="nbTableHeader" style="width: 568px">
                                <tr>
                                    <td >
                                        <strong>&nbsp;MASTER ITEM</strong>
                                    </td>
                                    <td ></td>
                                    <td >
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru"
                                            onserverclick="btnNew_ServerClick" />
                                    </td>
                                    <td >
                                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Simpan"
                                            onserverclick="btnUpdate_ServerClick" />
                                    </td>
                                    <td >
                                        <asp:Button ID="btnDelete" runat="server" Enabled="False" Height="22px" OnClick="Button1_Click"
                                            OnClientClick="return confirmation();" Text="Delete" />
                                    </td>
                                    <td >&nbsp;
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="98px" Height="17px">
                                            <asp:ListItem Value="PartNo">PartNo</asp:ListItem>
                                            <asp:ListItem>Tebal</asp:ListItem>
                                            <asp:ListItem>Panjang</asp:ListItem>
                                            <asp:ListItem>Lebar</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtSearch" runat="server" Width="96px"></asp:TextBox>
                                    </td>
                                    <td >
                                        <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cari"
                                            onserverclick="btnSearch_ServerClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr >
                        <td>
                            <div style="overflow: auto; height: 100%; width: 100%;">
                                <table class="tblForm" id="Table4" style="width: 100%;">
                                    <tr>
                                        <td>Proses&nbsp;&nbsp;
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="ddlProses" runat="server" Height="17px" Width="104px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlProses_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Tahap 1</asp:ListItem>
                                                <asp:ListItem Value="2">Transit</asp:ListItem>
                                                <asp:ListItem Value="3">Tahap 3</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp; Kode&nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlKode" runat="server" Height="17px" Width="104px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlKode_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                        </td>
                                        <td rowspan="6">
                                            <asp:Panel ID="Panel7" runat="server" BackColor="#CCFFCC" Font-Size="X-Small" Height="173px"
                                                ScrollBars="Both">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Underline="True" Text="Group Marketing"></asp:Label>
                                                <asp:RadioButtonList ID="RBList" runat="server" AutoPostBack="True" Font-Size="X-Small">
                                                </asp:RadioButtonList>
                                            </asp:Panel>
                                        </td>
                                        <td >
                                            <asp:TextBox ID="txtID" runat="server" BorderStyle="Groove" Width="76px" Visible="False">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >&nbsp;
                                        </td>
                                        <td >
                                            <asp:Panel ID="Panel4" runat="server" Enabled="False">
                                                &nbsp; Jenis&nbsp;&nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJenis_SelectedIndexChanged">
                                                                <asp:ListItem Value="3">OK</asp:ListItem>
                                                                <asp:ListItem Value="P">BP</asp:ListItem>
                                                                <asp:ListItem Value="W">KW</asp:ListItem>
                                                                <asp:ListItem Value="S">BS</asp:ListItem>
                                                                <asp:ListItem Value="1">Unfinish</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="0">--Pilih Jenis--</asp:ListItem>
                                                            </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;&nbsp; Sisi&nbsp;
                                                            <asp:DropDownList ID="ddlSisi" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSisi_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 11%; height: 6px" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%; font-weight: normal; font-size: x-small;" valign="top">&nbsp;
                                        </td>
                                        <td align="left" bgcolor="#CCFFCC" style="width: 492px; font-weight: normal; font-size: x-small;"
                                            valign="top">Tebal&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtTebal" runat="server" BorderStyle="Groove" Width="45px" AutoPostBack="True"
                                                            OnTextChanged="txtTebal_TextChanged"></asp:TextBox>
                                            &nbsp;&nbsp; Lebar&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtLebar" runat="server" BorderStyle="Groove" Width="46px" AutoPostBack="True"
                                                            OnTextChanged="txtLebar_TextChanged"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Panjang&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtPanjang" runat="server" BorderStyle="Groove" Width="46px" AutoPostBack="True"
                                                            OnTextChanged="txtPanjang_TextChanged"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp; Volume&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtVolume" runat="server" BorderStyle="Groove" Width="73px" ReadOnly="True"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td style="width: 11%; height: 6px" valign="top">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%; font-weight: normal; font-size: x-small;" valign="top">Part No.&nbsp;&nbsp;
                                        </td>
                                        <td align="left" style="width: 492px;" valign="top">
                                            <asp:TextBox ID="txtPartNo" runat="server" BorderStyle="Groove" Width="178px"></asp:TextBox>
                                        </td>
                                        <td style="width: 11%; height: 6px" valign="top">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%; font-weight: normal; font-size: x-small;" valign="top">Deskripsi Item&nbsp;&nbsp;
                                        </td>
                                        <td align="left" style="width: 492px;" valign="top">
                                            <asp:TextBox ID="txtNamaItem" runat="server" BorderStyle="Groove" Width="430px"></asp:TextBox>
                                        </td>
                                        <td style="width: 11%; height: 6px" valign="top">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 6px; font-weight: bold;" valign="top" align="left" colspan="2">
                                            <span style="font-size: 10pt">&nbsp; List Item</span>
                                        </td>
                                        <td style="width: 11%; height: 6px" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px" colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Vertical">
                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                    PageSize="200" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="Nama Item" />
                                                        <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                        <asp:BoundField DataField="Kode" HeaderText="Kode" />
                                                        <asp:BoundField DataField="Tebal" HeaderText="Tebal" />
                                                        <asp:BoundField DataField="Panjang" HeaderText="Panjang" />
                                                        <asp:BoundField DataField="Lebar" HeaderText="Lebar" />
                                                        <asp:BoundField DataField="Volume" HeaderText="Volume" />
                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 11%; height: 19px">&nbsp;
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
