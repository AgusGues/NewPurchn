<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormNonAktif.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormNonAktif" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 24px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="table-responsive" style="width:100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>NonAktif Dokumen</span>
                <div class="pull-right">

                </div>
            </div>
            <div class="panel-body">

                <asp:Panel ID="Panel1" runat="server" Visible="true">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">IdSystem</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNO" runat="server" Width="100%" ReadOnly="True" 
                                Enabled="False" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kategory</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDOK" runat="server" Width="100%" ReadOnly="True" Enabled="False" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Tgl.NonAktif</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtBerlaku" runat="server" Width="100%" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender" TargetControlID="txtBerlaku" Format="dd-MMM-yyyy"
                                runat="server"></ajaxToolkit:CalendarExtender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NomorDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtISO" runat="server" Width="100%" Enabled="False" ></asp:TextBox>
                                <asp:TextBox ID="txtNoUPD" runat="server" Visible="false" Width="90%"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNama" runat="server" Width="100%" Enabled="False" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Alasan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtAlasan" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelLampiran" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="LabelLampiran" runat="server">Dokument Baru</asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                                OnRowCommand="GridView4_RowCommand" 
                                OnRowDataBound="GridView4_RowDataBound" Style="font-family: 'Courier New', Courier, monospace;
                                font-size: 10pt; font-weight: 500; color: #FFFFFF;" Width="100%" 
                                ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FileLama") %>' CommandName="Download" Text='<%# Eval("FileLama") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="White" />
                                    <ItemStyle BackColor="#CCCCCC" ForeColor="White" /></asp:TemplateField>
                                </Columns></asp:GridView>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="PanelButtonHapus" runat="server" Visible="true"> 
                <div class="row">
                    <div class="col-md-12">
                        <input class="btn btn-info" id="btnHapus" runat="server" type="button" value="Non Aktifkan" onserverclick="btnHapus_ServerClick" />
                        <asp:RadioButton ID="RBManualHapus" runat="server" AutoPostBack="True" OnCheckedChanged="RBManualHapus_CheckedChanged" Text="&nbsp; Non Aktif Manual" TextAlign="Left"/>
                        <div style="padding: 2px;"></div>
                    </div>
                </div>   </asp:Panel>

                <asp:Panel ID="PanelGridPanel1" runat="server" Visible="true"> 
                <div class="table-responsive">
                    <table style="width: 100%;">
                        <tr style="width: 100%;">
                            <td colspan="5" style="height: 3px" valign="top">
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                    OnRowDataBound="GridView1_RowDataBound" Width="100%" 
                                    style="font-family: Calibri; font-size: small">
                                    <Columns>
                                        <asp:BoundField DataField="No" HeaderText="No" Visible="true" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /> </asp:BoundField>
                                        <asp:BoundField DataField="UpdNo" HeaderText="No. UPD" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /></asp:BoundField>
                                        <asp:BoundField DataField="NoDokumen" HeaderText="No. Dokumen" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /> </asp:BoundField>
                                        <asp:BoundField DataField="UpdName" HeaderText="Nama Dokumen" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />  </asp:BoundField>
                                        <asp:BoundField DataField="Kategori" HeaderText="Kategori" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />  </asp:BoundField>
                                        <asp:BoundField DataField="DeptName" HeaderText="Department" >
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" /> </asp:BoundField>
                                        <asp:BoundField DataField="TglPengajuan" DataFormatString="{0:d}" 
                                        HeaderText="Tgl. Pengajuan" >                                    
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />   </asp:BoundField>
                                        <asp:ButtonField CommandName="Add" Text="Pilih" >                                    
                                        <HeaderStyle Font-Names="Calibri" Font-Size="X-Small" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="X-Small" />   </asp:ButtonField>
                                    </Columns>
                                    <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="X-Small" />
                                    <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                    Font-Bold="True" Font-Names="tahoma" Font-Size="X-Small" ForeColor="white" />
                                    <PagerStyle BorderStyle="Solid" />
                                    <AlternatingRowStyle BackColor="Gainsboro" /> </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table> 
                </div></asp:Panel>  

                <asp:Panel ID="PanelPilihan" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-12">
                        <asp:RadioButton ID="RBBiasa" runat="server" AutoPostBack="True" OnCheckedChanged="RBBiasa_CheckedChanged"
                        Text="Dokumen Biasa" TextAlign="Left" />
                        <asp:RadioButton ID="RBKhusus" runat="server" AutoPostBack="True" OnCheckedChanged="RBKhusus_CheckedChanged"
                        Text="Dokumen Khusus" TextAlign="Left"  />
                        <asp:RadioButton ID="RBAwal" runat="server" AutoPostBack="True" OnCheckedChanged="RBAwal_CheckedChanged"
                        Text="Kembali ke Awal" TextAlign="Left" />
                        <div style="padding: 2px;"></div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="Panel2Biasa" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">CariNomor</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtNomor" runat="server" Width="100%" Style="font-family: Calibri;
                                font-size: x-small; font-weight: 700"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionInterval="200"
                                CompletionSetCount="10" ContextKey="0" EnableCaching="true" FirstRowSelected="True"
                                MinimumPrefixLength="1" ServiceMethod="CekNomorDokumenBiasa2" ServicePath="AutoCompleteUPD.asmx"
                                TargetControlID="txtNomor" UseContextKey="true"> </ajaxToolkit:AutoCompleteExtender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <input class="btn btn-info" id="btncek" runat="server" type="button" value="cari" onserverclick="btncek_ServerClick" />
                                <asp:TextBox ID="txtIDmaster2" runat="server" Visible="False"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NomorDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNomorisi" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kategory</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKategoriisi" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Tgl.NonAktif</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtNonAktifisi" runat="server" Width="100%" ></asp:TextBox>                       
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtNonAktifisi" Format="dd-MMM-yyyy"
                                runat="server"> </ajaxToolkit:CalendarExtender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NamaDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNamaisi" runat="server"  Width="100%" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDeptisi" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Alasan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtAlasanisi" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Label ID="LabelSuksesSimpan" runat="server" Visible="false">&nbsp;</asp:Label>
                                <input class="btn btn-info" id="btnHapusBiasa" runat="server" type="button" value="Non Aktif" onserverclick="btnHapusBiasa_ServerClick" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div> </asp:Panel>

                <asp:Panel ID="Panel2Khusus" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">CariNomor</div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtKhusus" runat="server" Width="100%" Style="font-family: Calibri; 
                                font-size: x-small; font-weight: 700"></asp:TextBox>
                                <ajaxtoolkit:autocompleteextender id="AutoCompleteExtender1" runat="server" completioninterval="200"
                                completionsetcount="10" contextkey="0" enablecaching="true" firstrowselected="True"
                                minimumprefixlength="1" servicemethod="CekNamaDokumenKhusus" servicepath="AutoCompleteUPD.asmx"
                                targetcontrolid="txtKhusus" usecontextkey="true"> </ajaxtoolkit:autocompleteextender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <input class="btn btn-info" id="btncekkhusus" runat="server" type="button" value="cari" onserverclick="btncekkhusus_ServerClick" />
                                <asp:TextBox ID="txtNamaDokumen" runat="server" Visible="False"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NomorDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNomorisiKhusus" runat="server" Width="100%" ReadOnly="True" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Kategory</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtKategoriisiKhusus" runat="server" Width="100%" ReadOnly="True" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Tgl.NonAktif</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtTglNonisiKhusus" runat="server" Width="100%" 
                                ></asp:TextBox>                       
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTglNonisiKhusus" Format="dd-MMM-yyyy"
                                runat="server"> </ajaxToolkit:CalendarExtender>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-4">NamaDokument</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtNamaisiKhusus" runat="server"  Width="100%" ReadOnly="True" ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Department</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtDeptisiKhusus" runat="server" Width="100%" ReadOnly="True"></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">Alasan</div>
                            <div class="col-md-8">
                                <asp:TextBox class="form-control" ID="txtAlasanisiKhusus" runat="server" TextMode="MultiLine" Height="100px"
                                ></asp:TextBox>
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-right">
                                <asp:Label ID="LabelSuksesSimpanKhusus" runat="server" Visible="false">&nbsp;</asp:Label>
                                <input class="btn btn-info" id="btnHapusKhusus" runat="server" type="button" value="Non Aktif" onserverclick="btnHapusKhusus_ServerClick" />
                                <div style="padding: 2px;"></div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

            </div>
        </div>
        <asp:Literal ID="pdfView" runat="server" Mode="PassThrough"></asp:Literal>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
