<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LReject.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LReject" %>
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
    
        //void Page_Load()
        //{
        //    string[] cookies = Request.Cookies.AllKeys;
        //    foreach (string cookie in cookies)
        //    {
        //        BulletedList1.Items.Add("Deleting " + cookie);
        //        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        //    }
        //}
        
       function imgChange(img) {
           document.LookUpCalendar.src = img;
       }
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
            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=LReject", 900, 800)
        }     
    </script>
    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">   
                    <div class="panel-heading">LAPORAN MUTASI REJECT PRODUK</div>
                    <div class="panel-body" id="input">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center"> Departemen</div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:DropDownList ID="ddlDept" runat="server" Height="22px" 
                                            OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged" Width="132px">
                                                <asp:ListItem>Logistik</asp:ListItem>
                                                <asp:ListItem>Finishing</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center"> Bulan</div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Height="22px" 
                                            OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged" Width="132px">
                                                <%--<asp:ListItem Value="0">---Pilih Bulan---</asp:ListItem>--%>
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
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center"> Tahun </div>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:DropDownList ID="ddTahun" runat="server"  Height="22px" Width="132px">
                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:RadioButton ID="RBBulan" runat="server" Checked="True" GroupName="a" Text="Bulanan" />
                                            &nbsp;<asp:RadioButton ID="RBBulan0" runat="server" GroupName="a" Text="Harian" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" OnClientClick="Cetak();" Text="Preview" />
                                        </div>
                                    </div>
                                </div>
                             </div>

                            <div style="padding: 30px"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </body>
        </html>
</asp:Content>
