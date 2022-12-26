<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POParsialSch.aspx.cs" Inherits="GRCweb1.ModalDialog.POParsialSch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />    
    <title></title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            document.title = "PO Parsial Scheduler";
        });
    </script>
</head>
<body style="padding:3px; background-color:Silver " >
    <form id="form1" runat="server">
    <div>
    <table style="width: 500px; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 50%">
                                INPUT PO FOR DELIVERY ORDER
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                <input type="button" id="btnClose" onclick="js:window.close()" value="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content" style="width:500px">
                        <table style="width:500px; border-collapse:collapse">
                            <tr>
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:15%">PO No</td>
                                <td style="width:25%">
                                    <asp:DropDownList ID="ddlPOx" runat="server" OnTextChanged="ddlPO_Change" 
                                        AutoPostBack="true" Width="100%" 
                                        onselectedindexchanged="ddlPOx_SelectedIndexChanged"></asp:DropDownList></td>
                                <td style="width:5%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                                <td>Supplier Name</td>
                                <td><asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtSupID" runat="server" Visible="false"></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td>&nbsp</td>
                                <td>Material</td>
                                <td><asp:TextBox ID="txtMaterial" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                                <td ><asp:TextBox ID="txtItemID" runat="server" Visible="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                                <td>OutStanding PO</td>
                                <td><asp:TextBox ID="txtOP" runat="server"></asp:TextBox></td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Jumlah Mobil</td>
                                <td><asp:TextBox ID="txtQtyMobil" runat="server" AutoPostBack="true" OnTextChanged="txtQtyMobil_Change"></asp:TextBox></td>
                                
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Estimasi Qty</td>
                                <td><asp:TextBox ID="txtEstQty" runat="server" ReadOnly="true"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Keterangan</td>
                                <td><asp:TextBox ID="txtKeterangan" runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr><td colspan="7"><hr /></td></tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>PO Type</td>
                                <td><asp:TextBox ID="txtDelivery" runat="server" ReadOnly="true"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr><td colspan="7">&nbsp;</td></tr>
                         </table>
                     </div>
                 </td>
              </tr>
            </table>
    </div>
    </form>
</body>
</html>
