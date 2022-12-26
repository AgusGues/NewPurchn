<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewFormTPP.aspx.cs" Inherits="GRCweb1.Modul.TPP.NewFormTPP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="bdp" %>

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
    <script type="text/javascript">
        function OpenDialog(idTPP) {
            params = 'dialogWidth:840px';
            params += '; dialogHeight:175%'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileTPP.aspx?ba=" + idTPP, "UploadFileTPP", params);
            return false;
        };

        function PreviewPDF(idTPP) {
            params = 'dialogWidth:890px';
            params += '; dialogHeight:600px'
            params += '; top=0, left=0'
            params += '; resizable:yes'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/PdfPreviewTPP.aspx?ba=" + idTPP, "Preview", params);
        };

        function openWindow(id) {
            window.showModalDialog("../../ModalDialog/PopUpCustomer.aspx?p=", " ", "resizable:yes;dialogHeight: 100px; dialogWidth: 600px;scrollbars:no;");
        }

    </script>
    <style type="text/css">
        .button {
            background: url(../../../../images/file_edit.png) no-repeat;
            cursor: pointer;
            border: none;
        }

        .auto-style1 {
            height: 18px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="table-responsive" style="width: 100%">
        <table style="width: 100%; border-top-style: solid; border-collapse: collapse;">
            <tr>
                <td>
                    <asp:Panel ID="PanelJ" runat="server">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width: 50%">
                                    <asp:Label ID="LJudul" runat="server" Text="Input Data TPP"></asp:Label>
                                </td>
                                <td style="width: 37px">
                                    <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                        type="button" value="Baru" onserverclick="BtnNew_ServerClick" />
                                </td>
                                <td style="width: 75px">
                                    <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Simpan" onserverclick="BtnUpdate_ServerClick" />
                                </td>
                                <td style="width: 5px">
                                    <%--<input id="btnLampiran" runat="server" style="background-color: white; font-weight: bold;
                                    font-size: 11px; width: 61px;" type="button" value="Lampiran" />--%>
                                    <input id="btnLampiran"
                                        runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                        type="button" value="Lampiran" onserverclick="BtnLampiran_ServerClick" />
                                </td>
                                <td style="width: 5px">
                                    <input id="btnPrint" runat="server" onserverclick="BtnPrint_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                        type="button" value="Print" /></td>
                                <td style="width: 5px">
                                    <input id="btnList" runat="server" onserverclick="BtnList_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                        type="button" value="List TPP" /></td>
                                <td style="width: 70px">&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                        <asp:ListItem Value="LaporanNo">Laporan No</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                                </td>
                                <td style="width: 3px">
                                    <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="PanelForm" runat="server" ScrollBars="Auto">
                        <table style="width: 100%; border-top-style: solid; border-collapse: collapse;">
                            <tr>
                                <td colspan="2" style="border: thin none #000000;"></td>
                            </tr>
                            <tr>
                                <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000;"
                                    width="75%">PT. BANGUNPERKASA ADHITAMASENTRA
                                </td>
                                <td align="right" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000; border-bottom: thin none #000000; font-size: x-small;"
                                    width="25%">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000;"
                                    width="75%">
                                    <b><span style="font-size: x-large">TINDAKAN PERBAIKAN DAN PENCEGAHAN</span></b>
                                </td>
                                <td style="border: thin solid #000000;" width="25%" align="right">Laporan No.
                                <asp:TextBox ID="TxtLaporanNo" runat="server" Width="167px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;"
                                    colspan="2">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="border: thin solid #000000; width: 25%;" width="10%" colspan="2">Diterbitkan :
                                                </td>
                                                <td style="border: thin solid #000000; border-collapse: collapse" width="75%">Asal Permasalahan :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" width="15%">Tgl Terbit TPP
                                                </td>
                                                <td style="border: thin solid #000000" width="15%">&nbsp;
                                                <bdp:BDPLite ID="txtTPP_Date0" runat="server" DateFormat="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Width="85%" Enabled="False">
                                                </bdp:BDPLite>
                                                </td>
                                                <td style="border-collapse: collapse" width="75%" rowspan="4">
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td width="40%" colspan="2" style="width: 50%">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="PanelRB1" runat="server">
                                                                                    1. Audit External
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButton ID="Rb1" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb1_CheckedChanged"
                                                                                    Text=" " />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Panel ID="PanelRB2" runat="server">
                                                                                    / Internal
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButton ID="Rb2" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb2_CheckedChanged"
                                                                                    Text=" " />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRB4" runat="server">
                                                                        3. NCR Customer Complain
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb4" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb4_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Panel ID="PanelRb7" runat="server">
                                                                        4. NCR Customer Complain Non Mutu
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <asp:RadioButton ID="Rb7" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb7_CheckedChanged"
                                                                        Text=" " /></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%" colspan="2" style="width: 50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Klausul No :&nbsp;
                                                                <asp:TextBox ID="txtKlausulNo" runat="server" AutoPostBack="True"
                                                                    BorderStyle="Groove" Height="20px"
                                                                    Width="182px"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                                        FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetKlausulNo"
                                                                        ServicePath="AutoComplete.asmx" TargetControlID="txtKlausulNo">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <input id="btnKlausul" runat="server" onserverclick="btnKlausul_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 20px;"
                                                                        title="List Klausul No" type="button"
                                                                        value="..." />
                                                                    <asp:Panel ID="Panelklausul" runat="server" Width="100%" Wrap="False"
                                                                        Visible="False" Height="100px" ScrollBars="Vertical">

                                                                        <asp:GridView ID="GridKlausul" runat="server" AutoGenerateColumns="False"
                                                                            OnRowCommand="GridKlausul_RowCommand" PageSize="25" Width="100%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Klausul_No" HeaderText="Klausul_No" />
                                                                                <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" />
                                                                                <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                            </Columns>
                                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                                                BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                                                ForeColor="Gold" />
                                                                            <PagerStyle BorderStyle="Solid" />
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                        </asp:GridView>

                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRb5" runat="server">
                                                                        5. Kecelakaan Kerja
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb5" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb5_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%" style="height: 23px">
                                                                    <asp:Panel ID="PanelRb1a" runat="server">
                                                                        &nbsp;&nbsp;&nbsp; a. Non conformity (Major)
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%" style="height: 23px">
                                                                    <asp:RadioButton ID="Rb1a" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="Rb1a_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                                <td width="40%" style="height: 23px">&nbsp;&nbsp;&nbsp; <span style="text-decoration: underline">Khusus kecelakaan kerja&nbsp;</span>
                                                                </td>
                                                                <td width="10%" style="height: 23px"></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRb1b" runat="server">
                                                                        &nbsp;&nbsp;&nbsp; b. Area of concern&nbsp;(Minor)
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb1b" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="Rb1b_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                                <td width="40%">&nbsp;&nbsp;&nbsp; Harus ada BA (Berita Acara)&nbsp;
                                                                </td>
                                                                <td width="10%">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRb1c" runat="server">
                                                                        &nbsp;&nbsp;&nbsp; c. Opportunity for improvement (rekomendasi)
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb1c" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="Rb1c_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRb6" runat="server" Visible="False">
                                                                        6. Lain-lain
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb6" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb6_CheckedChanged"
                                                                        Text=" " Visible="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <asp:Panel ID="PanelRb3" runat="server">
                                                                        2. NCR Proses
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:RadioButton ID="Rb3" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="Rb3_CheckedChanged"
                                                                        Text=" " />
                                                                </td>
                                                                <td width="40%">&nbsp;<asp:TextBox ID="txtKeterangan" runat="server" Visible="False" Width="100%"></asp:TextBox></td>
                                                                <td width="10%">&nbsp;
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" width="10%">Tgl Kejadian / Tgl Audit
                                                </td>
                                                <td style="border: thin solid #000000" width="20%">&nbsp;&nbsp;<bdp:BDPLite ID="txtTPP_Date" runat="server" DateFormat="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Width="95%">
                                                </bdp:BDPLite>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" width="10%">Departemen
                                                </td>
                                                <td style="border: thin solid #000000" width="15%">&nbsp;
                                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                                    Width="90%" Enabled="False">
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" width="10%">Bagian / Section / Area</td>
                                                <td style="border: thin solid #000000" width="15%">&nbsp;&nbsp;
                                                <asp:DropDownList ID="ddlBagian" runat="server"
                                                    OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" Width="90%">
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td style="border: thin none #000000" width="25%">Uraian Ketidaksesuaian :
                                                </td>
                                                <td align="right">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtUTSesuai" runat="server" Height="47px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="PanelAudite" runat="server" Visible="False">
                                                        <table style="width: 100%; border-collapse: collapse;">
                                                            <tr>
                                                                <td align="left" colspan="2" style="border: thin solid #000000; width: 40%;" width="20%">Kolom Persetujuan Khusus Audit :
                                                                </td>
                                                                <td align="center" style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;"
                                                                    width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="border: thin solid #000000" width="20%">&nbsp;Auditee
                                                                </td>
                                                                <td align="center" style="border: thin solid #000000" width="20%">Auditor
                                                                </td>
                                                                <td align="center" style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;"
                                                                    width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="border: thin solid #000000" width="20%">
                                                                    <asp:Label ID="LMgr0" runat="server" Text="Open"></asp:Label>
                                                                </td>
                                                                <td align="center" style="border: thin solid #000000" width="20%">
                                                                    <asp:Label ID="LAuditor0" runat="server" Text="Open"></asp:Label>
                                                                </td>
                                                                <td align="center" style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;"
                                                                    width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                                <td align="center" style="border: thin solid #FFFFFF" width="20%">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                    <asp:Panel ID="Panel7" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="7" class="auto-style1">Analisa Penyebab :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">&nbsp;
                                                </td>
                                                <td width="40%">
                                                    <i>Isi Ketidaksesuaian (ringkas) :</i>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" style="height: 25px">&nbsp;
                                                </td>
                                                <td width="10%" style="height: 25px">
                                                    <asp:Panel ID="Panel13" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkManusia" runat="server" AutoPostBack="True" OnCheckedChanged="chkManusia_CheckedChanged"
                                                            Text="Manusia" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%" style="height: 25px">&nbsp;
                                                </td>
                                                <td width="10%" style="height: 25px">
                                                    <asp:Panel ID="Panel15" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMesin" runat="server" AutoPostBack="True" OnCheckedChanged="chkMesin_CheckedChanged"
                                                            Text="Mesin" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%" style="height: 25px">&nbsp;
                                                </td>
                                                <td width="10%" style="height: 25px">&nbsp;
                                                </td>
                                                <td rowspan="5" width="40%">
                                                    <asp:TextBox ID="txtITSesuai" runat="server" Height="88px" TextMode="MultiLine"
                                                        Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" rowspan="3">&nbsp;
                                                <asp:Panel ID="Panel14" runat="server" Height="25px" HorizontalAlign="Center"
                                                    Width="95px">
                                                    <asp:CheckBox ID="chkLingkungan" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkLingkungan_CheckedChanged" Text="Lingkungan" />
                                                </asp:Panel>
                                                    &nbsp;
                                                </td>
                                                <td align="center" width="10%">
                                                    <asp:Image ID="Image4" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td align="center" width="10%">
                                                    <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/images/Panah Lurus.jpg"
                                                        Width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%" align="center">
                                                    <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%" align="center">
                                                    <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">
                                                    <asp:Panel ID="Panel17" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMaterial" runat="server" AutoPostBack="True" OnCheckedChanged="chkMaterial_CheckedChanged"
                                                            Text="Material" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">
                                                    <asp:Panel ID="Panel16" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMetode" runat="server" AutoPostBack="True" OnCheckedChanged="chkMetode_CheckedChanged"
                                                            Text="Metode" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td colspan="6">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">Mesin
                                                </td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtMesin" runat="server" Enabled="False" Height="44px" OnTextChanged="txtMesin_TextChanged"
                                                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">Manusia
                                                </td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtManusia" runat="server" Height="44px" TextMode="MultiLine" Width="100%"
                                                        Enabled="False" OnTextChanged="txtManusia_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">Material
                                                </td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtMaterial" runat="server" Height="44px" TextMode="MultiLine" Width="100%"
                                                        Enabled="False" OnTextChanged="txtMaterial_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">Metode
                                                </td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtMetode" runat="server" Height="44px" TextMode="MultiLine" Width="100%"
                                                        Enabled="False" OnTextChanged="txtMetode_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">Lingkungan
                                                </td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtLingkungan" runat="server" Height="44px" TextMode="MultiLine"
                                                        Width="100%" Enabled="False" OnTextChanged="txtLingkungan_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="40%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                    <asp:Panel ID="Panel8" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <input id="btnPerbaikan" runat="server" onserverclick="btnPerbaikan_ServerClick"
                                                        style="background-color: white; font-weight: bold; font-size: 11px; width: 185px;"
                                                        type="button" value="Tindakan Perbaikan" />
                                                    <asp:Panel ID="PanelPebaikan" runat="server" Height="50px" Visible="False">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="font-size: x-small" width="40%" align="center">Tindakan
                                                                </td>
                                                                <td style="font-size: x-small" align="center">Pelaku
                                                                </td>
                                                                <td style="font-size: x-small" align="center">Jadwal Selesai
                                                                </td>
                                                                <td style="font-size: x-small" align="center">&nbsp;</td>
                                                                <td style="font-size: x-small" align="center">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="font-size: x-small" width="40%">
                                                                    <asp:TextBox ID="txtTIndakan" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <asp:TextBox ID="txtPelaku" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <bdp:BDPLite ID="txtDateJS" DateFormat ="d" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal">
                                                                    </bdp:BDPLite>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">&nbsp;</td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <input id="btnAddPerbaikan" runat="server" onserverclick="btnAddPerbaikan_ServerClick"
                                                                        style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                                                        type="button" value="Simpan" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel9" runat="server" Height="80px" ScrollBars="Vertical">
                                                        <asp:GridView ID="GridPerbaikan" runat="server" AutoGenerateColumns="False" OnRowCommand="GridPerbaikan_RowCommand"
                                                            OnRowDataBound="GridPerbaikan_RowDataBound" PageSize="4" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="Verifikasi">
                                                                    <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ID">
                                                                    <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                                    <ItemStyle Width="40%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
                                                                <asp:TemplateField HeaderText="Jadwal Selesai">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateJS" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateJS" runat="server" DateFormat ="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aktual Selesai">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateAS" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateAS" runat="server" DateFormat ="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="rubah" Text="Edit" />
                                                                <asp:TemplateField HeaderText="Verifikasi">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkVerifikasi1" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkVerifikasi1_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tgl. Verifikasi">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateVF" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateVF" runat="server" DateFormat ="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="target" Text="Target" />
                                                                <asp:BoundField DataField="target" />
                                                                <asp:ButtonField CommandName="hapus" Text="Delete" />
                                                            </Columns>
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 1px">
                                                    <input id="btnPencegahann" runat="server" onserverclick="btnPencegahan_ServerClick"
                                                        style="background-color: white; font-weight: bold; font-size: 11px; width: 185px;"
                                                        type="button" value="Tindakan Pencegahan" />
                                                    &nbsp;<asp:Panel ID="PanelPencegahan" runat="server" Height="50px" Visible="False">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="font-size: x-small" width="40%" align="center">Tindakan
                                                                </td>
                                                                <td style="font-size: x-small" align="center">Pelaku
                                                                </td>
                                                                <td style="font-size: x-small" align="center">Jadwal Selesai
                                                                </td>
                                                                <td style="font-size: x-small" align="center">&nbsp;</td>
                                                                <td style="font-size: x-small" align="center">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="font-size: x-small" width="40%">
                                                                    <asp:TextBox ID="txtTIndakan0" runat="server" Width="100%"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <asp:TextBox ID="txtPelaku0" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <bdp:BDPLite ID="txtDateJS0" runat="server" DateFormat ="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal">
                                                                    </bdp:BDPLite>
                                                                </td>
                                                                <td align="center" style="font-size: x-small">&nbsp;</td>
                                                                <td align="center" style="font-size: x-small">
                                                                    <input id="btnAddPencegahan" runat="server" onserverclick="btnAddPencegahan_ServerClick"
                                                                        style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                                                        type="button" value="Simpan" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel10" runat="server" Height="80px" Width="100%" ScrollBars="Vertical">
                                                        <asp:GridView ID="GridPencegahan" runat="server" AutoGenerateColumns="False" PageSize="4"
                                                            Width="100%" OnRowCommand="GridPencegahan_RowCommand" OnRowDataBound="GridPencegahan_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="Verifikasi">
                                                                    <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ID">
                                                                    <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                                    <ItemStyle Width="40%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
                                                                <asp:TemplateField HeaderText="Jadwal Selesai">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateJS" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateJS" runat="server" DateFormat ="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Aktual Selesai">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateAS" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateAS" runat="server" DateFormat ="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="rubah" Text="Edit" />
                                                                <asp:TemplateField HeaderText="Verifikasi">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkVerifikasi0" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerifikasi0_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Tgl. Verifikasi">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LDateVF" runat="server" Text="Label"></asp:Label>
                                                                        <bdp:BDPLite ID="txtDateVF" runat="server" DateFormat="d" CssClass="style2"
                                                                            ToolTip="klik icon untuk merubah tanggal" Visible="False">
                                                                        </bdp:BDPLite>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="target" Text="Target" />
                                                                <asp:BoundField DataField="target" />
                                                                <asp:ButtonField CommandName="hapus" Text="Delete" />
                                                            </Columns>
                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                            <PagerStyle BorderStyle="Solid" />
                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PanelHSE" runat="server" Height="80px" Width="100%" ScrollBars="Vertical">
                                                        <asp:Label runat="server">Rekomendasi HSE (Khusus Kecelakaan Kerja) :</asp:Label><br />
                                                        <asp:CheckBox ID="chkhseya" runat="server" AutoPostBack="true" Text="Ada" 
                                                            oncheckedchanged="chkhseya_CheckedChanged" /><br />
                                                        <asp:TextBox ID="txthse" runat="server" Width="30%" ></asp:TextBox><br />
                                                        <asp:CheckBox ID="chkhseno" runat="server" AutoPostBack="True" Text="Tidak Ada" /><br /><br />
                                                    </asp:Panel><br />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                    <asp:Panel ID="PanelStatus" runat="server" Enabled="False">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td width="10%">STATUS :
                                                </td>
                                                <td width="7%">&nbsp;
                                                </td>
                                                <td width="5%">&nbsp;
                                                </td>
                                                <td width="7%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="15%">&nbsp;
                                                <input id="btnClose" runat="server" onserverclick="btnClose_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 42px;"
                                                    type="button" value="Close"
                                                    visible="False" />
                                                </td>
                                                <td align="left" colspan="2" style="text-decoration: underline">Khusus untuk audit :
                                                </td>
                                                <td align="left" colspan="2" style="text-decoration: underline">
                                                    <input id="btnSolve" runat="server" align="right" onserverclick="btnSolve_ServerClick"
                                                        style="background-color: white; font-weight: bold; font-size: 11px; width: 42px;"
                                                        type="button" value="Solved" visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td colspan="4">Tutup Kasus
                                                <asp:CheckBox ID="chkClose" runat="server" Text="  " AutoPostBack="True" OnCheckedChanged="chkClose_CheckedChanged" />
                                                    Tanggal :
                                                <bdp:BDPLite ID="txtDateTKasus" runat="server" DateFormat="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Enabled="False">
                                                    <TextBoxStyle Font-Size="X-Small" />
                                                </bdp:BDPLite>
                                                </td>
                                                <td width="15%">&nbsp;
                                                </td>
                                                <td width="7%" style="width: 12%">Solved
                                                <asp:CheckBox ID="chksolved" runat="server" Text="  " AutoPostBack="True" OnCheckedChanged="chksolved_CheckedChanged" />
                                                </td>
                                                <td width="7%" colspan="2">Tanggal :
                                                </td>
                                                <td width="10%">
                                                    <bdp:BDPLite ID="txtDateSolved" runat="server" DateFormat="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Enabled="False">
                                                    </bdp:BDPLite>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="7%">&nbsp;
                                                </td>
                                                <td width="5%">&nbsp;
                                                </td>
                                                <td width="7%">&nbsp;
                                                </td>
                                                <td width="10%">&nbsp;
                                                </td>
                                                <td width="15%">&nbsp;
                                                </td>
                                                <td colspan="3" style="width: 20%" width="10%">Tanggal waktu / due date Tanggal :
                                                </td>
                                                <td width="15%">
                                                    <bdp:BDPLite ID="txtDueDate" runat="server" DateFormat="d" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Enabled="False">
                                                    </bdp:BDPLite>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                    <asp:Panel ID="Panel12" runat="server">
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td colspan="4">Kolom Persetujuan / Pelaksanaan :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" align="center" width="20%">&nbsp; Spv /Kasie Dept.
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">&nbsp; Manager Dept.
                                                </td>
                                                <td style="border-style: solid; border-width: thin; border-color: #FFFFFF #000000 #FFFFFF #000000;"
                                                    align="center" width="20%">&nbsp;
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">&nbsp; Auditor (Khusus Audit)
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">&nbsp; Corp. ISO Manager
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border: thin solid #000000" align="center" width="20%">
                                                    <asp:Label ID="LSpv" runat="server" Text="Open"></asp:Label>
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">
                                                    <asp:Label ID="LMgr" runat="server" Text="Open"></asp:Label>
                                                </td>
                                                <td style="border-style: solid; border-width: thin; border-color: #FFFFFF #000000 #FFFFFF #000000;"
                                                    align="center" width="20%">
                                                    <asp:Label ID="LPMgr" runat="server" Text="Open" Visible="False"></asp:Label>
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">
                                                    <asp:Label ID="LAuditor" runat="server" Text="Open"></asp:Label>
                                                </td>
                                                <td style="border: thin solid #000000" align="center" width="20%">
                                                    <asp:Label ID="LMR" runat="server" Text="Open"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                    <asp:Panel ID="PanelLampiran" runat="server" ScrollBars="Auto" Height="570px" Visible="False">
                        <table style="width: 100%;">
                            <tr>
                                <td>Laporan No :
                                <asp:Label ID="LblLaporanNo" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td align="right">
                                    <input id="btnUpload0" runat="server" onserverclick="btnUpload0_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 131px;"
                                        type="button" value="Refresh Data" /><input
                                            id="btnUpload" runat="server" onserverclick="btnUpload_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 131px;"
                                            type="button" value="Tambah Lampiran" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridLampiran" runat="server" Height="100%" Width="100%" AutoGenerateColumns="False"
                                        OnRowCommand="GridLampiran_RowCommand"
                                        OnRowDataBound="GridLampiran_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="FIleName" HeaderText="Nama File" />
                                            <asp:BoundField DataField="TanggalUpload" HeaderText="Tanggal Upload" />
                                            <asp:ButtonField CommandName="lihat" Text="Preview" />
                                            <asp:ButtonField CommandName="hapus" Text="Hapus" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                    <asp:Panel ID="PanelList" runat="server" ScrollBars="Auto" Height="570px" Visible="False">
                        <table style="width: 100%;">
                            <tr>
                                <td align="right">Filter Data By
                                </td>
                                <td align="left" title="Departemen ">&nbsp;
                                <asp:Label ID="LDept" runat="server" Text="Departemen "></asp:Label>
                                    <asp:DropDownList ID="ddlDeptName0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                        Width="125px">
                                    </asp:DropDownList>
                                    &nbsp;Status&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                        Width="125px">
                                        <asp:ListItem>ALL</asp:ListItem>
                                        <asp:ListItem Value="0">Open</asp:ListItem>
                                        <asp:ListItem Value="1">Close</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp; Bulan
                                 <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                     Width="125px">
                                     <asp:ListItem Value="0">ALL</asp:ListItem>
                                     <asp:ListItem Value="1">Januari</asp:ListItem>
                                     <asp:ListItem Value="2">Februari</asp:ListItem>
                                     <asp:ListItem Value="3">Maret</asp:ListItem>
                                     <asp:ListItem Value="4">April</asp:ListItem>
                                     <asp:ListItem Value="5">Mei</asp:ListItem>
                                     <asp:ListItem Value="6">Juni</asp:ListItem>
                                     <asp:ListItem Value="7">Juli</asp:ListItem>
                                     <asp:ListItem Value="8">Agustus</asp:ListItem>
                                     <asp:ListItem Value="9">September</asp:ListItem>
                                     <asp:ListItem Value="10">Oktober</asp:ListItem>
                                     <asp:ListItem Value="11">November</asp:ListItem>
                                     <asp:ListItem Value="12">Desember</asp:ListItem>
                                 </asp:DropDownList>
                                    &nbsp; Tahun
                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                    Width="125px">
                                </asp:DropDownList>
                                    &nbsp; Asal Permasalahan
                                <asp:DropDownList ID="ddlMasalah" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" Width="225px">
                                </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GridTPP" runat="server" AllowPaging="false" AutoGenerateColumns="False" Visible="true"
                                        OnPageIndexChanging="GridTPP_PageIndexChanging" OnRowCommand="GridTPP_RowCommand"
                                        OnRowDataBound="GridTPP_RowDataBound" PageSize="60" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Closed" />
                                            <asp:BoundField DataField="Departemen" HeaderText="Departemen" />
                                            <asp:BoundField DataField="TPP_Date" DataFormatString="{0:d}" HeaderText="TPP_Date" />
                                            <asp:BoundField DataField="Laporan_No" HeaderText="Laporan_No" />
                                            <asp:BoundField DataField="Ketidaksesuaian" HeaderText="Ketidaksesuaian" />
                                            <asp:BoundField DataField="Uraian" HeaderText="Uraian" />
                                            <asp:BoundField DataField="Asal_Masalah" HeaderText="Asal_Masalah" />
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="LStatus" runat="server" Text="Label"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="KetStatus" HeaderText="KetStatus" />
                                            <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                            <asp:BoundField DataField="Due_Date" DataFormatString="{0:d}" HeaderText="Due Date" />
                                            <asp:ButtonField CommandName="Add" Text="Pilih" />

                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <br />
                                    <%-- <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="1" visible="false">
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width: 1%">
                                        </th>
                                        <th class="kotak" style="width: 1%">
                                            Departemen
                                        </th>
                                        <th class="kotak" style="width: 2%">
                                            TPP_Date
                                        </th>
                                        <th class="kotak" style="width: 2%">
                                            Laporan_No
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            Ketidaksesuaian
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            Uraian
                                        </th>
                                        <th class="kotak" style="width: 2%">
                                            Asal_Masalah
                                        </th>
                                         <th class="kotak" style="width: 2%">
                                            Status
                                        </th>
                                         <th class="kotak" style="width: 2%">
                                            Status
                                        </th>
                                         <th class="kotak" style="width: 2%">
                                            Approval
                                        </th>
                                         <th class="kotak" style="width: 2%">
                                           Due Date
                                        </th>
                                    </tr>
                                     <tbody>
                                         <asp:Repeater ID="GridTPPx" runat="server" OnItemDataBound="GridTPPx_DataBound" OnItemCommand="GridTPPx_Command">
                                             <ItemTemplate>
                                                 <tr class="EvenRows baris">
                                                     <td>
                                                         <%# Eval("Closed")%>
                                                     </td>
                                                     <td>
                                                        <%# Eval("Departemen")%>
                                                     </td>
                                                 </tr>
                                                  <tr class="Line3" style="height: 24px">
                                                            <td class="kotak tengah" colspan="11"> </td>
                                                        </tr>
                                             </ItemTemplate>
                                         </asp:Repeater>
                                     </tbody>
                                 </table>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
            </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
