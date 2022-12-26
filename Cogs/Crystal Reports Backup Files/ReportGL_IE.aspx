<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportGL_IE.aspx.cs" Inherits="GRCweb1.Modul.ReportGL.ReportGL_IE" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self"/>

    <link href="../aspnet_client/system_web/2_0_50727/CrystalReportWebFormViewer4/css/default.css" rel="stylesheet" type="text/css" />       
	<link rel="stylesheet" type="text/css" href="../../aspnet_client/system_web/4_6_114/crystalreportviewers13/js/crviewer/images/style.css" />

    <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<%--        <input class="hideButtonOnPrint" id="btnPrint" style="WIDTH: 74px; HEIGHT: 21px" onclick="window.print()" />
		type="button" value="Cetak" name="btnPrint">
        <asp:Button ID="BtnCetak1" runat="server" Height="21px" 
            onclick="BtnCetak1_Click" Text="Cetak ( Direct )" Visible="False" />
        <asp:DropDownList ID="ddlPrinter" runat="server" AutoPostBack="True" 
            Height="16px" onselectedindexchanged="ddlPrinter_SelectedIndexChanged" 
            Width="289px" Enabled="False" Visible="False">
        </asp:DropDownList>--%>

	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->

			<div class="form-horizontal" role="form">
				<div class="form-group">
					<div class="col-sm-9">

                        <span class="input-icon">
<%--                            <asp:DropDownList ID="ddlPrinter" class="col-sm-3 form-control center" runat="server" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlPrinter" class="form-control center" runat="server" OnSelectedIndexChanged="ddlPrinter_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span>
                        <span>
						    <button class="btn btn-info" type="button" id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick">
							    <i class="ace-icon fa fa-check bigger-110"></i>
							    Print
						    </button>
                        </span>

<%--                        <span class="input-icon">
    						<input type="text" id="txtTahun1" runat="server" placeholder="Tahun" class="col-xs-10 col-sm-5" />
							<i class="ace-icon fa fa-calendar green"></i>
                        </span>--%>
                    </div>
                </div>
            </div>
        </div>
        


<%--        <asp:Label ID="Label1" runat="server" Text="Label" Enabled="False" 
            Visible="False"></asp:Label>--%>


        <br />
        <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" 
            HasToggleGroupTreeButton="false" DisplayToolbar="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" ToolPanelView="None"            
            />
        </div>




    
    </div>
    </form>
</body>
</html>
