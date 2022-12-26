<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAnalisaResiko.aspx.cs" Inherits="GRCweb1.Modul.ISO.RekapAnalisaResiko" %>
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
    <style type="text/css">
        .demo
        {
            border: 1px solid #C0C0C0;
            border-collapse: collapse;
            padding: 5px;
        }
        .demo th
        {
            border: 1px solid #C0C0C0;
            padding: 5px;
            background: #F0F0F0;
        }
        .demo td
        {
            border: 1px solid #C0C0C0;
            padding: 5px;
        }
        .style6
        {
            width: 143px;
        }
    </style>

    <script type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            maintainScrollPosition();
        });
        function pageLoad() {
            maintainScrollPosition();
        }
        function maintainScrollPosition() {
            $("#div2").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
        }
        function setScrollPosition(scrollValue) {
            $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
        }
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }

        //        function confirm_close() {
        //            if (confirm("Solved Analisa Resiko!!!") == true) {
        //                window.showModalDialog('../../ModalDialog/ReasonSolvedAnalisaResko.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
        //            } else {
        //                return false;
        //            }
        //        }
        function OpenDialog2(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileSarmutx.aspx?ba=" + id + "&tablename=ISO_AnalisaRAttachment", "UploadFile", params);
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 30%; padding-left: 5px">
                                            <strong>&nbsp; Rekap Analisa Resiko </strong>
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                        <td style="width: 70%; padding-right: 5px" align="right">
                                            <asp:Button ID="btnSolved" runat="server" Text="Solved" OnClick="btnSolved_ServerClick"
                                                Enabled="false" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick"
                                                Enabled="false" />
                                            <asp:Button ID="btnForm" runat="server" Text="Form" OnClick="btnForm_serverClick" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                                            <asp:Button ID="btnToPDF" runat="server" Text="Export to PDF" OnClick="btnExportPDF_Click"
                                                Visible="false" />
                                            <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClick="btnPrint_ServerClick"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                                <div class="content">
                                    <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp; Periode :
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp;<asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="True" Width="25%"
                                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-- Pilih --</asp:ListItem>
                                                    <asp:ListItem Value="1">Semester I</asp:ListItem>
                                                    <asp:ListItem Value="2">Semester II</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged"
                                                    Width="15%">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp<asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="45%"
                                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" Enabled="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%;">
                                                <asp:Label ID="idArisk" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height: 700px; overflow: auto" id="lst" runat="server"
                                        onscroll="setScrollPosition(this.scrollTop);">
                                        <asp:Panel ID="PanelUtama" runat="server">
                                            <table id="Table1" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                                width: 100%" border="0">
                                                <thead>
                                                    <tr>
                                                        <th style="height: 17px" align="left">
                                                            PT.BANGUNPERKASA ADHITAMASENTRA
                                                        </th>
                                                        <th style="height: 17px">
                                                        </th>
                                                        <th style="height: 17px">
                                                        </th>
                                                        <th style="height: 17px">
                                                        </th>
                                                        <th style="height: 17px" align="right">
                                                            <%--ISO/RA/41/18/R4--%>
                                                            <asp:Label runat="server" ID="NoForm"></asp:Label>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th>
                                                        </th>
                                                        <th align="right">
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="5">
                                                            <h1>
                                                                RISK ASSESMENT</h1>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <th colspan="5" align="left">
                                                            Departemen :&nbsp;<asp:Label ID="departemen" runat="server"></asp:Label>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="Tbody1" runat="server">
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="panel1" runat="server">
                                            <table id="Table2" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                                width: 120%">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak " rowspan="2">
                                                            No.
                                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange"
                                                                Visible="false" />
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Klasifikasi Resiko
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Activities/ Kegiatan
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Risk
                                                        </th>
                                                        <th class="kotak " colspan="2">
                                                            Issue (Internal / External )
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Opportunity / Peluang
                                                        </th>
                                                        <th rowspan="2">
                                                            Level Kemungkinan
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Level Dampak
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Level Resiko
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Treatment / Mitigation
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Due Date
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Approval
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Status
                                                        </th>
                                                        <%--<th class="kotak " rowspan="2">
                                                            Lampiran
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            #
                                                        </th>--%>
                                                        <th class="kotak " rowspan="2">
                                                            UploadFile
                                                        </th>
                                                        <%-- <th class="kotak " rowspan="2">
                                                            Edit
                                                        </th>--%>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak ">
                                                            Internal
                                                        </th>
                                                        <th class="kotak ">
                                                            External
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tb" runat="server">
                                                    <asp:Repeater ID="lstPMX" runat="server" OnItemCommand="lstPMX_Command" OnItemDataBound="lstPMX_Databound">
                                                        <ItemTemplate>
                                                            <tr id="lst" runat="server" class="total baris">
                                                                <td class="kotak tengah" style="width: 2%">
                                                                    <%# Container.ItemIndex+1 %>
                                                                    <asp:Label ID="idxx" runat="server" ToolTip='<%# Eval("ID") %>'></asp:Label></span>
                                                                    <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                        OnCheckedChanged="chk_CheckedChangePrs" Visible="true" />
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <asp:Label ID="AnalisaRisk" runat="server" Text='<%#Eval("AnalisaResiko")%>'></asp:Label>&nbsp;
                                                                    <asp:Label ID="Ket" runat="server" Text='<%#Eval("Ket")%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Aktivitas")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Risk")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("IssueInternal1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("IssueEkternal1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Peluang1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 1%">
                                                                    <%# Eval("LvlKemungkinan")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 1%">
                                                                    <%# Eval("LvlDampak")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 3%">
                                                                    <%# Eval("LvlResiko")%>
                                                                    &nbsp; (
                                                                    <%# Eval("LvlResiko1")%>
                                                                    )
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Treatment1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("DueDate")%>/<%# Eval("Bulan")%>/<%# Eval("Tahun")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("Approval")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("StatusX")%>
                                                                </td>
                                                                <%--<td class="kotak angka" style="width: 10%">
                                                                    <%# Eval("FileName")%>
                                                                    &nbsp;
                                                                </td>--%>
                                                                <%--<td class="kotak tengah" style="width: 15%">
                                                                   <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                                                        <ItemTemplate>
                                                                    <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                        CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                        ImageUrl="~/images/Logo_Download.png" />
                                                                    <asp:ImageButton ToolTip='<%# Eval("ID") %>' ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                        CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("SarmutransID") %>' CommandName="hapus"
                                                                        ImageUrl="~/images/Delete.png" />--%>
                                                                <%--</ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>--%>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <asp:ImageButton ToolTip="Upload Attachment" ID="attPrs" runat="server" CssClass='<%# Eval("ID") %>'
                                                                        CommandArgument='<%# Container.ItemIndex %>' CommandName="attachPrs" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                </td>
                                                                <%-- <td class="kotak angka" style="width: 10%">
                                                                    <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="edit" ToolTip='<%# Eval("ID") %>' />
                                                                </td>--%>
                                                            </tr>
                                                            <asp:Repeater ID="attachmx" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                                <HeaderTemplate>
                                                                    <tr class="Line3">
                                                                        <td class="kotak">
                                                                        </td>
                                                                        <td class="kotak" colspan="2">
                                                                            <b>[ Document Lampiran ]</b>
                                                                        </td>
                                                                        <td class="kotak" colspan="12">
                                                                        </td>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris">
                                                                        <td class="kotak">
                                                                        </td>
                                                                        <td class="kotak" colspan="7" style="width: 90%">
                                                                            <%# Container.ItemIndex+1 %>.&nbsp;<%# Eval("FileName")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                                ImageUrl="~/images/Logo_Download.png"  />
                                                                            <asp:ImageButton ToolTip='<%# Eval("ID") %>' ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                                CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("SarmutransID") %>' CommandName="hapus"
                                                                                ImageUrl="~/images/Delete.png" />
                                                                        </td>
                                                                        <td class="kotak" colspan="6" style="width: 5%">
                                                                            <%# Eval("CreatedTime")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                            <br />
                                            <table id="Table3" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                                width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th align="left" width="70%">
                                                            <table style="width: 70%; border: 0; font-size: x-small; font-family: Calibri">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="Image2" runat="server" Height="204px" Width="544px" ImageUrl="~/images/P.jpg" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </th>
                                                        <th valign="top" width="30%">
                                                            <table style="font-size: x-small" class="demo" width="100%" border="1">
                                                                <%--<caption>Citeureup,</caption>--%>
                                                                <asp:Label ID="Alamat" runat="server"></asp:Label><br />
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        Di buat
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Diketahui
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Disetujui
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        <br />
                                                                        <asp:Image ID="HeadISOCtrp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/uum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="HeadISOKRwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/uum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="HeadISOJombang" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/uum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="mgrHRD" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/AgungH.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="mgrHRDKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/andihrdmgrkrw.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="mgrHRDJmbg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/mgrhrd-ekososilo-jmbg.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrBM" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/bachrodin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrBMKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/linda.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrBMJmbg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/mgrBM-Ambar-jmbg.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrQA" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Bahrul Ulum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrQAKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/cucunqa.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrQAJmbg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/mgrqa-paksuradi-jmbg.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrLogBj" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Hengky.png"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrLogBjKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/adilogistik.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrFin" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/bachrodin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrFinKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/wawanfin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrIT" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Sodikin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="spvPurch" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/aying.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrMtn" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Rofiq.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgtMtnKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/zhakim.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgtMtnJmbg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/mgrmtn-suprapto-jmbg.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrPPICCtrp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/nafiq.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrPPICKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/nuniek.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrDeliCtrp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/devian.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrMktCtrp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/lenimkt.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                            <%--<asp:Image ID="Image5" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/mgrmtn-suprapto-jmbg.jpg"
                                                                            ImageAlign="Middle" Visible="false" />--%>
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Image ID="MgrHRDCorp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Bastari.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrCorpDelCtrp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/devian.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrCoprMkt" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/lenimkt.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="PMbm" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Justin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="PMbmKrwg" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Zuhri.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MGRITCorp" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Iko.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrPurch" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/aying.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Image ID="HeadISOHrd" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/uum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        Manager
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Plant Mgr/Corp.Mgr
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Corp.ISO Manager
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div class="contentlist" style="height: 700px; overflow: auto" id="AllDept" runat="server"
                                        onscroll="setScrollPosition(this.scrollTop);" visible="false">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: 'Calibri Light';
                                                height: auto;" border="0">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th class="kotak " rowspan="2">
                                                            No.
                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange"
                                                                Visible="false" />
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Klasifikasi Resiko
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Activities/ Kegiatan
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Risk
                                                        </th>
                                                        <th class="kotak " colspan="2">
                                                            Issue (Internal / External )
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Opportunity / Peluang
                                                        </th>
                                                        <th rowspan="2">
                                                            Level Kemungkinan
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Level Dampak
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Level Resiko
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Treatment / Mitigation
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Due Date
                                                        </th>
                                                        <%--<th class="kotak " rowspan="2">
                                                            Approval
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            Status
                                                        </th>--%>
                                                        <%--<th class="kotak " rowspan="2">
                                                            Lampiran
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            #
                                                        </th>
                                                        <th class="kotak " rowspan="2">
                                                            UploadFile
                                                        </th>--%>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th class="kotak ">
                                                            Internal
                                                        </th>
                                                        <th class="kotak ">
                                                            External
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="Tbody2" runat="server">
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <ItemTemplate>
                                                            <tr id="AllDept" runat="server" class="EvenRows baris">
                                                                <td class="kotak tengah" style="width: 2%">
                                                                    <%# Container.ItemIndex+1 %></span>
                                                                    <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                        OnCheckedChanged="chk_CheckedChangePrs" Visible="false" />
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("AnalisaResiko")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Aktivitas")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Risk")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("IssueInternal1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("IssueEkternal1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Peluang1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 1%">
                                                                    <%# Eval("LvlKemungkinan")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 1%">
                                                                    <%# Eval("LvlDampak")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 3%">
                                                                    <%# Eval("LvlResiko")%>
                                                                    &nbsp; (
                                                                    <%# Eval("LvlResiko1")%>
                                                                    )
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <%# Eval("Treatment1")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("DueDate")%>/<%# Eval("Bulan")%>/<%# Eval("Tahun")%>
                                                                </td>
                                                                <%--<td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("Approval")%>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 4%">
                                                                    <%# Eval("StatusX")%>
                                                                </td>--%>
                                                                <%-- <td class="kotak angka" style="width: 10%">
                                                                    <%# Eval("FileName")%>
                                                                    &nbsp;
                                                                </td>--%>
                                                                <%-- <td class="kotak tengah" style="width: 10%">
                                                                    <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                                ImageUrl="~/images/Logo_Download.png" />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 10%">
                                                                    <asp:ImageButton ToolTip="Upload Attachment" ID="attPrs" runat="server" CssClass='<%# Eval("ID") %>'
                                                                        CommandArgument='<%# Container.ItemIndex %>' CommandName="attachPrs" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                            <br />
                                            <table id="Table5" style="border-collapse: collapse; font-size: x-small; font-family: Calibri;
                                                width: 100%">
                                                <thead>
                                                    <tr>
                                                        <th align="left" width="70%">
                                                            <table style="width: 70%; border: 0; font-size: x-small; font-family: Calibri">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="Image1" runat="server" Height="204px" Width="544px" ImageUrl="~/images/P.jpg" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </th>
                                                        <th valign="top" width="30%">
                                                            <table style="font-size: x-small" class="demo" width="100%" border="1">
                                                                <%--<caption>Citeureup,</caption>--%>
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        Di buat
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Diketahui
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Disetujui
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        <br />
                                                                        <asp:Image ID="MgrHRD1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/AgungH.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrBM1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Zuhri.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrQA1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Bahrul Ulum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrLogbbBJ" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Fajar R.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrFin1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/bachrodin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="MgrIT1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Sodikin.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="SpvPurch1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/ika.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <br />
                                                                        <br />
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Image ID="CorpHRD1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Bastari.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="PM1" runat="server" Width="83px" Height="51px" ImageUrl="~/images/PM.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="COrpIT1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/Iko.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                        <asp:Image ID="CorpPurch1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/aying.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Image ID="CorpISO1" runat="server" Width="83px" Height="51px" ImageUrl="~/Modul/ISO/Images/uum.jpg"
                                                                            ImageAlign="Middle" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="kotak tengah">
                                                                        Manager
                                                                    </td>
                                                                    <td class="style6">
                                                                        Corp.Plant Mgr/Corp.Mgr
                                                                    </td>
                                                                    <td class="kotak tengah">
                                                                        Corp.ISO Manager
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
