<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLListChartOfAccount.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLListChartOfAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">

                <asp:updatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <contentTemplate>
                
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
                
                </contentTemplate>
                </asp:updatePanel>

				<%--<div class="hr hr-24"></div>--%>






			</div>

		</div><!-- /.col -->

	</div><!-- /.row -->

</asp:Content>
