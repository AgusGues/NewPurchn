<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FCLokasi.aspx.cs" Inherits="GRCweb1.Modul.Factory.FCLokasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function confirmation() {
            if (confirm('Yakin mau hapus data ?')) {
                return true;
            } else {
                return false;
            }
        }
        //   function ConfirmIt() {

        //             var x = confirm("Do you Want to notify changes to User ??");

        //             var control = '<%=inpHide.ClientID%>';

        //             if (x == true) {

        //                 document.getElementById(control).value = "1";

        //             }

        //             else {

        //                 document.getElementById(control).value = "0";

        //             }

        //         }
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader" style="width: 622px">
                                    <tr>
                                        <td style="width: 97%">
                                            <strong>&nbsp;MASTER LOKASI</strong></td>
                                        <td style="width: 103%"></td>
                                        <td style="width: 37px">
                                            <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                        <td style="width: 75px">
                                            <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Simpan" onserverclick="btnUpdate_ServerClick"
                                                disabled="disabled" /></td>
                                        <td style="width: 5px">
                                            <asp:Button ID="btnDelete" runat="server" Enabled="False" Height="22px"
                                                OnClick="Button1_Click" OnClientClick="return confirmation();" Text="Delete" />
                                        </td>
                                        <td style="width: 70px">&nbsp;</td>
                                        <td style="width: 70px">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="98px" Height="17px">
                                                <asp:ListItem Value="lokasi">Lokasi</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td style="width: 70px">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="98px"></asp:TextBox></td>
                                        <td style="width: 70px">
                                            <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr height="100%">
                            <td height="100%" style="width: 100%">
                                <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                    cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 76px; font-size: x-small;" valign="top">&nbsp; Proses</td>
                                        <td style="width: 204px; height: 6px" valign="top">
                                            <asp:DropDownList ID="ddlProses" runat="server" AutoPostBack="True"
                                                Height="17px" OnSelectedIndexChanged="ddlProses_SelectedIndexChanged"
                                                Width="104px">
                                                <asp:ListItem Value="1">Tahap 1</asp:ListItem>
                                                <asp:ListItem Value="2">Transit</asp:ListItem>
                                                <asp:ListItem Value="3">Tahap 3</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 169px; height: 6px" valign="top" align="right">ID
                                                        <asp:TextBox ID="txtID" runat="server" BorderStyle="Groove" ReadOnly="True"
                                                            Width="101px"></asp:TextBox>
                                        </td>
                                        <td style="height: 6px; width: 209px;" valign="top">&nbsp;&nbsp;<input id="inpHide" runat="server" style="visibility: hidden" />
                                        </td>
                                        <td style="width: 205px; height: 6px" valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 76px; font-size: x-small;" valign="top">
                                            <span style="font-size: 10pt">&nbsp;Nama Lokasi</span></td>
                                        <td style="width: 204px; height: 6px" valign="top">
                                            <asp:TextBox ID="txtLokasi" runat="server" BorderStyle="Groove" Width="101px"></asp:TextBox>
                                        </td>
                                        <td style="height: 6px; width: 169px;" valign="top">&nbsp;</td>
                                        <td style="width: 209px; height: 6px" valign="top"></td>
                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 6px" valign="top" align="center" bgcolor="#CCCCCC"
                                            colspan="2">
                                            <span style="font-size: 10pt">&nbsp; List Lokasi</span></td>
                                        <td style="height: 6px; width: 169px;" valign="top">&nbsp;</td>
                                        <td style="width: 209px; height: 6px" valign="top"></td>
                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 76px; height: 19px">&nbsp;</td>
                                        <td style="width: 204px; height: 19px;">
                                            <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Vertical">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnRowCommand="GridView1_RowCommand" Width="100%" AllowPaging="True"
                                                    PageSize="24">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                                        <asp:BoundField DataField="lokasi" HeaderText="Lokasi" />
                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                    </Columns>
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove"
                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small"
                                                        ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 169px; height: 19px">&nbsp;</td>
                                        <td style="width: 209px; height: 19px">&nbsp;</td>
                                        <td style="width: 205px; height: 19px">&nbsp;</td>
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
