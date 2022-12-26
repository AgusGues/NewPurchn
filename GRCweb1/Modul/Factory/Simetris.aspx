<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Simetris.aspx.cs" Inherits="GRCweb1.Modul.Factory.Simetris" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive">
                <table cellpadding="0" cellspacing="0" style="table-layout: fixed;">
                    <tr>
                        <td style="width: 100%; height: 30px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 279px">
                                        <strong>PROSES MUTASI SIMETRIS</strong>
                                    </td>
                                    <td>
                                        <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru" />&nbsp;
                                    </td>
                                    <td style="width: auto; font-size: x-small;">&nbsp;
                                    </td>
                                    <td style="width: 5px">
                                        <asp:DropDownList ID="ddlCari" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem>PartNo</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-size: x-small; font-weight: normal;">
                                        <asp:TextBox ID="txtPartnoC" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                            Height="20px" OnTextChanged="txtPartnoC_TextChanged" Width="208px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoC">
                                        </cc1:AutoCompleteExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtPartnoC" Mask="AAA-A-99999999999AAA"
                                            MessageValidatorTip="False" MaskType="None" InputDirection="RightToLeft" Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
                                            AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" runat="server"
                                            ClearMaskOnLostFocus="false" AutoComplete="False" PromptCharacter=" "></cc1:MaskedEditExtender>
                                    </td>
                                    <td style="font-size: x-small; font-weight: normal;">&nbsp;<asp:TextBox ID="txtLokasiC" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                        Height="21px" OnTextChanged="txtLokasiC_TextChanged" Visible="False" Width="48px"></asp:TextBox><cc1:AutoCompleteExtender
                                            ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasiC">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>&nbsp;<input id="btnCari" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Cari" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <div style="height: 100%; width: 100%;">
                                <table id="Table4" border="0" cellpadding="0" cellspacing="1" class="tblForm" style="left: 0px; top: 0px; width: 100%; font-size: x-small;">
                                    <tr>
                                        <td colspan="3" style="height: 3px; font-weight: bold; font-size: x-small;" valign="top">List Stock Produk
                                        </td>
                                        <td colspan="2" style="height: 3px; font-weight: bold; font-size: x-small;" valign="top">&nbsp;
                                        </td>
                                        <td colspan="2" style="height: 3px; font-weight: normal; font-size: x-small;" valign="top"
                                            align="right">
                                            <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                                                OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="height: 3px" valign="top">
                                            <asp:Panel ID="Panel3" runat="server" BackColor="#CCFFCC" Height="100px" ScrollBars="Vertical"
                                                Wrap="False">
                                                <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewtrans_RowCommand"
                                                    PageSize="22" Width="100%">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                    <Columns>
                                                        <asp:BoundField DataField="GroupID" HeaderText="GroupID" />
                                                        <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                        <asp:BoundField DataField="lokid" HeaderText="lokid" />
                                                        <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                        <asp:BoundField DataField="PartName" HeaderText="Part Name" />
                                                        <asp:BoundField DataField="Tebal" HeaderText="Tebal" />
                                                        <asp:BoundField DataField="Lebar" HeaderText="Lebar" />
                                                        <asp:BoundField DataField="Panjang" HeaderText="Panjang" />
                                                        <asp:BoundField DataField="volume" HeaderText="Volume" />
                                                        <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
                                                        <asp:BoundField DataField="qty" HeaderText="Stock" />
                                                        <asp:ButtonField CommandName="Pilih" Text="Pilih" Visible="False" />
                                                    </Columns>
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="height: 3px" valign="top">
                                            <div style="width: 98%;">
                                                <table style="width: 100%;" bgcolor="Silver">
                                                    <tr style="width: 100%;">
                                                        <td style="width: 20%; font-size: x-small; font-weight: bold;">Input Proses Simetris
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table>
                                                <tr style="width: 94%;" bgcolor="#6699FF">
                                                    <td align="center" colspan="2"
                                                        style="width: 40%; font-family: Calibri; font-size: x-small; table-layout: auto;"
                                                        valign="middle">
                                                        <b>Tgl.&nbsp; Proses</b>
                                                        <asp:TextBox ID="DatePicker1" runat="server" AutoPostBack="True"
                                                            BorderStyle="Groove" Width="150px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="DatePicker1"></cc1:CalendarExtender>
                                                        &nbsp;<input id="btnRefresh" runat="server" onserverclick="btnRefresh_ServerClick"
                                                            style="background-color: white; font-weight: bold; font-size: 11px;"
                                                            type="button" value="Refresh Data" />
                                                    </td>
                                                    <td align="right" style="width: 10%">
                                                        <asp:Label ID="LabelDefect" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="false">&nbsp; Jenis Defect</asp:Label>
                                                        <asp:Label ID="LabelMCutter" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="false">&nbsp; Nama Mesin : &nbsp;</asp:Label>
                                                    </td>
                                                    <td colspan="2" style="width: 20%">
                                                        <asp:DropDownList ID="ddlDefect" runat="server" Height="16px" Style="font-family: Calibri; font-size: x-small"
                                                            Visible="false" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlMCutter" runat="server" Height="16px" Style="font-family: Calibri; font-size: x-small"
                                                            Visible="false" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" style="width: 30%">
                                                        <asp:Label ID="lbltglprod" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="false">&nbsp; tgl Produksi :</asp:Label>
                                                         <asp:TextBox ID="tglprod" runat="server" Visible="false"></asp:TextBox>
                                                         <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Format="dd-MMM-yyyy" TargetControlID="tglprod"></cc1:CalendarExtender>
                                                    </td>   
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                        <asp:Panel ID="Panel8" runat="server">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Panel ID="Panel2" runat="server" Width="398px">
                                                                            <table bgcolor="#CCCCCC">
                                                                                <tr>
                                                                                    <td align="center" bgcolor="#3366CC" colspan="4" style="font-size: x-small; font-weight: bold; color: #FFCC00;">LOKASI AWAL
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; width: 258px;">Partno
                                                                                    </td>
                                                                                    <td colspan="3" style="font-size: x-small; width: 172px;">
                                                                                        <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="20px" OnTextChanged="txtPartnoA_TextChanged"
                                                                                            Width="182px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                                                                            TargetControlID="txtPartnoA">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; width: 258px;">Ukuran
                                                                                    </td>
                                                                                    <td colspan="3" style="font-size: x-small;">
                                                                                        <asp:TextBox ID="txtTebal1" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                        &nbsp;X
                                                                                            <asp:TextBox ID="txtLebar1" runat="server" AutoPostBack="True"
                                                                                                BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                        &nbsp;X
                                                                                            <asp:TextBox ID="txtPanjang1" runat="server" AutoPostBack="True"
                                                                                                BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; width: 258px;">Part Name
                                                                                    </td>
                                                                                    <td colspan="3" style="font-size: x-small; width: 172px;">
                                                                                        <asp:TextBox ID="txtPartname1" runat="server" BorderStyle="Groove"
                                                                                            Height="20px" ReadOnly="True" Width="182px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; width: 258px;">Lokasi
                                                                                    </td>
                                                                                    <td style="font-size: x-small; width: 119px;">
                                                                                        <asp:TextBox ID="txtLokasi1" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" OnPreRender="txtLokasi1_PreRender"
                                                                                            OnTextChanged="txtLokasi1_TextChanged" Width="48px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" ContextKey="0"
                                                                                            EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetLokasiStockP" ServicePath="AutoComplete.asmx"
                                                                                            TargetControlID="txtLokasi1" UseContextKey="true">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td align="right" style="font-size: x-small; width: 133px;">Stock
                                                                                    </td>
                                                                                    <td style="font-size: x-small; width: 172px;">
                                                                                        <asp:TextBox ID="txtStock1" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Enabled="False" Height="21px"
                                                                                            OnTextChanged="txtLokasi1_TextChanged" Width="48px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="txtStock1_AutoCompleteExtender" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                            TargetControlID="txtStock1">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; width: 258px;">Quantity
                                                                                    </td>
                                                                                    <td colspan="3" style="font-size: x-small; width: 172px;">
                                                                                        <asp:TextBox ID="txtQty1" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" OnPreRender="txtQty1_PreRender"
                                                                                            OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" colspan="4" style="font-size: x-small; font-weight: normal;">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td style="width: 14px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Panel ID="Panel4" runat="server" Height="171px" Width="373px">
                                                                            <table bgcolor="#CCCCCC">
                                                                                <tr>
                                                                                    <td align="center" bgcolor="#3366CC" colspan="3" style="font-size: x-small; font-weight: bold; color: #FFCC00;">LOKASI AKHIR
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" style="font-size: x-small; font-weight: normal;">Partno
                                                                                    </td>
                                                                                    <td style="font-size: x-small; width: 0%;" width="50%">
                                                                                        <asp:TextBox ID="txtPartnoB" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" OnPreRender="txtPartnoB_PreRender"
                                                                                            OnTextChanged="txtPartnoB_TextChanged" Width="182px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi"
                                                                                            ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoB">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td style="font-size: x-small; width: 25%;" width="50%">&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right"
                                                                                        style="font-size: x-small; font-weight: normal; height: 28px;">Ukuran
                                                                                    </td>
                                                                                    <td colspan="2" style="font-size: x-small; height: 28px;">
                                                                                        <asp:TextBox ID="txtTebal2" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                        &nbsp;X
                                                                                            <asp:TextBox ID="txtLebar2" runat="server" AutoPostBack="True"
                                                                                                BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                        &nbsp;X
                                                                                            <asp:TextBox ID="txtPanjang2" runat="server" AutoPostBack="True"
                                                                                                BorderStyle="Groove" Height="21px" ReadOnly="True" Width="48px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" bgcolor="#CCCCCC" style="font-size: x-small; font-weight: normal; width: 377px;">Part Name
                                                                                    </td>
                                                                                    <td colspan="2" style="font-size: x-small; width: 172px;">
                                                                                        <asp:TextBox ID="txtPartname2" runat="server" BorderStyle="Groove"
                                                                                            Height="21px" ReadOnly="True" Width="182px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" bgcolor="#CCCCCC"
                                                                                        style="font-size: x-small; font-weight: normal;">Lokasi
                                                                                    </td>
                                                                                    <td colspan="2" style="font-size: x-small;">
                                                                                        <asp:TextBox ID="txtLokasi2" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" OnPreRender="txtLokasi2_PreRender"
                                                                                            OnTextChanged="txtLokasi2_TextChanged" Width="48px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                            TargetControlID="txtLokasi2">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stock
                                                                                            <asp:TextBox ID="txtStock2" runat="server" AutoPostBack="True"
                                                                                                BorderStyle="Groove" Enabled="False" Height="21px"
                                                                                                OnTextChanged="txtLokasi1_TextChanged" Width="48px"></asp:TextBox>
                                                                                        <cc1:AutoCompleteExtender ID="txtStock2_AutoCompleteExtender" runat="server"
                                                                                            CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                                            FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                            ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                            TargetControlID="txtStock2">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" style="font-size: x-small; font-weight: normal;">Quantity
                                                                                    </td>
                                                                                    <td colspan="2" style="font-size: x-small;">
                                                                                        <asp:TextBox ID="txtQty2" runat="server" AutoPostBack="True"
                                                                                            BorderStyle="Groove" Height="21px" OnPreRender="txtQty2_PreRender"
                                                                                            OnTextChanged="txtQty2_TextChanged" Width="48px"></asp:TextBox>
                                                                                        <input id="btnTansfer" runat="server" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                                            type="button" value="Transfer" />
                                                                                        <asp:TextBox ID="txtPengali" runat="server" AutoPostBack="True"
                                                                                            Font-Size="x-small" Height="21px" onfocus="this.select();" ReadOnly="True"
                                                                                            Visible="False" Width="19px">1</asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3" style="font-size: x-small; font-weight: normal;">
                                                                                        <asp:Panel ID="PanelNC" runat="server" BackColor="#FFFF99" Font-Size="X-Small"
                                                                                            HorizontalAlign="Center" Visible="False">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td style="width: 100%">
                                                                                                        <asp:RadioButton ID="RBNCHandling" runat="server" AutoPostBack="True"
                                                                                                            Checked="True" Font-Size="X-Small" ForeColor="Black" GroupName="x"
                                                                                                            OnCheckedChanged="RBNCHandling_CheckedChanged" Text="NC Handling" />
                                                                                                        <asp:RadioButton ID="RBNCSortir" runat="server" AutoPostBack="True"
                                                                                                            Font-Size="X-Small" ForeColor="Black" GroupName="x"
                                                                                                            OnCheckedChanged="RBNCSortir_CheckedChanged" Text="NC Sortir" />
                                                                                                        <asp:RadioButton ID="RBNonNC" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                                                                            ForeColor="Black" GroupName="x" OnCheckedChanged="RBNonNC_CheckedChanged"
                                                                                                            Text="Non NC" />
                                                                                                        <%--         <asp:Panel ID="PanelNCSortir" runat="server" BackColor="#FFCCCC" 
                                                                                                                Visible="False">
                                                                                                                <asp:RadioButton ID="RBStd" runat="server" AutoPostBack="True" Checked="True" 
                                                                                                                    Font-Size="X-Small" GroupName="y" Text="Standar" />
                                                                                                                <asp:RadioButton ID="RBEfo" runat="server" AutoPostBack="True" 
                                                                                                                    Font-Size="X-Small" GroupName="y" Text="EFO" />
                                                                                                            </asp:Panel>--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                        <asp:Panel ID="PanelBS" runat="server" BackColor="#FFCCCC" Visible="False">
                                                                                            <asp:RadioButton ID="RBFin" runat="server" AutoPostBack="True" Checked="True"
                                                                                                Font-Size="X-Small" GroupName="i" Text="BS Finishing" />
                                                                                            <asp:RadioButton ID="RBLog" runat="server" AutoPostBack="True"
                                                                                                Font-Size="X-Small" GroupName="i" Text="BS Logistik" />
                                                                                            <asp:RadioButton ID="RBKat" runat="server" AutoPostBack="True"
                                                                                                Font-Size="X-Small" GroupName="i" Text="BS KAT" />
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Panel ID="Panel7" runat="server" BackColor="#CCFFCC" Font-Size="X-Small"
                                                                            Height="173px" ScrollBars="Both">
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Underline="True"
                                                                                Text="Group Marketing"></asp:Label>
                                                                            <asp:RadioButtonList ID="RBList" runat="server" AutoPostBack="True"
                                                                                Font-Size="XX-Small">
                                                                            </asp:RadioButtonList>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: x-small; font-weight: bold;" colspan="5">
                                                        <asp:Panel ID="PanelOtomatis" runat="server" BackColor="Gray" ForeColor="White" Width="100%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="font-size: x-small; background-color: #C0C0C0;">
                                                                        <asp:CheckBox ID="ChkConvertBS" runat="server" AutoPostBack="True" Checked="True" Enabled="False" ForeColor="Black" OnCheckedChanged="ChkConvertBS_CheckedChanged" Text="Auto Produk BS" />
                                                                    </td>
                                                                    <td style="font-size: x-small;">
                                                                        <asp:Panel ID="PanelPotong" runat="server" BackColor="Silver" HorizontalAlign="Right">
                                                                            <asp:RadioButton ID="RBPotong1" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBPotong1_CheckedChanged" Text="Cara Potong 1" />
                                                                            <asp:RadioButton ID="RBPotong2" runat="server" AutoPostBack="True" Checked="True" GroupName="b" OnCheckedChanged="RBPotong2_CheckedChanged" Text="Cara Potong 2" />
                                                                            <asp:RadioButton ID="RBPotong3" runat="server" AutoPostBack="True" Checked="false" GroupName="b" OnCheckedChanged="RBPotong3_CheckedChanged" Text="Cara Potong 3" />  
                                                                        </asp:Panel>
                                                                    </td >
                                                                    <td></td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size: x-small;">&nbsp;: 
                                                                            (<asp:Label ID="LCPartnoBS1" runat="server" Text="-"></asp:Label>
                                                                        )
                                                                            <asp:Label ID="LCQtyBS1" runat="server" Text="0"></asp:Label>
                                                                        &nbsp;Lembar, ke lokasi :&nbsp;
                                                                            <asp:Label ID="LCLokBS1" runat="server" Text="0"></asp:Label>
                                                                    </td>
                                                                    <td style="font-size: x-small;">&nbsp;: 
                                                                            (<asp:Label ID="LCPartnoBS3" runat="server" Text="-"></asp:Label>
                                                                        )
                                                                            <asp:Label ID="LCQtyBS3" runat="server" Text="0"></asp:Label>
                                                                        &nbsp;Lembar, ke lokasi :&nbsp;
                                                                            <asp:Label ID="LCLokBS3" runat="server" Text="0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-size: x-small;">&nbsp;: 
                                                                            (<asp:Label ID="LCPartnoBS2" runat="server" Text="-"></asp:Label>
                                                                        )
                                                                            <asp:Label ID="LCQtyBS2" runat="server" Text="0"></asp:Label>
                                                                        &nbsp;Lembar, ke lokasi :&nbsp;
                                                                            <asp:Label ID="LCLokBS2" runat="server" Text="0"></asp:Label>
                                                                    </td>
                                                                    <td style="font-size: x-small;">&nbsp;: 
                                                                            (<asp:Label ID="LCPartnoBS4" runat="server" Text="-"></asp:Label>
                                                                        )
                                                                            <asp:Label ID="LCQtyBS4" runat="server" Text="0"></asp:Label>
                                                                        &nbsp;Lembar, ke lokasi :&nbsp;
                                                                            <asp:Label ID="LCLokBS4" runat="server" Text="0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: x-small; font-weight: bold; width: 158px;">List proses simetris </td>
                                                    <td colspan="2">&nbsp; </td>
                                                    <td align="right" colspan="2">&nbsp; </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:Panel ID="Panel5" runat="server" BackColor="#CCFFCC" Height="300px"
                                                            ScrollBars="Vertical">
                                                            <asp:GridView ID="GridViewSimetris" runat="server" AutoGenerateColumns="False"
                                                                OnRowCommand="GridViewSimetris_RowCommand" PageSize="22" Width="100%">
                                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                                    BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                                    ForeColor="Gold" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                    <asp:BoundField DataField="PartNoser" HeaderText="PartNo Awal" />
                                                                    <asp:BoundField DataField="lokasiser" HeaderText="Lokasi" />
                                                                    <asp:BoundField DataField="qtyinsm" HeaderText="Qty" />
                                                                    <asp:BoundField DataField="Groupname" HeaderText="Group" />
                                                                    <asp:BoundField DataField="partnosm" HeaderText="PartNo Akhir" />
                                                                    <asp:BoundField DataField="lokasism" HeaderText="Lokasi" />
                                                                    <asp:BoundField DataField="qtyoutsm" HeaderText="Qty" />
                                                                    <asp:BoundField DataField="MCutter" HeaderText="Mesin" />
                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Users" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="deletebtn" runat="server" CommandName="hapus"
                                                                                OnClientClick="return confirm('Yakin mau cancel simetris?');" Text="Cancel" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle BorderStyle="Solid" />
                                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 3px; width: 95px;" valign="top">&nbsp;
                                                    </td>
                                                    <td style="width: 171px; height: 3px" valign="top"></td>
                                                    <td colspan="2" style="height: 3px; width: 68px;" valign="top">&nbsp;
                                                    </td>
                                                    <td colspan="2" style="width: 135px; height: 3px" valign="top">&nbsp;
                                                    </td>
                                                    <td style="width: 205px; height: 3px" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
