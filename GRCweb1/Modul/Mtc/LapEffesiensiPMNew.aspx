<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapEffesiensiPMNew.aspx.cs" Inherits="GRCweb1.Modul.Mtc.LapEffesiensiPMNew" %>
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
                <table style="width:100%; height:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%; height:49px">
                            <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <td>&nbsp;&nbsp;&nbsp;LAPORAN EFFESIENSI PERAWATAN MESIN</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <div class="content">
                                <table style="margin-top:5px; border-collapse:collapse; width:100%; font-size:x-small">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:12%">Dari Tanggal</td>
                                        <td style="width:38%"><asp:TextBox ID="txtDari" runat="server" Width="50%"></asp:TextBox></td>
                                        <td style="width:40%">&nbsp;
                                            <cc1:CalendarExtender ID="ca1" runat="server" TargetControlID="txtDari" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Sampai Tanggal</td>
                                        <td><asp:TextBox ID="txtSampai" runat="server" Width="50%"></asp:TextBox></td>
                                        <td><cc1:CalendarExtender ID="ca2" runat="server" TargetControlID="txtSampai" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Department</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlDept" runat="server">
                                                <asp:ListItem Value="0">Maintenance</asp:ListItem>
                                                <asp:ListItem Value="4">Mekanik</asp:ListItem>
                                                <asp:ListItem Value="5">Electric</asp:ListItem>
                                                <asp:ListItem Value="18">Utility</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>View List</td>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbList_SelectedIndexChanged">
                                            <asp:ListItem Value="Rekap" Text="Rekap" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Detail" Text="Detail"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" 
                                                OnClick="btnExport_Click" Visible="False" />
                                        </td>
                                        <td class="angka"><span style="font-size:xx-small; color:#B0C4DE"><asp:Label ID="txtTime" runat="server" ></asp:Label></span>&nbsp;</td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height:340px" id="lst" runat="server"  >
                                    <div id="lstRekap" runat="server">
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                            <thead>
                                                <tr class="tengah">
                                                    <th class="kotak tbHeader" style="width:5%">No.</th>
                                                    <td class="kotak tbHeader" style="width:20%">Spare Part Group</td>
                                                    <td class="kotak tbHeader" style="width:10%">Value</td>
                                                    <td class="kotak tbHeader" style="width:5%">%</td>
                                                    <td class="" style="width:40%">&nbsp;</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="ListRekap" runat="server" OnItemDataBound="ListRekap_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak tengah"><%# Eval("Kode") %></td>
                                                            <td class="kotak"><%# Eval("GroupName") %></td>
                                                            <td class="kotak angka"><%# Eval("Total", "{0:N2}")%></td>
                                                            <td class="kotak tengah"><%# Eval("Prosen","{0:N2}") %></td>
                                                            <td style="background-color:Transparent">&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td class="kotak tengah"><%# Eval("Kode") %></td>
                                                            <td class="kotak"><%# Eval("GroupName") %></td>
                                                            <td class="kotak angka"><%# Eval("Total", "{0:N2}")%></td>
                                                            <td class="kotak tengah"><%# Eval("Prosen","{0:N2}") %></td>
                                                            <td style="background-color:Transparent">&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    
                                                </asp:Repeater>
                                                
                                            </tbody>
                                            <tfoot>
                                                <tr class="line3">
                                                    <td class="kotak angka " colspan="2">Total</td>
                                                    <td class="kotak angka"><asp:Label ID="txtTotals" runat="server"></asp:Label></td>
                                                    <td class="kotak angka">&nbsp;</td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                        <hr />
                                        
                                    </div>
                                    <div id="lstDetail" runat="server" visible="false">
                                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak" style="width:4%">No.</th>
                                                    <th class="kotak" style="width:10%">Tanggal</th>
                                                    <th class="kotak" style="width:10%">PakaiNo</th>
                                                    <th class="kotak" style="width:10%">ItemCode</th>
                                                    <th class="kotak" style="width:20%">ItemName</th>
                                                    <th class="kotak" style="width:5%">Unit</th>
                                                    <th class="kotak" style="width:8%">Quantity</th>
                                                    <th class="kotak" style="width:8%">Price</th>
                                                    <th class="kotak" style="width:8%">Total Price</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                             <asp:Repeater ID="lstSarmut" runat="server" OnItemDataBound="lstSarmutDataBound" OnItemCommand="lstSarmut_Command">
                                                <ItemTemplate>
                                                    <tr class="line3 baris">
                                                        <td class="kotak"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak" colspan="5"><asp:LinkButton ID="lnk" runat="server" Text='<%# Eval("GroupName") %>' CommandArgument='<%# Eval("ID") %>' CommandName="show"></asp:LinkButton></td>
                                                        <td class="kotak angka" colspan="3">
                                                            <%--<asp:ImageButton ID="btnShow" runat="server" CommandName="show" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/collapse.gif" />--%>
                                                        &nbsp;</td>
                                                    </tr>
                                                    <asp:Repeater ID="ListDetail" runat="server" Visible="true">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <td class="kotak angka"><%# Container.ItemIndex+1 %></td>
                                                                <td class="kotak tengah"><%# Eval("Tanggal") %></td>
                                                                <td class="kotak tengah"><%# Eval("PakaiNo") %></td>
                                                                <td class="kotak"><%# Eval("ItemCode") %></td>
                                                                <td class="kotak"><%# Eval("ItemName") %></td>
                                                                <td class="kotak"><%# Eval("Unit") %></td>
                                                                <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                                                <td class="kotak angka"><%# Eval("AvgPrice", "{0:N2}")%></td>
                                                                <td class="kotak angka"><%# Eval("TotalPrice", "{0:N2}")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    
                                                    </asp:Repeater>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak angka" colspan="6"><b><%# Eval("GroupName") %> Total </b></td>
                                                        <td class="kotak angka" colspan="3"><%# Eval("Total", "{0:N2}")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                             </asp:Repeater>
                                                
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
</asp:Content>
