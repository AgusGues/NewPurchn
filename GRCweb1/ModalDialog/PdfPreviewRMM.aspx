<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PdfPreviewRMM.aspx.cs" Inherits="GRCweb1.ModalDialog.PdfPreviewRMM" %>

<%@ Register Assembly="ZetroPDFViewer" Namespace="ZetroPDFViewer" TagPrefix="pdf" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>PDF PReview</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />

</head>
<body style="overflow:hidden">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="overflow:hidden">
        <table style="width:100%; border-collapse:collapse;">
            <tr>
                <td style="width:98%; height:30px">
                <table class="nbTableHeader">
                        <tr>
                            <td style="width: 50%">
                                PDF PREVIEW
                            </td>
                            <td style="padding-right: 20px" align="right">
                                <input type="button" id="btnClose" onclick="js:window.close();return false;" value="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>            
        </table>
            <asp:Literal ID="pdfView" runat="server" Mode="PassThrough"  ></asp:Literal>
    </div>
    </form>
</body>
</html>
