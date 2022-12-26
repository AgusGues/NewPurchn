<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputKertasQa.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.InputKertasQa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td class="tdHeader">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <strong>&nbsp;INPUT KERTAS</strong>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:Button ID="btnList" runat="server" Text="List Kertas" OnClick="btnList_Click"  />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Lokasi Plant
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="60%" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%; height: 21px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 10%; height: 21px;">
                                            Tanggal
                                        </td>
                                        <td style="width: 23%; height: 21px;">
                                            <asp:TextBox ID="txtTgl" runat="server" BorderStyle="Groove" Width="192px"></asp:TextBox>
                                        </td>
                                        <td style="width: 50%; height: 21px;" valign="top">
                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTgl" Format="dd-MMM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Jenis Inputan
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlJInputan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJInputan_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-- Pilih --</asp:ListItem>
                                                <asp:ListItem Value="1">Tambah Baru</asp:ListItem>
                                                <%-- <asp:ListItem Value="2">Perubahan Jenis</asp:ListItem>
                                                <asp:ListItem Value="3">Penghapusan</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:Label ID="idXtxt" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="PanelTambahBaru" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Sumber Kertas
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSumberKertas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlJSumberKertas_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Pilih --</asp:ListItem> 
                                                    <asp:ListItem Value="1">Lokal</asp:ListItem>
                                                    <asp:ListItem Value="2">Import</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Jenis Kertas
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJenisKertas" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddlNamaBarang_SelectedIndexChanged">
                                                </asp:DropDownList><asp:Label ID="nKertas" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ItemCode
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" BorderStyle="Groove" Width="192px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                Nama / Merk Kertas
                                            </td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtMerkKertas" runat="server" BorderStyle="Groove" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                Solid Content (ml)
                                            </td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtSolid" runat="server" BorderStyle="Groove" Width="50%"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                Freeness (CSF)
                                            </td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtFreeness" runat="server" BorderStyle="Groove" Width="50%"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                Asal Kertas
                                            </td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtAsalKertas" runat="server" BorderStyle="Groove" Width="70%"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%" valign="top">
                                                Keterangan
                                            </td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtKet" runat="server" Width="100%" TextMode="MultiLine" Height="60px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50%" valign="top">
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                                <div class="contentlist" style="height: 320px">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th style="width: 2%">
                                                    No.
                                                </th>
                                                <th style="width: 3%">
                                                    Sumber Kertas
                                                </th>
                                                <th style="width: 10%">
                                                    Jenis Kertas
                                                </th>
                                                <th style="width: 5%">
                                                    ItemCode
                                                </th>
                                                <th style="width: 8%">
                                                    Nama/ Merk Kertas
                                                </th>
                                                <th style="width: 2%">
                                                    Solid Content (ml)
                                                </th>
                                                <th style="width: 2%">
                                                    Freeness (CSF)
                                                </th>
                                                <th style="width: 5%">
                                                   Asal Kertas
                                                </th>
                                                <th style="width: 5%">
                                                   Keterangan
                                                </th>
                                                <th style="width: 3%">
                                                   Tanggal
                                                </th>
                                                 <th style="width: 5%">
                                                   Dibuat Oleh / Plant
                                                </th>
                                                <th style="width: 2%">
                                                   
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                         <asp:Repeater ID="lstQaKertas" runat="server" OnItemCommand="lstQaKertas_Command" OnItemDataBound="lstQaKertas_Databound">
                                                <ItemTemplate> 
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1  %></td>
                                                        <td class="kotak tengah"><%# Eval("NmSumberKertas") %></td>
                                                        <td class="kotak tengah"><%# Eval("NmKertas") %></td>
                                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        <td class="kotak tengah"><%# Eval("MerkKertas") %></td>
                                                        <td class="kotak tengah"><%# Eval("SolidContent","{0:N0}") %></td>
                                                        <td class="kotak tengah"><%# Eval("Freeness", "{0:N0}")%></td>
                                                        <td class="kotak tengah"><%# Eval("AsalKertas") %></td>
                                                        <td class="kotak tengah"><%# Eval("Ket") %></td>
                                                        <td class="kotak tengah"><%# Eval("Tanggal", "{0: dd/MM/yyyy}")%></td>
                                                        <td class="kotak"><%# Eval("NamaPlant")%></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandName="edit" CommandArgument='<%# Eval("ID") %>' ToolTip="Edit Data" />
                                                            <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandName="dele" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />
                                                            <%-- <asp:ImageButton ID="dels" runat="server" ImageUrl="~/images/Delete.png" CommandName="delet" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />--%>
                                                        </td>
                                                    </tr>
                                               </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
