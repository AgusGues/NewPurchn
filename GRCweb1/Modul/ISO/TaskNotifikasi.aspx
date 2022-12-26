<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskNotifikasi.aspx.cs" Inherits="GRCweb1.Modul.ISO.TaskNotifikasi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        label{font-size:12px;}
    </style>
     <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <script src="../../Scripts/calendar.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            //$('div.contentlist').css({ 'height': screen.height - 370 });
        });
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; height: 100%; border-collapse: collapse">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 100%">
                                        &nbsp;&nbsp;NOTIFIKASI TASK
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 100%">
                            <div class="content">
                                
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:30%">
                                            <asp:RadioButton ID="mustApproval" Checked="true" runat="server" GroupName="pilih" Text="Approval" OnCheckedChanged="mustAproval_Change" AutoPostBack="true" />
                                            <asp:RadioButton ID="target" runat="server" GroupName="pilih" Text="Target End" OnCheckedChanged="target_Change" AutoPostBack="true"  />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="width:100%; height:450px">
                                <table border="1" style="width:100%; border-collapse:collapse; font-size:x-small; font-family:Arial">
                                    <thead>
                                        <tr class="tbHeader baris">
                                            <th class="kotak" style="width:3%">No</th>
                                            <th class="kotak" style="width:10%">Task No</th>
                                            <th class="kotak" style="width:35%">Task Description</th>
                                            <th class="kotak" style="width:6%">Tgl Task</th>
                                            <th class="kotak" style="width:5%">Terget Ke</th>
                                            <th class="kotak" style="width:6%">Tgl Target</th>
                                            <th class="kotak" style="width:8%">PIC</th>
                                            <th class="kotak" style="width:9%">Manager</th>
                                            <th class="kotak" style="width:15%">Status</th>
                                            <%--<th class="kotak" style="width:5%"></th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstTask" runat="server" OnItemDataBound="lstTask_DataBound">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak " valign="top"><%# Eval("TaskNo") %></td>
                                                    <td class="kotak"><%# Eval("NewTask") %></td>
                                                    <td class="kotak tengah"><%# Eval("TglMulai","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("TargetKe") %></td>
                                                    <td class="kotak tengah"><%# Eval("TglTarget","{0:d}") %></td>
                                                    <td class="kotak"><%# Eval("Pic") %></td>
                                                    <td class="kotak"><%# Eval("UserHead") %></td>
                                                    <td class="kotak"><asp:Label ID="stsTask" runat="server"></asp:Label></td>
                                                    <%--<td class="kotak">&nbsp;</td>--%>
                                                </tr>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
