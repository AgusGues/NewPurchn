<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLMonthlyBalanceSheet.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLMonthlyBalanceSheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<div class="col-xs-12">
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
                                            <select class="chosen-select form-control" id="Select1" name="Select3Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>
                                    <span>
    									<label class="col-sm-1 control-label no-padding-right no-padding-left" for="form-field-1"> to </label>
                                    </span>
                                    <div class="col-sm-4">
										<div class="input-group">
                                            <select class="chosen-select form-control" id="Select2" name="Select4Name" runat="server" data-placeholder="Choose Chart No" contenteditable="true">                                                         
                                            </select>
										</div>
									</div>

								</div>
                            </div>

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
