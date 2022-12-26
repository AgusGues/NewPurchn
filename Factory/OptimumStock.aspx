<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptimumStock.aspx.cs" Inherits="GRCweb1.Modul.Factory.OptimumStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-responsive">
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td colspan="5">
                <asp:RadioButton ID="RBTglInput" runat="server" GroupName="a" Text="Tanggal  Input"
                    Visible="False" />
                <asp:RadioButton ID="RBTglProduksi" runat="server" GroupName="a" Text="Tanggal  Produksi"
                    Visible="False" />
                <asp:RadioButton ID="RBTglPotong" runat="server" Checked="True" GroupName="a" Text="Tanggal Potong"
                    Visible="False" />
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtdrtanggal_TextChanged" Enabled="False" Visible="False"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtsdtanggal_TextChanged" Enabled="False" Visible="False"></asp:TextBox>
                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 82px" align="right">
                Periode
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddTahun" runat="server">
                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                    Width="132px">
                    <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
                    <asp:ListItem Value="01">Januari</asp:ListItem>
                    <asp:ListItem Value="02">Februari</asp:ListItem>
                    <asp:ListItem Value="03">Maret</asp:ListItem>
                    <asp:ListItem Value="04">April</asp:ListItem>
                    <asp:ListItem Value="05">Mei</asp:ListItem>
                    <asp:ListItem Value="06">Juni</asp:ListItem>
                    <asp:ListItem Value="07">Juli</asp:ListItem>
                    <asp:ListItem Value="08">Agustus</asp:ListItem>
                    <asp:ListItem Value="09">September</asp:ListItem>
                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                    <asp:ListItem Value="12">Desember</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" />
            </td>
            <td>
                &nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 82px; height: 19px;">
                &nbsp;
            </td>
            <td style="width: 158px; height: 19px;">
                &nbsp;
            </td>
            <td style="width: 78px; height: 19px;">
            </td>
            <td style="width: 151px; height: 19px;">
                &nbsp;
            </td>
            <td style="height: 19px" align="right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" Wrap="False"
                    Height="400px">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                PT. BANGUNPERKASA ADHITAMASENTRA
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                Form. No. : PIC/K/PS/12/03/R9
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                PEMANTAUAN STOCK
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="center">
                                periode
                                <asp:Label ID="LblPeriode" runat="server" Visible="False"></asp:Label>
                                &nbsp;:
                                <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow">
                        </div>
                        <div style="height:300px; overflow:auto" runat="server"  id="DivMainContent">
                            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">
                                <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />
                            </asp:GridView>
                        </div>
                        <div id="DivFooterRow" style="overflow: hidden">
                        </div>
                    </div>
                    <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: xx-small">PROSENTASE = ((HARI KERJA X JML ITEM PRODUK 
                            STANDARD) - JLM KALI PRODUK DIBAWAH MINIMUM STOCK) / (HARI KERJA X JML ITEM 
                            PRODUK STANDARD)</td>
                    </tr>
                        <tr>
                            <td style="font-size: xx-small">
                                ((
                                <asp:Label ID="Label3" runat="server" Text="0"></asp:Label>
                                &nbsp;X
                                <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
                                &nbsp;) -
                                <asp:Label ID="Label5" runat="server" Text="0"></asp:Label>
                                &nbsp;) / (
                                <asp:Label ID="Label6" runat="server" Text="0"></asp:Label>
                                &nbsp;X
                                <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                                &nbsp;) =
                                <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                                &nbsp;/
                                <asp:Label ID="Label9" runat="server" Text="0"></asp:Label>
                                &nbsp;=
                                <asp:Label ID="Label10" runat="server" Text="0"></asp:Label>
                                &nbsp;%</td>
                        </tr>
                        <tr>
                            <td style="font-size: xx-small">
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
