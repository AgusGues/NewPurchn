<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg-default-gradient">
            &nbsp; Pilih Printer
             <asp:DropDownList ID="ddlPrinter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
             </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
        </div>
        <div class="card-body bg-default-gradient">
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
