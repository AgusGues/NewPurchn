<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantuanAprovalPO.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantuanAprovalPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        label {
            font-size: 12px;
        }
    </style>
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
        <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
        <script src="../../Scripts/json2.js" type="text/javascript"></script>
        <script type="text/javascript">

            $(document).ready(function () {
                //$(container).click(function(e) { getClickPosition(e); });
            });
            function showItemInfo() {
                $('#lstrs tbody tr').click(function (e) {
                    var topx = ($(this).parent().offset().top) + 10;
                    var lefty = ($(this).parent().offset().left);
                    var posTop = e.pageY - topx;
                    $('#jdls').html($(this).find('td').eq(3).html());
                    _getData($(this).find('td').eq(3).html());
                    $('#itemInfo')
                        .css({ "top": (posTop + $('#itemInfo').height()), "left": "50px" })
                        .show();
                });
            }
            function closeDiv() { $('#itemInfo').hide(); }
            function _getData(nopo) {
                $.ajax({
                    type: 'POST',
                    url: "LapPemantuanAprovalPO.aspx/GetItemByNoPO",
                    data: JSON.stringify({ "NoPO": nopo }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // dataType is json format
                    success: function (datax) {
                        $('#tblData tbody').html(datax.d)
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest[0])
                    }
                });
            }
        </script>

    </head>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <body class="no-skin">

                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                PEMANTAUAN APPROVAL PO
                            </div>
                            <div style="padding: 2px"></div>

                            <%--copy source design di sini--%>
                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="width: 100%; height: 100%; border-collapse: collapse">

                                    <tr>
                                        <td valign="top" style="width: 100%">
                                            <div class="" style="width: 100%">
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                    <tr>
                                                        <td style="width: 10%">&nbsp;</td>
                                                        <td style="width: 15%">Periode</td>
                                                        <td style="width: 30%">
                                                            <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="ddlTahuan" runat="server"></asp:DropDownList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td valign="top">Material Group</td>
                                                        <td>
                                                            <asp:CheckBoxList ID="ddlGroupID" runat="server" RepeatColumns="4" Width="100%"
                                                                RepeatDirection="Horizontal" BackColor="White" Font-Size="X-Small" BorderColor="Silver" CssClass="cursor"
                                                                OnDataBound="ddlGroup_DataBound">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                        <td>
                                                            <asp:RadioButton ID="rbtStock" Text="Stock" GroupName="Type" runat="server" />
                                                            &nbsp;
                                        <asp:RadioButton ID="rbtNonStock" Text="Non Stock" GroupName="Type" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnPreview" runat="server" Text="Preview By NO PO" OnClick="btnPreview_Click" />
                                                            <asp:Button ID="btnPreviewByTglPO" runat="server" Text="Preview By Tgl PO" OnClick="btnPreviewByTglPO_Click" />
                                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                                            <asp:CheckBox ID="chkDetail" runat="server" Text="Export With Detail Item PO" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr />


                                                <div class="contentlist" style="height: 360px">
                                                    <div id="itemInfo" style="display: none; background-color: ButtonFace; position: absolute; width: 90%; border: 3px solid; border-color: InactiveBorder; z-index: 999">
                                                        <table style="width: 100%; border-collapse: collapse;" class="nbTableHeader">
                                                            <tr valign="middle" style="height: 30px">
                                                                <td style="width: 90%; padding-left: 5px">
                                                                    <img src="../../images/clipboard_16.png" />&nbsp; <b>Item List No. PO : <span id="jdls"></span></b>
                                                                </td>
                                                                <td style="width: 10%;" class="angka">
                                                                    <img src="../../images/Delete.png" title="Close" style="cursor: pointer" onclick="closeDiv();" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="contentlist" style="overflow: auto; max-height: 250px">
                                                                        <table class="tbStandart" id="tblData">
                                                                            <thead>
                                                                                <tr class="tbHeader">
                                                                                    <th style="width: 5%" class="kotak">No.</th>
                                                                                    <th style="width: 10%" class="kotak">ItemCode</th>
                                                                                    <th style="width: 20%" class="kotak">ItemName</th>
                                                                                    <th style="width: 8%" class="kotak">Qty</th>
                                                                                    <th style="width: 8%" class="kotak">Del Date</th>
                                                                                    <th style="width: 20%" class="kotak">Suppler Name</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody></tbody>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                    <div id="lstNewP" style="display: block;" runat="server">
                                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0" id="Table1">
                                                            <thead>
                                                                <tr class="tbHeader">
                                                                    <th rowspan="2" style="width: 4%" class="kotak">No.</th>
                                                                    <th colspan="4" style="width: 20%" class="kotak">SPP</th>
                                                                    <th rowspan="2" style="width: 8%" class="kotak">No PO</th>
                                                                    <th colspan="2" style="width: 8%" class="kotak">INPUT PO</th>
                                                                    <th colspan="2" style="width: 10%" class="kotak">ON TIME PO</th>
                                                                    <th colspan="2" style="width: 10%" class="kotak">APPROVAL PO(HEAD)</th>
                                                                    <th colspan="2" style="width: 10%" class="kotak">ON TIME APV</th>
                                                                    <th colspan="2" style="width: 10%" class="kotak">APPROVAL PO(MGR)</th>
                                                                </tr>
                                                                <tr class="tbHeader">
                                                                    <th style="width: 7%" class="kotak">No.</th>
                                                                    <th style="width: 5%" class="kotak">ItemCode</th>
                                                                    <th style="width: 20%" class="kotak">ItemName</th>
                                                                    <th style="width: 6%" class="kotak">Mgr. Apv</th>
                                                                    <th class="kotak" style="width: 6%">Tanggal</th>
                                                                    <th class="kotak" style="width: 4%">Jam</th>
                                                                    <th class="kotak" style="width: 4%">OK</th>
                                                                    <th class="kotak" style="width: 4%">TIDAK</th>
                                                                    <th class="kotak" style="width: 6%">Tanggal</th>
                                                                    <th class="kotak" style="width: 4%">Jam</th>
                                                                    <th class="kotak" style="width: 4%">OK</th>
                                                                    <th class="kotak" style="width: 4%">TIDAK</th>
                                                                    <th class="kotak" style="width: 6%">Tanggal</th>
                                                                    <th class="kotak" style="width: 4%">Jam</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="lstNew" runat="server" OnItemDataBound="lstNew_DataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris" title="Click on Kolom PO fro detail Items PO">
                                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                            <td class="kotak tengah"><%# Eval("NOSPP") %></td>
                                                                            <td class="kotak tengah xx"><%# Eval("ItemCode")%></td>
                                                                            <td class="kotak "><%# Eval("ItemName")%></td>
                                                                            <td class="kotak tengah"><%# Eval("AlasanBatal") %></td>
                                                                            <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                                            <td class="kotak tengah"><%# Eval("POPurchnDate", "{0:d}")%></td>
                                                                            <td class="kotak tengah"><%# Eval("Jam")%></td>
                                                                            <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                                                            <td class="kotak tengah"><%# Eval("AlasanClose") %></td>
                                                                            <td class="kotak tengah"><%# Eval("ApproveDate1S")%></td>
                                                                            <td class="kotak tengah"><%# Eval("JamAppv1")%></td>
                                                                            <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("UP")%></td>
                                                                            <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("NoPol")%></td>
                                                                            <td class="kotak tengah"><%# Eval("ApproveDate2S")%></td>
                                                                            <td class="kotak tengah"><%# Eval("JamAppv2")%></td>
                                                                        </tr>

                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr class="OddRows baris" title="Click on Kolom PO fro detail Items PO">
                                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                                            <td class="kotak tengah"><%# Eval("NOSPP") %></td>
                                                                            <td class="kotak tengah xx"><%# Eval("ItemCode")%></td>
                                                                            <td class="kotak "><%# Eval("ItemName")%></td>
                                                                            <td class="kotak tengah"><%# Eval("AlasanBatal") %></td>
                                                                            <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                                            <td class="kotak tengah"><%# Eval("POPurchnDate", "{0:d}")%></td>
                                                                            <td class="kotak tengah"><%# Eval("Jam")%></td>
                                                                            <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                                                            <td class="kotak tengah"><%# Eval("AlasanClose") %></td>
                                                                            <td class="kotak tengah"><%# Eval("ApproveDate1S")%></td>
                                                                            <td class="kotak tengah"><%# Eval("JamAppv1")%></td>
                                                                            <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("UP")%></td>
                                                                            <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("NoPol")%></td>
                                                                            <td class="kotak tengah"><%# Eval("ApproveDate2S")%></td>
                                                                            <td class="kotak tengah"><%# Eval("JamAppv2")%></td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        <tr class="Line3">
                                                                            <td class="kotak angka" colspan="6">Total SPP</td>
                                                                            <td class="kotak angka" colspan="2">
                                                                                <asp:Label ID="ttSPP" runat="server"></asp:Label></td>
                                                                            <td class="kotak angka">
                                                                                <asp:Label ID="ttOkP" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttNoOKP" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttApOKP" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttAPNoOKP" runat="server"></asp:Label></td>
                                                                            <td class="kotak angka" colspan="2">&nbsp;</td>
                                                                        </tr>
                                                                        <tr class="">
                                                                            <td class="kotak angka" colspan="6"></td>
                                                                            <td class="kotak Line3 " colspan="2">Pencapaian (%)</td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttOK" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttNoOK" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttApOK" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka">
                                                                                <asp:Label ID="ttApNoOK" runat="server"></asp:Label></td>
                                                                            <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                                                        </tr>
                                                                    </FooterTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--source html ditutup di sini--%>
</asp:Content>
