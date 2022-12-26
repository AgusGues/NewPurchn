<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormBudgetingMaster.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormBudgetingMaster" %>
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
        /*table,tr,td{background-color: #fff;}*/
    </style>

    <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function() {
        $(document).ready(function() {
            divWidth();
            
        });
    });
    function _showAddMaster() {
//        $('#addMat').show()
//        $('#addMat1').show()
    }
    function _hideAddMaster() {
//        $('#addMat').hide()
//        $('#addMat1').hide()
    }
    function divWidth() {
        var id = "<%=approv.ClientID %>";
        //$('#' + id).css({ "width": screen.availWidth - 220 });
    }
    function simpandata(id,event){
        if(event.keyCode==13)
        {
//            $.ajax({
//                type:'POST',
//                url:
            return false;
        }
    }
</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        
            <div id="Div1" runat="server">
                <table style="width:100%; border-collapse:collapse">
                    <tr>
                        <td style="height:49px; width:100%">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width:50%; padding-left:10px"><b>MASTER BUDGET <asp:Label ID="jdl" runat="server"></asp:Label></b></td>
                                    <td style="width:50%; padding-right:10px;" align="right">
                                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" OnClick="btnSimpan_Click" />
                                        <%--<asp:Label ID="lblCari" runat="server" Text="Cari"></asp:Label>--%>
                                        <asp:TextBox ID="txtCari" Width="250px" Text="Find by Itemname or ItemCode" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Find by ItemCode or ItemName"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr>
                                        <td style="width:10%">&nbsp;</td>
                                        <td style="width:10%">Periode</td>
                                        <td style="width:20%">
                                            <asp:DropDownList ID="ddlSmt" runat="server" Visible="true">
                                                <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                                <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTahun" runat="server"></asp:DropDownList>
                                            </td>
                                        <td style="width:5%"></td>
                                        <td style="width:40%"><asp:Button ID="addMaster" runat="server" Text="Add Master" OnClick="addMaster_Click" />
                                        <span id="addMat" runat="server" visible="false">
                                        <asp:TextBox ID="txtFind" Width="70%" runat="server" Text="Find by Itemname" 
                                            onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;" 
                                            placeholder="Find by ItemName" AutoPostBack="true" OnTextChanged="txtFind_Change"></asp:TextBox></span></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td><td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnpreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td><span id="addMat1" runat="server" visible="false"><asp:DropDownList ID="ddlMaterial" runat="server" Width="80%"></asp:DropDownList>
                                        <asp:Button ID="addToMaster" runat="server" Text="Add" OnClick="addToMaster_Click" /></span>
                                        </td>
                                    </tr>
                                </table>
                                <div class="contentlist" style="overflow:hidden;" >
                                <div id="approv" runat="server" style="overflow:auto; height:430px">
                                <asp:Table ID="tbl" runat="server" CssClass="tbNormal">
                                       
                                </asp:Table>
                                 </div>   
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
             </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
