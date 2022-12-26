<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanPacking.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.PemantauanPacking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
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
            <div id="div1" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;
                                            PEMANTAUAN HASIL PACKING </b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
                                            onclick="btnExport_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" 
                                            onclick="btnSimpan_Click"  />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                                            onclick="btnApprove_Click"  />
                                        <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                         <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content" id="input" runat="server">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse;
                                    font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">
                                            &nbsp;&nbsp;&nbsp;&nbsp; Tanggal :
                                        </td>
                                        <td style="width: 15%">
                                          <asp:TextBox ID="txttanggal" runat="server" 
                                                ontextchanged="txttanggal_TextChanged"></asp:TextBox>
                                          <cc1:CalendarExtender
                                                            ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttanggal"
                                                            Enabled="True">
                                                        </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                        &nbsp;
                                        <asp:Button ID="Preview" runat="server" Text="Preview" onclick="Preview_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 750px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                        id="inputpacking">
                                        <thead>
                                            <tr class="tbHeader">
                                                
                                                <th rowspan="3" style="width: 3%" class="kotak">
                                                    No.
                                                </th>
                                                <th rowspan="3" colspan="2" style="width: 5%" class="kotak">
                                                   Jenis Ukuran
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Target/Palet
                                                </th>
                                                <th style="width: 10%"  colspan="8" class="kotak">
                                                    Pencapaian (Palet)
                                                </th>
                                                <th style="width: 10%" rowspan="3" colspan="2" class="kotak">
                                                    Jenis Ukuran
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Target/Palet
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Pencapaian (Palet)
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">  
                                                
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                <asp:Label ID="targetjamkrwga" runat="server" Text="Per jam   6 0rang" Visible="false"></asp:Label>
                                                <asp:Label ID="targetjamctrpa" runat="server" Text="Per jam   6 0rang" Visible="false"></asp:Label>
                                                <asp:Label ID="targetjamjmbga" runat="server" Text="Per jam   6 0rang" Visible="false"></asp:Label>
                                                </th>
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                <asp:Label ID="targetshiftkrwga" runat="server" Text="Per shift  (7 jam)" Visible="false"></asp:Label>
                                                <asp:Label ID="targetshiftctrpa" runat="server" Text="Per shift  (7 jam)" Visible="false"></asp:Label>
                                                <asp:Label ID="targetshiftjmbga" runat="server" Text="Per shift  (8 jam)" Visible="false"></asp:Label>
                                                
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                    Group A
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                    Group B
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group C
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group D
                                                </th>
                                                 <th rowspan="2" style="width: 5%" class="kotak">
                                                 <asp:Label ID="targetjamkrwgb" runat="server" Text="Per jam   6 0rang" Visible="false"></asp:Label>
                                                <asp:Label ID="targetjamctrpb" runat="server" Text="Per jam   6 0rang" Visible="false"></asp:Label>
                                                <asp:Label ID="targetjamjmbgb" runat="server" Text="Per jam   4 0rang" Visible="false"></asp:Label>
                                                </th>
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                <asp:Label ID="targetshiftkrwgb" runat="server" Text="Per shift (7 jam)" Visible="false"></asp:Label>
                                                <asp:Label ID="targetshiftctrpb" runat="server" Text="Per shift (7 jam)" Visible="false"></asp:Label>
                                                <asp:Label ID="targetshiftjmbgb" runat="server" Text="Per shift (8 jam)" Visible="false"></asp:Label>
                                                    
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group E
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstType" runat="server" 
                                                onitemdatabound="lstType_ItemDataBound" >
                                                <ItemTemplate>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="19" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#00CC00">
                                                            <%# Eval("JenisPacking") %>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="lstpacking" runat="server" 
                                                    OnItemDataBound="lstpacking_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" id="ps1" style="font-weight: bold" runat="server">
                                                                
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                   <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                </td>
                                                                <td class="kotak tengah " nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("JenisUkuran1") %>
                                                                 <asp:Label ID="lblid" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                  <%# Eval("JenisUkuranNo1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                  <%# Eval("TargetJam1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("TargetShift1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtTargetA" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TargetA", "{0:N2}").ToString()%>'
                                                                        AutoPostBack="false" Width="100%" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualA" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("ActualA", "{0:N2}") %>'
                                                                        Width="100%"  AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtTargetB" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TargetB", "{0:N2}").ToString()%>'
                                                                        AutoPostBack="false"  Width="100%" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualB" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("ActualB", "{0:N2}") %>'
                                                                        Width="100%"  AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtTargetC" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TargetC", "{0:N2}").ToString()%>'
                                                                        AutoPostBack="false"  Width="100%" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualC" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("ActualC", "{0:N2}") %>'
                                                                        Width="100%" AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtTargetD" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TargetD", "{0:N2}").ToString()%>'
                                                                        AutoPostBack="false"  Width="100%" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualD" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("ActualD", "{0:N2}") %>'
                                                                        Width="100%"  AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("JenisUkuran2")%>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("JenisUkuranNo2")%>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("TargetJam2")%>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width:5%">
                                                                <%# Eval("TargetShift2")%>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtTargetE" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TargetE", "{0:N2}").ToString()%>'
                                                                        AutoPostBack="false"  Width="100%" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualE" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("ActualE", "{0:N2}") %>'
                                                                        Width="100%"  AutoPostBack="false" onfocus="this.select()"></asp:TextBox>
                                                                 </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                               </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="total baris">
                                                <td class="kotak bold angka" colspan="4">
                                                    &nbsp;
                                                </td>
                                                <td class="kotak tengah">
                                                    hasil
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetA" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalA" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetB" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalB" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetC" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalC" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetD" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalD" runat="server"/></strong>
                                                </td>   
                                                <td class="kotak bold angka" colspan="3">
                                                </td>
                                                <td class="kotak tengah">
                                                    hasil
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetE" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalE" runat="server"/></strong>
                                                </td>                                                
                                            </tr>  
                                            <tr class="total baris">
                                                <td class="kotak bold angka" colspan="4">&nbsp;
                                                </td>
                                                <td class="kotak tengah">
                                                    %
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenA" runat="server"/>
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenB" runat="server"/>
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenC" runat="server"/>
                                                </td >
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenD" runat="server"/>
                                                </td >
                                                <td class="kotak bold angka" colspan="3">
                                                </td>
                                                <td class="kotak tengah">
                                                    %
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenE" runat="server"/>
                                                </td>
                                            </tr>
                                             <tr class="total baris">
                                                <td class="kotak bold angka" colspan="3">&nbsp;
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Jam Kerja
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamA" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    jam
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamB" runat="server"/>
                                                </td>
                                                 <td class="kotak tengah">
                                                    jam
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamC" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    jam
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamD" runat="server"/>
                                                </td>
                                                 <td class="kotak tengah">
                                                    jam
                                                </td>
                                                <td class="kotak bold angka" colspan="2">
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Jam Kerja
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamE" runat="server"/>
                                                </td>
                                                 <td class="kotak tengah">
                                                    jam
                                                </td>
                                             </tr> 
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="19">
                                                &nbsp
                                                </td>
                                             </tr>   
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Actual Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalActual" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    Palet    
                                                </td>
                                                <td colspan="12">
                                                
                                                </td>
                                             </tr>       
                                              <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Target Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalTarget" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    Palet    
                                                </td>
                                                <td colspan="12">
                                                
                                                </td>
                                             </tr>    
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp    
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Pencapaian Hasil Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalAll" runat="server"/>
                                                </td>
                                                <td colspan="13">
                                                
                                                </td>
                                             </tr>          
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="content" id="aprove" runat="server">
                                <table id="apv" runat="server" style="width: 100%; border-collapse: collapse;
                                    font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">
                                            &nbsp;&nbsp;&nbsp;&nbsp;Bulan:
                                        </td>
                                        <td style="width: 15%">
                                            &nbsp;<asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlBulan_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTahun_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                        <td >
                                            &nbsp;<asp:Button ID="Preview2" runat="server" Text="Preview" 
                                                onclick="Preview2_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div ID="div3" class="contentlist" style="overflow: scroll; height: 700px; width: 100%;"  runat="server">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0" 
                                        id="tblapv">
                                        <thead>
                                            <tr class="tbHeader">
                                                
                                                <th rowspan="3" style="width: 3%" class="kotak">
                                                    No.
                                                </th>
                                                <th rowspan="3" colspan="2" style="width: 5%" class="kotak">
                                                   Jenis Ukuran
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Target/Palet
                                                </th>
                                                <th style="width: 10%"  colspan="8" class="kotak">
                                                    Pencapaian (Palet)
                                                </th>
                                                <th style="width: 10%" rowspan="3" colspan="2"  class="kotak">
                                                    Jenis Ukuran
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Target/Palet
                                                </th>
                                                <th style="width: 10%"  colspan="2" class="kotak">
                                                    Pencapaian (Palet)
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">  
                                                
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                    Per jam   6 0rang
                                                </th>
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                    Per shift  (7 jam)
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                    Group A
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                    Group B
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group C
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group D
                                                </th>
                                                 <th rowspan="2" style="width: 5%" class="kotak">
                                                    Per jam 6 0rang
                                                </th>
                                                <th rowspan="2" style="width: 5%" class="kotak">
                                                    Per shift (7 jam)
                                                </th>
                                                <th colspan="2" style="width: 5%" class="kotak">
                                                   Group E
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Target
                                                </th>
                                                <th  style="width: 5%" class="kotak">
                                                    Aktual
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstType2" runat="server" 
                                                onitemdatabound="lstType2_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="19" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#00CC00">
                                                            <%# Eval("JenisPacking") %>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="lstpacking2" runat="server" 
                                                    onitemdatabound="lstpacking2_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" id="ps2" style="font-weight: bold" runat="server">
                                                                
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                   <asp:Label ID="lblRowNumber2" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                                </td>
                                                                <td class="kotak tengah " nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("JenisUkuran1") %>
                                                                 <asp:Label ID="lblidr" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                  <%# Eval("JenisUkuranNo1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                  <%# Eval("TargetJam1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("TargetShift1") %>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("TargetA", "{0:N2}")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("ActualA", "{0:N2}") %>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("TargetB", "{0:N2}")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("ActualB", "{0:N2}") %>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("TargetC", "{0:N2}")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("ActualC", "{0:N2}") %>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("TargetD", "{0:N2}")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("ActualD", "{0:N2}") %>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("JenisUkuran2")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("JenisUkuranNo2")%>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                 <%# Eval("TargetJam2")%>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="width:5%">
                                                                <%# Eval("TargetShift2")%>
                                                                </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("TargetE", "{0:N2}")%>
                                                                 </td>
                                                                 <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <%# Eval("ActualE", "{0:N2}") %>
                                                                 </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="total baris">
                                                <td class="kotak bold angka" colspan="4">
                                                &nbsp;
                                                </td>
                                                <td class="kotak tengah">
                                                    hasil
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetAr" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalAr" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetBr" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalBr" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetCr" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalCr" runat="server"/></strong>
                                                </td>  
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetDr" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalDr" runat="server"/></strong>
                                                </td>   
                                                <td class="kotak bold angka" colspan="3">
                                                </td>
                                                <td class="kotak tengah">
                                                    hasil
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalTargetEr" runat="server"/></strong>
                                                </td>
                                                <td class="kotak bold angka">
                                                    <strong>
                                                    <asp:Label ID="lblTotalAcutalEr" runat="server"/></strong>
                                                </td>                                                
                                            </tr>  
                                            <tr class="total baris">
                                                <td class="kotak bold angka" colspan="4">&nbsp;
                                                </td>
                                                <td class="kotak tengah">
                                                    %
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenAr" runat="server"/>
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenBr" runat="server"/>
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenCr" runat="server"/>
                                                </td >
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenDr" runat="server"/>
                                                </td >
                                                <td class="kotak bold angka" colspan="3">
                                                </td>
                                                <td class="kotak tengah">
                                                    %
                                                </td>
                                                <td class="kotak bold angka">&nbsp;
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblprsenEr" runat="server"/>
                                                </td>
                                            </tr>
                                             <tr class="total baris">
                                                <td class="kotak bold angka" colspan="3">&nbsp;
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Jam Kerja
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamAr" runat="server"/>
                                                </td>
                                                <td class="otak tengah">
                                                    jam
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamBr" runat="server"/>
                                                </td>
                                                 <td class="otak tengah">
                                                    jam
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamCr" runat="server"/>
                                                </td>
                                                <td class="otak tengah">
                                                    jam
                                                </td>
                                                 <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamDr" runat="server"/>
                                                </td>
                                                 <td class="otak tengah">
                                                    jam
                                                </td>
                                                <td class="kotak bold angka" colspan="2">
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Jam Kerja
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="lblTotalJamEr" runat="server"/>
                                                </td>
                                                 <td class="otak tengah">
                                                    jam
                                                </td>
                                             </tr>
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="19">
                                                &nbsp
                                                </td>
                                             </tr >   
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Actual Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalActual2" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    Palet    
                                                </td>
                                                <td colspan="12">
                                                </td>
                                             </tr>       
                                              <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Total Target Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalTarget2" runat="server"/>
                                                </td>
                                                <td class="kotak tengah">
                                                    Palet    
                                                </td>
                                                <td colspan="12">
                                                </td>
                                             </tr>  
                                             <tr class="total baris">
                                                <td class="kotak tengah" colspan="3">
                                                    &nbsp    
                                                </td>
                                                <td class="kotak tengah" colspan="2">
                                                    Pencapaian Hasil Packing
                                                </td>
                                                <td class="kotak bold angka">
                                                    <asp:Label ID="TotalAll2" runat="server"/>
                                                </td>
                                                <td colspan="13">
                                                </td>
                                             </tr>   
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
