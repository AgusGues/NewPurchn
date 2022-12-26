<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapImprovement.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapImprovement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <style type="text/css">
             .page-content {
                width:100%;
                background-color: #fff;
                margin: 0;
                padding: 1px 1px 1px;
                overflow-x: auto;
                min-height: .01%;
            }
            label{font-size:12px;}
            .tblLst tr:nth-child(odd){ 
	            background-color:#aaffd4;  
	            }
            .tblLst tr:nth-child(even) {
	            background-color:#ffffff;
	            }
            .tblLst
            {
                border-collapse: collapse;
            }
            .tblLst th{
	            background-color:Purple;
	            }
        </style>
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
            <div id="Div1" runat="server">
                <table style="width:100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                              
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;Laporan Improvement Kode Baru</strong>
                                        </td>
                                    </tr>
                                </table>
                                
                            </td>
                            
                        </tr>
                        <tr valign="top">
                            <td style="width: 100%" valign="top">
                                <%--form input --%>
                                <div style="background-color: #fff; vertical-align: top">
                                    <b>Select Criteria :</b>
                                    <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 99%;" border="0">
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:RadioButton ID="rbDetail" runat="server" Text="Detail" OnCheckedChanged="rbDetail_CheckedChanged"
                                                    AutoPostBack="true" GroupName="rptCriteria" />
                                                &nbsp;
                                                <asp:RadioButton ID="rbRekap" runat="server" Text="Rekap" Checked="true" OnCheckedChanged="rbRekap_CheckedChanged"
                                                    AutoPostBack="true" GroupName="rptCriteria" />
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Tahun</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Bulan</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0">--Pilih Bulan--</asp:ListItem>
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
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Plant</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlPlant" runat="server">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Citeureup</asp:ListItem>
                                                    <asp:ListItem Value="7">Karawang</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Group Purchn</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="GroupID" runat="server">
                                                    <asp:ListItem Value="0">All Group</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                &nbsp; Type Barang&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlCriteria" runat="server">
                                                    <asp:ListItem Value="">All</asp:ListItem>
                                                    <asp:ListItem Value=" and (Stock=1 or Stock=9)">Stock</asp:ListItem>
                                                    <asp:ListItem Value=" and Stock=0"> Non Stock</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top" colspan="2">
                                                &nbsp;
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:Button ID="preview" runat="server" Text="Preview" OnClick="preview_Click" />
                                                <asp:Button ID="printer" runat="server" Text="Print" OnClick="printer_Click" />
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--end of form input --%>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <hr />
           <div id="rkp" runat="server" style="width:100% ; padding: 5px; font-size: smaller; height:250px; overflow:auto">
                <table width="80%" style="border-collapse:collapse" border="1">
                    <thead>
                    <tr style="background-color:Gray">
                        <th width="10%">Tahun</th>
                        <th width="15%">Bulan</th>
                        <th width="8%">BB</th>
                        <th width="8%">BP</th>
                        <th width="8%">ATK</th>
                        <th width="8%">PYK</th>
                        <th width="8%">MKT</th>
                        <th width="8%">RKP</th>
                        <th width="8%">MEK</th>
                        <th width="8%">ELK</th>
                        <th width="10%">Total</th>
                    </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rkpList" runat="server">
                            <ItemTemplate>
                            <tr>
                                <td align="center"><%# Eval("Tahun") %></td>
                                <td align="left"><%# Eval("BulanLong") %></td>
                                <td align="right"><%# Eval("BB","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("BP","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("ATK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("PYK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("MKT","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("RKP", "{0:#,##0.00}")%></td>
                                <td align="right"><%# Eval("MEK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("ELK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("Total","{0:#,##0.00}") %></td>
                            </tr>
                           
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                            <tr style="background-color:#E0D8E0">
                                <td align="center"><%# Eval("Tahun") %></td>
                                <td align="left"><%# Eval("BulanLong") %></td>
                                <td align="right"><%# Eval("BB","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("BP","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("ATK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("PYK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("MKT","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("RKP", "{0:#,##0.00}")%></td>
                                <td align="right"><%# Eval("MEK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("ELK","{0:#,##0.00}") %></td>
                                <td align="right"><%# Eval("Total","{0:#,##0.00}") %></td>
                            </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
           </div>
            <div id="lst" runat="server" style="width: 100%; padding: 5px; font-size: smaller; height:300px; overflow:auto">
                <%--List stock--%>
                <table width="90%" class="table_even" style="border-collapse: collapse; font-size: x-small" border="1">
                    <thead>
                        <tr align="center" style="background-color:#FFA000">
                            <th width="5%">#</th>
                            <th width="10%">Item Code</th>
                            <th width="35%">Item Name</th>
                            <th width="5%">UoM</th>
                            <th width="10%">Stock</th>
                            <th width="10%">Plant</th>
                            <th width="8%">Approval</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="ListStock" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center"><%# Eval("ID") %> </td>
                                    <td align="center"><%# Eval("ItemCode") %></td>
                                    <td align="left"><%# Eval("ItemName") %></td>
                                    <td align="center"><%# Eval("UOMCode")%></td>
                                    <td align="left"><%# Eval("Keterangan")%></td>
                                    <td align="left"><%# Eval("UnitKerja")%></td>
                                    <td align="center"><%# Eval("Approval")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color:#E0D8E0">
                                    <td align="center"><%# Eval("ID") %> </td>
                                    <td align="center"><%# Eval("ItemCode") %></td>
                                    <td align="left"><%# Eval("ItemName") %></td>
                                    <td align="center"><%# Eval("UOMCode")%></td>
                                    <td align="left"><%# Eval("Keterangan")%></td>
                                    <td align="left"><%# Eval("UnitKerja")%></td>
                                    <td align="center"><%# Eval("Approval")%></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <%-- end of list stock --%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
