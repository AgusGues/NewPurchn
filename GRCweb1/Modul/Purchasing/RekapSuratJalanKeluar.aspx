<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapSuratJalanKeluar.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.RekapSuratJalanKeluar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td style="width: 100%; height: 49px">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width: 50%; padding-left: 10px">
                                    <strong>Rekap Surat Jalan Keluar</strong>
                                </td>
                                <td style="width: 50%; padding-right: 5px">&nbsp;<asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True"
                                    BorderStyle="Groove" Width="100px"></asp:TextBox><cc1:calendarextender
                                        id="CalendarExtender1" runat="server" format="dd-MMM-yyyy"
                                        targetcontrolid="txtdrtanggal">
                                    </cc1:calendarextender>
                                    &nbsp;s/d
                                    <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True"
                                        BorderStyle="Groove" Width="100px"></asp:TextBox>
                                    <cc1:calendarextender id="txtsdtanggal_CalendarExtender" runat="server"
                                        format="dd-MMM-yyyy" targetcontrolid="txtsdtanggal">
                                    </cc1:calendarextender>
                                    &nbsp;<asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click"
                                        Text="Preview" />
                                    &nbsp;
                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                        Text="Export To Execl" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Auto">
                            <strong>Rekap Surat Jalan Keluar</strong>
                            <br />
                            Periode :
                            <asp:Label ID="LblDr" runat="server"></asp:Label>
                            &nbsp;s/d
                            <asp:Label ID="LblSd" runat="server"></asp:Label>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                PageSize="25" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Nomor" HeaderText="No" />
                                    <asp:BoundField DataField="TglSJ" DataFormatString="{0:d}"
                                        HeaderText="Tanggal" />
                                    <asp:BoundField DataField="NoSJ" HeaderText="No Surat Jalan" />
                                    <asp:BoundField DataField="ItemCode" HeaderText="Kode Brg" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Nama Brg" />
                                    <asp:BoundField DataField="Tujuan" HeaderText="Tujuan" />
                                    <asp:BoundField DataField="Jumlah" HeaderText="Qty" />
                                    <asp:BoundField DataField="UOM" HeaderText="Satuan" />
                                    <asp:BoundField DataField="NoPolisi" HeaderText="No Polisi" />
                                    <asp:BoundField DataField="Ket" HeaderText="Keterangan" />
                                </Columns>
                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                    BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                    ForeColor="Gold" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
