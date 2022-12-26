<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="T1Adjust.aspx.cs" Inherits="GRCweb1.Modul.Factory.T1Adjust" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>Tahap 1 Adjustment</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Input Proses Adjustment</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-12 col-sm-6 form-group-sm">
<%--                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">No Adjust</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="noadjust" disabled>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>--%>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Tanggal Adjust</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="tgladjust" name="tgladjust" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">No Berita Acara</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="noba" style="text-transform:uppercase"/>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Keterangan</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="keterangan" style="text-transform:uppercase"/>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 form-group-sm">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Tanggal Produksi</label>
                                </div>
                                <div class="col-md-8">
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="tglproduksi" name="tglproduksi" type="text">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Partno</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="partno" style="text-transform:uppercase">
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Lokasi</label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="lokasi" style="width: 25%;text-transform:uppercase" />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="form-field-9">Qty Adjust</label>
                                </div>
                                <div class="col-md-2">
                                    <input type="text" id="qtyadjust" style="width: 75%" />
                                    <div style="padding: 2px"></div>
                                </div>
                                <div class="col-md-4">
                                    <label for="form-field-9">Qty Produksi</label>
                                </div>
                                <div class="col-md-2">
                                    <input type="text" id="qtyprod" style="width: 75%" disabled />
                                    <div style="padding: 2px"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer padding-8 clearfix">
                        <input type="radio" id="adjustin" name="adjust" value="In" checked>
                        <label for="html" style="padding: 5px">Adjust In</label>
                        <input type="radio" id="adjustout" name="adjust" value="Out" >
                        <label for="adjustout" style="padding: 5px">Adjust Out</label>
                        
                        <button type='button' id="simpanpartno" class="btn btn-primary pull-right">
                            <span class="bigger-110">Simpan</span>
                            <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                        </button>

                        <button type='button' id="addpartno" class="btn btn-success pull-right">
                            <span class="bigger-110">Add</span>
                            <i class="ace-icon fa fa-plus icon-on-right"></i>
                        </button>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tablereadd" class="table table-striped table-hover display nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Tanggal Adjust</th>
                                            <th>BA</th>
                                            <th>Type</th>
                                            <th>Partno</th>
                                            <th>Tanggal Produksi</th>
                                            <th>Lokasi</th>
                                            <th>Qty In</th>
                                            <th>Qty Out</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="list-dtl"></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">List Adjustment</h3>

                    </div>
                    <div class="panel-body">
                               <input type="radio" id="tgl" name="listtable" value=1 checked>
                        <label for="Tanggal" style="padding: 5px">Tanggal</label>
                        <input type="radio" id="approval" name="listtable" value=2 >
                        <label for="Approval" style="padding: 5px">Approval</label>
                        <div>
                                   <button type='button' id="approve" class="btn btn-success">
                            <span class="bigger-110">Approve</span>
                        </button>
                        </div>
                  
                        <div style="width: 100%;" >
                            <div class="table-responsive" id="listadjust">
                                <table id="tablelistadjust" class="table table-striped table-hover display nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Tanggal Adjust</th>
                                            <th>Adjust No</th>
                                            <th>BA</th>
                                            <th>Type</th>
                                            <th>Partno</th>
                                            <th>Tanggal Produksi</th>
                                            <th>Lokasi</th>
                                            <th>Qty In</th>
                                            <th>Qty Out</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>


                                                <div style="width: 100%;" >
                            <div class="table-responsive" id="listapv">
                                <table id="tablelistapv" class="table table-striped table-hover display nowrap" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
														<label class="pos-rel">
															<input type="checkbox" class="ace" />
															<span class="lbl"></span>
														</label>
													</th>
                                            <th>Tanggal Adjust</th>
                                            <th>No Adjust</th>
                                            <th>BA</th>
                                            <th>Type</th>
                                            <th>Partno</th>
                                            <th>Tanggal Produksi</th>
                                            <th>Lokasi</th>
                                            <th>Qty In</th>
                                            <th>Qty Out</th>
                                            <th>Approval</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="loader" id="loading"></div>
    </body>

    <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.min.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    <script src="../../assets/jquery.mask.min.js"></script>
    <script src="../../assets/terbilang.js"></script>
    <script src="../../Scripts/Factory/T1Adjust.js" type="text/javascript"></script>
    </html>

        <style>
.ui-datepicker {
    width: 23em;
}
       .loader {
            z-index: 9999;
            position: fixed;
            top: 50%;
            left: 50%;
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 100px;
            height: 100px;
            animation: spin 2s linear infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
            </style>
</asp:Content>