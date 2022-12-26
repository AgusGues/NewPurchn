<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveSPPNew.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApproveSPPNew" %>
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
<script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
<script type="text/javascript">
    function onKeyUp() {
        $("#<%=txtNotApproved.ClientID %>").keyup(function() {
            if ($(this).val().length == 2) {
                $("#<%=btnNotApprove.ClientID %>").removeAttr("disabled");
                $("#<%=btnApprove.ClientID %>").attr("disabled", "true");
            }
            if ($(this).val().length == 0) {
                $("#<%=btnNotApprove.ClientID %>").attr("disabled", "false");
                $("#<%=btnApprove.ClientID %>").removeAttr("disabled");
            }
        });
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approval Spp</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" OnClick="btnPrev_Click" />
                    <asp:Button class="btn btn-info" ID="btnApprove" runat="server" Text="Approved" OnClick="btnApprove_Click" />
                    <asp:Button class="btn btn-info" ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_Click" />
                    <asp:Button class="btn btn-info" ID="btnNotApprove" runat="server" Text="Not Approved" Enabled="false" OnClick="btnNotApprove_Click" />
                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="List SPP" Enabled="true" OnClick="btnList_ServerClick" />
                    &nbsp;
                    <%-- 
                    <asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor SPP" 
                    onfocus="if(this.value==this.defaultValue)this.value='';" 
                    onblur="if(this.value=='')this.value=this.defaultValue;" 
                    runat="server" placeholder="Find by Nomor SPP"></asp:TextBox>
                    --%>
                    <asp:TextBox ID="txtCari" Width="250px" 
                    runat="server" placeholder="Find by Nomor SPP"></asp:TextBox>
                    <asp:Button class="btn btn-info" ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                    <asp:HiddenField ID="noSPP" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTglSPP" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">MaterialGroup</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlGroupPurchn" runat="server" Enabled="true"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">PermintaanKirim</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKirim" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TypeBarang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlTipeBarang" runat="server"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">UserName</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUserName" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">KodeSpp</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoSPP" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TypeSpp</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlTypeSPP" runat="server" Enabled="true">
                                <asp:ListItem Value="1">Top Urgent</asp:ListItem>
                                <asp:ListItem Value="2">Biasa</asp:ListItem>
                                <asp:ListItem Value="3">Sesuai Schedule</asp:ListItem></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ApprovalStatus</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">HeadName</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtHeadName" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">AlasanNotApprove</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNotApproved" runat="server" TextMode="MultiLine" Rows="2" OnTextChanged="txtNotApproved_Change"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div>
                        <table class="tbStandart" style="border-collapse:collapse; font-size:x-small;">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width:5%">No.</th>
                                    <th class="kotak" style="width:10%">ItemCode</th>
                                    <th class="kotak" style="width:35%">Item Name</th>
                                    <td class="kotak" style="width:7%">Quantity</td>
                                    <th class="kotak" style="width:4%">Satuan</th>
                                    <td class="kotak" style="width:15%">Keterangan</td>
                                    <th class="kotak" style="width:5%">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstSPP" runat="server" OnItemCommand="lstSPP_ItemCommand" OnItemDataBound="lstSPP_DataBound">
                                <ItemTemplate>
                                    <tr class="EvenRows">
                                        <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                        <td class="kotak">
                                            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                                <tr>
                                                    <td style="width:90%"><asp:Label ID="itm" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label></td>
                                                    <td style="width:10%" align="right"><asp:Label ID="picMX" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                        <td class="kotak"><%# Eval("Satuan") %></td>
                                        <td class="kotak"><asp:Label ID="ket" runat="server" Text='<%# Eval("Keterangan") %>'></asp:Label></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>' CommandName="hapus" ToolTip="Hapus Item SPP" />
                                        </td>
                                    </tr>
                                </ItemTemplate> </asp:Repeater>
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
