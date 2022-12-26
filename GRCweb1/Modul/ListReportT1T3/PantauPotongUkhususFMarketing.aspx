<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PantauPotongUkhususFMarketing.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.PantauPotongUkhususFMarketing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" style="width: 100%" class="table-responsive">
                <table style="width: 100%; border-collapse: collapse; font-size: small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        PEMANTAUAN PEMOTONGAN UKURAN KHUSUS DARI MARKETING
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:TextBox ID="txtCari" runat="server" Width="250px"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--content--->
                            <div >
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Periode
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlBulan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTahun_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 440px; overflow: auto" id="lst" runat="server"
                                    onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak">
                                                    No
                                                </th>
                                                <th class="kotak">
                                                    Partno
                                                </th>
                                                <th class="kotak">
                                                    Target (lembar)
                                                </th>
                                                 <th class="kotak">
                                                    Saldo Awal (lembar)
                                                </th>
                                                <th class="kotak">
                                                    Penerimaan (lembar)
                                                </th>
                                                <th class="kotak txtUpper">
                                                    Pengeluaran (lembar)
                                                </th>
                                                <th class="kotak txtUpper">
                                                    Sisa Stock
                                                </th>
                                                <th class="kotak txtUpper">
                                                    %
                                                </th>
                                                <th class="kotak txtUpper">
                                                    Keterangan
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tb" runat="server">
                                            <asp:Repeater ID="lstMatrix" runat="server" OnItemDataBound="lstMatrix_DataBound">
                                                <ItemTemplate>
                                                <tr id="lst" runat="server" class="EvenRows baris">
                                                    <td class="kotak tengah">
                                                        <%# Eval("Urutan") %>
                                                    </td>
                                                    <td class="kotak " >
                                                        <asp:TextBox ID="txtPartno" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Height="20px"  Width="100%"></asp:TextBox>
                                                        <cc1:autocompleteextender id="AutoCompleteExtender1" runat="server" completioninterval="200"
                                                            completionsetcount="10" enablecaching="true" firstrowselected="True" minimumprefixlength="1"
                                                            servicemethod="GetNoProduk" servicepath="AutoComplete.asmx" targetcontrolid="txtPartno">
                                                                                            </cc1:autocompleteextender>
                                                        <asp:HiddenField ID="txtNilaiID" runat="server" Value='<%# Eval("ID") %>' />
                                                    </td>
                                                    <td class="kotak tengah" >
                                                        <asp:TextBox ID="TxtTarget" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Width="100%" OnTextChanged="txtPartno_TextChanged" Text="0"></asp:TextBox>
                                                    </td>
                                                    <td class="kotak tengah OddRows">
                                                        <%# Eval("SA", "{0:N0}")%>
                                                    </td>
                                                    <td class="kotak tengah OddRows">
                                                        <%# Eval("Pencapaian", "{0:N0}")%>
                                                    </td>
                                                    <td class="kotak tengah OddRows">
                                                        <%# Eval("Customer", "{0:N0}")%>
                                                    </td>
                                                    <td class="kotak tengah OddRows">
                                                        <%# Eval("SisaStock", "{0:N0}")%>
                                                    </td>
                                                    <td class="kotak tengah OddRows">
                                                        <%# Eval("Persen", "{0:N2}")%>
                                                    </td>
                                                    <td class="kotak tengah">
                                                        <asp:Label ID="lblketerangan" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr>
                                            <td>&nbsp</td>
                                            </tr>
                                            <tr>
                                                <td> </td>
                                                <td rowspan="2">
                                                    <strong>
                                                    <asp:Label ID="lblpencapaian" runat="server"/></strong>
                                                </td>
                                                <td> <strong>
                                                    <asp:Label ID="lblpersen" runat="server"/></strong>
                                                </td>
                                                <td> <strong>
                                                    <asp:Label ID="lblnilai" runat="server"/></strong>
                                                </td>
                                                <td rowspan="4">
                                                </td>
                                            </tr>
                                            <%--<tr class="Line3 Baris" id="lstF" runat="server">
                                                <td colspan="3" class="kotak angka txtUpper">
                                                    <b>TOTAL</b>
                                                </td>
                                                <td class="kotak angka">
                                                    &nbsp;
                                                </td>
                                                <td class="kotak angka">
                                                    &nbsp;
                                                </td>
                                                <td class="kotak angka">
                                                    &nbsp;
                                                </td>
                                                <td class="kotak angka">
                                                    &nbsp;
                                                </td>
                                                <td class="kotak angka">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7">
                                                    &nbsp;
                                                </td>
                                            </tr>--%>
                                        </tbody>
                                        <tfoot>
                                            <%--<tr>
                                                <td >
                                                    &nbsp;
                                                </td>
                                                <td >
                                                    &nbsp;
                                                </td>
                                                <td  >
                                                    &nbsp;
                                                </td>
                                                <td >
                                                    &nbsp;
                                                </td>
                                                <td >
                                                    Dibuat,
                                                </td>
                                                <td >
                                                    Mengetahui
                                                </td>
                                                <td >
                                                    &nbsp;
                                                </td>
                                            </tr>--%>
                                            
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
      
    <div class="contentlist" style="height: 440px;display: none; overflow: auto" id="Div5" runat="server"
    onscroll="setScrollPosition(this.scrollTop);">
        <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
            <thead>
                <tr class="tbHeader">
                    <th class="kotak">
                        No
                    </th>
                    <th class="kotak">
                        Partno
                    </th>
                    <th class="kotak">
                        Target (lembar)
                    </th>
                    <th class="kotak">
                        Saldo Awal (lembar)
                    </th>
                    <th class="kotak">
                        Penerimaan (lembar)
                    </th>
                    <th class="kotak txtUpper">
                        Pengeluaran (lembar)
                    </th>
                    <th class="kotak txtUpper">
                        Sisa Stock
                    </th>
                    <th class="kotak txtUpper">
                        %
                    </th>
                    <th class="kotak txtUpper">
                        Keterangan
                    </th>
                </tr>
            </thead>
            <tbody id="Tbody1" runat="server">
                <asp:Repeater ID="lstMatrix2" runat="server" OnItemDataBound="lstMatrix2_DataBound">
                    <ItemTemplate>
                        <tr id="lst2" runat="server" class="EvenRows baris">
                            <td class="kotak tengah">
                                <%# Eval("Urutan") %>
                            </td>
                            <td class="kotak " >
                                <asp:Label ID="txtPartno" runat="server" 
                                Height="20px"  Width="100%"></asp:Label>
                                
                            </td>
                            <td class="kotak tengah" >
                                <asp:Label ID="TxtTarget" runat="server" 
                                Width="100%" ></asp:Label>
                            </td>
                            <td class="kotak tengah OddRows">
                                <%# Eval("SA", "{0:N0}")%>
                            </td>
                            <td class="kotak tengah OddRows">
                                <%# Eval("Pencapaian", "{0:N0}")%>
                            </td>
                            <td class="kotak tengah OddRows">
                                <%# Eval("Customer", "{0:N0}")%>
                            </td>
                            <td class="kotak tengah OddRows">
                                <%# Eval("SisaStock", "{0:N0}")%>
                            </td>
                            <td class="kotak tengah OddRows">
                                <%# Eval("Persen", "{0:N2}")%>
                            </td>
                            <td class="kotak tengah">
                                <asp:Label ID="lblketerangan" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>&nbsp</td>
                </tr>
                <tr>
                    <td> </td>
                    <td>
                        <strong>
                        <asp:Label ID="lblpencapaian2" runat="server"/></strong>
                    </td>
                    <td> <strong>
                        <asp:Label ID="lblpersen2" runat="server"/></strong>
                    </td>
                    <td> <strong>
                        <asp:Label ID="lblnilai2" runat="server"/></strong>
                    </td>
                    
                </tr>
            </tbody>
            <tfoot>
            </tfoot>
        </table>
    </div>
</asp:Content>
