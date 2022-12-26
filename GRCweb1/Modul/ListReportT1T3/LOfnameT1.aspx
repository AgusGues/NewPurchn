<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LOfnameT1.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.LOfnameT11" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="table-responsive">
        <table class="nbTableHeader" style="width:100%; border-collapse:collapse">
            <tr>
            <td>
                <div class="content">
                    <hr />
                    <div class="contentlist">
                        
                        <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"
                                HasToggleGroupTreeButton="false" DisplayToolbar="false" EnableDatabaseLogonPrompt="False"
                                EnableParameterPrompt="False" ToolPanelView="None" Height="50px" ToolPanelWidth="100%" Width="350px" />
                    </div>
                </div>
            </td>
        </tr>
        </table>
    </div>
</asp:Content>
