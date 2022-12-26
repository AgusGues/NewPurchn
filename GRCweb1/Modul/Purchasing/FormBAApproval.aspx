<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormBAApproval.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormBAApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label{font-size:12px;}
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
        $(document).ready(function() {
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
        function OpenDialog(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFile.aspx?ba=" + id, "UploadFile", params);
        };
        function PreviewPDF2(id) {
            params = 'dialogWidth:890px';
            params += '; dialogHeight:600px'
            params += '; top=0, left=0'
            params += '; resizable:yes'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/PdfPreview.aspx?ba=" + id, "Preview", params);
        };
        function PreviewPDF(id) {
            params = 'width=890px';
            params += ', heigh=600px'
            params += ', top=20px, left=20px'
            params += ', resizable:yes'
            params += ', scrollbars:yes';
            window.open("../../ModalDialog/PDFPreview.aspx?ba=" + id, "Preview", params);
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;
                                            BERITA ACARA</b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnBack" runat="server" Text="Form BA" OnClick="btnBack_Click" />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                        <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                        <asp:HiddenField ID="appLevele" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse;
                                    font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            Periode :
                                        </td>
                                        <td style="width: 35%">
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                        id="baList">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th rowspan="2" style="width: 4%" class="kotak">
                                                    No.
                                                </th>
                                                <th rowspan="2" style="width: 10%" class="kotak">
                                                    BA Number
                                                </th>
                                                <th rowspan="2" style="width: 10%" class="kotak">
                                                    Depo
                                                </th>
                                                <th rowspan="2" style="width: 22%" class="kotak">
                                                    Nama Barang
                                                </th>
                                                <th rowspan="2" style="width: 4%" class="kotak">
                                                    Unit
                                                </th>
                                                <th colspan="2" class="kotak">
                                                    Jumlah (Bal)
                                                </th>
                                                <th rowspan="2" style="width: 5%">
                                                    Selisih Bal
                                                </th>
                                                <th rowspan="2" style="width: 8%" class="kotak">
                                                    Timbangan Supplier
                                                </th>
                                                <th rowspan="2" style="width: 8%" class="kotak">
                                                    Jumlah Sesuai Kadar Air BPAS
                                                </th>
                                                <th rowspan="2" style="width: 10%" class="kotak">
                                                    Adjustment
                                                </th>
                                                <th rowspan="2" class="kotak" style="width: 5%">
                                                    Prosentase Selisih(%)
                                                </th>
                                                <th rowspan="2" class="kotak" style="width: 5%">
                                                    &nbsp;
                                                </th>
                                            </tr>
                                            <tr class="tbHeader">
                                                <th style="width: 5%" class="kotak">
                                                    Supplier
                                                </th>
                                                <th style="width: 5%" class="kotak">
                                                    BPAS
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                <ItemTemplate>
                                                    <tr class="total baris">
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <span class="angka" style="width: 30%">
                                                                <%# Container.ItemIndex+1 %></span> <span class="tengah" style="width: 40%">
                                                                    <asp:CheckBox ID="chk" AutoPostBack="true" CssClass='<%# Eval("ID") %>' runat="server"
                                                                        OnCheckedChanged="chk_CheckedChange" /></span>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("BANum") %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("DepoKertasName").ToString().ToUpper() %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("ItemName") %>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%# Eval("Unit") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("JmlBal","{0:N2}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("JmlBalBPAS","{0:N2}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="slsBal" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("TotalSup", "{0:N2}")%>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <%# Eval("TotalBPAS", "{0:N2}")%>
                                                        </td>
                                                        <td class="kotak" nowrap="nowrap">
                                                            <span class="angka" style="width: 50%">
                                                                <%# Eval("Selisih", "{0:N2}")%></span> <span class="Line3" style="width: 40%">
                                                                    <asp:Label ID="adj" runat="server"></asp:Label></span>
                                                        </td>
                                                        <td class="kotak angka" nowrap="nowrap">
                                                            <span class="angka" style="width: 50%" id="ps" runat="server">
                                                                <%# Eval("ProsSelisih", "{0:N2}")%></span> <span class="tengah" style="width: 40%">
                                                                    <asp:ImageButton ID="attach" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="upload" ToolTip="Upload Attachment Konfirmasi" />
                                                                    <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="viewpdf" ToolTip="View Attachment" />
                                                                </span>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px">
                                                            <asp:ImageButton ToolTip="Attachment" ID="att" runat="server" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                            <asp:ImageButton ToolTip="Approval Info" ID="info" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                CommandName="appInfo" ImageUrl="~/TreeIcons/Icons/workerS.gif" />
                                                            <asp:ImageButton ToolTip="Detail info" ID="detail" runat="server" CssClass='<%# Eval("ID") %>'
                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="detail" ImageUrl="~/images/editor.png" />
                                                            <asp:ImageButton ToolTip="Process Adjust" ID="adjust" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="proses" ImageUrl="~/TreeIcons/Icons/oJornal.gif" />
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                        <HeaderTemplate>
                                                            <tr class="Line3" style="height: 24px">
                                                                <td class="kotak">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak" colspan="0">
                                                                    <b>Document Attachment</b>
                                                                </td>
                                                                <td class="kotak angka" colspan="4">
                                                                    <%--<asp:ImageButton ToolTip="Attachment" ID="attd" runat="server" CssClass='<%# Eval("ID") %>' CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif" />--%>
                                                                </td>
                                                                <td class="kotak" colspan="7" align="right" style="padding-right: 10px">
                                                                    <asp:Label ID="lblNextApp" runat="server" Font-Size="X-Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <td class="kotak">
                                                                </td>
                                                                <td class="kotak">
                                                                    <%# Container.ItemIndex+1 %>.
                                                                    <%# Eval("DocName") %>
                                                                </td>
                                                                <td class="kotak" colspan="2" style="border-right: 0px">
                                                                    <%# Eval("FileName") %>
                                                                </td>
                                                                <td class="kotak angka" style="border-left: 0px" colspan="2">
                                                                    <asp:ImageButton ToolTip="Click to Preview Document" ID="lihat" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                        CssClass='<%# Eval("ID") %>' CommandName="pre" ImageUrl="~/images/14.png" />
                                                                    <asp:ImageButton ToolTip="Click for delete attachment" ID="hapus" runat="server"
                                                                        CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("BAID") %>'
                                                                        CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                                </td>
                                                                <td class="kotak" colspan="7">
                                                                    &nbsp;<%# Eval("CreatedTime")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="OddRows baris" style="vertical-align: middle" id="dtl" runat="server"
                                                        visible="false">
                                                        <td colspan="13" class="kotak">
                                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                                <thead>
                                                                    <tr class="Line3" style="height: 24px">
                                                                        <td class="kotak">
                                                                        </td>
                                                                        <td class="kotak" colspan="10">
                                                                            <b>Detail Receipt</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="tbHeader">
                                                                        <th class="kotak" style="width: 4%">
                                                                            No.
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">
                                                                            ReceiptNo
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">
                                                                            Receipt Date
                                                                        </th>
                                                                        <th class="kotak" style="width: 15%">
                                                                            Agen
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            Qty Gross
                                                                        </th>
                                                                        <th class="kotak" style="width: 10%">
                                                                            Qty Netto
                                                                        </th>
                                                                        <th class="kotak" style="width: 8%">
                                                                            Kadar Air(Depo)
                                                                        </th>
                                                                        <th class="kotak" style="width: 5%">
                                                                            Jml(Bal)
                                                                        </th>
                                                                        <th class="kotak" style="width: 5%">
                                                                            &nbsp;
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="lstDetail" runat="server" OnItemDataBound="lstDetail_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris">
                                                                                <td class="kotak tengah">
                                                                                    <%# Container.ItemIndex+1 %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("ReceiptNo").ToString().ToUpper() %>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("ReceiptDate","{0:d}") %>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    <%# Eval("SupplierName") %>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                    <%# Eval("Gross","{0:N2}") %>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                    <%# Eval("Netto", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak tengah">
                                                                                    <%# Eval("KadarAir", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak angka">
                                                                                    <%# Eval("JmlBal", "{0:N2}")%>
                                                                                </td>
                                                                                <td class="kotak">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                                <%--<tfoot>
                                                                    <tr class="Line3">
                                                                        <td colspan="4" class="kotak angka"></td>
                                                                        <td class="kotak angka"><asp:Label ID="totA" runat="server"></asp:Label></td>
                                                                        <td class="kotak angka"><asp:Label ID="totB" runat="server"></asp:Label></td>
                                                                        <td class="kotak angka"><asp:Label ID="Ka" runat="server"></asp:Label></td>
                                                                        <td class="kotak angka"><asp:Label ID="totC" runat="server"></asp:Label></td>
                                                                        <td class="kotak">&nbsp;</td>
                                                                    </tr>
                                                                </tfoot>--%>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="infoApp" runat="server" Visible="false">
                                                        <HeaderTemplate>
                                                            <tr class="Line3" style="height: 24px">
                                                                <td class="kotak">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak" colspan="12">
                                                                    <b>List Approver</b>
                                                                </td>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris">
                                                                <td class="kotak angka">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="kotak" nowrap="nowrap">
                                                                    <%# Container.ItemIndex+1 %>.
                                                                    <%# Eval("UserName") %>
                                                                </td>
                                                                <td class="kotak" colspan="2">
                                                                    <%# Eval("AppStatus") %>
                                                                    on
                                                                    <%# Eval("CreatedTime", "{0:d/M/yyyy hh:mm:ss}")%>
                                                                </td>
                                                                <td class="kotak" colspan="9">
                                                                    <%# Eval("Keterangan") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="Upload1" runat="server" Visible="false" />
                <%--<asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
