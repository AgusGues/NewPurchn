<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KertasKadarAirList4PO.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KertasKadarAirList4PO" %>

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
        <script type="text/javascript">
            // fix for deprecated method in Chrome / js untuk bantu view modal dialog
            if (!window.showModalDialog) {
                window.showModalDialog = function (arg1, arg2, arg3) {
                    var w;
                    var h;
                    var resizable = "no";
                    var scroll = "no";
                    var status = "no";
                    // get the modal specs
                    var mdattrs = arg3.split(";");
                    for (i = 0; i < mdattrs.length; i++) {
                        var mdattr = mdattrs[i].split(":");
                        var n = mdattr[0];
                        var v = mdattr[1];
                        if (n) { n = n.trim().toLowerCase(); }
                        if (v) { v = v.trim().toLowerCase(); }
                        if (n == "dialogheight") {
                            h = v.replace("px", "");
                        } else if (n == "dialogwidth") {
                            w = v.replace("px", "");
                        } else if (n == "resizable") {
                            resizable = v;
                        } else if (n == "scroll") {
                            scroll = v;
                        } else if (n == "status") {
                            status = v;
                        }
                    }
                    var left = window.screenX + (window.outerWidth / 2) - (w / 2);
                    var top = window.screenY + (window.outerHeight / 2) - (h / 2);
                    var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                    targetWin.focus();
                };
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                maintainScrollPosition();
            });
            function pageLoad() {
                maintainScrollPosition();
            }
            function maintainScrollPosition() {
                $("#<%=lst.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function CreatePO(docno) {
            params = 'dialogWidth=' + document.body.clientWidth + 'px';
            params += ', dialogHeight=' + (document.body.clientHeight) + 'px';
            params += ', center=yes,status=no';
            params += ',scroll=no,resizable=no';
            //alert(params);
            window.showModalDialog("../../ModalDialog/POKertas0.aspx?d=" + docno, "Preview", params);
        }
        </script>


    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                LIST KADAR AIR KERTAS CREATE PO
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

                            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    <%--<tr>
                        <td style="width:100%; height:49px;">
                            <table class="nbTableHeader" style="width:100%; border-collapse:collapse;" >
                                <tr>
                                    <td style="width:40%; padding-left:10px;">
                                        <b>LIST KADAR AIR KERTAS CREATE PO</b>
                                    </td>
                                <td style="width:60%; padding-right:10px;"><asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                                    <tr>
                                        <td>
                                            <div>
                                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                                    <tr>
                                                        <td class="left" style="width: 5%">&nbsp;</td>
                                                        <td class="left" style="width: 25%">Filter : &nbsp;
                                    <asp:RadioButtonList ID="rFilter" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rFilter_Change">
                                        <asp:ListItem Value="0" Selected="True">Status Open</asp:ListItem>
                                        <asp:ListItem Value="1" Enabled="false">Status Close</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                                <p></p>
                                                <hr />
                                                <div class="contentlist" style="height: 420px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                        <thead>
                                                            <tr class="tbHeader">
                                                                <th class="kotak" style="width: 4%">No</th>
                                                                <th class="kotak" style="width: 8%">Tanggal</th>
                                                                <th class="kotak" style="width: 12%">Supplier Name</th>
                                                                <th class="kotak" style="width: 8%">No.SJ</th>
                                                                <th class="kotak" style="width: 8%">No. Mobil</th>
                                                                <th class="kotak" style="width: 15%">ItemName</th>
                                                                <th class="kotak" style="width: 5%">BK</th>
                                                                <th class="kotak" style="width: 5%">KA(%)</th>
                                                                <th class="kotak" style="width: 4%">Sampah(%)</th>
                                                                <th class="kotak" style="width: 5%">BB</th>
                                                                <th class="kotak" style="width: 7%">NoSPP</th>
                                                                <th class="kotak" style="width: 7%">NoPO</th>
                                                                <th class="kotak" style="width: 4%">&nbsp</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="lstKA" runat="server" OnItemCommand="lstKA_ItemCommand" OnItemDataBound="lstKA_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" id="listed" runat="server">
                                                                        <td class="kotak tengah">
                                                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td class="kotak tengah"><%# Eval("TglKirim","{0:d}") %></td>
                                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("SupplierName").ToString().ToUpper() %></td>
                                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("NoSJ")%></td>
                                                                        <td class="kotak"><%# Eval("NOPOL") %></td>
                                                                        <td class="kotak" style="white-space: nowrap"><%# Eval("ItemName") %></td>
                                                                        <td class="kotak angka"><%# Eval("GrossPlant","{0:N0}") %></td>
                                                                        <td class="kotak angka"><%# Eval("KADepo","{0:N2}") %></td>
                                                                        <td class="kotak angka"><%# Eval("Sampah","{0:N2}") %></td>
                                                                        <td class="kotak angka"><%# Eval("NettPlant", "{0:N0}")%></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="txtNoSPP" runat="server" CssClass="txtongrid" Width="80%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah"></td>
                                                                        <td class="kotak tengah">
                                                                            <asp:ImageButton ID="btnPO" runat="server" CommandArgument='<%# Eval("ID") %>' ToolTip="Click For Create PO" CommandName="po" ImageUrl="~/images/clipboard_16.png" />
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




                        </div>

                        <script src="../../assets/jquery.js" type="text/javascript"></script>
                        <script src="../../assets/js/jquery-ui.min.js"></script>
                        <script src="../../assets/select2.js"></script>
                        <script src="../../assets/datatable.js"></script>
                        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
                        </body>
    </html>

    <%--source html ditutup di sini--%>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
