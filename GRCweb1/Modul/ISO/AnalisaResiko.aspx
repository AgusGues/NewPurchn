<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalisaResiko.aspx.cs" Inherits="GRCweb1.Modul.ISO.AnalisaResiko" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    </script>

    <style>
        label {
            font-weight: 400;
            font-size: 12px;
        }
    </style>

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
                                            <strong>&nbsp; Analisa Resiko </strong>
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                        <td style="width: 70%; padding-right: 5px" align="right">
                                            <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_serverClick" />
                                            <%--<asp:Button ID="btnCetak" runat="server" Text="Cetak" />--%>
                                            <%--<asp:Button ID="btnList" runat="server" Text="List" />--%>
                                            <asp:Button ID="rekapList" runat="server" Text="List" OnClick="btnRekap_ServerClick" />
                                            <%--<asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="NoSJ">No Surat Jalan</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                            <asp:Button ID="btnSearch" runat="server" Text="Cari" />--%>
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
                                                &nbsp;Tanggal&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTglAnalisaRisk" runat="server" Width="35%" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td>
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTglAnalisaRisk"
                                                    Format="dd-MMM-yyyy" runat="server">
                                                </cc1:CalendarExtender>
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
                                                &nbsp;Departemen&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="30%"
                                                    Enabled="False"> 
                                                </asp:DropDownList>&nbsp;<asp:Label ID="IDDept" runat="server" Visible="false"></asp:Label>
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
                                                &nbsp;</td>
                                            <td style="width: 25%">
                                                <asp:RadioButton ID="RbIK" runat="server" AutoPostBack="True" Checked="True" 
                                                    GroupName="a" oncheckedchanged="RbIK_CheckedChanged" Text="Sesuai IK" />
                                                &nbsp;
                                                <asp:RadioButton ID="RbLain" runat="server" AutoPostBack="True" GroupName="a" 
                                                    oncheckedchanged="RbLain_CheckedChanged" Text="Lain-lain" />
                                                    <asp:Label ID="IDLain2" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp</td>
                                            <td style="width: 20%;">
                                                &nbsp</td>
                                            <td style="width: 10%;">
                                                &nbsp</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp;Klasifikasi Resiko&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlklasifikasiRisk" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlklasifikasiRisk_SelectedIndexChanged" Width="45%">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtKlasifikasi" runat="server" Visible="False" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%;">
                                                &nbsp;Activities / Kegiatan&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtKegiatan" runat="server" Width="100%"></asp:TextBox>
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
                                                &nbsp;Risk&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtRisk" runat="server" Width="100%"></asp:TextBox>
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
                                                &nbsp;Issue Internal&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtIssueInternal" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%;">
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
                                                &nbsp;Issue External&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtIssueExternal" runat="server" Width="100%"></asp:TextBox>
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
                                                &nbsp;Opportunity / Peluang&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txtOportunity" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%;">
                                                <%--&nbsp* Kosongkan Jika Tidak Ada..--%>
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
                                                &nbsp;Level Kemungkinan&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlKemungkinan" runat="server" AutoPostBack="True" Width="25%"
                                                    OnSelectedIndexChanged="ddlKemungkinan_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;<asp:Label ID="lvlKemungkinan" runat="server" Visible="false"></asp:Label></td>
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
                                                &nbsp;Level Dampak&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:DropDownList ID="ddlDampak" runat="server" AutoPostBack="True" Width="25%" OnSelectedIndexChanged="ddlDampak_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                &nbsp;<asp:Label ID="lvlDampak" runat="server" Visible="false"></asp:Label></td>
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
                                                <%--&nbsp;Level Resiko&nbsp;--%>
                                            </td>
                                            <td style="width: 25%">
                                                &nbsp;<asp:Label ID="lblResiko" runat="server" Visible="false"></asp:Label></td>
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
                                                &nbsp;Treatment / Mitigation&nbsp;
                                            </td>
                                            <td style="width: 25%">
                                                <asp:TextBox ID="txttreatment" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%;">
                                                <%--&nbsp* Kosongkan Jika Tidak Ada..--%>
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
                                                &nbsp;Due Date&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDueDate" runat="server" Width="35%" AutoPostBack="False" BorderStyle="Groove"
                                                    ReadOnly="False" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="ddlTargetM" runat="server" AutoPostBack="True" Width="115px"
                                                    Visible="true">
                                                    <asp:ListItem>Pilih Target</asp:ListItem>
                                                    <asp:ListItem>M1 (tiap tgl 7)</asp:ListItem>
                                                    <asp:ListItem>M2 (tiap tgl 14)</asp:ListItem>
                                                    <asp:ListItem>M3 (tiap tgl 21)</asp:ListItem>
                                                    <asp:ListItem>M4 (tiap akhir Bln)</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlBulan" runat="server" Height="19px" Width="80px" Visible="true">
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
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged" Width="15%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDueDate" Format="dd-MMM-yyyy"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 20%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 10%;">
                                                &nbsp
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="contentlist" style="height: 300px" onscroll="setScrollPosition(this.scrollTop);"
                                        id="div2">
                                        
                                        <table id="zib" style="border-collapse: collapse; font-size: x-small; width: 100%"
                                            border="0">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak " rowspan="2">
                                                        No.
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
                                                    <th class="kotak " rowspan="2">
                                                        Level Kemungkinan
                                                    </th>
                                                    <th class="kotak " rowspan="2" style="width: 80px">
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
                                                </tr>
                                                <tr class="tbHeader">
                                                    <th class="kotak ">
                                                        Internal
                                                    </th>
                                                    <th class="kotak ">
                                                        Internal
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tb" runat="server">
                                                <asp:Repeater ID="lstPMX" runat="server" OnItemCommand="lstPMX_Command" OnItemDataBound="lstPMX_Databound">
                                                    <ItemTemplate>
                                                        <tr id="lst" runat="server" class="EvenRows baris">
                                                            <td class="kotak angka">
                                                                <asp:Label ID="nn" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        
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
