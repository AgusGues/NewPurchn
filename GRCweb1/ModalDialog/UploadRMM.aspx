<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadRMM.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadRMM" %>

<head id="Head1" runat="server">
<base target="_self" />    
    <title>Upload File</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
        .style1
        {
            width: 100px;
            font-family: Calibri;
            font-size: x-small;
            font-weight: 100;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; padding:5px;">
    <table style="width: 100%; border-collapse: collapse;" bgcolor="#66CCFF">
        <tr>
            <td style="height: 49px; width: 100%">
                <table class="nbTableHeader">
                    <tr>
                        <td style="width: 50%">
                            <strong>UPLOAD DOKUMEN</strong>
                        </td>
                        <td style="width: 20%">                           
                        </td>
                        <td style="padding-right: 10px" align="right">                            
                             <input type="button" id="btnClose" onclick="js:window.close();return false;" value="Close" />                            
                        </td>
                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="font-size: x-small">
            <td>
                <div class="content">
                    <table style="border-collapse: collapse; width: 100%">  
                        <tr>
                            <td class="style1">
                                File Name
                            </td>
                            <td style="width: 80%">
                                <asp:FileUpload ID="Upload1" runat="server" Width="80%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" Font-Size="x-small"><i>All File Format!!</i></asp:Label>
                </div>
            </td>
        </tr>
          </table>
    </div>
    </form>
</body>
</html>
