<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM_MMSReport.aspx.cs" Inherits="GRCweb1.Modul.SarMut.BM_MMSReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:small">
                    <tr>
                        <td style="width:100%; height:49px">
                           <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <td style="width:50%; padding-left:10px">PEMANTAUAN EFFISIENSI MMS ANTI FOAM</td>
                                    <td style="width:50%; text-align:right; padding-right:10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Refresh" OnClick="btnNew_Click" 
                                            style="font-family: Calibri" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" 
                                            OnClick="btnSimpan_Click" style="font-family: Calibri" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            OnClick="btnCancel_Click" style="font-family: Calibri" />                                     
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>  
                            <div class="content" style="background:#fff;">
                                <table style="width:100%; border-collapse:collapse; font-size:small">
                                    <tr>
                                        <td style="width:5%">&nbsp;</td>
                                        <td style="width:15%; font-family: Calibri; text-align: left; font-weight: 700;">Periode</td>
                                        <td style="width:15%">
                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>                                     
                                        <td style="width:40%"><asp:Button ID="btnPreview" runat="server" Text="<< Preview >>" 
                                                OnClick="btnPreview_Click" 
                                                style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" 
                                                ForeColor="#000099" />
                                            <asp:Button ID="btnExport" runat="server" Text="<< Export to Excel >>" 
                                                OnClick="btnExport_Click" 
                                                style="font-family: Calibri; color: #FFFFFF; font-weight: 700; background-color: #6666FF;" />
                                        </td>
                                        <td>
                                   <table id="Table1" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    
                                    <tr style="width: 100%">
                                    
                                        <td style="width: 44%;">
                                            <asp:Label ID="lblakumulasioutput" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp; Akumulasi Output :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%;">
                                            <asp:TextBox ID="txtakumulasioutput" runat="server"  Style="font-family: Calibri;
                                                font-size: x-small" BorderStyle="None" Enabled="false" ></asp:TextBox>
                                            
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:Label ID="Label1" runat="server"  Visible="true"  Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp;(M3)</asp:Label>
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr style="width: 100%">
                                        <td style="width: 44%">
                                            <asp:Label ID="lblakumulasipemakaian" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold" >&nbsp; Akumulasi Pemakaian :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txtakumulasipemakaian" runat="server" Style="font-family: Calibri;
                                                font-size: x-small" BorderStyle="None" Enabled="false"></asp:TextBox>
                                            
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:Label ID="Label2" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp;(Kg)</asp:Label>
                                        </td>
                                    </tr>
                                    
                                    <tr style="width: 100%">
                                        <td style="width: 44%">
                                            <asp:Label ID="lblefisiensi" runat="server" Style="font-family: Calibri; font-size: x-small;
                                                font-weight: bold" Visible="true" >&#160; Effisiensi :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txttotaleffisiensi" runat="server" BorderStyle="None" Style="font-family: Calibri;
                                                font-size: x-small" Enabled="false"></asp:TextBox>
                                            
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:Label ID="Label3" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp;(Kg/M3)</asp:Label>
                                        </td>
                                    </tr>
                                   
                                </table>
                                        </td>
                                                                           
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="3">                                          
                                           
                                                <asp:Label ID="LabelStatus" runat="server" Visible="false" Width="100%"></asp:Label>
                                        </td>                                     
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td colspan="3">                                          
                                           
                                                &nbsp;</td>                                     
                                    </tr>
                                </table>
                               <asp:Panel ID="PanelCiteureup" runat="server" Visible="false">
                                <div class="contentlist" style="height:440px;overflow:auto" id="lst" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small; font-family: Calibri;" 
                                        border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th rowspan="3" class="kotak">Tanggal</th>                                            
                                                <th colspan="3" class="kotak txtUpper">Line 1</th>
                                                <th colspan="3" class="kotak txtUpper">Line 2</th>
                                                <th colspan="3" class="kotak txtUpper">Line 3</th>
                                                <th colspan="3" class="kotak txtUpper">Line 4</th> 
                                                <th rowspan="3" class="kotak">Keterangan</th>                                                                              
                                            </tr>
                                            <tr class="tbHeader">
                                                <th colspan="1" class="kotak">Output</th>                                               
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                                                                             
                                                <th colspan="1" class="kotak">Output</th>    
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th> 
                                                  
                                                <th colspan="1" class="kotak">Output</th>   
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                                   
                                                <th colspan="1" class="kotak">Output</th>   
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>   
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                              
                                                <th class="kotak">(Kg/M3)</th>
                                                
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                              
                                                <th class="kotak">(Kg/M3)</th>
                                                
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                              
                                                <th class="kotak">(Kg/M3)</th>
                                                
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                              
                                                <th class="kotak">(Kg/M3)</th>
                                             
                                                <%--<th class="kotak"> </th>--%>
                                            </tr>
                                        </thead>
                                        
                                        <tbody id="tb" runat="server" style="font-family: Calibri">
                                        
                                            <asp:Repeater ID="lstMatrix" runat="server" 
                                                OnItemDataBound="lstMatrix_DataBound" onitemcommand="lstMatrix_ItemCommand">
                                                <ItemTemplate>
                                                    <tr id="lst2" runat="server" class="EvenRows baris">
                                                        <td class="kotak tengah" ><%# Eval("Tanggal", "{0:d}")%></td>
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L1", "{0:N2}")%></td>  
                                                        <td class="kotak angka"><%# Eval("QtySPBL1", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL1", "{0:N2}")%></td>                                                       
                                                        
                                                       <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L2", "{0:N2}")%></td>  
                                                        <td class="kotak angka"><%# Eval("QtySPBL2", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL2", "{0:N2}")%></td>                                                       
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L3", "{0:N2}")%></td>  
                                                        <td class="kotak angka"><%# Eval("QtySPBL3", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL3", "{0:N2}")%></td>                                                     
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L4", "{0:N2}")%></td>   
                                                        <td class="kotak angka"><%# Eval("QtySPBL4", "{0:N2}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL4", "{0:N2}")%></td>                                                      
                                                        
                                                        <td class="kotak" style="background-color:#FF99FF;">
                                                            <asp:Label ID="txtKeterangan" runat="server" Visible="false" Width="100%">&nbsp;&nbsp;</asp:Label>
                                                            <asp:TextBox ID="Keterangan" runat="server" AutoPostBack="true" CssClass="txtOnGrid" Width="100%" Font-Names="calibri"></asp:TextBox>                                                           
                                                        </td>                                                       
                                                    </tr>
                                                    </ItemTemplate>  
                                            </asp:Repeater>
                                            
                                            <tr class="Line3 Baris" id="lstF" runat="server">
                                                        <td colspan="1" class="kotak txtUpper"></td>                                                        
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>     
                                           </tr> 
                                        </tbody>   
                                    </table>
                                </div>
                                </asp:Panel>
                           
                                <asp:Panel ID="PanelKarawang" runat="server" Visible="false">
                                <div class="contentlist" style="height:440px;overflow:auto" id="lstK" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small; font-family: Calibri;" 
                                        border="1">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th rowspan="3" class="kotak">Tanggal</th>                                            
                                                <th colspan="3" class="kotak txtUpper">Line 1</th>
                                                <th colspan="3" class="kotak txtUpper">Line 2</th>
                                                <th colspan="3" class="kotak txtUpper">Line 3</th>
                                                <th colspan="3" class="kotak txtUpper">Line 4</th> 
                                                <th colspan="3" class="kotak txtUpper">Line 5</th>
                                                <th colspan="3" class="kotak txtUpper">Line 6</th>
                                                <th rowspan="3" class="kotak">Keterangan</th>                                                                                         
                                            </tr>
                                            
                                            <tr class="tbHeader">
                                                <th colspan="1" class="kotak">Output</th>                                              
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                               
                                                <th colspan="1" class="kotak">Output</th>                                               
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                              
                                                <th colspan="1" class="kotak">Output</th>                                              
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                              
                                                <th colspan="1" class="kotak">Output</th>                                              
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                               
                                                <th colspan="1" class="kotak">Output</th>                                               
                                                <th colspan="1" class="kotak">Pemakaian</th>
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                              
                                                <th colspan="1" class="kotak">Output</th>
                                                <th colspan="1" class="kotak">Pemakaian</th>                                               
                                                <th colspan="1" class="kotak">Efesiensi</th>
                                               
                                            </tr>
                                            <tr class="tbHeader">
                                                <th class="kotak">(M3)</th>                                               
                                                <th class="kotak">(Kg)</th>
                                                <th class="kotak">(Kg/M3)</th>
                                               
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                               
                                                <th class="kotak">(Kg/M3)</th>
                                              
                                                <th class="kotak">(M3)</th>                                                
                                                <th class="kotak">(Kg)</th>
                                                <th class="kotak">(Kg/M3)</th>
                                              
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                               
                                                <th class="kotak">(Kg/M3)</th>
                                               
                                                <th class="kotak">(M3)</th>
                                                <th class="kotak">(Kg)</th>                                              
                                                <th class="kotak">(Kg/M3)</th>
                                               
                                                <th class="kotak">(M3)</th>                                               
                                                <th class="kotak">(Kg)</th>
                                                <th class="kotak">(Kg/M3)</th>
                                              
                                                
                                            </tr>
                                        </thead>
                                        <tbody id="tb2" runat="server" style="font-family: Calibri">
                                            <asp:Repeater ID="lstMatrixK2" runat="server" 
                                                OnItemDataBound="lstMatrixK2_DataBound" onitemcommand="lstMatrixK2_ItemCommand">
                                                <ItemTemplate>
                                                    <tr id="lstK2" runat="server" class="EvenRows baris">
                                                        <td class="kotak tengah"><%# Eval("Tanggal", "{0:d}")%></td>   
                                                                                                             
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L1", "{0:N5}")%></td>  
                                                        <td class="kotak angka"><%# Eval("QtySPBL1", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL1", "{0:N2}")%></td>                                                      
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L2", "{0:N5}")%></td> 
                                                        <td class="kotak angka"><%# Eval("QtySPBL2", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL2", "{0:N2}")%></td>                                                      
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L3", "{0:N5}")%></td> 
                                                        <td class="kotak angka"><%# Eval("QtySPBL3", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL3", "{0:N2}")%></td>                                                      
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L4", "{0:N5}")%></td>   
                                                        <td class="kotak angka"><%# Eval("QtySPBL4", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL4", "{0:N2}")%></td>                                                       
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L5", "{0:N5}")%></td>    
                                                        <td class="kotak angka"><%# Eval("QtySPBL5", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL5", "{0:N2}")%></td>                                                   
                                                        
                                                        <td class="kotak angka" style="white-space:nowrap"><%# Eval("OutM3L6", "{0:N5}")%></td>  
                                                        <td class="kotak angka"><%# Eval("QtySPBL6", "{0:N0}")%></td>
                                                        <td class="kotak angka"><%# Eval("EfesiensiL6", "{0:N2}")%></td>                                                     
                                                        
                                                        <td class="kotak" style="background-color:#FF99FF;">
                                                            <asp:Label ID="txtKeterangan" runat="server" Visible="false" Width="100%">&nbsp;&nbsp;</asp:Label>
                                                            <asp:TextBox ID="Keterangan" runat="server" AutoPostBack="true" CssClass="txtOnGrid" Width="100%" Font-Names="calibri"></asp:TextBox>                                                           
                                                        </td>                                                      
                                                    </tr>
                                                    </ItemTemplate> 
                                            </asp:Repeater>                                            
                                            
                                            <tr class="Line3 Baris" id="lstK2F" runat="server">
                                                        <td colspan="1" class="kotak txtUpper"></td>                                                        
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                        <td class="kotak angka"></td>
                                                    
                                                    </tr>
                                            
                                                  <%--  <tr><td colspan="22">&nbsp;</td></tr>--%>
                                                  
                                                 
                                        </tbody>
                                        
                                    </table>
                                </div>
                                </asp:Panel>
                            </div>
                            
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
