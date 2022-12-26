<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapAssetNew.aspx.cs" Inherits="GRCweb1.Modul.Asset_Management.RekapAssetNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript">

        function loadHistory(KodeAsset) {

            params = 'dialogWidth=1245px';
            params += ', dialogHeight=500px'
            params += ', top=0, left=0'
            params += ',scrollbars=yes';
            window.showModalDialog("../../ModalDialog/RekapDetailAssetKomponen.aspx?d=" + KodeAsset, "PES", params);

            return false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP ASSET</strong>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnBack" runat="server" Text="Form Input" Visible="false" OnClick="btnBack_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="content">
                                <table class="formh" style="width:100%; font-size:x-small">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:10%; font-family: Calibri;">Group Asset</td>
                                        <td style="width:30%"><asp:DropDownList ID="ddlGroupAsset" Width="60%" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGroupAsset_Change"></asp:DropDownList>
                                        <td style="width:30%">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td style="font-family: Calibri">Kelas Asset</td>
                                        <td><asp:DropDownList ID="ddlKelasAsset" AutoPostBack="true" runat="server" Width="60%" OnSelectedIndexChanged="ddlKelasAsset_Change"><asp:ListItem Value="">--Pilih--</asp:ListItem></asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr id="SubClassID" runat="server" visible="false">
                                        <td>&nbsp;</td>
                                        <td style="font-family: Calibri">SubKelas Asset</td>
                                        <td><asp:DropDownList ID="ddlSubKelasAsset" runat="server" Width="60%"><asp:ListItem Value="">--Pilih--</asp:ListItem></asp:DropDownList></td>
                                        <td></td>
                                    </tr>
                                    <tr style="display:none">
                                        <td>&nbsp;</td>
                                        <td style="font-family: Calibri">Lokasi Asset</td>
                                        <td colspan="2"><asp:DropDownList ID="ddlLokasiAsset" runat="server" Width="60%"></asp:DropDownList></td>
                                    </tr>
                                    <tr style="">
                                        <td>&nbsp;</td>
                                        <td style="font-family: Calibri">Urutkan Berdasarkan</td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlUrutan" runat="server" Width="60%">
                                                <asp:ListItem Value="NamaAsset">-</asp:ListItem>
                                                <asp:ListItem Value="Lokasi">Lokasi Asset</asp:ListItem>
                                                <%--<asp:ListItem Value="Tanggal">Tanggal Perolehan</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2">
                                            <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" 
                                                Text="Preview" style="font-family: Calibri" />
                                            <asp:Button ID="exPort" runat="server" OnClick="btnExport_Click" 
                                                Text="Export To Excel" style="font-family: Calibri" />
                                            <asp:Button ID="opname" runat="server" OnClick="btnOpname_Click" 
                                                Text="Sheet Opname" style="font-family: Calibri" />
                                        </td>
                                    </tr>
                                </table>
                                    <div class="contentlist" style="height:370px; display:block" id="lst" runat="server">
                                        <table style="width:100%; font-size:x-small; border-collapse:collapse">
                                        <thead>
                                            <tr class="tbHeader tengah">
                                                <th class="kotak" style="width:4%; font-family: Calibri;">No.</th>
                                                <%--<th class="kotak" style="width:12%">Kategori</th>
                                                <th class="kotak" style="width:10%">Kelas Asset</th>--%>
                                                <th class="kotak" style="width:8%; font-family: Calibri;">Kode Asset</th>
                                               <%-- <th class="kotak" style="width:8%">Kode Plant</th> --%>
                                                <th class="kotak" style="width:52%; font-family: Calibri;">Nama Asset</th>
                                                <th class="kotak" style="width:10%; font-family: Calibri;">Nilai Asset</th>
                                                <th class="kotak" style="width:21%; font-family: Calibri;">Lokasi</th>
                                                <th class="kotak" style="width:5%; font-family: Calibri;">Qty</th>
                                            </tr>
                                         </thead>
                                         <tbody style="font-family: Calibri">
                                         <asp:Repeater ID="lstKateg" runat="server" OnItemDataBound="lstKateg_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Eval("KodeGroup") %></td>
                                                    <td class="kotak" colspan="5">&nbsp;<b>GROUP ASSET :</b>&nbsp;<%# (Eval("NamaGroup")).ToString().ToUpper() %></td>
                                                    <%--<td class="kotak">&nbsp;</td>--%>
                                                 </tr>
                                                    <asp:Repeater ID="lstKelas" runat="server" OnItemDataBound="lstKelas_Databound" OnItemCommand="lstKelas_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="Line3">
                                                                <td class="kotak angka"><%# Eval("KodeClass") %></td>
                                                                <%--<td colspan="4" style="margin-left:10px">&nbsp;<b>CLASS :</b>&nbsp;<i><%# Eval("NamaClass").ToString().ToUpper() %> (<%# Eval("KodeClass2").ToString().ToUpper()%>)</i> &nbsp;&nbsp; - &nbsp;&nbsp;<b>SUBCLASS : </b>&nbsp;<i><%# Eval("NamaSubClass").ToString().ToUpper() %> (<%# Eval("KodeSubClass").ToString().ToUpper() %>)</i></td>--%>
                                                                <td colspan="4" style="margin-left:10px">&nbsp;<b>CLASS :</b>&nbsp;<i><%# Eval("NamaClass").ToString().ToUpper() %>&nbsp;&nbsp; - &nbsp;&nbsp;<b>SUBCLASS : </b>&nbsp;<i><%# Eval("NamaSubClass").ToString().ToUpper() %>(<%# Eval("KodeSubClass").ToString().ToUpper() %>)</i></td>
                                                                <%--<td class="kotak"><%# Eval("KodeAsset")%></td>--%>
                                                                <%--<td class="kotak">&nbsp;</td>--%>
                                                                <td class="kotak"><%# Eval("JumlahAsset")%></td>
                                                            </tr>
                                                            <asp:Repeater ID="lstSubClass" runat="server" OnItemDataBound="lstSubClass_DataBound" OnItemCommand="lstSubClass_ItemCommand">
                                                                <ItemTemplate>
                                                                    <%--<tr class="EvenRows baris">
                                                                       <td class="kotak">&nbsp;</td>
                                                                       <td class="kotak" colspan="4"><%# Eval("KodeGroup") %>. <%# Eval("NamaGroup") %></td>
                                                                       <td class="kotak">&nbsp;</td>
                                                                    </tr>--%>
                                                                    <tr class="EvenRows baris" id="lst" runat="server">  
                                                                       <td class="kotak">&nbsp;</td>
                                                                       <%--<td class="kotak" colspan="2"><%# Eval("KodeGroup") %>. <%# Eval("NamaGroup") %>.<%# Eval("KodeAsset")%></td>--%>
                                                                       <td class="kotak"><%# Eval("KodeAsset")%></td>
                                                                       <%--<td class="kotak"><asp:LinkButton ID="link" runat="server" Text='<%# Eval("KodeAsset") %>' CommandName="pilih" CommandArgument='<%# Eval("KodeAsset") %>'></asp:LinkButton></td>--%>
                                                                       <td class="kotak"><%# Eval("NamaGroup")%></td>
                                                                       <td class="kotak"><%# Eval("NilaiAsset","{0:N2}") %></td> 
                                                                       <td class="kotak" nowrap="nowrap"><%# Eval("NamaLokasi") %></td>
                                                                       <td class="kotak"><%# Eval("JumlahAsset")%></td>
                                                                       <%--<td class="kotak">&nbsp;</td>--%>
                                                                       <%--<td class="kotak"><%# Eval("KodeAsset")%></td>--%>
                                                                       <%--<td class="kotak"><%# Eval("KodeAsset")%></td>--%>
                                                                    </tr>
                                                                    <asp:Repeater ID="lstAsset" runat="server" OnItemCommand="lstAsset_ItemCommand" OnItemDataBound="lstAsset_DataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="OddRows baris">
                                                                                <td class="kotak tengah">&nbsp</td>
                                                                               <%-- <td class="kotak"><%# Eval("NamaClass")%></td>
                                                                                <td class="kotak"><%# Eval("NamaSubClass")%></td>--%>
                                                                                <td class="kotak tengah cls"><%# Eval("KodeAsset") %></td>
                                                                              <%--  <td class="kotak tengah"><%# Eval("ItemKode") %></td>--%>
                                                                                <td class="kotak"><%# Eval("Deskripsi").ToString().ToUpper()%><asp:Label ID="lbl" runat="server"></asp:Label></td>
                                                                                <td class="kotak" nowrap="nowrap"><%# Eval("NamaLokasi") %></td>
                                                                                <td class="kotak tengah">
                                                                                    <asp:ImageButton ImageUrl="~/images/folder.gif" ID="edit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                            </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                         </tbody>
                                        </table>
                                    </div>
                                    <div class="contentlist" id="lstOpname" runat="server" style="display:none;">
                                        <table style="width:100%; font-size:x-small; border-collapse:collapse;" border="1">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th style="width:4%" class="kotak">No</th>
                                                    <td style="width:10%" class="kotak">Kode Asset</td>
                                                    <td style="width:8%" class="kotak">Kode Plant</td>
                                                    <td style="width:45%" class="kotak">Deskripsi Asset</td>
                                                    <td style="width:12%" class="kotak">Value</td>
                                                    <td style="width:10%" class="kotak">Tgl Perolehan</td>
                                                    <td style="width:4%" class="kotak">&nbsp;</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstLokasi" runat="server" OnItemDataBound="lstLokasi_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="total baris">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak"><b>Lokasi </b></td>
                                                            <td class="kotak" colspan="3"><b> <%# Eval("KodeLokasi").ToString().ToUpper() %> . <%# Eval("NamaLokasi").ToString().ToUpper() %></b></td>
                                                            <td class="kotak">&nbsp;</td>
                                                        </tr>
                                                        <asp:Repeater ID="lstGroupAsset" runat="server" OnItemDataBound="lstGroupAsset_DataBound">
                                                            <ItemTemplate>
                                                                <tr class="Line3">
                                                                    <td class="kotak tengah"></td>
                                                                    <td class="kotak"><b>Group Asset </b></td>
                                                                    <td class="kotak" colspan="3"><b><%# Eval("KodeGroup") %> .<%# Eval("NamaGroup").ToString().ToUpper() %></b></td>
                                                                    <td class="kotak">&nbsp;</td>
                                                                </tr>
                                                                <asp:Repeater ID="lstKelas" runat="server" OnItemDataBound="lstKelas_DataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="OddRows baris">
                                                                            <td class="kotak angka"></td>
                                                                            <td class="kotak"><b>Kelas Asset </b></td>
                                                                            <td class="kotak" colspan="3"><b> <%# Eval("KodeClass") %>. <%# Eval("NamaClass").ToString().ToUpper() %></b></td>
                                                                            <td class="kotak">&nbsp;</td>
                                                                        </tr>
                                                                        <asp:Repeater ID="lstSubKlass" runat="server" OnItemDataBound="lstSubKlass_DataBound">
                                                                            <ItemTemplate>
                                                                                <tr class="Line3">
                                                                                    <td class="kotak">&nbsp;</td>
                                                                                    <td class="kotak"><b>Sub Kelas </b></td>
                                                                                    <td class="kotak" colspan="3"><b><%# Eval("KodeSubClass")%>.<%# Eval("NamaSubClass").ToString().ToUpper()%></b></td>
                                                                                    <td class="kotak">&nbsp;</td>
                                                                                </tr>
                                                                                <asp:Repeater ID="lstDetails" runat="server" OnItemDataBound="lstDetails_DataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr class="EvenRows baris">
                                                                                            <td class="kotak">&nbsp;</td>
                                                                                            <td class="kotak tengah"><%# Eval("KodeAsset") %></td>
                                                                                            <%--<td class="kotak tengah"><%# Eval("ItemKode") %></td>--%>
                                                                                            <td class="kotak"><%# Eval("Deskripsi").ToString().ToUpper() %></td>
                                                                                            
                                                                                            <td class="kotak"><%# Eval("NilaiAsset","{0:N2}") %></td>
                                                                                            <td class="kotak tengah"><%# Eval("TglAsset","{0:d}") %></td>
                                                                                            <td class="kotak tengah">&nbsp;</td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ItemTemplate>
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
