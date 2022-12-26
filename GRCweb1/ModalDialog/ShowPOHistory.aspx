<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPOHistory.aspx.cs" Inherits="GRCweb1.ModalDialog.ShowPOHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Scripts/text.css" rel="Stylesheet" type="text/css" />
    <title>History PO</title>
</head>
<body>
    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
    <table style="width: 100%; height: 100%">
        <tr class="nbTableHeader">
            <td style="width: 100%; height: 30px" valign="middle">
                <strong>&nbsp;HISTORY PO & ITEM BARANG</strong>
            </td>
        </tr>
        <tr>
            <td>
                <form id="form1" runat="server">
                <div style="padding: 5px; width: 99%; height: 100px; background-color: White; border: 2px solid #B0C4DE;
                    overflow: auto">
                    Info Item Barang<asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                        HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                        Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">
                        <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                </div>
                <div style="padding: 5px; width: 99%; height: 400px; background-color: White; border: 2px solid #B0C4DE;
                    overflow: auto">
                    History PO<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        PageSize="10" Width="100%" 
                        onpageindexchanging="GridView1_PageIndexChanging">
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
                </div>
                </form>
            </td>
        </tr>
    </table>
        </div>
</body>
</html>
