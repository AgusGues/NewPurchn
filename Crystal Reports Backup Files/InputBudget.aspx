<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputBudget.aspx.cs" Inherits="GRCweb1.Modul.Maintenance.InputBudget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<html lang="en">
    <head>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        
    </head>
<body class="no-skin">
    <div class="row">
		<div class="col-md-12">
            <div class="panel panel-primary">
				<div class=panel-heading>
                    <span class="the-title">Input Budget Maintenance</span>
                    <div class="pull-right">
                        <button class="btn btn-sm btn-info" type="button" onclick="baru()">New</button>
                        <button class="btn btn-sm btn-info" type="button" onclick="simpan()">Simpan</button>
                        <button class="btn btn-sm btn-info" type="button" onclick="hapus()">Hapus</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="row">
							    <div class="col-md-3">Tahun</div>
                                <div class="col-md-6">
                                    <input type="text" id="txttahun" class="col-sm-12 col-lg-12" autocomplete="off"/>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
							    <div class="col-md-3">Semester</div>
                                <div class="col-md-6">
                                    <select id="ddlsmt" class="col-sm-12 col-lg-12">
                                        <option value="I">Smt I</option>
                                        <option value="II">Smt II</option>
                                    </select>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
							    <div class="col-md-3">Total Budget</div>
                                <div class="col-md-6">
                                    <input type="text" id="txttotal" class="col-sm-12 col-lg-12" autocomplete="off"/>
                                     <input type="hidden" id="ID"  autocomplete="off">
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                        </div>
                    </div>
                </div>
            </div>
             <div class="panel panel-primary">
                <div class="panel-body panel-list">
					<div class="row">
                        <div class="col-md-6">
                            <div class="table-responsive">
                                <table id="tablelist" class="table table-striped table-bordered table-hover" >
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Tahun</th>
                                            <th>Smt</th>
                                            <th>Jumlah</th>
                                            <th>Created</th>
                                            <th> </th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
 
<script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../Scripts/jquery.blockui.min.js"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

 <script src="../../Scripts/Maintenance/InputBudget.js" type="text/javascript"></script>
    
</html>
</asp:Content>
