<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterRoles.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
          <TD style="height: 49px" ><table class=nbTableHeader>
                  <tr>
                      <td style="width: 100%">
                          <strong>&nbsp;Roles</strong></td>
                      <td style="width: 100%">
                      </td>                        
                      <td style="width: 37px">
                          <input id="btnNew" runat="server" style="background-image: url(../images/Button_Back.gif);
                              background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                      <td style="width: 75px"><input id="btnUpdate" runat="server" style="background-image: url(../images/Button_Back.gif);
                              background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick"/></td>
                      <td style="width: 5px"><input id="btnDelete" runat="server" style="background-image: url(../images/Button_Back.gif);
                              background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick"  /></td>                     
                      <td style="width: 70px">
                          <input id="btnPrint" runat="server" style="background-image: url(../images/Button_Back.gif);
                              background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cetak" onclick="Cetak()" /></td>
                      <td style="width: 70px">
                          <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                              <asp:ListItem Value="RolesName">Nama Roles</asp:ListItem>
                          </asp:DropDownList></td>
                      <td style="width: 70px">
                          <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox></td>
                      <td style="width: 70px">
                          <input id="btnSearch" runat="server" style="background-image: url(../images/Button_Back.gif);
                              background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick"/></td>
                  
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
                  <DIV style="OVERFLOW: auto; HEIGHT: 100%; width: 100%;" >
                  <DIV>
                  <TABLE id="TblIsi"  cellSpacing="0"
								    cellPadding="0" width="100%" border="0">
								    <TR>
									    <TD style="HEIGHT: 101px; width: 100%;">
										    <TABLE class="tblForm" id="Table4" style="LEFT: 0px; TOP: 0px; width: 103%;" cellSpacing="1"
											    cellPadding="0" border="0">
                                                <tr>
                                                    <td style="width: 197px; height: 3px" valign="top">
                                                    </td>
                                                    <td style="width: 204px; height: 3px" valign="top">
                                                    </td>
                                                    <td style="height: 3px; width: 169px;" valign="top">
                                                    </td>
                                                    <td style="width: 209px; height: 3px" valign="top">
                                                    </td>
                                                    <td style="width: 205px; height: 3px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 197px; height: 6px" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Nama Roles</span></td>
                                                    <td style="width: 204px; height: 6px" valign="top">
                                                        <asp:TextBox ID="txtRoleName" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox></td>
                                                    <td style="height: 6px; width: 169px;" valign="top">                                                      
                                                    </td>
                                                    <td style="width: 209px; height: 6px" valign="top">                                                       
                                                    </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>                                                                                 
                                                <tr>
                                                    <td style="width: 197px; height: 19px">
                                                    </td>
                                                    <td rowspan="1" style="width: 204px; height: 19px;">
                                                    </td>
                                                    <td style="width: 169px; height: 19px">
                                                    </td>
                                                    <td style="width: 209px; height: 19px">
                                                    </td>
                                                    <td style="width: 205px; height: 19px">
                                                    </td>
                                                </tr>
                                            </table>
                                            <HR width="100%" SIZE="1">
									    </TD>
								    </TR>
							    </TABLE>
                 </DIV>
                 <TABLE id="Table2" style="LEFT: 0px; TOP: 0px; width: 95%;" cellSpacing="1"
											    cellPadding="0" border="0" height="165">
                     <tr>
                         <td colspan="1" style="height: 3px" valign="top" width="100">
                         </td>
                         <td style="height: 3px" valign="top" colspan="1">
                             <span style="font-size: 10pt">&nbsp; <strong>List</strong></span></td>
                     </tr>
                     <tr>
                         <td style="height: 100%" valign="top" width="100">
                             &nbsp; &nbsp;
                         </td>
                         <td style="width: 100%; height: 100%" valign="top">  
                         <div id="div2" style="width:770px;height:320px;overflow:auto">
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                 Width="100%" onrowcommand="GridView1_RowCommand" AllowPaging="true" onpageindexchanging="GridView1_PageIndexChanging">
                                 <Columns>
                                     <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                     <asp:BoundField DataField="RolesName" HeaderText="Nama Roles" />                                       
                                     <asp:ButtonField CommandName="Add" Text="Pilih" />
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
