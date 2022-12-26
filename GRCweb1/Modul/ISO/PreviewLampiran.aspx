<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewLampiran.aspx.cs" Inherits="GRCweb1.Modul.ISO.PreviewLampiran" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload Lampiran</title>
</head>
<body>
    <form id="form1" runat="server">  
     
    <asp:Panel ID="PanelPreview" runat="server">
     <table width="100%">
      <tr valign="top">
       <td width="15px"><asp:Button ID="btnPrevious" runat="server" Text="<<" Font-Size="15" OnClick="btnPrev_ServerClick"/></td>
       <td align="center">
        <asp:Label ID="lblImage" runat="server" Text=""></asp:Label><br/>
        <asp:Image ID="Image1" runat="server" />
       </td>
       <td width="15px"><asp:Button ID="btnNext" runat="server" Text=">>" Font-Size="15" OnClick="btnNext_ServerClick"/></td>
      </tr>       
     </table>
    </asp:Panel>
    
    </form>
</body>
</html>
