<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HargaSupplier.aspx.cs" Inherits="GRCweb1.Modul.Master.HargaSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="JavaScript">
        function openWindow() {
            window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
        }

        function fnOrderView(OrderNo) {

            var windowFeatures = 'width=850px,height=650px,left=50,top:10';
            window.open("OrderView.aspx?OrderNo=" + OrderNo, "_blank", windowFeatures);

        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table \style="background-color: #C0C0C0">
                                    <tr>
                                        <td style="width: 218%">
                                            <strong>Setting HargaSupplier</strong>
                                        </td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                        </td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                        </td>
                                        <td style="width: 5px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    <table id="Table4" cellspacing="1" cellpadding="0" border="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                Cari nama Supplier<asp:TextBox ID="txtSearch1" runat="server"></asp:TextBox>
                                                <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                            </td>
                                            <td>Supplier :
                                                <asp:Label ID="lblSupplier" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="div4" style="overflow: auto; height: 500px;">

                                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowCommand="GridView2_RowCommand" Width="100%" Height="500px">
                                                        <Columns>
                                                            <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                                            <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" />
                                                            <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" />
                                                            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
                                                            <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                        </Columns>
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                            <td>
                                                <div id="div5" style="overflow: auto;height: 500px;">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" Height="300px">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Pilih">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="IdHarga" HeaderText="IdHarga" />
                                                            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                                                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                                                            <asp:BoundField DataField="Harga" HeaderText="Harga" />
                                                        </Columns>
                                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                                        <PagerStyle BorderStyle="Solid" />
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>

                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
