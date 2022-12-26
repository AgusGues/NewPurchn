<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LaporanOutstandingWO.aspx.cs" Inherits="GRCweb1.Modul.MTC.LaporanOutstandingWO" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>
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
    function confirm_batal() {
        if (confirm("Anda yakin untuk Batal ?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancelWO.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server">
                <div class="content">
                    <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <b>&nbsp;&nbsp;<span style="font-family: Calibri; font-size: large">LIST WO SUDAH LEWAT
                                                TARGET</span></b>
                                        </td>
                                        <td style="width: 50%; padding-right: 10px" align="right">
                                            <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td style="width: 13%; font-family: 'Courier New', Courier, monospace; font-size: large;
                                font-weight: bold;">
                                <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" OnCheckedChanged="RB1_CheckedChanged"
                                    Style="font-family: 'Courier New', Courier, monospace; font-size: x-small; text-align: left;
                                    font-weight: 700;" Text="&nbsp; Laporan WO" TextAlign="Left" Width="162px" />
                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="30%"
                                    Visible="false">
                                </asp:DropDownList>
                                &nbsp;<asp:Label ID="IDDept" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="txtUserID" runat="server" Visible="false"></asp:Label>
                                <asp:RadioButton ID="RBIT" runat="server" AutoPostBack="True" OnCheckedChanged="RBIT_CheckedChanged"
                                    Style="font-family: 'Courier New', Courier, monospace; font-size: x-small; text-align: left;
                                    font-weight: 700;" Text="||&nbsp; WO IT" TextAlign="Left" Width="162px" />
                                <asp:RadioButton ID="RBGA" runat="server" AutoPostBack="True" OnCheckedChanged="RBGA_CheckedChanged"
                                    Style="font-family: 'Courier New', Courier, monospace; font-size: x-small; text-align: left;
                                    font-weight: 700;" Text="&nbsp; WO HRD GA" TextAlign="Left" Width="162px" />
                                <asp:RadioButton ID="RBMtc" runat="server" AutoPostBack="True" OnCheckedChanged="RBMtc_CheckedChanged"
                                    Style="font-family: 'Courier New', Courier, monospace; font-size: x-small; text-align: left;
                                    font-weight: 700;" Text="&nbsp; WO Maintenance" TextAlign="Left" Width="162px" />
                                <asp:Label ID="resultMailSucc" runat="server" BackColor="White" class="result_done"
                                    Font-Size="X-Small" ForeColor="Lime" Visible="False"></asp:Label>
                                <asp:Label ID="resultMailFail" runat="server" class="result_fail" ForeColor="Red"
                                    Height="20px" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">                                   
                                    <div class="contentlist" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                        <table class="tbStandart">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th style="width: 5%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        No.
                                                    </th>
                                                    <th style="width: 10%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        No. WO
                                                    </th>
                                                    <th style="width: 23%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Uraian Pekerjaan
                                                    </th>
                                                    <th style="width: 10%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Pemberi WO
                                                    </th>
                                                    <th style="width: 8%; font-family: 'Calibri Light'" class="kotak" rowspan="2">
                                                        Pelaksana
                                                    </th>
                                                    <th style="width: 8%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Area WO
                                                    </th>
                                                    <th style="width: 5%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Ket.
                                                    </th>
                                                    <th style="width: 5%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Target
                                                    </th>
                                                    <th style="width: 20%; font-family: 'Calibri Light';" class="kotak" colspan="2">
                                                        Tanggal Target
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%">
                                                        &nbsp;
                                                    </th>
                                                    <tr class="tbHeader">
                                                        <th class="kotak" style="font-family: Calibri;">
                                                            Existing
                                                        </th>
                                                        <th class="kotak" style="font-family: Calibri;">
                                                            Baru
                                                        </th>
                                                    </tr>
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: 'Calibri Light'">
                                                <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="lst" runat="server">
                                                            <td class="kotak tengah">
                                                                <%# Eval("NomorUrut") %>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%--&nbsp;<%# Eval("NoWO") %>--%>
                                                                <asp:Label ID="txtNoWO" runat="server" Text='<%# Eval("NoWO") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak" align="justify">
                                                                <%# Eval("UraianPekerjaan") %>
                                                                <asp:Label ID="lblUraianPekerjaan" runat="server" Text='<%# Eval("UraianPekerjaan") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("FromDeptName")%>
                                                                <asp:Label ID="lblFromDeptName" runat="server" Text='<%# Eval("FromDeptName") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("Pelaksana")%>
                                                                <asp:Label ID="lblPelaksana" runat="server" Text='<%# Eval("Pelaksana") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("AreaWO")%>
                                                                <asp:Label ID="lblAreaWO" runat="server" Text='<%# Eval("AreaWO") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("Ket")%>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("Target")%>
                                                            </td>
                                                            <td class="kotak" width="10%">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="txtTarget" runat="server" Text='<%# Eval("TglTarget") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <bdp:BDPLite ID="txtUpdateTgl" runat="server" ToolTip="Edit tanggal" Enabled="false"
                                                                    TextBoxStyle-Width="70%">
                                                                </bdp:BDPLite>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="edit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" />
                                                                <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Save" Visible="false" />
                                                                <asp:ImageButton ToolTip="Manager harus cancel karena sdh T3 dan sdh lewat target !!"
                                                                    ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                    AlternateText='<%# Eval("ID") %>' CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                                    
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                    <div class="contentlist" id="Div2" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                        <table class="tbStandart">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th style="width: 3%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        No.
                                                    </th>
                                                    <th style="width: 9%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        No. WO
                                                    </th>
                                                    <th style="width: 23%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Uraian Pekerjaan
                                                    </th>
                                                    <th style="width: 10%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Pemberi WO
                                                    </th>
                                                    <th style="width: 7%; font-family: 'Calibri Light'" class="kotak" rowspan="2">
                                                        Pelaksana
                                                    </th>
                                                    <th style="width: 7%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Area WO
                                                    </th>
                                                    <th style="width: 5%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Ket.
                                                    </th>
                                                    <%--<th style="width: 10%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                Tanggal Target
                                            </th> --%>
                                                    <th style="width: 7%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Target I
                                                    </th>
                                                    <th style="width: 7%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Target II
                                                    </th>
                                                    <th style="width: 7%; font-family: 'Calibri Light';" class="kotak" rowspan="2">
                                                        Target III
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 3%">
                                                        &nbsp;
                                                    </th>                                                   
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: 'Calibri Light'">
                                                <asp:Repeater ID="lstBA2" runat="server" OnItemDataBound="lstBA2_DataBound" OnItemCommand="lstBA2_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="lst2" runat="server">
                                                            <td class="kotak tengah">
                                                                <%# Eval("NomorUrut") %>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%--&nbsp;<%# Eval("NoWO") %>--%>
                                                                <asp:Label ID="txtNoWO" runat="server" Text='<%# Eval("NoWO") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak" align="justify">
                                                                <%# Eval("UraianPekerjaan") %>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("FromDeptName")%>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("Pelaksana")%>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("AreaWO")%>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <%# Eval("Ket")%>
                                                            </td>
                                                            <%--<td class="kotak" width="10%">&nbsp;&nbsp;                                                        
                                                        <asp:Label ID="txtTarget" runat="server" Text='<%# Eval("TglTarget") %>'></asp:Label></td>--%>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="Target1" runat="server" Text='<%# Eval("Target01") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="Target2" runat="server" Text='<%# Eval("Target02") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="Target3" runat="server" Text='<%# Eval("Target03") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">                                                               
                                                                <asp:ImageButton ToolTip="Manager harus cancel karena sdh T3 dan sdh lewat target !!"
                                                                    ID="hapus2" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                    AlternateText='<%# Eval("ID") %>' CommandName="hps2" ImageUrl="~/images/Delete.png" />
                                                                <asp:ImageButton ToolTip="Refreshhh !!!"
                                                                    ID="hapus1" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                    AlternateText='<%# Eval("ID") %>' CommandName="hps1" ImageUrl="~/images/refresh.jpg" />    
                                                                <asp:ImageButton ToolTip="Approved !!!"
                                                                    ID="Apv2" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                    AlternateText='<%# Eval("ID") %>' CommandName="Apv2" ImageUrl="~/images/check.png" />
                                                                <asp:ImageButton ToolTip="Refreshhh !!!"
                                                                    ID="Apv1" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>'
                                                                    AlternateText='<%# Eval("ID") %>' CommandName="Apv1" ImageUrl="~/images/refresh.jpg" />    
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
