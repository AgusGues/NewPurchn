<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KasbonApproval.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KasbonApproval" %>
<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="bdp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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

        <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
        <script type="text/javascript">
            function onKeyUp() {
                $("#<%=txtNotApproved.ClientID %>").keyup(function() {
                    if ($(this).val().length == 2) {
                        $("#<%=btnNotApprove.ClientID %>").removeAttr("disabled");
                        $("#<%=btnApprove.ClientID %>").attr("disabled", "true");
                    }
                    if ($(this).val().length == 0) {
                        $("#<%=btnNotApprove.ClientID %>").attr("disabled", "false");
                        $("#<%=btnApprove.ClientID %>").removeAttr("disabled");
                    }
                });
            }
    
        </script>
        
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
                        APPROVAL Kasbon
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <tr>
                    <td style="width:100%;height:49px">
                        <table class="nbTableHeader">
                            <tr style="">
                                <td style="width:15%; padding-left:15px">
                                   <%-- <b>APPROVAL Kasbon</b>--%>
                                </td>
                                <td style="width:85%; padding-right:10px; text-align:right">
                                    <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" OnClick="btnPrev_Click" />
                                    <asp:Button ID="btnApprove" runat="server" Text="Approved" OnClick="btnApprove_Click" Enabled="false" />
                                    <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnNotApprove" runat="server" Text="Not Approved" Enabled="false" OnClick="btnNotApprove_Click" />
                                    <%--<asp:Button ID="btnCancel" runat="server" Text="List PO" Enabled="true" OnClick="btnList_ServerClick" />--%>
                                    &nbsp;
                                    <asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor Pengajuan" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Nomor PO"></asp:TextBox>
                                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                    <asp:HiddenField ID="noPengajuan" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="" style="padding-top:5px;">
                            <table class="tbStandart">
                                <tr>
                                    <td class="FormLabel">Tanggal</td>
                                    <td class="FormField">
                                        <%--<asp:TextBox ID="txtTglKasbon" runat="server" ReadOnly="true" Width="80%"></asp:TextBox>--%>
                                        <bdp:BDPLite ID="txtTglKasbon" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Width="95%" style="margin-left: 86px">
                                                </bdp:BDPLite>
                                    </td>
                                    <td class="FormLabel">No.Pengajuan</td>
                                    <td class="FormField"><asp:TextBox ID="txtNoPengajuan" runat="server" ReadOnly="true" Width="80%" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <%--<td class="FormField"><asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" Width="80%"></asp:TextBox></td>--%>
                                </tr>
                                <tr>
                                    <td class="FormLabel">Dept</td>
                                    <td class="FormField"><asp:TextBox ID="txtDept" runat="server" ReadOnly="true" Width="100%" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <td class="FormLabel">PIC</td>
                                    <td class="FormField"><asp:TextBox ID="txtPic" runat="server" ReadOnly="true" Width="100%" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <td class="FormField"></td>
                                </tr>
                                <tr>
                                    <td class="FormLabel">Dana Cadangan</td>
                                    <td class="FormField"><asp:TextBox ID="txtDanaCadangan" runat="server" ReadOnly="true" Width="80%" CssClass="form-control input-sm"></asp:TextBox></td>                                    
                                </tr>
                                <tr>
                                    <td class="FormLabel" style="vertical-align:top">Alasan Not Approved</td>
                                    <td class="FormField" colspan="2">
                                    <asp:TextBox TextMode="MultiLine" Rows="3" ID="txtNotApproved" runat="server" Width="70%" AutoPostBack="false" OnTextChanged="txtNotApproved_Change" CssClass="form-control input-sm"></asp:TextBox></td>
                                    <td class="FormField">
                                        Approval Status : <b><asp:Label ID="txtStatus" runat="server"></asp:Label></b> <br />
                                        Kasbon Type : <b><asp:Label ID="txtKasbonType" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <div class="contentlist" style="height:250px">
                                <table class="tbStandart">
                                   <thead>
                                    <tr class="tbHeader">
                                        <th style="width:3%" class="kotak">No.
                                            <%--<asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chkAll_CheckedChange" />--%>
                                        </th>
                                        <th style="width:5%" class="kotak">No.SPP</th>
                                        <th style="width:6%" class="kotak">Item Code</th>
                                        <th style="width:30%" class="kotak">Item Name</th>
                                        <th style="width:4%" class="kotak">Unit</th>
                                        <th style="width:5%" class="kotak">Quantity</th>
                                        <th style="width:6%" class="kotak">Estimasi Kasbon</th>
                                        <th style="width:8%" class="kotak">Total Estimasi</th>
                                        <%--<th style="width:6%" class="kotak">Deliv Date</th>--%>
                                        <%--<th style="width:3%" class="kotak">&nbsp;</th>
                                        <th style="width:3%" class="kotak">&nbsp;</th>--%>
                                    </tr>
                                   </thead> 
                                   <tbody>
                                    <asp:Repeater ID="lstItemKasbon" runat="server" OnItemDataBound="lstItemKasbon_DataBound">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %>
                                                <asp:CheckBox ID="chk" Text="" ToolTip='<%# Eval("ID") %>' runat="server" Visible="false" AutoPostBack="True" OnCheckedChanged="chk_CheckedChange"/>
                                                </td>
                                                <td class="kotak tengah"><%# Eval("NoSPP")%></td>
                                                <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                <td class="kotak"><%# Eval("NamaBarang")%></td>
                                                <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                                <td class="kotak angka"><%# Eval("Qty","{0:N2}") %></td>
                                                <td class="kotak angka"><%# Eval("EstimasiKasbon","{0:N2}") %></td>
                                                <td class="kotak angka"><asp:Label ID="tHarga" Text='<%# Eval("EstimasiKasbon","{0:N2}") %>' runat="server"></asp:Label></td>
                                                <%--<td class="kotak tengah" id="dlv"><%# Eval("DlvDate","{0:d}") %></td>
                                                <td class="kotak tengah">
                                                <asp:ImageButton ID="showHis" runat="server" ImageUrl="~/images/dtpJavaScript.gif" CommandArgument='<%# Eval("ItemName") %>' CommandName='
                                                <%# Eval("ItemTypeID") %>' ToolTip="Click For Show History & Stock Item" />
                                               
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td class="kotak Line3 angka" colspan="7">Sub Total</td>
                                                <td class="kotak angka Line3"><asp:Label ID="gTotal" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="bPPN" runat="server">
                                                <td class="kotak Line3 angka" colspan="7">Dana Cadangan</td>
                                                <td class="kotak Line3 angka"><asp:Label ID="gDC" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="kotak Line3 angka" colspan="7">Total Kasbon</td>
                                                <td class="kotak angka Line3"><asp:Label ID="grnTotal" runat="server"></asp:Label></td>
                                            </tr>
                                            
                                        </FooterTemplate>
                                    </asp:Repeater>
                                   </tbody>
                                </table>
                                
                            </div>
                        </div>
                    </td>
                </tr>
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
