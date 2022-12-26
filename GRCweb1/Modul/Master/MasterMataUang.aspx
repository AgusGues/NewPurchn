<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterMataUang.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterMataUang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed; height: 100%" width="100%">
                    <tbody>
                        <tr>
                            <td >
                                <table class="nbTableHeader" style="width: 100%; margin-top: 1px;">
                                    <tr>
                                        <td style="width: 47%">
                                            <strong>&nbsp;Mata Uang</strong>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" onserverclick="btnNew_ServerClick" type="button" value="Baru" />
                                        </td>
                                        <td style="width: 67px">
                                            <input id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" type="button" value="Simpan" />
                                        </td>
                                        <td style="width: 5px">
                                            <input id="btnDelete" runat="server" onserverclick="btnDelete_ServerClick" type="button" value="Hapus" />
                                        </td>
                                        <td style="width: 70px">
                                            <input type="button" id="btnList" runat="server" onserverclick="btnList_ServerClick" value="List Kurs" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnAddKurs" runat="server" onserverclick="btnAddKurs_ServerClick" type="button" value="Add Kurs BI" onclick="return btnAddKurs_onclick()" />
                                        </td>
                                        <td style="width: 4px">
                                            <input id="btnAddKurs0" runat="server" onserverclick="btnAddKurs0_ServerClick" type="button" value="Add Kurs Pajak" />
                                        </td>
                                        <td style="width: 119px">&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                    <div id="frm10" runat="server" visible="false">
                                        <table class="tblForm" style="width: 100%;">
                                            <tr>
                                                <td style="width: 230px; height: 3px" valign="top">&nbsp;<span style="font-size: 10pt"> Pilih Currency Info</span>&nbsp;
                                                </td>
                                                <td style="width: 204px; height: 3px" valign="top" colspan="2">
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"
                                                        Width="334px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 3px; width: 169px;" valign="top">&nbsp;
                                                </td>
                                                <td style="width: 209px; height: 3px" valign="top"></td>
                                                <td style="width: 205px; height: 3px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 230px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; ID</span>
                                                </td>
                                                <td style="height: 6px" valign="top" colspan="3">
                                                    <asp:TextBox ID="txtMUID" runat="server" BorderStyle="Groove" Width="63px" ReadOnly="True"></asp:TextBox>
                                                    <asp:Label ID="LabelKeterangan" runat="server" Text="*)"></asp:Label>
                                                </td>
                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 230px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Nama</span>
                                                </td>
                                                <td style="height: 6px; width: 204px;" valign="top">
                                                    <asp:TextBox ID="txtMUName" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                </td>
                                                <td align="right" valign="top">&nbsp;
                                                </td>
                                                <td style="height: 6px; width: 169px;" valign="top">&nbsp;
                                                </td>
                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 230px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp;Two ISO Letter&nbsp; </span>
                                                </td>
                                                <td style="height: 6px; width: 204px;" valign="top">
                                                    <asp:TextBox ID="Txt2ISO" runat="server" ReadOnly="True" Width="232px"></asp:TextBox>
                                                </td>
                                                <td align="right" style="height: 6px; width: 204px;" valign="top">&nbsp;
                                                </td>
                                                <td style="height: 6px; width: 169px;" valign="top">&nbsp;
                                                </td>
                                                <td style="width: 209px; height: 6px" valign="top">&nbsp;
                                                </td>
                                                <td style="width: 205px; height: 6px" valign="top">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 230px; height: 6px" valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Lambang</span>
                                                </td>
                                                <td style="height: 6px;" valign="top" colspan="2">
                                                    <asp:TextBox ID="txtMUSymbol" runat="server" Width="134px"></asp:TextBox>
                                                    <input id="btnSymbol" runat="server" onserverclick="btnSymbol_serverclick" style="background-image: url('../images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 91px;"
                                                        type="button"
                                                        value="Lambang saja" />&nbsp;
                                                <input id="btnSymbol0" runat="server" onserverclick="btnSymbol0_serverclick" style="background-image: url('../images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 139px;"
                                                    type="button"
                                                    value="Lambang + 2 ISO Letter" />
                                                </td>
                                                <td style="height: 6px; width: 169px;" valign="top"></td>
                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 40px; padding-left: 20px;" colspan="3">
                                                    <asp:Panel ID="PanelKurs" runat="server" Visible="False">
                                                        <table style="width: 100%;" bgcolor="#CCFFFF">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <b>Input Kurs BI</b>
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                                <td align="center">
                                                                    <input id="btnUpdate1" runat="server" onserverclick="btnUpdate1_ServerClick" style="background-image: url('file:///E:/MyProject/BPAS_KRWG_051211/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 59px;"
                                                                        type="button"
                                                                        value="Batal" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 137px">
                                                                    <span style="font-size: 10pt">&nbsp; Kurs</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtKurs" runat="server" BorderStyle="Groove" Width="94px">0</asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td align="center">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 137px">
                                                                    <span style="font-size: 10pt">&nbsp; Periode</span>
                                                                </td>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:TextBox ID="txtTgl1" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                                                    <cc11:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtTgl1"></cc11:CalendarExtender>
                                                                </td>
                                                                <td align="center" style="width: 28px">s/d
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTgl2" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                                                    <cc11:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtTgl2"></cc11:CalendarExtender>
                                                                </td>
                                                                <td align="center">
                                                                    <input id="btnUpdate0" runat="server" align="middle" onserverclick="btnUpdate0_ServerClick"
                                                                        style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                                                        value="Simpan" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 40px; padding-left: 20px">
                                                    <asp:Panel ID="PanelKurs0" runat="server" Visible="False">
                                                        <table bgcolor="#CCFFFF" style="width: 100%;">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <b>Input Kurs Pajak</b>
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                                <td align="center">
                                                                    <input id="btnUpdate2" runat="server" onserverclick="btnUpdate2_ServerClick" style="background-image: url('file:///E:/MyProject/BPAS_KRWG_051211/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px; width: 59px;"
                                                                        type="button"
                                                                        value="Batal" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 137px">
                                                                    <span style="font-size: 10pt">&nbsp; Kurs</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtKurs0" runat="server" BorderStyle="Groove" Width="94px">0</asp:TextBox>
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td align="center">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 137px">
                                                                    <span style="font-size: 10pt">&nbsp; Periode</span>
                                                                </td>
                                                                <td align="right" style="width: 90px">
                                                                    <asp:TextBox ID="txtTgl3" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                                                    <cc11:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtTgl1"></cc11:CalendarExtender>
                                                                </td>
                                                                <td align="center" style="width: 28px">s/d
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTgl4" runat="server" BorderStyle="Groove" Height="22px" Width="95px"></asp:TextBox>
                                                                    <cc11:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                                        TargetControlID="txtTgl2"></cc11:CalendarExtender>
                                                                </td>
                                                                <td align="center">
                                                                    <input id="btnUpdate3" runat="server" align="middle" onserverclick="btnUpdate3_ServerClick"
                                                                        style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Simpan" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 137px">&nbsp;</td>
                                                                <td align="right" style="width: 90px">&nbsp;</td>
                                                                <td align="center" style="width: 28px">&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td align="center">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td  colspan="3"
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <b>List Kurs BI</b>
                                    <div id="Panel3" runat="server" style="background-color: White;  border: 2px solid #B0C4DE; padding: 5px">
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                            OnRowDataBound="GridView1_RowDataBound" PageSize="20" Width="100%" Visible="false">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="MUID" HeaderText="MUID" />
                                                <asp:BoundField DataField="Kurs" HeaderText="Kurs" />
                                                <asp:BoundField DataField="drtgl" HeaderText="drtgl" />
                                                <asp:BoundField DataField="sdTgl" HeaderText="sdTgl" />
                                                <asp:ButtonField Text="Hapus" />
                                            </Columns>
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                                Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                        <div id="newList" runat="server" >
                                        <table style="border-collapse:collapse; width:100%; font-size:small">
                                            <thead style="display:block; height:25px">
                                            <tr class="tbHeader">
                                                <th class="kotak" style="width:5%">#</th>
                                                <th class="kotak" style="width:15%">Periode</th>
                                                <th class="kotak" style="width:10%">USD</th>
                                                <th class="kotak" style="width:10%">SGD</th>
                                                <th class="kotak" style="width:10%">JPY</th>
                                                <th class="kotak" style="width:10%">EUR</th>
                                                <th class="kotak" style="width:5%">&nbsp</th>
                                            </tr>
                                            </thead>
                                            <tbody style="display:block; max-height:200px; overflow:auto">
                                                <asp:Repeater ID="lstKurs" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="baris EvenRows">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah"><%# Eval("Periode") %></td>
                                                            <td class="kotak angka"><%# Eval("USD","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("SGD","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("JPY","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("EUR","{0:N2}") %></td>
                                                            <td class="kotak">&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                     <tr class="baris OddRows">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah"><%# Eval("Periode") %></td>
                                                            <td class="kotak angka"><%# Eval("USD","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("SGD","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("JPY","{0:N2}") %></td>
                                                            <td class="kotak angka"><%# Eval("EUR","{0:N2}") %></td>
                                                            <td class="kotak">&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        </div>
                                    </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
