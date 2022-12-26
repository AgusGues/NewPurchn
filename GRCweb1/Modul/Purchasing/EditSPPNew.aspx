<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditSPPNew.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.EditSPPNew" %>
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


        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                EDIT SPP
                            </div>
                            <div style="padding: 2px"></div>



                            <div id="Div1" runat="server">
                                <table style="table-layout: fixed" width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 100%; height: 49px">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%"></td>

                                                        <td>
                                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                                <asp:ListItem Value="NoSPP">No SPP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 3px">
                                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Cari"
                                                                onserverclick="btnSearch_ServerClick" />
                                                        </td>
                                                        <td style="width: 3px">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>

                            <tr>
                                <td style="width: 100%;">
                                    <div style="width: 100%; background-color: #B0C4DE">

                                        <div id="div2" style="width: 100%; height: 450px; overflow: auto; padding: 10px; background-color: White; border: 2px solid #B0C4DE">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                                                OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                    <asp:BoundField DataField="NoSPP" HeaderText="No SPP" />
                                                    <asp:BoundField DataField="Minta" HeaderText="Tanggal" />
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Kode Brg" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Nama Brg" />
                                                    <asp:BoundField DataField="UomCode" HeaderText="Satuan" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                    <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                </Columns>
                                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                <PagerStyle BorderStyle="Solid" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </td>
                            </tr>

                        </div>

                        <script src="../../assets/jquery.js" type="text/javascript"></script>
                        <script src="../../assets/js/jquery-ui.min.js"></script>
                        <script src="../../assets/select2.js"></script>
                        <script src="../../assets/datatable.js"></script>
                        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
                </body>
    </html>

    
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
