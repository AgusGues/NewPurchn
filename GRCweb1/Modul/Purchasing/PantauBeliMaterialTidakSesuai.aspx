<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PantauBeliMaterialTidakSesuai.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.PantauBeliMaterialTidakSesuai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>


        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>

        <style type="text/css">
            /* style css untuk row on hover color datagrid */
            .normal {
                background-color:;
            }

            .highlight {
                background-color: pink;
            }
            /* #cccccc */

            .sembunyi {
                display: none;
            }
        </style>

        <script type="text/javascript">
            function konfirmasi_simpan() {
                var txtSuratJalan = document.getElementById("<%=this.txtSuratJalan.ClientID %>").value;
            var ddlNoPO = document.getElementById("<%=this.ddlNoPO.ClientID %>").value;
            var ddlBarang = document.getElementById("<%=this.ddlBarang.ClientID %>").value;
            if (ddlNoPO == "") {
                alert("Nomor PO tidak diketahui, periksa nomor PO");
                return false;
            } else if (txtSuratJalan == "") {
                alert("Surat jalan tidak boleh kosong");
                document.getElementById("<%=this.txtSuratJalan.ClientID %>").focus();
                return false;
            } else if (ddlBarang == "-") {
                alert("Tentukan keterangan (Diterima/ Ditolak)");
                return false;
            } else {
                if (confirm('Yakin data yg akan anda input sudah benar ?\nklik OK untuk simpan data')) {
                    return true; //submit
                } else {
                    return false;
                }
            }
            }
        </script>

    </head>

    <body class="no-skin">

        <%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                LEMBAR PEMANTAUAN PEMBELIAN MATERIAL KETIDAKSESUAIAN PERSYARATAN PERUSAHAAN
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

                            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="height: 49px">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 100%">&nbsp;</td>
                                                        <td style="width: 37px">&nbsp;</td>
                                                        <td style="width: 75px">&nbsp;</td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                        <td style="width: 5px">&nbsp;</td>
                                                        <td style="width: 70px">&nbsp;</td>
                                                        <td style="width: 70px">&nbsp;</td>
                                                        <td style="width: 70px">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        
                                                        <td style="width: 37px">
                                                            <asp:Button ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_Click" Style="background-color: white; font-weight: bold; font-size: 11px;" />
                                                        </td>
                                                        <td style="width: 75px">
                                                            <asp:Button ID="btnSimpan" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                Text="Simpan" OnClick="btnSimpan_Click"
                                                                OnClientClick="return konfirmasi_simpan();" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            <asp:Button ID="btnUpdate1" runat="server" Text="Update" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                Enabled="false" OnClick="btnUpdate1_Click" />
                                                        </td>
                                                        <td style="width: 5px">
                                                            <asp:Button ID="btnDelete" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;"
                                                                Text="Hapus" Enabled="false" />
                                                        </td>
                                                        <td style="width: 70px">
                                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                                <asp:ListItem Value="GantiNo">No Ganti</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 70px">
                                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 70px">
                                                            <input id="btnSearch" runat="server"
                                                                style="background-image: url(../images/Button_Back.gif); background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Cari" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="content">


                                                    <table style="font-family: Calibri; font-size: 12px;" runat="server" id="TabelInputForm">
                                                        <tr>
                                                            <td>Tanggal Kejadian</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTgl" runat="server"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTgl" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Supplier</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSupplier" runat="server"
                                                                    OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>No. PO</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlNoPO" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlNoPO_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Material&nbsp;</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBahanBaku" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>No. Surat Jalan</td>
                                                            <td>
                                                                <asp:TextBox ID="txtSuratJalan" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Keterangan</td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBarang" runat="server" ClientIDMode="Static">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnReset" runat="server" CausesValidation="false" OnClientClick="this.form.reset(); return false;" Text="Reset" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Alasan</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlasan" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtKunci" runat="server"></asp:TextBox>
                                                                <asp:TextBox ID="txtIdApprove" runat="server"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <div align="center" id="div_approval" style="width: 100%; background-color: White;" runat="server">
                                                        <br />
                                                        <table width="90%">
                                                            <tr>
                                                                <td width="45%">List Approval </td>

                                                                <td align="right">
                                                                    <asp:Button ID="btnApprove1" runat="server" OnClick="btnApprove1_Click" Text="Approve" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Button ID="simpan" runat="server" OnClick="Simpan_Click" Text="Simpan" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <asp:GridView Width="90%" ID="GridView2" runat="server" ClientIDMode="Static"
                                                            AutoGenerateColumns="False" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound">

                                                            <Columns>

                                                                <asp:TemplateField HeaderText="No">
                                                                    <ItemTemplate>
                                                                        <span><%#Container.DataItemIndex + 1%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                <asp:BoundField DataField="TglKejadian" HeaderText="Tgl Kejadian" DataFormatString="{0:d}" />
                                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                                                <asp:BoundField DataField="ItemName" HeaderText="Material" />
                                                                <asp:BoundField DataField="NoPo" HeaderText="No. PO" />
                                                                <asp:BoundField DataField="NoSuratJalan" HeaderText="No. Surat Jalan" />
                                                                <asp:BoundField DataField="KetTdkSesuaiBrg" HeaderText="Keterangan" />
                                                                <asp:BoundField DataField="Alasan" HeaderText="Alasan" />
                                                                <asp:BoundField DataField="CreatedTime" HeaderText="Tgl Input" />
                                                                <asp:BoundField DataField="CreatedBy" HeaderText="Input By" />


                                                                <asp:TemplateField HeaderText="Tindakan Purchasing">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTindakanPurchasing" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:Label ID="lblTindakanPurchasing" runat="server" Text='<%# Eval("TindakanPurchasing")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Klarifikasi Supplier">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtKlarifikasiSupplier" runat="server" TextMode="MultiLine" Text=""></asp:TextBox>
                                                                        <asp:Label ID="lblKlarifikasiSupplier" runat="server" Text='<%# Eval("KlarifikasiSupplier")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pilih">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkRow" runat="server" Enabled="true" OnCheckedChanged="chkRowSelected_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>

                                                            <RowStyle Font-Names="tahoma" Font-Size="X-Small" />
                                                            <HeaderStyle Font-Names="tahoma" Font-Size="X-Small" Font-Bold="True" BackColor="Yellow" />
                                                            <PagerStyle BorderStyle="Dotted" />
                                                            <%-- zebra table <AlternatingRowStyle BackColor="Gainsboro" /> --%>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>

                                                    <hr>
                                                    <div class="contentlist" style="height: 350px">


                                                        <br />




                                                        <table>
                                                            <tr>
                                                                <td>Kejadian Bulan</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlBulanKejadian" runat="server">
                                                                    </asp:DropDownList>
                                                                    <td>Sampai</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlBulanKejadian2" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>Tahun</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlTahunKejadian" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="Button1" runat="server" Text="View" OnClick="Button1_Click" />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnExportExcel" runat="server" EnableViewState="False"
                                                                        OnClick="btnExportExcel_Click" Text="Export to Excel" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />


                                                        <div align="center" id="div2" style="width: 100%; height: 320px; overflow: auto">
                                                            <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="False" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" AllowPaging="true"
                                                                OnPageIndexChanging="OnPaging">

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <span><%#Container.DataItemIndex + 1%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                    <asp:BoundField DataField="TglKejadian" HeaderText="Tgl Kejadian" DataFormatString="{0:d}" />
                                                                    <asp:BoundField DataField="IdSupplier" HeaderText="IdSupplier" />
                                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                                                                    <asp:BoundField DataField="IdMaterial" HeaderText="IdMaterial" />
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Material" />
                                                                    <asp:BoundField DataField="NoPo" HeaderText="No. PO" />
                                                                    <asp:BoundField DataField="NoSuratJalan" HeaderText="No. Surat Jalan" />
                                                                    <asp:BoundField DataField="KetTdkSesuaiBrg" HeaderText="Keterangan" />
                                                                    <asp:BoundField DataField="Alasan" HeaderText="Alasan" />
                                                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="ApprovalStatus" />
                                                                    <asp:BoundField DataField="NextApprover" HeaderText="NextApprover" />
                                                                    <asp:BoundField DataField="NextApprover1" HeaderText="Approval Status" />

                                                                    <asp:BoundField DataField="CreatedBy" HeaderText="Input By" />
                                                                    <asp:BoundField DataField="CreatedTime" HeaderText="Tgl Input" />

                                                                    <asp:BoundField DataField="TindakanPurchasing" HeaderText="Tindakan Purchasing" />
                                                                    <asp:BoundField DataField="KlarifikasiSupplier" HeaderText="Klarifikasi Supplier" />

                                                                    <%-- <asp:ButtonField CommandName="pilihRow" Text=" Edit " HeaderText="Aksi" />  --%>

                                                                    <asp:TemplateField HeaderText="Aksi">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnLinkEdit" runat="server" Text="Edit" CommandName="pilihRow" CommandArgument="<%# Container.DataItemIndex %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>

                                                                <RowStyle Font-Names="tahoma" Font-Size="X-Small" />
                                                                <HeaderStyle Font-Names="tahoma" Font-Size="X-Small" Font-Bold="True" BackColor="RoyalBlue" ForeColor="Gold" />
                                                                <PagerStyle BorderStyle="Dotted" />
                                                                <%-- zebra table <AlternatingRowStyle BackColor="Gainsboro" /> --%>
                                                            </asp:GridView>

                                                        </div>

                                                        <asp:GridView ID="GridView3" runat="server" ClientIDMode="Static" AllowPaging="true"
                                                            OnPageIndexChanging="OnPaging" AutoGenerateColumns="false" CssClass="sembunyi" OnRowDataBound="GridView3_RowDataBound">

                                                            <Columns>
                                                                <asp:TemplateField>

                                                                    <HeaderTemplate>
                                                                        <tr style="background-color: #e6f3ff;">
                                                                            <th rowspan="2">No</th>
                                                                            <th rowspan="2">Tgl</th>
                                                                            <th rowspan="2">Nama Supplier</th>
                                                                            <th rowspan="2">Nama Bahan<br>
                                                                                Baku/Penunjang/Bakar</th>
                                                                            <th rowspan="2">No. PO</th>
                                                                            <th rowspan="2">No. Surat Jalan</th>
                                                                            <th colspan="2">Ketidaksesuaian Barang</th>
                                                                            <th rowspan="2">Alasan</th>
                                                                            <th rowspan="2">Tindakan Purchasing</th>
                                                                            <th rowspan="2">Klarifikasi Supplier</th>
                                                                        </tr>
                                                                        <tr style="background-color: #e6f3ff;">
                                                                            <td style="font-weight: bold; text-align: center;">Ditolak</td>
                                                                            <td style="font-weight: bold; text-align: center;">Diterima</td>
                                                                        </tr>
                                                                    </HeaderTemplate>

                                                                    <ItemTemplate>

                                                                        <%#Container.DataItemIndex + 1%>
                                                                        <td><%# Eval("TglKejadian","{0:d}")%></td>
                                                                        <td><%# Eval("SupplierName")%></td>
                                                                        <td><%# Eval("ItemName")%></td>
                                                                        <td><%# Eval("NoPo")%></td>
                                                                        <td><%# Eval("NoSuratJalan")%></td>
                                                                        <td style="text-align: center;"><%# Eval("Ditolak")%></td>
                                                                        <td style="text-align: center;"><%# Eval("Diterima")%></td>
                                                                        <td><%# Eval("Alasan")%></td>
                                                                        <td><%# Eval("TindakanPurchasing")%></td>
                                                                        <td><%# Eval("KlarifikasiSupplier")%></td>

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                            </Columns>


           
                                                        </asp:GridView>



                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>



                        </div>

                        <script src="../../assets/jquery.js" type="text/javascript"></script>
                        <script src="../../assets/js/jquery-ui.min.js"></script>
                        <script src="../../assets/select2.js"></script>
                        <script src="../../assets/datatable.js"></script>
                        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
                        </body>
    </html>

                        <%--source html ditutup di sini--%>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
