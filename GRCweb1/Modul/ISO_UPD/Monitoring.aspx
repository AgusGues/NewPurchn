<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Monitoring.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.Monitoring" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <style>
        label {
            font-weight: 400;
            font-size: 12px;
        }
        element.style {
            display: inline-block;
            width: 156px;
            font-family: Calibri;
            font-size: x-small;
            font-style: italic;
            background-color: #fff;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="div1" runat="server" class="table-responsive" style="width:100%">
                <table style="width: 100%; border-collapse: collapse">
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader">
                                <tr style="">
                                    <td style="width: 20%; padding-left: 15px">
                                        <b style="text-align: center">&nbsp;MONITORING ISO
                                            <br />
                                        </b> <asp:HiddenField ID="UserID" runat="server" />
                                        <asp:HiddenField ID="Bulan" runat="server" />
                                        <asp:HiddenField ID="Tahun" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <table style="width: 100%; border-collapse: collapse">
                    <tr bgcolor="#CCCCCC">
                        <td style="width: 100%;">
                            <table style="width: 100%;" bgcolor="#CCCCCC">
                                <tr style="width: 100%">
                                   
                                    <td style="width: 8%; font-size: x-small; font-weight: bold;">
                                        <asp:Label ID="LabelFilter" runat="server" Style="font-family: 'Calibri'; font-size: x-small;
                                            font-weight: 100" ForeColor="White">&nbsp;</asp:Label>
                                    </td>                                   
                                    <td style="width: 15%; font-size: x-small; text-align: left;">
                                        <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" Height="22px"
                                            OnSelectedIndexChanged="ddlFilter_Change" Style="font-family: Calibri; font-size: x-small"
                                            Width="140px">
                                        </asp:DropDownList>
                                    </td>
                                     <td style="width: 7%; font-size: x-small; text-align: left;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 10%; font-size: x-small; text-align: left;">
                                        <asp:RadioButton ID="RBPeriodeTahunan" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RBPeriodeTahunan_CheckedChanged" Style="font-family: Calibri;
                                            font-size: x-small;" Text="Periode Tahunan" TextAlign="Left" Width="156px"/>
                                    </td>
                                    <td style="width: 10%; font-size: x-small; text-align: left;">
                                        <asp:RadioButton ID="RBPeriodeBulanan" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RBPeriodeBulanan_CheckedChanged" Style="font-family: Calibri;
                                            font-size: x-small;" Text="Periode Bulanan" TextAlign="Left" Width="156px" />
                                    </td>
                                    <td style="width: 50%; font-size: x-small; text-align: left;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
                <asp:Panel ID="PanelPilihanBulan" runat="server" Visible="false" Style="font-family: Calibri;">
                    <table id="Table5" style="width: 100%;" bgcolor="#006600">
                        <tr style="width: 100%">
                            <td style="width: 12%; font-size: x-small; font-weight: bold;">
                                <asp:Label ID="LabelPBulan" runat="server" Style="font-family: 'Calibri'; font-size: x-small;
                                    font-weight: 100" ForeColor="White">&nbsp;</asp:Label>
                            </td>
                            <td style="width: 6%; font-size: x-small; text-align: left;">
                                <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="true" Height="22px" OnSelectedIndexChanged="ddlBulan_Change"
                                    Style="font-family: Calibri; font-size: x-small" Width="140px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 2%; font-size: x-small; text-align: left;">
                                <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Height="22px" OnSelectedIndexChanged="ddlTahun_Change"
                                    Style="font-family: Calibri; font-size: x-small" Width="80px">
                                </asp:DropDownList>
                            </td>                            
                            <td style="width: 10%; font-size: x-small; text-align: left; font-family: Calibri;">
                                <input id="BtnPreview" runat="server" onserverclick="BtnPreview_ServerClick" style="background-color: white;
                                    font-weight: bold; font-family: Calibri;" type="button" value="Preview" />
                            </td>
                            <td style="width: 70%; font-size: x-small; text-align: left;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelPilihanTahun" runat="server" Visible="false" Style="font-family: Calibri;">
                    <table id="Table6" style="width: 100%;" bgcolor="#006600">
                        <tr style="width: 100%">
                            <td style="width: 12%; font-size: x-small; font-weight: bold;">
                                <asp:Label ID="LabelPTahun" runat="server" Style="font-family: 'Calibri'; font-size: x-small;
                                    font-weight: 100" ForeColor="White">&nbsp;</asp:Label>
                            </td>
                            <td style="width: 6%; font-size: x-small; text-align: left;">
                                <asp:DropDownList ID="ddlPeriodeTahunAja" runat="server" AutoPostBack="true" Height="22px" OnSelectedIndexChanged="ddlPeriodeTahunAja_Change"
                                    Style="font-family: Calibri; font-size: x-small" Width="140px">
                                </asp:DropDownList>
                            </td>                                                      
                            <td style="width: 10%; font-size: x-small; text-align: left; font-family: Calibri;">
                                <input id="BtnPreviewTahun" runat="server" onserverclick="BtnPreviewTahun_ServerClick" style="background-color: white;
                                    font-weight: bold; font-family: Calibri;" type="button" value="Preview" />
                            </td>
                            <td style="width: 10%; font-size: x-small; text-align: left;">
                                &nbsp;
                            </td>
                            <td style="width: 62%; font-size: x-small; text-align: left;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelBulanan" runat="server" Visible="false" Style="font-family: Calibri;">
                <table style="width: 100%; border-collapse: collapse; overflow:scroll;">
                    <tr>
                        <td>
                        <asp:Panel ID="PanelHeaderPeriodeBulan" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table4" style="width: 100%;" bgcolor="#000066">
                                    <tr style="width: 100%">
                                        <td style="width: 30%; font-size: x-small;">
                                            <asp:Label ID="LabelH1" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                                font-weight: 700" ForeColor="White">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelShareAntarPlant" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table12" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelShareAntarPlant" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtShareAntarPlant" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBDetailShareAntarPlant" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBDetailShareAntarPlant_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHideDetailShareAntarPlant" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBHideDetailShareAntarPlant_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                </table>
                               <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />  
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelGridShareAntarPlant" runat="server" Visible="false" Style="font-family: Calibri;
                                font-size: small;">
                                <table id="Table13" style="width: 100%;">
                                    <asp:GridView ID="GridViewDataShareAntarPlant" runat="server" 
                                        AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="None" HorizontalAlign="Center">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" BorderColor="White" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TanggalShare" HeaderText="TANGGAL DISTRIBUSI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Permintaan2" HeaderText="PERMINTAAN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>      
                                            <asp:BoundField DataField="Alasan" HeaderText="ALASAN TDK IKUT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>  --%>                                    
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </table>
                            </asp:Panel>
                            
                       <%--   <asp:Panel ID="PanelShareJombang" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table14" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelShareJmb" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtShareJmb" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBDetailJmb" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBDetailJmb_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHideDetailJmb" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBHideDetailJmb_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                </table>
                               <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />  
                            </asp:Panel>
                           
                           <asp:Panel ID="PanelGridShareJmb" runat="server" Visible="false" Style="font-family: Calibri;
                                font-size: small;">
                                <table id="Table15" style="width: 100%;">
                                    <asp:GridView ID="GridViewShareJmb" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="None" ShowFooter="True">
                                        
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="3%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" Width="3%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="12%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="12%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" 
                                                    BorderColor="White" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="30%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tanggal" HeaderText="TANGGAL BUAT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>                                          
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="5%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="20%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="20%" />
                                            </asp:BoundField>                                           
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </table>
                            </asp:Panel>--%>
                            
                            <asp:Panel ID="PanelShare" runat="server" Visible="true" Style="font-family: Calibri;">
                            
                                <table id="Table1" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelShare" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtShare" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBDetailShare" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBDetailShare_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHideDetailShare" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBHideDetailShare_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                </table>
                               <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />  
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelGridShare" runat="server" Visible="false" Style="font-family: Calibri;font-size: small;">
                               <%--<div style="table-layout: fixed; overflow: scroll; visibility: collapse;">--%>
                              <%--  <table id="Table2" style="width: 100%;">--%>
                                <div style="overflow: scroll;">
                                    <asp:GridView ID="GridViewDataShare" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" OnRowDataBound="GridViewDataShare_RowDataBound"
                                        OnRowCommand="GridViewDataShare_RowCommand"
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="Horizontal">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" BorderColor="White" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TanggalShare" HeaderText="TANGGAL SHARE">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Permintaan2" HeaderText="PERMINTAAN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>      
                                            <asp:BoundField DataField="Alasan" HeaderText="ALASAN TDK IKUT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField> 
                                            <asp:BoundField DataField="Share_From" HeaderText="Asal UPD">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                            </asp:BoundField>     
                                            
                                             <asp:TemplateField HeaderText="Lampiran" ShowHeader="True">
                                             <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Lampiran") %>'
                                                        CommandName="Download" Text='<%# Eval("Lampiran") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    </div>
                              <%--  </table>--%>
                                <%--</div>--%></asp:Panel>
                            
                            <asp:Panel ID="PanelDokumenBaru" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table3" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelDokumenBaru" runat="server" Style="font-family: 'Calibri';
                                                font-size: x-small;" Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtDokumenBaru" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBDetailDokumenBaru" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBDetailDokumenBaru_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHideDetailDokumenBaru" runat="server" AutoPostBack="True"
                                                GroupName="a" OnCheckedChanged="RBHideDetailDokumenBaru_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                 <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelGridUPD" runat="server" Visible="false" Style="font-family: Calibri;
                                font-size: small;">
                                <table id="Table7" style="width: 100%;">
                                    <asp:GridView ID="GridViewUPD" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" OnRowDataBound="GridViewUPD_RowDataBound"
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="3%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" Width="3%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="12%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="12%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" BorderColor="White" Width="10%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="30%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tanggal" HeaderText="TANGGAL BUAT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="10%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%"/>
                                            </asp:BoundField>                                          
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="5%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" Width="5%"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" BorderWidth="1px" Width="20%"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="20%"/>
                                            </asp:BoundField>                                           
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </table>
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelDokumenPerubahan" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table8" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelPerubahan" runat="server" Style="font-family: 'Calibri';
                                                font-size: x-small;" Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtPerubahan" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBPerubahan" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBPerubahan_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHidePerubahan" runat="server" AutoPostBack="True"
                                                GroupName="a" OnCheckedChanged="RBHidePerubahan_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                 <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelGridUPDPerubahan" runat="server" Visible="false" Style="font-family: Calibri;
                                font-size: small;">
                                <table id="Table9" style="width: 100%;">
                                    <asp:GridView ID="GridViewPerubahan" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="3%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" Width="3%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="12%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="12%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" 
                                                    BorderColor="White" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="30%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tanggal" HeaderText="TANGGAL BUAT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>                                          
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="5%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="20%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="20%" />
                                            </asp:BoundField>                                           
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </table>
                            </asp:Panel>
                            
                            
                            <asp:Panel ID="PanelPemusnahan" runat="server" Visible="true" Style="font-family: Calibri;">
                                <table id="Table10" style="width: 100%;" bgcolor="#fff">
                                    <tr style="width: 100%">
                                        <td style="width: 30%;">
                                            <asp:Label ID="LabelPemusnahan" runat="server" Style="font-family: 'Calibri';
                                                font-size: x-small;" Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="txtPemusnahan" runat="server" Style="font-family: 'Calibri'; font-size: x-small;" 
                                            Visible="true" Font-Bold="True">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBPemusnahan" runat="server" AutoPostBack="True" GroupName="a"
                                                OnCheckedChanged="RBPemusnahan_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Lihat Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 10%; font-size: x-small; text-align: left;">
                                            <asp:RadioButton ID="RBHidePemusnahan" runat="server" AutoPostBack="True"
                                                GroupName="a" OnCheckedChanged="RBHidePemusnahan_CheckedChanged" Style="font-family: Calibri;
                                                font-size: x-small; font-style: italic; background-color: #fff;" Text=" .....Hide Detail >>"
                                                TextAlign="Left" Width="156px" />
                                        </td>
                                        <td style="width: 44%; font-size: x-small; text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                 <hr noshade="noshade" style="border: thin none #008080; background-color: #008080; background-image: none; background-repeat: no-repeat; color: #008080;" />
                            </asp:Panel>
                            
                            <asp:Panel ID="PanelGridPemusnahan" runat="server" Visible="false" Style="font-family: Calibri;
                                font-size: small;">
                                <table id="Table11" style="width: 100%;">
                                    <asp:GridView ID="GridViewPemusnahan" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" 
                                        Style="font-family: 'Calibri'; font-size: x-small; font-weight: 500;" Width="100%"
                                        GridLines="None">
                                        
                                        <Columns>
                                            <asp:BoundField DataField="No" HeaderText="NO.">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="3%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Font-Overline="False" Width="3%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaDept" HeaderText="DEPARTMENT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="12%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="12%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nomor" HeaderText="NOMOR DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderWidth="1px" 
                                                    BorderColor="White" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nama" HeaderText="NAMA DOKUMEN">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="30%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tanggal" HeaderText="TANGGAL BUAT">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Dotted" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Kategori" HeaderText="KATEGORI">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="10%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="10%" />
                                            </asp:BoundField>                                          
                                            <asp:BoundField DataField="RevisiNo" HeaderText="REV">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="5%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StatusApproval" HeaderText="STATUS">
                                                <HeaderStyle BackColor="#000066" BorderStyle="Solid" Font-Bold="True" Font-Names="Courier New"
                                                    Font-Size="X-Small" ForeColor="White" BorderColor="White" 
                                                    BorderWidth="1px" Width="20%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="X-Small" BorderColor="#000066" BorderStyle="Dotted"
                                                    BorderWidth="1px" Width="20%" />
                                            </asp:BoundField>                                           
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </table>
                            </asp:Panel>
                           
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
