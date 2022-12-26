<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PencapaianSarmut.aspx.cs" Inherits="GRCweb1.Modul.SasaranMutu.PencapaianSarmut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>

<%--        <link href="../../assets/css/text.css" rel="stylesheet" />--%>
<%--        <script type="text/javascript" src="../../assets/js/freeze-table.js"></script>--%>
        <script type="text/javascript" src="../../assets/js/bootstrap.min.js"></script>
    </head>

    <body class="no-skin">

        <div class="row">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                    </div>

                    <div class="panel-body">

                        <div class="col-xs-12 col-sm-2">
                            <div>
                                <div>
                                    <i class="fa fa-calendar bigger-110"></i>
                                    <label for="form-field-9">Tahun</label>
                                    <select class="form-control" id="tahun">
                                        <option value="2017">2017</option>
                                        <option value="2018">2018</option>
                                        <option value="2019">2019</option>
                                        <option value="2020">2020</option>
                                        <option value="2021">2021</option>
                                        <option value="2022">2022</option>
                                        <option value="2023">2023</option>
                                        <option value="2024">2024</option>
                                        <option value="2025">2025</option>
                                    </select>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-10">
                            <span class="help-inline col-xs-9">
                                <label class="middle" style="padding-bottom: 7px;">
                                    <input class="ace" type="checkbox" id="citereup">
                                    <span class="lbl">Citereup </span>
                                </label>

                                <label class="middle" style="padding-bottom: 7px;">
                                    <input class="ace" type="checkbox" id="karawang">
                                    <span class="lbl">Karawang </span>
                                </label>
                                <label class="middle" style="padding-bottom: 7px;">
                                    <input class="ace" type="checkbox" id="jombang">
                                    <span class="lbl">Jombang </span>
                                </label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="hr hr-24"></div>

        <div class="row">
            <div class="col-xs-12">
                <div  id ="panelctrp">
                                    <div class="panel panel-primary" >
                    <div class="panel-heading">
                        <h3 class="panel-title">PLANT CITEUREUP</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tablesarmutctrp" class="table table-striped table-hover table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Urutan </th>
                                            <th>NO </th>
                                            <th>DIMENSI</th>
                                            <th>SASARAN MUTU PERUSAHAAN</th>
                                            <th>DEPT</th>
                                            <th>SASARAN MUTU DEPARTEMEN</th>
                                            <th>PARAMETER TERUKUR</th>
                                            <th>Jan</th>
                                            <th>Feb</th>
                                            <th>Mar</th>
                                            <th>Apr</th>
                                            <th>Mei</th>
                                            <th>Jun</th>
                                            <th>SMT I</th>
                                            <th>Jul</th>
                                            <th>Agu</th>
                                            <th>Sep</th>
                                            <th>Okt</th>
                                            <th>Nov</th>
                                            <th>Des</th>
                                            <th>SMT II</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                </div>

            </div>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary" id="panelkrwg">
                    <div class="panel-heading">
                        <h3 class="panel-title">PLANT KARAWANG</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tablesarmutkrwg" class="table table-striped table-hover table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Urutan </th>
                                            <th>NO </th>
                                            <th>DIMENSI</th>
                                            <th>SASARAN MUTU PERUSAHAAN</th>
                                            <th>DEPT</th>
                                            <th>SASARAN MUTU DEPARTEMEN</th>
                                            <th>PARAMETER TERUKUR</th>
                                            <th>Jan</th>
                                            <th>Feb</th>
                                            <th>Mar</th>
                                            <th>Apr</th>
                                            <th>Mei</th>
                                            <th>Jun</th>
                                            <th>SMT I</th>
                                            <th>Jul</th>
                                            <th>Agu</th>
                                            <th>Sep</th>
                                            <th>Okt</th>
                                            <th>Nov</th>
                                            <th>Des</th>
                                            <th>SMT II</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary" id ="paneljmng">
                    <div class="panel-heading">
                        <h3 class="panel-title">PLANT JOMBANG</h3>
                    </div>
                    <div class="panel-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="tablesarmutjmng" class="table table-striped table-hover table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Urutan </th>
                                            <th>NO </th>
                                            <th>DIMENSI</th>
                                            <th>SASARAN MUTU PERUSAHAAN</th>
                                            <th>DEPT</th>
                                            <th>SASARAN MUTU DEPARTEMEN</th>
                                            <th>PARAMETER TERUKUR</th>
                                            <th>Jan</th>
                                            <th>Feb</th>
                                            <th>Mar</th>
                                            <th>Apr</th>
                                            <th>Mei</th>
                                            <th>Jun</th>
                                            <th>SMT I</th>
                                            <th>Jul</th>
                                            <th>Agu</th>
                                            <th>Sep</th>
                                            <th>Okt</th>
                                            <th>Nov</th>
                                            <th>Des</th>
                                            <th>SMT II</th>
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

    <script src="../../assets/js/jquery-ui.min.js"></script>
    <script src="../../assets/Datatables/datatables.js"></script>
    <script src="../../assets/Datatables/datatables.min.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.js"></script>
    <script src="../../assets/Datatables/Responsive-2.2.7/js/dataTables.responsive.min.js"></script>

    <script src="../../Scripts/jquery.blockui.min.js"></script>

    <script src="../../Scripts/Sarmut/PencapaianSarmut.js"></script>
    </html>
</asp:Content>