<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSuratJalanKeluar.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListSuratJalanKeluar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
            <table style="width:100%">
               <tbody>
                <tr>
                    <td style="width: 100%; height: 49px">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width: 50%; padding-left:10px">
                                    <strong>&nbsp;&bull;&nbsp;List Surat Jalan Keluar</strong>
                                </td>
                                <td style="width:50%; padding-right:5px">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Form Surat Jalan" OnClick="btnUpdate_ServerClick" />
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                        <asp:ListItem Value="NoSJ">No Surat Jalan</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Cari" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="content">
                                <hr />
                           <div class="contentlist" style="height:430px">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnRowCommand="GridView1_RowCommand" 
                                    OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                                    <Columns>
                                        <asp:BoundField DataField="TglSJ" HeaderText="Tanggal" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="NoSJ" HeaderText="No Surat Jalan"  />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Brg" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Nama Brg" />
                                        <asp:BoundField DataField="Tujuan" HeaderText="Tujuan" />
                                        <asp:BoundField DataField="Jumlah" HeaderText="Qty" />
                                        <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                        <asp:BoundField DataField="NoPolisi" HeaderText="No Polisi" />
                                        <asp:BoundField DataField="Ket" HeaderText="Keterangan" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>  
            </tbody>
          </table>
          </div>
</asp:Content>
