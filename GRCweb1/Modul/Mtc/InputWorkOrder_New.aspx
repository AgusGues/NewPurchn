<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputWorkOrder_New.aspx.cs" Inherits="GRCweb1.Modul.Mtc.InputWorkOrder_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function PreviewWO(IDLampiran) {
            MyPopUpWin("../../ModalDialog/PDFPreviewWO.aspx?wrk=" + IDLampiran, 900, 800)
        };

        function confirm_batal() {
            if (confirm("Anda yakin untuk Cancel ?") == true)
                //window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
                MyPopUpWin("../../ModalDialog/ReasonCancel.aspx", 830, 200)
            else
                return false;
        }
    </script>

    <script language="JavaScript" type="text/javascript">
        function Blink(controlId) {
            var control = document.getElementById(controlId);
            control.style.color = control.style.color == 'white' ? 'black' : 'white';
        }
        function CallBlink(controlId) {
            setInterval(function () { Blink(controlId); }, 800);

        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed; border-collapse: collapse" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader" width="100%'">
                                    <tr style="height: 49px">
                                        <td style="width: 100%"><strong>&nbsp;&nbsp;
                                            <asp:Label ID="LabelJudul" runat="server" Visible="false" Style="font-family: Calibri; font-size: medium; font-weight: bold"></asp:Label>
                                        </td>


                                        <td style="width: 37px">
                                            <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" OnClick="btnPrev_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <td style="width: 37px">
                                            <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <td style="width: 37px">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnUpdate1" runat="server" Text="Update" OnClick="btnUpdate1_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnUpdateTarget" runat="server" Text="Update" OnClick="btnUpdateTarget_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnFinish" runat="server" Text="Update" OnClick="btnFinish_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnUnApprove" runat="server" Text="Not Approve" OnClick="btnUnApprove_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <td style="width: 37px">
                                            <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <td style="width: 53px">
                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan"
                                                OnClick="btnSimpan_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <%--<td style="width: 31px">
                                            <asp:Button ID="btnDelete" runat="server" Text="Hapus" 
                                                OnClick="btnDelete_ServerClick" 
                                                style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>--%>
                                        <td style="width: 37px">
                                            <asp:Button ID="btnList" runat="server" Text="List WO" OnClick="btnList_ServerClick"
                                                Style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>

                                        <td style="width: 23px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px"
                                                Style="font-family: Calibri; font-size: x-small">
                                                <asp:ListItem Value="NoWO">No WO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"
                                                Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: x-small; font-family: Calibri;"
                                                type="button" value="Cari"
                                                onserverclick="btnSearch_ServerClick" />
                                        </td>
                                        <td style="width: 15px">
                                            <asp:HiddenField ID="noWO" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <%-- end of header panel --%>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100%" bgcolor="#CCCCCC">
                                <%--Panel Share WO--%>
                                <asp:Panel ID="PaneLShare" runat="server" Visible="false"
                                    Style="font-family: Calibri; background-color: #00CC00;" ForeColor="#00CC00">
                                    <table style="width: 100%">
                                        <tr style="width: 100%">
                                            <td style="width: 30%; font-size: x-small;">
                                                <asp:Label ID="LabelShare" runat="server" Style="font-family: 'Agency FB'; font-size: small; font-weight: 700"
                                                    Visible="false" BackColor="#00CC00">&nbsp;</asp:Label>
                                            </td>
                                            <td style="height: 3px; width: 30%;" valign="top">
                                                <asp:RadioButton ID="RBShare" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBShare_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; font-style: italic; background-color: #00CC00; color: #000066;"
                                                    Text="....Untuk melihatnya silahkan klik di lingkaran ini >>"
                                                    TextAlign="Left" Width="300px" />
                                            </td>
                                            <td style="height: 3px; width: 40%;" valign="top"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Share WO--%>

                                <%--Panel Tidak ada record--%>
                                <asp:Panel ID="PaneLinfo" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table10" style="width: 100%;">
                                        <tr>
                                            <td style="width: 80%; font-family: 'Courier New', Courier, monospace; font-size: large; color: #00FF00;">
                                                <asp:Label ID="LabelInfo" runat="server" Visible="False" Style="font-family: Calibri; font-size: large; font-weight: bold; color: #000066; text-align: center;"
                                                    BackColor="#00CC00" Width="100%">&nbsp; NO RECORD FOUND </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Tidak ada record--%>

                                <%--Panel Warning --%>
                                <asp:Panel ID="PaneLWarning" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table15" style="width: 100%;">
                                        <tr>
                                            <td style="width: 80%; font-family: 'Courier New', Courier, monospace; font-size: large; color: #00FF00;">
                                                <asp:Label ID="LabelWarning" runat="server" Visible="False" Style="font-family: Calibri; font-size: large; font-weight: bold; color: #000066; text-align: center;"
                                                    BackColor="#00CC00" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Warning--%>

                                <%--Panel Satu--%>
                                <asp:Panel ID="PanelSatu" runat="server" Visible="true" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table1" style="width: 100%;">

                                        <tr style="width: 100%">
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelNoWO" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; No WO</asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txtNoWO" runat="server" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                                        <asp:TextBox ID="txtCount" runat="server" ReadOnly="true" Style="font-family: Calibri; font-size: x-small; color: #000000;"
                                                            Width="38px"></asp:TextBox>
                                            </td>

                                            <td style="width: 10%">
                                                <asp:Label ID="LabelIDWO" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold"></asp:Label>
                                            </td>

                                            <td style="width: 10%">
                                                <asp:Label ID="LabelTipe" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Tipe WO</asp:Label>
                                            </td>

                                            <td style="width: 42%">
                                                <asp:TextBox ID="txtTipe" runat="server" Visible="false" ReadOnly="true" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                                <%--End Panel Satu--%>

                                <%--Panel Konfirmasi--%>
                                <asp:Panel ID="PanelKonfirmasi" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table9" style="width: 100%;">
                                        <tr style="width: 100%">
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPlant" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Plant Terkait</asp:Label>
                                            </td>
                                            <td style="width: 20%" colspan="2">
                                                <asp:TextBox ID="txtPlant" runat="server" Visible="false" ReadOnly="true" Width="50%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="LabelKonfirmasi" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Status Share WO</asp:Label>
                                            </td>
                                            <td style="width: 10%" colspan="2">
                                                <asp:TextBox ID="txtKonfirmasi" runat="server" Visible="false" ReadOnly="true" Width="50%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                            <td style="width: 42%">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Konversi--%>

                                <%--Panel Dua--%>
                                <asp:Panel ID="PanelDua" runat="server" Visible="true" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table2" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPeminta" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Dept Pemohon</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:TextBox ID="txtPeminta" runat="server" Visible="false" ReadOnly="true" Width="30%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelTglBuat" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Buat</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:TextBox ID="txtTglBuat" runat="server" Visible="false" ReadOnly="true" Width="20%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelTglDisetujui" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Disetujui</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:TextBox ID="txtTglDisetujui" runat="server" Visible="false" ReadOnly="true"
                                                    Width="20%" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Dua--%>

                                <%--Panel Pilih Dept--%>
                                <asp:Panel ID="PanelPilihDept" runat="server" Visible="true" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table6" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPDept" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Dept Penerima</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:DropDownList ID="ddlPDept" runat="server" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPDept_SelectedIndexChanged"
                                                    Height="16px" Style="font-family: Calibri; font-size: x-small" Width="20%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Area Kerja--%>

                                <%--Panel Area Kerja IT1--%>
                                <asp:Panel ID="PanelTipe" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table14" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelTipeWO" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Tipe WO</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipeWO" runat="server" Visible="false" AutoPostBack="true"
                                                    Style="font-family: Calibri">
                                                    <asp:ListItem Value="0">-- Pilih Tipe --</asp:ListItem>
                                                    <asp:ListItem Value="1">Urgent</asp:ListItem>
                                                    <asp:ListItem Value="2">Sesuai Schedulle</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>
                                <%--End Panel Area Kerja1--%>

                                <%--Panel Area Kerja IT--%>
                                <asp:Panel ID="PanelIT" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table7" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelJenis" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Jenis Pekerjaan</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="tblForm" id="Table8" style="width: 100%;">

                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="RBH" runat="server" AutoPostBack="True" OnCheckedChanged="RBH_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                                    Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hardware" TextAlign="Left" Width="129px" />

                                                <asp:RadioButton ID="RBS" runat="server" AutoPostBack="True" OnCheckedChanged="RBS_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; text-align: left; font-style: italic;"
                                                    Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Software" TextAlign="Left" Width="120px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Area Kerja--%>

                                <%--Panel Area Kerja IT2--%>
                                <asp:Panel ID="PanelHardware" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table12" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelHardware" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Permintaan</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlHardware" runat="server" AutoPostBack="True" Visible="false"
                                                    OnSelectedIndexChanged="ddlHardware_SelectedIndexChanged"
                                                    Style="font-family: Calibri">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Area Kerja IT2--%>

                                <%--Panel Area Kerja IT3--%>
                                <asp:Panel ID="PanelSoftware" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table13" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelSoftware" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Permintaan</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSoftware" runat="server" AutoPostBack="True" Visible="false"
                                                    OnSelectedIndexChanged="ddlSoftware_SelectedIndexChanged"
                                                    Style="font-family: Calibri">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Area Kerja IT3--%>

                                <%--Panel Area Kerja--%>
                                <asp:Panel ID="PanelAreaKerja" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table5" style="width: 100%;">
                                        <tr style="width: 100%">
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelAreaKerja" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Area Pekerjaan</asp:Label>
                                            </td>
                                            <td style="width: 10%" colspan="2">
                                                <asp:DropDownList ID="ddlArea" runat="server" Visible="false" AutoPostBack="True"
                                                    Height="16px" Style="font-family: Calibri; font-size: x-small" Width="100%" OnSelectedIndexChanged="ddlArea_Change">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 30%" rowspan="4">
                                                <asp:TextBox ID="txtPermintaan" runat="server" Visible="false" ReadOnly="true" Width="90%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%">&nbsp;
                                            </td>
                                            <td style="width: 32%">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Area Kerja--%>

                                <%--Panel Sub Area Kerja--%>
                                <asp:Panel ID="PanelSubArea" runat="server" Visible="false" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table11" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelSubArea" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Sub Area</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:DropDownList ID="ddlSubArea" runat="server" Visible="false" AutoPostBack="True" Height="16px" Style="font-family: Calibri; font-size: x-small"
                                                    Width="20%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Sub Area Kerja--%>

                                <%--Panel Tiga--%>
                                <asp:Panel ID="PanelTiga" runat="server" Visible="true" Style="font-family: Calibri">
                                    <table class="tblForm" id="Table3" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelUraian" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Uraian Order Pekerjaan</asp:Label>
                                            </td>
                                            <td style="" colspan="4">
                                                <asp:TextBox ID="txtUraian" runat="server" TextMode="MultiLine" Width="90%" ReadOnly="false"
                                                    Height="78px" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Tiga--%>

                                <%--Panel Empat--%>
                                <asp:Panel ID="PanelEmpat" runat="server" Visible="true" Style="font-family: Calibri">
                                    <table id="Table4" style="width: 100%;">
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelDept" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-weight: bold">&nbsp; Dept Penerima</asp:Label>
                                            </td>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelTargetSelesai" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Target Selesai</asp:Label>
                                            </td>
                                            <td style="width: 50%" colspan="2">
                                                <asp:TextBox ID="txtTargetSelesai" runat="server" Visible="false" ReadOnly="false"
                                                    Width="25%" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>

                                                <asp:Label ID="LabelWajibisi1" runat="server" Visible="false" Style="font-family: Calibri; font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                            </td>
                                            <td style="width: 35%">
                                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTargetSelesai" Format="dd-MMM-yyyy"
                                                    runat="server"></cc1:CalendarExtender>
                                            </td>
                                        </tr>

                                        <%--Pilihan Pelaksana bukan DDL--%>
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPelaksana" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Pelaksana</asp:Label>
                                            </td>
                                            <td style="width: 100%; float: right; overflow: auto;" colspan="2">

                                                <asp:TextBox ID="txtPelaksana" runat="server" Visible="false" ReadOnly="true" Width="1%"
                                                    Style="font-family: Calibri; font-size: x-small;"
                                                    Wrap="True"></asp:TextBox>
                                                <asp:TextBox ID="txtPelaksana5" runat="server" Visible="false" ReadOnly="true"
                                                    Width="30%" Style="font-family: Calibri"></asp:TextBox>


                                                <asp:Label ID="LabelWajibisi2" runat="server" Visible="false" Style="font-family: Calibri; font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                                <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>

                                            </td>
                                            <td colspan="2"></td>
                                        </tr>

                                        <%--Pilihan Pelaksana DDL--%>
                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPelaksana2" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Pelaksana</asp:Label>
                                            </td>
                                            <td style="width: 100%" colspan="2">
                                                <asp:DropDownList ID="ddlPelaksana" runat="server" Visible="false" AutoPostBack="True"
                                                    Height="16px" Style="font-family: Calibri; font-size: x-small" Width="20%">
                                                </asp:DropDownList>
                                                <asp:Label ID="LabelWajibisi4" runat="server" Visible="false" Style="font-family: Calibri; font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 18%; height: 28px;">
                                                <asp:Label ID="LabelFinishDate" runat="server" Visible="false" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Selesai Pekerjaan</asp:Label>
                                            </td>
                                            <td style="width: 100%; height: 28px;" colspan="2">
                                                <asp:TextBox ID="txtFinishDate" runat="server" Visible="false" ReadOnly="false" Width="25%"
                                                    Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                <asp:Label ID="LabelWajibisi3" runat="server" Visible="false" Style="font-family: Calibri; font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                            </td>
                                            <td style="width: 35%; height: 28px;">
                                                <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtFinishDate" Format="dd-MMM-yyyy"
                                                    runat="server"></cc1:CalendarExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 18%">
                                                <asp:Label ID="LabelPerbaikan" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Uraian Perbaikan</asp:Label>
                                            </td>
                                            <td style="" colspan="4">
                                                <asp:TextBox ID="txtPerbaikan" runat="server" TextMode="MultiLine" Width="90%" ReadOnly="false"
                                                    Height="78px" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                <asp:Label ID="LabelWajibisiPerbaikan" runat="server" Visible="false" Style="font-family: Calibri; font-size: xx-small; font-style: italic; color: #FF0000">* Optional</asp:Label>
                                            </td>
                                        </tr>

                                        <%--<tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelTrial" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Test / Trial</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtTrial" runat="server" Visible="false" ReadOnly="true" Width="25%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 35%">
                                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTrial" Format="dd-MMM-yyyy"
                                                            runat="server">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>--%>
                                        <%--<tr style="width: 100%">
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelHasilTrial" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Hasil Test / Trial</asp:Label>
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:RadioButton ID="RB1" runat="server" Visible="false" AutoPostBack="True" GroupName="a"
                                                            OnCheckedChanged="RB1_CheckedChanged" Style="font-family: Calibri; font-size: x-small;
                                                            text-align: left; font-style: italic;" Text="Hasil sesuai yg diharapkan" TextAlign="Left"
                                                            Width="50%" />
                                                    </td>
                                                    <td style="width: 35%">
                                                        <asp:RadioButton ID="RB2" runat="server" Visible="false" AutoPostBack="True" GroupName="a"
                                                            OnCheckedChanged="RB2_CheckedChanged" Style="font-family: Calibri; font-size: x-small;
                                                            text-align: left; font-style: italic;" Text="Perlu pekerjaan tambahan" TextAlign="Left"
                                                            Width="70%" />
                                                    </td>
                                                </tr>--%>
                                    </table>
                                </asp:Panel>
                                <%--End Panel Empat--%>
                                <%--Panel Lima--%>

                                <%--End Panel Lima--%>
                                <%--Panel Enam--%>
                                <hr />
                                <div  id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="0" id="baList" bgcolor="#999999">

                                        <thead>
                                            <tr class="tbHeader">
                                                <th style="width: 5%" class="kotak">ID</th>
                                                <th style="width: 70%" class="kotak">Nama File Lampiran</th>
                                                <th style="width: 3%">&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                <ItemTemplate>
                                                    <tr class="total baris">
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("IDLampiran")%>
                                                        </td>
                                                        <td class="kotak" nowrap="nowrap">&nbsp;&nbsp;
                                                    <%# Eval("FileName")%>
                                                        </td>
                                                        <td class="kotak angka" style="border-left: 0px" colspan="2">
                                                            <%--<asp:ImageButton ToolTip="Click to Preview Document" ID="lihat" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                        CssClass='<%# Eval("IDLampiran") %>' CommandName="pre" ImageUrl="~/images/14.png" />--%>
                                                            <asp:ImageButton ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("IDLampiran") %>'
                                                                CommandName="viewpdf" ToolTip="Click to Preview Document" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
