<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapUPDJobdesk.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.LapUPDJobdesk" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" width="100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;Laporan UPD Job Desk</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content" style="background:#fff;">
                                <table width="100%" style="border-collapse: collapse; margin-top: 10px">
                                    <asp:Panel ID="Panel3" runat="server" Width="280px">
                                    <tr>
                                        <td style="width: 10%; padding-left: 10px;">
                                            Periode
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px"> Department</td>
                                        <td><asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>   
                                        <td>&nbsp;</td>
                                     </tr>   
                                     </asp:Panel>   
                                     <%--<asp:Panel ID="Panel1" runat="server" Visible="False" > 
                                     <tr>
                                        <td style="padding-left: 10px"> Periode</td>
                                        <td><asp:DropDownList ID="ddlBulan" runat="server"> </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="ddlTahun2" runat="server" OnSelectedIndexChanged="ddlTahun_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList></td>
                                        <td></td>
                                     </tr> 
                                     </asp:Panel>--%>
                                     <tr>
                                        <td colspan="3"> &nbsp;</td>
                                     </tr>      
                                     <tr>
                                        <td>&nbsp;</td>
                                        <td><asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Execl" OnClick="btnExport_Click" />    
                                        </td>
                                        <td align="right"><%-- Filter By Month : &nbsp; <asp:DropDownList ID="ddlBulan" runat="server"> </asp:DropDownList>&nbsp; <asp:Button ID="btnPreview2" runat="server" Text="Preview" OnClick="btnPreview2_Click" />--%></td>
                                      </tr>      
                                  </table>
                                  <hr />          
                                  <div class="contentlist" style="height: 360px" id="div2" runat="server">
                                    <%--<div id="lst" runat="server">--%>
                                        <table id="jd" style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                            <tr class="tbHeader">
                                                <th class="kotak"> No.</th>
                                                <th class="kotak">Id</th>
                                                <th class="kotak">Jabatan</th>
                                                <th class="kotak">Atasan</th>
                                                <th class="kotak">Status</th>
                                                <th class="kotak">Revisi Ke</th>
                                                <th class="kotak">Tanggal Susun</th>   
                                                <%--<th class="kotak">Tanggal Berlaku</th>--%>
                                                <th class="kotak">Alasan Tidak Ikut Revisi</th>
                                                <th class="kotak"></th>
                                            </tr>
                                            <%--<tr class="tbHeader" id="hd2">
                                                <th class="kotak tengah" style="width: 3%">Plan Awal</th>
                                                <th class="kotak" style="width: 3%">Actual Selesai</th>    
                                                <%--<th class="kotak" style="width:5%">Revisi</th>
                                               <th class="kotak" style="width:5%">Actual</th>--%>
                                                <%--<th class="kotak" style="width: 3%">Verifikasi</th>
                                            </tr>--%>      
                                            <tbody>
                                                <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="Line3 baris">
                                                            <td colspan="11"><%# Container.ItemIndex+1 %>.&nbsp;<%# Eval("AliasDept")%></td>
                                                        </tr>
                                                        <asp:Repeater ID="ListJobDesk" runat="server" OnItemDataBound="ListJobDesk_DataBound" OnItemCommand="ListJobDesk_Command">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak tengah"> <%# Container.ItemIndex+1 %> </td> 
                                                                    <td class="kotak"><asp:Label ID="id" runat="server" Text='<%# Eval("ID") %>'></asp:Label></td>
                                                                    <td class="kotak"><%# Eval("BagianName")%></td>
                                                                    <td class="kotak"><%# Eval("Atasan")%></td>
                                                                    <td class="kotak tengah"><asp:Label ID="slvd" runat="server"></asp:Label></td>   
                                                                    <td class="kotak"><%# Eval("Revisi")%></td>
                                                                    <td class="kotak tengah"><%# Eval("TglSusun", "{0:d}")%></td>
                                                                    <td class="kotak"><%# Eval("AlasanTidakIkutRevisi")%></td> 
                                                                    <td class="kotak tengah">
                                                                    <%--<asp:ImageButton ID="Cetak" ImageUrl="~/images/clipboard_16.png" ToolTip="Preview" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="cetak" />--%>
                                                                    <asp:ImageButton ID="Add" ImageUrl="~/images/clipboard_16.png" ToolTip="Lihat" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Add" />
                                                                    <%--<asp:Button id="btnPrint" runat="server" Text="Cetak" CommandName="cetak" CommandArgument='<%# Eval("ID") %>' />
                                                                    <%--<asp:ImageButton ID="lstEdit" ImageUrl="~/images/clipboard_16.png" ToolTip="Edit RMM" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" ToolTip="Hapus RMM" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>  
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    <%--</div>--%>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
