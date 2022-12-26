<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLJournal2.aspx.cs" Inherits="GRCweb1.Modul.GeneralLedger.GLJournal1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--header-->
    <div class="row">
    	<div class="col-xs-12">
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
							<label class="col-sm-3 control-label" for="form-field-1">Voucher</label>
				            <div class="col-sm-9 input-group" >
                                <select class="chosen-select form-control" id="Select1" name="Select1Name" runat="server" data-placeholder="Choose Voucher" onserverchange="Select1_ServerChange">                                                         
                                </select>
				            </div>

                        </div>
                    </div>
				</div>
				<div class="col-xs-12 col-lg-6">
                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
								<label class="col-sm-3 control-label" for="form-field-1">Debet Trans</label>
								<div class="col-sm-9">
									<input type="text" id="txtDebetTrans" runat="server" placeholder="0" class="col-xs-10 col-sm-5 text-right" readonly="readonly"/>
								</div>
                        </div>
                    </div>
				</div>
			</div>
			<div class="row">
				<div class="col-xs-12 col-lg-6">
                    <div class="form-horizontal" role="form">                                                                            
						<div class="form-group">
			                <label class="col-sm-3 control-label" for="form-field-1">Journal #</label>
                            <div class="col-sm-4 no-padding-left" >
				                <div class="input-group" >
									<input type="text" id="txtJournalNo" runat="server" placeholder="Input Journal No"/>
				                </div>
			                </div>
                            <div class="col-sm-5 no-padding-left" >
				                <div class="input-group" >
					                <input class="form-control no-padding-right date-picker" placeholder="Input Journal Date" id="txtJournalDate" type="text" data-date-format="dd-mm-yyyy" runat="server"/>
					                <span class="input-group-addon">
						                <i class="fa fa-calendar bigger-110"></i>
					                </span>
				                </div>
			                </div>
						</div>

                    </div>
				</div>
				<div class="col-xs-12 col-lg-6">
                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
							<label class="col-sm-3 control-label" for="form-field-1">Credit Trans</label>
							<div class="col-sm-9">
								<input type="text" id="txtCreditTrans" runat="server" placeholder="0" class="col-xs-10 col-sm-5 text-right" readonly="readonly"/>
							</div>
                        </div>
                    </div>
				</div>
			</div>
			<div class="row">
				<div class="col-xs-12 col-lg-6">
                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
							<label class="col-sm-3 control-label" for="form-field-1">Remark</label>
				            <div class="col-sm-9 input-group" >
									<input class="form-control" type="text" id="txtJournalRemark" runat="server" placeholder="Input Journal Remark"/>
				            </div>

                        </div>
                    </div>
				</div>
				<div class="col-xs-12 col-lg-6">
                    <div class="form-horizontal" role="form">                                                    
                        <div class="form-group">
								<label class="col-sm-3 control-label" for="form-field-1">Balance</label>
								<div class="col-sm-9">
									<input type="text" id="txtBalance" placeholder="0" runat="server" class="col-xs-10 col-sm-5 text-right" readonly="readonly"/>
								</div>
                        </div>
                    </div>
				</div>
			</div>

        </div>
    </div>


    <!--table/grid-->
    <div class="row">
    	<div class="col-xs-12">

                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                 Width="100%" onrowcommand="GridView1_RowCommand" 
                                  onrowcreated="GridView1_RowCreated" 
                                   onrowdatabound="GridView1_RowDataBound" >
                                 <Columns>      
                                      <asp:TemplateField HeaderText="Group Items" ItemStyle-Width="135px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlGroupItems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupItems_SelectedIndexChanged" Width="180px" CausesValidation="False">
                                            </asp:DropDownList>
                                                 
                                        </ItemTemplate>
                                        <ItemStyle Width="135px" />
                                     </asp:TemplateField>                                                                                              
                                     <asp:TemplateField HeaderText="Nama Item" ItemStyle-Width="290px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlItemCode" runat="server" Width="330px" AutoPostBack="true" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                         <ItemStyle Width="290px" />
                                     </asp:TemplateField>      
                                     <asp:TemplateField HeaderText="Sat" ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSatuan" ReadOnly="true" Width="35px" AutoPostBack="true" ToolTip="1" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                         <ItemStyle Width="35px" />
                                     </asp:TemplateField>   
                                      <asp:TemplateField HeaderText="Std" ItemStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtType" ReadOnly="true" Width="40px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                          <ItemStyle Width="40px" />
                                     </asp:TemplateField>   
                                      <asp:TemplateField HeaderText="Jumlah" ItemStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" Width="40px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                          <ItemStyle Width="40px" />
                                     </asp:TemplateField>                                       
                                     <asp:TemplateField HeaderText="Harga" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrice" Width="70px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                         <ItemStyle Width="70px" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Point" ItemStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPoint" ReadOnly="true" Width="40px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                         <ItemStyle Width="40px" />
                                     </asp:TemplateField>                   
                                     <asp:ButtonField CommandName="Add" ImageUrl="~/images/add.gif" 
                                          ButtonType="Image"/>
                                     <asp:ButtonField CommandName="AddDelete" ButtonType="Image" 
                                          ImageUrl="~/images/trash.gif" />
                                 </Columns>
                                 <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                 <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                 <PagerStyle BorderStyle="Solid" />
                                 <AlternatingRowStyle BackColor="Gainsboro" />
                             </asp:GridView>                          


        </div>
    </div>



</asp:Content>
