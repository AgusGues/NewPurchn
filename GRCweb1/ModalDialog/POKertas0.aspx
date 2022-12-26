<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POKertas0.aspx.cs" Inherits="GRCweb1.ModalDialog.POKertas0" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" /> 
    <title>Create PO Kertas</title>
    <meta content="" charset="utf-8" />
    <link href="../Scripts/Calendar.css" type="text/css" rel="Stylesheet" />
    <link href="../Scripts/text.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.2.6-vsdoc.js"></script>
    <script type="text/javascript" src="../Scripts/calendar2.js"></script>
</head>
<script type="text/javascript">
    $(document).ready(function(){
        var w=dialogWidth.replace('px','');
        var h=dialogHeight.replace('px','');
        //alert(w);
        //$('div.content').attr('style','height:'+(parseInt(h)-70)+'px;');
        
    });
    function KadarAirView(id)
    {
        params = 'width=1024px';
        params += ', height=600px';
        params += ', top=20px, left=20px';
        params +=',scrollbars=1';
        //window.showModalDialog
        window.open("../../ModalDialog/FromKadarAirQA.aspx?ka=" + id, "Preview", params);
    }
</script>

<body style="background-color:Silver; overflow:hidden " >
    
    <div id='ctn' style="padding:3px; width:100%">
    <form id="form1" runat="server" style="padding:3px; width:99%">
        <table  style="border-collapse:collapse; width:100%">
            <tr>
                <td style="width:100%; height:49px">
                    <table class="nbTableHeader">
                        <tr>
                            <td style="width: 70%; padding-left:15px">
                                <h3>INPUT PO KERTAS KA 0%</h3>
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
                    <div class="content" style="padding:3px">
                        <table style="width:100%;border-collapse:collapse; font-size:small; margin:5px 0 5px 0">
                            <tr>
                                <td class="left" style="width:12%">Tanggal PO</td>
                                <td style="width:15%"><asp:TextBox ID="txtTglPO" runat="server" Width="100%"></asp:TextBox></td>
                                <td class="left" style="width:12%">&nbsp;</td>
                                <td class="left" style="width:15%">&nbsp;</td>
                                <td class="left" style="width:12%">&nbsp;No. PO</td>
                                <td style="width:15%"><asp:TextBox ID="txtNoPO" runat="server" Width="80%" Enabled="false"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="left">No.SPP</td>
                                <td><asp:DropDownList ID="ddlNoSPP" runat="server" Width="100%" OnSelectedIndexChanged="ddlNoSPP_Change" AutoPostBack="true"></asp:DropDownList></td>
                                <td>&nbsp;</td><td>&nbsp;</td>
                                <td class="left">&nbsp;Item SPP</td>
                                <td><asp:DropDownList ID="ddlItemSPP" runat="server" Width="100%"></asp:DropDownList></td>
                                <td><asp:HiddenField ID="txtUomID" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="left">Nama Supplier</td>
                                <td colspan="3"><asp:TextBox ID="txtNamaSupplier" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">&nbsp;Kode Supplier</td>
                                <td><asp:TextBox ID="txtKodeSupplier" runat="server" Width="100%"></asp:TextBox></td>
                                <td>&nbsp;<asp:HiddenField ID="txtSupplierID" runat="server" Value="0" /></td>
                            </tr>
                            <tr>
                                <td class="left">Up Supplier</td>
                                <td><asp:TextBox ID="txtUpSupplier" runat="server" Width="100%"></asp:TextBox></td>
                                <td class="left">&nbsp;Telp Supplier</td>
                                <td><asp:TextBox ID="txttelpSupplier" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">&nbsp;Fax Supplier</td>
                                <td><asp:TextBox ID="txtFaxSupplier" runat="server" Width="100%"></asp:TextBox></td>
                                <td><asp:HiddenField ID="txtSubCompanyID" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="left">Nama Barang</td>
                                <td colspan="2"><asp:TextBox ID="txtItemName" runat="server" Width="100%"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtSatuan" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">&nbsp;Mata Uang</td>
                                <td><asp:DropDownList ID="ddlMataUang" runat="server" Width="80%"></asp:DropDownList></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="left">Jumlah</td>
                                <td><asp:TextBox ID="txtJumlah" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">&nbsp;Harga</td>
                                <td><asp:TextBox ID="txtHarga" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">&nbsp;Delivery Date</td>
                                <td><asp:TextBox ID="txtDliveryDate" runat="server" Width="80%"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="left">Term Of Pay</td>
                                <td><asp:DropDownList ID="ddlTermOfPay" runat="server" Width="80%"></asp:DropDownList></td>
                                <td class="left">&nbsp;Term Of Delivery</td>
                                <td><asp:TextBox ID="txtTOD" runat="server" Width="100%"></asp:TextBox></td>
                                <td class="left">&nbsp;Discount</td>
                                <td><asp:TextBox ID="txtDicount" runat="server" Width="80%" Text="0"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="left">PPN</td>
                                <td>
                                    <asp:TextBox ID="txtPPN" runat="server" Width="30%"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                    PPH &nbsp;&nbsp;<asp:TextBox ID="txtPPH" runat="server" Width="30%" Text="0"></asp:TextBox>
                                </td>
                                <td class="left">&nbsp;Ongkos Kirim</td>
                                <td><asp:TextBox ID="txtOngkosKirim" runat="server" Width="80%"></asp:TextBox></td>
                                <td class="left">
                                    &nbsp;<asp:RadioButton ID="cash" runat="server" GroupName="c" Text="Cash" Checked="true" />
                                    &nbsp;<asp:RadioButton ID="credit" runat="server" GroupName="c" Text="Credit" />&nbsp;
                                    
                                </td>
                                <td align="right">Barang :&nbsp;<asp:RadioButton ID="local" runat="server" GroupName="mp" Text="Local" Checked="true" />
                                    &nbsp;<asp:RadioButton ID="import" runat="server" GroupName="mp" Text="Import" /></td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="left">Total Price</td>
                                <td><asp:TextBox ID="txtTotalPrice" runat="server" Width="90%"></asp:TextBox></td>
                                <td class="left"><asp:HiddenField ID="txtDKID" runat="server" value="0" /></td>
                                <td class="left">&nbsp;Remark's</td>
                                <td colspan="2"><asp:TextBox ID="txtKeterangan" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox></td>
                                <td><asp:HiddenField ID="txtDKAID" runat="server" value="0" /></td>
                            </tr>
                        </table>
                        <hr />
                        
                        <div class="contentlist" style="height:120px; width:98%; overflow:hidden">
                        <table style="border-collapse:collapse; width:90%; font-size:small">
                            <tr class="Line3 baris">
                                <td colspan="1" class="kotak line3">&nbsp;&nbsp;<b>Data Kadar Air</b></td>
                            </tr>
                            <tr class="tbHeader baris">
                                <th class="kotak" style="width:10%">Lokasi</th>
                                <th class="kotak" style="width:10%">No Document</th>
                                <th class="kotak" style="width:6%">Tanggal</th>
                                <th class="kotak" style="width:6%">No. Mobil</th>
                                <th class="kotak" style="width:8%">Berat Kotor</th>
                                <th class="kotak" style="width:8%">Berat Bersih</th>
                                <th class="kotak" style="width:5%">Kadar Air</th>
                                <th class="kotak" style="width:5%">Sampah</th>
                                <th class="kotak" style="width:5%">StdKA</th>
                                <th class="kotak" style="width:5%">Jml Bal</th>
                            </tr>
                            <tbody id="lstKAT" runat="server">
                                <tr class="EvenRows baris" id="plant" runat="server">
                                    <td class="kotak">Plant</td>
                                    <td class="kotak"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    
                                </tr>
                                <tr class="OddRows baris" id="depo" runat="server">
                                    <td class="kotak">Depo</td>
                                    <td class="kotak"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                        <p></p>
                    </div>
                </td>
            </tr>
        </table>
    </form>
        
    </div>
</body>
</html>
