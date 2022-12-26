<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPostingPencapaian.aspx.cs" Inherits="GRCweb1.Modul.SasaranMutu.FormPostingPencapaian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">
				<div class="form-group">
					<label class="col-sm-3 control-label no-padding-right" for="form-field-1"> Periode </label>

					<div class="col-sm-9">
                        <span class="input-icon">
                            <asp:DropDownList ID="ddlBulan1" class="col-sm-3 form-control center" runat="server" AutoPostBack="true">
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
            </div>
        </div>
    </div>


	<div class="row">
		<div class="col-xs-12">
			<!-- PAGE CONTENT BEGINS -->
			<div class="form-horizontal" role="form">
				<div class="form-group">
                    <%--<label for="inputWarning" class="col-xs-12 col-sm-3 control-label no-padding-right">Input with warning</label>--%>
					<div class="col-xs-12 col-sm-5">
					    <span class="block input-icon input-icon-right">
						    <label id="Judul1" class="width-100"> Sasaran Mutu Department </label>
						</span>
					</div>
                    <div class="col-xs-12 col-sm-2">

                    </div>
                    <div class="help-block col-xs-12 col-sm-reset inline"> Status </div>
				</div>
				<div class="form-group">
					<div class="col-xs-12 col-sm-5">
					    <span class="block input-icon input-icon-right">
						    <input type="text" id="Text1" class="width-100" runat="server" visible="false"/>
						</span>
					</div>
                    <button class="col-xs-12 col-sm-2 btn btn-prev" id="Button1" runat="server" visible="false">
						Posting
					</button>
                    <div class="help-block col-xs-12 col-sm-reset inline" id="div1" runat="server"></div>
				</div>
				<div class="form-group">
					<div class="col-xs-12 col-sm-5">
					    <span class="block input-icon input-icon-right">
						    <input type="text" id="Text2" class="width-100" runat="server" visible="false"/>
						</span>
					</div>
                    <button class="col-xs-12 col-sm-2 btn btn-prev" id="Button2" runat="server" visible="false">
						Posting
					</button>
                    <div class="help-block col-xs-12 col-sm-reset inline" id="div2" runat="server"></div>
				</div>



            </div>
        </div>
    </div>

</asp:Content>
