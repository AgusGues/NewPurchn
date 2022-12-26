<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="t3_BA_Input.aspx.cs" Inherits="GRCweb1.Modul.Factory.t3_BA_Input" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title>T3 Retur</title>

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

                <script type="text/javascript">
                    function confirmation() {
                        if (confirm('Yakin mau hapus data ?')) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                    function OpenDialog(id) {
                        params = 'dialogWidth:820px';
                        params += '; dialogHeight:200px'
                        params += '; top=0, left=0'
                        params += '; resizable:no'
                        params += ';scrollbars:no';
                        window.showModalDialog("../../ModalDialog/UploadFileT3_BA.aspx?ba=" + id + "&tablename=T3_BAAttachment", "UploadFile", params);
                    };
                </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">BERITA ACARA TAHAP 3</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnNew" runat="server" class="btn btn-sm" onserverclick="btnNew_ServerClick"
                                            type="button" value="Baru" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSimpan" runat="server" class="btn btn-sm" onserverclick="btnSimpan_ServerClick"
                                            type="button" value="Simpan" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnLampiran" runat="server" class="btn btn-sm" onserverclick="btnLampiran_ServerClick"
                                            type="button" value="Upload Lampiran" disabled="disabled" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlCari" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="NoBA">No. BA</asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtCariBA" runat="server" class="form-control" Width="208px"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnCari" runat="server" class="btn btn-sm" onserverclick="btnCari_ServerClick"
                                            type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel panel-body">
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">No. Berita Acara</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNoBA" runat="server" class="form-control" Width="235px"
                                                AutoPostBack="True" OnTextChanged="txtNoBA_TextChanged"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Tgl. Berita Acara</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTglBA" runat="server" AutoPostBack="True" class="form-control"
                                                OnTextChanged="txtTglBA_TextChanged" Width="235px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTglBA"></cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Type Berita Acara </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlKeterangan" runat="server" class="form-control" Width="235px">
                                                <asp:ListItem>PERMINTAAN PRODUK</asp:ListItem>
                                                <asp:ListItem>PRODUK MASUK</asp:ListItem>
                                                <asp:ListItem>KONSENSI</asp:ListItem>
                                            </asp:DropDownList>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <input id="btnAddItem" runat="server" onserverclick="btnTansfer_ServerClick" class="btn btn-sm btn-info" type="button" value="Add Item" />
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Partno</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPartnoA" runat="server" AutoPostBack="True" class="form-control"
                                                Width="182px" OnTextChanged="txtPartnoA_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoA">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Quantity</label>
                                        </div>
                                        <div class="col-md-8">
                                            <span class="input-icon input-icon-right">
                                                <asp:TextBox ID="txtQty1" runat="server" class="form-control" OnPreRender="txtQty1_PreRender"
                                                    OnTextChanged="txtQty1_TextChanged" Width="65px"></asp:TextBox>
                                            </span>
                                            <span class="input-icon input-icon-right">
                                                <label style="font-size: 13px">
                                                    <asp:RadioButton ID="RBIn" runat="server" AutoPostBack="True" Checked="True"
                                                        GroupName="A" />Adjust In</label>
                                            </span>
                                            <span class="input-icon input-icon-right">
                                                <label style="font-size: 13px">
                                                    <asp:RadioButton ID="RBOut" runat="server" AutoPostBack="True" GroupName="A"
                                                        OnCheckedChanged="RBOut_CheckedChanged" />Adjust Out
                                                </label>
                                            </span>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Dari/Ke Lokasi</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtLokasi" runat="server" AutoPostBack="True" class="form-control" Visible="True" Width="65px"></asp:TextBox><cc1:AutoCompleteExtender
                                                ID="txtLokasiC_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasi">
                                            </cc1:AutoCompleteExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 12px">Keterangan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtKeterangan" runat="server" class="form-control" Width="235px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-sm-4">
                                    <div class="col-md-12">
                                        <label for="form-field-9" style="font-size: 12px">List Lampiran</label>
                                    </div>

                                    <div class="col-md-12">
                                        <table width="100%" style="font-size: x-small">
                                            <thead>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td>
                                                                        <%# Eval("FileName") %>
                                                                    </td>
                                                                    <td align="right" width="25%">
                                                                        <asp:ImageButton ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                                            CommandName="pre" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Logo_Download.png"
                                                                            ToolTip="Click to Preview Document" />
                                                                        <asp:ImageButton ID="hapus" runat="server" AlternateText='<%# Eval("BAID") %>' CommandArgument="<%# Container.ItemIndex %>"
                                                                            CommandName="hps" CssClass='<%# Eval("ID") %>' ImageUrl="~/images/Delete.png"
                                                                            ToolTip="Click for delete attachment" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                           
                        </div>

                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel panel-body">
                                <asp:GridView ID="GridItem0" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                    PageSize="22" Width="100%">
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                        <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                        <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                        <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                        <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                    </Columns>
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">List Berita Acara Tahap 3</h3>
                                <div class="pull-right">
                                    <asp:TextBox ID="txtdrtanggal" runat="server"  AutoPostBack="True" OnTextChanged="txtdrtanggal_TextChanged" ForeColor="Black"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                                    s/d Tanggal
                        <asp:TextBox ID="txtsdtanggal" runat="server"  AutoPostBack="True" OnTextChanged="txtsdtanggal_TextChanged" ForeColor="Black" ></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="panel-body">
                                <asp:Panel ID="Panel9" runat="server" Height="200px" ScrollBars="Vertical">
                                    <asp:GridView ID="GridItemList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridItem_RowCommand"
                                        PageSize="22" Width="100%">
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="BADate" DataFormatString="{0:dd-MMM-yyyy}"
                                                HeaderText="Tanggal" />
                                            <asp:BoundField DataField="BANo" HeaderText="No BA" />
                                            <asp:BoundField DataField="BAType" HeaderText="BA Type" />
                                            <asp:BoundField DataField="AdjustType" HeaderText="Adjust Type" />
                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                            <asp:BoundField DataField="QtyIn" HeaderText="QtyIn" />
                                            <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                            <asp:BoundField DataField="Approval" HeaderText="Approval" />
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
