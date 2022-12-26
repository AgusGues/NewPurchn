<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalProject.aspx.cs" Inherits="GRCweb1.Modul.Maintenance.Aproval_Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Approve Project</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    </head>
    <body class="no-skin">

        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">APPROVAL PROJECT IMPROVEMENT</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">

                            <div class="col-xs-12 col-sm-6">
                                <input type="text" id="cari" placeholder="Search Improvement" size="50" style="font-weight: bold" />
                                <button class='btn btn-success' id="btncari" type='button'><i class='fa fa-check'></i>Search </button>
                            </div>
                            <div class="wizard-actions">

                                <button id="prev" type="button" class="btn btn-prev" disabled="disabled">
                                    <i class="ace-icon fa fa-arrow-left"></i>
                                    Prev
                                </button>
                                <button id="next" type="button" class="btn btn-primary btn-next" data-last="Finish">
                                    Next
                                    <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
                                </button>
                                <button class='btn btn-success' id="approve" type='button'><i class='fa fa-check'></i>Approve </button>
                                <button class='btn btn-danger' id="notapprove" type='button'><i class='fa fa-times'></i>Not Approve </button>
                                <button class='btn btn-danger' id="notapproveest" type='button'><i class='fa fa-times'></i>Not Approve </button>
                                <button class='btn btn-inverse' id="reschedule" type='button'><i class="fa fa-calendar"></i>ReSchedule </button>
                            </div>

                            <hr />
                            <div class="col-xs-12 col-sm-6">
                                <div>
                                    <label for="form-field-9">No. Improvement</label>
                                    <div class="input-group">
                                        <input type="text" id="noimprove" placeholder="No. Improvement" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Dept. Pemohon</label>
                                    <div>
                                        <input type="text" id="pemohon" placeholder="pemohon" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Dept. Penerima</label>
                                    <div>
                                        <input type="text" id="penerima" placeholder="Penerima" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Nama Improvement</label>
                                    <div>
                                        <textarea class="form-control" id="improve" placeholder="Nama Improvement" style="font-weight: bold"></textarea>
                                    </div>
                                    <label for="form-field-9">Quantity</label>
                                    <div>
                                        <input type="text" id="qty" placeholder="Quantity" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Sasaran Improvement</label>
                                    <div>
                                        <input type="text" id="sasaran" placeholder="Sasaran Improvement" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Target/Tujuan</label>
                                    <div>
                                        <textarea class="form-control" id="target" placeholder="Target/Tujuan" style="font-weight: bold"></textarea>
                                    </div>
                                    <label for="form-field-9">Satuan</label>
                                    <div>
                                        <input type="text" id="satuan" placeholder="Satuan" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Area Improvement</label>
                                    <div>
                                        <textarea class="form-control" id="area" placeholder="Default Text" style="font-weight: bold"></textarea>
                                    </div>
                                    <div class="checkbox" id="barang">
                                        <label>
                                            <input name="form-field-checkbox" id="barangbekas" type="checkbox" class="ace">
                                            <span class="lbl">Pakai Barang Bekas</span>
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <div>
                                    <label for="form-field-9">Tanggal</label>
                                    <div>
                                        <input type="text" id="tanggal" placeholder="Tanggal" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Target Finished Date</label>
                                    <div class="input-group">
                                        <input class="form-control date-picker" id="finished" type="text" style="font-weight: bold">
                                        <span class="input-group-addon">
                                            <i class="fa fa-calendar bigger-110"></i>
                                        </span>
                                    </div>
                                    <label for="form-field-9">Status</label>
                                    <div>
                                        <input type="text" id="status" placeholder="Status" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Dibuat Oleh</label>
                                    <div>
                                        <input type="text" id="dibuat" placeholder="Dibuat Oleh" size="50" style="font-weight: bold" />
                                    </div>
                                    <label for="form-field-9">Improvement group</label>
                                    <div>
                                        <input type="text" id="group" placeholder="Improvement group" size="50" style="font-weight: bold" />
                                    </div>

                                    <label for="form-field-9">Estimasi Biaya</label>
                                    <div>
                                        <input type="text" id="biaya" placeholder="Estimasi Biaya" size="50" style="font-weight: bold" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="id" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="hidestatus" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="hiderowstatus" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="hideapv" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="hidedeptid" />
                                    </div>
                                    <div>
                                        <input type="hidden" id="hideapvpm" />
                                    </div>
                                    <hr />
                                    <div class="alert alert-info" id="info" style="font-weight: bold;">
                                    </div>
                                    <div class="alert alert-danger" id="danger" style="font-weight: bold;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">LIST ESTIMASI MATERIAL</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tableListmaterial" class="table table-striped table-hover table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>No </th>
                                            <th>Item Code</th>
                                            <th>Item Name</th>
                                            <th>Unit</th>
                                            <th>Quantity</th>
                                            <th>Harga</th>
                                            <th>Scehdule Pakai</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
          <script src="../../assets/js/jquery.min.js"></script>
        <script src="../../assets/js/bootstrap.min.js"></script>

    <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/js/jquery.dataTables.min.js"></script>
    <script src="../../assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="../../assets/js/dataTables.responsive.min.js"></script>
    <script src="../../assets/js/dataTables.tableTools.min.js"></script>
    <script src="../../assets/js/dataTables.colVis.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    <script src="../../Scripts/Maintenance/ApproveProject.js"></script>

    </html>
    <style>
    </style>
</asp:Content>