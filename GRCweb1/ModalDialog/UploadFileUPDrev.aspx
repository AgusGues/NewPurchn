<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileUPDrev.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadFileUPDrev" %>

<%@ Register TagPrefix="BDP" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
    
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />    
    <title>Upload File</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
        .style1
        {
            font-family: Calibri;
            font-size: x-small;
            font-weight: 700;
            width: 139px;
        }
        .style4
        {
            height: 6px;
            width: 139px;
        }
        </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; padding:5px;">
    <table style="width: 100%; border-collapse: collapse;" bgcolor="#99CCFF">
        <tr>
            <td style="height: 70px; width: 100%">
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
                <div>
                    <table style="border-collapse: collapse; width: 100%">
                    <tr>
                            <td style="font-family: Calibri; font-weight: 700;" valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp; No. Dokumen</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtNo" runat="server" Width="563px" Enabled="false" 
                                    style="font-family: Calibri; margin-left: 0px;"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-style: italic; color: #0000FF;" 
                                valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Update No. Dokumen</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtUpdateNo" runat="server" Width="563px" Enabled="true" 
                                    style="font-family: Calibri"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-weight: 700;" valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp; Nama Dokumen</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtNamaDokumen" runat="server" Width="563px" Enabled="false" 
                                    style="font-family: Calibri; margin-left: 0px;"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-style: italic; color: #0000FF;" 
                                valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Update Dokumen</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtUpdateNamaDokumen" runat="server" Width="563px" Enabled="true" 
                                    style="font-family: Calibri"></asp:TextBox>
                            </td>
                        </tr>
                    
                        <tr>
                            <td style="font-family: Calibri; font-weight: 700;" valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Revisi No</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtRevisiNo" runat="server" Width="50px" Enabled="false" 
                                    style="font-family: Calibri; margin-left: 0px;"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-style: italic; color: #0000FF;" 
                                valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Update Revisi No</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtUpdateRev" runat="server" Width="50px" Enabled="true" 
                                    style="font-family: Calibri"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-weight: 700;" valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Tgl Berlaku</span>
                            </td>
                            <td style="width: 50px; height: 6px" valign="top">
                                <asp:TextBox ID="txtTglBerlaku" runat="server" Width="151px"  Enabled="false" 
                                    Style="font-family: Calibri"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                         <td style="font-family: Calibri; font-style: italic; color: #0000FF;" 
                                valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp;&nbsp;Update Tgl Berlaku</span>
                            </td>
                            
                            <td style="width: 75px; height: 6px" valign="top">
                                <bdp:BDPLite ID="txtUpdateTglBerlaku" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                    Width="220%" Enabled="true" Height="16px">
                                </bdp:BDPLite>
                            </td>
                        </tr>
                        
                        <tr>
                            <td style="font-family: Calibri; font-weight: 700;" valign="top" class="style4">
                                <span style="font-size: 10pt">&nbsp; File UPD</span>
                            </td>
                            <td>
                                <asp:FileUpload ID="Upload1" runat="server" Width="277px" 
                                    Style="margin-left: 0px; font-family: Calibri; font-weight: 700;" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                    OnClick="btnUpload_Click" style="font-family: Calibri; font-weight: 700" />
                            </td>
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
