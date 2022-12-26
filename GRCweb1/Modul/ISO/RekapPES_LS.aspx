<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPES_LS.aspx.cs" Inherits="GRCweb1.Modul.ISO.RekapPES_LS" %>

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
                        Rekap PES - Print
                    </div>
                    <div style="padding: 2px"></div>

                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                        <tr>
                            <td style="width: 10%; padding-left: 10px;">Periode
                            </td>
                            <td style="width: 20%">
                                <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                    <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px">Department
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" Text="Preview" />
                            </td>
                            <td>&nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                        To Excel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; font-size: x-small;">
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
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="500px">
                                    <table class="table-responsive" width="100%">
                                        <tr>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="19" style="font-size: small; font-weight: bold">PT. BANGUNPERKASA ADHITAMASENTRA
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                            <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="font-size: larger; font-weight: bold" colspan="33">DATA NILAI (PES)
                                <asp:Label ID="LPeriode" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="DivRoot" class="table-responsive" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" PageSize="20"
                                                Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">
                                                <RowStyle BackColor="White" Font-Names="tahoma" Font-Size="XX-Small" />
                                                <HeaderStyle BackColor="#CCCCCC" BorderColor="#CCCCCC" BorderStyle="Groove" BorderWidth="2px"
                                                    Font-Names="tahoma" Font-Size="XX-Small" />
                                                <PagerStyle BorderStyle="Solid" />
                                            </asp:GridView>
                                            <table width="50%">
                                                <tr>
                                                    <td>Bobot PES&nbsp;
                                        <asp:Label ID="lblDept" runat="server" Text=""></asp:Label>
                                                        &nbsp;Plant
                                        <asp:Label ID="lblPlant" runat="server" Text=""></asp:Label>
                                                        <asp:GridView ID="GrdDynamic0" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                            HorizontalAlign="Center" OnRowCreated="grv0MergeHeader_RowCreated" PageSize="20"
                                                            Style="margin-right: 0px" Width="98%" OnDataBinding="GridView2_DataBinding">
                                                            <RowStyle BackColor="White" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="#CCCCCC" BorderColor="#CCCCCC" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="DivFooterRow" style="overflow: hidden">
                                        </div>
                                    </div>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" style="font-size: small; font-weight: bold" colspan="13">
                                                    <asp:Label ID="txtLokasi" runat="server"></asp:Label>
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; width: 2%;">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="left" style="font-size: small; font-weight: bold">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; width: 2%;">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size: small; font-weight: bold; height: 23px;" colspan="9">Dibuat Oleh
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" colspan="6">Diketahui Oleh
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" colspan="5">Disetujui oleh
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 23px;" width="2%"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px; width: 2%;"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                                <td align="center" style="font-size: small; font-weight: bold; height: 66px;" width="2%"></td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="font-size: small; font-weight: bold" colspan="9">(..............................)
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" colspan="6">(..............................)
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" colspan="5">(..............................)
                                                </td>
                                                <td align="center" style="font-size: small; font-weight: bold" width="2%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>


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
