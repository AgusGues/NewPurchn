<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputScrab.aspx.cs" Inherits="GRCweb1.Modul.Boardmill.InputScrab" %>

<%--taroh di setelah 1 baris pertama file--%>
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




        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        INPUT SCRAB
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>
                    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="table-layout: fixed" width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%; height: 49px">
                                        <table class="nbTableHeader">
                                            <tr>
                                                <td style="width: 90%">
                                                    <%--<asp:Label ID="Ljudul" runat="server" Text="Label">INPUT SCRAB</asp:Label>--%>
                                                </td>

                                                <td style="width: 75px">
                                                    <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                                </td>

                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <div style="width: 100%;">
                                            <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                                <tr>
                                                    <td style="width: 170px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Tanggal</span>&nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtTanggalInput" runat="server"
                                                            OnTextChanged="txtTanggalInput_TextChanged"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtTanggalInput" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="width: 205px; height: 3px" valign="top">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 170px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Type Scrab</span>&nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"
                                                            TabIndex="11" Width="233">
                                                            <asp:ListItem Value="0">--Pilih--</asp:ListItem>
                                                            <asp:ListItem Value="1">SCRAB KERING</asp:ListItem>
                                                            <asp:ListItem Value="2">SCRAB BASAH</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 205px; height: 3px" valign="top">&nbsp;
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 197px;" valign="top">
                                                        <span style="font-size: 10pt">&nbsp; Pilih Inputan</span>
                                                    </td>
                                                    <td style="width: 204px;">
                                                        <asp:RadioButton ID="rdPalet" runat="server" Checked="True" Font-Size="X-Small" GroupName="g1"
                                                            Text="Palet" AutoPostBack="True"
                                                            OnCheckedChanged="rdPalet_CheckedChanged" />
                                                        &nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdBin" runat="server" Font-Size="X-Small" GroupName="g1"
                                                    Text="BIN" AutoPostBack="True" OnCheckedChanged="rdBin_CheckedChanged" />
                                                    </td>
                                                    <td style="width: 205px;" valign="top"></td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 170px;">
                                                        <span style="font-size: 10pt">&nbsp;Jumlah</span>
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:TextBox ID="txtJumlah" runat="server" BorderStyle="Groove" TabIndex="3" Width="233" AutoPostBack="True" OnTextChanged="txtJumlah_TextChanged"></asp:TextBox>
                                                    </td>

                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 170px;">
                                                        <span style="font-size: 10pt">&nbsp; Kg</span>
                                                    </td>
                                                    <td style="width: 204px;" valign="top">
                                                        <asp:TextBox ID="txtKg" runat="server" TabIndex="3" Width="233"></asp:TextBox>
                                                    </td>

                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 169px;">
                                                        <span style="font-size: 10pt">&nbsp; M3</span>
                                                    </td>

                                                    <td style="width: 209px;">
                                                        <asp:TextBox ID="txtM3" runat="server" BorderStyle="Groove" Width="233" TabIndex="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 169px;">
                                                        <span style="font-size: 10pt">&nbsp; Keterangan</span>
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" Width="233" TabIndex="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                </tr>

                                <tr>
                                    <td style="width: 170px;">
                                        <asp:Label ID="lblErrorName" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                                    </td>
                                    <td style="" colspan="3"></td>
                                    <td style="width: 205px;">&nbsp;
                                    </td>
                                </tr>
                        </table>
                        <span style="font-size: 10pt">&nbsp; <strong>List</strong></span>
                        <div style="border: 2px solid #B0C4DE; height: 200px; width: 100%; padding: 10px; background-color: White">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                HorizontalAlign="Center" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                TabIndex="12" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="Tanggal" HeaderText="Tanggal">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JenisScrab" HeaderText="Type Scrab" />
                                    <asp:BoundField DataField="SatuanBerat" HeaderText="Satuan" />
                                    <asp:BoundField DataField="Jumlah" HeaderText="Jumlah">
                                        <ItemStyle Width="20px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Kg" HeaderText="Berat(Kg)" />
                                    <asp:BoundField DataField="M3" HeaderText="M3">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>

                                </Columns>
                                <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />
                            </asp:GridView>
                        </div>
                    </div>
                    </td>
                        </tr>
                    </tbody>
                </table>
                </div>



                <script src="../../assets/jquery.js" type="text/javascript"></script>
                <script src="../../assets/js/jquery-ui.min.js"></script>
                <script src="../../assets/select2.js"></script>
                <script src="../../assets/datatable.js"></script>
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>
</asp:Content>
