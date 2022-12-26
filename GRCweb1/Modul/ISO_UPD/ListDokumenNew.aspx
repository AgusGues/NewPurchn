<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListDokumenNew.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.ListDokumenNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
  // fix for deprecated method in Chrome 37 / js untuk bantu view modal dialog
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

        function PreviewPDF(id) {
            params = 'dialogWidth:890px';
            params += '; dialogHeight:600px'
            params += '; top=0, left=0'
            params += '; resizable:yes'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/PdfPreviewUPD.aspx?ba=" + id, "Preview", params);
            return false;
        };

        function OpenDialog(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:400px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileUPDrev.aspx?ba=" + id, "Preview", params);
            return false;
        };

        function OpenDialog2(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:470px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileUPD_Dist.aspx?ba=" + id, "Preview", params);
            return false;
        };

        function OpenDialog3(id) {
            params = 'dialogWidth: 820px';
            params += '; dialogHeight:250px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileUPD_APlant.aspx?ba=" + id, "Preview", params);
            return false;
        };
    
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

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server"  class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>DAFTAR DOKUMEN TERDISTRIBUSI</b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:HiddenField ID="appLevele" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content" style="background:white;">
                                <hr />
                                <table style="width: 90%; border-collapse: collapse; font-size: x-small" border="0">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 6px; font-size: x-small; font-family: Calibri;"
                                            valign="middle">
                                            <b>&nbsp; DEPARTMENT</b>
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" Height="22px" 
                                                OnSelectedIndexChanged="ddlDept_Change" Style="font-family: Calibri;
                                                font-size: x-small" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="1" style="width: 196px; height: 19px">
                                            <b>JENIS DOKUMEN</b>&nbsp;<asp:DropDownList ID="ddlJenis" runat="server" 
                                                AutoPostBack="true" Height="22px" OnSelectedIndexChanged="ddlJenis_Change" Style="font-family: Calibri;
                                                font-size: x-small" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 150px; height: 6px; font-size: x-small; font-family: Calibri;"
                                            valign="middle">
                                            <b>DOKUMEN DEPT. TERKAIT</b>
                                            <asp:DropDownList ID="ddlDept0" runat="server" AutoPostBack="true" 
                                                Height="22px" OnSelectedIndexChanged="ddlJenis_Change" Style="font-family: Calibri;
                                                font-size: x-small" Width="140px">
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="1" style="width: 196px; height: 19px">
                                            &nbsp;</td>
                                        <td style="width: 196px; height: 19px;">
                                            <input id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" style="background-color: white;
                                                font-weight: bold; font-size: 11px; height: 22px;" type="button" value="Preview" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivRoot" align="left">
                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                    </div>
                                    <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                        <table id="thr" style="width: 99%; border-collapse: collapse; font-size: x-small" border="0">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th style="width: 3%" class="kotak">
                                                        No
                                                    </th>
                                                    <th style="width: 7%" class="kotak">
                                                        No Dokumen
                                                    </th>
                                                    <th style="width: 30%" class="kotak">
                                                        Nama Dokumen
                                                    </th>
                                                    <th style="width: 7%" class="kotak">
                                                        Revisi Ke
                                                    </th>
                                                    <th style="width: 7%" class="kotak">
                                                        Tgl Berlaku
                                                    </th>
                                                    <%--<th style="width: 7%" class="kotak">
                                                    Tgl Distribusi
                                                </th>--%>
                                                    <th style="width: 30%" class="kotak">
                                                        Lampiran
                                                    </th>
                                                    <th style="width: 7%" class="kotak">
                                                        View PDF
                                                    </th>
                                                    <th style="width: 9%" class="kotak">
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="kotak tengah">
                                                                <span class="tengah" style="width: 40%">
                                                                    <%# Eval("No") %></span>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;<%# Eval("NoDocument")%>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;<%# Eval("DocName")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("RevisiNo")%>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("TglBerlaku","{0:dd-MM-yyyy}")%>
                                                            </td>
                                                            <%--<td class="kotak tengah">
                                                            <%# Eval("TglDistribusi","{0:dd-MM-yyyy}")%>
                                                        </td>--%>
                                                            <td class="kotak tengah">
                                                                <%# Eval("FileName")%>
                                                            </td>
                                                            <td class="kotak tengah" style="padding-right: 1px">
                                                                <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="viewpdf" ToolTip="View Attachment" />
                                                            </td>
                                                            <td class="kotak tengah" style="padding-right: 1px" >
                                                                <asp:ImageButton ToolTip="Edit Master Dokumen" ID="att" runat="server" CssClass='<%# Eval("ID") %>'
                                                                    CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" 
                                                                    ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                    
                                                                <asp:ImageButton ToolTip="Edit Distribusi" ID="edit" runat="server" CssClass='<%# Eval("ID") %>'
                                                                    CommandArgument='<%# Container.ItemIndex %>' CommandName="edit" 
                                                                    ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                    
                                                                <asp:ImageButton ToolTip="Upload Pdf antar Plant" ID="ReUpload" runat="server" CssClass='<%# Eval("ID") %>'
                                                                    CommandArgument='<%# Container.ItemIndex %>' CommandName="ReUpload" 
                                                                    ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                    
                                                                <asp:ImageButton ToolTip="Hapus" ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                    CssClass='<%# Eval("IDm") %>' AlternateText='<%# Eval("IDm") %>' CommandName="hps"
                                                                    ImageUrl="~/images/Delete.png" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </ItemTemplate> </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div id="DivFooterRow" style="overflow: hidden">
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
