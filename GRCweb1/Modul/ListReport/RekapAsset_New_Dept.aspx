<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAsset_New_Dept.aspx.cs" Inherits="GRCweb1.Modul.ListReport.RekapAsset_New_Dept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%;">
                            <table class="nbTableHeader" style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr  style="height:49px">
                                    <td style="width:50%; padding-left:10px">
                                        <span style="font-family: 'Courier New', Courier, monospace; font-size: medium">
                                        <strong>REKAP ASSET PER DEPARTMENT</strong></span>
                                    </td>
                                    <td >
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                   <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <tr style="display:none">
                                            <td style="width:10%; font-family: Calibri;">Periode :</td>
                                            <td style="width:25%">
                                                <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                       <tr>
                                           <td style="width: 18%">
                                               <asp:Label ID="labelDept" runat="server" Visible="true" Style="font-family: Calibri;
                                                   font-size: x-small; font-weight: bold">&nbsp; Department</asp:Label>
                                           </td>
                                           <td>
                                               <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" Visible="true"
                                                   OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" 
                                                   Style="font-family: Calibri; font-weight: 700;">
                                               </asp:DropDownList>
                                           </td>
                                           <td>
                                               &nbsp;
                                           </td>
                                       </tr>
                                            
                                        <tr>
                                            <td style="font-family: Calibri; font-weight: 700">&nbsp; Periode :</td>
                                            <td style="font-family: Calibri; font-weight: 700" colspan="2"> 2010  s/d <%= DateTime.Now.Year.ToString() %></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td colspan="2">
                                                <asp:Button ID="btnPreview" runat="server" Text="Preview" 
                                                    OnClick="btnPreview_Click" style="font-family: Calibri" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
                                                    OnClick="btnExport_Click" style="font-family: Calibri" />
                                            </td>
                                        </tr>
                                   </table>
                                <div class="contentlist" style="height:400px" id="lst" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" rowspan="2" style="width:4%; font-family: Calibri;">No.</th>
                                                <th class="kotak" rowspan="2" style="width:10%; font-family: Calibri;">ItemCode</th>
                                                <th class="kotak" rowspan="2" style="width:35%; font-family: Calibri;">ItemName</th>
                                                <th class="kotak" rowspan="2" style="width:4%; font-family: Calibri;">Satuan</th>
                                                <th class="kotak" rowspan="2" style="width:6%; font-family: Calibri;">Saldo Awal</th>
                                                <th class="kotak" rowspan="2" style="width:6%; font-family: Calibri;">Pembelian</th>
                                                <th class="kotak" colspan="2" style="font-family: Calibri">Adjustment</th>
                                                <%--
                                                <th style="display:none" class="kotak" colspan="2">Mutasi Antar Dept</th>
                                               --%>
                                                <th class="kotak" rowspan="2" style="width:6%; font-family: Calibri;">Saldo Akhir</th>
                                                <th class="kotak" rowspan="2" style="width:6%; font-family: Calibri;">Kategori</th>
                                               <%--
                                                <th style="display:none" class="kotak" rowspan="2" style="width:6%">SPB</th>
                                                <th style="display:none" class="kotak" rowspan="2" style="width:6%">Stock Gudang</th>
                                              --%>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:8%; font-family: Calibri;">IN</th>
                                                <th class="kotak" style="width:8%; font-family: Calibri;">OUT / RettOff</th>
                                                
                                                
                                              <%--
                                                <th style="display:none" class="kotak" style="width:8%">IN</th>
                                                <th style="display:none" class="kotak" style="width:8%">Out</th>
                                              --%>
                                            </tr>
                                        </thead>
                                        <tbody style="font-family: Calibri">
                                            <asp:Repeater ID="lstAsset" runat="server" OnItemDataBound="lstAsset_Databound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak tengah" style="white-space:nowrap"><%# Eval("ItemCode") %></td>
                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                        <td class="kotak tengah"><%# Eval("Unit") %></td>
                                                        <td class="kotak angka"><%# Eval("SaldoAwal","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("Pembelian","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AdjustIN","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AdjustOut","{0:N0}") %></td>
                                                      <%--
                                                        <td style="display:none" class="kotak angka"><%# Eval("MutasiIN","{0:N0}") %></td>
                                                        <td style="display:none" class="kotak angka"><%# Eval("MutasiOut","{0:N0}") %></td>
                                                     --%>
                                                        <td class="kotak angka"><%# Eval("SaldoAkhir","{0:N0}") %></td>
                                                        <td class="kotak"><%# Eval("Kategori") %></td>
                                                       <%-- 
                                                        <td style="display:none" class="kotak angka"><%# Eval("SPB","{0:N0}") %></td>
                                                        <td style="display:none" class="kotak angka"><%# Eval("StockGudang","{0:N0}") %></td>
                                                      --%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="Line3 baris total" id="total" runat="server">
                                                <td colspan="4" class="kotak angka">Total</td>
                                                <td class="kotak angka 1"></td>
                                                <td class="kotak angka 2"></td>
                                                <td class="kotak angka"></td>
                                                <td class="kotak angka"></td>
                                               <%--
                                                <td style="display:none;" class="kotak angka"></td>
                                                <td style="display:none;" class="kotak angka"></td>
                                                --%>
                                             
                                                <td style="display:;" class="kotak angka"></td>
                                                <%--
                                                <td style="display:none;" class="kotak angka"></td>
                                                <td style="display:none;" class="kotak angka"></td>
                                               --%>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
