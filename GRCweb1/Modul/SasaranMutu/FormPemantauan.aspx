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


            gridView15 = $('#GridView15a').gridviewScroll({
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
            gridView16 = $('#GridView16a').gridviewScroll({
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
            gridView17 = $('#GridView17a').gridviewScroll({
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
            gridView18 = $('#GridView18a').gridviewScroll({
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
            gridView19 = $('#GridView19a').gridviewScroll({
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
            gridView20 = $('#GridView20a').gridviewScroll({
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
            gridView21 = $('#GridView21a').gridviewScroll({
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
            gridView22 = $('#GridView22a').gridviewScroll({
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
            gridView23 = $('#GridView23a').gridviewScroll({
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
            gridView24 = $('#GridView24a').gridviewScroll({
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
            gridView25 = $('#GridView25a').gridviewScroll({
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
            gridView26 = $('#GridView26a').gridviewScroll({
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
            gridView27 = $('#GridView27a').gridviewScroll({
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
            gridView28 = $('#GridView28a').gridviewScroll({
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
            gridView29 = $('#GridView29a').gridviewScroll({
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
            gridView30 = $('#GridView30a').gridviewScroll({
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
            gridView31 = $('#GridView31a').gridviewScroll({
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
            gridView32 = $('#GridView32a').gridviewScroll({
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
            gridView33 = $('#GridView33a').gridviewScroll({
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
            gridView34 = $('#GridView34a').gridviewScroll({
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
            gridView35 = $('#GridView35a').gridviewScroll({
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
            gridView36 = $('#GridView36a').gridviewScroll({
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
            gridView37 = $('#GridView37a').gridviewScroll({
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
            gridView38 = $('#GridView38a').gridviewScroll({
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
            gridView39 = $('#GridView39a').gridviewScroll({
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
            gridView40 = $('#GridView40a').gridviewScroll({
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
            gridView41 = $('#GridView41a').gridviewScroll({
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
            gridView42 = $('#GridView42a').gridviewScroll({
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
            gridView43 = $('#GridView43a').gridviewScroll({
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
            gridView44 = $('#GridView44a').gridviewScroll({
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
            gridView45 = $('#GridView45a').gridviewScroll({
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
            gridView46 = $('#GridView46a').gridviewScroll({
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
            gridView47 = $('#GridView47a').gridviewScroll({
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

            gridView48 = $('#GridView48a').gridviewScroll({
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
            gridView53 = $('#GridView53a').gridviewScroll({
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
                <a class="pink"> &nbsp; </a>
                <a class="pink"> Tahun </a>
                <a class="pink"> &nbsp; </a>
<%--                <span>
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
                </span>--%>
                <span>
                    <asp:DropDownList class="chosen-select" id="selectTahun" name="Select1Name" runat="server" data-placeholder="Choose Voucher" OnSelectedIndexChanged="selectTahun_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem>2017</asp:ListItem>
                        <asp:ListItem>2018</asp:ListItem>
                        <asp:ListItem>2019</asp:ListItem>
                        <asp:ListItem>2020</asp:ListItem>
                        <asp:ListItem>2021</asp:ListItem>
                        <asp:ListItem>2022</asp:ListItem>
                        <asp:ListItem>2023</asp:ListItem>
                        <asp:ListItem>2024</asp:ListItem>
                        <asp:ListItem>2025</asp:ListItem>
                    </asp:DropDownList>
                </span>

                <a class="pink"> &nbsp;&nbsp; </a>
                <i class="ace-icon fa fa-hand-o-right icon-animated-hand-pointer blue"></i>
                <a class="pink"> &nbsp; </a>
                <a class="pink"> View Plant </a>
                <a class="pink"> &nbsp; </a>
                <input type="checkbox" class="ace" /><span class="lbl"> Citereup</span>
                <a class="pink"> &nbsp; </a>
                <input type="checkbox" class="ace" /><span class="lbl"> Karawang</span>

                <a class="pink" id="txtDepo" runat="server" visible="false"> Depo </a>
                <span>
                    <select class="input-medium" id="selectDepo" runat="server" visible="false">
                        <option value="1">Citereup</option>
                        <option value="2">Karawang</option>
                    </select>
                </span>
        </h4>
		</div>


        <!-- cut here -->



        <!-- display label from behind code -->
        <div class="row">
            <asp:Label ID="LblDisplaySarMut" runat="server"
>           </asp:Label>
        </div>



   </div>
</asp:Content>
