<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormAdjustment.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel() {

        }

        function Cetak() {
            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipMRS", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        function confirm_delete() {
            if (confirm("Anda yakin untuk Cancel ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
    </script>

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel"
                ConfirmOnFormSubmit="false" />
            <div id="Div1" runat="server">
                <table style="table-layout: fixed; height: 100%; width: 100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;Adjustment</strong>
                                        </td>
                                        <td style="width: 100%">
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold;
                                                font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnList" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="List" onserverclick="btnList_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">Adj No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                                <div height: 100%; width: 100%; background-color: #B0C4DE">
                                    <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                        cellpadding="0" border="0">
                                        <tr>
                                            <td style="width: 249px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 204px; height: 3px" valign="top">
                                            </td>
                                            <td style="height: 3px; width: 102px;" valign="top">
                                            </td>
                                            <td style="width: 209px; height: 3px; font-weight: 700;" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; height: 3px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; No Adj</span>
                                            </td>
                                            <td style="width: 204px; height: 3px" valign="top">
                                                <asp:TextBox ID="txtPakaiNo" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="height: 3px; width: 102px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Stok</span>
                                            </td>
                                            <td style="width: 209px; height: 3px" valign="top">
                                                <asp:TextBox ID="txtStok" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" Enabled="False" Font-Bold="True" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>                                            
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Tipe Barang
                                            </td>
                                            <td style="" valign="top">
                                                <asp:DropDownList ID="ddlTipeBarang" runat="server" AutoPostBack="True" 
                                                    Width="233px" onselectedindexchanged="ddlTipeBarang_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                                &nbsp; Tanggal&nbsp;
                                            </td>
                                            <td style="width: 209px; " valign="top">
                                                <asp:TextBox ID="txtTanggal" runat="server" BorderStyle="Groove" Height="20px" Width="128px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtTanggal">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 205px; " valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        
                                        <%-- Tambahan 14-01-2021 --%>
                                        <tr id="tr1" runat="server" visible="false">
                                            <td style="width: 249px;" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="" valign="top" colspan="2">
                                                <asp:RadioButton ID="RBTunggal" runat="server"  Text="Tunggal" Visible="true" Checked="true" 
                                                AutoPostBack="True" OnCheckedChanged="RBTunggal_CheckedChanged" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="RBKomponen" runat="server" Text="Komponen" Visible="true" 
                                                AutoPostBack="True" OnCheckedChanged="RBKomponen_CheckedChanged" /> 
                                            </td>
                                            <td style="" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        
                                          <%-- Tambahan 29-03-2021 --%>
                                        <tr id="tr3" runat="server" visible="false">
                                            <td style="width: 249px;" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="" valign="top" colspan="2">
                                                <asp:RadioButton ID="rbStok" runat="server" GroupName="C" Text="Stok" Visible="true" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rbNonStok" runat="server" GroupName="C" Text="Rett Off" Visible="true" /> 
                                                
                                            </td>
                                            <td style="" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        
                                         <%-- End Tambahan 14-01-2021 --%>
                                         
                                        <tr id="tr2" runat="server" visible="false">
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Tipe Adjusment&nbsp;
                                            </td>
                                            <td style="" valign="top" colspan="2">
                                                <asp:RadioButton ID="rbTambah" runat="server" GroupName="B" Text="Tambah" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="rbKurang" runat="server" GroupName="B" Text="Kurang" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="rbDisposal" runat="server" GroupName="B" Text="Disposal" Visible="false" />
                                            </td>
                                            
                                            <td style="" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Keterangan
                                            </td>
                                            <td colspan="3" style="" valign="top">
                                                <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" Width="567px"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Cari Nama Barang&nbsp;
                                            </td>
                                            <td style="" valign="top" colspan="3">
                                                <asp:TextBox ID="txtCariNamaBrg" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="567px"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; " valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Nama Barang&nbsp;
                                            </td>
                                            <td style=";" valign="top" colspan="3">
                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="567px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px; " valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Kode Barang
                                            </td>
                                            <td style="; width: 204px;" valign="top">
                                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="; width: 102px;" valign="top">
                                                &nbsp; Qty
                                            </td>
                                            <td style="width: 209px; " valign="top">
                                                <asp:TextBox ID="txtQtyPakai" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; " valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; " valign="top">
                                                &nbsp; Satuan
                                            </td>
                                            <td style=";" valign="top">
                                                <asp:TextBox ID="txtUom" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style=";" valign="top">
                                                &nbsp; Dibuat Oleh
                                            </td>
                                            <td style=";" valign="top">
                                                <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; " valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px; height: 19px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td rowspan="1" style="width: 204px; height: 19px;">
                                                <asp:TextBox ID="txtUomID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Visible="False" Width="52px"></asp:TextBox>
                                                <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Visible="False" Width="52px" Height="22px"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px; height: 19px">
                                                <span style="font-size: 10pt">&nbsp;</span>
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;<asp:LinkButton ID="lbAddOP" runat="server" Font-Size="10pt" OnClick="lbAddOP_Click">Tambah</asp:LinkButton>
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="border: 2px solid #B0C4DE; padding: 5px; background-color: White">
                                        <table id="Table2" style="left: 0px; top: 0px; width: 95%;">
                                            <tr align="left">
                                                <td style="height: 3px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; <strong>List Adjustment</strong></span>
                                                </td>
                                                <td style="height: 3px" valign="top" align="right">
                                                    <asp:CheckBox ID="ChkAll0" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                        OnCheckedChanged="ChkAll0_CheckedChanged" Text="Tampilkan Menu Approval" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 100%" valign="top" width="100" colspan="2">
                                                    <div id="div2" style="width: 997px; height: 320px; overflow: auto; padding: 5px;">
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="AdjustType" HeaderText="Tipe Adj" />
                                                                <asp:BoundField DataField="ItemTypeID" HeaderText="Tipe Brg" />
                                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                                <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                                <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                                                <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                                                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                                                <asp:BoundField DataField="ID" HeaderText="ID">
                                                                    <HeaderStyle Width="0px" />
                                                                    <ItemStyle Width="0px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                        <asp:Panel ID="PanelApprove" runat="server" Visible="False" Width="309px">
                                                            <input id="btnApprove" runat="server" onserverclick="btnApprove_ServerClick" style="background-color: white;
                                                                font-weight: bold; font-size: 11px; width: 63px;" type="button" value="Approve" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                                OnCheckedChanged="ChkAll_CheckedChanged" Text="Approve All" />
                                                        </asp:Panel>
                                                        <asp:GridView ID="GridApprove" runat="server" AutoGenerateColumns="False" PageSize="20"
                                                            Visible="False" Width="100%" AllowPaging="false">
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ItemTypeID" HeaderText="TypeID" />
                                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                                                <asp:TemplateField HeaderText="Approve">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkapv" runat="server" Text="Approve" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="adjustdate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                                <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                                                <asp:BoundField DataField="AdjustType" HeaderText="Type Adj" />
                                                                <asp:BoundField DataField="itemcode" HeaderText="Kode Barang" />
                                                                <asp:BoundField DataField="itemname" HeaderText="Nama Barang" />
                                                                <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                                                <asp:BoundField DataField="uomcode" HeaderText="Satuan" />
                                                                <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                                            </Columns>
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
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
