<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLock.aspx.cs" Inherits="GRCweb1.Modul.COGS.SystemLock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                LOCK SYSTEM 
                            </div>
                            <div style="padding: 2px"></div>




                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>


                                        <div class="col-md-3">
                                            <label for="form-field-9">Tanggal</label>
                                        </div>

                                        <div class="col-md-8">
                                            <asp:TextBox ID="DariTgl" runat="server" AutoPostBack="true" OnTextChanged="DariTgl_TextChanged" Height="30px"></asp:TextBox>&nbsp;&nbsp s/d &nbsp;&nbsp;
                                            <asp:TextBox ID="SampaiTgl" runat="server" Height="30px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="frTgl" EnableViewState="true" TargetControlID="DariTgl" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                            <cc1:CalendarExtender ID="CalendarExtender1" EnableViewState="true" TargetControlID="SampaiTgl" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                        </div>

                                    </div>


                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9">Periode</label>
                                        </div>

                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlDurasi" runat="server" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddlDurasi_SelectedIndexChanged">
                                                <asp:ListItem Value="FullDay">Full Day</asp:ListItem>
                                                <asp:ListItem Value="Custom">Custom</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:CheckBoxList ID="UsrList" runat="server" BackColor="White" Font-Size="X-Small" CssClass="MyImageButton" AutoPostBack="false" OnSelectedIndexChanged="UsrList_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9">Dari Jam</label>
                                        </div>

                                        <div class="col-md-8">
                                            <asp:TextBox ID="DariJam" runat="server" Width="60px" Height="30px"></asp:TextBox>&nbsp;s/d &nbsp;
                                            <asp:TextBox ID="SampaiJam" runat="server" Width="60px" Height="30px" AutoPostBack="true" OnTextChanged="SampaiJam_TextChanged"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                            <label for="form-field-9">Keterangan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtKeterangan" runat="server" class="form-control form-control-sm" TextMode="MultiLine" Height="130px" Width="325px"></asp:TextBox>
                                        </div>

                                        <div class="col-md-3">
                                            <%--<label for="form-field-9">Keterangan</label>--%>
                                        </div>
                                        <div class="col-md-8">

                                            <asp:CheckBox ID="AutoOpen" runat="server" Text="Auto Unlock" />

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-8">
                                            <asp:LinkButton ID="Locked" runat="server" class="btn btn-sm btn-info" OnClick="Lock_Click">Lock</asp:LinkButton>&nbsp
                                            <asp:LinkButton ID="unLock" runat="server" class="btn btn-sm btn-info" OnClick="unLock_Click">Un Lock</asp:LinkButton>
                                            <asp:TextBox ID="listLock" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>


                                </div>


                                <div class="col-md-6">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <label for="form-field-9">Lock Type</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:RadioButton GroupName="Lt" ID="Allusr" Text="All User" runat="server" AutoPostBack="true" OnCheckedChanged="All_CheckedChanged" Checked="true" />&nbsp;
                                            <asp:RadioButton GroupName="Lt" ID="DeptSelect" Text="Selected Dept." runat="server" AutoPostBack="true" OnCheckedChanged="Dept_CheckedChanged" />&nbsp;
                                            <asp:RadioButton GroupName="Lt" ID="ModulName" Text="Modul" runat="server" />
                                        </div>

                                    </div>
                                </div>





                            </div>

                            <div style="padding: 6px"></div>
                            
                            <table style="width: 100%; border-collapse: collapse; font-size: smaller" class="table table-bordered table-condensed table-hover">
                                <thead>
                                    <tr align="center">
                                        <th style="width: 5%; border: 1px solid" rowspan="2">No.</th>
                                        <th style="border: 1px solid" colspan="4">Peroide.</th>
                                        <th style="width: 8%; border: 1px solid" rowspan="2">Durasi</th>
                                        <th style="width: 25%; border: 1px solid" rowspan="2">Keterangan</th>
                                        <th style="width: 5%; border: 1px solid" rowspan="2">Status</th>
                                        <th style="width: 15%; border: 1px solid" rowspan="2">Lock User</th>
                                        <th style="width: 5%; border: 1px solid" rowspan="2">&nbsp;</th>
                                    </tr>
                                    <tr style="background-color: #F0E68C">
                                        <th style="width: 10%; border: 1px solid">Dari Tanggal</th>
                                        <th style="width: 5%; border: 1px solid">Jam</th>
                                        <th style="width: 10%; border: 1px solid">Sampai Tanggal</th>
                                        <th style="width: 5%; border: 1px solid">Jam</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstLock" runat="server" OnItemCommand="lstLock_ItemCommand">
                                        <ItemTemplate>
                                            <tr align="center" valign="top" style="cursor: pointer">
                                                <td style="border: 1px solid"><%# Eval("ID") %></td>
                                                <td style="border: 1px solid"><%# Eval("DariTgl","{0:d}") %></td>
                                                <td style="border: 1px solid"><%# Eval("DariJam") %></td>
                                                <td style="border: 1px solid"><%# Eval("SampaiTgl","{0:d}") %></td>
                                                <td style="border: 1px solid"><%# Eval("SampaiJam") %></td>
                                                <td style="border: 1px solid"><%# Eval("Durasi") %></td>
                                                <td style="border: 1px solid" align="left"><%# Eval("Keterangan") %></td>
                                                <td style="border: 1px solid"><%# Eval("Statuse") %></td>
                                                <td style="border: 1px solid" align="left" nowrap="nowrap"><%# Eval("UserLock") %></td>
                                                <td style="border: 1px solid">
                                                    <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/images/folder.gif" ToolTip="Edit/Unlock Proses" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr align="center" valign="top" cursor: pointer">
                                                <td style="border: 1px solid"><%# Eval("ID") %></td>
                                                <td style="border: 1px solid"><%# Eval("DariTgl","{0:d}") %></td>
                                                <td style="border: 1px solid"><%# Eval("DariJam") %></td>
                                                <td style="border: 1px solid"><%# Eval("SampaiTgl","{0:d}") %></td>
                                                <td style="border: 1px solid"><%# Eval("SampaiJam") %></td>
                                                <td style="border: 1px solid"><%# Eval("Durasi") %></td>
                                                <td style="border: 1px solid" align="left"><%# Eval("Keterangan") %></td>
                                                <td style="border: 1px solid"><%# Eval("Statuse") %></td>
                                                <td style="border: 1px solid;" align="left" nowrap="nowrap"><%# Eval("UserLock") %></td>
                                                <td style="border: 1px solid">
                                                    <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/images/folder.gif" ToolTip="Edit/Unlock Proses" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <div style="padding: 6px"></div>
                        </div>



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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
