<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pengiriman.aspx.cs" Inherits="GRCweb1.Modul.Factory.Pengiriman" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>Pengiriman</title>

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

                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">PROSES PENGIRIMAN </h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" class="btn btn-sm" runat="server" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold;"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" class="form-control" runat="server" AutoPostBack="True"
                                            Width="102px">
                                            <asp:ListItem Value="ScheduleNo">Schedule No</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>

                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtScheduleNo" class="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtScheduleNo_TextChanged" Width="168px"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" class="btn btn-sm btn-info" type="button" value="Cari" onclick="return btnSearch_onclick()" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-6">
                                        <label for="form-field-9">Input Pengiriman : </label>
                                        <asp:RadioButton ID="RBOffLine" runat="server" Style="margin: 5px;" GroupName="M" Text="Mode Off Line" />
                                        <asp:RadioButton ID="RBOnLine" runat="server" Checked="True" GroupName="M" Text="Mode On Line" />
                                    </div>
                                    <div class="col-xs-12 col-sm-3">
                                        <asp:CheckBox ID="chkDeco" runat="server" Checked="True" OnCheckedChanged="chkDeco_CheckedChanged" Text="Konversi DecoStone" AutoPostBack="True" />
                                    </div>
                                    <div class="col-xs-12 col-sm-3">
                                        <asp:CheckBox ID="ChkHide1" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small" OnCheckedChanged="ChkHide1_CheckedChanged" Text="Tampilkan Input Pengiriman" />
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Panel ID="Panel8" runat="server">

                                        <div class="col-xs-12 col-sm-6">
                                            <label for="form-field-9">Type Surat Jalan : </label>

                                            <asp:RadioButton ID="RBToko" runat="server" AutoPostBack="True" Checked="True" GroupName="a"
                                                OnCheckedChanged="RBToko_CheckedChanged" Text="Toko" />
                                            &nbsp;<asp:RadioButton ID="RBDepo" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBDepo_CheckedChanged" Text="Depo" />
                                            &nbsp;
                                                            <asp:RadioButton ID="RBExport" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBExport_CheckedChanged"
                                                                Text="Export" />
                                            &nbsp;<asp:RadioButton ID="RBCuma" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBCuma_CheckedChanged" Text="Memo" />

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">SJ No.</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtSJNo" class="form-control" runat="server" AutoPostBack="True"
                                                        OnTextChanged="txtSJNo_TextChanged" Width="230px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetSJNofromSiapKirim" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtSJNo" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                    <div style="padding: 2px"></div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">Tgl.Surat Jalan</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtDate" class="form-control" runat="server" AutoPostBack="True"
                                                        Width="182px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtDate"></cc1:CalendarExtender>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">Tgl. Aktual Kirim</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtDate0" class="form-control" runat="server" AutoPostBack="True"
                                                        Width="182px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtDate0"></cc1:CalendarExtender>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">OP No</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtOPNo" class="form-control" runat="server" AutoPostBack="True"
                                                        Width="182px" OnTextChanged="txtOPNo_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtOPNo_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                        CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetOPNo" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtOPNo" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="form-field-9">Customer</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtCustomer" class="form-control" runat="server" AutoPostBack="True"
                                                        Width="376px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtCustomer_AutoCompleteExtender" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" ContextKey="0"
                                                        EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetCustomer" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtCustomer" UseContextKey="true">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-6">
                                            <asp:Panel ID="Panel10" runat="server">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                                    PageSize="24" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                        <asp:BoundField DataField="ScheduleNo" HeaderText="ScheduleNo" />
                                                        <asp:BoundField DataField="SuratJalanNo" HeaderText="SuratJalanNo" />
                                                        <asp:BoundField DataField="CreatedTime" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <asp:CheckBox ID="ChkSJNo" runat="server" AutoPostBack="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide2_CheckedChanged" Text="Data penyiapan By SJNo." Visible="False" />
                        <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                            OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan Item Surat Jalan" />
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Item Surat Jalan</h3>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel9" runat="server">
                                    <asp:GridView ID="GridViewSJ" runat="server" AutoGenerateColumns="False" CellPadding="6"
                                        EnableModelValidation="True" Font-Size="X-Small" ForeColor="#333333" BorderStyle="Groove" BorderWidth="2px"
                                        OnRowCommand="GridViewSJ_RowCommand" OnRowDataBound="GridViewSJ_RowDataBound">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="ItemIDSJ" HeaderText="ItemID" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tebal" HeaderText="T">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lebar" HeaderText="L">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Panjang" HeaderText="P" />
                                            <asp:BoundField DataField="qty" HeaderText="QtySJ">
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtyP" HeaderText="QtyP">
                                                <ItemStyle ForeColor="Blue" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Potong Stock Pabrik">
                                                <ItemTemplate>
                                                    <asp:Button ID="btn_Show" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                        CommandName="Details" Font-Size="X-Small" Height="21px" Style="margin-top: 0px"
                                                        Text="List Item Penyiapan" Width="124px" />
                                                    <asp:Button ID="Cancel" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                        CommandName="Cancel" Font-Size="X-Small" Height="21px" Text="Hide Details" Visible="false"
                                                        Width="85px" />
                                                    <%--child gridview with bound fields --%>
                                                    <asp:Button ID="transfer" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                                                        CommandName="transfer" Font-Size="X-Small" Height="21px" Text="Transfer" Visible="False"
                                                        Width="85px" />
                                                    &nbsp;<asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False"
                                                        PageSize="22" Width="100%">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="ItemIdser" HeaderText="ItemId" />
                                                            <asp:BoundField DataField="tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                            <asp:BoundField DataField="groupdesc" HeaderText="Group desc" />
                                                            <asp:BoundField DataField="partnokrm" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="lokasiser" HeaderText="dr Lokasi" />
                                                            <asp:BoundField DataField="lokasikrm" HeaderText="Lokasi" />
                                                            <asp:BoundField DataField="qty" HeaderText="Qty Penyiapan">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Qty Kirim">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQtyKirim" runat="server" Font-Size="X-Small" Height="20px" Width="64px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Font-Size="X-Small" Height="19px" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                        Width="100%">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="tgltrans" DataFormatString="{0:d}" HeaderText="Tanggal" />
                                                            <asp:BoundField DataField="groupdesc" HeaderText="Group desc" />
                                                            <asp:BoundField DataField="partnokrm" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="lokasiser" HeaderText="dr Lokasi" />
                                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                                            <asp:BoundField DataField="lokasikrm" HeaderText="ke Lokasi" />
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="X-Small" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Item Surat Jalan</h3>
                            </div>

                            <div class="panel-body">
                                <asp:Panel ID="Panel5" runat="server">
                                    <asp:GridView ID="GridViewKirim" runat="server" AutoGenerateColumns="False" PageSize="22"
                                        Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="opno" HeaderText="OPNO" />
                                            <asp:BoundField DataField="sjno" HeaderText="SJNO" />
                                            <asp:BoundField DataField="customer" HeaderText="Customer" />
                                            <asp:BoundField DataField="tgltrans" HeaderText="Tanggal" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="groupdesc" HeaderText="Group desc" />
                                            <asp:BoundField DataField="partnokrm" HeaderText="PartNo" />
                                            <asp:BoundField DataField="lokasiser" HeaderText="dr Lokasi" />
                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                            <asp:BoundField DataField="lokasikrm" HeaderText="ke Lokasi" />
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>