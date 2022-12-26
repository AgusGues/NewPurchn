<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PODOEdit.aspx.cs" Inherits="GRCweb1.ModalDialog.PODOEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />    
    <title></title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            document.title = "DO Update";
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
                            <td style="width: 70%">
                                <b>UPDATE DELIVERY ORDER <asp:Label ID="Judul" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <asp:Button ID="btnSimpan" runat="server" Text="Update" OnClick="btnSimpan_CLick" />
                                <%--<asp:Button ID="btnDelete" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                                <input type="button" id="btnClose" onclick="js:window.close()" value="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content">
                    <hr />
                        <table style="width:100%; border-collapse:collapse; font-size:small">
                            <tr>
                                <td style="width:30%">No. PO</td>
                                <td style="width:65%">
                                    <asp:DropDownList ID="ddlPO" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddlPO_Change"></asp:DropDownList>
                                </td>
                                <td><asp:TextBox ID="txtSchPOID" runat="server" Visible="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Supplier Name</td>
                                <td><asp:TextBox ID="txtSupplierName" runat="server" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtSupID" runat="server" Visible="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Material Name</td>
                                <td><asp:TextBox ID="txtMaterial" runat="server" ReadOnly="true" Font-Names="CourierNew" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtItemID" runat="server" Visible="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Outstanding PO</td>
                                <td><asp:TextBox ID="txtOP" runat="server" Width="40%" ReadOnly="false"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Jumlah Mobil</td>
                                <td><asp:TextBox ID="txtQtyMobil" runat="server" Width="40%" AutoPostBack="true" OnTextChanged="txtQtyMobil_Change"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Estimasi Qty</td>
                                <td><asp:TextBox ID="txtEstQty" runat="server" Width="40%"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Keterangan</td>
                                <td><asp:TextBox ID="txtKeterangan" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr><td colspan="3"><hr /></td></tr>
                            <tr>
                                <td>PO Type</td>
                                <td><asp:TextBox ID="txtDelivery" runat="server" Width="40%" ReadOnly="true"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
               </td>
            </tr>
        </table>
    </div>
  </form>
 </body>
</html>