<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RMMNew.aspx.cs" Inherits="GRCweb1.Modul.RMM.RMMNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    if (!window.showModalDialog) {
        window.showModalDialog = function (arg1, arg2, arg3) {
            var w;
            var h;
            var resizable = "no";
            var scroll = "no";
            var status = "no";
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
        window.showModalDialog("../../ModalDialog/UploadFileSMT.aspx?smt=" + idSMT, "UploadFileSMT", params);
        return false;
    };

    function PreviewPDF(idTPP) {
        params = 'dialogWidth:890px';
        params += '; dialogHeight:600px'
        params += '; top=0, left=0'
        params += '; resizable:yes'
        params += ';scrollbars:no';
        window.showModalDialog("../../ModalDialog/PdfPreviewSMT.aspx?smt=" + idSMT, "Preview", params);
    };

    function openWindow(id) {
        window.showModalDialog("../../ModalDialog/PopUpCustomer.aspx?p=", " ", "resizable:yes;dialogHeight: 100px; dialogWidth: 600px;scrollbars:no;");
    }
    function openDialog(id) {
        window.showModalDialog('../../ModalDialog/PopUpDialog.aspx', '', 'resizable:yes;dialogHeight: 180px; dialogWidth: 500px;scrollbars=no');
    }
    function updateRMM(id) {
        window.showModalDialog("../../ModalDialog/RMMEdit.aspx?p=" + id, "RMM Update", "resizable:yes;dialogHeight: 400px; dialogWidth: 517px;scrollbars:no;");
    }

</script>
<style type="text/css">
    .button {
        background: url(../../../../images/file_edit.png) no-repeat;
        cursor: pointer;
        border: none;
    }

    .style6 {
        width: 12%;
    }

    label {
        font-weight: 400;
        font-size: 12px;
    }

    .auto-style1 {
        width: 179px;
        height: 39px;
    }

    .auto-style2 {
        width: 374px;
        height: 39px;
    }

    .auto-style3 {
        width: 102px;
        height: 39px;
    }

    .auto-style4 {
        height: 39px;
    }

    .auto-style5 {
        width: 205px;
        height: 39px;
    }
</style>
   <asp:UpdatePanel ID="notRefresh" runat="server">
       <ContentTemplate>

       
<div class="table-responsive" style="width:100%">
    <table style="width: 100%; border-top-style: solid; border-collapse: collapse;">
        <tr>
            <td>
                <asp:Panel ID="PanelJ" runat="server">
                <table class="nbTableHeader">
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="LJudul" runat="server" Text="Input RMM"></asp:Label>
                        </td>
                        <td style="width: 37px">
                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                            font-size: 11px; width: 61px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                        </td>
                        <td style="width: 75px">
                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                            font-size: 11px;" type="button" value="Simpan" onserverclick="BtnUpdate_ServerClick" />
                        </td>
                        <td style="width: 5px">
                            <%--<input id="btnLampiran" runat="server" style="background-color: white; font-weight: bold;
                            font-size: 11px; width: 61px;" type="button" value="Lampiran" />--%><input id="btnLampiran"
                            runat="server" style="background-color: white; font-weight: bold; font-size: 11px;
                            width: 61px;" type="button" value="Lampiran" onserverclick="btnLampiran_ServerClick" />
                        </td>
                        <td style="width: 5px">
                            <input ID="btnPrint" runat="server" onserverclick="BtnPrint_ServerClick" style="background-color: white; font-weight: bold;
                            font-size: 11px; width: 61px;" type="button" value="Print" /></td>
                            <td style="width: 5px">
                                <input ID="btnList" runat="server" onserverclick="btnList_ServerClick" style="background-color: white; font-weight: bold;
                                font-size: 11px; width: 61px;" type="button" value="List RMM" /></td>
                                <td style="width: 70px">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                    <asp:ListItem Value="RMM_No">No RMM</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox>
                            </td>
                            <td style="width: 3px">
                                <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="PanelForm" runat="server" ScrollBars="Auto">
                <table style="width: 100%; ">
                    <tr>
                        <td style="width: 249px;"></td>
                        <td style="width: 374px;"></td>
                        <td style="width: 102px;"></td>
                        <td style="width: 209px;"> </td>
                        <td style="width: 205px;"></td>
                    </tr>
                    <tr>
                        <td valign="top" class="auto-style1">
                            <span style="font-size: 12px">&nbsp; No RMM</span>
                        </td>
                        <td valign="top" class="auto-style2">
                            <asp:TextBox ID="txtRMM_No" runat="server" Width="50%" ReadOnly="True" Enabled="False"></asp:TextBox><asp:Label ID='idX' runat="server" Visible="false"></asp:Label>
                        </td>
                        <td valign="top" class="auto-style3">
                            <span style="font-size: 12px">&nbsp; Sumber Daya</span>
                        </td>
                        <td valign="top" class="auto-style4" colspan="2">
                            &nbsp; 
                            <asp:RadioButton ID="chkMesin" runat="server" GroupName="a" Text="Mesin" />
                            <asp:RadioButton ID="chkMetode" runat="server" GroupName="a" Text="Metode" />
                            <asp:RadioButton ID="chkManusia" runat="server" GroupName="a" Text="Manusia" />
                            <asp:RadioButton ID="chkMaterial" runat="server" GroupName="a" Text="Material" />
                            &nbsp;
                            <asp:RadioButton ID="chkLingkungan" runat="server" GroupName="a" Text="Lingkungan" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px;" valign="top">
                            <span style="font-size: 12px">&nbsp; Tanggal</span>
                        </td>
                        <td style="width: 374px;" valign="top">
                            <asp:TextBox ID="txtRMM_Date0" AutoPostBack="True" runat="server" Width="65%" AutoComplete="off" Enabled="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="ctxtRMM_Date0" runat="server" Format="dd-MM-yyyy" TargetControlID="txtRMM_Date0"></cc1:CalendarExtender>
                        </td>
                        <td style="width: 102px;" valign="top">
                            <span style="font-size: 12px">&nbsp;</span>
                        </td>
                        <td style="width: 209px;" valign="top">
                            &nbsp; 
                        </td>
                        <td style="width: 205px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px;" valign="top">
                            <span style="font-size: 12px">&nbsp; Departemen / Bagian</span>
                        </td>
                        <td style="width: 374px;" valign="top"> <asp:DropDownList ID="ddlDeptName" 
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                            Width="50%" Enabled="false">
                        </asp:DropDownList></td>
                        <td style=" width: 102px;" valign="top"><span style="font-size: 12px">&nbsp; Aktivitas</span><spanstyle="font-size: 12px">&nbsp;</span> </td> 
                        <td valign="top" rowspan="2"><asp:TextBox ID="txtAktivitas" 
                            runat="server" Width="100%" Height="50px" TextMode="MultiLine"></asp:TextBox></td>        
                            <td style="width: 205px;" valign="top"></td>    

                        </tr>
                        <tr>
                            <td style="width: 179px;" valign="top">
                                <span style="font-size: 12px">&nbsp; Dimensi</span>
                            </td>
                            <td style="width: 374px;" valign="top">
                                <asp:DropDownList ID="ddlDimensi" runat="server" OnSelectedIndexChanged="ddlDimensi_SelectedIndexChanged"
                                Width="50%" Enabled="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 205px;" valign="top">
                        </td>
                        <td style="width: 205px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 179px;" valign="top">
                            <span style="font-size: 12px">&nbsp; Sasaran Mutu Perusahaan</span>
                        </td>
                        <td style="width: 374px;" valign="top"><asp:DropDownList ID="ddlSarmutPershn" runat="server"  OnSelectedIndexChanged="ddlSarmutPershn_SelectedIndexChanged"
                            Enabled="true">
                        </asp:DropDownList></td>
                        <td style=" width: 102px;" valign="top">
                            <span style="font-size: 12px">&nbsp; Pelaku</span>
                        </td>
                        <td style="width: 209px;" valign="top"><asp:TextBox ID="txtPelaku" runat="server"></asp:TextBox></td>
                        <td style="width: 205px;" valign="top"> </td>
                    </tr>
                    <tr>
                        <td style="width: 179px;" valign="top">
                            <span style="font-size: 12px">&nbsp; Sasaran Mutu Dept / Bagian</span>
                        </td>
                        <td style="width: 374px;" valign="top">
                            <asp:DropDownList ID="ddlSarmutDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSarmutDept_SelectedIndexChanged"
                            Enabled="true" Width="50%">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="true" Enabled="true" Width="30%">
                    </asp:DropDownList>
                </td>
                <td style="width: 102px;" valign="top">
                    <span style="font-size: 12px">&nbsp; Jadwal Selesai</span>
                </td>
                <td style="width: 209px;" valign="top">
                    <asp:DropDownList ID="ddlTargetM" runat="server" AutoPostBack="True" Width="90px"
                    Visible="true">
                    <asp:ListItem>Pilih Target</asp:ListItem>
                    <asp:ListItem>M1 (tiap tgl 7)</asp:ListItem>
                    <asp:ListItem>M2 (tiap tgl 14)</asp:ListItem>
                    <asp:ListItem>M3 (tiap tgl 21)</asp:ListItem>
                    <asp:ListItem>M4 (tiap akhir Bln)</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="70px" Visible="true">
                <asp:ListItem>Pilih Bulan</asp:ListItem>
                <asp:ListItem>Januari</asp:ListItem>
                <asp:ListItem>Februari</asp:ListItem>
                <asp:ListItem>Maret</asp:ListItem>
                <asp:ListItem>April</asp:ListItem>
                <asp:ListItem>Mei</asp:ListItem>
                <asp:ListItem>Juni</asp:ListItem>
                <asp:ListItem>Juli</asp:ListItem>
                <asp:ListItem>Agustus</asp:ListItem>
                <asp:ListItem>September</asp:ListItem>
                <asp:ListItem>Oktober</asp:ListItem>
                <asp:ListItem>November</asp:ListItem>
                <asp:ListItem>Desember</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:TextBox ID="txtTahun" runat="server" BorderStyle="Groove" Height="22px" Width="40px" Visible="true"></asp:TextBox>--%>
            <asp:DropDownList ID="ddlTahunInput" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahunInput_SelectedIndexChanged" Width="15%">
                                                </asp:DropDownList>
            <cc1:CalendarExtender ID="cc1" runat="server" TargetControlID="txttgl" Format="dd-MM-yyyy">
        </cc1:CalendarExtender>
    </td>
    <td style="width: 205px;" valign="top">
    </td>
</tr>
<tr>
    <td style="width: 179px;" valign="top">
        <span style="font-size: 12px">&nbsp; </span>
    </td>
    <td style="width: 374px;" valign="top">
    </td>
    <td style="width: 102px;" valign="top">
        <span style="font-size: 12px">&nbsp; Actual Selesai</span>
    </td>
    <td style="width: 209px;" valign="top">
        
        <asp:TextBox ID="txtActualJS" AutoPostBack="True" runat="server" AutoComplete="off"></asp:TextBox>
<cc1:CalendarExtender ID="ctxtActualJS" runat="server" Format="dd-MM-yyyy" TargetControlID="txtActualJS"></cc1:CalendarExtender>

    </td>
    <td style="width: 205px;" valign="top">
    </td>
</tr>
<tr>
    <td style="width: 249px;">
    </td>
    <td style="width: 374px;">
        <asp:TextBox ID="txttgl" runat="server" Width="50%" ReadOnly="True" Enabled="False"
        Visible="false"></asp:TextBox>
    </td>
    <td style="width: 102px;">
    </td>
    <td style="width: 209px;">
    </td>
    <td style="width: 205px;">
    </td>
</tr>
<tr>
    <td style="width: 179px;" valign="top">
        <span style="font-size: 12px">&nbsp; </span>
    </td>
    <td style="width: 374px;" valign="top">
        <asp:TextBox ID="txtSmtr" runat="server" Width="50%" ReadOnly="True" Enabled="False"
        Visible="false"></asp:TextBox>
    </td>
    <td style="width: 102px;" valign="top">
        <span style="font-size: 12px">&nbsp; </span>
    </td>
    <td style="width: 209px;" valign="top">
    </td>
    <td style="width: 205px;" valign="top">
        <asp:Button ID="lbAddItem" runat="server" OnClick="lbAddItem_Click" Text="Tambah" />
    </td>
</tr>
<tr>
    <td colspan="5">
        <hr />
    </td>
</tr>
<tr>
    <td colspan="5">
        <asp:Panel ID="TableRepeat" runat="server">
        <table class="tbStandart">
            <thead>
                <tr class="tbHeader">
                    <th class="kotak" style="width: 1%">
                        No.
                    </th>
                    <%-- <th class="kotak" style="width: 2%">Tanggal</th>    
                    <th class="kotak" style="width: 3%">Dept</th>
                    <th class="kotak" style="width: 5%">Dimensi</th>
                    <th class="kotak" style="width: 6%">Sarmut Perusahaan</th>    
                    <th class="kotak" style="width: 6%">Sarmut Departemen</th>--%>
                    <th class="kotak" style="width: 3%">
                        Sumber Daya
                    </th>
                    <th class="kotak" style="width: 15%">
                        Aktivitas
                    </th>
                    <th class="kotak" style="width: 2%">
                        Pelaku
                    </th>
                    <th class="kotak" style="width: 3%">
                        Jadwal Selesai
                    </th>
                    <th class="kotak" style="width: 3%">
                        Aktual Selesai
                    </th>
                    <th class="kotak" style="width: 2%">
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="lstRMM" runat="server" OnItemCommand="lstRMM_Command" OnItemDataBound="lstRMM_Databound">
                <ItemTemplate>
                    <tr class="EvenRows baris">
                        <td class="kotak tengah">
                            <%# Container.ItemIndex+1  %>
                        </td>
                        <%-- <td class="kotak tengah"><%# Eval("Tgl_RMM","{0:d}")%></td>--%>
                        <%--<td class="kotak tengah" nowrap="nowrap"><%# Eval("DeptName")%></td>--%>
                        <%--<td class="kotak tengah"><%# Eval("DimensiName") %></td>--%>
                        <%--<td class="kotak"><%# Eval("SMTPerusahaan")%></td> --%>
                        <%--<td class="kotak"><%# Eval("SDept")%></td>  --%>
                        <%-- <td class="kotak tengah"><%# Eval("RMM_SumberDaya")%></td>--%>
                        <td class="kotak tengah">
                            <asp:Label ID="Sumberdaya" runat="server"></asp:Label>
                        </td>
                        <td class="kotak">
                            <%# Eval("Aktivitas")%>
                        </td>
                        <td class="kotak tengah">
                            <%# Eval("Pelaku")%>
                        </td>
                        <td class="kotak tengah">
                            <%# Eval("Jadwal_Selesai", "{0:d}")%>
                        </td>
                        <td class="kotak tengah">
                            <%# Eval("Aktual_Selesai", "{0:d}")%>
                        </td>
                        <td class="kotak tengah" nowrap="nowrap">
                            <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandName="edit"
                            CommandArgument='<%# Eval("ID") %>' ToolTip="Edit Data" />
                            <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandName="dele"
                            CommandArgument='<%# Container.ItemIndex %>' ToolTip="Hapus Data" />
                            <%--<asp:ImageButton ID="dels" runat="server" ImageUrl="~/images/Delete.png" CommandName="delet" CommandArgument='<%# Eval("ID") %>' ToolTip="Hapus Data" />--%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
</td>
</tr>
<tr>
    <td style="width: 249px;">
    </td>
    <td style="width: 374px;">
    </td>
    <td style="width: 102px;">
    </td>
    <td style="width: 209px;">
    </td>
    <td style="width: 205px;">
    </td>
</tr>
<tr>
    <td colspan="5">
        <asp:Panel ID="Panel9" runat="server" Height="120px" ScrollBars="Vertical">
        <asp:GridView ID="GridRMMDetail" runat="server" AutoGenerateColumns="False" OnRowCommand="GridRMMDetail_RowCommand"
        OnRowDataBound="GridRMMDetail_RowDataBound" PageSize="4" Width="100%">
        <Columns>
            <asp:BoundField DataField="Verifikasi">
            <ItemStyle BackColor="Blue" ForeColor="Blue" />
        </asp:BoundField>
        <asp:BoundField DataField="ID">
        <ItemStyle BackColor="Blue" ForeColor="Blue" />
    </asp:BoundField>
    <asp:BoundField DataField="Aktivitas" HeaderText="Aktivitas">
    <ItemStyle Width="40%" />
</asp:BoundField>
<asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
<asp:TemplateField HeaderText="Jadwal Selesai">
<ItemTemplate>
    <asp:Label ID="LDateJS" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtDateJS" AutoPostBack="True" runat="server" AutoComplete="off" Visible="False"></asp:TextBox>
    <cc1:CalendarExtender ID="ctxtDateJS" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateJS"></cc1:CalendarExtender>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Aktual Selesai">
<ItemTemplate>
    <asp:Label ID="LDateAS" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtDateAS" AutoPostBack="True" runat="server" AutoComplete="off" Visible="False"></asp:TextBox>
    <cc1:CalendarExtender ID="ctxtDateAS" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateAS"></cc1:CalendarExtender>
</ItemTemplate>
</asp:TemplateField>
<asp:ButtonField CommandName="rubah" Text="Edit" />
<asp:TemplateField HeaderText="Verifikasi">
<ItemTemplate>
    <asp:CheckBox ID="chkVerifikasi1" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerifikasi1_CheckedChanged" />
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Tgl. Verifikasi">
<ItemTemplate>
    <asp:Label ID="LDateVF" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtDateVF" AutoPostBack="True" runat="server" AutoComplete="off" Visible="False"></asp:TextBox>
    <cc1:CalendarExtender ID="ctxtDateVF" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateVF"></cc1:CalendarExtender>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="RMM_SumberDayaID" HeaderText="SumberDaya" />
<asp:ButtonField CommandName="hapus" Text="Delete" />
<asp:ButtonField CommandName="target" Text="Target" />
<asp:BoundField DataField="target" />
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
</table>
</asp:Panel>
</td>
</tr>
<tr>
    <td align="center" colspan="2" ; font-size: x-small;">
        <asp:Panel ID="PanelStatus" runat="server" Enabled="False">
        <table style="width: 100%;">
            <tr>
                <td width="10%">
                    <%-- STATUS :--%>
                </td>
                <td width="7%">
                    &nbsp;
                </td>
                <td width="5%">
                    &nbsp;
                </td>
                <td width="7%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="15%">
                    &nbsp;
                    <%-- <input id="btnClose" runat="server" onserverclick="btnClose_ServerClick" style="background-color: white;
                    font-weight: bold; font-size: 11px; width: 42px;" type="button" value="Close"
                    visible="False" />--%>
                </td>
                <td align="left" colspan="2" style="text-decoration: underline">
                    Khusus untuk audit :
                </td>
                <td align="left" colspan="2" style="text-decoration: underline">
                    <input id="btnSolve" runat="server" align="right" onserverclick="btnSolve_ServerClick"
                    style="background-color: white; font-weight: bold; font-size: 11px; width: 42px;"
                    type="button" value="Solved" visible="False" />
                </td>
            </tr>
            <tr>
                <td width="10%">
                    &nbsp;
                </td>
                <td colspan="4">

                </td>
                <td width="15%">
                    &nbsp;
                </td>
                <td width="7%" style="width: 12%">
                    Solved
                    <asp:CheckBox ID="chksolved" runat="server" Text="  " AutoPostBack="True" OnCheckedChanged="chksolved_CheckedChanged" />
                </td>
                <td width="7%" colspan="2">
                    Tanggal :
                </td>
                <td width="10%">
                    <asp:TextBox ID="txtDateSolved" AutoPostBack="True" runat="server" AutoComplete="off" Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="ctxtDateSolved" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateSolved"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="7%">
                    &nbsp;
                </td>
                <td width="5%">
                    &nbsp;
                </td>
                <td width="7%">

                </td>
                <td width="10%">

                </td>
                <td width="15%">

                </td>
                <td colspan="3" width="5%">
                    due date Tanggal :
                </td>
                <td width="20%">
                    <asp:TextBox ID="txtDueDate" AutoPostBack="True" runat="server" AutoComplete="off" Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="ctxtDueDate" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDueDate"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    Cancel&nbsp;<asp:CheckBox ID="chkCancel" runat="server" AutoPostBack="True" OnCheckedChanged="chkCancel1_CheckedChanged" />
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>

            </tr>
        </table>
    </asp:Panel>
</td>
</tr>
<tr>
    <td>
        <asp:Panel ID="PanelLampiran" runat="server" ScrollBars="Auto" Height="570px" Visible="False">
        <table style="width: 100%;">
            <tr>
                <td>
                    Laporan No :
                    <asp:Label ID="LblLaporanNo" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    <input id="btnHapus" runat="server" style="background-color: white; font-weight: bold;
                    font-size: 11px; width: 131px;" type="button" value="Hapus" onserverclick="btnHapus_Click" />
                    <input id="btnUpload0" runat="server" onserverclick="btnUpload0_ServerClick" style="background-color: white;
                    font-weight: bold; font-size: 11px; width: 131px;" type="button" value="Refresh Data" /><input
                    id="btnUpload" runat="server" onserverclick="btnUpload_ServerClick" style="background-color: white;
                    font-weight: bold; font-size: 11px; width: 131px;" type="button" value="Tambah Lampiran" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                    id="Table1">
                    <thead>
                        <tr class="tbHeader">
                            <th style="width: 10%" class="kotak">
                                ID
                            </th>
                            <th style="width: 70%" class="kotak">
                                Nama File
                            </th>
                            <th style="width: 70%" class="kotak">
                                Tanggal Upload
                            </th>
                            <th style="width: 10%" class="kotak">
                            </th>

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="GridLampiran0" runat="server" OnItemDataBound="GridLampiran0_DataBound" OnItemCommand="GridLampiran0_Command">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="EvenRows baris" style="background-color: #FFFFFF">
                                <td class="kotak tengah" nowrap="nowrap" style="width:2%">
                                    <span class="angka" style="width: 10%">
                                        <%-- <%# Container.ItemIndex+1 %>--%></span>
                                        <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                        OnCheckedChanged="chk_CheckedChangePrs" />&nbsp;
                                        <%# Eval("ID") %></span>
                                    </td>
                                    <td class="kotak" nowrap="nowrap" style="width: 70%">
                                        <span class="angka">
                                            <%# Eval("FIleName") %></span>
                                        </td>
                                      <td class="kotak" nowrap="nowrap" style="width: 70%">
                                        <span class="angka">
                                            <%# Eval("TanggalUpload") %></span>
                                        </td>
                                        <td class="kotak tengah" nowrap="nowrap" style="width: 10%">

                                            <asp:ImageButton ToolTip="Click to Download" ID="lihatatt" runat="server"
                                            CommandArgument='<%# Eval("FIleName") %>' CssClass='<%# Eval("ID") %>' CommandName="downloadx"
                                            ImageUrl="~/images/Logo_Download.png" />&nbsp;
                                            <%--<asp:ImageButton ToolTip='<%# Eval("ID") %>' ID="hapusatt" runat="server"
                                            CommandArgument='<%# Eval("ID") %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("ID") %>'
                                            CommandName="hapusatt" ImageUrl="~/images/Delete.png" />--%>
                                        </td>
                                        <td class="kotak tengah" nowrap="nowrap" style="width: 10%">
                                            
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <asp:GridView ID="GridLampiran" runat="server" Height="100%" Width="100%" AutoGenerateColumns="False"
                    OnRowCommand="GridLampiran_RowCommand" OnRowDataBound="GridLampiran_RowDataBound" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="FIleName" HeaderText="Nama File" />
                        <asp:ButtonField CommandName="download" Text="Download" />
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
    <td>
        <asp:Panel ID="PanelList" runat="server" ScrollBars="Auto" Height="570px" Visible="False">
        <table style="width: 100%; border-collapse: collapse">
            <asp:Panel ID="filter" runat="server" >
            <tr>
                <td colspan="5" align="left">
                    Filter Data By &nbsp;&nbsp;
                    &nbsp; Departemen
                    <asp:DropDownList ID="ddlDeptName0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                    Width="125px">
                </asp:DropDownList>
                &nbsp;Status Verifikasi&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                Width="125px">
                <asp:ListItem>ALL</asp:ListItem>
                <asp:ListItem Value="0">Open</asp:ListItem>
                <asp:ListItem Value="1">Close</asp:ListItem>
            </asp:DropDownList>
            &nbsp; Tahun
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
            Width="125px">
        </asp:DropDownList>
        <%--&nbsp; Asal Permasalahan
        <asp:DropDownList ID="ddlMasalah" runat="server" AutoPostBack="True" 
        OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" Width="225px">
    </asp:DropDownList> --%>
</td>
</tr>
</asp:Panel>
<tr>
    <td>
        <hr />
    </td>
</tr>
<tr>
    <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" Visible="true"
        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="500">
        <Columns>
            <%--<asp:BoundField DataField="ID">
            <%--<ItemStyle BackColor="Blue" ForeColor="Blue" />
        </asp:BoundField>--%>
        <asp:TemplateField HeaderText="No">
        <ItemTemplate>
            <span><%#Container.DataItemIndex + 1%></span>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="Solved">
    <%--<ItemStyle BackColor="Blue" ForeColor="Blue" />--%>
</asp:BoundField>
<asp:BoundField DataField="Tgl_RMM" HeaderText="Tgl RMM" DataFormatString="{0:d}" />
<asp:BoundField DataField="DeptName" HeaderText="Departemen" />
<asp:BoundField DataField="RMM_No" HeaderText="No RMM" />
<asp:BoundField DataField="SMTPerusahaan" HeaderText="Sarmut Perusahaan" />
<asp:BoundField DataField="DimensiName" HeaderText="Dimensi" />
<asp:BoundField DataField="SDept" HeaderText="Sarmut Dept" />
<asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
<asp:BoundField DataField="Aktivitas" HeaderText="Aktivitas" />
<asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
<asp:BoundField DataField="SumberDaya" HeaderText="SumberDaya" />
<asp:BoundField DataField="Approval" HeaderText="Approval" />
<asp:BoundField DataField="Jadwal_Selesai" HeaderText="Jadwal Selesai" DataFormatString="{0:d}" />
<%-- <asp:BoundField DataField="Jadwal_Selesai" HeaderText="Aktual Selesai" DataFormatString="{0:d}" />--%>
<asp:BoundField DataField="Verifikasi" HeaderText="Verifikasi" />
<asp:ButtonField CommandName="Add" Text="Pilih" />
</Columns>
<RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
<HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
<PagerStyle BorderStyle="Solid" />
<AlternatingRowStyle BackColor="Gainsboro" />
</asp:GridView>
<asp:Panel ID="tblx" runat="server" Visible="false">
<table style="width: 100%; border-collapse: collapse; font-size: x-small">
    <tr class="tbHeader">
        <th class="kotak" rowspan="2" style="width: 1%">
            No.
        </th>
        <th class="kotak" rowspan="2" style="width: 3%">
            Tgl RMM
        </th>
        <th class="kotak" rowspan="2" style="width: 4%">
            Departemen
        </th>
        <th class="kotak" rowspan="2" style="width: 5%">
            No RMM
        </th>
        <th class="kotak" rowspan="2" style="width: 8%">
            Sasaran Perusahaan
        </th>
        <th class="kotak" rowspan="2" style="width: 15%">
            Tindakan Perbaikan Pencapaian Sasaran Mutu
        </th>
        <th class="kotak" colspan="3">
            Tanggal
        </th>

        <th class="kotak" rowspan="2" style="width: 3%">
            Verifikasi
        </th>
        <th class="kotak" rowspan="2" style="width: 3%">
            Status
        </th>
        <th class="kotak" rowspan="2" style="width: 3%">
            Approval
        </th>
        <th class="kotak" style="width: 2%" rowspan="2">
        </th>

    </tr>
    <tr class="tbHeader" id="hd2">
        <th class="kotak tengah" style="width: 3%">
            Plan Awal
        </th>
        <th class="kotak" style="width: 3%">
            Actual Selesai
        </th>
        <th class="kotak" style="width: 3%">
            Tgl Verifikasi
        </th>

    </tr>
    
<tbody>
    <asp:Repeater ID="lstRMMxx" runat="server" OnItemDataBound="lstRMMxx_DataBound" OnItemCommand="lstRMMxx_ItemCommand">
    <ItemTemplate>
        <tr class="EvenRows baris" id="rr" title='<%# Eval("ID")%>' runat="server">
            <td class="kotak tengah">
                <asp:Label ID="nom" runat="server"><%# Eval("ID")%></asp:Label>
            </td>
            <td class="kotak tengah">
                <%# Eval("Tgl_RMM", "{0:d}")%>
            </td>
            <td class="kotak tengah">
                <%# Eval("DeptName")%>
            </td>
            <td class="kotak tengah">
                <%# Eval("RMM_No")%>
            </td>
            <td class="kotak tengah">
                <%# Eval("SMTPerusahaan")%>
            </td> 
            <td class="kotak">
                <%# Eval("Aktivitas")%>
            </td>
            <td class="kotak tengah">
                <%# Eval("Minggu")%>/<%# Eval("Bulan1")%>/<%# Eval("Year")%></td>
                <td class="kotak tengah">
                    <%# Eval("Aktual_Selesai","{0:d}")%>
                </td>
                <td class="kotak tengah"><%# Eval("TglVerifikasi","{0:d}")  %></td>
                <td class="kotak tengah">
                    <asp:Label ID="verf" runat="server"></asp:Label>
                </td>   
                <td class="kotak tengah"><asp:Label ID="slvd" runat="server"></asp:Label></td> 
                <td class="kotak tengah">
                    <%# Eval("Approval")%>
                </td>
                <td class="kotak tengah">
                    <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID")%>' CommandName="edit" />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>

            <tr class="OddRows baris" id="rr" title='<%# Eval("ID")%>' runat="server">
                <td class="kotak tengah">
                    <asp:Label ID="nom" runat="server"><%# Eval("ID")%></asp:Label>
                </td>
                <td class="kotak tengah">
                    <%# Eval("Tgl_RMM", "{0:d}")%>
                </td>
                <td class="kotak tengah">
                    <%# Eval("DeptName")%>
                </td>
                <td class="kotak tengah">
                    <%# Eval("RMM_No")%>
                </td>
                <td class="kotak tengah">
                    <%# Eval("SMTPerusahaan")%>
                </td>
                <td class="kotak">
                    <%# Eval("Aktivitas")%>
                </td> 
                <td class="kotak tengah">
                    <%# Eval("Minggu")%>/<%# Eval("Bulan1")%>/<%# Eval("Year")%></td>
                    <td class="kotak tengah">
                        <%# Eval("Aktual_Selesai","{0:d}")  %>
                    </td>
                    <td class="kotak tengah"><%# Eval("TglVerifikasi","{0:d}")  %></td>
                    <td class="kotak tengah">
                        <asp:Label ID="verf" runat="server"></asp:Label>
                    </td>    
                    <td class="kotak tengah"><asp:Label ID="slvd" runat="server"></asp:Label></td>
                    <td class="kotak tengah">
                        <%# Eval("Approval")%>
                    </td>
                    <td class="kotak tengah">
                        <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID")%>' CommandName="edit"  />
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
</asp:Panel>
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
