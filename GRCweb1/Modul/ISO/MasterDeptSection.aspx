<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterDeptSection.aspx.cs" Inherits="GRCweb1.Modul.ISO.MasterDeptSection" %>
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
        table,tr,td{background-color: #fff;}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="table-layout: fixed; width: 100%; height: 100%" border="0">
                <tr>
                    <td colspan="5" style="height: 49px">
                        <!--header tabel-->
                        <table class="nbTableHeader" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <strong>&nbsp;MASTER SECTION</strong>
                                </td>
                                <td style="width: 100%">
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
                                    <input id="btnDelete" runat="server" style="background-color: white; font-weight: bold;
                                        font-size: 11px;" type="button" value="Hapus" onserverclick="btnDelete_ServerClick" />
                                </td>
                                <td style="width: 70px">
                                    <%--<input id="btnPrint" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Cetak" onclick="Cetak()" />--%>
                                </td>
                                <td style="width: 70px">
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                        <asp:ListItem Value="BagianName">Section Name</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 70px">
                                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                    <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                        font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="5" valign="top" align="left">
                        <div id="div3" style="background-color: Scrollbar;">
                            <table class="tblForm" id="table4" style="left: 0px; top: 0px;" border="0">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        Prosentase Bobot PES
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="font-size: 10pt">&nbsp; Departemen</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                            Width="233px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 209px; height: 6px; font-size: x-small; background-color: #FEFFFF;"
                                        valign="top">
                                        KPI
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        <asp:TextBox ID="txtKPI" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        %</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp; Section Name</span>
                                    </td>
                                    <td rowspan="1">
                                        <asp:TextBox ID="txtISOBagian" runat="server" BorderStyle="Groove" Width="233" OnTextChanged="txtISOBagian_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        SOP
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        <asp:TextBox ID="txtSOP" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        %</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp; User Group</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="userGroup" runat="server" Enabled="false">
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        Task
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        <asp:TextBox ID="txtTask" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        %</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        Disiplin
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        <asp:TextBox ID="txtDisiplin" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        %</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        Total Bobot PES
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        <asp:TextBox ID="txtTotal" runat="server" ReadOnly="True" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="background-color: #FEFFFF">
                                        %</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="background-color: #FEFFFF">
                                        Mulai berlaku</td>
                                    <td colspan="2" style="background-color: #FEFFFF">
                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlBulan_Change">
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtTahun" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="BagianID" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="height: 19px;" colspan="3">
                                        <span id="errMsg" runat="server"></span>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div id="div5" style="background-color: Scrollbar; margin-left: 5px; padding: 10px;
                            border: 1px">
                            <span>LIST SECTION</span>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="GridView1_RowCommand" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                                PageSize="15">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                    <asp:BoundField DataField="BagianName" HeaderText="Nama Bagian" />
                                    <asp:BoundField DataField="DeptName" HeaderText="Nama Departemen" />
                                    <asp:BoundField DataField="UserGroupID" HeaderText="GroupID" Visible="true" />
                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                </Columns>
                                <RowStyle Font-Names="tahoma" Font-Size="Smaller" BackColor="WhiteSmoke" />
                                <HeaderStyle Font-Names="tahoma" Font-Size="Smaller" BackColor="RoyalBlue" BorderColor="#404040"
                                    BorderStyle="Groove" BorderWidth="2px" Font-Bold="true" ForeColor="Gold" />
                                <PagerStyle BorderStyle="Solid" />
                                <AlternatingRowStyle BackColor="Gainsboro" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr valign="top" style="border: 1px solid #0000">
                    <td colspan="5" valign="top" align="left">
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
