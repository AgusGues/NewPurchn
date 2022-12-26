﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StrukturOrg.aspx.cs" Inherits="GRCweb1.Modul.ISO.StrukturOrg" %>
<%--taroh di setelah 1 baris pertama file--%>
<%--<%@ Register Assembly="ZetroPDFViewer" Namespace="ZetroPDFViewer" TagPrefix="pdf" %>--%>
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

        <script type="text/javascript">
            function OpenDialog() {
                params = 'dialogWidth:520px';
                params += '; dialogHeight:200px'
                params += '; resizable:no'
                params += ';scrollbars:no';
                window.showModalDialog("../../ModalDialog/UploadFileOrg.aspx", "UploadFile", params);
            };
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
                        Struktur Organisasi 
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>

            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                    <tr>
                        <td style="width:100%; height:49px">
                            <table class="nbTableHeader">
                                    <tr>
                                        <%--<td style="width: 60%">
                                            <strong>&nbsp;Struktur Organisasi </strong>
                                        </td>--%>
                                        <td style="width:40%; padding-right:10px" align="right">
                                            <input type="button" value="Upload File" onclick="OpenDialog();" id="btnUpload" runat="server"/>
                                        </td>
                                    </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small; margin-top:8px">
                                    <tr>
                                        <td style="width:5%">&nbsp;</td>
                                        <td style="width:10%">Department</td>
                                        <td style="width:25%"><asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control input-sm"></asp:DropDownList></td>
                                        <td style="width:60%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                        <td><asp:Button ID="btnView" runat="server" Text="Preview" OnClick="btnView_Click" CssClass="btn btn-primary btn-sm" /></td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height:450px;" id="ls" runat="server">
                                    <%--<iframe id="prev" runat="server" width="100%" height="450px"; style="overflow:auto; border:none" ></iframe>--%>
                                    
                                    <iframe id="prev" runat="server" style="overflow:auto; border:none"></iframe>
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
