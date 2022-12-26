<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POParsialArmada.aspx.cs" Inherits="GRCweb1.ModalDialog.POParsialArmada" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />    
    <title></title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            document.title = "Armada";
        });
    </script>
</head>
<body style="padding:3px; background-color:Silver " >
    <form id="form1" runat="server">
    <div>
    <table style="width: 840px; border-collapse: collapse;">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 70%">
                                <b>ARMADA UNTUK DO KE <asp:Label ID="Judul" runat="server"></asp:Label></b>
                            </td>
                            <td style="padding-right: 10px" align="right">
                                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_CLick" />
                                <input type="button" id="btnClose" onclick="js:window.close()" value="Close" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content">
                        <table style="width:100%; border-collapse:collapse; font-size:small">
                            <tr class="tbHeader">
                                <th class="kotak" style="width:5%">&nbsp;</th>
                                <th class="kotak" style="width:10%">No Polisi</th>
                                <th class="kotak" style="width:10%">Kendaraan</th>
                                <th class="kotak" style="width:5%">Ritase</th>
                                <th class="kotak" style="width:20%">Driver</th>
                                <th class="kotak" style="width:20%">CO Driver</th>
                                <th class="kotak" style="width:6%">Jam</th>
                                <th class="kotak" style="width:20%">Tujuan Kirim</th>
                            </tr>
                            <asp:Repeater ID="lstPlant" runat="server" OnItemDataBound="lstPlant_DataBound">
                                <ItemTemplate>
                                    <tr class="total baris">
                                        <td class="kotak">&nbsp;</td>
                                        <td class="kotak" colspan="7"><%# Eval("Lokasi") %><asp:TextBox ID="idlok" runat="server" Visible="false" Text='<%# Eval("DepoID") %>'></asp:TextBox></td>
                                    </tr>
                                    <asp:Repeater ID="lstNoPol" runat="server" OnItemDataBound="lstNoPol_DataBound">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak tengah"><asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_Click" AutoPostBack="true" ToolTip='<%# Eval("ID") %>'  /></td>
                                                <td class="kotak" style="white-space:nowrap; overflow:hidden">&nbsp;&nbsp;&nbsp;<%# Eval("NoPol") %></td>
                                                <td class="kotak" style="white-space:nowrap; overflow:hidden"><%# Eval("ItemName") %></td>
                                                <td class="kotak tengah"><asp:Label ID="rts" runat="server" Width="100%" ToolTip='<%# Eval("NoPol") %>'></asp:Label></td>
                                                <td class="kotak"><asp:DropdownList ID="txtDriver" runat="server" Enabled="false" Width="100%"></asp:DropdownList></td>
                                                <td class="kotak"><asp:DropdownList ID="txtCoDriver" runat="server" Enabled="false" Width="100%"></asp:DropdownList></td>
                                                <td class="kotak"><asp:TextBox ID="txtJam" AccessKey='<%# Container.ItemIndex+1 %>' runat="server" Enabled="false" Width="100%"></asp:TextBox></td>
                                                <td class="kotak"><asp:DropDownList ID="ddlTujuan" runat="server" Enabled="false" ToolTip='<%# Eval("NoPol") %>'></asp:DropDownList></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="oddrows baris">
                                        <td class="kotak tengah"><asp:CheckBox ID="chks" runat="server" OnCheckedChanged="chk_Click" AutoPostBack="true" ToolTip='<%# Eval("DepoID") %>'  /></td>
                                        <td class="kotak"><asp:TextBox ID="txtNPol" runat="server" Width="100%" CssClass="" Text='E 8359 MA'></asp:TextBox></td>
                                        <td class="kotak">&nbsp;</td>
                                        <td class="kotak">&nbsp;</td>
                                        <td class="kotak"><asp:DropdownList ID="txtDriver" runat="server" Enabled="false" Width="100%"></asp:DropdownList></td>
                                        <td class="kotak"><asp:DropdownList ID="txtCoDriver" runat="server" Enabled="false" Width="100%"></asp:DropdownList></td>
                                        <td class="kotak"><asp:TextBox ID="txtJam" AccessKey='' runat="server" Enabled="false" Width="100%"></asp:TextBox></td>
                                        <td class="kotak"><asp:DropDownList ID="ddlTujuan" runat="server" Enabled="false" ToolTip=''></asp:DropDownList></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            
                            <tr><td colspan="5">
                                    <asp:TextBox ID="jmlMobil" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="SchID" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="POID" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="schDate" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
      </table>        
    </div>
    </form>
</body>
</html>