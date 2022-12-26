<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PODistrribusiDialog.aspx.cs" Inherits="GRCweb1.ModalDialog.PODistrribusiDialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"><base />    
    <title></title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {

        });
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <table style="width: 850px; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 50%">
                                INPUT PO DISTRIBUSI
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <input type="button" id="btnClose" onclick="js:window.close()" value="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content" style="width:855px">
                        <table style="width:850px; border-collapse:collapse">
                            <tr>
                                <td style="width:2%">&nbsp;</td>
                                <td style="width:10%">PO No</td>
                                <td style="width:15%"><asp:TextBox ID="NoPO" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                <td style="width:2%">&nbsp;</td>
                                <td style="width:10%">Supplier Name</td>
                                <td style="width:15%"><asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="true" Width="100%"></asp:TextBox></td>
                                <td style="width:5%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Dikirim Via</td>
                                <td><asp:DropDownList ID="ddlKirim" runat="server">
                                    <asp:ListItem Value="">-Pilih--</asp:ListItem>
                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                    <asp:ListItem Value="Fax">Fax</asp:ListItem>
                                    <asp:ListItem Value="Langsung">Langsung</asp:ListItem>
                                </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td><td><asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>
                                <td>Dikirim Tanggal</td>
                                <td><asp:TextBox ID="txtKirimDate" runat="server" ></asp:TextBox> 
                                    <a onclick="showCalendarControl(txtKirimDate)" href="#"><img src="../images/dtpJavaScript.gif" border="0" /></a></td>
                                <td>&nbsp</td>
                                <td><%--<cc1:CalendarExtender ID="cc1Cal" runat="server" TargetControlID="txtKirimDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%></td>
                                <td>&nbsp</td>
                                <td>&nbsp;</td>                                            
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>DiTerima By</td>
                                <td><asp:TextBox ID="txtTerimaBy" runat="server"></asp:TextBox></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>DiTerima Tanggal</td>
                                <td><asp:TextBox ID="txtTerimaDate" runat="server" ReadOnly="true"></asp:TextBox>
                                <a onclick="showCalendarControl(txtTerimaDate)" href="#"><img src="../images/dtpJavaScript.gif" border="0"  alt=""/></a>
                                </td>
                                <td>&nbsp;</td>
                                <td><%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTerimaDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Est. Delivery</td>
                                <td><asp:TextBox ID="txtEstDelivery" runat="server"></asp:TextBox>
                                <a onclick="showCalendarControl(txtEstDelivery)" href="#"><img src="../images/dtpJavaScript.gif" border="0" /></a>
                                </td>
                                <td>&nbsp;</td>
                                <td><%--<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEstDelivery" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Act. Delivery</td>
                                <td><asp:TextBox ID="txtActDelivery" runat="server"></asp:TextBox>
                                <a onclick="showCalendarControl(txtActDelivery)" href="#"><img src="../images/dtpJavaScript.gif" border="0" /></a>
                                </td>
                                <td>&nbsp;</td>
                                <td><%--<cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtActDelivery" Format="dd-MMM-yyyy"></cc1:CalendarExtender>--%></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Keterangan</td>
                                <td colspan="3"><asp:TextBox ID="txtKeterangan" TextMode="MultiLine" Rows="3" Width="85%" runat="server"></asp:TextBox></td>
                                <td>&nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td colspan="7"><hr /></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="right">
                                <%--<asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_onClick" />--%>
                                <input type="button" id="btnSimpan" runat="server" value="Simpan" onserverclick="btnSimpan_onserverclick" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <div class="contentlist" style="height: 140px; width:840px">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>