<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReasonLockUnlock.aspx.cs" Inherits="GRCweb1.ModalDialog.ReasonLockUnlock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Div1" runat="server">
        <table style="table-layout: fixed; height: 100%" width="100%">
            <tbody>
                <tr>
                    <td style="width: 100%; height: 49px" bgcolor="gray">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Label ID="Label3" runat="server" Text="ALASAN LOCK/ UNLOCK" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                </td>
                                
                                <td style="width: 37px">
                                    <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Clear Text"
                                        onserverclick="btnNew_ServerClick" />
                                </td>
                                <td style="width: 75px">
                                    <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update"
                                        onserverclick="btnUpdate_ServerClick" />
                                </td>
                                <td style="width:5px">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="100%" style="width: 100%">
                        <div style="overflow: hidden; height: 100%; width: 100%;" class="content" >
                            <table class="tblForm" id="Table4" style="width: 100%;">
                                <tr>
                                    <td>
                                    <hr />
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td style="height: 6px; width:100%;" valign="top">
                                        <asp:TextBox ID="txtAlasanCancel" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td >
                                    <hr />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
