<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListUPD.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.ListUPD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <asp:Panel ID="PanelUtama" runat="server" Visible="false">
                    <table style="table-layout: fixed" width="100%">
                        <tbody>
                            <tr>
                                <td style="width: 100%; height: 49px">
                                    <table class="nbTableHeader">
                                        <tr>
                                            <td style="width: 100%">
                                                <strong>&nbsp;&bull;&nbsp;List UPD</strong>
                                            </td>
                                            <td style="width: 75px">
                                                <input id="btnCancel" runat="server" style="background-color: white; font-weight: bold;
                                                    font-size: 11px;" type="button" value="Cancel" onserverclick="btnCancel_ServerClick" />
                                            </td>
                                            <td style="width: 75px">
                                                <input id="btn1" runat="server" style="background-color: white; font-weight: bold;
                                                    font-size: 11px;" type="button" value="Form Input UPD" onserverclick="btn1_ServerClick" />
                                            </td>
                                            <td style="width: 75px">
                                                <input id="btnlampiran" runat="server" style="background-color: white; font-weight: bold;
                                                    font-size: 11px;" type="button" value="+ Lampiran" onserverclick="btnlampiran_ServerClick" />
                                            </td>
                                            <td style="width: 75px">
                                                <input id="btnList" runat="server" style="background-color: white; font-weight: bold;
                                                    font-size: 11px;" type="button" value="List UPD" onserverclick="btnList_ServerClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelUpload" runat="server" Visible="false">
                    <table class="tblForm" style="width: 50%; border-collapse: collapse; font-size: x-small">
                        <tr>                                                   
                            <td style="width: 10%; padding-left: 15px">
                                &nbsp;<span class="style1" style="font-family: Calibri; font-weight: bold">- UPLOAD FILE -</span>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;&nbsp;
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" Style="font-family: Calibri;
                                    font-weight: 700" />
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Refresh" Style="font-family: Calibri;
                                    font-weight: 700" />
                                <asp:Button ID="Button" runat="server" OnClick="Button_Click" Text="Upload" Style="font-family: Calibri;
                                    font-weight: 700" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <hr />
                <asp:Panel ID="PanelH" runat="server" Visible="true" bgcolor="#669999">
                    <table style="width: 100%;" bgcolor="#669999">
                        <tr style="width: 100%" bgcolor="#669999">
                            <td style="width: 75%; font-family: Calibri;">
                                <span style="font-size: 10pt">&nbsp;&nbsp;<b> </b></span><b><span style="font-size: large">
                                    LIST INPUTAN UPD</span></b>
                            </td>
                            <td style="width: 10%">
                                <asp:RadioButton ID="RB1" runat="server" AutoPostBack="True" OnCheckedChanged="RB1_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: small; text-align: center;
                                    font-style: italic;" Text="&nbsp;Dept. ISO" TextAlign="Left" 
                                    Width="100%"/>
                            </td>
                            <td style="width: 15%">
                                <asp:RadioButton ID="RB2" runat="server" AutoPostBack="True" OnCheckedChanged="RB2_CheckedChanged"
                                    Style="font-family: 'Brush Script MT'; font-size: small; text-align: center;
                                    " Text="&nbsp; Dept. Lain-nya" TextAlign="Left" Width="100%"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelISI" runat="server" Visible="false" bgcolor="#669999">
                    <table id="Table4" style="width: 100%;" bgcolor="#669999">
                        <table style="width: 100%;" bgcolor="#669999">
                            <tr style="width: 100%" bgcolor="#669999">
                                <td style="width: 3%; font-family: Calibri;">
                                    <span style="font-size: 10pt">&nbsp;&nbsp;<b> ID</b></span>
                                </td>
                                <td style="width: 7%; background-color: #669999;">
                                    <asp:TextBox ID="txtID" runat="server" Width="30" ReadOnly="True" Enabled="False"
                                        Style="font-family: Calibri"></asp:TextBox>
                                </td>
                                <td style="width: 10%; font-family: Calibri; font-weight: 700; background-color: #669999;">
                                    <span style="font-size: 10pt">&nbsp; No. Dokumen</span>
                                </td>
                                <td style="width: 15%; font-family: Calibri; background-color: #669999;">
                                    <asp:TextBox ID="txtNo" runat="server" Width="100" ReadOnly="True" Style="font-family: Calibri"></asp:TextBox>
                                </td>
                                <td style="width: 10%; font-family: Calibri; font-weight: 700; background-color: #669999;">
                                    <span style="font-size: 10pt">&nbsp; Nama Dokumen</span>
                                </td>
                                <td style="width: 55%; font-family: Calibri;">
                                    <asp:TextBox ID="txtNama" runat="server" Width="500px" ReadOnly="True" 
                                         Style="font-family: Calibri"></asp:TextBox>
                                </td>
                            </tr>
                        </table>                        
                        <table style="width: 100%">
                            <tr>
                                <td>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtNamaFile" runat="server" ReadOnly="False" Visible="false" Width="20%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="4" OnRowCommand="GridView2_RowCommand" 
                                    OnRowDataBound="GridView2_RowDataBound" Style="font-family: Calibri; font-size: 10pt;
                                        font-weight: 500" Width="500px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File" ShowHeader="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                    CommandArgument='<%# Eval("File") %>' CommandName="Download" 
                                                    Text='<%# Eval("File") %>'>
                                                    </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="Tanggal Upload">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                </asp:GridView>
                            </td>
                            </tr>
                        </table>
                        <%--</tr>--%>
                   <%-- </table>--%>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <%--<td>
                                &nbsp;
                            </td>--%>
                            <td>
                                <%--<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="4" OnRowCommand="GridView2_RowCommand" 
                                    OnRowDataBound="GridView2_RowDataBound" Style="font-family: Calibri; font-size: 10pt;
                                        font-weight: 500" Width="500px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="File" ShowHeader="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                    CommandArgument='<%# Eval("File") %>' CommandName="Download" 
                                                    Text='<%# Eval("File") %>'>
                                                    </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Date" HeaderText="Tanggal Upload">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                </asp:GridView>--%>
                            </td>
                        </tr>
                    </table>
                    </table>
                </asp:Panel>
                <hr />
                <div id="div2" style="width: 100%; height: 450px; overflow: auto; padding: 10px;
                    padding-left: 15px; background-color: InactiveCaption">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="25" Style="text-align: left">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="true">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="5%" />
                            </asp:BoundField>
                            
                             <asp:BoundField DataField="DeptName" HeaderText="Department" Visible="true">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="15%" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="NoDokumen" HeaderText="Dokumen">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UpdName" HeaderText="Nama Dokumen">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="18%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CategoryUPD" HeaderText="Kategori Dokumen">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JenisUPD" HeaderText="Jenis Dokumen">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tanggal" HeaderText="Tgl Pengajuan" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Apv" HeaderText="Status">
                                <HeaderStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Alasan" HeaderText="Alasan">
                                <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NamaFile" HeaderText="Nama File">
                                <HeaderStyle Width="18%" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Add" Text="Pilih" Visible="true">
                                <HeaderStyle Width="2%" />
                            </asp:ButtonField>
                           <%-- <asp:ButtonField CommandName="Add2" Text="Upload" Visible="true">
                                <HeaderStyle Width="2%" />
                            </asp:ButtonField>--%>
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040"
                            BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                        <PagerStyle BorderStyle="Solid" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
