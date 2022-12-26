<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterUserRoles.aspx.cs" Inherits="GRCweb1.Modul.Master.MasterUserRoles" %>

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
            <div id="Div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>

                        <tr>
                            <td style="width: 100%;">
                                
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 218%">
                                                            <strong>USER ROLES</strong></td>
                                                        <td style="width: 37px">
                                                            <input id="btnNew" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                                        <td style="width: 75px">
                                                            <input id="btnUpdate" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" /></td>
                                                        <td style="width: 5px"></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                                <asp:ListItem Value="UserID">User ID</asp:ListItem>
                                                                <asp:ListItem Value="UserName">Nama User</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                                                        <td style="width: 3px">
                                                            <input id="btnSearch" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;"
                                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                                                    </tr>
                                                </table>
                                            
                            </td>
                        </tr>
                        <tr height="100%">
                            <td height="100%" style="width: 100%">
                                <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
                                    <tbody>
                                        <tr >
                                            <td>
                                               
                                                                    <div>
                                                                        <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                                                            cellpadding="0" border="0">
                                                                            <tr>
                                                                                <td style="width: 197px; height: 3px" valign="top"></td>
                                                                                <td style="width: 204px; height: 3px" valign="top"></td>
                                                                                <td style="height: 3px; width: 169px;" valign="top"></td>
                                                                                <td style="width: 209px; height: 3px" valign="top"></td>
                                                                                <td style="width: 205px; height: 3px" valign="top"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 197px; height: 6px" valign="top">
                                                                                    <span style="font-size: 10pt">&nbsp; ID User</span></td>
                                                                                <td style="width: 204px; height: 6px" valign="top">
                                                                                    <asp:TextBox ID="txtUserID" runat="server" BorderStyle="Groove"
                                                                                        Width="233" ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="height: 6px; width: 169px;" valign="top"></td>
                                                                                <td style="width: 209px; height: 6px" valign="top"></td>
                                                                                <td style="width: 205px; height: 6px" valign="top"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 197px; height: 19px">
                                                                                    <span style="font-size: 10pt">&nbsp; Nama User</span>
                                                                                </td>
                                                                                <td style="width: 204px; height: 6px" valign="top">
                                                                                    <asp:TextBox ID="txtUserName" runat="server" BorderStyle="Groove"
                                                                                        Width="233" ReadOnly="True"></asp:TextBox></td>
                                                                                <td style="width: 169px; height: 19px"></td>
                                                                                <td style="width: 209px; height: 19px"></td>
                                                                                <td style="width: 205px; height: 19px"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <table id="Table2" style="left: 0px; top: 0px; width: 95%;" cellspacing="1"
                                                                        cellpadding="0" border="0" height="165">
                                                                        <tr>
                                                                            <td style="height: 3px; width: 203px;" valign="top" colspan="1"></td>
                                                                            <td style="height: 3px" valign="top" colspan="1">
                                                                                <span style="font-size: 10pt">&nbsp; <strong>List</strong></span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 203px; height: 100%" valign="top">&nbsp; &nbsp;
                                                                            </td>
                                                                            <td style="width: 100%; height: 100%" valign="top">
                                                                                <div id="div2" style="width: 770px; height: 400px; overflow: auto">
                                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Pilih">
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" Enabled="true" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="RolesName" HeaderText="Nama Role" />
                                                                                        </Columns>
                                                                                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                                                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                                                        <PagerStyle BorderStyle="Solid" />
                                                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                           
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
