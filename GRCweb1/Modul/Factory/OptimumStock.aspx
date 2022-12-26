<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptimumStock.aspx.cs" Inherits="GRCweb1.Modul.Factory.OptimumStock" %>

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

        <script language="javascript" type="text/javascript">
            function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                var tbl = document.getElementById(gridId);
                if (tbl) {
                    var DivHR = document.getElementById('DivHeaderRow');
                    var DivMC = document.getElementById('DivMainContent');
                    var DivFR = document.getElementById('DivFooterRow');

                    //*** Set divheaderRow Properties ****
                    DivHR.style.height = headerHeight + 'px';
                    DivHR.style.width = '98.5%';
                    DivHR.style.position = 'relative';
                    DivHR.style.top = '0px';
                    DivHR.style.zIndex = '2';
                    //***DivHR.style.verticalAlign = 'top';

                    //*** Set divMainContent Properties ****
                    DivMC.style.width = width + '%';
                    DivMC.style.height = height + 'px';
                    DivMC.style.position = 'relative';
                    DivMC.style.top = -headerHeight + 'px';
                    DivMC.style.zIndex = '0';

                    //*** Set divFooterRow Properties ****
                    DivFR.style.width = (parseInt(width)) + 'px';
                    DivFR.style.position = 'relative';
                    DivFR.style.top = -headerHeight + 'px';
                    DivFR.style.verticalAlign = 'top';
                    DivFR.style.paddingtop = '2px';

                    if (isFooter) {
                        var tblfr = tbl.cloneNode(true);
                        tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                        var tblBody = document.createElement('tbody');
                        tblfr.style.width = '100%';
                        tblfr.cellSpacing = "0";
                        tblfr.border = "0px";
                        tblfr.rules = "none";
                        //*****In the case of Footer Row *******
                        tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                        tblfr.appendChild(tblBody);
                        DivFR.appendChild(tblfr);
                    }
                    //****Copy Header in divHeaderRow****
                    DivHR.appendChild(tbl.cloneNode(true));
                }
            }

            function OnScrollDiv(Scrollablediv) {
                document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
            }


            function btnHitung_onclick() {

            }

        </script>

    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Optimum Stock
                    </div>
                    <div style="padding: 2px"></div>


                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                    <table style="width: 100%; font-size: x-small;"">
                        <tr>
                            <td colspan="5">
                                <asp:RadioButton ID="RBTglInput" runat="server" GroupName="a" Text="Tanggal  Input"
                                    Visible="False" />
                                <asp:RadioButton ID="RBTglProduksi" runat="server" GroupName="a" Text="Tanggal  Produksi"
                                    Visible="False" />
                                <asp:RadioButton ID="RBTglPotong" runat="server" Checked="True" GroupName="a" Text="Tanggal Potong"
                                    Visible="False" />
                                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                                    OnTextChanged="txtdrtanggal_TextChanged" Enabled="False" Visible="False"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"
                                    OnTextChanged="txtsdtanggal_TextChanged" Enabled="False" Visible="False"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                    TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 82px" align="right">Periode
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddTahun" runat="server">
                                    <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged"
                                    Width="132px">
                                    <asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>
                                    <asp:ListItem Value="01">Januari</asp:ListItem>
                                    <asp:ListItem Value="02">Februari</asp:ListItem>
                                    <asp:ListItem Value="03">Maret</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">Mei</asp:ListItem>
                                    <asp:ListItem Value="06">Juni</asp:ListItem>
                                    <asp:ListItem Value="07">Juli</asp:ListItem>
                                    <asp:ListItem Value="08">Agustus</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">Oktober</asp:ListItem>
                                    <asp:ListItem Value="11">Nopember</asp:ListItem>
                                    <asp:ListItem Value="12">Desember</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Preview" />
                            </td>
                            <td>&nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 82px; height: 19px;">&nbsp;
                            </td>
                            <td style="width: 158px; height: 19px;">&nbsp;
                            </td>
                            <td style="width: 78px; height: 19px;"></td>
                            <td style="width: 151px; height: 19px;">&nbsp;
                            </td>
                            <td style="height: 19px" align="right">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Panel ID="Panel1" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" Wrap="False"
                                    Height="400px">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>PT. BANGUNPERKASA ADHITAMASENTRA
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td align="right">Form. No. : &nbsp;
                                                <asp:Label ID="NomorOp" runat="server" Text="Nomor"></asp:Label>
                                                <%--PIC/K/PS/12/03/R9--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td align="center">PEMANTAUAN STOCK
                                            </td>
                                            <td>
                                                <%--&nbsp;
                                <asp:Label ID="nooptimum" runat="server" Text="Nomor"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td align="center">periode
                                <asp:Label ID="LblPeriode" runat="server" Visible="False"></asp:Label>
                                                &nbsp;:
                                <asp:Label ID="LblTgl1" runat="server"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
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
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="font-size: xx-small">PROSENTASE = ((HARI KERJA X JML ITEM PRODUK 
                        STANDARD) - JLM KALI PRODUK DIBAWAH MINIMUM STOCK) / (HARI KERJA X JML ITEM 
                        PRODUK STANDARD)</td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: xx-small">((
                                <asp:Label ID="Label3" runat="server" Text="0"></asp:Label>
                                                &nbsp;X
                                <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
                                                &nbsp;) -
                                <asp:Label ID="Label5" runat="server" Text="0"></asp:Label>
                                                &nbsp;) / (
                                <asp:Label ID="Label6" runat="server" Text="0"></asp:Label>
                                                &nbsp;X
                                <asp:Label ID="Label7" runat="server" Text="0"></asp:Label>
                                                &nbsp;) =
                                <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                                                &nbsp;/
                                <asp:Label ID="Label9" runat="server" Text="0"></asp:Label>
                                                &nbsp;=
                                <asp:Label ID="Label10" runat="server" Text="0"></asp:Label>
                                                &nbsp;%</td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: xx-small">&nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
