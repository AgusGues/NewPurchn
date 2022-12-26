<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputTask.aspx.cs" Inherits="GRCweb1.Modul.ISO.FormInputTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 8px 8px 8px;
            overflow-x: auto;
            min-height: .01%;
        }

        .btn {
            font-style: normal;
            border: 1px solid transparent;
            padding: 2px 4px;
            font-size: 11px;
            height: 23px;
            border-radius: 4px;
        }

        input, select, .form-control, select.form-control, select.form-group-sm .form-control {
            height: 24px;
            color: #000;
            padding: 2px 4px;
            font-size: 12px;
            border: 1px solid #d5d5d5;
            border-radius: 4px;
        }

        .table > tbody > tr > th, .table > tbody > tr > td {
            border: 0px solid #fff;
            padding: 2px 4px;
            font-size: 12px;
            color: #fff;
            font-family: sans-serif;
        }

        .contentlist {
            border: 0px solid #B0C4DE;
        }

        label {
            font-size: 12px;
        }
    </style>
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

    <script type="text/javascript" language="javascript">

        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel() {

        }

        function confirm_delete() {
            if (confirm("Yakin Akan Cancel Task ini") == true) {
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            }
        }
        function checkDate(sender, args) {
            var dCurrentDate = new Date();
            dCurrentDate.setDate(dCurrentDate.getDate() - 8)
            if (sender._selectedDate < (dCurrentDate)) {
                alert("Anda tidak dapat memilih H-7 dari hari ini!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <span>Input Task</span>
                        <div class="pull-right">
                            <asp:Button class="btn btn-info" ID="btnNew" runat="server" OnClick="btnNew_ServerClick" Text="New" />
                            <input class="btn btn-info" id="btnSave" runat="server" type="button" value="Simpan" onserverclick="btnSave_ServerClick" />
                            <input class="btn btn-info" id="btnPrint" runat="server" onserverclick="btnPrint_ServerClick" type="button" value="Cetak" />
                            <asp:Button class="btn btn-info" ID="btnCancel" runat="server" OnClick="btnCancel_ServerClick" Text="Cancel" Enabled="false" />
                            <input class="btn btn-info" id="btnListSolved" runat="server" onserverclick="btnListSolved_ServerClick" type="button"
                                value="List Solved" />
                            <input class="btn btn-info" id="btnList" runat="server" type="button" value="List UnSolved" onserverclick="btnList_ServerClick" />
                            <input class="btn btn-info" id="btnListCancel" runat="server" type="button" value="List Cancel" onserverclick="btnListCancel_ServerClick" />
                            <input class="btn btn-info" id="btnLampiran" runat="server" type="button" value="Lampiran" onclick="window.open('UploadLampiran.aspx', 'WinMe', 'width=700,height=250,directories=no,location=no,menubar=no,resizable=yes,scrollbars=1,status=no,toolbar=no');"
                                onserverclick="btnLampiran_ServerClick" visible="False" />
                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                <asp:ListItem Value="SuratJalanNo">No Task</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                            <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">TaskNo</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtTaskNo" runat="server" AutoPostBack="True"
                                            ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                 <div class="row">
                                    <div class="col-md-2">Pic </div>
                                    <div class="col-md-10">
                                        <asp:DropDownList class="form-control" Width="100%" ID="ddlPIC" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPIC_SelectedIndexChanged"></asp:DropDownList>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-12">
                                <div class="row">
                                    <div class="col-md-1">JenisTask</div>
                                    <div class="col-md-11">
                                        <asp:RadioButton ID="rbTask0" runat="server" Checked="True" GroupName="AA" Text="Biasa" />
                                        &nbsp;&nbsp;
                                <asp:RadioButton ID="rbTask1" runat="server" GroupName="AA" Text="Improvement-Level 1" />
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">Department</div>
                                    <div class="col-md-10">
                                        <asp:DropDownList class="form-control" Width="100%" ID="ddlDept" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">StartDate</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" Width="100%" ID="txtTglMulai" runat="server" AutoPostBack="False"
                                            ReadOnly="False"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTglMulai" OnClientDateSelectionChanged="checkDate"></cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTglTarget"></cc1:CalendarExtender>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">Section</div>
                                    <div class="col-md-10">
                                        <asp:DropDownList class="form-control" Width="100%" ID="ddlBagian" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBagian_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">Approval</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtApproval" runat="server" AutoPostBack="True"
                                            ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-12">
                                <div class="row">
                                    <div class="col-md-1">Category</div>
                                    <div class="col-md-11">
                                        <asp:DropDownList class="form-control" Width="100%" ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">Bobot</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtBobotNilai" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">PointNilai</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtPointNilai" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-12">
                                <div class="row">
                                    <div class="col-md-1">NewTask</div>
                                    <div class="col-md-11">
                                        <asp:TextBox class="form-control" ID="txtKeterangan" runat="server"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-12 col-sm-6">
                                <div class="row">
                                    <div class="col-md-2">Status</div>
                                    <div class="col-md-10">
                                        <asp:DropDownList class="form-control" ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            <asp:ListItem>-- Choose Status --</asp:ListItem>
                                            <asp:ListItem>T1</asp:ListItem>
                                            <asp:ListItem>T2</asp:ListItem>
                                            <asp:ListItem>T3</asp:ListItem>
                                            <asp:ListItem>T4</asp:ListItem>
                                            <asp:ListItem>T5</asp:ListItem>
                                            <asp:ListItem>T6</asp:ListItem>
                                            <asp:ListItem>Selesai</asp:ListItem>
                                            <asp:ListItem>Cancel</asp:ListItem>
                                        </asp:DropDownList>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglSelesai" />
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-md-2">FinishDate</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" Width="100%" ID="txtTglSelesai" runat="server" AutoPostBack="True" Enabled="false" ReadOnly="False"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                 <div class="row">
                                    <div class="col-md-2">TargetDate</div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtTglTarget" runat="server" AutoPostBack="False" BorderStyle="Groove" ReadOnly="False" Visible="false" Width="233"></asp:TextBox>
                                        <asp:DropDownList ID="ddlTargetM" runat="server" AutoPostBack="True" Visible="true" Width="115px">
                                            <asp:ListItem>Pilih Target</asp:ListItem>
                                            <asp:ListItem>M1 (tiap tgl 7)</asp:ListItem>
                                            <asp:ListItem>M2 (tiap tgl 14)</asp:ListItem>
                                            <asp:ListItem>M3 (tiap tgl 21)</asp:ListItem>
                                            <asp:ListItem>M4 (tiap akhir Bln)</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlBulan" runat="server" Visible="true" Width="90px">
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
                                        <asp:TextBox ID="txtTahun" runat="server" BorderStyle="Groove" Visible="true" Width="50px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cc1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglTarget" />
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">InputBy</div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtPic" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="ifCancel" runat="server" Visible="false">Alasan Cancel</asp:Label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox class="form-control" ID="txtTglTargetAktip" runat="server" AutoPostBack="False" ReadOnly="True" Visible="False"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTglTargetAktip_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglTargetAktip" />
                                        <asp:TextBox ID="txtAlasanCancel" runat="server" Rows="2" TextMode="MultiLine" Visible="false" Width="233px"></asp:TextBox>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <asp:Button class="btn btn-info" ID="lbUpdateTask" runat="server" OnClick="lbUpdateTask_Click" Text="Update Task" />
                                        <asp:Label ID="resultMailSucc" runat="server" BackColor="White" class="result_done" Font-Size="X-Small" ForeColor="Lime" Visible="False"></asp:Label>
                                        <asp:Label ID="resultMailFail" runat="server" class="result_fail" ForeColor="Red" Height="20px" Visible="False"></asp:Label>
                                        <div style="padding: 2px"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="table-responsive">
                            <div id="div3">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    AllowPaging="true" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound" Visible="false">
                                    <Columns>
                                        <asp:BoundField DataField="IDdetail" HeaderText="IDd">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NewTask" HeaderText="Task Description">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TargetKe" HeaderText="Target-Ke">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TglTarget" HeaderText="Target Date">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PIC" HeaderText="PIC">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="white" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                                <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="1">
                                    <thead>
                                        <tr class="tbHeader baris">
                                            <th class="kotak" style="width: 4%">ID</th>
                                            <th class="kotak" style="width: 45%">Task Description</th>
                                            <th class="kotak" style="width: 5%">Target Ke</th>
                                            <th class="kotak" style="width: 8%">Target Date</th>
                                            <th class="kotak" style="width: 8%">Status</th>
                                            <th class="kotak" style="width: 10%">PIC</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstTask" runat="server" OnItemDataBound="lstTask_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Eval("IDdetail")%></td>
                                                    <td class="kotak"><%# Eval("NewTask")%></td>
                                                    <td class="kotak tengah"><%# Eval("TargetKe") %></td>
                                                    <td class="kotak tengah"><%# Eval("TglTarget","{0:d}")%></td>
                                                    <td class="kotak tengah">
                                                        <asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </td>
                                                    <td class="kotak"><%# Eval("PIC")%></td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="OddRows baris">
                                                    <td class="kotak tengah"><%# Eval("IDdetail")%></td>
                                                    <td class="kotak"><%# Eval("NewTask")%></td>
                                                    <td class="kotak tengah"><%# Eval("TargetKe") %></td>
                                                    <td class="kotak tengah"><%# Eval("TglTarget","{0:d}")%></td>
                                                    <td class="kotak tengah">
                                                        <asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </td>
                                                    <td class="kotak"><%# Eval("PIC")%></td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
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
