<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InputPemakaianListrik.aspx.cs" Inherits="GRCweb1.Modul.Maintenance.InputPemakaianListrik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
<html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Simetris</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

        <link rel="stylesheet" href="../../assets/css/daterangepicker.min.css" />
		
    </head>
    <body class="no-skin">
        <div class="row">
             <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                     <div class="panel-heading">
                        <h3 class="panel-title">Input pemakaian Listrik</h3>
                    </div>
                    <div class="panel-body">

                        <div class="col-lg-12">
                            <div class="col-lg-1">
                                Tanggal
                            </div>
                            <div class="col-lg-2">
                                <div class="input-group">
                                    <input class="form-control date-picker"  type="text" id="txttgl" onchange ="aktif()" autocomplete="off">
										<span class="input-group-addon">
											<i class="fa fa-calendar bigger-110"></i>
										</span>
								</div>
                            </div>
                            <div class="col-lg-2">

                            </div>
                             <div class="col-lg-1">
                                 Line
                            </div>
                             <div class="col-lg-1">
                                  <input type="text" id="txtline" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                             <div class="col-lg-3">

                            </div>
                             <div class="col-lg-2">
                                 <asp:Label ID="keterangan1" runat="server"></asp:Label>
                                 <%--Ket : 1 s/d 4 target 110%--%> <br />
                                 &nbsp  &nbsp  &nbsp &nbsp&nbsp 
                                 <asp:Label ID="keterangan2" runat="server"></asp:Label>
                                 <%--5 s/d 6 target 100%--%>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <br />
                        </div>

                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-4 text-center">
                               <h3><b>Stand</b></h3> 
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-4 text-center">
                                <h3><b>Penyesuaian</b></h3>
                            </div>
                           
                        </div>
                        <div class="col-lg-12">
                            <br />
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2 text-center">
                                kWh
                            </div>
                            <div class="col-lg-2 text-center">
                                kVarh
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2 text-center">
                                kWh
                            </div>
                            <div class="col-lg-2 text-center">
                                kVarh
                            </div>
                             <div class="col-lg-2 text-center">
                                keterangan
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                                PJT
                            </div>
                            <div class="col-lg-2">
                                <input type="text" id="txtkwhpjt" class="col-sm-12 col-lg-12" autocomplete="off" />
                            </div>
                             <div class="col-lg-2">
                                <input type="text" id="txtkvarhpjt" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2">
                                <input type="text" id="txtkwhpjt2" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                             <div class="col-lg-2">
                                <input type="text" id="txtkvarhpjt2" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                            <div class="col-lg-2">
                                <input type="text" id="txtketerangan" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <br />
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                                PLN
                            </div>
                            <div class="col-lg-2">
                                <input type="text" id="txtkwhpln" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                             <div class="col-lg-2">
                                <input type="text" id="txtkvarhpln" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2">
                                <input type="text" id="txtkwhpln2" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                             <div class="col-lg-2">
                                <input type="text" id="txtkvarhpln2" class="col-sm-12 col-lg-12" autocomplete="off"/>
                            </div>
                        </div>
                         <div class="col-lg-12">
                            <br />
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                               <button class="btn btn-sm btn-primary" type="button" onclick="InputPemakaian()">
                                   Input
                                </button>
                            </div>
                            <div class="col-lg-3">
                                <button class="btn btn-sm btn-danger" type="button" onclick="UpdatePemakaian()">
                                   Update
                                </button>
                            </div>
                            <div class="col-lg-2">
                               <button class="btn btn-sm btn-success" type="button" id="btnup" onclick="Penyesuaian()">
                                   <i class="ace-icon fa fa-plus bigger-110"></i>Input
                                </button>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 form-group-sm">
                <div class="widget-box">
                    <div class="panel-heading">
                        <h3 class="panel-title">Rekap Pemakaian Energi Listrik</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                Periode
                            </div>
                            <div class="col-lg-4">
                                 <select id="ddlbulan" class="col-sm-12 col-lg-10">
                                     <option value="">Pilih Bulan</option>
                                     <option value="01">Januari</option>
                                     <option value="02">Februari</option>
                                     <option value="03">Maret</option>
                                     <option value="04">April</option>
                                     <option value="05">Mei</option>
                                     <option value="06">Juni</option>
                                     <option value="07">Juli</option>
                                     <option value="08">Agustus</option>
                                     <option value="09">September</option>
                                     <option value="10">Oktober</option>
                                     <option value="11">November</option>
                                     <option value="12">Desember</option>
                                </select>
                            </div>
                            <div class="col-lg-3">
                                <input type="text" id="txttahun" class="col-sm-12 col-lg-10" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                             <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                               <button class="btn btn-sm btn-info" type="button" onclick="RekapPemakaian()">
                                    <i class="ace-icon fa fa-search bigger-110"></i>Preview
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 form-group-sm">
                <div class="widget-box">
                    <div class="panel-heading">
                        <h3 class="panel-title">Rekap Efisiensi Listrik</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                Periode
                            </div>
                            <div class="col-lg-4">
                                 <select id="ddlbulan2" class="col-sm-12 col-lg-10">
                                     <option value="">Pilih Bulan</option>
                                     <option value="01">Januari</option>
                                     <option value="02">Februari</option>
                                     <option value="03">Maret</option>
                                     <option value="04">April</option>
                                     <option value="05">Mei</option>
                                     <option value="06">Juni</option>
                                     <option value="07">Juli</option>
                                     <option value="08">Agustus</option>
                                     <option value="09">September</option>
                                     <option value="10">Oktober</option>
                                     <option value="11">November</option>
                                     <option value="12">Desember</option>
                                </select>
                            </div>
                            <div class="col-lg-3">
                                <input type="text" id="txttahun2" class="col-sm-12 col-lg-10" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                             <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                               <button class="btn btn-sm btn-info" type="button" onclick="RekapEfisiensi()">
                                    <i class="ace-icon fa fa-search bigger-110"></i>Preview
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalpemakaian" class="modal fade" tabindex="-1">
				<div class="modal-dialog1">
				    <div class="modal-content">
						<div class="modal-header text-center">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
							<h3 class="smaller lighter blue no-margin">Rekap Pemakaian Energi Listrik</h3>
						</div>
                        <div class="modal-body">
                            <iframe id="txtArea1" style="display:none"></iframe>
                            <button id="btnExport" onclick="fnExcelReport();"> EXPORT </button>
						    <button  onclick="printData()"> Print </button>
						    <div class="table-responsive">
                                <div id="tabel">
                                    <div class="col-lg-12">Rekap</div>
                                    <table id="tablepemakaian" class="table table-striped table-bordered table-hover" border="1">
                                        <thead>
                                            <tr>
                                                <th rowspan="3">Tanggal</th>
                                                <th colspan="4">PJT</th>
                                                <th colspan="4">PLN</th>
                                                <th rowspan="3">Pemakaian <br /> Harian <br /> kWh Total</th>
                                                <th rowspan="3">Pemakaian <br /> Harian <br /> kVarh Total</th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">kWh</th>
                                                <th colspan="2">kVarh</th>
                                                <th colspan="2">kWh</th>
                                                <th colspan ="2">kVarh</th>
                                            </tr>
                                            <tr>
                                                <th>Stand</th>
                                                <th>Terpakai</th>
                                                <th>Stand</th>
                                                <th>terpakai</th>
                                                <th>Stand</th>
                                                <th>Terpakai</th>
                                                <th>Stand</th>
                                                <th>terpakai</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                        </tbody>
                                        <tbody>
                                            <tr>
                                               <td class="text-right">Total</td>
                                               <td></td>
                                               <td id="result1"></td>
                                               <td></td>
                                               <td id="result2"></td>
                                               <td></td>
                                               <td id="result3"></td>
                                               <td></td>
                                               <td id="result4"></td>
                                               <td id="totalkwh"></td>
                                               <td id="totalkvarh"></td>
                                            </tr>
                                        </tbody>
                                </table>
                            </div>
                        </div>
                        </div>
                        <div class="modal-footer">
							<button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
								<i class="ace-icon fa fa-times"></i>
								Exit
						    </button>
						</div>
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div>
            <div id="modalefisiensi" class="modal fade" tabindex="-1">
				<div class="modal-dialog1">
				    <div class="modal-content">
						<div class="modal-header text-center">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
							<h3 class="smaller lighter blue no-margin">Rekap Pemantauan Efisiensi Pemakaian Listrik</h3>
						</div>
                        <div class="modal-body">
						    <div class="table-responsive">
                            <table id="tableEfisiensi" class="table table-striped table-bordered table-hover" >
                                <thead>
                                    <tr>
                                        <th>Tanggal</th>
                                        <th>kWh Terpakai</th>
                                        <th>kVarh Terpakai</th>
                                        <th>Output <br />Produksi</th>
                                        <th>kWh/M3</th>
                                        <th>Prosentase (%)</th>
                                        <th>Keterangan</th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                        </div>
                        <div class="modal-footer">
							<button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
								<i class="ace-icon fa fa-times"></i>
								Exit
						    </button>
						</div>
					</div><!-- /.modal-content -->
				</div><!-- /.modal-dialog -->
			</div>
        </div>
    </body>
     <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    
        <script src="../../Scripts/Maintenance/RekapListrik.js" type="text/javascript"></script>
    
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
