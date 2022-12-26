<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPenawaran.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.FormPenawaran" %>
<%--taroh di setelah 1 baris pertama file--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--source html dimulai dari sini--%>

    <!DOCTYPE html>
    <html lang="en">
    <head>
       

        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Widgets - Ace Admin</title>
        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
        <link rel="stylesheet" href="../../assets/select2.css" />
        <link rel="stylesheet" href="../../assets/datatable.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <style>
        .panelbox {background-color: #efeded;padding: 2px;}
        html,body,.form-control,button{font-size: 11px;}
        .input-group-addon{background: white;}
        .fz11{font-size: 11px;}
        .fz10{font-size: 10px;}
        .the-loader{
            position: fixed;top: 0px;left: 0px; ; width:100%; height: 100%;background-color: rgba(0,0,0,0.1);font-size: 50px;
            text-align: center; z-index: 666;font-size: 13px;padding: 4px 4px; font-size: 20px;
        }
        .input-xs{
            font-size: 11px;height: 11px;
        }
    </style>

        <script language="JavaScript">   
            function openWindow() {
                window.showModalDialog("../../ModalDialog/FormPOPurchn.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
            }

            function onCancel() {

            }
            //function Cetak() {
            //    var wn = window.showModalDialog("../../Report/Report2.aspx?IdReport=Tawar", "", "resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes");
            //}

            function MyPopUpWin(url, width, height) {
                var leftPosition, topPosition;
                leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
                topPosition = (window.screen.height / 2) - ((height / 2) + 50);
                window.open(url, "Window2",
                    "status=no,height=" + height + ",width=" + width + ",resizable=yes,left="
                    + leftPosition + ",top=" + topPosition + ",screenX=" + leftPosition + ",screenY="
                    + topPosition + ",toolbar=no,menubar=no,scrollbars=no,location=no,directories=no");
            }

            function Cetak() {
                var cek;
                MyPopUpWin("../Report/Report.aspx?IdReport=Tawar", 900, 800)
            }

            function confirm_batal() {
                if (confirm("Anda yakin untuk Batal ?") == true)
                    window.showModalDialog('../../ModalDialog/ReasonCancel.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
                else
                    return false;
            }
            function confirm_close() {
                if (confirm("Anda yakin untuk Close ?") == true)
                    window.showModalDialog('../../ModalDialog/ReasonClose.aspx', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
                else
                    return false;
            }


        </script>

        
    </head>
	
        <body class="no-skin">
		
		<%--Panel di dipindah disini jika pakai, jika tidak ada dihapus saja--%>
		<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">    
		<ContentTemplate>  
		<%--Panel di pindah disini jika pakai jika tidak ada dihapus saja--%>
		
		
            <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Input Penawaran
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>
			
                    <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                    <table style="table-layout: fixed; width: 100%;" height="100%" cellspacing="0" cellpadding="0" width="100%">
                        <tbody>

                            <tr>
                                <td style="width: 100%;">
                                    <table class="nbTableHeader">
                                        <tbody>
                                            <tr>
                                                <%--<td style="width: 703px; height: 49px">
                                                    <table class="nbTableHeader">
                                                </td>--%>
                                                <tr>
                                                    <td style="width: 216%">
                                                        <%--<strong>Input Penawaran</strong>--%>

                                                    </td>
                                                    <td style="width: 37px">
                                                        <input id="btnNew" CssClass="btn btn-primary btn-sm" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" /></td>
                                                    <td style="width: 75px">
                                                        <input id="btnUpdate" CssClass="btn btn-primary btn-sm" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" /></td>
                                                    
                                                    <td style="width: 5px">
                                                        <input id="btnPrint" CssClass="btn btn-primary btn-sm" onclick="Cetak()" runat="server" type="button" value="Cetak" /></td>

                                                    <td>
                                                        <input id="btnList" runat="server" CssClass="btn btn-primary btn-sm" type="button" value="List" onserverclick="btnList_ServerClick" /></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSearch" CssClass="form-control input-sm" runat="server" Width="100px">
                                                            <asp:ListItem Value="NoPO">No. Tawar</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:TextBox ID="txtSearch" Width="100px" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                                                    <td style="width: 3px">
                                                        <input id="btnSearch"  runat="server" CssClass="btn btn-primary btn-sm" type="button" value="Cari" onserverclick="btnSearch_ServerClick" /></td>
                                                </tr>
                                            
                                </td>
                            </tr>
                            
                            <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
                                <tbody>
                                    <tr class="treeRow1" valign="top">
                                        <td>
                                            <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" border="1" height="100%" width="100%">
                                                <tbody>
                                                    <tr style="width: 100%; height: 100%">
                                                        <td style="width: 100%; height: 100%;">
                                                            <%--<DIV style="OVERFLOW: auto; HEIGHT: 100%; width: 100%;" >--%>
                                                            <div>
                                                                <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td style="height: 101px; width: 100%;">
                                                                            <table id="Table4" style="left: 0px; top: 0px; width: 100%;" cellspacing="1"
                                                                                cellpadding="0" border="0">
                                                                                <tr>
                                                                                    <td style="width: 197px; height: 3px" valign="top"></td>
                                                                                    <td style="width: 204px; height: 3px" valign="top"></td>
                                                                                    <td style="height: 3px; width: 169px;" valign="top"></td>
                                                                                    <td style="width: 209px; height: 3px" valign="top">
                                                                                        <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control input-sm" ReadOnly="True" Visible="False" Width="233"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 205px; height: 3px" valign="top"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 197px; height: 6px" valign="top">
                                                                                        <span style="font-size: 10pt">&nbsp; No. Penawaran </span></td>
                                                                                    <td style="width: 204px; height: 6px" valign="top">
                                                                                        <asp:TextBox ID="txtNoPO" runat="server" CssClass="form-control input-sm" Width="233"></asp:TextBox></td>
                                                                                    <td style="height: 6px; width: 169px;" valign="top">
                                                                                        <span style="font-size: 10pt">&nbsp; Tanggal</span></td>
                                                                                    <td style="width: 209px; height: 6px" valign="top">
                                                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control input-sm" Width="233"></asp:TextBox></td>
                                                                                    <td style="width: 169px; height: 19px" valign="top">
                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd-MMM-yyyy" runat="server"></cc1:CalendarExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                        </td>
                                                                        <td style="width: 197px; height: 6px">
                                                                            <span style="font-size: 10pt">&nbsp; No. SPP</span></td>
                                                                        <td style="width: 204px; height: 19px" valign="top">
                                                                            <asp:TextBox ID="txtSPP" runat="server" CssClass="form-control input-sm"
                                                                                Width="233" AutoPostBack="True" ReadOnly="False" OnTextChanged="txtSPP_TextChanged" onkeyup="this.value=this.value.toUpperCase()"></asp:TextBox></td>
                                                                        <td style="width: 197px; height: 6px" valign="top">
                                                                            <span style="font-size: 10pt">&nbsp; Item SPP </span></td>
                                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                                            <asp:DropDownList ID="ddlItemSPP" CssClass="form-control input-sm" runat="server" Width="233px"
                                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlItemSPP_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 197px; height: 6px">
                                                                            <span style="font-size: 10pt">&nbsp; Cari Supplier</span></td>
                                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                                            <asp:TextBox ID="txtCariSupplier" runat="server" AutoPostBack="True"
                                                                                CssClass="form-control input-sm" Width="233"></asp:TextBox>
                                                                        </td>
                                                        </td>
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Tipe SPP </span></td>
                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                            <asp:DropDownList ID="ddlTipeSPP" CssClass="form-control input-sm" runat="server" Width="233px"
                                                                AutoPostBack="False" Enabled="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp; Nama Supplier</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:DropDownList ID="ddlSupplier" CssClass="form-control input-sm" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" Width="233px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Kode Supplier</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtKodeSupplier" runat="server" CssClass="form-control input-sm"
                                                                Width="233" ReadOnly="True"></asp:TextBox></td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; UP Supplier</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtUp" runat="server" CssClass="form-control input-sm" ReadOnly="True"
                                                                Width="233"></asp:TextBox>
                                                        </td>

                                                        <td style="width: 197px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp; Fax Supplier</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtFax" runat="server" CssClass="form-control input-sm"
                                                                Width="233" ReadOnly="True"></asp:TextBox></td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 197px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp; Telp Supplier</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtTelepon" runat="server" CssClass="form-control input-sm"
                                                                ReadOnly="True" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 197px; height: 6px" valign="top">
                                                            <span style="font-size: 10pt">&nbsp; Q t y</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control input-sm" OnUnload="txtQty_TextChanged"
                                                                Width="233" AutoPostBack="True" ReadOnly="False">0</asp:TextBox></td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp; Nama Barang</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">
                                                            <asp:TextBox ID="txtNamaBarang" runat="server" CssClass="form-control input-sm"
                                                                ReadOnly="True" Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 197px; height: 6px" valign="top">&nbsp;</td>
                                                        <td style="width: 204px; height: 19px" valign="top">&nbsp;</td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 197px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp; Satuan</span></td>
                                                        <td rowspan="1" style="width: 204px; height: 19px">
                                                            <asp:TextBox ID="txtSatuan" runat="server" CssClass="form-control input-sm" ReadOnly="True"
                                                                Width="233"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 197px; height: 6px" valign="top">&nbsp;</td>
                                                        <td style="width: 204px; height: 19px" valign="top">&nbsp;</td>
                                                        <td style="width: 205px; height: 6px" valign="top"></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 150px; height: 6px">
                                                            <span style="font-size: 10pt">&nbsp;</span></td>
                                                        <td style="width: 204px; height: 19px" valign="top">&nbsp;&nbsp;
                                                                                                            
                                                        </td>
                                                        <td style="width: 209px; height: 19px">&nbsp;</td>
                                                        <td style="width: 209px; height: 6px" valign="top">
                                                        &nbsp;<td style="width: 205px; height: 19px" align="center">&nbsp;</td>
                                                    </tr>
                                            </table>
                                            <table id="Table1" style="left: 0px; top: 0px; width: 100%;" cellspacing="1"
                                                cellpadding="0" border="0" height="20">
                                                <tr>
                                                    <td style="width: 425px; height: 6px">
                                                        <span style="font-size: 10pt">&nbsp;</span></td>
                                                    <td style="width: 770px; height: 19px" valign="top">&nbsp;</td>
                                                    <td style="width: 749px; height: 19px" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbAddItem" runat="server" Font-Size="10pt"
                                                            OnClick="lbAddItem_Click">Add Item</asp:LinkButton>
                                                    </td>
                                                    <td style="width: 700px; height: 19px" valign="top">&nbsp;</td>
                                                </tr>

                                            </table>


                                            <hr width="100%" size="1">
                </div>
                <table id="Table2" style="left: 0px; top: 0px; width: 95%;" cellspacing="1"
                    cellpadding="0" border="0" height="165">

                    <tr>
                        <td style="height: 3px; width: 203px;" valign="top" colspan="1"></td>
                        <td style="height: 3px" valign="top" colspan="1">
                            <span style="font-size: 10pt">&nbsp; <strong>List</strong></span></td>
                    </tr>
                    <tr>
                        <td style="width: 203px; height: 100%" valign="top">&nbsp; &nbsp;
                        </td>
                        <td style="width: 100%; height: 100%" valign="top">
                            <div id="div2" style="width: 770px; height: 320px; overflow: auto">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    Width="100%" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>

                                        <asp:BoundField DataField="NoSPP" HeaderText="No. SPP" />
                                        <asp:BoundField DataField="ItemCode" HeaderText="Kode Barang" />
                                        <asp:BoundField DataField="NamaBarang" HeaderText="Nama Barang" />
                                        <asp:BoundField DataField="Qty" HeaderText="Q t y" />
                                        <asp:BoundField DataField="Satuan" HeaderText="Satuan" />
                                        <asp:BoundField DataField="Price" HeaderText="H a r g a" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                        <asp:ButtonField CommandName="AddDelete" Text="Hapus" />
                                    </Columns>
                                    <RowStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="WhiteSmoke" />
                                    <HeaderStyle Font-Names="tahoma" Font-Size="XX-Small" BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>

                </DIV></TD></TR>
              </TBODY>
            </TABLE>
            </TD></TR>
       </TBODY></TABLE></TR></TBODY></TABLE>
     </div>

              </div>

            <script src="../../assets/jquery.js" type="text/javascript"></script>
            <script src="../../assets/js/jquery-ui.min.js"></script>
            <script src="../../assets/select2.js"></script>
            <script src="../../assets/datatable.js"></script>
            <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

    <%--source html ditutup di sini--%>
                </ContentTemplate>
       </asp:UpdatePanel>

</asp:Content>