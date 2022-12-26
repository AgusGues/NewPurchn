<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormUpdateMasterTanggal.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormUpdateMasterTanggal" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

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
                <span>Update Tanggal Berlaku Dan Revisi Master Dokument</span>
                <div class="pull-right">
                    <input class="btn btn-info" id="btnUpdate" runat="server" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');" 
                    type="button" value="Update" onserverclick="btnUpdate_ServerClick" />
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">KatergoryDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" width="100%" ID="txtK" runat="server" onkeyup="this.value=this.value.toUpperCase()"
                                TabIndex = "1" Enabled="False" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">TglMulaiBerlaku</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtT" runat="server" 
                                TabIndex = "7"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender" TargetControlID="txtT" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>  
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                       <div class="row">
                            <div class="col-md-4">RevisiKe</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtR" runat="server"
                                TabIndex = "5" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">No.Form</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNoF" runat="server"
                                TabIndex = "5"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
                    Width="100%" style="font-family: Calibri; font-size: medium;">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                        <asp:BoundField DataField="idCategory" HeaderText="Kategori Dokumen" />
                        <asp:BoundField DataField="Tanggal" DataFormatString="{0:d}" HeaderText="Tanggal Mulai Berlaku" />
                        <asp:BoundField DataField="RevNo" HeaderText="Revisi Ke" /> 
                        <asp:BoundField DataField="FormNO" HeaderText="No. Form" />                                                                  <asp:ButtonField CommandName="Add" Text="Pilih" />                                                                    
                    </Columns>
                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                    BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" 
                    ForeColor="white" />
                    <PagerStyle BorderStyle="Solid" />
                    <AlternatingRowStyle BackColor="Gainsboro" /></asp:GridView>
                </div>
            </div>
        </div>
    </div>
</ContentTemplate> 
</asp:UpdatePanel>     
</asp:Content>
