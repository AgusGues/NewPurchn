<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantauanNotMatchSPP.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantauanNotMatchSPP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="div1" runat="server">
       <div class="row">
		    <div class="col-md-12">
                <div class="panel panel-primary">
				    <div class=panel-heading>
                        <span class="the-title">LEMBAR PEMANTAUAN ON TIME & KESESUAIAN PEMENUHAN SPP</span>
                    </div>
                    <div  class="row" >
                        <div class="col-md-12">
                            <div style="padding: 2px"></div>
                             <div class="row">
						        <div class="col-md-2" style="text-align: right">Periode</div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDrTgl" runat="server"></asp:TextBox> </div>
                                <div class="col-md-1"style="text-align: center">s/d</div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSdTgl" runat="server"></asp:TextBox>
                                    <asp:DropDownList ID="ddlTahun" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2" style="text-align: right">Material Group</div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtGroupID" runat="server" Visible="false" Font-Size="X-Small" ></asp:TextBox>
                                    <asp:DropDownList ID="ddlGroupID" runat="server" Visible="false" ></asp:DropDownList>
                                             
                                    <!--test multi select dropdown-->
                                    <asp:CheckBoxList ID="ddlGroup" BorderStyle="Solid" runat="server" BackColor="White" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="cursor">
                                                
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2" style="text-align: right">View Data Base on</div>
                                <div class="col-md-8">
                                    <asp:RadioButtonList ID="rbData" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"> 
                                        <asp:ListItem Value="SPP">SPP Date</asp:ListItem> 
                                        <asp:ListItem Value="RMS">Receipt Date</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkAll" runat="server" Text="Hanya yang Tidak sesuai" Checked="true"  />
                                </div>
                                <div class="col-md-4">
                                    <asp:RadioButton id="rbtStock" Text="Stock" GroupName="Type" runat="server"/> &nbsp;
                                    <asp:RadioButton id="rbtNonStock" Text="Non Stock" GroupName="Type" runat="server"/>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    <asp:Button ID="preview" runat="server" Text="Preview" cssclass="btn-xs btn-success" OnClick="preview_Click" />
                                    <asp:Button ID="ExportXls" runat="server" cssclass="btn-xs btn-success" Text="Export to Excel" OnClick="ExportToExcel" />
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary">
                    <div class="panel-body panel-list">
					    <div class="row">
                            <div id="lst" runat="server" class="contentlist" style="height:380px; overflow:auto">
                                <table class="table table-striped table-bordered table-hover" style="width:100%; border-collapse:collapse; font-size:xx-small" border="0">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:3%">No</th>
                                            <th class="kotak" style="width:7%">SPP No</th>
                                            <th class="kotak" style="width:7%; white-space:nowrap">PO No</th>
                                            <th class="kotak" style="width:12%">Supplier</th>
                                            <th class="kotak" style="width:30%">Item Name</th>
                                            <th class="kotak" style="width:4%">Qty</th>
                                            <th class="kotak" style="width:4%">Lead Time</th>
                                            <th class="kotak" style="width:8%">Schedule Delivery</th>
                                            <th class="kotak" style="width:6%">Actual Delivery</th>
                                            <th class="kotak" style="width:7%; white-space:nowrap">No RMS</th>
                                            <th class="kotak" style="width:4%">Time(Hari)</th>
                                            <th class="kotak" style="">Keterangan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstLate" runat="server">
                                            <ItemTemplate>
                                                <tr class="EvenRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak"><%# Eval("PONo") %></td>
                                                    <td class="kotak"><%# Eval("SupplierName") %></td>
                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                    <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                    <td class="kotak tengah"><%# Eval("LeadTime") %></td>
                                                    <td class="kotak tengah"><%# Eval("DelivDate","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Minta", "{0:d}")%></td>
                                                    <td class="kotak tengah"><%# Eval("RMSNo") %></td>
                                                    <td class="kotak tengah"><%# Eval("Lambat") %></td>
                                                    <td class="kotak"><%# Eval("Keterangan") %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="OddRows baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak tengah"><%# Eval("NoSPP") %></td>
                                                    <td class="kotak"><%# Eval("PONo") %></td>
                                                    <td class="kotak"><%# Eval("SupplierName") %></td>
                                                    <td class="kotak"><%# Eval("ItemName") %></td>
                                                    <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                    <td class="kotak tengah"><%# Eval("LeadTime") %></td>
                                                    <td class="kotak tengah"><%# Eval("DelivDate","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("Minta", "{0:d}")%></td>
                                                    <td class="kotak tengah"><%# Eval("RMSNo") %></td>
                                                    <td class="kotak tengah"><%# Eval("Lambat") %></td>
                                                    <td class="kotak"><%# Eval("Keterangan") %></td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr class="Line3">
                                            <td colspan="11" class="angka kotak" style="text-align: right">Total Item SPP</td>
                                            <td class="kotak angka tp"><%=TotalSPP.ToString("###,###.#0") %></td>
                                        </tr>
                                        <tr class="EvenRows">
                                            <td colspan="11" class="angka kotak" style="text-align: right">Total Not Match</td>
                                            <td class="kotak angka tm"><%=TotalLate.ToString("###,###.#0") %></td>
                                        </tr>
                                        <tr class="Line3">
                                            <td colspan="11" class="angka kotak" style="text-align: right">Pencapaian</td>
                                            <td class="kotak angka pr"><%=Procentase.ToString("###,###.#0") %> %</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
