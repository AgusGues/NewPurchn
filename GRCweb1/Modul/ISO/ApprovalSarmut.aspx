<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalSarmut.aspx.cs" Inherits="GRCweb1.Modul.ISO.ApprovalSarmut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            window.showModalDialog("../../ModalDialog/UploadFileSarmut.aspx?ba=" + id + "&tablename=SPD_AttachmentDep", "UploadFile", params);
        };
        function OpenDialog2(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileSarmut.aspx?ba=" + id + "&tablename=SPD_AttachmentPrs", "UploadFile", params);
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;
                                            SARMUT DAN PEMANTAUAN </b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnSimpan_Click" 
                                            Text="Refresh Data" />
                                        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" 
                                            Text="Export to Excel" />
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
                                            &nbsp; Periode :
                                        </td>
                                        <td style="width: 15%">
                                            &nbsp;<asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlTahun_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 35%">
                                            &nbsp;<asp:Panel ID="PanelApv" runat="server" HorizontalAlign="Center">
                                                Mode
                                                <asp:RadioButton ID="RbApv" runat="server" AutoPostBack="True" Checked="True" GroupName="a"
                                                    OnCheckedChanged="RbApv_CheckedChanged" Text="Approval" />
                                                <asp:RadioButton ID="RbList" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RbList_CheckedChanged"
                                                    Text="List" />
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            &nbsp; Departemen :
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                Width="204px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                        id="baList">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th rowspan="1" class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                    No.
                                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange" />
                                                </th>
                                                <th rowspan="1" style="width: 30%" class="kotak">
                                                    Description
                                                </th>
                                                <th style="width: 5%" class="kotak">
                                                    Satuan
                                                </th>
                                                <th colspan="2" style="width: 10%" class="kotak">
                                                    Target
                                                </th>
                                                <th rowspan="1" style="width: 5%" class="kotak">
                                                    Aktual Pecapaian
                                                </th>
                                                <th rowspan="1" style="width: 5%" class="kotak">
                                                    Tercapai / Tidak
                                                </th>
                                                <th rowspan="1" style="width: 5%" class="kotak">
                                                    Status Apv
                                                </th>
                                                <th rowspan="1" style="width: 15%" class="kotak">
                                                    Tgl Apv
                                                </th>
                                                <th colspan="1" style="width: 20%" class="kotak">
                                                    Lampiran
                                                </th>
                                                <th rowspan="1" style="width: 5%">
                                                    Upload File
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstType" runat="server" OnItemDataBound="lstType_DataBound">
                                                <ItemTemplate>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="11" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#00CC00">
                                                            <span class="kotak">
                                                                <%# Eval("JenisSarmut")%></span>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="lstPrs" runat="server" OnItemDataBound="lstPrs_DataBound" OnItemCommand="lstPrs_Command">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                    <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                        OnCheckedChanged="chk_CheckedChangePrs" /></span>
                                                                </td>
                                                                <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                    <%# Eval("Description") %>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <%# Eval("Satuan").ToString() %>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <asp:Label ID="lblTargetP" runat="server" Text='<%# Eval("Target", "{0:N2}").ToString()%>'></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <%# Eval("Param") %>
                                                                </td>
                                                                <td class="kotak" style="width: 5%">
                                                                    <asp:TextBox ID="txtActualP" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Actual", "{0:N2}") %>'
                                                                        Width="100%" OnTextChanged="txtKeterangan1_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <asp:Label ID="lblTercapaiP" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <%# Eval("StatusApv") %>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 5%">
                                                                    <%# Eval("TglApv") %>
                                                                </td>
                                                                <td class="kotak angka" style="width: 30%">
                                                                    <table width="100%" style="font-size: x-small">
                                                                        <thead>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                                                                        <HeaderTemplate>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr class="OddRows baris">
                                                                                                <td>
                                                                                                    <%# Eval("FileName") %>
                                                                                                </td>
                                                                                                <td align="right" width="15%">
                                                                                                    <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                                                        CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                                                        ImageUrl="~/images/Logo_Download.png" />
                                                                                                    <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                                                        CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("SarmutransID") %>'
                                                                                                        CommandName="hpsprs" ImageUrl="~/images/Delete.png" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </td>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </td>
                                                                <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px">
                                                                    <asp:ImageButton ToolTip="Upload Attachment" ID="attPrs" runat="server" CssClass='<%# Eval("ID") %>'
                                                                        CommandArgument='<%# Container.ItemIndex %>' CommandName="attachPrs" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="lstDetail" runat="server" OnItemDataBound="lstDetail_DataBound"
                                                                OnItemCommand="lstDetail_Command">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris">
                                                                        <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                            <span class="angka">
                                                                                <asp:CheckBox ID="chk" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                                    OnCheckedChanged="chk_CheckedChangeRpt" /></span>
                                                                        </td>
                                                                        <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                            <%#  Eval("Description")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <%# Eval("Satuan").ToString().ToUpper() %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("Target", "{0:N2}").ToString()%>'></asp:Label>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <%# Eval("Param") %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <asp:TextBox ID="txtActual" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Actual", "{0:N2}") %>'
                                                                                Width="100%" OnTextChanged="txtKeterangan_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <asp:Label ID="lblTercapai" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="kotak tengah" style="width: 5%">
                                                                            <%# Eval("StatusApv") %>
                                                                        </td>
                                                                         <td class="kotak tengah" style="width: 5%">
                                                                            <%# Eval("TglApv") %>
                                                                        </td>
                                                                        <td class="kotak angka" style="width: 30%">
                                                                            <table width="100%" style="font-size: x-small">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                                                                <HeaderTemplate>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr class="EvenRows baris">
                                                                                                        <td>
                                                                                                            <%# Eval("FileName") %>
                                                                                                        </td>
                                                                                                        <td align="right" width="15%">
                                                                                                            <asp:ImageButton ToolTip="Click to Preview Document" ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                                                                                CssClass='<%# Eval("ID") %>' CommandName="pre" ImageUrl="~/images/Logo_Download.png" />
                                                                                                            <asp:ImageButton ToolTip="Click for delete attachment" ID="hapus" runat="server"
                                                                                                                CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("SarmutransID") %>'
                                                                                                                CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </td>
                                                                                    </tr>
                                                                                </thead>
                                                                            </table>
                                                                        </td>
                                                                        <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px">
                                                                            <asp:ImageButton ToolTip="Upload Attachment Detail" ID="att" runat="server" CssClass='<%# Eval("ID") %>'
                                                                                CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif"
                                                                                Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstDetail2" runat="server" OnItemDataBound="lstDetail2_DataBound"
                                                                        OnItemCommand="lstDetail2_Command">
                                                                        <ItemTemplate>
                                                                            <tr class="EvenRows baris" style="background-color: #FFFFFF">
                                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                                    <span class="angka">
                                                                                        <asp:CheckBox ID="chk2" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server" /></span>
                                                                                </td>
                                                                                <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                                    <%# "&nbsp; &nbsp; &nbsp; &nbsp;" + Eval("Description")%>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <%# Eval("Satuan").ToString()%>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <asp:Label ID="lblTarget2" runat="server" Text='<%# Eval("Target", "{0:N2}").ToString()%>'></asp:Label>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <%# Eval("Param") %>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <asp:TextBox ID="txtActual2" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Actual", "{0:N2}") %>'
                                                                                        Width="100%" OnTextChanged="txtKeterangan2_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <asp:Label ID="lblTercapai2" runat="server" Text=""></asp:Label>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <asp:Label ID="lblStatusApv" runat="server" Text='<%# Eval("StatusApv") %>'></asp:Label>
                                                                                </td>
                                                                                <td class="kotak tengah" style="width: 5%">
                                                                                    <asp:Label ID="lblTglApv" runat="server" Text='<%# Eval("TglApv") %>'></asp:Label>
                                                                                </td>
                                                                                <td class="kotak angka" style="width: 30%">
                                                                                </td>
                                                                                <td class="kotak tengah" nowrap="nowrap" style="padding-right: 1px">
                                                                                </td>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="contentlist" style="height: 450px" id="lst2" runat="server" onscroll="setScrollPosition(this.scrollTop);" visible="false">
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                    id="Table1">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th rowspan="1" class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                No.
                                            </th>
                                            <th rowspan="1" style="width: 30%" class="kotak">
                                                Description
                                            </th>
                                            <th style="width: 5%" class="kotak">
                                                Satuan
                                            </th>
                                            <th colspan="2" style="width: 10%" class="kotak">
                                                Target
                                            </th>
                                            <th rowspan="1" style="width: 5%" class="kotak">
                                                Aktual Pecapaian
                                            </th>
                                            <th rowspan="1" style="width: 5%" class="kotak">
                                                Tercapai / Tidak
                                            </th>
                                            <th rowspan="1" style="width: 5%" class="kotak">
                                                Status Apv
                                            </th>
                                             <th rowspan="1" style="width: 5%" class="kotak">
                                                Tgl Apv
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstTypex" runat="server" OnItemDataBound="lstTypex_DataBound">
                                            <ItemTemplate>
                                                <tr class="OddRows baris">
                                                    <td class="kotak" nowrap="nowrap" colspan="8" style="width: 5%; font-size: small;
                                                        font-weight: bolder;" bgcolor="#00CC00">
                                                        <span class="kotak">
                                                            <%# Eval("JenisSarmut")%></span>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="lstPrsx" runat="server" OnItemDataBound="lstPrsx_DataBound" >
                                                    <ItemTemplate>
                                                        <tr class="OddRows baris" style="font-weight: bold">
                                                            <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                <span class="angka">
                                                                    <%# Container.ItemIndex+1 %></span>
                                                                
                                                            </td>
                                                            <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                <%# Eval("Description") %>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 5%">
                                                                <%# Eval("Satuan").ToString() %>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 5%">
                                                                <%# Eval("Target", "{0:N2}").ToString()%>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 5%">
                                                                <%# Eval("Param") %>
                                                            </td>
                                                            <td class="kotak" style="width: 5%">
                                                                <%# Eval("Actual", "{0:N2}") %>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 5%">
                                                                <asp:Label ID="lblTercapaiP" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah" style="width: 5%">
                                                                <%# Eval("StatusApv") %>
                                                            </td>
                                                             <td class="kotak tengah" style="width: 5%">
                                                                <%# Eval("TglApv") %>
                                                            </td>
                                                            
                                                        </tr>
                                                        <asp:Repeater ID="lstDetailx" runat="server" OnItemDataBound="lstDetailx_DataBound"
                                                            OnItemCommand="lstDetail_Command">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                        <span class="angka"></span>
                                                                    </td>
                                                                    <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                        <%#  Eval("Description")%>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("Satuan").ToString().ToUpper() %>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("Target", "{0:N2}").ToString()%>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("Param") %>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("Actual", "{0:N2}") %>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <asp:Label ID="lblTercapai" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("StatusApv") %>
                                                                    </td>
                                                                    <td class="kotak tengah" style="width: 5%">
                                                                        <%# Eval("TglApv") %>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <asp:Repeater ID="lstDetail2x" runat="server" >
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris" style="background-color: #FFFFFF">
                                                                            <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                                <span class="angka"></span>
                                                                            </td>
                                                                            <td class="kotak" nowrap="nowrap" style="width: 30%">
                                                                                <%# "&nbsp; &nbsp; &nbsp; &nbsp;" + Eval("Description")%>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("Satuan").ToString()%>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("Target", "{0:N2}").ToString()%>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("Param") %>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("Actual", "{0:N2}") %>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <asp:Label ID="lblTercapai2" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("StatusApv") %>
                                                                            </td>
                                                                            <td class="kotak tengah" style="width: 5%">
                                                                                <%# Eval("TglApv") %>
                                                                            </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                 <table style="font-size: x-small">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="LblPlant" runat="server" Text="Label"></asp:Label>
                            ,
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                        </td>
                        <td>
                            Menyetujui
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Mengetahui
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Dibuat Oleh
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" Height="27px" ImageUrl="~/images/PM.jpg" Width="47px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="center">
                            <asp:Image ID="Image2" runat="server" Height="27px" ImageUrl="~/images/PM.jpg" Width="47px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="center">
                            <asp:Image ID="Image3" runat="server" Height="27px" ImageUrl="~/images/PM.jpg" Width="47px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text=" "></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
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
                    <tr>
                   
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            (<asp:Label ID="LblPM" runat="server" Text="Label"></asp:Label>)
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            (<asp:Label ID="LblMgr" runat="server" Text="Label"></asp:Label>)
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            (<asp:Label ID="LblAdmin" runat="server" Text="Label"></asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>                      
                    </tr>
                </table>
                            </div>
                        </td>
                    </tr>
                </table>
               
                <%--<asp:FileUpload ID="Upload1" runat="server" Visible="false" />--%>
                <%--<asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
