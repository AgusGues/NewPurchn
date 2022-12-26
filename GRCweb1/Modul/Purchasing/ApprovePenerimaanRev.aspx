<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovePenerimaanRev.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApprovePenerimaanRev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel()
        { }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%;">
                    <tbody>
                        <tr>
                            <td>
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width:50%; padding-left:10px;">
                                            <strong>&nbsp;PERSETUJUAN PEMBAYARAN</strong>&nbsp;&nbsp
                                            
                                        </td>
                                        <td style="width:50%; padding-left:10px;">
                                            <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" />
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Approve" onserverclick="btnUpdate_ServerClick" />
                                        &nbsp;
                                            <input id="btnList" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 125px; height: 22px;"
                                                type="button" value="List Open Receipt"
                                                onserverclick="btnList_ServerClick" visible="False" />
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="NoInvoice">No Invoice</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <table id="Tableisi" style="width: 100%;">
                                        <tr>
                                            <td></td>
                                            <td colspan="4">
                                                <div id="infoKurs" runat="server" style="display: none;"></div>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; No. Invoice
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <span style="font-size: 10pt">&nbsp; Tgl. Terima Tagihan</span>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtTTagihanDate" AutoPostBack="false" runat="server" BorderStyle="Groove" Width="233"
                                                    OnTextChanged="txtTTagihanDate_TextChanged"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTTagihanDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTTagihanDate" EnableViewState="true"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="font-size: 10pt">&nbsp; No. Faktur Pajak</span>
                                            </td>
                                            <td rowspan="1">
                                                <asp:TextBox ID="txtFakturPajak" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="20px" Width="233px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="None"
                                                    AutoComplete="False" ClearMaskOnLostFocus="false" ClipboardEnabled="False" DisplayMoney="None"
                                                    Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" InputDirection="RightToLeft"
                                                    Mask="999.999-99.99999999" MaskType="None" MessageValidatorTip="False" PromptCharacter=" "
                                                    TargetControlID="txtFakturPajak"></cc1:MaskedEditExtender>
                                            </td>
                                            <td></td>
                                            <td>
                                                <span style="font-size: 10pt">&nbsp;<span style="font-size: 10pt">&nbsp;Tgl. Faktur
                                                    Pajak</span></span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFakPajakDate" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFakPajakDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtFakPajakDate" EnableViewState="true"></cc1:CalendarExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <span style="font-size: 10pt">&nbsp;&nbsp;Kurs Pajak</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtKursPajak" runat="server" BorderStyle="Groove" Width="233px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td style="font-size: x-small;" colspan="3">&nbsp;&nbsp;Grand Total &gt;&gt;&gt;&nbsp; Harga :&nbsp;
                                                <asp:Label ID="LTotal" runat="server" ForeColor="Blue" Text="0"></asp:Label>
                                                &nbsp;&nbsp; PPN :
                                                <asp:Label ID="LPPN" runat="server" ForeColor="Blue" Text="0"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp; Tagihan :&nbsp;
                                                <asp:Label ID="LTagihan" runat="server" ForeColor="Blue" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="font-size: 10pt">&nbsp; Keterangan</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" Width="233px"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <input id="btnNew0" runat="server" onserverclick="btnNew0_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                    type="button" value="Close List Receipt" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Panel ID="PanelReceipt" runat="server" Font-Size="X-Small" Wrap="False">
                                                    &nbsp;&nbsp; Group&nbsp;
                                                    <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"
                                                        OnTextChanged="ddlGroup_TextChanged" TabIndex="8" Width="233">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Search By ReceiptNo&nbsp;
                                                    <asp:TextBox ID="txtCariReceipt" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        Font-Size="X-Small" onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txNama_TextChanged"
                                                        TabIndex="1" Width="150px" ToolTip="Cari ReceiptNo"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetReceiptByTagihan" ServicePath="AutoComplete.asmx" TargetControlID="txtCariReceipt"
                                                        CompletionListCssClass="autocomplete_completionListElement">
                                                    </cc1:AutoCompleteExtender>
                                                    &nbsp;&nbsp;<asp:ImageButton ID="btnSearch3" runat="server" CssClass="MyImageButton"
                                                        Font-Bold="true" Height="16px" ImageUrl="~/images/search.png" OnClick="btnSearch3_Click"
                                                        Text="Search" ToolTip="Cari ReceiptNo" Width="20px" />
                                                    <span style="font-size: 10pt">&nbsp;Search BySupplier&nbsp;
                                                        <asp:TextBox ID="txtCariSupplier" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Font-Size="X-Small" onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtCariSupplier_TextChanged"
                                                            TabIndex="1" ToolTip="Cari Supplier" Width="300px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtCariSupplier_AutoCompleteExtender" runat="server"
                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                            MinimumPrefixLength="1" UseContextKey="true" ContextKey="0" ServiceMethod="GetReceiptByTagihanSupplier"
                                                            ServicePath="AutoComplete.asmx" TargetControlID="txtCariSupplier" CompletionListCssClass="autocomplete_completionListElement">
                                                        </cc1:AutoCompleteExtender>
                                                        &nbsp;
                                                        <asp:ImageButton ID="btnSearch4" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                            Height="16px" ImageUrl="~/images/search.png" Text="Search" ToolTip="Cari Supplier"
                                                            Width="20px" />
                                                        <hr />
                                                        &nbsp; <strong>List Receipt</strong></span>
                                                    <asp:Panel ID="Panel3" runat="server" Height="220px" ScrollBars="Auto" Wrap="False">
                                                        <asp:GridView ID="GridViewReceipt" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                                            Font-Size="X-Small" OnRowCommand="GridViewReceipt_RowCommand" Width="100%" OnRowCancelingEdit="GridViewReceipt_RowCancelingEdit">
                                                            <Columns>
                                                                <asp:BoundField DataField="ID" HeaderText="ID">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Suppliername" HeaderText="Supplier">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NPWP" HeaderText="NPWP">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="receiptno" HeaderText="ReceiptNo">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:d}" HeaderText="ReceiptDate">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Currency" HeaderText="Currency">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Total" DataFormatString="{0:N2}" HeaderText="DPP">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PPN" DataFormatString="{0:N2}" HeaderText="PPN">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Tagihan" DataFormatString="{0:N2}" HeaderText="Total">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="POID" HeaderText="POID">
                                                                    <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Receipt Detail">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btn_Show" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                                            CommandName="Details" Font-Size="X-Small" Height="19px" OnClick="btn_Show_Click"
                                                                            Style="margin-top: 0px" Text="Show Details" Width="88px" />
                                                                        <asp:Button ID="Cancel" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                                            CommandName="Cancel" Font-Size="X-Small" Height="19px" Text="Hide Details" Visible="False"
                                                                            Width="85px" />
                                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                                            Font-Size="XX-Small" ForeColor="Black" Width="100%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SPPNo" HeaderText="SPPNo">
                                                                                    <ItemStyle Font-Size="XX-Small" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PONo" HeaderText="PONo">
                                                                                    <ItemStyle Font-Size="XX-Small" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode">
                                                                                    <ItemStyle Font-Size="XX-Small" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Quantity" DataFormatString="{0:N2}" HeaderText="Qty">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="Price">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="totalprice" DataFormatString="{0:N2}" HeaderText="Total">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#7C6F57" />
                                                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#E3EAEB" />
                                                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                        </asp:GridView>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Font-Size="X-Small" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:ButtonField Text="Pilih" CommandName="pilih">
                                                                    <ItemStyle VerticalAlign="Top" />
                                                                </asp:ButtonField>
                                                            </Columns>
                                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                            <RowStyle ForeColor="#000066" />
                                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <span style="font-size: 10pt">&nbsp; <strong>List Persetujuan Pembayaran</strong></span>
                                                <asp:GridView
                                                    ID="GridViewInvoice" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                                    Font-Size="X-Small" OnRowCancelingEdit="GridViewInvoice_RowCancelingEdit" OnRowCommand="GridViewInvoice_RowCommand"
                                                    OnSelectedIndexChanged="GridViewInvoice_SelectedIndexChanged" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID">
                                                            <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Suppliername" HeaderText="Supplier">
                                                            <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NPWP" HeaderText="NPWP">
                                                            <ItemStyle Font-Size="XX-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="receiptno" HeaderText="ReceiptNo">
                                                            <ItemStyle Font-Size="X-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:d}" HeaderText="ReceiptDate">
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="jtempodate" DataFormatString="{0:d}" HeaderText="JatuhTempo">
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Currency" HeaderText="Currency">
                                                            <ItemStyle Font-Size="X-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Total" DataFormatString="{0:N2}" HeaderText="DPP">
                                                            <ItemStyle Font-Size="X-Small" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PPN" DataFormatString="{0:N2}" HeaderText="PPN">
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Tagihan" DataFormatString="{0:N2}" HeaderText="Total">
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Receipt Detail">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_Show0" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                                    CommandName="Details" Font-Size="X-Small" Height="19px" OnClick="btn_Show_Click"
                                                                    Style="margin-top: 0px" Text="Show Details" Width="88px" />
                                                                <asp:Button ID="Cancel0" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                                    CommandName="Cancel" Font-Size="X-Small" Height="19px" Text="Hide Details" Visible="false"
                                                                    Width="85px" />
                                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                                                    Font-Size="XX-Small" ForeColor="Black" Width="100%">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SPPNo" HeaderText="SPPNo" />
                                                                        <asp:BoundField DataField="PONo" HeaderText="PONo" />
                                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                                                                        <asp:BoundField DataField="Quantity" DataFormatString="{0:N2}" HeaderText="Qty">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="Price">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Totalprice" DataFormatString="{0:N2}" HeaderText="Total">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                    <RowStyle BackColor="#E3EAEB" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                </asp:GridView>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Size="X-Small" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        OnRowCommand="GridView1_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="Sppno" HeaderText="No. SPP" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                            <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                            <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                                            <asp:BoundField DataField="price" HeaderText="Harga" />
                                                            <asp:BoundField DataField="totalprice" HeaderText="Total" />
                                                        </Columns>
                                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
