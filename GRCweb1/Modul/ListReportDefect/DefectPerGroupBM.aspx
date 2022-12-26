<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefectPerGroupBM.aspx.cs" Inherits="GRCweb1.Modul.ListReportDefect.DefectPerGroupBM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td colspan="3">
                <asp:RadioButton ID="RBTglInput" runat="server" GroupName="a" Text="Tanggal  Input" />
                <asp:RadioButton ID="RBTglProduksi" runat="server" GroupName="a" Text="Tanggal  Produksi" />
                <asp:RadioButton ID="RBTglPotong" runat="server" Checked="True" GroupName="a" Text="Tanggal Potong" />
            </td>
        </tr>
        <tr>
            <td style="width: 82px">
                Dari Tanggal
            </td>
            <td>
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" 
                    Width="151px" ontextchanged="txtdrtanggal_TextChanged"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
                s/d Tanggal
                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" 
                    Width="151px" ontextchanged="txtsdtanggal_TextChanged"></asp:TextBox>
                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal">
                </cc1:CalendarExtender>
            &nbsp;&nbsp; Jumlah decimal untuk kolom Meter Kubik =&nbsp;
                <asp:TextBox ID="txtDecimal" runat="server" Width="32px">1</asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 19px;" colspan="2">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
                    Text="Preview Per Group" />
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" 
                    Text="Preview Per Line" />
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
            &nbsp;</td>
            <td style="height: 19px" align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" ScrollBars="Vertical"
                    Wrap="False" Height="436px">
                    Rekap Defect BM
                    <asp:Label ID="LbPer" runat="server"></asp:Label>
                    , periode
                    <asp:Label ID="LblPeriode" runat="server"></asp:Label>
                    &nbsp;:
                    <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                    &nbsp;s/d&nbsp;
                    <asp:Label ID="LblTgl2" runat="server"></asp:Label>
                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" PageSize="15"
                        Width="100%">
                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                    &nbsp;
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 95px">
                                Total BP :</td>
                            <td class="style4" style="width: 217px">
                                <asp:Label ID="LBP" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                &nbsp;Lembar</td>
                            <td>
                                <asp:Label ID="LBP0" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                &nbsp;Kubik</td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Total Potong :</td>
                            <td class="style4" style="width: 217px">
                                <asp:Label ID="LPotong" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                &nbsp;Lembar</td>
                            <td>
                                <asp:Label ID="LPotong0" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                &nbsp;Kubik</td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                &nbsp;</td>
                            <td class="style4" style="width: 217px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
