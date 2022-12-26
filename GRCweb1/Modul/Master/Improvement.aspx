<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Improvement.aspx.cs" Inherits="GRCweb1.Modul.Master.Improvement" %>
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
        label{font-size:12px;}
    </style>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }       
    </script>
    <script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            var panelProg = $get('divImage');
            panelProg.style.display = '';
            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = false;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divImage and hide it again
            var panelProg = $get('divImage');
            panelProg.style.display = 'none';
        }
        function btnDelete_onclick() {

        }

        function btnUnApprove_onclick() {

        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" style="background-color: ButtonFace">
                <table style="table-layout: fixed; height: 100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height:49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width:100%">
                                            <strong>&nbsp;Improvement Kode Barang</strong>
                                            <asp:Label ID="Ljudul" runat="server" Text="Label" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Baru" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Simpan" />
                                        </td>
                                        <td style="width: 25px">
                                            <input id="btnDelete" runat="server" onserverclick="btnDelete_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Delete" disabled="disabled" />
                                        </td>
                                        <td style="width: 75px" align="center">
                                            <input id="btnApprove" runat="server" onserverclick="btnApprove_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Approve" disabled="disabled" />
                                        </td>
                                         <td style="width: 100px" align="center">
                                            <input id="btnUnApprove" runat="server" onserverclick="btnUnApprove_ServerClick"
                                                style="background-color: white; font-weight: bold; font-size: 11px; width: 80px;"
                                                type="button" value="UnApprove" disabled="disabled" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnEmail" runat="server" onserverclick="btnEmail_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px; width: 116px;" type="button" value="Kirim E-Mail" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ItemName">Nama Item</asp:ListItem>
                                                <asp:ListItem Value="ItemCode">Kode Item</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Cari" />
                                        </td>
                                        <td style="width: 100px">
                                            <div id="divImage" style="display: none; float: left;">
                                                <asp:Image ID="img1" runat="server" Height="16px" ImageUrl="~/Resource_Web/images/loading_animation2.gif"
                                                    Width="84px" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="height: 100%">
                            <td style="width: 100%">
                                <div style="overflow: auto; width: 100%; background-color: #B0C4DE; padding:10px;">
                                    <!--<asp:Panel ID="PanelInput" runat="server" Font-Size="X-Small">-->
                                        <table style="width: 100%; border-collapse:collapse" border="0">
                                            <tr>
                                                <td style="width: 15%">
                                                    <span style="font-size: small">&nbsp;</span>&nbsp;
                                                </td>
                                                <td style="font-size: small; width:85%" align="left" colspan="4">
                                                    <asp:RadioButton ID="RBStock" runat="server" AutoPostBack="True" Checked="True" GroupName="d"
                                                        OnCheckedChanged="RBStock_CheckedChanged" Text="Stock" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="RBNonStock" runat="server" AutoPostBack="True" GroupName="d"
                                                        OnCheckedChanged="RBNonStock_CheckedChanged" Text="Non Stock" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%">
                                                    <span style="font-size: small">&nbsp;Item Type</span>&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" TabIndex="11" Width="90%">
                                                        <asp:ListItem Value="1">INVENTORY</asp:ListItem>
                                                        <asp:ListItem Value="2">ASSET</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="font-size: small; width:10%">
                                                    <span style="font-size: small">Group&nbsp;&nbsp;&nbsp;</span>
                                                </td>
                                                <td colspan="2" style="width:35%">
                                                    <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" TabIndex="8"
                                                        OnTextChanged="ddlGroup_TextChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <span style="font-size: small">Input&nbsp;</span>
                                                </td>
                                                <td colspan="4">
                                                    <asp:Panel ID="PanelNama" runat="server" style="width: 100%;">
                                                        <table bgcolor="#E0E8F0" style="width: 90%; border:1.5px solid #FFF0E0">
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <span style="font-size:small">Nama [1]</span>
                                                                </td>
                                                                <td style="width: 25%; font-size: small;">
                                                                    <asp:TextBox ID="txtNama" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txNama_TextChanged"
                                                                        TabIndex="1" Width="209px" Font-Size="X-Small"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="txtPartnoC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetNamaBarang" ServicePath="AutoComplete.asmx" TargetControlID="txtNama">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <asp:ImageButton ID="btnSearch1" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                                        Height="16px" ImageUrl="~/Images/cute_ball_search.png" OnClick="btnSearch_Click"
                                                                        Text="Search" ToolTip="Cari" Width="20px" />
                                                                    <asp:Panel ID="PanelItemName" runat="server" Height="131px" Visible="False">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Panel ID="Panel4" runat="server" Height="96px" ScrollBars="Vertical">
                                                                                        <asp:GridView ID="GridItemName" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                            OnRowCommand="GridItemName_RowCommand" Width="100%">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="INCode" HeaderText="Kode" />
                                                                                                <asp:BoundField DataField="ItemName" HeaderText="Daftar Nama barang" />
                                                                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                                            </Columns>
                                                                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                            <PagerStyle BorderStyle="Solid" />
                                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20px" valign="middle">
                                                                                    &nbsp;<asp:ImageButton ID="btnSave" runat="server" Height="16px" ImageUrl="~/images/Save.png"
                                                                                        OnClick="btnSave_Click" ToolTip="Simpan" Visible="False" Width="18px" />
                                                                                </td>
                                                                                <td style="width: 214px">
                                                                                    <asp:TextBox ID="txtAddName" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtUkuran_TextChanged"
                                                                                        TabIndex="1" Visible="False" Width="214px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 26px" align="right">
                                                                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/images/add.gif" OnClick="btnAdd_Click"
                                                                                        Style="width: 18px" ToolTip="Tambah" BackColor="Silver" Width="26px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td style="width: 1%; font-size: small;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 10%; font-size: small;"> Type [2]
                                                                </td>
                                                                <td style="font-size: small; width:25%;">
                                                                    <asp:TextBox ID="txtType" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" 
                                                                        OnTextChanged="txtType_TextChanged" TabIndex="1" Width="209px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="Autocompleteextender2" runat="server" CompletionInterval="200"
                                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetMerk" ServicePath="AutoComplete.asmx" TargetControlID="txtType">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <asp:ImageButton ID="btnSearch2" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                                        Height="16px" ImageUrl="~/Images/cute_ball_search.png" OnClick="btnSearch2_Click"
                                                                        Text="Search" ToolTip="Cari" Width="20px" />
                                                                    <asp:Panel ID="PanelItemType" runat="server" Height="131px" Visible="False" ScrollBars="Vertical"
                                                                        Wrap="False">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:GridView ID="GridItemType" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                        OnRowCommand="GridItemType_RowCommand" Width="100%">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Kode" HeaderText="Kode" />
                                                                                            <asp:BoundField DataField="INType" HeaderText="Daftar Type Barang" />
                                                                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                                        </Columns>
                                                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;<asp:ImageButton ID="btnSave1" runat="server" Height="16px" ImageUrl="~/images/Save.png"
                                                                                        OnClick="btnSave1_Click" ToolTip="Simpan" Visible="False" Width="18px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAddType" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtUkuran_TextChanged"
                                                                                        TabIndex="1" Visible="False" Width="214px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnAdd1" runat="server" BackColor="#CCCCCC" ImageUrl="~/images/add.gif"
                                                                                        OnClick="btnAdd1_Click" Style="width: 18px" ToolTip="Tambah" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; font-size: small;">
                                                                    Ukuran [3]
                                                                </td>
                                                                <td style="font-size: small;">
                                                                    <asp:TextBox ID="txtUkuran" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtUkuran_TextChanged"
                                                                        TabIndex="1" Width="209px"></asp:TextBox>
                                                                    <asp:ImageButton ID="btnSearch4" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                                        Height="16px" ImageUrl="~/Images/cute_ball_search.png" Text="Search" ToolTip="Cari"
                                                                        Width="20px" OnClick="btnSearch4_Click" />
                                                                    <asp:Panel ID="PanelUkuran" runat="server" Height="131px" Visible="False" ScrollBars="Vertical">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Panel ID="Panel5" runat="server" Height="96px" ScrollBars="Vertical">
                                                                                        <asp:GridView ID="GridUkuran" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            HorizontalAlign="Center" OnRowCommand="GridUkuran_RowCommand" OnSelectedIndexChanged="GridUkuran_SelectedIndexChanged"
                                                                                            Width="100%">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Kode" HeaderText="Kode" />
                                                                                                <asp:BoundField DataField="INType" HeaderText="Daftar Ukuran" />
                                                                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                                            </Columns>
                                                                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                            <PagerStyle BorderStyle="Solid" />
                                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20px" valign="middle">
                                                                                    &nbsp;<asp:ImageButton ID="btnSave2" runat="server" Height="16px" ImageUrl="~/images/Save.png"
                                                                                        ToolTip="Simpan" Visible="False" Width="18px" OnClick="btnSave2_Click" />
                                                                                </td>
                                                                                <td style="width: 214px">
                                                                                    <asp:TextBox ID="txtAddUkuran" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtUkuran_TextChanged"
                                                                                        TabIndex="1" Visible="False" Width="214px"></asp:TextBox>
                                                                                </td>
                                                                                <td align="right" style="width: 26px">
                                                                                    <asp:ImageButton ID="btnAdd2" runat="server" BackColor="Silver" ImageUrl="~/images/add.gif"
                                                                                        Style="width: 18px" ToolTip="Tambah" Width="26px" OnClick="btnAdd2_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td style="font-size: small;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 10%; font-size: small;">
                                                                    Merk [4]
                                                                </td>
                                                                <td style="font-size: small">
                                                                    <asp:TextBox ID="txtMerk" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" 
                                                                        OnTextChanged="txtMerk_TextChanged" TabIndex="1" Width="160px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="Autocompleteextender1" runat="server" CompletionInterval="200"
                                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetMerk" ServicePath="AutoComplete.asmx" TargetControlID="txtMerk">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <asp:ImageButton ID="btnSearch0" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                                        Height="16px" ImageUrl="~/Images/cute_ball_search.png" OnClick="btnSearch0_Click"
                                                                        Text="Search" ToolTip="Cari" Width="20px" />
                                                                    <asp:Panel ID="PanelItemMerk" runat="server" Height="131px" Visible="False">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:GridView ID="GridItemMerk" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                                                        OnRowCommand="GridItemMerk_RowCommand" Width="100%">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Kode" HeaderText="Kode" />
                                                                                            <asp:BoundField DataField="INMerk" HeaderText="Daftar Merk" />
                                                                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                                        </Columns>
                                                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;<asp:ImageButton ID="btnSave0" runat="server" Height="16px" ImageUrl="~/images/Save.png"
                                                                                        OnClick="btnSave0_Click" ToolTip="Simpan" Visible="False" Width="18px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAddMerk" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtUkuran_TextChanged"
                                                                                        TabIndex="1" Visible="False" Width="214px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnAdd0" runat="server" BackColor="Silver" ImageUrl="~/images/add.gif"
                                                                                        OnClick="btnAdd0_Click" Style="width: 18px" ToolTip="Tambah" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10%; font-size: small;">Jenis [5]
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <asp:TextBox ID="txtJenis" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtJenis_TextChanged"
                                                                        TabIndex="1" Width="160px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="Autocompleteextender3" runat="server" CompletionInterval="200"
                                                                        CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetJenis" ServicePath="AutoComplete.asmx" TargetControlID="txtJenis">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td style="width: 1%; font-size: small;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 10%; font-size: small;">Part Number [6]
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPartNum" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        onkeyup="this.value=this.value.toUpperCase()" OnTextChanged="txtPartNum_TextChanged"
                                                                        TabIndex="1" Width="160px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: small; width: 100px;">
                                                    Nama Item
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtItemName" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                        TabIndex="2" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 26px; width: 100px;">
                                                    <span style="font-size: small"><span style="font-size: small">Kode Item</span></span>
                                                </td>
                                                <td colspan="" style="font-size: small;">
                                                    <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        ReadOnly="true " Width="150px"></asp:TextBox><font size="small" style="font-stretch: extra-expanded">123456789012345</font>
                                                </td>
                                                <td colspan="2" style="height: 26px; font-size: 9px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <span style="font-size: small"><span style="font-size: small">Satuan</span> </span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSatuan" runat="server" TabIndex="9" Width="100px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td colspan="">Lead Time&nbsp;</td>
                                                <td colspan="2"><asp:TextBox ID="leadTime" runat="server">0</asp:TextBox> Hari
                                                &nbsp;</tr>
                                            <tr>
                                                <td style="width: 110px">
                                                    <span style="font-size: small"><span style="font-size: small">Keterangan</span> </span>
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" TabIndex="12"
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr >
                                                <td style="border-top:2px solid">&nbsp;</td>
                                                <td style="border-top:2px solid" colspan="4"><span style="font-size:x-small">Digit (1-2)Kode Group + (3-6)Kode Nama  + (7-9)noUrut per kode nama + (10-11)Kode Type +(12-13) Ukuran+ (14-15)Kode Merk </span></td>
                                            </tr>
                                        </table>
                                <!--</asp:Panel>-->
                                </div>
                                <div style="width: 100%; height: 250px; border:2px solid #B0C4DE">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse:collapse">
                                        <%--<tr>
                                            <td style="height: 6px" valign="top" colspan="5">
                                            </td>
                                        </tr>--%>
                                        <tr style="background-color:#C0C0C0">
                                            <td style="height: 19px">
                                                Send&nbsp; Email To :&nbsp;
                                                <asp:Label ID="LblEmail" runat="server" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtEmailApprover" runat="server" CssClass="txtbox" ReadOnly="True"
                                                    Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="height: 19px">
                                                &nbsp;
                                            </td>
                                            <td style="height: 19px;" colspan="2">
                                                <asp:Label ID="resultMailSucc" runat="server" class="result_done" Height="20px" Visible="False"
                                                    BackColor="White" ForeColor="Lime" Font-Size="X-Small"></asp:Label>
                                                <asp:Label ID="resultMailFail" runat="server" class="result_fail" Height="20px" Visible="False"
                                                    ForeColor="Red"></asp:Label>
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 19px; width: 375px;">
                                                <asp:RadioButton ID="RBListImprovement" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="a" OnCheckedChanged="RBListImprovement_CheckedChanged" Text="List Improvement" />
                                                <asp:RadioButton ID="RBListInventory" runat="server" AutoPostBack="True" GroupName="a"
                                                    OnCheckedChanged="RBListInventory_CheckedChanged" Text="List Inventory" />
                                                <asp:RadioButton ID="RBListAsset" runat="server" GroupName="a" OnCheckedChanged="RBListAsset_CheckedChanged"
                                                    Text="List Asset" AutoPostBack="True" />
                                                    
                                            </td>
                                            <td align="center" style="height: 19px; width: 457px;">
                                                <asp:Label ID="Label4" runat="server" Text="Server" Visible="False"></asp:Label>
                                                
                                                <asp:RadioButton ID="RBCiteureup" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="b" Text="Citeureup" OnCheckedChanged="RBCiteureup_CheckedChanged"
                                                    Visible="False" />
                                                <asp:RadioButton ID="RBKarawang" runat="server" AutoPostBack="True" GroupName="b"
                                                    OnCheckedChanged="RBKarawang_CheckedChanged" Text="Karawang" Visible="False" />
                                                    <asp:RadioButton ID="RBJombang" runat="server" AutoPostBack="True" GroupName="b"
                                                    OnCheckedChanged="RBJombang_CheckedChanged" Text="Jombang" Visible="False" />
                                                    
                                                <asp:TextBox ID="TextCariNama" runat="server" AutoPostBack="True" OnTextChanged="TextCariNama_TextChanged"
                                                    Visible="False"></asp:TextBox>
                                                <asp:ImageButton ID="btnSearch3" runat="server" CssClass="MyImageButton" Font-Bold="true"
                                                    Height="16px" ImageUrl="~/images/search.png" OnClick="btnSearch3_Click" Text="Search"
                                                    ToolTip="Cari" Width="20px" Visible="False" />
                                                <asp:Label ID="Label3" runat="server" Text="List By"></asp:Label>
                                                <asp:RadioButton ID="RBImprovement" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="c" OnCheckedChanged="RBImprovement_CheckedChanged" Text="Improvement" />
                                                <asp:RadioButton ID="RBApproval" runat="server" AutoPostBack="True" GroupName="c"
                                                    OnCheckedChanged="RBApproval_CheckedChanged" Text="Approval" />
                                                    <asp:RadioButton ID="RBUnApv" runat="server" GroupName="c" OnCheckedChanged="RBUnApv_CheckedChanged"
                                                    Text="Un Approved" AutoPostBack="True" />
                                            </td>
                                            <td align="center" style="height: 19px; width: 100px;" width="200">
                                                <asp:Button ID="btnApproveAll" runat="server" Text="Approve All" OnClick="ApproveALL" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 3px;" valign="top">
                                                <div style="width: 100%; padding:5px">
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                        Width="100%" PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Item">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                                            <asp:BoundField DataField="UOMDesc" HeaderText="Satuan" />
                                                            <asp:BoundField DataField="Approvalstatus" HeaderText="Status Approval" />
                                                            <asp:BoundField DataField="UnitKerja" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LastModifiedTime" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                            <asp:BoundField DataField="leadtime" HeaderText="LeadTime" />
                                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                        </Columns>
                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridBarang" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"
                                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="GroupDescription" HeaderText="Group" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="Kode Item">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                                            <asp:BoundField DataField="UOMDesc" HeaderText="Satuan" />
                                                        </Columns>
                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" Font-Size="X-Small" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 3px;" valign="top">
                                                <asp:Panel ID="PanelEmail" runat="server" BorderColor="#CCCCCC" Visible="False">
                                                    <table bgcolor="#CCFFCC" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 92px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 377px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 93px">
                                                                <asp:Label ID="LStock" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="X-Small"
                                                                    Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small">Item Type</span>&nbsp;
                                                            </td>
                                                            <td style="width: 377px">
                                                                <asp:Label ID="LddlItemType" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                            </td>
                                                            <td style="width: 93px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small"><span style="font-size: small">Group&nbsp;&nbsp;&nbsp;
                                                                </span></span>
                                                            </td>
                                                            <td style="width: 377px">
                                                                <asp:Label ID="LddlGroup" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                            </td>
                                                            <td style="font-size: small; width: 93px;">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 377px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="font-size: small; width: 93px;">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small"><span style="font-size: small">Kode Item</span></span>
                                                            </td>
                                                            <td style="width: 377px">
                                                                <asp:Label ID="LtxtItemCode" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                            </td>
                                                            <td style="width: 93px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small">Nama Item</span>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:Panel ID="Panel3" runat="server" Font-Size="X-Small">
                                                                    <table style="width: 100%; background-color: #99CCFF; font-size: small;">
                                                                        <tr>
                                                                            <td style="width: 72px">
                                                                                <span style="font-size: small">Nama</span>
                                                                            </td>
                                                                            <td style="width: 309px">
                                                                                <asp:Label ID="LtxtNama" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 75px">
                                                                                Merk
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LtxtMerk" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 72px">
                                                                                Type
                                                                            </td>
                                                                            <td style="width: 309px">
                                                                                <asp:Label ID="LtxtType" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 75px">
                                                                                Jenis
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LtxtJenis" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 72px">
                                                                                Ukuran
                                                                            </td>
                                                                            <td style="width: 309px">
                                                                                <asp:Label ID="LtxtUkuran" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 75px">
                                                                                Part Number
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LtxtPartNum" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:Label ID="LtxtItemName" runat="server" Font-Size="X-Small" Style="font-weight: 700"
                                                                        Text="Label"></asp:Label>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small"><span style="font-size: small">Satuan</span> </span>
                                                            </td>
                                                            <td style="width: 377px">
                                                                <asp:Label ID="LddlSatuan" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                            </td>
                                                            <td style="width: 93px">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 92px">
                                                                <span style="font-size: small"><span style="font-size: small">Keterangan</span> </span>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:Label ID="LtxtKeterangan" runat="server" Font-Size="X-Small" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                                &nbsp;
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
