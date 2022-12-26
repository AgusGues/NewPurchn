<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReasonCancel.aspx.cs" Inherits="GRCweb1.ModalDialog.ReasonCancel" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
	<meta name="description" content="overview &amp; stats" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>


    <link rel="stylesheet" href="/assets/css/bootstrap.min.css" />

    <%--form-group{margin-bottom:15px} by default, ubah ke 7--%>
    <%--<link rel="stylesheet" href="/assets/font-awesome/4.2.0/css/font-awesome.min.css" />--%>
    <link rel="stylesheet" href="/assets/font-awesome/4.7.0/css/font-awesome.css" />

    <!-- page specific plugin styles -->
    <%--<link rel="stylesheet" href="assets/css/jquery-ui.min.css" />--%>
    <link rel="stylesheet" href="/assets/css/jquery-ui.custom.min.css" />
    <%--<link rel="stylesheet" href="/assets/css/jquery.gritter.min.css" />--%>
    <link rel="stylesheet" href="/assets/css/chosen.min.css" />

    <!-- text fonts -->
    <link rel="stylesheet" href="/assets/fonts/fonts.googleapis.com.css" />

    <!-- ace styles -->
    <%--<link rel="stylesheet" href="assets/css/ace.min.css" class="ace-main-stylesheet" id="main-ace-style" />--%>
    <link rel="stylesheet" href="/assets/css/ace.min.css" class="ace-main-stylesheet" id="main_ace_style"/>
    <!--[if lte IE 9]>
	    <link rel="stylesheet" href="assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
    <![endif]-->

    <!--[if lte IE 9]>
	    <link rel="stylesheet" href="assets/css/ace-ie.min.css" />
    <![endif]-->

    <!-- inline styles related to this page -->
    <!-- ace settings handler -->
    <script src="/assets/js/ace-extra.min.js"></script>

    <!-- HTML5shiv and Respond.js for IE8 to support HTML5 elements and media queries -->

    <!--[if lte IE 8]>
    <script src="assets/js/html5shiv.min.js"></script>
    <script src="assets/js/respond.min.js"></script>
    <![endif]-->

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>

<body class="no-skin" runat="server">
		<div id="navbar" class="navbar navbar-default">
			<script type="text/javascript">
				try{ace.settings.check('navbar' , 'fixed')}catch(e){}
			</script>
			<div class="navbar-container" id="navbar-container">
<%--				<button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler" data-target="#sidebar">
					<span class="sr-only">Toggle sidebar</span>

					<span class="icon-bar"></span>

					<span class="icon-bar"></span>

					<span class="icon-bar"></span>
				</button>--%>

				<div class="navbar-header pull-left">
					<a href="index.html" class="navbar-brand">
						<small>
							<%--<i class="fa fa-leaf"></i>--%>
							Alasan Cancel
						</small>
					</a>
				</div>
            </div>
        </div>

<%--    <div class="main-container" id="main-container">
			<script type="text/javascript">
				try{ace.settings.check('main-container' , 'fixed')}catch(e){}
			</script>--%>

            <div class="row" runat="server">
                <div class="col-xs-12">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Alasan</label>
                            <div class="col-sm-8 no-padding-left">
                                <%--<input type="text" in="txtAlasanCancel" runat="server" placeholder="Input Alasan Cancel" style="width:500px; height:150px;"/>--%>
                                <textarea id="txtAlasanCancel" runat="server" placeholder="Input Alasan Cancel" style="width:500px; height:150px;"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div runat="server">
                <div class="row no-margin-bottom" runat="server">
                    <div class="col-xs-12">
                        <div class="form-actions no-margin-top center no-padding-top no-padding-bottom no-margin-bottom" runat="server">
                            <label class="control-label pull-left" id="lblFromKlik" runat="server"></label>
                            <a class="btn btn-white btn-info btn-bold" id="btnClearReason" runat="server" onserverclick="btnClearReason_ServerClick">
                                <i class="ace-icon fa fa-pencil-square-o bigger-120 blue"></i>
                                Clear Text
                            </a>
                            <a class="btn btn-white btn-warning btn-bold" id="btnUpdateReason" runat="server" onserverclick="btnUpdateReason_ServerClick">
                                <i class="ace-icon glyphicon glyphicon-map-marker bigger-120 orange"></i>
                                Update
                            </a>
                      <input id="btnUpdate" runat="server" type="button" value="Update" onclick="btnUpdateReason_ServerClick"/>

                            <%--<a class="btn btn-white btn-warning btn-bold" id="btnMaps2" runat="server" onserverclick="btnMaps2_ServerClick">
                                <i class="ace-icon fa fa-trash-o bigger-120 orange"></i>
                                List Maps Toko2
                            </a>--%>
                        </div>
                    </div>
                </div>
            </div>

    <%--</div>--%>

</body>

</html>
