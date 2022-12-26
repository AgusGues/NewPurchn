<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapRMM.aspx.cs" Inherits="GRCweb1.Modul.RMM.RekapRMM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        label{font-size:12px;}
    </style>
    <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <script type="text/javascript">
        function updateDO(id) {
            window.showModalDialog("../../ModalDialog/RMMEdit.aspx?p=" + id, "RMM Update", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
        }
</script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP RMM</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content" style="background:#fff;">
                                <table width="100%" style="border-collapse: collapse; margin-top: 10px">
                                    <tr>
                                        <td></td>
                                        <td><asp:RadioButton ID="RBSemesteran" runat="server" AutoPostBack="True" 
                                                              Checked="True" Font-Size="X-Small" GroupName="a" 
                                                              OnCheckedChanged="RBSemeteran_CheckedChanged" Text="Semester" />
                                                          &nbsp;<asp:RadioButton ID="RBBulan" runat="server" AutoPostBack="True" 
                                                              Font-Size="X-Small" GroupName="a" oncheckedchanged="RBBulanan_CheckedChanged" 
                                                              Text="Bulan" /></td>
                                        <td></td>
                                    </tr>
                                    <asp:Panel ID="Panel3" runat="server" Width="280px">
                                    <tr>
                                        <td style="width: 10%; padding-left: 10px;">
                                            Periode
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlSemester" runat="server" OnSelectedIndexChanged="ddlSemester_Change"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="1">Semester I</asp:ListItem>
                                                <asp:ListItem Value="2">Semester II</asp:ListItem>
                                            </asp:DropDownList>
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
                                     <asp:Panel ID="Panel1" runat="server" Visible="False" > 
                                     <tr>
                                        <td style="padding-left: 10px"> Periode</td>
                                        <td><asp:DropDownList ID="ddlBulan" runat="server"> </asp:DropDownList>&nbsp;&nbsp;<asp:DropDownList ID="ddlTahun2" runat="server" OnSelectedIndexChanged="ddlTahun_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList></td>
                                        <td></td>
                                     </tr> 
                                     </asp:Panel>
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
                                  <div class="contentlist" style="height: 360px" id="div2" runat="server">
                                    <%--<div id="lst" runat="server">--%>
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                            <tr class="tbHeader">
                                                <th class="kotak" rowspan="2" style="width: 1%"> No.</th>
                                                <th class="kotak" rowspan="2" style="width: 5%">No RMM</th>   
                                                <th class="kotak" rowspan="2" style="width: 12%">Sasaran Mutu</th> 
                                                <th class="kotak" rowspan="2" style="width: 20%">Tindakan Perbaikan Pencapaian Sasaran Mutu</th>
                                                <th class="kotak" colspan="3">Tanggal</th>
                                                <th class="kotak" rowspan="2" style="width: 3%">Status</th>    
                                                <th class="kotak" rowspan="2" style="width: 7%">Keterangan</th>
                                                <th class="kotak" style="width: 2%" rowspan="2"></th>
                                            </tr>
                                            <tr class="tbHeader" id="hd2">
                                                <th class="kotak tengah" style="width: 3%">Plan Awal</th>
                                                <th class="kotak" style="width: 3%">Actual Selesai</th>    
                                                <%--<th class="kotak" style="width:5%">Revisi</th>
                                               <th class="kotak" style="width:5%">Actual</th>--%>
                                                <th class="kotak" style="width: 3%">Verifikasi</th>
                                            </tr>        
                                            <tbody>
                                                <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="Line3 baris">
                                                            <td colspan="10"><%# Container.ItemIndex+1 %>.&nbsp;<%# Eval("Departemen") %></td>
                                                        </tr>
                                                        <asp:Repeater ID="ListRMM1" runat="server" OnItemDataBound="ListRMM1_DataBound" OnItemCommand="ListRMM1_Command">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak tengah"> <%# Container.ItemIndex+1 %> </td>
                                                                    <td class="kotak"><%# Eval("RMM_No") %></td>
                                                                    <td class="kotak"><%# Eval("SDept") %></td>
                                                                    <td class="kotak"><%# Eval("Aktivitas") %> </td>
                                                                    <td class="kotak tengah"> <%# Eval("Minggu")%>/<%# Eval("Bulan1")%>/<%# Eval("Year")%></td>
                                                                    <td class="kotak tengah"><%# Eval("Aktual_Selesai","{0:d}")  %></td>   
                                                                    <td class="kotak tengah"><%# Eval("TglVerifikasi","{0:d}")  %></td>
                                                                    <td class="kotak tengah"><asp:Label ID="slvd" runat="server"></asp:Label></td>    
                                                                    <td class="kotak tengah"><%# Eval("Ket")  %></td>
                                                                    <td class="kotak tengah">
                                                                    <asp:ImageButton ID="lstEdit" ImageUrl="~/images/clipboard_16.png" ToolTip="Edit RMM" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" ToolTip="Hapus RMM" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
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
