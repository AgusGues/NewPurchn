<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrintPOforFax.aspx.cs" Inherits="GRCweb1.Modul.Purchasing.PrintPOforFax" %>
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
                window.showModalDialog("../../ModalDialog/TransferDetail.aspx", "", "resizable:yes;dialogHeight: 400px; dialogWidth: 700px;scrollbars=yes");
            }

            function fnOrderView(OrderNo) {

                var windowFeatures = 'width=850px,height=650px,left=50,top:10';
                window.open("OrderView.aspx?OrderNo=" + OrderNo, "_blank", windowFeatures);

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
                        PRINT P O
                    </div>
                    <div style="padding: 2px"></div>
                
                     
				
			<%--copy source design di sini--%>
            <div id="Div1" runat="server" class="table-responsive" style="width:100%">
                <table style="table-layout: fixed" height="100%" cellspacing="0" cellpadding="0"
                    width="100%">
                    <tbody>

                        <tr>
                            <td style="width: 100%;">
                                <table class="nbTableHeader">
                                    <tbody>
                                        <tr>
                                            <td style="width: 703px; height: 49px">
                                                <table class="nbTableHeader">
                                                    <tr>
                                                        <td style="width: 218%">
                                                            <%--<strong>PRINT  P O</strong></td>--%>
                                                        <td style="width: 37px"></td>
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="ddlSearch" CssClass="form-control input-sm" runat="server" Width="120px">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:TextBox ID="txtSearch" CssClass="form-control input-sm" runat="server" Width="150px"></asp:TextBox></td>
                                            <td style="width: 3px">
                                                <input id="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" type="button" value="Cari" /></td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                </TD>
  </tr>
  <tr height="100%">
      <td height="100%" style="width: 100%">
          <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" height="100%" width="100%">
              <tbody>
                  <tr class="treeRow1" valign="top">
                      <td>
                          <table class="nbTable1" cellspacing="0" cols="1" cellpadding="0" border="1" height="100%" width="100%">
                              <tbody>
                                  <tr style="width: 100%; height: 100%">
                                      <td style="width: 100%; height: 100%;">
                                          <div height: 100%; width: 100%;">
                                              <div>
                                                  <table id="TblIsi" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                      <tr>
                                                          <td style="height: 101px; width: 100%;">
                                                              <table class="tblForm" id="Table4" style="left: 0px; top: 0px; width: 103%;" cellspacing="1"
                                                                  cellpadding="0" border="0">
                                                                  <tr>
                                                                      <td style="width: 156px; height: 3px" valign="top"></td>
                                                                      <td style="width: 204px; height: 3px" valign="top"></td>
                                                                      <td style="height: 3px; width: 169px;" valign="top"></td>
                                                                      <td style="width: 209px; height: 3px" valign="top"></td>
                                                                      <td style="width: 205px; height: 3px" valign="top"></td>
                                                                  </tr>

                                                                  <tr>
                                                                      <td style="width: 156px; height: 6px" valign="top">
                                                                          <span style="font-size: 10pt">&nbsp; Nomor PO :</span></td>
                                                                      <td style="width: 204px; height: 6px" valign="top">
                                                                          <asp:TextBox ID="txtNOPO" CssClass="form-control input-sm" runat="server" Width="233"></asp:TextBox>
                                                                      </td>
                                                                      <td style="height: 6px; width: 169px;" valign="top">
                                                                          <input id="btnCetak" runat="server"
                                                                              CssClass="btn btn-primary btn-sm"
                                                                              type="button" value="Print" onserverclick="btnPrint_ServerClick" /></td>
                                                                      <td style="width: 209px; height: 6px" valign="top"></td>
                                                                      <td style="width: 205px; height: 6px" valign="top"></td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td style="width: 156px; height: 6px" valign="top">&nbsp;</td>
                                                                      <td style="width: 204px; height: 6px" valign="top"></td>
                                                                      <td style="width: 169px; height: 19px"></td>
                                                                      <td style="width: 209px; height: 19px"></td>
                                                                      <td style="width: 205px; height: 19px"></td>
                                                                  </tr>
                                                              </table>
                                              </div>
                                              <table id="Table2" style="left: 0px; top: 0px; width: 95%;" cellspacing="1"
                                                  cellpadding="0" border="0" height="165">
                                                  <tr>
                                                      <td style="height: 3px; width: 203px;" valign="top" colspan="1"></td>
                                                      <td style="height: 3px" valign="top" colspan="1"></td>
                                                  </tr>
                                                  <tr>
                                                      <td style="width: 203px; height: 100%" valign="top">&nbsp; &nbsp;
                                                      </td>
                                                      <td style="width: 100%; height: 100%" valign="top">
                                                          <div id="div2" style="width: 740px; height: 320px; overflow: auto">
                                                          </div>
                                                      </td>
                                                  </tr>
                                              </table>
                                          </div>
                                      </td>
                                  </tr>
                              </tbody>
                          </table>
                      </td>
                  </tr>
              </tbody>
          </table>
  </tr>
                </TBODY></TABLE>
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
