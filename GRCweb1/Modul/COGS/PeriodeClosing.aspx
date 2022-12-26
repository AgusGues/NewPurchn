<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PeriodeClosing.aspx.cs" Inherits="GRCweb1.Modul.COGS.PeriodeClosing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <script language="Javascript" type="text/javascript" src="../../Script/calendar.js"></script> 
  <script src='<%=ResolveUrl("~/Script/jquery-1.2.6-vsdoc.js") %>' type="text/javascript"></script>
  <script language="javascript" type="text/javascript">
      $(document).ready(function () {

      })
      function pros() {

          postingProses(1)

      }
      function postingProses(id) {
         var imgurl ='<%=ResolveUrl("~/images/loader.GIF")%>';
         var imgok   ='<%=ResolveUrl("~/images/Check.png")%>';
         var Thn = $('#<%=ddlTahun.ClientID %>').val();
         var bln = $('#<%=ddlBulan.ClientID %>').val();
         var usr = $('#<%=txtUser.ClientID %>').val();
         var wpo = $('#<%=Purchn.ClientID %>').is(':checked');
         if (wpo == true) {
             $('#invpost').show();
             $('#readsts2').html("Proses posting inventory...please wait");
         } else {
             $('#invpost').hide();
             $('#readsts2').html("Proses Posting Production...please wait <img src='" + imgurl +"'");
         }
         $('#Td' + id).html('<img src=' + imgurl + ' />');
         $('#Td' + (id-1)).html('<img src=' + imgok + ' />');
          var url = '<%=ResolveUrl("~/Modul/COGS/PostingProses.ashx") %>'
          $.post(url, { 'ID': id, 'Thn': Thn, 'bln': bln, 'user': usr, 'wpo': wpo }, function (response) {
              var ulang = response.toString().split(":");

              if (wpo == true) {
                  $('#readsts2').html(ulang[1]);
                  if (ulang[0] > 0) {
                      postingProses(ulang[0]);
                  } else {
                      $('#readsts2').html(response).fadeOut(5000);
                      document.location.reload();
                  }
              } else {
                  if (ulang[0] > 0) {
                      $('#readsts2').html(response);
                      //$('#readsts2').html("Proses Posting BJ...please wait <img src='" + imgurl +"'");
                      postingProses(ulang[0]);
                  }
                  //$('#readsts2').html(response);
              }
          });
      }

  </script>


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
            .panelbox {
                background-color: #efeded;
                padding: 2px;
            }

            html, body, .form-control, button {
                font-size: 11px;
            }

            .input-group-addon {
                background: white;
            }

            .fz11 {
                font-size: 11px;
            }

            .fz10 {
                font-size: 10px;
            }

            .the-loader {
                position: fixed;
                top: 0px;
                left: 0px;
                ;
                width: 100%;
                height: 100%;
                background-color: rgba(0,0,0,0.1);
                font-size: 50px;
                text-align: center;
                z-index: 666;
                font-size: 13px;
                padding: 4px 4px;
                font-size: 20px;
            }

            .input-xs {
                font-size: 11px;
                height: 11px;
            }
        </style>

  
  
 


    </head>
    <body class="no-skin">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <span>CLOSING PERIODE </span>

                                <div class="pull-right">

                                    <asp:Button class="btn btn-info" ID="btnUpdate" runat="server" OnClientClick ="pros()" Text="Closing Periode" />
                                    <asp:Button class="btn btn-info" ID="btnCancel" runat="server" Text="Open Periode" OnClick="btnCancel_ServerClick" />
                                    <asp:Button class="btn btn-info" ID="btnPrint" runat="server" OnClientClick ="pros()" />
                                
                                </div>

                            </div>
                            <div style="padding: 2px"></div>
                            <div class="panel-body">
                                
                                <div class="col-xs-12 col-sm-12">
                                   
                                    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                                   <asp:TextBox ID="txtUser" runat="server" Visible="false"></asp:TextBox>
                                   
                                    <div class="col-xs-12 col-sm-6">
                                        <div class="row">
                                            <div class="col-md-2">Tahun</div>
                                            <div class="col-md-10">
                                                <asp:DropDownList ID="ddlTahun" class="form-control" Width="100%" runat="server" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="">--Pilih Tahun--</asp:ListItem>
                                                </asp:DropDownList>
                                                <span id="infoclose" runat="server" visible="false">Please wait....</span>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-6">
                                        <div class="row">
                                            <div class="col-md-2">Bulan</div>
                                            <div class="col-md-10">
                                                <asp:DropDownList class="form-control" Width="100%" ID="ddlBulan" runat="server" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="">--Pilih Bulan--</asp:ListItem>
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
                                                <span id="readsts" runat="server" style="font-size:xx-small; font-style:italic;"></span>
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 col-sm-12">
                                    <div class="col-xs-12 col-sm-12">
                                        <div class="row">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-11">
                                                <asp:RadioButton ID="Purchn" runat="server" Checked="True" GroupName="Sys" Text="Inventory" oncheckedchanged="Purchn_CheckedChanged" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="Prod" runat="server" GroupName="Sys" Text="Production" AutoPostBack="true" oncheckedchanged="Prod_CheckedChanged" />
                                                <div style="padding: 2px"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <%--tabel--%>

                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <table width="100%" class="table table-bordered table-condensed table-hover">
                                                <tr style="border: 1px solid">
                                                    <th style="width: 4%; border: 1px solid">
                                                        &nbsp;
                                                    </th>
                                                    <th style="width: 20%; border: 1px solid">
                                                        Periode
                                                    </th>
                                                    <th style="width: 25%; border: 1px solid" colspan="2">
                                                        Inventory
                                                    </th>
                                                    <th style="width: 25%; border: 1px solid" colspan="2">
                                                        Production
                                                    </th>
                                                </tr>
                                                <tbody>
                                                    <asp:Repeater ID="clsList" runat="server" OnItemDataBound="clsList_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr align="center" style="background-color: White; cursor: pointer;">
                                                                <td style="border: 1px solid">
                                                                    <%# Eval("ID") %>
                                                                </td>
                                                                <td align="left" style="border: 1px solid">
                                                                    <%# Eval("nBulan") %>
                                                                </td>
                                                                <td style="border: 1px solid; width: 8%" id="inv" runat="server">
                                                                    <%# Eval("Inventory") %>
                                                                </td>
                                                                <td align="left" style="border: 1px solid; width: 17%">
                                                                    <%# Eval("CreatedBy")%>
                                                                </td>
                                                                <td style="border: 1px solid; width: 8%" id="prd" runat="server">
                                                                    <%# Eval("Produksi")%>
                                                                </td>
                                                                <td align="left" style="border: 1px solid; width: 17%;">
                                                                    <%# Eval("LastModifiedBy")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr align="center" style="background-color: #DCDCDC; cursor: pointer;">
                                                                <td style="border: 1px solid">
                                                                    <%# Eval("ID") %>
                                                                </td>
                                                                <td align="left" style="border: 1px solid">
                                                                    <%# Eval("nBulan") %>
                                                                </td>
                                                                <td style="border: 1px solid; width: 8%" id="inv" runat="server">
                                                                    <%# Eval("Inventory") %>
                                                                </td>
                                                                <td align="left" style="border: 1px solid; width: 17%">
                                                                    <%# Eval("CreatedBy")%>
                                                                </td>
                                                                <td style="border: 1px solid; width: 8%" id="prd" runat="server">
                                                                    <%# Eval("Produksi")%>
                                                                </td>
                                                                <td align="left" style="border: 1px solid; width: 17%;">
                                                                    <%# Eval("LastModifiedBy")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td style="width: 40%;" valign="top">
                                         <span id="readsts2" style="font-size:small; font-style:italic;"></span>
                                        <div id="invpost" style="border:1px solid; display:none">
                                            <table id="postpros" style="width:100%;">
                                                <tr>
                                                    <td align="center" style="width:5%">&nbsp</td>
                                                    <td style="width:25%">Proses Posting</td>
                                                    <td style="width:10%">&nbsp;</td>
                                                    <td style="width:">&nbsp;</td>
                                                </tr>
                                                
                                                <tr id="Tr1" visible="true">
                                                    <td align="center">1.</td>
                                                    <td>Bahan Baku</td>
                                                    <td id="Td1"></td>
                                                </tr>
                                                <tr id="Tr2" visible="true">
                                                    <td align="center">2.</td>
                                                    <td>Bahan Pembantu</td>
                                                    <td id="Td2"></td>
                                                </tr>
                                                <tr id="Tr3" visible="true">
                                                    <td align="center">3.</td>
                                                    <td>ATK</td>
                                                    <td id="Td3"></td>
                                                </tr>
                                                <tr id="Tr4" visible="true">
                                                    <td align="center">4.</td>
                                                    <td>Asset</td>
                                                    <td id="Td4"></td>
                                                </tr>
                                                <tr id="Tr5" visible="true">
                                                    <td align="center">5.</td>
                                                    <td>Biaya</td>
                                                    <td id="Td5"></td>
                                                </tr>
                                                <tr id="Tr6" visible="true">
                                                    <td align="center">6.</td>
                                                    <td>Project</td>
                                                    <td id="Td6"></td>
                                                </tr>
                                                <tr id="Tr7" visible="true">
                                                    <td align="center">7.</td>
                                                    <td>Marketing</td>
                                                    <td id="Td7"></td>
                                                </tr>
                                                <tr id="Tr8" visible="true">
                                                    <td align="center">8.</td>
                                                    <td>Electrik</td>
                                                    <td id="Td8"></td>
                                                </tr>
                                                <tr id="Tr9" visible="true">
                                                    <td align="center">9.</td>
                                                    <td>Mekanik</td>
                                                    <td id="Td9"></td>
                                                </tr>
                                                <tr id="Tr10" visible="true">
                                                    <td align="center">10.</td>
                                                    <td>Repacking</td>
                                                    <td id="Td10"></td>
                                                </tr>
                                            </table>
                                         </div>   
                                        </td>
                                    </tr>
                                </table>

                                <%--tabel--%>

                            </div>

                        </div>
                    </div>
                </div>
                <script src="../../assets/jquery.js" type="text/javascript"></script>
                <script src="../../assets/js/jquery-ui.min.js"></script>
                <script src="../../assets/select2.js"></script>
                <script src="../../assets/datatable.js"></script>
                <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
                </body>
    </html>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
