<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormApprovalTask.aspx.cs" Inherits="GRCweb1.Modul.ISO.FormApprovalTask" %>
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

    function Cetak() {
        var wn = window.showModalDialog("../../Report/Report2.aspx?IdReport=SuratJalan", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
    }

    function onCancel() {

    }

    function openWindow() {
        window.showModalDialog("../../ModalDialog/ExpedisiDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
    }


    function confirm_delete() {
        if (confirm("Anda yakin untuk Cancel Surat Jalan?") == true)
            window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        else
            return false;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">    
<ContentTemplate>  
    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approval Task</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnSebelumnya" runat="server" onserverclick="btnSebelumnya_ServerClick"
                    type="button"
                    value="Sebelumnya" />
                    <input class="btn btn-info" id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" type="button" value="Approve" />
                    <input class="btn btn-info" id="btnSelanjutnya" runat="server" onserverclick="btnSesudahnya_ServerClick"
                    cssclass="btn btn-primary btn-sm" type="button"
                    value="Selanjutnya" />
                    <input class="btn btn-info" id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                    <asp:ListItem Value="SuratJalanNo">No Task</asp:ListItem></asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <input class="btn btn-info" id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">TaskNo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTaskNo" runat="server" AutoPostBack="True"
                                ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Pic</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPic" runat="server" AutoPostBack="True"
                                ReadOnly="True"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                ></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Section</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlBagian" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBagian_SelectedIndexChanged"
                                ></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">StartDate</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTglMulai" runat="server" AutoPostBack="False" 
                                ReadOnly="False"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTglMulai"></cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTglTarget"></cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTglSelesai"></cc1:CalendarExtender>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Category</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                ></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NewTask</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKeterangan" runat="server"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Score</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtBobotNilai" runat="server"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Status</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlStatus" runat="server">
                                <asp:ListItem>-- Choose Status --</asp:ListItem>
                                <asp:ListItem>T1</asp:ListItem>
                                <asp:ListItem>T2</asp:ListItem>
                                <asp:ListItem>T3</asp:ListItem>
                                <asp:ListItem>T4</asp:ListItem>
                                <asp:ListItem>T5</asp:ListItem>
                                <asp:ListItem>T6</asp:ListItem>
                                <asp:ListItem>Selesai</asp:ListItem>
                                <asp:ListItem>Cancel</asp:ListItem></asp:DropDownList>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TargetDate</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTglTarget" runat="server" AutoPostBack="False"
                                ReadOnly="False"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">FinishDate</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTglSelesai" runat="server" AutoPostBack="False" 
                                ReadOnly="False"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="txtCancel" runat="server">Alasan Cancel</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtAlasanCancel" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:HyperLink ID="lnkDokLamaran" runat="server" Target="_blank" NavigateUrl="" Text="[ Lihat Dok. Lamaran ]"
                                BorderStyle="None" Visible="false"></asp:HyperLink>
                            </div>
                            <div class="col-md-8">
                                <input style="background-color: white; font-weight: bold; font-size: 11px;" id="btnLampiran"
                                runat="server" type="button" value="Lihat Lampiran" onclick="window.open('UploadLampiran.aspx','WinMe','width=900,height=600,directories=no,location=no,menubar=no,resizable=yes,scrollbars=1,status=no,toolbar=no');" />
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div id="div3">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowDataBound="GridView1_RowDataBound" Visible="false" >
                        <Columns>
                            <asp:BoundField DataField="idDetail" HeaderText="IDd">
                            <ItemStyle Width="30px" /> </asp:BoundField>
                            <asp:BoundField DataField="NewTask" HeaderText="Task Description">
                            <ItemStyle Width="" /></asp:BoundField>
                            <asp:BoundField DataField="TargetKe" HeaderText="Target">
                            <ItemStyle Width="60px" /></asp:BoundField>
                            <asp:BoundField DataField="TglTarget" HeaderText="Target date">
                            <ItemStyle Width="100px" /> </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                            <ItemStyle Width="100px" />  </asp:BoundField>
                            <asp:BoundField DataField="PIC" HeaderText="PIC">
                            <ItemStyle Width="100px" /></asp:BoundField>
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                        <table style="width:100%; border-collapse:collapse; font-size:x-small">
                            <thead>
                                <tr class="tbHeader baris">
                                    <th class="kotak" style="width:4%">ID</th>
                                    <th class="kotak" style="width:45%">Task Description</th>
                                    <th class="kotak" style="width:5%">Target Ke</th>
                                    <th class="kotak" style="width:8%">Target Date</th>
                                    <th class="kotak" style="width:8%">Status</th>
                                    <th class="kotak" style="width:10%">PIC</th>
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
                                        <td class="kotak tengah"><asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                        <td class="kotak"><%# Eval("PIC")%></td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="OddRows baris">
                                        <td class="kotak tengah"><%# Eval("IDdetail")%></td>
                                        <td class="kotak"><%# Eval("NewTask")%></td>
                                        <td class="kotak tengah"><%# Eval("TargetKe") %></td>
                                        <td class="kotak tengah"><%# Eval("TglTarget","{0:d}")%></td>
                                        <td class="kotak tengah"><asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                        <td class="kotak"><%# Eval("PIC")%></td>
                                    </tr>
                                </AlternatingItemTemplate></asp:Repeater>
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