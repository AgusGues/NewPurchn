<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputUangMukaOP.aspx.cs" Inherits="GRCweb1.ModalDialog.InputUangMukaOP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div id="Div1" runat="server" style="padding:5px; width:100%">
            <table style="width:99%; border-collapse:collapse; font-size:x-small">
                <tr>
                    <td style="height:35px; width:100%" class="nbTableHeader" valign="middle">
                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                            <tr>
                                <td style="width:60%; padding-left:10px">
                                    <strong><b>INPUT UANG MUKA</b></strong>
                                </td>
                                <td style="width:40%; padding-right:10px" align="right">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                        onclick="btnBatal_ServerClick" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="content">
                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                <tr visible="false">
                                    <td style="width:20%">No.PO</td>
                                    <td style="width:40%"><asp:TextBox ID="txtNoPO" runat="server" Width="70%"></asp:TextBox></td>
                                    <td style="width:30%">&nbsp;</td>
                                </tr>
                                <thead>
                                <tr class="tbHeader">
                                    <th class="kotak">Pembayaan</th>
                                    <th class="kotak">Jumlah(%)</th>
                                    <th class="kotak">Keterangan</th>
                                </tr>
                                </thead>
                                <tbody>
                                    <tr id="tr1" runat="server">
                                        <td><asp:TextBox ID="txtJbayar" runat="server" Width="100%"  Text="Uang Muka" 
                                                CssClass="txtongrid" ontextchanged="txtJbayar_TextChanged" Visible="False"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtUangMuka" runat="server" Width="100%" Visible="False" ></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtKetUangMuka" runat="server" Width="100%" 
                                                TextMode="MultiLine" Visible="False"></asp:TextBox></td>
                                    </tr>
                                    <tr id="tr2" runat="server">
                                        <td><asp:TextBox ID="txtJbayar1" runat="server" Width="100%" Text="Termin 1" CssClass="txtongrid"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtUangMuka1" runat="server" Width="100%" Text="0"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtKetUangMuka1" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox></td>
                                    </tr>
                                    <tr id="tr3" runat="server">
                                        <td><asp:TextBox ID="txtJbayar2" runat="server" Width="100%" Text="Termin 2" CssClass="txtongrid"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtUangMuka2" runat="server" Width="100%"  Text="0"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtKetUangMuka2" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox></td>
                                    </tr>
                                    <tr id="tr4" runat="server">
                                        <td><asp:TextBox ID="txtJbayar3" runat="server" Width="100%" Text="Termin 3" CssClass="txtongrid"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtUangMuka3" runat="server" Width="100%"  Text="0"></asp:TextBox></td>
                                        <td><asp:TextBox ID="TextBox6" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox></td>
                                    </tr>
                                    <tr id="tr5" runat="server">
                                        <td><asp:TextBox ID="txtJbayar4" runat="server" Width="100%" Text="Termin 4" CssClass="txtongrid"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtUangMuka4" runat="server" Width="100%"  Text="0"></asp:TextBox></td>
                                        <td><asp:TextBox ID="txtKetUangMuka3" runat="server" Width="100%" TextMode="MultiLine" ></asp:TextBox></td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td><td>&nbsp;</td>
                                    <td><asp:Button ID="btnSimpan" runat="server" Text="Simpan Uang Muka" OnClick="btnUpdate_ServerClick" /></td>
                                </tr>
                                </tfoot>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
