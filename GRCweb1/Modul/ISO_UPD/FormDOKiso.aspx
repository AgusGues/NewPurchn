<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormDOKiso.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormDOKiso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>

<script language="JavaScript">
    function imgChange(img) {
        document.LookUpCalendar.src = img;
    }


</script>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Aktivasi Dokument New</span>
                <div class="pull-right">
                    <asp:HiddenField ID="txtIDMaster" runat="server" />
                    <asp:HiddenField ID="txtType" runat="server" />
                    <asp:HiddenField ID="txtDeptName" runat="server" />
                    <asp:HiddenField ID="txtDeptID" runat="server" />
                    <asp:HiddenField ID="txtNamaFile2" runat="server" />
                    <asp:HiddenField ID="txtKategoriUPD" runat="server" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">No Upd System</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNO" runat="server" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">KategoryDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDOK" runat="server" onkeyup="this.value=this.value.toUpperCase()"
                                ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">NamaDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNama" runat="server" TextMode="MultiLine" Height="50%" ReadOnly="true"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NoDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtISO" runat="server"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">RevisiKe</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtRev" runat="server"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TglMulaiBerlaku</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtBerlaku" runat="server"></asp:TextBox>                         
                                <cc1:CalendarExtender ID="CalendarExtender" TargetControlID="txtBerlaku" Format="dd-MMM-yyyy"
                                runat="server"></cc1:CalendarExtender>  
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <input class="btn btn-info" id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:Panel ID="PanelGrid" runat="server" Visible="false">
                    <table style="width: 100%;">                                                                                
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="4" OnRowCommand="GridView2_RowCommand" 
                                OnRowDataBound="GridView2_RowDataBound" Style="font-family: Calibri; font-size: 10pt;
                                font-weight: 500" Width="500px">
                                <Columns>
                                    <asp:TemplateField HeaderText="File" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                        CommandArgument='<%# Eval("File") %>' CommandName="Download" 
                                        Text='<%# Eval("File") %>'></asp:LinkButton>
                                    </ItemTemplate></asp:TemplateField>
                                    <asp:BoundField DataField="Date" HeaderText="Tanggal Upload">
                                    <HeaderStyle Width="100px" /></asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="Black" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" /></asp:GridView>
                            </td>                                                                                  
                        </tr>
                        <tr>
                            <td style="width: 143px">
                                &nbsp;
                            </td>
                        </tr>
                    </table></asp:Panel>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" 
                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true">
                        <HeaderStyle Width="5px" Font-Names="Calibri" Font-Size="X-Small" 
                        HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" /></asp:BoundField>
                        <asp:BoundField DataField="UpdNo" HeaderText="No. UPD System" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="10%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" /> </asp:BoundField>
                        <asp:BoundField DataField="NoDokumen" HeaderText="No.Dokumen" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="6%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="10%" Wrap="True" /> </asp:BoundField>
                        <asp:BoundField DataField="UpdName" HeaderText="Nama Dokumen" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="29%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />  </asp:BoundField>
                        <asp:BoundField DataField="CategoryUPD" HeaderText="Kategori Dokumen" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="15%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /> </asp:BoundField>
                        <asp:BoundField DataField="DeptID" HeaderText="Department" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="10%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />  </asp:BoundField>
                        <asp:BoundField DataField="TglPengajuan" DataFormatString="{0:d}" 
                        HeaderText="Tgl. Pengajuan" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="10%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />  </asp:BoundField>
                        <%--<asp:BoundField DataField="NamaFile" HeaderText="File Dokumen" />--%>
                        <asp:BoundField DataField="StatusUPD" HeaderText="Status" >
                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" HorizontalAlign="Center" 
                        VerticalAlign="Middle" Width="10%" />
                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /> </asp:BoundField>
                        <asp:ButtonField CommandName="Add" Text="Pilih" >
                        <HeaderStyle Font-Names="Curlz MT" Font-Size="X-Small" />
                        <ItemStyle Font-Bold="True" Font-Names="Brush Script MT" Font-Size="X-Small" 
                        HorizontalAlign="Center" VerticalAlign="Middle" />  </asp:ButtonField>
                        <%-- 
                        <asp:BoundField DataField="IDmaster" HeaderText="M" Visible="true">
                        <HeaderStyle Width="2px" Wrap="False" /></asp:BoundField>
                        <asp:BoundField DataField="Type" HeaderText="T" Visible="true">
                        <HeaderStyle Width="2px" Wrap="False" /> </asp:BoundField>
                        --%>
                        <%--<asp:BoundField DataField="Alasan" HeaderText="Alasan" Visible="false" />--%>
                    </Columns>
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                    BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                    ForeColor="white" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /> </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
