<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMasterSupplierPurch.aspx.cs" Inherits="GRCweb1.Modul.Master.NewMasterSupplierPurch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <script type="text/javascript">

        //function confirm_lock() {
        //    if (confirm("Anda yakin akan lock/ unlock ?") == true) {
        //        window.showModalDialog('../../ModalDialog/ReasonLockUnlock.aspx?j=Revisi', '', 'resizable:yes;dialogHeight: 300px; dialogWidth: 400px;scrollbars=yes');
        //    } else {
        //        return false;
        //    }
        //}
        function OpenDialog(id) {
            params = 'dialogWidth:820px';
            params += '; dialogHeight:200px'
            params += '; top=0, left=0'
            params += '; resizable:no'
            params += ';scrollbars:no';
            window.showModalDialog("../../ModalDialog/UploadFileSupp.aspx?ba=" + id + "&tablename=SuppPurchAtachment", "UploadFile", params);
        };
    </script>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style> 
    <script language="Javascript" type="text/javascript" src="../../Scripts/calendar.js"></script>
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" style="width:100%">    
         
    <ContentTemplate>            
        <div id="Div1" runat="server">
            <table style="width: 100%">
                <tbody>
                    <tr>
                        <td style="width: 100%; height: 49px">
                            <table class="nbTableHeader">
                                <tr>
                                    <td style="width: 100%">
                                        <strong>&nbsp;SUPPLIER</strong>
                                    </td>
                                    <td style="width: 37px">
                                        <input id="btnNew" runat="server" type="button" value="Baru" onserverclick="btnNew_ServerClick" />
                                    </td>
                                    <td style="width: 75px">
                                        <input id="btnUpdate" runat="server" type="button" value="Simpan" onserverclick="btnUpdate_ServerClick" />
                                    </td>
                                    <td style="width: 5px">
                                        <asp:Button ID="btnDelete" runat="server" Text="Lock" onclick="btnDelete_ServerClick" />
                                        <asp:Button ID="btnUnlock" runat="server" Text="unLock" onclick="btnUnlock_ServerClick" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                                            <asp:ListItem Value="SupplierCode">Kode Supplier</asp:ListItem>
                                            <asp:ListItem Value="SupplierName">Nama Supplier</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 3px">
                                        <input id="btnSearch" runat="server" type="button" value="Cari" onserverclick="btnSearch_ServerClick" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="content" style="background:#fff;">
                                <table id="Table4" 
                                    style="width: 100%; border-collapse: collapse; font-size: x-small;" border="0">
                                    <tr>
                                        <td valign="top">
                                            &nbsp;</td>
                                        <td valign="top" colspan="3">
                                            
                                        


                                        </td>
                                        <td colspan="2" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="txtInfoRevisi" runat="server" CssClass="cursor" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td colspan="3" valign="top">
                                            <asp:TextBox ID="txtKeterangan" runat="server" BorderStyle="Groove" Height="19px" TabIndex="3" TextMode="MultiLine" Visible="false" Width="521px"></asp:TextBox>
                                        </td>
                                        <td colspan="2" valign="top">
                                            <asp:Label ID="txtStatus" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <span style="font-size: 10pt">&nbsp; Kode Supplier</span>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtSupplierCode" runat="server" BorderStyle="Groove" Width="233" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <span style="font-size: 10pt">&nbsp; U P</span>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtUP" runat="server" BorderStyle="Groove" Width="233" TabIndex="3"></asp:TextBox>
                                        </td>
                                        
                                        <td valign="top">
                                            No. KTP</td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtKTP" runat="server" BorderStyle="Groove" TabIndex="3" 
                                                Width="233"></asp:TextBox>
                                        </td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td ><span style="font-size: 10pt">&nbsp; Nama Supplier</span></td>
                                        <td><asp:TextBox ID="txtSupplierName" runat="server" BorderStyle="Groove" 
                                                onkeyup="this.value=this.value.toUpperCase()" Width="228px" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td><span style="font-size: 10pt">&nbsp; Telepon</span>
                                        </td>
                                        <td><asp:TextBox ID="txtTelepon" runat="server" BorderStyle="Groove" Width="233" TabIndex="4"></asp:TextBox>
                                        </td>
                                        <td>
                                            NPWP Pribadi</td>
                                        <td>
                                            <asp:TextBox ID="txtNPWP0" runat="server" AutoPostBack="True" 
                                                BorderStyle="Groove" Width="233px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtNPWP0_AutoCompleteExtender" runat="server" 
                                                CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" 
                                                FirstRowSelected="True" MinimumPrefixLength="1" 
                                                ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" 
                                                TargetControlID="txtNPWP0">
                                            </cc1:AutoCompleteExtender>
                                            
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td><span style="font-size: 10pt">&nbsp; Alamat</span></td>
                                        <td rowspan="2"><asp:TextBox ID="txtAlamat" runat="server" TextMode="MultiLine" 
                                                Rows="3" Width="234px"></asp:TextBox></td>
                                        <td valign="top"><span style="font-size: 10pt">&nbsp; Fax</span></td>
                                        <td valign="top"><asp:TextBox ID="txtFax" runat="server" BorderStyle="Groove" Width="233" TabIndex="5"></asp:TextBox>
                                        <span id="statSupp" runat="server" style="color:Red; font-size:x-small"></span></td>
                                        <td valign="top">
                                            Lampiran</td>
                                        <td valign="top">
                                        <asp:FileUpload ID="Upload1" runat="server" Width="80%" />
                                            <input ID="btnUpload" runat="server"   onserverclick="btnUpload_ServerClick"
                                                type="button" value="Upload" visible="true" /></td>
                                        <tr>
                                            <td style="height: 24px">
                                                </td>
                                            <td valign="top" style="height: 24px">
                                                &nbsp; <span style="font-size: 10pt">HandPhone </span>&nbsp;</td>
                                            <td valign="top" style="height: 24px">
                                                <asp:TextBox ID="txtHandphone" runat="server" BorderStyle="Groove" TabIndex="6" 
                                                    Width="233"></asp:TextBox>
                                            </td>
                                            <td valign="top" colspan="2" rowspan="4">
                                                <table width="100%" style="font-size: x-small">
                                                    <thead>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater ID="attachm" runat="server" OnItemCommand="attachm_Command" OnItemDataBound="attachm_DataBound">
                                                                    <HeaderTemplate>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="EvenRows baris">
                                                                            <td>
                                                                                <%# Eval("FileName") %>
                                                                            </td>
                                                                            <td align="right" width="15%">
                                                                                <asp:ImageButton ToolTip="Click to Preview Document" ID="lihat" runat="server" CommandArgument='<%# Eval("FileName") %>'
                                                                                    CssClass='<%# Eval("ID") %>' CommandName="pre" ImageUrl="~/images/Logo_Download.png" />
                                                                                <asp:ImageButton ToolTip="Click for delete attachment" ID="hapus" runat="server"
                                                                                    CommandArgument='<%# Container.ItemIndex %>' CssClass='<%# Eval("ID") %>' AlternateText='<%# Eval("SupplierID") %>'
                                                                                    CommandName="hps" ImageUrl="~/images/Delete.png" />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </td>
                                            <tr>
                                                <td>
                                                    <span style="font-size: 10pt">&nbsp; Tgl Join </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtJoin" runat="server" BorderStyle="Groove" TabIndex="7" 
                                                        Width="233"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <span style="font-size: 10pt">&nbsp; E-Mail</span>
                                                </td>
                                                <td>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtJoin">
                                                    </cc1:CalendarExtender>
                                                    <asp:TextBox ID="txtEmail" runat="server" BorderStyle="Groove" TabIndex="7" 
                                                        Width="233"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-size: 10pt">&nbsp; No. N P W P</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNPWP" runat="server" AutoPostBack="True" 
                                                        BorderStyle="Groove" Width="233px"></asp:TextBox>
                                                    <%# Eval("SupplierName")%>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                        CompletionInterval="200" CompletionSetCount="10" EnableCaching="true" 
                                                        FirstRowSelected="True" MinimumPrefixLength="1" 
                                                        ServiceMethod="GetNoProdukStock" ServicePath="AutoComplete.asmx" 
                                                        TargetControlID="txtNPWP">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td>
                                                    <span style="font-size: 10pt">&nbsp;Bayar Dengan</span></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMataUang" runat="server" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span style="font-size: 10pt">&nbsp; PKP</span></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPKP" runat="server" AutoPostBack="True" 
                                                        OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="59px">
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:Label ID="Label3" runat="server" Font-Size="XX-Small" 
                                                        Text="Saat input PO akan otomatis dikenakan PPN" Visible="false"></asp:Label></td>
                                                <td>
                                                    &nbsp; Sub Company&nbsp;</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubCompany" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlSubCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Jenis Usaha</span></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtJenisUsaha" runat="server" BorderStyle="Groove" 
                                                        TabIndex="5" Width="233"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp; <asp:Label ID="lblAgen" runat="server" Text="Data Agen" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAgen" runat="server" Width="100%"  Visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Nama Rekening</span></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtNamaRekening" runat="server" BorderStyle="Groove" 
                                                        TabIndex="5" Width="233"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Bank Rekening</span></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtBankRekening" runat="server" BorderStyle="Groove" 
                                                        TabIndex="5" Width="233"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                           <tr>
                                                <td valign="top">
                                                    <span style="font-size: 10pt">&nbsp; Nomor Rekening</span></td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtNomorRekening" runat="server" BorderStyle="Groove" 
                                                        TabIndex="5" Width="233"></asp:TextBox>
                                                </td>
                                           </tr>
                                        </tr>
                                    </tr>
                                </table>
                                <hr />
                                <div class="contentlist" style="height:310px";>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                                        OnRowDataBound="GridView1_RowDataBound" Width="110%">
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <span><%#Container.DataItemIndex + 1%></span>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="true" />
                                            <asp:BoundField DataField="SupplierCode" HeaderText="Kode" />
                                            <asp:BoundField DataField="SupplierName" HeaderText="Nama" />
                                            <asp:BoundField DataField="Alamat" HeaderText="Alamat" />
                                            <asp:BoundField DataField="UP" HeaderText="UP" />
                                            <asp:BoundField DataField="Telepon" HeaderText="Telepon" />
                                            <asp:BoundField DataField="Fax" HeaderText="Fax" />
                                            <asp:BoundField DataField="Handphone" HeaderText="Handphone" />
                                            <asp:BoundField DataField="JoinDate" DataFormatString="{0:d}" HeaderText="Join Date" />
                                            <asp:BoundField DataField="NPWP" HeaderText="NPWP" />
                                            <asp:BoundField DataField="EMail" HeaderText="E-Mail" />
                                            <asp:BoundField DataField="JenisUsaha" HeaderText="JenisUsaha" />
                                            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                            <asp:ButtonField CommandName="Add" Text="Pilih"  />
                                        </Columns>
                                        <RowStyle BackColor="WhiteSmoke" Font-Names="tahoma" Font-Size="XX-Small" />
                                        <HeaderStyle BackColor="RoyalBlue" BorderColor="#404040" BorderStyle="Groove" BorderWidth="2px"
                                            Font-Bold="True" Font-Names="tahoma" Font-Size="XX-Small" ForeColor="Gold" />
                                        <PagerStyle BorderStyle="Solid" />
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                    </asp:GridView>
                                    <table class="tbStandart" style=" font-size:xx-small" width : "110%">
                                        <thead>
                                            <tr class="tbHeader">
                                                <%--<th style="width:4%" class="kotak">No.</th> --%>
                                                <th style="width:5%" class="kotak">Kode</th>
                                                <th style="width:15%" class="kotak">Supplier Name</th>
                                                <th style="width:20%" class="kotak">Alamat</th>
                                                <th style="width:10%" class="kotak">UP</th>
                                                <th style="width:5%" class="kotak">Telepon</th>
                                                <th style="width:5%" class="kotak">Fax</th>
                                                <th style="width:10%" class="kotak">Email</th>
                                                <th style="width:5%" class="kotak">NPWP</th>
                                                <th style="width:15%" class="kotak">Jenis Usaha</th>
                                                <th style="width:15%" class="kotak">Nama Rekening</th>
                                                <th style="width:15%" class="kotak">Bank Rekening</th>
                                                <th style="width:15%" class="kotak">Nomor Rekening</th>
                                                <th style="width:10%; display:none;" class="kotak">KETERANGAN</th>
                                                <th style="width:5%" class="kotak">&nbsp;</th>
                                                  
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="lstSup" runat="server" OnItemDataBound="lstSup_Databound" OnItemCommand="lstSup_Command">
                                                <ItemTemplate>
                                                    <tr class="EvenRows baris" id="lstR" runat="server">
                                                        <%--<td class="kotak tengah"><%# Container.ItemIndex + 1%></td> --%>
                                                        <td class="kotak tengah"  style="width: 5%"><%# Eval("SupplierCode")%></td>
                                                        <td class="kotak"  style="width: 15%"><%# Eval("SupplierName")%></td>
                                                        <td class="kotak"  style="width: 20%"><%# Eval("Alamat")%></td>
                                                        <td class="kotak"  style="width: 10%"><%# Eval("UP")%></td>
                                                        <td  class="kotak"  style="width: 5%"><%# Eval("Telepon")%> - <%# Eval("Handphone")%></td>
                                                        <td class="kotak"  style="width: 5%"><%# Eval("Fax")%></td>
                                                        <td class="kotak"  style="width: 10%"><%# Eval("EMail")%></td>
                                                        <td class="kotak"  style="width: 5%"><%# Eval("NPWP")%></td>
                                                        <td class="kotak"  style="width: 15%"><%# Eval("JenisUsaha")%></td>
                                                        <td class="kotak"  style="width: 15%"><%# Eval("NamaRekening")%></td>
                                                        <td class="kotak"  style="width: 15%"><%# Eval("BankRekening")%></td>
                                                        <td class="kotak"  style="width: 15%"><%# Eval("NomorRekening")%></td>
                                                        <td id="ket" class="kotak"><%# Eval("Keterangan")%></td>
                                                        <td class="kotak tengah">
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/folder.gif" CommandArgument='<%# Eval("ID") %>' CommandName="add" ToolTip="Click for edit" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                       <div style="margin-top: 20px;">
                                        <table style="width: 600px;">
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click">First</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click">Previous</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand" 
								                    OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" 
											                    Text='<%# Eval("PageText") %> ' Width="20px">
										                    </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click">Next</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbLast" runat="server" OnClick="lbLast_Click">Last</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
            </table>
            
                                    <%--lock--%>
                                    <asp:Label ID="lblHidden" runat="server" Text=""></asp:Label>
                                        <cc1:ModalPopupExtender ID="mpePopUp" runat="server"
                                                    TargetControlID="lblHidden"
                                                    PopupControlID="panEdit"
                                                    CancelControlID="btnUpdateClose"
                                                    BackgroundCssClass="modalBackground">
                                        </cc1:ModalPopupExtender>

                                        <asp:Panel ID="panEdit" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                            <table style="table-layout: fixed; height: 100%" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100%; height: 49px" bgcolor="gray">
                                                            <table class="nbTableHeader">
                                                                <tr>
                                                                    <td style="width: 100%">
                                                                        <asp:Label ID="Label1" runat="server" Text="ALASAN LOCK/ UNLOCK" Font-Bold="True" Font-Names="Verdana"
                                                                            Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                                    </td>

                                                                    <td style="width: 37px">
                                                                        <input id="btnUpdateClose" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose_ServerClick" />
                                                                    </td>
                                                                    <td style="width: 75px">
                                                                        <input id="btnUpdateAlasan" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan_ServerClick" />
                                                                    </td>
                                                                    <td style="width: 5px">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="100%" style="width: 100%">
                                                            <div style="overflow: hidden; height: 100%; width: 100%;">
                                                                <table class="tblForm" style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td style="height: 6px; width: 100%;" valign="top">
                                                                            <asp:TextBox ID="txtAlasanCancel" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                        <%--lock--%>



                                <%--unlock--%>
                                <asp:Label ID="lblHidden1" runat="server" Text=""></asp:Label>
                                        <cc1:ModalPopupExtender ID="mpePopUp1" runat="server"
                                                    TargetControlID="lblHidden1"
                                                    PopupControlID="panEdit1"
                                                    CancelControlID="btnUpdateClose"
                                                    BackgroundCssClass="modalBackground">
                                        </cc1:ModalPopupExtender>

                                        <asp:Panel ID="panEdit1" runat="server" CssClass="Popup" align="center" Style="width: 600px;">
                                            <table style="table-layout: fixed; height: 100%" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100%; height: 49px" bgcolor="gray">
                                                            <table class="nbTableHeader">
                                                                <tr>
                                                                    <td style="width: 100%">
                                                                        <asp:Label ID="Label4" runat="server" Text="ALASAN LOCK/ UNLOCK" Font-Bold="True" Font-Names="Verdana"
                                                                            Font-Size="12pt" ForeColor="Yellow"></asp:Label>
                                                                    </td>

                                                                    <td style="width: 37px">
                                                                        <input id="btnUpdateClose1" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Close" onserverclick="btnUpdateClose1_ServerClick" />
                                                                    </td>
                                                                    <td style="width: 75px">
                                                                        <input id="btnUpdateAlasan1" runat="server" style="background-color: white; font-weight: bold; font-size: 11px;" type="button" value="Update" onserverclick="btnUpdateAlasan1_ServerClick" />
                                                                    </td>
                                                                    <td style="width: 5px">&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="100%" style="width: 100%">
                                                            <div style="overflow: hidden; height: 100%; width: 100%;">
                                                                <table class="tblForm" style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td style="height: 6px; width: 100%;" valign="top">
                                                                            <asp:TextBox ID="txtAlasanCancel1" runat="server" Width="100%" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <hr />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                        <%--unlock--%>

        </div>
        
        
        

     </ContentTemplate> 
     <Triggers> <asp:PostBackTrigger ControlID="btnUpload" /></Triggers>
     </asp:UpdatePanel>     
</asp:Content>
