<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KertasDepoList0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KertasDepoList0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 8px 8px 8px;
            overflow-x: auto;
            min-height: .01%;
        }

        .btn {
            font-style: normal;
            border: 1px solid transparent;
            padding: 2px 4px;
            font-size: 11px;
            height: 24px;
            border-radius: 4px;
        }

        input, select, .form-control, select.form-control, select.form-group-sm .form-control {
            height: 24px;
            color: #000;
            padding: 2px 4px;
            font-size: 12px;
            border: 1px solid #d5d5d5;
            border-radius: 4px;
        }

        .table > tbody > tr > th, .table > tbody > tr > td {
            border: 0px solid #fff;
            padding: 2px 4px;
            font-size: 12px;
            color: #fff;
            font-family: sans-serif;
        }

        .contentlist {
            border: 0px solid #B0C4DE;
        }

        label {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">

        function ViewPDF(id) {
            params = 'width=890px';
            params += ', heigh=600px'
            params += ', top=20px, left=20px'
            params += ', resizable:yes'
            params += ', scrollbars:yes';
            window.open("../../ModalDialog/PDFPreview.aspx?nosj=" + id, "Preview", params);
        }
        function UploadPDF(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFile.aspx?nosj=" + id, "UploadFile", params);
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

    <div id="div1" runat="server" class="table-responsive" style="width: 100%;">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span><b>LIST PENGIRIMAN KERTAS DEPO</b></span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnBack" runat="server" Text="Input Pengiriman" OnClick="btnBack_Click" />
                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="List Pengiriman" OnClick="btnList_CLick" />

                </div>
            </div>
        <div id="lst" runat="server">
            <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                <tr>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 10%">Depo</td>
                    <td style="width: 25%">
                        <asp:DropDownList ID="ddlDepo" runat="server"></asp:DropDownList></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Periode</td>
                    <td>
                        <asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Tujuan Kirim</td>
                    <td>
                        <asp:DropDownList ID="ddlTujuanKirim" runat="server" Width="100%"></asp:DropDownList></td>
                    <td>&nbsp;</td>
                </tr>
                <tr id="log" runat="server">
                    <td>&nbsp;</td>
                    <td>Estimasi Kedatangan</td>
                    <td>
                        <asp:TextBox ID="txtEstimasi" runat="server" Width="50%"></asp:TextBox>
                        <div style="display: none">
                            <asp:RadioButton GroupName="ctr" ID="RadioButton1" runat="server" Text="All" />
                            <asp:RadioButton GroupName="ctr" ID="chkOTW" runat="server" Text="Kedatangan Hari ini" />
                            <asp:RadioButton GroupName="ctr" ID="chkOTW1" runat="server" Text="Kedatangan Besok" />
                        </div>
                    </td>
                    <td>
                        <cc1:CalendarExtender ID="ca1" runat="server" TargetControlID="txtEstimasi" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button class="btn btn-info" ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                        <asp:Button class="btn btn-info" ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Monitoring DO Kertas (untuk Periode 2020 ke bawah)</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="table-responsive" style="height: 500px" id="lstKirim" runat="server">
            <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                <thead>
                    <tr class="tbHeader">
                        <%--<th class="kotak" style="width:4%">No.</th>--%>
                        <th class="kotak" style="width: 9%">Depo</th>
                        <th class="kotak" style="width: 11%">Supplier</th>
                        <th class="kotak" style="width: 7%">Checker</th>
                        <th class="kotak" style="width: 6%">Tgl Kirim</th>
                        <th class="kotak" style="width: 6%">Estimasi Tiba</th>
                        <th class="kotak" style="width: 5%">Tujuan</th>
                        <th class="kotak" style="width: 10%">No SJ</th>
                        <th class="kotak" style="width: 9%">Expedisi</th>
                        <th class="kotak" style="width: 7%">PlatMobil</th>
                        <th class="kotak" style="width: 6%">Gross</th>
                        <th class="kotak" style="width: 6%">Kadar Air</th>
                        <th class="kotak" style="width: 6%">Netto</th>
                        <th class="kotak" style="width: 6%">Jml Bal</th>
                        <th class="kotak" style="width: 6%">Sampah</th>
                        <th class="kotak">&nbsp;</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="lstDepo" runat="server" OnItemDataBound="lstDepo_DataBound" OnItemCommand="lstDepo_Command">
                        <ItemTemplate>
                            <tr class="EvenRows baris" id="xx" runat="server">
                                <%--<td class="kotak tengah"><%# Container.ItemIndex+1 %></td>--%>
                                <td class="kotak" style="white-space: nowrap; overflow: hidden"><%# Container.ItemIndex+1 %>. <%# Eval("DepoName") %></td>
                                <td class="kotak fontKecil" style="white-space: nowrap; overflow: hidden">
                                    <asp:Label ID="txtspl" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label></td>
                                <td class="kotak"><%# Eval("Checker") %></td>
                                <td class="kotak tengah"><%# Eval("TglKirim","{0:d}") %></td>
                                <td class="kotak tengah"><%# Eval("TglETA", "{0:d}")%></td>
                                <td class="kotak tengah"><%# Eval("KirimVia") %></td>
                                <td class="kotak" style="white-space: nowrap"><%# Eval("NoSJ") %></td>
                                <td class="kotak" style="white-space: nowrap"><%# Eval("Expedisi") %></td>
                                <td class="kotak" style="white-space: nowrap"><%# Eval("NoPOL") %></td>
                                <td class="kotak angka"><%# Eval("GrossDepo","{0:N0}") %></td>
                                <td class="kotak angka"><%# Eval("KADepo", "{0:N2}")%></td>
                                <td class="kotak angka"><%# Eval("NettDepo", "{0:N0}")%></td>
                                <td class="kotak angka"><%# Eval("JmlBAL", "{0:N0}")%></td>
                                <td class="kotak angka"><%# Eval("Sampah", "{0:N2}")%></td>
                                <td class="kotak tengah" style="white-space: nowrap">
                                    <asp:ImageButton ID="edt" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" ImageUrl="~/images/folder.gif" />
                                    <asp:ImageButton ID="hps" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="delet" ImageUrl="~/images/trash.gif" />
                                    <asp:ImageButton ID="del" runat="server" CommandArgument='<%# Container.ItemIndex %>' CommandName="hapus" ImageUrl="~/images/Delete.png" />
                                    <asp:ImageButton ID="sts" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="tStatus" ImageUrl="~/images/po.png" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="table-responsive" id="lstMonitoring" runat="server">
            <div style="overflow: hidden;" id="DivHeaderRow">
            </div>
            <div style="overflow: scroll;height: 500px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <thead>
                        <tr class="tbHeader">
                            <th class="kotak 0" rowspan="2" style="width: 3%">#</th>
                            <th class="kotak 1" rowspan="2" style="width: 10%">No. SJ</th>
                            <th class="kotak 2" rowspan="2" style="width: 6%">No. Mobil</th>
                            <th class="kotak 3" rowspan="2" style="width: 9%">Expedisi</th>
                            <th class="kotak 4" rowspan="2" style="width: 6%">Tgl Kirim</th>
                            <th class="kotak 5" rowspan="2" style="width: 5%">BK</th>
                            <th class="kotak 6" rowspan="2" style="width: 5%">BB</th>
                            <th class="kotak 7" rowspan="2" style="width: 5%">KA 20%</th>
                            <th class="kotak 8" rowspan="2" style="width: 5%">Jml BAL</th>
                            <th class="kotak 7" rowspan="2" style="width: 5%">Sam pah</th>
                            <th class="kotak 9" rowspan="2" style="width: 5%">Tuju an</th>
                            <th class="kotak 10" rowspan="2" style="width: 6%">Tgl Estimasi</th>
                            <th class="kotak 11" rowspan="2" style="width: 6%">Tgl Tiba</th>
                            <th class="kotak 12" rowspan="2" style="width: 5%">NoPO</th>
                            <th class="kotak 13" rowspan="2" style="width: 5%">BK</th>
                            <th class="kotak 14" rowspan="2" style="width: 5%">BB</th>
                            <th class="kotak 15" rowspan="2" style="width: 5%">KA 0%</th>
                            <th class="kotak 8" rowspan="2" style="width: 5%">Jml BAL</th>
                            <th class="kotak" rowspan="2" style="width: 4%">Sam pah</th>
                            <th class="kotak 15" colspan="3" style="width: 8%">Selisih BB</th>
                            <th class="kotak 15" colspan="3" style="width: 30%">Data Pembayaran</th>
                        </tr>
                        <tr class="tbHeader">
                            <th class="kotak" style="width: 4%">(Kgs)</th>
                            <th class="kotak" style="width: 4%">(%)</th>
                            <th class="kotak" style="width: 4%">Bal</th>
                            <th class="kotak" style="width: 10%">Cash</th>
                            <th class="kotak" style="width: 10%">DP</th>
                            <th class="kotak" style="width: 10%">Pelunasan</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="lstMonDepo" runat="server" OnItemDataBound="lstMonDepo_DataBound">
                            <ItemTemplate>
                                <tr class="line3 baris">
                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                    <td class="kotak" colspan="24"><%# Eval("DepoName") %></td>
                                </tr>
                                <asp:Repeater ID="lstDepoDetail" runat="server" OnItemDataBound="lstDepoDetail_Databound">
                                    <ItemTemplate>
                                        <tr class="EvenRows baris" id="lstx" runat="server">
                                            <td class="kotak angka 0"><%# Container.ItemIndex+1 %></td>
                                            <td class="kotak 1" style="white-space: nowrap"><%# Eval("NoSJ") %></td>
                                            <td class="kotak 2" style="white-space: nowrap"><%# Eval("NOPOL") %></td>
                                            <td class="kotak 3" style="white-space: nowrap"><%# Eval("Expedisi") %></td>
                                            <td class="kotak tengah 4" style="white-space: nowrap"><%# Eval("TglKirim", "{0:d}")%></td>
                                            <td class="kotak angka 5"><%# Eval("GrossDepo", "{0:N0}")%></td>
                                            <td class="kotak angka 6"><%# Eval("NettDepo", "{0:N0}")%></td>
                                            <td class="kotak angka 7"><%# Eval("KADepo", "{0:N2}")%></td>
                                            <td class="kotak angka 8"><%# Eval("JmlBAL", "{0:N0}")%></td>
                                            <td class="kotak angka 8"><%# Eval("SampahDepo", "{0:N2}")%></td>
                                            <td class="kotak tengah 9"><%# Eval("KirimVia") %></td>
                                            <td class="kotak tengah 10" style="white-space: nowrap"><%# Eval("TglETA", "{0:d}")%></td>
                                            <td class="kotak tengah 11" style="white-space: nowrap"><%# (DateTime.Parse(Eval("TglReceipt").ToString()).Year < 2015)?"": Eval("TglReceipt", "{0:d}")%></td>
                                            <td class="kotak tengah 12" style="white-space: nowrap"><%# Eval("NOPO") %></td>
                                            <td class="kotak angka 13"><%# Eval("GrossPlant", "{0:N0}")%></td>
                                            <td class="kotak angka 14"><%# Eval("NettPlant", "{0:N0}")%></td>
                                            <td class="kotak angka 15"><%# Eval("KAPlant","{0:N2}") %></td>
                                            <td class="kotak angka 16"><%# Eval("Jumlah","{0:N0}") %></td>
                                            <td class="kotak angka 17"><%# Eval("Sampah","{0:N2}") %></td>
                                            <td class="kotak angka 18"><%# Eval("SelisihBB","{0:N0}")%></td>
                                            <td class="kotak tengah 19"><%# Eval("Persen","{0:N2}") %></td>
                                            <td class="kotak tengah 20" style="white-space: nowrap"></td>
                                            <td class="kotak angka 17"><%# Eval("NoCash")%></td>
                                            <td class="kotak angka 18"><%# Eval("NoDP")%></td>
                                            <td class="kotak tengah 19"><%# Eval("NoPelunasan")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div id="DivFooterRow" style="overflow: hidden">
            </div>
        </div>
            </div>
    </div>
</asp:Content>
