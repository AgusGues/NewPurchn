<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormProcessConvert.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormProcessConvert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .page-content {
            width:100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }
        label,td,span{font-size:12px;}
        /*table,tr,td{background-color: #fff;}*/
    </style>
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

    <script language="JavaScript" type="text/javascript">
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel()
        { }

        function Cetak() {
            var wn = window.showModalDialog("../Report/Report.aspx?IdReport=SlipMRS", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        function confirm_delete() {
            if (confirm("Anda yakin untuk Cancel ?") == true)
                window.showModalDialog('../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel" ConfirmOnFormSubmit="false" />
            <div id="Div1" runat="server">
                <table style="table-layout: fixed;width:100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;PROSES RE-PACK</strong>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold;
                                                font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnList" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="List Table" onserverclick="btnList_ServerClick" />
                                        </td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                        </td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                        <td style="width: 3px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <div style="width: 100%; background-color: ActiveCaption;">
                                    <table class="tblForm" id="Table4" style="width: 100%;">
                                        <tr>
                                            <td style="width: 148px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 182px; height: 3px" valign="top">
                                            </td>
                                            <td style="height: 3px; width: 155px;" valign="top">
                                            </td>
                                            <td style="width: 209px; height: 3px" valign="top">
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 3px" valign="top">
                                                <span style="font-size: 10pt">&nbsp; No Proses</span>
                                            </td>
                                            <td style="width: 182px; height: 3px" valign="top">
                                                <asp:TextBox ID="txtProsesNo" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="height: 3px; width: 155px;" valign="top">
                                                &nbsp; No Tabel Re-Pack&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 3px" valign="top">
                                                <asp:TextBox ID="txtConversiNo" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 3px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 6px" valign="top">
                                                &nbsp; Dari Kode Barang&nbsp;
                                            </td>
                                            <td style="width: 182px; height: 6px" valign="top">
                                                <asp:DropDownList ID="ddlFromItemCode" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlFromItemCode_SelectedIndexChanged" Width="233px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 6px; width: 155px;" valign="top">
                                                &nbsp; Ke Kode Barang
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                <asp:DropDownList ID="ddlToItemCode" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlToItemCode_SelectedIndexChanged" Width="233px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 169px; height: 19px" valign="top">
                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 6px" valign="top" colspan="2">
                                                <asp:TextBox ID="txtFromItemName" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="380px"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="height: 6px" valign="top">
                                                <asp:TextBox ID="txtToItemName" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="380px"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 6px" valign="top">
                                                &nbsp; Dari Satuan&nbsp;
                                            </td>
                                            <td style="height: 6px; width: 182px;" valign="top">
                                                <asp:DropDownList ID="ddlFromUomCode" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlFromUomCode_SelectedIndexChanged" Width="233px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 6px; width: 155px;" valign="top">
                                                &nbsp; Ke Satuan&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                <asp:DropDownList ID="ddlToUomCode" runat="server" AutoPostBack="True" Height="16px"
                                                    OnSelectedIndexChanged="ddlToUomCode_SelectedIndexChanged" Width="233px" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 6px" valign="top">
                                                &nbsp; Dari Jumlah&nbsp;Tabel
                                            </td>
                                            <td style="height: 6px; width: 182px;" valign="top">
                                                <asp:TextBox ID="txtFromJumlah" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="height: 6px; width: 155px;" valign="top">
                                                &nbsp; Ke Jumlah&nbsp;Tabel
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                <asp:TextBox ID="txtToJumlah" runat="server" BorderStyle="Groove" Width="233" AutoPostBack="True"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 6px" valign="top">
                                                &nbsp; Dari Jumlah Proses&nbsp;
                                            </td>
                                            <td style="height: 6px; width: 182px;" valign="top">
                                                <asp:TextBox ID="txtFromJumlahProses" runat="server" BorderStyle="Groove" ReadOnly="False"
                                                    Width="233" AutoPostBack="True" OnTextChanged="txtFromJumlahProses_TextChanged"></asp:TextBox>
                                            </td>
                                            <td style="width: 155px; height: 19px" valign="top">
                                                &nbsp; Ke Jumlah Proses&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                <asp:TextBox ID="txtToJumlahProses" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 6px" valign="top">
                                                &nbsp; Dibuat Oleh&nbsp;
                                            </td>
                                            <td style="height: 6px; width: 182px;" valign="top">
                                                <asp:TextBox ID="txtCreatedBy" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 155px; height: 19px" valign="top">
                                                &nbsp; Tanggal&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 6px" valign="top">
                                                <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" BorderStyle="Groove"
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 6px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 19px">
                                                &nbsp;
                                            </td>
                                            <td rowspan="1" style="width: 182px; height: 19px;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 155px; height: 19px">
                                                &nbsp; Stok Akhir&nbsp;
                                            </td>
                                            <td style="width: 209px; height: 19px">
                                                <asp:TextBox ID="txtStok" runat="server" BorderStyle="Groove" ReadOnly="True" Width="233"></asp:TextBox>
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 148px; height: 19px">
                                            </td>
                                            <td rowspan="1" style="width: 182px; height: 19px">
                                            </td>
                                            <td style="width: 155px; height: 19px">
                                            </td>
                                            <td style="width: 209px; height: 19px" align="right">
                                                <asp:LinkButton ID="lbAddOP" runat="server" Font-Size="10pt" OnClick="lbAddOP_Click"
                                                    Visible="False">Tambah</asp:LinkButton>
                                            </td>
                                            <td style="width: 205px; height: 19px">
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="div2" style="width: 100%; height: 320px; overflow: auto; border: 2px solid ActiveCaption;
                                        background-color: White; padding: 8px">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                            OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="RepackNo" HeaderText="No" />
                                                <asp:BoundField DataField="FromItemCode" HeaderText="Dari Kd Brg" />
                                                <asp:BoundField DataField="FromQty" HeaderText="Dari Jumlah" />
                                                <asp:BoundField DataField="FromUomCode" HeaderText="Dari Satuan" />
                                                <asp:BoundField DataField="ToItemCode" HeaderText="Ke Kd Brg" />
                                                <asp:BoundField DataField="ToQty" HeaderText="Ke Jumlah" />
                                                <asp:BoundField DataField="ToUomCode" HeaderText="Ke Satuan" />
                                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                            </Columns>
                                            <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                            <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
