<%@ Page Title="Login Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GRCweb1.Default" %>

<!DOCTYPE html>

<html lang="en">
	<head>
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<meta charset="utf-8" />
		<title>Login Page - GRC Board</title>

		<meta name="description" content="User login page" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

		<!-- bootstrap & fontawesome -->
		<link rel="stylesheet" href="assets/css/bootstrap.min.css" />
		<link rel="stylesheet" href="assets/font-awesome/4.2.0/css/font-awesome.min.css" />

		<!-- text fonts -->
		<link rel="stylesheet" href="assets/fonts/fonts.googleapis.com.css" />
		        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
		<!-- ace styles -->
		<link rel="stylesheet" href="assets/css/ace.min.css" />

		<!--[if lte IE 9]>
			<link rel="stylesheet" href="assets/css/ace-part2.min.css" />
		<![endif]-->
		<link rel="stylesheet" href="assets/css/ace-rtl.min.css" />

		<!--[if lte IE 9]>
		  <link rel="stylesheet" href="assets/css/ace-ie.min.css" />
		<![endif]-->

		<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->

		<!--[if lt IE 9]>
		<script src="assets/js/html5shiv.min.js"></script>
		<script src="assets/js/respond.min.js"></script>
		<![endif]-->

       <script type="text/javascript">
           window.onload = function ()
           {
               var tgl = new Date();
               var menit = tgl.getMinutes();
               var sisa3 = menit % 3;
               var sisa2 = menit % 2;
               if (sisa3 == 0 && sisa2 > 0)
               {
                   //alert('3');
                   document.getElementById("<%=btnloginblur.ClientID %>").click();
               }
               if (sisa3 > 0 && sisa2 == 0)
               {
                   //alert('2');
                   document.getElementById("<%=btnloginlight.ClientID %>").click();
               }
               if (sisa3 > 0 && sisa2 > 0)
               {
                   //alert('1');
                   document.getElementById("<%=btnlogindark.ClientID %>").click();
               }
               $(document).keypress(function (event) {

                   var keycode = (event.keyCode ? event.keyCode : event.which);
                   if (keycode == '13') {
                       $("#btnLogin1").click();
                   }
               });
           }
        </script>

	</head>

<body class="login-layout" id="MyBodyLogin" runat="server">

		<div class="main-container" runat="server">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">
								<h1>
									<%--<i class="ace-icon fa fa-leaf green"></i>--%>
                                    <asp:Image ID="Image1" ImageUrl="~/img/GRCLogo.png" Width="44px" Height="30px" runat="server" />
									<span class="red"><b>GRC Board</b></span>
									<span class="white" id="id-text2">System</span>
								</h1>
								<%--<h4 class="blue" id="id-company-text">&copy; Company Name</h4>--%>
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header blue lighter bigger">
												<i class="ace-icon fa fa-coffee green"></i>
												Please Enter Your Information
											</h4>

											<div class="space-6"></div>

                                            <form id="form1" runat="server">
                                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="text" class="form-control" placeholder="Username" runat="server" id="txtLogin" />
															<i class="ace-icon fa fa-user"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password" class="form-control" placeholder="Password" runat="server" id="txtPassword"/>
															<i class="ace-icon fa fa-lock"></i>
														</span>
													</label>

													<div class="space"></div>

													<div class="clearfix center">
														<%--<label class="inline">
															<input type="checkbox" class="ace" />
															<span class="lbl"> Remember Me</span>
														</label>--%>

														<button id="btnLogin1" runat="server" name="LoginButton" type="button" class="btn btn-lg btn-primary btn-block" onserverclick="btnLogin1_ServerClick" accesskey="L">
															<i class="ace-icon fa fa-key"></i>
															<span class="bigger-110">Login</span>
														</button>
                                                        <%--<input id="btnLogin" runat="server" name="LoginButton" class="btn btn-lg btn-success btn-block" value="Login" type="submit" onserverclick="btnLogin_ServerClick" accesskey="L">--%>

													</div>

                                        <div style="padding-top:9px;">
                                            <asp:Label ID="lblText" runat="server" Text="" Style="color: red;"></asp:Label>
                                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin1_ServerClick" Text="Log In" Visible="false" />
                                        </div>

													<div class="space-4"></div>
												</fieldset>
											</form>

											<%--<div class="social-or-login center">
												<span class="bigger-110">Or Login Using</span>
											</div>

											<div class="space-6"></div>

											<div class="social-login center">
												<a class="btn btn-primary">
													<i class="ace-icon fa fa-facebook"></i>
												</a>

												<a class="btn btn-info">
													<i class="ace-icon fa fa-twitter"></i>
												</a>

												<a class="btn btn-danger">
													<i class="ace-icon fa fa-google-plus"></i>
												</a>
											</div>--%>
										</div><!-- /.widget-main -->

										<div>
											<a class="btn btn-block btn-primary">
                                            <img class="" src="/img/MASKOT_GRCBOARD1jempol25.png" alt="Ahlinya Papan Semen" />
											<span> &nbsp;<b>GRC Board, Ahlinya Papan Semen</b></span>
											</a>
										</div>


									</div><!-- /.widget-body -->
								</div><!-- /.login-box -->

								<%--<div id="forgot-box" class="forgot-box widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header red lighter bigger">
												<i class="ace-icon fa fa-key"></i>
												Retrieve Password
											</h4>

											<div class="space-6"></div>
											<p>
												Enter your email and to receive instructions
											</p>

											<form>
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="email" class="form-control" placeholder="Email" />
															<i class="ace-icon fa fa-envelope"></i>
														</span>
													</label>

													<div class="clearfix">
														<button type="button" class="width-35 pull-right btn btn-sm btn-danger">
															<i class="ace-icon fa fa-lightbulb-o"></i>
															<span class="bigger-110">Send Me!</span>
														</button>
													</div>
												</fieldset>
											</form>
										</div><!-- /.widget-main -->

										<div class="toolbar center">
											<a href="#" data-target="#login-box" class="back-to-login-link">
												Back to login
												<i class="ace-icon fa fa-arrow-right"></i>
											</a>
										</div>
									</div><!-- /.widget-body -->
								</div><!-- /.forgot-box -->

								<div id="signup-box" class="signup-box widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header green lighter bigger">
												<i class="ace-icon fa fa-users blue"></i>
												New User Registration
											</h4>

											<div class="space-6"></div>
											<p> Enter your details to begin: </p>

											<form>
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="email" class="form-control" placeholder="Email" />
															<i class="ace-icon fa fa-envelope"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="text" class="form-control" placeholder="Username" />
															<i class="ace-icon fa fa-user"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password" class="form-control" placeholder="Password" />
															<i class="ace-icon fa fa-lock"></i>
														</span>
													</label>

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password" class="form-control" placeholder="Repeat password" />
															<i class="ace-icon fa fa-retweet"></i>
														</span>
													</label>

													<label class="block">
														<input type="checkbox" class="ace" />
														<span class="lbl">
															I accept the
															<a href="#">User Agreement</a>
														</span>
													</label>

													<div class="space-24"></div>

													<div class="clearfix">
														<button type="reset" class="width-30 pull-left btn btn-sm">
															<i class="ace-icon fa fa-refresh"></i>
															<span class="bigger-110">Reset</span>
														</button>

														<button type="button" class="width-65 pull-right btn btn-sm btn-success">
															<span class="bigger-110">Register</span>

															<i class="ace-icon fa fa-arrow-right icon-on-right"></i>
														</button>
													</div>
												</fieldset>
											</form>
										</div>

										<div class="toolbar center">
											<a href="#" data-target="#login-box" class="back-to-login-link">
												<i class="ace-icon fa fa-arrow-left"></i>
												Back to login
											</a>
										</div>
									</div><!-- /.widget-body -->
								</div><!-- /.signup-box -->--%>
							</div><!-- /.position-relative -->

							<div class="navbar-fixed-top align-right hide" runat="server">
								<br />
								&nbsp;
								<a id="btnlogindark" href="#" runat="server">Dark</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btnloginblur" href="#" runat="server">Blur</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btnloginlight" href="#" runat="server">Light</a>
								&nbsp; &nbsp; &nbsp;
								<br />
							</div>
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div><!-- /.main-content -->
		</div><!-- /.main-container -->

		<!-- basic scripts -->

		<!--[if !IE]> -->
		<script src="assets/js/jquery.2.1.1.min.js"></script>

		<!-- <![endif]-->

		<!--[if IE]>
<script src="assets/js/jquery.1.11.1.min.js"></script>
<![endif]-->

		<!--[if !IE]> -->
		<script type="text/javascript">
			window.jQuery || document.write("<script src='assets/js/jquery.min.js'>"+"<"+"/script>");
		</script>

		<!-- <![endif]-->

		<!--[if IE]>
<script type="text/javascript">
 window.jQuery || document.write("<script src='assets/js/jquery1x.min.js'>"+"<"+"/script>");
</script>
<![endif]-->
		<script type="text/javascript">
			if('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>"+"<"+"/script>");
		</script>

		<!-- inline scripts related to this page -->
		<script type="text/javascript">
			jQuery(function($) {
			 $(document).on('click', '.toolbar a[data-target]', function(e) {
				e.preventDefault();
				var target = $(this).data('target');
				$('.widget-box.visible').removeClass('visible');//hide others
				$(target).addClass('visible');//show target
			 });
			});

			//you don't need this, just used for changing background
			jQuery(function($) {
			 $('#btnlogindark').on('click', function(e) {
				$('body').attr('class', 'login-layout');
				$('#id-text2').attr('class', 'white');
				$('#id-company-text').attr('class', 'blue');

				e.preventDefault();
			 });
			 $('#btnloginlight').on('click', function(e) {
				$('body').attr('class', 'login-layout light-login');
				$('#id-text2').attr('class', 'grey');
				$('#id-company-text').attr('class', 'blue');

				e.preventDefault();
			 });
			 $('#btnloginblur').on('click', function(e) {
				$('body').attr('class', 'login-layout blur-login');
				$('#id-text2').attr('class', 'white');
				$('#id-company-text').attr('class', 'light-blue');

				e.preventDefault();
			 });

			});
		</script>

</body>
</html>
