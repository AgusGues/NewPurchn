<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputProjectNew_Rev1.aspx.cs" Inherits="GRCweb1.Modul.MTC.InputProjectNew_Rev1" %>
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
       
    </style>

    <script language="javascript" src="../../Script/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    


            <div id="Div1" runat="server">
                <div class="panel panel-primary">
                        <td style="width: 100%;">
                            <%--  header panel--%>
                            <div class="panel-heading">
                                <strong>&nbsp;<span style="font-family: Calibri">Input Improvement</span></strong>
                                <div class="pull-right">
                                    <asp:Button class="btn btn-info" ID="btnNew" runat="server" Text="Baru" OnClick="btnNew_ServerClick" Style="font-family: Calibri" />
                                    <asp:Button class="btn btn-info" ID="btnUpdate" runat="server" Text="Simpan" OnClick="btnUpdate_ServerClick"
                                        Style="font-family: Calibri" />
                                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="List" OnClick="btnList_ServerClick"
                                        Style="font-family: Calibri" />
                                    <asp:Button class="btn btn-info" ID="btnDelete" runat="server" Text="Hapus" OnClick="btnDelete_ServerClick"
                                        Style="font-family: Calibri" />
                                    <asp:DropDownList class="btn btn-info" ID="ddlSearch" runat="server" Width="120px" Style="font-family: Calibri">
                                        <asp:ListItem Value="Nomor">No Improvement</asp:ListItem>
                                        <asp:ListItem Value="ProjectName">Nama Improvement</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox class="btn btn-info" ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                                    <asp:Button class="btn btn-info" ID="btnSearch" runat="server" Text="Cari" OnClick="btnSearch_Click" Style="font-family: Calibri" />
                                    &nbsp;
                                </div>
                            </div>
                            <%-- end of header panel --%>
                        </td>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">No Improvement </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtNoImprovement" runat="server" Width="50%" Enabled="False" Style="font-family: Calibri"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="InfoLabelSave" runat="server" ForeColor="#00CC00" Font-Italic="False"
                                            BackColor="#0000CC" Style="font-family: Calibri; font-size: x-small; font-style: italic; color: #FFFFFF"></asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <span style="display: none">
                                            <asp:TextBox ID="txtMinEstimasi" runat="server"></asp:TextBox>
                                        </span>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        Dept. Penerima
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlDept" runat="server" Style="font-family: Calibri" Width="150px">
                                            <asp:ListItem Value="0">---- Pilih Dept ----</asp:ListItem>
                                            <asp:ListItem Value="7">HRD & GA</asp:ListItem>
                                            <asp:ListItem Value="19">MAINTENANCE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="trHead" class="row" runat="server" visible="false">
                                    <div class="col-md-4">
                                        Nama Head
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlHead" runat="server" Width="400px">
                                            <asp:ListItem Value="0">---- Pilih Head ----</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        Tanggal
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtTglProject" runat="server" Width="25%" Style="font-family: Calibri"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTglProject" Format="dd-MMM-yyyy"
                                            runat="server"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <%--penambahan agus 10-05-2022--%>
                                 <div class="row">
                                    <div class="col-md-4">
                                        Kondisi Sebelum/yang ada sekarang
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtKondisiSebelum" runat="server" TextMode="MultiLine" Width="85%"
                                            CssClass="txtUpper" Style="font-family: Calibri"></asp:TextBox>
                                     </div>
                                </div>
                               <div class="row">
                                    <div class="col-md-4">
                                        Kondisi yang di harapkan
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtKondisiyangdiharapkan" runat="server" TextMode="MultiLine" Width="85%"
                                            CssClass="txtUpper" Style="font-family: Calibri"></asp:TextBox>
                                   </div>
                                </div>
                                <%--penambahan agus 10-05-2022--%>
                                 <div class="row">
                                    <div class="col-md-4">
                                        Improvement Group
                                     </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlProjectGroup" runat="server" Style="font-family: Calibri">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               <div class="row">
                                    <div class="col-md-4">
                                        Nama Improvement
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtProjectName" runat="server" TextMode="MultiLine" Width="85%"
                                            CssClass="txtUpper" Style="font-family: Calibri"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        Sasaran Improvement
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlSasaran" runat="server" Style="font-family: Calibri">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               <div class="row">
                                    <div class="col-md-4">
                                        Tujuan / Target
                                    </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDetailSasaran" runat="server" TextMode="MultiLine" Width="85%"
                                            CssClass="txtUpper" Height="65px" Style="font-family: Calibri"></asp:TextBox>
                                   </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        Sub Project
                                   </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtSubProjectName" runat="server" Width="85%" Enabled="false" CssClass="txtUpper"></asp:TextBox>
                                   </div>
                                </div>
                               <div class="row">
                                    <div class="col-md-4">
                                        Dept. Pemohon
                                    </div>
                                    <div class="col-md-8">
                                        <%--<asp:DropDownList ID="ddlDeptName" runat="server"></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtDeptName" runat="server" ReadOnly="True" Width="310px" Style="font-family: Calibri"></asp:TextBox>
                                        <asp:TextBox ID="txtID" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                   </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        Area Improvement
                                    </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlProdLine" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProdLine_SelectedIndexChanged"
                                            Style="font-family: Calibri">
                                        </asp:DropDownList>
                                   </div>
                                </div>
                               <div class="row">
                                    <div class="col-md-4">
                                        Sub Area
                                   </div>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlArea" runat="server" Style="font-family: Calibri">
                                        </asp:DropDownList>
                                   </div>
                                
                                    <div class="col-md-4">
                                        Jumlah
                                   </div>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtEstimasiBiaya" runat="server" CssClass="angka" Width="20%" AutoPostBack="true"
                                            OnTextChanged="txtEstimasiBiaya_Change" Visible="false" Enabled="False" Style="font-family: Calibri"></asp:TextBox>
                                        <asp:TextBox ID="txtQtyProject" runat="server" Width="5%" CssClass="angka" Style="font-family: Calibri">1</asp:TextBox>
                                        &nbsp;&nbsp;<span style="font-family: Calibri"> Satuan :</span> &nbsp;<asp:DropDownList
                                            ID="ddlUnit" runat="server" Style="font-family: Calibri">
                                        </asp:DropDownList>
                                   </div>
                                </div>
                                <div class="col-md-4">
                                        <asp:DropDownList ID="ddlApproval" runat="server" Enabled="false" Visible="false">
                                            <asp:ListItem Value="1">Plant Manager</asp:ListItem>
                                            <asp:ListItem Value="2">Direksi</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Visible="false">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Open</asp:ListItem>
                                            <asp:ListItem Value="2">Release</asp:ListItem>
                                            <asp:ListItem Value="3">Close</asp:ListItem>
                                            <asp:ListItem Value="4">Pending</asp:ListItem>
                                            <asp:ListItem Value="-1">Cancel</asp:ListItem>
                                        </asp:DropDownList>
                                   
                                        <asp:TextBox ID="txtProjectID" runat="server" Visible="false"></asp:TextBox>
                                   </div>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <div style="padding: 5px; width: 100%; height: 180px" class="contentlist" style="background: #fff;">
                                    <table width="100%" style="border-collapse: collapse; font-size: smaller" border="0">
                                        <thead>
                                            <tr class="tbHeader" style="height: 24px; font-size: x-small">
                                                <th class="kotak" style="width: 5%">No.
                                                </th>
                                                <th class="kotak" style="width: 30%">Improvement
                                                </th>
                                                <th class="kotak" style="width: 15%">Dept.
                                                </th>
                                                <th class="kotak" style="width: 8%">Tgl. Mulai
                                                </th>
                                                <th class="kotak" style="width: 8%">Tgl. Selesai
                                                </th>
                                                <th class="kotak" style="width: 8%">Estimasi Biaya
                                                </th>
                                                <th class="kotak" style="width: 8%">Approval
                                                </th>
                                                <th class="kotak" style="width: 8%">Status
                                                </th>
                                                <th class="kotak" style="width: 5%">&nbsp;
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstProject" runat="server" OnItemDataBound="lstProject_DataBound"
                                                OnItemCommand="lstSarmut_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris">
                                                        <td class="kotak tengah">
                                                            <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td class="kotak">
                                                            <%# Eval("NamaProject").ToString().ToUpper()%>
                                                        </td>
                                                        <td class="kotak">
                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%#Eval("ProjectDate", "{0:d}")%>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <%#Eval("FinishDate","{0:d}") %>
                                                        </td>
                                                        <td class="kotak angka">
                                                            <asp:Label ID="lblBiaya" runat="server"><%# Eval("Biaya","{0:N2}") %></asp:Label>
                                                        </td>
                                                        <td class="kotak">
                                                            <asp:Label ID="app" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="kotak">
                                                            <asp:Label ID="sts" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="lstEdit" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="edit" />
                                                            <asp:ImageButton ID="lstDel" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="dtlProject" runat="server" OnItemDataBound="dtlProject_ItemDataBound"
                                                        OnItemCommand="dtlProject_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="baris" style="background-color: #FFFACD">
                                                                <td class="kotak"></td>
                                                                <td class="kotak">&nbsp;&nbsp;<img src="../../images/dots.gif" alt="&bull;" />&nbsp;<%# Eval("SubProjectName").ToString().ToUpper()%></td>
                                                                <td class="kotak" colspan="2">&nbsp;&nbsp;<asp:Label ID="grpName" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%#Eval("ProjectDate", "{0:d}")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%#Eval("FinishDate","{0:d}") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Biaya","{0:N2}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:ImageButton ID="lstEditdt" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDelft" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr class="baris" style="background-color: ">
                                                                <td class="kotak"></td>
                                                                <td class="kotak">&nbsp;&nbsp;<img src="../../images/dots.gif" alt="&bull;" />&nbsp;<%# Eval("SubProjectName").ToString().ToUpper()%></td>
                                                                <td class="kotak" colspan="2">&nbsp;&nbsp;<asp:Label ID="grpName" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%#Eval("ProjectDate", "{0:d}")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%#Eval("FinishDate","{0:d}") %>
                                                                </td>
                                                                <td class="kotak angka">
                                                                    <%# Eval("Biaya","{0:N2}") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:ImageButton ID="lstEditdt" ImageUrl="~/images/folder.gif" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="edit" />
                                                                    <asp:ImageButton ID="lstDelft" ImageUrl="~/images/Delete.png" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                        CommandName="del" OnClientClick="javascript:return confirm('Are you sure to delete record?')" />
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
 
</asp:Content>
