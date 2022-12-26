<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SPKPReport2.aspx.cs" Inherits="GRCweb1.Modul.SPKP.SPKPReport2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <!DOCTYPE html>
    <html lang="en">
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />

    <title>Report SPKP</title>
    <meta name="description" content="Common form elements and layouts" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
    <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
    <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../assets/css/chosen.min.css" />

    </head>

    <body class="no-skin" runat="server">
        <div class="row" runat="server">
            <div class="col-xs-12 col-xs-12 form-group-sm">
                <div class="panel panel-primary">   
                    <div class="panel-heading">
                        Report SPKP
                        <div class="pull-right">
                            <div class="col-md-12">
                                <asp:Button ID="btnexport" CssClass="btn-success btn-xs" runat="server" Text="Export Excel" OnClick="btnexport_Click" />
                                &nbsp
                                <asp:Button ID="btninput" CssClass="btn-success btn-xs" runat="server" Text="Input SPKP" OnClick="btninput_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
						    <div class="col-md-2" style=" text-align:right;">No SPKP</div>
                            <div class="col-md-2">
                                <asp:TextBox ID="nospkp" CssClass="input-sm form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div style="padding: 2px"></div>
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-2">
                                <asp:Button ID="Button1" CssClass="btn-success btn-xs" runat="server" Text="Preview" OnClick="btnpreview_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body" id="report">
                    <div  class="row">
                        <div runat="server" class="col-md-12" id="div3" >
                            
                              <table id="table" class="table table-striped table-bordered table-hover" border="1">
                                <thead>
                                    <tr>
                                        <th class="text-center">Tanggal</th>
                                        <th class="text-center">Line 1</th>
                                        <th class="text-center">Line 2</th>
                                        <th class="text-center">Line 3</th>
                                        <th class="text-center">Line 4</th>
                                        <th class="text-center">Line 5</th>
                                        <th class="text-center">Line 6</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstspkp" runat="server" OnItemDataBound="lstspkp_ItemDataBound1" >
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%--<%# Eval("tanggal", "{0: dd-MMM-yyyy}") %>--%>
                                                     <asp:GridView ID="GridTanggal" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic2" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" >
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic3" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic4" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic5" runat="server"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynamic6" runat="server" 
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    subtotal
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%"  ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub2" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub3" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub4" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub5" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="GrdDynSub6" runat="server" CssClass="table table-bordered"
                                                    AutoGenerateColumns="False" PageSize="15" Width="100%" ShowHeader="False">
                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                                    <PagerStyle BorderStyle="Solid" />
                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                    <RowStyle HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                               <tfoot>
                                   <tr>
                                       <td>
                                           Grand Total
                                       </td>

                                       <td>
                                            <asp:GridView ID="GrdDyntotal" runat="server" 
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                       <td>
                                           <asp:GridView ID="GrdDyntotal2" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                       <td>
                                           <asp:GridView ID="GrdDyntotal3" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                       <td>
                                           <asp:GridView ID="GrdDyntotal4" runat="server" 
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                       <td>
                                           <asp:GridView ID="GrdDyntotal5" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                       <td>
                                           <asp:GridView ID="GrdDyntotal6" runat="server" 
                                            AutoGenerateColumns="False" PageSize="15" Width="100%" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" ShowHeader="False">
                                            <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma"  />
                                            <PagerStyle BorderStyle="Solid" />
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <RowStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            </asp:GridView>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td colspan="5">
                                           Keterangan :<br /><br />
                                           *Note : Toleransi penyimpangan produk diatas ketebalan 4mm ± 10%<br />
                                       </td>
                                       <td class="text-center">BOARDMILL</td>
                                       <td class="text-center">PPIC</td>
                                   </tr>
                               </tfoot>
                            </table>    
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
</html>
</asp:Content>
