<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalPONew2.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.ApprovalPONew2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 24px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script type="text/javascript">
    function onKeyUp() {
        $("#<%=txtNotApproved.ClientID %>").keyup(function () {
            if ($(this).val().length == 2) {
                $("#<%=btnNotApprove.ClientID %>").removeAttr("disabled");
                $("#<%=btnApprove.ClientID %>").attr("disabled", "true");
            }
            if ($(this).val().length == 0) {
                $("#<%=btnNotApprove.ClientID %>").attr("disabled", "false");
                $("#<%=btnApprove.ClientID %>").removeAttr("disabled");
            }
        });
    }
</script>
<script type="text/javascript">
    if (!window.showModalDialog) {
        window.showModalDialog = function (arg1, arg2, arg3) {
            var w;
            var h;
            var resizable = "no";
            var scroll = "no";
            var status = "no";
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
<script type="text/javascript">
    function show_history(id, itm) {
        window.showModalDialog('../../ModalDialog/ShowPOHistory.aspx?txt=' + id + '&itm=' + itm, '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1024px;scrollbars=yes');
        return false;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server" class="table-responsive" style="width: 100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approval Po</span>
                <div class="pull-right">
                    <asp:TextBox ID="txtLapakID" runat="server" Visible="false">></asp:TextBox>
                    <asp:TextBox ID="txtKirimanID" runat="server" Visible="false"></asp:TextBox>
                    <asp:Button class="btn btn-info" ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" OnClick="btnPrev_Click" />
                    <asp:Button class="btn btn-info" ID="btnApprove" runat="server" Text="Approved" OnClick="btnApprove_Click" />
                    <asp:Button class="btn btn-info" ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_Click" />
                    <asp:Button class="btn btn-info" ID="btnNotApprove" runat="server" Text="Not Approved" Enabled="false"
                    OnClick="btnNotApprove_Click" />
                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="List PO" Enabled="true" OnClick="btnList_ServerClick" />
                    &nbsp;
                    <asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor PO" onfocus="if(this.value==this.defaultValue)this.value='';"
                    onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by Nomor PO"></asp:TextBox>
                    <asp:Button class="btn btn-info" ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                    <asp:HiddenField ID="noPO" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tgl.Po</div>
                            <div class="col-md-8">
                            <asp:TextBox class="form-control" ID="txtTglPO" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Supplier</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtSupplier" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TermOfPayment</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTOP" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Ppn</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPPN" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">MataUang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlMUID" runat="server"> </asp:DropDownList>
                                &nbsp;
                                <asp:Label ID="txtKurs" runat="server"></asp:Label>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Discount</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDiscount" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TotalPrice</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTotalPrice" ReadOnly="true" runat="server"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">AlasanNotApprove</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" TextMode="MultiLine" Rows="3" ID="txtNotApproved" runat="server"
                                AutoPostBack="false" OnTextChanged="txtNotApproved_Change"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">KodePo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoPO" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">UpSupplier</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUpSupplier" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TermOfDelivery</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTOD" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Pph</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPPH" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">UangMuka</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUangMuka" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="txtFrom" runat="server"></asp:Label>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Remarks</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtRemarks" ReadOnly="true" runat="server" Height="52px"
                                TextMode="MultiLine"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">OngkosKirim</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtOngkosKirim" ReadOnly="true" runat="server"></asp:TextBox>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">ApprovalStatus</div>
                            <div class="col-md-8">
                                <asp:Label class="form-control" ID="txtStatus" runat="server"></asp:Label>
                                <div style="padding:2px"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Term Of Payment<br />
                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" PageSize="15"
                    Width="100%">
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                    Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="white" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" />  </asp:GridView> </asp:Panel>

                    <div class="contentlist" style="height: 250px">
                        <table class="tbStandart">
                            <thead>
                                <tr class="tbHeader">
                                    <th style="width: 3%" class="kotak">No.  </th>
                                    <th style="width: 5%" class="kotak">No.SPP   </th>
                                    <th style="width: 6%" class="kotak">Item Code    </th>
                                    <th style="width: 30%" class="kotak">Item Name    </th>
                                    <th style="width: 4%" class="kotak">Unit    </th>
                                    <th style="width: 5%" class="kotak">Quantity     </th>
                                    <th style="width: 6%" class="kotak">Price    </th>
                                    <th style="width: 8%" class="kotak">Total Price      </th>
                                    <th style="width: 6%" class="kotak">Deliv Date       </th>
                                    <th style="width: 3%" class="kotak">&nbsp;   </th>
                                    <th style="width: 3%" class="kotak">&nbsp;    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstItemPO" runat="server" OnItemCommand="lstPO_ItemCommand" OnItemDataBound="lstItemPO_DataBound">
                                <ItemTemplate>
                                    <tr class="EvenRows baris">
                                        <td class="kotak tengah">
                                            <%# Container.ItemIndex+1 %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("DocumentNo") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("ItemCode") %>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("ItemName") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("UOMCode") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("Qty","{0:N2}") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("Price","{0:N2}") %>
                                        </td>
                                        <td class="kotak angka">
                                            <asp:Label ID="tHarga" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak tengah" id="dlv">
                                            <%# Eval("DlvDate","{0:d}") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="showHis" runat="server" ImageUrl="~/images/dtpJavaScript.gif"
                                            CommandArgument='<%# Eval("ItemName") %>' CommandName='
                                            <%# Eval("ItemTypeID") %>'
                                            ToolTip="Click For Show History & Stock Item" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="5" rowspan="6">
                                            <asp:Label ID="pointLapak" runat="server" Visible="false"><b>Informasi Supplier Kertas</b>
                                            <table style="border-collapse: collapse; width: 85%; font-size: x-small">
                                                <tr>
                                                    <td style="width: 20%">Group
                                                    </td>
                                                    <td style="width: 1%" class="tengah">:
                                                    </td>
                                                    <td style="width: 80%">
                                                        <asp:Label ID="txtGroup" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Agen
                                                    </td>
                                                    <td class="tengah">:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="txtAgen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Point
                                                    </td>
                                                    <td class="tengah ">:
                                                    </td>
                                                    <td class="angka">
                                                        <asp:Label ID="txtTotalPoint" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Tambahan Poin PO Ini
                                                    </td>
                                                    <td class="tengah">:
                                                    </td>
                                                    <td class="angka">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table></asp:Label>
                                        </td>
                                        <td class="kotak Line3 angka" colspan="2">Total
                                        </td>
                                        <td class="kotak angka Line3">
                                            <asp:Label ID="gTotal" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3" colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="bDisc" runat="server">
                                        <td class="kotak Line3 angka" colspan="2">Diskon
                                            <asp:Label ID="dsk" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3 angka">
                                            <asp:Label ID="gDisc" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3" colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="tbDisc" runat="server">
                                        <td class="kotak Line3 angka" colspan="2">Total setelah Diskon
                                        </td>
                                        <td class="kotak Line3 angka">
                                            <asp:Label ID="tDisc" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3" colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="bPPN" runat="server">
                                        <td class="kotak Line3 angka" colspan="2">PPN 11%
                                        </td>
                                        <td class="kotak Line3 angka">
                                            <asp:Label ID="gPPN" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3" colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="kotak Line3 angka" colspan="2">Grand Total
                                        </td>
                                        <td class="kotak angka Line3">
                                            <asp:Label ID="grnTotal" runat="server"></asp:Label>
                                        </td>
                                        <td class="kotak Line3" colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                </FooterTemplate> </asp:Repeater>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
