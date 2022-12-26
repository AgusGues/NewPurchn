<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPakaiBaku.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPakaiBaku" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <!DOCTYPE html>

<html>
<head>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
	<meta charset="utf-8" />
	<title>Widgets - Ace Admin</title>
	<meta name="description" content="Common form elements and layouts" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
	--%>

     <script language="javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
    <%--<script language="javascript" type="text/javascript" src="../../Script/jquery-1.2.6-vsdoc.js"></script>--%>

    <script language="javaScript" type="text/javascript">
        
        function imgChange(img) {
            document.LookUpCalendar.src = img;
        }
        function onCancel()
        { }

        function Cetak() {
            var wn = window.showModalDialog("../../Report/Report.aspx?IdReport=SlipPakaiBaku", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
        }

        function confirm_delete() {
            if (confirm("Anda yakin untuk Cancel ?") == true)
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
            else
                return false;
        }
        
        function confirm_del() {
            if (confirm("Yakin data ini akan dihapus?") == true) 
            {
                document.getElementById('<%= hps.ClientID %>').value = "ya";
               
            } else {
            document.getElementById('<%= hps.ClientID %>').value = "no";
           
            
            }
        }
        function Resethps() {
            document.getElementById('<%=hps.ClientID %>').value = "";
        }
    
        
            $("input.org").click(function() {
                $(this).val('ok');
            })
            $('#GridView1').click(function()
            {
            alert($(this).attr("ID"));
            })
        
        function confirm_del() {
            if (confirm("Yakin data ini akan dihapus?") == true) {
                $('input.org').val('ya');
            } else {
                //document.getElementById('<%= hps.ClientID %>').value = "no";
            $('input.org').val('no');
            }
        }
        function confirm_hapus() {
            if (confirm("Anda yakin untuk akan dihapus ?") == true) {
                window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogheight: 300px; dialogWidth: 400px;scrollbars=yes');
            } else {
                return;
            }
        }
    </script>
   <%-- <style>
		.panelbox {background-color: #efeded;padding: 2px;}
		html,body,.form-control,button{font-size: 11px;}
		.input-group-addon{background: white;}
		.fz11{font-size: 11px;}
		.the-loader{
			position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
			text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
		}
		.input-xs{
			font-size: 11px;height: 11px;width: 100%;
		}
		.modal-footer{
			padding-left: : 1px;padding-top: 2px;padding-right: 1px;padding-bottom: 2px;
		}
		.btn-data{padding:1px 1px;font-size:8px;line-height:0.5;border-radius:3px;}
	</style>
</head>
<body class="no-skin">--%>
   <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancel"
                ConfirmText="Anda yakin untuk Cancel Surat Jalan?" OnClientCancel="onCancel"
                ConfirmOnFormSubmit="false" />
             
    <div class="row">
		<div class="col-md-12">
            <div class="panel panel-primary">
				<div class=panel-heading>
                    <span class="the-title">Bahan Baku Drawing Slip</span>
                    <div class="pull-right">
                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; color: #000000;" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; color: #000000;" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                        <asp:Button ID="btnCancel" runat="server" Style="background-color: white; font-weight: bold; font-size: 11px;" Text="Cancel" OnClick="btnCancel_ServerClick" ForeColor="Black" />
                        <input id="btnPrint" onclick="Cetak()" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; color: #000000;" type="button" value="Cetak" />
                        <asp:Button ID="btnUnlok" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" Text="UnLock" ToolTip="Click Unlock to Edit Data by user"  OnClick="btnUnlock_ServerClick" Visible="false" ForeColor="Black" />  
                        <input id="btnList" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; color: #000000;" type="button" value="List" onserverclick="btnList_ServerClick" />
                        <asp:DropDownList ID="ddlSearch" runat="server" Width="120px" Height="25px">
                            <asp:ListItem Value="ScheduleNo">BBDS No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSearch" runat="server" Width="128px" Height="20px"></asp:TextBox>
                        <input id="btnSearch" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; color: #000000;" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                    </div>
                </div>
                <div id="Div5" class="panel-body panel-list" runat="server">
					<div  class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-4">
                                        <input id="hpuse" runat="server" class="org" type="text" value="" visible="false" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="col-md-6">
                                <div class="row">
								    <div class="col-md-4">
                                        <asp:TextBox ID="hps" runat="server" Visible="false" AutoPostBack="True" OnTextChanged="hps_TextChanged" CssClass="tst"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                            <div style="padding: 2px"></div>
						<div class="col-md-6">
                            <div class="row">
								<div class="col-md-3">No BBDS</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPakaiNo" runat="server" CssClass="form-control input-sm" ReadOnly="True" Height="30px"></asp:TextBox>
                                  
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Nama Dept</div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Height="30px" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Produksi Line</div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlProdLine" runat="server" CssClass="form-control input-sm" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Shift</div>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlShift" runat="server" AutoPostBack="True" onselectedindexchanged="ddlShift_SelectedIndexChanged" CssClass="form-control input-sm">
                                        <asp:ListItem>Shift 1</asp:ListItem>
                                        <asp:ListItem>Shift 2</asp:ListItem>
                                        <asp:ListItem>Shift 3</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div style="padding: 2px"></div>
                            <div class="row">
							    <div class="col-md-3">Cari Nama Brg</div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtCariNamaBrg" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Nama Barang</div>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="stk" runat="server" Text="Stocked" Checked="true" />
                                 </div>
                            </div>
                             <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Kode Barang</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Satuan</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtUom" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3"> Keterangan</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtKeterangan" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
								<div class="col-md-3">Dibuat Oleh</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
							<div class="row">
                                <div class="col-md-3">Stok</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtStok" runat="server" AutoPostBack="True" CssClass="form-control input-sm" Enabled="False" Font-Bold="True" ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                            
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-3">Kode Dept</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtKodeDept" runat="server" AutoPostBack="True" CssClass="form-control input-sm" ReadOnly="True"></asp:TextBox>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-3">Tanggal</div>
                                <div class="col-md-6">
                                     <asp:TextBox ID="txtTanggal" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                 </div>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTanggal" Format="dd-MMM-yyyy"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                            </div>
                            <div style="padding: 55px"></div>
                       
                            
                            
                        </div>
                        <div style="padding: 2px"></div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-3">Qty Pakai</div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtQtyPakai" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                 </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-3">Status</div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtUomID" runat="server" AutoPostBack="True" CssClass="form-control input-sm" ReadOnly="True" Visible="False"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtGroupID" runat="server" AutoPostBack="True" CssClass="form-control input-sm" ReadOnly="True" Style="margin-top: 0px" Visible="False" Width="52px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="padding: 2px"></div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">

                                 </div>
                            </div>
                            <div style="padding: 20px"></div>
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                     <asp:Button ID="lbAddOP" runat="server" Text="Tambah" OnClick="lbAddOP_Click" />
                                 </div>
                            </div>
                        </div>
                        <div style="padding: 2px"></div>
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Panel ID="Panel2" runat="server" BackColor="White" Font-Size="Smaller" Height="41px" HorizontalAlign="Center" Width="174px">
                                        <br />
                                        <asp:Button ID="Button1" runat="server" Height="25px" OnClick="Button1_Click" Text="Matikan Alarm ReOrder 1 menit" Width="162px" Font-Size="XX-Small" />
                                    </asp:Panel>
                                    </div>
                                <div class="col-md-4">
                                    <span style="font-size: 10pt">
                                       <asp:Timer ID="Timer2" runat="server" Enabled="False" Interval="1000" OnTick="Timer2_Tick">
                                       </asp:Timer>
                                       </span>
                                </div>
                                <div class="col-md-4">
                                    <span style="font-size: 10pt">
                                        <asp:Timer ID="Timer1" runat="server" Enabled="False" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           <div class="panel panel-primary">
				
                <div class="panel-body panel-list">
					<div class="row">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" Width="100%" Visible="true">
                            <Columns>
                                <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                <asp:BoundField DataField="ItemName" HeaderText="Nama Barang" />
                                <asp:BoundField DataField="Quantity" HeaderText="Jumlah" />
                                <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                <asp:BoundField DataField="LineNo" HeaderText="Line" />
                                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                <asp:ButtonField CommandName="AddDelete" Text="Hapus" Visible="true" />
                                <asp:BoundField DataField="ID" Visible="false" />
                                             
                           </Columns>
                           <%--<RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                           <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                           <PagerStyle BorderStyle="Solid" />
                           <AlternatingRowStyle BackColor="Gainsboro" />--%>
                        </asp:GridView>
                        <table width="100%" style="border-collapse:collapse; font-size:smaller; display:none" visible="true">
                            <tr class="tbHeader tengah">
                                <th class="kotak" style="width:5%">#</th>
                                <th class="kotak" style="width:10%">Kode Barang</th>
                                <th class="kotak" style="width:30%">Nama Barang</th>
                                <th class="kotak" style="width:12%">Jumlah</th>
                                <th class="kotak" style="width:8%">UOM</th>
                                <th class="kotak" style="width:5%">Line</th>
                                <th class="kotak" style="width:25%">Keterangan</th>
                                <th class="kotak" style="width:5%">&nbsp;</th>
                            </tr>
                            <asp:Repeater ID="lstSPB" runat="server" OnItemDataBound="lstSPB_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="baris"  style="background-color:#DCDCDC">
                                        <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                        <td class="kotak kiri"><%# Eval("ItemName") %></td>
                                        <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                        <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                        <td class="kotak tengah"><%# Eval("LineNo") %></td>
                                        <td class="kotak kiri"><%#Eval("Keterangan") %></td>
                                        <td class="kotak tengah">
                                            <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus" OnClientClick="confirm_hapus()" CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="baris">
                                        <td class="kotak tengah"><%# Container.ItemIndex + 1 %></td>
                                        <td class="kotak tengah"><%# Eval("ItemCode") %></td>
                                        <td class="kotak kiri"><%# Eval("ItemName") %></td>
                                        <td class="kotak angka"><%# Eval("Quantity","{0:N2}") %></td>
                                        <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                        <td class="kotak tengah"><%# Eval("LineNo") %></td>
                                        <td class="kotak kiri"><%#Eval("Keterangan") %></td>
                                        <td class="kotak tengah">
                                        <asp:Label ID="Act" runat="server">
                                        <asp:ImageButton runat="server" ImageUrl="~/images/Delete.png" ID="btnHapus" onclientclick="confirm_hapus();"  CommandArgument='<%#Eval("ID") %>' CommandName="Hapus" /></asp:Label>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
<%--</body>
    </html>--%>

</asp:Content>
