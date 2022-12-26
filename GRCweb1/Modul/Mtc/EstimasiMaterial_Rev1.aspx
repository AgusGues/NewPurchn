<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstimasiMaterial_Rev1.aspx.cs" Inherits="GRCweb1.Modul.MTC.EstimasiMaterial_Rev1" %>
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
    </style>

    <script type="text/javascript">
    $(document).ready(function() {
        maintainScrollPosition();
    });
    function pageLoad() {
        maintainScrollPosition();
    }
    function maintainScrollPosition() {
        $("#<%=lst.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
    }
    function setScrollPosition(scrollValue) {
        $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
    }    
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

    <ContentTemplate>
        <div id="div1">
            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <tr>
                    <td style="width:100%">
                        <table class="nbTableHeader" style="width:100%; border-collapse:collapse; font-size:x-small">
                            <tr style="height:49px">
                                <td style="width:20%">&nbsp;&nbsp;<span style="font-family: Calibri"><b>Estimasi 
                                    Material</b></span></td>
                                <td style="width:70%; padding-right:10px" align="right">
                                    <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" 
                                        style="font-family: Calibri" />
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" 
                                        OnClick="btnSimpan_Click" style="font-family: Calibri" />
                                    <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_Click" 
                                        style="font-family: Calibri" />
                                    <asp:TextBox ID="txtCari" Width="200px" Text="Find by No.Improvement" 
                                        onfocus="if(this.value==this.defaultValue)this.value='';" 
                                        onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" 
                                        placeholder="Find by No.Improvement" style="font-family: Calibri"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" 
                                        style="font-family: Calibri" />
                                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="content" style="background:#fff;">
                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr>
                                    <td style="width:2%">&nbsp;</td>
                                    <td style="width:15%; font-family: Calibri;">No.Improvement</td>
                                    <td style="width:25%"><asp:TextBox ID="txtNomor" runat="server" Width="80%" 
                                            AutoPostBack="true" OnTextChanged="txtNomor_Change" 
                                            style="font-family: Calibri"></asp:TextBox></td>
                                    <td style="width:5%"><asp:HiddenField ID="txtProjectID" runat="server" /></td>
                                    <td style="width:10%">
                                        <cc1:AutoCompleteExtender ID="autocpl" runat="server" TargetControlID="txtNomor" CompletionSetCount="10"
                                         ServicePath="AutoComplete.asmx" ServiceMethod="GetListProjectNo" MinimumPrefixLength="2" 
                                         FirstRowSelected="true" EnableCaching="true"></cc1:AutoCompleteExtender>
                                    </td>
                                    <td style="width:25%"><asp:HiddenField ID="txtFinishDate" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Nama Improvement</td>
                                    <td colspan="3"><asp:TextBox ID="txtNamaImprovement" runat="server" 
                                            TextMode="MultiLine" Width="100%" style="font-family: Calibri"></asp:TextBox></td>
                                    <td><asp:Label ID="lblFinish" runat="server"></asp:Label><asp:HiddenField ID="txtID" runat="server" Value="0" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Type Barang</td>
                                    <td><asp:DropDownList ID="ddlMaterialType" runat="server" Width="60%" 
                                            style="font-family: Calibri"></asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Cari Material</td>
                                    <td colspan="3"><asp:TextBox ID="txtNamaBarang" runat="server" Width="100%" 
                                            AutoPostBack="true" OnTextChanged="txtNamaBarang_Change" 
                                            style="font-family: Calibri"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Pilih Material</td>
                                    <td colspan="4"><asp:DropDownList ID="ddlNamaBarang" AutoPostBack="true" 
                                            runat="server" Width="100%" OnSelectedIndexChanged="ddlNamaBarang_Change" 
                                            style="font-family: Calibri"></asp:DropDownList></td>
                                    <%--<td>&nbsp;</td>--%>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Jumlah</td>
                                    <td><asp:TextBox ID="txtJumlah" runat="server" style="font-family: Calibri"></asp:TextBox>&nbsp;<asp:Label ID="txtUom" runat="server"></asp:Label></td>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Harga PO terakhir</td>
                                    <td><asp:TextBox ID="txtHarga" runat="server" Width="50%" 
                                            style="font-family: Calibri"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td style="font-family: Calibri">Schedule Pemakaian</td>
                                    <td><asp:TextBox ID="txtSchedule" runat="server" style="font-family: Calibri"></asp:TextBox></td>
                                    <td><cc1:CalendarExtender ID="CA" TargetControlID="txtSchedule" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender></td>
                                    <td style="font-family: Calibri">Tanggal PO</td>
                                    <td><asp:TextBox ID="txtTglPO" runat="server" style="font-family: Calibri"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="btnAddItem" runat="server" Text="Add Item" 
                                            OnClick="btnAddItem_Click" style="font-family: Calibri" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update Item" 
                                            OnClick="btnUpdate_Click" Visible="false" style="font-family: Calibri" />
                                    </td>
                                    
                                </tr>
                            </table>
                            <asp:Label ID="txtMessage" runat="server" ForeColor="Red"></asp:Label>
                            <div class="contentlist" style="height:280px" onscroll="setScrollPosition(this.scrollTop);" id="lst" runat="server">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width:3%; font-family: Calibri;">No.</th>
                                        <th class="kotak" style="width:10%; font-family: Calibri;">Item Code</th>
                                        <th class="kotak" style="width:40%; font-family: Calibri;">Item Name</th>
                                        <th class="kotak" style="width:5%; font-family: Calibri;">Unit</th>
                                        <th class="kotak" style="width:8%; font-family: Calibri;">Quantity</th>
                                        <th class="kotak" style="width:8%; font-family: Calibri;">Harga</th>
                                        <th class="kotak" style="width:8%; font-family: Calibri;">TotalHarga</th>
                                        <th class="kotak" style="width:10%; font-family: Calibri;">Sch. Pakai</th>
                                        <th class="kotak" style="width:5%">&nbsp;</th>
                                        <th class="" style="background-color:Transparent">&nbsp;</th>
                                    </tr>
                                    <tbody style="font-family: Calibri">
                                        <asp:Repeater ID="lstMaterial" runat="server" OnItemDataBound="lstMaterial_DataBound" OnItemCommand="lstMaterial_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris" id="xx" runat="server" title='<%# Eval("ItemName") %>'>
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                    <td class="Kotak tengah"><%# Eval("UomCode") %></td>
                                                    <td class="kotak angka"><%# Eval("Jumlah","{0:N2}") %></td>
                                                    <td class="kotak angka"><%# Eval("Harga","{0:N2}") %></td>
                                                    <td class="kotak angka"><%# Eval("TotalHarga","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Schedule","{0:d}") %></td>
                                                    <td class="kotak tengah">
                                                        <asp:ImageButton ID="edt" ToolTip="Edit Data" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%#Eval("ID") %>' CommandName="edit" />
                                                        <asp:ImageButton ID="del" ToolTip="Hapus Data" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%#Eval("ID") %>' CommandName="del" />
                                                        <asp:ImageButton ID="clo" ToolTip="Hapus Data" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Container.ItemIndex %>' CommandName="hps" />
                                                    </td>
                                                    <td style="background-color:Transparent">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
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
