<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterUsers.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
    <ContentTemplate>         
    <div id="Div1" runat="server">       
    <table style="table-layout: fixed; height:100%; width:100%">
         <tr>
            <td style="height: 49px; width:100%" >
                  <table class="nbTableHeader">
                      <tr>
                          <td style="width: 100%">
                              <strong>&nbsp;USER</strong>
                          </td>
                          
                          <td style="width: 37px">
                              <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                  font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                          </td>
                          <td style="width: 75px">
                              <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                  font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                          </td>
                          <td style="width: 5px">
                              <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold;
                                  font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick" />
                          </td>
                          <td style="width: 70px">
                              <input id="btnPrint" runat="server" style="background-color: white; font-weight: bold;
                                  font-size: 11px;" type="button" value="Cetak" onclick="Cetak()" />
                          </td>
                          <td style="width: 70px">
                              <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                  <asp:ListItem Value="UserID">ID User</asp:ListItem>
                                  <asp:ListItem Value="UserName">Nama User</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td style="width: 70px">
                              <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                          </td>
                          <td style="width: 70px">
                              <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                  font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                          </td>
                      </tr>
                  </table>
            </td>
        </tr>
        <tr >
            <td style="width: 100%">
            <div class="content">
		        <table class="tblForm" id="Table4" style="width: 100%;">
                    <tr>
                        <td style="width: 197px;" valign="top">
                        </td>
                        <td style="width: 204px;" valign="top">
                        </td>
                        <td style="width: 169px;" valign="top">
                        </td>
                        <td style="width: 209px;" valign="top">
                        </td>
                        <td style="width: 205px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 197px; height: 6px" valign="top">
                            <span style="font-size: 10pt">&nbsp; ID User</span></td>
                        <td style="width: 204px; height: 6px" valign="top">
                            <asp:TextBox ID="txtUserID" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox></td>
                        <td style="height: 6px; width: 169px;" valign="top">                                                        
                        </td>
                        <td style="width: 209px; height: 6px" valign="top">
                            </td>
                        <td style="width: 205px; height: 6px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 197px; HEIGHT: 6px" valign="top">
                            <span style="font-size: 10pt">&nbsp; Nama User</span></td>
                        <td style="HEIGHT: 6px; width: 204px;" valign="top">
                            <asp:TextBox ID="txtUserName" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox></td>
                        <td style="height: 6px; width: 169px;" valign="top">                                                        
                        </td>
                        <td style="width: 209px; height: 6px" valign="top">
                        </td>
                        <td style="width: 205px; height: 6px" valign="top">
                        </td>
                    </tr>                                                        
                     <tr>
                        <td style="WIDTH: 197px; HEIGHT: 6px" valign="top">
                            <span style="font-size: 10pt">&nbsp; Tipe Unit Kerja</span></td>
                        <td style="HEIGHT: 6px; width: 204px;" valign="top">
                            <asp:DropDownList ID="ddlTypeUnitKerja" runat="server" Width="233px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlTypeUnitKerja_SelectedIndexChanged">
                            <asp:ListItem Value="0">-- Pilih Tipe Unit Kerja --</asp:ListItem>
                            <asp:ListItem Value="1">Distributor</asp:ListItem>
                            <asp:ListItem Value="2">Depo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 6px; width: 169px;" valign="top">                                                        
                        </td>
                        <td style="width: 209px; height: 6px" valign="top">
                        </td>
                        <td style="width: 205px; height: 6px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 197px; HEIGHT: 6px" valign="top">
                            <span style="font-size: 10pt">&nbsp; Unit Kerja</span></td>
                        <td style="HEIGHT: 6px; width: 204px;" valign="top">
                            <asp:DropDownList ID="ddlUnitKerja" runat="server" Width="233px">                                                     
                            </asp:DropDownList>
                        </td>
                        <td style="height: 6px; width: 169px;" valign="top">                                                        
                        </td>
                        <td style="width: 209px; height: 6px" valign="top">
                        </td>
                        <td style="width: 205px; height: 6px" valign="top">
                        </td>
                    </tr>
                     <tr>
                        <td style="WIDTH: 197px; HEIGHT: 6px" valign="top">
                            <span style="font-size: 10pt">&nbsp; Password</span></td>
                        <td style="HEIGHT: 6px; width: 204px;" valign="top">
                            <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Groove" Width="233" 
                                TextMode="Password"></asp:TextBox></td>
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
                <hr />
                <div id="div2" class="contentlist" style="height: 350px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                        OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="UserID" HeaderText="ID User" />
                            <asp:BoundField DataField="UserName" HeaderText="Nama User" />
                            <asp:BoundField DataField="TypeUnitKerja" HeaderText="Tipe Unit Kerja" />
                            <asp:BoundField DataField="UnitKerjaID" HeaderText="UnitKerja" />
                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                    <br />
                    
                </div> 
            </div>
            </td>
        </tr>
        </table>
     </div>
  </ContentTemplate>     
</asp:UpdatePanel>
</asp:Content>
