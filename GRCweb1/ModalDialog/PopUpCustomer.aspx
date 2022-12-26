<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpCustomer.aspx.cs" Inherits="GRCweb1.ModalDialog.PopUpCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            document.title = "TPP NCC";
        });
        

    </script>
</head>
<body style="padding:3px; background-color:Silver " >
    <form id="form1" runat="server">
    <div>
    <table style=" border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 50%">
                                Input Nama Customer
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                <asp:Button  ID="btnClose" runat="server" Text="Close" OnClick="BtnClose_ServerClick"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content" style="width:500px">
                        <table style="border-collapse:collapse">
                            <tr>
                                <td width="25%">Customer Name</td>
                                <td><asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="false" Width="100%"></asp:TextBox></td>
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
