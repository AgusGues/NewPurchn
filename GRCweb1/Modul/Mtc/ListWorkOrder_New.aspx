<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListWorkOrder_New.aspx.cs" Inherits="GRCweb1.Modul.Mtc.ListWorkOrder_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
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
        function MyPopUpWin(url, width, height) {
            var leftPosition, topPosition;
            leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
            topPosition = (window.screen.height / 2) - ((height / 2) + 50);
            window.open(url, "Window2",
            "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
            + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
            + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
        }
        function OpenDialog(WOID) {
            
            MyPopUpWin("../../ModalDialog/UploadFileWO_New.aspx?wo=" + WOID, 830, 200)
        };

        //    function PreviewPDF2(id) {
        //        params = 'dialogWidth:890px';
        //        params += '; dialogHeight:600px'
        //        params += '; top=0, left=0'
        //        params += '; resizable:yes'
        //        params += ';scrollbars:no';
        //        window.showModalDialog("../../ModalDialog/PdfPreview.aspx?ba=" + id, "Preview", params);
        //    };
        function PreviewPDF(id) {
            params = 'width=890px';
            params += ', heigh=600px'
            params += ', top=20px, left=20px'
            params += ', resizable:yes'
            params += ', scrollbars:yes';
            window.open("../../ModalDialog/PDFPreviewWO.aspx?wrk=" + id, "Preview", params);
        };

        function confirm_batal() {
            if (confirm("Anda yakin untuk Cancel ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width: 100%">
                <table style="width: 100%; border-collapse: collapse; font-size: x-small">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader" style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        <b>&nbsp;&nbsp;<span style="font-family: Calibri; font-size: large">WORK ORDER</span></b>
                                    </td>
                                    <td style="width: 50%; padding-right: 10px" align="right">
                                        <asp:Button ID="btnBack" runat="server" Text="Form Work Order" OnClick="btnBack_Click"
                                            Style="font-family: Calibri; font-weight: 700" />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"
                                            Style="font-family: Calibri; font-weight: 700" />
                                        <asp:Button ID="btnUnApprove" runat="server" Text="Un Approve" OnClick="btnUnApprove_ServerClick"
                                            Style="font-weight: 700; font-family: Calibri" />
                                        <asp:HiddenField ID="appLevele" runat="server" />
                                        <asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table id="criteria" runat="server" style="width: 100%; border-collapse: collapse; font-size: x-small">
                                    <tr>
                                        <td style="width: 10%">&nbsp;
                                        </td>
                                        <%-- <td style="width:15%; font-family: Calibri; font-weight: 700; font-size: small;">Periode :</td>--%>
                                        <td style="width: 10%">
                                            <asp:Label ID="LabelPeriode" runat="server" Visible="true" Style="font-family: Calibri; font-size: medium; font-weight: 700;">&nbsp; Periode :</asp:Label>
                                        </td>
                                        <td style="width: 100%">
                                            <asp:DropDownList ID="ddlBulan" runat="server" Style="font-family: Calibri">
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlTahun" runat="server" Style="font-family: Calibri">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">&nbsp;</td>
                                        <td style="width: 10%">&nbsp;</td>
                                        <td style="width: 100%">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click"
                                                Style="font-family: Calibri" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click"
                                                Style="font-family: Calibri" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="PaneLShare" runat="server" Visible="false" Style="font-family: Calibri; background-color: #CC3300;"
                                    ForeColor="Red">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="0" id="tbl1">
                                        <tr style="width: 100%">
                                            <td style="width: 30%; font-size: x-small;">&nbsp;
                                                <asp:Label ID="LabelShare" runat="server" Style="font-family: 'Agency FB'; font-size: small; font-weight: 700"
                                                    Visible="false" BackColor="#CC3300" ForeColor="White">&nbsp;</asp:Label>
                                            </td>
                                            <td style="height: 3px; width: 30%;" valign="top">
                                                <asp:RadioButton ID="RBInt" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBInt_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; font-style: italic; background-color: #CC3300;" Text="....Untuk melihatnya silahkan klik di lingkaran ini >>"
                                                    TextAlign="Left" Width="300px" ForeColor="White" />
                                            </td>
                                            <td style="height: 3px; width: 30%;" valign="top">
                                                <asp:RadioButton ID="RBback" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBback_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; font-style: italic; background-color: #CC3300;" Text="....Back >>"
                                                    TextAlign="Left" Width="300px" ForeColor="White" />
                                            </td>
                                            <td style="height: 3px; width: 10%;" valign="top"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelUpdateWO" runat="server" Visible="false" Style="font-family: Calibri; background-color: #CC3300;"
                                    ForeColor="Red">
                                    <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                        border="0" id="Table1">
                                        <tr style="width: 100%">
                                            <td style="width: 30%; font-size: x-small;">&nbsp;
                                                <asp:Label ID="LabelUpdate1" runat="server" Style="font-family: 'Agency FB'; font-size: small; font-weight: 700"
                                                    Visible="false" BackColor="#CC3300" ForeColor="White">&nbsp;</asp:Label>
                                            </td>
                                            <td style="height: 3px; width: 30%;" valign="top">
                                                <asp:RadioButton ID="RB10" runat="server" AutoPostBack="True" GroupName="b" OnCheckedChanged="RB10_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; font-style: italic; background-color: #CC3300;" Text="....Untuk melihatnya silahkan klik di lingkaran ini >>"
                                                    TextAlign="Left" Width="300px" ForeColor="White" />
                                            </td>
                                            <td style="height: 3px; width: 30%;" valign="top">
                                                <%-- <asp:RadioButton ID="RB20" runat="server" AutoPostBack="True" OnCheckedChanged="RB20_CheckedChanged"
                                                    Style="font-family: Calibri; font-size: x-small; font-style: italic; background-color: #CC3300;" Text="....Back >>"
                                                    TextAlign="Left" Width="300px" ForeColor="White" />--%>
                                            </td>
                                            <td style="height: 3px; width: 10%;" valign="top"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="PanelRptr" runat="server" Visible="false">
                                    <div class="contentlist" style="height: 450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                                        <table style="width: 100%; border-collapse: collapse; font-size: x-small; font-family: Calibri;"
                                            border="0" id="baList">
                                            <thead>
                                                <tr class="tbHeader">
                                                    <th style="width: 3%" class="kotak">No.
                                                    </th>
                                                    <%-- <th style="width:6%" class="kotak">From Plant</th>--%>
                                                    <th style="width: 13%" class="kotak">WO Number
                                                    </th>
                                                    <%-- <th style="width:7%" class="kotak">From Dept</th> 
                                        <th style="width:7%" class="kotak">To Dept</th>--%>
                                                    <th style="width: 43%" class="kotak">Uraian Pekerjaan
                                                    </th>
                                                    <th style="width: 12%" class="kotak">Area Pekerjaan
                                                    </th>
                                                    <th style="width: 8%" class="kotak">Tgl Dibuat
                                                    </th>
                                                    <th style="width: 8%" class="kotak">Tgl Approve
                                                    </th>
                                                    <th style="width: 8%" class="kotak">Status
                                                    </th>
                                                    <th style="width: 3%">&nbsp;
                                                    </th>
                                                    <th style="width: 2%">&nbsp;
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody style="font-size: x-small">
                                                <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                                    <ItemTemplate>
                                                        <tr class="total baris">
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <span class="angka" style="width: 30%">
                                                                    <%# Container.ItemIndex+1 %></span> <span class="tengah" style="width: 40%">
                                                                        <asp:CheckBox ID="chk" AutoPostBack="true" CssClass='<%# Eval("WOID") %>' runat="server"
                                                                            OnCheckedChanged="chk_CheckedChange" /></span>
                                                            </td>
                                                            <%--<td class="kotak">&nbsp;<%# Eval("PlantName") %></td>--%>
                                                            <td class="kotak tengah" nowrap="nowrap">
                                                                <%# Eval("NoWO") %>
                                                            </td>
                                                            <%--<td class="kotak">&nbsp;<%# Eval("FromDeptName") %></td>   
                                                <td class="kotak">&nbsp;<%# Eval("ToDeptName") %></td>--%>
                                                            <td class="kotak">&nbsp;<%# Eval("UraianPekerjaan") %></td>
                                                            <td class="kotak">&nbsp;<%# Eval("AreaWO") %></td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("CreatedTime","{0:d}") %>
                                                            </td>
                                                            <td class="kotak tengah">
                                                                <%# Eval("CreatedTime1","{0:d}") %>
                                                            </td>
                                                            <td class="kotak tengah" col>&nbsp;<%# Eval("StatusApv") %></td>
                                                            <td class="kotak" colspan="2" nowrap="nowrap">
                                                                <span class="tengah" style="width: 40%">
                                                                    <asp:ImageButton ToolTip="Masukkan Lampiran" ID="att" runat="server" CssClass='<%# Eval("WOID") %>'
                                                                        CommandArgument='<%# Container.ItemIndex %>' CommandName="attach" ImageUrl="~/TreeIcons/Icons/BookY.gif" />
                                                                    <asp:ImageButton ToolTip="Hapus inputan WO" ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                        CssClass='<%# Eval("WOID") %>' AlternateText='<%# Eval("WOID") %>' CommandName="hps"
                                                                        ImageUrl="~/images/Delete.png" />
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="attachm1" runat="server" OnItemDataBound="attachm1_DataBound">
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak"></td>
                                                                    <%--<td class="kotak" colspan="3" style="border-right:0px">&nbsp;<%# Container.ItemIndex+1 %>. <%# Eval("FileName")%></td>                                                     
                                                        <td class="kotak angka" style="border-left:0px" colspan="2">
                                                            <asp:ImageButton ToolTip="Lihat Lampiran" ID="lihat" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("IDLampiran") %>' CommandName="pre" ImageUrl="~/images/14.png" />
                                                            <asp:ImageButton ToolTip="Hapus Lampiran" ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("WOID") %>' CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                        </td>--%>
                                                                    <td class="kotak" colspan="2">&nbsp;Plant :&nbsp;<%# Eval("PlantName")%>&nbsp;||&nbsp;&nbsp;<%# Eval("FromDeptName")%>&nbsp;>>&nbsp;&nbsp;<%# Eval("ToDeptName")%></td>
                                                                    <td class="kotak" colspan="6" bgcolor="#669999">&nbsp;&nbsp; Alasan Tidak Ikut :&nbsp;<%# Eval("AlasanNotApvOP")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                            <HeaderTemplate>
                                                                <tr class="Line3" style="height: 24px">
                                                                    <td class="kotak">&nbsp;
                                                                    </td>
                                                                    <td class="kotak" colspan="11">&nbsp;<b>Lampiran WO</b>
                                                                    </td>
                                                                    <%--<td class="kotak" colspan="7"> --%>
                                                                </td>
                                                                </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="EvenRows baris">
                                                                    <td class="kotak"></td>
                                                                    <td class="kotak" colspan="3" style="border-right: 0px">&nbsp;<%# Container.ItemIndex+1 %>.
                                                                    <%# Eval("FileName")%>
                                                                    </td>
                                                                    <td class="kotak angka" style="border-left: 0px" colspan="2">
                                                                        <asp:ImageButton ToolTip="Lihat Lampiran" ID="lihat" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                            CssClass='<%# Eval("IDLampiran") %>' CommandName="pre" ImageUrl="~/images/14.png" />
                                                                        <asp:ImageButton ToolTip="Hapus Lampiran" ID="hapus" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                                            CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("WOID") %>' CommandName="hps"
                                                                            ImageUrl="~/images/Delete.png" />
                                                                    </td>
                                                                    <td class="kotak" colspan="6">&nbsp;<%# Eval("CreatedTime")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <%--  </ItemTemplate>
                                            </asp:Repeater>
                                                        --%>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>

                        </td>
                    </tr>
                </table>
                <asp:FileUpload ID="Upload1" runat="server" Visible="false" />
                <%--<asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
