<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputMaster.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormInputMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="table-responsive" style="width:100%">
            <table cellpadding="0" cellspacing="2" border="0">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="font-family: Calibri; width: 450px; font-size: x-small;">
                                    &nbsp;<span style="font-size: x-small; font-weight: bold">&nbsp;</span> <span style="font-size: large">
                                        <b>FORM INPUT MASTER DOKUMEN ISO</b></span>
                                </td>
                            </tr>
                        </table>
                        <%--Panel Form Biasa--%>
                        <asp:Panel ID="PanelPilihan" runat="server" Visible="True">
                         <table>
                         <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="LabelJenis" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                            Visible="true">&#160; Jenis Dokumen :</asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Height="16px" OnTextChanged="ddlJenisChanged"
                                            Style="font-family: Calibri; font-size: small" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                         </table>
                         </asp:Panel>
                        <%--Panel Form Biasa--%>
                        <asp:Panel ID="PanelBiasa" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <caption>
                                <hr />
                                <%--<tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="LabelJenis" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                            Visible="true">&#160; Jenis Dokumen :</asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="false" Height="16px"
                                            Style="font-family: Calibri; font-size: small" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 200px">
                                        <asp:Label ID="LabelCek" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                            Visible="false"></asp:Label>                                            
                                    </td>                                  
                                            
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtD" runat="server"  Enabled="true" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700" Width="200px" Visible="false"></asp:TextBox>
                                            <%--<asp:Label ID="txtD" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                            Visible="false"></asp:Label>--%> 
                                            
                                        <ajaxtoolkit:autocompleteextender id="AutoCompleteExtender4" runat="server" completioninterval="200"
                                            completionsetcount="10" contextkey="0" enablecaching="true" firstrowselected="True"
                                            minimumprefixlength="1" servicemethod="CekNomorDokumenBiasa" servicepath="AutoCompleteUPD.asmx"
                                            targetcontrolid="txtD" usecontextkey="true">
                                            </ajaxtoolkit:autocompleteextender>                                         
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        <div align="left" style="height: 15px">
                                            <input id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" style="width: 50px;
                                                font-family: Calibri;" type="button" value="Cek" />
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        <div align="left" style="height: 15px">
                                            <input id="btnUlang" runat="server" onserverclick="btnUlang_ServerClick" style="width: 50px;
                                                font-family: Calibri;" type="button" value="Refresh" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtTipe" runat="server" Visible="false" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 400px" colspan="2" align="center">
                                        <asp:Label ID="LabelKet" runat="server" BackColor="#00CC00" Font-Size="Medium" Style="font-family: Calibri;
                                            font-size: x-small; text-align: center;" Width="300px" Visible="False" Enabled="True"
                                            Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                         </asp:Panel>
                        <%--END Panel Form Biasa--%>
                        
                          <%--Panel Form Khusus--%>
                        <asp:Panel ID="PanelKhusus" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <caption>
                                <hr />                                
                                <tr>
                                    <td style="width: 200px">
                                        <asp:Label ID="LabelCekNama" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                            Visible="true">&#160; Cek Nama Dokumen :</asp:Label>                                            
                                    </td>                                  
                                            
                                    <td style="width: 100px">
                                        <asp:TextBox ID="txtNamaKhusus" runat="server"  Enabled="true" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                            
                                        <ajaxtoolkit:autocompleteextender id="AutoCompleteExtender1" runat="server" completioninterval="200"
                                            completionsetcount="10" contextkey="0" enablecaching="true" firstrowselected="True"
                                            minimumprefixlength="1" servicemethod="CekNamaDokumenKhusus" servicepath="AutoCompleteUPD.asmx"
                                            targetcontrolid="txtNamaKhusus" usecontextkey="true">
                                            </ajaxtoolkit:autocompleteextender>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        <div align="left" style="height: 15px">
                                            <input id="btnSearch2" runat="server" onserverclick="btnSearch2_ServerClick" style="width: 50px;
                                                font-family: Calibri;" type="button" value="Cek" />
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        <div align="left" style="height: 15px">
                                            <input id="btnUlang2" runat="server" onserverclick="btnUlang2_ServerClick" style="width: 50px;
                                                font-family: Calibri;" type="button" value="Refresh" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:TextBox ID="TextBox2" runat="server" Visible="false" Style="font-family: Calibri;
                                            font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 400px" colspan="2" align="center">
                                        <asp:Label ID="Label3" runat="server" BackColor="#00CC00" Font-Size="Medium" Style="font-family: Calibri;
                                            font-size: x-small; text-align: center;" Width="300px" Visible="False" Enabled="True"
                                            Font-Italic="True"></asp:Label>
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                         </asp:Panel>
                        <%--END Panel Form Khusus--%>
                        
                        <%--Panel Form Inputan--%>
                        <asp:Panel ID="PanelFormInput" runat="server" Visible="false">
                            <div class="contentlist" style="height: 450px" id="lst" runat="server">
                                <table style="width: 80%; border-collapse: collapse; font-size: x-small" border="0"
                                    bgcolor="#99CCFF">
                                    <tr>
                                        <td style="width: 200px">
                                            <asp:Label ID="LabelNomor" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                Visible="true">&nbsp; Nomor Dokumen</asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtNO" runat="server" Enabled="false" ReadOnly="true" Style="font-family: Calibri;
                                                font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px">
                                            <asp:Label ID="LabelNama" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                Visible="true">&nbsp; Nama Dokumen</asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtNama" runat="server" Enabled="true" Style="font-family: Calibri;
                                                font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px">
                                            <asp:Label ID="LabelMulai" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                Visible="true">&nbsp; Mulai Berlaku</asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtMulai" runat="server" Enabled="true" Style="font-family: Calibri;
                                                font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                        </td>
                                        <td style="width: 169px; height: 19px">
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtMulai">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px">
                                            <asp:Label ID="LabelRevisi" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                Visible="true">&nbsp; Revisi Ke</asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtRevisi" runat="server" Enabled="true" Style="font-family: Calibri;
                                                font-size: small; font-weight: 700" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px">
                                            <asp:Label ID="LabelDept" runat="server" Style="font-family: Calibri; font-size: x-small;"
                                                Visible="true">&nbsp; Department</asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td style="width: 100px">
                                            <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="false" Height="16px"
                                                Style="font-family: Calibri; font-size: small" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 50px">
                                            <div align="left" style="height: 15px">
                                                <input id="btnSave" runat="server" onserverclick="btnSave_ServerClick" style="width: 50px;
                                                    font-family: Calibri;" type="button" value="Simpan" />
                                            </div>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 400px" colspan="2" align="center">
                                            <asp:Label ID="LabelSave" runat="server" BackColor="#00CC00" Font-Size="Medium" Style="font-family: Calibri;
                                                font-size: x-small; text-align: center;" Width="300px" Visible="False" Enabled="True"
                                                Font-Italic="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <%--END Panel Form Inputan--%>
                        
                    </td>
                </tr>
            </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
