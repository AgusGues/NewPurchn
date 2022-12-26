<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpDialog.aspx.cs" Inherits="GRCweb1.ModalDialog.PopUpDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />    
    <title></title> 
    <link href="../Scripts/text.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:98%; border-collapse:collapse; margin:5px; height:100%">
            <tr class="nbTableHeader">
                <td style="width:100%">
                <asp:Label ID="txtCaption" runat="server" Text="Input Keterangan"></asp:Label>
                </td>
                
            </tr>
            <tr class="content" style="padding:5px;">
                <td><asp:TextBox ID="txtKeterangan" runat="server" TextMode="MultiLine" Rows="3" Width="98%"></asp:TextBox></td>
            </tr>
            <tr><td><hr /></td></tr>
            <tr><td align="center" style="padding-right:10px">
            <asp:Button ID="simpan" runat="server" Text="Update" OnClick="simpan_Click" /></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
