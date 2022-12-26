<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SerahWorkOrder_New.aspx.cs" Inherits="GRCweb1.Modul.Mtc.SerahWorkOrder_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

    function PreviewWO(IDLampiran) {
        params = 'dialogWidth:1000px';
        params += '; dialogHeight:600px'
        params += '; top=0, left=0'
        params += '; resizable:yes'
        params += ';scrollbars:yes';
        window.showModalDialog("../../ModalDialog/PDFPreviewWO.aspx?wrk=" + IDLampiran, "Preview", params);
        return false;
    };
    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server">
                <table style="table-layout: fixed; border-collapse: collapse" width="100%">
                    <tbody>
                        <tr>
                            <td style="width: 100%;">                               
                                <table class="nbTableHeader" width="100%'">
                                    <tr style="height: 49px"> 
                                        <td style="width: 70%"><strong>&nbsp;&nbsp;
                                            <asp:Label ID="LabelJudul" runat="server" Visible="false" Style="font-family: Calibri;
                                                font-size: medium; font-weight: bold"></asp:Label>
                                        </td>                                    
                                       
                                        <td style="width: 37px">
                                            <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" OnClick="btnPrev_ServerClick" 
                                                style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>
                                        
                                        <td style="width: 37px">
                                            <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" OnClick="btnNext_ServerClick" 
                                                style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>                                        
                                        <td style="width: 37px">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_ServerClick" 
                                                style="font-family: Calibri; font-size: x-small; font-weight: 700" />
                                        </td>                                       
                                        <%--<td style="width: 15px">
                                            <asp:HiddenField ID="noWO" runat="server" />
                                        </td>--%>
                                         <td>
                                            <asp:HiddenField ID="noWO" runat="server" />
                                        </td>
                                        <td style="width: 50%">
                                            <asp:DropDownList ID="ddlSearch" runat="server" Width="80px">
                                                <asp:ListItem Value="NoWo">No Wo</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:TextBox ID="txtSearch" runat="server" Width="95px"></asp:TextBox>
                                             <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold;
                                                font-size: 11px;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                        </td>
                                    </tr>
                                </table>
                              
                            </td>
                        </tr>
                        
                            <tr>
                                <td style="width: 100%" bgcolor="#CCCCCC">
                                    <%--<div style="margin: 5px; font-size: smaller; background-color: #B0C4DE; width: 100%">--%>
                                        <%--Panel Satu--%>
                                        <asp:Panel ID="PanelSatu" runat="server" Visible="true" Style="font-family: Calibri">
                                            <table class="tblForm" id="Table1" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelNoWO" runat="server" Visible="true" Style="font-family: Calibri;
                                                            font-size: x-small; font-weight: bold">&nbsp; No WO</asp:Label>
                                                    </td>
                                                    <td style="">
                                                        <asp:TextBox ID="txtNoWO" runat="server" ReadOnly="true" Style="font-family: Calibri;
                                                            font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                   <%-- <td style="width: 50%">
                                                        &nbsp;
                                                    </td>--%>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelIDWO" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-weight: bold"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--End Panel Satu--%>
                                        
                                        <%--Panel Dua--%>
                                        <asp:Panel ID="PanelDua" runat="server" Visible="true" Style="font-family: Calibri">
                                            <table class="tblForm" id="Table2" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelPeminta" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-weight: bold">&nbsp; Dept Pemohon</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtPeminta" runat="server" Visible="false" ReadOnly="true" Width="30%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelTglBuat" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Buat</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtTglBuat" runat="server" Visible="false" ReadOnly="true" Width="20%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelTglDisetujui" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Disetujui</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtTglDisetujui" runat="server" Visible="false" ReadOnly="true"
                                                            Width="20%" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--End Panel Dua--%>   
                                         
                                         <%--Panel Tiga--%>
                                        <asp:Panel ID="PanelTiga" runat="server" Visible="true" Style="font-family: Calibri">
                                            <table class="tblForm" id="Table3" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelUraian" runat="server" Visible="true" Style="font-family: Calibri;
                                                            font-size: x-small; font-weight: bold">&nbsp; Uraian Order Pekerjaan</asp:Label>
                                                    </td>
                                                    <td style="" colspan="4">
                                                        <asp:TextBox ID="txtUraian" runat="server" TextMode="MultiLine" Width="90%" ReadOnly="false"
                                                            Height="78px" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--End Panel Tiga--%>   
                                        
                                        <%--Panel Area Kerja--%>  
                                    <asp:Panel ID="PanelAreaKerja" runat="server" Visible="true" Style="font-family: Calibri">
                                        <table class="tblForm" id="Table5" style="width: 100%;">
                                            <tr>
                                                <td style="width: 18%">
                                                    <asp:Label ID="LabelAreaKerja" runat="server" Visible="false" Style="font-family: Calibri;
                                                        font-size: x-small; font-weight: bold">&nbsp; Area Pekerjaan</asp:Label>
                                                </td>
                                                <td style="width: 100%" colspan="2">
                                                    <asp:DropDownList ID="ddlArea" runat="server" Visible="false" AutoPostBack="True" Height="16px" Style="font-family: Calibri;
                                                        font-size: x-small" Width="20%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--End Panel Area Kerja--%> 
                                         
                                         <%--Panel Empat--%>
                                        <asp:Panel ID="PanelEmpat" runat="server" Visible="true" Style="font-family: Calibri">
                                            <table class="tblForm" id="Table4" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelDept" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-weight: bold">&nbsp; Dept Penerima</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtPenerima" runat="server" Visible="false" ReadOnly="true" Width="30%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelTargetSelesai" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Target Selesai</asp:Label>
                                                    </td>
                                                    <td style="width: 50%" colspan="2">
                                                        <asp:TextBox ID="txtTargetSelesai" runat="server" Visible="false" ReadOnly="false"
                                                            Width="25%" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                            
                                                            <asp:Label ID="LabelWajibisi1" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                                    </td>
                                                    <td style="width: 35%">
                                                        <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTargetSelesai" Format="dd-MMM-yyyy"
                                                            runat="server">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelPelaksana" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Pelaksana</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtPelaksana" runat="server" Visible="false" ReadOnly="true" Width="30%"
                                                            Style="font-family: Calibri; font-size: x-small; text-align: left;"></asp:TextBox>
                                                        <asp:Label ID="LabelWajibisi2" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelFinishDate" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Selesai Pekerjaan</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtFinishDate" runat="server" Visible="false" ReadOnly="true" Width="25%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        <asp:Label ID="LabelWajibisi3" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: xx-small; font-style: italic; color: #FF0000">* wajib diisi</asp:Label>
                                                    </td>
                                                    <td style="width: 35%">
                                                        <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtFinishDate" Format="dd-MMM-yyyy"
                                                            runat="server">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelTrial" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Tanggal Test / Trial</asp:Label>
                                                    </td>
                                                    <td style="width: 100%" colspan="2">
                                                        <asp:TextBox ID="txtTrial" runat="server" Visible="false" ReadOnly="true" Width="25%"
                                                            Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 35%">
                                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTrial" Format="dd-MMM-yyyy"
                                                            runat="server">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelHasilTrial" runat="server" Visible="false" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Hasil Test / Trial</asp:Label>
                                                    </td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td style="width: 18%">
                                                        <asp:Label ID="LabelPerbaikan" runat="server" Visible="true" Style="font-family: Calibri;
                                                            font-size: x-small; font-style: italic;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp; Uraian Perbaikan</asp:Label>
                                                    </td>
                                                    <td style="" colspan="4">
                                                        <asp:TextBox ID="txtPerbaikan" runat="server" TextMode="MultiLine" Width="90%" ReadOnly="false"
                                                            Height="78px" Style="font-family: Calibri; font-size: x-small"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </asp:Panel>
                                        <%--End Panel Empat--%> 
                                     <%--Panel Lima--%> 
                                     
                                     <%--End Panel Lima--%> 
                                     <%--Panel Enam--%>
                                     <hr />
                                   <div style="height:450px" id="lst" runat="server" onscroll="setScrollPosition(this.scrollTop);">
                        <table style="width:100%; border-collapse:collapse; font-size:x-small; font-family: Calibri;" 
                                border="0" id="baList" bgcolor="White">
                               
                                <thead>                                
                                    <tr class="tbHeader">
                                        <th style="width:5%" class="kotak">ID</th>
                                        <th style="width:70%" class="kotak">Nama File Lampiran</th>                                                                             
                                        <th style="width:3%">&nbsp;</th>
                                    </tr>                                  
                                </thead>
                                <tbody> 
                                    <asp:Repeater ID="lstBA" runat="server" OnItemDataBound="lstBA_DataBound" OnItemCommand="lstBA_Command">
                                        <ItemTemplate>
                                            <tr class="total baris">
                                                <td class="kotak tengah" nowrap="nowrap">                                               
                                                        <%# Eval("IDLampiran")%>
                                                </td>
                                                <td class="kotak" nowrap="nowrap">&nbsp;&nbsp;
                                                    <%# Eval("FileName")%>
                                                </td>
                                                <td class="kotak angka" style="border-left: 0px" colspan="2">
                                                    <%--<asp:ImageButton ToolTip="Click to Preview Document" ID="lihat" runat="server" CommandArgument='<%# Container.ItemIndex %>'
                                                        CssClass='<%# Eval("IDLampiran") %>' CommandName="pre" ImageUrl="~/images/14.png" />--%>
                                                    <asp:ImageButton  ID="view" runat="server" ImageUrl="~/images/14.png" CommandArgument='<%# Eval("IDLampiran") %>'
                                                    CommandName="viewpdf"  ToolTip="Click to Preview Document"/>
                                                </td>
                                            </tr>
                                        </ItemTemplate>                                                                  
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>   
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
