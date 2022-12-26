<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report2.aspx.cs" Inherits="GRCweb1.Modul.Report.Report21" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

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
        <div class="bg-default-gradient">
            &nbsp; Pilih Printer
             <asp:DropDownList ID="ddlPrinter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
             </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
             <%--<asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />--%>
        </div>
        <div class="card-body bg-default-gradient">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="overflow: scroll; width: 100%; height: 100%">
                        <div id="dvReportFinal">
                            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"
                                HasToggleGroupTreeButton="false" DisplayToolbar="false" EnableDatabaseLogonPrompt="False"
                                EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
