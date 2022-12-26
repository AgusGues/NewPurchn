<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLChartOfAccount.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLChartOfAccount" %>

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
								Master Chart of Account
							</a>
						</li>

						<li class="" runat="server" id="liTab2">
							<a class="" href="?HrefTabOnClick=tab2" runat="server">
								<i class="blue ace-icon fa fa-user bigger-110"></i>
								Master Voucher
							</a>
						</li>
					</ul>

					<div class="tab-content">
						<div id="coa" class="tab-pane in active" style="height:510px" runat="server">

                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Chart No </label>
                                    <div class="col-sm-4 no-padding-right" >
                                        <input class="form-control input-mask-coa" type="text" id="txtChartNo" runat="server" placeholder="Input Chart No" onserverchange="txtChartNo_ServerChange"/>
									</div>
								</div>
                            </div>
                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Chart Name </label>
                                    <div class="col-sm-8 no-padding-right" >
                                        <input class="form-control" type="text" id="txtChartName" runat="server" placeholder="Input Chart Name"/>
									</div>
								</div>
                            </div>
                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Level </label>
                                    <div class="col-sm-4 no-padding-right" >
                                        <input class="form-control" type="text" id="txtLevel" runat="server" placeholder="Input Level" />
									</div>
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Group </label>
                                    <div class="col-sm-4 no-padding-right" >
										<div class="input-group no-padding-right" >
                                            <select class="chosen-select form-control" id="selectGroup" name="SelectGroup" runat="server" data-placeholder="Choose Group" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>

								</div>
                            </div>
                            <div class="form-horizontal" role="form">                                                    
                                <div class="form-group">
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Currency Code </label>
                                    <div class="col-sm-4 no-padding-right" >
										<div class="input-group no-padding-right" >
                                            <select class="chosen-select form-control" id="selectKurs" name="SelectKurs" runat="server" data-placeholder="Choose Currency" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>
									<label class="col-sm-2 control-label no-padding-right" for="form-field-1"> Postable </label>
                                    <div class="col-sm-4 no-padding-right" >
											<label class="inline">
												<input name="form-field-radio" type="radio" class="ace" />
												<span class="lbl middle"> No</span>
											</label>
											&nbsp; &nbsp;
											<label class="inline">
												<input name="form-field-radio" type="radio" class="ace" />
												<span class="lbl middle"> Yes</span>
											</label>
									</div>

								</div>
                            </div>

                            <div class="form-horizontal" role="form">                                                    
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!--<h3 class="header smaller lighter blue">jQuery dataTables</h3>
							        <div class="clearfix">
								        <div class="pull-right tableTools-container"></div>
							        </div>
							        <div class="table-header">
								        Results for "Latest Registered Domains"
							        </div>-->

                                        <!-- div.table-responsive -->
                                        <!-- div.dataTables_borderWrap -->

                                        <div>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <table id="dynamic-table" class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th class="center">
                                                                    <label class="pos-rel">
                                                                        <input type="checkbox" class="ace" />
                                                                        <span class="lbl"></span>
                                                                    </label>
                                                                </th>
                                                                <th class="hidden-480">ID</th>
                                                                <th>Chart No</th>
                                                                <th class="hidden-480">Chart Name</th>
                                                                <th>Description</th>
                                                                <%--											<th>
												        <i class="ace-icon fa fa-clock-o bigger-110 hidden-480"></i>
												        Update
											        </th>--%>
                                                                <th class="hidden-480">Ccy</th>
                                                                <th>Amount</th>

                                                                <th></th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <tr>
                                                        <td class="center">
                                                            <%--<%# Container.ItemIndex + 1 %></td>  <!-- data index -->--%>
                                                            <label class="pos-rel">
                                                                <input type="checkbox" class="ace" />
                                                                <span class="lbl"></span>
                                                            </label>
                                                        </td>

                                                        <td class="hidden-480">
                                                            <%--<asp:LinkButton ID="lbnDetailID" runat="server" CommandArgument='<%# Eval("txtDetailID") %>' Text='<%# Eval("txtDetailID") %>' Visible="true"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lbnID" runat="server" CommandArgument='<%# Eval("txtID") %>' Text='<%# Eval("txtID") %>'></asp:LinkButton>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblChartNo" runat="server" Text='<%# Eval("txtChartNo") %>'></asp:Label>
                                                            <%--<a href="#">app.com</a>--%>
                                                        </td>

                                                        <td class="hidden-480">
                                                            <asp:Label ID="lblChartName" runat="server" Text='<%# Eval("txtChartName") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("txtDescription") %>'></asp:Label>
                                                        </td>
                                                        <td class="hidden-480">
                                                            <asp:Label ID="lblCcy" runat="server" Text='<%# Eval("txtCcy") %>'></asp:Label>
                                                        </td>
                                                        <td class="text-right">
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("txtAmount") %>'></asp:Label>
                                                            <%--<span class="label label-sm label-warning">Expiring</span>--%>
                                                        </td>

                                                        <td>
                                                            <div class="hidden-sm hidden-xs action-buttons">
                                                                <%--<a class="blue" href="#">--%>
        <%--                                                        <a class="blue" id="btnPilih" runat="server" onserverclick="btnPilih_ServerClick">
                                                                    <i class="ace-icon fa fa-search-plus bigger-130"></i>
                                                                </a>                                                        --%>
                                                                <asp:LinkButton ID="lbnEdit" CommandName="Edit" CommandArgument='<%# Eval("txtID") %>' OnClick="btnPilih_ServerClick" runat="server">
                                                                <i class="ace-icon fa fa-pencil bigger-130"></i></asp:LinkButton>

        <%--                                                        <a class="green" href="#">
                                                                    <i class="ace-icon fa fa-pencil bigger-130"></i>
                                                                </a>
                                                                <a class="red" href="#">
                                                                    <i class="ace-icon fa fa-trash-o bigger-130"></i>
                                                                </a>--%>
                                                            </div>

                                                            <div class="hidden-md hidden-lg">
                                                                <div class="inline pos-rel">
                                                                    <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                                                        <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                                                    </button>

                                                                    <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                                                        <li>
                                                                            <a href="#" class="tooltip-info" data-rel="tooltip" title="View">
                                                                                <span class="blue">
                                                                                    <i class="ace-icon fa fa-search-plus bigger-120"></i>
                                                                                </span>
                                                                            </a>
                                                                        </li>

                                                                        <li>
                                                                            <a href="#" class="tooltip-success" data-rel="tooltip" title="Edit">
                                                                                <span class="green">
                                                                                    <i class="ace-icon fa fa-pencil-square-o bigger-120"></i>
                                                                                </span>
                                                                            </a>
                                                                        </li>

                                                                        <li>
                                                                            <a href="#" class="tooltip-error" data-rel="tooltip" title="Delete">
                                                                                <span class="red">
                                                                                    <i class="ace-icon fa fa-trash-o bigger-120"></i>
                                                                                </span>
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>


                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
								        </table>
                                                            
                                                </FooterTemplate>
                                            </asp:Repeater>
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
        </div>
    </div>




	<div class="row no-padding-top">
		<div class="col-xs-12">

			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">
                
				<div class="clearfix form-actions">

					<div class="col-md-offset-3 col-md-9">
						<button class="btn btn-info" type="button" id="btnSave" runat="server" onserverclick="btnSave_ServerClick">
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
                               
			</div>

		</div><!-- /.col -->


	</div><!-- /.row -->



</asp:Content>
