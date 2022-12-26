<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArmadaEdit.aspx.cs" Inherits="GRCweb1.ModalDialog.ArmadaEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />    
    <title></title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
<%--    <script type="text/javascript" src="../Scripts/jquery-imask.js"></script>
    <script type="text/javascript" src="../Scripts/json2.js"></script>
--%>    <script type="text/javascript">
        $(document).ready(function() {
            document.title = "DO Update";
            //var jam = '<%=txtJam.ClientID %>';
            //$('#' + jam).iMask("00:00");
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
                                <b>UPDATE PENGATURAN ARMADA <asp:Label ID="Judul" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <asp:Button ID="btnSimpan" runat="server" Text="Update" OnClick="btnSimpan_CLick" />
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
                                    <asp:TextBox ID="txtNoPO" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td><asp:TextBox ID="txtArmID" runat="server" Visible="false" Width="16%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Material Name</td>
                                <td><asp:TextBox ID="txtItemName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Supplier Name</td>
                                <td><asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>DO Number</td>
                                <td><asp:TextBox ID="txtDoNum" runat="server"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>No. Polisi</td>
                                <td><asp:DropDownList ID="ddlNopol" runat="server" Width="70%"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Driver Name</td>
                                <td><asp:DropDownList ID="ddlDriver" runat="server" Width="70%"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Co Driver Name</td>
                                <td><asp:DropDownList ID="ddlCoDriver" runat="server" Width="70%"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Jam</td>
                                <td><asp:TextBox ID="txtJam" runat="server" Width="30%" ></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Keterangan</td>
                                <td><asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox></td>
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
