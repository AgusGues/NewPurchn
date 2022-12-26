<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApvRMM2.aspx.cs" Inherits="GRCweb1.Modul.RMM.ApvRMM2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
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
        
        //function OpenDialog(id) {
        //    params = 'dialogWidth:820px';
        //    params += '; dialogHeight:200px'
        //    params += '; top=0, left=0'
        //    params += '; resizable:no'
        //    params += ';scrollbars:no';
        //    window.showModalDialog("../../ModalDialog/UploadFileSarmut.aspx?ba=" + id + "&tablename=SPD_AttachmentDep", "UploadFile", params);
        //};
        //function OpenDialog2(id) {
        //    params = 'dialogWidth:820px';
        //    params += '; dialogHeight:200px'
        //    params += '; top=0, left=0'
        //    params += '; resizable:no'
        //   params += ';scrollbars:no';
        //    window.showModalDialog("../../ModalDialog/UploadFileSarmut.aspx?ba=" + id + "&tablename=SPD_AttachmentPrs", "UploadFile", params);
        //};
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;<%-- <%=Judul %>--%>
                                            APPROVAL RMM </b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel"
                                            Visible="false" />
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click"
                                            Visible="false" />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                        <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" />
                                        <asp:HiddenField ID="rMMNO" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse;
                                    font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 35%">
                                        </td>
                                        <td>
                                        <asp:Panel runat="server" ID="IsoOnly">
                                             &nbsp; Departemen :
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                                Width="204px">
                                            </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0"
                                        id="baList">
                                        <thead>
                                            <tr class="tbHeader">
                                                <th rowspan="1" class="kotak tengah" nowrap="nowrap" style="width: 4%">
                                                    #
                                                     <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Text="ALL" OnCheckedChanged="chk_CheckedChange" />
                                                </th>
                                                <th rowspan="1" style="width: 7%" class="kotak">
                                                    No RMM
                                                </th>
                                                <th style="width: 5%" class="kotak">
                                                    Tgl.RMM
                                                </th>
                                                <th rowspan="1" style="width: 5%" class="kotak">
                                                    Departemen
                                                </th>
                                                <th rowspan="1" style="width: 30%" class="kotak">
                                                    Aktivitas
                                                </th>
                                                <th rowspan="1" style="width: 5%" class="kotak">
                                                    Status Apv
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstAppRMM" runat="server" OnItemDataBound="lstAppRMM_DataBound">
                                                <ItemTemplate>
                                                    <tr class="OddRows baris">
                                                        <td class="kotak tengah" nowrap="nowrap" style="width: 4%">
                                                            <span class="angka">
                                                                <%-- <%# Container.ItemIndex+1 %>--%></span>
                                                            <asp:CheckBox ID="chkprs" AutoPostBack="true" ToolTip='<%# Eval("ID") %>' runat="server"
                                                                OnCheckedChanged="chk_CheckedChangePrs" /></span>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("RMM_No")%>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("Tgl_RMM","{0:d}") %>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("DeptFrom")%>
                                                        </td>
                                                        <td class="kotak" nowrap="nowrap">
                                                            <%# Eval("Aktivitas")%>
                                                        </td>
                                                        <td class="kotak tengah" nowrap="nowrap">
                                                            <%# Eval("Approval")%>
                                                        </td>
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
