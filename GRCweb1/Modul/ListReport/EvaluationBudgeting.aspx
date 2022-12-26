<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluationBudgeting.aspx.cs" Inherits="GRCweb1.Modul.ListReport.EvaluationBudgeting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
        <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <div id="Div1" runat="server" class="table-responsive" style="width: 100%"  >
             <table style="table-layout: fixed; height: 100%" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;PEMANTAUAN SPB SPARE PART </strong>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" valign="top">
                                <div class="content">
                                <!-- ketika width pakai % jangan di campur pakai px ya; tampilannya jadi jelek
                                     biar enak maintenance nya bari 1 aja yang di kasih parameter width di td nya-->
                                    <table style="width: 100%; border-collapse:collapse; font-size:x-small">
                                        <tr>
                                            <td class="style4" style="width:10%"></td>
                                            <td style="width:20%">
                                                &nbsp;
                                            </td>
                                            <td style="width:20%">
                                                &nbsp;
                                            </td>
                                            <td style="width:30%"><asp:TextBox ID="txtSearch" runat="server" Visible="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp; Periode</td>
                                            <td>
                                                <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td> </td>
                                            <td> </td>
                                        </tr>
                                        <tr>
                                            <td >&nbsp; Departement</td>
                                            <td ><asp:DropDownList ID="ddlDept" runat="server"></asp:DropDownList>
                                            </td>
                                            <td colspan="2"><asp:CheckBox ID="chkSls" runat="server" Text="Only Over Budget" />&nbsp;
                                                <asp:CheckBox ID="chkNonB" runat="server" Text="Only Non Budget" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="2">
                                                <asp:Button ID="btnPreview" runat="server" onclick="btnPreview_Click" Text="Preview" />
                                                <asp:Button ID="btnExport" runat="server" onclick="btnExport_Click" Text="Export to Excel" />
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height: 360px;">
                                        <div id="lst" runat="server">
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                            <tr class="tbHeader">
                                                 <th class="kotak" style="width:2%">ID</th>
                                                <th class="kotak" style="width:15%">Dept</th>
                                                <th class="kotak" style="width:10%">ItemCode</th>
                                                <th class="kotak" style="width:25%">ItemName</th>
                                                <th class="kotak" style="width:5%">Satuan</th>
                                                <th class="kotak" style="width:5%">BudgetQty</th>
                                                <th class="kotak" style="width:5%">Budget/Bulan</th>
                                                <th class="kotak" style="width:5%">Budget/Tahun</th>
                                                <th class="kotak" style="width:10%">Kategori</th>
                                                <th class="kotak" style="width:8%">Pemakaian</th>
                                                <th class="kotak" style="width:8%">Selisih</th>
                                                <th class="kotak" style="width:15%">Keterangan</th>
                                            </tr>
                                            <tbody><!-- bikin baru ga ada yang cocock ya-->
                                                <asp:Repeater ID="EvBudget1" runat="server" OnItemDataBound="EvBudget1_DataBound" OnItemCommand="EvBudget1_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak " nowrap="nowrap"><%# Eval("DeptName")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                            <td class="kotak " style="white-space:inherit"><%# Eval("ItemName") %></td>
                                                            <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                            <td class="kotak angka"><%# Eval("MaxQty","{0:N1}")%></td>
                                                            <td class="kotak angka"><%# Eval("BBulan", "{0:N1}")%></td>
                                                            <td class="kotak angka"><%# Eval("BTahun", "{0:N1}")%></td>
                                                            <td class="kotak "><%# Eval("Kategori") %></td>
                                                            <td class="kotak angka"><%# Eval("Pemakaian", "{0:N1}")%></td>
                                                            <td class="kotak angka"><asp:Label ID="sls" runat="server" Text='<%# Eval("Selisih", "{0:N1}")%>'></asp:Label></td>
                                                            <td class="kotak "><%# Eval("Keterangan") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <%--<AlternatingItemTemplate>
                                                    <tr class="OddRows baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                             <td class="kotak tengah"><%# Eval("Dept")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                            <td class="kotak tengah"><%# Eval("ItemName") %></td>
                                                            <%--<td class="kotak"><%# Eval("Satuan") %></td>
                                                            <td class="kotak tengah"><%# Eval("BudgetQty") %></td>
                                                            <td class="kotak tengah"><%# Eval("BBulan") %></td>
                                                            <td class="kotak tengah"><%# Eval("BTahun") %></td>
                                                            <td class="kotak tengah"><%# Eval("Kategori") %></td>
                                                            <td class="kotak tengah"><%# Eval("Pemakaian") %></td>
                                                            <td class="kotak tengah"><%# Eval("Selisih") %></td>
                                                            <td class="kotak tengah"><%# Eval("Keterangan") %></td>
                                                        </tr>
                                                    </AlternatingItemTemplate>--%>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
             </div>
</asp:Content>
