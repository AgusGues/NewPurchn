<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateMasterPES.aspx.cs" Inherits="GRCweb1.Modul.ISO.UpdateMasterPES" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Yakin mau hapus data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function pageScroll() {
            window.scrollBy(0, 50); // horizontal and vertical scroll increments
            scrolldelay = setTimeout('pageScroll()', 100); // scrolls every 100 milliseconds
        }
        function stopScroll() {
            clearTimeout(scrolldelay);
        }
    </script>
    <style type="text/css">
        label {
            font-weight: 400;
            font-size: 10px;
        }
    </style>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" style="width: 100%; height: 100%" class="table-responsive">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr valign="top">
                        <td style="width: 100%">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    UPD PES
                                </div>
                                <div class="panel-body ">
                                    <div class="content">
                                        <table style="width: 100%; font-size: x-small; border-collapse: collapse">
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>JENIS PES
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPesType" runat="server" Width="30%" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlPesType_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">KPI</asp:ListItem>
                                                        <asp:ListItem Value="3">SOP</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">&nbsp;
                                                </td>
                                                <td style="width: 15%">DEPT
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:DropDownList AutoPostBack="True" ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedChange"
                                                        meta:resourcekey="ddlDeptResource1">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 40%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>NAMA
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPIC" runat="server" OnTextChanged="ddlPIC_Change" AutoPostBack="True"
                                                        meta:resourcekey="ddlPICResource1" OnSelectedIndexChanged="ddlPIC_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>SORTIR BERDASARKAN ITEM
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>&nbsp
                                    <asp:Button ID="btnCari" runat="server" meta:resourcekey="btnPreviewResource1" OnClick="btnCari_Click"
                                        Text="CARI" />
                                                    &nbsp;
                                    <asp:Button ID="btnPreview" runat="server" meta:resourcekey="btnPreviewResource1"
                                        OnClick="btnPreview_Click" Text="Preview" />
                                                    &nbsp;
                                    <asp:Button ID="btnExport" runat="server" meta:resourcekey="btnExportResource1" OnClick="btnExport_Click"
                                        Text="Export to Excel" Visible="false" />
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Font-Size="X-Small"
                                                        Text="Jika pada kolom target score ada lambang &quot;&lt;&quot; atau &quot;&gt;&quot; tambahkan spasi setelahnya. contoh benar &quot;&lt; 7&quot; , contoh salah   &quot;&lt;7&quot; "></asp:Label>
                                                    &nbsp
                                                </td>
                                            </tr>
                                        </table>
                                        <hr />
                                        <div class="contentlist">
                                            <div id="lst" runat="server">
                                                <table class="table table-bordered" style="width: 220%; border-collapse: collapse; font-size: x-small; display: block">
                                                    <thead>
                                                        <tr class=" tbHeader baris">
                                                            <th class="kotak" rowspan="3" style="width: 20%">No.
                                                            </th>
                                                            <%--<th class="kotak" rowspan="3" style="width:4%" visible="false"></th>--%>
                                                            <th class="kotak" rowspan="3" style="width: 20%">PIC
                                                            </th>
                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 40%">Description
                                                            </th>
                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 20%">Target
                                                            </th>
                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 20%">Bobot (%)
                                                            </th>
                                                            <th class="kotak" rowspan="2" colspan="3" style="width: 20%">Program Checking
                                                            </th>
                                                            <th class="kotak" colspan="4" style="width: 20%">Pencapaian
                                                            </th>
                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 40%">Verifikasi________________
                                                            </th>
                                                            <th class="kotak" rowspan="3" style="width: 20%">Action
                                                            </th>
                                                        </tr>
                                                        <tr class="tbHeader baris">
                                                            <th class="kotak" colspan="2" style="width: 10%">Target
                                                            </th>
                                                            <th class="kotak" colspan="2" style="width: 10%">Score
                                                            </th>
                                                        </tr>
                                                        <tr class="tbHeader baris">
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Description--%>
                                                            <th class="kotak" style="width: 5%">Sesudah
                                                            </th>
                                                            <%--Description--%>
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Target--%>
                                                            <th class="kotak" style="width: 5%">Sesudah
                                                            </th>
                                                            <%--Target--%>
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Bobot--%>
                                                            <th class="kotak" style="width: 5%">Sesudah
                                                            </th>
                                                            <%--Bobot--%>
                                                            <th class="kotak" visible="false"></th>
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Program Checking--%>
                                                            <th class="kotak" style="width: 5%">Sesudah
                                                            </th>
                                                            <%--Program Checking--%>
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Pencapaian--%>
                                                            <th class="kotak" style="width: 10%">Sesudah
                                                            </th>
                                                            <%--Pencapaian--%>
                                                            <th class="kotak" style="width: 10%">Sebelum
                                                            </th>
                                                            <%--Pencapaian--%>
                                                            <th class="kotak" style="width: 10%">Sesudah
                                                            </th>
                                                            <%--Pencapaian--%>
                                                            <th class="kotak" style="width: 20%">Keterangan
                                                            </th>
                                                            <%--Verifikasi--%>
                                                            <th class="kotak" style="width: 5%">FEED BACK TIM PES
                                                            </th>
                                                            <%--Verifikasi--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                                            <ItemTemplate>
                                                                <asp:Repeater ID="lstPIC" runat="server" OnItemDataBound="lstPIC_DataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris">
                                                                        </tr>
                                                                        <asp:Repeater ID="lstCat" runat="server" OnItemDataBound="lstCat_DataBound">
                                                                            <ItemTemplate>
                                                                                <asp:Repeater ID="lstPES" runat="server" OnItemDataBound="lstPES_DataBound" OnItemCommand="lstPES_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris" valign="top">
                                                                                            <td class="kotak tengah">
                                                                                                <%# Container.ItemIndex+1 %>.
                                                                                            </td>
                                                                                            <asp:TextBox ID="txtID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:TextBox>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:TextBox ID="ddlNama" runat="server" AutoPostBack="True" CssClass="txtongrid"
                                                                                                    Text='<%# Eval("PIC")%>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:Label ID="desk" runat="server" CssClass="txtongrid" Width="100px" Height="100%"
                                                                                                    Text='<%#Eval("Desk") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:TextBox ID="txtDescription2" runat="server" Width="100px" Height="100%" CssClass="txtongrid"
                                                                                                    TextMode="MultiLine" Text='<%# Eval("Desk2")%>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:Label ID="Target" runat="server" CssClass="txtongrid" Width="100px" Height="100%"
                                                                                                    Text=' <%# Eval("Target") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:TextBox ID="txtTarget" runat="server" Width="100px" Height="100%" CssClass="txtongrid"
                                                                                                    TextMode="MultiLine" Text='<%# Eval("Target2")%>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:Label ID="BobotNilai" runat="server" CssClass="txtongrid" Text='<%# Eval("BobotNilai","{0:N0}") %> '></asp:Label>
                                                                                                %
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:TextBox ID="txtBobot" runat="server" Width="50px" Height="100%" CssClass="txtongrid"
                                                                                                    TextMode="MultiLine" Text='<%# Eval("BobotNilai2","{0:N0}")%>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah" visible="false">
                                                                                                <asp:TextBox ID="txtIDUser" runat="server" Text='<%# Eval("IDUser")%>' Visible="false"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:Label ID="Checking" runat="server" CssClass="txtongrid" Width="100px" Height="100%"
                                                                                                    Text=' <%# Eval("Checking") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:TextBox ID="txtChecking" runat="server" Width="100px" Height="100%" CssClass="txtongrid"
                                                                                                    TextMode="MultiLine" Text='<%# Eval("Checking2")%>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="kotak tengah" colspan="4" style="width: 100%">
                                                                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                                                                    <asp:Repeater ID="lstScr" runat="server" OnItemDataBound="lstscr_DataBound" OnItemCommand="lstscr_ItemCommand">
                                                                                                        <ItemTemplate>
                                                                                                            <tr class="EvenRows baris" style="width: 100%">
                                                                                                                <td class="kotak tengah" visible="false">
                                                                                                                    <asp:TextBox ID="txtID2" runat="server" CssClass="txtongrid" Text='<%# Eval("ID2")%>'
                                                                                                                        Visible="false"></asp:TextBox>
                                                                                                                </td>
                                                                                                                </td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Label ID="Pencapaian" runat="server" CssClass="txtongrid" Width="40px" Text='<%# Eval("Pencapaian") %>'></asp:Label>
                                                                    </td>
                                                                                                                <td class="kotak tengah">
                                                                                                                    <asp:TextBox ID="txtPencapaian" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                                                        Text='<%# Eval("Pencapaian2")%>' Width="50px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td class="kotak tengah">
                                                                                                                    <asp:TextBox ID="txtScore" runat="server" CssClass="txtongrid" Text='<%# Eval("Score")%>'
                                                                                                                        Width="40px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td class="kotak tengah">
                                                                                                                    <asp:TextBox ID="txtScore2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                                                        Text='<%# Eval("Score2")%>' Width="50px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td class="kotak tengah">
                                                                                                                    <asp:LinkButton ID="lnkUpdate2" runat="server" CommandName="update2" CommandArgument='<%# Eval("ID") %>'>Simpan</asp:LinkButton>
                                                                                                                    <asp:LinkButton ID="LinkPublishScore" runat="server" CommandName="publishscore" CommandArgument='<%# Eval("ID") %>'
                                                                                                                        Visible="false">Publish</asp:LinkButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </table>
                                                                                                <td class="kotak">
                                                                                                    <asp:CheckBox ID="chksmt" runat="server" Text="Semesteran" /><div style="padding: 1px;"></div>
                                                                                                    <asp:CheckBox ID="chkthn" runat="server" Text="Tahunan" /><div style="padding: 1px;"></div>
                                                                                                    <asp:CheckBox ID="chkhapus" runat="server" Text="Hapus Item" /><div style="padding: 1px;"></div>
                                                                                                    <asp:CheckBox ID="chkbatal" runat="server" Text="Batal Diajukan" /><div style="padding: 1px;"></div>
                                                                                                    <asp:CheckBox ID="chkapv" runat="server" Text="Approved" Visible="false" /><div style="padding: 1px;"></div>
                                                                                                    <asp:LinkButton ID="lnkNew" runat="server" OnClientClick="pageScroll()" CommandName="baru"
                                                                                                        CommandArgument='<%# Eval("ID") %>'>Item Baru</asp:LinkButton>&nbsp;
                                                                                                </td>
                                                                                                <td class="kotak tengah">
                                                                                                    <asp:TextBox ID="txtFeedBack" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                                        Width="40px" Text='<%# Eval("FeedBack")%>'></asp:TextBox>
                                                                                                </td>
                                                                                            </td>
                                                                                            <td class="kotak tengah">
                                                                                                <asp:LinkButton ID="lnkSimpan" runat="server" CommandName="simpan" CommandArgument='<%# Eval("ID") %>'>Simpan</asp:LinkButton>&nbsp;
                                                    <asp:LinkButton ID="lnkApproved" runat="server" CommandName="approved" CommandArgument='<%# Eval("ID") %>'>Approved</asp:LinkButton>&nbsp;
                                                    <asp:LinkButton ID="LinkPublish" runat="server" CommandName="publish" CommandArgument='<%# Eval("ID") %>'
                                                        Visible="false">Publish</asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <tr class=" tbHeader baris">
                                                        <td class="kotak" rowspan="3" style="width: 4%"></td>
                                                        <%--<th class="kotak" rowspan="3" style="width:4%" visible="false"></th>--%>
                                                        <td class="kotak" rowspan="3" style="width: 4%">Total
                                                        </td>
                                                        <td class="kotak" rowspan="2" colspan="2" style="width: 40%"></td>
                                                        <td class="kotak" rowspan="2" colspan="2" style="width: 12%"></td>
                                                        <td class="kotak" rowspan="2" colspan="2" style="width: 5%">
                                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="txtongrid"
                                                                Width="100%" Height="100%"></asp:TextBox>
                                                        </td>
                                                        <td class="kotak" rowspan="2" colspan="3" style="width: 12%"></td>
                                                        <td class="kotak" colspan="4" style="width: 12%"></td>
                                                        <td class="kotak" rowspan="2" colspan="2"></td>
                                                        <td class="kotak" rowspan="3" style="width: 4%">tion
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="lblAddItem" runat="server" Text="+" OnClick="lbAddItem_Click" />
                                                            <asp:TextBox ID="BagianID" runat="server" TextMode="MultiLine" Visible="false" Width="75px"></asp:TextBox>
                                                            <asp:Panel ID="panel1" runat="server" Visible="False">
                                                                <table class="table table-bordered" style="width: 100%; border-collapse: collapse; font-size: x-small; display: block"
                                                                    border="0">
                                                                    <thead>
                                                                        <tr class=" tbHeaderPrd">
                                                                            <th class="kotak" colspan="12">Mulai Berlaku Bulan
                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                                                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                            </th>
                                                                        </tr>
                                                                        <tr class=" tbHeader baris">
                                                                            <th class="kotak" rowspan="3" style="width: 4%">No.
                                                                            </th>
                                                                            <%--<th class="kotak" rowspan="3" style="width:4%" visible="false"></th>--%>
                                                                            <th class="kotak" rowspan="3" style="width: 4%">PIC
                                                                            </th>
                                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 40%">Description
                                                                            </th>
                                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 12%">Target
                                                                            </th>
                                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 5%">Bobot (%)
                                                                            </th>
                                                                            <th class="kotak" rowspan="2" colspan="2" style="width: 12%">Program Checking
                                                                            </th>
                                                                            <th class="kotak" colspan="4" style="width: 12%">Pencapaian
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="tbHeader baris">
                                                                            <th class="kotak" colspan="2" style="width: 10%">Target
                                                                            </th>
                                                                            <th class="kotak" colspan="2" style="width: 10%">Score
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="tbHeader baris">
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <%--Description--%>
                                                                            <th class="kotak" style="width: 5%">Sesudah
                                                                            </th>
                                                                            <%--Description--%>
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <%--Target--%>
                                                                            <th class="kotak" style="width: 5%">Sesudah
                                                                            </th>
                                                                            <%--Target--%>
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <%--Bobot--%>
                                                                            <th class="kotak" style="width: 5%">Sesudah
                                                                            </th>
                                                                            <%--Bobot--%>
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <%--Program Checking--%>
                                                                            <th class="kotak" style="width: 5%">Sesudah
                                                                            </th>
                                                                            <%--Program Checking--%>
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <%--Pencapaian--%>
                                                                            <th class="kotak" style="width: 10%">Sesudah
                                                                            </th>
                                                                            <%--Pencapaian--%>
                                                                            <th class="kotak" style="width: 10%">Sebelum
                                                                            </th>
                                                                            <th class="kotak" style="width: 10%">Sesudah
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tr class="EvenRows baris" valign="top">
                                                                        <td class="kotak tengah">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:DropDownList ID="ddlPIC2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPIC2_SelectedIndexChanged"
                                                                                meta:resourcekey="ddlPICResource1">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="desc" runat="server" Enabled="false" CssClass="txtongrid" Width="75px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="desc2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="175px" Height="100%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="txtongrid" Width="60px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="target2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Height="100%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="bobot" runat="server" Enabled="false" CssClass="txtongrid" Width="45px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="bobot2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="70px" Height="100%"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="TextBox2" runat="server" Enabled="false" CssClass="txtongrid" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:TextBox ID="checking2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Height="100%" Width="100px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget" runat="server" Enabled="false" CssClass="txtongrid" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore" runat="server" Enabled="false" CssClass="txtongrid" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore2" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="5">
                                                                            <asp:CheckBoxList ID="keterangan" runat="server">
                                                                                <asp:ListItem Value="0" style="font-size: 10px;">Semesteran</asp:ListItem>
                                                                                <asp:ListItem Value="1" style="font-size: 10px;">Tahunan</asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="feedbck" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="140px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="3" valign="middle">
                                                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan"
                                                                                OnClick="Simpan_Click" />
                                                                            <%--<asp:Button ID="Button1" OnClientClick="stopScroll()" runat="server" Text="Simpan"
    OnClick="Simpan_Click" />--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="EvenRows baris" valign="top">
                                                                        <td class="kotak tengah">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="pentarget3" runat="server" CssClass="txtongrid"
                                                                                TextMode="MultiLine" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget4" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="penscore3" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore4" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox20" runat="server" TextMode="MultiLine" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox21" runat="server" TextMode="MultiLine" Width="140px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah"></td>
                                                                    </tr>
                                                                    <tr class="EvenRows baris" valign="top">
                                                                        <td class="kotak tengah">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="pentarget5" runat="server" CssClass="txtongrid"
                                                                                TextMode="MultiLine" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget6" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="penscore5" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore6" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox36" runat="server" TextMode="MultiLine" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox37" runat="server" TextMode="MultiLine" Width="140px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah"></td>
                                                                    </tr>
                                                                    <tr class="EvenRows baris" valign="top">
                                                                        <td class="kotak tengah">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="pentarget7" runat="server" CssClass="txtongrid"
                                                                                TextMode="MultiLine" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget8" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="penscore7" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore8" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox38" runat="server" TextMode="MultiLine" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" rowspan="2" valign="middle">
                                                                            <asp:Button ID="btnCacel" runat="server" OnClick="btnCacel_Click" OnClientClick="stopScroll()"
                                                                                Text="Cancel" />
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox39" runat="server" TextMode="MultiLine" Width="140px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah"></td>
                                                                    </tr>
                                                                    <tr class="EvenRows baris" valign="top">
                                                                        <td class="kotak tengah">&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="pentarget9" runat="server" CssClass="txtongrid"
                                                                                TextMode="MultiLine" Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="pentarget10" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="penscore9" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="40px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox ID="penscore10" runat="server" CssClass="txtongrid" TextMode="MultiLine"
                                                                                Width="50px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox50" runat="server" TextMode="MultiLine" Width="70px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <asp:TextBox Visible="false" ID="TextBox51" runat="server" TextMode="MultiLine" Width="140px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah"></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
