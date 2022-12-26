<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterBarang.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterBarang" %>
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
    </style>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }       
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Label ID="Ljudul" runat="server" Text="Label">MASTER BARANG</asp:Label>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" visible="False" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" visible="False" />
                                        </td>
                                        <td style="width: 5px">
                                            <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick" visible="False" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnPrint" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" visible="False" value="Cetak" onclick="Cetak()" />
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
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color:#fff">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse:collapse">
                                        <tr>
                                            <td style="width: 170px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Item Type</span>&nbsp;
                                            </td>
                                            <td  valign="top">
                                                <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"
                                                    TabIndex="11" Width="233">
                                                    <asp:ListItem Value="1">INVENTORY</asp:ListItem>
                                                    <asp:ListItem Value="2">ASSET</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td  valign="top">
                                                <span style="font-size: 10pt">&nbsp; Group</span>
                                            </td>
                                            <td  valign="top">
                                                <asp:DropDownList ID="ddlGroup" runat="server" TabIndex="11" Width="233">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr id="contoh" runat="server" visible="false">
                                            <td style="width: 170px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp;</span>
                                            </td>
                                            <td colspan="3"  valign="top">
                                                <span style="font-size: 10pt">&nbsp;<b><span style="font-size: 10pt"> Contoh:</span>
                                                </b>
                                                    <asp:Label ID="LContoh" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                                                </span>
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 197px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Kode Item</span>
                                            </td>
                                            <td style=" width: 209px;" valign="top">
                                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="true " Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 170px;" valign="top">
                                                <span style="font-size: 10pt" runat="server" visible="true">&nbsp; Prefix Item</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtPrefix" runat="server" BorderStyle="Groove" Width="233" TabIndex="1"
                                                    AutoPostBack="True" onkeyup="this.value=this.value.toUpperCase()" 
                                                    OnTextChanged="txtPrefix_TextChanged" Visible="true"></asp:TextBox>
                                                
                                            </td>
                                            
                                            <td style="width: 205px;" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Nama Item</span>
                                            </td>
                                            <td style="" valign="top" colspan="3">
                                                <asp:TextBox ID="txtItemName" runat="server" BorderStyle="Groove" Width="530px" TabIndex="2"
                                                    ReadOnly="True"></asp:TextBox>
                                                <asp:Panel ID="PanelNama" runat="server">
                                                    <table bgcolor="#99CCFF" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 99px">
                                                                <span style="font-size: 10pt">Nama</span>
                                                            </td>
                                                            <td style="width: 213px">
                                                                <asp:TextBox ID="txNama" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="209px" OnTextChanged="txNama_TextChanged"
                                                                    OnDataBinding="txNama_DataBinding"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 99px; font-size: x-small;">
                                                                Merk
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMerk" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="160px" OnTextChanged="txtMerk_TextChanged"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 99px; font-size: x-small;">
                                                                Type
                                                            </td>
                                                            <td style="width: 213px">
                                                                <asp:TextBox ID="txtType" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="209px" OnTextChanged="txtType_TextChanged"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 99px; font-size: x-small;">
                                                                Jenis
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtJenis" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="160px" OnTextChanged="txtJenis_TextChanged"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 99px; font-size: x-small;">
                                                                Ukuran
                                                            </td>
                                                            <td style="width: 213px">
                                                                <asp:TextBox ID="txtUkuran" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="209px" OnTextChanged="txtUkuran_TextChanged"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 99px; font-size: x-small;">
                                                                Part Number
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPartNum" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                    onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="160px" OnTextChanged="txtPartNum_TextChanged"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                        <tr id="sppl" runat="server" visible="false">
                                            <td style="width: 170px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Supplier</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="7" Width="233">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Departemen</span>&nbsp;
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:DropDownList ID="ddlDepartemen" runat="server" TabIndex="8" Width="233">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;">
                                                <span style="font-size: 10pt">&nbsp;Jumlah</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtJumlah" runat="server" BorderStyle="Groove" TabIndex="3" 
                                                    Visible="true" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px;">
                                                &nbsp;</td>
                                            <td style="width: 209px;" valign="top">
                                                &nbsp;</td>
                                            <td style="width: 205px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;">
                                                <span style="font-size: 10pt">&nbsp; ReOrder</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtReorder" runat="server" TabIndex="3" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px;">
                                                <span style="font-size: 10pt">&nbsp; Satuan</span>
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:DropDownList ID="ddlSatuan" runat="server" TabIndex="9" Width="233">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 169px;">
                                                <span style="font-size: 10pt">&nbsp; Minimum Stock</span>
                                            </td>
                                            <td style="width: 209px;">
                                                <asp:TextBox ID="txtMinStock" runat="server" BorderStyle="Groove" Width="233" TabIndex="10"></asp:TextBox>
                                            </td>
                                            <td style="width: 170px;">
                                                <span style="font-size: 10pt">&nbsp; Maximum Stock</span>
                                            </td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtHarga" runat="server" BorderStyle="Groove" Width="233" TabIndex="4" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtMaxStock" runat="server" Width="233" TabIndex="11"></asp:TextBox>
                                            </td>
                                            
                                            <td style="width: 205px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;">
                                                <span style="font-size: 10pt">&nbsp; Gudang</span>
                                            </td>
                                            <td style="width: 204px;">
                                                <asp:DropDownList ID="ddlGudang" runat="server" TabIndex="5" Height="16px" Width="63px">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px;">
                                                <span style="font-size: 10pt">&nbsp; Rak</span>
                                            </td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlRak" runat="server" Width="233" TabIndex="11">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;">
                                                <span style="font-size: 10pt">&nbsp; Short Key</span>
                                            </td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtShortKey" runat="server" BorderStyle="Groove" TabIndex="6" Width="59px"></asp:TextBox>
                                                &nbsp; &nbsp;
                                            </td>
                                            <td style="width: 169px;">
                                                <span style="font-size: 10pt">&nbsp; Keterangan</span>
                                            </td>
                                            <td style="width: 204px;">
                                                <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" TabIndex="12"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px; font-size: x-small;">
                                                &nbsp;&nbsp; Lead Time
                                            </td>
                                            <td style="width: 204px; font-size: x-small; top: auto;" valign="middle">
                                                <asp:TextBox ID="txtLeadTime" runat="server" BorderStyle="Groove" TabIndex="6" Width="59px"></asp:TextBox>
                                                (Hari)
                                            </td>
                                            <td style="width: 169px;">
                                                <%-- stock/nonstock--%>
                                                <asp:RadioButton ID="RBStock" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                                                    GroupName="g2" OnCheckedChanged="RBAktif_CheckedChanged" Text="Stock" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RBNonStock" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                    GroupName="g2" OnCheckedChanged="RBNonAktif_CheckedChanged" Text="Non Stock" />
                                            </td>
                                            <td style="width: 204px;">
                                                <asp:RadioButton ID="RBAktif" runat="server" Checked="True" Font-Size="X-Small" GroupName="g1"
                                                    Text="Aktif" AutoPostBack="True" OnCheckedChanged="RBAktif_CheckedChanged" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="RBNonAktif" runat="server" Font-Size="X-Small" GroupName="g1"
                                                    Text="Non Aktif" AutoPostBack="True" OnCheckedChanged="RBNonAktif_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 170px;">
                                                <asp:Label ID="lblErrorName" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                            </td>
                                            <td style="" colspan="3">
                                                <asp:Panel ID="Panel3" runat="server" Visible="true">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 101px; font-size: x-small;">
                                                                Alasan Non Aktif
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlasanNonAktif" runat="server" BorderStyle="Groove" TabIndex="10"
                                                                    Width="570px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 205px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <span style="font-size: 10pt">&nbsp; <strong>List</strong></span>
                                    <div style="border:2px solid #B0C4DE; height:200px; width:100%; padding:10px; background-color:White">
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                            TabIndex="12" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Item">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ItemName" HeaderText="Nama Item" />
                                                <%--<asp:BoundField DataField="SupplierCode" HeaderText="Supplier" />
                                                <asp:BoundField DataField="DeptName" HeaderText="Dept">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>--%>
                                                
                                                <asp:BoundField DataField="UOMDesc" HeaderText="Satuan" />
                                                <asp:BoundField DataField="Jumlah" HeaderText="Jumlah" 
                                                    DataFormatString="{0:N0}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MinStock" HeaderText="Min Stock" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MaxStock" HeaderText="Max Stock" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Stock" HeaderText="Tipe Stock">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Gudang" HeaderText="Gdg">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeadTime" HeaderText="LeadTime" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Aktif" HeaderText="Aktif">
                                                    <%--<ItemStyle Width="20px" />--%>
                                                </asp:BoundField>
                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
