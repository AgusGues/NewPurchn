<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LaporanWorkOrder_New.aspx.cs" Inherits="GRCweb1.Modul.MTC.LaporanWorkOrder_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }

        label {
            font-size: 12px;
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
        function OpenDialog(WOID) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileWO.aspx?wo=" + WOID, "UploadFile", params);
        };

        function PreviewPDF(id) {
            params = 'width=890px';
            params += ', heigh=600px'
            params += ', top=20px, left=20px'
            params += ', resizable:yes'
            params += ', scrollbars:yes';
            window.open("../../ModalDialog/PDFPreviewWO.aspx?wrk=" + id, "Preview", params);
        };
    </script>

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                //var DivFR = document.getElementById('DivFooterRow');

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
                //DivFR.style.width = (parseInt(width)) + 'px';
                //DivFR.style.position = 'relative';
                //DivFR.style.top = -headerHeight + 'px';
                //DivFR.style.verticalAlign = 'top';
                //DivFR.style.paddingtop = '2px';

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
                    //DivFR.appendChild(tblfr);
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%;">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;<span style="font-family: Calibri; font-size: large">WORK ORDER</span></b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr style="width: 100%">
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="LabelDept" runat="server" Visible="false" Style="font-family: Calibri; font-size: medium; font-weight: 700;">&nbsp; Department :</asp:Label>
                                        </td>
                                        <td style="width: 18%">
                                            <asp:DropDownList ID="ddlDept" runat="server" Visible="false" AutoPostBack="true"
                                                Style="font-family: Calibri" OnTextChanged="ddlDept_change">
                                                <asp:ListItem Value="0">---- Pilih Dept ----</asp:ListItem>
                                                <asp:ListItem Value="7">HRD & GA</asp:ListItem>
                                                <asp:ListItem Value="19">MAINTENANCE</asp:ListItem>
                                                <asp:ListItem Value="14">IT</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%; text-align: left;" align="right" valign="middle">
                                            <asp:Label ID="LabelTotal" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>
                                        <td style="width: 20%" align="left">
                                            <asp:Label ID="LabelTotalNilai" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                            <asp:Label ID="Label01" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>

                                    </tr>

                                    <tr style="width: 100%">
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="LabelPeriode" runat="server" Visible="true" Style="font-family: Calibri; font-size: medium; font-weight: 700;">&nbsp; Periode :</asp:Label>
                                        </td>
                                        <td style="width: 18%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Style="font-family: Calibri" OnSelectedIndexChanged="ddlBulan_Change">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server" Style="font-family: Calibri" OnSelectedIndexChanged="ddlTahun_Change">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;&nbsp;                                            
                                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click"
                                                Style="font-family: Calibri" Text="Preview" />
                                            <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%; text-align: left;" align="right" valign="middle">
                                            <asp:Label ID="LabelTarget" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>
                                        <td style="width: 20%" align="left">
                                            <asp:Label ID="LabelTargetNilai" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                            <asp:Label ID="Label02" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%" colspan="2">&nbsp;
                                        </td>
                                        <td style="width: 18%">&nbsp;</td>
                                        <td style="width: 5%; text-align: left;" align="right" valign="middle">
                                            <asp:Label ID="LabelPersen" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>
                                        <td style="width: 20%" align="left">
                                            <asp:Label ID="LabelPersenNilai" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                            <asp:Label ID="Label03" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table id="Table2" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                ID="RB1" runat="server" AutoPostBack="True" Checked="false" OnCheckedChanged="RB1_CheckedChanged"
                                                Style="font-size: x-small; text-align: left; font-family: Calibri; font-weight: 700; font-style: italic;" Text="&nbsp; Pemantauan WO Masuk"
                                                TextAlign="Left" Width="174px" Visible="false" />
                                            <asp:RadioButton ID="RBK" runat="server" AutoPostBack="True" Checked="false" OnCheckedChanged="RBK_CheckedChanged"
                                                Style="font-size: x-small; text-align: left; font-family: Calibri; font-weight: 700; font-style: italic;" Text="&nbsp; Pemantauan WO Keluar"
                                                TextAlign="Left" Width="164px" />
                                            <asp:RadioButton ID="RB2" runat="server" AutoPostBack="True" Checked="false" OnCheckedChanged="RB2_CheckedChanged"
                                                Style="font-size: x-small; text-align: left; font-family: Calibri; font-style: italic; font-weight: 700;" Text="&nbsp; Pemantauan WO per Bulan"
                                                TextAlign="Left" Width="178px" />
                                            <asp:RadioButton ID="RBPO" runat="server" AutoPostBack="True" Checked="false" OnCheckedChanged="RBPO_CheckedChanged"
                                                Style="font-size: x-small; text-align: left; font-family: Calibri; font-style: italic; font-weight: 700;"
                                                Text="&nbsp; Semua WO ( Status Open - Closed )"
                                                TextAlign="Left" Width="218px" />
                                            <asp:RadioButton ID="RBLewat" runat="server" AutoPostBack="True" Checked="false"
                                                OnCheckedChanged="RBLewat_CheckedChanged" Style="font-size: x-small; text-align: left; font-family: Calibri; font-style: italic; font-weight: 700;"
                                                Text="&nbsp; WO Lewat"
                                                TextAlign="Left" Width="100px" Visible="false" />
                                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                                Style="font-family: Calibri" Text="Export to Excel" />
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <div id="DivRoot" align="left">
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <table id="thr" style="border-collapse: collapse; font-size: x-small; font-family: Arial">
                                                <thead>
                                                    <tr class="tbHeader">
                                                        <th rowspan="2" style="width: 3%" class="kotak">No.
                                                        </th>
                                                        <th rowspan="2" style="width: 7%">WO Number
                                                        </th>
                                                        <th rowspan="2" style="width: 22%" class="kotak">Uraian Pekerjaan
                                                        </th>
                                                        <th rowspan="2" style="width: 12%" class="kotak">Area
                                                        </th>
                                                        <th colspan="6" class="kotak">Tanggal
                                                        </th>
                                                        <th rowspan="2" style="width: 7%" class="kotak">Pelaksana
                                                        </th>
                                                        <th rowspan="2" style="width: 7%" class="kotak">Status
                                                        </th>
                                                        <th rowspan="2" style="width: 7%" class="kotak">Sisa Waktu Pekerjaan
                                                        </th>
                                                        <th rowspan="2" style="width: 9%" class="kotak">Keterangan
                                                        </th>
                                                        <th rowspan="2" style="width: 5%" class="kotak">DiBuat Oleh
                                                        </th>
                                                        <th rowspan="2" style="width: 5%" class="kotak">Selisih HK
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader">
                                                        <th style="width: 7%" class="kotak">Dibuat
                                                        </th>
                                                        <th style="width: 7%" class="kotak">Masuk
                                                        </th>
                                                        <th style="width: 7%" class="kotak">Update Pelaksana WO
                                                        </th>
                                                        <th style="width: 7%" class="kotak">WO di Update
                                                        </th>
                                                        <th style="width: 7%" class="kotak">Target
                                                        </th>
                                                        <th style="width: 7%" class="kotak">Selesai
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris">
                                                                <td class="kotak" colspan="16" bgcolor="#00CC00">&nbsp;<b>&nbsp;&nbsp;<%# Eval("DeptName") %>&nbsp;</b>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound">
                                                                <ItemTemplate>
                                                                    <tr class="Line3">
                                                                        <td class="kotak tengah" nowrap="nowrap">
                                                                            <span class="angka" style="width: 30%">
                                                                                <%# Container.ItemIndex+1 %></span> <span class="tengah" style="width: 40%"></span>
                                                                        </td>
                                                                        <td class="kotak tengah" nowrap="nowrap">
                                                                            <%# Eval("NoWO") %>
                                                                        </td>
                                                                        <td class="kotak">&nbsp;<%# Eval("UraianPekerjaan") %></td>
                                                                        <td class="kotak">&nbsp;<%# Eval("SubArea2")%></td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("CreatedTime","{0:d}") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("ApvMgr","{0:d}") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                        <%# Eval("UpdatePelaksanaTime","{0:d}") %>
                                                                        </td>
                                                                        
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("waktu2","{0:d}") %>
                                                                        </td>
                                                                         <td class="kotak tengah">
                                                                            <%# Eval("DueDateWO","{0:d}") %>
                                                                        </td>
                                                                        
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("FinishDate2","{0:d}") %>
                                                                        </td>
                                                                        <td class="kotak">&nbsp;<%# Eval("Pelaksana") %></td>
                                                                        <td class="kotak tengah" colspan="1">&nbsp;<%# Eval("StatusApv") %></td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("SisaHari")%>
                                                                            Hari
                                                                        </td>
                                                                        <td class="kotak tengah">&nbsp;<%# Eval("StatusWO") %></td>
                                                                        <td class="kotak tengah">&nbsp;<%# Eval("CreatedBy") %></td>
                                                                        <td class="kotak tengah">&nbsp;<%# Eval("Selisih") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                
                                            </table>
                                            <table id="Table1" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                <tr style="width: 100%">
                                                    <td style="width: 15%">&nbsp;
                                                    </td>
                                                    <td style="width: 2%">&nbsp;
                                                    </td>
                                                    <td colspan="2" style="width: 73%">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td style="width: 15%">
                                                        <asp:Label ID="LPeriode" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Periode</asp:Label>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <asp:Label ID="LKoma1" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold">:</asp:Label>
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:Label ID="txtPeriod" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold"></asp:Label>
                                                   
                                                       <%-- <asp:TextBox ID="" runat="server" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"
                                                            BorderStyle="None"></asp:TextBox>--%>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 73%">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td style="width: 15%">
                                                        <asp:Label ID="LTotal" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            ToolTip="WO yang sudah diberi tanggal DeadLine oleh Manager MTN">&nbsp; Total WO</asp:Label>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <asp:Label ID="LKoma2" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold">:</asp:Label>
                                                    </td>
                                                    <td style="width: 10%">
                                                         <asp:Label ID="txtTotal" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold"></asp:Label>
                                                   
                                                        <%--<asp:TextBox ID="" runat="server" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"
                                                            BorderStyle="None"></asp:TextBox>--%>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 73%">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td style="width: 15%">
                                                        <asp:Label ID="LTarget" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="true" ToolTip="WO sudah Selesai sesuai Target">&#160; Tercapai</asp:Label>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <asp:Label ID="LKoma3" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="true">:</asp:Label>
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:Label ID="txtTarget" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold" Visible="true"></asp:Label>
                                                        <%--<asp:TextBox ID="" runat="server" BorderStyle="None" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>--%>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 73%">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td style="width: 15%">
                                                        <asp:Label ID="LPersen" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="true">&#160; Persen Pencapaian</asp:Label>
                                                    </td>
                                                    <td style="width: 2%">
                                                        <asp:Label ID="LKoma4" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold"
                                                            Visible="true">:</asp:Label>
                                                    </td>
                                                    <td style="width: 10%">
                                                        <asp:Label ID="TxtPersen" runat="server" Style="font-family: Calibri; font-size: x-small; font-weight: bold" Visible="true"></asp:Label>
                                                       
                                                        <%--<asp:TextBox ID="" runat="server" BorderStyle="None" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"
                                                            ToolTip="( Selesai sesuai target / Total WO ) x 100"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="width: 73%">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
