<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T3BAInput.aspx.cs" Inherits="GRCweb1.Modul.Factory.T3BAInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .main-container {
            display: block;
            content: "";
            position: absolute;
            z-index: -2;
            width: 100%;
            max-width: inherit;
            bottom: 0;
            top: 0;
            background-color: #FFF;
            overflow-x: auto;
        }
        label {
            font-weight: 400;
            font-size: 10px;
        }
    </style>
    <script type="text/javascript">
  // fix for deprecated method in Chrome / js untuk bantu view modal dialog
  if (!window.showModalDialog) {
     window.showModalDialog = function (arg1, arg2, arg3) {
        var w;
        var h;
        var resizable = "no";
        var scroll = "no";
        var status = "no";
        // get the modal specs
        var mdattrs = arg3.split(";");
        for (i = 0; i < mdattrs.length; i++) {
           var mdattr = mdattrs[i].split(":");
           var n = mdattr[0];
           var v = mdattr[1];
           if (n) { n = n.trim().toLowerCase(); }
           if (v) { v = v.trim().toLowerCase(); }
           if (n == "dialogheight") {
              h = v.replace("px", "");
           } else if (n == "dialogwidth") {
              w = v.replace("px", "");
           } else if (n == "resizable") {
              resizable = v;
           } else if (n == "scroll") {
              scroll = v;
           } else if (n == "status") {
              status = v;
           }
        }
        var left = window.screenX + (window.outerWidth / 2) - (w / 2);
        var top = window.screenY + (window.outerHeight / 2) - (h / 2);
        var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        targetWin.focus();
     };
  }
</script>
    <script language="Javascript" type="text/javascript" src="../../Script/calendar.js"></script>

    <script type="text/javascript">
        function confirmation() {
            if (confirm('Yakin mau hapus data ?')) {
                return true;
            } else {
                return false;
            }
        }
        function OpenDialog(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileT3_BA.aspx?ba=" + id + "&tablename=T3_BAAttachment", "UploadFile", params);
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table cellpadding="0" cellspacing="0" style="table-layout: fixed" width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 279px">
                                        <strong>BERITA ACARA TAHAP 3</strong>
                                    </td>
                                    <td align="right">
                                        <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px;" type="button" value="Baru" />&nbsp;
                                        <input id="btnSimpan" runat="server" onserverclick="btnSimpan_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px; width: 63px;" type="button" value="Simpan" />
                                        <input id="btnLampiran" runat="server" onserverclick="btnLampiran_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px;" type="button" value="Upload Lampiran" disabled="disabled" />&nbsp;
                                        <asp:DropDownList ID="ddlCari" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="NoBA">No. BA</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtCariBA" runat="server" BorderStyle="Groove" Height="20px" Width="208px"></asp:TextBox>
                                        &nbsp;<input id="btnCari" runat="server" onserverclick="btnCari_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px;" type="button" value="Cari" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="font-size: x-small; font-weight: bold;">
                                    </td>
                                    <td align="right" style="font-size: x-small;">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table id="tabelInput" align="right" style="width: 100%; font-size: x-small;">
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;No. Berita Acara
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNoBA" runat="server" BorderStyle="Groove" Height="20px" Width="235px"
                                                            AutoPostBack="True" OnTextChanged="txtNoBA_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="width: 7px">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        Partno
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Height="20px" Width="182px" OnTextChanged="txtPartnoA_TextChanged"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                            ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td rowspan="5" valign="top">
                                                        <table width="100%" style="font-size: x-small">
                                                            <thead>
                                                                <tr>
                                                                    <td>
                                                                        List Lampiran
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td bgcolor="#CCCCFF">
                                                                        <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                                            <HeaderTemplate>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr class="EvenRows baris">
                                                                                    <td>
                                                                                        <%# Eval("FileName") %>
                                                                                    </td>
                                                                                    <td align="right" width="25%">
                                                                                        <asp:ImageButton ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                                                            CommandName="pre" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Logo_Download.png"
                                                                                            ToolTip="Click to Preview Document" />
                                                                                        <asp:ImageButton ID="hapus" runat="server" AlternateText='<%# Eval("BAID") %>' CommandArgument="<%# Container.ItemIndex %>"
                                                                                            CommandName="hps" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Delete.png"
                                                                                            ToolTip="Click for delete attachment" />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Tgl.&nbsp; Berita Acara
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTglBA" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            OnTextChanged="txtTglBA_TextChanged" Width="235px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                            TargetControlID="txtTglBA">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td align="left" style="width: 7px">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        Quantity
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtQty1" runat="server" BorderStyle="Groove" Height="21px" OnPreRender="txtQty1_PreRender"
                                                            OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="RBIn" runat="server" AutoPostBack="True" Checked="True" 
                                                            GroupName="A" Text="Adjust In" />
                                                        <asp:RadioButton ID="RBOut" runat="server" AutoPostBack="True" GroupName="A" 
                                                            OnCheckedChanged="RBOut_CheckedChanged" Text="Adjust Out" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Type&nbsp; Berita Acara
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlKeterangan" runat="server" Width="235px">
                                                            <asp:ListItem>PERMINTAAN PRODUK</asp:ListItem>
                                                            <asp:ListItem>PRODUK MASUK</asp:ListItem>
                                                            <asp:ListItem>KONSENSI</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 7px">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        Dari/Ke Lokasi</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtLokasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Height="21px"  Visible="True" Width="48px"></asp:TextBox><cc1:AutoCompleteExtender
                                                                ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasi">
                                                            </cc1:AutoCompleteExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;</td>
                                                    <td align="left">
                                                        <input ID="btnAddItem" runat="server" onserverclick="btnTansfer_ServerClick" style="background-color: white;
                                                            font-weight: bold; font-size: 11px;" type="button" value="Add Item" /></td>
                                                    <td align="left" style="width: 7px">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                         Keterangan&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" 
                                                            Height="20px" Width="235px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left" style="width: 7px">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="font-size: x-small; font-weight: bold;">
                                                        <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                                            PageSize="22" Width="100%">
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                                                <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                                                <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                                            </Columns>
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="font-size: x-small; font-weight: bold;">
                                                    <hr />
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="font-size: x-small; font-weight: bold;">
                                                        List Berita Acara Tahap 3
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="font-size: x-small; font-weight: bold;">
                                                        <asp:Panel ID="Panel9" runat="server" Height="200px" ScrollBars="Vertical">
                                                            <asp:GridView ID="GridItemList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                                                PageSize="22" Width="100%">
                                                                <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                    <asp:BoundField DataField="BADate" DataFormatString="{0:d}" 
                                                                        HeaderText="Tanggal" />
                                                                    <asp:BoundField DataField="BANo" HeaderText="No BA" />
                                                                    <asp:BoundField DataField="BAType" HeaderText="BA Type" />
                                                                    <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                                                    <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                    <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                                                    <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                                    <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                                                </Columns>
                                                                <PagerStyle BorderStyle="Solid" />
                                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
