<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ListKaryawanStaff.aspx.cs" Inherits="GRCweb1.Modul.ISO.ListKaryawanStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
            function OpenDialog2(id) {
                params = 'dialogWidth:820px';
                params += '; dialogHeight:175px'
                params += '; top=0, left=0'
                params += '; resizable:no'
                params += ';scrollbars:no';
                window.showModalDialog("../../ModalDialog/UploadFileSosPES.aspx?ba=" + id + "&tablename=ISO_SosialisasiPES", "UploadFile", params);
            };
            function OpenDialog3(id) {
                params = 'dialogWidth:820px';
                params += '; dialogHeight:175px'
                params += '; top=0, left=0'
                params += '; resizable:no'
                params += ';scrollbars:no';
                window.showModalDialog("../../ModalDialog/UploadFileOJDPES.aspx?ba=" + id + "&tablename=ISO_OJDPES", "UploadFile", params);
            };
            function OpenDialog4(id) {
                params = 'dialogWidth:820px';
                params += '; dialogHeight:175px'
                params += '; top=0, left=0'
                params += '; resizable:no'
                params += ';scrollbars:no';
                window.showModalDialog("../../ModalDialog/UploadFilePenjurian.aspx?ba=" + id + "&tablename=ISO_PenjurianPES", "UploadFile", params);
            };
        </script>
        </head>

    <body class="no-skin">

        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <span class="text-left"><b>DAFTAR KARYAWAN STAFF</b></span>
                        <div class="pull-right">
                            <input id="btnRefresh" runat="server" style="background-color: blue; font-weight: bold; font-size: 11px;" type="button" value="Refresh" onserverclick="btnRefresh_ServerClick" />
                            <input id="btnExport" runat="server" style="background-color: blue; font-weight: bold; font-size: 11px;" type="button" value="Export" onserverclick="btnExport_ServerClick" />
                            <asp:Button ID="btnNew" runat="server" Text="Refersh Data" Visible="false" OnClick="btnNew_Click" />
                        </div>
                    </div>
                    <div style="padding: 2px"></div>

                    <div id="Div1" runat="server">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />

                            <tr valign="top">
                                <td style="width: 100%" colspan="2">
                                    <hr />
                                    <div id="lst" runat="server">
                                        <div class="contentlist" style="height: 410px" runat="server">
                                           
                                           <table style="width: 100%; border-collapse: collapse; font-size: x-small; display: block" border="0" id="mytable">
                                                    <thead>
                                                        <tr class=" tbHeader baris">
                                                            <th class="kotak" rowspan="2" style="width: 4%">NO.
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 6%">NIP
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 15%">NAMA
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 10%">DEPARTMENT
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 6%">TGL MASUK
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 15%">JABATAN
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 8%">SYSTEM PES
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 8%">TGL. SOSIALISASI
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 10%">LAMPIRAN
                                                            </th>
                                                            <th rowspan="1" style="width: 3%" id="t1">Upload File
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 8%">TGL. OJD
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 10%">LAMPIRAN OJD
                                                            </th>
                                                            <th rowspan="1" style="width: 3%" id="t2">Upload File
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 8%">TGL. Penjurian PES
                                                            </th>
                                                            <th class="kotak" rowspan="2" style="width: 10%">LAMPIRAN Penjurian PES
                                                            </th>
                                                            <th rowspan="1" style="width: 3%" id="t3">Upload File
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody style="font-family: Calibri">
                                                        <asp:Repeater ID="lstKaryawanStaff" runat="server" OnItemDataBound="lstKaryawanStaff_DataBound" OnItemCommand="lstKaryawanStaff_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris" valign="top" id="ps1" runat="server">
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("No") %>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("NIP") %>
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("NAMA") %>
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("DEPARTMENT")%>
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <%# Eval("TGLMASUK")%>
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("BAGIAN")%>
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("KETERANGAN")%>
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("Tgl_Sosialisasi")%>
                                                                    </td>
                                                                    <td class="kotak">
                                                                        <table width="100%" style="font-size: x-small">
                                                                            <thead>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                                                                            <HeaderTemplate>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr class="kotak">
                                                                                                    <td>
                                                                                                        <%# Eval("Attachment") %>
                                                                                                    </td>
                                                                                                    <td align="right" width="15%">
                                                                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                                            CommandArgument='<%# Eval("Attachment") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs" ImageUrl="~/images/Logo_Download.png" />
                                                                                                        <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                                                            CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' CommandName="hpsprs" ImageUrl="~/images/Delete.png" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </td>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </td>
                                                                    <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px" id="grid1">
                                                                        <asp:ImageButton ToolTip="Upload Attachment" ID="attprs" runat="server" CssClass='<%# Eval("ID") %>'
                                                                            CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif" Enabled="True" />
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                        <%# Eval("Tgl_OJD")%>
                                                                    </td>
                                                                    <td class="kotak">
                                                                        <table width="100%" style="font-size: x-small">
                                                                            <thead>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Repeater ID="attachOJD" runat="server" OnItemCommand="attachOJD_Command" OnItemDataBound="attachOJD_DataBound">
                                                                                            <HeaderTemplate>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr class="kotak">
                                                                                                    <td>
                                                                                                        <%# Eval("Attachment") %>
                                                                                                    </td>
                                                                                                    <td align="right" width="15%">
                                                                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                                            CommandArgument='<%# Eval("Attachment") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                                                            ImageUrl="~/images/Logo_Download.png" />
                                                                                                        <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                                                            CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                                                            CommandName="hpsprs" ImageUrl="~/images/Delete.png" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </td>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </td>
                                                                    <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px" id="grid2">
                                                                        <asp:ImageButton ToolTip="Upload Attachment" ID="attojd" runat="server" CssClass='<%# Eval("ID") %>'
                                                                            CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif"
                                                                            Enabled="True" />
                                                                    </td>
                                                                    <td class="kotak">&nbsp;
                                                                         <%# Eval("tgl_Penjurian")%>
                                                                    </td>
                                                                    <td class="kotak">
                                                                        <table width="100%" style="font-size: x-small">
                                                                            <thead>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Repeater ID="attachjuri" runat="server" OnItemCommand="attachjuri_Command" OnItemDataBound="attachjuri_DataBound">
                                                                                            <HeaderTemplate>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr class="kotak">
                                                                                                    <td>
                                                                                                        <%# Eval("Attachment") %>
                                                                                                    </td>
                                                                                                    <td align="right" width="15%">
                                                                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                                            CommandArgument='<%# Eval("Attachment") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                                                            ImageUrl="~/images/Logo_Download.png" />
                                                                                                        <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                                                            CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                                                            CommandName="hpsprs" ImageUrl="~/images/Delete.png" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </td>
                                                                                </tr>
                                                                            </thead>
                                                                        </table>
                                                                    </td>
                                                                    <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px" id="grid3">
                                                                        <asp:ImageButton ToolTip="Upload Attachment" ID="attjuri" runat="server" CssClass='<%# Eval("ID") %>'
                                                                            CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif"
                                                                            Enabled="True" />
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


                    <script type="text/javascript">


                        function loadHistory(id) {
                            //        $('#ldr').show();
                            params = 'dialogWidth=1245px';
                            params += ', dialogHeight=500px'
                            params += ', top=0, left=0'
                            params += ',scrollbars=yes';
                            window.showModalDialog("../../ModalDialog/RekapHistoryBKaryawan.aspx?d=" + id, "PES", params);
                            //        $('#ldr').hide();
                            return false;
                        }
                    </script>


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
