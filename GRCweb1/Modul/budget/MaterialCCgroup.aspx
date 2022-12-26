<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaterialCCgroup.aspx.cs" Inherits="GRCweb1.Modul.Budgeting.MaterialCCgroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function GetKey(source, eventArgs) {
            $('#<%=txtItemID.ClientID %>').val(eventArgs.get_value());
        }
        function GetKeys(source, eventArgs) {
            $('#<%=txtPartNoID.ClientID %>').val(eventArgs.get_value());
        }
        function GetLokasiID(source, eventArgs) {
            $('#<%=txtLokasiID.ClientID %>').val(eventArgs.get_value());
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table class="table-responsive">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; padding-left: 10px">
                                        <asp:Label ID="txtJudul" runat="server" Text="MATERIAL COST CENTER GROUP"></asp:Label>
                                    </td>
                                    <td style="width: 50%; text-align: right; padding-right: 10px">
                                        <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <asp:TextBox ID="txtCari" runat="server" Width="250px"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 5%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Department
                                        </td>
                                        <td style="width: 30%">
                                            <asp:DropDownList ID="ddlDept" runat="server" Width="80%" OnSelectedIndexChanged="ddlDept_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Control Group
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMatGroupCC" runat="server" Width="80%" OnSelectedIndexChanged="ddlMatGroupCC_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="forPJ1" runat="server" visible="false">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Spare Part ItemName
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlItemName" runat="server" Width="80%" OnSelectedIndexChanged="ddlItemName_Change"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="forInv" runat="server">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Material ItemName
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMaterialName" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:AutoCompleteExtender ID="act1" runat="server" TargetControlID="txtMaterialName"
                                                CompletionSetCount="20" ServiceMethod="GetInventory" ServicePath="~/Modul/Purchasing/AutoComplete.asmx"
                                                MinimumPrefixLength="3" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionInterval="100" EnableCaching="true" OnClientItemSelected="GetKey">
                                            </cc1:AutoCompleteExtender>
                                            <asp:HiddenField ID="txtItemID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                    <tr id="forPJ" runat="server" visible="false">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Part No
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPathNo" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPathNo"
                                                CompletionSetCount="20" ServiceMethod="GetNoProdukCC" ServicePath="~/Modul/Purchasing/AutoComplete.asmx"
                                                MinimumPrefixLength="3" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionInterval="100" EnableCaching="true" OnClientItemSelected="GetKeys">
                                            </cc1:AutoCompleteExtender>
                                            <asp:HiddenField ID="txtPartNoID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                    <tr id="forPJ2" runat="server" visible="false">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Lokasi
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLokasi" runat="server" Width="50%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtLokasi"
                                                CompletionSetCount="20" ServiceMethod="GetLokasiStockCC" ServicePath="~/Modul/Purchasing/AutoComplete.asmx"
                                                MinimumPrefixLength="1" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionInterval="100" EnableCaching="true" OnClientItemSelected="GetLokasiID">
                                            </cc1:AutoCompleteExtender>
                                            <asp:HiddenField ID="txtLokasiID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Item Code
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtItemCode" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddItem" runat="server" Text="Add Item" OnClick="btnAddItem_Click" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 370px;">
                                    <table style="width: 100%; border-collapse: collapse; font-size: small">
                                        <thead>
                                            <tr class="tbHeader" id="lInven" runat="server" visible="false">
                                                <th class="kotak" style="width: 5%">
                                                    No.
                                                </th>
                                                <th class="kotak" style="width: 10%">
                                                    ItemCode
                                                </th>
                                                <th class="kotak" style="width: 30%">
                                                    ItemName
                                                </th>
                                                <th class="kotak" style="width: 5%">
                                                    &nbsp;
                                                </th>
                                                <th class="transparant">
                                                </th>
                                            </tr>
                                            <tr class="tbHeader" id="lPartNo" runat="server" visible="false">
                                                <th class="kotak" style="width: 5%">
                                                    No.
                                                </th>
                                                <th class="kotak" style="width: 30%">
                                                    PartNo
                                                </th>
                                                <th class="kotak" style="width: 10%">
                                                    Lokasi
                                                </th>
                                                <th class="kotak" style="width: 5%">
                                                    &nbsp;
                                                </th>
                                                <th class="transparant">
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstItem" runat="server" OnItemDataBound="lstItem_DataBound" OnItemCommand="lstItem_Command">
                                                <ItemTemplate>
                                                    <tr id="lst" runat="server">
                                                        <td class="kotak tengah">
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("ItemCode") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("ItemName") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="del" runat="server" CommandArgument='<%# Eval("ItemID") %>'
                                                                CommandName="hps" ImageUrl="~/images/trash.gif" />
                                                            <asp:ImageButton ID="hpus" runat="server" CommandArgument='<%# Container.ItemIndex+1 %>'
                                                                CommandName="hpse" ImageUrl="~/images/Close.gif" />
                                                        </td>
                                                        <td class="transparant">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="lstPartNo" runat="server" Visible="false" OnItemDataBound="lstPartNo_DataBound"
                                                OnItemCommand="lstPartNo_Command">
                                                <ItemTemplate>
                                                    <tr id="lst" runat="server">
                                                        <td class="kotak tengah">
                                                            <%# Container.ItemIndex+1 %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("PartNo") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("Lokasi") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="del" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="hps"
                                                                ImageUrl="~/images/trash.gif" />
                                                            <asp:ImageButton ID="hpus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                CommandName="hpse" ImageUrl="~/images/Close.gif" />
                                                        </td>
                                                        <td class="transparant">
                                                            &nbsp;
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
