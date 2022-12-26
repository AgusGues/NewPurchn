<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormDistribusi.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormDistribusi" %>
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

        function OpenDialog(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:350px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileUPD.aspx?ba=" + id, "UploadFileUPD", params);
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
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr style="width: 80%">
                                    <td style="width: 25%">
                                        <b>&nbsp;&nbsp;OUTSTANDING DISTRIBUSI</b>
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:HiddenField ID="appLevele" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                    
                                    <td style="width: 25%; text-align: right;" valign="middle" colspan="0" 
                                        rowspan="0"><span style="font-family: Calibri; font-size: x-small"><b>Find By</b></span> 
                                        :&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="RBNama" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBNama_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: right; font-style: italic;"
                                            Text="Nama" TextAlign="Left" Width="20%" />
                                   <%-- </td>
                                    
                                     <td style="height: 3px; width: 5px; text-align: right;" valign="top">--%><asp:RadioButton 
                                            ID="RBNomor" runat="server" AutoPostBack="True" 
                                             GroupName="a" OnCheckedChanged="RBNomor_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: right; font-style: italic;"
                                            Text="Nomor" TextAlign="Left" Width="20%" />
                                    </td>
                                    
                                    <td style="width: 45%;" align="right">
                                        <asp:TextBox ID="txtCari" Width="80%" Text="Cari" onfocus="if(this.value==this.defaultValue)this.value='';"
                                            onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Nomor Dokumen"
                                            Style="font-family: Calibri"></asp:TextBox>
                                            
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" Style="font-family: Calibri" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr id="tr" runat="server">
                                    <td style="width: 150px; height: 6px; font-size: x-small; font-family: Calibri;"
                                        valign="middle">
                                        <b>&nbsp; Filter berdasarkan :</b>
                                    </td>
                                </tr>
                                <tr id="tr1" runat="server">
                                    <td rowspan="1" style="width: 75px; height: 19px">
                                        <asp:DropDownList ID="ddlFilter" AutoPostBack="true" runat="server" Height="22px"
                                            Width="140px" Style="font-family: Calibri; font-size: x-small" OnSelectedIndexChanged="ddlFilter_Change">
                                            <asp:ListItem Value="0">--- Pilih ---</asp:ListItem>
                                            <asp:ListItem Value="createdtime">Yang Terbaru</asp:ListItem>                                           
                                            <asp:ListItem Value="dept">Department</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="1" style="height: 19px; width: 62px;">
                                        <asp:DropDownList ID="ddlJD" AutoPostBack="true" runat="server" Height="22px" Visible="false"
                                            Width="250px" Style="font-family: Calibri; font-size: x-small" OnSelectedIndexChanged="ddlJD_Change">
                                        </asp:DropDownList>
                                    </td>
                                    
                                    <td rowspan="1" style="width: 75px; height: 19px">
                                        <asp:DropDownList ID="ddlJ" AutoPostBack="true" runat="server" Height="22px" Visible="false"
                                            Width="140px" Style="font-family: Calibri; font-size: x-small" OnSelectedIndexChanged="ddlJ_Change">
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td style="width: 500px">
                                        <asp:Panel ID="PanelEdit" runat="server" Visible="False" BackColor="#FFFFCC" 
                                            style="font-family: Calibri; font-size: x-small">
                                           <table style="width:100%;">
                                                <tr style="width: 100%;">
                                                    <td style="width: 5%; font-size: x-small;">
                                                        ID.</td>
                                                    <td style="width: 10%">
                                                        <span style="font-family: Calibri"><span style="font-size: x-small">
                                                        <asp:TextBox ID="txtID" runat="server" Width="49px" 
                                                            style="font-family: Calibri"></asp:TextBox>
                                                        </span></span>
                                                    </td>
                                                    <td style="width: 10%; font-size: x-small;">
                                                        Nama </td>
                                                    <td style="width: 45%">
                                                        <asp:TextBox ID="txtDokumen" runat="server" Width="100%" 
                                                            style="font-family: Calibri"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; font-size: x-small;">
                                                        Dept </td>
                                                    <td style="width: 20%;">
                                                        <asp:DropDownList ID="ddlRubah" AutoPostBack="true" runat="server" Height="22px"
                                                            Visible="true" Width="140px" Style="font-family: Calibri; font-size: x-small"
                                                            OnSelectedIndexChanged="ddlRubah_Change">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <input ID="btnSimpan" runat="server" onserverclick="btnSimpan_ServerClick" 
                                                            type="button" value="Simpan" style="font-family: Calibri; font-weight: 700" />  
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>--%>
                                    <td style="padding-right: 10px" align="right">
                                        <input type="button" id="btnClose" runat="server" onserverclick="btnClose_ServerClick"
                                            visible="false" value="Refresh" style="font-family: Calibri; font-weight: 700" />
                                            <input type="button" id="btnRefresh" runat="server" onserverclick="btnRefresh_ServerClick"
                                            visible="false" value="Refresh" style="font-family: Calibri; font-weight: 700" />
                                    </td>
                                </tr>
                                <tr id="tr2" runat="server">
                                <td style="width: 100%" colspan="3">
                                        <asp:Panel ID="PanelEdit" runat="server" Visible="False" BackColor="#FFFFCC" 
                                            style="font-family: Calibri; font-size: x-small">
                                           <table style="width:100%;">
                                                <tr style="width: 100%;">
                                                    <td style="width: 5%; font-size: x-small; text-align: center;">
                                                        ID.</td>
                                                    <td style="width: 5%">
                                                        <span style="font-family: Calibri"><span style="font-size: x-small">
                                                        <asp:TextBox ID="txtID" runat="server" Width="49px" 
                                                            style="font-family: Calibri"></asp:TextBox>
                                                        </span></span>
                                                    </td>
                                                    <td style="width: 8%; font-size: x-small; text-align: center;">
                                                        No. </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txtNo" runat="server" Width="100%" 
                                                            style="font-family: Calibri"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 15%; font-size: x-small; text-align: center;">
                                                        Nama Dokumen </td>
                                                    <td style="width: 30%">
                                                        <asp:TextBox ID="txtDokumen" runat="server" Width="100%" 
                                                            style="font-family: Calibri"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; font-size: x-small; text-align: center;">
                                                        Dept. Pemilik </td>
                                                    <td style="width: 20%;">
                                                        <asp:DropDownList ID="ddlRubah" AutoPostBack="true" runat="server" Height="22px"
                                                            Visible="true" Width="140px" Style="font-family: Calibri; font-size: x-small"
                                                            OnSelectedIndexChanged="ddlRubah_Change">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <input ID="btnSimpan" runat="server" onserverclick="btnSimpan_ServerClick" 
                                                            type="button" value="Simpan" style="font-family: Calibri; font-weight: 700" />  
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            
                            <table style="width: 100%">
                                <tr style="width: 100%">
                                    <td style="height: 3px; width: 10%;" valign="top">
                                        <asp:RadioButton ID="RBShare" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBShare_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                            Text="&nbsp; Share Dokumen" TextAlign="Left" Width="135px" />
                                    </td>
                                     <td style="height: 3px; width: 20%;" valign="top">
                                        <asp:RadioButton ID="RBNotShare" runat="server" AutoPostBack="True" 
                                             GroupName="a" OnCheckedChanged="RBNotShare_CheckedChanged"
                                            Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                            Text="&nbsp; Distribusi Dokumen" TextAlign="Left" Width="156px" />
                                    </td>
                                    <td style="height: 3px; width: 70%;" valign="top">
                                       
                                    </td>
                                </tr>
                            </table>
                            
                            <hr />
                            <div id="DivRoot" align="left">
                                <div style="overflow: hidden;" id="DivHeaderRow">
                                </div>
                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                    <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small"
                                        border="0">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th style="width: 4%" class="kotak">
                                                    No. Urut
                                                </th>
                                                <th style="width: 4%" class="kotak">
                                                    ID
                                                </th>
                                                <th style="width: 10%" class="kotak">
                                                    No. Dokumen
                                                </th>
                                                <th style="width: 20%" class="kotak">
                                                    Dept. Pemilik Dokumen
                                                </th>
                                                <th style="width: 41%" class="kotak">
                                                    Nama Dokumen
                                                </th>
                                                <th style="width: 10%" class="kotak">
                                                    Kategori
                                                </th>
                                                <th style="width: 10%" class="kotak">
                                                    &nbsp;
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
                                                        <td class="kotak tengah">
                                                            <span class="tengah" style="width: 40%">
                                                                <%# Eval("ID") %></span>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("NoDocument")%>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("DeptName")%>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("DocName")%>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("CategoryUPD") %>
                                                        </td>
                                                        <td class="kotak tengah" style="padding-right: 1px">
                                                            <asp:ImageButton ToolTip="Distribusi Dokumen" ID="att" runat="server" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                
                                                            <asp:ImageButton ToolTip="Edit Master data" ID="EditMaster" runat="server" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="edit" ImageUrl="~/TreeIcons/Icons/page.gif" />
                                                                
                                                            <asp:ImageButton ToolTip="Hapus Dokumen" ID="hapus" runat="server" CssClass='<%# Eval("ID") %>' 
                                                                CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div id="DivFooterRow" style="overflow: hidden">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="Upload1" runat="server" Visible="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
