<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListMaster.aspx.cs" Inherits="GRCweb1.Modul.ISO.ListMaster" %>

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
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>

    </head>
    <body class="no-skin">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                LIST PES (KPI / SOP)
                            </div>
                            <div style="padding: 2px"></div>

                            <table style="width: 100%; border-collapse: collapse; font-size: x-small">

                                <tr valign="top">
                                    <td style="width: 100%">
                                        <div class="content">
                                            <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                                <tr>
                                                    <td style="width: 10%">&nbsp;</td>
                                                    <td style="width: 15%">Departement</td>
                                                    <td style="width: 30%">
                                                        <asp:DropDownList AutoPostBack="True" CssClass="form-control input-sm" ID="ddlDept" runat="server"
                                                            OnSelectedIndexChanged="ddlDept_SelectedChange"
                                                            meta:resourcekey="ddlDeptResource1">
                                                        </asp:DropDownList></td>
                                                    <td style="width: 40%">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td></td>
                                                    <td>PIC</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPIC" runat="server" CssClass="form-control input-sm" OnTextChanged="ddlPIC_Change"
                                                            AutoPostBack="True" meta:resourcekey="ddlPICResource1">
                                                        </asp:DropDownList></td>
                                                    <td>&nbsp</td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-sm" Text="Preview"
                                                            OnClick="btnPreview_Click" meta:resourcekey="btnPreviewResource1" />
                                                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary btn-sm" Text="Export to Excel"
                                                            OnClick="btnExport_Click" meta:resourcekey="btnExportResource1" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <hr />
                                            <div class="contentlist" style="height: 410px">
                                                <div id="lst" runat="server">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; display: block" border="0">
                                                        <thead>
                                                            <tr class=" tbHeader baris">
                                                                <th class="kotak" rowspan="2" style="width: 4%">No.</th>
                                                                <th class="kotak" rowspan="2" style="width: 40%">Description</th>
                                                                <th class="kotak" rowspan="2" style="width: 12%">Target</th>
                                                                <th class="kotak" rowspan="2" style="width: 5%">Bobot</th>
                                                                <th class="kotak" rowspan="2" style="width: 12%">Program Checking</th>
                                                                <th class="kotak" colspan="2">Pencapaian</th>
                                                            </tr>
                                                            <tr class="tbHeader baris">
                                                                <th class="kotak" style="width: 10%">Target</th>
                                                                <th class="kotak" style="width: 5%">Score</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="OddRows baris">
                                                                        <td class="kotak" colspan="7">&nbsp;<b><%#Container.ItemIndex+1 %>.&nbsp;<%# Eval("DeptName") %></b></td>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstPIC" runat="server" OnItemDataBound="lstPIC_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris">
                                                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                                <td class="kotak" colspan="6"><b><%#Eval("NIK") %> -&nbsp;<%# Eval("PIC") %>&nbsp;[&nbsp;<%# Eval("BagianName") %>&nbsp;]</b></td>
                                                                            </tr>
                                                                            <asp:Repeater ID="lstCat" runat="server" OnItemDataBound="lstCat_DataBound">
                                                                                <ItemTemplate>
                                                                                    <tr class="total">
                                                                                        <td class="kotak angka"><%# Container.ItemIndex+1 %>&nbsp;</td>
                                                                                        <td class="kotak" colspan="6"><%# Eval("PESName") %></td>
                                                                                    </tr>
                                                                                    <asp:Repeater ID="lstPES" runat="server" OnItemDataBound="lstPES_DataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr class="EvenRows baris" valign="top">
                                                                                                <td class="kotak tengah">&nbsp;</td>
                                                                                                <td class="kotak"><%# Container.ItemIndex+1 %>.&nbsp;<%#Eval("Desk") %></td>
                                                                                                <td class="kotak tengah"><%# Eval("Target") %></td>
                                                                                                <td class="kotak tengah"><%# Eval("BobotNilai","{0:N0}") %>%</td>
                                                                                                <td class="kotak"><%# Eval("Checking") %></td>
                                                                                                <td class="kotak tengah" colspan="2">
                                                                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                                                                        <asp:Repeater ID="lstScr" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <tr class="EvenRows baris">
                                                                                                                    <td class="kotak tengah" style="width: 75%"><%# Eval("Pencapaian") %><asp:Label
                                                                                                                        ID="xx" runat="server" ForeColor="White" Text=" "
                                                                                                                        meta:resourcekey="xxResource1"></asp:Label></td>
                                                                                                                    <td class="kotak tengah" style="width: 25%"><%# Eval("Score") %></td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </ItemTemplate>

                                                                                    </asp:Repeater>
                                                                                    <tr class="total">
                                                                                        <td class="kotak angka" colspan="3">Total Bobot&nbsp; <%# Eval("PESName") %></td>
                                                                                        <td class="kotak">
                                                                                            <asp:Label ID="txtTotal" runat="server" CssClass="txtongrid"
                                                                                                Width="100%" Height="100%"></asp:Label></td>
                                                                                        <td class="kotak" colspan="3"></td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
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
