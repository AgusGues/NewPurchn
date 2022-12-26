<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KasbonOntimePemantauan.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KasbonOntimePemantauan" %>
<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                        PEMANTAUAN ONTIME KASBON
                    </div>
                    <div style="padding: 2px"></div>                                  
				
			<%--copy source design di sini--%>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:small">
                    <%--<tr>
                        <td style="width:100%; height:49px">
                            <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <td style="width:40%; padding-left:10px"><b>PEMANTAUAN ONTIME KASBON</b></td>
                                    <td style="width:60%; padding-right:10px" align="right">                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <div>
                                <table style="width:100%; border-collapse:collapse; font-size:small; margin-top:5px">
                                    <tr>
                                        <td style="width:10%;">&nbsp;</td>
                                        <td style="width:10%">Periode</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%;">&nbsp;</td>
                                        <td style="width:10%">PIC</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlPic" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-sm" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary btn-sm" Text="Export to Excel" OnClick="btnExport_Click" />
                                       </td>
                                    </tr>
                                </table>
                                <hr/>
                                <div class="contentlist" style="height:400px" id="lstNewP" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <tr class="tbHeader">
                                            <th style="width:4%" class="kotak">No.</th>
                                            <th style="width:10%" class="kotak">No Pengajuan</th>
                                            <th style="width:10%" class="kotak">No Kasbon</th>
                                            <th style="width:10%" class="kotak">No SPP</th>
                                            <th style="width:10%" class="kotak">No PO</th>
                                            <th style="width:30%" class="kotak">Item Name</th>
                                            <th style="width:10%" class="kotak">Apv Kasbon</th>
                                            <th style="width:10%" class="kotak">Apv Realisasi</th>
                                            <th style="width:10%" class="kotak">ON TIME</th>
                                        </tr>
                                        <%--<tr class="tbHeader">
                                            <th style="width:8%" class="kotak">No SPP</th>
                                             <th style="" class="kotak">Item Name</th>
                                            <th style="width:10%" class="kotak">Mgr. Apv</th>
                                            <th class="kotak" style="width:10%">Tanggal</th>
                                            <th class="kotak" style="width:5%">Jam</th>
                                            <%--<th class="kotak" style="width:5%">OK</th>--%>
                                         <%--</tr>--%>
                                        <asp:Repeater ID="lstBiaya" runat="server" OnItemDataBound="lstBiaya_Databound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris" id="trs" runat="server">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah" style="white-space:nowrap"><%# Eval("NoPengajuan")%></td>
                                                    <td class="kotak tengah"><%# Eval("NoKasbon")%></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP")%></td>
                                                    <td class="kotak tengah"><%# Eval("NoPo")%></td>
                                                    <td class="kotak" style="white-space:inherit"><%# Eval("NamaBarang")%></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate2", "{0:d}")%></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate4", "{0:d}")%></td>    
                                                    <td class="kotak tengah">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tfoot>
                                            <tr id="ttNO" runat="server" class="total baris">
                                                <td class="kotak angka" colspan="8">Total No :</td>
                                                <td class="kotak tengah"></td>
                                            </tr>
                                            <tr id="ttOK" runat="server" class="total baris">
                                                <td class="kotak angka" colspan="8">Total Ok :</td>
                                                <td class="kotak tengah"></td>
                                            </tr>
                                            <tr id="ttBatal" runat="server" class="total baris">
                                                <td class="kotak angka" colspan="8">Total Batal :</td>
                                                <td class="kotak tengah"></td>
                                            </tr>
                                            <tr id="tt" runat="server" class="total baris">
                                                <td class="kotak angka" colspan="8">Prosentase :</td>
                                                <td class="kotak tengah"></td>
                                            </tr>
                                        </tfoot>
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
