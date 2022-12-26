<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LinkProduktifitasOutput.aspx.cs" Inherits="GRCweb1.Modul.Factory.LinkProduktifitasOutput" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePenal1" runat="server">
        <ContentTemplate>
           <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; font-family: Calibri;">
                                        <b>PENGURANG WAKTU/MENIT DENGAN WAKTU BREAKDOWN UNTUK MODUL PRODUKTIFITAS DAN OUTPUT PRODUKSI
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <asp:Panel ID="PanelAtas" runat="server" Visible="true" BackColor="White">
                                    <div id="Div2" style="height: 30px;">
                                        <table style="width: 100%">
                                            <tr style="width: 100%">
                                                <td style="font-size: x-small; width: 20%;" bgcolor="#CCCCCC">
                                                    <asp:Label ID="LabelAtas" runat="server" Style="font-family: Calibri; font-size: small; font-weight: 700"
                                                        Visible="true">&nbsp; Filter Berdasarkan   :</asp:Label>
                                                </td>
                                                <td style="height: 3px; width: 80%; text-align: center; color: #FFFFFF; font-weight: 700;"
                                                    valign="top" bgcolor="#6699FF">

                                                    <asp:RadioButton ID="RBP" runat="server" AutoPostBack="True" OnCheckedChanged="RBP_CheckedChanged"
                                                        Style="font-family: Calibri; font-size: x-small; font-style: italic;" Text="Produktifitas"
                                                        TextAlign="Left" Width="300px" />

                                                    <asp:RadioButton ID="RBO" runat="server" AutoPostBack="True" OnCheckedChanged="RBO_CheckedChanged"
                                                        Style="font-family: Calibri; font-size: x-small; font-style: italic;" Text="OutPut"
                                                        TextAlign="Left" Width="300px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <hr />
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%; font-family: Calibri; font-weight: 700;">&nbsp; Pilih Line & GROUP 
                                        </td>
                                        <td style="width: 60%; font-family: Calibri; font-weight: 700;">
                                            <asp:DropDownList ID="ddlLine" AutoPostBack="True" runat="server"
                                                OnTextChanged="ddlLine_SelectedChange"
                                                Style="font-family: Calibri; font-weight: 700; font-size: x-small;" Width="20%">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGroup" runat="server"
                                                Style="font-family: Calibri; font-weight: 700; font-size: x-small;" Enabled="false" Width="15%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>

                                    <tr id="periodebln" runat="server" visible="false">
                                        <td style="width: 20%; font-family: Calibri; font-weight: 700;">&nbsp; Pilih Periode Bulan
                                        </td>
                                        <td style="width: 60%; font-family: Calibri; font-weight: 700;">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Width="40%" Style="font-family: Calibri; font-weight: 700; font-size: x-small;">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server" Width="10%" Style="font-family: Calibri; font-weight: 700; font-size: x-small;">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%">
                                            <%--<asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                Style="font-family: Calibri" />--%>
                                        </td>
                                    </tr>

                                    <tr id="periodeharian" runat="server" visible="true">
                                        <td style="width: 20%; font-family: Calibri; font-weight: 700;">&nbsp; Pilih Periode Tanggal
                                        </td>
                                        <td rowspan="1" style="width: 278px;">
                                            <asp:TextBox ID="txtFromPostingPeriod" runat="server" BorderStyle="Groove" Width="200px"
                                                Style="font-size: x-small; font-family: Calibri;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtFromPostingPeriod"></cc1:CalendarExtender>
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>

                                    <tr id="Tr1" runat="server" visible="true">
                                        <td style="width: 20%; font-family: Calibri; font-weight: 700;">&nbsp; 
                                        </td>
                                        <td rowspan="1" style="width: 278px;">
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                Style="font-family: Calibri; font-size: x-small;" />

                                            &nbsp;<asp:RadioButton ID="rbBulan" runat="server" AutoPostBack="True"
                                                Checked="false" Font-Size="X-Small" Text="bulanan" OnCheckedChanged="rbBulan_CheckedChanged"
                                                Style="font-family: Calibri" />
                                            &nbsp;<asp:RadioButton ID="rbHarian" runat="server" AutoPostBack="True" Checked="true"
                                                Font-Size="X-Small" Text="harian" OnCheckedChanged="rbHarian_CheckedChanged" Style="font-family: Calibri" />
                                        </td>
                                        <td></td>
                                        <td style="width: 20%"></td>
                                    </tr>

                                </table>
                                <asp:Panel ID="PanelProduktifitas" runat="server">
                                    <div class="contentlist" id="lstP" style="height: 360px">
                                        <table class="tbStandart" style="width: 100%; font-size: x-small; font-family: Calibri;">
                                            <tr style="width: 100%">
                                                <td style="width: 3%;">
                                                    <asp:Label ID="lblNamaIndex" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold; text-align: left;">Hal :</asp:Label>
                                                </td>
                                                <td style="width: 5%; text-align: left;">
                                                    <asp:Label ID="lblIndex" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold"></asp:Label>
                                                </td>
                                                <td width="92%"></td>
                                            </tr>
                                        </table>
                                        <table class="tbStandart" style="width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak tengah" rowspan="2" style="width: 7%; font-family: Calibri;">Tanggal
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 41%; font-family: Calibri;">Deskripsi
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%; font-family: Calibri;">GP
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 7%; font-family: Calibri;">Waktu/Menit
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%; font-family: Calibri;">BDT Sch
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 15%; font-family: Calibri;">Waktu/Menit - BDT Sch
                                                    </th>
                                                    <th class="kotak tengah" colspan="1" style="width: 15%; font-family: Calibri;">BDT Sch
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%; font-family: Calibri;">&nbsp;
                                                    </th>
                                                    <tr class="tbHeader">
                                                        <th class="kotak tengah" style="width: 15%; font-family: Calibri;">Update
                                                        </th>
                                                    </tr>
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: Calibri">
                                                <asp:Repeater ID="lst" runat="server" OnItemDataBound="lst_DataBound" OnItemCommand="lst_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="lst2" runat="server">
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtTgl" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak">&nbsp;
                                                                <asp:Label ID="txtDeskripsi" runat="server" Text='<%# Eval("deskripsi") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtGP" runat="server" Text='<%# Eval("GP") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtWaktuMenit" runat="server" Text='<%# Eval("WaktuMenit2") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtBDT_Sch0" runat="server" Text='<%# Eval("BDT_Sch") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtWaktuMenit2" runat="server" Text='<%# Eval("WaktuMenit") %>'></asp:Label>
                                                            </td>
                                                            <%-- <td class="kotak tengah">
                                                                <asp:Label ID="txtBDT_Sch" runat="server" Text='<%# Eval("BDT_Sch") %>'></asp:Label>
                                                            </td>--%>
                                                            <td class="kotak tengah">
                                                                <asp:DropDownList ID="ddlGP" runat="server" Width="70%" CssClass="noBorder" AutoPostBack="true"
                                                                    OnTextChanged="ddlGP_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="edit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" />
                                                                <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Save" Visible="false" />
                                                                <%-- <asp:ImageButton ID="add" runat="server" ImageUrl="~/images/add.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="add" />--%>
                                                                <asp:ImageButton ID="del" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="del" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <tr class="total baris" id="ftr" runat="server">
                                                            <td class="kotak angka" colspan="5">
                                                                <strong>Total</strong>
                                                            </td>
                                                            <td class="kotak bold angka"></td>
                                                            <td class="kotak bold angka"></td>
                                                            <td class="kotak bold angka"></td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <table align="center">

                                            <tr>
                                                <td style="font-family: Calibri; font-size: x-small">
                                                    <div style="text-align: center; font-family: Calibri; font-size: small;">
                                                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPage" Style="padding: 4px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-weight: bold"
                                                                    CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                                                    runat="server" Font-Bold="True"><%# Container.DataItem %>  
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelOutPut" runat="server" Visible="false" Style="font-size: xx-small">
                                    <div class="contentlist" id="Div3" style="height: 360px">
                                        <table class="tbStandart" style="width: 100%; font-size: x-small; font-family: Calibri;">
                                            <tr style="width: 100%">
                                                <td style="width: 3%;">
                                                    <asp:Label ID="lblNamaIndex1" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold; text-align: left;">Hal :</asp:Label>
                                                </td>
                                                <td style="width: 5%; text-align: left;">
                                                    <asp:Label ID="lblIndex1" runat="server" Visible="true" Style="font-family: Calibri; font-size: x-small; font-weight: bold"></asp:Label>
                                                </td>
                                                <td width="92%"></td>
                                            </tr>
                                        </table>
                                        <table class="tbStandart" style="width: 100%; font-size: x-small; font-family: Calibri;">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th class="kotak tengah" rowspan="2" style="width: 7%;">Tanggal
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 15%;">Deskripsi
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%;">GP
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 8%;">Waktu/Menit
                                                    </th>
                                                    <th class="kotak tengah" colspan="2" style="width: 15%;">BDT
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 15%;">Waktu/Menit-Sch-NonSch
                                                    </th>
                                                    <th class="kotak tengah" colspan="2" style="width: 30%;">Update
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%;">&nbsp;
                                                    </th>
                                                    <tr class="tbHeader">
                                                        <th class="kotak tengah" style="width: 7%;">Sch
                                                        </th>
                                                        <th class="kotak tengah" style="width: 8%;">Non Sch
                                                        </th>
                                                        <th class="kotak tengah" style="width: 15%;">Sch
                                                        </th>
                                                        <th class="kotak tengah" style="width: 15%;">Non Sch
                                                        </th>
                                                    </tr>
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: Calibri; font-size: x-small;">
                                                <div>
                                                    <asp:Repeater ID="lst3" runat="server" OnItemDataBound="lst3_DataBound" OnItemCommand="lst3_Command">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris" id="lst4" runat="server">
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="txtTgl2" runat="server" Text='<%# Eval("tgl") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak">&nbsp;
                                                                    <asp:Label ID="txtDeskripsi2" runat="server" Text='<%# Eval("deskripsi") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="txtGP2" runat="server" Text='<%# Eval("GP") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <asp:Label ID="txtWaktuMenit2" runat="server" Text='<%# Eval("WaktuMenit2") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <asp:Label ID="txtBDT_Sch02" runat="server" Text='<%# Eval("BDT_Sch") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <asp:Label ID="txtBDT_NonSch02" runat="server" Text='<%# Eval("BDT_NonSch") %>'></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="txtWaktuMenit22" runat="server" Text='<%# Eval("WaktuMenit") %>'></asp:Label>
                                                                </td>
                                                                <%--<td class="kotak tengah">
                                                                <asp:DropDownList ID="ddlSch" runat="server" Width="70%" CssClass="noBorder" 
                                                               AutoPostBack="true"
                                                                    OnTextChanged="ddlSch_SelectedIndexChanged" Font-Names="Calibri">
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                                <td class="kotak tengah">
                                                                    <asp:DropDownList ID="ddlSch" runat="server" Width="70%" CssClass="noBorder" OnTextChanged="ddlSch_SelectedIndexChanged"
                                                                        Font-Names="Calibri">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <%--<td class="kotak tengah">
                                                                <asp:DropDownList ID="ddlNonSch" runat="server" Width="70%" CssClass="noBorder" 
                                                                AutoPostBack="true"
                                                                    OnTextChanged="ddlNonSch_SelectedIndexChanged" Font-Names="Calibri">
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                                <td class="kotak tengah">
                                                                    <asp:DropDownList ID="ddlNonSch" runat="server" Width="70%" CssClass="noBorder" OnTextChanged="ddlNonSch_SelectedIndexChanged"
                                                                        Font-Names="Calibri">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:ImageButton ID="edit2" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="Edit2" />
                                                                    <asp:ImageButton ID="simpan2" runat="server" ImageUrl="~/images/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="Save2" Visible="false" />
                                                                    <asp:ImageButton ID="del2" runat="server" ImageUrl="~/images/Delete.png" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="del2" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr class="total baris" id="ftr2" runat="server">
                                                                <td class="kotak angka" colspan="6">
                                                                    <strong>Total</strong>
                                                                </td>
                                                                <td class="kotak bold angka"></td>
                                                            </tr>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </tbody>
                                        </table>
                                        <table align="center">
                                            <%--  <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>--%>
                                            <%--<tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="font-family: Calibri; font-size: x-small">
                                                    <div style="text-align: center; font-family: Calibri; font-size: small;">
                                                        <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPage" Style="padding: 1px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-weight: bold"
                                                                    CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                                                    runat="server" Font-Bold="True"><%# Container.DataItem %>  
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
