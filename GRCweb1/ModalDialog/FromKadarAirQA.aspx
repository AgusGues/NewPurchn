﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FromKadarAirQA.aspx.cs" Inherits="GRCweb1.ModalDialog.FromKadarAirQA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>From Kadar Air</title>
    <style type="text/css">
        .box {border:1.5px solid #838b8b;}
        .rata_c{text-align:center;}
        .garis_l{border-left:none;}
        .garis_r{border-right:none;}
        .garis_t{border-top:none;}
        .garis_b{border-bottom:none;}
        .nbTableHeader
        {
	        padding: 1;
	        border-collapse: collapse;
		        overflow: scroll;
	        width: 100%;
		        height: 100%;
		        font-size: small;
		        font-family: tahoma;
		        font-weight: bold;
		        background-color: gray;
		        color: #ffff66;
		        border-spacing: 0;
        }
        .style1
        {
            height: 21px;
        }
    </style>
    
</head>
<script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=docka.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>KADAR AIR</title>');
            printWindow.document.write("<style type='text/css'> "+
                                       " .box {border:1.5px solid #838b8b;} "+
                                       " .rata_c{text-align:center;} "+
                                       " .garis_l{border-left:none;} "+
                                       " .garis_r{border-right:none;} "+
                                       " .garis_t{border-top:none;} "+
                                       " .garis_b{border-bottom:none;} "+
                                       " .nbTableHeader "+
                                       " { "+
	                                   "     padding: 1; "+
	                                   "     border-collapse: collapse; "+
		                               "         overflow: scroll; "+
	                                   "     width: 100%; "+
		                               "         height: 100%; "+
		                               "         font-size: small; "+
		                               "         font-family: tahoma; "+
		                               "         font-weight: bold; "+
		                               "         background-color: gray; "+
		                               "         color: #ffff66; "+
		                               "         border-spacing: 0; "+
                                       " } "+
                                       " .tengah{text-align:center;}"+
                                       " </style>");
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            //setTimeout(function () {
                printWindow.print();
            //}, 500);
             printWindow.close();
           return false;
        }
    </script>
<body style="overflow:auto">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" AsyncPostBackTimeout="100000">
    </asp:ScriptManager>
    <div >
        <table class="nbTableHeader" style="border-collapse:collapse">
            <tr>
                <td style="width:50%; padding-left:10px;">
                    <b>KADAR AIR PREVIEW <asp:Label ID="dokKA" runat="server"></asp:Label></b>
                </td>
                <td style="width:50%; padding-right:10px" align="right">
                    <asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClientClick="PrintPanel();"  />
                </td>
            </tr>
        </table>
    </div>
    <div style="padding-right:5px; padding-left:5px; overflow:auto" id="docka" runat="server">
        <table style="width:100%; border-collapse:collapse;" border="0">
            <tr>
                <td style="width:80%; padding-left:10px">PT. BANGUNPERKASA ADHITAMASENTRA</td>
                <td style="width:20%; padding-right:10px; white-space:nowrap" align="right">
                    <asp:Label ID="txtNoFrom" runat="server" Text="Form No. QA/K/FPKA/59/03/R3" Font-Size="Smaller" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width:100%; height:50px" valign="middle" align="center">
                    <h2>FORM PEMERIKSAAN</h2>
                    <h3>KADAR AIR BAHAN BAKU KERTAS</h3>
                </td>
            </tr>
            <tr>
                <td class="box" align="center">HASIL TUSUKAN (*)<td class="box garis_b" valign="middle">
                Jenis Kertas</td>
            </tr>
            <tr>
                <td rowspan="12" class="box" valign="top">
                    <table style="width:100%; border-collapse:collapse;" border="0">
                        <tr class="rata_c">
                            <td style="width:10%" class="box" rowspan="2">Sample Ke</td>
                            <td style="width:10%" class="box" rowspan="2">Nomor Bale</td>
                            <td class="box" colspan="3">Kadar Air</td>
                            <td style="width:10%" class="box" rowspan="2">Sample Ke</td>
                            <td style="width:10%" class="box" rowspan="2">Nomor Bale</td>
                            <td class="box" colspan="3">Kadar Air</td>
                        </tr>
                        <tr class="rata_c">
                            <td style="width:10%" class="box">Tusuk 1</td>
                            <td style="width:10%" class="box">Tusuk 2</td>
                            <td style="width:10%" class="box">Rata2 Kadar Air</td>
                            <td style="width:10%" class="box">Tusuk 1</td>
                            <td style="width:10%" class="box">Tusuk 2</td>
                            <td style="width:10%" class="box">Rata2 Kadar Air</td>
                        </tr>
                        <tbody>
                            <asp:Repeater ID="lstTusuk" runat="server">
                                <ItemTemplate>
                                    <tr class="rata_c box">
                                        <%--<td class="box"><%# Eval("NoBall") %> /  <%# Eval("BALKe") %></td>--%>
                                        <td class="box"><%# Eval("BALKe")%></td>
                                        <td class="box"><%#(decimal.Parse(Eval("NoBall").ToString()) == 0) ? "0" : Eval("NoBall")%> </td>
                                        <%--<%#(decimal.Parse(Eval("NoBall").ToString()) == 0) ? "" : "/"%> --%>
                                        <%--<%#(decimal.Parse(Eval("BALKe").ToString()) == 0) ? "" : Eval("BALKe")%></td>--%>
                                        <td class="box"><%# Eval("Tusuk1") %></td>
                                        <td class="box"><%# Eval("Tusuk2") %></td>
                                        <td class="box"><%# Eval("AvgKA") %></td>
                                        
                                        <td class="box"><%# Eval("BALKe1")%></td>
                                        <td class="box"><%#(decimal.Parse(Eval("NoBall1").ToString()) == 0) ? "0" : Eval("NoBall1")%> </td>
                                        <%--<td class="box"><%#(decimal.Parse(Eval("BALKe1").ToString()) == 0) ? Container.ItemIndex + 26 : Eval("BALKe1")%></td>--%>
                                        <td class="box"><%# (decimal.Parse(Eval("BALKe1").ToString())==0)?"":Eval("Tusuk11") %></td>
                                        <td class="box"><%# (decimal.Parse(Eval("BALKe1").ToString())==0)?"":Eval("Tusuk21") %></td>
                                        <td class="box"><%# (decimal.Parse(Eval("BALKe1").ToString())==0)?"":Eval("AvgKA1") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% for (int i = (lstTusuk.Items.Count+1); i <= (25); i++)
                               { %>
                            <tr class="rata_c box">
                                        <td class="box"><%=i %></td>
                                        <td class="box">0</td>
                                        <td class="box">&nbsp;</td>
                                        <td class="box">&nbsp;</td>
                                        <td class="box">&nbsp;</td>
                                        <td class="box"><%=(i+25) %></td>
                                        <td class="box">0</td>
                                        <td class="box">&nbsp;</td>
                                        <td class="box">&nbsp;</td>
                                        <td class="box">&nbsp;</td>
                                    </tr>
                            <% } %>
                        </tbody>
                    </table>
                </td>
                <td class=" box garis_b" valign="middle">&nbsp;</td>
            </tr>
            <tr><td valign="top" class="box garis_t" style="height:40px; padding-left:10px; font-weight:bold">
                <asp:Label ID="txtItemName" runat="server" Text="KERTAS KANTONG SEMEN"></asp:Label></td></tr>
            <tr><td class="box garis_b">Supplier :</tr>
            <tr><td class="box garis_t" style="height:40px; padding-left:10px; font-weight:bold"><asp:Label ID="txtSuppName" runat="server" Text="FUAD HASAN"></asp:Label></td></tr>
            <tr><td class="box garis_b">Tanggal / Jam</td></tr>
            <tr><td class="box garis_t" style="height:40px; padding-left:10px; font-weight:bold"><asp:Label ID="txtTglKA" runat="server"></asp:Label></td></tr>
            <tr><td class="box garis_b">Hasil Timbangan (Kg)</td></tr>
            <tr><td class="box garis_t garis_b">Berat Kotor</td></tr>
            <tr><td class="box garis_t garis_b" style="height:30px; padding-left:10px; font-weight:bold"><asp:Label ID="txtBK" runat="server"></asp:Label></td></tr>
            <tr><td class="box garis_t garis_b">&nbsp;</td></tr>
            <tr><td class="box garis_t" style="height:30px; padding-left:10px; font-weight:bold">
                <asp:Label ID="txtBB" runat="server" Visible="False"></asp:Label></td></tr>
            <tr><td class="box" style="height:40px">&nbsp;</td></tr>
               <tr>
                <td colspan="2" style="width:100%">
                    <small><i>(*) : Hasil tusukan berdasarkan angka yang muncul pada Moisture Tester dengan pengambilan sample 25% - 30% dari jumlah total
                    ball dalam 1x pengiriman</i></small><br /><br />
                    <table style="width:100%; border-collapse:collapse">
                   <tr>
                        <td colspan="3">Perhitungan Berat Bersih </td>
                        <td style="width:30%" rowspan="15" valign="top">
                            &nbsp;</td>
                   </tr>
                   <tr>
                        <td style="width:5%">&nbsp;</td>
                        <td style="width:15%">Berat Kotor</td>
                        <td style="width:35%">:<span style="padding-left:10px; font-weight:bold">
                        <asp:Label ID="txtBeratKotor" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr style="display:none">
                        <td class="style1"></td>
                        <td class="style1">Jumlah Sample</td>
                        <td class="style1">: <span style="padding-left:10px; font-weight:bold"><asp:Label ID="txtJumlahSample" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr style="display:none">
                        <td>&nbsp;</td>
                        <td>Jml Sample Basah</td>
                        <td>: <span style="padding-left:10px; font-weight:bold"><asp:Label ID="txtJmlSampleBasah" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr style="display:none">
                        <td>&nbsp;</td>
                        <td>Pros.Sample Basah</td>
                        <td>: <span style="padding-left:10px; font-weight:bold">
                            <asp:Label ID="txtProsSampleBasah" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td>Rata2 Kadar Air</td>
                        <td>: <span style="padding-left:10px; font-weight:bold"><asp:Label ID="txtKadarAir" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td>Pros. Sampah (%)</td>
                        <td>:&nbsp;&nbsp; Berat Sampah / Berat Sample × 100</td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>:&nbsp;&nbsp;
                            <asp:Label ID="txtBeratSample" runat="server" style="font-weight: 700"></asp:Label>
                                </td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>:&nbsp;&nbsp;
                            <asp:Label ID="txtBeratSample0" runat="server" style="font-weight: 700"></asp:Label>
                                </td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td>Berat Potongan</td>
                        <td>: Berat Kotor x ((Rata2 Kadar Air - <asp:Label ID="txtstdKA" runat="server" 
                                Text="-"></asp:Label>%)+ 
                            Pros. Sampah(%))</td>
                   </tr>
                   <tr runat="server" id="tr01">
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- Std. KA &nbsp;(<asp:Label ID="txtstdKA2" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="txtBeratKotor1" runat="server"></asp:Label> x (
                            <asp:Label ID="txtSelisihKA" runat="server"></asp:Label> +(
                            <asp:Label ID="txtSmph" runat="server"></asp:Label>))</span>
                        
                        <%--<td>&nbsp;</td>
                        <td></td>--%>
                        &nbsp;= <span style="padding-left:10px; font-weight:bold"> <asp:Label ID="txtPotongan3" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr runat="server" id="tr1"> 
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- KA yg aktif saat ini &nbsp;(<asp:Label ID="txtstdKA3" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="txtBeratKotor3" runat="server"></asp:Label> x (
                            <asp:Label ID="txtSelisihKA3" runat="server"></asp:Label> +(
                            <asp:Label ID="txtSmph3" runat="server"></asp:Label>))</span>   
                        &nbsp;= <span style="padding-left:10px; font-weight:bold"> <asp:Label ID="txtPotongan" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr runat="server" id="tr11"> 
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- KA yg aktif saat ini &nbsp;(<asp:Label ID="txtstdKA31" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="Label2" runat="server"></asp:Label> - </span>   
                       </td>
                   </tr>
                  <%-- <tr runat="server" id="tr111"> 
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- KA yg aktif saat ini &nbsp;(<asp:Label ID="txtstdKA311" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="txtBeratKotor31" runat="server"></asp:Label> x (
                            <asp:Label ID="txtSelisihKA31" runat="server"></asp:Label> +(
                            <asp:Label ID="txtSmph31" runat="server"></asp:Label>))</span>   
                        &nbsp;= <span style="padding-left:10px; font-weight:bold"> <asp:Label ID="txtPotongan31" runat="server"></asp:Label></span></td>
                   </tr>--%>
                   <%--<tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> <asp:Label ID="txtPotongan" runat="server"></asp:Label></span></td>
                   </tr>--%>
                   <tr>
                        <td>&nbsp;</td>
                        <td>Berat Bersih</td>
                        <td>: Berat Kotor - Berat Potongan</td>
                   </tr>
                    <tr runat="server" id="tr02">
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- Std. KA &nbsp;(<asp:Label ID="txtstdKA4" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="txtBeratKotor2" runat="server"></asp:Label> - 
                            <asp:Label ID="txtPotongan4" runat="server"></asp:Label>
                            <asp:Label ID="txtSph4" runat="server"  Visible="false"></asp:Label></span>
                       <%-- </td>
                   </tr>
                   <tr>
                        <td>&nbsp;</td>
                        <td></td>--%>
                        &nbsp;= <span style="padding-left:10px; font-weight:bold"><asp:Label ID="txtBeratBersih" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr runat="server" id="tr2">
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- KA yg aktif saat ini &nbsp;(<asp:Label ID="txtstdKA5" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="txtBeratKotor4" runat="server"></asp:Label> - 
                            <asp:Label ID="txtPotongan2" runat="server"></asp:Label>
                            <asp:Label ID="txtSph2" runat="server"  Visible="false"></asp:Label></span>
                       
                        &nbsp;= <span style="padding-left:10px; font-weight:bold"><asp:Label ID="txtBeratBersih2" runat="server"></asp:Label></span></td>
                   </tr>
                   <tr runat="server" id="tr22">
                        <td>&nbsp;</td>
                        <td style="font-size: small; font-style: italic">&nbsp;- KA yg aktif saat ini &nbsp;(<asp:Label ID="txtstdKA51" runat="server" Text=""></asp:Label>%)</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="Label1" runat="server"></asp:Label> - </span>   
                       </td>
                   </tr>
                   
                   <tr runat="server" id="tr3">
                        <td>&nbsp;</td> 
                        <td style="font-size: small; font-style: italic">&nbsp;- Berat Bersih 0%  &nbsp;</td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="lblBB0_Persen" runat="server"></asp:Label></span>   
                       </td>
                   </tr>
                   
                   <tr runat="server" id="tr4">
                        <td>&nbsp;</td> 
                        <td style="font-size: small; font-style: italic">&nbsp; &nbsp;<asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="lblBB0_Persen_Nilai" runat="server"></asp:Label></span>   
                       </td>
                   </tr>
                   
                   <tr runat="server" id="tr5">
                        <td>&nbsp;</td> 
                        <td style="font-size: small; font-style: italic">&nbsp; &nbsp;<asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                        <td>:<span style="padding-left:10px; font-weight:bold"> 
                            <asp:Label ID="lblBB0_Persen_Nilai1" runat="server"></asp:Label></span>   
                       </td>
                   </tr>
                   
                </table>
                </td>
                <tr>
                    <td colspan="2"><br />
                        <table style="width:100%; border-collapse:collapse;">
                            <tr style="height:30px" valign="middle">
                                <td style="width:60%" class="box garis_b">Keputusan Akhir</td>
                                <td style="width:20%" class="box rata_c">Diperiksa</td>
                                <td style="width:20%" class="box rata_c">Diperiksa</td>
                           </tr>
                           <tr>
                                <td class="box garis_b garis_t" valign="middle" style="padding-left:5px; vertical-align:middle">
                                <asp:Image ID="imgChk1" runat="server" ImageUrl="~/images/yescheck.png" Visible="true" />
                                <asp:Image ID="imgNo1" runat="server" ImageUrl="~/images/nocheck.png" Visible="false" /> &nbsp;
                                Diterima (Berat Bersih)
                                </td>
                                <td rowspan="3" class="box garis_b tengah" id="aphead" runat="server"></td>
                                <td rowspan="3" class="box garis_b tengah" id="apmgr" runat="server"></td>
                           </tr>
                           <tr>
                                <td class="box garis_b garis_t" valign="middle" style="padding-left:5px">
                                <asp:Image ID="imgChk2" runat="server" ImageUrl="~/images/yescheck.png" Visible="false" />
                                <asp:Image ID="imgNo2" runat="server" ImageUrl="~/images/nocheck.png" Visible="true" /> &nbsp;
                                Diterima Setelah Sortir
                                </td>
                           </tr>
                           <tr>
                                <td class="box garis_b garis_t" valign="middle" style="padding-left:5px">
                                <asp:Image ID="imgChk13" runat="server" ImageUrl="~/images/yescheck.png" Visible="false" />
                                <asp:Image ID="imgNo13" runat="server" ImageUrl="~/images/nocheck.png" Visible="true" /> &nbsp;
                                Tidak Diterima
                                </td>
                           </tr>
                           <tr>
                                <td class="box garis_b garis_t">&nbsp;</td>
                                <td align="center" class="box garis_b garis_t">QA Material Inspection</td>
                                <td align="center" class="box garis_b garis_t">Manager QA</td>
                           </tr>
                           <tr>
                                <td class="box garis_t">&nbsp;</td>
                                <td class="box garis_t">Tgl/Nama</td>
                                <td class="box garis_t">Tgl/Nama</td>
                           </tr>
                        </table>
                    </td>
                </tr>
           </tr>
        </table>
    </div>
    </form>
</body>
</html>


