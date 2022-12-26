<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListSPKP.aspx.cs" Inherits="GRCweb1.Modul.spkp.ListSPKP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!DOCTYPE html>
    <html lang="en">
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <title>Report SPKP</title>
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/chosen.min.css" />

    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">   
                    <div class="panel-heading">
                        List SPKP
                        <div class="pull-right">
                            <div class="col-md-12">
                                <button class="btn-xs btn-success" type="button" onclick="Form()">Input SPKP</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div  class="row">
                        <div class="col-md-12" id="listspkp">
                            <table id="tablelist" class="table table-striped table-bordered table-hover" border="1">
                                <thead>
                                    <tr>
                                        <th class="text-center">No</th>
                                        <th class="text-center">No SPKP</th>
                                        <th class="text-center">Action</th>
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
        <div id="modallistspkp" class="modal fade" tabindex="-1">
		    <div class="modal-dialog1">
			    <div class="modal-content">
				    <div class="modal-header text-center">
					    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					    <h3 class="smaller lighter blue no-margin">SPKP</h3>
				    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            No SPKP
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="nospkp" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            Line
                                        </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <select class=" form-control" id="ddlline" onchange="loadspkp()">
                                                    <option value="0">Pilih Line</option>
                                                    <option value="1">Line 1</option>
                                                    <option value="2">Line 2</option>
                                                    <option value="3">Line 3</option>
                                                    <option value="4">Line 4</option>
                                                    <option value="5">Line 5</option>
                                                    <option value="6">Line 6</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding: 30px"></div>
                                <table id="tablespkp" class="table table-striped table-bordered table-hover" border="1">
                                    <thead>
                                        <tr>
                                            <th class="text-center">Tanggal</th>
                                            <th class="text-center">Shift</th>
                                            <th class="text-center">Kategori</th>
                                            <th class="text-center">Tebal</th>
                                            <th class="text-center">Ukuran</th>
                                            <th class="text-center">Target</th>
                                            <th class="text-center">Keterangan</th>
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
         <div id="modalupdatespkp" class="modal fade" tabindex="-1">
		    <div class="modal-dialog">
			    <div class="modal-content">
				    <div class="modal-header text-center">
					    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					    <h3 class="smaller lighter blue no-margin">Update SPKP</h3>
				    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="hidden" class="input-sm form-control" id="ID" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Tanggal
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Tanggal"disabled />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Shift
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Shift" disabled/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Kategori
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Kategori" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Tebal
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Tebal" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Ukuran
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Ukuran" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Target
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Target" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                             Keterangan
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" class="input-sm form-control" id="Keterangan" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="col-md-12">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <button class="btn-xs btn-success" type="button" onclick="update()">Save</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 20px"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>          
        <div class="panel-body boxShadow the-loader" >
            <div style="padding:7%;"></div>
            <i class="fa fa-spinner fa-spin"></i>
        </div>
    </body>
<script src="../../Scripts/SPKP/ListSPKP.js" type="text/javascript"></script>
<script src="../../assets/js/jquery-ui.min.js"></script>
<script src="../../Scripts/jquery.blockui.min.js"></script>
<script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
<script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

</html>
    <style>
        .modal-dialog1 {
            width: 1200px;
            margin: 30px auto;
        }
        .table thead,
        .table th {text-align: center;
        }
    </style>
</asp:Content>
