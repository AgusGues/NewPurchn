<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListUPDJobDesk.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.ListUPDJobDesk" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openWindow() {
            window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
        }
     
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" width="100%">
                <table style="table-layout: fixed" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;&bull;&nbsp;List UPD JobDesk</strong>
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Form JOBDESK" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                <asp:ListItem Value="ScheduleNo">No JOBDESK</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 3px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                        <td style="width:5px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <%--<hr />
                    <span style="font-size: 10pt">&nbsp; <strong>List</strong></span> --%>
                <hr />
                <div id="div2" style="width: 100%; height: 450px; overflow: auto; padding: 10px; padding-left: 15px;
                    background-color: #fff">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="TglSusun" HeaderText="Tanggal" />
                            <asp:BoundField DataField="DeptID" HeaderText="Dept" />
                            <asp:BoundField DataField="BagianName" HeaderText="Jabatan" />
                            <%--<asp:BoundField DataField="TUJabatan" HeaderText="Tujuan Umum Jabatan" />
                            <asp:BoundField DataField="TPJabatan" HeaderText="Tugas Pokok Jabatan" />
                            <asp:BoundField DataField="HubunganKerja" HeaderText="Hubungan Kerja" />
                            <asp:BoundField DataField="TanggungJawab" HeaderText="Tanggung Jawab" />
                            <asp:BoundField DataField="Wewenang" HeaderText="Wewenang" />
                            <asp:BoundField DataField="Pengetahuan" HeaderText="Pengetahuan" />
                            <asp:BoundField DataField="Bawahan" HeaderText="Nama Jabatan Bawahan Langsung" />--%>
                            <asp:BoundField DataField="Approval" HeaderText="Status" />
                            <asp:BoundField DataField="AlasanCancel" HeaderText="Alasan UnApproved" />
                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
