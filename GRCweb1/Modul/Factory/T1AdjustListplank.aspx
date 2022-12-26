<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T1AdjustListplank.aspx.cs" Inherits="GRCweb1.Modul.Factory.T1AdjustListplank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>Destacking</title>

                <meta name="description" content="Common form elements and layouts" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
                <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
                <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">PROSES ADJUSTMENT LISTPLANK</h3>
                            </div>
                            <div class="panel-body">

                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9">No Adjust</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtAdjustNo" runat="server" class="form-control" ReadOnly="True"
                                                ToolTip="Otomatis dibuat saat disimpan" Width="244px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9">Tanggal Adjust</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTanggal" class="form-control" runat="server" AutoPostBack="True"
                                                Width="244px" OnTextChanged="txtTanggal_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9">No Berita Acara</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtBA" class="form-control" runat="server" Width="244px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="panel panel-info">

                                        <div class="panel-heading">
                                            <h3 class="panel-title">Type Adjustment :
                                                                                                                     <asp:RadioButton ID="RBIn" runat="server" Style="margin: 7px;" AutoPostBack="True" Checked="True" GroupName="A" Text="Adjust In" />
                                                <asp:RadioButton ID="RBOut" runat="server" AutoPostBack="True" GroupName="A" Text="Adjust Out" />
                                            </h3>
                                        </div>
                                        <div class="panel-body">

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">Proses</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList class="form-control" ID="ddlprocess" Width="50%" runat="server">
                                                        <asp:ListItem>I99</asp:ListItem>
                                                        <asp:ListItem>RuningSaw</asp:ListItem>
                                                        <asp:ListItem>Bevel</asp:ListItem>
                                                        <asp:ListItem>Straping</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">PartNo Awal</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtPartnoA0" class="form-control" runat="server" AutoPostBack="True"
                                                        Height="25px" OnTextChanged="txtPartnoA0_TextChanged"
                                                        Width="50%"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtPartnoA0_AutoCompleteExtender" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                        FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetNoProdukAsalListplank" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtPartnoA0">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">PartNo Tujuan</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtPartnoA" class="form-control" runat="server" AutoPostBack="True"
                                                        Height="25px" OnTextChanged="txtPartnoA_TextChanged"
                                                        Width="50%"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                        FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProduk"
                                                        ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">Qty Adjust</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtQty1" class="form-control" runat="server" Height="25px" OnPreRender="txtQty1_PreRender"
                                                        OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer text-right">
                                            <input id="btnTansfer" runat="server" class="btn btn-sm btn-green" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Add Item" /></td>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel-footer text-right">
                                <input id="btnNew" runat="server" class="btn btn-sm btn-info" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                    type="button" value="Baru" />
                                <input id="btnSimpan" runat="server" class="btn btn-sm btn-primary" onserverclick="btnSimpan_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 63px;"
                                    type="button" value="Simpan" />
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6 form-group-sm">
                                    <div class="row">
                                        <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                            PageSize="22" Width="100%">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="AdjustDate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                                <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                                <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Process" HeaderText="Process" />
                                                <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                                <asp:ButtonField CommandName="hapus" Text="Cancel" />
                                            </Columns>
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Item Adjustment by
                                                            <asp:RadioButton ID="RBTanggal" Style="margin: 7px;" runat="server" AutoPostBack="True" Checked="True"
                                                                Font-Size="X-Small" GroupName="B" OnCheckedChanged="RBTanggal_CheckedChanged"
                                                                Text="Tanggal" />
                                    <asp:RadioButton ID="RBNoBA" Style="margin: 7px;" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        GroupName="B" OnCheckedChanged="RBAdjustNo_CheckedChanged" Text="No. Berita Acara" />
                                    <asp:RadioButton ID="RBApproval" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                        GroupName="B" OnCheckedChanged="RBApproval_CheckedChanged" Text="Approval" />
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                </div>
                                <div class="row">
                                    <asp:Panel ID="PanelApprove" runat="server" Visible="False">
                                        <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                            OnCheckedChanged="ChkAll_CheckedChanged" Text="Approve All" />
                                        &nbsp;<input id="btnApprove" runat="server" onserverclick="btnApprove_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 63px; height: 23px;"
                                            type="button" value="Approve" />
                                    </asp:Panel>
                                </div>

                                <div class="row">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:GridView ID="GridItem" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                            PageSize="22" Width="100%">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="AdjustDate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                                <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                                <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Process" HeaderText="Process" />
                                                <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                                <asp:ButtonField CommandName="hapus" Text="Cancel" Visible="False" />
                                            </Columns>
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                        <asp:GridView ID="GridApprove" runat="server" AutoGenerateColumns="False" PageSize="22"
                                            Width="100%">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkapv" runat="server" Text="Approve" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="adjustdate" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                <asp:BoundField DataField="AdjustNo" HeaderText="AdjustNo" />
                                                <asp:BoundField DataField="NoBA" HeaderText="BA" />
                                                <asp:BoundField DataField="AdjustType" HeaderText="Type" />
                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Process" HeaderText="Process" />
                                            </Columns>
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>