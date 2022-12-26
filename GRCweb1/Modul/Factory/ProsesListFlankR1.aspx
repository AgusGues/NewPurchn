<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProsesListFlankR1.aspx.cs" Inherits="GRCweb1.Modul.Factory.ProsesListFlankR1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        label {
            font-size: 12px;
        }
        .auto-style1 {
            font-size: medium;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = '98.5%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                //***DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + '%';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '0';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width)) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }

        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


        function btnHitung_onclick() {

        }



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table width="100%" class="table-responsive">
                    <tr>
                        <td align="left">
                            <strong><span class="auto-style1">PROSES LISTPLANK RENOVASI 1</span></strong>
                        </td>
                        <td align="right">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel8" runat="server" Width="100%">
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                    <table bgcolor="#CCCCCC" width="100%" class="table-responsive">
                                        <tr>
                                            <td colspan="2" style="font-size: x-small">
                                                <b>Dari&nbsp; lokasi / Proses =&gt;</b><asp:RadioButton ID="RBP99" runat="server"
                                                    GroupName="b" Text="P99" AutoPostBack="True" OnCheckedChanged="RBP99_CheckedChanged"
                                                    Enabled="False" Visible="False" />
                                                <asp:RadioButton ID="RBI99" runat="server" AutoPostBack="True" Checked="True" GroupName="b"
                                                    OnCheckedChanged="RBI99_CheckedChanged" Text="Renovasi 1" />
                                                &nbsp;<asp:RadioButton ID="RBRuningSaw" runat="server" GroupName="b" Text="Runing Saw"
                                                    AutoPostBack="True" OnCheckedChanged="RBRuningSaw_CheckedChanged" />
                                                &nbsp;<asp:RadioButton ID="RBBevel" runat="server" GroupName="b" Text="Bevel" AutoPostBack="True"
                                                    OnCheckedChanged="RBBevel_CheckedChanged" />
                                                &nbsp;<asp:RadioButton ID="RBPrint" runat="server" GroupName="b" Text="Print" AutoPostBack="True"
                                                    OnCheckedChanged="RBPrint_CheckedChanged" Visible="False" />
                                                &nbsp;<asp:RadioButton ID="RBStraping" runat="server" GroupName="b" Text="Straping"
                                                    AutoPostBack="True" OnCheckedChanged="RBStraping_CheckedChanged" />
                                            </td>
                                            <td align="right" colspan="3" style="font-size: x-small">Filter By PartNo
                                                <asp:DropDownList ID="ddlPartno" runat="server" AutoPostBack="True" Height="22px"
                                                    OnSelectedIndexChanged="ddlPartno_SelectedIndexChanged" Width="175px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="5" style="font-size: x-small; font-weight: normal;">
                                                <asp:Panel ID="Paneltransit" runat="server" Height="230px" ScrollBars="Both" BackColor="White"
                                                    Width="100%">
                                                    <asp:GridView ID="GridViewtrans0" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                        Width="100%">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="TglProduksi" DataFormatString="{0:d}" HeaderText="Tgl.Produksi" />
                                                            <asp:BoundField DataField="Palet" HeaderText="Palet" />
                                                            <asp:BoundField DataField="Partnodest" HeaderText="PartNo Asal" />
                                                            <asp:BoundField DataField="tglserah" DataFormatString="{0:d}" HeaderText="Tgl.Serah" />
                                                            <asp:BoundField DataField="PartNoser" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="Lokasiser" HeaderText="Lokasi" />
                                                            <asp:BoundField DataField="qtyin" HeaderText="Stock">
                                                                <ItemStyle ForeColor="#FF0066" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="QtyOut">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQtyMutasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                                        Height="21px" OnTextChanged="txtQtyOut_TextChanged" Width="48px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pilih">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkMutasi" runat="server" AutoPostBack="True" OnCheckedChanged="ChkMutasi_CheckedChanged"
                                                                        Text="Mutasi" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GridViewMutasiLok" runat="server" AutoGenerateColumns="False" PageSize="22"
                                                        Width="100%">
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PartNo" HeaderText="PartNo" />
                                                            <asp:BoundField DataField="lokasi1" HeaderText="Lokasi Awal" />
                                                            <asp:BoundField DataField="lokasi2" HeaderText="Lokasi Akhir" />
                                                            <asp:BoundField DataField="qty" HeaderText="Qty" />
                                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                                            <asp:BoundField DataField="CreatedTime" HeaderText="Created Time" />
                                                        </Columns>
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size: x-small; font-weight: normal;" valign="middle"
                                                colspan="3">
                                                <b>Ke Lokasi / Proses selanjutnya =&gt;&nbsp;
                                                    <asp:RadioButton ID="RBI990" runat="server" AutoPostBack="True"
                                                        GroupName="c" OnCheckedChanged="RBI990_CheckedChanged"
                                                        Text="I99" Enabled="False" Visible="False" />
                                                    <asp:RadioButton ID="RBRuningSaw0" runat="server" AutoPostBack="True" Checked="True"
                                                        GroupName="c" OnCheckedChanged="RBRuningSaw0_CheckedChanged" Text="Runing Saw" />
                                                    <asp:RadioButton ID="RBBevel0" runat="server" AutoPostBack="True" GroupName="c" OnCheckedChanged="RBBevel0_CheckedChanged"
                                                        Text="Bevel" />
                                                    <asp:RadioButton ID="RBPrint0" runat="server" AutoPostBack="True"
                                                        GroupName="c" OnCheckedChanged="RBPrint0_CheckedChanged"
                                                        Text="Print" Visible="False" />
                                                    &nbsp;<asp:RadioButton ID="RBRenovasi" runat="server" AutoPostBack="True"
                                                        GroupName="c" OnCheckedChanged="RBRenovasi_CheckedChanged"
                                                        Text="Renovasi 2" />
                                                    <asp:RadioButton ID="RBStraping0" runat="server" AutoPostBack="True" GroupName="c"
                                                        OnCheckedChanged="RBStraping0_CheckedChanged" Text="Straping" />
                                                    <asp:RadioButton ID="RBLogistik" runat="server" AutoPostBack="True" GroupName="c"
                                                        OnCheckedChanged="RBLogistik_CheckedChanged" Text="Tahap 3" />
                                                    &nbsp;</b>&nbsp;&nbsp;
                                            </td>
                                            <td align="right" style="font-size: x-small; font-weight: normal;" valign="middle">
                                                <b>
                                                    <asp:Panel ID="PCut2H" runat="server" Width="386px">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-size: x-small; font-weight: bold;">Produk Tujuan<asp:RadioButton ID="RBKali13" runat="server" AutoPostBack="True" Checked="True"
                                                                    GroupName="e" OnCheckedChanged="RBKali13_CheckedChanged" Text="100 mm" />
                                                                    &nbsp;<asp:RadioButton ID="RBKali14" runat="server" AutoPostBack="True" GroupName="e"
                                                                        OnCheckedChanged="RBKali14_CheckedChanged" Text="200 mm" />
                                                                    <b>
                                                                        <asp:RadioButton ID="RBNonStd" runat="server" AutoPostBack="True" GroupName="e"
                                                                            OnCheckedChanged="RBNonStd_CheckedChanged" Text="250 mm" />
                                                                    </b>&nbsp;<asp:RadioButton ID="RBKali15" runat="server" AutoPostBack="True" GroupName="e"
                                                                        OnCheckedChanged="RBKali15_CheckedChanged" Text="300 mm" Visible="False" />
                                                                    <b>
                                                                        <br />
                                                                        <asp:RadioButton ID="RBKaliNSTD" runat="server" AutoPostBack="True" GroupName="e"
                                                                            OnCheckedChanged="RBKaliNSTD_CheckedChanged" Text="Non Std" />
                                                                        <b>&nbsp;<asp:TextBox ID="txtPanjangNStd" runat="server"
                                                                            AutoPostBack="True" BorderStyle="Groove"
                                                                            Height="21px" onfocus="this.select();"
                                                                            OnTextChanged="RBKaliNSTD_CheckedChanged" Width="48px"></asp:TextBox></b>&nbsp;
                                                                            mm</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </b>
                                            </td>
                                            <td align="right" style="font-size: x-small; font-weight: normal;" valign="middle">
                                                <input id="btnTansfer" runat="server" onserverclick="btnTansfer_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                    type="button" value="Transfer" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="font-size: x-small; font-weight: normal;" valign="middle">
                                                <b>Tanggal Potong </b><b>
                                                    <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        OnTextChanged="txtTanggal_TextChanged" Width="120px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                                </b><b>Jumlah Potong</b><b>
                                                    <asp:TextBox ID="txtQtyOut" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        Height="21px" onfocus="this.select();" OnTextChanged="txtQty1_TextChanged" Width="48px"></asp:TextBox>
                                                </b>
                                            </td>

                                            <asp:Panel ID="Panel1" runat="server">
                                                <td align="left" colspan="4" style="font-size: x-small; font-weight: normal;" valign="middle">Partno Transfer
                                                <asp:TextBox ID="txtPartnoTransfer" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtPartnoOK_TextChanged"
                                                    Width="175px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="txtPartnoTransfer_AutoCompleteExtender" runat="server"
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                        TargetControlID="txtPartnoTransfer">
                                                    </cc1:AutoCompleteExtender>
                                                    &nbsp;Jumlah Transfer <b>
                                                        <asp:TextBox ID="txtQtyTransfer" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                            Height="21px" onfocus="this.select();" Width="48px" ReadOnly="True"></asp:TextBox>
                                                    </b>
                                            </asp:Panel>

                                            <asp:Panel ID="Panel9" runat="server">
                                                Partno Transfer
                                                <asp:TextBox ID="txtPartnoTransfer2" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Height="21px" onfocus="this.select();" OnTextChanged="txtPartnoOK_TextChanged"
                                                    Width="175px"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                    CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                    MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                    TargetControlID="txtPartnoTransfer2">
                                                </cc1:AutoCompleteExtender>
                                                &nbsp;Jumlah Transfer <b>
                                                    <asp:TextBox ID="txtQtyTransfer2" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                        Height="21px" onfocus="this.select();" Width="48px"></asp:TextBox>
                                                </b>
                                            </asp:Panel>
                        </td>
                    </tr>

                    <%--<tr>
                                            <td align="left" style="font-size: x-small; font-weight: normal;" 
                                                valign="middle">
                                                &nbsp;</td>
                                            <td align="left" colspan="4" style="font-size: x-small; font-weight: normal;" 
                                                valign="middle">
                                                &nbsp;</td>
                                        </tr>--%>
                    <tr id="trCnC2" runat="server" visible="false">
                        <td align="left" style="font-size: x-small; font-weight: normal;" valign="middle">&nbsp;
                                                <asp:CheckBox ID="ChkCnC" runat="server" AutoPostBack="True" OnCheckedChanged="ChkCnC_CheckedChanged"
                                                    Text="Keluar alur ListPlank" Visible="False" />
                        </td>
                        <td align="left" colspan="4" style="font-size: x-small; font-weight: normal;" valign="middle">
                            <asp:Panel ID="PanelCnC2" runat="server" Height="26px" Visible="False">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="left" colspan="4" style="font-size: x-small; font-weight: normal;" valign="middle">
                                            <asp:Label ID="LabelPartnoCnC" runat="server" Text="Partno Transfer " Visible="true"></asp:Label>
                                            <asp:TextBox ID="txtPartnoCnC" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Height="21px" onfocus="this.select();" Width="150px"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtPartnoCnC">
                                            </cc1:AutoCompleteExtender>

                                            &nbsp;<asp:Label ID="LabelLokasi" runat="server" Text="Lokasi" Visible="true"></asp:Label>
                                            <asp:TextBox
                                                ID="txtLokasi" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Height="21px" onfocus="this.select();" ReadOnly="false" Width="48px"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetLokasiStock" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtLokasi">
                                            </cc1:AutoCompleteExtender>

                                            &nbsp;<asp:Label ID="LabelPartnoTCnC" runat="server" Text="Qty" Visible="true"></asp:Label>
                                            <asp:TextBox
                                                ID="txtPartnoTCnC" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                Height="21px" onfocus="this.select();" ReadOnly="True" Width="48px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5" style="font-size: x-small; font-weight: normal;" valign="middle">
                            <asp:Panel ID="PanelSerah" runat="server" Visible="False" BackColor="#99CCFF">
                                <table style="width: 100%;">
                                    <tr>
                                        <td align="left" style="font-size: x-small; font-weight: bold;"
                                            bgcolor="#0099CC">&nbsp;
                                        </td>
                                        <td align="left" bgcolor="#0099CC"
                                            style="font-size: x-small; font-weight: bold;">&nbsp;
                                        </td>
                                        <td align="left" bgcolor="#0099CC" colspan="3"
                                            style="font-size: x-small; font-weight: bold;">&nbsp;
                                        </td>
                                        <td align="left" bgcolor="#0099CC" colspan="3" style="font-size: x-small; font-weight: bold;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; font-weight: normal;" colspan="3">
                                            <asp:RadioButton ID="RBOK" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBOK_CheckedChanged"
                                                Text="Mutasi Partno OK " Checked="True" />
                                            &nbsp;
                                                                <asp:TextBox ID="txtPartnoOK" runat="server" AutoPostBack="True"
                                                                    BorderStyle="Groove" Height="21px" onfocus="this.select();"
                                                                    OnTextChanged="txtPartnoOK_TextChanged" Width="175px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi"
                                                ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoOK">
                                            </cc1:AutoCompleteExtender>
                                            &nbsp;
                                                                <asp:Label ID="LabelqtyOK" runat="server" Text="Qty"></asp:Label>
                                            &nbsp;
                                                                <asp:TextBox ID="txtQtyOK" runat="server" AutoPostBack="True" Height="21px"
                                                                    onfocus="this.select();" ReadOnly="True" Width="57px"></asp:TextBox>
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;">&nbsp;
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;" colspan="2">
                                            <asp:Label ID="LabelOK" runat="server" Text="ke Lokasi " Visible="False"></asp:Label>
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;">
                                            <asp:TextBox ID="txtlokOK" runat="server" Height="21px" Width="57px" AutoPostBack="True"
                                                OnTextChanged="txtlokOK_TextChanged" Visible="False">D99</asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetLokasiStock" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtlokOK">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td align="right" style="font-size: x-small;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: x-small; font-weight: normal;" colspan="3">
                                            <asp:RadioButton ID="RBBP" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RadioButton2_CheckedChanged"
                                                Text="Mutasi Partno BP " />
                                            &nbsp;
                                                                <asp:TextBox ID="txtPartnoBP" runat="server" AutoPostBack="True"
                                                                    BorderStyle="Groove" Height="21px" onfocus="this.select();"
                                                                    OnTextChanged="txtPartnoBP_TextChanged" Visible="False"
                                                                    Width="175px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true"
                                                FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetNoProdukJadi"
                                                ServicePath="AutoComplete.asmx" TargetControlID="txtPartnoBP">
                                            </cc1:AutoCompleteExtender>
                                            &nbsp;
                                                                <asp:Label ID="LabelqtyBP" runat="server" Text="Qty" Visible="False"></asp:Label>
                                            &nbsp;
                                                                <asp:TextBox ID="txtQtyBP" runat="server" AutoPostBack="True" Height="21px"
                                                                    onfocus="this.select();" ReadOnly="True" Visible="False" Width="57px"></asp:TextBox>
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;">&nbsp;
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;" colspan="2">
                                            <asp:Label ID="LabelOK0" runat="server" Text="ke Lokasi" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left" style="font-size: x-small; font-weight: normal;">
                                            <asp:TextBox ID="txtlokBP" runat="server" Height="21px" Width="57px"
                                                Visible="False">F38</asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="200"
                                                CompletionSetCount="10" EnableCaching="true" FirstRowSelected="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetLokasiStock" UseContextKey="true" ContextKey="0" ServicePath="AutoComplete.asmx"
                                                TargetControlID="txtLokBP">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td align="right" style="font-size: x-small;">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: x-small; font-weight: bold;">List Proses ListPlank&nbsp;
                            <asp:TextBox ID="txtdrtanggal" runat="server" AutoPostBack="True" Height="21px"
                                Width="151px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtdrtanggal"></cc1:CalendarExtender>
                            s/d Tanggal
                        <asp:TextBox ID="txtsdtanggal" runat="server" AutoPostBack="True" Height="21px"
                            Width="151px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtsdtanggal_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtsdtanggal"></cc1:CalendarExtender>
                            &nbsp;<asp:Button ID="Button1" runat="server" Height="21px" OnClick="Button1_Click"
                                Text="Refresh" />
                        </td>
                        </td>
                        <td style="font-size: x-small; font-weight: bold;">
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Export  To Excel</asp:LinkButton>
                        </td>
                    </tr>
                <tr>
                    <td colspan="2">
                        <div id="DivRoot" align="left">
                            <div style="overflow: hidden;" id="DivHeaderRow">
                            </div>
                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                    HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated"
                                    PageSize="20" Style="margin-right: 0px" Width="98%" OnDataBinding="GridView1_DataBinding">

                                    <RowStyle BackColor="#EFF3FB" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                            <div id="DivFooterRow" style="overflow: hidden">
                            </div>
                        </div>
                    </td>
                </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
