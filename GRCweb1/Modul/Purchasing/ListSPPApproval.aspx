<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSPPApproval.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ListSPPApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 24px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script language="JavaScript" type="text/javascript">
    function openWindow() {
        window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
    }
</script>
<script language="javascript" type="text/javascript">
    function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
        var tbl = document.getElementById(gridId);
        if (tbl) {
            var DivHR = document.getElementById('DivHeaderRow');
            var DivMC = document.getElementById('DivMainContent');
            var DivFR = document.getElementById('DivFooterRow');

            DivHR.style.height = headerHeight + 'px';
            DivHR.style.width = (parseInt(width - 1)) + '%';
            DivHR.style.position = 'relative';
            DivHR.style.top = '0px';
            DivHR.style.zIndex = '2';
            DivHR.style.verticalAlign = 'top';

            DivMC.style.width = width + '%';
            DivMC.style.height = height + 'px';
            DivMC.style.position = 'relative';
            DivMC.style.top = -headerHeight + 'px';
            DivMC.style.zIndex = '0';

            DivFR.style.width = (parseInt(width)) + 'px';
            DivFR.style.position = 'relative';
            DivFR.style.top = -headerHeight + 'px';
            DivFR.style.verticalAlign = 'top';
            DivFR.style.paddingtop = '2px';

            if (isFooter) {
                var tblfr = tbl.cloneNode(true);
                tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                var tblBody = document.createElement('tbody');
                tblfr.style.width = '100%';
                tblfr.cellSpacing = "0";
                tblfr.border = "0px";
                tblfr.rules = "none";
                tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                tblfr.appendChild(tblBody);
                DivFR.appendChild(tblfr);
            }
            DivHR.appendChild(tbl.cloneNode(true));
        }
    }

    function OnScrollDiv(Scrollablediv) {
        document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approval Spp</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Form SPP Approval" onserverclick="btnUpdate_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="ScheduleNo">No SPP</asp:ListItem> </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="5" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="NoSPP" HeaderText="No SPP" />
                        <asp:BoundField DataField="Minta" HeaderText="Tanggal" />
                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Brg" />
                        <asp:BoundField DataField="ItemName" HeaderText="Nama Brg" />
                        <asp:BoundField DataField="UomCode" HeaderText="Satuan" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Approval" HeaderText="Approval" />
                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                    </Columns>
                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /> </asp:GridView>
                    <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow"> </div>
                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <table id="lstRepeater" rules="all" style="width: 100%; font-size: x-small; border-collapse: collapse;  display: ">
                                <thead>
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width: 4%">
                                            No.
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            No.SPP&nbsp;
                                            <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" ImageUrl="~/images/BtnDown.png"
                                            ToolTip="Sort Ascending" Width="15px" OnClick="ImageButton1_Click" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" ImageUrl="~/images/BtnUp.png"
                                            ToolTip="Sort Descending" Width="15px" OnClick="ImageButton2_Click" />
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            Tanggal&nbsp;
                                            <asp:ImageButton ID="ImageButton3" runat="server" Height="15px" ImageUrl="~/images/BtnDown.png"
                                            ToolTip="Sort Ascending" Width="15px" OnClick="ImageButton3_Click" />
                                            <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/BtnUp.png"
                                            ToolTip="Sort Descending" Width="15px" OnClick="ImageButton4_Click" />
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            Item Code&nbsp;
                                            <asp:ImageButton ID="ImageButton5" runat="server" Height="15px" ImageUrl="~/images/BtnDown.png"
                                            ToolTip="Sort Ascending" Width="15px" OnClick="ImageButton5_Click" />
                                            <asp:ImageButton ID="ImageButton6" runat="server" Height="15px" ImageUrl="~/images/BtnUp.png"
                                            ToolTip="Sort Descending" Width="15px" OnClick="ImageButton6_Click" />
                                        </th>
                                        <th class="kotak" style="width: 31%">
                                            ItemName&nbsp;
                                            <asp:ImageButton ID="ImageButton7" runat="server" Height="15px" ImageUrl="~/images/BtnDown.png"
                                            ToolTip="Sort Ascending" Width="15px" OnClick="ImageButton7_Click" />
                                            <asp:ImageButton ID="ImageButton8" runat="server" Height="15px" ImageUrl="~/images/BtnUp.png"
                                            ToolTip="Sort Descending" Width="15px" OnClick="ImageButton8_Click" />
                                        </th>
                                        <th class="kotak" style="width: 5%">
                                            Qty
                                        </th>
                                        <th class="kotak" style="width: 5%">
                                            Satuan
                                        </th>
                                        <th class="kotak" style="width: 8%">
                                            Status
                                        </th>
                                        <th class="kotak" style="width: 8%">
                                            Approval
                                        </th>
                                        <th class="kotak" style="width: 5%">
                                            &nbsp
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstSPP" runat="server" OnItemCommand="lstSPP_Command" OnItemDataBound="lstSPP_DataBound">
                                    <ItemTemplate>
                                        <tr class="baris EvenRows kotak">
                                            <td class="kotak tengah">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("NoSPP") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Minta","{0:d}") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("ItemCode") %>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("ItemName") %>
                                            </td>
                                            <td class="kotak angka">
                                                <%# Eval("JumlahSisa","{0:N2}")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("UomCode")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtSts" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtApv" runat="server" Text='<%# Eval("Approval") %>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:ImageButton ImageUrl="~/images/folder.gif" runat="server" ID="lstPilih" CommandArgument='<%# Eval("NoSPP") %>'
                                                CommandName="Pilih" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="baris OddRows kotak">
                                            <td class="kotak tengah">
                                                <%# Container.ItemIndex+1 %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("NoSPP") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Minta","{0:d}") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("ItemCode") %>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("ItemName") %>
                                            </td>
                                            <td class="kotak angka">
                                                <%# Eval("JumlahSisa","{0:N2}")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("UomCode")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtSts" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtApv" runat="server" Text='<%# Eval("Approval") %>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:ImageButton ImageUrl="~/images/folder.gif" runat="server" ID="lstPilih" CommandArgument='<%# Eval("NoSPP") %>'
                                                CommandName="Pilih" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate> </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div id="DivFooterRow" style="overflow: hidden">  </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
