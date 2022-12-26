<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputDisiplin.aspx.cs" Inherits="GRCweb1.Modul.ISO.FormInputDisiplin" %>

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

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        maintainScrollPosition();
    });
    function pageLoad() {
        maintainScrollPosition();
    }
    function maintainScrollPosition() {
        $("#<%=lst.ClientID %>").scrollTop($('#<%=hfScrollPosition.ClientID%>').val());
    }
    function setScrollPosition(scrollValue) {
        $('#<%=hfScrollPosition.ClientID%>').val(scrollValue);
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
                                INPUT DATA DISIPLIN
                            </div>
                            <div style="padding: 2px"></div>



                            <%--copy source design di sini--%>

       <div id="Div1" runat="server" class="table-responsive" style="width:100%">
            <table style="width:100%; border-collapse:collapse; font-size:x-small">
                <%--<tr valign="top">
                    <td style="height:49px">
                        <table class="nbTableHeader">
                            <tr>
                                <td style="width:100%">&nbsp;&nbsp;INPUT DATA DISIPLIN</td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr valign="top">
                    <td style="width:100%">
                        <div class="">
                            <table style="width:100%; font-size:small; border-collapse:collapse">
                                <tr>
                                    <td style="width:10%">&nbsp;</td>
                                    <td style="width:10%">Periode</td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnBulan_Change"></asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnTahun_Change"></asp:DropDownList>
                                    </td>
                                    <td style="width:45%"><asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>Departemen</td>
                                    <td><asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_Change"></asp:DropDownList></td>
                                    <td><asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_OnClick" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_OnClick" /></td>
                                </tr>
                            </table>
                        <hr />
                        <div class="contentlist" style="height:440px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                            <table style="width:100%; font-size:x-small; border-collapse:collapse" id="tbAsal">
                            <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" rowspan="2" style="width:3%">No.</th>
                                    <th class="kotak" rowspan="2" style="width:5%">NIP</th>
                                    <th class="kotak" rowspan="2" style="width:15%">Nama</th>
                                    <th class="kotak" rowspan="2" style="width:10%">Jabatan</th>
                                    <th class="kotak" colspan="2">Sakit</th>
                                    <th class="kotak" colspan="2">Ijin</th>
                                    <th class="kotak" colspan="2">Alpa</th>
                                    <th class="kotak" colspan="2">Terlambat</th>
                                    <th class="kotak" colspan="2">Sanksi SP</th>
                                    <th class="kotak" colspan="2">Sanksi ST</th>
                                    <th class="kotak" rowspan="2" style="width:4%">Total</th>
                                    <th class="kotak" rowspan="2" style="width:5%">Nilai</th>
                                    <th class="kotak" rowspan="2" style="width:5%">&nbsp;</th>
                                </tr>
                                <tr class="OddRows">
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                </tr>
                              </thead>
                                <tbody>
                                    <asp:Repeater ID="lstDept" runat="server" OnItemDataBound="lstDept_DataBound">
                                        <ItemTemplate>
                                            <tr class="Line3 baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak" colspan="15"><%# Eval("DeptName") %></tr>
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak">&nbsp;</td>
                                            </tr>
                                            <asp:Repeater ID="lstPerson" runat="server" OnItemDataBound="lstPerson_DataBound" OnItemCommand="lstPerson_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak angka"><%# Container.ItemIndex+1 %><asp:TextBox ID="txtUserID" runat="server" Text='<%# Eval("UserID") %>' Visible="false"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox Width="100%" ID="txtNIK" runat="server" Text='<%# Eval("NIK") %>' CssClass="txtongrid tengah" ReadOnly="true"></asp:TextBox></td>
                                                        <td class="kotak" nowrap="nowrap">
                                                            <asp:TextBox Width="85%" ID="txtNama" runat="server" Text='<%# Eval("Nama") %>' CssClass="txtongrid" ReadOnly="true"></asp:TextBox>
                                                            <asp:CheckBox ID="chkN" runat="server" ToolTip='<%# Container.ItemIndex %>' AutoPostBack="true" OnCheckedChanged="chkn_CheckedChange" />
                                                        </td>
                                                        <td class="kotak" nowrap="nowrap"><%# Eval("BagianName").ToString().ToUpper() %><asp:TextBox ID="txtBagianID" runat="server" Text='<%# Eval("BagianID") %>' Visible="false"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilSakit" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilSakit_Change"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCSakit" runat="server" CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                        
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilIjin" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilIjin_Change"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCIjin" runat="server" CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                        
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilAlpha" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilAlpha_Change"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCAlpha" runat="server"  CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                       
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilLate" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilLate_Change"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCLate" runat="server" CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                        
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilSP" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilSP_Change"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCSP" runat="server" CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                       
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilTP" runat="server" CssClass="txtongrid tengah" Width="100%" Text="0" onfocus="this.select()" OnTextChanged="txtNilTP_Change"></asp:TextBox></td>                                                       
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="ddlSCTP" runat="server" CssClass="txtongrid tengah" Width="100%" Text="100" onfocus="this.select()"></asp:TextBox></td>
                                                        
                                                        
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtNilTotal" runat="server" CssClass="txtongrid angka" Width="100%" ReadOnly="true" Text="600"></asp:TextBox></td>
                                                        <td class="kotak tengah"><asp:TextBox ToolTip='<%# Container.ItemIndex %>' ID="txtSCTotal" runat="server" CssClass="txtongrid angka" Width="100%" ReadOnly="true" Text="100"></asp:TextBox></td>
                                                        <td class="kotak tengah">
                                                            <asp:TextBox ID="txtIDData" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="txtIDScore" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:ImageButton ID="edit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Container.ItemIndex %>' CommandName="Edit" />
                                                            <asp:ImageButton ID="simpan" runat="server" ImageUrl="~/images/Save_blue.png" ToolTip='<%# Eval("UserID") %>' CommandArgument='<%# Container.ItemIndex %>' CommandName="Save" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <div id="lste" runat="server" style="display:none">
                            <table style="border-collapse:collapse; font-size:x-small; width:100%" border="1" id="tblcopy">
                                <thead>
                                <tr class="tbHeader">
                                    <th class="kotak" rowspan="2" style="width:3%">No.</th>
                                    <th class="kotak" rowspan="2" style="width:5%">NIP</th>
                                    <th class="kotak" rowspan="2" style="width:15%">Nama</th>
                                    <th class="kotak" rowspan="2" style="width:10%">Jabatan</th>
                                    <th class="kotak" colspan="2">Sakit</th>
                                    <th class="kotak" colspan="2">Ijin</th>
                                    <th class="kotak" colspan="2">Alpa</th>
                                    <th class="kotak" colspan="2">Terlambat</th>
                                    <th class="kotak" colspan="2">Sanksi SP</th>
                                    <th class="kotak" colspan="2">Sanksi ST</th>
                                    <th class="kotak" rowspan="2" style="width:4%">Total</th>
                                    <th class="kotak" rowspan="2" style="width:5%">Nilai</th>
                                    <%--<th class="kotak" rowspan="2" style="width:5%">&nbsp;</th>--%>
                                </tr>
                                <tr class="OddRows">
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                    <th class="kotak" style="width:4%">Jml</th>
                                    <th class="kotak" style="width:5%">Nilai</th>
                                </tr>
                              </thead>
                                <tbody>
                                <asp:Repeater ID="lstDeptC" runat="server" OnItemDataBound="lstDeptC_DataBound">
                                        <ItemTemplate>
                                            <tr class="Line3 baris">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak" colspan="15"><%# Eval("DeptName") %></tr>
                                                <td class="kotak">&nbsp;</td>
                                                <td class="kotak">&nbsp;</td>
                                            </tr>
                                            <asp:Repeater ID="lstPrs" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td><%# Eval("NIK") %></td>
                                                    <td><%# Eval("Nama") %></td>
                                                    <td><%# Eval("BagianName") %></td>
                                                    <td><%# Eval("JmlSakit","{0:N0}") %></td>
                                                    <td><%# Eval("ScoreSakit", "{0:N0}")%></td>
                                                    <td><%# Eval("JmlIjin", "{0:N0}")%></td>
                                                    <td><%# Eval("ScoreIjin", "{0:N0}")%></td>
                                                    <td><%# Eval("JmlAlpa", "{0:N0}")%></td>
                                                    <td><%# Eval("ScoreAlpa", "{0:N0}")%></td>
                                                    <td><%# Eval("JmlLambat", "{0:N0}")%></td>
                                                    <td><%# Eval("ScoreLambat", "{0:N0}")%></td>
                                                    <td><%# Eval("JmlSP", "{0:N0}")%></td>
                                                    <td><%# Eval("ScoreSP", "{0:N0}")%></td>
                                                    <td><%# Eval("JmlST", "{0:N0}")%></td>
                                                    <td><%# Eval("ScoreST", "{0:N0}")%></td>
                                                    <td><%# Eval("Total", "{0:N0}")%></td>
                                                    <td><%# Eval("Score", "{0:N2}")%></td>
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
