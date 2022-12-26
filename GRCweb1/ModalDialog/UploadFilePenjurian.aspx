<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFilePenjurian.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadFilePenjurian" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<base target="_self" />    
    <title>Upload File Soasialisasi PES</title>
    <link href="../Script/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Script/Calendar.css" type="text/css" rel="Stylesheet" />
    <link href="../Script/text.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Script/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Script/calendar2.js"></script>
    
</head>

<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerpes" runat="server" AsyncPostBackTimeout="10000000" ScriptMode="Release">
    </asp:ScriptManager>
    <div style="width:800px; padding:5px;">
    <table style="width: 100%; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 50%"><strong>UPLOAD DOCUMENT PENJURIAN</strong></td>
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
                        <tr>
                            <td>Tanggal Penjurian</td>
                            <td>
                        <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px"
                            Width="151px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                            TargetControlID="txtdrtanggal">
                        </cc1:CalendarExtender>
                                    </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30%">File Name</td>
                            <td style="width:60%">
                                <asp:FileUpload ID="Upload1" runat="server" Width="80%" />
                            </td>
                            <td style="width:10%"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
                        </tr>
                    </table>
                            <hr />
                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" Font-Size="x-small"><i> </i></asp:Label>

                    </div>
                 </td>
              </tr>
          </table>
    </div>
    </form>
</body>
</html>
