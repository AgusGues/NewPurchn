<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM_PelumasPlat.aspx.cs" Inherits="GRCweb1.Modul.SarMut.BM_PelumasPlat" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table-responsive" style="width: 100%">
    <table style="width: 100%; font-size: x-small;" bgcolor="#bfbebc">
        <tr>
            <td colspan="2">
                PEMANTAUAN PELUMAS PLAT DESTACKING
            </td>
        </tr>
        <tr>
            <td>
                Bulan
                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                    Width="132px">
                    <%--<asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>--%>
                    <asp:ListItem Value="1">Januari</asp:ListItem>
                    <asp:ListItem Value="2">Februari</asp:ListItem>
                    <asp:ListItem Value="3">Maret</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">Mei</asp:ListItem>
                    <asp:ListItem Value="6">Juni</asp:ListItem>
                    <asp:ListItem Value="7">Juli</asp:ListItem>
                    <asp:ListItem Value="8">Agustus</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                    <asp:ListItem Value="12">Desember</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp; Tahun
                <asp:DropDownList ID="ddTahun" runat="server">
                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Line
                <asp:DropDownList ID="ddlLine" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" />
                <asp:Button ID="btnExport" runat="server" OnClick="ExportToExcel_Click" Text="Export To Excel" />
            </td>
            
            <td align="right">
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtdrtanggal_TextChanged" Visible="false"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                    OnTextChanged="txtdrtanggal_TextChanged" Visible="false"></asp:TextBox>
                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal">
                </cc1:CalendarExtender>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Visible="false">Export 
                        To Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div runat="server" id="dataTable">
                    <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCFFFF" Wrap="False"
                        Height="500px" HorizontalAlign="Center">
                        LEMBAR PEMANTAUAN PELUMASAN PLAT DESTAKING
                        <br />
                        Periode &nbsp;:&nbsp;<asp:Label ID="LblTgl1" runat="server"></asp:Label>
                        <br />
                        
                                    Total Akumulasi &nbsp; L1 - L6 &nbsp;:
                                    <asp:Label ID="lbltotalakumulasi" runat="server"></asp:Label>&nbsp;Ltr/M3
                        
                        <table style="width: 100%; font-size: x-small;">
                            <tr>
                                <td align="Left">
                                    LINE&nbsp;:&nbsp;<asp:Label ID="LblLine" runat="server"></asp:Label>
                                   
                                </td>
                                
                                <td align="right">
                                    Target &nbsp;:
                                    <asp:Label ID="LblTarget" runat="server"></asp:Label>&nbsp;Ltr/M3
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="1">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak tengah" style="width: 10%">
                                                    Tgl.Produksi
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Pemakaian BCO (Liter)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Pemakaian Solar (Liter)
                                                </th>
                                                 <th class="kotak tengah" style="width: 15%">
                                                    Output Produksi NonPress (Meter3)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Output Produksi Press (Meter3)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Liter/Meter3
                                                </th>
                                                <th class="kotak tengah" style="width: 25%">
                                                    Keterangan
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="DataTb" runat="server" style="font-family: Calibri">
                                        
                                            <asp:Repeater ID="lstPlat" runat="server" OnItemDataBound="lstPlat_DataBound" OnItemCommand="lstPlat_ItemCommand">
                                                <ItemTemplate>
                                                    <tr id="lst2" runat="server" class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <%--<%# Eval("Tanggal", "{0:d}")%>--%>
                                                            <asp:Label ID="lblTgl" runat="server" Text='<%# Eval("Tanggal", "{0:d}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("QtyBco", "{0:N2}")%>--%>
                                                            <asp:Label ID="lblQtyBco" runat="server" Text='<%# Eval("QtyBco", "{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("QtySolar", "{0:N2}")%>--%>
                                                            <asp:Label ID="lblQtySolar" runat="server" Text='<%# Eval("QtySolar", "{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("OutPutM3","{0:N2}")%>--%>
                                                            <asp:Label ID="lblOutputM3NP" runat="server" Text='<%# Eval("OutPutM3NP","{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("OutPutM3","{0:N2}")%>--%>
                                                            <asp:Label ID="lblOutPutM3" runat="server" Text='<%# Eval("OutPutM3","{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="lblLtrMtr" runat="server" Text='<%# Eval("LtrMtr","{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="lblHarga" runat="server" Text='<%# Eval("Harga","{0:N0}")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="#CCFFFF" Wrap="False"
                        Height="500px" HorizontalAlign="Center">
                        LEMBAR PEMANTAUAN PELUMASAN PLAT DESTAKING
                        <br />
                        Periode &nbsp;:&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                        <br />
                        
                                    Total Akumulasi &nbsp; L1 - L6 &nbsp;:
                                    <asp:Label ID="Label2" runat="server"></asp:Label>&nbsp;Ltr/M3
                        
                        <table style="width: 100%; font-size: x-small;">
                            <tr>
                                <td align="Left">
                                    LINE&nbsp;:&nbsp;<asp:Label ID="Label3" runat="server"></asp:Label>
                                   
                                </td>
                                
                                <td align="right">
                                    Target &nbsp;:
                                    <asp:Label ID="Label4" runat="server"></asp:Label>&nbsp;Ltr/M3
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="1">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak tengah" style="width: 10%">
                                                    Tgl.Produksi
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Pemakaian BCO (Liter)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Pemakaian Solar (Liter)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Output Produksi (Meter3)
                                                </th>
                                                <th class="kotak tengah" style="width: 15%">
                                                    Liter/Meter3
                                                </th>
                                                <th class="kotak tengah" style="width: 25%">
                                                    Keterangan
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody1" runat="server" style="font-family: Calibri">
                                        
                                            <asp:Repeater ID="lstPlatjmbg" runat="server" OnItemDataBound="lstPlatjmbg_DataBound" OnItemCommand="lstPlatjmbg_ItemCommand">
                                                <ItemTemplate>
                                                    <tr id="lst2" runat="server" class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <%--<%# Eval("Tanggal", "{0:d}")%>--%>
                                                            <asp:Label ID="lblTgl" runat="server" Text='<%# Eval("Tanggal", "{0:d}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("QtyBco", "{0:N2}")%>--%>
                                                            <asp:Label ID="lblQtyBco" runat="server" Text='<%# Eval("QtyBco", "{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("QtySolar", "{0:N2}")%>--%>
                                                            <asp:Label ID="lblQtySolar" runat="server" Text='<%# Eval("QtySolar", "{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%--<%# Eval("OutPutM3","{0:N2}")%>--%>
                                                            <asp:Label ID="lblOutPutM3" runat="server" Text='<%# Eval("OutPutM3","{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="lblLtrMtr" runat="server" Text='<%# Eval("LtrMtr","{0:N2}")%>'></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="lblHarga" runat="server" Text='<%# Eval("Harga","{0:N0}")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
