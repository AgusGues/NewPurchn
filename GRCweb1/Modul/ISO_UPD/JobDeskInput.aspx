<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JobDeskInput.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.JobDeskInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="div1" runat="server" class="table-responsive" style="width:100%">
            <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                <tr>
                    <td style="width: 100%; height: 49px">
                        <table class="nbTableHeader" style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <b>&nbsp;INPUT JOBDESC </b>
                                </td>
                                <td style="width: 50%; padding-right: 10px" align="right">
                                    <%--<asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" />--%>
                                    <asp:Button id="btnBaru" runat="server" Text="Baru" OnClick="btnBaru_ServerClick" />
                                    <asp:Button id="btnLaporan" runat="server" Text="Laporan UPD JobDesk" OnClick="btnLaporan_ServerClick" Visible="false" />
                                    <asp:Button id="btnPrint" runat="server" Text="Cetak" OnClick="btnPrint_ServerClick" />
                                    <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="false" />
                                    <asp:Button id="btnList" runat="server" Text="List" OnClick="btnList_ServerClick" />
                                    <%--<asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                    <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                    <asp:HiddenField ID="appLevele" runat="server" />
                                    <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />--%>
                                    <asp:HiddenField ID="ID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="content" style="background:#fff;">
                            <table id="criteria" runat="server" style="border-collapse: collapse;">
                                <%--<tr>
                                    <td>
                                        <b>JOB DESCRIPTION</b>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>Departemen</td> 
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtDept" runat="server" Width="100%" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                            <asp:TextBox ID="txtID" runat="server" BorderStyle="Groove" Width="25" ReadOnly="True" Visible="false"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td>Nama Jabatan</td> 
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlBagian" runat="server">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBagian" runat="server" Width="100%" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama Jabatan Atasan Langsung</td> 
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtAtasan" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama Jabatan Bawahan Langsung</td> 
                                    <td>:</td>
                                    <td>
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                        <asp:Repeater ID="lstBawahan" runat="server">
                                            <ItemTemplate>
                                                <tr class="OddRows baris" style="font-weight: bold">
                                                    <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                        <span class="angka"><%# Container.ItemIndex+1 %></span>
                                                    </td>
                                                    <td class="kotak tengah" style="width: 30%">
                                                        <asp:TextBox ID="txtBawahan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Bawahan")%>'
                                                            Width="100%"></asp:TextBox>
                                                        <asp:TextBox ID="IDBawahan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                    </td>
                                                    
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tanggal disusun</td> 
                                    <td>:</td>
                                    <td>
                                        <%--<bdp:bdplite id="txtTglSusun" runat="server" cssclass="style2" tooltip="klik icon untuk merubah tanggal"
                                        width="95%" style="margin-left: 86px">
                                        </bdp:bdplite>--%>
                                        <asp:TextBox ID="txtTglSusun" runat="server" AutoPostBack="False" BorderStyle="Groove"
                                            ReadOnly="False" Width="233"></asp:TextBox>
                                    </td>
                                    <td style="width: 205px; height: 12px" valign="top">
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtTglSusun">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Revisi</td> 
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtRevisi" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <div class="contentlist" id="dvScroll" runat="server" style="height:288px; overflow:auto">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0" id="baList">
                                        <tbody>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">I. Tujuan Umum Jabatan</span>
                                                        </td>
                                                    </tr>
                                                     <asp:Repeater ID="lstTUJ" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtTUJabatan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TUJabatan")%>' TextMode="MultiLine"
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDTUJabatan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                     
                                                     <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">II. Tugas Pokok Jabatan</span>
                                                        </td>
                                                    </tr>
                                                     <asp:Repeater ID="lstTPJ" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtTPJabatan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TPJabatan")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDTPJabatan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    
                                                    <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">III. Hubungan Kerja</span>
                                                        </td>
                                                    </tr>
                                                     <asp:Repeater ID="lstHK" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtHK" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("HubunganKerja")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDHK" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                     
                                                     <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">IV. Tanggung Jawab</span>
                                                        </td>
                                                    </tr>
                                                     <asp:Repeater ID="lstTJ" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtTanggungJawab" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("TanggungJawab")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDTanggungJawab" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                     
                                                     <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">V. Wewenang</span>
                                                        </td>
                                                    </tr>
                                                     <asp:Repeater ID="lstWewenang" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtWewenang" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Wewenang")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDWewenang" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                     
                                                     <tr class="OddRows baris">
                                                        <td class="kotak" nowrap="nowrap" colspan="10" style="width: 5%; font-size: small;
                                                            font-weight: bolder;" bgcolor="#fff">
                                                            <span class="kotak">VI. Kualifikasi Jabatan</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td style="font-size: small">Pendidikan</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstPend" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtPendidikan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Pendidikan")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDPendidikan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Pengalaman</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstPengalaman" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtPengalaman" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Pengalaman")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDPengalaman" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Usia</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstUsia" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtUsia" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Usia")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDUsia" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Pengetahuan</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstPengetahuan" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtPengetahuan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Pengetahuan")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDPengetahuan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Keterampilan</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstKeterampilan" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtKeterampilan" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Keterampilan")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDKeterampilan" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Fisik</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstFisik" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtFisik" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("Fisik")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDFisik" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                                    <tr>
                                                    <td style="font-size: small">Non Fisik</td>
                                                    </tr>
                                                     <asp:Repeater ID="lstNonFisik" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="OddRows baris" style="font-weight: bold">
                                                                <td class="kotak tengah" nowrap="nowrap" style="width: 5%">
                                                                    <span class="angka">
                                                                        <%# Container.ItemIndex+1 %></span>
                                                                </td>
                                                                <td class="kotak tengah" style="width: 30%">
                                                                    <asp:TextBox ID="txtNonFisik" CssClass="txtongrid" runat="server" Visible="true" Text='<%# Eval("NonFisik")%>'
                                                                        Width="100%"></asp:TextBox>
                                                                    <asp:TextBox ID="IDNonFisik" CssClass="txtongrid" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                     </asp:Repeater>
                                        </tbody>
                                    </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>

</asp:UpdatePanel>
</asp:Content>
