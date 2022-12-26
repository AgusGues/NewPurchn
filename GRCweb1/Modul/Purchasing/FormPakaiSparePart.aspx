<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiSparePart.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiSparePart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }

        label, td, span {
            font-size: 12px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="javaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel() { }

        function Cetak() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipPakaiPart", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        //function confirm_delete() {
        //    if (confirm("Anda yakin untuk Cancel ?") == true)
        //        window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        //    else
        //        return false;
        //}
        function infoupdate() {
            window.showModalDialog('../ModalDialog/InfoUpdate.aspx', '', 'resizable:yes;dialogheight: 500px; dialogWidth: 750px;scrollbars=yes; toolbar:no');
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed; height: 100%" width="100%" style="background: #fff;">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <!-- header panel--->
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 50%; padding-left: 5px">
                                            <strong>&nbsp;Spare Part Drawing Slip</strong>
                                        </td>
                                        <td style="width: 50%; padding-right: 5px;">
                                            <input id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                            <input id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_ServerClick" />
                                            <input id="btnPrint" onclick="Cetak()" runat="server" type="button" value="Cetak" />
                                            <input id="btnList" runat="server" type="button" value="List" onserverclick="btnList_ServerClick" />
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">SPDS No</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                            <input id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- end of header panel-->
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <!--panel form inputan-->
                                <div style="height: 100%" class="content" style="background: #fff;">
                                    <table style="border-collapse: collapse; width: 100%; margin-top: 5px; font-size: x-small" border="0">
                                        <tr>
                                            <td style="width: 128px; height: 17px;" valign="top"></td>
                                            <td style="width: 200px; height: 17px;" valign="top"></td>
                                            <td style="width: 85px; height: 17px;" valign="top"></td>
                                            <td class="kotak line3 tengah" style="width: 100px; height: 17px;" valign="top">Available</td>
                                            <td class="kotak line3 tengah" style="width: 100px; height: 17px;" valign="top">Blocked</td>
                                            <td style="width: 200px; padding-top: 5px" valign="top" rowspan="7">
                                                <div class="contentlist" id="showBudget" runat="server" visible="false" style="background: #fff;">
                                                    <table class="tbStandart">
                                                        <tr class="Line3">
                                                            <td colspan="2" class=" kotak"><b>Info Budget </b></td>
                                                        </tr>
                                                        <tr class="EvenRows baris" style="display: none">
                                                            <td class="angka kotak" style="width: 60%">Periode</td>
                                                            <td class="kotak tengah" style="width: 40%">
                                                                <asp:Label ID="lblPeriode" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="OddRows baris">
                                                            <td class="angka kotak">Total Budget</td>
                                                            <td class="angka kotak">
                                                                <asp:Label ID="lblBudget" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="OddRows baris" id="addbdg" runat="server">
                                                            <td class="angka kotak" style="white-space: nowrap">Budget Tambahan</td>
                                                            <td class="angka kotak">
                                                                <asp:Label ID="lbladdBudget" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="EvenRows baris">
                                                            <td class="angka kotak">Total SPB</td>
                                                            <td class="angka kotak">
                                                                <asp:Label ID="lblSPB" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="OddRows baris">
                                                            <td class="angka kotak">Sisa Budget</td>
                                                            <td class="angka kotak">
                                                                <asp:Label ID="lblSisa" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr class="EvenRows baris">
                                                            <td class="angka kotak">Tipe Budget</td>
                                                            <td class="kotak">
                                                                <asp:Label ID="lbltBudget" runat="server"></asp:Label></td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><span>&nbsp; No. SPDS</span></td>
                                            <td>
                                                <asp:TextBox ID="txtPakaiNo" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"></asp:TextBox></td>
                                            <td class="kotak line3"><span>&nbsp; Stock</span></td>
                                            <td class="kotak tengah" style="width: 100px;">
                                                <asp:TextBox ID="txtStok" runat="server" Width="100%" Enabled="False" Font-Bold="True" ReadOnly="True" CssClass="angka"></asp:TextBox>
                                            </td>
                                            <td class="kotak tengah" style="width: 100px;">
                                                <asp:TextBox ID="txtPending" runat="server" Width="100%" Enabled="False" Font-Bold="True" ReadOnly="True" CssClass="angka"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 128px; font-size: 12px;" valign="top">&nbsp; Nama Dept&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" Width="233px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtKodeDept" runat="server" AutoPostBack="True" Visible="false" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">&nbsp; Tanggal&nbsp;
                                            </td>
                                            <td style="width: 209px;" valign="top" colspan="2">
                                                <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" OnTextChanged="txtTanggal_TextChanged"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                                                    runat="server"></cc1:CalendarExtender>
                                            </td>
                                            <%--<td style="width: 169px;" valign="top">
                                                
                                            </td>--%>
                                        </tr>
                                        <tr id="listProject" runat="server" visible="false">
                                            <td style="width: 128px;">
                                                <span id="prj" runat="server">&nbsp;&nbsp;Project Name</span>&nbsp;</td>
                                            <td style="" colspan="2">
                                                <asp:TextBox ID="txtNomor" runat="server" AutoPostBack="true"
                                                    OnTextChanged="txtNomor_Change" Width="50%"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="autocpl" runat="server" CompletionSetCount="10"
                                                    EnableCaching="true" FirstRowSelected="true" MinimumPrefixLength="2"
                                                    ServiceMethod="GetListProjectNo" ServicePath="AutoComplete.asmx"
                                                    TargetControlID="txtNomor">
                                                </cc1:AutoCompleteExtender>
                                                <asp:DropDownList ID="ddlProjectName" runat="server" Height="16px"
                                                    Visible="false" Width="50%" CssClass="gambar" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlProjectName_Change">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px; font-size: 12px;" valign="top">&nbsp; Cari Nama Brg&nbsp;
                                            </td>
                                            <td colspan="2" valign="top">
                                                <asp:TextBox ID="txtCariNamaBrg" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="90%"></asp:TextBox>
                                            </td>
                                            <td colspan="2">&nbsp;&nbsp;<asp:CheckBox ID="stk" runat="server" Text="Stocked" Checked="true" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;" valign="top">
                                                <span>&nbsp; Nama Barang</span>
                                            </td>
                                            <td style="" valign="top" colspan="2">
                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" Width="90%">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px; font-size: 12px;" valign="top">&nbsp; Kode Barang&nbsp;
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" BorderStyle="Groove" Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">
                                                <span>&nbsp; Qty Pakai</span>
                                                <asp:Label ID="bdgQty" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 209px;" valign="top" colspan="2">
                                                <asp:TextBox ID="txtQtyPakai" runat="server" BorderStyle="Groove" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px; font-size: 12px;" valign="top">
                                                <span style="font-size: 10pt">&nbsp; Keterangan</span>
                                            </td>
                                            <td style="width: 204px;" valign="top">
                                                <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove"
                                                    Width="349px"></asp:TextBox>
                                            </td>
                                            <td style="width: 102px;" valign="top">&nbsp; Satuan&nbsp;
                                            </td>
                                            <td style="width: 209px;" valign="top" colspan="2">
                                                <asp:DropDownList ID="txtUomID" runat="server">
                                                </asp:DropDownList>
                                                <asp:TextBox Visible="false" ID="txtUom" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 128px; font-size: 12px; height: 30px;">&nbsp; SP Group
                                            </td>
                                            <td rowspan="1" style="width: 205px; height: 30px;">
                                                <asp:DropDownList ID="spGroup" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="spGroup_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 102px; font-size: 12px; height: 30px;">&nbsp;<span id="x1" runat="server"> No. Kendaraan</span>
                                            </td>
                                            <td colspan="3" style="height: 30px">
                                                <span id="x2" runat="server">
                                                    <asp:DropDownList ID="ddlNoPolisi" runat="server" Width="180px"
                                                        OnSelectedIndexChanged="ddlNoPolisi_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>&nbsp<asp:CheckBox ID="CheckBox1" runat="server" Text="Plant" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                                </span>

                                            </td>

                                        </tr>
                                        <tr style="font-size: x-small;" id="spZona" runat="server" visible="false">
                                            <td>&nbsp; Zona</td>
                                            <td>
                                                <asp:DropDownList ID="ddlZona" runat="server"></asp:DropDownList></td>
                                            <td style="width: 102px;">&nbsp;&nbsp;<span id="frk" runat="server" style="font-size: 10pt">&nbsp;</span>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlForklif" runat="server" Visible="false" CssClass="gambar">
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;">
                                                <span id="prjx" runat="server" visible="false">&nbsp;&nbsp;Project Name</span>&nbsp;</td>
                                            <td style="width: 204px;">
                                                <asp:DropDownList ID="ddlProjectNamex" runat="server" Height="16px" Visible="false" Width="342px" CssClass="gambar"></asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td style="width: 205px;">

                                                <asp:TextBox ID="txtPakaiTipe" runat="server" AutoPostBack="True"
                                                    BorderStyle="Groove" ReadOnly="True" Style="margin-top: 0px" Visible="False"
                                                    Width="52px"></asp:TextBox>
                                                <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True"
                                                    BorderStyle="Groove" ReadOnly="True" Style="margin-top: 0px" Visible="false"
                                                    Width="52px"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 128px;">&nbsp;</td>
                                            <td rowspan="1" style="width: 204px;">
                                                <asp:Panel ID="PanelPalet" runat="server" Visible="False">
                                                    <asp:RadioButton ID="RbPPacking" runat="server" AutoPostBack="True"
                                                        Checked="True" GroupName="p" Text="Palet Kirim" Visible="True" />
                                                    &nbsp;
                                                <asp:RadioButton ID="RbPStock" runat="server" AutoPostBack="True" GroupName="p"
                                                    Text="Palet Stock" Visible="True" />
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 102px;"></td>
                                            <td style="width: 209px;" colspan="2">
                                                <asp:Button ID="lbAddOP" runat="server" OnClick="lbAddOP_Click" Text="Tambah" />
                                                <%--<asp:LinkButton ID="lbAddOP" runat="server" Font-Size="10pt" OnClick="lbAddOP_Click">Tambah</asp:LinkButton>--%>
                                            </td>
                                            <td style="width: 205px;">
                                                <asp:TextBox ID="txtAvgPrice" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- end of panel form inputan-->
                                    <!--grid view
                                    <b>&nbsp;List Pemakaian</b>-->
                                    <div id="div2" class="contentlist" style="width: 100%; height: 200px; padding: 5px; overflow: auto; background-color: White; border: 2px solid #B0C4DE">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="98%"
                                            OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanging="GridView1_SelectedIndexChanged"
                                            OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                                <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="false" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                    <!--grid view-->
                                </div>
                                <div id="LstSarmut" visible="false" style="width: 90%">
                                    <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove"
                                        ReadOnly="True" Visible="false" Width="150px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>

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

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
