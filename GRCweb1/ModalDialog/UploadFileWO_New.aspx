<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileWO_New.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadFileWO_New" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />    
    <title>Upload File</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; padding:5px;">
    <table style="width: 100%; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader" style="width: 100%">
                        <tr>
                            <td style="width: 50%"><strong>UPLOAD DOCUMENT</strong></td>
                            <td style="width:20%">
                                <div id="divImage" style="display: none; float: left; width: 100px; height: 9px;">
                                    <asp:Image ID="img1" runat="server" Height="10px" ImageUrl="~/Resource_Web/images/loading_animation2.gif" Width="75px" />
                                </div>
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
                    <div class="content">
                    <table style="border-collapse:collapse; width:100%">
                        <%--<tr>
                            <td>Document Name</td>
                            <td><asp:DropDownList ID="ddlDocument" runat="server"></asp:DropDownList></td>
                            <td>&nbsp;</td>
                        </tr>--%>
                        <tr>
                            <td style="width:30%">File Name</td>
                            <td style="width:60%">
                                <asp:FileUpload ID="Upload1" runat="server" Width="100%" />
                            </td>
                            <td style="width:10%"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
                        </tr>
                    </table>
                            <hr />
                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" Font-Size="x-small"><i>Only pdf format</i></asp:Label>

                    </div>
                 </td>
              </tr>
          </table>
    </div>
    </form>
</body>
</html>
