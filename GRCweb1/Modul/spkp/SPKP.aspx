<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SPKP.aspx.cs" Inherits="GRCweb1.Modul.SPKP.SPKP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />

        <title>SPKP</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/chosen.min.css" />
        <style>
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }
        </style>
    </head>

    <body class="no-skin">
        <div class="row">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        SPKP
                        <div class="pull-right">
                            <div class="col-md-12">
                                <button class="btn-xs btn-success" type="button" onclick="Baru()">Baru</button>
                                &nbsp
                                <button class="btn-xs btn-success" type="button" onclick="Simpan()">Simpan</button>
                                &nbsp
                                <button class="btn-xs btn-success" type="button" onclick="List()">List</button>
                                &nbsp
                                 <button class="btn-xs btn-success" type="button" onclick="report()" id="btnreport">Report SPKP</button>
                                &nbsp
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="input">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-1">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            Tanggal
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="input-group">
                                            <input class="form-control date-picker" onchange="getday()" type="text" id="txttgl" autocomplete="off">
                                            <span class="input-group-addon">
                                                <i class="fa fa-calendar bigger-110"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            Line
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <select class=" form-control" id="ddlline">
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
                                <div class="col-md-1">
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
                            </div>

                            <div style="padding: 30px"></div>

                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table id="tblinput" class="table">
                                        <thead>
                                            <tr>
                                                <th class="text-center">Tanggal</th>
                                                <th>Shift</th>
                                                <th class="text-center">Kategori</th>
                                                <th class="text-center">Tebal</th>
                                                <th class="text-center">Ukuran</th>
                                                <th class="text-center">Target</th>
                                                <th class="text-center">Keterangan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th class="text-center">
                                                    <select class="chosen-select form-control kategori" id="ddlkategori1a" data-placeholder="pilih" onchange="setkategori1a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th class="text-center">
                                                    <select class="chosen-select form-control tebal" id="ddltebal1a" data-placeholder="Pilih" onchange="settebal1a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th class="text-center">
                                                    <select class="chosen-select form-control ukuran" id="ddlukuran1a" data-placeholder="Pilih" onchange="setUkuran1a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th class="text-center">
                                                    <input type="text" class="input-sm form-control target" name="" id="1a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="1aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori1b" onchange="setkategori1b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal1b" onchange="settebal1b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran1b" onchange="setUkuran1b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="1b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="1bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori2a" onchange="setkategori2a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal2a" onchange="settebal2a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran2a" onchange="setUkuran2a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="2a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="2aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control kategori" id="ddlkategori2b" onchange="setkategori2b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class=" form-control tebal" id="ddltebal2b" onchange="settebal2b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran2b" onchange="setUkuran2b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="2b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="2bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori3a" onchange="setkategori3a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal3a" onchange="settebal3a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran3a" onchange="setUkuran3a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="3a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="3aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal1f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori3b" onchange="setkategori3b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal3b" onchange="settebal3b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran3b" onchange="setUkuran3b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="3b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="3bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="1" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori4a" onchange="setkategori4a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal4a" onchange="settebal4a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran4a" onchange="setUkuran4a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="4a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="4aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori4b" onchange="setkategori4b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal4b" onchange="settebal4b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran4b" onchange="setUkuran4b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="4b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="4bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori5a" onchange="setkategori5a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal5a" onchange="settebal5a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran5a" onchange="setUkuran5a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="5a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="5aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori5b" onchange="setkategori5b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal5b" onchange="settebal5b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran5b" onchange="setUkuran5b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="5b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="5bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori6a" onchange="setkategori6a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal6a" onchange="settebal6a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran6a" onchange="setUkuran6a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="6a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="6aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori6b" onchange="setkategori6b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal6b" onchange="settebal6b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran6b" onchange="setUkuran6b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="6b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="6bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="2" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori7a" onchange="setkategori7a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal7a" onchange="settebal7a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran7a" onchange="setUkuran7a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="7a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="7aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori7b" onchange="setkategori7b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal7b" onchange="settebal7b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran7b" onchange="setUkuran7b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="7b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="7bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori8a" onchange="setkategori8a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal8a" onchange="settebal8a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran8a" onchange="setUkuran8a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="8a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="8aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori8b" onchange="setkategori8b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal8b" onchange="settebal8b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran8b" onchange="setUkuran8b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="8b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="8bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori9a" onchange="setkategori9a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal9a" onchange="settebal9a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran9a" onchange="setUkuran9a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="9a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="9aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori9b" onchange="setkategori9b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal9b" onchange="settebal9b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran9b" onchange="setUkuran9b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="9b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="9bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="3" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori10a" onchange="setkategori10a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal10a" onchange="settebal10a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran10a" onchange="setUkuran10a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="10a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="10aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori10b" onchange="setkategori10b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal10b" onchange="settebal10b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran10b" onchange="setUkuran10b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="10b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="10bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori11a" onchange="setkategori11a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal11a" onchange="settebal11a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran11a" onchange="setUkuran11a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="11a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="11aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori11b" onchange="setkategori11b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal11b" onchange="settebal11b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran11b" onchange="setUkuran11b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="11b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="11bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori12a" onchange="setkategori12a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal12a" onchange="settebal12a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran12a" onchange="setUkuran12a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="12a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="12aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori12b" onchange="setkategori12b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal12b" onchange="settebal12b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran12b" onchange="setUkuran12b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="12b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="12bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="4" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori13a" onchange="setkategori13a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal13a" onchange="settebal13a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran13a" onchange="setUkuran13a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="13a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="13aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori13b" onchange="setkategori13b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal13b" onchange="settebal13b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran13b" onchange="setUkuran13b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="13b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="13bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori14a" onchange="setkategori14a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal14a" onchange="settebal14a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran14a" onchange="setUkuran14a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="14a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="14aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori14b" onchange="setkategori14b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal14b" onchange="settebal14b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran14b" onchange="setUkuran14b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="14b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="14bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori15a" onchange="setkategori15a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal15a" onchange="settebal15a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran15a" onchange="setUkuran15a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="15a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="15aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori15b" onchange="setkategori15b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal15b" onchange="settebal15b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran15b" onchange="setUkuran15b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="15b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="15bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="5" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori16a" onchange="setkategori16a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal16a" onchange="settebal16a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran16a" onchange="setUkuran16a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="16a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="16aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori16b" onchange="setkategori16b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal16b" onchange="settebal16b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran16b" onchange="setUkuran16b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="16b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="16bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori17a" onchange="setkategori17a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal17a" onchange="settebal17a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran17a" onchange="setUkuran17a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="17a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="17aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori17b" onchange="setkategori17b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal17b" onchange="settebal17b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran17b" onchange="setUkuran17b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="17b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="17bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori18a" onchange="setkategori18a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal18a" onchange="settebal18a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran18a" onchange="setUkuran18a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="18a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="18aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori18b" onchange="setkategori18b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal18b" onchange="settebal18b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran18b" onchange="setUkuran18b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="18b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="18bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="6" style="display: none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori19a" onchange="setkategori19a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal19a" onchange="settebal19a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran19a" onchange="setUkuran19a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="19a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="19aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7b" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori19b" onchange="setkategori19b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal19b" onchange="settebal19b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran19b" onchange="setUkuran19b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="19b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="19bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7c" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori20a" onchange="setkategori20a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal20a" onchange="settebal20a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran20a" onchange="setUkuran20a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="20a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="20aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7d" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>2</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori20b" onchange="setkategori20b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal20b" onchange="settebal20b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran20b" onchange="setUkuran20b(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="20b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="20bb" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7e" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift">3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori21a" onchange="setkategori21a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal21a" onchange="settebal21a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran21a" onchange="setUkuran21a(value)">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="21a" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="21aa" />
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7f" hidden></label>
                                                </th>
                                                <th>
                                                    <label class="shift" hidden>3</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori21b">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control tebal" id="ddltebal21b">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class="form-control ukuran" id="ddlukuran21b">
                                                        <option value=""></option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="21b" />
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control Keterangan" name="" id="21bb" />
                                                </th>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="panel-body boxShadow the-loader">
            <div style="padding: 7%;"></div>
            <i class="fa fa-spinner fa-spin"></i>
        </div>
    </body>
    <script src="../../Scripts/SPKP/SPKP.js" type="text/javascript"></script>
    <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../Scripts/jquery.blockui.min.js"></script>
    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

    </html>
</asp:Content>
