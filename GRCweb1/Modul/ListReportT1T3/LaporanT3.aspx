<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LaporanT3.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LaporanT3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!DOCTYPE html>
    <html lang="en">
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <title>SPKP</title>
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/chosen.min.css" />
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
    </style>
    <script type="text/javascript" src="../../Scripts/calendar.js"></script>
    <script type="text/javascript">
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
            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LapOpnameT3B", 900, 800);
        }
        function Cetak2() {
            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LapOpnameT3T", 900, 800);
        }
    </script>
    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">   
                    <div class="panel-heading" style="color: #000000">LAPORAN OPNAME TAHAP III
                        <div class="pull-right">
                            <div class="col-md-12">
                                <input id="btn1" runat="server" align="middle" onserverclick="btn1_ServerClick" style="
                                font-weight: bold; font-size: 11px; height: 22px; width: 125px;" type="button"
                                value="Preview" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="input">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB1_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: right;" Text="General Opname ( Tahunan )"
                                            TextAlign="Left" Width="300px" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">              
                                            <asp:RadioButton ID="RB2" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB2_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: right;" Text="Bulanan"
                                            TextAlign="Left" Width="100px" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding:15px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">Bulan</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" Style="font-family: Calibri; font-size: small" Width="180px">
                                                <asp:ListItem Value="DesQty">Januari</asp:ListItem>
                                                <asp:ListItem Value="JanQty">Februari</asp:ListItem>
                                                <asp:ListItem Value="FebQty">Maret</asp:ListItem>
                                                <asp:ListItem Value="MarQty">April</asp:ListItem>
                                                <asp:ListItem Value="AprQty">Mei</asp:ListItem>
                                                <asp:ListItem Value="MeiQty">Juni</asp:ListItem>
                                                <asp:ListItem Value="JunQty">Juli</asp:ListItem>
                                                <asp:ListItem Value="JulQty">Agustus</asp:ListItem>
                                                <asp:ListItem Value="AguQty">September</asp:ListItem>
                                                <asp:ListItem Value="SepQty">Oktober</asp:ListItem>
                                                <asp:ListItem Value="OktQty">November</asp:ListItem>
                                                <asp:ListItem Value="NovQty">Desember</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">Tahun</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:DropDownList ID="ddlTahun" runat="server" Height="22px" Style="font-family: Calibri;
                                            font-size: small" Width="100px">
                                                                
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding:15px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">Tgl Stock Opname</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:TextBox ID="txtTgl" runat="server" Enabled="False" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700; margin-left: 0px;" Width="180px"></asp:TextBox>
                                                        
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTgl">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding:15px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">Tgl Cutt Off</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:TextBox ID="txtCut" runat="server" Enabled="False" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700; margin-left: 0px;" Width="180px"></asp:TextBox>
                                                        
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtCut">
                                            </cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding:15px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">PartNo</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True" Height="20px" OnTextChanged="txtPartnoA_TextChanged"
                                            Width="180px" Style="background-color: #FFFFFF; font-family: Calibri; font-weight: 700;
                                            font-size: small;"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                            CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetMasterPartNoT3" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
                                            </cc1:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding:15px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">Stoker</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                        <asp:DropDownList ID="ddlS" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="ddlS_SelectedIndexChanged"
                                        Style="margin-left: 0px; font-family: Calibri; font-size: small;" Width="180px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
       
</asp:Content>
