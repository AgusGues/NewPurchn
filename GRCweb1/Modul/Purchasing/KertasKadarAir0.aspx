<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KertasKadarAir0.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.KertasKadarAir0" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    function GetKey(source, eventArgs) {
        $('#<%=txtSupplierID.ClientID %>').val(eventArgs.get_value());
    }
</script>
<script type="text/javascript">
    function SetFocus() {
        document.getElementById('<%=txtTusuk1.ClientID %>').focus();
        return false;
    }
    function CetakFrom(docno) {
        params = 'width=1024px';
        params += ', heigh=600px'
        params += ', top=20px, left=20px'
        window.open("../../ModalDialog/FromKadarAirQA.aspx?ka=" + docno, "Preview", params);
    }

</script>
<script type="text/javascript">
    function MyPopUpWin(url, width, height) {
        var leftPosition, topPosition;
        leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
        topPosition = (window.screen.height / 2) - ((height / 2) + 50);
        window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
    }
    function confirm_revisi() {
        if (confirm("Anda yakin Pengiriman ini mau di tolak ?") == true)
            MyPopUpWin('../../ModalDialog/ReasonCancel.aspx?IdJudul=Revisi', 800, 300);
        else
            return false;
    }
</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:Label ID="lblHidden" runat="server" Text="-"></asp:Label>
    <cc1:ModalPopupExtender ID="mpePopUp" runat="server" TargetControlID="lblHidden" PopupControlID="divPopUp" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <div id="divPopUp" class="tengah " style="background-color: #00FFFF">
        <div id="Header" class="header" style="font-weight: bold">Input Alasan Tolak</div>
        <div id="buttons">
            <div id="DivbtnOK" class="buttonOK">
                &nbsp;&nbsp;Alasan &nbsp;&nbsp;
                <asp:TextBox ID="txtAlasanTolak" runat="server" CssClass="txtUpper" AutoPostBack="false" TextMode="MultiLine" Width="457px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btnOk" runat="server" Text="Update" OnClick="btnOk_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />&nbsp;&nbsp;
                <br />
            </div>
            <div>
                <asp:Label ID="Label1" runat="server" Text="-"></asp:Label>
            </div>
        </div>
    </div>
    <div id="div1" runat="server">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Input Kadar Air Kertas</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" />
                    <asp:Button class="btn btn-info" ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                    <asp:Button class="btn btn-info" ID="btnCetak" runat="server" Text="Cetak" Visible="true" OnClick="btnCetak_Click" />
                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="List Kadar Air" OnClick="btnList_Clik" />
                    <asp:HiddenField ID="__LASTFOCUS" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Tanggal</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTanggal" runat="server" AutoPostBack="True"
                                OnTextChanged="txtTanggal_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="ca1" runat="server" Format="dd-MMM-yyyy"
                                TargetControlID="txtTanggal"></cc1:CalendarExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">DocumentNo</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDocNo" runat="server" Width="100%"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">JenisBarang</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlNamaBarang" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlNamaBarang_Change"></asp:DropDownList>
                                <cc1:AutoCompleteExtender ID="act1" runat="server" TargetControlID="txtSupplier" ServiceMethod="GetSupplierKertas"
                                ServicePath="AutoComplete.asmx" MinimumPrefixLength="2"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionInterval="100" EnableCaching="true"
                                OnClientItemSelected="GetKey"></cc1:AutoCompleteExtender>
                                <asp:HiddenField ID="txtSupplierID" runat="server" Value="0" OnValueChanged="txtSupplier_Change" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Supplier</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtSupplier" runat="server" Width="100%" AutoPostBack="true"></asp:TextBox>
                                <asp:HiddenField ID="txtItemCode" runat="server" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">PlatMobil</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" Width="100%" ID="txtNOPOL" runat="server" CssClass="txtUpper" OnTextChanged="txtNOPOL_Change" AutoPostBack="true"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="mmsk" runat="server" MaskType="none" ClearMaskOnLostFocus="false" TargetControlID="txtNOPOL" Mask="$$-9999-$$$"></cc1:MaskedEditExtender>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">SuratJalanDepo</div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlSJDepo" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlSJDepo_Change"></asp:DropDownList>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">HasilTimbangan(Kg)</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtJmlTimbangan" runat="server" AutoPostBack="false" OnTextChanged="txtJmlTimbangan_Change"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">JumlahBall</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtjmlBal" runat="server" AutoPostBack="True" OnTextChanged="txtjmlBal_TextChanged"></asp:TextBox>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">UploadDataScaner</div>
                            <div class="col-md-8">
                                <asp:FileUpload class="form-control" ID="Upload1" runat="server" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Button class="btn btn-info" ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                        <asp:Button class="btn btn-info" ID="btnTolak" runat="server" Text="Tolak" OnClick="btnTolak_Click" />
                        <div style="padding:2px;"></div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-6">
                        <div class="table-responsive">
                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="1">
                                <thead>
                                    <tr class="tbHeader">
                                        <th colspan="6" class="tengah kotak">Hasil Tusukan</th>
                                    </tr>
                                    <tr class="tbHeader">
                                        <th style="width: 12%" class="kotak">Bal Ke</th>
                                        <th style="width: 12%" class="kotak">No Bal</th>
                                        <th style="width: 12%" class="kotak">Titik 1</th>
                                        <th style="width: 12%" class="kotak">Titik 2</th>
                                        <th style="width: 15%" class="kotak">Rata-rata KA(%)</th>
                                        <th style="width: 5%" class="kotak">Test</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="OddRows baris">
                                        <td class="kotak tengah">
                                            <asp:HiddenField ID="txtID" runat="server" />
                                            <asp:TextBox ID="txtBalke" runat="server" ReadOnly="true" class="txtongrid tengah" Width="100%" Text="0"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtBall" runat="server" class="tengah" Width="100%" AutoPostBack="false"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtTusuk1" runat="server" class="tengah" Width="100%" AutoPostBack="false" OnTextChanged="txtTusuk1_Change"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtTusuk2" runat="server" class="tengah" Width="100%" AutoPostBack="true" OnTextChanged="txtTusuk2_Change"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:TextBox ID="txtAvgKA" runat="server" class="txtongrid tengah" AutoPostBack="false" OnTextChanged="txtAvgKA_Change" Width="100%"></asp:TextBox>
                                        </td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton ID="btnAdd" runat="server" ToolTip="Simpan Data" OnClick="btnAdd_Click" ImageUrl="~/images/Save_blue.png" OnClientClick="return SetFocus()" />
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="lstKA" runat="server" OnItemCommand="lstKA_Command" OnItemDataBound="lstKA_DataBound">
                                    <ItemTemplate>
                                        <tr class="EvenRows baris" id="trs" runat="server">
                                            <td class="kotak tengah"><%# Eval("BALKe") %></td>
                                            <td class="kotak tengah"><%# Eval("NoBall") %></td>
                                            <td class="kotak tengah"><%# Eval("Tusuk1") %></td>
                                            <td class="kotak tengah"><%# Eval("Tusuk2") %></td>
                                            <td class="kotak tengah"><%# Eval("AvgTusuk") %></td>
                                            <td class="kotak tengah">
                                                <asp:ImageButton ID="btndel" runat="server" ToolTip='<%# Container.ItemIndex %>' ImageUrl="~/images/trash.gif" CommandArgument='<%# Container.ItemIndex %>' CommandName="hapus" />
                                                <asp:ImageButton ID="btnEdt" runat="server" ToolTip="Edit Raw" ImageUrl="~/images/folder.gif" CommandArgument='<%# Container.ItemIndex %>' CommandName="edit" />
                                            </td>
                                        </tr>
                                    </ItemTemplate></asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div style="padding:2px;"></div>
                    </div>
                    <div class="col-md-6 table-responsive">
                        <label style="background-color: #337ab7;width: 100%;color:#fff;">&nbsp;Perhitungan Berat Bersih</label>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">BeratKotor</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtBK" runat="server" ReadOnly="true" width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">StdKadarAir</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="stKA" runat="server" ReadOnly="true" width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row" style="display: none">
                                    <div class="col-md-4">Jum.Sample</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtJmlSample" runat="server" width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row" style="display: none">
                                    <div class="col-md-4">Jum.SampleBasah</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtJmlSampleBasah" runat="server" width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row" style="display: none">
                                    <div class="col-md-4">Pros.SampleBasah</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtProsSB" runat="server" width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">Rata2KadarAir(%)</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtAvgKadarAir" runat="server" width="100%"></asp:TextBox>
                                        <asp:TextBox ID="txtSampah" runat="server" OnTextChanged="txtSampah_Change"
                                        AutoPostBack="true" Visible="False">0</asp:TextBox>
                                        <asp:TextBox ID="txtberatDiPotong" runat="server"
                                        ToolTip="Berat Kotor x Pros. Sample Basah (%)" Visible="False"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">BeratSample</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtBeratSample" runat="server" Width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">BeratSampah</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtBeratSampah" runat="server" Width="100%" OnTextChanged="txtBS_Change" AutoPostBack="true"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">Persentase(%)</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtprosSampah" runat="server" Width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">BeratPotongan</div>
                                    <div class="col-md-8">
                                        <asp:Label ID="LabelBP" runat="server"></asp:Label>
                                        <asp:TextBox class="form-control" ID="txtBeratPotongan" runat="server" width="100%"
                                        ToolTip="Berat Kotor x ((Rata2 Kadar Air - StdKA)+ Sampah))"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">BeratBersih</div>
                                    <div class="col-md-8">
                                        <asp:Label ID="LabelBB" runat="server"></asp:Label>
                                        <asp:TextBox class="form-control" ID="txtBeratBersih" runat="server" width="100%"
                                        ToolTip="Berat Kotor - Berat Potongan"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">BeratPotongan(%)</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtBeratPotongan0" runat="server"
                                        ToolTip="Berat Kotor x ((Rata2 Kadar Air - StdKA)+ Sampah))" Width="100%"></asp:TextBox>
                                        <div style="padding:2px;"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">BeratBersih0%</div>
                                    <div class="col-md-8">
                                        <asp:TextBox class="form-control" ID="txtBeratBersih0" runat="server" 
                                        ToolTip="Berat Kotor - Berat Potongan" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-md-12">
                                <b>Keputusan Akhir</b>
                                <asp:RadioButton GroupName="kpts" ID="chkKA" value="1" Checked="true"
                                runat="server" Text="Diterima (Berat Bersih)" Enabled="False" />
                                <asp:RadioButton GroupName="kpts" ID="chkKA1" value="2" runat="server"
                                Text="Diterima Setelah Sortir" Enabled="False" />
                                <asp:RadioButton GroupName="kpts" ID="chkKA2" value="-1" runat="server"
                                Text="Tidak Di terima" Enabled="False" />
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel3" runat="server">
                        <hr>
                        <div class="row">
                            <div class="col-md-12">
                                Total kirim periode
                                <asp:Label ID="LPeriode1" runat="server"></asp:Label>
                                &nbsp;s/d
                                <asp:Label ID="LPeriode2" runat="server"></asp:Label>
                                &nbsp;=
                                <asp:Label ID="LTotalKirim" runat="server" Text="0"></asp:Label>
                                &nbsp;Kg.
                                <div style="padding:2px;"></div>
                            </div>
                            <div class="col-md-12">
                                Minimal toleransi kadar air 30% =
                                <asp:Label ID="LTotalKirim0" runat="server" Text="0"></asp:Label>
                                &nbsp;Kg.
                                <div style="padding:2px;"></div>
                            </div>
                            <div class="col-md-12">
                                Maksimal toleransi kadar air 30% =
                                <asp:Label ID="LTotalKirim1" runat="server" Text="0"></asp:Label>
                                &nbsp;Kg.
                                <div style="padding:2px;"></div>
                            </div>
                            <div class="col-md-12">
                                Akumulasi toleransi kadar air 30% bulan ini =
                                <asp:Label ID="LTotalKirim2" runat="server" Text="0"></asp:Label>
                                &nbsp;Kg.
                            </div>
                        </div></asp:Panel>
                        <hr>
                        <div class="row">
                            <div class="col-md-12">
                                <span style="color:blue;">
                                    (*) Hasil tusukan berdasarkan angka yang muncul pada Moisture Tester dengan pengambilan sample 25% - 30% dari jumlah total bal dalam 1x pengiriman
                                </span>
                                <div style="padding:2px;"></div>
                            </div>
                        </div>
                        <div style="padding: 2px;"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="lst" runat="server" AutoGenerateColumns="true"
                            Visible="false"></asp:GridView>
                        </div>
                        <div style="padding: 2px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="btnUpload" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
