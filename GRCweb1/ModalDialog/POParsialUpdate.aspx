<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POParsialUpdate.aspx.cs" Inherits="GRCweb1.ModalDialog.POParsialUpdate" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />    
    <link href="../Scripts/text.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
    <table style="width: 840px; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 70%">
                                <b>UPDATE SCHEDULE <asp:Label ID="Judul" runat="server"></asp:Label></b>
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
                        <table style="width:100%; font-size:small border-collapse:collapse">
                            <tr>
                                <td style="width:10%">&nbsp;</td>
                                <td style="width:10%">Schedule No.</td>
                                <td style="width:20%">
                                <asp:TextBox ID="txtSchNo1" runat="server" Enabled="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlPoNo1" runat="server" Width="70%" OnTextChanged="ddlPoNo_Click" AutoPostBack="true" Visible="false"></asp:DropDownList></td>
                                <td style="width:4%">&nbsp;</td>
                                <td style="width:10%"></td>
                                <td style="width:20%"></td>
                                <td style="width:10%"><asp:HiddenField ID="scID" runat="server" /></td>
                            </tr>
                            <tr style="display:none">
                                <td></td>
                                <td>Supplier Name</td>
                                <td><asp:DropDownList ID="ddlSupplier1" runat="server" Width="100%"></asp:DropDownList></td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td></td>
                                
                            </tr>
                            <tr>
                                <td></td>
                                <td>Item Name</td>
                                <td>
                                <asp:DropDownList ID="ddlMaterial1" runat="server" Width="100%"  >
                                </asp:DropDownList></td>
                                <td></td>
                                <td>Estimasi Qty</td>
                                <td><asp:TextBox ID="txtOutPO1" runat="server" Visible="true" ></asp:TextBox> </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Jumlah Mobil</td>
                                <td><asp:TextBox ID="txtQuantity1" runat="server" AutoPostBack="true" OnTextChanged="txt_onChange1" ></asp:TextBox></td>
                                <td></td>
                                <td>Delivery Date</td>
                                <td><asp:TextBox ID="txtSchDate1" runat="server"></asp:TextBox></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td valign="top">Keterangan</td>
                                <td colspan="3"><asp:TextBox ID="txtKeterangan1" runat="server" Rows="3" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                                <td><cc1:CalendarExtender ID="CA1" runat="server" TargetControlID="txtSchDate1" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td>
                                <td align="right">
                                   <%-- <asp:Button ID="btnAdd" runat="server" Text="Tambah" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" />--%>
                                </td>
                                <%--<td></td>
                                <td></td>--%>
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
