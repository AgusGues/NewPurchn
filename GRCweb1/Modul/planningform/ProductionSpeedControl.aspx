<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductionSpeedControl.aspx.cs" Inherits="GRCweb1.Modul.planningform.ProductionSpeedControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"  Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    $(document).ready(function() {
        maintainScrollPosition();
    });
    function pageLoad() {
        maintainScrollPosition();
    }
    function maintainScrollPosition() {
        $("#<%=lst2.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
    }
    function setScrollPosition(scrollValue) {
        $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
    }      
</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="height:49px">
                            <table class="nbTableHeader" style="width:100%">
                                <tr>
                                    <td>SPEED MONITORING</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:small; margin-top:5px">
                                    <tr>
                                        <td style="width:4%">&nbsp;</td>
                                        <td style="width:10%">Tanggal</td>
                                        <td style="width:30%"><asp:TextBox ID="txtTanggal" runat="server" 
                                                ontextchanged="txtTanggal_TextChanged"></asp:TextBox>
                                           <asp:Button ID="btnPrev" runat="server" Text="&lArr; Prev" OnClick="btnPrev_Click" />&nbsp
                                           <asp:Button ID="btnNext" runat="server" Text="Next &rArr;" OnClick="btnNext_Click" />
                                        </td>
                                        <td align="right" style="padding-right:10px"><div id="frmUpload" runat="server">
                                            <asp:FileUpload ID="Upload1" runat="server" Width="80%" />
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </div>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="34">
                                            <cc1:CalendarExtender ID="Ca1" runat="server" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td><td>&nbsp;</td>
                                        <td><asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" /></td>
                                        <td align="right" style="padding-right:10px"><div id="frmUpload1" runat="server">
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></div></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td align="left" style="padding-right:10px">
                                            Pilih interval waktu
                                            <asp:RadioButton ID="RBmenit1" runat="server" AutoPostBack="True" 
                                                Checked="True" GroupName="m" oncheckedchanged="RBmenit1_CheckedChanged" 
                                                Text="1 Menit" />
                                            &nbsp;<asp:RadioButton ID="RBmenit2" runat="server" AutoPostBack="True" 
                                                GroupName="m" oncheckedchanged="RBmenit2_CheckedChanged" Text="5 Menit" />
                                            &nbsp;<asp:RadioButton ID="RBmenit3" runat="server" AutoPostBack="True" 
                                                GroupName="m" oncheckedchanged="RBmenit3_CheckedChanged" Text="10  Menit" />
                                            &nbsp;<asp:RadioButton ID="RBmenit4" runat="server" AutoPostBack="True" 
                                                GroupName="m" oncheckedchanged="RBmenit4_CheckedChanged" Text="15 Menit" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height:430px; overflow:auto" id="lst2" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <asp:GridView ID="lst" runat="server" AutoGenerateColumns="true" Visible="false"></asp:GridView>
                                    <cc2:Chart ID="Chart2" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                   <hr />
                                   <cc2:Chart ID="Chart3" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                   <hr />
                                   <cc2:Chart ID="Chart4" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                   <hr />
                                   <cc2:Chart ID="Chart5" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                   <hr />
                                   <cc2:Chart ID="Chart6" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                   <hr />
                                   <cc2:Chart ID="Chart7" runat="server" Height="400px" Width="1100px" BorderlineColor="Blue" Visible="true">
                                        <Titles><cc2:Title Name="title" ShadowOffset="3" Font="Arial,12pt,style=Bold"></cc2:Title></Titles>
                                        <ChartAreas><cc2:ChartArea Name="Area1"></cc2:ChartArea></ChartAreas>
                                   </cc2:Chart>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
        </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
