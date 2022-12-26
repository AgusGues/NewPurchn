<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DelivBeliKaList0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.DelivBeliKaList0" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function GetKey(source, eventArgs) {
        $('#<%=txtSupplierID.ClientID %>').val(eventArgs.get_value());
    }
    </script>
    
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
        function CetakFrom(docno){
        params = 'width=1024px';
        params += ', height=600px';
        params += ', top=20px, left=20px';
        params +=',scrollbars=1';
        //window.showModalDialog
        window.open("../../ModalDialog/FromBeliQA.aspx?ka=" + docno, "Preview", params);
      }
    </script>
    <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%; height:49px;">
                            <table class="nbTableHeader" style="width:100%; border-collapse:collapse;" >
                                <tr>
                                    <td style="width:40%; padding-left:10px;">
                                        <b>LIST KADAR AIR KERTAS</b>
                                    </td>
                                    <td style="width:60%; padding-right:10px" align="right">
                                        <asp:HiddenField ID="txtSupplierID" runat="server" Value="0" />
                                       <asp:Button ID="btnBack" runat="server" Text="Form Input" OnClick="btnBack_Click" />
                                       <asp:Button ID="btnApproval" runat="server" Text="Approved" 
                                            OnClick="btnApproval_Click" Visible="False" />
                                       <asp:Label ID="lblFind" runat="server" Text="Find by Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupplier" runat="server" AutoPostBack="true" Width="300px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="act1" runat="server" CompletionInterval="100" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            EnableCaching="true" MinimumPrefixLength="2" OnClientItemSelected="GetKey" 
                                            ServiceMethod="GetSupplierKertas" ServicePath="AutoComplete.asmx" 
                                            TargetControlID="txtSupplier">
                                        </cc1:AutoCompleteExtender>
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
                            <p></p>
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr>
                                        <td style="width:5%">&nbsp;</td>
                                        <td style="width:10%">Periode</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width:40%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Lokasi QA</td>
                                        <td><asp:DropDownList ID="ddlPlant" runat="server"></asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td><asp:Button ID="btnView" runat="server" Text="Preview" OnClick="btnView_Click" /></td>
                                        <td><asp:HiddenField ID="txtID" runat="server" /></td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height:400px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:4%">No</th>
                                                <th class="kotak" style="width:8%">Doc. No</th>
                                                <th class="kotak" style="width:8%">Tanggal</th>
                                                <th class="kotak" style="width:12%">Supplier Name</th>
                                                <th class="kotak" style="width:15%">ItemName</th>
                                                <th class="kotak" style="width:5%">BK</th>
                                                <th class="kotak" style="width:5%">KA(%)</th>
                                                <th class="kotak" style="width:4%">Sampah(%)</th>
                                                <th class="kotak" style="width:5%">BB</th>
                                                <th class="kotak" style="width:5%">Jml BAL</th>
                                                <th class="kotak" style="width:4%">&nbsp</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstKA" runat="server" OnItemDataBound="lstKA_DataBound" OnItemCommand="lstKA_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:CheckBox ID="chk" runat="server" Visible="true" ToolTip='<%# Eval("ID") %>' /></td>
                                                        <td class="kotak tengah"><%# Eval("DocNo") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglCheck","{0:d}") %></td>
                                                        <td class="kotak" style="white-space:nowrap"><%# Eval("SupplierName").ToString().ToUpper() %></td>
                                                        <td class="kotak"><%# Eval("ItemName") %></td>
                                                        <td class="kotak angka"><%# Eval("GrossPlant","{0:N0}") %></td>
                                                        <td class="kotak angka"><%# Eval("AvgKA","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Sampah","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant", "{0:N0}")%></td>
                                                        <td class="kotak tengah"><%# Eval("JmlBAL","{0:N0}") %><asp:Label ID="sts" runat="server" Text='<%# Eval("Keputusan") %>' Visible="false"></asp:Label></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="btnPrev" runat="server" CommandArgument='<%=Eval("DocNo %>' ToolTip="Click For Preview Detail" CommandName="prev" ImageUrl="~/images/clipboard_16.png" />
                                                            <asp:ImageButton ID="btnApp" runat="server" CommandArgument='<%=Eval("DocNo %>' ToolTip="Click For Preview Detail" CommandName="app" ImageUrl="~/images/Approved_16.png" />
                                                            <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("DocNo") %>' ToolTip='<%# Eval("DocNo") %>' CommandName="prn" ImageUrl="~/images/printer_small.png" />
                                                            <asp:ImageButton ID="btnPO" runat="server" CommandArgument='<%# Eval("DocNo") %>' ToolTip='Create PO' CommandName="po" ImageUrl="~/images/editor.png" Visible="false" />
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
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
