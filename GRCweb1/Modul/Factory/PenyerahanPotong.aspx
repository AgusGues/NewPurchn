<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PenyerahanPotong.aspx.cs" Inherits="GRCweb1.Modul.Factory.PenyerahanPotong" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script type="text/javascript">
        function doClick(textboxName, button, e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox
            if (key == 13) {
                //Get the button the user wants to have clicked
                var txtbox = document.getElementById(textboxName);
                var btn = document.getElementById(button);
                if (btn != null) {
                    btn.click();
                }
                if (txtbox != null) { //If we find the button click it
                    txtbox.focus();
                    event.keyCode = 0
                }
            }

        }

        function OnChange() {
            var btn = document.getElementById("btnTansfer");
            Button1.click();
        }

        function change_color() {
            document.getElementById("<%=btnTansfer.ClientID%>").style.backgroundColor = "blue";
            document.getElementById("<%=btnTansfer.ClientID%>").style.color = "white";

        }
        function change_color1() {
            document.getElementById("<%=btnTansfer.ClientID%>").style.backgroundColor = "white";
            document.getElementById("<%=btnTansfer.ClientID%>").style.color = "black";
            var txtbox = document.getElementById("<%=txtnopalet.ClientID%>");
            txtbox.focus();
        }
        function change_color2() {
            document.getElementById("<%=txtnopalet.ClientID%>").style.backgroundColor = "blue";
            document.getElementById("<%=txtnopalet.ClientID%>").style.color = "white";
        }
        function change_color3() {
            document.getElementById("<%=txtnopalet.ClientID%>").style.backgroundColor = "white";
            document.getElementById("<%=txtnopalet.ClientID%>").style.color = "black";
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 100%">
                        <table class="nbTableHeader" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <strong>SERAH TERIMA DAN PEMOTONGAN PRODUK
                                        <asp:RadioButton ID="RB1000" runat="server" AutoPostBack="True" Checked="True" GroupName="a"
                                            OnCheckedChanged="RB1000_CheckedChanged" Text="1020X2020" />
                                        <asp:RadioButton ID="RB9Mili" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB9Mili_CheckedChanged"
                                            Text="8 dan 9 MM" />
                                        <asp:RadioButton ID="RB4Mili" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RB4Mili_CheckedChanged"
                                            Text="4 MM dan lainnya" />
                                    </strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="center">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="center">
                                    <div id="Div1" runat="server" style="overflow: scroll">
                                        <table id="Table4" border="0" cellpadding="0" cellspacing="1" style="left: 0px; top: 0px;
                                            font-size: x-small; border-collapse: collapse;">
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table border="0" cellpadding="0" cellspacing="1" style="left: 0px; top: 0px; font-size: x-small;
                                                            border-collapse: collapse;" width="100%">
                                                            <tr bgcolor="#CCFFFF">
                                                                <td align="left" bgcolor="#CCFFFF">
                                                                    <asp:Panel ID="PanelProduksi" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="ChkTglProduksi0" runat="server" AutoPostBack="True" OnCheckedChanged="ChkTglProduksi0_CheckedChanged"
                                                                                        Text="Semua" />
                                                                                    &nbsp;
                                                                                    <asp:CheckBox ID="ChkOven" runat="server" AutoPostBack="True" 
                                                                                        oncheckedchanged="ChkOven_CheckedChanged" Text="Edit Proses Oven" />
                                                                                    &nbsp;
                                                                                    <asp:Label ID="Label3" runat="server" Text="Tgl. Produksi"></asp:Label>
                                                                                </td>
                                                                                <td align="left" bgcolor="#CCFFFF">
                                                                                    <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                        OnTextChanged="txtDate_TextChanged" Width="100px"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                                                        TargetControlID="txtDate">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td align="left" bgcolor="#CCFFFF">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="right">
                                                                                    Cari No Palet
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtnopalet" runat="server" AutoPostBack="True" Height="20px" OnTextChanged="txtNoPalet_TextChanged"
                                                                                        Width="72px"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="txtNoPalet_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx2"
                                                                                        CompletionInterval="200" CompletionSetCount="20" EnableCaching="true" FirstRowSelected="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetPaletBM" ServicePath="AutoComplete.asmx"
                                                                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtNoPalet">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <td align="right">
                                                                        Tgl. Potong
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtDateSerah" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                            OnTextChanged="txtDateSerah_TextChanged" Width="100px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                            TargetControlID="txtDateSerah">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="right" valign="middle">
                                                                        Group Cuter
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="ddlCutter" runat="server" Height="22px" Width="78px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:RadioButton ID="RBCut1" runat="server" AutoPostBack="True" Checked="True" GroupName="c"
                                                                            OnCheckedChanged="RBCut1_CheckedChanged" Text="tahap I" Visible="False" />
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:RadioButton ID="RBCut2" runat="server" AutoPostBack="True" GroupName="c" OnCheckedChanged="RBCut2_CheckedChanged"
                                                                            Text="tahap III" Visible="False" />
                                                                    </td>
                                                                    <td align="right">
                                                                        &nbsp;
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 3px;" valign="top">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: outset; padding: inherit;
                                                                font-size: x-small; font-weight: bold; height: 27px;">
                                                                <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" Style="margin-left: 0px">
                                                                    <table style="border-style: inset; width: 100%;">
                                                                        <tr>
                                                                            <td align="center" colspan="2" valign="top" style="border-style: outset; padding: inherit;
                                                                                font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="Panel3" runat="server" BackColor="#CCFFFF" BorderColor="#CCFFFF" Height="140px"
                                                                                    ScrollBars="Vertical" Wrap="False">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="left" style="font-size: x-small; font-weight: bold;" valign="top">
                                                                                                LIST
                                                                                                <asp:RadioButton ID="RBSource1" runat="server" AutoPostBack="True" 
                                                                                                    Checked="True" GroupName="S" OnCheckedChanged="RBSource1_CheckedChanged" 
                                                                                                    Text="JEMUR" />
                                                                                                <asp:RadioButton ID="RBSource2" runat="server" AutoPostBack="True" 
                                                                                                    GroupName="S" OnCheckedChanged="RBSource2_CheckedChanged" 
                                                                                                    Text="ListPlank Proses 1" Visible="False" />
                                                                                            </td>
                                                                                            <td align="right" style="font-size: x-small; font-weight: bold;" valign="top">
                                                                                                <asp:RadioButton ID="RBWetCut" runat="server" AutoPostBack="True" 
                                                                                                    Checked="True" GroupName="cut" oncheckedchanged="RBWetCut_CheckedChanged" 
                                                                                                    Text="Proses Wet Cut" />
                                                                                                &nbsp;<asp:RadioButton ID="RBDryCut" runat="server" AutoPostBack="True" 
                                                                                                    GroupName="cut" oncheckedchanged="RBDryCut_CheckedChanged" 
                                                                                                    Text="Proses Dry Cut" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left" colspan="2" style="font-size: x-small; font-weight: bold;" 
                                                                                                valign="top">
                                                                                                <asp:GridView ID="GridViewAsimetris" runat="server" AutoGenerateColumns="False" 
                                                                                                    CellPadding="4" EnableModelValidation="True" Font-Size="X-Small" 
                                                                                                    ForeColor="#333333" GridLines="None" 
                                                                                                    OnRowCancelingEdit="GridViewAsimetris_RowCancelingEdit" 
                                                                                                    OnRowCommand="GridViewAsimetris_RowCommand" 
                                                                                                    onselectedindexchanged="GridViewAsimetris_SelectedIndexChanged1" Width="100%">
                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="docno" HeaderText="Doc No" />
                                                                                                        <asp:BoundField DataField="Tgltrans" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl. Proses">
                                                                                                            <ItemStyle Font-Size="X-Small" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="Partnoin" HeaderText="Dari PartNo">
                                                                                                            <ItemStyle Font-Size="X-Small" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="Lokasiin" HeaderText="Dari Lok">
                                                                                                            <ItemStyle Font-Size="X-Small" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="qtyin" HeaderText="Qty">
                                                                                                            <ItemStyle Font-Size="X-Small" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:TemplateField HeaderText="Detail Proses Asimetris">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Button ID="btn_Show" runat="server" 
                                                                                                                    CommandArgument="<%# Container.DataItemIndex%>" CommandName="Details" 
                                                                                                                    Font-Size="X-Small" Height="19px" OnClick="btn_Show_Click" 
                                                                                                                    Style="margin-top: 0px" Text="Show Details" Width="88px" />
                                                                                                                <asp:Button ID="Cancel" runat="server" 
                                                                                                                    CommandArgument="<%# Container.DataItemIndex%>" CommandName="Cancel" 
                                                                                                                    Font-Size="X-Small" Height="19px" Text="Hide Details" Visible="false" 
                                                                                                                    Width="85px" />
                                                                                                                <%--child gridview with bound fields --%>
                                                                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                                                                                    CellPadding="4" EnableModelValidation="True" Font-Size="X-Small" 
                                                                                                                    ForeColor="Black">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="GroupName" HeaderText="G.Marketing" />
                                                                                                                        <asp:BoundField DataField="PartnoOut" HeaderText="PartNo" />
                                                                                                                        <asp:BoundField DataField="LokasiOut" HeaderText="Lokasi" />
                                                                                                                        <asp:BoundField DataField="QtyOut" HeaderText="Qty" />
                                                                                                                    </Columns>
                                                                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                                                                    <RowStyle BackColor="#E3EAEB" />
                                                                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
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
                                                                                                <asp:GridView ID="GridViewOven" runat="server" AutoGenerateColumns="False" 
                                                                                                    onrowdatabound="GridViewOven_RowDataBound" Visible="False" Width="100%">
                                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                                                                        ForeColor="Gold" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                        <asp:BoundField ConvertEmptyStringToNull="False" DataField="TglProduksi" 
                                                                                                            DataFormatString="{0:d}" HeaderText="Tgl Produksi" />
                                                                                                        <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                                                        <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
                                                                                                        <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                                                                        <asp:BoundField DataField="TglJemur" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl.Jemur" />
                                                                                                        <asp:BoundField DataField="Qtyin" HeaderText="Qty" />
                                                                                                        <asp:BoundField DataField="Rak" HeaderText="Rak" />
                                                                                                        <asp:TemplateField HeaderText="Oven">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtJemur" runat="server" AutoPostBack="True" 
                                                                                                                    ontextchanged="txtJemur_TextChanged" Width="50px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GridViewokbp" runat="server" AutoGenerateColumns="False" 
                                                                                                    OnRowCommand="GridViewokbp_RowCommand" 
                                                                                                    OnRowDataBound="GridViewokbp_RowDataBound" PageSize="22" 
                                                                                                    ToolTip="Gunakan tombol &quot;Tab&quot; atau &quot;Enter&quot; untuk pindah cell" 
                                                                                                    Width="100%">
                                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="x-small" />
                                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="x-small" 
                                                                                                        ForeColor="Gold" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                        <asp:BoundField DataField="DestID" HeaderText="DestID" />
                                                                                                        <asp:BoundField DataField="TglProduksi" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl. Prod." />
                                                                                                        <asp:BoundField DataField="Formula" HeaderText="Jenis" />
                                                                                                        <asp:BoundField DataField="Lokasi" HeaderText="Lok" />
                                                                                                        <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                                                        <asp:BoundField DataField="PAlet" HeaderText="Palet" />
                                                                                                        <asp:BoundField DataField="TglJemur" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl. Jemur" />
                                                                                                        <asp:BoundField DataField="Rak" HeaderText="Rak" />
                                                                                                        <asp:BoundField DataField="Qtyin" HeaderText="QtyIn" />
                                                                                                        <asp:BoundField DataField="QtyOut" HeaderText="QtyOut" />
                                                                                                        <asp:BoundField DataField="Sisa" HeaderText="Sisa" />
                                                                                                        <asp:ButtonField CommandName="pilih" Text="Pilih" />
                                                                                                        <asp:BoundField DataField="fail">
                                                                                                            <ItemStyle BackColor="Black" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="GridViewTerimaBP" runat="server" AutoGenerateColumns="False" 
                                                                                                    PageSize="22" Width="100%">
                                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="x-small" />
                                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="x-small" 
                                                                                                        ForeColor="Gold" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                        <asp:BoundField DataField="Destid" HeaderText="Destid" />
                                                                                                        <asp:BoundField DataField="TglProduksi" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl. Produksi" />
                                                                                                        <asp:BoundField DataField="partnoser" HeaderText="DPartNo" />
                                                                                                        <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                                                                        <asp:BoundField DataField="tglSerah" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                            HeaderText="Tgl.Serah" />
                                                                                                        <asp:BoundField DataField="LokasiSer" HeaderText="DLokasi" />
                                                                                                        <asp:BoundField DataField="Partnotrm" HeaderText="KPartNo" />
                                                                                                        <asp:BoundField DataField="LokasiTrm" HeaderText="KLokasi" />
                                                                                                        <asp:BoundField DataField="QtyInTrm" HeaderText="Qty" />
                                                                                                        <asp:TemplateField HeaderText="CutQty">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtQtyMutasi0" runat="server" AutoPostBack="True" 
                                                                                                                    Font-Size="x-small" onfocus="this.select();" 
                                                                                                                    OnTextChanged="txtQtyMutasi0_TextChanged" Width="40px">0</asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Pilih">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="ChkMutasi0" runat="server" AutoPostBack="True" 
                                                                                                                    OnCheckedChanged="ChkMutasi0_CheckedChanged" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="cutlevel" HeaderText="CutLvl">
                                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="2" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="Panel5" runat="server" BorderStyle="Outset" Width="1000px">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" style="border-style: inset; padding: inherit; font-size: x-small;
                                                                                                font-weight: bold; background-color: #669999; color: #FFFFFF;" 
                                                                                                colspan="13">
                                                                                                <asp:Panel ID="Panel1" runat="server">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td bgcolor="#0066FF">
                                                                                                                <asp:RadioButton ID="RBSuperPanel" runat="server" AutoPostBack="True" GroupName="d"
                                                                                                                    OnCheckedChanged="RBSuperPanel_CheckedChanged" Text="SuperPanel" Visible="False"
                                                                                                                    Checked="True" ForeColor="White" />
                                                                                                                <asp:RadioButton ID="RBLisflank" runat="server" AutoPostBack="True" GroupName="d"
                                                                                                                    OnCheckedChanged="RBLisflank_CheckedChanged" Text="ListPlank" Visible="False"
                                                                                                                    ForeColor="White" />
                                                                                                            </td>
                                                                                                            <td style="color: #FFFFFF">
                                                                                                            </td>
                                                                                                            <td align="center" style="border-style: none; padding: inherit; font-size: x-small;
                                                                                                                font-weight: bold; background-color: #669999; color: #FFFFFF;" rowspan="2">
                                                                                                                <asp:Panel ID="PanelLevel" runat="server" Visible="False" Font-Size="X-Small" BackColor="#6666FF">
                                                                                                                    <table width="100%" style="font-size: x-small">
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:RadioButton ID="RBLisPlank1" runat="server" AutoPostBack="True" GroupName="g"
                                                                                                                                    Text="Proses 1" ForeColor="White" OnCheckedChanged="RBLisPlank1_CheckedChanged"
                                                                                                                                    Checked="True" Visible="False" />
                                                                                                                                <asp:RadioButton ID="RBLisPlank2" runat="server" AutoPostBack="True" GroupName="g"
                                                                                                                                    Text="Proses 2" ForeColor="White" OnCheckedChanged="RBLisPlank2_CheckedChanged"
                                                                                                                                    Visible="False" />
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Panel ID="PanelOpt" runat="server" Visible="False" Font-Size="X-Small" BackColor="#99CCFF">
                                                                                                                                    <input id="btnTansfer0" runat="server" align="right" onserverclick="txtDate_TextChanged"
                                                                                                                                        style="background-color: white; font-weight: bold; font-size: 11px; height: 21px;
                                                                                                                                        width: 39px;" type="button" value="Refresh" visible="False" />
                                                                                                                                    <asp:RadioButton ID="RBKali2" runat="server" AutoPostBack="True" Checked="True" GroupName="b"
                                                                                                                                        OnCheckedChanged="RBKali2_CheckedChanged" Text="1000X1000" />
                                                                                                                                    <asp:RadioButton ID="RBKali4" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali4_CheckedChanged"
                                                                                                                                        Text="1210 X 2440" Visible="False" />
                                                                                                                                    <asp:RadioButton ID="RBKali19" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali19_CheckedChanged"
                                                                                                                                        Text="1220 X 2440" Visible="False" />
                                                                                                                                    <asp:RadioButton ID="RBKali6" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali6_CheckedChanged"
                                                                                                                                        Text="1215 X 2440" Visible="False" />
                                                                                                                                    <asp:RadioButton ID="RBKali12" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali12_CheckedChanged"
                                                                                                                                        Text="1233 X 2440" Visible="False" />
                                                                                                                                    <asp:RadioButton ID="RBKali16" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali16_CheckedChanged"
                                                                                                                                        Text="1230 X 2440" />
                                                                                                                                    <asp:RadioButton ID="RBKali18" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RBKali18_CheckedChanged"
                                                                                                                                        Text="1240 X 2460" />
                                                                                                                                    <asp:RadioButton ID="RBKali21" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                                                        OnCheckedChanged="RBKali21_CheckedChanged" Text="1230 X 3600" />
                                                                                                                                    <asp:RadioButton ID="RBKali17" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                                                        OnCheckedChanged="RBKali17_CheckedChanged" Text="1244 X 3600" />
                                                                                                                                    <asp:RadioButton ID="RBKali20" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                                                        OnCheckedChanged="RBKali20_CheckedChanged" Text="1230 X 3000" />
                                                                                                                                </asp:Panel>
                                                                                                                                <asp:Panel ID="PCut2H" runat="server" Visible="False">
                                                                                                                                    <table style="width: 100%;">
                                                                                                                                        <tr>
                                                                                                                                            <td align="center" style="font-size: x-small; font-weight: bold;" bgcolor="#99CCFF">
                                                                                                                                                <asp:RadioButton ID="RBKali13" runat="server" AutoPostBack="True" Checked="True"
                                                                                                                                                    GroupName="e" OnCheckedChanged="RBKali13_CheckedChanged" Text="100 X 2440" />
                                                                                                                                                &nbsp;<asp:RadioButton ID="RBKali14" runat="server" AutoPostBack="True" GroupName="e"
                                                                                                                                                    OnCheckedChanged="RBKali14_CheckedChanged" Text="200 X 2440" />
                                                                                                                                                &nbsp;<asp:RadioButton ID="RBKali15" runat="server" AutoPostBack="True" GroupName="e"
                                                                                                                                                    OnCheckedChanged="RBKali15_CheckedChanged" Text="300 X 2440" />
                                                                                                                                            </td>
                                                                                                                                            <td align="center" style="font-size: x-small; font-weight: bold; height: 27px;">
                                                                                                                                                <asp:RadioButton ID="RBSimpleFlank" runat="server" AutoPostBack="True" GroupName="f"
                                                                                                                                                    OnCheckedChanged="RBSimpleFlank_CheckedChanged" Text="Standar" Checked="True" />
                                                                                                                                                <asp:RadioButton ID="RBSuperFlank" runat="server" AutoPostBack="True" GroupName="f"
                                                                                                                                                    OnCheckedChanged="RBSuperFlank_CheckedChanged" Text="Bevel" />
                                                                                                                                            </td>
                                                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; font-weight: bold;">
                                                                                                                                                <input id="btnTansfer1" runat="server" align="right" onserverclick="btnTansfer1_ServerClick"
                                                                                                                                                    style="background-color: #99CCFF; font-weight: bold; font-size: 11px;" type="button"
                                                                                                                                                    value="Transfer" />
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </asp:Panel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" style="border-style: none; font-size: x-small;">
                                                                                                ID
                                                                                            </td>
                                                                                            <td align="center" style="border-style: none; font-size: x-small">
                                                                                                DestID
                                                                                            </td>
                                                                                            <td align="center" style="border-style: none; font-size: x-small">
                                                                                                Tgl. Prod.
                                                                                            </td>
                                                                                            <td align="center" style="border-style: none; font-size: x-small">
                                                                                                Jenis
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Palet
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Tgl. Jemur
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Rak
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Qty In
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Qty Out
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                Serah
                                                                                            </td>
                                                                                            <td align="center" style="font-size: x-small">
                                                                                                <asp:Label ID="Loven" runat="server" Visible="False">Oven</asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small;" bgcolor="White">
                                                                                                <asp:Label ID="LID" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small;" bgcolor="White">
                                                                                                <asp:Label ID="LDestID" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LTglProd" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LJenis" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LLokasi" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LPartno" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LPalet" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LTglJemur" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LRak" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LQtyIn" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:Label ID="LQtyOut" runat="server">0</asp:Label>
                                                                                            </td>
                                                                                            <td align="center" style="border-style: solid; font-size: x-small" bgcolor="White">
                                                                                                <asp:TextBox ID="txtSerah" runat="server" BorderStyle="Groove" Height="21px" onfocus="this.select();"
                                                                                                    Width="40px" AutoPostBack="True" OnTextChanged="txtSerah_TextChanged"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="White" 
                                                                                                style="border-style: solid; font-size: x-small">
                                                                                                <asp:DropDownList ID="ddlOven" runat="server" Height="22px" Visible="False" 
                                                                                                    Width="48px">
                                                                                                    <asp:ListItem>1</asp:ListItem>
                                                                                                    <asp:ListItem>2</asp:ListItem>
                                                                                                    <asp:ListItem>3</asp:ListItem>
                                                                                                    <asp:ListItem>4</asp:ListItem>
                                                                                                    <asp:ListItem>0</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="font-size: x-small; font-weight: bold;" colspan="2">
                                                                                <asp:Panel ID="PanelInput" runat="server" ForeColor="White" Visible="False" HorizontalAlign="Center">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Panel ID="PanelInput1" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                                    ForeColor="White" Visible="False">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                Partno Asal
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                Lokasi
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                Qty
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtPartnoAsal" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                                    onfocus="this.select();" ReadOnly="True" Width="175px"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoAsal_AutoCompleteExtender" runat="server"
                                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtPartnoAsal">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtlokAsal" onfocus="this.select();" runat="server" Font-Size="x-small"
                                                                                                                    Height="21px" ReadOnly="True" Width="50px" OnTextChanged="txtlokAsal_TextChanged"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtQtyAsal" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyAsal_TextChanged"
                                                                                                                    Width="40px" BackColor="#FFCCFF">0</asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Panel ID="PanelInput3" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                                    ForeColor="White" Visible="True">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small" valign="top">
                                                                                                                Partno OK
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small" valign="top">
                                                                                                                Lokasi
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small" valign="top">
                                                                                                                Qty
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtPartnoPOK" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtPartnoPOK0_TextChanged"
                                                                                                                    Width="175px"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoPOK_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoPOK">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtlokPOK" onfocus="this.select();" runat="server" AutoPostBack="True"
                                                                                                                    Font-Size="x-small" Height="21px" OnTextChanged="txtlokPOK0_TextChanged" Width="50px"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender18" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtlokPOK" UseContextKey="true">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                &nbsp;<asp:TextBox ID="txtQtyPOK" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyPOK_TextChanged"
                                                                                                                    Width="40px" BackColor="#99FFCC" ReadOnly="True">0</asp:TextBox></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                &nbsp;</td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                &nbsp;</td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                &nbsp;</td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Panel ID="PanelInput2" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                                    ForeColor="White" Visible="True">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Partno BP
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Lokasi
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Qty
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                <asp:TextBox ID="txtPartnoPBP" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                                    onfocus="this.select();" Width="175px" AutoPostBack="True" OnTextChanged="txtPartnoPBP_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoPBP">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                <asp:TextBox ID="txtlokPBP" onfocus="this.select();" runat="server" Font-Size="x-small"
                                                                                                                    Height="21px" Width="50px" AutoPostBack="True" 
                                                                                                                    OnTextChanged="txtlokPBP_TextChanged" Visible="False"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtlokPBP" UseContextKey="true">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                                <asp:TextBox ID="txtQtyPBP" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyPBP_TextChanged"
                                                                                                                    Width="40px">0</asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" bgcolor="#CCCCCC" colspan="3" 
                                                                                                                style="font-size: x-small; height: 16px;">
                                                                                                                <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" GroupName="x" 
                                                                                                                    Text="Akan diteruskan ke proses listplank ronovasi I" />
                                                                                                                &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" GroupName="x" 
                                                                                                                    Text="Akan diteruskan ke Lokasi B99" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="PanelPartnoOK" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                    ForeColor="White" Width="50%">
                                                                                    <asp:Label ID="LPartno1" runat="server" Text="PartNo OK"></asp:Label>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                Qty
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" rowspan="2" style="font-size: x-small">
                                                                                                <asp:Panel ID="PanelTahapIII0" runat="server" BackColor="Gray" ForeColor="White"
                                                                                                    Visible="True">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                Partno
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Lokasi
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Qty
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                <asp:TextBox ID="txtPartnoOK1" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                                    onfocus="this.select();" OnTextChanged="txtPartnoOK1_TextChanged" Width="175px"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoOK1_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoOK1">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtlokOK1" runat="server" Font-Size="x-small" Height="21px" Width="50px"
                                                                                                                    BackColor="#FF9966"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                                <asp:TextBox ID="txtQtyOK1" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyOK1_TextChanged"
                                                                                                                    Width="40px" BackColor="#CCFFFF">0</asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoOK" runat="server" BorderStyle="Groove" Height="21px" onfocus="this.select();"
                                                                                                    Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoOK">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokOK" runat="server" Height="21px" Width="30px" 
                                                                                                    AutoPostBack="True" ontextchanged="txtlokOK_TextChanged">I99</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyOK" runat="server" AutoPostBack="True" CssClass="tengah" Height="21px"
                                                                                                    onfocus="this.select();" OnTextChanged="txtQtyOK_TextChanged" Width="40px" ReadOnly="True"
                                                                                                    BackColor="#CCFF99">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td align="left" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="PanelPartnoKW" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                    ForeColor="White" Width="100%">
                                                                                    <asp:Label ID="LPartno2" runat="server" Text="PartNo KW"></asp:Label>
                                                                                    <asp:RadioButtonList ID="RBList" runat="server" AutoPostBack="True" 
                                                                                                    Font-Size="XX-Small" RepeatDirection="Horizontal" 
                                                                                                    RepeatLayout="Flow" 
                                                                                        onselectedindexchanged="RBList_SelectedIndexChanged">
                                                                                                </asp:RadioButtonList>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                                
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                Qty
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" rowspan="2" style="font-size: x-small">
                                                                                                <asp:Panel ID="PanelTahapIII1" runat="server" BackColor="Gray" ForeColor="White"
                                                                                                    Visible="True">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                Partno
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                Lokasi
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                Qty
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                <asp:TextBox ID="txtPartnoKW1" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                                    onfocus="this.select();" Width="175px" 
                                                                                                                    ontextchanged="txtPartnoKW1_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoKW1_AutoCompleteExtender" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoKW1">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                <asp:TextBox ID="txtlokKW1" runat="server" Font-Size="x-small" Height="21px" Width="50px"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                                <asp:TextBox ID="txtQtyKW1" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyKW1_TextChanged"
                                                                                                                    Width="40px">0</asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoKW" runat="server" BorderStyle="Groove" Height="21px" onfocus="this.select();"
                                                                                                    Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoKW">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokKW" runat="server" Font-Size="x-small" Height="21px" Width="30px">I99</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyKW" runat="server" AutoPostBack="True" CssClass="tengah" Font-Size="x-small"
                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyKW_TextChanged" Width="40px"
                                                                                                    BackColor="Yellow">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="font-size: x-small; font-weight: bold;" valign="top">
                                                                                <asp:Panel ID="PanelPartnoBPFinish" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                    ForeColor="White" Width="50%">
                                                                                    Partno BP Finish<table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Qty
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" rowspan="2" style="font-size: x-small">
                                                                                                <asp:Panel ID="Panel7" runat="server">
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoBPF" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukBP" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoBPF">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokBPF" runat="server" Font-Size="x-small" Height="21px" Width="30px">B99</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyBPF" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyBPF_TextChanged"
                                                                                                    Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoBPF0" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px" 
                                                                                                    ontextchanged="txtPartnoBPF0_TextChanged"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoBPF0_AutoCompleteExtender" runat="server"
                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukBP" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtPartnoBPF0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokBPF0" runat="server" Font-Size="x-small" Height="21px" Width="50px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender17" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtlokBPF0" UseContextKey="true">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyBPF0" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyBPF0_TextChanged"
                                                                                                    ReadOnly="True" Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="PanelSample" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                    ForeColor="White" Width="100%">
                                                                                    Sample<table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Qty
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoBPS" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoBPS">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokBPS" runat="server" Font-Size="x-small" Width="30px">Q99</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center">
                                                                                                <asp:TextBox ID="txtQtyBPS" runat="server" Font-Size="x-small" onfocus="this.select();"
                                                                                                    Width="40px" OnTextChanged="txtQtyBPS_TextChanged" AutoPostBack="True">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:Panel ID="Panel18" runat="server">
                                                                                        <asp:GridView ID="GridBSAuto" runat="server" AutoGenerateColumns="False" 
                                                                                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                                                                                            onselectedindexchanged="GridView3_SelectedIndexChanged" Width="100%">
                                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Partno" HeaderText="Partno" >
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Qty" HeaderText="Qty" >
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" >
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td align="left" style="font-size: x-small; font-weight: bold;" valign="top">
                                                                                <asp:Panel ID="PanelPartnoBPUnFinish" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                    ForeColor="White" Width="50%">
                                                                                    Partno BP UnFinish<table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                Qty
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Partno
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                Qty
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoBPU" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoBPU">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokBPU" runat="server" Font-Size="x-small" Height="21px" 
                                                                                                    Width="30px" AutoPostBack="True" ontextchanged="txtlokBPU_TextChanged">C99</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyBPU" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyBPU_TextChanged"
                                                                                                    Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtPartnoBPU0" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoBPU0_AutoCompleteExtender" runat="server"
                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukBM" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtPartnoBPU0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                <asp:TextBox ID="txtlokBPU0" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                    Height="21px" Width="50px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; text-align: center;">
                                                                                                <asp:TextBox ID="txtQtyBPU0" runat="server" AutoPostBack="True" CssClass="tengah"
                                                                                                    Font-Size="x-small" Height="21px" onfocus="this.select();" OnTextChanged="txtQtyBPU0_TextChanged"
                                                                                                    ReadOnly="True" Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="PanelPartnoBPUnFinish0" runat="server" BackColor="Gray" 
                                                                                    BorderStyle="Outset" ForeColor="White" Visible="False" Width="75%">
                                                                                    Informasi Proses BP Super Panel<table style="width: 100%; color: #FFFFFF;">
                                                                                        <tr>
                                                                                            <td >
                                                                                                Lokasi C9 untuk proses BP Unfinish
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td >
                                                                                                Lokasi I99 untuk diteruskan ke proses Listplank
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="Panel9" runat="server" BackColor="Gray" BorderStyle="Outset" ForeColor="White"
                                                                                    Width="352px">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="Gray" style="font-size: x-small; color: #FFFFFF;">
                                                                                                Pelarian
                                                                                            </td>
                                                                                            <td align="right" bgcolor="Gray" style="font-size: x-small">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; font-weight: bold;">
                                                                                                <input id="btnTambah" runat="server" onserverclick="btnTambah_ServerClick" style="background-color: white;
                                                                                                    font-weight: bold; font-size: small; height: 22px;" type="button" value="+" />
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; font-weight: bold;">
                                                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                                                                                    PageSize="1">
                                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="x-small" />
                                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                                        Font-Bold="True" Font-Names="tahoma" Font-Size="x-small" ForeColor="Gold" />
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="PartNo">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtPartNo" runat="server" AutoPostBack="true" Font-Size="x-small"
                                                                                                                    Height="19px" onfocus="this.select();" OnTextChanged="txtPartNo_TextChanged"
                                                                                                                    Width="175px"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="500"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetNoProdukBM1" ServicePath="AutoComplete.asmx" TargetControlID="txtPartNo">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle Width="100px" />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Lok">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtLokasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                                                    Height="20px" ReadOnly="True" Width="36px">P99</asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                                    ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx" TargetControlID="txtLokasi">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Qty JU">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="txtQtyJU" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                                                    Font-Size="X-Small" Height="21px" onfocus="this.select();" onkeyup="OnChange"
                                                                                                                    OnTextChanged="txtQtyJU_TextChanged" Width="48px"></asp:TextBox>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                            <td bgcolor="#CCCCCC" style="font-size: x-small; font-weight: bold;">
                                                                                                <input id="btnTansfer" runat="server" onblur="change_color1()" onfocus="change_color()"
                                                                                                    onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold;
                                                                                                    font-size: 11px; height: 36px; width: 59px;" type="button" value="Transfer" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:Button ID="Button2" runat="server" Height="16px" OnClick="Button2_Click" Width="0px" />
                                                                                <asp:Button ID="Button1" runat="server" Height="21px" OnClick="Button1_Click" Style="margin-top: 0px"
                                                                                    Width="0px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="border-style: inset; height: 3px; font-weight: bold;" valign="top">
                                                                                List Serah
                                                                                <input id="btnTansfer3" runat="server" onblur="change_color1()" onserverclick="btnTerima_ServerClick"
                                                                                    style="background-color: white; font-weight: bold; font-size: 11px; height: 19px;
                                                                                    width: 59px;" type="button" value="Refresh" />
                                                                            </td>
                                                                            <td style="border-style: inset; height: 3px; font-weight: bold;" valign="top">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="height: 3px" valign="top">
                                                                                <div id="div3" style="width: auto; height: 256px; overflow: scroll; clip: rect(auto, auto, auto, auto);
                                                                                    background-color: #CCFFFF;">
                                                                                    <asp:GridView ID="GridViewSerah" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSerah_PageIndexChanging"
                                                                                        PageSize="15" Width="100%" onrowcommand="GridViewSerah_RowCommand">
                                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="x-small" />
                                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="x-small" ForeColor="Gold" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                            <asp:BoundField DataField="TglProduksi" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tgl. Prod." />
                                                                                            <asp:BoundField DataField="TglSerah" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tgl. Serah" />
                                                                                            <asp:BoundField DataField="PartNodest" HeaderText="PartNo 1" />
                                                                                            <asp:BoundField DataField="LokasiDest" HeaderText="Lokasi" />
                                                                                            <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                                                            <asp:BoundField DataField="Partnoser" HeaderText="Partno 2" />
                                                                                            <asp:BoundField DataField="LokasiSer" HeaderText="Lokasi" />
                                                                                            <asp:BoundField DataField="Qtyin" HeaderText="Qty In" />
                                                                                            <asp:BoundField DataField="QtyOut" HeaderText="Qty Out" />
                                                                                            <asp:ButtonField CommandName="batal" Text="Cancel" />
                                                                                        </Columns>
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="PCut2D" runat="server" BackColor="#CCCCCC" Style="margin-left: 0px"
                                                                    Visible="False">
                                                                    <table style="border-style: inset; width: 100%;">
                                                                        <tr>
                                                                            <td align="center" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="Pcaricut2" runat="server">
                                                                                    <table style="width: 100%;">
                                                                                        <tr style="font-size: x-small">
                                                                                            <%--<td>
                                                                                                <asp:TextBox ID="txtDateTerima" runat="server" BorderStyle="Groove" Width="133" AutoPostBack="true"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="extc1" Format="dd-MMM-yyyy" runat="server" TargetControlID="txtDateTerima">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>--%>
                                                                                            <td align="left">
                                                                                                <asp:RadioButton ID="RBPSOK" runat="server" AutoPostBack="True" Checked="True" GroupName="f"
                                                                                                    Text="Pemotongan dari Produk OK" OnCheckedChanged="RBPSOK_CheckedChanged" />
                                                                                                <asp:RadioButton ID="RBPSBP" runat="server" AutoPostBack="True" GroupName="f" Text="Pemotongan dari Produk BP"
                                                                                                    OnCheckedChanged="RBPSBP_CheckedChanged" />
                                                                                            </td>
                                                                                            <%--<td>
                                                                                                <asp:TextBox ID="txtDatePotong" runat="server" BorderStyle="Groove" Width="133" AutoPostBack="True"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtDatePotong" Format="dd-MMM-yyyy"
                                                                                                    runat="server">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>--%>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="Panel17" runat="server" Height="138px" ScrollBars="Auto">
                                                                                    <asp:GridView ID="GridViewTerima" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                                                        Width="100%">
                                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="x-small" />
                                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="x-small" ForeColor="Gold" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                            <asp:BoundField DataField="Destid" HeaderText="Destid" />
                                                                                            <asp:BoundField DataField="TglProduksi" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tgl. Produksi" />
                                                                                            <asp:BoundField DataField="partnoser" HeaderText="DPartNo" />
                                                                                            <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                                                            <asp:BoundField DataField="tglSerah" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tgl.Serah" />
                                                                                            <asp:BoundField DataField="LokasiSer" HeaderText="DLokasi" />
                                                                                            <asp:BoundField DataField="Partnotrm" HeaderText="KPartNo" />
                                                                                            <asp:BoundField DataField="LokasiTrm" HeaderText="KLokasi" />
                                                                                            <asp:BoundField DataField="QtyInTrm" HeaderText="Qty" />
                                                                                            <asp:TemplateField HeaderText="CutQty">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtQtyMutasi" runat="server" Font-Size="x-small" onfocus="this.select();"
                                                                                                        Width="40px" AutoPostBack="True" OnTextChanged="txtQtyMutasi_TextChanged">0</asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Pilih">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="ChkMutasi" runat="server" AutoPostBack="True" OnCheckedChanged="ChkMutasi_CheckedChanged" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="PanelInput4" runat="server" BorderStyle="Outset" ForeColor="White"
                                                                                    HorizontalAlign="Center">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="PCut3D" runat="server" BackColor="#CCCCCC" Style="margin-left: 0px"
                                                                    Visible="False">
                                                                    <asp:Panel ID="Panel12" runat="server">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td align="left" bgcolor="#CCCCCC" style="border-style: inset; padding: inherit;
                                                                                    font-size: x-small; font-weight: bold; height: 27px;" valign="middle">
                                                                                    Hasil potong :
                                                                                    <asp:TextBox ID="txtPengali" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                        Height="21px" onfocus="this.select();" OnTextChanged="txtPengali_TextChanged"
                                                                                        Width="19px" ReadOnly="True" BackColor="#CCCCCC">1</asp:TextBox>
                                                                                    &nbsp;kali PartNo Asal&nbsp;
                                                                                </td>
                                                                                <td align="left" bgcolor="#CCCCCC" style="border-style: inset; padding: inherit;
                                                                                    font-size: x-small; font-weight: bold; height: 27px;" valign="middle">
                                                                                    <asp:RadioButton ID="RBSimetris" runat="server" AutoPostBack="True" Checked="True"
                                                                                        GroupName="g" OnCheckedChanged="RBSimetris_CheckedChanged" Text="Mode Simetris" />
                                                                                    &nbsp;<asp:RadioButton ID="RBAsimetris" runat="server" AutoPostBack="True" GroupName="g"
                                                                                        OnCheckedChanged="RBAsimetris_CheckedChanged" Text="Mode Asimetris" />
                                                                                </td>
                                                                                <td align="right" bgcolor="#CCCCCC" style="border-style: inset; padding: inherit;
                                                                                    font-size: x-small; font-weight: bold; height: 27px;" valign="middle">
                                                                                    <asp:CheckBox ID="ChkHide2" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                                                        OnCheckedChanged="ChkHide2_CheckedChanged" Text="Tampilkan List Stock Produk" />
                                                                                </td>
                                                                                <td align="right" bgcolor="#CCCCCC" style="border-style: inset; padding: inherit;
                                                                                    font-size: x-small; font-weight: bold; height: 27px;">
                                                                                    <input id="btnTansfer2" runat="server" onserverclick="btnTansfer2_ServerClick" style="background-color: white;
                                                                                        font-weight: bold; font-size: 11px;" type="button" value="Transfer" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <table style="border-style: inset; width: 100%;">
                                                                        <tr>
                                                                            <td align="center" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="Panel14" runat="server" Height="196px" ScrollBars="Auto">
                                                                                    <asp:Panel ID="Panel6" runat="server" BackColor="#CCFFCC" Height="100px" ScrollBars="Vertical"
                                                                                        Wrap="False" Visible="False">
                                                                                        <asp:GridView ID="GridViewtrans" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewtrans_RowCommand"
                                                                                            PageSize="22" Width="100%">
                                                                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="GroupID" HeaderText="GroupID" />
                                                                                                <asp:BoundField DataField="ItemID" HeaderText="ItemID" />
                                                                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                <asp:BoundField DataField="lokid" HeaderText="lokid" />
                                                                                                <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                                                                <asp:BoundField DataField="PartName" HeaderText="Part Name" />
                                                                                                <asp:BoundField DataField="Tebal" HeaderText="Tebal" />
                                                                                                <asp:BoundField DataField="Lebar" HeaderText="Lebar" />
                                                                                                <asp:BoundField DataField="Panjang" HeaderText="Panjang" />
                                                                                                <asp:BoundField DataField="volume" HeaderText="Volume" />
                                                                                                <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" />
                                                                                                <asp:BoundField DataField="qty" HeaderText="Stock" />
                                                                                                <asp:ButtonField CommandName="Pilih" Text="Pilih" Visible="False" />
                                                                                            </Columns>
                                                                                            <PagerStyle BorderStyle="Solid" />
                                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="font-size: x-small; font-weight: bold;">
                                                                                <asp:Panel ID="Panel15" runat="server" BackColor="Gray" BorderStyle="Inset" ForeColor="White">
                                                                                    <table style="width: 100%;">
                                                                                        <%--<tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" colspan="3" style="font-size: x-small">
                                                                                                Dari Penerimaan
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" colspan="3" style="font-size: x-small">
                                                                                                Hasil Potong OK
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" colspan="3" style="font-size: x-small">
                                                                                                Hasil Potong BP
                                                                                            </td>
                                                                                        </tr>--%>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Partno Asal
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Qty
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Partno OK
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Volume
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Lokasi
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small; height: 16px;">
                                                                                                Qty
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoAsal">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                                <asp:TextBox ID="txtPartnoAsal0" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px" AutoPostBack="True" OnTextChanged="txtPartnoAsal0_TextChanged"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoAsal0_AutoCompleteExtender0" runat="server"
                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtPartnoAsal0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="border-style: none; font-size: x-small;">
                                                                                                <asp:TextBox ID="txtlokAsal0" runat="server" Font-Size="x-small" Height="21px" Width="50px"
                                                                                                    AutoPostBack="True" OnTextChanged="txtlokAsal0_TextChanged"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender14" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetLokasiStockP" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtlokAsal0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                <asp:TextBox ID="txtQtyAsal0" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyAsal0_TextChanged"
                                                                                                    Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                <asp:TextBox ID="txtPartnoPOK0" runat="server" BorderStyle="Groove" Height="21px"
                                                                                                    onfocus="this.select();" Width="175px" AutoPostBack="True" OnTextChanged="txtPartnoPOK0_TextChanged"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoPOK0_AutoCompleteExtender" runat="server"
                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukOKbyLuas" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtPartnoPOK0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;" width="100">
                                                                                                <asp:Label ID="LVolumePotong" runat="server" Text="0"></asp:Label>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                <asp:TextBox ID="txtlokPOK0" runat="server" Font-Size="x-small" Height="21px" Width="50px"
                                                                                                    AutoPostBack="True" OnTextChanged="txtlokPOK0_TextChanged"></asp:TextBox>
                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" CompletionInterval="200"
                                                                                                    CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                                                                    ServiceMethod="GetLokasiTransT3" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                                                                    TargetControlID="txtlokPOK0">
                                                                                                </cc1:AutoCompleteExtender>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                <asp:TextBox ID="txtQtyPOK0" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtQtyPOK0_TextChanged"
                                                                                                    Width="40px">0</asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                Volume Asal :
                                                                                                <asp:Label ID="LVolumeAsal" runat="server" Text="0"></asp:Label>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" colspan="2" style="font-size: x-small;">
                                                                                                Total Waste :
                                                                                                <asp:Label ID="LTotVolume" runat="server" Text="0"></asp:Label>
                                                                                            </td>
                                                                                            <td align="center" bgcolor="#CCCCCC" colspan="4" style="font-size: x-small;">
                                                                                                <asp:Panel ID="PanelAsimetrisOK" runat="server" BackColor="Gray" BorderStyle="Outset"
                                                                                                    ForeColor="White" Visible="False">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                <asp:TextBox ID="txtPartnoPOK2" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                                                    Height="21px" onfocus="this.select();" Width="175px" OnTextChanged="txtPartnoPOK2_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoPOK_AutoCompleteExtender0" runat="server"
                                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtPartnoPOK2">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small" width="100">
                                                                                                                &nbsp;
                                                                                                                <asp:Label ID="LVolumePotong2" runat="server" Text="0"></asp:Label>
                                                                                                                X<asp:Label ID="LPengali2" runat="server" Text="0"></asp:Label></td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                <asp:TextBox ID="txtlokPOK2" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" Width="50px" OnTextChanged="txtlokPOK2_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender19" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtlokPOK2" UseContextKey="true">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small">
                                                                                                                &nbsp;<asp:TextBox ID="txtQtyPOK2" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" Width="40px" OnTextChanged="txtQtyPOK2_TextChanged">0</asp:TextBox></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtPartnoPOK3" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                                                                    Height="21px" onfocus="this.select();" Width="175px" OnTextChanged="txtPartnoPOK3_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtPartnoPOK1_AutoCompleteExtender0" runat="server"
                                                                                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtPartnoPOK3">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                &nbsp;
                                                                                                                <asp:Label ID="LVolumePotong3" runat="server" Text="0"></asp:Label>
                                                                                                                X<asp:Label ID="LPengali3" runat="server" Text="0"></asp:Label></td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtlokPOK3" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" Width="50px" OnTextChanged="txtlokPOK3_TextChanged"></asp:TextBox>
                                                                                                                <cc1:AutoCompleteExtender ID="txtlokPOK1_AutoCompleteExtender0" runat="server" CompletionInterval="200"
                                                                                                                    CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                                                                                                    MinimumPrefixLength="1" ServiceMethod="GetLokasiTransT3" ServicePath="AutoComplete.asmx"
                                                                                                                    TargetControlID="txtlokPOK3" UseContextKey="true">
                                                                                                                </cc1:AutoCompleteExtender>
                                                                                                            </td>
                                                                                                            <td align="center" bgcolor="#CCCCCC" style="font-size: x-small;">
                                                                                                                <asp:TextBox ID="txtQtyPOK3" runat="server" AutoPostBack="True" Font-Size="x-small"
                                                                                                                    Height="21px" onfocus="this.select();" Width="40px" OnTextChanged="txtQtyPOK3_TextChanged">0</asp:TextBox>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="PanelOtomatis" runat="server" BackColor="Gray" ForeColor="White">
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;">
                                                                                                                <asp:CheckBox ID="ChkConvertBS" runat="server" AutoPostBack="True" Checked="True"
                                                                                                                    OnCheckedChanged="ChkConvertBS_CheckedChanged" Text="Auto Produk BS" />
                                                                                                            </td>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;">
                                                                                                                <asp:Panel ID="PanelPotong" runat="server">
                                                                                                                    <asp:RadioButton ID="RBPotong1" runat="server" AutoPostBack="True" Checked="True"
                                                                                                                        GroupName="b" OnCheckedChanged="RBPotong1_CheckedChanged" Text="Cara Potong 1" />
                                                                                                                    <asp:RadioButton ID="RBPotong2" runat="server" AutoPostBack="True" GroupName="b"
                                                                                                                        OnCheckedChanged="RBPotong2_CheckedChanged" Text="Cara Potong 2" />
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;" colspan="2">
                                                                                                                &nbsp;: (<asp:Label ID="LCPartnoBS1" runat="server" Text="-"></asp:Label>)
                                                                                                                <asp:Label ID="LCQtyBS1" runat="server" Text="0"></asp:Label>
                                                                                                                &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                                <asp:Label ID="LCLokBS1" runat="server" Text="0"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;" colspan="2">
                                                                                                                &nbsp;: (<asp:Label ID="LCPartnoBS2" runat="server" Text="-"></asp:Label>)
                                                                                                                <asp:Label ID="LCQtyBS2" runat="server" Text="0"></asp:Label>
                                                                                                                &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                                <asp:Label ID="LCLokBS2" runat="server" Text="0"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;" colspan="2">
                                                                                                                &nbsp;: (<asp:Label ID="LCPartnoBS3" runat="server" Text="-"></asp:Label>)
                                                                                                                <asp:Label ID="LCQtyBS3" runat="server" Text="0"></asp:Label>
                                                                                                                &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                                <asp:Label ID="LCLokBS3" runat="server" Text="0"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="color: #FFFFFF; font-size: x-small;" colspan="2">
                                                                                                                &nbsp;: (<asp:Label ID="LCPartnoBS4" runat="server" Text="-"></asp:Label>)
                                                                                                                <asp:Label ID="LCQtyBS4" runat="server" Text="0"></asp:Label>
                                                                                                                &nbsp;Lembar, ke lokasi :&nbsp;
                                                                                                                <asp:Label ID="LCLokBS4" runat="server" Text="0"></asp:Label>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <div id="div5" style="width: auto; height: 156px; overflow: scroll; clip: rect(auto, auto, auto, auto);
                                                                                    background-color: #CCFFFF;">
                                                                                    <asp:GridView ID="GridViewSimetris" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                                                        Width="100%">
                                                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="PartNoser" HeaderText="PartNo Awal" />
                                                                                            <asp:BoundField DataField="lokasiser" HeaderText="Lokasi" />
                                                                                            <asp:BoundField DataField="qtyinsm" HeaderText="Qty" />
                                                                                            <asp:BoundField DataField="Groupname" HeaderText="Group" />
                                                                                            <asp:BoundField DataField="partnosm" HeaderText="PartNo Akhir" />
                                                                                            <asp:BoundField DataField="lokasism" HeaderText="Lokasi" />
                                                                                            <asp:BoundField DataField="qtyoutsm" HeaderText="Qty" />
                                                                                        </Columns>
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
