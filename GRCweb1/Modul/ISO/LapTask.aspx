<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapTask.aspx.cs" Inherits="GRCweb1.Modul.ISO.LapTask" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 1px 1px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control,
    td{height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th {padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}
</style>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(prm_InitializeRequest);
    prm.add_endRequest(prm_EndRequest);
    function prm_InitializeRequest(sender, args) {
        var panelProg = $get('divImage');
        panelProg.style.display = '';
        $get(args._postBackElement.id).disabled = false;
    }
    function prm_EndRequest(sender, args) {
        var panelProg = $get('divImage');
        panelProg.style.display = 'none';
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
            DivHR.style.width = (parseInt(width)) + '%';
            DivHR.style.position = 'relative';
            DivHR.style.top = '0px';
            DivHR.style.zIndex = '2';
            DivHR.style.verticalAlign = 'top';
            DivMC.style.width = width + '%';
            DivMC.style.height = '400' + 'px';
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


    function btnHitung_onclick() {

    }

</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <link href="../../Scripts/text.css" rel="stylesheet" type="text/css" />
    <div id="Div1" runat="server"  style="background-color: #fff;">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Laporan Rekap Task</span>
                <div class="pull-right">
                <asp:Button class="btn btn-info" ID="btnPrint" runat="server" Height="25px" Visible="false" Text="Preview" OnClick="btnPrint_Click" />
                    <asp:Button class="btn btn-info" ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                    <asp:Button class="btn btn-info" ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">DariTanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtTanggal" runat="server" Width="105px"></asp:TextBox> s/d 
                                <asp:TextBox ID="txtSdTanggal" runat="server" Width="105px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="txtSdTanggal_CalendarExtender" TargetControlID="txtSdTanggal" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" AutoPostBack="true" 
                                OnTextChanged="ddlDept_SelectedChange"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">PIC</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" width="93%" ID="ddlPICs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPIC_SelectedChange"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Status</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" width="93%" ID="ddlstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedChange">
                                <asp:ListItem Value="0">--All--</asp:ListItem>
                                <asp:ListItem Value="1">Solved</asp:ListItem>
                                <asp:ListItem Value="2">Unsolved</asp:ListItem>
                                <asp:ListItem Value="3">Cancel</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div style="height:400px;background-color: #fff;" id="lstr" runat="server">
                        <div id="DivRoot" align="left" style="background-color: #fff;height:400px">
                            <div style="overflow: hidden;height:400px" id="DivHeaderRow"></div>
                            <div onscroll="OnScrollDiv(this)" id="DivMainContent" style=";overflow: scroll;height:400px">
                                <table class="table-bordered" id="thr" style="width:100%;height:400px; border-collapse:collapse; font-size:x-small; display:" border="0" >
                                    <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:4%">No</th>
                                            <th class="kotak" style="width:8%">No. Task</th>
                                            <th class="kotak" style="width:6%">Tgl Mulai</th>
                                            <th class="kotak" style="width:32%">Task Name</th>
                                            <th class="kotak" style="width:5%">T1</th>
                                            <th class="kotak" style="width:5%">T2</th>
                                            <th class="kotak" style="width:5%">T3</th>
                                            <th class="kotak" style="width:5%">T4</th>
                                            <th class="kotak" style="width:5%">T5</th>
                                            <th class="kotak" style="width:5%">T6</th>
                                            <th class="kotak" style="width:6%">Tgl Selesai</th>
                                            <th class="kotak" style="width:5%">Bobot</th>
                                            <th class="kotak" style="width:5%">Score</th>
                                            <th class="kotak" style="width:5%">Point</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                        <ItemTemplate>
                                            <tr class="total">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak " colspan="2"><%# Eval("DeptName") %></td>
                                                <td class="kotak" colspan="11">Pemberi Task : <%# Eval("PemberiTask") %> </td>
                                            </tr>
                                            <asp:Repeater ID="lstPIC" runat="server" OnItemDataBound="lstPIC_DataBound">
                                            <ItemTemplate>
                                                <tr class="Line3">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak" colspan="3"><%# Eval("UserName") %> - <%# Eval("BagianName") %></td>
                                                    <td class="kotak" colspan="4">Bobot Task di PES : <b> <asp:Label ID="bbPES" runat="server"></asp:Label></b></td>
                                                    <td class="kotak" colspan="6">Minimum Bobot : <b><asp:Label ID="min" runat="server"></asp:Label></b></td>
                                                </tr>
                                                <asp:Repeater ID="lstTaks" runat="server" OnItemDataBound="lstTask_DataBound">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak angka"><%#Container.ItemIndex+1 %><%--<%#Eval("TaskID") %>--%>&nbsp;&nbsp;</td>
                                                        <td class="kotak" nowrap="nowrap"><%# Eval("TaskNo") %></td>
                                                        <td class="kotak tengah"><%# Eval("TglMulai","{0:d}") %></td>
                                                        <td class="kotak"><%# Eval("NewTask") %></td>
                                                        <td class="kotak tengah"><asp:Label ID="T1" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="T2" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="T3" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="T4" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="T5" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="T6" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><asp:Label ID="TglSelesai" runat="server"></asp:Label></td>
                                                        <td class="kotak tengah"><%# Eval("BobotNilai") %></td>
                                                        <td class="kotak angka"><asp:Label ID="Point" runat="server"></asp:Label></td>
                                                        <td class="kotak angka  "><asp:Label ID="Score" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="subtotal baris" id="xx" runat="server">
                                                        <td colspan="11" class="kotak angka">&nbsp;</td>
                                                        <td class="kotak tengah"><%# Eval("TotalBBT", "{0:N0}")%></td>
                                                        <td class="kotak "><%--<%# Eval("PointNilai","{0:N2}") %>--%></td>
                                                        <td class="kotak angka"><asp:Label ID="ttSc" runat="server"></asp:Label></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                    </table>
                </div>
                <div id="DivFooterRow" style="overflow: hidden"></div>
            </div>
        </div>
    </div>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
