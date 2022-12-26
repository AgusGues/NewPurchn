<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApvTPPHSE.aspx.cs" Inherits="GRCweb1.Modul.TPP.ApvTPPHSE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="bdp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            <div id="div1" runat="server">
                <table>
                    <tr>
                        <td>
                            <table class="nbTableHeader">
                                <tr style="">
                                    <td style="width: 20%; padding-left: 15px">
                                        <b style="text-align: center">&nbsp;APPROVAL TPP HSE
                                            <br />
                                        </b>
                                    </td>
                                    <td style="width: 70%; padding-right: 10px; text-align: right">
                                        <asp:Button ID="btnPrev" runat="server" Text="Sebelumnya" Enabled="false" 
                                            onclick="btnPrev_Click"  />
                                        <asp:Button ID="btnApprove" runat="server" Text="Approved" 
                                            onclick="btnApprove_Click"  />
                                        <asp:Button ID="btnNext" runat="server" Text="Selanjutnya" 
                                            onclick="btnNext_Click"  />
                                        <asp:Button ID="btnNotApprove" runat="server" Text="Not Approved"  />
                                        <asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor TPP" onfocus="if(this.value==this.defaultValue)this.value='';"
                                            onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" 
                                            placeholder="Find by Nomor TPP" Enabled="False"></asp:TextBox>
                                        <asp:Button ID="btnCari" runat="server" Text="Cari"  
                                            Enabled="False" />
                                        <asp:HiddenField ID="noTPP" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 99%;  padding: 5px">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 197px; height: 6px" valign="top">
                                            <span style="font-size: 10pt">&nbsp; No. TPP </span>
                                        </td>
                                        <td style="width: 204px; height: 6px" valign="top">
                                            <asp:TextBox ID="txtNoPO" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                Style="font-family: Arial, Helvetica, sans-serif; font-size: small"></asp:TextBox>
                                        </td>
                                        <td style="height: 6px; width: 169px;" valign="top">
                                            <span style="font-size: 10pt">&nbsp; Tgl. TPP</span>
                                        </td>
                                        <td style="width: 209px; height: 6px" valign="top">
                                            <asp:TextBox ID="txtDate" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                Style="font-family: Arial, Helvetica, sans-serif; font-size: small"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtID" runat="server" BorderStyle="Groove" Width="25" ReadOnly="True"
                                                Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 197px; height: 6px" valign="top">
                                            &nbsp;
                                        </td>
                                        <td style="width: 204px; height: 19px" valign="top">
                                            <asp:TextBox ID="txtDeptF" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"
                                                Style="font-family: Arial, Helvetica, sans-serif; font-size: small" Visible="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 197px; height: 6px" valign="top">
                                            <span style="font-size: 10pt">&nbsp; Status Aproval </span>
                                        </td>
                                        <td rowspan="1" style="width: 204px; height: 19px">
                                            <asp:TextBox ID="txtApv" runat="server" BorderStyle="Groove" Width="100" ReadOnly="True"
                                                Style="font-family: Arial, Helvetica, sans-serif; font-size: small">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 245px;">
                                           <asp:Label ID="resultMailSucc" runat="server" BackColor="White" 
                                              class="result_done" Font-Size="X-Small" ForeColor="Lime"  
                                              Visible="False"></asp:Label>
                                           <asp:Label ID="resultMailFail" runat="server" class="result_fail" 
                                              ForeColor="Red" Height="20px" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 205px; height: 6px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 197px; height: 6px" valign="top">
                                            <span style="font-size: 10pt">&nbsp; Uraian Ketidaksesuaian</span>
                                        </td>
                                        <td style="height: 19px" colspan="3">
                                            <asp:TextBox ID="txtTidakS" runat="server" BorderStyle="Groove" TextMode="MultiLine"
                                                Height="100px" Font-Size="X-Small" Width="100%" Style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: small"></asp:TextBox>
                                        </td>
                                        <td style="height: 19px" valign="bottom">
                                            <asp:CheckBox ID="chkList" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="chkList_CheckedChanged" Text=" " />List TPP
                                            <asp:CheckBox ID="chkDetail" runat="server" AutoPostBack="True" 
                                                oncheckedchanged="chkDetail_CheckedChanged" Text=" " />Detail
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Rekomendasi HSE
                                        </td>
                                        <td>
                                              <asp:CheckBox ID="chkya" runat="server" AutoPostBack="True" 
                                                 Text="Ya" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        
                                        <td style="height: 19px" colspan="3">
                                             <asp:TextBox ID="txtRekomHSE" runat="server" BorderStyle="Groove" TextMode="MultiLine"
                                                Font-Size="X-Small" Width="100%" Style="font-family: Arial, Helvetica, sans-serif;
                                                font-size: small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                              <asp:CheckBox ID="chkno" runat="server" AutoPostBack="True" 
                                                 Text="Tidak" />                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="height: 6px" valign="top">
                                            <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                                Width="100%" Visible="False">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                                                    CellPadding="4" HorizontalAlign="Left" 
                                                    OnPageIndexChanging="GridView1_PageIndexChanging" 
                                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" 
                                                    PageSize="5" 
                                                    Style="margin-right: 40px;
                                                margin-bottom: 0px; font-family: Arial, Helvetica, sans-serif; font-size: small;" 
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                        <asp:BoundField DataField="Laporan_No" HeaderText="No.TPP">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DeptFrom" HeaderText="Dept. Pemberi">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TPP_Date" DataFormatString="{0:d}" 
                                                            HeaderText="Tgl.TPP">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Uraian" HeaderText="Uraian">
                                                            <ControlStyle Width="70px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="55%" Wrap="True" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Approval" HeaderText="Status">
                                                            <HeaderStyle HorizontalAlign="Center" Width="13%" />
                                                        </asp:BoundField>
                                                        <asp:ButtonField CommandName="Add" Text="Pilih">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                            <ItemStyle ForeColor="#3399FF" />
                                                        </asp:ButtonField>
                                                    </Columns>
                                                    <RowStyle BackColor="White" Font-Names="tahoma" Font-Size="X-Small" 
                                                        ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                    <HeaderStyle BackColor="#666666" BorderColor="Black" BorderStyle="Groove" 
                                                        BorderWidth="1px" Font-Bold="false" Font-Names="Arial" Font-Size="Smaller" 
                                                        ForeColor="White" Wrap="True" />
                                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                    <PagerStyle BackColor="#99CCCC" BorderStyle="Solid" ForeColor="#003399" 
                                                        HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="height: 6px" valign="top">
                                            <asp:Panel ID="PanelForm" runat="server" BackColor="White" ScrollBars="Auto" 
                                                Visible="False">
                                                <table style="width: 100%; border-top-style: solid; border-collapse: collapse;">
                                                    <tr>
                                                        <td colspan="2" style="border: thin none #000000;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-left: thin solid #000000; border-right: thin solid #000000; border-top: thin solid #000000;
                                border-bottom: thin none #000000;" width="75%">
                                                            PT. BANGUNPERKASA ADHITAMASENTRA
                                                        </td>
                                                        <td align="right" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin solid #000000; border-bottom: thin none #000000; font-size: x-small;" width="25%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000;" width="75%">
                                                            <b><span style="font-size: x-large">TINDAKAN PERBAIKAN DAN PENCEGAHAN</span></b>
                                                        </td>
                                                        <td align="right" style="border: thin solid #000000;" width="25%">
                                                            Laporan No.
                                                            <asp:TextBox ID="TxtLaporanNo" runat="server" ReadOnly="True" Width="167px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="Panel5" runat="server">
                                                                <table style="width: 100%; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td colspan="2" style="border: thin solid #000000; width: 25%;" width="10%">
                                                                            Diterbitkan :
                                                                        </td>
                                                                        <td style="border: thin solid #000000; border-collapse: collapse" width="75%">
                                                                            Asal Permasalahan :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border: thin solid #000000" width="15%">
                                                                            Tgl Terbit TPP
                                                                        </td>
                                                                        <td style="border: thin solid #000000" width="15%">
                                                                            &nbsp;
                                                                            <bdp:BDPLite ID="txtTPP_Date0" runat="server" CssClass="style2" Enabled="False" 
                                                                                ToolTip="klik icon untuk merubah tanggal" Width="95%">
                                                                            </bdp:BDPLite>
                                                                        </td>
                                                                        <td rowspan="3" style="border-collapse: collapse" width="75%">
                                                                            <asp:Panel ID="Panel6" runat="server">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td colspan="2" style="width: 50%" width="40%">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Panel ID="PanelRB1" runat="server">
                                                                                                            1. Audit External</asp:Panel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:RadioButton ID="Rb1" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                            Text=" " />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Panel ID="PanelRB2" runat="server">
                                                                                                            / Internal</asp:Panel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:RadioButton ID="Rb2" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                            Text=" " />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRB4" runat="server">
                                                                                                3. NCR Customer Complain</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb4" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2" style="width: 50%" width="40%">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Klausul No :&nbsp;
                                                                                            <asp:TextBox ID="txtKlausulNo" runat="server" AutoPostBack="True" 
                                                                                                BorderStyle="Groove" Height="20px" Width="182px"></asp:TextBox>
                                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                                                                completioninterval="200" completionsetcount="10" enablecaching="true" 
                                                                                                firstrowselected="True" minimumprefixlength="1" servicemethod="GetKlausulNo" 
                                                                                                servicepath="AutoComplete.asmx" targetcontrolid="txtKlausulNo">
                                                                                            </cc1:AutoCompleteExtender>
&nbsp;<asp:Panel ID="Panelklausul" runat="server" Height="100px" ScrollBars="Vertical" Visible="False" Width="100%" Wrap="False">
                                                                                                <asp:GridView ID="GridKlausul" runat="server" AutoGenerateColumns="False" 
                                                                                                    PageSize="25" Width="100%">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="Klausul_No" HeaderText="Klausul_No" />
                                                                                                        <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" />
                                                                                                        <asp:ButtonField CommandName="Add" Text="Pilih" />
                                                                                                    </Columns>
                                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                                                                        ForeColor="Gold" />
                                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRb5" runat="server">
                                                                                                4. Kecelakaan Kerja</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb5" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 23px" width="40%">
                                                                                            <asp:Panel ID="PanelRb1a" runat="server">
                                                                                                &nbsp;&nbsp;&nbsp; a. Non conformity (Major)</asp:Panel>
                                                                                        </td>
                                                                                        <td style="height: 23px" width="10%">
                                                                                            <asp:RadioButton ID="Rb1a" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                        <td style="height: 23px" width="40%">
                                                                                            &nbsp;&nbsp;&nbsp; <span style="text-decoration: underline">Khusus kecelakaan kerja&nbsp;</span>
                                                                                        </td>
                                                                                        <td style="height: 23px" width="10%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRb1b" runat="server">
                                                                                                &nbsp;&nbsp;&nbsp; b. Area of concern&nbsp;(Minor)</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb1b" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                        <td width="40%">
                                                                                            &nbsp;&nbsp;&nbsp; Harus ada BA (Berita Acara)&nbsp;
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRb1c" runat="server">
                                                                                                &nbsp;&nbsp;&nbsp; c. Opportunity for improvement (rekomendasi)</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb1c" runat="server" AutoPostBack="True" GroupName="b" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRb6" runat="server">
                                                                                                5. Lain-lain</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb6" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="40%">
                                                                                            <asp:Panel ID="PanelRb3" runat="server">
                                                                                                2. NCR Proses</asp:Panel>
                                                                                        </td>
                                                                                        <td width="10%">
                                                                                            <asp:RadioButton ID="Rb3" runat="server" AutoPostBack="True" GroupName="a" 
                                                                                                Text=" " />
                                                                                        </td>
                                                                                        <td width="40%">
                                                                                            &nbsp;<asp:TextBox ID="txtKeterangan" runat="server" Visible="False" Width="100%"></asp:TextBox></td>
                                                                                        <td width="10%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border: thin solid #000000" width="10%">
                                                                            Tgl Kejadian / Tgl Audit
                                                                        </td>
                                                                        <td style="border: thin solid #000000" width="15%">
                                                                            &nbsp;&nbsp;<bdp:BDPLite ID="txtTPP_Date" runat="server" CssClass="style2" 
                                                                                ToolTip="klik icon untuk merubah tanggal" Width="95%">
                                                                            </bdp:BDPLite>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="border: thin solid #000000" width="10%">
                                                                            Departemen
                                                                        </td>
                                                                        <td style="border: thin solid #000000" width="15%">
                                                                            &nbsp;
                                                                            <asp:DropDownList ID="ddlDeptName" runat="server" 
                                                                                Enabled="False" Width="90%">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="Panel4" runat="server">
                                                                <table style="width: 100%; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td style="border: thin none #000000" width="25%">
                                                                            Uraian Ketidaksesuaian :
                                                                        </td>
                                                                        <td align="right">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:TextBox ID="txtUTSesuai" runat="server" Height="47px" TextMode="MultiLine" 
                                                                                Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="PanelAudite" runat="server" Visible="False">
                                                                                <table style="width: 100%; border-collapse: collapse;">
                                                                                    <tr>
                                                                                        <td align="left" colspan="2" style="border: thin solid #000000; width: 40%;" 
                                                                                            width="20%">
                                                                                            Kolom Persetujuan Khusus Audit :
                                                                                        </td>
                                                                                        <td align="center" 
                                                                                            style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;" 
                                                                                            width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                                            &nbsp;Auditee
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                                            Auditor
                                                                                        </td>
                                                                                        <td align="center" 
                                                                                            style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;" 
                                                                                            width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                                            <asp:Label ID="LMgr0" runat="server" Text="Open"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                                            <asp:Label ID="LAuditor0" runat="server" Text="Open"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" 
                                                                                            style="border-style: solid; border-width: thin; border-color: #FFFFFF #FFFFFF #FFFFFF #000000;" 
                                                                                            width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td align="center" style="border: thin solid #FFFFFF" width="20%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="Panel7" runat="server">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td colspan="7">
                                                                            Analisa Penyebab :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="40%">
                                                                            <i>Isi Ketidaksesuaian (ringkas) :</i>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 25px" width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="height: 25px" width="10%">
                                                                            <asp:Panel ID="Panel13" runat="server" Height="24px" HorizontalAlign="Center" 
                                                                                Width="85px">
                                                                                <asp:CheckBox ID="chkManusia" runat="server" AutoPostBack="True" 
                                                                                    Text="Manusia" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="height: 25px" width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="height: 25px" width="10%">
                                                                            <asp:Panel ID="Panel15" runat="server" Height="24px" HorizontalAlign="Center" 
                                                                                Width="85px">
                                                                                <asp:CheckBox ID="chkMesin" runat="server" AutoPostBack="True" Text="Mesin" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="height: 25px" width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="height: 25px" width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td rowspan="5" width="40%">
                                                                            <asp:TextBox ID="txtITSesuai" runat="server" Enabled="False" Height="88px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="center" width="10%">
                                                                            <asp:Image ID="Image4" runat="server" Height="16px" 
                                                                                ImageUrl="~/images/Panah miringB.jpg" Width="41px" />
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="center" width="10%">
                                                                            <asp:Image ID="Image5" runat="server" Height="16px" 
                                                                                ImageUrl="~/images/Panah miringB.jpg" Width="41px" />
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            <asp:Panel ID="Panel14" runat="server" Height="25px" HorizontalAlign="Center" 
                                                                                Width="95px">
                                                                                <asp:CheckBox ID="chkLingkungan" runat="server" AutoPostBack="True" 
                                                                                    Text="Lingkungan" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td colspan="5">
                                                                            <asp:Image ID="Image2" runat="server" Height="16px" 
                                                                                ImageUrl="~/images/Panah Lurus.jpg" Width="100%" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="center" width="10%">
                                                                            <asp:Image ID="Image1" runat="server" Height="16px" 
                                                                                ImageUrl="~/images/Panah miring.jpg" Width="41px" />
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="center" width="10%">
                                                                            <asp:Image ID="Image3" runat="server" Height="16px" 
                                                                                ImageUrl="~/images/Panah miring.jpg" Width="41px" />
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            <asp:Panel ID="Panel17" runat="server" Height="24px" HorizontalAlign="Center" 
                                                                                Width="85px">
                                                                                <asp:CheckBox ID="chkMaterial" runat="server" AutoPostBack="True" 
                                                                                    Text="Material" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            <asp:Panel ID="Panel16" runat="server" Height="24px" HorizontalAlign="Center" 
                                                                                Width="85px">
                                                                                <asp:CheckBox ID="chkMetode" runat="server" AutoPostBack="True" Text="Metode" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td colspan="6">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            Mesin
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <asp:TextBox ID="txtMesin" runat="server" Enabled="False" Height="44px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            Manusia
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <asp:TextBox ID="txtManusia" runat="server" Enabled="False" Height="44px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            Material
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <asp:TextBox ID="txtMaterial" runat="server" Enabled="False" Height="44px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            Metode
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <asp:TextBox ID="txtMetode" runat="server" Enabled="False" Height="44px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            Lingkungan
                                                                        </td>
                                                                        <td colspan="6">
                                                                            <asp:TextBox ID="txtLingkungan" runat="server" Enabled="False" Height="44px" 
                                                                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="40%">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="Panel8" runat="server">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
&nbsp;<asp:Panel ID="PanelPebaikan" runat="server" Height="50px" Visible="False">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td align="center" style="font-size: x-small" width="40%">
                                                                                            Tindakan
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Pelaku
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Jadwal Selesai
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Aktual Selesai
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" style="font-size: x-small" width="40%">
                                                                                            <asp:TextBox ID="txtTIndakan" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            <asp:TextBox ID="txtPelaku" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            <bdp:BDPLite ID="txtDateJS1" runat="server" CssClass="style2" 
                                                                                                ToolTip="klik icon untuk merubah tanggal">
                                                                                            </bdp:BDPLite>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            <bdp:BDPLite ID="txtDateAS1" runat="server" CssClass="style2" 
                                                                                                ToolTip="klik icon untuk merubah tanggal">
                                                                                            </bdp:BDPLite>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel9" runat="server" Height="80px" ScrollBars="Vertical">
                                                                                <asp:GridView ID="GridPerbaikan" runat="server" AutoGenerateColumns="False" 
                                                                                    PageSize="4" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Verifikasi">
                                                                                            <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ID">
                                                                                            <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                                                            <ItemStyle Width="60%" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
                                                                                        <asp:BoundField DataField="Jadwal_Selesai" DataFormatString="{0:d}" 
                                                                                            HeaderText="Jadwal Selesai" />
                                                                                        <asp:BoundField DataField="Aktual_Selesai" DataFormatString="{0:d}" 
                                                                                            HeaderText="Aktual Selesai" />
                                                                                        <asp:TemplateField HeaderText="Verifikasi">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkVerifikasi1" runat="server" AutoPostBack="True" 
                                                                                                    Enabled="False" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="TglVerifikasi" HeaderText="Tgl Verifikasi" />
                                                                                    </Columns>
                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                                                        ForeColor="Gold" />
                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 1px">
                                                                            &nbsp;&nbsp;<asp:Panel ID="PanelPencegahan" runat="server" Height="50px" Visible="False">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td align="center" style="font-size: x-small" width="40%">
                                                                                            Tindakan
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Pelaku
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Jadwal Selesai
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            Aktual Selesai
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            &nbsp;
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
                                                                                            <bdp:BDPLite ID="txtDateJS2" runat="server" CssClass="style2" 
                                                                                                ToolTip="klik icon untuk merubah tanggal">
                                                                                            </bdp:BDPLite>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
                                                                                            <bdp:BDPLite ID="txtDateAS2" runat="server" CssClass="style2" 
                                                                                                ToolTip="klik icon untuk merubah tanggal">
                                                                                            </bdp:BDPLite>
                                                                                        </td>
                                                                                        <td align="center" style="font-size: x-small">
&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="Panel10" runat="server" Height="80px" ScrollBars="Vertical" 
                                                                                Width="100%">
                                                                                <asp:GridView ID="GridPencegahan" runat="server" AutoGenerateColumns="False" 
                                                                                    PageSize="4" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Verifikasi">
                                                                                            <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ID">
                                                                                            <ItemStyle BackColor="Blue" ForeColor="Blue" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="TIndakan" HeaderText="TIndakan">
                                                                                            <ItemStyle Width="60%" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Pelaku" HeaderText="Pelaku" />
                                                                                        <asp:BoundField DataField="Jadwal_Selesai" DataFormatString="{0:d}" 
                                                                                            HeaderText="Jadwal Selesai" />
                                                                                        <asp:BoundField DataField="Aktual_Selesai" DataFormatString="{0:d}" 
                                                                                            HeaderText="Aktual Selesai" />
                                                                                        <asp:TemplateField HeaderText="Verifikasi">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkVerifikasi0" runat="server" AutoPostBack="True" 
                                                                                                    Enabled="False" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="TglVerifikasi" HeaderText="Tgl Verifikasi" />
                                                                                    </Columns>
                                                                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                                                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" 
                                                                                        BorderWidth="2px" Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" 
                                                                                        ForeColor="Gold" />
                                                                                    <PagerStyle BorderStyle="Solid" />
                                                                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="PanelHSE" runat="server" Height="80px" Width="100%" ScrollBars="Vertical">
                                                                                <asp:Label ID="Label1" runat="server">Rekomendasi HSE (Khusus Kecelakaan Kerja) :</asp:Label><br />
                                                                                <asp:CheckBox ID="chkhseya" runat="server" AutoPostBack="true" Text="Ada" /><br />
                                                                                <asp:TextBox ID="txthse" runat="server" Width="30%" ></asp:TextBox><br />
                                                                                <asp:CheckBox ID="chkhseno" runat="server" AutoPostBack="True" Text="Tidak Ada" /><br /><br />
                                                                            </asp:Panel><br />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="PanelStatus" runat="server" Enabled="False">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td width="10%">
                                                                            STATUS :
                                                                        </td>
                                                                        <td width="7%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="5%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="7%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="15%">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td align="left" colspan="2" style="text-decoration: underline">
                                                                            Khusus untuk audit :
                                                                        </td>
                                                                        <td align="left" colspan="2" style="text-decoration: underline">
&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td colspan="4">
                                                                            Tutup Kasus
                                                                            <asp:CheckBox ID="chkClose" runat="server" AutoPostBack="True" Text="  " />
                                                                            Tanggal :
                                                                            <bdp:BDPLite ID="txtDateTKasus" runat="server" CssClass="style2" 
                                                                                Enabled="False" ToolTip="klik icon untuk merubah tanggal">
                                                                                <TextBoxStyle Font-Size="X-Small" />
                                                                            </bdp:BDPLite>
                                                                        </td>
                                                                        <td width="15%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 12%" width="7%">
                                                                            Solved
                                                                            <asp:CheckBox ID="chksolved" runat="server" AutoPostBack="True" Text="  " />
                                                                        </td>
                                                                        <td colspan="2" width="7%">
                                                                            Tanggal :
                                                                        </td>
                                                                        <td width="10%">
                                                                            <bdp:BDPLite ID="txtDateSolved0" runat="server" CssClass="style2" 
                                                                                Enabled="False" ToolTip="klik icon untuk merubah tanggal">
                                                                            </bdp:BDPLite>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="7%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="5%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="7%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="10%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td width="15%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td colspan="3" style="width: 20%" width="10%">
                                                                            Tanggal waktu / due date Tanggal :
                                                                        </td>
                                                                        <td width="10%">
                                                                            <bdp:BDPLite ID="txtDueDate0" runat="server" CssClass="style2" Enabled="False" 
                                                                                ToolTip="klik icon untuk merubah tanggal">
                                                                            </bdp:BDPLite>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2" style="border-left: thin solid #000000; border-right: thin solid #000000;
                                border-top: thin none #000000; border-bottom: thin solid #000000; font-size: x-small;">
                                                            <asp:Panel ID="Panel12" runat="server">
                                                                <table style="width: 100%; border-collapse: collapse;">
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            Kolom Persetujuan / Pelaksanaan :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            &nbsp; Spv /Kasie Dept.
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            &nbsp; Manager Dept.
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            &nbsp; Plant Manager
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            &nbsp; Auditor (Khusus Audit)
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            &nbsp; MR
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            <asp:Label ID="LSpv" runat="server" Text="Open"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            <asp:Label ID="LMgr" runat="server" Text="Open"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            <asp:Label ID="LPMgr" runat="server" Text="Open"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            <asp:Label ID="LAuditor" runat="server" Text="Open"></asp:Label>
                                                                        </td>
                                                                        <td align="center" style="border: thin solid #000000" width="20%">
                                                                            <asp:Label ID="LMR" runat="server" Text="Open"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
