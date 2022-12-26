<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapLoadingTime.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.RekapLoadingTime" %>
<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"  Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>



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

		<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">    
		<ContentTemplate>--%>  
		
        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>
		
		
            <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        REKAP PENCAPAIAN LOADING TIME
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>
             <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                   <%-- <tr>
                        <td style="height:49px">
                            <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <td>REKAP PENCAPAIAN LOADING TIME</td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <div class=""><br />
                                <table style="width:100%; border-collapse:collapse;">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:15%">Periode</td>
                                        <td style="width:35%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <%--<tr>
                                        <td>&nbsp;</td>
                                        <td>Sampai Tanggal</td>
                                        <td><asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox></td>
                                        <td><cc1:CalendarExtender ID="ca2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td>
                                    </tr>--%>
                                    <tr><td colspan="4">&nbsp</td></tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" 
                                                onclick="btnExport_Click" />
                                            <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height:420px; text-align:center">
                                
                                    <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                        <tr>
                                            <td colspan="7" align="center">
                                                <cc2:Chart ID="Chart1" runat="server" Height="400px" Width="1000px" BorderlineColor="Blue">
                                                    <Titles>
                                                        <cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title>
                                                    </Titles>
                                                    <ChartAreas>
                                                        <cc2:ChartArea Name="Area1"></cc2:ChartArea>
                                                    </ChartAreas>
                                                    
                                                </cc2:Chart>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <div id="lst" runat="server">
                                    <table style="width:80%;  border-collapse:collapse; font-size:x-small;" border="0" >
                                        <thead>
                                            <tr>
                                                <td colspan="7" id="xx">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th class="" colspan="3">&nbsp;</th>
                                                <th class="kotak">Per Bulan</th>
                                                <th class="kotak">Per Hari</th>
                                                <td class="" colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <th class="kotak" colspan="3">Jumlah Mobil yang dimuat</th>
                                                <th class="kotak"><%=TotalMobil.ToString("###,##0") %></th>
                                                <th class="kotak"><%=AvgPerHari.ToString("##0.00") %></th>
                                                <td class="" colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr><th colspan="7">&nbsp;</th></tr>
                                            <tr>
                                                <th class="" colspan="3">&nbsp;</th>
                                                <th class="kotak">Toleransi/Bulan</th>
                                                <th class="kotak">Toleransi/Hari</th>
                                                <th class="kotak">Pencapaian/Hari</th>
                                                <th class="kotak">Pencapaian/Hari</th>
                                            </tr>
                                            <tr>
                                                <th class="kotak" colspan="3">Jumlah Mobil yang Lewat</th>
                                                <th class="kotak"><%=TolBulan.ToString("##0.00")%></th>
                                                <th class="kotak"><%=TolHari.ToString("##0.00") %></th>
                                                <th class="kotak"><%=CapaiBulan.ToString("##0")%></th>
                                                <th class="kotak"><%=CapaiHari.ToString("##0.00")%></th>
                                            </tr>
                                            <tr><th colspan="7">&nbsp;</th></tr>
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:5%">Tgl</th>
                                                <th class="kotak" style="width:8%">Target</th>
                                                <th class="kotak" style="width:10%">Pencapaian</th>
                                                <th class="kotak" style="width:10%">Mobil Lewat</th>
                                                <th class="kotak" style="width:8%">BPAS</th>
                                                <th class="kotak" style="width:8%">Armada Luar</th>
                                                <th class="kotak" style="width:15%">Keterangan</th>
                                                <th class="kotak" style="width:15%">Jml Mobil</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstLoading" runat="server" OnItemDataBound="lstLoading_Databind">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris kotak">
                                                        <td class="kotak tengah"><%# Eval("ID") %></td>
                                                        <td class="kotak tengah"><%# Eval("Targete") %>%</td>
                                                        <td class="kotak angka"><%# Eval("Pencapaian","{0:n2}") %>%</td>
                                                        <td class="kotak angka"><%# Eval("JmlLewat") %></td>
                                                        <td class="kotak angka"><%# Eval("MobilSendiri") %></td>
                                                        <td class="kotak angka"><%# Eval("EkspedisiID")%></td>
                                                        <td class="kotak"><asp:Label ID="ket" runat="server" Text=""></asp:Label></td>
                                                        <td class="kotak tengah"><%# Eval("JmlMobil")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr class="Line3 tengah kotak" style="font-weight:bold">
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak"><%=TargetSOP %></td>
                                                <td class="kotak angka"><%=Pencapaiane.ToString("##0.00") %>%</td>
                                                <td class="kotak angka"><%=CapaiBulan %></td>
                                                <td class="kotak angka"><%=BPAS %></td>
                                                <td class="kotak angka"><%=Luar %></td>
                                                <td class="kotak angka">&nbsp;</td>
                                                <td class="kotak tengah"><%=TjmlMobil.ToString("N0") %></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                    </div>
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
                <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>