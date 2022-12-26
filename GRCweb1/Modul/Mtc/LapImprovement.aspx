<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapImprovement.aspx.cs" Inherits="GRCweb1.Modul.MTC.LapImprovement" %>
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
            <div id="div1" runat="server">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%">
                            <table class="nbTableHeader" width="100%'">
                                <tr style="height: 49px">
                                    <td style="width: 50%">
                                        <strong>&nbsp;List Improvement</strong>
                                    </td>
                                    <td style="width:50%; padding-right:10px" align="right">
                                        <asp:Button ID="btnToForm" runat="server" Text="Form Input" OnClick="btnToForm_Click" />
                                        <asp:TextBox ID="txtCari" Width="200px" Text="Find by Improvement" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Improvement"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr>
                                        <td style="width:3%"><asp:HiddenField ID="txtUrl" runat="server" /></td>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:15%">&nbsp;</td>
                                        <td style="width:3%">&nbsp;</td>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:15%">&nbsp;</td>
                                        <td style="width:5%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Status Project</td>
                                        <td><asp:DropDownList ID="ddlStatus" runat="server">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="0">Open</asp:ListItem>
                                                <asp:ListItem Value="2">Release</asp:ListItem>
                                                <asp:ListItem Value="21">Finish</asp:ListItem>
                                                <asp:ListItem Value="3">Close</asp:ListItem>
                                                <asp:ListItem Value="4">Pending</asp:ListItem>
                                                <asp:ListItem Value="-1">Cancel</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Dept. Pemohon</td>
                                        <td><asp:DropDownList ID="ddlDeptName" runat="server">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td><asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height:400px" onscroll="setScrollPosition(this.scrollTop);" id="lst" runat="server">
                                    <table class="tbStandart" border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:2%">No.</th>
                                                <th class="kotak" style="width:5%">Nomor</th>
                                                <th class="kotak" style="width:6%">Tgl Mulai</th>
                                                <th class="kotak" style="width:6%">Tgl Selesai</th>
                                                <th class="kotak" style="width:25%">Nama Improvement</th>
                                                <th class="kotak" style="width:8%">Estimasi Biaya</th>
                                                <th class="kotak" style="width:8%">Biaya Aktual</th>
                                                <th class="kotak"style="width:5%">Apv</th>
                                                <th class="kotak"style="width:5%">Status</th>
                                                <th class="kotak"style="width:10%">Sasaran</th>
                                                <th class="kotak" style="width:10%">Pemohon</th>
                                                <%--<th class="kotak"style="width:4%"></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                           <asp:Repeater ID="lstProject" runat="server" OnItemDataBound="lstProjcet_DataBound" OnItemCommand="lstProject_Command">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris" id="brs" runat="server" title='<%# Eval("ID") %>'>
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah" nowrap="nowrap"><%# Eval("Nomor") %></td>
                                                    <td class="kotak tengah"><%# Eval("FromDate","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("ToDate", "{0:d}")%></td>
                                                    <td class="kotak">
                                                        <asp:Label ID="dsc" runat="server" Visible="false" Text='<%# Eval("NamaProject") %>'></asp:Label>
                                                        <asp:LinkButton ID="lnk" runat="server" Text='<%# Eval("NamaProject") %>' CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                    </td>
                                                    <td class="kotak angka"><%# Eval("Biaya","{0:N0}") %></td>
                                                    <td class="kotak angka"><asp:Label ID="txtAktual" runat="server"></asp:Label></td>
                                                    <td class="kotak tengah"><asp:Label ID="txtApv" runat="server" Text='<%# Eval("Approval") %>'></asp:Label></td>
                                                    <td class="kotak tengah"><asp:Label ID="txtSts" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                                                    <td class="kotak"><%# Eval("Sasaran") %></td>
                                                    <td class="kotak" nowrap="nowrap"><%# Eval("DeptName") %></td>
                                                    <%--<td class="kotak tengah">
                                                    </td>--%>
                                                </tr>
                                                <!--Estimasi Material-->
                                                <asp:Repeater ID="lstEstimasi" runat="server">
                                                    <HeaderTemplate>
                                                        <tr style="height:3px" class="total"><th colspan="11" class="kotak"></th></tr>
                                                        <tr class="EvenRows">
                                                            <th class="kotak" rowspan="2">#</th>
                                                            <th class="Kotak" rowspan="2">ItemCode</th>
                                                            <th class="kotak" rowspan="2" colspan="4">ItemName</th>
                                                            <th class="kotak" colspan="2">Planing</th>
                                                            <th class="kotak" colspan="2">Actual</th>
                                                            <th class="kotak" rowspan="2">Selisih</th>
                                                        </tr>
                                                        <tr class="EvenRows">
                                                            <th class="kotak">Qty</th>
                                                            <th class="kotak">Total Harga</th>
                                                            <th class="Kotak">Qty</th>
                                                            <th class="kotak">Total Harga</th>
                                                            
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td class="kotak angka"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah">'<%# Eval("ItemCode") %></td>
                                                            <td class="kotak" colspan="4">
                                                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                                                    <tr>
                                                                        <td style="width:75%"><%# Eval("ItemName") %></td>
                                                                        <td style="width:20%; text-align:right;" class="angka"><%# Eval("UomCode") %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td class="kotak angka"><%# Eval("Jumlah","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("PricePlanning","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("QtyAktual","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("Avgprice", "{0:N2}")%></td>
                                                            <td class="kotak angka"><%# Eval("Harga", "{0:N2}")%> </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr class="total" style="height:5px">
                                                            <td colspan="11"></td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
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
