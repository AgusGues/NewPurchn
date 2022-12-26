<%@ Page Title="Close Period Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLClosePeriod.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLClosePeriod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">
				<div class="form-group">
					<label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Current Period </label>

					<div class="col-sm-9">
                        <span class="input-icon">
                            <asp:DropDownList ID="ddlBulan1" class="col-sm-3 form-control center" runat="server" OnSelectedIndexChanged="ddlBulan1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="01">Januari</asp:ListItem>
                                <asp:ListItem Value="02">Februari</asp:ListItem>
                                <asp:ListItem Value="03">Maret</asp:ListItem>
                                <asp:ListItem Value="04">April</asp:ListItem>
                                <asp:ListItem Value="05">Mei</asp:ListItem>
                                <asp:ListItem Value="06">Juni</asp:ListItem>
                                <asp:ListItem Value="07">Juli</asp:ListItem>
                                <asp:ListItem Value="08">Agustus</asp:ListItem>
                                <asp:ListItem Value="09">September</asp:ListItem>
                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">Desember</asp:ListItem>
                            </asp:DropDownList>
                        </span>
                        <span class="input-icon">
    						<input type="text" id="txtTahun1" runat="server" placeholder="Tahun" class="col-xs-10 col-sm-5" />
							<i class="ace-icon fa fa-calendar green"></i>
                        </span>
					</div>
				</div>

				<div class="form-group">
					<label class="col-sm-3 control-label no-padding-right" for="form-field-1-1"> Next Period </label>

					<div class="col-sm-9">
                        <span class="input-icon">
                            <asp:DropDownList ID="ddlBulan2" class="col-sm-3 form-control center"  runat="server">
                                <asp:ListItem Value="01">Januari</asp:ListItem>
                                <asp:ListItem Value="02">Februari</asp:ListItem>
                                <asp:ListItem Value="03">Maret</asp:ListItem>
                                <asp:ListItem Value="04">April</asp:ListItem>
                                <asp:ListItem Value="05">Mei</asp:ListItem>
                                <asp:ListItem Value="06">Juni</asp:ListItem>
                                <asp:ListItem Value="07">Juli</asp:ListItem>
                                <asp:ListItem Value="08">Agustus</asp:ListItem>
                                <asp:ListItem Value="09">September</asp:ListItem>
                                <asp:ListItem Value="10">Oktober</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">Desember</asp:ListItem>
                            </asp:DropDownList>
                        </span>
                        <span class="input-icon">
    						<input type="text" id="txtTahun2" runat="server" placeholder="Tahun" class="col-xs-10 col-sm-5" />
							<i class="ace-icon fa fa-calendar green"></i>
                        </span>
					</div>
				</div>

                <asp:updatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <contentTemplate>                
				<div class="clearfix form-actions">
					<div class="col-md-offset-3 col-md-9">
						<button class="btn btn-info" type="button" id="btnSave" runat="server" onserverclick="btnSave_ServerClick" visible="false">
							<i class="ace-icon fa fa-check bigger-110"></i>
							Close Period
						</button>

						<button class="btn btn-info" type="button" id="btnSave1" runat="server" onserverclick="btnSave1_ServerClick">
							<i class="ace-icon fa fa-check bigger-110"></i>
							Close Period
						</button>

						&nbsp; &nbsp; &nbsp;
						<button class="btn" type="reset" id="btnReset" runat="server" onserverclick="btnReset_ServerClick">
							<i class="ace-icon fa fa-undo bigger-110"></i>
							Reset Period
						</button>



					</div>
				</div>                
                </contentTemplate>
                </asp:updatePanel>

				<%--<div class="hr hr-24"></div>--%>






			</div>

		</div><!-- /.col -->

	</div><!-- /.row -->

<%--                                        <asp:updatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <contentTemplate>

						<div class="row" runat="server">
							<div class="col-xs-12">
								<!-- PAGE CONTENT BEGINS -->
								<div class="form-horizontal" role="form" runat="server">
									<div class="row">
										<div class="col-xs-12 col-sm-4">
											<div class="widget-box">
												<div class="widget-header">
													<h4 class="widget-title">Select Box</h4>

													<span class="widget-toolbar">
														<a href="#" data-action="settings">
															<i class="ace-icon fa fa-cog"></i>
														</a>

														<a href="#" data-action="reload">
															<i class="ace-icon fa fa-refresh"></i>
														</a>

														<a href="#" data-action="collapse">
															<i class="ace-icon fa fa-chevron-up"></i>
														</a>

														<a href="#" data-action="close">
															<i class="ace-icon fa fa-times"></i>
														</a>
													</span>
												</div>
												<div class="widget-body">
													<div class="widget-main" runat="server">

														<div runat="server">
															<label for="formfieldselect3" runat="server">Chosen</label>

															<br />
															<select class="chosen-select form-control" id="formfieldselect3" data-placeholder="Choose a State..." runat="server" onclick="btn1_Click">
																<option value="">  </option>
																<option value="AL">Alabama</option>
																<option value="AK">Alaska</option>
																<option value="AZ">Arizona</option>
																<option value="AR">Arkansas</option>
																<option value="CA">California</option>
																<option value="CO">Colorado</option>
																<option value="CT">Connecticut</option>
																<option value="DE">Delaware</option>
																<option value="FL">Florida</option>
																<option value="GA">Georgia</option>
																<option value="HI">Hawaii</option>
																<option value="ID">Idaho</option>
																<option value="IL">Illinois</option>
																<option value="IN">Indiana</option>
																<option value="IA">Iowa</option>
																<option value="KS">Kansas</option>
																<option value="KY">Kentucky</option>
																<option value="LA">Louisiana</option>
																<option value="ME">Maine</option>
																<option value="MD">Maryland</option>
																<option value="MA">Massachusetts</option>
																<option value="MI">Michigan</option>
																<option value="MN">Minnesota</option>
																<option value="MS">Mississippi</option>
																<option value="MO">Missouri</option>
																<option value="MT">Montana</option>
																<option value="NE">Nebraska</option>
																<option value="NV">Nevada</option>
																<option value="NH">New Hampshire</option>
																<option value="NJ">New Jersey</option>
																<option value="NM">New Mexico</option>
																<option value="NY">New York</option>
																<option value="NC">North Carolina</option>
																<option value="ND">North Dakota</option>
																<option value="OH">Ohio</option>
																<option value="OK">Oklahoma</option>
																<option value="OR">Oregon</option>
																<option value="PA">Pennsylvania</option>
																<option value="RI">Rhode Island</option>
																<option value="SC">South Carolina</option>
																<option value="SD">South Dakota</option>
																<option value="TN">Tennessee</option>
																<option value="TX">Texas</option>
																<option value="UT">Utah</option>
																<option value="VT">Vermont</option>
																<option value="VA">Virginia</option>
																<option value="WA">Washington</option>
																<option value="WV">West Virginia</option>
																<option value="WI">Wisconsin</option>
																<option value="WY">Wyoming</option>
															</select>

                                                            <br />

                                                            <select class="chosen-select form-control" id="Select1" name="Select1Name" runat="server" data-placeholder="Choose a State..." contenteditable="true">
                                                         
                                                            </select>

                                                            <!--ok 1-->
<!--                                                            <div id="DivPlant" runat="server" class="chosen-select form-control" data-placeholder="Choose a State...">
                                                            </div>-->


														</div>

														<hr />
													</div>




												</div>
											</div>
										</div><!-- /.span -->

									</div><!-- /.row -->

								</div>

								<div class="hr hr-18 dotted hr-double"></div>


							</div><!-- /.col -->
						</div><!-- /.row -->
                                            </contentTemplate>
                                        </asp:updatePanel>--%>






<%--		<link rel="stylesheet" href="assets/css/chosen.min.css" />

		<script src="assets/js/chosen.jquery.min.js"></script>




		<script type="text/javascript">
			jQuery(function($) {
				//$('#id-disable-check').on('click', function() {
				//	var inp = $('#form-input-readonly').get(0);
				//	if(inp.hasAttribute('disabled')) {
				//		inp.setAttribute('readonly' , 'true');
				//		inp.removeAttribute('disabled');
				//		inp.value="This text field is readonly!";
				//	}
				//	else {
				//		inp.setAttribute('disabled' , 'disabled');
				//		inp.removeAttribute('readonly');
				//		inp.value="This text field is disabled!";
				//	}
				//});
			


			
			    if(!ace.vars['touch']) {
			        $("#btn1").click();

			        $('.chosen-select').chosen({
			            allow_single_deselect: true

			        });
			        //resize the chosen on window resize
			

					$(window)
					.off('resize.chosen')
					.on('resize.chosen', function() {
					    $('.chosen-select').each(function () {
							 var $this = $(this);
							 $this.next().css({ 'width': $this.parent().width() });

						})
					}).trigger('resize.chosen');
					//resize chosen on sidebar collapse/expand
					$(document).on('settings.ace.chosen', function(e, event_name, event_val) {
					    alert("test11111");
					    if (event_name != 'sidebar_collapsed') return;
						$('.chosen-select').each(function() {
							 var $this = $(this);
							 $this.next().css({ 'width': $this.parent().width() });

							 alert("test1");

						})
					});
			

					$('#chosen-multiple-style .btn').on('click', function (e) {

					    alert("test1");

						var target = $(this).find('input[type=radio]');
						var which = parseInt(target.val());
						if(which == 2) $('#form-field-select-4').addClass('tag-input-style');
						 else $('#form-field-select-4').removeClass('tag-input-style');
					});

					$("#btn1").click();

				}
			
				
				
				$(document).one('ajaxloadstart.page', function(e) {
					$('textarea[class*=autosize]').trigger('autosize.destroy');
					$('.limiterBox,.autosizejs').remove();
					$('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
				});
			
			});
		</script>--%>



 
</asp:Content>


