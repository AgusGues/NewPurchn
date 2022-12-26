<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormApprovalPES5R.aspx.cs" Inherits="GRCweb1.Modul.ISO.FormApprovalPES5R" %>

<%--taroh di setelah 1 baris pertama file--%>
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



    </head>

    <body class="no-skin">


        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                       APPROVAL <%=(Request.QueryString["p"]==null)?"SOP":Request.QueryString["p"].ToString() %>
                    </div>
                    <div style="padding: 2px"></div>
                    <%--copy source design di sini--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                        <table style="table-layout: fixed; height: 100%; width: 100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%; height: 49px">
                                        <table class="nbTableHeader">
                                            <tr>
                                                <td style="width: 100%">
                                                    
                                                </td>
                                                <td style="width: 75px">&nbsp;
                                                </td>
                                                <td style="width: 75px">
                                                    <input id="btnSebelumnya" runat="server" onserverclick="btnSebelumnya_ServerClick"
                                                        style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                                        value="Sebelumnya" />
                                                </td>
                                                <td style="width: 5px">
                                                    <input id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Approve" disabled="true" />
                                                </td>
                                                <td style="width: 5px">
                                                    <input id="btnUnUpdate" runat="server" onserverclick="btnUnUpdate_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="UnApprove" disabled="true" />
                                                </td>
                                                <td style="width: 5px">
                                                    <input id="btnSelanjutnya" runat="server" onserverclick="btnSesudahnya_ServerClick"
                                                        style="background-color: white; font-weight: bold; font-size: 11px;" type="button"
                                                        value="Selanjutnya" />
                                                </td>
                                                <td style="width: 70px">
                                                    <input id="btnList" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="List" onserverclick="btnList_ServerClick" />
                                                </td>
                                                <td style="width: 70px">
                                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                                        <asp:ListItem Value="SuratJalanNo">No SOP</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 70px">
                                                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                                </td>
                                                <td style="width: 70px">
                                                    <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                        type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 100%">
                                        <div class="content" style="display: none">
                                            <table class="tblForm" id="Table4" style="width: 100%; font-size: x-small; border-collapse: collapse; display: block">
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; SOP No
                                                    </td>
                                                    <td style="width: 196px;">
                                                        <asp:TextBox ID="txtTaskNo" runat="server" AutoPostBack="True" ReadOnly="True" Width="233px"></asp:TextBox>
                                                        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 3px; width: 154px;">&nbsp; PIC
                                                    </td>
                                                    <td style="width: 245px;">
                                                        <asp:TextBox ID="txtPic" runat="server" AutoPostBack="True" ReadOnly="True" Width="233px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; Department
                                                    </td>
                                                    <td style="height: 3px">
                                                        <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                            Width="233px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 3px; width: 154px;">&nbsp;&nbsp;Section
                                                    </td>
                                                    <td style="height: 3px">
                                                        <asp:DropDownList ID="ddlBagian" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBagian_SelectedIndexChanged"
                                                            Width="233px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; Periode
                                                    </td>
                                                    <td style="width: 196px;">
                                                        <asp:TextBox ID="txtTglMulai" runat="server" AutoPostBack="False" ReadOnly="False"
                                                            Width="233" Visible="false"></asp:TextBox>
                                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBulan_Change"></asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTahun_Change"></asp:DropDownList>
                                                    </td>
                                                    <td style="width: 154px;">&nbsp;
                                                    </td>
                                                    <td style="">&nbsp;
                                                    </td>
                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td style="width: 177px;">&nbsp; Category
                                                    </td>
                                                    <td style="" colspan="3">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="712px"></asp:DropDownList>
                                                    </td>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTglMulai"></cc1:CalendarExtender>
                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; Category SOP&nbsp;
                                                    </td>
                                                    <td style="" colspan="3">
                                                        <asp:TextBox ID="txtPenjelasanSOP" runat="server" Width="712px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 205px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; Bobot
                                                    </td>
                                                    <td style="">
                                                        <asp:TextBox ID="txtBobotNilai" runat="server" Width="233"></asp:TextBox>
                                                        <asp:Label ID="lbSatuan" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="">&nbsp; Target&nbsp;
                                                    </td>
                                                    <td style="">
                                                        <asp:TextBox ID="txtTarget" runat="server" ReadOnly="True" Width="233"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 205px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px;">&nbsp; Keterangan SOP
                                                    </td>
                                                    <td colspan="3" style="">
                                                        <asp:TextBox ID="txtKeterangan" runat="server" Width="712px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 177px; height: 6px">&nbsp; Input By&nbsp;
                                                    </td>
                                                    <td style="height: 6px;">
                                                        <asp:TextBox ID="txtPic0" runat="server" ReadOnly="True" Width="233px"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 6px; width: 154px;">&nbsp;
                                                    </td>
                                                    <td style="height: 6px;">&nbsp;
                                                    </td>
                                                </tr>

                                            </table>
                                            <hr />
                                            <div id="div3" class="contentlist" style="height: 320px; overflow: auto">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    AllowPaging="true" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    OnRowDataBound="GridView1_RowDataBound" Visible="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="IdDetail" HeaderText="IDd">
                                                            <ItemStyle Width="30px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NewSop" HeaderText="SOP Description">
                                                            <ItemStyle Width="250px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="bobotNilai" HeaderText="Bobot">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="KetTargetKe" HeaderText="Target">
                                                            <ItemStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PointNilai" HeaderText="Point Nilai">
                                                            <ItemStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                                            <ItemStyle Width="50px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PIC" HeaderText="PIC">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                                                        BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                </asp:GridView>
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                                                    <thead>
                                                        <tr class="tbHeader baris">
                                                            <th class="kotak" style="width: 4%">ID</th>
                                                            <th class="kotak" style="width: 45%">SOP Item</th>
                                                            <th class="kotak" style="width: 5%">Bobot</th>
                                                            <th class="kotak" style="width: 10%">Pencapaian</th>
                                                            <th class="kotak" style="width: 8%">Score</th>
                                                            <th class="kotak" style="width: 8%">Status</th>
                                                            <th class="kotak" style="width: 10%">PIC</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstTask" runat="server" OnItemDataBound="lstTask_DataBound">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris" style="height: 24px">
                                                                    <td class="kotak tengah"><%# Eval("IDdetail")%></td>
                                                                    <td class="kotak"><%# Eval("NewSop")%></td>
                                                                    <td class="kotak tengah"><%# Eval("BobotNilai") %>%</td>
                                                                    <td class="kotak tengah"><%# Eval("KetTargetKe")%></td>
                                                                    <td class="kotak tengah"><%# Eval("PointNilai")%></td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                                                    <td class="kotak"><%# Eval("PIC")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="OddRows baris" style="height: 24px">
                                                                    <td class="kotak tengah"><%# Eval("IDdetail")%></td>
                                                                    <td class="kotak"><%# Eval("NewSop")%></td>
                                                                    <td class="kotak tengah"><%# Eval("BobotNilai") %>%</td>
                                                                    <td class="kotak tengah"><%# Eval("KetTargetKe")%></td>
                                                                    <td class="kotak tengah"><%# Eval("PointNilai")%></td>
                                                                    <td class="kotak tengah">
                                                                        <asp:Label ID="sts" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                                                    <td class="kotak"><%# Eval("PIC")%></td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="content">
                                            <hr />
                                            <table style="width: 100%; border-collapse: collapse; font-size: medium">

                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>Departemen</td>
                                                    <td>
                                                        <asp:TextBox ID="txtDepartemen" runat="server" Width="50%" ReadOnly="true"></asp:TextBox></td>
                                                    <td>
                                                        <asp:HiddenField ID="txtDepartemenID" runat="server" />
                                                    </td>
                                                </tr>

                                            </table>
                                            <hr />
                                            <div class="contentlist" style="height: 420px">
                                                <table style="width: 100%; border-collapse: collapse; font-size: x-small; display: block">
                                                    <thead>
                                                        <tr class=" tbHeader baris">
                                                            <th class="kotak" rowspan="2" style="width: 4%">&nbsp;</th>
                                                            <th class="kotak" rowspan="2" style="width: 30%"><%=(Request.QueryString["p"]!=null)?Request.QueryString["p"].ToString():"SOP"%> Description</th>
                                                            <th class="kotak" rowspan="2" style="width: 5%">Bobot</th>
                                                            <th class="kotak" rowspan="2" style="width: 10%">Target</th>
                                                            <th class="kotak" style="width: 8%">Pencapaian</th>
                                                            <th class="kotak" style="width: 5%">Score</th>
                                                            <th class="kotak" style="width: 5%">Nilai</th>
                                                            <th class="kotak" rowspan="2" style="width: 15%">Alasan UnApprove</th>
                                                            <th class="kotak" rowspan="2" style="width: 5%">Ket</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="lstH" runat="server" OnItemDataBound="lstH_DataBound" OnItemCommand="lstH_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="total baris">
                                                                    <td class="kotak tengah ">
                                                                        <asp:CheckBox ID="chkAll2" Text="" CssClass='<%# Container.ItemIndex %>' runat="server" OnCheckedChanged="chkAll_Check" AutoPostBack="true" /></td>
                                                                    <td class="kotak" colspan="8"><b><%# Eval("Periode") %></b></td>
                                                                </tr>
                                                                <asp:Repeater ID="lstRkp" runat="server" OnItemDataBound="lstRkp_DataBound">
                                                                    <ItemTemplate>
                                                                        <tr class=" EvenRows baris">
                                                                            <td class="kotak angka">
                                                                                <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" OnCheckedChanged="chk_Checked" />&nbsp;</td>
                                                                            <td class="kotak"><%# Eval("SOPName") %> - &nbsp;<b>[
                                                                                <asp:Label ID="xxx" runat="server" Text='<%# Eval("Approval")%>'></asp:Label>
                                                                                ]</b></td>
                                                                            <td class="kotak tengah"><%# Eval("BobotNilai", "{0:N0}")%>%</td>
                                                                            <td class="kotak"><%# Eval("Target") %></td>
                                                                            <td class="kotak tengah"><%# Eval("Pencapaian")%></td>
                                                                            <td class="kotak tengah"><%# Eval("Score", "{0:N0}")%></td>
                                                                            <td class="kotak tengah"><%# Eval("Nilai", "{0:N2}")%></td>
                                                                            <td class="kotak">
                                                                                <asp:TextBox ID="txtAlasanUnApprove" runat="server" ToolTip='<%# Container.ItemIndex %>'
                                                                                    CssClass="txtongrid" Width="100%" Text='<%# Eval("AlasanUnApprove")%>'></asp:TextBox><!--<%# Eval("AlasanUnApprove")%>--></td>

                                                                            <td class="kotak tengah"><%# Eval("Penilaianx")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:Repeater>
                                                                <asp:Repeater ID="lstTot" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr class="total">
                                                                            <td colspan="2" class="angka kotak"><b>Total</b>&nbsp;</td>
                                                                            <td class="kotak tengah"><%# Eval("TotalBobot","{0:N0}") %>%</td>
                                                                            <td class="kotak" colspan="3">&nbsp;</td>
                                                                            <td class="kotak tengah"><%# Eval("TotalNilai","{0:N2}") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>

                </div>
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
</asp:Content>
