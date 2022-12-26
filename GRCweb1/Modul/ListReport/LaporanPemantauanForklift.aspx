<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LaporanPemantauanForklift.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LaporanPemantaunForklift" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divImage and set it to visible
            var panelProg = $get('divImage');
            panelProg.style.display = '';
            // Disable button that caused a postback
            $get(args._postBackElement.id).disabled = false;
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divImage and hide it again
            var panelProg = $get('divImage');
            panelProg.style.display = 'none';
        }
    </script>

    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width - 1)) + '%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '2';
                DivHR.style.verticalAlign = 'top';

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

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr valign="top">
                        <td style="height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        &nbsp;&nbsp;REKAP PEMAKAIAN SPARE PART FORKLIFT
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td style="width: 100%">
                            <div >
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td>
                                            Periode
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBulan" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Jenis Forklift
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlForkLift" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:RadioButton ID="RBDetail" runat="server" AutoPostBack="True" 
                                                Checked="True" GroupName="a" oncheckedchanged="RBDetail_CheckedChanged" 
                                                 />Detail Spare Part
                                            <asp:RadioButton ID="RBRekap" runat="server" AutoPostBack="True" GroupName="a" 
                                                oncheckedchanged="RBRekap_CheckedChanged" />Pemantauan Spare Part
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <asp:Panel ID="PanelDetail" runat="server" Visible="True">
                                    <div style="height: 430px" id="lst" runat="server">
                                        <!--start Content Tabel -->
                                        <div id="rpl" runat="server">
                                            <div id="DivRoot" align="left">
                                                <div style="overflow: hidden;" id="DivHeaderRow">
                                                </div>
                                                <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                    <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small"
                                                        border="0">
                                                        <tr class="tbHeader">
                                                            <th class="kotak" style="width: 4%">
                                                                No.
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                Tanggal Pakai
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                No Pakai
                                                            </th>
                                                            <th class="kotak" style="width: 10%">
                                                                Kode Barang
                                                            </th>
                                                            <th class="kotak" style="width: 30%">
                                                                Nama Barang
                                                            </th>
                                                            <th class="kotak" style="width: 2%">
                                                                Satuan
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                Quantity
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                Harga
                                                            </th>
                                                            <th class="kotak" style="width: 8%">
                                                                Jumlah
                                                            </th>
                                                            <th class="kotak" style="width: ">
                                                                Keterangan
                                                            </th>
                                                        </tr>
                                                        <tbody>
                                                            <asp:Repeater ID="Reportpakaispfl" runat="server" OnItemDataBound="Reportpakaispfl_databound">
                                                                <ItemTemplate>
                                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                                        <td class="kotak tengah">
                                                                            <%# Container.ItemIndex+1 %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap">
                                                                            <%# Eval("Tanggal", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap">
                                                                            <%# Eval("PakaiNo")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap" title="">
                                                                            <%# Eval("ItemCode")%>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("ItemName")%>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("Unit") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Quantity") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("AvgPrice","{0:#,#00.00}") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Jumlah", "{0:#,#00.00}")%>
                                                                        </td>
                                                                        <td class="kotak ">
                                                                            <%# Eval("Keterangan")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="OddRows baris" id="tr1" runat="server">
                                                                        <td class="kotak tengah">
                                                                            <%# Container.ItemIndex+1 %>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap">
                                                                            <%# Eval("Tanggal", "{0:d}")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap">
                                                                            <%# Eval("PakaiNo")%>
                                                                        </td>
                                                                        <td class="kotak tengah" style="white-space: nowrap" title="">
                                                                            <%# Eval("ItemCode")%>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("ItemName")%>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("Unit") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Quantity") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("AvgPrice","{0:#,#00.00}") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Jumlah", "{0:#,#00.00}")%>
                                                                        </td>
                                                                        <td class="kotak ">
                                                                            <%# Eval("Keterangan")%>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                                <FooterTemplate>
                                                                    <tr class="Line3 baris" id="tr1" runat="server">
                                                                        <td class="kotak bold angka" colspan="8">
                                                                            Total
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div id="DivFooterRow" style="overflow: hidden">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelRekap" runat="server" Visible="True" Height="400px" style="overflow-x:scroll;">
                                        <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" 
                                            PageSize="15" Width="100%" >
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                  
                                </asp:Panel>
                                <!---end tabel -->
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
