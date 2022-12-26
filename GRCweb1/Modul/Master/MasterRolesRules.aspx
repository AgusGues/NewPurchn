<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterRolesRules.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterRolesRules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
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
                <table style=" height="100%" cellspacing="0" cellpadding="0"width="100%">
                    <tbody>

                        <tr>
                            <td style="width: 100%;">

                                <table class="nbTableHeader">
                                    <tr>
                                        <td style="width: 218%">
                                            <strong>ROLE RULES</strong></td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" /></td>
                                        <td style="width: 5px"></td>
                                        <td></td>
                                        <td>
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                                        <td style="width: 3px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr >
                            <td >

                                <div style="overflow: auto; height: 100%; width: 100%;">
                                    
                                                <table  id="Table4"  cellspacing="1"
                                                    cellpadding="0" border="0">
                                                    <tr>
                                                        <td ></td>
                                                        <td ></td>
                                                        <td></td>
                                                        <td ></td>
                                                        <td ></td>
                                                    </tr>
                                                    <tr>
                                                        <td ></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlNamaRole" runat="server" Width="232px"
                                                                OnSelectedIndexChanged="ddlNamaRole_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>

                                                        </td>
                                                        <td ></td>
                                                        <td></td>
                                                        <td ></td>
                                                    </tr>
                                                </table>
                                                <hr width="100%" size="1">
                                            
                                    
                                                <div id="div2" style=" height: 400px; overflow: auto">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Pilih">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="RuleName" HeaderText="Nama Rule" />
                                                            <asp:BoundField DataField="idname" HeaderText="ID Name" />
                                                        </Columns>
                                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
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
