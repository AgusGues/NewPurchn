<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KasbonInput.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KasbonInput" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="bdp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    // fix for deprecated method in Chrome / js untuk bantu view modal dialog
    if (!window.showModalDialog) {
        window.showModalDialog = function (arg1, arg2, arg3) {
            var w;
            var h;
            var resizable = "no";
            var scroll = "no";
            var status = "no";
            // get the modal specs
            var mdattrs = arg3.split(";");
            for (i = 0; i < mdattrs.length; i++) {
                var mdattr = mdattrs[i].split(":");
                var n = mdattr[0];
                var v = mdattr[1];
                if (n) { n = n.trim().toLowerCase(); }
                if (v) { v = v.trim().toLowerCase(); }
                if (n == "dialogheight") {
                    h = v.replace("px", "");
                } else if (n == "dialogwidth") {
                    w = v.replace("px", "");
                } else if (n == "resizable") {
                    resizable = v;
                } else if (n == "scroll") {
                    scroll = v;
                } else if (n == "status") {
                    status = v;
                }
            }
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.focus();
        };
    }
</script>
    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>
       

        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
        .panelbox {background-color: #efeded;padding: 2px;}
        html,body,.form-control,button{font-size: 11px;}
        .input-group-addon{background: white;}
        .fz11{font-size: 11px;}
        .fz10{font-size: 10px;}
        .the-loader{
            position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
            text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
        }
        .input-xs{
            font-size: 11px;height: 11px;
        }
    </style>

        <script type="text/javascript">
            function confirm_batal() {
                if (confirm("Anda yakin untuk Batal ?") == true)
                    window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
                else
                    return false;
            }
                function confirm_batal() {
                    if (confirm("Anda yakin untuk Batal ?") == true)
                        window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
                    else
                        return false;
                }
        </script>

       <%-- <script type="text/javascript">

                function MyPopUpWin(url, width, height) {
                    var leftPosition, topPosition;
                    leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
                    topPosition = (window.screen.height / 2) - ((height / 2) + 50);
                    window.open(url, "Window2",
                        "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
                        + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
                        + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
                }

                function Cetak() {
                    var cek;
                    MyPopUpWin("../Report/Report.aspx?IdReport=kasbon", 900, 800)
                }

        

    </script>--%>
        
    </head>
	
        <body class="no-skin">
		
		<%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>
		<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">    
		<ContentTemplate>  
		<%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>
		
		
            <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        INPUT KASBON
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%" >
                <table style="table-layout: fixed; width: 100%; border-collapse: collapse">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="border-collapse: collapse">
                                    <tr>
                                        <td style="width: 100%">
                                            <%--<strong>INPUT KASBON</strong>--%>
                                        </td>
                                        <td><input id="btnNew" runat="server" style="font-weight: bold;font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                        <td><asp:Button id="btnUpdate" runat="server" style="font-weight: bold;font-size: 11px;" Text="Simpan" OnClick="btnUpdate_ServerClick" /></td>
                                        <%--<td><asp:Button id="btnRevisi" runat="server" style="font-weight: bold;font-size: 11px;" Text="Update" OnClick="btnRevisi_ServerClick" /></td>--%>
                                        <td><asp:Button ID="btnCancel" runat="server" Style="font-weight: bold; font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" /></td>
                                        <td><asp:Button id="btnPrint" runat="server" style="font-weight: bold;font-size: 11px;"  Text="Cetak" OnClick="btnPrint_ServerClick" /></td>
                                        <td><asp:Button id="btnList" runat="server" style="font-weight: bold;font-size: 11px;" Text="List" OnClick="btnList_ServerClick" /></td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="" style="padding:2px";>
                                    <table id="Table4" style="width: 100%; border-collapse: collapse; font-size:x-small" border="0">
                                        <tr>
                                            <td colspan="1" ></td>
                                            <td>
                                                <asp:RadioButton ID="rbTopUrgent" runat="server"  AutoPostBack="True" OnCheckedChanged="rbTopUrgent_CheckedChanged" Text="Top Urgent" />
                                            </td>
                                            <td colspan="2" ></td>
                                            <%--<td style="width: 17%;">Top Urgent</td>--%>
                                            <td style="width: 10%;">Item Name</td>
                                            <td style="width: 17%;"><asp:TextBox ID="txtNamaBarang" runat="server" Width="100%" Visible="false" CssClass="form-control input-sm"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">No. Pengajuan</td>
                                            <td style="width: 17%;"><asp:TextBox ID="txtNoPengajuan" runat="server" Width="100%" Enabled="false" CssClass="form-control input-sm"></asp:TextBox></td>
                                            <td colspan="2" ></td>
                                            <td style="width: 10%;">&nbsp;No. Kasbon</td>
                                            <td style="width: 17%;"><asp:TextBox ID="txtNoKasbon" runat="server" Width="100%" Enabled="false" CssClass="form-control input-sm"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">&nbsp; No. SPP</td>
                                            <td style="width:17%"><asp:TextBox ID="txtSPP" runat="server" Width="100%" AutoPostBack="True" OnTextChanged="txtSPP_TextChanged" onkeyup="this.value=this.value.toUpperCase()" CssClass="form-control input-sm"></asp:TextBox>
                                            <span><asp:ImageButton Visible="false" ID="nSPP" runat="server" ImageUrl="~/images/feed.jpg" AlternateText="SPP" ToolTip="Click for Close SPP by Reason"/></span>
                                            <td colspan="2" ><asp:Label ID="stPO" runat="server" cssClass="" ForeColor="Red"></asp:Label></td>
                                            <td style="width:10%">&nbsp; Dept</td>
                                            <td style="width:20%"><asp:TextBox ID="txtDept" runat="server" Width="100%" Enabled="false" CssClass="form-control input-sm"></asp:TextBox></td>
                                            <td style="width:20% ">
                                                <%--<asp:Label ID="txtInfoRevisi" runat="server" CssClass="cursor" ForeColor="Red"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp; Item SPP</td>
                                            <td style="width:20%"><asp:DropDownList ID="ddlItemSPP" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlItemSPP_SelectedIndexChanged" CssClass="form-control input-sm">
                                                </asp:DropDownList></td>
                                            <td colspan="2" ></td>
                                            <td style="width:10%">&nbsp; Tgl Pengajuan</td>
                                            <td style="width:20%">
                                                <%--<asp:TextBox ID="txtDate" runat="server" Width="100%"></asp:TextBox>--%>
                                                <bdp:BDPLite ID="txtDate" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Width="95%" style="margin-left: 86px">
                                                </bdp:BDPLite>
                                            </td> 
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp; Jumlah SPP</td><td style="width:20%"><asp:TextBox ID="txtJumlahSPP" runat="server" Width="100%" CssClass="form-control input-sm"></asp:TextBox></td>
                                            <td colspan="2" ></td>
                                            <td style="width:10%">&nbsp; Estimasi Kasbon</td><td style="width:20%"><asp:TextBox ID="txtEstimasiKasbon" runat="server" Width="100%" CssClass="form-control input-sm"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%">&nbsp; PIC</td><td style="width:20%"><asp:DropDownList ID="ddlPIC" runat="server" Width="100%" CssClass="form-control input-sm">
                                                </asp:DropDownList></asp:TextBox></td>
                                            <td colspan="2" ></td>
                                            <td>
                                            <asp:Button ID="lbAddItem" runat="server" OnClick="lbAddItem_Click" Font-Size="10pt"  Text="Add Item" />
                                            <asp:Button ID="lblAdd" runat="server" OnClick="lbLAdd_Click" Font-Size="10pt"  Text="Add item" Visible="false" />
                                            </td>
                                    <%--<td class="FormLabel" style="vertical-align:top">Alasan Cancel</td>--%>
                                            <td>
                                            <asp:TextBox TextMode="MultiLine" Visible="false" Rows="3" ToolTip="Alasan Cancel" ID="txtCancel" runat="server" Width="70%">
                                            </asp:TextBox></td>
                                        </tr>
                                    </table>
                                    
                                    <div class="contentlist" style="height: 200px;">
                                    <asp:Panel ID="Panel1" runat="server">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand"
                                        OnRowDataBound="GridView2_RowDataBound" Width="91%" Visible="false">
                                        <Columns>
                                            <%--<asp:BoundField DataField="SPPDetailID" HeaderText="ID" /> --%>
                                            <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                            <%--<asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />--%>
                                            <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                            <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                            <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                            <asp:BoundField DataField="EstimasiKasbon" DataFormatString="{0:N2}" HeaderText="Estimasi Kasbon" ItemStyle-HorizontalAlign="Right" />
                                            
                                            <%--<asp:ButtonField CommandName="batal" Text="Cancel" />
                                            <asp:ButtonField CommandName="Close" Text="Close" />
                                            <asp:ButtonField CommandName="Hapus" Text="Hapus" />--%>
                                            <asp:BoundField DataField="status" ShowHeader="False">
                                                <ControlStyle Width="0px" />
                                                <FooterStyle Width="0px" />
                                                <HeaderStyle Width="0px" />
                                                <ItemStyle BackColor="Black" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                        OnRowDataBound="GridView1_RowDataBound" Visible="False" Width="91%">
                                        <Columns>
                                            <%--<asp:BoundField DataField="SPPDetailID" HeaderText="ID" /> --%>
                                            <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                            <%--<asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />--%>
                                            <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang"  />
                                            <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                            <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                            <asp:BoundField DataField="EstimasiKasbon" DataFormatString="{0:N2}" HeaderText="Estimasi Kasbon" />
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <table id="tblPO" width="100%" style="width:100%; border-collapse:collapse; font-size:x-small; position:inherit">
                                        <thead>
                                        <tr class="tbHeader">
                                            <th style="width:4%" class="kotak">#</th>
                                            <th style="width:10%" class="kotak">NoSPP</th>
                                            <%--<th style="width:10%" class="kotak">Item Code</th>--%>
                                            <th style="width:32%" class="kotak">ItemName</th>
                                            <th style="width:8%" class="kotak">Quantity</th>
                                            <th style="width:5%" class="kotak">Satuan</th>
                                            <th style="width:10%" class="kotak">Estimasi Kasbon</th>
                                            <%--<th style="width:5%" class="kotak">&nbsp;</th>--%>
                                        </tr>
                                        <asp:Repeater ID="lstKasbon" runat="server" OnItemDataBound="lstKasbon_DataBound">
                                            <ItemTemplate>
                                                <tr class=" EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex +1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah">
                                                        <table style="width:100%; font-size:x-small; border-collapse:collapse">
                                                            <tr>
                                                                <td style="width:90%" align="center"><%# Eval("NamaBarang")%></td>
                                                                <td style="width:9%;" align="center">
                                                                <%--<sup>
                                                                    <asp:Label ID="lblRevInfo" Width="100%" runat="server" Text="1" BackColor="Red" ForeColor="White" ToolTip="Klik for revision info"></asp:Label>
                                                                </sup>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td class="kotak"><%# Eval("NamaBarang")%></td>--%>
                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                    <td class="kotak angka xx"><%#Eval("EstimasiKasbon", "{0:N2}")%></td>
                                                    <%--<td class="kotak tengah">
                                                        <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                        <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="confirm_hapus();" />
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td class="kotak Line3 angka" colspan="5">Sub Total</td>
                                                    <td class="kotak angka Line3"><asp:TextBox ID="gTotal" runat="server" Enabled="false"></asp:TextBox></td>
                                                    <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="kotak Line3 angka" colspan="5">Dana Cadangan</td>
                                                    <td class="kotak angka Line3">
                                                        <asp:TextBox ID="txtDanaCadangan" runat="server" AutoPostBack="True" OnTextChanged="txtDanaCadangan_TextChanged" ></asp:TextBox>
                                                    </td>
                                                    <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="kotak Line3 angka" colspan="5">Total Kasbon</td>
                                                    <td class="kotak angka Line3">
                                                        <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                </tr>
                                            </FooterTemplate>
                                            <AlternatingItemTemplate>
                                            <tr class="OddRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex +1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah">
                                                        <table style="width:100%; font-size:x-small; border-collapse:collapse">
                                                            <tr>
                                                                <td style="width:90%" align="center"><%# Eval("NamaBarang") %></td>
                                                                <td style="width:9%;" align="center">
                                                                <%--<sup><asp:Label ID="lblRevInfo" Width="100%" runat="server" Text="1" BackColor="Red" ForeColor="White" ToolTip="Klik for revision info"></asp:Label></sup>--%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <%--<td class="kotak "><%# Eval("NamaBarang")%></td>--%>
                                                    <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Satuan") %></td>
                                                    <td class="kotak angka"><%#Eval("EstimasiKasbon", "{0:N2}")%></td>
                                                    <%--<td class="kotak tengah">
                                                         <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />--%>
                                                        <%--<asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" onclientclick="javascript:return confirm('Are you sure to delete record?')" />--%>
                                                        <%--<asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="confirm_hapus();" />
                                                    </td>--%>
                                                </tr></AlternatingItemTemplate>
                                        </asp:Repeater>
                                        </thead>
                                    </table>
                                    </asp:Panel>
                                    
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <table id="Table1" width="100%" style="width: 100%; border-collapse: collapse; font-size: x-small;
                                                position: inherit">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th style="width: 4%" class="kotak">
                                                            #
                                                        </th>
                                                        <th style="width: 32%" class="kotak">
                                                            ItemName
                                                        </th>
                                                        <th style="width: 8%" class="kotak">
                                                            Quantity
                                                        </th>
                                                        <th style="width: 5%" class="kotak">
                                                            Satuan
                                                        </th>
                                                        <th style="width: 10%" class="kotak">
                                                            Estimasi Kasbon
                                                        </th>
                                                        <%--<th style="width:5%" class="kotak">&nbsp;</th>--%>
                                                    </tr>
                                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="lstKasbon_DataBound">
                                                        <ItemTemplate>
                                                            <tr class=" EvenRows baris">
                                                                <td class="kotak tengah">
                                                                    <%# Container.ItemIndex +1 %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                                        <tr>
                                                                            <td style="width: 90%" align="center">
                                                                                <%# Eval("NamaBarang")%>
                                                                            </td>
                                                                            <td style="width: 9%;" align="center">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <%--<td class="kotak"><%# Eval("NamaBarang")%></td>--%>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Qty","{0:N2}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Satuan") %>
                                                                </td>
                                                                <td class="kotak angka xx">
                                                                    <%#Eval("EstimasiKasbon", "{0:N2}")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr>
                                                                <td class="kotak Line3 angka" colspan="4">
                                                                    Sub Total
                                                                </td>
                                                                <td class="kotak angka Line3">
                                                                    <asp:TextBox ID="gTotal" runat="server" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td class="kotak Line3 angka" colspan="4">
                                                                    Dana Cadangan
                                                                </td>
                                                                <td class="kotak angka Line3">
                                                                    <asp:TextBox ID="txtDanaCadangan" runat="server" AutoPostBack="True" OnTextChanged="txtDanaCadangan_TextChanged"></asp:TextBox>
                                                                </td>
                                                                <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td class="kotak Line3 angka" colspan="4">
                                                                    Total Kasbon
                                                                </td>
                                                                <td class="kotak angka Line3">
                                                                    <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <%--<td class="kotak Line3" colspan="2">&nbsp;</td>--%>
                                                            </tr>
                                                        </FooterTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="OddRows baris">
                                                                <td class="kotak tengah">
                                                                    <%# Container.ItemIndex +1 %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                                        <tr>
                                                                            <td style="width: 90%" align="center">
                                                                                <%# Eval("NamaBarang") %>
                                                                            </td>
                                                                            <td style="width: 9%;" align="center">                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Qty","{0:N2}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Satuan") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%#Eval("EstimasiKasbon", "{0:N2}")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                </div>
                                    
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>




              </div>

            <script src="../../assets/jquery.js" type="text/javascript"></script>
            <script src="../../assets/js/jquery-ui.min.js"></script>
            <script src="../../assets/select2.js"></script>
            <script src="../../assets/datatable.js"></script>
            <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>
        </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
