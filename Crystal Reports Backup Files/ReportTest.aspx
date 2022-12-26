<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTest.aspx.cs" Inherits="GRCweb1.ReportTest.ReportTest" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 250px;
        }
        .auto-style2 {
            width: 267px;
        }
        .auto-style3 {
            width: 258px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
					    <input id="Text1" type="text" runat="server" class="auto-style1"/>
					    <input id="Text2" type="text" runat="server" class="auto-style2"/>
					    <input id="Text3" type="text" runat="server" class="auto-style3"/>

        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
            HasToggleGroupTreeButton="false" DisplayToolbar="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" ToolPanelView="None"                       
            />
        <cr:crystalreportsource runat="server">
            <report filename="ReportTest\cr1.rpt">
            </report>
                        </cr:crystalreportsource>

    </div>
    </form>
</body>
</html>
