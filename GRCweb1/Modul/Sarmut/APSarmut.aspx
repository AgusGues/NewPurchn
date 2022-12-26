<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="APSarmut.aspx.cs" Inherits="GRCweb1.Modul.Sarmut.APSarmut" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="bdp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .page-content {
            width: 100%;
            background-color: #fff;
            margin: 0;
            padding: 1px 1px 1px;
            overflow-x: auto;
            min-height: .01%;
        }

        label, td, span {
            font-size: 12px;
        }

        table, tr, td {
            background-color: #fff;
        }

        .auto-style1 {
            width: 19%;
            height: 21px;
        }

        .auto-style2 {
            width: 1%;
            height: 21px;
        }

        .auto-style3 {
            width: 25%;
            height: 21px;
        }

        .auto-style4 {
            width: 1%;
        }

        .auto-style6 {
            height: 25px;
            width: 1%;
        }

        .auto-style7 {
            height: 19px;
        }
    </style>
    <script type="text/javascript">
        // fix for deprecated method in Chrome / js untuk bantu view modal dialog
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {
                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";
                // get the modal specs
                var mdattrs = arg3.split(";");
                for (i = 0; i < mdattrs.length; i++) {
                    var mdattr = mdattrs[i].split(":");
                    var n = mdattr[0];
                    var v = mdattr[1];
                    if (n) { n = n.trim().toLowerCase(); }
                    if (v) { v = v.trim().toLowerCase(); }
                    if (n == "dialogheight") {
                        h = v.replace("px", "");
                    } else if (n == "dialogwidth") {
                        w = v.replace("px", "");
                    } else if (n == "resizable") {
                        resizable = v;
                    } else if (n == "scroll") {
                        scroll = v;
                    } else if (n == "status") {
                        status = v;
                    }
                }
                var left = window.screenX + (window.outerWidth / 2) - (w / 2);
                var top = window.screenY + (window.outerHeight / 2) - (h / 2);
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }

    </script>
    <script src="../../Script/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../Script/calendar.js"></script>
    <script src="../../Scripts/jquery-1.10.2.js"></script>
    <script src="../../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../../Scripts/Sarmut/AnalisaData.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" runat="server" class="table-responsive" width="100%">
                <table style="height: 100%; width: 100%">
                    <tr>
                        <td>
                            <table class="nbTableHeader">
                                <tr style="width: 100%">
                                    <td style="width: 50%">
                                        <strong>&nbsp;ANALISA & PEMANTAUAN SASARAN MUTU </strong>&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 60%; padding-right: 10px" align="right">
                                        <%--<asp:Button ID="btnExport" runat="server" 
                                            Text="Export to Excel" />--%>
                                        <input id="btnNew" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Baru" onserverclick="BtnNew_ServerClick" />
                                        <input id="btnUpdate" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                            type="button" value="Simpan" onclientclick="return false;" onserverclick="BtnUpdate_ServerClick" />
                                        <input id="BtnPareto" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="Pareto" onserverclick="BtnPareto_ServerClick" />
                                        <input id="btnLampiran" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="Lampiran" onserverclick="BtnLampiran_ServerClick" />
                                        <input id="btnPrint" runat="server" onserverclick="BtnPrint_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="Cetak" />
                                        <input id="btnList" runat="server" onserverclick="BtnList_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="List" />
                                        <input id="btnRekap" runat="server" onserverclick="BtnRekap_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="Rekap"
                                            visible="false" />
                                        <%-- <input id="BtnPemantauan" runat="server" onserverclick="BtnRekap_ServerClick" style="background-color: white;
                                            font-weight: bold; font-size: 11px; width: 61px;" type="button" value="Pemantauan"
                                            visible="false" />--%>
                                        <input id="btnBackApv" runat="server" onserverclick="btnApproveForm_Click" style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                            type="button" value="Form Apv"
                                            visible="false" />
                                        <%--<asp:HiddenField ID="hfScrollPosition" Value="0" runat="server" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 100%; width: 100%; background-color: #B0C4DE">
                                <table class="tblForm" id="Table4" style="width: 100%; border-collapse: collapse">
                                    <tr style="border-bottom: 1px solid">
                                        <td style="width: 197px; border-bottom: 1px solid" valign="top">
                                            <asp:RadioButton ID="RBAnalisa" runat="server" AutoPostBack="True" Checked="True"
                                                OnCheckedChanged="RBx_CheckedChanged" Font-Size="X-Small" GroupName="a" Text="Analisa & Pemantauan" />
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="height: 3px; border-bottom: 1px solid" valign="top">
                                            <asp:RadioButton ID="RBSemester" runat="server" AutoPostBack="True" Font-Size="X-Small"
                                                GroupName="a" Text="Semester" OnCheckedChanged="RBx1_CheckedChanged" />
                                        </td>
                                        <td style="height: 3px; border-bottom: 1px solid" valign="top"></td>
                                        <td style="width: 209px; border-bottom: 1px solid" valign="top"></td>
                                        <td style="width: 205px; border-bottom: 1px solid" valign="top"></td>
                                    </tr>
                                </table>

                            </div>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="AnalisaPanel" runat="server">
                    <asp:Panel ID="Panel3" runat="server">
                        <table class="tblForm" id="Table2" style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td valign="top">&nbsp;</td>
                                <td valign="top"><span style="font-size: 10pt">PT.BANGUNPERKASA ADHITAMASENTRA </span>&nbsp; </td>
                                <td align="right">
                                    <input id="btnCetak" onserverclick="btnPrint0_ServerClick" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;"
                                        type="button" value="Cetak Langsung" visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">&nbsp;</td>
                                <td align="left" colspan="2" valign="top"><span style="font-size: 12pt">ANALISA DATA PENCAPAIAN SASARAN MUTU / PEMANTAUAN DEPARTEMEN </span>&nbsp; </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                        <table class="tblForm" id="Table1" style="width: 100%; border-collapse: collapse">
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt"></span></td>
                                <td style="width: 1%;" align="center"></td>
                                <td style="width: 25%;" colspan="2"></td>
                                <td style="width: 25%;" rowspan="12"><span style="font-size: 10pt">
                                    <cc2:Chart ID="Chart1" runat="server" BorderlineColor="Blue" Height="320px" Width="380px">
                                        <Titles>
                                            <cc2:Title Font="Arial,12pt,style=Bold" Name="title" ShadowOffset="3">
                                            </cc2:Title>
                                        </Titles>
                                        <ChartAreas>
                                            <cc2:ChartArea Name="Area1">
                                            </cc2:ChartArea>
                                        </ChartAreas>
                                    </cc2:Chart>
                                </span></td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">Analisa No.</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">
                                    <asp:TextBox ID="sarmutnotxt" runat="server" Enabled="false" Width="50%"></asp:TextBox>
                                    <asp:Label ID="IDa" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">Tanggal</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">
                                    <bdp:BDPLite ID="txtAsarmut_Date0" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                        Width="95%" Enabled="false">
                                    </bdp:BDPLite>
                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style2">&nbsp;</td>
                                <td style="width: 19%; height: 21px;" valign="top"><span style="font-size: 10pt">1.&nbsp;Departemen / Bagian</span> </td>
                                <td style="width: 1%; height: 21px;" align="center">: </td>
                                <td style="width: 5%; height: 21px;" colspan="2">
                                    <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged"
                                        Width="40%" Enabled="False">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 25%; height: 21px;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">2.&nbsp;Sasaran Mutu/Pemantauan Dept</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 35%;" colspan="2">
                                    <asp:DropDownList ID="ddlsmtP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsmtP_SelectedIndexChanged"
                                        Width="80%">
                                    </asp:DropDownList>
                                    &nbsp;<asp:Label ID="typeID" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">3.&nbsp;Line</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">
                                    <asp:DropDownList ID="ddlsarmutDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsmtDept_SelectedIndexChanged"
                                        Width="70%">
                                    </asp:DropDownList>
                                    <asp:Label ID="IDx" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="xxx" runat="server" Visible="true"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <%--<tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">4.&nbsp;Pemantauan Dept / Bagian</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">- </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>--%>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">4.&nbsp;Target</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">
                                    <asp:Label ID="txtTargetx" runat="server" OnTextChanged="Target_Change">&nbsp;&nbsp;</asp:Label>
                                    <asp:Label
                                        ID="Label1" runat="server" OnTextChanged="actual_Change"></asp:Label>
                                    (<asp:Label ID="txtParam" runat="server" OnTextChanged="Target_Change"></asp:Label>
                                    )<%--<asp:TextBox ID="txtTargetx" runat="server" OnTextChanged="Target_Change" CssClass="txtOnGrid"
                                                                        Width="50%"  AutoPostBack="true" ReadOnly="true" ></asp:TextBox>--%></td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">5.&nbsp;Monitoring Pencapaian</span> </td>
                                <td style="width: 1%;" align="center"></td>
                                <td style="width: 25%;" colspan="2"></td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>


                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;5.1&nbsp;Periode</span> </td>
                                <td style="width: 1%;" align="center">: </td>
                                <td style="width: 25%;" colspan="2">
                                    <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="True" Visible="false"
                                        OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                        <asp:ListItem Value="">--Pilih--</asp:ListItem>
                                        <asp:ListItem Value="Semester I">Semester I</asp:ListItem>
                                        <asp:ListItem Value="Semester II">Semester II</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBulan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTahun_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label runat="server" ID="bulanSmT0" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>



                            <tr>
                                <td valign="top" class="auto-style2">&nbsp;</td>
                                <td class="auto-style1" valign="top"><span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;5.2&nbsp;Hasil Pencapaian</span> </td>
                                <td align="center" class="auto-style2">: </td>

                                <td class="auto-style3">
                                    <asp:Label ID="txtActual" runat="server" OnTextChanged="actual_Change"></asp:Label>
                                    <asp:TextBox ID="txtSemeterAct" runat="server" Width="20%" Visible="false"></asp:TextBox>
                                    <asp:Label ID="txtSatuan" runat="server" OnTextChanged="actual_Change"></asp:Label>
                                </td>

                                <td class="auto-style4">

                                    <%--Penambahan agus 24-10-2022--%>
                                    <span style="font-size: 10pt">5.4&nbsp;Kesimpulan</span>
                                    <br />
                                    <%--Penambahan agus 24-10-2022--%>

                                    <span style="font-size: 10pt">
                                        <asp:CheckBox ID="chkTercapai" runat="server" ClientIDMode="Static" checked="false" Text="Tercapai" />
                                    </span>
                                </td>

                                <td class="auto-style3">&nbsp; </td>
                            </tr>

                            <tr>
                            </tr>



                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">&nbsp;&nbsp;&nbsp;5.3&nbsp;Grafik Pencapaian</span> </td>
                                <td style="width: 1%;" align="center"></td>

                                <td style="width: 25%;">
                                    <asp:Label ID="txttahun" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="txtbulan" runat="server" Visible="false"></asp:Label>
                                </td>



                                <td style="width: 25%;">

                                    <span style="font-size: 10pt">

                                        <asp:CheckBox ID="chkTTercapai" runat="server" ClientIDMode="Static" Checked="false" Text="Tidak Tercapai" />

                                    </span>

                                </td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>



                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td colspan="4" valign="top"><span style="font-size: 10pt">&nbsp;&nbsp;
                                    <table id="Table3" class="tblForm" style="width: 100%; border-collapse: collapse">
                                        <tr>
                                            <span style="font-size: 10pt">
                                                <td style="width: 70%;" valign="top"><%--<img alt src="../../images/exampleChart.JPG" />--%></td>
                                            </span>
                                            <td style="width: 20%;" valign="top"></td>
                                        </tr>
                                    </table>
                                </span></td>
                                <td colspan="2" valign="top">
                                    <div id="tbldata" runat="server" visible="false">
                                        <table style="width: 30%; border-collapse: collapse; font-size: x-small;" border="0">
                                            <thead>
                                                <tr>
                                                    <th class="kotak">ID </th>
                                                    <th class="kotak">Tahun </th>
                                                    <th class="kotak">Bulan </th>
                                                    <th class="kotak">Sarmut Dept </th>
                                                    <th class="kotak">Actual </th>
                                                    <th class="kotak">Target </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="lstdt" runat="server" OnItemDataBound="lstdt_DataBound">
                                                    <ItemTemplate>
                                                        <tr class="EvenRows baris kotak">
                                                            <td class="kotak tengah"><%# Eval("ID") %></td>
                                                            <td class="kotak tengah"><%# Eval("Tahun") %></td>
                                                            <td class="kotak tengah"><%# Eval("Bulan") %></td>
                                                            <td class="kotak tengah"><%# Eval("SarmutDepartemen")%></td>
                                                            <td class="kotak tengah"><%# Eval("Actual")%></td>
                                                            <td class="kotak tengah"><%# Eval("Target")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>



                            <tr>
                                <td valign="top" class="auto-style4">&nbsp;</td>
                                <td style="width: 19%;" valign="top"><span style="font-size: 10pt">6.&nbsp;Analisa Penyebab</span> </td>
                                <td style="width: 1%;" align="center"></td>
                                <td style="width: 25%;" colspan="2"></td>
                                <td style="width: 25%;"></td>
                                <td style="width: 25%;">&nbsp; </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel5" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 40%">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="auto-style6">&nbsp;</td>
                                                <td style="height: 25px" width="15%">&nbsp; </td>
                                                <td width="10%" style="height: 25px">
                                                    <asp:Panel ID="Panel13" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkManusia" runat="server" AutoPostBack="True" OnCheckedChanged="chkManusia_CheckedChanged"
                                                            Text="Manusia" ViewStateMode="Enabled" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%" style="height: 25px">&nbsp; </td>
                                                <td width="10%" style="height: 25px">
                                                    <asp:Panel ID="Panel15" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMesin" runat="server" AutoPostBack="True" OnCheckedChanged="chkMesin_CheckedChanged"
                                                            Text="Mesin" ViewStateMode="Enabled" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%" style="height: 25px">&nbsp; </td>
                                                <td width="10%" style="height: 25px">&nbsp; </td>

                                            </tr>
                                            <tr>
                                                <td rowspan="3" class="auto-style4">&nbsp;</td>
                                                <td rowspan="3" width="10%">
                                                    <asp:Panel ID="Panel14" runat="server" Height="25px" HorizontalAlign="Center">
                                                        <asp:CheckBox ID="chkLingkungan" runat="server" AutoPostBack="True" OnCheckedChanged="chkLingkungan_CheckedChanged" Text="Lingkungan" ViewStateMode="Enabled" />
                                                        &nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td align="center" width="10%">
                                                    <asp:Image ID="Image4" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                                <td align="center" width="10%">
                                                    <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/images/Panah miringB.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%">&nbsp; </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/images/Panah Lurus.jpg"
                                                        Width="100%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%" align="center">
                                                    <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%" align="center">
                                                    <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/Panah miring.jpg"
                                                        Width="41px" />
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style4">&nbsp;</td>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%">
                                                    <asp:Panel ID="Panel17" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMaterial" runat="server" AutoPostBack="True" OnCheckedChanged="chkMaterial_CheckedChanged"
                                                            Text="Material" ViewStateMode="Enabled" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                                <td width="10%">
                                                    <asp:Panel ID="Panel16" runat="server" Height="24px" HorizontalAlign="Center" Width="85px">
                                                        <asp:CheckBox ID="chkMetode" runat="server" AutoPostBack="True" OnCheckedChanged="chkMetode_CheckedChanged"
                                                            Text="Metode" ViewStateMode="Enabled" />
                                                    </asp:Panel>
                                                </td>
                                                <td width="10%">&nbsp; </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width: 60%">
                                    <asp:Panel ID="Panel4" runat="server" BorderStyle="Dotted">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 1%">&nbsp;</td>
                                                <td style="width: 10%">Manusia </td>
                                                <td style="width: 89%">
                                                    <asp:TextBox ID="txtManusia" runat="server" Enabled="False" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Mesin </td>
                                                <td>
                                                    <asp:TextBox ID="txtMesin" runat="server" Enabled="False" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Material </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaterial" runat="server" Enabled="False" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Metode </td>
                                                <td>
                                                    <asp:TextBox ID="txtMetode" runat="server" Enabled="False" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>Lingkungan </td>
                                                <td>
                                                    <asp:TextBox ID="txtLingkungan" runat="server" Enabled="False" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>

                    <table style="width: 100%;">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>



                        <tr>
                            <td style="width: 19%;" valign="top"><span style="font-size: 10pt">7.&nbsp;Tindakan Perbaikan & Pencegahan</span> </td>
                        </tr>
                        <br />

                        <tr>
                            <td style="width: 19%;" valign="top"><span style="font-size: 10pt">7.1&nbsp;Tindakan Perbaikan</span> </td>
                        </tr>

                        <tr>

                            <td>
                                <input id="btnPerbaikan" runat="server" onserverclick="btnPerbaikan_ServerClick"
                                    style="background-color: white; font-weight: bold; font-size: 11px; width: 185px;"
                                    type="button" value="Tindakan Perbaikan" />

                                <asp:Panel ID="PanelPebaikan" runat="server" Height="50px" Visible="true">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="center" class="auto-style7" style="font-size: x-small" width="40%">Tindakan </td>
                                            <td align="center" class="auto-style7" style="font-size: x-small">Pelaku </td>
                                            <td align="center" class="auto-style7" style="font-size: x-small">Jadwal Selesai </td>
                                            <td align="center" class="auto-style7" style="font-size: x-small">&nbsp; </td>
                                            <td align="center" class="auto-style7" style="font-size: x-small">&nbsp; </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="font-size: x-small" width="40%">
                                                <asp:TextBox ID="txtTIndakan" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td align="center" style="font-size: x-small">
                                                <asp:TextBox ID="txtPelaku" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="center" style="font-size: x-small">
                                                <asp:TextBox ID="txtDateJSp" runat="server" Width="80%"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalEx" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateJSp" />
                                            </td>
                                            <td align="center" style="font-size: x-small">&nbsp; </td>
                                            <td align="center" style="font-size: x-small">
                                                <input id="btnAddPerbaikan" runat="server" onserverclick="btnAddPerbaikan_ServerClick" type="button" value="Simpan" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel9" runat="server" Height="80px" ScrollBars="Vertical">
                                    <asp:GridView ID="GridPerbaikan" runat="server" AutoGenerateColumns="False" OnRowCommand="GridPerbaikan_RowCommand"
                                        OnRowDataBound="GridPerbaikan_RowDataBound" PageSize="4" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="Verifikasi">
                                                <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDPer">
                                                <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                            </asp:BoundField>

                                            <%--<asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                <ItemStyle Width="40%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />--%>

                                            <asp:TemplateField HeaderText="Tindakan">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTindakanPerbaikan" runat="server" Text=""><%# Eval("Tindakan")%></asp:Label>
                                                    <asp:TextBox ID="txtEditTindakanPerbaikan" runat="server" Width="100%" Visible="false"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pelaku">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblPelakuPerbaikan" runat="server" Text=""><%# Eval("Pelaku")%></asp:Label>
                                                    <asp:TextBox ID="txtEditPelakuPerbaikan" runat="server" Width="100%" Visible="false"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:ButtonField CommandName="rubahperbaikan" Text="Edit" />


                                            <asp:TemplateField HeaderText="Jadwal Selesai">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateJS" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateJS" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aktual Selesai">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateAS" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateAS" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="rubah" Text="Edit" />
                                            <asp:TemplateField HeaderText="Verifikasi">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVerifikasi1" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerifikasi1_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tgl. Verifikasi">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateVF" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateVF" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="targetx" Text="Target" Visible="false" />
                                            <asp:BoundField DataField="targetx" />
                                            <asp:ButtonField CommandName="hapus" Text="Delete" />
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 19%;" valign="top"><span style="font-size: 10pt">7.2&nbsp;Tindakan Perbaikan</span> </td>
                        </tr>

                        <tr>
                            <td style="height: 1px">
                                <input id="btnPencegahann" runat="server" onserverclick="btnPencegahan_ServerClick"
                                    style="background-color: white; font-weight: bold; font-size: 11px; width: 185px;"
                                    type="button" value="Tindakan Pencegahan" />
                                &nbsp;<asp:Panel ID="PanelPencegahan" runat="server" Height="50px" Visible="true">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="font-size: x-small" width="40%" align="center">Tindakan
                                            </td>
                                            <td style="font-size: x-small" align="center">Pelaku
                                            </td>
                                            <td style="font-size: x-small" align="center">Jadwal Selesai
                                            </td>
                                            <td style="font-size: x-small" align="center">&nbsp;
                                            </td>
                                            <td style="font-size: x-small" align="center">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="font-size: x-small" width="40%">
                                                <asp:TextBox ID="txtTIndakan0" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td align="center" style="font-size: x-small">
                                                <asp:TextBox ID="txtPelaku0" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="center" style="font-size: x-small">
                                                <asp:TextBox ID="txtDateJS0" runat="server" Width="80%"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDateJS0_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateJS0" />
                                            </td>
                                            <td align="center" style="font-size: x-small">&nbsp;
                                            </td>
                                            <td align="center" style="font-size: x-small">
                                                <input id="btnAddPencegahan" runat="server" onserverclick="btnAddPencegahan_ServerClick"
                                                    style="background-color: white; font-weight: bold; font-size: 11px; width: 61px;"
                                                    type="button" value="Simpan" />
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel10" runat="server" Height="80px" Width="100%" ScrollBars="Vertical">
                                    <asp:GridView ID="GridPencegahan" runat="server" AutoGenerateColumns="False" PageSize="4"
                                        Width="100%" OnRowCommand="GridPencegahan_RowCommand" OnRowDataBound="GridPencegahan_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Verifikasi">
                                                <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDPer">
                                                <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                            </asp:BoundField>

                                            <%--<asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                <ItemStyle Width="40%" />
                                            </asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />--%>

                                            <asp:TemplateField HeaderText="TIndakan">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTindakanPencegahan" runat="server" Text=""><%# Eval("TIndakan")%></asp:Label>
                                                    <asp:TextBox ID="txtEditTindakanPencegahan" runat="server" Width="100%" Visible="false"></asp:TextBox>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pelaku">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtEditPelakuPencegahan" runat="server" Width="100%" Visible="false"></asp:TextBox>
                                                    <asp:Label ID="lblPelakuPencegahan" runat="server" Text=""><%# Eval("Pelaku")%></asp:Label>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="rubahpencegahan" Text="Edit" />

                                            <asp:TemplateField HeaderText="Jadwal Selesai">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateJS" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateJS" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aktual Selesai">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateAS" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateAS" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="rubah" Text="Edit" />
                                            <asp:TemplateField HeaderText="Verifikasi">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVerifikasi0" runat="server" AutoPostBack="True" OnCheckedChanged="chkVerifikasi0_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tgl. Verifikasi">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDateVF" runat="server" Text="Label"></asp:Label>
                                                    <bdp:BDPLite ID="txtDateVF" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                        Visible="False">
                                                    </bdp:BDPLite>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="targetx" Text="Target" Visible="false" />
                                            <asp:BoundField DataField="target" />
                                            <asp:ButtonField CommandName="hapus" Text="Delete" />
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td valign="top" style="width: 20%;">&nbsp;</td>
                                        <td valign="top" style="width: 80%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; background-color: #C0C0C0;" valign="top">&nbsp;<strong>Upload Pareto</strong>&nbsp;</td>
                                        <td style="width: 80%; background-color: #C0C0C0;" valign="top">
                                            <asp:FileUpload ID="UploadPareto" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="PanelStatus" runat="server" Enabled="False">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td width="10%" style="font-size: x-small">
                                                <%--Status :--%>
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="5%">&nbsp;
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="10%">&nbsp;
                                            </td>
                                            <td width="15%">&nbsp;
                                                <%--<input id="btnClose" runat="server" onserverclick="btnClose_ServerClick" style="background-color: white;
                                                    font-weight: bold; font-size: 11px; width: 42px;" type="button" value="Close"
                                                    visible="False"  />--%>
                                            </td>
                                            <td align="left" colspan="2" style="text-decoration: underline"></td>
                                            <td align="left" colspan="2" style="text-decoration: underline">
                                                <input id="btnSolve" runat="server" align="right" onserverclick="btnSolve_ServerClick"
                                                    style="background-color: white; font-weight: bold; font-size: 11px; width: 42px;"
                                                    type="button" value="Solved" visible="False" />
                                            </td>
                                        </tr>
                                        <tr style="font-size: x-small">
                                            <td width="10%">
                                                <input id="btnClose" runat="server" onserverclick="btnClose_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 42px;"
                                                    type="button" value="Close"
                                                    visible="False" />
                                            </td>
                                            <td colspan="4" style="font-size: x-small">Closed
                                                <asp:CheckBox ID="chkClose" runat="server" Text="  " AutoPostBack="True" OnCheckedChanged="chkClose_CheckedChanged" />
                                                Tanggal :
                                                <bdp:BDPLite ID="txtDateTKasus" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Enabled="true">
                                                    <TextBoxStyle Font-Size="X-Small" />
                                                </bdp:BDPLite>
                                            </td>
                                            <td width="15%">&nbsp;
                                            </td>
                                            <td width="7%" style="width: 12%">Solved
                                                <asp:CheckBox ID="chksolved" runat="server" Text="  " AutoPostBack="True" OnCheckedChanged="chksolved_CheckedChanged" />
                                            </td>
                                            <td width="7%" colspan="2">Tanggal :
                                            </td>
                                            <td width="10%">
                                                <bdp:BDPLite ID="txtDateSolved" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Enabled="False">
                                                </bdp:BDPLite>
                                            </td>
                                        </tr>
                                        <tr style="font-size: x-small">
                                            <td width="10%">&nbsp;
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="5%">&nbsp;
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="10%">&nbsp;
                                            </td>
                                            <td width="15%">&nbsp;
                                            </td>
                                            <td colspan="3" style="width: 20%" width="10%">Tanggal waktu / due date Tanggal :
                                            </td>
                                            <td width="20%">
                                                <bdp:BDPLite ID="txtDueDate" runat="server" CssClass="style2" ToolTip="klik icon untuk merubah tanggal"
                                                    Enabled="False">
                                                </bdp:BDPLite>
                                            </td>
                                        </tr>
                                        <tr style="font-size: x-small">
                                            <td width="10%">&nbsp;
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="5%">&nbsp;
                                            </td>
                                            <td width="7%">&nbsp;
                                            </td>
                                            <td width="10%">&nbsp;
                                            </td>
                                            <td width="15%">&nbsp;
                                            </td>
                                            <td colspan="3" style="width: 20%" width="10%"></td>
                                            <td width="10%"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelList" runat="server" Visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right">Filter By
                            </td>
                            <td align="left" title="Departemen ">&nbsp;
                            <asp:Label ID="LDept" runat="server" Text="Departemen "></asp:Label>&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlDeptName1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName0_SelectedIndexChanged"
                                Width="125px">
                            </asp:DropDownList>
                                &nbsp;Status&nbsp;&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDeptName0_SelectedIndexChanged" Width="125px">
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem Value="0">Open</asp:ListItem>
                                    <asp:ListItem Value="1">Close</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                            <asp:Label ID="BulanPilih" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlBulan0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName0_SelectedIndexChanged"
                                    Width="125px">
                                    <asp:ListItem Value="0">ALL</asp:ListItem>
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
                                &nbsp;
                            <asp:Label ID="txtSemester" runat="server" Visible="true"></asp:Label>
                                <asp:DropDownList ID="ddlsmtList" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDeptName0_SelectedIndexChanged" Visible="true">
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem Value="Semester I">Semester I</asp:ListItem>
                                    <asp:ListItem Value="Semester II">Semester II</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; Tahun &nbsp;
                            <asp:DropDownList ID="ddlTahun0" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptName0_SelectedIndexChanged"
                                Width="125px">
                            </asp:DropDownList>

                                &nbsp;&nbsp;<asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"
                                    Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridSMT" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridSMT_PageIndexChanging" OnRowCommand="GridSMT_RowCommand"
                                    OnRowDataBound="GridSMT_RowDataBound" PageSize="15" Width="100%">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="LStatus" runat="server" Text="Label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="KetStatus" HeaderText="KetStatus" />--%>
                                        <%--<asp:BoundField DataField="Due_Date" DataFormatString="{0:d}" HeaderText="Due Date" />--%>
                                        <asp:BoundField DataField="ID">
                                            <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Dept" HeaderText="Departemen" />
                                        <asp:BoundField DataField="AnNo" HeaderText="No Analisa" />
                                        <asp:BoundField DataField="TglAnalisa" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tanggal" />
                                        <asp:BoundField DataField="NamaBulan" HeaderText="Periode" />
                                        <asp:BoundField DataField="Semester" HeaderText="Periode Semester" />
                                        <asp:BoundField DataField="SarmutPerusahaan" HeaderText="Sarmut Perusahaan" />
                                        <asp:BoundField DataField="SarmutDepartemen" HeaderText="Sarmut Departemen" />
                                        <asp:BoundField DataField="TargetVID" HeaderText="Target" />
                                        <asp:BoundField DataField="Actual" HeaderText="Pencapaian" />
                                        <asp:BoundField DataField="Jenis" HeaderText="Jenis" />
                                        <asp:BoundField DataField="Ket" HeaderText="Kesimpulan" />
                                        <asp:BoundField DataField="Approval" HeaderText="Approval" />
                                        <asp:BoundField DataField="CLosed" HeaderText="Status" />
                                        <asp:BoundField DataField="Solved" HeaderText="Ket" />
                                        <asp:BoundField DataField="Verifikasi" HeaderText="Verf" />
                                        <asp:BoundField DataField="Due_Date" HeaderText="Due_Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print"
                                                    Text="Print" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("ID") %>' Visible="False" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApvMgr" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Tgl. Apv. Mgr." />
                                    </Columns>
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                        Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>

                    </table>
                    <br />
                    <div id="lst" runat="server" visible="false">
                        <table style="width: 100%; border-collapse: collapse; font-size: x-small;" border="0">
                            <thead style="font-weight: bold">
                                <tr class="tbHeader">
                                    <td width="3%" align="center">No
                                    </td>
                                    <td width="8%" align="center">Departemen
                                    </td>
                                    <td align="center" width="7%">No Analisa
                                    </td>
                                    <td align="center" width="10%">Tanggal
                                    </td>
                                    <td align="center" width="29%">Periode
                                    </td>
                                    <td align="center" width="4%">Sarmut Perusahaan
                                    </td>
                                    <td align="center" width="4%">Sarmut Departemen
                                    </td>
                                    <td align="center" width="20%">Target
                                    </td>
                                    <td align="center" width="4%">Pencapaian
                                    </td>
                                    <td align="center" width="4%">Jenis
                                    </td>
                                    <td align="center" width="4%">Kesimpulan
                                    </td>
                                    <td align="center" width="4%">Approval
                                    </td>
                                    <td align="center" width="4%">Status
                                    </td>
                                    <td align="center" width="4%">Ket
                                    </td>
                                    <td align="center" width="4%">Verf
                                    </td>
                                    <td align="center" width="4%">Due Date
                                    </td>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="lstAnalisaData" runat="server">
                                    <ItemTemplate>
                                        <tr class="EvenRows baris kotak">
                                            <td style="border: 1px solid grey" align="center">
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Dept")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("AnNo")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("TglAnalisa", "{0:d}")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("NamaBulan")%>
                                            </td>
                                            <td class="kotak angka">
                                                <%# Eval("SarmutPerusahaan")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("SarmutDepartemen")%>
                                            </td>
                                            <td class="kotak">
                                                <%# Eval("TargetVID")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Actual")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Jenis")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Ket")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Approval")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("CLosed")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Solved")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Verifikasi")%>
                                            </td>
                                            <td class="kotak tengah">
                                                <%# Eval("Due_Date", "{0:d}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr class="Line3 tengah kotak">
                                    <td class="kotak">&nbsp;
                                    </td>
                                    <td class="kotak"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka"></td>
                                    <td class="kotak angka">&nbsp;
                                    </td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                    <td class="kotak tengah"></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelRekap" runat="server" Visible="false">
                    <div id="Div2" runat="server">
                        <table style="table-layout: fixed" width="100%">
                            <tbody>
                                <%-- <tr>
                            <td style="width: 100%; height: 49px">
                                <table class="nbTableHeader" style="width: 100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <strong>&nbsp;REKAP ANALISA DAN PEMANTAUAN</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                                <tr>
                                    <td height="100%" valign="top" class="content">
                                        <table width="100%" style="border-collapse: collapse; margin-top: 10px">
                                            <tr>
                                                <td style="width: 10%; padding-left: 10px;">Periode
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:DropDownList ID="ddlPeriodeBulan" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlPeriodeTahun" runat="server" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px">Department
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDeptRekap" runat="server" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 10px">&nbsp;
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="RBAll" runat="server" Checked="True" GroupName="a" Text="All" />&nbsp;<asp:RadioButton
                                                        ID="RBOpen" runat="server" GroupName="a" Text="Open" />
                                                    &nbsp;<asp:RadioButton ID="RBClosed" runat="server" GroupName="a" Text="Closed" />
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                    <asp:Button ID="btnToExcel" runat="server" Text="Export To Execl" OnClick="btnExport_Click" />
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                                <td align="right"></td>
                                            </tr>
                                        </table>
                                        <div class="contentlist" style="height: 500px" id="div3" runat="server">
                                            <table style="width: 100%; border-collapse: collapse; font-size: x-small" border="0">
                                                <tr class="tbHeader">
                                                    <th>#
                                                    </th>
                                                    <th>Departemen
                                                    </th>
                                                    <th>No Analisa
                                                    </th>
                                                    <th>Tanggal
                                                    </th>
                                                    <th>Periode
                                                    </th>
                                                    <th>Periode Semester
                                                    </th>
                                                    <th>Sarmut Perusahaan
                                                    </th>
                                                    <th>Sarmut Departemen
                                                    </th>
                                                    <th>Target
                                                    </th>
                                                    <th>Pencapaian
                                                    </th>
                                                    <th>Jenis
                                                    </th>
                                                    <th>Kesimpulan
                                                    </th>
                                                    <th>Approval
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                    <th>Ket
                                                    </th>
                                                    <th>Verf
                                                    </th>
                                                    <th>Due Date
                                                    </th>
                                                </tr>
                                                <tbody>
                                                    <asp:Repeater ID="ListAnalisaData" runat="server" OnItemDataBound="ListAnalisaData_DataBound"
                                                        OnItemCommand="ListAnalisaData_Command">
                                                        <ItemTemplate>
                                                            <tr class="EvenRows baris">
                                                                <td class="kotak tengah" width="1%">
                                                                    <%# Container.ItemIndex+1 %>
                                                                <td class="kotak tengah" width="5%">
                                                                    <%# Eval("Dept") %>
                                                                </td>
                                                                <td class="kotak tengah" width="5%">
                                                                    <%# Eval("AnNo") %>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("TglAnalisa", "{0:d}")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("NamaBulan")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Semester")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("SarMutPerusahaan")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("SarmutDepartemen")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("TargetVID")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Actual")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Jenis")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Ket")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Approval")%>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="Status" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="Sop" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <asp:Label ID="Verf" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="kotak tengah">
                                                                    <%# Eval("Due_Date", "{0:d}")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelLampiran" runat="server" Visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td>No Analisa :
                            <asp:Label ID="sarmutNo" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td align="right">
                                <input id="btnHapus" runat="server" style="background-color: white; font-weight: bold; font-size: 11px; width: 131px;" type="button" value="Hapus" onserverclick="btnHapus_Click" />
                                <input id="btnUpload0" runat="server" onserverclick="btnUpload0_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 131px;"
                                    type="button" value="Refresh Data" />
                                <input id="btnUpload" runat="server" onserverclick="btnUpload_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px; width: 131px;"
                                    type="button" value="Tambah Lampiran" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridLampiran" runat="server" Height="100%" Width="100%" AutoGenerateColumns="False"
                                    OnRowCommand="GridLampiran_RowCommand" OnRowDataBound="GridLampiran_RowDataBound"
                                    Visible="false">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="FIleName" HeaderText="Nama File" />
                                        <asp:BoundField DataField="TanggalUpload" HeaderText="Tanggal Upload" />
                                        <asp:ButtonField CommandName="lihat" Text="Preview" />
                                        <asp:ButtonField CommandName="hapus" Text="Hapus" />
                                    </Columns>
                                </asp:GridView>
                                <table id="zib" style="border-collapse: collapse; font-size: x-small; width: 100%" border="0">
                                    <thead>
                                        <tr class="tbHeader">

                                            <th class="kotak tengah" width="4%">No.
                                            </th>
                                            <th class="kotak ">FileName
                                            </th>
                                            <th class="kotak ">Tanggal Upload
                                            </th>
                                            <th class="kotak ">#
                                            </th>
                                            <%--<th class="kotak ">
                                            #
                                        </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="tb" runat="server">
                                        <asp:Repeater ID="attachPrs" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                            <ItemTemplate>
                                                <tr id="lst" runat="server" class="EvenRows baris">

                                                    <td class="kotak tengah">
                                                        <span class="angka" style="width: 10%">
                                                            <asp:CheckBox ID="chkprs" AutoPostBack="false" ToolTip='<%# Eval("ID") %>' runat="server" OnCheckedChanged="chk_CheckedChangePrs" /></span>
                                                        <%# Eval("ID") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <%# Eval("FileName") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <%# Eval("TanggalUpload") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                            CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                            ImageUrl="~/images/Logo_Download.png" />
                                                    </td>
                                                    <%--<td class="kotak angka">
                                                </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelPareto" runat="server" Visible="false">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%">No Analisa :
                            <asp:Label ID="sarmutNoP" runat="server" Text="Label"></asp:Label>
                                &nbsp;
                                <asp:Label ID="sarmutID" runat="server" Text="Label" Visible="False"></asp:Label>
                            </td>
                            <td align="right" style="width: 40%">
                                <asp:Panel ID="Panel18" runat="server" Visible="False">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%"><strong>Upload Pareto </strong></td>
                                            <td style="width: 60%">&nbsp;<asp:FileUpload ID="UploadPareto0" runat="server" />
                                                &nbsp;</td>
                                            <td style="width: 20%">
                                                <input id="Button4" runat="server" onserverclick="btnUploadPareto0_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                                    type="button" value="Upload" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td align="right" style="width: 10%">&nbsp;
                                <input id="Button3" runat="server" onserverclick="btnTambahPareto_ServerClick" style="background-color: white; font-weight: bold; font-size: 11px;"
                                    type="button" value="Tambah Pareto" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table id="zib1" style="border-collapse: collapse; font-size: x-small; width: 100%" border="0">
                                    <thead>
                                        <tr class="tbHeader">

                                            <th class="kotak tengah" width="4%">No.
                                            </th>
                                            <th class="kotak ">FileName
                                            </th>
                                            <th class="kotak ">Tanggal Upload
                                            </th>
                                            <th class="kotak ">#
                                            </th>
                                            <%--<th class="kotak ">
                                            #
                                        </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="Tbody1" runat="server">
                                        <asp:Repeater ID="attachPareto" runat="server" OnItemCommand="attachPrs_Command" OnItemDataBound="attachPrs_DataBound">
                                            <ItemTemplate>
                                                <tr id="lst" runat="server" class="EvenRows baris">

                                                    <td class="kotak tengah">
                                                        <span class="angka" style="width: 10%">
                                                            <asp:CheckBox ID="chkprs" AutoPostBack="false" ToolTip='<%# Eval("ID") %>' runat="server" OnCheckedChanged="chk_CheckedChangePrs" /></span>
                                                        <%# Eval("ID") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <%# Eval("FileName") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <%# Eval("TanggalUpload") %>
                                                    </td>
                                                    <td class="kotak angka">
                                                        <asp:ImageButton ToolTip="Click to Preview Document" ID="lihatprs" runat="server"
                                                            CommandArgument='<%# Eval("FileName") %>' CssClass='<%# Eval("ID") %>' CommandName="preprs"
                                                            ImageUrl="~/images/Logo_Download.png" />
                                                    </td>
                                                    <%--<td class="kotak angka">
                                                </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnToExcel" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
            <asp:PostBackTrigger ControlID="Button4" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
