<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalProject_Rev1.aspx.cs" Inherits="GRCweb1.Modul.Mtc.ApprovalProject_Rev1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                </script>
            </head>

            <body class="no-skin">
                <div class="row">
                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading clearfix">
                                <h3 class="panel-title pull-left">Approval Project Improvement</h3>
                                <div class="pull-right">
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnRefresh" class="btn btn-sm btn-info" runat="server" Text="Refresh"
                                            OnClick="btnRefresh_Click" Style="font-family: Calibri" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnPrev" class="btn btn-sm btn-success" runat="server" Enabled="false" Text="Sebelumnya"
                                            OnClick="btnPrev_Click" Style="font-family: Calibri" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnNext" class="btn btn-sm btn-success" runat="server" Text="Selanjutnya"
                                            OnClick="btnNext_Click" Style="font-family: Calibri" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="bntApprove" class="btn btn-sm btn-info" runat="server" Text="Approved"
                                            OnClick="bntApprove_Click" Style="font-family: Calibri" ToolTip="Tombol Approved " />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnCancel" class="btn btn-sm btn-danger" runat="server" Text="Not Approved"
                                            OnClick="btnCancel_Click" Visible="false" Style="font-family: Calibri" ToolTip="Tombol Cancel" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnRe" class="btn btn-sm btn-info" runat="server" Text="ReScheddule"
                                            OnClick="btnRe_Click" Visible="false" Style="font-family: Calibri" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnApproveEst" class="btn btn-sm btn-info" runat="server" Text="Approved"
                                            OnClick="btnApproveEst_Click" Visible="false" Style="font-family: Calibri" ToolTip="Tombol Approved Estimasi Material" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnTargetTujuan" class="btn btn-sm btn-info" runat="server" Text="Approved"
                                            OnClick="btnTargetTujuan_Click" Visible="false" Style="font-family: Calibri" ToolTip="Tombol Approved Target" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnApproveEstCancel" class="btn btn-sm btn-danger" runat="server" Text="Not Approved"
                                            OnClick="btnApproveEstCancel_Click" Visible="false" Style="font-family: Calibri" ToolTip="Tombol Cancel Estimasi Material" />
                                    </span>

                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnTargetTujuanCancel" class="btn btn-sm btn-danger" runat="server" Text="Not Approved"
                                            OnClick="btnTargetTujuanCancel_Click" Visible="false" Style="font-family: Calibri" ToolTip="Tombol Cancel Target Tujuan" />
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:TextBox ID="txtCari" Width="200px" Text="Find by Improvement"
                                            onfocus="if(this.value==this.defaultValue)this.value='';"
                                            onblur="if(this.value=='')this.value=this.defaultValue;" runat="server"
                                            placeholder="Find by Improvement" Style="font-family: Calibri" BackColor="Black" Font-Bold="True" Font-Italic="True"></asp:TextBox>
                                    </span>
                                    <span class="input-icon input-icon-right">
                                        <asp:Button ID="btnCari" class="btn btn-sm btn-info" runat="server" Text="Cari" OnClick="btnSearch_Click"
                                        style="font-family: Calibri" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <asp:HiddenField ID="txtID" runat="server" Value="0" />
                            <asp:HiddenField ID="txtJmlData" runat="server" Value="0" />
                            <div class="panel-body">
                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">No. Improvement</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNoImprovement" class="form-control" Width="80%" runat="server" 
                                            style="font-family: Calibri"></asp:TextBox>
                                            <asp:TextBox ID="txt1" Width="15%" class="form-control" runat="server" 
                                            style="font-family: Calibri; background-color: #99CCFF;" 
                                            BorderStyle="None"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Dept. Pemohon</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDeptPemohon" class="form-control" Width="100%" runat="server" 
                                            style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Dept. Penerima</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDept" class="form-control" Width="100%" runat="server" 
                                            style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Nama Improvement </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNamaImprovement" class="form-control" Width="80%" 
                                            TextMode="MultiLine" runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Quantity</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtQty" class="form-control" Width="100%" runat="server" 
                                            style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Sasaran Improvement</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtSasaran" class="form-control" Width="100%" runat="server" 
                                            style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Target / Tujuan</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDetailSasaran" class="form-control" TextMode="MultiLine" Width="80%" 
                                            runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Target Finished Date</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTglFinish" class="form-control" Width="200px" Text="Target belum di tentukan" onfocus="if(this.value==this.defaultValue)this.value='';"
                                            onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder="Target belum di tentukan"
                                            Style="font-family: Calibri">
                                        </asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTglFinish" Format="dd-MMM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Status</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtStatusApproval" class="form-control" runat="server" Style="font-family: Calibri" 
                                            Width="350px">&nbsp;</asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Dibuat Oleh</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtPembuat" class="form-control" runat="server" Style="font-family: Calibri" 
                                            Width="350px"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Tanggal</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtTanggal" class="form-control" runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <asp:TextBox ID="txtStatus" class="form-control" Width="10%" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtRowstatus" class="form-control" Width="10%" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtApproval" class="form-control" Width="10%" runat="server" Visible="false"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Improvement group</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtGroupName" class="form-control" runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <asp:TextBox ID="txtDeptID" class="form-control" Width="10%" runat="server" Visible="false"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Satuan</label>
                                        </div>
                                        <div class="col-md-8">
                                           <asp:TextBox ID="txtSatuan" class="form-control" runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Area Improvement </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtArea" class="form-control" runat="server" style="font-family: Calibri"></asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="form-field-9" style="font-size: 14px">Estimasi Biaya </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtBiaya" class="form-control" Width="200px" Text="" onfocus="if(this.value==this.defaultValue)this.value='';"
                                            onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" placeholder=""
                                            Style="font-family: Calibri">
                                        </asp:TextBox>
                                            <div style="padding: 2px"></div>
                                        </div>
                                    </div>
                                </div>
                                <asp:RadioButton ID="RBBekas" runat="server" AutoPostBack="True" GroupName="a" 
                                            OnCheckedChanged="RBBekas_CheckedChanged" 
                                            Style="font-family: Calibri; font-size: x-small; text-align: left;" 
                                            Text="Pakai barang bekas" TextAlign="Left" Visible="false" Width="150px" />
                            </div>
                        </div>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <asp:Label ID="LabelInfo" runat="server" Visible="false"                                             
                                            Style="font-family: Calibri;
                                            font-size: medium; font-weight: 700; color: #0000CC; background-color: #00FF00;">&nbsp;</asp:Label>&nbsp;&nbsp;
                                            
                                        <asp:Label ID="LabelInfo2" runat="server" Visible="false" Style="font-family: Calibri;
                                            font-size: medium; font-weight: 700" BackColor="Red" ForeColor="Yellow">&nbsp;</asp:Label>
                    </div>

                    <div class="col-xs-12 col-sm-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading ">
                                <h3 class="panel-title">List Estimasi Material</h3>
                            </div>
                            <div class="panel-body">
                                <div class="contentlist" style="height:300px">
                                <table style="width:100%; border-collapse:collapse; font-size:x-small">
                                    <tr class="tbHeader">
                                        <th class="kotak" style="width:3%; font-family: Calibri;">No.</th>
                                        <th class="kotak" style="width:10%; font-family: Calibri;">Item Code</th>
                                        <th class="kotak" style="width:35%; font-family: Calibri;">Item Name</th>
                                        <th class="kotak" style="width:5%; font-family: Calibri;">Unit</th>
                                        <th class="kotak" style="width:8%; font-family: Calibri;">Quantity</th>
                                        <th class="kotak" style="width:10%; font-family: Calibri;">Sch. Pakai</th>
                                        <th class="" style="background-color:Transparent">&nbsp;</th>
                                    </tr>
                                    <tbody style="font-family: Calibri">
                                        <asp:Repeater ID="lstMaterial" runat="server">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                    <td class="Kotak tengah"><%# Eval("UomCode") %></td>
                                                    <td class="kotak angka"><%# Eval("Jumlah","{0:N2}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Schedule","{0:d}") %></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>