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
        .panelbox {background-color: #efeded;padding: 2px;}
        html,body,.form-control,button{font-size: 11px;}
        .input-group-addon{background: white;}
        .fz11{font-size: 11px;}
        .fz10{font-size: 10px;}
        .the-loader{
            position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
            text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
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
                                 <button class="btn-xs btn-success" type="button" onclick="report()" id="btnreport">Report SPKP</button>
                                &nbsp
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" id="input">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            Line
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
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
                             </div>

                            <div style="padding: 30px"></div>

                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <table id="tblinput" class="table">
                                        <thead>
                                            <tr>
                                                <th class="text-center" >Tanggal</th>
                                                <th>Shift</th>
                                                <th class="text-center">Kategori</th>
                                                <th class="text-center">Tebal</th>
                                                <th class="text-center">Ukuran</th>
                                                <th class="text-center">Target</th>
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
                                                    <select class="chosen-select form-control kategori" id="ddlkategori1a" data-placeholder="pilih" onchange="setkategori(value)">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th class="text-center">
                                                    <select class="chosen-select form-control tebal" id="ddltebal1a" data-placeholder="Pilih" onchange="settebal(value)" >
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th class="text-center">
                                                    <select class="chosen-select form-control ukuran" id="ddlukuran1a" data-placeholder="Pilih" onchange="setUkuran(value)">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th class="text-center" contenteditable='true'>
                                                    <input type="text" class="input-sm form-control target" name="" id="1a" />
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
                                                    <select class="form-control  kategori" id="ddlkategori1b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal1b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran1b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="1b" />
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
                                                    <select class="form-control  kategori" id="ddlkategori2a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal2a" >
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran2a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="2a" />
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
                                                    <select class="form-control kategori" id="ddlkategori2b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <select class=" form-control tebal" id="ddltebal2b" >
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran2b" >
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="2b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori3a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal3a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran3a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="3a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori3b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal3b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran3b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="3b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th> <input type="text" class="input-sm form-control target" name="" id="1" style="display:none"/></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal2"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori4a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal4a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran4a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="4a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori4b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal4b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran4b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="4b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori5a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal5a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran5a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="5a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori5b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal5b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran5b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="5b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori6a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal6a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran6a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="6a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori6b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal6b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran6b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="6b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th><input type="text" class="input-sm form-control target" name="" id="2" style="display:none"/></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal3"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori7a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal7a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran7a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="7a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori7b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal7b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran7b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="7b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori8a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal8a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran8a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="8a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori8b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal8b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran8b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="8b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori9a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal9a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran9a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="9a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori9b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal9b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran9b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="9b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th><input type="text" class="input-sm form-control target" name="" id="3" style="display:none"/></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal4"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori10a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal10a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran10a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="10a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori10b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal10b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran10b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="10b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori11a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal11a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran11a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="11a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori11b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal11b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran11b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="11b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori12a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal12a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran12a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="12a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori12b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal12b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran12b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="12b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th><input type="text" class="input-sm form-control target" name="" id="4" style="display:none"/></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal5"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori13a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal13a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran13a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="13a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori13b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal13b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran13b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="13b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori14a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal14a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran14a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="14a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori14b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal14b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran14b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="14b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori15a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal15a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran15a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="15a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori15b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal15b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran15b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="15b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th><input type="text" class="input-sm form-control target" name="" id="5" style="display:none"/></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal6"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori16a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal16a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran16a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="16a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori16b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal16b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran16b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="16b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori17a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal17a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran17a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="17a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori17b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal17b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran17b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="17b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori18a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal18a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran18a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="18a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori18b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal18b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran18b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="18b"/>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th><input type="text" class="input-sm form-control target" name="" id="6" style="display:none" /></th>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <label class="tgl" id="tanggal7"></label>
                                                </th>
                                                <th>
                                                    <label class="shift">1</label>
                                                </th>
                                                <th>
                                                    <select class="form-control  kategori" id="ddlkategori19a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal19a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran19a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="19a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori19b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal19b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran19b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="19b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori20a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal20a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran20a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="20a"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori20b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal20b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran20b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="20b"/>
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
                                                    <select class="form-control  kategori" id="ddlkategori21a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal21a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran21a">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="21a"/>
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
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                     <select class="form-control tebal" id="ddltebal21b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                   <select class="form-control ukuran" id="ddlukuran21b">
                                                        <option value=""> </option>
                                                    </select>
                                                </th>
                                                <th>
                                                    <input type="text" class="input-sm form-control target" name="" id="21b"/>
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
        <div class="panel-body boxShadow the-loader" >
            <div style="padding:7%;"></div>
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
