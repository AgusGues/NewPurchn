<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputSOPNew.aspx.cs" Inherits="GRCweb1.Modul.ISO.FormInputSOPNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style type="text/css">
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
<asp:UpdatePanel ID="UpdatePenal1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>INPUT<%=(Request.QueryString["tp"].ToString()=="3")?"SOP":"KPI" %></span>
                <div class="pull-right">
                    <asp:Label ID="txtTaskNo" runat="server" CssClass="bold"></asp:Label>
                    <asp:TextBox ID="txtTglMulai" runat="server" Visible="false"></asp:TextBox>
                    <asp:Button class="btn btn-primary" ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                    <asp:Button class="btn btn-primary" ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Periode</div>
                            <div class="col-md-8">
                            <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>
                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTahun_Change"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtPES" runat="server" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtNamaPES" runat="server" Visible="false"></asp:TextBox>
                                <asp:DropDownList class="form-control" ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div style="padding:2px;"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="col-md-4">PicName</div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtPICName" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtProses" runat="server" Visible="False"></asp:TextBox>
                            <asp:DropDownList  class="form-control" ID="ddlPIC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPIC_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px;"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">SectionName</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlBagian" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBagian_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px;"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:Panel ID="Panel1" runat="server">
                <div class="contentlist" id="lstP" style="height: 360px">
                    <table class="table-bordered tbStandart" border="1">
                        <thead>
                            <tr class="tbHeader">
                                <th class="kotak" rowspan="2" style="width: 4%">
                                    No.
                                </th>
                                <th class="kotak" rowspan="2" style="width: 35%">
                                    Category Name
                                </th>
                                <th class="kotak" rowspan="2" style="width: 5%">
                                    Bobot
                                </th>
                                <th class="kotak" colspan="2">
                                    Pencapaian
                                </th>
                                <th class="kotak" rowspan="2" style="width: 5%">
                                    Score
                                </th>
                                <th class="kotak" rowspan="2" style="width: 5%">
                                    Point
                                </th>
                                <th class="kotak" rowspan="2" style="width: 7%">
                                    Status
                                </th>
                                <th class="kotak" rowspan="2" style="width: 10%">
                                    Tgl Input
                                </th>
                                <th class="kotak" rowspan="2" style="width: 10%">
                                    Tgl Approved
                                </th>
                                <th class="kotak" rowspan="2" style="width: 5%">
                                    &nbsp;
                                </th>
                            </tr>
                            <tr class="tbHeader">
                                <th class="kotak" style="width: 8%">
                                    Aktual
                                </th>
                                <th class="kotak" style="width: 8%">
                                    Target
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="lstSOP" runat="server" OnItemDataBound="lstSOP_DataBound" OnItemCommand="lstSOP_Command">
                            <ItemTemplate>
                                <tr class="EvenRows baris" title='<%# Eval("Target") %>' id="lstP" runat="server">
                                    <td class="kotak tengah">
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td class="kotak">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                            <tr>
                                                <td style="width: 90%">
                                                    <%--<%# Eval("Description") %>--%>
                                                    <asp:Label ID="LabelSOP" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </td>
                                                <%--<td style="width: 10%" class="angka">
                                                    <asp:CheckBox ID="chkRbb" runat="server" Visible="false" AutoPostBack="true" CssClass='<%# Container.ItemIndex %>'
                                                    OnCheckedChanged="chkRbb_Checked" ToolTip="Klik untuk mengisi nilai item ini" />
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="kotak tengah">
                                        <%# Eval("BobotNilai","{0:N0}") %>%
                                    </td>
                                    <td class="kotak">
                                        <asp:TextBox ID="txtKeterangan" runat="server" ToolTip='<%# Container.ItemIndex %>'
                                        CssClass="txtongrid" Width="100%" OnTextChanged="txtKeterangan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:TextBox ID="txtID" runat="server" Visible="false" Text='<%# Eval("CategoryID") %>'></asp:TextBox><!--ISO_USERCategory.ID,ISO_SOP.CategoryID-->
                                        <asp:TextBox ID="txtCatID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:TextBox><!--ISO_USERCategory.CategoryID,ISO_SOP.ID-->
                                        <asp:TextBox ID="txtBobot" runat="server" Visible="false" Text='<%# Eval("BobotNilai") %>'></asp:TextBox>
                                        <asp:TextBox ID="txtItemName" runat="server" Visible="false" Text='<%# Eval("Description") %>'></asp:TextBox>
                                    </td>
                                    <td class="kotak">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100%" CssClass="noBorder"
                                        AutoPostBack="true" OnTextChanged="ddlStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="kotak tengah">
                                    <asp:Label ID="txtPointNilai" runat="server"></asp:Label>
                                </td>
                                <td class="kotak tengah">
                                    <asp:Label ID="txtPoint" runat="server" Text='<%# Eval("Point") %>'></asp:Label>
                                </td>
                                <td class="kotak tengah">
                                    <asp:Label ID="txtStatus" runat="server"></asp:Label>
                                </td>
                                <td class="kotak tengah">
                                    <asp:Label ID="txttglinput" runat="server"></asp:Label>
                                </td>
                                <td class="kotak tengah">
                                    <asp:Label ID="txttglapproved" runat="server"></asp:Label>
                                </td>
                                <td class="kotak tengah">
                                    <asp:ImageButton ID="edit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                    CommandName="Edit" />
                                    <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                    CommandName="Save" Visible="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="Line3">
                                <td class="kotak angka" colspan="2">
                                    <b>Total Bobot</b> &nbsp;
                                </td>
                                <td class="kotak tengah bold">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </td>
                                <td class="kotak angka" colspan="3">
                                    <b>Total Point</b> &nbsp;
                                </td>
                                <td class="kotak tengah bold">
                                    <asp:Label ID="lblPoint" runat="server"></asp:Label>
                                </td>
                                <td class="kotak" colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="false">
    <div class="contentlist" id="Div2" style="height: 360px">
        <table class="tabel table-bordered" id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small;
        display: block" border="0">
        <thead>
            <tr class="tbHeader baris">
                <th class="kotak" rowspan="2" style="width: 4%">
                    No.
                </th>
                <th class="kotak" rowspan="2" style="width: 35%">
                    Category Name
                </th>
                <th class="kotak" rowspan="2" style="width: 5%">
                    Bobot
                </th>
                <th class="kotak" colspan="2">
                    Pencapaian
                </th>
                <th class="kotak" rowspan="2" style="width: 5%">
                    Score
                </th>
                <th class="kotak" rowspan="2" style="width: 5%">
                    Point
                </th>
                <th class="kotak" rowspan="2" style="width: 7%">
                    Status
                </th>
                <th class="kotak" rowspan="2" style="width: 10%">
                    Tgl Input
                    <th class="kotak" rowspan="2" style="width: 10%">
                        Tgl Approved
                    </th>
                    <th class="kotak" rowspan="2" style="width: 5%">
                        &nbsp;
                    </th>
                </tr>
                <tr class="tbHeader">
                    <th class="kotak" style="width: 8%">
                        Aktual
                    </th>
                    <th class="kotak" style="width: 8%">
                        Target
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="lstH" runat="server" OnItemDataBound="lstH_DataBound">
                <ItemTemplate>
                    <tr class="OddRows baris">
                        <td class="kotak tengah">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td class="kotak" colspan="6">
                            <b>
                                <%# Eval("PIC") %>
                                -
                                <%# Eval("BagianName") %></b>
                            </td>
                        </tr>
                        <asp:Repeater ID="lstRkp" runat="server" OnItemDataBound="lstPES_DataBound">
                        <ItemTemplate>
                            <tr class=" EvenRows baris">
                                <td class="kotak angka">
                                    <%# Container.ItemIndex+1 %>&nbsp;
                                </td>
                                <td class="kotak">
                                    <%# Eval("SOPName") %>                                                                    
                                </td>
                                <td class="kotak tengah">
                                    <%# Eval("BobotNilai", "{0:N0}")%>%
                                </td>
                                <td class="kotak">
                                    <%# Eval("Target") %>
                                </td>
                                <td class="kotak tengah ">
                                   <%# Eval("Pencapaian")%>                                                                
                               </td>
                               <td class="kotak tengah">
                                <asp:Label ID="sc" runat="server" Text='<%# Eval("Score", "{0:N0}")%>'></asp:Label>
                            </td>
                            <td class="kotak tengah">
                                <%# Eval("Nilai", "{0:N2}")%>
                            </td>
                            <td class="kotak">
                                <%# Eval("StatusApv")%>
                            </td>
                            <td class="kotak tengah">
                                <%# (Convert.ToDateTime(Eval("TglInput")))%>
                            </td>
                            <td class="kotak tengah">
                                <%# (Convert.ToDateTime(Eval("TglApproved")))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="lstTot" runat="server">
                <ItemTemplate>
                    <tr class="total">
                        <td colspan="2" class="angka kotak">
                            <b>Total</b>&nbsp;
                        </td>
                        <td class="kotak tengah">
                            <%# Eval("TotalBobot","{0:N0}") %>%
                        </td>
                        <td colspan="3" class="angka kotak">
                            <b>Total Point</b>&nbsp;
                        </td>
                        <td class="kotak tengah">
                            <%# Eval("TotalNilai","{0:N2}") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</tbody>
</table>
</div>
</asp:Panel>
</div>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
