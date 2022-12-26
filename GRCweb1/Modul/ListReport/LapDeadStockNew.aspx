<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapDeadStockNew.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapDeadStockNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width-1)) + '%';
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

    </script>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr valign="top">
                        <td style="height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        &nbsp;&nbsp;REKAP SPARE PART NON STOCK
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div class="content">
                                <table style="width: 100%; border-collapse: collapse; font-size: small">
                                    <tr>
                                        <td style="width: 10%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 10%">
                                            Periode
                                        </td>
                                        <td style="width: 35%">
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
                                            &nbsp;
                                        </td>
                                        <td>
                                            Departemen
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDept" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rbNS" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">All Non Stock</asp:ListItem>
                                                <asp:ListItem Value="3">Warning Stock</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">Dead Stock</asp:ListItem>
                                                <asp:ListItem Value="2">Non Dead Stock</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2 ">
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                            <asp:Button  id="btnSent" runat="server" Text="Sent Email" OnClick="btnSent_ServerClick" />
                                            <asp:Label ID="lblError" runat="server" Text="" Style="color: red;"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="height: 296px" id="lst" runat="server">
                                    <div id="DivRoot" >
                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                        </div>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                            <table id="thr" style="width: 100%; border-collapse: collapse; font-size: x-small"
                                                border="0">
                                                <thead>
                                                    <tr class="tbHeader baris">
                                                        <th rowspan="2" class="kotak" style="width: 4%">
                                                            No.
                                                        </th>
                                                        <th rowspan="2" class="kotak" style="width: 10%">
                                                            Item Code
                                                        </th>
                                                        <th rowspan="2" class="kotak" style="width: 35%">
                                                            Item Name
                                                        </th>
                                                        <th rowspan="2" class="kotak" style="width: 5%">
                                                            Unit
                                                        </th>
                                                        <th colspan="2" class="kotak">
                                                            Tanggal Terakhir
                                                        </th>
                                                        <th rowspan="2" class="kotak" style="width: 5%">
                                                            Umur(Bln)
                                                        </th>
                                                        <th rowspan="2" class="kotak" style="width: 8%">
                                                            Stock
                                                        </th>
                                                    </tr>
                                                    <tr class="tbHeader baris">
                                                        <th class="kotak" style="width: 10%">
                                                            Penerimaan
                                                        </th>
                                                        <th class="kotak" style="width: 10%">
                                                            Pemakaian
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="lstDS" runat="server" OnItemDataBound="lstDS_DataBound">
                                                        <ItemTemplate>
                                                            <tr class="total baris">
                                                                <td class="kotak tengah">
                                                                    <%# Container.ItemIndex+1 %>
                                                                </td>
                                                                <td colspan="3" class="kotak">
                                                                    <%# Eval("DeptName") %>
                                                                </td>
                                                                <td colspan="4" class="kotak">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="lstDetail" runat="server" OnItemDataBound="lstDetail_Databound">
                                                                <ItemTemplate>
                                                                    <tr class="OddRows baris" id="tr1" runat="server">
                                                                        <td class="kotak angka">
                                                                            <%# Container.ItemIndex+1 %>&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah xx">
                                                                            <%# Eval("ItemCode") %>
                                                                        </td>
                                                                        <td class="kotakItemName">
                                                                            <%# Eval("ItemName") %>'>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("Satuan") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("ReceiptDate") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("PakaiDate") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Umur","{0:N0}") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Stock","{0:N2}") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="EvenRows baris" id="tr1" runat="server">
                                                                        <td class="kotak angka">
                                                                            <%# Container.ItemIndex+1 %>&nbsp;
                                                                        </td>
                                                                        <td class="kotak tengah xx">
                                                                            <%# Eval("ItemCode") %>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("ItemName") %>
                                                                        </td>
                                                                        <td class="kotak">
                                                                            <%# Eval("Satuan") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("ReceiptDate") %>
                                                                        </td>
                                                                        <td class="kotak tengah">
                                                                            <%# Eval("PakaiDate") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Umur","{0:N0}") %>
                                                                        </td>
                                                                        <td class="kotak angka">
                                                                            <%# Eval("Stock","{0:N2}") %>
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
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
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel3" runat="server" BackColor="#99CCFF" Height="21px" HorizontalAlign="Center">
                <asp:Label ID="Label3" runat="server" BackColor="White" Height="100%" Text="   +   "
                    Font-Bold="True" Font-Size="Medium"></asp:Label>
                <asp:Label ID="LTambah" runat="server" BackColor="White" Height="100%" Text="0"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" BackColor="White" Height="100%" Text="   -   "></asp:Label>
                <asp:Label ID="LKurang" runat="server" BackColor="White" Height="100%" Text="0"></asp:Label>
                &nbsp;</asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
