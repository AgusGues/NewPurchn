<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rekapdefectiso_New.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.Rekapdefectiso_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" runat="server" class="table-responsive" style="width:100%" >
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td colspan="5">
                REKAP DEFECT ISO
            </td>
        </tr>
        <tr bgcolor="#CCCCCC">
            <td>
                
                <asp:Panel ID="PanelBln" runat="server">
                Bulan
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                    Width="132px" AutoPostBack="True">
                    <%--<asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>--%>
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
                &nbsp;&nbsp; Tahun
                <asp:DropDownList ID="ddTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTahun_SelectedIndexChanged">
                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                </asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="PanelHari" runat="server" Visible="False">
                    dr Tgl.&nbsp;<asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" 
                        Height="21px" OnTextChanged="txtdrtanggal_TextChanged" Width="151px"></asp:TextBox><cc1:CalendarExtender 
                        ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" 
                        TargetControlID="txtdrtanggal">
                    </cc1:CalendarExtender>
                    &nbsp;sd Tgl.<asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" 
                        Height="21px" OnTextChanged="txtdrtanggal_TextChanged" Width="151px"></asp:TextBox><cc1:CalendarExtender 
                        ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy" 
                        TargetControlID="txtsdtanggal">
                    </cc1:CalendarExtender>
                </asp:Panel>
            </td>
            <td>
                
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
                    Text="Preview" />
            </td>
            <td align="center">
                <asp:RadioButton ID="RBBulanan" runat="server" GroupName="b" Text="Bulanan" 
                    AutoPostBack="True" oncheckedchanged="RBBulanan_CheckedChanged" 
                    Checked="True" />
                <asp:RadioButton ID="RBHarian" runat="server" GroupName="b" Text="Harian" 
                    AutoPostBack="True" oncheckedchanged="RBHarian_CheckedChanged" />
            </td>
            <td align="center">
                <asp:RadioButton ID="RBKubik" runat="server" GroupName="a" 
                    Text="Meter Kubik" AutoPostBack="True" Checked="True" 
                    oncheckedchanged="RBLembar_CheckedChanged" />
                <asp:RadioButton ID="RBLembar" runat="server" GroupName="a" Text="Lembar" 
                    AutoPostBack="True" oncheckedchanged="RBLembar_CheckedChanged" />
            </td>
            <td align="right">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCFFFF" Wrap="False"
                    Height="500px" HorizontalAlign="Center" ScrollBars="Vertical">
                    REKAP DEFECT
                    <br />
                    Periode &nbsp;:
                    <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LblTgl2" runat="server">Satuan Lembar</asp:Label>
                    <br />
                    <br />
                    <div id="DivRoot" align="left">
                            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                Style="margin-right: 0px" Width="98%" OnRowDataBound="GrdDynamic_RowDataBound">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Names="tahoma"
                                    Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            
                            <asp:GridView ID="GrdDynamic2" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated2" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            
                            <asp:GridView ID="GrdDynamic3" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated3" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:GridView ID="GrdDynamic4" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated4" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:GridView ID="GrdDynamic5" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated5" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:GridView ID="GrdDynamic6" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated6" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:GridView ID="GrdDynamic7" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated7" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:GridView ID="GrdDynamic8" runat="server" AutoGenerateColumns="False" 
                                CaptionAlign="Left" HorizontalAlign="Center" 
                                OnRowCreated="grvMergeHeader_RowCreated8" 
                                OnRowDataBound="GrdDynamic_RowDataBound" PageSize="20" 
                                Style="margin-right: 0px" Width="98%">
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:Panel ID="Panel2" runat="server" Font-Size="X-Small">
                                <table style="width: 100%; font-size: x-small;">
                                    <tr>
                                        <td style="height: 19px" colspan="8">
                                            ,
                                            <asp:Label ID="LblTgl6" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px" colspan="2">
                                            Dibuat :
                                        </td>
                                        <td colspan="4" style="height: 19px">
                                            Disetujui :
                                        </td>
                                        <td colspan="2" style="height: 19px" width="20%">
                                            &nbsp; Mengetahui :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )</td>
                                        <td colspan="4">
                                            (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )
                                        </td>
                                        <td colspan="2">
                                            &nbsp; (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
        </div>
</asp:Content>
