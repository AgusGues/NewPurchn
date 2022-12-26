<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PemantauanScrabKeringDanBasah.aspx.cs" Inherits="GRCweb1.Modul.Boardmill.PemantauanScrabKeringDanBasah" %>

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


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        PEMANTAUAN SCRAB KERING DAN BASAH
                    </div>
                    <div style="padding: 2px"></div>


                    <div id="div1" runat="server" class="table-responsive" style="width:100%">
            <table style="table-layout:fixed; width:100%;">
            <tr>
                <td colspan="5" style="height: 49px">
                
               </td>
            </tr>
            <tr>
            <td colspan="5" valign="top" align="left">
                <div id="div2" class="">
                <table id="headerPO" width="100%" style="border-collapse:collapse;" visible="true" runat="server">
                    <tr><td colspan="5">&nbsp;</td></tr>
                    <tr>
                        <td width="15%" align="right" style="height: 24px">Periode &nbsp;</td>
                        <td width="25%" style="height: 24px"> 
                            <asp:TextBox ID="txtDrTgl" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy" EnableViewState="true">
                                                </cc1:CalendarExtender>
                             <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy" EnableViewState="true">
                                                </cc1:CalendarExtender>
                        </td>
                        
                        <td width="5%" style="height: 24px"></td>
                        
                        <td width="25%" style="height: 24px">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                        </td>
                    </tr>
                    
                   <tr>
                        <td width="15%" align="right">Jenis Scrab&nbsp;</td>
                        <td width="20%" colspan="2" style="width: 25%">
                        <asp:DropDownList ID="ddlPilihan" runat="server" onselectedindexchanged="ddlPilihan_SelectedIndexChanged" AutoPostBack="true" Width="120px">
                            <asp:ListItem Value="0">--ALL--</asp:ListItem>
                            <asp:ListItem Value="1">SCRAB KERING</asp:ListItem>
                            <asp:ListItem Value="2">SCRAB BASAH</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td width="15%" align="right">&nbsp;</td>
                        
                    </tr>
                    
                    <tr>
                        <td width="15%" align="right">&nbsp;</td>
                        <td width="20%" colspan="2" style="width: 25%">
                        <asp:Button ID="Preview" runat="server" Text="Preview" onclick="Preview_Click" />
                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                            <asp:Button ID="ExportXls" runat="server" onclick="ExportToExcel" Text="Export to Excel" Visible="true" />
                        </td>
                        <td>
                            <table id="Table1" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    
                                    <tr style="width: 100%">
                                    
                                        <td style="width: 44%;">
                                            <asp:Label ID="lblakumulasioutput" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold">&nbsp; Tahun :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%;">
                                            <asp:TextBox ID="txttahun" runat="server"  Style="font-family: Calibri;
                                                font-size: x-small" BorderStyle="None" Enabled="false" ></asp:TextBox>
                                            
                                        </td>
                                        
                                        
                                    </tr>
                                    
                                    <tr style="width: 100%">
                                        <td style="width: 44%">
                                            <asp:Label ID="lblakumulasipemakaian" runat="server" Visible="true" Style="font-family: Calibri;
                                                font-size: x-small; font-weight: bold" >&nbsp; Bulan :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txtbulan" runat="server" Style="font-family: Calibri;
                                                font-size: x-small" BorderStyle="None" Enabled="false"></asp:TextBox>
                                            
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr style="width: 100%">
                                        <td style="width: 44%">
                                            <asp:Label ID="lblefisiensi" runat="server" Style="font-family: Calibri; font-size: x-small;
                                                font-weight: bold" Visible="true" >&#160; Status Approval :</asp:Label>
                                        </td>
                                        
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txtstatusapproval" runat="server" BorderStyle="None" Style="font-family: Calibri;
                                                font-size: x-small" Enabled="false"></asp:TextBox>
                                            
                                        </td>
                                        
                                    </tr>
                                   
                                </table>
                        </td>
                        
                        <td width="15%" align="right">&nbsp;</td>
                        
                        
                        
                    </tr>
                    </table>
                    
                <hr />
                    <div  id="lstSP" runat="server" style="width:100%; height:350px; background-color:White; border:2px solid #B0C4DE;; padding:3px; overflow:auto">
                        <div id="lst" runat="server" class="contentlist" style="height:380px">
                        <table width="100%" style="border-collapse:collapse; font-size:x-small"  id="lst1" border=1px; >
                            <thead>
                                <tr align="center" style="background-color:#E0D8E0;">
                                    <th style="width:1%; border:1px solid grey">No.</th>
                                    <th style="width:1%; border:1px solid grey">ID</th>                                    
                                    <th style="width:2%; border:1px solid grey">Tanggal</th>
                                    <th style="width:2%; border:1px solid grey">Jenis Scrab</th>
                                    <th style="width:2%; border:1px solid grey">Palet</th>
                                    <th style="width:1%; border:1px solid grey">Jumlah</th>
                                    <th style="width:2%; border:1px solid grey">Berat/Kg</th>
                                    <th style="width:1%; border:1px solid grey">M3</th>
                                    <th style="width:3%; border:1px solid grey">Keterangan</th>
                                    <th style="width:2%; border:1px solid grey">Tanggal</th>
                                    <th style="width:2%; border:1px solid grey">Jenis Scrab</th>
                                    <th style="width:2%; border:1px solid grey">Bin</th>
                                    <th style="width:1%; border:1px solid grey">Jumlah</th>
                                    <th style="width:2%; border:1px solid grey">Berat/Kg</th>
                                    <th style="width:1%; border:1px solid grey">M3</th>
                                    <th style="width:3%; border:1px solid grey">Keterangan</th>
                                    <th style="width:1%; border:1px solid grey">Total M3</th>
                                </tr>
                            </thead>
                           
                                <asp:Repeater ID="lstScrab" runat="server" OnItemDataBound="lstScrab_ItemDataBound" >
                                <ItemTemplate>
                                    <tr align="center" valign="top">
                                        <td align="center" border=1px; ><%#Eval("Nom") %></td>
                                        <td align="center" valign="top" border=1px; ><%# Eval("Id") %></td>
                                        <td align="left" border:1px; ><%# Eval("Tanggal","{0:d}") %></td>
                                        <td border=1px; ><%# Eval("JenisScrab") %></td>
                                        <td border=1px; ><%# Eval("Palet") %></td>
                                        <td border:1px; ><%# Eval("PaletJumlah") %></td>
                                        <td border:1px; ><%# Eval("BeratPalet") %></td>
                                        <td border:1px; ><%# Eval("M3Palet", "{0:N1}")%></td>
                                        
                                        <td border=1px; ><%# Eval("KeteranganPalet")%></td>
                                        <td align="left" border:1px; ><%# Eval("TangglKedua", "{0:d}")%></td>
                                        <td border=1px; ><%# Eval("JenisScrabkedua")%></td>
                                        <td border:1px; ><%# Eval("Bin") %></td>
                                        <td border:1px; ><%# Eval("BinJumlah") %></td>
                                        <td border:1px; ><%# Eval("BeratBin") %></td>
                                        <td border:1px; ><%# Eval("M3Bin", "{0:N1}")%></td>
                                        
                                        <td border:1px; ><%# Eval("KeteranganBin") %></td>
                                        <td border:1px; ><%# Eval("TotalM3","{0:N1}") %></td>
                                        
                                   </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                <tr class="Line3">
                                                            <td class="kotak angka" colspan="5">
                                                                <b>Total</b> &nbsp;
                                                            </td>
                                                            <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lbljumlah" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lblberatkg"  runat="server"></asp:Label>
                                                            </td>
                                                             <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lblhasilm3"  runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="4">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lbljumlah1" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lblberatkg1"  runat="server"></asp:Label>
                                                            </td>
                                                             <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lblhasilm31"  runat="server"></asp:Label>
                                                            </td>
                                                            <td class="kotak" colspan="1">
                                                                &nbsp;
                                                            </td>
                                                            <td class="kotak tengah bold" align="center" valign="top">
                                                                <asp:Label ID="lblhasitotalm3"  runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                              </FooterTemplate>
                             </asp:Repeater>
                          
                        </table>
                        </div>
                    </div>
                </div>
                </td>
                </tr>
                </table>
                </div>
                    
                </div>
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
</asp:Content>
