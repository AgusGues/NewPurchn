<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Destacking.aspx.cs" EnableEventValidation="false" Inherits="GRCweb1.Modul.Factory.Destacking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title></title>

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
                </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">INPUT DESTACKING</h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" class="btn btn-sm btn-info" onserverclick="btnNew_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 44px;"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnUpdate" runat="server" disabled="disabled" class="btn btn-sm btn-info" onserverclick="btnUpdate_ServerClick"
                                            style="background-color: white; font-weight: bold; font-size: 11px; width: 58px; height: 22px;"
                                            type="button" value="Simpan" visible="False" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnDelete" runat="server" Enabled="False" class="btn btn-sm btn-danger" OnClick="Button1_Click"
                                            OnClientClick="return confirmation();" Text="Hapus" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="150px" class="form-control" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            <asp:ListItem Value="F.nopalet=">Palet</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" onkeyup="this.value=this.value.toUpperCase()"
                                            Width="89px" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSearch" runat="server" class="btn btn-sm btn-info" onserverclick="btnSearch_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-12">
                                        <asp:CheckBox ID="ChkHide" runat="server" AutoPostBack="True" Checked="True" Font-Size="X-Small"
                                                    OnCheckedChanged="ChkHide_CheckedChanged" Text="Tampilkan Input Data Destacking" />
                                    <div style="padding: 2px"></div>
                                            </div>
                                <div class="col-xs-12 col-sm-8">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Insert Mode"></asp:Label>
                                        </div>
                                        <div class="panel-body">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <div class="row">
                                                    <span>
                                                        <asp:RadioButton ID="RBProses" runat="server" GroupName="a" />
                                                        <label for="form-field-9" style="font-size: 12px">Untuk Proses Jemur</label>
                                                    </span>
                                                    <span>
                                                        <asp:RadioButton ID="RBProses0" runat="server" GroupName="a" Checked="True" />
                                                        <label for="form-field-9" style="font-size: 12px">Untuk Proses Oven</label>
                                                    </span>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Tgl. Produksi</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtDate" runat="server" style="font-size: 12px" class="form-control" AutoPostBack="True" Enabled="False"
                                                                OnTextChanged="txtDate_TextChanged" ReadOnly="True"></asp:TextBox>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Input Jam</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTglIn" style="font-size: 12px" runat="server" AutoPostBack="True"
                                                                BackColor="#FF99FF" class="form-control"
                                                                onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="160px"></asp:TextBox>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Jenis</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlFormula" style="font-size: 12px" runat="server" AutoPostBack="True"
                                                                class="form-control" OnSelectedIndexChanged="ddlFormula_SelectedIndexChanged"
                                                                OnTextChanged="ddlFormula_TextChanged" Width="127px">
                                                            </asp:DropDownList>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">No. Produksi</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlPartno" style="font-size: 12px" runat="server" class="form-control" OnSelectedIndexChanged="ddlPartno_SelectedIndexChanged"
                                                                Width="165px" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">No. Palet</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtNoPalet" runat="server" style="font-size: 12px" AutoPostBack="True" class="form-control" OnTextChanged="txtNoPalet_TextChanged"
                                                                Width="72px" ReadOnly="True">1000</asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtNoPalet_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx2"
                                                                CompletionInterval="200" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetPaletBM" ServicePath="AutoComplete.asmx"
                                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtNoPalet">
                                                            </cc1:AutoCompleteExtender>
                                                            <asp:TextBox ID="txtNoProduksi" runat="server" AutoPostBack="True" class="form-control"
                                                                OnTextChanged="txtNoProduksi_TextChanged" Visible="False" Width="100px"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtNoProduksi_AutoCompleteExtender" runat="server"
                                                                BehaviorID="AutoCompleteEx0" CompletionInterval="200" CompletionSetCount="20"
                                                                EnableCaching="true" Enabled="True" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                TargetControlID="txtNoProduksi">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Shift </label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlShift" style="font-size: 12px" runat="server" class="form-control"
                                                                Width="72px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">s/d</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTglOut" style="font-size: 12px" runat="server" AutoPostBack="True"
                                                                BackColor="#FF99FF" class="form-control"
                                                                onkeyup="this.value=this.value.toUpperCase()" TabIndex="1" Width="160px"></asp:TextBox>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Group</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlGroup" style="font-size: 12px" runat="server" class="form-control"
                                                                OnTextChanged="ddlGroup_TextChanged" Width="72px">
                                                            </asp:DropDownList>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Lokasi</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtLokasi" runat="server" style="font-size: 12px" AutoPostBack="True" class="form-control" OnTextChanged="txtLokasi_TextChanged"
                                                                Width="72px" ReadOnly="True">A1000</asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="txtLokasiAutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx1"
                                                                CompletionInterval="500" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetLokasiBM" ServicePath="AutoComplete.asmx"
                                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtLokasi">
                                                            </cc1:AutoCompleteExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label for="form-field-9" style="font-size: 14px">Jumlah</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtJumlah" runat="server" style="font-size: 14px" class="form-control" OnPreRender="txtJumlah_PreRender"
                                                                OnTextChanged="txtJumlah_TextChanged" Width="72px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="None"
                                                                AutoComplete="False" ClearMaskOnLostFocus="false" DisplayMoney="None" ErrorTooltipEnabled="True"
                                                                Filtered="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890" InputDirection="RightToLeft"
                                                                Mask="9999" MaskType="None" MessageValidatorTip="False"
                                                                PromptCharacter=" " TargetControlID="txtJumlah"></cc1:MaskedEditExtender>
                                                            <div style="padding: 2px"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="panel panel-footer text-right">
                                            <asp:TextBox ID="txtID" runat="server" class="form-control" ReadOnly="True" Visible="False" Width="54px"></asp:TextBox>
                                            <asp:Button ID="btnTransfer" runat="server"  class="btn btn-sm btn-primary" OnClick="Button1_Click1"
                                                Text="Transfer" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC"
                                            BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana"
                                            Font-Size="8pt" ForeColor="#003399" Height="149px" OnSelectionChanged="Calendar1_SelectionChanged"
                                            Width="225px">
                                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                                                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        </asp:Calendar>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">List Produksi Destacking </h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-12">
                                    <span class="input-icon input-icon-right">
                                         <label for="form-field-9" style="font-size: 14px">dari Tanggal</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtdrtanggal" runat="server" class="form-control" AutoPostBack="True"
                                                Width="151px"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <label for="form-field-9" style="font-size: 14px">s/d Tanggal</label>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" class="form-control"
                                                Width="151px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnRefresh" runat="server"
                                            onserverclick="btnRefresh_ServerClick" class="btn btn-sm btn-primary" style="background-image: none; font-weight: bold; background-color: white"
                                            type="button" value="Refresh Data"
                                            visible="True" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton3_Click">Export To Excel</asp:LinkButton>
                                    </span>
                                </div>
                                <div class="col-xs-12 col-sm-12">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                        GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                        PageSize="20" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl. Produksi">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Formula" HeaderText="Jenis">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PlantName" HeaderText="Line" />
                                            <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                            <asp:BoundField DataField="PlantGroup" HeaderText="Group">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lokasi" HeaderText="Lokasi">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PAlet" HeaderText="PAlet">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Qty" HeaderText="Qty">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="drJam" HeaderText="Dari Jam" />
                                            <asp:BoundField DataField="sdJam" HeaderText="s/d Jam" />
                                            <asp:ButtonField CommandName="rubah" Text="Edit">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>