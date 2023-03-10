<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileUPD.aspx.cs" Inherits="GRCweb1.ModalDialog.UploadFileUPD" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />    
    <title>Upload File</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    
    <style type="text/css">
        .style1
        {
            width: 200px;
            font-family: Calibri;
            font-size: x-small;
            font-weight: 700;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:800px; padding:5px;">
    <table style="width: 100%; border-collapse: collapse;">
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
                            <td>
                            </td>
                           
                                <td style="width: 80%">
                                    <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        OnCheckedChanged="ChkAll_CheckedChanged" Style="font-family: Calibri; font-weight: 700;
                                        font-size: 9pt;" Text="Pilih semua" TextAlign="Left" />
                                <%--</td>
                                <td style="width: 235px">--%>
                                    <asp:CheckBox ID="Chk01" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        OnCheckedChanged="Chk01_CheckedChanged" Style="font-family: Calibri; font-weight: 700;
                                        font-size: 9pt;" Text="Tidak di Share ke Plant terkait" TextAlign="Left" />
                                </td>
                           
                        </tr>
                        <tr>
                            <td class="style1" valign="top">
                                Pilih Department
                            </td>                                 
                            <td style="width: 70%; height: 100%" valign="top">
                                <div id="div2" style="width: 400px; height: 200px; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="Pilih">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="check" runat="server" Enabled="true" Text="Check List" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DeptName" HeaderText="Nama Department" />
                                        </Columns>
                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                File Name
                            </td>
                            <td style="width: 20%">
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
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" Font-Size="x-small"><i>Only pdf format</i></asp:Label>
                </div>
            </td>
        </tr>
          </table>
    </div>
    </form>
</body>
</html>
