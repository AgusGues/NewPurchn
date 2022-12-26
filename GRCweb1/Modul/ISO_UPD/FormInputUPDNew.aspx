<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormInputUPDNew.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormInputUPDNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 24px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Input Dokument Iso</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnNew" runat="server" OnClick="btnNew_ServerClick" Text="Baru" />
                    <asp:Button class="btn btn-info" ID="btnSave" runat="server" Text="Simpan" OnClick="btnSave_ServerClick"/>
                    <asp:Button class="btn btn-info" ID="btnsave2" runat="server" Text="Update" tabindex="2" OnClick="btnSave2_ServerClick" />
                    <asp:Button class="btn btn-info" ID="btnList" runat="server" Text="ListUpd" OnClick="btnL_ServerClick"/>
                    <%--
                    <asp:DropDownList ID="ddlSearch" runat="server" Width="120px" Style="font-family: Calibri;font-size: x-small">
                    <asp:ListItem Value="SuratJalanNo">No.Document</asp:ListItem></asp:DropDownList>
                    --%>
                    <%--
                    <asp:TextBox ID="txtSearch" runat="server" Width="128px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_ServerClick" Text="Cari" />
                    --%>
                </div>
            </div>
            <div class="panel-body">

                <asp:Panel ID="PanelUpload" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">Upload Softcopy Upd</div>
                            <div class="col-md-8">
                                <div class="input-group">
                                    <asp:FileUpload class="form-control" ID="FileUpload1" runat="server" />
                                    <div class="input-group-btn">
                                        <asp:Button class="btn btn-info" ID="Button" runat="server" OnClick="Button_Click" Text="Upload" />
                                    </div>
                                </div>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="GridView2_RowCommand"
                    Width="687px">
                    <Columns>
                        <asp:TemplateField HeaderText="Nama File" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("File") %>'
                            CommandName="Download" Text='<%# Eval("File") %>'></asp:LinkButton>
                        </ItemTemplate></asp:TemplateField>
                        <asp:BoundField DataField="Size" HeaderText="Kapasitas File in Bytes" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" /></asp:GridView>
                </div></asp:Panel>

                <asp:Panel ID="PanelAwalMenu" runat="server" Visible="false" Style="">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelNoWO" runat="server" Visible="true" >No.</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtUpdNo" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <table width="100%" style="border: 0px solid #fff;background-color: transparent;">
                                    <tr id="PilihanDept" runat="server" visible="false">
                                        <td>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:Label ID="LabelDept" runat="server" Visible="true">Sub Dept. RnD</asp:Label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:DropDownList class="form-control" ID="ddlLabelDept" runat="server" Visible="true" AutoPostBack="True"></asp:DropDownList>
                                                    <div style="padding:2px;"></div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelJDokumen" runat="server" Visible="true">JenisDokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlJDoc" runat="server" Visible="true" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlJDoc_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">

                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelKD" runat="server" Visible="false" Style="">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelKD1" runat="server" Visible="true">Kategori Dokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:DropDownList class="form-control" ID="ddlKD1" runat="server" Visible="true" AutoPostBack="True" OnSelectedIndexChanged="ddlKD1_SelectedIndexChanged"></asp:DropDownList>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelKD2" runat="server" Visible="true" >Nama Dokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKD2" runat="server" ReadOnly="false"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelKD3" runat="server" Visible="true">Alasan Pengadaan</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKD3" runat="server" 
                                TextMode="MultiLine" Height="100px"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">

                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelPilihan" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-12">
                        <asp:RadioButton ID="RB11" runat="server" AutoPostBack="True" OnCheckedChanged="RB11_CheckedChanged"
                        Text="Biasa" TextAlign="Left" Width="100px" Font-Underline="True" />
                        <asp:RadioButton ID="RB22" runat="server" AutoPostBack="True" OnCheckedChanged="RB22_CheckedChanged"
                        Text="Khusus" TextAlign="Left" Width="100px" 
                        Font-Underline="True" />
                        <div style="padding: 2px;"></div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelPDK" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelPDK1" runat="server" Visible="true">Nama Dokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPDK1" runat="server" OnTextChanged="UPDTextChange" ReadOnly="false"
                                ToolTip="Masukkan Nama Dokumen diisini !!"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionInterval="200"
                                CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                MinimumPrefixLength="1" ServiceMethod="GetNamaKhusus" ServicePath="AutoCompleteUPD.asmx"
                                TargetControlID="txtPDK1" UseContextKey="true"></ajaxToolkit:AutoCompleteExtender>
                                <asp:Label ID="Label8" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelPDK3" runat="server" Visible="true">Alasan Perubahan</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPDK3" runat="server" TextMode="MultiLine" Height="137px"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label1" runat="server" Visible="false" Style="
                                font-size: x-small; font-weight: bold">&nbsp; Alasan Perubahan</asp:Label>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                Width="100%" OnRowCommand="GridView3_RowCommand"
                                AllowPaging="True" OnRowDataBound="GridView3_RowDataBound" OnPageIndexChanging="GridView3_PageIndexChanging"
                                PageSize="5" HorizontalAlign="Center" Style="margin-bottom: 5px; font-size: xx-small;"
                                BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                Font-Size="Smaller" Font-Bold="False" Font-Names="Calibri">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="true">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="NoDokumen" HeaderText="No.">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="UpdName" HeaderText="Nama">
                                    <HeaderStyle Width="75%" Wrap="False" /></asp:BoundField>
                                    <asp:BoundField DataField="RevisiNo" HeaderText="Rev">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="TglBerlaku" HeaderText="Tgl. Aktif" DataFormatString="{0:d}">
                                    <HeaderStyle Width="20%" /></asp:BoundField>
                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                </Columns>
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" Font-Size="Smaller" />
                                <HeaderStyle Font-Names="Aharoni" Font-Size="XX-Small" BackColor="#003399" BorderColor="#404040"
                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="#CCCCFF" Wrap="True" />
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Names="Calibri" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <EditRowStyle Font-Names="Calibri" Font-Size="XX-Small" /></asp:GridView>
                                <asp:Label ID="Label2" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold">&nbsp; Alasan Perubahan</asp:Label>
                                <asp:Label ID="Label9" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">

                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelPDB" runat="server" Visible="false" Style="">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelPDB1" runat="server" Visible="true">Nomor Dokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPDB1" runat="server" AutoPostBack="true" OnTextChanged="txtPDB1Change"
                                ReadOnly="false" ToolTip="Masukkan Nomor Dokumen diisini !!"></asp:TextBox>  
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                MinimumPrefixLength="1" ServiceMethod="GetNamaBiasa" ServicePath="AutoCompleteUPD.asmx"
                                TargetControlID="txtPDB1" UseContextKey="true"></ajaxToolkit:AutoCompleteExtender>
                                <asp:Label ID="Label5" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelPDB3" runat="server" Visible="false">Nama Dokumen</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPDB3" runat="server" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label6" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <asp:Label ID="Label3" runat="server" Visible="false" Style="
                                font-size: x-small; font-weight: bold"></asp:Label>
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                                Width="100%" OnRowCommand="GridView4_RowCommand"
                                AllowPaging="True" OnRowDataBound="GridView4_RowDataBound" OnPageIndexChanging="GridView4_PageIndexChanging"
                                PageSize="5" HorizontalAlign="Center" Style="margin-bottom: 5px; font-size: xx-small;"
                                BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                Font-Size="Smaller" Font-Bold="False" Font-Names="Calibri">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="true">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="NoDokumen" HeaderText="No.">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="UpdName" HeaderText="Nama">
                                    <HeaderStyle Width="75%" Wrap="False" /></asp:BoundField>
                                    <asp:BoundField DataField="RevisiNo" HeaderText="Rev">
                                    <HeaderStyle Width="5%" /></asp:BoundField>
                                    <asp:BoundField DataField="TglBerlaku" HeaderText="Tgl. Aktif" DataFormatString="{0:d}">
                                    <HeaderStyle Width="20%" /></asp:BoundField>
                                    <asp:ButtonField CommandName="Add" Text="Pilih" />
                                </Columns>
                                <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" Font-Size="Smaller" />
                                <HeaderStyle Font-Names="Aharoni" Font-Size="XX-Small" BackColor="#003399" BorderColor="#404040"
                                BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="#CCCCFF" Wrap="True" />
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Names="Calibri" Font-Size="XX-Small" />
                                <PagerStyle BorderStyle="Solid" BackColor="#99CCCC" ForeColor="#003399"  />
                                <EditRowStyle Font-Names="Calibri" Font-Size="XX-Small" /></asp:GridView>
                                <asp:Label ID="Label4" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelPDB2" runat="server" Visible="true">&nbsp; Alasan Perubahan</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtPDB2" runat="server" TextMode="MultiLine"
                                Height="100px"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" Visible="false" Style="
                                font-size: small; font-weight: bold"></asp:Label>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">

                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <div class="table-responsive">
                    <div style="height: 300px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                        PageSize="5" HorizontalAlign="Left" Style="margin-right: 40px; margin-bottom: 0px;"
                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False">
                            <HeaderStyle Width="5%" /></asp:BoundField>
                            <asp:BoundField DataField="UpdNo" HeaderText="No.">
                            <HeaderStyle Width="5%" /></asp:BoundField>
                            <asp:BoundField DataField="NoDokumen" HeaderText="No.Dokumen">
                            <HeaderStyle Width="5%" /> </asp:BoundField>
                            <asp:BoundField DataField="UpdName" HeaderText="Nama Dokumen">
                            <HeaderStyle Width="25%" Wrap="False" /> </asp:BoundField>
                            <asp:BoundField DataField="CategoryUPD" HeaderText="Kategori Dokumen">
                            <HeaderStyle Width="10%" /> </asp:BoundField>
                            <asp:BoundField DataField="JenisUPd" HeaderText="Jenis Dokumen">
                            <HeaderStyle Width="10%" /> </asp:BoundField>
                            <asp:BoundField DataField="TglPengajuan" HeaderText="Tgl. Pengajuan" DataFormatString="{0:d}">
                            <HeaderStyle Width="10%" /> </asp:BoundField>
                            <asp:BoundField DataField="Alasan" HeaderText="Alasan">
                            <ControlStyle Width="40%" />
                            <%-- <HeaderStyle Width="100px" Wrap="True" />--%>  </asp:BoundField>
                            <asp:BoundField DataField="StatusAPV" HeaderText="Status Aproval">
                            <HeaderStyle Width="8%" /> </asp:BoundField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="Dibuat Oleh">
                            <HeaderStyle Width="7%" />   </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="#003399" BorderColor="#404040"
                        BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="#CCCCFF" Wrap="True" />
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <PagerStyle BorderStyle="Solid" BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" /> </asp:GridView>
                    </div>
                </div>

            </div>
        </div>
    </div>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="Button" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
