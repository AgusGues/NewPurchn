<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KasbonRealisasiApproval.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KasbonRealisasiApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>

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
        label{font-size:12px;}
        table,tr,td{background-color: #fff;}
    </style>

    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
<%--<script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>--%>
<script type="text/javascript">
    function onKeyUp() {
        $("#<%=txtNotApproved.ClientID %>").keyup(function() {
            if ($(this).val().length == 2) {
                $("#<%=btnNotApprove.ClientID %>").removeAttr("disabled");
                $("#<%=btnApprove.ClientID %>").attr("disabled", "true");
            }
            if ($(this).val().length == 0) {
                $("#<%=btnNotApprove.ClientID %>").attr("disabled", "false");
                $("#<%=btnApprove.ClientID %>").removeAttr("disabled");
            }
        });
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="div1" runat="server">
            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <tr>
                    <td style="width:100%;height:49px">
                        <table class="nbTableHeader">
                            <tr style="">
                                <td style="width:15%; padding-left:15px">
                                    <b>APPROVAL REALISASI KASBON</b>
                                </td>
                                <td style="width:85%; padding-right:10px; text-align:right">
                                    <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" OnClick="btnPrev_Click" />
                                    <asp:Button ID="btnApprove" runat="server" Text="Approved" OnClick="btnApprove_Click" />
                                    <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnNotApprove" runat="server" Text="Not Approved" Enabled="false" OnClick="btnNotApprove_Click" />
                                    <%--<asp:Button ID="btnCancel" runat="server" Text="List PO" Enabled="true" OnClick="btnList_ServerClick" />--%>
                                    &nbsp;
                                    <asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor Kasbon" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Nomor PO"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                    <asp:HiddenField ID="noKasbon" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="content" style="padding-top:5px;" style="background-color: #fff;">
                            <table class="tbStandart">
                                <tr>
                                    <td class="FormLabel">Tanggal</td>
                                    <td class="FormField">
                                        <%--<asp:TextBox ID="txtTglKasbon" runat="server" ReadOnly="true" Width="80%"></asp:TextBox>--%>
                                        <bdp:BDPLite ID="txtTglKasbon" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Width="95%" style="margin-left: 86px">
                                                </bdp:BDPLite>
                                    </td>
                                    <td class="FormLabel">No.Kasbon</td>
                                    <td class="FormField"><asp:TextBox ID="txtNoKasbon" runat="server" ReadOnly="true" Width="80%"></asp:TextBox>
                                    <asp:TextBox ID="txtCount" runat="server" ReadOnly="true" Style="font-family: Calibri; font-size: x-small; color: #000000;" Width="38px" Visible="false"></asp:TextBox>
                                    </td>
                                    <td class="FormField">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="FormLabel">Dept</td>
                                    <td class="FormField"><asp:TextBox ID="txtDept" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                    <td class="FormLabel">PIC</td>
                                    <td class="FormField"><asp:TextBox ID="txtPic" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                    <td class="FormField"><asp:TextBox ID="txtKasbonType" runat="server" Width="100%" Visible="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="FormLabel">Dana Cadangan</td>
                                    <td class="FormField"><asp:TextBox ID="txtDanaCadangan" runat="server" ReadOnly="true" Width="80%"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="FormLabel" style="vertical-align:top">Alasan Not Approved</td>
                                    <td class="FormField" colspan="2">
                                    <asp:TextBox TextMode="MultiLine" Rows="3" ID="txtNotApproved" runat="server" Width="70%" AutoPostBack="false" OnTextChanged="txtNotApproved_Change"></asp:TextBox></td>
                                    <td class="FormField">Approval Status : <b><asp:Label ID="txtStatus" runat="server"></asp:Label></b></td>
                                </tr>
                            </table>
                            <div class="contentlist" style="height:250px">
                                <table class="tbStandart">
                                   <thead>
                                    <tr class="tbHeader">
                                        <th style="width:3%" class="kotak">No.</th>
                                        <th style="width:5%" class="kotak">No.SPP</th>
                                        <th style="width:6%" class="kotak">Item Code</th>
                                        <th style="width:30%" class="kotak">Item Name</th>
                                        <th style="width:4%" class="kotak">Unit</th>
                                        <th style="width:5%" class="kotak">Qty SPP</th>
                                        <th style="width:6%" class="kotak">Estimasi Kasbon</th>
                                        <th style="width:6%" class="kotak">Total Estimasi</th>
                                        <th style="width:5%" class="kotak">No.PO</th>
                                        <th style="width:5%" class="kotak">Qty PO</th>
                                        <th style="width:6%" class="kotak">Harga PO</th>
                                        <th style="width:10%" class="kotak">Total PO</th>
                                        <th style="width:8%" class="kotak">Selisih</th>
                                        <%--<th style="width:6%" class="kotak">Deliv Date</th>--%>
                                        <%--<th style="width:3%" class="kotak">&nbsp;</th>
                                        <th style="width:3%" class="kotak">&nbsp;</th>--%>
                                    </tr>
                                   </thead> 
                                   <tbody>
                                    <asp:Repeater ID="lstItemKasbon" runat="server" OnItemDataBound="lstItemKasbon_DataBound">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak tengah"><%# Eval("NoSPP")%></td>
                                                <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                <td class="kotak"><%# Eval("NamaBarang")%></td>
                                                <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                                <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                <td class="kotak angka"><%# Eval("EstimasiKasbon","{0:N2}") %></td>
                                                <td class="kotak angka"><asp:Label ID="total" runat="server"></asp:Label></td>
                                                <td class="kotak tengah"><%# Eval("NoPO")%></td>
                                                <td class="kotak angka"><%# Eval("QtyPO","{0:N2}") %></td>
                                                <td class="kotak angka"><%# Eval("Price","{0:N2}") %></td>
                                                <td class="kotak angka xx"><asp:Label ID="totalPO" runat="server"></asp:Label></td>
                                                <td class="kotak angka xx"><asp:Label ID="selisih" runat="server"></asp:Label></td>
                                                <%--<td class="kotak tengah" id="dlv"><%# Eval("DlvDate","{0:d}") %></td>
                                                <td class="kotak tengah">
                                                <asp:ImageButton ID="showHis" runat="server" ImageUrl="~/images/dtpJavaScript.gif" CommandArgument='<%# Eval("ItemName") %>' CommandName='
                                                <%# Eval("ItemTypeID") %>' ToolTip="Click For Show History & Stock Item" />
                                               
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td class="kotak Line3 angka" colspan="7">Sub Total</td>
                                                <td class="kotak angka Line3"><asp:Label ID="gTotal" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka" colspan="3">PPN</td>
                                                <td class="kotak angka Line3"><asp:Label ID="Ppn" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="bPPN" runat="server">
                                                <td class="kotak Line3 angka" colspan="7">Dana Cadangan</td>
                                                <td class="kotak Line3 angka"><asp:Label ID="gDC" runat="server"></asp:Label></td>
                                                <%--<td class="kotak Line3 angka" colspan="4"></td>--%>
                                                <td class="kotak Line3 angka" colspan="3">Ongkos Kirim</td>
                                                <td class="kotak angka Line3"><asp:Label ID="OngkosKirim" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="kotak Line3 angka" colspan="7">Total Kasbon</td>
                                                <td class="kotak angka Line3"><asp:Label ID="grnTotal" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka" colspan="3">Total PO</td>
                                                <td class="kotak angka Line3"><asp:Label ID="TotalPO" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="kotak Line3 angka" colspan="8"></td>
                                                <td class="kotak Line3 angka" colspan="3">Total Kekurangan/Kelebihan Kasbon</td>
                                                <td class="kotak angka Line3"><asp:Label ID="TotalAll" runat="server"></asp:Label></td>                                                
                                            </tr>
                                            
                                            
                                        </FooterTemplate>
                                    </asp:Repeater>
                                   </tbody>
                                </table>
                                
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtLapakID" runat="server" Visible="false">></asp:TextBox>
            <asp:TextBox ID="txtKirimanID" runat="server" Visible="false"></asp:TextBox>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
