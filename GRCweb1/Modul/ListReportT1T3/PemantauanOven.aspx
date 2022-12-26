<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanOven.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.PemantauanOven" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!DOCTYPE html>
    <html lang="en">
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <title>Report SPKP</title>
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/chosen.min.css" />

    </head>

    <body class="no-skin" runat="server">
        <div class="row" runat="server">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">   
                    <div class="panel-heading">Pemantauan Oven
                     </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
						    <div class="col-md-1"></div>
						    <div class="col-md-1">Bulan</div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" Width="132px">
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
                                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">Tahun</div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddTahun" runat="server">
                                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                </asp:DropDownList>
                             </div>
                            <div class="col-md-1">Line</div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlOven" runat="server">
                                    <asp:ListItem>---Pilih Oven---</asp:ListItem>
                                    <asp:ListItem>Oven 1</asp:ListItem>
                                    <asp:ListItem>Oven 2</asp:ListItem>
                                    <asp:ListItem>Oven 3</asp:ListItem>
                                    <asp:ListItem>Oven 4</asp:ListItem>
                                    <asp:ListItem>Akumulasi</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="padding: 2px"></div>
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" meta:resourcekey="Button1Resource1" />
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" meta:resourcekey="LinkButton3Resource1">Export To Excel</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" ScrollBars="Vertical"
                    Wrap="False" Height="500px">
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                Pemantauan Output &nbsp;
                                <asp:Label ID="LblPeriode" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                Periode : <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                                &nbsp;
                                <asp:Label ID="LblTgl2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div id="DivRoot" align="left">
                            <div style="overflow: hidden;" id="DivHeaderRow">
                            </div>
                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                    HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                    Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">
                                    <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                            <div id="DivFooterRow" style="overflow: hidden">
                            </div>
                        </div>
                            </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>
</asp:Content>
