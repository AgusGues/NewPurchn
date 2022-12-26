<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputBendingStrength.aspx.cs" Inherits="GRCweb1.Modul.Boardmill.InputBendingStrength" %>

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

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>

        <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        INPUT BENDING STRENGTH
                    </div>
                    <div style="padding: 2px"></div>



                    <%--copy source design di sini--%>
                    <div id="Div1" runat="server" class="table-responsive" style="width: 100%">

                        <div class="contentlist" id="lstRkp" runat="server" style="height: 420px">
                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                <thead>
                                    <tr class="tbHeader">
                                        <th class="kotak" colspan="13">ROUTINE TEST REPORT
                                        </th>
                                    </tr>
                                    <tr class="tbHeader">
                                        <th class="kotak" rowspan="2" style="width: 4%">Prod. Date
                                        </th>
                                        <th class="kotak" rowspan="2" style="width: 4%">Formula
                                        </th>
                                        <th class="kotak" colspan="4">Density
                                        </th>
                                        <th class="kotak">Water Content
                                        </th>
                                        <th class="kotak" colspan="2">Water Absorption
                                        </th>
                                        <th class="kotak" colspan="2">LB
                                        </th>
                                        <th class="kotak" colspan="2">LK
                                        </th>
                                    </tr>
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width: 3%">BK
                                        </th>
                                        <th class="kotak" style="width: 3%">t
                                        </th>
                                        <th class="kotak" style="width: 3%">l
                                        </th>
                                        <th class="kotak" style="width: 3%">p
                                        </th>
                                        <th class="kotak" style="width: 3%">BA
                                        </th>
                                        <th class="kotak" style="width: 3%">BB
                                        </th>
                                        <th class="kotak" style="width: 3%">BK
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="EvenRows baris">
                                        <td class="kotak">
                                            <asp:TextBox ID="txtTanggalInput2" runat="server" OnTextChanged="txtTanggalInput2_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTanggalInput2"
                                                Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:DropDownList ID="ddlFormula1" runat="server" AutoPostBack="true" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBK" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtT" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak">
                                            <asp:TextBox ID="txtP" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBA" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak">
                                            <asp:TextBox ID="txtBB" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBK2" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtLBC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtLBL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtLKC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtLKL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                                <tr class="tbHeader">
                                    <th class="kotak" colspan="14" style="width: 4%">
                                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        <input id="btnListRountineTest" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="List" onserverclick="btnListRountineTest_ServerClick" />
                                    </th>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                <thead>
                                    <tr class="tbHeader">
                                        <th class="kotak" colspan="14">PRODUCTION TESTING REPORT
                                        </th>
                                    </tr>
                                    <tr class="tbHeader">
                                        <th class="kotak" rowspan="2" style="width: 4%">Prod. Date
                                        </th>
                                        <th class="kotak" rowspan="2" style="width: 4%">Formula
                                        </th>
                                        <th class="kotak" rowspan="2" style="width: 4%">Group Produksi
                                        </th>
                                        <th class="kotak" rowspan="2" style="width: 4%">Jenis Produksi
                                        </th>
                                        <th class="kotak" colspan="2">Thickness (mm)
                                        </th>
                                        <th class="kotak" colspan="2">Peak Load (kgf)
                                        </th>
                                        <th class="kotak" colspan="2">Peak Elongation (mm)
                                        </th>
                                        <th class="kotak" colspan="2">Bending Strength (kgf/cm)
                                        </th>
                                        <th class="kotak" colspan="2">Area Under Curve
                                        </th>
                                    </tr>
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                        <th class="kotak" style="width: 3%">C
                                        </th>
                                        <th class="kotak" style="width: 3%">L
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="EvenRows baris">
                                        <td class="kotak">
                                            <asp:TextBox ID="txtTanggalInput" runat="server" OnTextChanged="txtTanggalInput_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtTanggalInput" Format="dd-MMM-yyyy"
                                                EnableViewState="true"></cc1:CalendarExtender>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:DropDownList ID="ddlFormula" runat="server" AutoPostBack="true" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="kotak">
                                            <asp:DropDownList ID="ddlGroupProduksi" runat="server" AutoPostBack="true" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="kotak">
                                            <asp:DropDownList ID="ddlJenisProduksi" runat="server" AutoPostBack="true" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtThicknessC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtThicknessL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtPeakLoadC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak">
                                            <asp:TextBox ID="txtPeakloadL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtPeakElongationC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak">
                                            <asp:TextBox ID="txtPeakElongationL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBendingStrengthC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBendingStrengthL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtAreaUncerCurveC" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtAreaUnderCurveL" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                                <tr class="tbHeader">
                                    <th class="kotak" colspan="16" style="width: 4%">
                                        <input id="btnUpdate1" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Simpan" onserverclick="btnUpdate1_ServerClick" />
                                        <input id="btnProductionTest" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="List" onserverclick="btnProductionTest_ServerClick" />
                                    </th>
                                </tr>
                            </table>
                        </div>


                    </div>
                </div>
            </div>
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
