<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoryPO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.HistoryPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 99px">
                            Search By
                        </td>
                        <td style="width: 132px">
                            <asp:DropDownList ID="ddlSearch" runat="server" Height="16px" Width="239px">
                                <asp:ListItem Value="popurchn.nopo like">NO. PO</asp:ListItem>
                                <asp:ListItem Value="SPP.nospp like">NO. SPP</asp:ListItem>
                                <asp:ListItem Value="POPurchnDetail.ItemID in(select ID from Inventory where ItemCode like">ITEM CODE</asp:ListItem>
                                <asp:ListItem Value=" popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like">ITEM NAME (INVENTORY)</asp:ListItem>
                                <asp:ListItem Value=" popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like">ITEM NAME (ASSET)</asp:ListItem>
                                <%--<asp:ListItem Value=" popurchndetail.itemtypeid=3 and popurchndetail.itemid in (select id from biaya where itemname like">ITEM NAME (BIAYA)</asp:ListItem>--%>
                                <asp:ListItem Value="3">ITEM NAME (BIAYA)</asp:ListItem>
                                <asp:ListItem Value="SuppPurch.SupplierName like">SUPPLIER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 193px">
                            <asp:TextBox ID="TxtValue" runat="server" Width="188px" AutoPostBack="True" OnTextChanged="BtnSearch_Click"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnSearch" runat="server" Height="24px" OnClick="BtnSearch_Click"
                                Text="Cari" Width="56px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                        <td align="left" colspan="2">
                            <asp:RadioButton ID="RBgrid" runat="server" AutoPostBack="True" Checked="True" GroupName="g1"
                                OnCheckedChanged="RBgrid_CheckedChanged" Text="Grid View Mode" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RBReport" runat="server" AutoPostBack="True" GroupName="g1"
                                OnCheckedChanged="RBReport_CheckedChanged" Text="Report View Mode" />
                        </td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Font-Size="X-Small" Height="23px" OnClick="btnExport_Click"
                                Text="Export To Excel" Width="116px" Visible="False" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div style="padding: 5px; width: 100%; background-color: White; border: 2px solid #B0C4DE; overflow:auto">
                    <%--<asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Auto" Width="100%">
                <div style="width:100%; overflow:auto">--%>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="30" Width="100%"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Height="85px">
                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                        <Columns>
                            <asp:BoundField DataField="POPurchnDate" DataFormatString="{0:d}" HeaderText="PO IssueDate" />
                            <asp:BoundField DataField="DlvDate" DataFormatString="{0:d}" HeaderText="Delivery Date" />
                            <asp:BoundField DataField="NoPO" HeaderText="NoPO" />
                            <asp:BoundField DataField="NoSPP" HeaderText="NoSPP" />
                            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                            <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="Price" />
                            <asp:BoundField DataField="CRC" HeaderText="CRC" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
                            <asp:BoundField DataField="Termin" HeaderText="Term of Payment" />
                            <asp:BoundField DataField="Delivery" HeaderText="Term of Delivery" />
                            <asp:BoundField DataField="Disc" HeaderText="Disc" />
                            <asp:BoundField DataField="PPN" HeaderText="PPN" />
                        </Columns>
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                    <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"
                                HasToggleGroupTreeButton="false" DisplayToolbar="false" EnableDatabaseLogonPrompt="False"
                                EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px" />
                    <%-- </div>
                 </asp:Panel>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
