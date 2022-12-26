<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPOPurchn.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListPOPurchn" %>
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
    <script type="text/javascript">
        function openWindow() {
            window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 218%">
                                            <strong>List PO</strong>
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Form PO" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">No PO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 3px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="height: 100%; width: 100%; border:2px solid gray; padding:10px">
                                    <span style="font-size: 10pt">&nbsp; <strong>List</strong></span>
                                    <div id="div2" style="width: 100%; height: 450px; overflow: auto">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                            OnRowCommand="GridView1_RowCommand" 
                                            OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                                            <Columns>
                                                <asp:BoundField DataField="NoPO" HeaderText="No. PO" />
                                                <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                                <asp:BoundField DataField="Qty" HeaderText="Q t y" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                                <asp:BoundField DataField="Price" DataFormatString="{0:N0}" 
                                                    ItemStyle-HorizontalAlign="Right" HeaderText="H a r g a" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
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
