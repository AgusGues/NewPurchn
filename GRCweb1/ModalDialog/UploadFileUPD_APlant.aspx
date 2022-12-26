<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileUPD_APlant.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadFileUPD_APlant" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />    
    <title>Upload File</title>
    
    
    
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
        .style1
        {
            width: 25%;
            font-family: Calibri;
            font-size: small;
            
        }
        #btnClose
        {
            font-family: Calibri;
            font-size: small;
            }
        .style2
        {
            width: 11%;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
    <div style="width:98%; padding:3px;">
    <table style="width: 100%; border-collapse: collapse;">
        <tr>
            <td style="height: 49px; width: 100%">
                <table class="nbTableHeader">
                    <tr>
                        <td style="width: 50%; font-family: 'Agency FB'; font-size: x-large;">
                            <strong>&nbsp;&nbsp;UPLOAD DOKUMEN ANTAR PLANT</strong>
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
                        <tr style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                                &nbsp;&nbsp;Tujuan
                            </td>
                            <td style="width: 80%">
                                <asp:RadioButton ID="RBC" runat="server" AutoPostBack="True" OnCheckedChanged="RBC_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: medium; text-align: left;
                                    font-style: italic;" Text="&nbsp; Plant Citeureup" TextAlign="Left" 
                                    Width="25%"/>
                                
                                <asp:RadioButton ID="RBK" runat="server" AutoPostBack="True" OnCheckedChanged="RBK_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: medium; text-align: left;
                                    font-style: italic;" Text="&nbsp; Plant Karawang" TextAlign="Left" 
                                    Width="25%"/>
                                
                                <asp:RadioButton ID="RBJ" runat="server" AutoPostBack="True" OnCheckedChanged="RBJ_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: medium; text-align: left;
                                    font-style: italic;" Text="&nbsp; Plant Jombang" TextAlign="Left" 
                                    Width="25%"/>
                                    
                            </td>
                            <td style="width: 10%; font-family: Calibri; font-size: x-small; ">
                                                              
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                                &nbsp;&nbsp;Jenis
                            </td>
                            <td style="width: 80%">
                                <asp:RadioButton ID="RBBiasa" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="RBBiasa_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: medium; text-align: left;
                                    " Text="&nbsp; Dokumen Biasa" TextAlign="Left" 
                                    Width="25%"/>
                                
                                <asp:RadioButton ID="RBKhusus" runat="server" AutoPostBack="True" OnCheckedChanged="RBKhusus_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: medium; text-align: left;
                                    font-style: italic;" Text="&nbsp; Dokumen Khusus" TextAlign="Left" 
                                    Width="25%"/>     
                                    
                            </td>
                            <td style="width: 10%; font-family: Calibri; font-size: x-small; ">
                                                              
                            </td>
                        </tr>
                        
                        <tr id="tr1" runat="server" style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                                &nbsp;&nbsp;Nomor Dokumen
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtNo" runat="server" 
                                    Style="font-family: Calibri; font-size: small;" Width="50%"></asp:TextBox>
                                    
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetNamaBiasa" ServicePath="AutoCompleteUPD2.asmx"
                                                TargetControlID="txtNo" UseContextKey="true">
                                            </ajaxToolkit:AutoCompleteExtender>
                                    
                                    <asp:Button ID="btnCek" runat="server" Text="Cek di plant terkait" 
                                            OnClick="btnCek_ServerClick" 
                                    style="font-family: Calibri; font-size: small;" />
                               <%-- <asp:Label ID="LabelNo" runat="server" Visible="true" Style="font-family: Calibri;
                                    font-size: small; font-weight: 700; color: #FF6600;"></asp:Label>--%>
                            </td>
                            <td style="width: 10%; font-family: Calibri; font-size: x-small; ">
                                                              
                            </td>
                        </tr>
                        
                        <tr id="tr01" runat="server" style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                            </td>
                            <td style="width: 80%">
                                <asp:Label ID="LabelNo" runat="server" Visible="true" Style="font-family: Calibri;
                                    font-size: small; font-weight: 700; color: #FF6600;"></asp:Label>
                            </td>
                            <td style="width: 10%; font-family: Calibri; font-size: x-small;">
                            </td>
                        </tr>
                        
                        <tr id="tr2" runat="server" style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                                &nbsp;&nbsp;Nama Dokumen
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="txtNama" runat="server" Style="font-family: Calibri; font-size: small;"
                                    Width="50%"></asp:TextBox>
                                    
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                    MinimumPrefixLength="1" ServiceMethod="GetNamaKhusus" ServicePath="AutoCompleteUPD2.asmx"
                                    TargetControlID="txtNama" UseContextKey="true">
                                </ajaxToolkit:AutoCompleteExtender>
                                    
                                <asp:Button ID="btnCek2" runat="server" Text="Cek di plant terkait" OnClick="btnCek2_ServerClick"
                                    Style="font-family: Calibri; font-size: small;" />
                               <%-- <asp:Label ID="LabelNama" runat="server" Visible="true" Style="font-family: Calibri;
                                    font-size: small; font-weight: 700; color: #FF6600;"></asp:Label>--%>
                            </td>
                        </tr>
                        
                        <tr id="tr02" runat="server" style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                            </td>
                            <td style="width: 80%">
                                <asp:Label ID="LabelNama" runat="server" Visible="true" Style="font-family: Calibri;
                                    font-size: small; font-weight: 700; color: #FF6600;"></asp:Label>
                            </td>
                            <td style="width: 10%; font-family: Calibri; font-size: x-small;">
                            </td>
                        </tr>
                        
                        <tr style="width: 100%">
                            <td style="font-family: Calibri; font-size: small;" class="style2">
                                &nbsp;&nbsp;Nama File Pdf
                            </td>
                            <td style="width: 80%">
                                <asp:FileUpload ID="Upload1" runat="server" Width="100%" 
                                    Style="font-family: Calibri; font-size: small;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                                    OnClick="btnUpload_Click" 
                                    style="font-family: Calibri; font-size: small;" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" 
                                Font-Size="x-small" style="font-family: 'Times New Roman', Times, serif"><i>Only pdf format</i></asp:Label>
                </div>
            </td>
        </tr>
          </table>
    </div>
     
    </form>
</body>
</html>

