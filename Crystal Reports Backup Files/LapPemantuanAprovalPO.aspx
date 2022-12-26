<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LapPemantuanAprovalPO.aspx.cs" Inherits="GRCweb1.Modul.ListReport.LapPemantuanAprovalPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta charset="utf-8" />
        <title>Destacking</title>

        <meta name="description" content="Common form elements and layouts" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

        <script src="../../assets/js/jquery-2.1.1.js" type="text/javascript"></script>
        <link rel="stylesheet" href="../../assets/css/jquery-ui.min.css" />
        <link rel="stylesheet" href="../../assets/jquery-confirm-v3.3.4/css/jquery-confirm.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/FixedHeader-3.1.8/css/fixedHeader.bootstrap.min.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.css" />
        <link rel="stylesheet" href="../../assets/Datatables/Responsive-2.2.7/css/responsive.bootstrap.min.css" />
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js" type="text/javascript"></script>
        
        <style type="text/css">
        .CheckBoxListCssClass
        {
            font-family:Courier New;
            color:OrangeRed;
            font-style:italic;
            font-weight:bold;
            font-size:large;
            }
    </style>

    </head>
    

    <body class="no-skin">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        PEMANTAUAN APPROVAL PO
                    </div>
                    <div class="panel-body" style="max-height: 5;">

                        <div style="padding: 2px"></div>
                        <div class="row">
                            <div class="col-md-12">
                                &nbsp;<div class="badge bg-primary text-wrap" style="width: 10rem;">
                                    Priode &nbsp;&nbsp; 
                                </div>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBulan" runat="server"></asp:DropDownList>&nbsp;
                               <asp:DropDownList ID="ddlTahuan" runat="server"></asp:DropDownList>
                            </div>

                        </div>
                        <div style="padding: 2px"></div>


                        <div class="row">
                            <div class="col-md-12">
                               &nbsp;<div class="badge bg-primary text-wrap" style="width: 10rem;">
                                   Material Group
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:CheckBoxList ID="ddlGroupID"  runat="server" RepeatColumns="2" Width="100%" CssClass="CheckBoxListCssClass"
                                    RepeatDirection="Horizontal"
                                    OnDataBound="ddlGroup_DataBound">
                                </asp:CheckBoxList>
                            </div>
                            <div style="padding: 2px"></div>

                            <div class="col-md-12">
                                        <asp:RadioButton id="rbtStock" Text="Stock" GroupName="Type" CssClass="CheckBoxListCssClass" runat="server"/> &nbsp;
                                        <asp:RadioButton id="rbtNonStock" Text="Non Stock" GroupName="Type" CssClass="CheckBoxListCssClass" runat="server"/>
                                        <asp:Button ID="btnPreview" runat="server" Text="Preview By NO PO" OnClick="btnPreview_Click" />
                                        <asp:Button ID="btnPreviewByTglPO" runat="server" Text="Preview By Tgl PO" OnClick="btnPreviewByTglPO_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                                        <asp:CheckBox ID="chkDetail" runat="server" Text="Export With Detail Item PO" Visible="false"/>
                             </div>

                            <div style="padding:2px"></div>
                            
                       <div class="contentlist" style="height:360px">
                        <div id="itemInfo" style="display:none ; background-color:ButtonFace; position:absolute;
                                width: 90%; border:3px solid; border-color:InactiveBorder;z-index:999">
                                    <table style="width:100%; border-collapse:collapse;" class="nbTableHeader">
                                        <tr valign="middle" style=" height:30px">
                                            <td style="width:90%; padding-left:5px">
                                                <img src="../../images/clipboard_16.png" />&nbsp; <b>Item List No. PO : <span id="jdls"></span></b>
                                            </td>
                                            <td style="width:10%;" class="angka">
                                                <img src="../../images/Delete.png" title="Close" style="cursor:pointer"  onclick="closeDiv();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="contentlist" style=" overflow: auto; max-height:250px">
                                                    <table class="tbStandart" id="tblData">
                                                        <thead>
                                                            <tr class="tbHeader">
                                                                <th style="width:5%" class="kotak">No.<th style="width:5%" class="kotak">No.</th>
                                                                <th style="width:10%" class="kotak">ItemCode</th>
                                                                <th style="width:20%" class="kotak">ItemName</th>
                                                                <th style="width:8%" class="kotak">Qty</th>
                                                                <th style="width:8%" class="kotak">Del Date</th>
                                                                <th style="width:20%" class="kotak">Suppler Name</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                            </div>
                        <div id="lstNewP" style="display:block;" runat="server">
                         <table style="width:100%; border-collapse:collapse; font-size:x-small" border="0" id="Table1">
                            <thead>
                                <tr class="tbHeader">
                                    <th rowspan="2" style="width:4%" class="kotak">No.</th>
                                    <th colspan="4" style="width:20%" class="kotak">SPP</th>
                                    <th rowspan="2" style="width:8%" class="kotak">No PO</th>
                                    <th colspan="2" style="width:8%" class="kotak">INPUT PO</th>
                                    <th colspan="2" style="width:10%" class="kotak">ON TIME PO</th>
                                    <th colspan="2" style="width:10%" class="kotak">APPROVAL PO(HEAD)</th>
                                    <th colspan="2" style="width:10%" class="kotak">ON TIME APV</th>
                                    <th colspan="2" style="width:10%" class="kotak">APPROVAL PO(MGR)</th>
                                </tr>
                                <tr class="tbHeader">
                                    <th style="width:7%" class="kotak">No.</th>
                                    <th style="width:5%" class="kotak">ItemCode</th>
                                    <th style="width:20%" class="kotak">ItemName</th>
                                    <th style="width:6%" class="kotak">Mgr. Apv</th>
                                    <th class="kotak" style="width:6%">Tanggal</th>
                                    <th class="kotak" style="width:4%">Jam</th>
                                    <th class="kotak" style="width:4%">OK</th>
                                    <th class="kotak" style="width:4%">TIDAK</th>
                                    <th class="kotak" style="width:6%">Tanggal</th>
                                    <th class="kotak" style="width:4%">Jam</th>
                                    <th class="kotak" style="width:4%">OK</th>
                                    <th class="kotak" style="width:4%">TIDAK</th>
                                    <th class="kotak" style="width:6%">Tanggal</th>
                                    <th class="kotak" style="width:4%">Jam</th>
                                    
                                </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="lstNew" runat="server" OnItemDataBound="lstNew_DataBound">
                                        <ItemTemplate>
                                            <tr class="EvenRows baris" title="Click on Kolom PO fro detail Items PO">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak tengah"><%# Eval("NOSPP") %></td>
                                                <td class="kotak tengah xx"><%# Eval("ItemCode")%></td>
                                                <td class="kotak "><%# Eval("ItemName")%></td>
                                                <td class="kotak tengah"><%# Eval("AlasanBatal") %></td>
                                                <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                <td class="kotak tengah"><%# Eval("POPurchnDate", "{0:d}")%></td>
                                                <td class="kotak tengah"><%# Eval("Jam")%></td>
                                                <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                                <td class="kotak tengah"><%# Eval("AlasanClose") %></td>
                                                <td class="kotak tengah"><%# Eval("ApproveDate1S")%></td>
                                                <td class="kotak tengah"><%# Eval("JamAppv1")%></td>
                                                <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("UP")%></td>
                                                <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("NoPol")%></td>
                                                <td class="kotak tengah"><%# Eval("ApproveDate2S")%></td>
                                                <td class="kotak tengah"><%# Eval("JamAppv2")%></td>                                               
                                            </tr>
                                            
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                             <tr class="OddRows baris" title="Click on Kolom PO fro detail Items PO">
                                                <td class="kotak tengah"><%# Container.ItemIndex+1 %></td>
                                                <td class="kotak tengah"><%# Eval("NOSPP") %></td>
                                                <td class="kotak tengah xx"><%# Eval("ItemCode")%></td>
                                                <td class="kotak "><%# Eval("ItemName")%></td>
                                                <td class="kotak tengah"><%# Eval("AlasanBatal") %></td>
                                                <td class="kotak tengah"><%# Eval("NoPO") %></td>
                                                <td class="kotak tengah"><%# Eval("POPurchnDate", "{0:d}")%></td>
                                                <td class="kotak tengah"><%# Eval("Jam")%></td>
                                                <td class="kotak tengah"><%# Eval("UOMCode") %></td>
                                                <td class="kotak tengah"><%# Eval("AlasanClose") %></td>
                                                <td class="kotak tengah"><%# Eval("ApproveDate1S")%></td>
                                                <td class="kotak tengah"><%# Eval("JamAppv1")%></td>
                                                <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("UP")%></td>
                                                <td class="kotak tengah"><%# (Eval("JamAppv1") == string.Empty) ? "" : Eval("NoPol")%></td>
                                                <td class="kotak tengah"><%# Eval("ApproveDate2S")%></td>
                                                <td class="kotak tengah"><%# Eval("JamAppv2")%></td>
                                            </tr>                                          
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <tr class="Line3">
                                                <td class="kotak angka" colspan="6">Total SPP</td>
                                                <td class="kotak angka" colspan="2"><asp:Label ID="ttSPP" runat="server"></asp:Label></td>
                                                <td class="kotak angka"><asp:Label ID="ttOkP" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttNoOKP" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttApOKP" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttAPNoOKP" runat="server"></asp:Label></td>
                                                <td class="kotak angka" colspan="2">&nbsp;</td>
                                            </tr>
                                            <tr class="">
                                                <td class="kotak angka" colspan="6"></td>
                                                <td class="kotak Line3 " colspan="2">Pencapaian (%)</td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttOK" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttNoOK" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttApOK" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka"><asp:Label ID="ttApNoOK" runat="server"></asp:Label></td>
                                                <td class="kotak Line3 angka" colspan="2">&nbsp;</td>
                                            </tr>
                                        </FooterTemplate>
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
        








        <script src="../../assets/jquery.js" type="text/javascript"></script>
        <script src="../../assets/js/jquery-ui.min.js"></script>
        <script src="../../assets/select2.js"></script>
        <script src="../../assets/datatable.js"></script>
        <script src="../../assets/jquery-confirm-v3.3.4/js/jquery-confirm.js"></script>
    </body>
    </html>

</asp:Content>
