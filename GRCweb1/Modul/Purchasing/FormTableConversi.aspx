<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormTableConversi.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormTableConversi" %>
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
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
    function onCancel() 
    {  }

    function Cetak() 
    {
        var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipMRS", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function confirm_delete() 
    {
        if (confirm("Anda yakin untuk Cancel ?") == true)
            window.showModalDialog('../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
    </script>

<%--    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  --%>
<%--    <script language="JavaScript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
    </script>  --%>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
    <ContentTemplate>    
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
            ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel" ConfirmOnFormSubmit="false" />    
    
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
                      <td style="width: 100%">
                          <strong>&nbsp;TABEL CONVERSI</strong></td>
                      <td style="width: 100%">
                      </td>                        
                      <td style="width: 37px">
                          <input id="btnNew" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                      <td style="width: 75px"><input id="btnUpdate" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Simpan" onserverclick="btnUpdate_ServerClick"/></td>
                              
                      <td style="width: 5px">
                           <asp:Button id="btnCancel"  runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;"                               
                               Text="Cancel" onclick="btnCancel_ServerClick"/></td>

                      <td style="width: 70px">
                          <input id="btnList" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="List" onserverclick="btnList_ServerClick" /></td>
                      <td style="width: 70px">
                          <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">                              
                              <asp:ListItem Value="ScheduleNo">No</asp:ListItem>                              
                          </asp:DropDownList></td>
                      <td style="width: 70px">
                          <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox></td>
                      <td style="width: 70px">
                          <input id="btnSearch" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Cari" onserverclick="btnSearch_ServerClick"/></td>
                  
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
										    <TABLE class="tblForm" id="Table4" 
                                                style="LEFT: 0px; TOP: 0px; width: 103%; font-size: x-small;" cellSpacing="1"
											    cellPadding="0" border="0">
                                                <tr>
                                                    <td style="width: 148px; height: 3px" valign="top">
                                                    </td>
                                                    <td style="width: 182px; height: 3px" valign="top">
                                                    </td>
                                                    <td style="height: 3px; width: 155px;" valign="top">
                                                        &nbsp;</td>
                                                    <td style="width: 209px; height: 3px" valign="top">
                                                        &nbsp;</td>
                                                    <td style="width: 205px; height: 3px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 148px; height: 3px" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; No</span></td>
                                                    <td style="width: 182px; height: 3px" valign="top">
                                                       <asp:TextBox ID="txtConversiNo" runat="server" BorderStyle="Groove" Width="233" 
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 3px; width: 155px;" valign="top">
                                                        &nbsp;</td>
                                                    <td style="width: 209px; height: 3px" valign="top">
                                                        &nbsp;</td>
                                                    <td style="width: 205px; height: 3px" valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 148px; height: 6px" valign="top">
                                                        &nbsp; Cari Kode Barang&nbsp;</td>
                                                    <td style="width: 182px; height: 6px; font-size: xx-small;" valign="top">
                                                        <asp:TextBox ID="txtFromCariItemCode" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove" Font-Italic="True" Font-Size="X-Small" Height="22px" 
                                                            OnTextChanged="txtFromCariItemCode_TextChanged" Width="333px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" 
                                                            FirstRowSelected="True" MinimumPrefixLength="1" 
                                                            ServiceMethod="GetItemInventory" ServicePath="AutoComplete.asmx" 
                                                            TargetControlID="txtFromCariItemCode">
                                                        </cc1:AutoCompleteExtender>
                                                     </td>
                                                    <td style="height: 6px; width: 155px;" valign="top">                                                       
                                                        &nbsp; Cari Kode Barang&nbsp;</td>
                                                    <td style="width: 209px; height: 6px" valign="top">                                                       
                                                        <asp:TextBox ID="txtToCariItemCode" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove" Font-Size="X-Small" Height="22px" Width="333px" 
                                                            ontextchanged="txtToCariItemCode_TextChanged"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtToCariItemCode_AutoCompleteExtender" 
                                                            runat="server" CompletionInterval="200" CompletionSetCount="10" 
                                                            EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1" 
                                                            ServiceMethod="GetItemInventory" ServicePath="AutoComplete.asmx" 
                                                            TargetControlID="txtToCariItemCode">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 148px; height: 6px" valign="top">
                                                        &nbsp; Dari Kode Barang&nbsp;</td>
                                                    <td style="width: 182px; height: 6px" valign="top">
                                                        <asp:DropDownList ID="ddlFromItemCode" runat="server" AutoPostBack="True" 
                                                            Height="16px" onselectedindexchanged="ddlFromItemCode_SelectedIndexChanged" 
                                                            Width="333px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 6px; width: 155px;" valign="top">                                                       
                                                        &nbsp; Ke Kode Barang</td>
   
                                                    <td style="width: 209px; height: 6px" valign="top">                                                       
                                                        <asp:DropDownList ID="ddlToItemCode" runat="server" AutoPostBack="True" 
                                                            Height="16px" onselectedindexchanged="ddlToItemCode_SelectedIndexChanged" 
                                                            Width="333px">
                                                        </asp:DropDownList>
                                                    </td>

                                                    <td style="width: 169px; height: 19px" valign="top">                                                    
                                                      <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy" runat="server">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="height: 6px" valign="top" colspan="2" align="right">
                                                        <asp:TextBox ID="txtFromItemName" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove" ReadOnly="True" Width="380px"></asp:TextBox>
                                                     </td>
                                                     <td colspan="2" style="height: 6px" valign="top" align="right">
                                                         <asp:TextBox ID="txtToItemName" runat="server" AutoPostBack="True" 
                                                             BorderStyle="Groove" ReadOnly="True" Width="380px"></asp:TextBox>
                                                     </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>
                                                <TR>
                                                    <TD style="WIDTH: 148px; HEIGHT: 6px" valign="top">
                                                        &nbsp; Dari Satuan&nbsp;</td>
                                                    <TD style="HEIGHT: 6px; width: 182px;" valign="top">
                                                        <asp:DropDownList ID="ddlFromUomCode" runat="server" AutoPostBack="True" 
                                                            Height="16px" onselectedindexchanged="ddlFromUomCode_SelectedIndexChanged" 
                                                            Width="233px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 6px; width: 155px;" valign="top">  
                                                        &nbsp; Ke Satuan&nbsp;</td>
                                                    <td style="width: 209px; height: 6px" valign="top">
                                                        <asp:DropDownList ID="ddlToUomCode" runat="server" AutoPostBack="True" 
                                                            Height="16px" onselectedindexchanged="ddlToUomCode_SelectedIndexChanged" 
                                                            Width="233px" Enabled="False">
                                                        </asp:DropDownList>
                                                        </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>    
                                                      <TR>
                                                    <TD style="WIDTH: 148px; HEIGHT: 6px" valign="top">
                                                        &nbsp; Dari Jumlah&nbsp;</td>
                                                    <TD style="HEIGHT: 6px; width: 182px;" valign="top">
                                                        <asp:TextBox ID="txtFromJumlah" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove" Width="233"></asp:TextBox>
                                                          </td>
                                                    <td style="height: 6px; width: 155px;" valign="top">  
                                                        &nbsp; Ke Jumlah&nbsp;</td>
                                                    <td style="width: 209px; height: 6px" valign="top">
                                                        <asp:TextBox ID="txtToJumlah" runat="server" BorderStyle="Groove" Width="233" 
                                                            AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>  
                                                 <TR>
                                                    <TD style="WIDTH: 148px; HEIGHT: 6px" valign="top">
                                                        &nbsp; Dibuat Oleh&nbsp;</td>
                                                    <TD style="HEIGHT: 6px; width: 182px;" valign="top">
                                                        <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" 
                                                            ReadOnly="True" Width="233"></asp:TextBox>
                                                     </td>
                                                    <td style="width: 155px; height: 19px" valign="top">                                                    
                                                        &nbsp; Tanggal&nbsp;</td>
                                                    <td style="width: 209px; height: 6px" valign="top">
                                                        <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" 
                                                            BorderStyle="Groove" Width="233"></asp:TextBox>
                                                     </td>
                                                    <td style="width: 205px; height: 6px" valign="top">
                                                    </td>
                                                </tr>                                                          
                                                <tr>
                                                    <td style="width: 148px; height: 19px">
                                                        &nbsp;</td>
                                                    <td rowspan="1" style="width: 182px; height: 19px;">
                                                        &nbsp;</td>
                                                    <td style="width: 155px; height: 19px">
                                                        &nbsp;</td>
                                                    <td style="width: 209px; height: 19px">
                                                        &nbsp;</td>
                                                    <td style="width: 205px; height: 19px">
                                                    </td>
                                                </tr>             
                                                 <tr>
                                                    <td style="width: 148px; height: 19px">
                                                    </td>
                                                    <td rowspan="1" style="width: 182px; height: 19px">
                                                    </td>
                                                    <td style="width: 155px; height: 19px">
                                                       
                                                    </td>
                                                    <td style="width: 209px; height: 19px" align="right" >
                                                        <asp:LinkButton ID="lbAddOP" runat="server" Font-Size="10pt" 
                                                            onclick="lbAddOP_Click" Visible="False">Tambah</asp:LinkButton>
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
                                 Width="100%" onrowcommand="GridView1_RowCommand">
                                 <Columns>
                                     <asp:BoundField DataField="ConversiNo" HeaderText="No" />
                                     <asp:BoundField DataField="FromItemCode" HeaderText="Dari Kd Brg" />
                                     <asp:BoundField DataField="FromQty" HeaderText="Dari Jumlah" />
                                     <asp:BoundField DataField="FromUomCode" HeaderText="Dari Satuan" />
                                     <asp:BoundField DataField="ToItemCode" HeaderText="Ke Kd Brg" />
                                     <asp:BoundField DataField="ToQty" HeaderText="Ke Jumlah" />
                                     <asp:BoundField DataField="ToUomCode" HeaderText="Ke Satuan" />                                                                    
                                     <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
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
