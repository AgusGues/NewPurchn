<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapKirimM3.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.RekapKirimM3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title></title>

                <meta name="description" content="Common form elements and layouts" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
                <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
                <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
                </script>
                <%--void Page_Load()
                {
                    string[] cookies = Request.Cookies.AllKeys;
                    foreach (string cookie in cookies)
                    {
                        BulletedList1.Items.Add("Deleting " + cookie);
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                }
    
                    function imgChange(img) {
                        document.LookUpCalendar.src = img;
                }  --%>   
          

        </script>
        </head>
             <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">REKAP PENGIRIMAN 2 PLANT (M3)</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Bulan</label>
                                        </div>
                                        <div class="col-md-9">
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
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9" style="font-size: 14px">Tahun</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="ddTahun" runat="server" Height="22px" Width="132px">
                                                <asp:ListItem Value="0">---Pilih Tahun---</asp:ListItem>
                                            </asp:DropDownList>
                                         </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Preview" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export To Excel</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-12">
                                    <div class="row">
                                        <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#CCFFFF" 
                                                  Height="436px" ScrollBars="Vertical" Wrap="False">
                                            Rekap Pengiriman 3 Plant (M3)&nbsp; periode <asp:Label ID="LblPeriode" runat="server"></asp:Label>
                                            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" 
                                                CaptionAlign="Left" HorizontalAlign="Center" 
                                                onrowdatabound="GrdDynamic_RowDataBound" PageSize="20" 
                                                Style="margin-right: 0px" Width="98%">
                                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" />
                                                <HeaderStyle BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" 
                                                Font-Names="tahoma" Font-Size="XX-Small" />
                                                <PagerStyle BorderStyle="Solid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
