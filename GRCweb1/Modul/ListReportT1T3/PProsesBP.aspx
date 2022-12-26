<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PProsesBP.aspx.cs" Inherits="GRCweb1.Modul.ISO.PProsesBP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            // Get the instance of PageRequestManager.
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            // Add initializeRequest and endRequest
            prm.add_initializeRequest(prm_InitializeRequest);
            prm.add_endRequest(prm_EndRequest);

            // Called when async postback begins
            function prm_InitializeRequest(sender, args) {
                // get the divImage and set it to visible
                var panelProg = $get('divImage');
                panelProg.style.display = '';
                // Disable button that caused a postback
                $get(args._postBackElement.id).disabled = false;
            }

            // Called when async postback ends
            function prm_EndRequest(sender, args) {
                // get the divImage and hide it again
                var panelProg = $get('divImage');
                panelProg.style.display = 'none';
            }
        </script>

        <script language="javascript" type="text/javascript">
            function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                var tbl = document.getElementById(gridId);
                if (tbl) {
                    var DivHR = document.getElementById('DivHeaderRow');
                    var DivMC = document.getElementById('DivMainContent');
                    var DivFR = document.getElementById('DivFooterRow');

                    //*** Set divheaderRow Properties ****
                    DivHR.style.height = headerHeight + 'px';
                    DivHR.style.width = (parseInt(width - 1)) + '%';
                    DivHR.style.position = 'relative';
                    DivHR.style.top = '0px';
                    DivHR.style.zIndex = '2';
                    DivHR.style.verticalAlign = 'top';

                    //*** Set divMainContent Properties ****
                    DivMC.style.width = width + '%';
                    DivMC.style.height = height + 'px';
                    DivMC.style.position = 'relative';
                    DivMC.style.top = -headerHeight + 'px';
                    DivMC.style.zIndex = '0';

                    //*** Set divFooterRow Properties ****
                    DivFR.style.width = (parseInt(width)) + 'px';
                    DivFR.style.position = 'relative';
                    DivFR.style.top = -headerHeight + 'px';
                    DivFR.style.verticalAlign = 'top';
                    DivFR.style.paddingtop = '2px';

                    if (isFooter) {
                        var tblfr = tbl.cloneNode(true);
                        tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                        var tblBody = document.createElement('tbody');
                        tblfr.style.width = '100%';
                        tblfr.cellSpacing = "0";
                        tblfr.border = "0px";
                        tblfr.rules = "none";
                        //*****In the case of Footer Row *******
                        tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                        tblfr.appendChild(tblBody);
                        DivFR.appendChild(tblfr);
                    }
                    //****Copy Header in divHeaderRow****
                    DivHR.appendChild(tbl.cloneNode(true));
                }
            }

            function OnScrollDiv(Scrollablediv) {
                document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
            }


            function btnHitung_onclick() {

            }

        </script>
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
                    var left = window.screenX + (window.outerWidth) - (w / 2);
                    var top = window.screenY + (window.outerHeight) - (h / 2);
                    var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                    targetWin.focus();
                };
            }
</script>

    </head>

    <body class="no-skin">

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Pemantauan Proses Produk BP
                    </div>
                    <div style="padding: 2px"></div>
                    <link href="../../Scripts/text.css" rel="stylesheet" type="text/css" />
                    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <tr>
                                <td>
                                    <div class="">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                            <tr>
                                                <td style="width: 10%; padding-left: 10px;">Periode
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:DropDownList ID="ddlTahun" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click"
                                                        Text="Preview" />
                                                    &nbsp;<asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                                        Text="Export To Excel" />
                                                </td>
                                            </tr>

                                        </table>
                                        <hr />
                                        <div style="height: 100%; width: 100%;" id="lstr" runat="server">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <table id="thr" style="border-collapse: collapse; font-size: x-small; font-family: Arial; width: 100%; height: 100%;">
                                                        <thead>
                                                            <tr class="tbHeader" id="hd1" runat="server">
                                                                <th class="kotak tengah" rowspan="3">Bulan</th>
                                                                <th class="kotak tengah" rowspan="3">Saldo Awal
                                                                </th>
                                                                <th class="kotak tengah" rowspan="2" colspan="2">BP-IN (Lbr)
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3">BP
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3">Adj(in)
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3">Total BP-IN (Lbr)
                                                                </th>
                                                                <th class="kotak tengah" colspan="20">Total BP-OUT (Jml Lbr Potong)
                                                                </th>
                                                                 <th class="kotak tengah" rowspan="3">Adj(out)
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3">Total BP-OUT (Lbr)
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3">Saldo Akhir
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader" id="hd2" runat="server">
                                                                <th class="kotak tengah" colspan="4">1200 x 2400
                                                                </th>
                                                                <th class="kotak tengah" colspan="4">1000 x 1000
                                                                </th>
                                                                <th class="kotak tengah" colspan="4">1000 x 2000
                                                                </th>
                                                                <th class="kotak tengah" colspan="4">Belahan (600x2400)
                                                                </th>
                                                                <th class="kotak tengah" colspan="4">Sortir
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader" id="hd3" runat="server">
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP Oven
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">NC (OK)
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">OK
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BS
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">Jumlah
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">OK
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BS
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">Jumlah
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">OK
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BS
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">Jumlah
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">OK
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BS
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">Jumlah
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">OK
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BP
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">BS
                                                                </th>
                                                                <th class="kotak tengah" style="font-size: xx-small;">Jumlah
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="total baris" id="dpt" runat="server">
                                                                        <td class="kotak">UKURAN : &nbsp;
                                                                        </td>
                                                                        <td class="kotak" colspan="30">
                                                                            <b>
                                                                                <%# Eval("ukuran") %></b>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstPES" runat="server" >
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" id="ps1" runat="server">
                                                                                <td class="kotak kiri bold OddRows">
                                                                                    <%# Eval("bln1")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("sa") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyin1")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyin2") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyin3")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyin4") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyin5")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout6") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout7")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout8") %>
                                                                                </td>
                                                                                
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout10")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout11") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout12")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout13") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout15")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout16") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout17")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout18") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout20")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout21") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout22")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout23") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout25")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout26") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout27")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout28") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("qtyout30")%>
                                                                                </td>
                                                                                <td class="kotak kanan">
                                                                                    <%# Eval("qtyout31") %>
                                                                                </td>
                                                                                 <td class="kotak kanan">
                                                                                    <%# Eval("qtyout32") %>
                                                                                </td>
                                                                                <td class="kotak kanan OddRows">
                                                                                    <%# Eval("saldo")%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div id="DivFooterRow" style="overflow: hidden">
                                                </div>
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
</asp:Content>
