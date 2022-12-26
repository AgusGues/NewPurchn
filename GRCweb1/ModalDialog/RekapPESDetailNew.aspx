<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RekapPESDetailNew.aspx.cs" Inherits="GRCweb1.ModalDialog.RekapPESDetailNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REKAP PES</title>
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" AsyncPostBackTimeout="100000">
    </asp:ScriptManager>
    <div>
        <table style="width:100%; padding:5px">
            <tr>
                <td style="height:49px; width:100%">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width:60%"><asp:Label ID="txtJudul" runat="server" Text="REKAP KPI"></asp:Label></td>
                            <td style="width:40%; padding-right:10px" align="right">
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
                        <table style="width:100%; border-collapse:collapse;">
                            <tr>
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:13%">DEPARTEMEN</td>
                                <td style="width:50%; border-bottom:1px solid">:&nbsp;<asp:Label ID="txtDept1" runat="server" Visible="true"></asp:Label></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>PIC NAME</td>
                                <td style=" border-bottom:1px solid">:&nbsp;<asp:Label ID="txtPIC1" runat="server" Visible="true"></asp:Label></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>JABATAN</td>
                                <td style=" border-bottom:1px solid">:&nbsp;<asp:Label ID="txtJabatan1" runat="server" Visible="true"></asp:Label></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <hr />
                        <div class="contentlist" style="height:360px;width:99%" >
                                <table  id="thr" style="border-collapse:collapse; font-size:small; table-layout:fixed; font-family:Arial; width: 100%;" border="0">
                                    <thead>
                                        <tr class="tbHeader" id="hd1" runat="server">
                                            <th class="kotak" rowspan="2" style="width:50%; "></th>
                                            <th class="kotak" rowspan="2" style="width:50px">Bobot</th>
                                            <th class="kotak" colspan="2">Jan</th>
                                            <th class="kotak" colspan="2">Feb</th>
                                            <th class="kotak" colspan="2">Mar</th>
                                            <th class="kotak" colspan="2">Apr</th>
                                            <th class="kotak" colspan="2">Mei</th>
                                            <th class="kotak" colspan="2">Jun</th>
                                            <th class="kotak" colspan="2">Jul</th>
                                            <th class="kotak" colspan="2">Ags</th>
                                            <th class="kotak" colspan="2">Sep</th>
                                            <th class="kotak" colspan="2">Okt</th>
                                            <th class="kotak" colspan="2">Nov</th>
                                            <th class="kotak" colspan="2">Des</th>
                                            <th class="kotak" rowspan="2" style="width:60px">Total</th>
                                        </tr>
                                        <tr class="tbHeader" id="hd2" runat="server">
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                            <th class="kotak tengah" style="width:40px">Score</th>
                                            <th class="kotak tengah" style="width:40px">Point</th>
                                        </tr>
                                     </thead>
                                     <tbody>
                                        <asp:Repeater ID="lstPES" runat="server" OnItemDataBound="lstPES_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris" id="ps1" runat="server">
                                                    <td class="kotak" style="white-space:nowrap; overflow:hidden; width: 50%;" title='<%# Eval("PESName") %>'>
                                                        <%# Container.ItemIndex+1 %>. <%# Eval("PESName") %>
                                                    </td>
                                                    <td class="kotak tengah"><%# Eval("Bobot", "{0:N0}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("JanB", "{0:N1}") : Eval("JanN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Jan", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("FebB", "{0:N1}") : Eval("FebN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Feb", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("MarB", "{0:N1}") : Eval("MarN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Mar", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("AprB", "{0:N1}") : Eval("AprN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Apr", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("MeiB", "{0:N1}") : Eval("MeiN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Mei", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("JunB", "{0:N1}") : Eval("JunN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Jun", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("JulB", "{0:N1}") : Eval("JulN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Jul", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("AgsB", "{0:N1}") : Eval("AgsN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Ags", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("SepB", "{0:N1}") : Eval("SepN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Sep", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("OktB", "{0:N1}") : Eval("OktN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Okt", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("NopB", "{0:N1}") : Eval("NopN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Nop", "{0:N1}")%></td>
                                                    <td class="kotak tengah"><%#(int.Parse(Eval("Urutan").ToString()) == 999) ? Eval("DesB", "{0:N1}") : Eval("DesN", "{0:N1}")%></td>
                                                    <td class="kotak tengah OddRows"><%# Eval("Des", "{0:N1}")%></td>
                                                    <td class="kotak tengah bold baris">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                            
                                        </asp:Repeater>
                                     </tbody>
                                 </table>
                               <hr />
                                 <table id="rbt" runat="server" visible="false" style="width:100%; border-collapse:collapse; font-size:small">
                                    <%--<tr><td colspan="6"><b><i>Keterangan :</i></b></td></tr>--%>
                                    <tr style="height:20px">
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:2%" class="rebobot">&nbsp;</td>
                                        <td style="width:10%; white-space:nowrap; padding-left:10px">Target Semesteran</td>
                                        <td style="width:3%" >&nbsp;</td>
                                        <td style="width:2%" class="kotak rebobot2">&nbsp;</td>
                                        <td style="width:10%; white-space:nowrap;padding-left:10px">Target Tahunan</td>
                                        <td style="width:3%" >&nbsp;</td>
                                        <td style="width:2%" class="kotak Line3">&nbsp;</td>
                                        <td style="width:15%; white-space:nowrap;padding-left:10px">Pencapaian Nilai Rebobot</td>
                                        <td>&nbsp;</td>
                                    </tr>
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
