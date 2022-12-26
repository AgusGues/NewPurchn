<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadLampiran.aspx.cs" Inherits="GRCweb1.Modul.ISO.UploadLampiran" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload Lampiran</title>

    <script type="text/javascript">
        function uploadStart() {
            document.getElementById('<%# lblMsg.ClientID %>').innerHTML = "";
        }

        function uploadSuccess() {
            document.getElementById('frm1').setAttribute('src', 'PreviewLampiran.aspx');
        }

        function uploadFail() {
            document.getElementById('<%# lblMsg.ClientID %>').innerHTML = "Upload Gagal";

            document.getElementById('frm1').setAttribute('src', 'PreviewLampiran.aspx');
        }
    </script>

</head>
<body>
    
   <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server" />
  
    <asp:Panel ID="PanelUpload" runat="server" Visible="true">
     <table>
      <tr>
       <td>File Upload : </td>
       <td>
        <asp:AsyncFileUpload ID="AjaxfileUpload" OnClientUploadStarted="uploadStart" OnClientUploadComplete="uploadSuccess"
         OnClientUploadError="uploadFail" CompleteBackColor="White" Width="350px" runat="server"
         UploaderStyle="Traditional" UploadingBackColor="#CCFFFF" ThrobberID="imgLoading" OnUploadedComplete="fileUploadSuccess"/>
       </td>
       <td><asp:Image ID="imgLoading" runat="server" ImageUrl="~/Resource_Web/images/loading.gif" Width="17" Height="17"/></td>
       <td><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
      </tr>
     </table>  
    </asp:Panel>
    <hr/>

    <iframe id="frm1" src="PreviewLampiran.aspx" width="100%" height="5000px" frameborder="0" scrolling="no" ></iframe>
    
    </form>
        
</body>
</html>
