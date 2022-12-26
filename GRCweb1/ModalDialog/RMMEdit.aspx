<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMMEdit.aspx.cs" Inherits="GRCweb1.ModalDialog.RMMEdit" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>
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
            document.title = "RMM Update";
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
                                &nbsp;<b>UPDATE RMM  <asp:Label ID="Judul" runat="server"></asp:Label></b>
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
                                <td style="width:30%">Aktivitas</td>
                                <td style="width:65%">
                                   <asp:TextBox ID="txtRMM_No" runat="server" Width="90%" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox><%--<asp:DropDownList ID="ddlRMM" runat="server" Width="60%" AutoPostBack="true" Enabled="true" ></asp:DropDownList>  <asp:TextBox ID="txtRMM_No" runat="server" ReadOnly="true"></asp:TextBox>--%></td>
                                </td>
                                <td><asp:TextBox ID="txtRMMID" runat="server" Visible="false" Width="45%"></asp:TextBox></td>
                            </tr>
                           
                            <tr>
                                <td>&nbsp;</td>
                                <td>Solved<asp:CheckBox ID="chksolved" runat="server" Text="" AutoPostBack="True" OnCheckedChanged="chksolved_CheckedChanged"/>
                                &nbsp;&nbsp; <bdp:BDPLite ID="txtDateSolved" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Enabled="true">
                                                </bdp:BDPLite>
                                </td
                                                >
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Keterangan</td>
                                <td><asp:TextBox ID="txtKeterangan" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr><td colspan="3"><hr /></td></tr>
                            <%-- <tr>
                                <td>PO Type</td>
                                <td><asp:TextBox ID="txtDelivery" runat="server" Width="40%" ReadOnly="true"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>--%>
                        </table>
                    </div>
               </td>
            </tr>
        </table>
    </div>
  </form>
 </body>
</html>