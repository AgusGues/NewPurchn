<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewLapPemantauanStatusSPP.aspx.cs" Inherits="GRCweb1.Modul.ListReport.NewLapPemantauanStatusSPP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>  
    <script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $('div.contentlist').css({ 'height': screen.height - 370 });
    });
    function alasanpending() {
            alert(" alasan ya");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="div1" runat="server">
        <div class="row">
		    <div class="col-md-12">
                <div class="panel panel-primary">
				    <div class=panel-heading>
                        <span class="the-title">PEMANTAUAN SPP</span>
                    </div>
                    <div  class="row" >
                        <div class="col-md-12">
                            <div style="padding: 2px"></div>
                             <div class="row">
						        <div class="col-md-2" style="text-align: right">Periode</div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDrTgl" runat="server" CssClass="form-control input-sm"></asp:TextBox> </div>
                                <div class="col-md-1"style="text-align: center">s/d</div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSdTgl" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <asp:DropDownList ID="ddlTahun" runat="server" Visible="false"></asp:DropDownList>
                                    <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtDrTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CE2"  runat="server" TargetControlID="txtSdTgl" Format="dd-MMM-yyyy" EnableViewState="true"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2" style="text-align: right">Material Group</div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtGroupID" runat="server" Visible="false" Font-Size="X-Small" ></asp:TextBox>
                                    <asp:DropDownList ID="ddlGroupID" runat="server" Visible="false" ></asp:DropDownList>
                                                
                                                <!--test multi select dropdown-->
                                    <asp:CheckBoxList ID="ddlGroup" BorderStyle="Solid" runat="server" BackColor="White" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="cursor" Font-Size="10pt">
                                        
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2"></div>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="noStatusPO" runat="server" Text="Tanpa Status PO" /> &nbsp; &nbsp; &nbsp;
                                    <asp:RadioButton id="rbtStock" Text="Stock" GroupName="Type" runat="server"/> &nbsp;
                                    <asp:RadioButton id="rbtNonStock" Text="Non Stock" GroupName="Type" runat="server"/>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
						        <div class="col-md-2"></div>
                                <div class="col-md-1">
                                    <asp:Button ID="preview" runat="server" Text="Preview" cssclass="btn-xs btn-success" OnClick="preview_Click" /></div>
                                <div class="col-md-1">
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
                                <table class="table table-striped table-bordered table-hover" style="width:100%; border-collapse:collapse" border="1">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:3%">No</th>
                                            <th class="kotak" style="width:10%">No SPP</th>
                                            <th class="kotak" style="width:5%">Tanggal Apv</th>
                                            <th class="kotak" style="width:5%">Waktu</th>
                                            <th class="kotak" style="width:25%">Nama Barang</th>
                                            <th class="kotak" style="width:5%">Qty</th>
                                            <th class="kotak" style="width:10%">Status SPP</th>
                                            <th class="kotak" style="width:3%">Lead Time</th>
                                            <th class="kotak" style="width:5%">Sch Delivery</th>
                                            <th class="kotak" style="width:5%">Act Delivery</th>
                                            <th class="kotak" style="width:8%">No. RMS</th>
                                            <th class="kotak" style="width:4%">Keterlam batan</th>
                                        </tr>
                                       
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="lstSPP" runat="server" OnItemDataBound="lstSPP_DataBound">
                                            <ItemTemplate>
                                                <tr class="total baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak "><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate2","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate2","{0:HH:mm:ss}") %></td>
                                                    <td class="kotak" colspan="8"></td>                                                   
                                                </tr>
                                                <asp:Repeater ID="lstDetailSPP" runat="server" OnItemDataBound="lstDtlSPP_DataBound" OnItemCommand="lstDtlSPP_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak angka" valign="top"><%# Container.ItemIndex+1 %>&nbsp;</td>
                                                            <td class="kotak" ><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                            <td class="kotak">
                                                            <asp:Label ID="txt" runat="server" Width="75%" Text='<%# Eval("Satuan") %>'></asp:Label>
                                                            <asp:Panel ID="edt" runat="server" style="width:100%">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDown" OnSelectedIndexChanged="ddlStatus_Change" AutoPostBack="true">
                                                                    <%--<asp:ListItem Value="0">--pilih alasan--</asp:ListItem>--%>
                                                                    <asp:ListItem Value="0">Spesifikasi Tidak Lengkap</asp:ListItem>
                                                                    <asp:ListItem Value="1">Menunggu Perbandingan Harga</asp:ListItem>
                                                                    <asp:ListItem Value="2">Discontinoue</asp:ListItem>
                                                                    <asp:ListItem Value="3">Input Manual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtInputMan" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:ImageButton ID="simpan" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="save" ImageUrl="~/assets/img/disk.jpg" />
                                                                </asp:Panel>
                                                                <span id="ext" runat="server" style="width:10%; text-align:right" >
                                                                <asp:ImageButton ID="updSts" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="upd" ImageUrl="~/assets/img/folder.gif" ToolTip="Click For Edit" />
                                                                </span>
                                                            </td>
                                                            <td class="kotak tengah"><%# Eval("ItemID") %></td>
                                                            <td class="kotak"><%# Eval("TglKirim","{0:d}") %></td>
                                                            <td class="kotak"><%# Eval("AlasanPending") %></td>
                                                            <td class="kotak tengah"><%# Eval("CariItemName")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak angka" valign="top"><%# Container.ItemIndex+1 %>&nbsp;</td>
                                                            <td class="kotak"><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                            <td class="kotak">
                                                                <asp:Label ID="txt" runat="server" Width="75%" Text='<%# Eval("Satuan") %>'></asp:Label>
                                                            <asp:Panel ID="edt" runat="server" style="width:100%">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDown" OnSelectedIndexChanged="ddlStatus_Change">
                                                                    <asp:ListItem Value="0">Spesifikasi Tidak Lengkap</asp:ListItem>
                                                                    <asp:ListItem Value="1">Menunggu Perbandingan Harga</asp:ListItem>
                                                                    <asp:ListItem Value="2">Discontinoue</asp:ListItem>
                                                                    <asp:ListItem Value="3">Input Manual</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtInputMan" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:ImageButton ID="simpan" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="save" ImageUrl="~/assets/img/check.png" />
                                                                </asp:Panel>
                                                                <span id="ext" runat="server" style="width:10%; text-align:right" >
                                                                    <asp:ImageButton ID="updSts" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="upd" ImageUrl="~/assets/img/folder.gif" />
                                                                </span>
                                                            </td>
                                                            <td class="kotak tengah"><%# Eval("ItemID") %></td>
                                                            <td class="kotak"><%# Eval("TglKirim","{0:d}") %></td>
                                                            <td class="kotak"><%# Eval("AlasanPending") %></td>
                                                            <td class="kotak tengah"><%# Eval("CariItemName")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <div id="lstForPrint" runat="server" style="width:5px; height:5px; overflow:hidden">
                            <table class="tblForm" style="width:100%; border-collapse:collapse" border="1">
                                    <thead>
                                        <tr class="tbHeader">
                                            <th class="kotak" style="width:3%">No</th>
                                            <th class="kotak" style="width:10%">No SPP</th>
                                            <th class="kotak" style="width:5%">Tanggal Apv</th>
                                            <th class="kotak" style="width:5%">Waktu</th>
                                            <th class="kotak" style="width:25%">Nama Barang</th>
                                            <th class="kotak" style="width:5%">Qty</th>
                                            <th class="kotak" style="width:10%">Status SPP</th>
                                            <th class="kotak" style="width:3%">Lead Time</th>
                                            <th class="kotak" style="width:5%">Sch Delivery</th>
                                            <th class="kotak" style="width:5%">Act Delivery</th>
                                            <th class="kotak" style="width:8%">No. RMS</th>
                                            <th class="kotak" style="width:4%">Keterlam batan</th>
                                        </tr>
                                       
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="lstSPP_DataBound4">
                                            <ItemTemplate>
                                                <tr class="total baris">
                                                    <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                    <td class="kotak "><%# Eval("NoSPP") %></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate2","{0:d}") %></td>
                                                    <td class="kotak tengah"><%# Eval("ApproveDate2","{0:HH:mm:ss}") %></td>
                                                    <td class="kotak" colspan="8"></td>                                                   
                                                </tr>
                                                <asp:Repeater ID="lstDetailSPP4" runat="server" >
                                                    <ItemTemplate>
                                                        <tr class="OddRows baris">
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak angka" valign="top"><%# Container.ItemIndex+1 %>&nbsp;</td>
                                                            <td class="kotak" ><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                            <td class="kotak"><%# Eval("Satuan") %></td>
                                                            <td class="kotak tengah"><%# Eval("ItemID") %></td>
                                                            <td class="kotak"><%# Eval("TglKirim","{0:d}") %></td>
                                                            <td class="kotak"><%# Eval("AlasanPending") %></td>
                                                            <td class="kotak tengah"><%# Eval("CariItemName")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="EvenRows baris">
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak">&nbsp;</td>
                                                            <td class="kotak angka" valign="top"><%# Container.ItemIndex+1 %>&nbsp;</td>
                                                            <td class="kotak"><%# Eval("ItemName") %></td>
                                                            <td class="kotak angka"><%# Eval("Quantity","{0:N0}") %></td>
                                                            <td class="kotak"><%# Eval("Satuan") %></td>
                                                            <td class="kotak tengah"><%# Eval("ItemID") %></td>
                                                            <td class="kotak"><%# Eval("TglKirim","{0:d}") %></td>
                                                            <td class="kotak"><%# Eval("AlasanPending") %></td>
                                                            <td class="kotak tengah"><%# Eval("CariItemName")%></td>
                                                            <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    
                                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
