<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLGeneralLedger.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLGeneralLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="row" runat="server">
		<div class="col-xs-12" runat="server">
            <div class="tabbable inbox-tabs" runat="server">
					<ul class="nav nav-tabs" id="myTab3" runat="server">
						<li class="active" runat="server" id="liTab1">
							<a class="" href="?HrefTabOnClick=tab1" runat="server">
								<i class="pink ace-icon fa fa-tachometer bigger-110"></i>
								Daily
							</a>
						</li>

						<li class="" runat="server" id="liTab2">
							<a class="" href="?HrefTabOnClick=tab2" runat="server">
								<i class="blue ace-icon fa fa-user bigger-110"></i>
								Monthly
							</a>
						</li>
					</ul>
					<div class="tab-content">
						<div id="home3" class="tab-pane in active" style="height:300px" runat="server">

                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Date from </label>
                                    <div class="col-sm-4 no-padding-right" >
										<div class="input-group no-padding-right" >
											<%--<input class="form-control no-padding-right date-picker" id="id-date-picker-1" type="text" data-date-format="dd-mm-yyyy" />--%>
											<input class="form-control no-padding-right date-picker" id="drTgl" type="text" data-date-format="dd-mm-yyyy" runat="server"/>
											<span class="input-group-addon">
												<i class="fa fa-calendar bigger-110"></i>
											</span>
										</div>
									</div>
                                    <span>
    									<label class="col-sm-1 control-label no-padding-right no-padding-left" for="form-field-1"> to </label>
                                    </span>
                                    <div class="col-sm-4">
										<div class="input-group">
											<input class="form-control date-picker" id="sdTgl" type="text" data-date-format="dd-mm-yyyy" runat="server"/>
											<span class="input-group-addon">
												<i class="fa fa-calendar bigger-110"></i>
											</span>
										</div>
									</div>

								</div>
                            </div>

                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Chart No </label>
                                    <div class="col-sm-4 no-padding-right" >
										<div class="input-group no-padding-right" >
                                            <select class="chosen-select form-control" id="Select1" name="Select1Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>
                                    <span>
    									<label class="col-sm-1 control-label no-padding-right no-padding-left" for="form-field-1"> to </label>
                                    </span>
                                    <div class="col-sm-4">
										<div class="input-group">
                                            <select class="chosen-select form-control" id="Select2" name="Select2Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>

								</div>
                            </div>

						</div>

						<div id="profile3" class="tab-pane" style="height:300px" runat="server">
			                <div class="form-horizontal" role="form">
				                <div class="form-group">
					                <label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Current Period </label>
					                <div class="col-sm-9">
                                        <span class="input-icon">
                                            <asp:DropDownList ID="ddlBulan1" class="col-sm-3 form-control center" runat="server">
                                                <asp:ListItem Value="0">Pilih Bulan</asp:ListItem>
                                                <asp:ListItem Value="1">Januari</asp:ListItem>
                                                <asp:ListItem Value="2">Februari</asp:ListItem>
                                                <asp:ListItem Value="3">Maret</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">Mei</asp:ListItem>
                                                <asp:ListItem Value="6">Juni</asp:ListItem>
                                                <asp:ListItem Value="7">Juli</asp:ListItem>
                                                <asp:ListItem Value="8">Agustus</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
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
                            </div>

                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Chart No </label>
                                    <div class="col-sm-4 no-padding-right" >
										<div class="input-group no-padding-right" >
                                            <select class="chosen-select form-control" id="Select3" name="Select3Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>
                                    <span>
    									<label class="col-sm-1 control-label no-padding-right no-padding-left" for="form-field-1"> to </label>
                                    </span>
                                    <div class="col-sm-4">
										<div class="input-group">
                                            <select class="chosen-select form-control" id="Select4" name="Select4Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>

								</div>
                            </div>

						</div>
					</div>
				</div>
            <div class="vspace-6-sm"></div>

        </div>
    </div>


	<div class="row">
		<div class="col-xs-12">

			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">

<%--                <asp:updatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <contentTemplate>--%>
                
				<div class="clearfix form-actions">


					<div class="col-md-offset-3 col-md-9">
						<button class="btn btn-info" type="button" id="btnSave" runat="server" onserverclick="btnSave_ServerClick">
						<%--<button class="btn btn-info" type="button" id="btnSave" onclick="showModalPopUp('GeneralLedger')">--%>
							<i class="ace-icon fa fa-check bigger-110"></i>
							Preview
						</button>

						&nbsp; &nbsp; &nbsp;
						<button class="btn" type="reset" id="btnReset" runat="server" onserverclick="btnReset_ServerClick">
							<i class="ace-icon fa fa-undo bigger-110"></i>
							Reset Periew
						</button>
					</div>
				</div>
                
<%--                </contentTemplate>
                </asp:updatePanel>--%>

				<%--<div class="hr hr-24"></div>--%>
               
			</div>

		</div><!-- /.col -->


	</div><!-- /.row -->

</asp:Content>
