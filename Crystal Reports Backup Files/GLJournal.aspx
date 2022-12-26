<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLJournal.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLJournal" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--    <script type="text/javascript">
        function openModalTable() {
            $('#modal-form').modal('show');
        }

        function InputJournalDialog() {
            window.showModalDialog('JournalDialog.aspx', '', 'resizable:yes;dialogHeight: 400px; dialogWidth: 1000px;scrollbars=Yes');
        }
    </script>--%>


    <div>

        <!--header-->
        <div class="row">
            <div class="col-xs-12">
                <!--header-->
                <div class="row">
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Voucher</label>
                                <div class="col-sm-9 no-padding-left">
                                    <select class="chosen-select" id="Select1" name="Select1Name" runat="server" data-placeholder="Choose Voucher">
                                    </select>
                                </div>
                            </div></div>
                    </div>
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="form-field-1">Journal #</label>
                            <div class="col-sm-4 no-padding-left">
                                <div class="input-group">
                                    <input type="text" id="txtJournalNo" runat="server" placeholder="Temporary Journal No" />
                                </div>
                            </div>
                            <div class="col-sm-5 no-padding-left">
                                <div class="input-group">
                                    <input class="form-control no-padding-right date-picker" placeholder="Input Journal Date" id="txtJournalDate" type="text" data-date-format="dd-mm-yyyy" runat="server" onserverchange="txtJournalDate_ServerChange"/>
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar bigger-110"></i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <%--                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
								<label class="col-sm-3 control-label" for="form-field-1">Debet Trans</label>
								<div class="col-sm-9">
									<input type="text" id="txtDebetTrans" runat="server" placeholder="0" class="col-xs-10 col-sm-5 text-right" readonly="readonly"/>
								</div>
                        </div>
                    </div>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-horizontal" role="form">
                        </div>
                    </div>
                    <div class="col-xs-12 col-lg-6">
                        <%--                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
							<label class="col-sm-3 control-label" for="form-field-1">Credit Trans</label>
							<div class="col-sm-9">
								<input type="text" id="txtCreditTrans" runat="server" placeholder="0" class="col-xs-10 col-sm-5 text-right" readonly="readonly"/>
							</div>
                        </div>
                    </div>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-sm-3 control-label" for="form-field-1">Remark</label>
                                <div class="col-sm-9 input-group">
                                    <input class="form-control" type="text" id="txtJournalRemark" runat="server" placeholder="Input Journal Remark" onserverchange="txtJournalRemark_ServerChange" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-lg-6">
                        <div class="form-group">
                            <label class="col-sm-3 control-label" for="form-field-1">Balance</label>
                            <div class="col-sm-7 control-label no-padding-left">
                                <input type="text" id="txtBalance" placeholder="0" runat="server" class="col-xs-10 col-sm-5 text-right" readonly="readonly" />
                            </div>

                            <div class="col-sm-2 no-padding-left">
								<span class="pull-right inline">
									<span class="grey">style:</span>
										<a class="btn btn-minier btn-yellow active" id="btnStyle1" runat="server" onserverclick="btnStyle1_ServerClick">1</a>
										<a class="btn btn-minier btn-yellow" id="btnStyle2" runat="server" onserverclick="btnStyle2_ServerClick">2</a>
                                </span>
                            </div>

                        </div>

                    </div>
                </div>
                <!--header-->
            </div>
        </div>

        <div class="hr" id="divGarisHeader" runat="server"></div>

        <!--grid 1-->
        <div class="row no-padding-bottom" id="divGridStyle2" runat="server" visible="false">
            <div class="col-xs-12">

                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->

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
                    <!-- /.col -->

                </div>
            </div>
        </div>

        <!--input jurnal detail style-1-->
        <div class="row" id="divDetailStyle1" runat="server">
            <div class="col-xs-12">
                <!--detail-->
                <div class="row">
                    <div class="col-xs-12 form-group">
                        <div class="form-group">
                            <label class="col-lg-2 control-label no-padding-left">Chart No</label>
                            <div class="col-lg-10 control-label no-padding-left">
                                <%--<select class="chosen-select" id="SelectChartNoGrid" name="Select1Name" runat="server" data-placeholder="Choose Voucher">
                                </select>--%>
                                <asp:DropDownList class="chosen-select" id="SelectChartNoGrid" name="Select1Name" runat="server" data-placeholder="Choose Voucher" OnSelectedIndexChanged="SelectChartNoGrid_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 form-group">
                        <div class="form-group">
                            <label class="col-lg-2 control-label no-padding-left">Description</label>
                            <input class="col-lg-10 no-padding-left" type="text" id="txtDescription" runat="server" placeholder=" Input Description  " />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 form-group">
                        <div class="form-group">
                            <label class="col-lg-2 control-label no-padding-left">Department</label>
                            <div class="col-lg-4 input-medium no-padding-left">
                                <select class="chosen-select" id="ddlDept" name="Select1Name" runat="server" data-placeholder="Choose Dept">
                                </select>
                            </div>
                            <input class="col-lg-7 no-padding-left" type="text" id="txtDept" runat="server" placeholder=" Non Department  " />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 form-group">
                        <div class="form-group">
                            <label class="col-lg-2 control-label no-padding-left">Cost Center</label>
                            <div class="col-lg-4 input-medium no-padding-left">
                                <select class="chosen-select" id="ddlCostCenter" name="Select1Name" runat="server" data-placeholder="Choose Cost Center">
                                </select>
                            </div>
                            <input class="col-lg-7 no-padding-left" type="text" id="txtCostCenter" runat="server" placeholder=" Non Cost Center " />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 form-group">
                        <%--<div class="form-group">--%>
                        <div>
                            <label class="col-lg-2 control-label no-padding-left">Amount</label>
                            <input class="col-lg-1 no-padding-left" type="text" id="txtKurs" runat="server" placeholder=" Currency " readonly />
							<label class="col-lg-1">
								<input name="switch-field-1" class="ace ace-switch ace-switch-4 btn-flat" type="checkbox" id="chkDebetCredit" runat="server"/>
								<span class="lbl" data-lbl="Debet&nbsp;&nbsp;&nbsp;Credit"></span>
							</label>
                            <input class="col-lg-5 no-margin-left no-padding-left text-right" type="text" id="txtAmountGrid" runat="server" placeholder=" Input Amount " onserverchange="txtAmountGrid_ServerChange" />
                            <label class="col-lg-1 control-label">Rate</label>
                            <input class="col-lg-2 text-right" type="text" id="txtRate" runat="server" placeholder=" 1.00 " readonly />
                        </div>
                    </div>
                </div>

                <%--			<div class="row">
				<div class="col-xs-12 col-lg-6">

                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
							<label class="col-sm-3 control-label" for="form-field-1">Voucher</label>
				            <div class="col-sm-9 input-group" >
                                <select class="chosen-select form-control" id="SelectChartNoGrid" name="Select1Name" runat="server" data-placeholder="Choose Voucher">                                                         
                                </select>
				            </div>

                        </div>
                    </div>

				</div>

			</div>--%>
            </div>
        </div>

        <%--btnGrid--%>
        <div class="row no-margin-bottom">
            <div class="col-xs-12">
                <div class="form-actions no-margin-top center no-padding-top no-padding-bottom no-margin-bottom" runat="server">
                    <label class="input-small blue">Period Book :</label>
                    <select class="input-medium " id="selectMonth" runat="server" onserverchange="selectMonth_ServerChange" disabled="disabled">
                        <option value="1">January</option>
                        <option value="2">February</option>
                        <option value="3">March</option>
                        <option value="4">April</option>
                        <option value="5">May</option>
                        <option value="6">June</option>
                        <option value="7">July</option>
                        <option value="8">August</option>
                        <option value="9">September</option>
                        <option value="10">October</option>
                        <option value="11">November</option>
                        <option value="12">December</option>
                    </select>
                    <input class="input-small" type="text" id="txtTahun" runat="server" placeholder="Input Year" disabled="disabled" />

                    <%--                <h4 class="pink">
					<i class="ace-icon fa fa-hand-o-right green"></i>
					<a href="#modal-form" role="button" class="blue" data-toggle="modal"> Form Inside a Modal Box </a>
				</h4>--%>
                    <%--                <a href="#modal-form" role="button" class="btn btn-white btn-info btn-bold" data-toggle="modal" id="btnAdd" runat="server">                      
					<i class="ace-icon fa fa-check bigger-120 blue"></i>
					<span>Add</span>
                </a>--%>



                    <a class="btn btn-white btn-info btn-bold" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick">
                        <i class="ace-icon fa fa-check bigger-120 blue"></i>
                        Add
                    </a>
                    <%--				<a class="btn btn-white btn-info btn-bold" id="btn1" runat="server" onserverclick="btnAdd_ServerClick">
					<i class="ace-icon fa fa-check bigger-120 blue"></i>
					Add
				</a>--%>

                    <%--                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>--%>

                    <a class="btn btn-white btn-info btn-bold" id="btnSave" runat="server" onserverclick="btnSave_ServerClick">
                        <i class="ace-icon fa fa-pencil-square-o bigger-120 blue"></i>
                        Save
                    </a>
                    <%--                <a id="test" class="btn btn-white btn-info btn-bold" runat="server" onserverclick="btnSave_ServerClick">
                    <i class="ace-icon fa fa-pencil-square-o bigger-120 blue"></i>Simpan</a>--%>

                    <a class="btn btn-white btn-warning btn-bold" id="btnDelete" runat="server" onserverclick="btnDelete_ServerClick">
                        <i class="ace-icon fa fa-trash-o bigger-120 orange"></i>
                        Delete
                    </a>
                    <a class="btn btn-white btn-default btn-round" id="btnCancel" runat="server" onserverclick="btnCancel_ServerClick">
                        <i class="ace-icon fa fa-times red2"></i>
                        Cancel
                    </a>

                    <%--                    </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>

        <%--btnHeader--%>
        <div class="row">
            <div class="col-xs-12">
                <div class="clearfix form-actions no-margin-top center">

                    <%--                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>--%>

                    <%--<div class="col-md-offset-2 col-md-10">--%>
                    <a class="btn btn-info" id="btnAddJournalHeader" runat="server" onserverclick="btnAddJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-check bigger-110"></i>
                        Add
                    </a>
                    <%--						<a class="btn btn-info"  id="Button1" runat="server" onserverclick="btnAddJournalHeader_ServerClick">
							<i class="ace-icon fa fa-check bigger-110"></i>
							Add
						</a>--%>
                    <a class="btn btn-info" id="btnEditJournalHeader" runat="server" onserverclick="btnEditJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-pencil-square-o bigger-110"></i>
                        Edit
                    </a>

                    <a class="btn btn-info" id="btnDeleteJournalHeader" runat="server" onserverclick="btnDeleteJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-trash-o bigger-110"></i>
                        Delete
                    </a>
                    <%--<a class="btn btn-info" role="button" id="btnBrowseJournalHeader" href="#modalTable">--%>
                   <a class="btn btn-info" id="btnBrowseJournalHeader" runat="server" onserverclick="btnBrowseJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-desktop bigger-110"></i>
                        Browse
                    </a>
                    <a class="btn btn-info" id="btnSaveJournalHeader" runat="server" onserverclick="btnSaveJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-floppy-o bigger-110"></i>
                        Save
                    </a>
                    <a class="btn btn-info" id="btnPrintJournalHeader" runat="server" onserverclick="btnPrintJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-print bigger-110"></i>
                        Print
                    </a>
                    <a class="btn btn-info" id="btnCancelJournalHeader" runat="server" onserverclick="btnCancelJournalHeader_ServerClick">
                        <i class="ace-icon fa fa-times bigger-110"></i>
                        Exit
                    </a>

                    <%--						<button class="btn" type="reset" id="btnReset" runat="server" onserverclick="btnReset_ServerClick">
							<i class="ace-icon fa fa-undo bigger-110"></i>
							Reset Period
						</button>--%>
                    <%--</div>--%>

                    <%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>

        <!--grid 2-->
        <div class="row no-padding-bottom" id="divGridStyle1" runat="server">
            <div class="col-xs-12">
                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->

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
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <HeaderTemplate>
                                            <table id="dynamic-table2" class="table table-striped table-bordered table-hover">
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
                                                    <asp:LinkButton ID="lbnID2" runat="server" CommandArgument='<%# Eval("txtID2") %>' Text='<%# Eval("txtID2") %>'></asp:LinkButton>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblChartNo2" runat="server" Text='<%# Eval("txtChartNo2") %>'></asp:Label>
                                                    <%--<a href="#">app.com</a>--%>
                                                </td>

                                                <td class="hidden-480">
                                                    <asp:Label ID="lblChartName2" runat="server" Text='<%# Eval("txtChartName2") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescription2" runat="server" Text='<%# Eval("txtDescription2") %>'></asp:Label>
                                                </td>
                                                <td class="hidden-480">
                                                    <asp:Label ID="lblCcy2" runat="server" Text='<%# Eval("txtCcy2") %>'></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <asp:Label ID="lblAmount2" runat="server" Text='<%# Eval("txtAmount2") %>'></asp:Label>
                                                    <%--<span class="label label-sm label-warning">Expiring</span>--%>
                                                </td>

                                                <td>
                                                    <div class="hidden-sm hidden-xs action-buttons">
                                                        <%--<a class="blue" href="#">--%>
<%--                                                        <a class="blue" id="btnPilih" runat="server" onserverclick="btnPilih_ServerClick">
                                                            <i class="ace-icon fa fa-search-plus bigger-130"></i>
                                                        </a>                                                        --%>
                                                        <asp:LinkButton ID="lbnEdit2" CommandName="Edit" CommandArgument='<%# Eval("txtID2") %>' OnClick="btnPilih_ServerClick" runat="server">
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
                    <!-- /.col -->

                </div>
            </div>
        </div>

        <!-- input detail style 2-->
        <div class="row" id="divDetailStyle2">
            <div id="modalFormJournal" class="modal" tabindex="-1">
    	        <div class="modal-dialog" style="width:900px;margin-top:150px">

		        <div class="modal-content">
			        <div class="modal-header">
				        <button type="button" class="close" data-dismiss="modal">&times;</button>
				        <h4 class="blue bigger">Input Detail Journal</h4>
			        </div>

			        <div class="modal-body">                
				        <div class="row">
					        <div class="col-xs-12 form-group">
                                <div class="form-group">
    						        <label class="col-lg-2 control-label no-padding-left">Chart No</label>
				                    <div class="col-lg-10 input-xxlarge no-padding-left">
                                        <select class="chosen-select2" id="SelectChartNoGrid2" name="Select1Name2" runat="server" data-placeholder="Choose Voucher">                                                         
                                        </select>
				                    </div>
                                </div>
					        </div>
				        </div>
				        <div class="row">
					        <div class="col-xs-12 form-group">
                            <div class="form-group">                                                    
						        <label class="col-lg-2 control-label no-padding-left">Description</label>
        			            <input class="col-lg-10 no-padding-left"  type="text" id="txtDescription2" runat="server" placeholder=" Input Description  "/>                       
                            </div>
					        </div>
				        </div>
				        <div class="row">
					        <div class="col-xs-12 form-group">
                                <div class="form-group">
    						        <label class="col-lg-2 control-label no-padding-left">Department</label>
				                    <div class="col-lg-4 input-medium no-padding-left">
                                        <select class="chosen-select" id="ddlDept2" name="Select1Name" runat="server" data-placeholder="Choose Dept">                                                         
                                        </select>
				                    </div>
            			            <input class="col-lg-7 no-padding-left"  type="text" id="txtDept2" runat="server" placeholder=" Non Department  "/>                       
                                </div>
					        </div>
				        </div>
				        <div class="row">
					        <div class="col-xs-12 form-group">
                                <div class="form-group">
    						        <label class="col-lg-2 control-label no-padding-left">Cost Center</label>
				                    <div class="col-lg-4 input-medium no-padding-left">
                                        <select class="chosen-select" id="ddlCostCenter2" name="Select1Name2" runat="server" data-placeholder="Choose Cost Center">                                                         
                                        </select>
				                    </div>
            			            <input class="col-lg-7 no-padding-left"  type="text" id="txtCostCenter2" runat="server" placeholder=" Non Cost Center "/>                       
                                </div>
					        </div>
				        </div>
				        <div class="row">
					        <div class="col-xs-12 form-group">
                                <%--<div class="form-group">--%>
                                <div>
    						        <label class="col-lg-2 control-label no-padding-left">Amount</label>
                			        <input class="col-lg-1 no-padding-left"  type="text" id="txtKurs2" runat="server" placeholder=" Currency " readonly/>                       
							        <label class="col-lg-1 no-padding-left">
								        <input name="switch-field-1" class="ace ace-switch ace-switch-4 btn-flat" type="checkbox" id="chkDebetCredit2" runat="server"/>
								        <span class="lbl" data-lbl="Debet&nbsp;&nbsp;&nbsp;Credit"></span>
							        </label>
                                    <input class="col-lg-5 no-padding-left text-right"  type="text" id="txtAmountGrid2" runat="server" placeholder=" Input Amount " onserverchange="txtAmountGrid2_ServerChange"/>                       
    						        <label class="col-lg-1 control-label">Rate</label>
            			            <input class="col-lg-1 text-right"  type="text" id="txtRate2" runat="server" placeholder=" 1.00 " readonly/>                       
                                </div>
						
					        </div>
				        </div>



			        </div>

			        <div class="modal-footer">
				        <a class="btn btn-sm btn-primary" id="btnSaveDialog" runat="server" onserverclick="btnSaveDialog_ServerClick">
					        <i class="ace-icon fa fa-check"></i>
					        Save
				        </a>
				        <a class="btn btn-sm" data-dismiss="modal">
					        <i class="ace-icon fa fa-times"></i>
					        Cancel
				        </a>
			        </div>
		        </div>

	            </div>
            </div>
        </div>



   	    <!--<div class="modal-dialog">-->
        <div class="row" id="divTableBrowse">
            <div id="modalTableBrowse" class="modal" tabindex="-1">
    	    <div class="modal-dialog" style="width:900px;margin-top:20px">

		        <div class="modal-content">
			        <div class="modal-header no-padding">
						<div class="table-header">
							<a  class="close" data-dismiss="modal" aria-hidden="true">
								<span class="white">&times;</span>
							</a>
							List Journal
						</div>
			        </div>
			        <div class="modal-body no-padding">                
                        <div>
                            <asp:Repeater ID="Repeater3" runat="server">
                                <HeaderTemplate>
                                    <table id="dynamic-table-browse" class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th class="center">
                                                    <label class="pos-rel">
                                                        <input type="checkbox" class="ace" />
                                                        <span class="lbl"></span>
                                                    </label>
                                                </th>
                                                <th class="hidden-480">ID</th>
                                                <th>Juornal Date</th>
                                                <th class="hidden-480">Voucher Code</th>
                                                <th>Voucher No</th>
                                                <th class="hidden-480">Remark</th>
                                                <%--<th>Amount</th>--%>

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
                                            <asp:LinkButton ID="lbnID3" runat="server" CommandArgument='<%# Eval("txtID3") %>' Text='<%# Eval("txtID3") %>'></asp:LinkButton>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblChartNo3" runat="server" Text='<%# Eval("txtChartNo3") %>'></asp:Label>
                                        </td>

                                        <td class="hidden-480">
                                            <asp:Label ID="lblChartName3" runat="server" Text='<%# Eval("txtChartName3") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription3" runat="server" Text='<%# Eval("txtDescription3") %>'></asp:Label>
                                        </td>
                                        <td class="hidden-480">
                                            <asp:Label ID="lblCcy3" runat="server" Text='<%# Eval("txtCcy3") %>'></asp:Label>
                                        </td>
<%--                                        <td class="text-right">
                                            <asp:Label ID="lblAmount2" runat="server" Text='<%# Eval("txtAmount2") %>'></asp:Label>
                                        </td>--%>

                                        <td>
                                            <div class="hidden-sm hidden-xs action-buttons">
                                                <asp:LinkButton ID="btnChooseJournal" CommandName="Edit" CommandArgument='<%# Eval("txtID3") %>' OnClick="btnChooseJournal_Click" runat="server">
                                                <i class="ace-icon fa fa-pencil bigger-130"></i></asp:LinkButton>
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

			        <div class="modal-footer">
				        <a class="btn btn-sm btn-danger" data-dismiss="modal">
					        <i class="ace-icon fa fa-times"></i>
					        Close
				        </a>
			        </div>

                </div>
            </div>            
            </div>
        </div>



    </div>

    

</asp:Content>




