<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TotalDefect.aspx.cs" Inherits="GRCweb1.Modul.ListReportDefect.TotalDefect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            MyPopUpWin("../ReportT1T3/Report.aspx?IdReport=RekapDefect", 900, 800)
        }     
    </script> 
    <table style="width: 100%; font-size: x-small;">
        <tr>
            <td colspan="4">
                <asp:RadioButton ID="RBTglProduksi" runat="server" Checked="True" GroupName="a" 
                    Text="Tanggal Produksi" Visible="False" />
                &nbsp;<asp:RadioButton ID="RBTglSerah" runat="server" GroupName="a" Text="Tanggal Serah Oven" 
                    Visible="False" />
            </td>
            <td style="width: 220px" valign="middle">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 126px">
                Dari Tanggal
            </td>
            <td style="width: 158px">
                <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal">
                </cc1:CalendarExtender>
            </td>
            <td style="width: 78px">
                s/d Tanggal
            </td>
            <td style="width: 151px">
                <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px" Width="151px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal">
                </cc1:CalendarExtender>
            </td>
            <td style="width: 220px" valign="middle">
                &nbsp;
                <asp:Label ID="LblDept" runat="server" Text="Departemen"></asp:Label>
                &nbsp;&nbsp;<asp:DropDownList ID="ddlDepartemen" runat="server" Height="16px" 
                    Width="120px">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="2">Board Mill</asp:ListItem>
                    <asp:ListItem Value="3">Finishing</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <%--&nbsp;
                Proses Pengeringan --%>
                <asp:DropDownList ID="ddlDepartemen0" runat="server" 
                    Height="16px" Width="172px" Visible="False">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="1">Oven</asp:ListItem>
                    <asp:ListItem Value="2">Jemur Manual</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 126px">
                Proses Pressing</td>
            <td style="width: 158px">
                <asp:DropDownList ID="ddlPressing" runat="server" 
                    Height="16px" Width="172px">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="YES">Pressing</asp:ListItem>
                    <asp:ListItem Value="NO">Non Pressing</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 78px">
                &nbsp;</td>
            <td style="width: 151px">
                &nbsp;</td>
            <td style="width: 220px" valign="middle">
                &nbsp;</td>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 126px; height: 19px;">
            </td>
            <td style="width: 158px; height: 19px;">
            </td>
            <td style="width: 78px; height: 19px;">
            </td>
            <td style="width: 151px; height: 19px;">
            </td>
            <td colspan="2" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td style="width: 126px">
                &nbsp;
            </td>
            <td style="width: 158px">
                &nbsp;
            </td>
            <td style="width: 78px">
                &nbsp;
            </td>
            <td align="right" style="width: 151px">
                &nbsp;
            </td>
            <td align="center" style="width: 220px">
                <asp:Button ID="BtnPreview" runat="server" Height="20px" Text="Preview" Width="66px"
                    OnClick="BtnPreview_Click" OnClientClick="Cetak();" />
            </td>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
