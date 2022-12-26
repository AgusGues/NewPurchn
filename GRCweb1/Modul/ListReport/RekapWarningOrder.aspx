<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapWarningOrder.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapWarningOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
  // fix for deprecated method in Chrome / js untuk bantu view modal dialog
  if (!window.showModalDialog) {
     window.showModalDialog = function (arg1, arg2, arg3) {
        var w;
        var h;
        var resizable = "no";
        var scroll = "no";
        var status = "no";
        // get the modal specs
        var mdattrs = arg3.split(";");
        for (i = 0; i < mdattrs.length; i++) {
           var mdattr = mdattrs[i].split(":");
           var n = mdattr[0];
           var v = mdattr[1];
           if (n) { n = n.trim().toLowerCase(); }
           if (v) { v = v.trim().toLowerCase(); }
           if (n == "dialogheight") {
              h = v.replace("px", "");
           } else if (n == "dialogwidth") {
              w = v.replace("px", "");
           } else if (n == "resizable") {
              resizable = v;
           } else if (n == "scroll") {
              scroll = v;
           } else if (n == "status") {
              status = v;
           }
        }
        var left = window.screenX + (window.outerWidth / 2) - (w / 2);
        var top = window.screenY + (window.outerHeight / 2) - (h / 2);
        var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        targetWin.focus();
     };
  }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
        <div id="div1" runat="server">
            <table style="table-layout:fixed; width:100%;">
            <tr>
                <td colspan="5" style="height: 49px">
                <!--header tabel-->
                <table class="nbTableHeader" width="100%">
                  <tr>
                      <td style="width: 40%"><strong>&nbsp;REKAP WARNING ORDER</strong></td>
                      <td style="width: 60%"></td>
                  </tr>
                </table>
                <!-- end of header-->
               </td>
            </tr>
            <tr>
            <td colspan="5" valign="top" align="left">
                <div id="div2" style="background-color:Scrollbar;padding:10px; width:100%;">
                <table id="headerPO" width="100%" style="border-collapse:collapse;" visible="true" runat="server">
                    <tr>
                        <td width="15%" align="right">Per Tanggal &nbsp;</td>
                        <td width="20%"><asp:TextBox ID="fromDate" runat="server"></asp:TextBox></td>
                        <td width="5%"></td>
                        <td width="10%" align="right">
                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="fromDate" Format="dd/MM/yyyy" runat="server">
                        </cc1:CalendarExtender>
                        </td>
                        <td width="25%"></td>
                    </tr>
                   
                   <tr>
                        <td width="15%" align="right">Group&nbsp;</td>
                        <td width="20%" colspan="2" style="width: 25%">
                            <asp:DropDownList ID="ddlGroupID" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlGroupID_SelectedIndexChanged">
                            <asp:ListItem Value="1,2">Bahan Baku dan BP</asp:ListItem>
                            <asp:ListItem Value="8,9" Selected="True">Spare Part</asp:ListItem>
                            <asp:ListItem Value="7,10">Marketing</asp:ListItem>
                            <asp:ListItem Value="3">ATK</asp:ListItem>
                            </asp:DropDownList></td>
                        <td width="15%" align="right">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr><td colspan="4">&nbsp;</td></tr>
                    <tr>
                        <td width="15%" align="right">&nbsp;</td>
                        <td width="20%" colspan="2" style="width: 25%">
                        <asp:Button ID="Preview" runat="server" Text="Preview" onclick="Preview_Click" />
                        <asp:Button ID="Print" runat="server" Text="Preview" OnClick="Print_Click" Visible="false" />
                        <asp:Button ID="ExportToExcel" runat="server" Text="ToExcel" Visible="false" onclick="ExportToExcel_Click" /></td>
                        <td width="15%" align="right">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    </table>
                <hr />
                </div>    
                <div style="width: 100%; height: 350px; overflow: auto; background-color: White;
                    border: 2px solid #B0C4DE">
                    <table style="width: 100%; border-collapse: collapse; font-size:smaller">
                        <asp:Repeater ID="lstRekapWn" runat="server">
                            <HeaderTemplate>
                            <tr class="header">
                                <th class="kotak" style="width:4%">No.</th>
                                <th class="kotak" style="width:10%">Kode Barang</th>
                                <th class="kotak" style="width:30%">Nama Barang</th>
                                <th class="kotak" style="width:5%">Unit</th>
                                <th class="kotak" style="width:5%">Min</th>
                                <th class="kotak" style="width:8%">ReOrder</th>
                                <th class="kotak" style="width:8%">Stock</th>
                                <th class="kotak" style="width:8%">SPP</th>
                                <th class="kotak" style="width:8%">PO</th>
                                <th class="kotak" style="width:15%">Remark</th>
                            </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baris" style="background-color:#DCDCDC">
                                    <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                    <td class="kotak tengah"><span style="color:White">'</span><%# Eval("ItemCode") %></td>
                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                    <td class="kotak"><%# Eval("UOMName") %></td>
                                    <td class="kotak angka"><%# Eval("MinStock","{0:N2}") %></td>
                                    <td class="kotak angka"><%# Eval("ReOrder", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("Stock", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("OtSPP", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("OtPO", "{0:N2}")%></td>
                                    <td class="kotak"><%# Eval("Remark")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                            <tr class="baris">
                                    <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                    <td class="kotak tengah"><span style="color:White">'</span><%# Eval("ItemCode") %></td>
                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                    <td class="kotak"><%# Eval("UOMName") %></td>
                                    <td class="kotak angka"><%# Eval("MinStock","{0:N2}") %></td>
                                    <td class="kotak angka"><%# Eval("ReOrder", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("Stock", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("OtSPP", "{0:N2}")%></td>
                                    <td class="kotak angka"><%# Eval("OtPO", "{0:N2}")%></td>
                                    <td class="kotak"><%# Eval("Remark")%></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                 </td>
             </tr>
            </table>
         </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
