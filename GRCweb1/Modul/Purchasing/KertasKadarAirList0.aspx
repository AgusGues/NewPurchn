<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KertasKadarAirList0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KertasKadarAirList0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 1px 1px 1px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}.btn-info{border-radius: 4px;}
    hr {margin-top: 4px;margin-bottom: 4px;border: 0;border-top: 1px solid #000;}
</style>
<script type="text/javascript">
    function CetakFrom(docno) {
        params = 'width=1024px';
        params += ', height=600px';
        params += ', top=20px, left=20px';
        params += ',scrollbars=1';
        window.open("../../ModalDialog/FromKadarAirQA.aspx?ka=" + docno, "Preview", params);
    }
    function confirmation() {
        if (confirm('Yakin mau hapus data ?')) {
            return true;
        } else {
            return false;
        }
    }
    function OpenModal() {
        $("#MyPopup").modal("show");
    }
</script>
<asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>List Kadar Air Kertas</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnBack" runat="server" Text="Form Input" OnClick="btnBack_Click" />
                    <asp:Button class="btn btn-info" ID="btnApproval" runat="server" Text="Approved" OnClick="btnApproval_Click" />
                    <asp:Label ID="lblFind" runat="server" Text="Find by Nopol "></asp:Label>
                    <asp:TextBox ID="txtCari" runat="server" Width="250px"></asp:TextBox>
                    <asp:Button class="btn btn-info" ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Periode</div>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBulan" runat="server" width="150"></asp:DropDownList>
                                <asp:DropDownList ID="ddlTahun" runat="server" width="150"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">LokasiQa</div>
                            <div class="col-md-8">
                            <asp:DropDownList class="form-control" width="100%" ID="ddlPlant" runat="server"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Button class="btn btn-info" ID="btnView" runat="server" Text="Preview" OnClick="btnView_Click" />
                                <asp:HiddenField ID="txtID" runat="server" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <div class="contentlist" style="height: 400px" id="lst" runat="server" >
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" style="width: 4%">No
                                    </th>
                                    <th class="kotak" style="width: 8%">Doc. No
                                    </th>
                                    <th class="kotak" style="width: 8%">Tanggal
                                    </th>
                                    <th class="kotak" style="width: 12%">Supplier Name
                                    </th>
                                    <th class="kotak" style="width: 8%">No. Mobil
                                    </th>
                                    <th class="kotak" style="width: 15%">ItemName
                                    </th>
                                    <th class="kotak" style="width: 5%">BK
                                    </th>
                                    <th class="kotak" style="width: 5%">KA(%)
                                    </th>
                                    <th class="kotak" style="width: 4%">Sampah(%)
                                    </th>
                                    <th class="kotak" style="width: 5%">BB 20%</th>
                                    <th class="kotak" style="width: 5%">Jml BAL
                                    </th>
                                    <th class="kotak" style="width: 5%">Status</th>
                                    <th class="kotak" style="width: 5%">Keterangan</th>
                                    <th class="kotak" style="width: 4%">&nbsp
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstKA" runat="server" OnItemDataBound="lstKA_DataBound" OnItemCommand="lstKA_Command">
                                <ItemTemplate>
                                    <tr class="EvenRows baris" id="lstx" runat="server">
                                        <td class="kotak tengah">
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                            <asp:CheckBox ID="chk" runat="server" Visible="true" ToolTip='<%# Eval("ID") %>' />
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("DocNo") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("TglCheck","{0:d}") %>
                                        </td>
                                        <td class="kotak" style="white-space: nowrap">
                                            <%# Eval("SupplierName").ToString().ToUpper() %>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("NOPOL") %>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("ItemName") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("GrossPlant","{0:N0}") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("AvgKA","{0:N2}") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("Sampah","{0:N2}") %>
                                        </td>
                                        <td class="kotak angka">
                                            <%# Eval("NettPlant", "{0:N0}")%>
                                        </td>
                                        <td class="kotak tengah">
                                            <%# Eval("JmlBAL","{0:N0}") %><asp:Label ID="sts" runat="server" Text='<%# Eval("Keputusan") %>'
                                            Visible="false"></asp:Label>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("status") %>
                                        </td>
                                        <td class="kotak">
                                            <%# Eval("alasan") %>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="btnPrev" runat="server" CommandArgument='<%=Eval("DocNo %>'
                                            ToolTip="Click For Preview Detail" CommandName="prev" ImageUrl="~/images/clipboard_16.png" />
                                            <asp:ImageButton ID="btnApp" runat="server" CommandArgument='<%=Eval("DocNo %>' ToolTip="Click For Preview Detail"
                                            CommandName="prev" ImageUrl="~/images/Approved_16.png" />
                                            <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("DocNo") %>'
                                            ToolTip='<%# Eval("DocNo") %>' CommandName="prn" ImageUrl="~/images/printer_small.png" />
                                            <asp:ImageButton ID="btnPO" runat="server" CommandArgument='<%# Eval("DocNo") %>'
                                            ToolTip='Create PO' CommandName="po" ImageUrl="~/images/editor.png" Visible="false" />
                                            <asp:ImageButton ID="btndel"  OnClientClick ="return confirmation();"  runat="server" ToolTip='Hapus data' ImageUrl="~/images/trash.gif" 
                                            CommandArgument='<%# Eval("ID") %>' CommandName="hapus" />
                                            <asp:Panel ID="Panel1" runat="server" Visible="False" BackColor="White">
                                            Alasan Hapus
                                            <asp:TextBox ID="txtalasan" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnSimpan" runat="server" Text="Simpan" CommandArgument='<%# Eval("ID") %>' CommandName="simpan" />
                                            <asp:Button ID="btnBatal" runat="server" Text="Cancel" CommandArgument='<%# Eval("ID") %>' CommandName="batal" /></asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate></asp:Repeater>
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
