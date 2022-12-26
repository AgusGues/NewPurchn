<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListTask.aspx.cs" Inherits="GRCweb1.Modul.ISO.ListTask" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script type="text/javascript">
    function confirmation() {
        if (confirm('Yakin mau hapus data ?')) {
            return true;
        } else {
            return false;
        }
    }
</script>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>
                    List <%=(Request.QueryString["p"]==null)?"Task "+Request.QueryString["TipeTask"].ToString():Request.QueryString["p"].ToString() %>
                </span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Form Task" onserverclick="btnUpdate_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;">
                                    <tr id="trtgl" runat="server">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-3">Periode</div>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtdrtanggal"
                                                    runat="server" AutoPostBack="True" meta:resourcekey="txtdrtanggalResource1"
                                                    Style="text-align: center; margin-left: 16px; font-family: Calibri;" Width="110px" OnTextChanged="ddlPIC_Change"></asp:TextBox>
                                                    &nbsp;s/d
                                                    <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" meta:resourcekey="txtsdtanggalResource1"
                                                    Width="110px" Style="font-family: Calibri; margin-left: 0px" OnTextChanged="ddlPIC_Change"></asp:TextBox>
                                                    <cc1:CalendarExtender
                                                    ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdrtanggal"
                                                    Enabled="True"></cc1:CalendarExtender>
                                                    <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtsdtanggal" Enabled="True"></cc1:CalendarExtender>
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDeptName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                Width="90%" Enabled="true" style="font-family: Calibri"></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-4">Pic</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlPIC" runat="server" AutoPostBack="true" Width="100%" 
                                OnSelectedIndexChanged="ddlPIC_Change" style="font-family: Calibri"></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="PanelUpload" runat="server" Visible="False" BackColor="#CCFFCC">
                <table style="border-collapse: collapse; width: 100%">
                    <tr>
                        <td style="width: 30%">
                            File Name
                        </td>
                        <td style="width: 60%">
                            <asp:FileUpload ID="Uploadlisttask" runat="server" />
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button class="btn btn-info" ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                        </td>
                    </tr>
                </table></asp:Panel>
                <div class="table-responsive">
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Arial" Font-Size="x-small"><i> </i></asp:Label>
                    <div style="height: 400px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                        Visible="false" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="13">
                        <Columns>
                            <asp:BoundField DataField="TaskNo" HeaderText="No Task" />
                            <asp:BoundField DataField="TglMulai" HeaderText="Tgl Mulai" />
                            <asp:BoundField DataField="NewTask" HeaderText="Task" />
                            <asp:BoundField DataField="BagianName" HeaderText="Section" />
                            <asp:BoundField DataField="BobotNilai" HeaderText="Bobot" />
                            <asp:BoundField DataField="TargetKe" HeaderText="Target" />
                            <asp:BoundField DataField="TglTarget" HeaderText="Tgl Target" />
                            <asp:BoundField DataField="PIC" HeaderText="PIC" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                        <div id="dTask" runat="server">
                            <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                <thead>
                                    <tr class="tbHeader baris">
                                        <th class="kotak" style="width: 3%">
                                            No.
                                        </th>
                                        <th class="kotak" style="width: 8%">
                                            Doc. No
                                        </th>
                                        <th class="kotak" style="width: 7%">
                                            Tanggal
                                        </th>
                                        <th class="kotak" style="width: 25%">
                                            Description
                                        </th>
                                        <th class="kotak" style="width: 8%">
                                            Section
                                        </th>
                                        <th class="kotak" style="width: 5%">
                                            Bobot
                                        </th>
                                        <th class="kotak" style="width: 5%">
                                            Target
                                        </th>
                                        <th class="kotak" style="width: 8%">
                                            Tgl Target
                                        </th>
                                        <th class="kotak" style="width: 10%">
                                            PIC
                                        </th>
                                        <th class="kotak" style="width: 7%">
                                            Status
                                        </th>
                                        <th class="kotak" style="width: 14%">
                                            Lampiran
                                        </th>
                                        <th class="kotak" style="width: 14%">
                                            Tgl Upload
                                        </th>
                                        <th class="kotak" style="width: 14%">
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstPes" runat="server" OnItemDataBound="lstPes_DataBound" OnItemCommand="lstPes_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="EvenRows baris" id="rr" title='<%# Eval("PIC")%>' runat="server">
                                            <td class="kotak tengah">
                                                <asp:Label ID="nom" runat="server"><%# Container.ItemIndex+1 %></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TaskNo") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TglMulai","{0:d}")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("NewTask")%><asp:Label ID="xy" runat="server" Text=" - "></asp:Label><%# Eval("AlasanCancel") %>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("BagianName")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("BobotNilai")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtTarget" runat="server" Text='<%# Eval("TargetKe")%>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TglTarget","{0:d}")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("PIC")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="lblSts" runat="server"></asp:Label>
                                            </td>
                                            <td class="kotak">
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
                                                                        <td align="right" width="20%">
                                                                            <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                            CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                            ImageUrl="~/images/Logo_Download.png" />
                                                                            <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                            CommandArgument='<%# Eval("ID") %>' CssClass='<%# Eval("ID") %>' CommandName="hpsprs"
                                                                            ImageUrl="~/images/Delete.png" OnClientClick="return confirmation();" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate></asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </td>
                                            
                                            <td class="kotak tengah"> <%# Eval("TglUpload","{0:d}")%> </td>
                                            
                                            <td class="kotak tengah">
                                                <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="edit" />
                                                <asp:ImageButton ToolTip="Click for Add attachment" ID="addprs" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="add" ImageUrl="~/images/Add.png" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="OddRows baris" id="rr" title='<%# Eval("PIC")%>' runat="server">
                                            <td class="kotak tengah">
                                                <asp:Label ID="nom" runat="server"><%# Container.ItemIndex+1 %></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TaskNo") %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TglMulai","{0:d}")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("NewTask")%><asp:Label ID="xy" runat="server" Text=" - "></asp:Label><%# Eval("AlasanCancel") %>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("BagianName")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("BobotNilai")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="txtTarget" runat="server" Text='<%# Eval("TargetKe")%>'></asp:Label>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TglTarget","{0:d}")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("PIC")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <asp:Label ID="lblSts" runat="server"></asp:Label>
                                            </td>
                                            <td class="kotak">
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
                                                                        <td align="right" width="20%">
                                                                            <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                                            CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                                            ImageUrl="~/images/Logo_Download.png" />
                                                                            <asp:ImageButton ToolTip="Click for delete attachment" ID="hapusprs" runat="server"
                                                                            CommandArgument='<%# Eval("ID") %>' CssClass='<%# Eval("ID") %>' CommandName="hpsprs"
                                                                            ImageUrl="~/images/Delete.png" OnClientClick="return confirmation();" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate></asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </td>
                                            
                                             <td class="kotak tengah"><%# Eval("TglUpload","{0:d}")%></td>
                                            
                                            <td class="kotak tengah">
                                                <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="edit" />
                                                <asp:ImageButton ToolTip="Click for Add attachment" ID="addprs" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                CommandName="add" ImageUrl="~/images/Add.png" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate></asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div id="dSOP" runat="server">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <thead class="theadstatic">
                                <tr class="tbHeader">
                                    <th class="kotak" rowspan="2" style="width: 3%">
                                        No.
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 8%">
                                        Doc. No
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 5%">
                                        Periode
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 30%">
                                        Description
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 8%">
                                        Section
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 5%">
                                        Bobot
                                    </th>
                                    <th class="kotak" colspan="3">
                                        Pencapaian
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 10%">
                                        PIC
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 5%">
                                        Status
                                    </th>
                                    <th class="kotak" rowspan="2" style="width: 3%">
                                    </th>
                                </tr>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width: 5%">
                                        Target
                                    </th>
                                    <th class="kotak" style="width: 4%">
                                        Score
                                    </th>
                                    <th class="kotak" style="width: 4%">
                                        Aktual
                                    </th>
                                </tr>
                            </thead>
                            <tbody class="tbodyscroll">
                                <asp:Repeater ID="lstSOP" runat="server" OnItemDataBound="lstPes_DataBound" OnItemCommand="lstPes_ItemCommand">
                                <ItemTemplate>
                                    <tr class="EvenRows baris">
                                        <td class="kotak tengah">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("TaskNo") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("TglMulai","{0:MMM-yyy}")%>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("NewTask")%>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("BagianName")%>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("NilaiBobot","{0:N0}")%>%
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("KetTargetKe")%>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("TargetKe")%>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("Ket")%>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("PIC")%>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:Label ID="lblSts" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="edt" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                            CommandName="edit" />
                                        </td>
                                    </tr>
                                </ItemTemplate></asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
