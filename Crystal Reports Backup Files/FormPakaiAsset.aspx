<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiAsset.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiAsset" %>
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
        <style>
        .panelbox {background-color: #efeded;padding: 2px;}
        html,body,.form-control,button{font-size: 11px;}
        .input-group-addon{background: white;}
        .fz11{font-size: 11px;}
        .fz10{font-size: 10px;}
        .the-loader{
            position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
            text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
        }
        .input-xs{
            font-size: 11px;height: 11px;
        }
    </style>
		
    </head>

    <body class="no-skin">
        <div class="row">
             <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">
                     <div class="panel-heading">
                        Asset Drawing Slip
                         <div class="pull-right">
                            
                            <div class="col-md-6">
                                <button class="btn-xs btn-success" type="button" onclick="Baru()">Baru</button>
                                &nbsp
                                <button class="btn-xs btn-success" type="button" onclick="Simpan()">Simpan</button>
                                &nbsp
                                <button class="btn-xs btn-success" id="btncancel" type="button" onclick="Cancel()">Cancel</button>
                                &nbsp
                                <button class="btn-xs btn-success" id="btnprint" type="button" onclick="Cetak()">Cetak</button>
                                &nbsp
                                <button class="btn-xs btn-success" type="button" onclick="List()">List</button>
                            </div>
                            <div class="col-md-2">
                                <select class=" form-control" id=""><option value=""> ADS No </option></select>
                            </div>
                            <div class="col-md-3">
                                 <input type="text" class="input-sm form-control" name="" id="txtcari"/>
                            </div>
                            <div class="col-md-1">
                                <button class="btn-xs btn-success" type="button" onclick="Load()">Cari</button>
                             </div>
                        </div>
                    </div>
                    <div class="panel-body">
                       <div  class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            No Ads
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtpakaino"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Kode Dept
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtkodedept"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                             Nama Dept
                                        </div>
                                        <div class="col-md-6">
                                            <select class=" form-control" id="ddldept" onchange="getdept(value)">
                                                <option value="0"> - - Pilih Dept - - </option>
                                            </select>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                        <div class="row">
                                        <div class="col-md-3">
                                             Cari Barang
                                        </div>
                                        <div class="col-md-9">
                                            <input type="text" class="input-sm form-control" name="" id="txtNama" />
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Nama Barang
                                        </div>
                                        <div class="col-md-9">
                                            <select class=" form-control" id="ddlnama" onchange="getkode(value)">
                                                <option value=""> </option>
                                            </select>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Kode Barang
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtkodebarang"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Satuan
                                         </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtsatuan"/>
                                        </div>
                                    </div>
                                    <div style="padding: 10px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Status Asset
                                        </div>
                                        <div class="col-md-6">
                                             <select class=" form-control" id="ddlStatusAsset" onchange="statuschange(value)">
                                                <option value="0"> - - Pilih Status - - </option>
                                                <option value="1">Baru </option>
                                                <option value="2">Pengganti</option>
                                            </select>
                                         </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Sp Group
                                        </div>
                                        <div class="col-md-6">
                                             <select class=" form-control" id="ddlsp">
                                                <option value="0">  </option>
                                                     </select>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-3">
                                            Stok
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtstok" />
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Tanggal
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="form-control input-sm date-picker" id="tanggal"/>
                                         </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Tipe Asset
                                        </div>
                                        <div class="col-md-6">
                                            <select class=" form-control" id="ddltipe">
                                                <option value=""> - - Pilih Tipe Asset - - </option>
                                            </select>
                                        </div>
                                    </div>
                                    <div style="padding: 40px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Qty Pakai
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtpakai"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Keterangan
                                        </div>
                                        <div class="col-md-6">
                                            <textarea id="form-field-11" class="autosize-transition form-control" style="overflow: hidden; overflow-wrap: break-word; resize: horizontal; height: 50px;"></textarea>
                                        </div>
                                        <div class="col-md-1">
                                            <input type="text" class="input-sm form-control" name="" id="txtGroupID" style="display:none"/>
                                        </div>
                                        <div class="col-md-1">
                                            <input type="text" class="input-sm form-control" name="" id="txtUOMID" style="display:none"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Nomor Asset
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="txtnoasset"/>
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            Dibuat Oleh
                                        </div>
                                        <div class="col-md-6">
                                            <input type="text" class="input-sm form-control" name="" id="CreatedBy" />
                                        </div>
                                    </div>
                                    <div style="padding: 2px"></div>
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-6">
                                            <button class="btn btn-sm btn-primary" id="btntambah" type="button" onclick="tambah()">
                                                <i class="ace-icon fa glyphicon-plus  bigger-110"></i>Tambah
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-xs-12 form-group-sm" id="Tbldetail">
                <div class="panel panel-primary">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table id="tabledetail" class="table table-striped table-bordered table-hover" >
                               <thead>
                                    <tr>
                                        <th>Nama Barang</th>
                                        <th>Kode Barang</th>
                                        <th>Jumlah</th>
                                        <th>UOM</th>
                                        <th>Keterangan</th>
                                    </tr>
                                </thead>
                            </table>
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
        <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.js"></script>
    <script src="../../assets/Datatables/FixedHeader-3.1.8/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    
    <script src="../../Scripts/Purchasing/pakaiasset.js" type="text/javascript"></script>
    <script src="../../assets/js/daterangepicker.min.js"></script>

</html>

</asp:Content>
