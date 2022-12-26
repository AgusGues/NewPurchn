<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPESNew3.aspx.cs" Inherits="GRCweb1.Modul.ISO.RekapPESNew3" %>

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
                        REKAP PES
                    </div>
                    <div style="padding: 2px"></div>



                    <%--copy source design di sini--%>

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
                                                    <asp:DropDownList ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_Change"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                                        <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_Change"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px">Department
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_Change"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click"
                                                        Text="Preview" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px">PIC Name
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPIC" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                                        Text="Export To Excel" />
                                                </td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <div style="height: 100%" id="lstr" runat="server">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <table id="thr" style="border-collapse: collapse; font-size: x-small; font-family: Arial">
                                                        <thead>
                                                            <tr class="tbHeader" id="hd1" runat="server">
                                                                <th class="kotak" rowspan="3" style="width: 200px"></th>
                                                                <th class="kotak" rowspan="3" style="width: 50px">Bobot
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Jan
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Feb
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Mar
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Apr
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Mei
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Jun
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Jul
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Ags
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Sep
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Okt
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Nov
                                                                </th>
                                                                <th class="kotak tengah" colspan="2">Des
                                                                </th>
                                                                <th class="kotak tengah" rowspan="3" style="width: 60px">Total
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader" id="hd2" runat="server">
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                                <th class="kotak" rowspan="2" style="width: 40px">Point
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Nilai
                                                                </th>
                                                            </tr>
                                                            <tr class="tbHeader" id="hd3" runat="server">
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                                <th class="kotak" style="width: 40px; font-size: xx-small;">Point x Bobot
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="total baris" id="dpt" runat="server">
                                                                        <td class="kotak">DEPARTMENT
                                                                        </td>
                                                                        <td class="kotak" colspan="26">
                                                                            <b>
                                                                                <%# Eval("DeptName") %></b>
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstPIC" runat="server" OnItemDataBound="lstPIC_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="Line3 baris" id="pic" runat="server">
                                                                                <td class="kotak" colspan="26">
                                                                                    <b>
                                                                                        <%# Container.ItemIndex+1 %>.
                                                                                <%# Eval("Nama") %>
                                                                                    </b>-
                                                                            <%# Eval("BagianName") %>
                                                                                </td>
                                                                                <td class="kotak" colspan="1">&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <asp:Repeater ID="lstPES" runat="server" OnItemDataBound="lstPES_DataBound">
                                                                                <ItemTemplate>
                                                                                    <tr class="EvenRows baris" id="ps1" runat="server">
                                                                                        <td class="kotak" style="padding-left: 5px;">
                                                                                            <table style="border-collapse: collapse; width: 100%; font-size: x-small; font-family: Arial">
                                                                                                <tr>
                                                                                                    <td style="width: 30%" class="tengah"></td>
                                                                                                    <%--<td style="width:20%" class="angka"><%# Container.ItemIndex+1 %>.</td>--%>
                                                                                                    <td style="width: 60%" class="angka">
                                                                                                        <%# Eval("PESName") %>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td class="kotak tengah bold OddRows">
                                                                                            <%# Eval("Bobot", "{0:N0}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Jan","{0:N1}") %>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("JanN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Feb", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("FebN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Mar", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("MarN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Apr", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("AprN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Mei", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("MeiN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Jun", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("JunN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Jul", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("JulN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Ags", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("AgsN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Sep", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("SepN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Okt", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("OktN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Nop", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("NopN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah">
                                                                                            <%# Eval("Des", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah OddRows">
                                                                                            <%# Eval("DesN", "{0:N1}")%>
                                                                                        </td>
                                                                                        <td class="kotak tengah bold baris">&nbap;
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <tr class="Line3 baris" id="ftr" runat="server">
                                                                                        <td class="kotak bold angka">Total
                                                                                        </td>
                                                                                        <td class="kotak bold angka">&nbsp;
                                                                                        </td>
                                                                                        <td class="kotak bold angka" colspan="24"></td>
                                                                                        <td class="kotak tengah">&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </ItemTemplate>
                                                                        <SeparatorTemplate>
                                                                            <tr style="height: 5px" class="total" id="smtr" runat="server">
                                                                                <td colspan="27" class="kotak">&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </SeparatorTemplate>
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


                    <script type="text/javascript">

                        function displayPopUP(id) {
                            var wh = ($(window).width());
                            var hh = $(window).height();
                            var po = $('#popup').outerHeight();
                            //alert(po);
                            $('#bgr').attr('style', 'width:' + wh + ';height:' + hh);
                            $('#popcontent').attr('style', 'height:' + (po - 40));
                            $('#popup').show();//.attr("style","display:block");
                            $('#bgr').show();
                            $('#popcontent').html();
                        }
                        function closed() {
                            $('#popcontent').html('');
                            $('#popup').hide();
                            $('#bgr').hide();
                        }
                        function loadPES(id) {
                            $('#ldr').show();
                            params = 'dialogWidth=2000px';
                            params += ', dialogHeight=500px'
                            params += ', top=0, left=0'
                            params += ',scrollbars=yes';
                            window.showModalDialog("../../ModalDialog/RekapPESDetailNew.aspx?d=" + id, "PES", params);
                            $('#ldr').hide();
                            return false;
                        }
                    </script>

                    <div style="width: 100%; height: 100%; display: none;" class="modalBackground" id="bgr">
                    </div>
                    <div class="content" id="popup" style="display: none; position: absolute; margin: 0; border: 3px solid gray; width: 95%; height: 600px; left: 10%; top: 10; z-index: 9999">
                        <div style="height: 35px; width: 100%">
                            <table class="nbTableHeader" style="width: 100%;">
                                <tr>
                                    <td style="width: 60%; border-bottom: 2px solid silver">
                                        <strong>&nbsp;REKAP PES </strong>
                                    </td>
                                    <td style="width: 40%; padding-right: 10px; border-bottom: 2px solid silver" align="right"
                                        valign="middle">
                                        <img src="../../images/Delete.png" style="cursor: pointer" title="close" onclick="closed();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; background-color: InfoBackground; overflow: auto" id="popcontent">
                            <iframe id="dtl" style="width: 100%; height: 100%; border: none"></iframe>
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


</asp:Content>
