<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EffBatuBara.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.EffBatuBara" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>
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
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>



    </head>

    <body class="no-skin">


        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Effisiensi Batu Bara
                            </div>
                            <div style="padding: 2px"></div>


                            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td>
                                            <div class="">
                                                <br />
                                                <table style="width: 100%; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="width: 10%">&nbsp;</td>
                                                        <td style="width: 15%">Periode</td>
                                                        <td style="width: 35%">
                                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                                                <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
                                                                <asp:ListItem Value="1">Januari</asp:ListItem>
                                                                <asp:ListItem Value="2">Februari</asp:ListItem>
                                                                <asp:ListItem Value="3">Maret</asp:ListItem>
                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                <asp:ListItem Value="5">Mei</asp:ListItem>
                                                                <asp:ListItem Value="6">Juni</asp:ListItem>
                                                                <asp:ListItem Value="7">Juli</asp:ListItem>
                                                                <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">Desember</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                                 <asp:DropDownList ID="ddlTahun" runat="server">
                                                                     <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                                                 </asp:DropDownList>
                                                        </td>
                                                        <td></td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4">&nbsp</td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                                            <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                                <hr />
                                                <div class="contentlist" style="height: 420px; text-align: center">
                                                   <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                      
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

                                                       
                                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small;" border="0">
                                                            
                                                            
                                                            <thead>
                                                                <td id="grc"></td> <td id="spasi"></td> <td id="spasi1"></td> <td id="spasi3"></td> <td id="isosai"></td> <td id="spasi2"></td>
                                                                <tr>
                                                                    <td colspan="7" id="xx">&nbsp;</td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <th>&nbsp;</th>
                                                                    <th class="kotak">KG/M3 </th>
                                                                    <th class="kotak">Dibuat</th>
                                                                    <th class="kotak">Mengetahui</th>
                                                                    <th class="kotak">Menyetujui</th>


                                                                </tr>

                                                                <tr>
                                                                    <th>PENCAPAIAN :</th>
                                                                    <th class="kotak"><%=hasilpencapaian.ToString("##0.00") %></th>
                                                                    <td id="admin" class="kotak" rowspan="2">&nbsp;<asp:Image ID="Image2" runat="server" Height="50" Width="50" /></td>
                                                                    <td id="manager" class="kotak" rowspan="2">&nbsp;<asp:Image ID="Image1" runat="server" Height="50" Width="50" /></td>
                                                                    <td id="pm" class="kotak" rowspan="2">&nbsp;<asp:Image ID="Image3" runat="server" Height="50" Width="50" /></td>


                                                                </tr>

                                                                <tr>
                                                                    <th></th>
                                                                    <th class="kotak">(<%=hasilpencapaian.ToString("##0.00") %> / 40) x 100 = <%=hasilakhir.ToString("##0.00") %></th>
                                                                </tr>

                                                                <tr>
                                                                    <th></th>
                                                                    <th class=""></th>
                                                                    <td class="kotak">&nbsp;<asp:Label ID="LblAdmin" runat="server"></asp:Label></td>
                                                                    <td class="kotak">&nbsp;<asp:Label ID="LblMgr" runat="server"></asp:Label></td>
                                                                    <td class="kotak">&nbsp;<asp:Label ID="LblPM" runat="server"></asp:Label></td>


                                                                </tr>

                                                                <tr>
                                                                    <th colspan="7">&nbsp;</th>
                                                                </tr>
                                                                <tr class="tbHeader">

                                                                    <th class="kotak tengah" rowspan="2" style="width: 1%">NO.</th>
                                                                    <th class="kotak tengah" rowspan="2" style="width: 5%">TANGGAL</th>
                                                                    <th class="kotak tengah" colspan="2" style="width: 15%">OUTPUT OVEN DRYING</th>
                                                                    <th class="kotak tengah" rowspan="2" style="width: 8%">BATU BARA</th>
                                                                    <th class="kotak tengah" rowspan="2" style="width: 8%">KG/M3</th>

                                                                </tr>

                                                                <tr class="tbHeader">
                                                                    <th class="kotak tengah" style="width: 5%">LEMBAR
                                                                    </th>
                                                                    <th class="kotak tengah" style="width: 5%">M3
                                                                    </th>
                                                                </tr>

                                                            </thead>

                                                            <tbody>
                                                                <asp:Repeater ID="lstBatuBara" runat="server" OnItemDataBound="lstBatuBara_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris kotak">
                                                                            <td class="kotak tengah"><%# Eval("Nom") %></td>
                                                                            <td class="kotak tengah"><%# Eval("Tanggal","{0:d}") %></td>
                                                                            <td class="kotak tengah"><%# Eval("Lembar","{0:N0}") %></td>
                                                                            <td class="kotak angka"><%# Eval("M3","{0:N2}") %></td>
                                                                            <td class="kotak angka"><%# Eval("QtyBatubara","{0:N0}") %></td>
                                                                            <td class="kotak angka"><%# Eval("Kgm3","{0:N2}") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:Repeater>

                                                                <asp:Repeater ID="lstBatuBaraJumlah" runat="server" OnItemDataBound="lstBatuBaraJumlah_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris kotak">
                                                                            <td class="kotak tengah">
                                                                                <td class="kotak tengah"><%# Eval("Keterangan") %></td>
                                                                                <td class="kotak tengah"><%# Eval("Jmllebar","{0:N0}") %></td>
                                                                                <td class="kotak angka"><%# Eval("Jmlm3","{0:N2}") %></td>
                                                                                <td class="kotak angka"><%# Eval("Jmlbatubara","{0:N0}") %></td>
                                                                                <td class="kotak angka"><%# Eval("Jmlkgm3","{0:N2}") %></td>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>


                                                            </tbody>


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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
