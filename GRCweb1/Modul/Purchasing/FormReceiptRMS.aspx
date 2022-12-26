<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormReceiptRMS.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormReceiptRMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }

        label {
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        // fix for deprecated method in Chrome / js untuk bantu view modal dialog
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {
                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";
                // get the modal specs
                var mdattrs = arg3.split(";");
                for (i = 0; i < mdattrs.length; i++) {
                    var mdattr = mdattrs[i].split(":");
                    var n = mdattr[0];
                    var v = mdattr[1];
                    if (n) { n = n.trim().toLowerCase(); }
                    if (v) { v = v.trim().toLowerCase(); }
                    if (n == "dialogheight") {
                        h = v.replace("px", "");
                    } else if (n == "dialogwidth") {
                        w = v.replace("px", "");
                    } else if (n == "resizable") {
                        resizable = v;
                    } else if (n == "scroll") {
                        scroll = v;
                    } else if (n == "status") {
                        status = v;
                    }
                }
                var left = window.screenX + (window.outerWidth / 2) - (w / 2);
                var top = window.screenY + (window.outerHeight / 2) - (h / 2);
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
    </script>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel() { }

        function Cetak() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipRMS", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        //function confirm_delete() 
        //{
        //    if (confirm("Anda yakin untuk Cancel ?") == true)
        //        window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 250px; dialogWidth: 400px;scrollbars=no');
        //    else
        //        return false;
        //}

        function AgenLapak(id) {
            window.showModalDialog('../../ModalDialog/AgenLapak.aspx?id=' + id, '', 'status=0;toolbar=0;resizable:0;dialogHeight: 200px; dialogWidth: 500px;scrollbars=0;addressbar=0');
        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>


            <%--<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID=""
                ConfirmText="Anda yakin cancel untuk RMS ini?" OnClientCancel="onCancel"
                ConfirmOnFormSubmit="false"  />--%>






            <div id="Div1" runat="server">



                <table style="table-layout: fixed; height: 100%" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; padding-right: 5px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;RAW Receipt Slip</strong>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                Text="Cancel" OnClick="btnCancel_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <input id="btnPrint" onclick="Cetak()" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cetak" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnList" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="List" onserverclick="btnList_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnReprint" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="RePrint" onserverclick="btnReprint_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">RAW No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; height: 100%;" valign="top">
                                <div id="frm10" runat="server" style="width: 100%; background-color: #fff;">
                                    <table class="tblForm" id="Table4" style="width: 100%; font-size: x-small">
                                        <tr class="OddRows">
                                            <td style="padding-right: 3px" valign="middle" colspan="2" align="right">
                                                <asp:RadioButton ID="RadioBB" runat="server" Checked="True" Font-Size="X-Small" GroupName="g1" Text="Bahan Baku" />&nbsp;
                                                <asp:RadioButton ID="RadioBP" runat="server" Font-Size="X-Small" GroupName="g1" Text="Bahan Penunjang" />&nbsp;
                                                <asp:RadioButton ID="RadioBS" runat="server" GroupName="g1" Text="Bahan Bakar" />
                                            </td>
                                            <td valign="top"><span style="font-size: 10pt">&nbsp; No RRS</span></td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtMrsNo" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"></asp:TextBox>
                                                <asp:HiddenField ID="txtSubCompanyID" runat="server" Value="0" />
                                            </td>
                                            <td style="width: 205px;" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">&nbsp; Cari PO&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtCariOP" runat="server" AutoPostBack="True" BorderStyle="Groove" Width="233"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="poExt" runat="server" TargetControlID="txtCariOP" CompletionSetCount="2"
                                                    MinimumPrefixLength="3" ServicePath="AutoComplete.asmx" ServiceMethod="GetParsialPO">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td style="height: 3px; width: 102px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Stok</span>
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtStok" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" Enabled="False" Font-Bold="True" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                <asp:HiddenField ID="txtPOTipe" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">&nbsp; No PO&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtPONo" runat="server" BorderStyle="Groove" ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px; font-size: x-small;" valign="top">&nbsp; Tanggal
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp; Supplier&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtSupplier" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                    Width="233"></asp:TextBox></td>
                                            <td style="width: 102px; font-size: x-small;" valign="top">&nbsp; Dibuat Oleh&nbsp;
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                    Width="233" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 169px;" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Nama Barang</span>
                                            </td>
                                            <td style="height: 6px" valign="middle" colspan="3">
                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="570px">
                                                </asp:DropDownList>
                                                <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" PageSize="15" Width="100%">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                        ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                <asp:TextBox ID="txtItemID" runat="server" Text="" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">&nbsp; Kode Barang&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Qty PO</span>
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtQty" runat="server" BorderStyle="Groove" ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;" valign="top"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">&nbsp; Satuan&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtUom" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Qty Terima</span>
                                            </td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtQtyTerima" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;" valign="top">
                                                <asp:TextBox ID="txtTimbAsli" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; No SPP</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtNoSpp" runat="server" AutoPostBack="false" BorderStyle="Groove"
                                                    Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">&nbsp;&nbsp;<asp:Label ID="spl" runat="server" Text="Qty BPAS"></asp:Label></td>
                                            <td style="width: 209px;" valign="top">
                                                <asp:TextBox ID="txtQtyTimbang" runat="server" BorderStyle="Groove" Width="233" AutoPostBack="true" OnTextChanged="txtQtyTimbang_Change" Text="0"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;" valign="top"></td>
                                        </tr>
                                        <tr id="forLine" runat="server">
                                            <td style="width: 249px;">&nbsp; Untuk Line
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlToPlan" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlToPlan_SelectedIndexChanged" Width="570px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="forLapak" runat="server" visible="false">
                                            <td style="width: 249px;">&nbsp; No. Surat Jalan Supplier
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlSjLapak" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlSjLapak_SelectedIndexChanged" Width="570px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtKirimanID" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtLapakID" runat="server" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtItemCode2" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="forAgenJabar" runat="server" visible="false">
                                            <td>&nbsp; No.Surat Jalan Supplier</td>
                                            <td>
                                                <asp:TextBox ID="txtNoSJ" runat="server" Width="233"></asp:TextBox></td>
                                            <td>&nbsp;No. Mobil</td>
                                            <td>
                                                <asp:TextBox ID="txtNOPOL" runat="server" AutoPostBack="true" OnTextChanged="txtNOPOL_Change"></asp:TextBox></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp; <span style="font-size: 10pt">Kadar Air (%)</span>
                                            </td>
                                            <td rowspan="1" style="width: 204px;">
                                                <asp:TextBox ID="txtKadarAir" runat="server" Width="200px" AutoPostBack="true" OnTextChanged="txtKadarAir_Change" Text="0"></asp:TextBox>
                                                <%--<asp:TextBox ID="txtKadarAir" runat="server" BorderStyle="Groove" Width="200" AutoPostBack="true" OnTextChanged="txtKadarAir_Change" Text="0"></asp:TextBox>--%>
                                            </td>
                                            <td style="width: 102px;">&nbsp; Keterangan
                                            </td>
                                            <td style="width: 209px;">
                                                <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 249px;">&nbsp;
                                                <asp:Label ID="MemoHarian" runat="server" Visible="false">&nbsp;DO No</asp:Label>
                                            </td>
                                            <td style="width: 209px;">
                                                <asp:DropDownList ID="ddlMemoHarian" runat="server" Visible="false" Width="250px" OnSelectedIndexChanged="ddlMemoHarian_Change" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                            <td style="width: 102px;" valign="top">&nbsp;<asp:TextBox ID="txtPrice" runat="server" BorderStyle="Groove" ReadOnly="True" Visible="False"></asp:TextBox></td>
                                            <td style="width: 209px;" valign="middle">
                                                <%--<asp:LinkButton ID="lbAddOP0" runat="server" Font-Size="10pt" OnClick="lbAddOP_Click">Tambah</asp:LinkButton>--%>
                                                <asp:Button ID="lbAddOP" runat="server" Font-Size="10pt" OnClick="lbAddOP_Click" Text="Tambah" />&nbsp;
                                                <asp:Button ID="btnEdit" runat="server" Text="Simpan Edit" OnClick="EditData" Visible="false" />
                                            </td>
                                            <td align="right" style="width: 209px;">
                                                <asp:TextBox ID="txtReceiptID" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    &nbsp;&nbsp;<b>List Item</b>
                                    <div style="width: 100%; height: 200px; overflow: auto; border: 3px solid #B0C4DE; background-color: White; padding: 8px">
                                        <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="PONo" HeaderText="No PO" />
                                                <asp:BoundField DataField="SPPNo" HeaderText="No SPP" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                                <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                                <asp:BoundField DataField="KadarAir" HeaderText="Kadar Air" />
                                                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                        <table width="100%" style="border-collapse: collapse; font-size: x-small">
                                            <thead>
                                                <tr class="tbHeader tengah">
                                                    <th class="kotak" style="width: 4%">No.</th>
                                                    <th class="kotak" style="width: 8%">No.PO</th>
                                                    <th class="kotak" style="width: 8%">No.SPP</th>
                                                    <th class="kotak" style="width: 10%">ItemCode</th>
                                                    <th class="kotak" style="width: 20%">ItemName</th>
                                                    <th class="kotak" style="width: 10%">Jumlah</th>
                                                    <th class="kotak" style="width: 5%">Unit</th>
                                                    <th class="kotak" style="width: 8%">Kd Air(%)</th>
                                                    <th class="kotak" style="width: 15%">Keterangan</th>
                                                    <th class="kotak" style="width: 5%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstRMS" runat="server" OnItemDataBound="lstRMS_DataBound" OnItemCommand="lstRMS_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="baris EvenRows">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah"><%# Eval("PONo") %></td>
                                                            <td class="kotak tengah"><%# Eval("SPPNo") %></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                            <td class="kotak" nowrap="nowrap"><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtQty" runat="server" Text='<%# Eval("Quantity","{0:N2}") %>'></asp:Label>&nbsp;</td>
                                                            <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                                            <td class="kotak angka"><%# Eval("KadarAir") %>&nbsp;</td>
                                                            <td class="kotak "><%# Eval("Keterangan") %></td>
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                <asp:ImageButton ID="DelNew" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Container.ItemIndex %>' CommandName="deln" />
                                                                <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" />
                                                                <asp:ImageButton ID="lstLock" ImageUrl="~/images/lock_closed.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="unlock" />
                                                                <asp:ImageButton ID="lstUnLock" ImageUrl="~/images/lock_open.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="lock" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="baris OddRows">
                                                            <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                            <td class="kotak tengah"><%# Eval("PONo") %></td>
                                                            <td class="kotak tengah"><%# Eval("SPPNo") %></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                            <td class="kotak " nowrap="nowrap"><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka">
                                                                <asp:Label ID="txtQty" runat="server" Text='<%# Eval("Quantity","{0:N2}") %>'></asp:Label>&nbsp;</td>
                                                            <td class="kotak tengah"><%# Eval("UOMCode")%></td>
                                                            <td class="kotak angka"><%# Eval("KadarAir") %>&nbsp;</td>
                                                            <td class="kotak "><%# Eval("Keterangan") %></td>
                                                            <td class="kotak tengah">
                                                                <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit" />
                                                                <asp:ImageButton ID="DelNew" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Container.ItemIndex %>' CommandName="deln" />
                                                                <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
                                                                <asp:ImageButton ID="lstLock" ImageUrl="~/images/lock_closed.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="unlock" />
                                                                <asp:ImageButton ID="lstUnLock" ImageUrl="~/images/lock_open.png" runat="server" CommandArgument='<%# Eval("ReceiptID") %>' CommandName="lock" />
                                                            </td>
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

            <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
            <cc1:ModalPopupExtender ID="mpePopUp" runat="server"
                TargetControlID="lblHidden"
                PopupControlID="panEdit"
                CancelControlID="btnUpdateClose"
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>


            <asp:Panel ID="panEdit" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                <table style="table-layout: fixed; height: 100%" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px" bgcolor="gray">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:Label ID="Label3" runat="server" Text="ALASAN BATAL" Font-Bold="True" Font-Names="Verdana"
                                                Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                        </td>

                                        <td style="width: 37px">
                                            <input id="btnUpdateClose" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdateAlasan" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan_ServerClick" />
                                        </td>
                                        <td style="width: 5px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="100%" style="width: 100%">
                                <div style="overflow: hidden; height: 100%; width: 100%;" class="content">
                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                        <tr>
                                            <td>
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="height: 6px; width: 100%;" valign="top">
                                                <asp:TextBox ID="txtAlasanCancel" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
