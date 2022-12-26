<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="GRCweb1.Modul.ReportT1T3.Report" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg-default-gradient" >
            <asp:Panel ID="Panel1" runat="server" Visible="false">
                &nbsp; Pilih Printer
             <asp:DropDownList ID="ddlPrinter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
             </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:Panel>
        </div>
        <div class="card-body bg-default-gradient" >
            <div class="row">
                <div class="col-md-12">
                    <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"
                        HasToggleGroupTreeButton="false" DisplayToolbar="false" EnableDatabaseLogonPrompt="False"
                        EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
