<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormDefectNew.aspx.cs" Inherits="GRCweb1.Modul.DefectMenu.FormDefectNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
TagPrefix="BDP" %>
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
<script language="javascript" type="text/javascript">
    function change_color() {
        document.getElementById("<%=btnTansfer.ClientID%>").style.backgroundColor = "blue";
        document.getElementById("<%=btnTansfer.ClientID%>").style.color = "white";
        document.que
    }
    function change_color1() {
        document.getElementById("<%=btnTansfer.ClientID%>").style.backgroundColor = "white";
        document.getElementById("<%=btnTansfer.ClientID%>").style.color = "black";
    }
    function change_color2() {
        document.getElementById("<%=txtNoPalet.ClientID%>").style.backgroundColor = "blue";
        document.getElementById("<%=txtNoPalet.ClientID%>").style.color = "white";
    }
    function change_color3() {
        document.getElementById("<%=txtNoPalet.ClientID%>").style.backgroundColor = "white";
        document.getElementById("<%=txtNoPalet.ClientID%>").style.color = "black";
    }

    function doClick(textboxName, button, e) {
        var key;
        if (window.event)
            key = window.event.keyCode;  
        else
            key = e.which;   
        if (key == 13) {
            var txtbox = document.getElementById(textboxName);
            var btn = document.getElementById(button);
            if (btn != null) {
                btn.click();
            }
            if (txtbox != null) { 
                txtbox.focus();
                event.keyCode = 0
            }
        }
    }
    function btnTansfer_onclick() {

    }

</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <span>Input Defect</span>
            <div class="pull-right">
                <input class="btn btn-info" id="btnActivate" runat="server" onserverclick="btnActivate_ServerClick" type="button" visible="False" />
                <input class="btn btn-info" id="btnNew" runat="server" onserverclick="btnNew_ServerClick" type="button" value="Baru" visible="True" />
                <asp:RadioButton ID="RBInput" runat="server" AutoPostBack="True" Font-Size="X-Small"
                GroupName="m" Text="Mode Input" Checked="True" OnCheckedChanged="RBInput_CheckedChanged" />
                <asp:RadioButton ID="RBInput0" runat="server" AutoPostBack="True" Font-Size="X-Small"
                GroupName="m" Text="Mode Hapus" OnCheckedChanged="RBInput0_CheckedChanged" />
                <input class="btn btn-info" id="btnPrint" runat="server" type="button" value="Cetak" visible="False" />
                <asp:DropDownList ID="ddlSearch" runat="server" Width="120px" Visible="False">
                <asp:ListItem Value="DefectNo">NoDefect</asp:ListItem>
                <asp:ListItem Value="ReguCode">Regu</asp:ListItem></asp:DropDownList>
                <asp:TextBox ID="txtSearch" runat="server" Width="128px" Visible="False"></asp:TextBox>
                <input class="btn btn-info" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" type="button" value="Cari" visible="False" />
                <asp:CheckBox ID="ChkHide" runat="server" AutoPostBack="True" Checked="True" 
                Font-Size="X-Small" OnCheckedChanged="ChkHide_CheckedChanged" 
                Text="Tampilkan List Produksi" Visible="False" />
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="PanelDelete" runat="server" Width="100%" Visible="False">
            Tanggal Periksa
            <BDP:BDPLite ID="txtDatePeriksa0" runat="server" CssClass="styleDef" Enabled="true"
            ToolTip="klik icon untuk merubah tanggal">
            <TextBoxStyle Font-Size="X-Small" /></BDP:BDPLite>
            No. Palet
            <asp:TextBox ID="txtNoPaletD" runat="server" AutoPostBack="True" Height="20px" onfocus="this.select()"
            Width="72px"></asp:TextBox>
            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx2"
            CompletionInterval="200" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
            MinimumPrefixLength="1" ServiceMethod="GetPaletBM" ServicePath="AutoComplete.asmx"
            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtNoPaletD"></cc1:AutoCompleteExtender>
            <input id="BtnHapus" runat="server" style="background-color: white; 
            font-weight: bold; font-size: 11px;" type="button" onserverclick="btnDelete_ServerClick" value="Hapus" visible="true" /><asp:TextBox 
            ID="txtNoPalet" runat="server" AutoPostBack="True" Height="20px" 
            onblur="change_color3()" onfocus="change_color2();this.select()" 
            OnTextChanged="txtNoPalet_TextChanged" Width="72px"></asp:TextBox>
            <cc1:AutoCompleteExtender 
            ID="txtNoPalet_AutoCompleteExtender" runat="server" 
            BehaviorID="AutoCompleteEx2" CompletionInterval="200" CompletionSetCount="20" 
            EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1" 
            ServiceMethod="GetPaletBM" ServicePath="AutoComplete.asmx" 
            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtNoPalet"></cc1:AutoCompleteExtender></asp:Panel>

            <div style="padding: 2px;"></div>
            <asp:Panel ID="Panel3" runat="server" BorderColor="#CCFFFF" Wrap="False">
            <div class="row">
                <div class="col-md-2">
                    <asp:RadioButton ID="RBTglProduksi" runat="server" AutoPostBack="True" Checked="False"
                    Font-Size="X-Small" GroupName="a" OnCheckedChanged="RBTglProduksi_CheckedChanged"
                    Text="Tgl. Produksi" Visible="False" />
                    <asp:RadioButton ID="RBTglSerah" runat="server" AutoPostBack="True" Font-Size="X-Small"
                    GroupName="a" OnCheckedChanged="RBTglSerah_CheckedChanged" Text="Tgl. Produksi"
                    Checked="True" />
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="#3399FF" Font-Size="XX-Small"
                    Height="16px" OnSelectionChanged="Calendar1_SelectionChanged" Width="178px"></asp:Calendar>
                </div>
                <div class="col-md-10">
                    <asp:RadioButton ID="RBSerah1" runat="server" AutoPostBack="True" Checked="True"
                    GroupName="c" OnCheckedChanged="RBSerah1_CheckedChanged" 
                    Text="List Produksi" />
                    &nbsp;
                    <asp:RadioButton ID="RBSerah2" runat="server" AutoPostBack="True" GroupName="c"
                    OnCheckedChanged="RBSerah2_CheckedChanged" Text="List Serah BP ListPlank" 
                    Visible="False" />
                    &nbsp;
                    <asp:CheckBox ID="ChkPartNo" runat="server" AutoPostBack="True" OnCheckedChanged="ChkPartNo_CheckedChanged"
                    Text="Sort By Partno Pelarian" Visible="False" />
                    &nbsp;
                    <asp:DropDownList ID="ddlPartno" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPartno_SelectedIndexChanged"
                    Visible="False" Width="200px"></asp:DropDownList>
                    &nbsp; 
                    Defect No
                    <asp:TextBox ID="txtDefectNo" runat="server" BorderStyle="Groove" 
                    Enabled="False" MaxLength="25" OnTextChanged="txtDefectNo_TextChanged" 
                    Width="72px"></asp:TextBox>
                    &nbsp;
                    <asp:Panel ID="Panel4" runat="server" Height="143px" ScrollBars="Auto" Style="margin-left: 0px">
                    <asp:GridView ID="GridViewSerah" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewSerah_RowCommand"
                    PageSize="15" Width="100%">
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="white" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl. Produksi">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="Formula" HeaderText="Formula">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="PlantGroup" HeaderText="Group">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="Lokasi" HeaderText="Lokasi">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="PartNo" HeaderText="PartNo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="PAlet" HeaderText="PAlet">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="Qty" HeaderText="Qty Produksi">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField DataField="QtyPotong" HeaderText="Qty Potong" />
                        <asp:BoundField DataField="Sisa" HeaderText="Sisa" />
                        <asp:ButtonField CommandName="pilih" Text="Pilih" />
                    </Columns>
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView></asp:Panel>
                </div>
            </div></asp:Panel>

            <div style="padding: 2px;"></div>
            <asp:Panel ID="Panel2" runat="server">
            <div class="row">
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-md-4">TglSerah</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtDatePeriksa" runat="server" AutoPostBack="True" ontextchanged="txtDatePeriksa_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDatePeriksa_CalendarExtender" runat="server" 
                            Format="dd-MMM-yyyy" TargetControlID="txtDatePeriksa"></cc1:CalendarExtender>
                            <div style="padding: 2px;"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">TglProduksi</div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtDateProd" runat="server" AutoPostBack="True" OnTextChanged="txtDateProd_TextChanged"
                            ReadOnly="True"></asp:TextBox>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">UkuranDefect</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlUkuran0" runat="server" OnSelectedIndexChanged="ddlUkuran0_SelectedIndexChanged"
                            ></asp:DropDownList>
                            &nbsp;<asp:Label ID="LblBagi" runat="server" ForeColor="Red"></asp:Label>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label3" runat="server"
                            ForeColor="blue" Height="16px" Text="Total Potong" Width="107px"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtTotalPotong" runat="server"
                            AutoPostBack="True" ontextchanged="txtTotalPotong_TextChanged"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            AutoComplete="False" ClearTextOnInvalid="False" ErrorTooltipEnabled="True" 
                            InputDirection="RightToLeft" Mask="9999" MaskType="Number" 
                            OnFocusCssClass="styleDef" PromptCharacter="_" TargetControlID="txtTotalPotong"></cc1:MaskedEditExtender>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">GroupCuter</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlCutter" runat="server" 
                            OnSelectedIndexChanged="ddlCutter_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">GroupProduksi</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" width="100%" ID="ddlProd" runat="server" Enabled="False"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label5" runat="server"
                            ForeColor="blue" Height="16px" Text="Total BP " Width="136px"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtTotalBP" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="txtTotalBP_MaskedEditExtender" runat="server" 
                            AutoComplete="False" ClearTextOnInvalid="False" ErrorTooltipEnabled="True" 
                            InputDirection="RightToLeft" Mask="9999" MaskType="Number" 
                            OnFocusCssClass="styleDef" PromptCharacter="_" TargetControlID="txtTotalBP"></cc1:MaskedEditExtender>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">GroupJemur</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" ID="ddlJemur" runat="server"
                            OnSelectedIndexChanged="ddlJemur_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">Jenis</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" width="100%" ID="ddlJenis" runat="server" Enabled="False" 
                            OnSelectedIndexChanged="ddlJenis_SelectedIndexChanged">
                            <asp:ListItem Value="0">--- ALL ---</asp:ListItem>
                            <asp:ListItem Value="1">INT</asp:ListItem>
                            <asp:ListItem Value="2">INP</asp:ListItem>
                            <asp:ListItem Value="3">EXT</asp:ListItem></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="Label6" runat="server" ForeColor="blue" Height="16px" Text="Total KW" Width="136px"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtTotalkw" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="txtTotalkw_MaskedEditExtender" runat="server" AutoComplete="False" ClearTextOnInvalid="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="9999" MaskType="Number" OnFocusCssClass="styleDef" PromptCharacter="_" TargetControlID="txtTotalkw" />
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">UkuranProduksi</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" width="100%" ID="ddlUkuran" runat="server" Enabled="False" onselectedindexchanged="ddlUkuran_SelectedIndexChanged"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">ProdukNo</div>
                        <div class="col-md-8">
                            <asp:DropDownList class="form-control" width="100%" ID="ddlFormula" runat="server" AutoPostBack="True" Enabled="False"></asp:DropDownList>
                            <div style="padding:2px"></div>
                        </div>
                    </div>
                </div>
            </div></asp:Panel>

            <asp:Panel ID="PanelDef" runat="server" Enabled="False" Height="160px">
            <table border="0" style="border-collapse: collapse;" width="100%">
                <tr>
                    <td bgcolor="#337ab7" colspan="7" style="padding: 15px" align="center" width="100%">
                        <table border="0" style="border-collapse: collapse;" width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LblDef0" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty0" runat="server" CssClass="style5" Style="margin-left: 0px"
                                    Width="25px" AutoPostBack="True" OnTextChanged="txtDefQty0_TextChanged"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 72px">
                                    <asp:Label ID="LID0" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef4" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty4" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty4_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 92px">
                                    <asp:Label ID="LID4" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef8" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty8" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty8_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 72px">
                                    <asp:Label ID="LID8" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="LblDef12" runat="server" Font-Bold="True"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty12" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty12_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID12" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LblDef1" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty1" runat="server" CssClass="style5" Style="margin-left: 0px"
                                    Width="25px" AutoPostBack="True" OnTextChanged="txtDefQty1_TextChanged"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 72px">
                                    <asp:Label ID="LID1" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef5" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty5" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty5_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 92px">
                                    <asp:Label ID="LID5" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef9" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty9" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty9_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID9" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDef13" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty13" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty13_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID13" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LblDef2" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty2" runat="server" CssClass="style5" Style="margin-left: 0px"
                                    Width="25px" AutoPostBack="True" OnTextChanged="txtDefQty2_TextChanged"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 72px">
                                    <asp:Label ID="LID2" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef6" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty6" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty6_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 92px">
                                    <asp:Label ID="LID6" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef10" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty10" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty10_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID10" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDef14" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty14" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty14_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID14" runat="server"
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LblDef3" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty3" runat="server" CssClass="style5" Style="margin-left: 0px"
                                    Width="25px" AutoPostBack="True" OnTextChanged="txtDefQty3_TextChanged"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 72px">
                                    <asp:Label ID="LID3" runat="server" ForeColor="blue"
                                    Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef7" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty7" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty7_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 92px">
                                    <asp:Label ID="LID7" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LblDef11" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty11" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty11_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID11" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblDef15" runat="server" 
                                    ForeColor="blue" Height="16px" Text="LblDef"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefQty15" runat="server" AutoPostBack="True" 
                                    CssClass="style5" OnTextChanged="txtDefQty15_TextChanged" 
                                    Style="margin-left: 0px" Width="25px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LID15" runat="server"  
                                    ForeColor="blue" Height="16px" Text="LblDef" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr bgcolor="#62a8d1">
                    <td align="right" valign="middle">
                        <asp:CheckBox ID="CheckBox1" runat="server" ForeColor="white" Text="Auto Calculate" />
                        <asp:Label ID="Label4" runat="server" ForeColor="white"
                        Height="16px" Text="Total Defect" Width="136px"></asp:Label>
                        &nbsp;
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox ID="txtTotalDefect" runat="server" ReadOnly="True" Width="50px">0</asp:TextBox>
                        <cc1:MaskedEditExtender ID="txtTotalDefect_MaskedEditExtender" runat="server" AutoComplete="False"
                        ClearTextOnInvalid="False" ErrorTooltipEnabled="True" InputDirection="RightToLeft"
                        Mask="9999" MaskType="Number" OnFocusCssClass="styleDef" PromptCharacter="_"
                        TargetControlID="txtTotalDefect"></cc1:MaskedEditExtender>
                    </td>
                    <td align="right" valign="middle">
                        &nbsp;
                    </td>
                    <td align="left" valign="middle">
                        &nbsp;
                    </td>
                    <td align="right" valign="middle">
                        &nbsp;
                    </td>
                    <td align="left" valign="middle">
                        &nbsp;
                    </td>
                    <td align="right" valign="middle">
                        <input id="btnTansfer" runat="server" onblur="change_color1()" onfocus="change_color()"
                        onserverclick="btnTansfer_ServerClick" style="background-image: none; font-weight: bold;
                        background-color: white" type="button" value="Transfer" visible="False" />
                        <asp:Button ID="Button1" runat="server" onblur="Button1_Click1" TabIndex="28" Text="Transfer"
                        OnClick="Button1_Click1" />
                    </td>
                </tr>
            </table></asp:Panel>
            <br>
            <div class="visible-xs">
                <div style="padding:30px;"></div>
            </div>
            <label style="background-color: #337ab7;width:100%;">
                <span style="color:white;">ListTransaksiDefect</span>
                <div class="pull-right">
                    <asp:RadioButton ID="RBLoadByTglProduksi" runat="server" AutoPostBack="True" GroupName="b"
                    OnCheckedChanged="RBLoadByTglProduksi_CheckedChanged" Text="By Tgl Produksi" style="color:white;"/>
                    &nbsp;
                    <asp:RadioButton ID="RBLoadByTglSerah" runat="server" AutoPostBack="True" Checked="True"
                    GroupName="b" OnCheckedChanged="RBLoadByTglSerah_CheckedChanged" Text="By Tgl Serah" style="color:white;"/>
                    <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px"
                    Width="151px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                    <span style="color:white;">s/d Tanggal</span>
                    <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px"
                    Width="151px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                    TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                    &nbsp; 
                    <span style="color:white;">Proses Pressing</span>
                    <asp:DropDownList ID="ddlPressing" runat="server" Width="100px">
                    <asp:ListItem Value="0">ALL</asp:ListItem>
                    <asp:ListItem Value="YES">Pressing</asp:ListItem>
                    <asp:ListItem Value="NO">Non Pressing</asp:ListItem></asp:DropDownList>
                    &nbsp;
                    <input class="btn btn-info" id="btnRefresh" runat="server" 
                    onserverclick="btnRefresh_ServerClick" type="button" value="Refresh Data" 
                    visible="True" />
                    <asp:LinkButton class="btn btn-info" ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export 
                    To Excel</asp:LinkButton>
                </div>
            </label>

            <asp:Panel ID="Panel1" runat="server" BorderColor="#CCFFFF" ScrollBars="Vertical"
            Wrap="False" Height="189px">
            <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" PageSize="15"
            Width="100%" OnRowCommand="GrdDynamic_RowCommand">
            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="white" />
            <PagerStyle BorderStyle="Solid" />
            <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
            <asp:GridView ID="GrdDynamic0" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdDynamic_RowCommand"
            PageSize="15" Visible="False" Width="100%">
            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
            <PagerStyle BorderStyle="Solid" />
            <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView></asp:Panel>
            <hr>
            <div id="DivHeaderRow" style="visibility: hidden">
                List Transaksi Defect
                <asp:GridView ID="GridViewDef" runat="server" AutoGenerateColumns="False"
                CellPadding="4" EnableModelValidation="True" Font-Size="X-Small" ForeColor="#333333"
                GridLines="None" OnRowCancelingEdit="GridViewDef_RowCancelingEdit" OnRowCommand="GridViewDef_RowCommand"
                OnRowDataBound="GridViewDef_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="tgl" DataFormatString="{0:d}" HeaderText="Tgl.Periksa">
                    <ItemStyle Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="DefectNo" HeaderText="Defect No">
                <ItemStyle Font-Size="X-Small" /></asp:BoundField>
                <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl.Produksi">
                <ItemStyle Font-Size="X-Small" /></asp:BoundField>
                <asp:BoundField DataField="Jenis" HeaderText="Jenis" />
                <asp:BoundField DataField="GProd" HeaderText="G. Prod.">
                <ItemStyle Font-Size="X-Small" /></asp:BoundField>
                <asp:BoundField DataField="GJmr" HeaderText="G.Jmr" />
                <asp:BoundField DataField="GCut" HeaderText="GCut" />
                <asp:BoundField DataField="Ukuran" HeaderText="Ukuran" />
                <asp:BoundField DataField="NoPalet" HeaderText="Palet" />
                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                <asp:TemplateField HeaderText="Defect Detail">
                <ItemTemplate>
                    <asp:Button ID="btn_Show" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                    CommandName="Details" Font-Size="X-Small" Height="21px" Style="margin-top: 0px"
                    Text="Detail" Width="50px" />
                    &nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="Cancel" runat="server" CommandArgument="<%# Container.DataItemIndex%>"
                    CommandName="Cancel" Font-Size="X-Small" Height="21px" Text="Hide Detail" Visible="false"
                    Width="85px" />
                    &nbsp;&nbsp;
                    <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False"
                    Font-Size="X-Small" PageSize="22" Width="100%">
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                    Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="DefectName" HeaderText="Defect" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" />
                    </Columns>
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                </ItemTemplate>
                <ItemStyle Font-Size="X-Small" /></asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" /></asp:GridView></div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
