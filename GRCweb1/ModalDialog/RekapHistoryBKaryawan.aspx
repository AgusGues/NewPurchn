<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RekapHistoryBKaryawan.aspx.cs" Inherits="GRCweb1.ModalDialog.RekapHistoryBKaryawan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HISTORY BAGIAN </title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager2" runat="server" AsyncPostBackTimeout="100000">
    </asp:ScriptManager>--%>
    <div>
        <table style="width: 100%; padding: 5px">
            <tr>
                <td style="height: 49px; width: 100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="txtJudul" runat="server" Text="HISTORY BAGIAN"></asp:Label>
                            </td>
                            <td style="width: 40%; padding-right: 10px" align="right">
                                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                <input type="button" id="btnClose" value="Close" onclick="js:window.close()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="content" id="lstr" runat="server">
                        <table style="width: 100%; border-collapse: collapse; font-family: Calibri;">
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;
                                </td>
                                <td style="width: 13%; font-family: Calibri;">
                                    DEPARTEMEN
                                </td>
                                <td style="width: 50%; border-bottom: 1px solid">
                                    :&nbsp;<asp:Label ID="txtDept1" runat="server" Visible="true" Style="font-family: Calibri"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="font-family: Calibri">
                                    PIC NAME
                                </td>
                                <td style="border-bottom: 1px solid">
                                    :&nbsp;<asp:Label ID="txtPIC1" runat="server" Visible="true" Style="font-family: Calibri"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="font-family: Calibri">
                                    JABATAN TERAKHIR
                                </td>
                                <td style="border-bottom: 1px solid">
                                    :&nbsp;<asp:Label ID="txtJabatan1" runat="server" Visible="true" Style="font-family: Calibri"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div class="contentlist" style="height: 360px; width: 99%">
                            <table id="thr" style="border-collapse: collapse; font-size: small; table-layout: fixed;
                                font-family: Arial" border="0">
                                <thead>
                                    <tr class="tbHeader" id="hd1" runat="server">
                                        <th class="kotak tengah">
                                            DEPARTMENT
                                        </th>
                                        <th class="kotak tengah">
                                            JABATAN
                                        </th>
                                        <th class="kotak tengah">
                                            MULAI
                                        </th>
                                        <th class="kotak tengah">
                                            SAMPAI
                                        </th>
                                    </tr>
                                </thead>
                                <tbody style="font-family: Calibri; font-size: x-small;">
                                    <asp:Repeater ID="lstPES" runat="server" OnItemDataBound="lstPES_DataBound">
                                        <ItemTemplate>
                                            <tr>
                                            <td class="kotak">&nbsp;&nbsp;
                                                    <%# Eval("DeptName")%>
                                                </td>
                                                <td class="kotak">&nbsp;&nbsp;
                                                    <%# Eval("BagianName")%>
                                                </td>
                                                <td class="kotak">&nbsp;&nbsp;
                                                    <%--<%# Eval("MulaiBulan") + " " + "-" + " " +Eval("MulaiTahun")%>--%>
                                                    <%# Eval("Keterangan")%>
                                                </td>
                                                <td class="kotak">&nbsp;&nbsp;
                                                    <%--<%# Eval("SampaiBulan") + " " + "-" + " " +Eval("SampaiTahun")%>--%>
                                                    <%# Eval("KeteranganStatus")%>
                                                </td>                                               
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
