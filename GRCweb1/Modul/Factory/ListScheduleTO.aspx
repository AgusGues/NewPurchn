<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListScheduleTO.aspx.cs" Inherits="GRCweb1.Modul.Factory.ListScheduleTO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <head>
                <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
                <meta charset="utf-8" />
                <title></title>

                <meta name="description" content="Common form elements and layouts" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

                <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
                <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
                <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.css" />
                <link rel="stylesheet" href="../../assets/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.css" />
                <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.dataTables.min.css" />
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

                <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h2 class="panel-title pull-left">DAFTAR SCHEDULE TO</h2>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <input id="btnUpdate" runat="server" class="btn btn-sm btn-info" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif'); background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Form Surat Jalan" onserverclick="btnUpdate_ServerClick" /></td>   
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:DropDownList ID="ddlSearch" class="form-control" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <input id="btnSearch" runat="server" class="btn btn-sm btn-primary" style="background-image: url('file:///D:/GRCBoardProject/GRCBoardProject/Modul/images/Button_Back.gif');
                              background-color: white; font-weight: bold; font-size: 11px;" 
                              type="button" value="Cari" />
                                    </span>
                                </div>
                            </div>
                            <div class="panel-body">

                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <div id="div2" style="height:400px;overflow:auto">
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                 Width="100%" onrowcommand="GridView1_RowCommand" 
                                   onrowdatabound="GridView1_RowDataBound" AllowPaging="true" 
                                   onpageindexchanging="GridView1_PageIndexChanging" PageSize="20"
                                   >
                                 <Columns>                                     
                                     <asp:BoundField DataField="ScheduleNo" HeaderText="No Schedule" />  
                                     <asp:BoundField DataField="ScheduleDate" HeaderText="Tgl Schedule" />
                                     <asp:BoundField DataField="TransferOrderNo" HeaderText="No Transfer Order" />
                                     <asp:BoundField DataField="TransferOrderDate" HeaderText="Tgl Transfer Order" />                                     
                                     <asp:BoundField DataField="FromDepo" HeaderText="Dari Depo" />
                                     <asp:BoundField DataField="ToDepo" HeaderText="Ke Depo" />
                                     <asp:BoundField DataField="FromDepoAddress" HeaderText="Dari Alamat Depo" />
                                     <asp:BoundField DataField="ToDepoAddress" HeaderText="Ke Alamat Depo" />
                                     <asp:ButtonField CommandName="Add" Text="Pilih" />
                                 </Columns>
                                 <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                 <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                 <PagerStyle BorderStyle="Solid" />
                                 <AlternatingRowStyle BackColor="Gainsboro" />
                             </asp:GridView>                          
                             </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
