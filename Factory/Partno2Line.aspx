<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Partno2Line.aspx.cs" Inherits="GRCweb1.Modul.Factory.Partno2Line" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePenal1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%; font-family: Calibri;">
                                        <b>LIST PARTNO LISTPLANK & UKURAN KHUSUS YG MASUK KE LAPORAN REKAP DEFECT ISO
                                    </td>                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div >
                                <asp:Panel ID="PanelAtas" runat="server" Visible="true" BackColor="White">
                                    <div id="Div2" style="height: 30px;">
                                        <table style="width: 100%">
                                            <tr style="width: 100%">
                                                <td style="font-size: x-small; width: 20%;" >
                                                    <asp:Label ID="LabelAtas" runat="server" Style="font-family: Calibri; font-size: small;
                                                        font-weight: 700" Visible="true">&nbsp; Filter Berdasarkan   :</asp:Label>
                                                </td>
                                                <td style="height: 3px; width: 80%; text-align: center; color: #FFFFFF; font-weight: 700;"
                                                    valign="top" >
                                                    
                                                    <asp:RadioButton ID="RBUK" runat="server" AutoPostBack="True" OnCheckedChanged="RBUK_CheckedChanged"
                                                        Style="font-family: Calibri; font-size: x-small; font-style: italic;" Text="Partno Ukuran Khusus"
                                                        TextAlign="Left" Width="300px" ForeColor="Black" />
                                                        
                                                    <asp:RadioButton ID="RBLP" runat="server" AutoPostBack="True" OnCheckedChanged="RBLP_CheckedChanged"
                                                        Style="font-family: Calibri; font-size: x-small; font-style: italic;" Text="Partno ListPlank"
                                                        TextAlign="Left" Width="300px" ForeColor="Black" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <hr />
                                <table>
                                    <tr>
                                        <td style="width: 40%; font-family: Calibri; font-weight: 700;">
                                            &nbsp; Pilih Periode :
                                            <asp:DropDownList AutoPostBack="True" ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_SelectedChange"
                                                Style="font-family: Calibri; font-weight: 700">
                                            </asp:DropDownList>
                                            <asp:DropDownList AutoPostBack="True" ID="ddlTahun" runat="server" OnSelectedIndexChanged="ddlTahun_SelectedChange"
                                                Style="font-family: Calibri; font-weight: 700">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%">
                                                <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                    Style="font-family: Calibri" />
                                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                   
                                </table>
                                <asp:Panel ID="Panel1" runat="server">
                                   
                                    <div class="contentlist" id="lstP" style="height: 360px; overflow-x:auto;">
                                        <table class="tbStandart" style="width: 100%">
                                            <thead>
                                                <tr class="tbHeader">
                                                <th class="kotak tengah" rowspan="2" style="width: 5%; font-family: Calibri;">
                                                        No
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 10%; font-family: Calibri;">
                                                        Partno
                                                    </th>
                                                    <th class="kotak tengah" rowspan="2" style="width: 10%; font-family: Calibri;">
                                                        Asal Line
                                                    </th>
                                                    <th class="kotak tengah" colspan="2" style="width: 60%; font-family: Calibri;">
                                                        Keterangan Line
                                                    </th>                                                    
                                                    <th class="kotak tengah" rowspan="2" style="width: 5%">
                                                        &nbsp;
                                                    </th>
                                                    <tr class="tbHeader">
                                                        <th class="kotak tengah" style="width: 5%; font-family: Calibri;">
                                                            Line
                                                        </th>
                                                        <th class="kotak tengah" style="width: 5%; font-family: Calibri;">
                                                            Update
                                                        </th>
                                                    </tr>
                                                </tr>
                                            </thead>
                                            <tbody style="font-family: Calibri">
                                                <asp:Repeater ID="lst" runat="server" OnItemDataBound="lst_DataBound" OnItemCommand="lst_Command">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris" id="lst2" runat="server">
                                                        <td class="kotak tengah">
                                                                <asp:Label ID="txtNo" runat="server" Text='<%# Eval("No") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak">
                                                                <asp:Label ID="txtPartno" runat="server" Text='<%# Eval("Partno") %>'></asp:Label>
                                                            </td>
                                                             <td class="kotak tengah">
                                                                <asp:Label ID="txtKet" runat="server" Text='<%# Eval("Ket") %>'></asp:Label>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <asp:Label ID="txtLine" runat="server" Text='<%# Eval("Line") %>'></asp:Label>
                                                            </td>
                                                            
                                                            <td class="kotak tengah">
                                                                <asp:DropDownList ID="ddlLine" runat="server" Width="40%" CssClass="noBorder" AutoPostBack="true"
                                                                    OnTextChanged="ddlLine_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="kotak ">
                                                                <asp:ImageButton ID="edit" runat="server" ImageUrl="~/img/folder.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Edit" />
                                                                <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/img/Save_blue.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="Save" Visible="false" />
                                                                <asp:ImageButton ID="add" runat="server" ImageUrl="~/img/add.gif" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="add" />
                                                                <asp:ImageButton ID="del" runat="server" ImageUrl="~/img/Delete.png" CommandArgument='<%# Eval("ID") %>'
                                                                    CommandName="del" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
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
