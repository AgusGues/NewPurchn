<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterAssetNumeric.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterAssetNumeric" %>
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
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" style="background-color: ButtonFace">
                <table style="width: 100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;Kode Barang Asset Numeric</strong>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnRefresh" runat="server" visible="true" onserverclick="btnRefresh_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Refresh" />
                                        </td>
                                         <td style="width: 70px">
                                            <input id="btnSaveAssetKomp" runat="server" visible="false" onserverclick="btnSaveAssetKomp_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Simpan" />
                                        </td>
                                      <td style="width: 70px">
                                            <input id="btnSave" runat="server" visible="false" onserverclick="btnSave_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px;" type="button" value="Simpan" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ItemName">Nama Asset</asp:ListItem>
                                                <asp:ListItem Value="ItemCode">Kode Asset</asp:ListItem>
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
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color: #B0C4DE;">
                                    <table style="width: 100%; border-collapse: collapse" border="0">
                                        <tr style="width: 100%;">
                                            <td style="width: 20%;">
                                            </td>
                                            <td style="font-size: small; width: 85%; background-color: #CCCCCC;" align="left"
                                                colspan="4">
                                                <asp:RadioButton ID="RBAssetUtama" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="d" OnCheckedChanged="RBAssetUtama_CheckedChanged" Text="Asset"
                                                    Style="font-family: Calibri; font-weight: 700" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RBAssetKomponen" runat="server" AutoPostBack="True" GroupName="d"
                                                    OnCheckedChanged="RBAssetKomponen_CheckedChanged" Text="Komponen Asset" Style="font-family: Calibri;
                                                    font-weight: 700" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="PanelAssetUtama" runat="server" Visible="true">
                                        <table style="width: 100%; border-collapse: collapse" border="0">
                                            <tr style="width: 100%;">
                                                <td style="width: 25%; font-weight: 700; font-family: 'Sitka Banner'; font-size: small;"
                                                    colspan="3">
                                                    <span style="font-size: small">INPUTAN KODE ASSET</span>
                                                </td>
                                                <td style="width: 75%;" colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 25%;" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Jenis Asset
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="True" OnTextChanged="ddlJenis_Change"
                                                        TabIndex="3" Width="219px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Dept Pemilik Asset
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlDeptID" runat="server" TabIndex="3" Width="219px" AutoPostBack="True"
                                                        OnTextChanged="ddlDeptID_Change">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Group Asset
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlGroupAsset" runat="server" AutoPostBack="True" OnTextChanged="ddlGroupAsset_Change"
                                                        TabIndex="1" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Class Asset
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <%--  <asp:DropDownList ID="ddlClasAsset" runat="server" TabIndex="2" Width="250px" AutoPostBack="True"
                                                    OnTextChanged="ddlClasAsset_Change">
                                                </asp:DropDownList>--%>
                                                    <asp:TextBox ID="txtClassAsset" runat="server" AutoPostBack="True" CssClass="txtUpper"
                                                        OnTextChanged="txtClassAsset_Change" Enabled="false" TabIndex="5" Width="100%"
                                                        Text="Ketik nama Class disini" onfocus="if(this.value==this.defaultValue)this.value='';"
                                                        onblur="if(this.value=='')this.value=this.defaultValue;" placeholder="Ketik nama Class disini"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetNamaClass" ServicePath="AutoCompleteAsset.asmx"
                                                        TargetControlID="txtClassAsset" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small; font-style: italic;"
                                                    colspan="3">
                                                    &nbsp; Jika nama Class/Asset belum ada silahkan buat di sini >>
                                                    <asp:Button ID="btnNewClass" runat="server" OnClick="btnNewClass_ServerClick" Text="New Class"
                                                        Style="font-family: Calibri" />
                                                    <asp:Button ID="btnNewAsset" runat="server" OnClick="btnNewAsset_ServerClick" Text="New Asset"
                                                        Style="font-family: Calibri" />
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <%--<td style="width: 15%; font-family: Calibri; font-size: x-small;">Nama Asset</td>--%>
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    <asp:Label ID="LabelNamaAsset" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small;">&nbsp</asp:Label>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;</td>
                                                <td style="width: 2%;">
                                                    <asp:DropDownList ID="ddlSubClassAsset" runat="server" AutoPostBack="True" OnTextChanged="ddlSubClassAsset_Change"
                                                        TabIndex="4" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Nama Asset Baru :
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle" colspan="2">
                                                    <asp:TextBox ID="txtCLassNew" runat="server" CssClass="txtUpper" AutoPostBack="True"
                                                        OnTextChanged="txtCLassNew_Change" Enabled="false" TabIndex="5" Width="100%"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetNamaAssetTunggal" ServicePath="AutoCompleteAsset.asmx"
                                                        TargetControlID="txtCLassNew" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp; 
                                                <asp:Label ID="LabelSatuan" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small;">&nbsp</asp:Label></td>
                                                
                                                <td style="width: 2%;">
                                                    <asp:Label ID="LabelKomaSatuan" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlSatuan0" runat="server" Visible="false" AutoPostBack="True" OnTextChanged="ddlSatuan0_Change"
                                                        TabIndex="9" Width="150px">
                                                    </asp:DropDownList>
                                                </td>      
                                                
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Kode Asset :
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle" colspan="2">
                                                    <asp:TextBox ID="txtKodeIndukAsset" runat="server" ReadOnly="true " Style="font-family: Calibri;
                                                        text-align: center;" Width="100px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Visible="false" Style="font-family: 'Courier New', Courier, monospace;
                                                        font-size: small; font-weight: 700; color: #0000CC; font-style: italic; background-color: #99FFCC;"></asp:Label></td>
                                            </tr>
                                            <tr style="width: 100%;">
                                               <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    <asp:Label ID="LabelLokasi" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small;">&nbsp</asp:Label>
                                                </td>
                                                <td style="width: 2%;">
                                                    <asp:Label ID="LabelKomaLokasi" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small;"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="LokasiID0" runat="server" Visible="false" CausesValidation="True" Height="24px"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td style="width: 2%; font-family: Calibri; font-size: x-small;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 44%; font-family: Calibri; font-size: xx-small; text-align: left;
                                                    font-style: italic; font-weight: 700;" colspan="3">
                                                    <span style="font-size: x-small">Digit (1) Kode Plant + (2) Group + (3-5) Kode Class
                                                        + (6) Kode Dept + (7-9) Urutan</span>
                                                </td>
                                            </tr> 
                                            <tr style="width: 100%;">
                                                <td style="width: 15%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 25%; font-family: Calibri;" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                           <%-- <tr>
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">
                                                    Satuan
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlSatuan0" runat="server" AutoPostBack="True" OnTextChanged="ddlSatuan0_Change"
                                                        TabIndex="9" Width="150px">
                                                    </asp:DropDownList>
                                                </td>                                                
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small; font-style: italic;"
                                                    colspan="3">
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr> --%>
                                            <%--<tr>
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">
                                                    Lokasi
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="LokasiID0" runat="server" CausesValidation="True" Height="24px"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr> --%>                                        
                                            
                                            <%--Tambahan--%>
                                            <asp:Panel ID="PanelAssetTunggalDetail" runat="server" Visible="false">
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Type
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:TextBox ID="txtType" runat="server" CssClass="txtUpper" Enabled="false" Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Ukuran
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:TextBox ID="txtUkuran" runat="server" CssClass="txtUpper" Enabled="false" TabIndex="5"
                                                        Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small; height: 23px;">&nbsp;
                                                    Merk
                                                </td>
                                                <td style="width: 2%; height: 23px;">
                                                    :
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:TextBox ID="txtMerk" runat="server" CssClass="txtUpper" Enabled="false" TabIndex="5"
                                                        Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 2%; height: 23px;">
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small; height: 23px;">
                                                </td>
                                                <td style="width: 2%; height: 23px;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;
                                                    height: 23px;" valign="middle">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Jenis
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:TextBox ID="txtJenis" runat="server" CssClass="txtUpper" Enabled="false" TabIndex="5"
                                                        Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    PartNumber
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:TextBox ID="txtPartNumber" runat="server" CssClass="txtUpper" Enabled="false"
                                                        TabIndex="5" Width="100%"></asp:TextBox>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Satuan
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlSatuan" runat="server" AutoPostBack="True" OnTextChanged="ddlSatuan_Change"
                                                        TabIndex="9" Width="150px">
                                                    </asp:DropDownList>
                                                </td>                                                
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small; font-style: italic;"
                                                    colspan="3">
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Lokasi
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="LokasiID" runat="server" CausesValidation="True" Height="24px"
                                                        Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%; font-family: Calibri; font-size: x-small;">
                                                </td>
                                                <td style="width: 2%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%; font-family: Calibri; font-size: x-small;">&nbsp;
                                                    Lead Time
                                                </td>
                                                <td style="width: 2%;">
                                                    :
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:TextBox ID="txtLeadTime" runat="server" AutoPostBack="True" CssClass="txtUpper"
                                                        OnTextChanged="txtLeadTime_Change" Enabled="false" TabIndex="5" Width="20%" Style="font-size: x-small"></asp:TextBox>
                                                    <span style="font-family: Calibri; font-style: italic; font-size: x-small">&nbsp;hari
                                                    </span>
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 11%;">
                                                </td>
                                                <td style="font-size: x-small; width: 44%; font-family: Calibri; text-align: left;"
                                                    valign="middle" colspan="2">
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td style="width: 15%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 2%;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 25%; font-family: Calibri;" colspan="5">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                        
                                    </table>
                                    </asp:Panel>
                                    <%--End Panel Inputan Asset Utama--%>
                                    
                                    <asp:Panel ID="PanelKomponenAsset" runat="server" Visible="false">
                                        <table style="width: 100%; border-collapse: collapse" border="0">
                                        <tr style="width: 100%;">
                                            <td style="font-weight: 700; font-family: 'Sitka Banner'; font-size: small;" 
                                                colspan="3">
                                                <span style="font-size: small">INPUTAN KODE ASSET ( KOMPONEN )</span>
                                            </td>
                                            <td style="width: 70%;" colspan="3">&nbsp;</td>                         
                                        </tr>
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%; font-family: Calibri; font-size: x-small;">Dikerjakan oleh 
                                                Dept</td>
                                            <td style="width: 1%;">:</td>
                                            <td style="width: 25%;" colspan="5">
                                                <asp:DropDownList ID="ddlDeptID0" runat="server" AutoPostBack="True" 
                                                    OnTextChanged="ddlDeptID0_Change" TabIndex="3" Width="219px">
                                                </asp:DropDownList>
                                                <asp:RadioButton ID="RBBaru" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="d" OnCheckedChanged="RBBaru_CheckedChanged" Text="Buat Baru"
                                                    Style="font-family: Calibri; font-weight: 700" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RBLama" runat="server" AutoPostBack="True" GroupName="d"
                                                    OnCheckedChanged="RBLama_CheckedChanged" Text="Improvment" Style="font-family: Calibri;
                                                    font-weight: 700" />
                                            </td>                                       
                                        </tr>
                                        
                                            <tr style="width: 100%;">
                                                <td style="width: 16%; font-family: Calibri; font-size: x-small;">
                                                    Kode Asset </td>
                                                <td style="width: 1%;">
                                                    :</td>
                                                <td colspan="5" style="width: 25%;">
                                                    <asp:DropDownList ID="ddlIndukAsset" runat="server" AutoPostBack="True" 
                                                        OnTextChanged="ddlIndukAsset_Change" TabIndex="1" Width="750px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%; font-family: Calibri; font-size: x-small;">Nama Komponen</td>
                                            <td style="width: 1%;">:</td>                                          
                                            <td style="width: 25%;" colspan="5">
                                                <asp:TextBox ID="txtNamaKomponenAsset" runat="server" AutoPostBack="True" CssClass="txtUpper" OnTextChanged="txtNamaKomponenAsset_Change" TabIndex="5" Width="100%"></asp:TextBox>
                                                
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetNamaKomponenAssetTemp" ServicePath="AutoCompleteAsset.asmx"
                                                TargetControlID="txtNamaKomponenAsset" UseContextKey="true">
                                            </cc1:AutoCompleteExtender>
                                            
                                            </td>                                           
                                        </tr>
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%; height: 21px;"></td>
                                            <td style="width: 1%; height: 21px;"></td>
                                            <td style="width: 25%; font-family: Calibri; font-size: x-small; font-style: italic; height: 21px; font-weight: 700;" 
                                                valign="top">&nbsp;Bisa barang SparePart / Biaya(Jasa)</td>
                                            <td style="width: 2%; height: 21px;"></td>
                                            <td style="width: 20%; height: 21px;"></td>
                                            <td style="width: 2%; height: 21px;"></td>
                                            <td style="width: 39%; height: 21px;"></td>
                                        </tr> 
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%; font-family: Calibri; font-size: x-small;">LeadTime</td>
                                            <td style="width: 1%;">:</td>                                          
                                            <td style="width: 25%; font-family: Calibri;" colspan="1">
                                                <asp:TextBox ID="txtLeadTime2" runat="server" AutoPostBack="True" CssClass="txtUpper" OnTextChanged="txtLeadTime2_Change"  TabIndex="5" Width="30%"></asp:TextBox>
                                                <i><span style="font-size: x-small">hari</span></i>                                            
                                               
                                            
                                            </td>                                           
                                        </tr>  
                                        
                                        <tr>
                                                <td style="width: 16%; font-family: Calibri; font-size: x-small;">
                                                    Satuan</td>
                                                <td style="width: 1%;">
                                                    :</td>
                                                <td style="width: 25%;">
                                                    <asp:DropDownList ID="ddlSatuan2" runat="server" TabIndex="9" Width="90%">
                                                    </asp:DropDownList>
                                                </td>
                                            <td style="width: 2%; height: 21px;"></td>
                                            <td style="width: 20%; height: 21px;"></td>
                                            <td style="width: 2%; height: 21px;"></td>
                                            <td style="width: 39%; height: 21px;"></td>
                                            </tr>  
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%; font-family: Calibri; font-size: x-small;">Kode Komponen Asset</td>
                                            <td style="width: 1%;">:</td>                                          
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtKomponenAsset" runat="server" ReadOnly="true" CssClass="txtUpper" TabIndex="5" Width="60%"></asp:TextBox>
                                            </td>
                                            <td style="width: 2%;">&nbsp;</td>
                                            <td style="width: 20%;">&nbsp;</td>
                                            <td style="width: 2%;">&nbsp;</td>
                                            <td style="width: 39%;">&nbsp;</td>
                                        </tr>                                   
                                        
                                        <tr style="width: 100%;">
                                            <td style="width: 16%;">&nbsp;</td>
                                            <td style="width: 1%;">&nbsp;</td>
                                            <td style="width: 25%; font-family: Calibri;" colspan="5">
                                            </td>
                                        </tr>
                                        
                                    </table>
                                    </asp:Panel>
                                </div>
                                <div style="width: 100%; height: 250px; background-color: #9999FF;">
                                <asp:Panel ID="PanelGridAssetUtama" runat="server" Visible="true">
                                    <table id="Table4" class="tblForm" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <td colspan="5" style="height: 3px;" valign="top">&nbsp;&nbsp;List :
                                                <div style="width: 100%; padding: 5px">
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                                        Style="font-family: Calibri; font-size: x-small" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            </asp:BoundField>                                                          
                                                            <asp:BoundField DataField="NamaGroup" HeaderText="Group Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NamaClass" HeaderText="Kelas Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Wrap="True" />
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NamaDepartment" HeaderText="Department">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundField>                                                          
                                                             <asp:BoundField DataField="KodeAsset" HeaderText="Kode Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            </asp:BoundField>                                                            
                                                            <asp:BoundField DataField="NamaAsset" HeaderText="Nama Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Wrap="True" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="TipeAssetS" HeaderText="Tipe">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Wrap="True" />
                                                            </asp:BoundField>                                                         
                                                            <asp:BoundField DataField="TglBuat" HeaderText="Tgl Buat">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Width="30%" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" 
                                                                    Width="30%" />
                                                                <HeaderStyle Width="15%" />
                                                                <ItemStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundField>                                                            
                                                        </Columns>
                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                     </asp:Panel>
                                     
                                      <asp:Panel ID="PanelGridKomponen" runat="server" Visible="false">
                                    <table id="Table1" class="tblForm" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <td colspan="5" style="height: 3px;" valign="top">&nbsp;&nbsp;List :
                                                <div style="width: 100%; padding: 5px">
                                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        HorizontalAlign="Center" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowCommand="GridView2_RowCommand"
                                                        Style="font-family: Calibri; font-size: x-small" Width="100%">
                                                        <Columns>     
                                                                                                                                                               
                                                            <asp:BoundField DataField="KodeAsset" HeaderText="Kode Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Wrap="True" Width="10%" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="10%" VerticalAlign="Middle" HorizontalAlign="Center" />                                                              
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="NamaAsset" HeaderText="Nama Asset">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Wrap="True" Width="20%"/>
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="20%" />                                                               
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="KodeKomponenAsset" HeaderText="Kode Komponen">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Width="13%" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="13%" VerticalAlign="Middle" HorizontalAlign="Center" />                                                                
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="NamaKomponenAsset" HeaderText="Nama Komponen">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Width="32%" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="32%" />                                                                
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="Satuan" HeaderText="Satuan">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Width="10%"/>
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="10%" />                                                                
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="TglBuat" HeaderText="Tanggal Buat">
                                                                <HeaderStyle BackColor="#999999" Font-Bold="True" Font-Names="Calibri" Font-Size="X-Small"
                                                                    ForeColor="Black" Width="15%" />
                                                                <ItemStyle Font-Names="Calibri" Font-Size="X-Small" Width="15%" Wrap="True" VerticalAlign="Middle" HorizontalAlign="Center" />                                                              
                                                            </asp:BoundField>
                                                            
                                                        </Columns>
                                                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                     </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
