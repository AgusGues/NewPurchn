<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.cs" Inherits="GRCweb1.Modul.Report.Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Rekap PO</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
        <script src="../../assets/html2pdf/dist/html2pdf.bundle.min.js" type="text/javascript"></script>
        <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    </head>
    <body class="no-skin">
         <div class="bg-default-gradient">
             
        &nbsp; Pilih Printer
             <asp:DropDownList ID="ddlPrinter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
             </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click"  />
        </div>
        <div class="card-body bg-default-gradient">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="overflow: scroll; width: 100%; height: 100%">
                        <div id="dvReportFinal">
                            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"
                                HasToggleGroupTreeButton="false" DisplayToolbar="True" EnableDatabaseLogonPrompt="False"
                                EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="bg-default-gradient">
        </div>
    </body>
</asp:Content>
