<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormApprovalPM.aspx.cs" Inherits="GRCweb1.Modul.ISO_UPD.FormApprovalPM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .page-content {width:100%;background-color: #fff;margin: 0;padding: 8px 8px 8px;overflow-x: auto;min-height: .01%;}
    .btn {font-style: normal;border: 1px solid transparent;padding: 2px 4px;font-size: 11px;height: 23px;border-radius: 4px;}
    input,select,.form-control,select.form-control,select.form-group-sm .form-control
    {height: 24px;color: #000;padding: 2px 4px;font-size: 12px;border: 1px solid #d5d5d5;border-radius: 4px;}
    .table > tbody > tr > th,.table > tbody > tr > td 
    {border: 0px solid #fff;padding: 2px 4px;font-size: 12px;color:#fff;font-family: sans-serif;}
    .contentlist {border: 0px solid #B0C4DE;}label{font-size: 12px;}
</style>
<script type="text/javascript">
    if (!window.showModalDialog) {
        window.showModalDialog = function (arg1, arg2, arg3) {
            var w;
            var h;
            var resizable = "no";
            var scroll = "no";
            var status = "no";
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
<script src="../../Scripts/jquery-1.2.6-vsdoc.js" type="text/javascript"></script>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div id="div1" runat="server" class="table-responsive" style="width:100%">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span>Approval Upd</span>
                <div class="pull-right">
                    <asp:Button class="btn btn-info" ID="btnPrev" runat="server" Text="<<Prev" Enabled="false" 
                    OnClick="btnPrev_Click" />
                    <asp:Button class="btn btn-info" ID="btnApprove" runat="server" Text="Approved" 
                    OnClick="btnApprove_Click"/>
                    <asp:Button class="btn btn-info" ID="btnNotApv" runat="server" Text="Not Approved" Enabled="false"
                    OnClick="btnNotApv_Click" />
                    <asp:Button class="btn btn-info" ID="btnNext" runat="server" Text="Nex>>" 
                    OnClick="btnNext_Click" />
                    <%-- <asp:Button ID="btnNotApprove" runat="server" Text="Not Approved" Enabled="false"
                    OnClick="btnNotApprove_Click" style="font-family: Calibri" />--%>
                    &nbsp;
                    <%--<asp:TextBox ID="txtCari" Width="250px" Text="Find by Nomor UPD" onfocus="if(this.value==this.defaultValue)this.value='';"
                    onblur="if(this.value=='')this.value=this.defaultValue;" runat="server" 
                    placeholder="Find by Nomor SPP" style="font-family: Calibri"></asp:TextBox>
                    <asp:Button ID="btnCari" runat="server" Text="Cari" OnClick="btnCari_Click" 
                    style="font-family: Calibri" />--%>
                    <asp:HiddenField ID="noSPP" runat="server" />
                </div>
            </div>
            <div class="panel-body">

                <asp:Panel ID="PanelRB" runat="server" Visible="true">
                <div class="table-responsive">
                    <table id="Table5" style="width: 100%;">
                        <tr>
                            <%--Samping Kiri--%>
                            <td style="width: 40%">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <td style="width: 50%;">
                                            <b><asp:Label ID="LabelInfoShare" runat="server" Visible="false"></asp:Label></b>
                                        </td>
                                        <td style="width: 50%;" class="text-right">
                                            <asp:RadioButton ID="RBShare" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="RBShare_CheckedChanged"
                                            Text="....Untuk melihatnya silahkan klik di lingkaran ini >>" TextAlign="Left" Width="300px" />
                                        </td>
                                        <td style="height: 3px; width: 40%;" valign="top">
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 45%; font-size: x-small;" rowspan="2">
                                            <asp:Label ID="LabelInfoNotApprove" runat="server" Style="font-family: 'Agency FB';
                                            font-size: small; font-weight: 700" Visible="false" BackColor="#00CC00">&nbsp;</asp:Label>
                                        </td>
                                        <%--<td style="height: 3px; width: 20%;" valign="top"></td>--%>
                                        <td style="height: 3px; width: 10%;" valign="top">
                                            <asp:RadioButton ID="RBNotApprove" runat="server" AutoPostBack="True" GroupName="a"
                                            Visible="False" OnCheckedChanged="RBNotApprove_CheckedChanged" Style="font-family: Calibri;
                                            font-size: x-small; font-style: italic; background-color: #CCCCFF; color: #000066;"
                                            Text="....Untuk melihatnya silahkan klik di lingkaran ini >>" 
                                            TextAlign="Left" Width="300px" />
                                        </td>
                                        <td style="height: 3px; width: 45%;" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div></asp:Panel>

                <asp:Panel ID="PanelRBNotApprove" runat="server" Visible="true" 
                Style="font-family: Calibri; background-color: #CCCCFF;" 
                BackColor="#669999" ForeColor="White">
                <div>
                    <table id="Table4" style="width: 100%;">                                    
                        <tr>
                            <%--Samping Kiri--%>
                            <td style="width: 40%">
                                <table style="width: 100%">
                                    <tr style="width: 100%">
                                        <%-- <td style="width: 20%; color: #000066; font-size: x-small; background-color: #99FF33;">
                                        <asp:Label ID="LabelInfoNotApprove" runat="server" Style="font-family: 'Agency FB'; font-size: small; font-weight: 700" Visible="false">&nbsp;</asp:Label></td>--%>
                                        <td style="height: 3px; width: 20%; background-color: #CCCCFF;" valign="top">
                                            <%--<asp:RadioButton ID="RBNotApprove" runat="server" AutoPostBack="True" GroupName="a"
                                            OnCheckedChanged="RBNotApprove_CheckedChanged" Style="font-family: Calibri; font-size: x-small;font-style: italic; background-color: #CCCCFF; color: #000066;" Text="silahkan klik disini >>" TextAlign="Left" Width="156px" />--%>
                                        </td>
                                        <td style="height: 3px; width: 70%;" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>                                           
                        </tr>
                    </table>
                </div></asp:Panel>

                <asp:Panel ID="Panel1" runat="server" Visible="true">
                <div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelNoS" runat="server" Visible="true">NoSystem</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtNoS" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelDept1" runat="server" Visible="true">Department</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtDept1" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelDibuatOleh" runat="server" Visible="true">DibuatOleh</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtDibuatOleh" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelPermintaan" runat="server" Visible="true">Permintaan</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtPermintaan" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelTglPengajuan" runat="server" Visible="true">TglPengajuan</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtTglPengajuan" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="LabelAlasan" runat="server" Visible="true">AlasanPengajuan :</asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox class="form-control" ID="txtAlasan" runat="server" TextMode="MultiLine" Rows="7" Visible="true"
                                    ReadOnly="true"></asp:TextBox>
                                    <div style="padding: 2px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="Panel2" runat="server" Visible="true">
                <div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelKategori" runat="server" Visible="true">Katergory</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtKategori" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding:2px;"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="LabelNoDok" runat="server" Visible="true">NoDokumen</asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox class="form-control" ID="txtNoDok" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
                                    <div style="padding:2px;"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="LabelNamaDok1" runat="server" Visible="true">NamaDokumen :</asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox class="form-control" ID="txtNamDok1" runat="server" TextMode="MultiLine" Rows="2" Visible="true"
                                    ReadOnly="true"></asp:TextBox>
                                    <div style="padding:2px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div></asp:Panel>

                <asp:Panel ID="Panel3" runat="server" Visible="true" 
                Style="font-family: Calibri" BackColor="#669999">
                <div class="table-responsive">
                    <table id="Table3" style="width: 100%;">
                        <tr>
                            <%--Samping Kiri--%>
                            <td style="width: 40%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="LabelRevisi" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="false">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:TextBox ID="txtRevisi" runat="server" Visible="false" ReadOnly="true" Style="font-family: 'Courier New', Courier, monospace;
                                            font-size: small" Width="50px"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="LabelRevisiKe" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="false">&nbsp;</asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:TextBox ID="txtRevisiKe" runat="server" Visible="false" ReadOnly="true" Style="font-family: 'Courier New', Courier, monospace;
                                            font-size: small" Width="50px"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%--<tr><td style="width: 20%"> <asp:Label ID="Label1" runat="server" Style="font-family: 'Agency FB'; font-size: small;font-weight: 700" Visible="true">&nbsp; </asp:Label> </td> <td style="width: 50%"> &nbsp; </td> </tr>--%>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label2" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label32" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label4" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label5" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <%--Samping Kanan--%>
                            <td style="width: 60%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label6" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; DRAFT DOKUMEN BARU :</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 300px">
                                            <%--  <table style="margin-right: 29px; text-align: left;">--%>
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" OnRowCommand="GridView3_RowCommand"
                                            OnRowDataBound="GridView3_RowDataBound" Style="font-family: 'Courier New', Courier, monospace; font-size: 10pt;
                                            font-weight: 500;" Width="600px" GridLines="None" ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument='<%# Eval("NamaFile") %>'
                                                    CommandName="Download" Text='<%# Eval("NamaFile") %>'>
                                                </asp:LinkButton></ItemTemplate></asp:TemplateField>
                                            </Columns></asp:GridView>
                                            <%-- </table>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label31" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label8" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; DOKUMEN LAMA :</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 300px">
                                            <%--  <table style="margin-right: 29px; text-align: left;">--%>
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White" Enabled="true"
                                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="1" OnRowCommand="GridView4_RowCommand"
                                            OnRowDataBound="GridView4_RowDataBound" Style="font-family: 'Courier New', Courier, monospace; font-size: 10pt;
                                            font-weight: 500;" Width="600px" GridLines="None" ShowHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FileLama") %>'
                                                    CommandName="Download" Text='<%# Eval("FileLama") %>'></asp:LinkButton>
                                                </ItemTemplate></asp:TemplateField>
                                            </Columns></asp:GridView>
                                            <%-- </table>--%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label21" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label41" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label51" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <asp:Label ID="Label7" runat="server" Style="font-family: 'Agency FB'; font-size: small;
                                            font-weight: 700" Visible="true">&nbsp; </asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div></asp:Panel>

                <asp:Panel ID="PanelPreview" runat="server" Visible="false" Style="font-family: Calibri" BackColor="#669999">
                <div class="table-responsive">
                    <table style="width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="width: 100%; font-family: 'Agency FB'; font-size: small;font-weight: 700; height: 30px; color: #FFFFFF;" bgcolor="#9900CC" align="center">
                                <table>
                                    <tr>
                                        <td style="width: 100%; color: #FFFFFF; font-family: 'Agency FB'; font-weight: bold;"; align="center">
                                            .:: Preview Dokumen Lama ::.
                                        </td>                                            
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="pdfView" runat="server" Mode="PassThrough"></asp:Literal>
                </div></asp:Panel>

                <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
                <cc1:ModalPopupExtender ID="mpePopUp" runat="server"
                TargetControlID="lblHidden"
                PopupControlID="panEdit"
                CancelControlID="btnUpdateClose"
                BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>


                <asp:Panel ID="panEdit" runat="server" CssClass="Popup" Style="width: 600px;">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <span class="text-left">AlasanCancel</span>
                        <div class="pull-right">
                            <input class="btn btn-info" id="btnUpdateClose" runat="server" type="button" value="Close" />
                            <input class="btn btn-info" id="btnUpdateAlasan" runat="server" type="button" value="Update" onserverclick="btnUpdateAlasan_ServerClick" />
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:TextBox class="form-control" ID="txtAlasanCancel" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </div>
                </div></asp:Panel>

            </div>
        </div>
    </div>
</ContentTemplate>

</asp:UpdatePanel>
</asp:Content>
