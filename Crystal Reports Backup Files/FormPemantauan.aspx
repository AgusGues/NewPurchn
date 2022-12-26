<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormPemantauan.aspx.cs" Inherits="GRCweb1.Modul.SasaranMutu.FormPemantauan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMasterPage" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
<%--        <div  id="SarMutDiv1" runat="server" visible="true">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None"> 
            <Columns> 
                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-BackColor="#EFEFEF" /> 
                <asp:BoundField HeaderText="VoucherCode" DataField="VoucherCode" ItemStyle-BackColor="#EFEFEF" /> 
                <asp:BoundField HeaderText="VoucherName" DataField="VoucherName" /> 
                <asp:BoundField HeaderText="SignedPerson" DataField="SignedPerson" /> 
                <asp:BoundField HeaderText="PrintMode" DataField="PrintMode" /> 
                <asp:BoundField HeaderText="ChartNo" DataField="ChartNo" /> 
            </Columns> 
            <HeaderStyle CssClass="GridviewScrollHeader" /> 
            <RowStyle CssClass="GridviewScrollItem" /> 
            <PagerStyle CssClass="GridviewScrollPager" /> 
            </asp:GridView> 
        </div>
    
        <div  id="SarMutDiv2" runat="server" visible="true">
            <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None"> 
            <Columns> 
                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-BackColor="#EFEFEF" /> 
                <asp:BoundField HeaderText="VoucherCode" DataField="VoucherCode" ItemStyle-BackColor="#EFEFEF" /> 
                <asp:BoundField HeaderText="VoucherName" DataField="VoucherName" /> 
                <asp:BoundField HeaderText="SignedPerson" DataField="SignedPerson" /> 
                <asp:BoundField HeaderText="PrintMode" DataField="PrintMode" /> 
                <asp:BoundField HeaderText="ChartNo" DataField="ChartNo" /> 
            </Columns> 
            <HeaderStyle CssClass="GridviewScrollHeader1" /> 
            <RowStyle CssClass="GridviewScrollItem1" /> 
            <PagerStyle CssClass="GridviewScrollPager1" /> 
            </asp:GridView> 
        </div>

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 
        <script type="text/javascript" src="../../gridviewScroll.min.js"></script> 
        <link href="../../test1.css" rel="stylesheet" />
        <link href="../../test2.css" rel="stylesheet" />
        <script type="text/javascript"> 
            $(document).ready(function () { 
                gridviewScroll(); 
            }); 
 
            function gridviewScroll() { 
                $('#<%=GridView1.ClientID%>').gridviewScroll({ 
                    width: 400, 
                    height: 200, 
                    freezesize: 2 
                }); 
                $('#<%=GridView2.ClientID%>').gridviewScroll({ 
                    width: 400, 
                    height: 200, 
                    freezesize: 2 
                }); 
            } 
        </script>--%>


    <!-- GridViewScroll with jQuery -->
    <script type="text/javascript" src="../../jquery.min.js"></script>
    <script type="text/javascript" src="../../jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../gridviewScroll.min.js"></script>
    <link href="../../GridviewScroll.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            gridView1 = $('#GridView1a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView2 = $('#GridView2a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView3 = $('#GridView3a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView4 = $('#GridView4a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView5 = $('#GridView5a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView6 = $('#GridView6a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView7 = $('#GridView7a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView8 = $('#GridView8a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView9 = $('#GridView9a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView10 = $('#GridView10a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView11 = $('#GridView11a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView12 = $('#GridView12a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView13 = $('#GridView13a').gridviewScroll({
                //width: 600,
                //width: 1250,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });
            gridView14 = $('#GridView14a').gridviewScroll({
                //width: 600,
                width: 1100,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "/img/arrowvt.png",
                varrowbottomimg: "/img/arrowvb.png",
                harrowleftimg: "/img/arrowhl.png",
                harrowrightimg: "/img/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 8
            });

        }
	</script>
    <!-- GridViewScroll with jQuery -->


		<div class="row">
            <h4 class="lighter">
                <i class="ace-icon fa fa-hand-o-right icon-animated-hand-pointer blue"></i>
                <a class="pink"> Tahun </a>
                <span>
                    <select class="input-small center" id="selectTahun" runat="server">
                        <option value="2017">2017</option>
                        <option value="2018">2018</option>
                        <option value="2019">2019</option>
                        <option value="2020">2020</option>
                        <option value="2021">2021</option>
                        <option value="2022">2022</option>
                        <option value="2023">2023</option>
                        <option value="2024">2024</option>
                        <option value="2025">2025</option>
                    </select>
                </span>
                <a class="pink" id="txtDepo" runat="server" visible="false"> Depo </a>
                <span>
                    <select class="input-medium" id="selectDepo" runat="server" visible="false">
                        <option value="1">Citereup</option>
                        <option value="2">Karawang</option>
                    </select>
                </span>
        </h4>
		</div>


		<div class="row">
			<div class="col-xs-12 col-sm-12">

                <!--Output Produksi-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Output Produksi > Output Produksi</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div1" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp1" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView1a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp1aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp1aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp1aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp1aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp1aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp1aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp1aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp1aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp1aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp1aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp1aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp1aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp1aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp1aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp1aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp1aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp1aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp1aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp1aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp1aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg1" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater2" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView2a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg2aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg2aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg2aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg2aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg2aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg2aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg2aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg2aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg2aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg2aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg2aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg2aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg2aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg2aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg2aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg2aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg2aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg2aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg2aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg2aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>

					</div>
				</div>

                <!--Breakdown Time Produksi-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Output Produksi > Breakdown Time Produksi</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div2" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp2" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater3" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView3a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp3aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp3aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp3aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp3aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp3aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp3aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp3aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp3aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp3aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp3aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp3aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp3aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp3aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp3aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp3aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp3aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp3aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp3aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp3aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp3aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg2" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater4" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView4a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg4aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg4aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg4aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg4aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg4aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg4aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg4aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg4aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg4aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg4aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg4aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg4aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg4aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg4aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg4aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg4aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg4aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg4aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg4aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg4aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>

					</div>
				</div>

                <!--Breakdown Time Maintenance-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Output Produksi > Breakdown Time Maintenance</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div3" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp3" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater5" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView5a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp5aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp5aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp5aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp5aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp5aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp5aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp5aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp5aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp5aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp5aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp5aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp5aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp5aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp5aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp5aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp5aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp5aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp5aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp5aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp5aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg3" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater6" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView6a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg6aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg6aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg6aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg6aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg6aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg6aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg6aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg6aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg6aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg6aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg6aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg6aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg6aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg6aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg6aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg6aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg6aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg6aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg6aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg6aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>

					</div>
				</div>

                <!--Tingkat Produktivitas Delivery-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Tingkat Produktivitas Delivery</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div4" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp4" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater7" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView7a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp7aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp7aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp7aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp7aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp7aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp7aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp7aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp7aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp7aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp7aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp7aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp7aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp7aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp7aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp7aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp7aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp7aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp7aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp7aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp7aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg4" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater8" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView8a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg8aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg8aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg8aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg8aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg8aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg8aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg8aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg8aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg8aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg8aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg8aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg8aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg8aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg8aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg8aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg8aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg8aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg8aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg8aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg8aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Tingkat Ketidak-Hadiran-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Tingkat Ketidak-Hadiran</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div5" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp5" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater9" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView9a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp9aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp9aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp9aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp9aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp9aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp9aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp9aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp9aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp9aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp9aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp9aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp9aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp9aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp9aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp9aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp9aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp9aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp9aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp9aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp9aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg5" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater10" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView10a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg10aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg10aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg10aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg10aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg10aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg10aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg10aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg10aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg10aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg10aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg10aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg10aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg10aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg10aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg10aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg10aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg10aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg10aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg10aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg10aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Output Finishing-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Output Finishing</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div6" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp6" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater11" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView11a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp11aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp11aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp11aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp11aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp11aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp11aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp11aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp11aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp11aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp11aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp11aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp11aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp11aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp11aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp11aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp11aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp11aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp11aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp11aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp11aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg6" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater12" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView12a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg12aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg12aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg12aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg12aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg12aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg12aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg12aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg12aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg12aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg12aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg12aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg12aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg12aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg12aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg12aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg12aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg12aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg12aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg12aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg12aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Output Cutter-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Output Cutter</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div7" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp7" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater13" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView13a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp13aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp13aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp13aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp13aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp13aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp13aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp13aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp13aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp13aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp13aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp13aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp13aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp13aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp13aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp13aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp13aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp13aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp13aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp13aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp13aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg7" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater14" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView14a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg14aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg14aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg14aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg14aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg14aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg14aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg14aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg14aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg14aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg14aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg14aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg14aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg14aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg14aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg14aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg14aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg14aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg14aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg14aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg14aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Output Oven Drying-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Output Oven Drying</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div8" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp8" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater15" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView15a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp15aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp15aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp15aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp15aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp15aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp15aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp15aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp15aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp15aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp15aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp15aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp15aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp15aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp15aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp15aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp15aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp15aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp15aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp15aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp15aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg8" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater16" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView16a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg16aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg16aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg16aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg16aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg16aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg16aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg16aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg16aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg16aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg16aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg16aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg16aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg16aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg16aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg16aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg16aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg16aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg16aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg16aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg16aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Output Pelarian Produk-->
                <div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-success arrowed-right">Produktivitas > Tingkat Produktivitas Delivery > Pelarian Produk</span>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div9" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp9" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater17" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView17a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp17aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp17aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp17aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp17aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp17aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp17aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp17aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp17aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp17aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp17aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp17aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp17aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp17aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp17aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp17aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp17aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp17aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp17aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp17aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp17aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg9" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater18" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView18a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg18aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg18aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg18aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg18aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg18aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg18aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg18aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg18aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg18aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg18aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg18aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg18aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg18aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg18aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg18aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg18aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg18aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg18aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg18aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg18aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>


                <!--2. Pencapaian Produk KW1-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-warning arrowed-right">Quality Product > Pencapaian Produk KW1 > Pencapaian Produk KW1</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div10" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp10" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater19" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView19a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp19aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp19aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp19aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp19aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp19aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp19aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp19aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp19aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp19aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp19aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp19aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp19aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp19aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp19aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp19aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp19aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp19aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp19aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp19aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp19aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg10" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater20" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView20a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg20aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg20aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg20aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg20aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg20aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg20aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg20aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg20aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg20aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg20aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg20aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg20aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg20aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg20aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg20aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg20aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg20aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg20aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg20aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg20aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Defect Board Mill-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-warning arrowed-right">Quality Product > Pencapaian Produk KW1 > Defect Board Mill</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div11" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp11" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater21" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView21a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp21aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp21aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp21aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp21aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp21aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp21aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp21aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp21aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp21aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp21aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp21aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp21aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp21aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp21aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp21aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp21aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp21aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp21aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp21aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp21aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg11" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater22" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView22a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg22aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg22aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg22aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg22aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg22aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg22aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg22aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg22aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg22aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg22aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg22aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg22aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg22aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg22aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg22aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg22aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg22aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg22aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg22aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg22aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Utilisasi dari produk BP-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-warning arrowed-right">Quality Product > Pencapaian Produk KW1 > Utilisasi dari Produk BP</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div12" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp12" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater23" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView23a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp23aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp23aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp23aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp23aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp23aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp23aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp23aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp23aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp23aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp23aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp23aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp23aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp23aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp23aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp23aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp23aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp23aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp23aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp23aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp23aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg12" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater24" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView24a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg24aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg24aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg24aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg24aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg24aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg24aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg24aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg24aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg24aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg24aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg24aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg24aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg24aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg24aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg24aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg24aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg24aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg24aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg24aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg24aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Penurunan Reject Produk-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-warning arrowed-right">Quality Product > Pencapaian Produk KW1 > Penurunan Reject Produk</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div13" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp13" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater25" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView25a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp25aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp25aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp25aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp25aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp25aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp25aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp25aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp25aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp25aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp25aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp25aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp25aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp25aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp25aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp25aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp25aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp25aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp25aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp25aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp25aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg13" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater26" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView26a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg26aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg26aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg26aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg26aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg26aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg26aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg26aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg26aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg26aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg26aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg26aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg26aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg26aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg26aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg26aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg26aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg26aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg26aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg26aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg26aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Tingkat Kepuasan Pelanggan-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-purple arrowed-right">Kepuasan Pelanggan > Tingkat Kepuasan Pelanggan > Tingkat Kepuasan Pelanggan</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div14" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp14" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater27" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView27a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp27aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp27aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp27aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp27aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp27aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp27aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp27aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp27aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp27aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp27aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp27aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp27aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp27aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp27aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp27aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp27aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp27aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp27aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp27aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp27aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg14" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater28" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView28a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg28aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg28aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg28aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg28aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg28aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg28aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg28aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg28aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg28aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg28aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg28aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg28aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg28aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg28aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg28aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg28aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg28aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg28aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg28aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg28aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Kecepatan Waktu Loading-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-purple arrowed-right">Kepuasan Pelanggan > Tingkat Kepuasan Pelanggan > Kecepatan Waktu Loading</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div15" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp15" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater29" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView29a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp29aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp29aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp29aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp29aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp29aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp29aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp29aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp29aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp29aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp29aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp29aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp29aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp29aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp29aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp29aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp29aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp29aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp29aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp29aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp29aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg15" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater30" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView30a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg30aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg30aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg30aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg30aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg30aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg30aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg30aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg30aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg30aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg30aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg30aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg30aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg30aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg30aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg30aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg30aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg30aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg30aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg30aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg30aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Customer Complaint terhadap Mutu Produk -->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-purple arrowed-right">Kepuasan Pelanggan > Tingkat Kepuasan Pelanggan > Customer Complaint terhadap Mutu Produk</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div16" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp16" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater31" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView31a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp31aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp31aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp31aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp31aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp31aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp31aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp31aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp31aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp31aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp31aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp31aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp31aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp31aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp31aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp31aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp31aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp31aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp31aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp31aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp31aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg16" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater32" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView32a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg32aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg32aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg32aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg32aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg32aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg32aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg32aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg32aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg32aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg32aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg32aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg32aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg32aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg32aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg32aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg32aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg32aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg32aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg32aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg32aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>


                <!-- Pencapaian Efektivitas Sumber Daya-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-pink arrowed-right">Efektivitas > Pencapaian Efektivitas Sumber Daya > Pencapaian Efektivitas Sumber Daya</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div17" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp17" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater33" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView33a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp33aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp33aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp33aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp33aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp33aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp33aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp33aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp33aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp33aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp33aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp33aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp33aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp33aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp33aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp33aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp33aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp33aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp33aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp33aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp33aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg17" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater34" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView34a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg34aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg34aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg34aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg34aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg34aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg34aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg34aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg34aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg34aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg34aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg34aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg34aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg34aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg34aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg34aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg34aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg34aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg34aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg34aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg34aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!-- Perbandingan Harga Kompetitif-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-pink arrowed-right">Efektivitas > Pencapaian Efektivitas Sumber Daya > Perbandingan Harga Kompetitif</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div18" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp18" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater35" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView35a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp35aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp35aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp35aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp35aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp35aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp35aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp35aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp35aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp35aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp35aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp35aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp35aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp35aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp35aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp35aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp35aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp35aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp35aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp35aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp35aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg18" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater36" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView36a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg36aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg36aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg36aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg36aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg36aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg36aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg36aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg36aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg36aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg36aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg36aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg36aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg36aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg36aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg36aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg36aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg36aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg36aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg36aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg36aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!-- Pemasukan Pulp Lokal-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-pink arrowed-right">Efektivitas > Pencapaian Efektivitas Sumber Daya > Pemasukan Pulp Lokal</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div19" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp19" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater37" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView37a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp37aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp37aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp37aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp37aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp37aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp37aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp37aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp37aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp37aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp37aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp37aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp37aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp37aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp37aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp37aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp37aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp37aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp37aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp37aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp37aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg19" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater38" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView38a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg38aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg38aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg38aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg38aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg38aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg38aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg38aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg38aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg38aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg38aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg38aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg38aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg38aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg38aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg38aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg38aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg38aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg38aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg38aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg38aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!-- Overtime Karyawan-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-pink arrowed-right">Efektivitas > Pencapaian Efektivitas Sumber Daya > Overtime Karyawan</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div20" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp20" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater39" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView39a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp39aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp39aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp39aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp39aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp39aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp39aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp39aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp39aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp39aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp39aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp39aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp39aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp39aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp39aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp39aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp39aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp39aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp39aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp39aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp39aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg20" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater40" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView40a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg40aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg40aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg40aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg40aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg40aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg40aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg40aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg40aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg40aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg40aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg40aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg40aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg40aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg40aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg40aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg40aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg40aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg40aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg40aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg40aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Efesiensi Perawatan Mesin-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-pink arrowed-right">Efektivitas > Pencapaian Efektivitas Sumber Daya > Efesiensi Perawatan Mesin</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div21" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp21" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater41" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView41a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp41aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp41aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp41aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp41aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp41aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp41aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp41aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp41aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp41aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp41aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp41aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp41aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp41aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp41aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp41aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp41aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp41aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp41aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp41aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp41aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg21" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater42" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView42a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg42aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg42aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg42aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg42aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg42aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg42aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg42aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg42aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg42aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg42aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg42aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg42aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg42aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg42aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg42aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg42aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg42aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg42aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg42aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg42aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>


                <!--Tingkat Kompentensi Karyawan-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-yellow arrowed-right">SDM yang Handal > Tingkat Kompentensi Karyawan > Tingkat Kompentensi Karyawan</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div22" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp22" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater43" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView43a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp43aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp43aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp43aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp43aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp43aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp43aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp43aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp43aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp43aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp43aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp43aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp43aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp43aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp43aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp43aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp43aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp43aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp43aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp43aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp43aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg22" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater44" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView44a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg44aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg44aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg44aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg44aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg44aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg44aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg44aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg44aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg44aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg44aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg44aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg44aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg44aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg44aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg44aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg44aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg44aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg44aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg44aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg44aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>

                <!--Pengembangan Produk/Proses-->
				<div class="timeline-item clearfix no-padding-bottom no-margin-bottom">
					<div class="widget-box transparent no-margin-left no-padding-bottom no-margin-bottom">
						<div class="widget-header widget-header-small no-padding-bottom no-margin-bottom">
							<h5 class="widget-title smaller no-margin-left no-padding-left">
                                <span class="label label-inverse arrowed-right">Development > Pengembangan Produk/Proses > Pengembangan Produk/Proses</span>
								<%--<span class="grey">Plant Citereup</span>--%>
							</h5>
							<span class="widget-toolbar">
								<a href="#" data-action="reload">
									<i class="ace-icon fa fa-refresh"></i>
								</a>
								<a href="#" data-action="collapse">
									<i class="ace-icon fa fa-chevron-up"></i>
								</a>
							    <a href="#" data-action="close">
								    <i class="ace-icon fa fa-times"></i>
							    </a>
							</span>
						</div>

						<div id="div23" runat="server" class="widget-body no-padding-bottom no-margin-bottom">
							<div class="widget-main">

                                <div id="DivCtrp23" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater45" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView45a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="blue center">Plant Citereup</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2"  class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblCtrpCol1" runat="server" Text='<%# Eval("Ctrp45aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Ctrp45aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Ctrp45aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Ctrp45aCol4") %>' ></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Ctrp45aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Ctrp45aCol6") %>'></asp:Label></td>
                                            <td><a href="../../About.aspx"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Ctrp45aCol7") %>'></asp:Label></a></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Ctrp45aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Ctrp45aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Ctrp45aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Ctrp45aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Ctrp45aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Ctrp45aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Ctrp45aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Ctrp45aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Ctrp45aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Ctrp45aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Ctrp45aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Ctrp45aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Ctrp45aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

                                <div id="DivKrwg23" runat="server" visible="true">
                                    <asp:Repeater ID="Repeater46" runat="server">
                                    <HeaderTemplate>

    	                            <table id="GridView46a" style="width:100%;border-collapse:collapse;font-family:·L³n¥¿¶ÂÅé, Tahoma, Arial, Verdana;font-weight: normal;font-size: 12px;color: #333333;">
		                                <tr class="GridviewScrollHeader">
			                                <td colspan="3" class="purple center">Plant Karawang</td>
                                            <td rowspan="2" class="red">SmtI-LY</td>
                                            <td rowspan="2" class="red">SmtII-LY</td>
                                            <%--<td colspan="2">Package</td>--%>
                                            <td rowspan="2">Tahun</td>
                                            <td rowspan="2">Jan</td>
                                            <td rowspan="2">Feb</td>
                                            <td rowspan="2">Mar</td>
                                            <td rowspan="2">Apr</td>
                                            <td rowspan="2">Mei</td>
                                            <td rowspan="2">Jun</td>
                                            <td rowspan="2" class="red">Smt-I</td>
                                            <td rowspan="2">Jul</td>
                                            <td rowspan="2">Agu</td>
                                            <td rowspan="2">Sep</td>
                                            <td rowspan="2">Okt</td>
                                            <td rowspan="2">Nov</td>
                                            <td rowspan="2">Des</td>
                                            <td rowspan="2" class="red" id="tdCol19" runat="server" visible="true">Smt-II</td>
		                                </tr>
                                        <tr class="GridviewScrollHeader">
			                                <td>Dept</td>
                                            <td>SarMut</td>
                                            <td>Parameter Terukur</td>

                                            <%--<td>Weight</td>
                                            <td>Size</td>--%>
		                                </tr>
                                        
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <tr class="GridviewScrollItem">
			                                <td style="background-color:#EFEFEF;"><asp:Label ID="lblKrwgCol1" runat="server" Text='<%# Eval("Krwg46aCol1") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label1" runat="server" Text='<%# Eval("Krwg46aCol2") %>'></asp:Label></td>
                                            <td style="background-color:#EFEFEF;"><asp:Label ID="Label2" runat="server" Text='<%# Eval("Krwg46aCol3") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Krwg46aCol4") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Krwg46aCol5") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Krwg46aCol6") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Krwg46aCol7") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Krwg46aCol8") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("Krwg46aCol9") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("Krwg46aCol10") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("Krwg46aCol11") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("Krwg46aCol12") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("Krwg46aCol13") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label13" runat="server" Text='<%# Eval("Krwg46aCol14") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label14" runat="server" Text='<%# Eval("Krwg46aCol15") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label15" runat="server" Text='<%# Eval("Krwg46aCol16") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label16" runat="server" Text='<%# Eval("Krwg46aCol17") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label17" runat="server" Text='<%# Eval("Krwg46aCol18") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label18" runat="server" Visible="true" Text='<%# Eval("Krwg46aCol19") %>'></asp:Label></td>
                                            <td><asp:Label ID="Label19" runat="server" Visible="true" Text='<%# Eval("Krwg46aCol20") %>'></asp:Label></td>
		                                </tr>

                                        </ItemTemplate>
                                        <FooterTemplate>

	                                </table>
                                        </FooterTemplate>
                                        </asp:Repeater>

                                </div>

							</div>
						</div>
					</div>
				</div>



            </div>
        </div>


    </div>









    
</asp:Content>
