<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPengirimanDepo.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPengirimanDepo" %>
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
                        REKAP PENGIRIMAN KERTAS DEPO
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>

                <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:small">
                    <tr>
                        <td style="width:100%; height:49px">
                        <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <%--td style="width:40%; padding-left:10px">REKAP PENGIRIMAN KERTAS DEPO</td>--%>
                                    <td style="width:60%; padding-right:10px" align="right">
                                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr>
                                        <td style="width:10%; padding-left:10px"></td>
                                        <td style="width:15%"><asp:DropDownList ID="ddlDepo" runat="server" Visible="false"></asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left:10px">Periode :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                        <asp:Button ID="btnPreview" runat="server" Text="Preview"  OnClick="btnPreview_Click" />
                                        <%--<asp:Button ID="btnExport" runat="server" Text="OK" OnClick="btnPrint_Click" />--%>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <br />
                                <div class="contentlist" style="height:400px" id="ctn" runat="server">
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="Line3">
                                                <th colspan="6" align="left"><b>Rekap Selisih Bulanan (All Kiriman)</b></th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="transparant">&nbsp;</th>
                                                <th style="width:15%" class="kotak">Depo Name</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Depo</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Pabrik</th>
                                                <th style="width:10%" class="kotak">Selisih</th>
                                                <th style="width:10%" class="kotak">%</th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstRekap" runat="server" OnItemDataBound="lstRekap_Databound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="baris" runat="server">
                                                        <td class="transparent"></td>
                                                        <td class="kotak"><%# Eval("DepoName") %></td>
                                                        <td class="kotak angka"><%# Eval("NettDepo","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Selisih","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Persen","{0:N2}") %></td>
                                                        <td style="background-color:Transparent">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <hr />
                                    
                                    <!--selisih checker --->
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="Line3">
                                                <th colspan="6" align="left"><b>Rekap Selisih By Checker</b></th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="transparant">&nbsp;</th>
                                                <th style="width:15%" class="kotak">Checker Name</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Depo</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Pabrik</th>
                                                <th style="width:10%" class="kotak">Selisih</th>
                                                <th style="width:10%" class="kotak">%</th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstChecker" runat="server" OnItemDataBound="lstChecker_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="brsCh" runat="server">
                                                        <td class="transparant">&nbsp;</td>
                                                        <td class="kotak"><%# Eval("Checker") %></td>
                                                        <td class="kotak angka"><%# Eval("NettDepo","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Selisih","{0:N2}") %></td>
                                                        <th class="kotak tengah"><%# Eval("Persen","{0:N2}") %></th>
                                                        <td style="background-color:Transparent">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="Line3 Baris" id="chTotal" runat="server">
                                                <td class="transparant">&nbsp;</td>
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak angka"></td>
                                                <td class="kotak angka"></td>
                                                <td class="kotak angka"></td>
                                                <td class="kotak angka"></td>
                                            </tr>
                                        </tfoot>
                                     </table>  
                                     <hr />  
                                    <!--selisih by supplier -->
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="Line3">
                                                <th colspan="6" align="left"><b>Rekap Selisih By Supplier</b></th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="transparant">&nbsp;</th>
                                                <th style="width:15%" class="kotak">Supplier Name</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Depo</th>
                                                <th style="width:10%" class="kotak">Berat Bersih Pabrik</th>
                                                <th style="width:10%" class="kotak">Selisih</th>
                                                <th style="width:10%" class="kotak">%</th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstSuppler" runat="server" OnItemDataBound="lstChecker_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="brsCh" runat="server">
                                                        <td class="transparant">&nbsp;</td>
                                                        <td class="kotak" style="white-space:nowrap"><%# Eval("SupplierName")%></td>
                                                        <td class="kotak angka"><%# Eval("NettDepo","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("Selisih","{0:N2}") %></td>
                                                        <th class="kotak tengah"><%# Eval("Persen","{0:N2}") %></th>
                                                        <td style="background-color:Transparent">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                     </table>    
                                    <hr />
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                        <tr class="Line3">
                                            <th colspan="5" align="left"><b> Rekapan Klarifikasi Selisih > 5%</b></th>
                                            <th style="background-color:Transparent">&nbsp;</th>
                                        </tr>
                                        
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="transparant">&nbsp;</th>
                                                <th style="width:15%" class="kotak">Depo Name</th>
                                                <th style="width:15%" class="kotak">Total Selisih >5%</th>
                                                <th style="width:15%" class="kotak">Klarifikasi On Time</th>
                                                <th style="width:10%" class="kotak">% Ontime</th>
                                                <th style="background-color:Transparent">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstKla" runat="server" OnItemDataBound="lstKla_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="brs" runat="server">
                                                        <td class="transparant">&nbsp;</td>
                                                        <td class="kotak"><%# Eval("DepoName") %></td>
                                                        <td class="kotak angka"><%# Eval("NettDepo","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("NettPlant","{0:N2}") %></td>
                                                        <td class="kotak angka"><%# Eval("SelisihBB","{0:N2}") %></td>
                                                        <td style="background-color:Transparent">&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <hr />
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0">
                                        <thead>
                                            <tr class="Line3">
                                                <th class="left" colspan="8"><b>Detail Klarifikasi</b></th>
                                                <th class="transparant">&nbsp;</th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th style="width:5%" class="kotak">No</th>
                                                <th style="width:15%" class="kotak">Depo</th>
                                                <th style="width:15%" class="kotak">No.SJ</th>
                                                <th style="width:10%" class="kotak">% Selisih</th>
                                                <th style="width:10%" class="kotak">Tgl Muat</th>
                                                <th style="width:10%" class="kotak">Tgl Klarifikasi</th>
                                                <th style="width:10%" class="kotak">Sesuai</th>
                                                <th style="width:10%" class="kotak">Tidak Sesuai</th>
                                                <th class="transparant">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstDepo" runat="server" OnItemDataBound="lstDepo_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="dtl" runat="server">
                                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                        <td class="kotak"><%# Eval("DepoName") %></td>
                                                        <td class="kotak" style="white-space:nowrap"><%# Eval("NoSJ")%></td>
                                                        <td class="kotak tengah"><%# Eval("Persen", "{0:N2}")%></td>
                                                        <td class="kotak tengah"><%# Eval("CreatedBy")%></td>
                                                        <td class="kotak tengah"><%# Eval("LastModifiedBy")%></td>
                                                        <td class="kotak tengah"><%# Eval("Sesuai","{0:N0}") %></td>
                                                        <td class="kotak tengah"><%# Eval("TdkSesuai","{0:N0}") %></td>
                                                        <td class="transparant">&nbsp;</td>
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
