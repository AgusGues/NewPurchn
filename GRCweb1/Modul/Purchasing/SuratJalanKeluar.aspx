<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuratJalanKeluar.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.SuratJalanKeluar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#div2").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function Cetak() {
            MyPopUpWin("../Report/Report.aspx?IdReport=SlipSJK", 900, 800)
        }
        //function Cetak() {
        //    var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipSJK", "", "resizable:yes;dialogheight: 600px; dialogWidth: 900px;scrollbars=yes");
        //}
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           <div id="Div1" runat="server">
             <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 30%; padding-left:5px">
                                            <strong>&nbsp; INPUT SURAT JALAN  </strong>
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                        <td style="width: 70%; padding-right:5px" align="right">
                                            <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_serverClick" />
                                            <asp:Button ID="btnCetak" runat="server" OnClientClick="Cetak();" 
                                                Text="Cetak" />
                                            <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_ServerClick" />
                                            <asp:Button ID="rekapList" runat="server" Text="Rekap" OnClick="btnRekap_ServerClick" />
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="NoSJ">No Surat Jalan</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" Text="Cari" OnClick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                            
                                <div class="content">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <td style="width: 15%;">&nbsp</td>
                                            <td style="width: 25%">&nbsp</td>
                                            <td style="width: 10%;">&nbsp</td>
                                            <td style="width: 20%;">&nbsp</td>
                                            <td style="width: 10%;">&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">&nbsp;Tanggal&nbsp;</td>
                                            <td><asp:TextBox ID="txtTglSJ" runat="server" Width="192px"></asp:TextBox></td>
                                            <td><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTglSJ" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                             </td><td style="width: 20%;">&nbsp</td>
                                            <td style="width: 10%;">&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">&nbsp;No Surat Jalan&nbsp;</td>
                                            <td><asp:TextBox ID="txtNoSJ" runat="server" BorderStyle="Groove" Width="80%" BackColor="#afacac" ReadOnly="True"></asp:TextBox></td>
                                            <td>&nbsp</td>
                                            <td style="width: 20%;">&nbsp</td>
                                            <td style="width: 10%;">&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">&nbsp;Tipe Barang&nbsp;</td>
                                            <td>
                                            <asp:DropDownList ID="ddlTipeBarang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipeBarang_SelectedIndexChanged"
                                                Width="50%">
                                            </asp:DropDownList></td>
                                            <td>&nbsp</td>
                                            <td style="width: 20%;">&nbsp</td>
                                            <td style="width: 10%;">&nbsp</td>
                                        </tr>
                                        <tr id="dll" runat="server" visible="false">
                                            <td style="width: 15%" valign="top">&nbsp;Nama Material&nbsp;</td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNamaMaterial" runat="server" Width="100%" TextMode="MultiLine" Rows="3" ></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="cB" runat="server">
                                            <td style="width: 15%">
                                                &nbsp;Cari Nama Material&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtCariNamaBrg" runat="server" AutoPostBack="True" Width="100%" ></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr id="cb1" runat="server">
                                            <td style="width: 15%">
                                                &nbsp;Pilih Material&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 15%">&nbsp;Jumlah&nbsp; 
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtQty" runat="server" Width="80px"></asp:TextBox> 
                                                &nbsp;&nbsp;&nbsp;&nbsp;No Polisi&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:TextBox ID="txtNopol" runat="server" Width="80px" onkeyup="this.value=this.value.toUpperCase()">
                                               </asp:TextBox></td>
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">&nbsp;Satuan&nbsp;</td>
                                            <td>
                                            <asp:DropDownList ID="ddlSatuan" runat="server" AutoPostBack="true"
                                                Width="50%">
                                            </asp:DropDownList>
                                            </td>
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                        </tr>
                                         <tr>
                                            <td style="width: 15%" valign="top">&nbsp;Tujuan&nbsp;</td>
                                            <td colspan="2">
                                               <asp:TextBox ID="txtTujuan" runat="server" BorderStyle="Groove" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                                
                                            </td>
                                            
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%" valign="top">&nbsp;Keterangan&nbsp;</td>
                                            <td colspan="2">
                                               <asp:TextBox ID="txtKet" runat="server" BorderStyle="Groove" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td valign="bottom">&nbsp<asp:Button ID="lbAddOP" runat="server" Text="Add Item" OnClick="lbAddOP_Click" /></td>
                                            <td>&nbsp<asp:HiddenField ID="txtID" runat="server" Value="0" /></td>
                                        </tr>
                                        
                                        
                                        <tr>
                                            <td style="width: 15%" valign="top">&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td valign="bottom">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        
                                        
                                    </table>
                                    <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                <asp:BoundField DataField="Tujuan" HeaderText="Tujuan" />
                                                <asp:BoundField DataField="Jumlah" HeaderText="Jumlah" />
                                                <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                                <asp:BoundField DataField="Ket" HeaderText="Keterangan" />
                                                <asp:BoundField DataField="NoPolisi" HeaderText="No Polisi" />
                                               
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    <div class="contentlist" style="height:200px" onscroll="setScrollPosition(this.scrollTop);" id="div2">
                                        <!--New grid-->
                                        <table class="tbStandart">
                                            <thead>
                                                <tr class="tbHeader" style="height:25px">
                                                    <th class="kotak" style="width: 2%"> No.</th>
                                                    <th class="kotak" style="width: 15%">Nama Barang</th>
                                                    <th class="kotak" style="width: 15%">Tujuan</th>
                                                    <th class="kotak" style="width: 3%">Qty</th>
                                                    <th class="kotak" style="width: 3%">Satuan</th>
                                                    <th class="kotak" style="width: 12%">Keterangan</th>
                                                    <th class="kotak" style="width: 5%">No Polisi</th>
                                                    <th class="kotak" style="width: 3%">&nbsp;</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstSJ" runat="server" OnItemCommand="lstSJ_Command" OnItemDataBound="lstSJ_Databound">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="trd" runat="server">
                                                            <td class="kotak tengah" ><%# Container.ItemIndex+1  %></td>
                                                            <td class="kotak" ><%# Eval("ItemName") %></td>
                                                            <td class="kotak" ><%# Eval("Tujuan")%></td>
                                                            <td class="kotak tengah"><%# Eval("Jumlah")%></td>
                                                            <td class="kotak tengah"><%# Eval("Satuan")%></td>
                                                            <td class="kotak"><%# Eval("Ket")%></td>
                                                            <td class="kotak tengah"><%# Eval("NoPolisi")%></td>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandName="edit" CommandArgument='<%# Eval("ID") %>' ToolTip="Edit Data" />
                                                                <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandName="dele" CommandArgument='<%# Container.ItemIndex %>' ToolTip="Hapus Data" />
                                                                <%-- <asp:ImageButton ID="dels" runat="server" ImageUrl="~/images/Delete.png" CommandName="delet" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
           </table>
      </div>
 </ContentTemplate> 
 </asp:UpdatePanel>     
</asp:Content>
