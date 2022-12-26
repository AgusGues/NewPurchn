<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListTabelConversi.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListTabelConversi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label,td,span{font-size:12px;}
        /*table,tr,td{background-color: #fff;}*/
    </style>
     <script language="JavaScript">
        function openWindow() {
            window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
        }

      
     
</script>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">        
    <ContentTemplate>            
    <div id="Div1" runat="server">
    <TABLE style="TABLE-LAYOUT: fixed" height="100%" cellSpacing=0 cellPadding=0 
width="100%">
  <TBODY>
  
  <TR>
    <TD style="width: 100%;">
        <TABLE class=nbTableHeader>
        <TBODY>
        <TR >
          <TD style="width: 703px; height: 49px" ><table class=nbTableHeader></td>
                  <tr>
                      <td style="width: 218%">
                          <strong>Daftar Tabel Conversi</strong></td>                      
                      <td style="width: 75px">
                          <input id="btnUpdate" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Form Conversi" 
                              onserverclick="btnUpdate_ServerClick" /></td>                      
                      <td>
                          <asp:DropDownList ID="ddlSearch" runat="server" Width="120px"> 
                            <asp:ListItem Value="ScheduleNo">No</asp:ListItem>                             
                          </asp:DropDownList></td>
                      <td>
                          <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                      <td style="width: 3px">
                          <input id="btnSearch" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                  </tr>
              </table></TD></TR></TBODY></TABLE>
    </TD>
  </tr>
  <TR height="100%">
    <TD height="100%" style="width: 100%">
    <TABLE class=nbTable1 cellSpacing=0 cols=1 cellPadding=0 height="100%" width="100%">
        <TBODY>
        <TR class=treeRow1 vAlign=top>
          <TD>
            <TABLE class=nbTable1 cellSpacing=0 cols=1 cellPadding=0 border=1 height="100%" width="100%">
              <TBODY>
              <TR style="width: 100%;HEIGHT: 100%">
                <TD style="width: 100%; height: 100%;">
                  <DIV style="HEIGHT: 100%; width: 100%;" >
                  <DIV>
                  <TABLE id="TblIsi"  cellSpacing="0"
								    cellPadding="0" width="100%" border="0">
								    <TR>
									    <TD style="HEIGHT: 101px; width: 100%;">
										    <TABLE id="Table4" style="LEFT: 0px; TOP: 0px; width: 100%;" cellSpacing="1"
											    cellPadding="0" border="0">
                                                
                                            </table>
                                            <HR width="100%" SIZE="1">
                 </DIV>
                      <TABLE id="Table2" style="LEFT: 0px; TOP: 0px; width: auto;" cellSpacing="1"
											    cellPadding="0" border="0" height="165">
                     <tr>
                         <td style="height: 3px; width: 203px;" valign="top" colspan="1">
                         </td>
                         <td style="height: 3px" valign="top" colspan="1">
                             <span style="font-size: 10pt">&nbsp; <strong>List</strong></span></td>
                     </tr>
                     <tr>
                         <td style="width: 203px; height: 100%" valign="top">
       &nbsp; &nbsp;
                         </td>
                         <td style="width: 100%; height: 100%" valign="top">
                           <div id="div2" style="width:auto; height:auto; overflow:auto">
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                 Width="100%" onrowcommand="GridView1_RowCommand" 
                                   OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" 
                                   onpageindexchanging="GridView1_PageIndexChanging">
                                 <Columns>                                     
                                     <asp:BoundField DataField="ConversiNo" HeaderText="No" />
                                     <asp:BoundField DataField="FromItemCode" HeaderText="Dari Kd Brg" />
                                     <asp:BoundField DataField="FromQty" HeaderText="Dari Jumlah" />
                                     <asp:BoundField DataField="FromUomCode" HeaderText="Dari Satuan" />
                                     <asp:BoundField DataField="Jumlah" HeaderText="STOK RE-PACK" />
                                     <asp:BoundField DataField="ToItemCode" HeaderText="Ke Kd Brg" />
                                     <asp:BoundField DataField="ToQty" HeaderText="Ke Jumlah" />
                                     <asp:BoundField DataField="ToUomCode" HeaderText="Ke Satuan" />                                                                    
                                     <asp:ButtonField CommandName="Add" Text="Pilih" />
                                     <asp:BoundField DataField="FromItemName" HeaderText="Dari Nama Brg" >
                                         <ItemStyle Wrap="False" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="ToItemName" HeaderText="Ke Nama Brg" >
                                         <ItemStyle Wrap="False" />
                                     </asp:BoundField>
                                 </Columns>
                                 <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                 <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                 <PagerStyle BorderStyle="Solid" />
                                 <AlternatingRowStyle BackColor="Gainsboro" />
                             </asp:GridView>                          
                             </div>                                     
                         </td>
                     </tr>
                 </table>
                  </DIV></TD></TR>
              </TBODY>
            </TABLE>
            </TD></TR>
       </TBODY></TABLE></TR></TBODY></TABLE>
     </div>
     </ContentTemplate> 
     </asp:UpdatePanel>     

</asp:Content>
