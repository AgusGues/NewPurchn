<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPes.aspx.cs" Inherits="GRCweb1.Modul.ISO.EditPes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>
       

        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
        .panelbox {background-color: #efeded;padding: 2px;}
        html,body,.form-control,button{font-size: 11px;}
        .input-group-addon{background: white;}
        .fz11{font-size: 11px;}
        .fz10{font-size: 10px;}
        .the-loader{
            position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
            text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
        }
        .input-xs{
            font-size: 11px;height: 11px;
        }
    </style>
        
    </head>
        <body class="no-skin">
            <body class="no-skin">

           

   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">    
    <ContentTemplate>  

            <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        OPEN INPUTAN KPI atau SOP 
                    </div>
                    <div style="padding: 2px"></div>




            <div id="div1" class="table-responsive" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <%--<tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <span style="font-family: 'Courier New', Courier, monospace; font-size: medium; color: #0000CC">
                                        <b>&nbsp;OPEN INPUTAN KPI atau SOP</b></span>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:HiddenField ID="appLevele" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <div>
                                <hr />
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                    <tr style="width: 100%">
                                        <td style="width: 20%; height: 6px; font-size: x-small; font-family: Calibri;" valign="middle">
                                            <b>&nbsp; INPUTAN  </b>
                                        </td>
                                        <td style="width: 20%">: &nbsp;
                                            <span style="font-family: Calibri">
                                                <asp:DropDownList ID="ddlPES" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPES_Change"
                                                    Style="font-family: Calibri; font-size: x-small" Width="100px">
                                                    <asp:ListItem Value="0">-- Pilih --</asp:ListItem>
                                                    <asp:ListItem Value="1">KPI</asp:ListItem>
                                                    <asp:ListItem Value="3">SOP</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 20%; font-family: Calibri;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 20%; height: 6px; font-size: x-small; font-family: Calibri;" valign="middle">
                                            <b>&nbsp; DEPARTMENT  </b>
                                        </td>
                                        <td style="width: 20%">: &nbsp;
                                            <span style="font-family: Calibri">
                                                <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_Change"
                                                    Style="font-family: Calibri; font-size: x-small" Width="220px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 20%; font-family: Calibri;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 20%; font-size: x-small; font-family: Calibri;" valign="middle">
                                            <b>&nbsp; NAMA PIC </b>
                                        </td>
                                        <td style="width: 20%">: &nbsp;
                                            <span style="font-family: Calibri">
                                                <asp:DropDownList ID="ddlPIC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPIC_Change"
                                                    Style="font-family: Calibri; font-size: x-small" Width="220px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                        <td style="width: 20%; font-family: Calibri;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 20%; height: 6px; font-size: x-small; font-family: Calibri;" valign="middle">
                                            <b>&nbsp; PERIODE INPUTAN</b>
                                        </td>
                                        <td style="width: 50%; font-family: Calibri;" rowspan="3">: &nbsp;
                                            <span style="font-family: Calibri">
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTahun_Change"
                                                    Style="font-family: Calibri; font-size: x-small" Width="100px">
                                                </asp:DropDownList>
                                            </span><span style="font-family: Calibri">
                                                <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_Change"
                                                    Style="font-family: Calibri; font-size: x-small; margin-left: 0px;" 
                                                Width="150px">
                                                </asp:DropDownList>
                                            </span><asp:Button ID="btncancel" runat="server" Text="Cancel Inputan" OnClick="btncancel_Click" 
                                        style="font-family: Calibri; font-size: x-small;" Width="90px" 
                                                BorderStyle="None" />
                                        </td>
                                        <%--<td style="width: 20%">
                                            &nbsp;
                                        </td>--%>
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 450px" id="lst" runat="server">
                                    <table style="width: 99%; border-collapse: collapse; font-size: x-small" border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th style="width: 5%; font-family: Calibri;" class="kotak">
                                                    No
                                                </th>
                                                <th style="width: 55%; font-family: Calibri;" class="kotak">
                                                    Item
                                                </th>
                                                <th style="width: 5%; font-family: Calibri;" class="kotak">
                                                    Bobot
                                                </th>
                                                <th style="width: 10%; font-family: Calibri;" class="kotak">
                                                    Pencapaian
                                                </th>
                                                <th style="width: 10%; font-family: Calibri;" class="kotak">
                                                    Nilai
                                                </th>
                                                <th style="width: 10%; font-family: Calibri;" class="kotak">
                                                    Approval
                                                </th>
                                                <th style="width: 5%" class="kotak">                                                    
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody style="font-family: Calibri">
                                            <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="kotak tengah">
                                                            <span class="tengah" style="width: 40%">
                                                                <%# Eval("No") %></span>
                                                        </td>
                                                        <td class="kotak">
                                                            &nbsp;<%# Eval("Description")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("Bobot")%>%
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("TargetPencapaian")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("PointNilai")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("StatusApproval")%>
                                                        </td>
                                                        <%--<td class="kotak tengah">
                                                            <%# Eval("TglBerlaku","{0:dd-MM-yyyy}")%>
                                                        </td>--%>
                                                        <td class="kotak tengah" style="padding-right: 1px">
                                                            <asp:ImageButton ToolTip="Lock" ID="att" runat="server" Visible="false" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/GemBok1.jpg" />
                                                                
                                                                <asp:ImageButton ToolTip="UnLock" ID="att2" runat="server" Visible="false" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="attach2" ImageUrl="~/TreeIcons/Icons/GemBok2.jpg" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>                                       
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


            <script src="../../assets/jquery.js" type="text/javascript"></script>
            <script src="../../assets/js/jquery-ui.min.js"></script>
            <script src="../../assets/select2.js"></script>
            <script src="../../assets/datatable.js"></script>
            <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>

</asp:Content>
