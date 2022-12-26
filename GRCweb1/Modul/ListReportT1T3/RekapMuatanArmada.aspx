<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapMuatanArmada.aspx.cs" Inherits="GRCweb1.Modul.ListReportT1T3.RekapMuatanArmada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP MUATAN ARMADA</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" valign="top" class="content">
                                <table width="100%" style="border-collapse: collapse; margin-top: 10px">
                                    <tr>
                                        <td style="width: 12%; padding-left: 10px; height: 21px;"></td>
                                        <td style="width: 20%; height: 21px;"> </td>    
                                        <td style="height: 21px"> </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 12%; padding-left: 10px;">
                                            Periode
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtTglPeriod" runat="server" BorderStyle="Groove" Width="150" 
                                                AutoPostBack="True" ontextchanged="txtTglPeriod_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtTglPeriod">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; width: 12%;">Expedisi</td>
                                        <td> 
                                            <asp:DropDownList ID="ddlPlant" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged"
                                            Enabled="true" Width="100%" >
                                            </asp:DropDownList>    
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px; width: 12%;">Jenis Kendaraan</td>
                                        <td> <asp:DropDownList ID="ddlArmada" runat="server"  Enabled="true" Width="50%" >
                                            </asp:DropDownList>    
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"> &nbsp;</td>
                                     </tr>
                                      <tr>
                                        <td style="width: 12%">&nbsp;</td>
                                        <td><asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Execl" OnClick="btnExport_Click" />    
                                        </td>
                                        <td align="right"></td>
                                      </tr>    
                                </table>
                                <hr /> 
                                <div class="contentlist" style="height: 450px" id="div2" runat="server">
                                    <table style="width: 40%; font-size: x-small" border="0">
                                        <tr>
                                            <th colspan="4" align="left">
                                                <asp:Label runat="server" ID="lbltglSch"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                    <table style="width: 45%; border-collapse: collapse; font-size: x-small" border="0">
                                        <tr class="tbHeader">
                                        <th class="kotak" style="width: 1%" rowspan ="2"> #</th>
                                        <th class="kotak" style="width: 10%"> ARMADA</th>
                                        <th class="kotak" style="width: 10%" rowspan ="2">Schedule No</th>
                                        <th class="kotak" style="width: 15%" rowspan ="2">NAMA PRODUK</th>
                                        <th class="kotak" style="width: 3%" rowspan ="2">JUMLAH</th>
                                        </tr>
                                        <tr class="tbHeader">
                                        <th class="kotak" style="width: 5%"> 
                                            <asp:Label ID="lblPlant" runat="server" Text=""></asp:Label></th>
                                        </tr>
                                        <tbody>
                                            <asp:Repeater ID="lstArmada" runat="server" OnItemDataBound="lstArmada_DataBound">
                                                <ItemTemplate>
                                                    <tr class="total baris">
                                                        <td class="kotak tengah" ><b><%# Container.ItemIndex+1 %>.</b></td>
                                                        <td colspan="4" class="kotak ">
                                                            <b>&nbsp;&nbsp;<asp:Label runat="server" ID="lblCarType" Text='<%# Eval("CarType") %>'></asp:Label></b></td>
                                                    </tr>
                                                    <asp:Repeater ID="ListArmada1" runat="server" OnItemDataBound="ListArmada1_DataBound" OnItemCommand="ListArmada1_Command">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris">
                                                                <td class="kotak tengah">
                                                                    <%-- <%# Container.ItemIndex+1 %>--%>
                                                                </td>
                                                                <td class="kotak tengah"> </td>
                                                                <td class="kotak tengah"> <%# Eval("ExScheduleNo")%></td>
                                                                <td class="kotak "><%# Eval("Description")%></td>
                                                                <td class="kotak"><%# Eval("Qty")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <td class="kotak tengah">
                                                                    <%-- <%# Container.ItemIndex+1 %>--%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%-- <%# Eval("ScheduleDate", "{0:d}")%>--%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%-- <%# Eval("ScheduleDate", "{0:d}")%>--%>
                                                                </td>
                                                                <td class="kotak ">
                                                                    <%# Eval("Description")%>
                                                                </td>
                                                                <td class="kotak">
                                                                    <%# Eval("Qty")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <br />
                                     
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
