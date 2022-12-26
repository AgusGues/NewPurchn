using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Factory;
using Cogs;
using System.Web.Services;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using DefectFacade;
using System.IO;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LEffesiensiBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                getYear();
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");

            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 30 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"));
            Link2Sarmut();
            //LoadDrop();
        }



        //private void LoadDrop()
        //{

        //    string strSQL =
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_sarmut1]') AND type in (N'U')) DROP TABLE [dbo].tmp_sarmut1 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual_all " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_all " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L1]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L1 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L2]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L2 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L3]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L3 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L4]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L4 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L5]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L5 " +
        //    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L6]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L6 ";


        //    try
        //    {
        //        SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //        sqlCon.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
        //        da.SelectCommand.CommandTimeout = 0;
        //    }
        //    catch { }
        //}


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid(string tgl1)
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
            {
                return;
            }

            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string tglproses = string.Empty;
            string sqlselect = string.Empty;
            string sqlinpivot = string.Empty;

            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string strSQL =
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProposionate]') AND type in (N'U')) DROP TABLE [dbo].[tblProposionate] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiAll]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiAll] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiPerShift] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiNonPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiNonPerShift] " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksi]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksi " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPvt]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPvt " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff]') AND type in (N'U')) DROP TABLE [dbo].tmpeff " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff1]') AND type in (N'U')) DROP TABLE [dbo].tmpeff1 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff2]') AND type in (N'U')) DROP TABLE [dbo].tmpeff2 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff3]') AND type in (N'U')) DROP TABLE [dbo].tmpeff3 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff4]') AND type in (N'U')) DROP TABLE [dbo].tmpeff4 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff5]') AND type in (N'U')) DROP TABLE [dbo].tmpeff5 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff6]') AND type in (N'U')) DROP TABLE [dbo].tmpeff6 " +

            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_sarmut1]') AND type in (N'U')) DROP TABLE [dbo].tmp_sarmut1 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual_all " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_all " +

            "declare @line int " +
            "declare @thnbln char(6) " +
            "declare @unitkerja int " +
            "declare @Pressing char(4) " +
            "declare @NonPressing char(4) " +

            "set @thnbln='" + thnbln + "' " +
            "set @line=" + ddlLine.SelectedValue +
            "set @unitkerja=" + users.UnitKerjaID +
            "set @Pressing='1375' " +
            "set @NonPressing='1250' " +
            " " +

            "select Tanggal,prodline,case when Total>0 then (Shf1/total*100) else 100  end Pros1, " +
            "case when Total>0 then (Shf2/total*100) else 0  end Pros2,case when Total>0 then (Shf3/total*100) else 0  end Pros3 into tblProposionate from ( " +
            "select Tanggal,prodline,sum(Shf1)Shf1,sum(Shf2)Shf2,sum(Shf3)Shf3 ,sum(Shf1+Shf2+Shf3) Total from ( " +
            "select A.pakaidate Tanggal,B.prodline,sum(B.Quantity) Shf1,0 Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and I.ItemName like 'semen%' and B.Keterangan like '%shift 1%' group by A.pakaidate,prodline " +
            "union all " +
            "select A.pakaidate Tanggal,B.prodline,0 Shf1,sum(B.Quantity) Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and I.ItemName like 'semen%' and B.Keterangan like '%shift 2%' group by A.pakaidate,prodline " +
            "union all " +
            "select A.pakaidate Tanggal,B.prodline,0 Shf1,0 Shf2,sum(B.Quantity) Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and I.ItemName like 'semen%' and B.Keterangan like '%shift 3%' group by A.pakaidate,prodline " +
            "union all " +
            "select distinct A.pakaidate Tanggal,B.prodline,0 Shf1,0 Shf2,0 Shf3 from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 " +
            ")A " +
            "group by Tanggal,prodline)B " +
            " " +
            "/* " +
            "select prodline,Tanggal,Total into tblPakaiAll from ( " +
            "select prodline,Tanggal,sum(Shf1) Total from ( " +
            "select B.prodline,convert(char,A.pakaidate,112) Tanggal, sum(B.Quantity) Shf1,0 Shf2,0 Shf3 from PakaiDetail B " +
            "inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 group by prodline,A.pakaidate " +
            ")A group by prodline,Tanggal " +
            ")B where prodline >0 " +
            "*/ " +
            "select  Tanggal,prodline,Qty into tblPakaiNonPerShift from ( " +
            "select Tanggal,prodline,sum(Qty)Qty from ( " +
            "select B.prodline,convert(char,A.pakaidate,112) Tanggal, sum(B.Quantity) Qty from PakaiDetail B  " +
            "inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  and B.Keterangan Not like '%shift%' group by prodline,A.pakaidate " +
            ")A group by prodline,Tanggal " +
            ")B " +
            " " +
            "select  Tanggal,prodline,shif, case shif when 1 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* " +
            "isnull((select Pros1 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  " +
            "when 2 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* " +
            "isnull((select Pros2 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  " +
            "when 3 then Qty+(isnull((select qty from tblPakaiNonPerShift where tanggal=B.tanggal and prodline=B.prodline),0)* " +
            "isnull((select Pros3 from tblProposionate where prodline =B.prodline and Tanggal=B.Tanggal  ),0))/100  " +
            "end Qty into tblPakaiPerShift from ( " +
            "select Tanggal,prodline,shif,sum(Qty)Qty from ( " +
            "select B.prodline,convert(char,A.pakaidate,112) Tanggal,1 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  and B.Keterangan like '%shift 1%' group by prodline,A.pakaidate " +
            "union all " +
            "select B.prodline,convert(char,A.pakaidate,112) Tanggal,2 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and B.Keterangan like '%shift 2%' group by prodline,A.pakaidate " +
            "union all " +
            "select B.prodline,convert(char,A.pakaidate,112) Tanggal,3 shif, sum(B.Quantity) Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1 and B.Keterangan like '%shift 3%' group by prodline,A.pakaidate " +
            "union all " +
            "select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,1 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  " +
            "union all " +
            "select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,2 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  " +
            "union all " +
            "select distinct B.prodline,convert(char,A.pakaidate,112) Tanggal,3 shif, 0 Qty from PakaiDetail B inner join pakai A on A.id=B.PakaiID  inner join inventory I on B.ItemID = I.ID  " +
            "where B.RowStatus>-1 and left(convert(char,A.pakaidate,112),6)=@thnbln  and I.GroupID=1  " +
            ")A group by prodline,Tanggal,shif " +
            ")B  " +
            " " +
            "select * into OuputProduksi from ( " +
            "select convert(char,A.tglproduksi,112)tanggal,right(rtrim(P.PlantName),1) Line,A.[shift] shif,G.[Group],sum(A.Qty) Qty,case when isnull(I.Pressing,'Yes')='Yes' then 'P' else 'NP' end  P, " +
            "rtrim(cast(cast(cast(substring(I.Partno,7,3) as int)/10 as decimal(8,1)) as char)) +'X'+ substring(I.Partno,10,4)+'X'+substring(I.Partno,14,4)+ +' '  " +
            "+case when isnull(I.Pressing,'Yes')='Yes' then 'P' else 'NP' end  partno,I.tebal,I.Lebar,I.Panjang  " +
            "from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID  " +
            "where A.Qty>0 and A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and right(rtrim(P.PlantName),1)=@line group by P.PlantName, A.TglProduksi,A.[shift],G.[Group],I.Partno,I.tebal, " +
            "I.Lebar,I.Panjang,I.Pressing    " +
            "union all " +
            "select tanggal, prodline Line,shif,'-' [Group],0 Qty,'P' P,'-' Partno,1 Tebal,1 Lebar,1 Panjang  " +
            "from tblPakaiPerShift where prodline=@line and rtrim(cast(tanggal as char))+cast(shif as char) not in  " +
            "(select distinct rtrim(cast(convert(char,tglproduksi,112) as char))+cast(shift as char) from bm_destacking  " +
            "where convert(char,TglProduksi,112)=tblPakaiPerShift.tanggal and plantid=@line) " +
            ")Z order by tanggal, shif " +
            " " +
            "DECLARE @cols AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX); " +
            "select @cols = STUFF((SELECT distinct ',' + QUOTENAME(c.Partno)  " +
            "            FROM OuputProduksi c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
            "select @sumcols = STUFF((SELECT distinct '),sum(' + QUOTENAME(c.Partno) FROM OuputProduksi c  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  " +
            " " +
            "set @query = 'SELECT  tebal,Tanggal, Line, shif,[group],P, ' + @cols + ' into OuputProduksiPvt from  " +
            "            (select tebal,Tanggal, Line, shif,[group], " +
            "p,partno,qty from OuputProduksi) x " +
            "            pivot  " +
            "            ( " +
            "               sum(qty) for partno in (' + @cols + ') " +
            "            ) p ' " +
            "execute(@query) " +
            "declare @K char " +
            "declare @V varchar(max) " +
            "declare @M3 varchar(max) " +
            "declare @TM3 varchar(max) " +
            "declare @TM31 varchar(max) " +
            "declare @TM32 varchar(max) " +
            "declare @ColEnd varchar(max) " +
            "declare @sumColEnd varchar(max) " +
            "set @V='select sum(((tebal*lebar*panjang)/1000000000)*qty' " +
            "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerja) " +
            " " +
            "if @line=1  " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'A'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'A'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'A<=6],  " +
            "case when B.[Group]=''' + @K + 'A'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'A'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'A>6], " +
            "case when B.[Group]=''' + @K + 'B'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'B'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'B<=6],  " +
            "case when B.[Group]=''' + @K + 'B'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'B'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'B>6], " +
            "case when B.[Group]=''' + @K + 'C'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'C'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'C<=6],  " +
            "case when B.[Group]=''' + @K + 'C'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'C'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'C>6], " +
            "case when B.[Group]=''' + @K + 'D'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'D'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'D<=6],  " +
            "case when B.[Group]=''' + @K + 'D'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'D'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'D>6]' " +
            "set @TM31=',[' + @K + 'A<=6] + [' + @K + 'B<=6] + [' + @K + 'C<=6] + [' + @K + 'D<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'A>6] + [' + @K + 'B>6] + [' + @K + 'C>6] + [' + @K + 'D>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'A<=6] + [' + @K + 'B<=6] + [' + @K + 'C<=6] + [' + @K + 'D<=6] + [' + @K + 'A>6] + [' + @K + 'B>6] + [' + @K + 'C>6] + [' + @K + 'D>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'A<=6],[' + @K + 'A>6],[' + @K + 'B<=6],[' + @K + 'B>6],[' + @K + 'C<=6],[' + @K + 'C>6],[' + @K + 'D<=6],[' + @K + 'D>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'A<=6]),sum([' + @K + 'A>6]),sum([' + @K + 'B<=6]),sum([' + @K + 'B>6]),sum([' + @K + 'C<=6]),sum([' + @K + 'C>6]),sum([' + @K + 'D<=6]),sum([' + @K + 'D>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +
            "if @line=2 " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'E'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'E'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif )else 0 end [' + @K + 'E<=6],  " +
            "case when B.[Group]=''' + @K + 'E'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'E'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'E>6], " +
            "case when B.[Group]=''' + @K + 'F'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'F'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'F<=6],  " +
            "case when B.[Group]=''' + @K + 'F'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'F'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'F>6], " +
            "case when B.[Group]=''' + @K + 'G'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'G'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'G<=6],  " +
            "case when B.[Group]=''' + @K + 'G'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'G'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'G>6], " +
            "case when B.[Group]=''' + @K + 'H'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'H'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'H<=6],  " +
            "case when B.[Group]=''' + @K + 'H'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'H'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'H>6]' " +
            "set @TM31=',[' + @K + 'E<=6] + [' + @K + 'F<=6] + [' + @K + 'G<=6] + [' + @K + 'H<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'E>6] + [' + @K + 'F>6] + [' + @K + 'G>6] + [' + @K + 'H>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'E<=6] + [' + @K + 'F<=6] + [' + @K + 'G<=6] + [' + @K + 'H<=6] + [' + @K + 'E>6] + [' + @K + 'F>6] + [' + @K + 'G>6] + [' + @K + 'H>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'E<=6],[' + @K + 'E>6],[' + @K + 'F<=6],[' + @K + 'F>6],[' + @K + 'G<=6],[' + @K + 'G>6],[' + @K + 'H<=6],[' + @K + 'H>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'E<=6]),sum([' + @K + 'E>6]),sum([' + @K + 'F<=6]),sum([' + @K + 'F>6]),sum([' + @K + 'G<=6]),sum([' + @K + 'G>6]),sum([' + @K + 'H<=6]),sum([' + @K + 'H>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +
            "if @line=3 " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'I'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'I'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'I<=6],  " +
            "case when B.[Group]=''' + @K + 'I'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'I'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'I>6], " +
            "case when B.[Group]=''' + @K + 'J'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'J'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'J<=6],  " +
            "case when B.[Group]=''' + @K + 'J'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'J'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'J>6], " +
            "case when B.[Group]=''' + @K + 'K'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'K'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'K<=6],  " +
            "case when B.[Group]=''' + @K + 'K'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'K'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'K>6], " +
            "case when B.[Group]=''' + @K + 'L'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'L'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'L<=6],  " +
            "case when B.[Group]=''' + @K + 'L'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'L'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'L>6]' " +
            "set @TM31=',[' + @K + 'I<=6] + [' + @K + 'J<=6] + [' + @K + 'K<=6] + [' + @K + 'L<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'I>6] + [' + @K + 'J>6] + [' + @K + 'K>6] + [' + @K + 'L>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'I<=6] + [' + @K + 'J<=6] + [' + @K + 'K<=6] + [' + @K + 'L<=6] + [' + @K + 'I>6] + [' + @K + 'J>6] + [' + @K + 'K>6] + [' + @K + 'L>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'I<=6],[' + @K + 'I>6],[' + @K + 'J<=6],[' + @K + 'J>6],[' + @K + 'K<=6],[' + @K + 'K>6],[' + @K + 'L<=6],[' + @K + 'L>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'I<=6]),sum([' + @K + 'I>6]),sum([' + @K + 'J<=6]),sum([' + @K + 'J>6]),sum([' + @K + 'K<=6]),sum([' + @K + 'K>6]),sum([' + @K + 'L<=6]),sum([' + @K + 'L>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +
            "if @line=4 " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'M'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'M'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'M<=6],  " +
            "case when B.[Group]=''' + @K + 'M'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'M'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'M>6], " +
            "case when B.[Group]=''' + @K + 'N'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'N'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'N<=6],  " +
            "case when B.[Group]=''' + @K + 'N'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'N'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'N>6], " +
            "case when B.[Group]=''' + @K + 'O'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'O'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'O<=6],  " +
            "case when B.[Group]=''' + @K + 'O'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'O'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'O>6], " +
            "case when B.[Group]=''' + @K + 'P'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'P'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'P<=6],  " +
            "case when B.[Group]=''' + @K + 'P'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'P'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'P>6]' " +
            "set @TM31=',[' + @K + 'M<=6] + [' + @K + 'N<=6] + [' + @K + 'O<=6] + [' + @K + 'P<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'M>6] + [' + @K + 'N>6] + [' + @K + 'O>6] + [' + @K + 'P>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'M<=6] + [' + @K + 'N<=6] + [' + @K + 'O<=6] + [' + @K + 'P<=6] + [' + @K + 'M>6] + [' + @K + 'N>6] + [' + @K + 'O>6] + [' + @K + 'P>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'M<=6],[' + @K + 'M>6],[' + @K + 'N<=6],[' + @K + 'N>6],[' + @K + 'O<=6],[' + @K + 'O>6],[' + @K + 'P<=6],[' + @K + 'P>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'M<=6]),sum([' + @K + 'M>6]),sum([' + @K + 'N<=6]),sum([' + @K + 'N>6]),sum([' + @K + 'O<=6]),sum([' + @K + 'O>6]),sum([' + @K + 'P<=6]),sum([' + @K + 'P>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +
            "if @line=5 " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'Q'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'Q'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'Q<=6],  " +
            "case when B.[Group]=''' + @K + 'Q'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'Q'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'Q>6], " +
            "case when B.[Group]=''' + @K + 'R'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'R'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'R<=6],  " +
            "case when B.[Group]=''' + @K + 'R'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'R'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'R>6], " +
            "case when B.[Group]=''' + @K + 'S'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'S'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'S<=6],  " +
            "case when B.[Group]=''' + @K + 'S'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'S'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'S>6], " +
            "case when B.[Group]=''' + @K + 'T'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'T'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'T<=6],  " +
            "case when B.[Group]=''' + @K + 'T'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'T'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'T>6]' " +
            "set @TM31=',[' + @K + 'Q<=6] + [' + @K + 'R<=6] + [' + @K + 'S<=6] + [' + @K + 'T<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'Q>6] + [' + @K + 'R>6] + [' + @K + 'S>6] + [' + @K + 'T>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'Q<=6] + [' + @K + 'R<=6] + [' + @K + 'S<=6] + [' + @K + 'T<=6] + [' + @K + 'Q>6] + [' + @K + 'R>6] + [' + @K + 'S>6] + [' + @K + 'T>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'Q<=6],[' + @K + 'Q>6],[' + @K + 'R<=6],[' + @K + 'R>6],[' + @K + 'S<=6],[' + @K + 'S>6],[' + @K + 'T<=6],[' + @K + 'T>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'Q<=6]),sum([' + @K + 'Q>6]),sum([' + @K + 'R<=6]),sum([' + @K + 'R>6]),sum([' + @K + 'S<=6]),sum([' + @K + 'S>6]),sum([' + @K + 'T<=6]),sum([' + @K + 'T>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +
            "if @line=6 " +
            "begin " +
            "set @M3='case when B.[Group]=''' + @K + 'U'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'U'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'U<=6],  " +
            "case when B.[Group]=''' + @K + 'U'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'U'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'U>6], " +
            "case when B.[Group]=''' + @K + 'V'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'V'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'V<=6],  " +
            "case when B.[Group]=''' + @K + 'V'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'V'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'V>6], " +
            "case when B.[Group]=''' + @K + 'W'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'W'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'W<=6],  " +
            "case when B.[Group]=''' + @K + 'W'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'W'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'W>6], " +
            "case when B.[Group]=''' + @K + 'X'' and B.tebal<=6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'X'' and  tanggal=A.tanggal and tebal=B.tebal and tebal <=6 and P=B.P and shif=B.shif)else 0 end [' + @K + 'X<=6],  " +
            "case when B.[Group]=''' + @K + 'X'' and B.tebal>6 then (' + @V + ') from OuputProduksi where [group]=''' + @K + 'X'' and  tanggal=A.tanggal and tebal=B.tebal and tebal >6  and P=B.P and shif=B.shif)else 0 end [' + @K + 'X>6]' " +
            "set @TM31=',[' + @K + 'U<=6] + [' + @K + 'V<=6] + [' + @K + 'W<=6] + [' + @K + 'X<=6] [Tot_<=6]' " +
            "set @TM32=',[' + @K + 'U>6] + [' + @K + 'V>6] + [' + @K + 'W>6] + [' + @K + 'X>6] [Tot_>6]' " +
            "set @TM3=',[' + @K + 'U<=6] + [' + @K + 'V<=6] + [' + @K + 'W<=6] + [' + @K + 'X<=6] + [' + @K + 'U>6] + [' + @K + 'V>6] + [' + @K + 'W>6] + [' + @K + 'X>6] [Tot_M3]' " +
            "set @ColEnd='[' + @K + 'U<=6],[' + @K + 'U>6],[' + @K + 'V<=6],[' + @K + 'V>6],[' + @K + 'W<=6],[' + @K + 'W>6],[' + @K + 'X<=6],[' + @K + 'X>6], [Tot_<=6],[Tot_>6], [Tot_M3]' " +
            "set @sumColEnd='sum([' + @K + 'U<=6]),sum([' + @K + 'U>6]),sum([' + @K + 'V<=6]),sum([' + @K + 'V>6]),sum([' + @K + 'W<=6]),sum([' + @K + 'W>6]),sum([' + @K + 'X<=6]),sum([' + @K + 'X>6]), sum([Tot_<=6]),sum([Tot_>6]), sum([Tot_M3])' " +
            "end " +

            "set @query=' " +
            "select ROW_NUMBER() OVER (ORDER BY Tanggal,shif) AS ID,* ' + @TM31 + @TM32 +@TM3+ ' into tmpeff from ( " +
            "select B.P,A.Tanggal,A.shif,B.[Group],A.Qty PakaiBB ,' + @cols + ','+@M3 + ' " +
            "from tblPakaiPershift A inner join OuputProduksiPvt B on A.ProdLine=B.Line and A.tanggal=B.tanggal and A.shif=B.shif  " +
            "where prodline=  '+ cast(@line as char) +') C order by Tanggal,shif,[GROUP]' " +
            "execute(@query) " +
            "set @query='select  id,P,Tanggal,Shif,[Group],case when tot_m3>0 then (PakaiBB * (tot_m3/isnull((select sum(tot_m3) from tmpeff where tanggal=A.Tanggal and shif=A.shif ),0)))  " +
            "else PakaiBB end PakaiBB,'+@cols+',' + @ColEnd +' into tmpeff1 from tmpeff A' " +

            "execute(@query) " +

            "set @query='select  P, SUBSTRING(tanggal,7,2)+ ''-''+ SUBSTRING(tanggal,5,2)+''-''+ SUBSTRING(tanggal,1,4) Tanggal,Shif,[Group],PakaiBB,'+@cols +', " +
            "case when pakaiBB >0 and tot_M3>0 then (case when P=''P'' then ('+ @Pressing +'/(pakaiBB/tot_M3)*100) else  ('+ @NonPressing + " +
            "'/(pakaiBB/tot_M3)*100) end) else 0 end Efisiensi, case when pakaiBB >0 and tot_M3>0 then (pakaiBB/tot_M3) else 0 end [Kg/m3],'+@ColEnd +' into tmpeff2 from tmpeff1 A' " +
            "execute(@query) " +
            "set @query=' select * into  tmpeff3  from (  " +

            "select  * from tmpeff2  " +

            "union all  " +

            "select '''' P,''Total_P'' Tanggal,''-'' Shif,''-'' [Group],sum(PakaiBB)'+@sumcols +'), " +
            "(isnull((select sum(pakaiBB) from tmpeff2  where P=''NP''),0) +  sum(pakaiBB)) / (sum(Tot_M3) + (isnull((select sum(Tot_M3) from tmpeff2  where P=''NP''),0) *  " +
            "'+ @Pressing+' / ' +  @nonPressing  + ')) Efisiensi,case when sum(tot_M3)>0 then sum(isnull(pakaibb,0))/sum(tot_M3) else 0 end [Kg/m3],'+@sumColEnd +'  from tmpeff2 A where P=''P''  " +

            "union all  " +

            "select '''' P,''Total_NP'' Tanggal,''-'' Shif,''-'' [Group],sum(PakaiBB)'+@sumcols +'), " +
            "sum(pakaibb)/(sum([Tot_M3])*'+ @nonPressing +' / '+ @Pressing +')  Efisiensi,case when sum(tot_M3)>0 then sum(isnull(pakaibb,0))/sum(tot_M3) else 0 end [Kg/m3],'+@sumColEnd +'  from tmpeff2 A where P=''NP'')Z' " +

            "execute  (@query) " +
            "set @query=' select * into tmpeff4 from (select Tanggal, Shif, [Group],PakaiBB,'+@cols +',Efisiensi,[Kg/m3],'+@ColEnd +'  from tmpeff3 union all  " +
            "select''Total'' Tanggal, Shif, [Group],sum(PakaiBB)PakaiBB'+@sumcols +'),avg(Efisiensi),avg([Kg/m3]),'+@sumColEnd +'  from tmpeff3 where tanggal like ''total%'' " +
            " group by  Shif, [Group])E' " +
            "execute (@query) " +

            "select [Group],case when P='P' then 'Pressing' else 'NonPressing' end Jenis_Produksi,sum(pakaibb)Pakaibb,sum(tot_m3)Produksi_M3,case when  sum(tot_m3) >0 then sum(pakaibb)/sum(tot_m3) else 0 end Efisiensi  " +
            "into tmpeff5 from tmpeff3 where P like '%P%' group by P,[group] order by [group]  " +

            "select * into tmpeff6 from ( " +
            "select *,case when jenis_produksi='Pressing' " +
            "then (A.PakaiBB + (select case when count(A1.Jenis_Produksi)>0 then sum(A1.PakaiBB) else 0 end asasa from tmpeff5 A1 where A1.[group]=A.[group] and A1.Jenis_Produksi='NonPressing'))/ " +
            "(A.Produksi_M3+(select case when count(A2.Jenis_Produksi)>0 then sum(A2.Produksi_M3*@NonPressing/@Pressing) else 0 end aaa from tmpeff5 A2 where A2.[group]=A.[group] and A2.Jenis_Produksi='NonPressing')) " +
            " when jenis_produksi='NonPressing' then (A.PakaiBB/(A.Produksi_M3*(cast(@NonPressing as decimal(10,2))/cast(@Pressing as decimal(10,2))))) " +
            " else 0 " +
            "end Ef_Kumulatif from tmpeff5 A where A.[group]<>'-' )E " +

            "select * from tmpeff4 ";

            try
            {
                SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
                da.SelectCommand.CommandTimeout = 0;
                #region Code for preparing the DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
                dcol.AutoIncrement = true;
                #endregion
                GrdDynamic.Columns.Clear();
                foreach (DataColumn col in dt.Columns)
                {
                    BoundField bfield = new BoundField();
                    bfield.DataField = col.ColumnName;
                    bfield.HeaderText = col.ColumnName;
                    bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "PakaiBB")
                        bfield.DataFormatString = "{0:N1}";
                    if (bfield.HeaderText.Trim() != "Jenis")
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    else
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    if (bfield.HeaderText.Trim() == "Tanggal")
                        bfield.DataFormatString = "{0:d}";
                    if (bfield.HeaderText.Trim() == "Tot_M3")
                        bfield.DataFormatString = "{0:N1}";
                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();
                LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
                LblLine.Text = ddlLine.SelectedValue;
                loadDynamicGrid1();
            }
            catch { }
        }

        private void loadDynamicGrid1()
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
            {
                return;
            }
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string tglproses = string.Empty;

            /** Tambahan Adjust Kalsium dan Semen Curah **/
            string AdjustNPG1 = string.Empty; string AdjustNPG2 = string.Empty; string AdjustNPG3 = string.Empty; string AdjustNPG4 = string.Empty;
            string AdjustPG1 = string.Empty; string AdjustPG2 = string.Empty; ; string AdjustPG3 = string.Empty; string AdjustPG4 = string.Empty;

            string Line = string.Empty;
            string G1 = string.Empty; string G2 = string.Empty; string G3 = string.Empty; string G4 = string.Empty;

            if (users.UnitKerjaID == 1)
            {
                if (ddlLine.SelectedValue != "0")
                {
                    if (ddlLine.SelectedValue == "1")
                    {
                        Line = "1"; G1 = "CA"; G2 = "CB"; G3 = "CC"; G4 = "CD";
                    }
                    if (ddlLine.SelectedValue == "2")
                    {
                        Line = "2"; G1 = "CE"; G2 = "CF"; G3 = "CG"; G4 = "CH";
                    }
                    if (ddlLine.SelectedValue == "3")
                    {
                        Line = "3"; G1 = "CI"; G2 = "CJ"; G3 = "CK"; G4 = "CL";
                    }
                    if (ddlLine.SelectedValue == "4")
                    {
                        Line = "4"; G1 = "CM"; G2 = "CN"; G3 = "CO"; G4 = "CP";
                    }
                }
            }
            else if (users.UnitKerjaID == 7)
            {
                if (ddlLine.SelectedValue != "0")
                {
                    if (ddlLine.SelectedValue == "1")
                    {
                        Line = "1"; G1 = "KA"; G2 = "KB"; G3 = "KC"; G4 = "KD";
                    }
                    if (ddlLine.SelectedValue == "2")
                    {
                        Line = "2"; G1 = "KE"; G2 = "KF"; G3 = "KG"; G4 = "KH";
                    }
                    if (ddlLine.SelectedValue == "3")
                    {
                        Line = "3"; G1 = "KI"; G2 = "KJ"; G3 = "KK"; G4 = "KL";
                    }
                    if (ddlLine.SelectedValue == "4")
                    {
                        Line = "4"; G1 = "KM"; G2 = "KN"; G3 = "KO"; G4 = "KP";
                    }
                    if (ddlLine.SelectedValue == "5")
                    {
                        Line = "5"; G1 = "KQ"; G2 = "KR"; G3 = "KS"; G4 = "KT";
                    }
                    if (ddlLine.SelectedValue == "6")
                    {
                        Line = "6"; G1 = "KU"; G2 = "KV"; G3 = "KW"; G4 = "KX";
                    }
                }
            }
            else if (users.UnitKerjaID == 13)
            {
                if (ddlLine.SelectedValue != "0")
                {
                    if (ddlLine.SelectedValue == "1")
                    {
                        Line = "1"; G1 = "jA"; G2 = "JB"; G3 = "JC"; G4 = "JD";
                    }
                    if (ddlLine.SelectedValue == "2")
                    {
                        Line = "2"; G1 = "JE"; G2 = "JF"; G3 = "JG"; G4 = "JH";
                    }
                    if (ddlLine.SelectedValue == "3")
                    {
                        Line = "3"; G1 = "JI"; G2 = "JJ"; G3 = "JK"; G4 = "JL";
                    }
                    if (ddlLine.SelectedValue == "4")
                    {
                        Line = "4"; G1 = "JM"; G2 = "JN"; G3 = "JO"; G4 = "JP";
                    }
                    if (ddlLine.SelectedValue == "5")
                    {
                        Line = "5"; G1 = "JQ"; G2 = "JR"; G3 = "JS"; G4 = "JT";
                    }
                    if (ddlLine.SelectedValue == "6")
                    {
                        Line = "6"; G1 = "JU"; G2 = "JV"; G3 = "JW"; G4 = "JX";
                    }
                }
            }

            string thnbln2 = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            DomainEBB2 d1 = new DomainEBB2();
            FacadeDEBB2 f1 = new FacadeDEBB2();
            d1 = f1.RetrieveDataAdjustPressing(thnbln2, Line, G1, G2, G3, G4);

            DomainEBB2 d2 = new DomainEBB2();
            FacadeDEBB2 f2 = new FacadeDEBB2();
            d2 = f2.RetrieveDataAdjustNonPressing(thnbln2, Line, G1, G2, G3, G4);

            if (Line == "1" || Line == "2" || Line == "3" || Line == "4" || Line == "5" || Line == "6")
            {
                AdjustPG1 = d1.QtyG1.ToString().Replace(",", ".");
                AdjustPG2 = d1.QtyG2.ToString().Replace(",", ".");
                AdjustPG3 = d1.QtyG3.ToString().Replace(",", ".");
                AdjustPG4 = d1.QtyG4.ToString().Replace(",", ".");

                AdjustNPG1 = d2.QtyG1.ToString().Replace(",", ".");
                AdjustNPG2 = d2.QtyG2.ToString().Replace(",", ".");
                AdjustNPG3 = d2.QtyG3.ToString().Replace(",", ".");
                AdjustNPG4 = d2.QtyG4.ToString().Replace(",", ".");
            }
            /** End Tambahan **/

            string strSQL =
                "declare @line int " +
                "declare @Pressing char(4) " +
                "declare @NonPressing char(4) " +
                "set @line=" + ddlLine.SelectedValue + " " +
                "declare @unitkerja int " +
                "set @unitkerja=" + users.UnitKerjaID +
                "set @Pressing='1375' " +
                "set @NonPressing='1250' " +
                "declare @K char " +
                "declare @Query varchar(Max) " +
                "declare @G1  char,@G2 char,@G3 char,@G4 char " +

                "declare @Actual decimal(18,2) " +
                "declare @ttl int " +
                "declare @ttl_all int  " +
                "declare @ttl_L1 int declare @ttl_L2 int declare @ttl_L3 int declare @ttl_L4 int " +
                "declare @Sarmut_ada int  " +
                "declare @thnbln char(6)  " +
                "declare @thn char(6)  " +
                "declare @bln char(6)  " +
                "declare @ada int  " +
                "set @thnbln='" + thnbln2 + "' " +
                "set @thn = SUBSTRING(@thnbln,1,4) " +
                "set @bln = SUBSTRING(@thnbln,5,2) " +

                //"if @line=1 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L1]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L1 " +
                //"end " +

                //"if @line=2 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L2]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L2 " +
                //"end " +

                //"if @line=3 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L3]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L3 " +
                //"end " +

                //"if @line=4 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L4]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L4 " +
                //"end " +

                //"if @line=5 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L5]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L5 " +
                //"end " +

                //"if @line=6 begin " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_L6]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_L6 " +
                //"end " +

                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerja) " +

                "if @line=1 " +
                "begin set @G1='A' set @G2='B' set @G3='C' set @G4='D' end  " +
                "if @line=2 " +
                "begin set @G1='E' set @G2='F' set @G3='G' set @G4='H' end  " +
                "if @line=3 " +
                "begin set @G1='I' set @G2='J' set @G3='K' set @G4='L' end  " +
                "if @line=4 " +
                "begin set @G1='M' set @G2='N' set @G3='O' set @G4='P' end  " +
                "if @line=5 " +
                "begin set @G1='Q' set @G2='R' set @G3='S' set @G4='T' end  " +
                "if @line=6 " +
                "begin set @G1='U' set @G2='V' set @G3='W' set @G4='X' end  " +

                "set @query=' " +

                "Select *,A1+B1+C1+D1 Z1," +
                "" + AdjustNPG1 + "+" + AdjustNPG2 + "+" + AdjustNPG3 + "+" + AdjustNPG4 + " Zd, " +
                "A2+B2+C2+D2 Z2," +
                "case when (A2+B2+C2+D2) >0 then ((A1+B1+C1+D1)+(" + AdjustNPG1 + "+" + AdjustNPG2 + "+" + AdjustNPG3 + "+" + AdjustNPG4 + "))/(A2+B2+C2+D2)else 0 end Z3, " +
                "case when (A2+B2+C2+D2) >0 then ((A1+B1+C1+D1)+(" + AdjustNPG1 + "+" + AdjustNPG2 + "+" + AdjustNPG3 + "+" + AdjustNPG4 + "))/((A2+B2+C2+D2) * '+ @nonpressing +'/'+@Pressing+') else 0 end Z4 from ( " +

                "select ''Non_Pressing'' Jenis_Produksi,'+@NonPressing +'[Std(Kg/M3)], " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''nonPressing''),0)A1, " +
                "cast(''" + AdjustNPG1 + "'' as decimal(10,0))AdjA1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''nonPressing''),0) A2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''nonPressing''),0)A3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''nonPressing''),0)A4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''nonPressing''),0)B1, " +
                "cast(''" + AdjustNPG2 + "'' as decimal(10,0))AdjB1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''nonPressing''),0)B2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''nonPressing''),0)B3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''nonPressing''),0)B4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''nonPressing''),0)C1, " +
                "cast(''" + AdjustNPG3 + "'' as decimal(10,0))AdjC1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''nonPressing''),0)C2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''nonPressing''),0)C3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''nonPressing''),0)C4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''nonPressing''),0)D1, " +
                "cast(''" + AdjustNPG4 + "'' as decimal(10,0))AdjD1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G4 +'''and Jenis_Produksi=''nonPressing''),0)D2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''nonPressing''),0)D3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''nonPressing''),0)D4)A   " +

                "union all  " +

                "Select *,A1+B1+C1+D1 Z1," +
                "" + AdjustPG1 + "+" + AdjustPG2 + "+" + AdjustPG3 + "+" + AdjustPG4 + " Zd, " +
                "A2+B2+C2+D2 Z2," +
                "case when (A2+B2+C2+D2) >0 then ((A1+B1+C1+D1)+(" + AdjustPG1 + "+" + AdjustPG2 + "+" + AdjustPG3 + "+" + AdjustPG4 + "))/(A2+B2+C2+D2) else 0 end Z3, " +
                "case when (A2+B2+C2+D2) >0 then (isnull((select sum(pakaibb) from tmpeff6),0)+(" + AdjustPG1 + "+" + AdjustPG2 + "+" + AdjustPG3 + "+" + AdjustPG4 + "))/((A2+B2+C2+D2) + isnull((select sum(produksi_m3)  " +
                "from tmpeff6 where Jenis_Produksi=''nonPressing''),0)* '+@nonpressing+'/'+@Pressing+') else 0 end Z4 from ( " +
                "select ''Pressing'' Jenis_Produksi,'+ @Pressing+' [Std(Kg/M3)], " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''Pressing''),0)A1, " +
                "cast(''" + AdjustPG1 + "'' as Decimal(10,0)) AdjA1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''Pressing''),0) A2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''Pressing''),0)A3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G1 +''' and Jenis_Produksi=''Pressing''),0)A4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''Pressing''),0)B1, " +
                "cast(''" + AdjustPG2 + "'' as decimal(10,0))AdjB1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''Pressing''),0)B2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''Pressing''),0)B3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G2 +''' and Jenis_Produksi=''Pressing''),0)B4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''Pressing''),0)C1, " +
                "cast(''" + AdjustPG3 + "'' as decimal(10,0))AdjC1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''Pressing''),0)C2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''Pressing''),0)C3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G3 +''' and Jenis_Produksi=''Pressing''),0)C4, " +

                "isnull((select pakaibb from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''Pressing''),0)D1, " +
                "cast(''" + AdjustPG4 + "'' as decimal(10,0))AdjD1, " +
                "isnull((select produksi_m3 from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''Pressing''),0)D2, " +
                "isnull((select efisiensi from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''Pressing''),0)D3, " +
                "isnull((select ef_kumulatif from tmpeff6 where [group]='''+ @K+@G4 +''' and Jenis_Produksi=''Pressing''),0)D4)A ' " +
                "execute(@query) " +

                "select Jenis_Produksi,sum(pakaiBB)SPB,sum(Produksi_M3)OutPut, " +
                "case when sum(Produksi_M3)>0 then sum(PakaiBB)/(sum(Produksi_M3)) else 0 end Efisiensi, " +
                "case when jenis_produksi='NonPressing' and sum(Produksi_M3)>0 then sum(PakaiBB)/((sum(Produksi_M3)* @nonpressing/@Pressing))  " +
                "when jenis_produksi='NonPressing' and sum(Produksi_M3)<=0 then 0 " +
                "when jenis_produksi='Pressing' and sum(Produksi_M3)>0 " +
                "then (isnull((select sum(pakaibb) from tmpeff6),0))/((sum(Produksi_M3)) + isnull((select sum(produksi_m3)  from tmpeff6 where Jenis_Produksi='nonPressing'),0)* @nonpressing/@Pressing) " +
                "when jenis_produksi='Pressing' and sum(Produksi_M3)<=0 then 0 " +
                "end Ef_Komulatif,@Line Line into tmp_sarmut1 from tmpeff6 group by jenis_produksi " +

                "set @ttl=isnull((select count(Jenis_Produksi) from tmp_sarmut1 ),0) " +

                "if @ttl>1 begin " +
                "select cast(sum(SPB)/(isnull((select OutPut from tmp_sarmut1 where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_sarmut1 where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0))Actual into tmp_actual from tmp_sarmut1	" +
                "end " +

                "if @ttl=1 begin " +
                "select cast(Ef_Komulatif as decimal(18,0))Actual into tmp_actual from tmp_sarmut1	 " +
                "end " +

                " /** Line 1 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=1 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 1' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from (select * from Sarmut_EBB where Line='1' and Tahun=@thn and BUlan=@bln and RowStatus>-1 ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 1 **/ " +
                "  " +
                " /** Line 2 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=2 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 2' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from ( " +
                " select * from Sarmut_EBB where Line='1' and Tahun=@thn and BUlan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='2' and Tahun=@thn and BUlan=@bln and RowStatus>-1  " +
                " ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 2 **/ " +
                "  " +
                " /** Line 3 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=3 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 3' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from ( " +
                " select * from Sarmut_EBB where Line='1' and Tahun=@thn and BUlan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='2' and Tahun=@thn and BUlan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='3' and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 3 **/ " +
                "  " +

                " /** Line 4 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=4 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 4' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from ( " +
                " select * from Sarmut_EBB where Line='1' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='2' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='3' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='4' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 4 **/ " +

                " /** Line 5 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=5 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 5' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from ( " +
                " select * from Sarmut_EBB where Line='1' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='2' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='3' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='4' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='5' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 5 **/ " +

                " /** Line 6 **/ " +
                " /** cek Nilai Sarmut di DB temp **/ " +
                " if @line=6 begin   " +
                " set @ada =isnull((select count(ID) from Sarmut_EBB where RowStatus>-1 and Line=@line and Tahun=@thn and Bulan=@bln),0) " +
                " /** end cek Nilai Sarmut di DB temp **/ " +
                " /** cek Nilai Sarmut di DB **/ " +
                " set @Sarmut_ada=isnull((select ID from SPD_Trans  where approval=0 and tahun=@thn and bulan=@bln and SarmutDeptID in ( select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='Efisiensi Pemakaian Bahan Baku') and RowStatus>-1 and SarmutDepartemen='Line 6' ) and RowStatus>-1 ),0) " +
                " /** end cek Nilai Sarmut di DB **/ " +
                "  " +
                " if @ada=0 begin " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " if @ada>0 and  @Sarmut_ada>0 begin " +
                " update Sarmut_EBB set RowStatus=-9 where Line=@line and Tahun=@thn and BUlan=@bln and RowStatus>-1 " +
                " insert into Sarmut_EBB (Jenis_Produksi,SPB,OutPut,Efisiensi,Ef_Komulatif,Line,RowStatus,Tahun,Bulan)  " +
                " select *,'0',@thn,@bln from tmp_sarmut1 " +
                " end " +
                "  " +
                " select Jenis_Produksi,sum(SPB)SPB,sum(OutPut)OutPut,sum(Efisiensi)Efisiensi,sum(Ef_Komulatif)Ef_Komulatif  " +
                " into tmp_data_all  " +
                " from ( " +
                " select * from Sarmut_EBB where Line='1' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='2' and Tahun=@thn and Bulan=@bln and RowStatus>-1  " +
                " union all " +
                " select * from Sarmut_EBB where Line='3' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='4' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='5' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " union all " +
                " select * from Sarmut_EBB where Line='6' and Tahun=@thn and Bulan=@bln and RowStatus>-1 " +
                " ) as x group by Jenis_Produksi " +
                "  " +
                " select cast(sum(SPB)/(isnull((select OutPut from tmp_data_all where Jenis_Produksi='Pressing'),0)+(isnull((select OutPut from tmp_data_all where Jenis_Produksi='NonPressing'),0)*@nonpressing/@Pressing))  as decimal(18,0)) Actual into tmp_actual_all from tmp_data_all " +
                "  " +
                " end " +
                " /** End Line 6 **/ " +

                "/*IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProposionate]') AND type in (N'U')) DROP TABLE [dbo].[tblProposionate] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiAll]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiAll] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiPerShift] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPakaiNonPerShift]') AND type in (N'U')) DROP TABLE [dbo].[tblPakaiNonPerShift] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksi]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksi " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPvt]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPvt " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff]') AND type in (N'U')) DROP TABLE [dbo].tmpeff " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff1]') AND type in (N'U')) DROP TABLE [dbo].tmpeff1 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff2]') AND type in (N'U')) DROP TABLE [dbo].tmpeff2 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff3]') AND type in (N'U')) DROP TABLE [dbo].tmpeff3 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff4]') AND type in (N'U')) DROP TABLE [dbo].tmpeff4 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff5]') AND type in (N'U')) DROP TABLE [dbo].tmpeff5 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpeff6]') AND type in (N'U')) DROP TABLE [dbo].tmpeff6 */";


            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion

            GrdDynamic1.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                bfield.DataFormatString = "{0:N0}";

                if (bfield.HeaderText.Trim() != "Jenis")
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                else
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;

                if (bfield.HeaderText.Trim().Substring(1, 1) == "1")
                    bfield.HeaderText = "PakaiBB";
                bfield.DataFormatString = "{0:N1}";

                if (bfield.HeaderText.Trim().Substring(1, 1) == "d")
                    bfield.HeaderText = "Adj";

                if (bfield.HeaderText.Trim().Substring(1, 1) == "2")
                {
                    bfield.HeaderText = "Hsl. Prod M3";
                    bfield.DataFormatString = "{0:N1}";
                }

                if (bfield.HeaderText.Trim().Substring(1, 1) == "3")
                    bfield.HeaderText = "Efisiensi";

                if (bfield.HeaderText.Trim().Substring(1, 1) == "4")
                    bfield.HeaderText = "Ef. Kumulatif";

                GrdDynamic1.Columns.Add(bfield);
            }
            GrdDynamic1.DataSource = dt;
            GrdDynamic1.DataBind();
        }

        private void Link2Sarmut()
        {
            /** Link by Beny
             *  Added 31 Juli 2021
             * **/
            Users user = (Users)Session["Users"];
            string sarmutPrs = "Efisiensi Pemakaian Bahan Baku";
            ZetroView z00 = new ZetroView();
            z00.QueryType = Operation.CUSTOM;

            z00.CustomQuery =

            "declare @tahun int,@bulan int,@thnbln nvarchar(6) " +
            "set @tahun=" + ddTahun.SelectedValue + " " +
            "set @bulan=" + ddlBulan.SelectedValue + " " +
            "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +

            "update SPD_Trans set Actual=(select Actual from tmp_actual) where approval=0 and tahun=@tahun and bulan=@bulan and SarmutDeptID in ( " +
            "select ID from SPD_Departemen where SarmutPID in (select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and " +
            "SarMutPerusahaan='" + sarmutPrs + "') and RowStatus>-1 and SarmutDepartemen='Line " + ddlLine.SelectedValue + "' ) " +
            "and RowStatus>-1 " +

            "update SPD_TransPrs set actual=(select Actual from tmp_actual_all) where RowStatus>-1 and Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
            "select ID from SPD_Perusahaan where deptid=1 and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')" +

            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_sarmut1]') AND type in (N'U')) DROP TABLE [dbo].tmp_sarmut1 " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_actual_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_actual_all " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_data_all]') AND type in (N'U')) DROP TABLE [dbo].tmp_data_all ";

            SqlDataReader sdr00 = z00.Retrieve();

        }

        protected decimal retrievetodest(string strquery)
        {
            decimal result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strquery;
            SqlDataReader sdr = zl.Retrieve();
            try
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = decimal.Parse(sdr["result"].ToString());
                    }
                }
            }
            catch { }
            return result;
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "EffesiensiBB_Line_" + ddlLine.SelectedValue + "_" + ddTahun.SelectedValue + ddlBulan.SelectedValue + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) || 
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy")!=DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtdrtanggal.Text = "01-" + DateTime.Parse(txtsdtanggal.Text).ToString("MMM-yyyy");
        }
        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) ||
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy") != DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Parse(txtdrtanggal.Text).AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd")
            //        + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //GridView HeaderGrid = (GridView)sender;
                //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell HeaderCell = new TableCell();
                //HeaderCell = new TableCell();
                //HeaderCell.Text = "Tanggal";
                //HeaderCell.ColumnSpan = 33;
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderGridRow.Cells.Add(HeaderCell);
                //GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);


            }
        }
        protected void grv1MergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            Users users = (Users)Session["Users"];
            string strgroup = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select rtrim(kodelokasi) kode from company where depoid=" + users.UnitKerjaID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    strgroup = sdr["kode"].ToString();
                }
            }
            string G1 = string.Empty;
            string G2 = string.Empty;
            string G3 = string.Empty;
            string G4 = string.Empty;

            if (ddlLine.SelectedValue == "1")
            {
                G1 = "A"; G2 = "B"; G3 = "C"; G4 = "D";
            }
            if (ddlLine.SelectedValue == "2")
            {
                G1 = "E"; G2 = "F"; G3 = "G"; G4 = "H";
            }
            if (ddlLine.SelectedValue == "3")
            {
                G1 = "I"; G2 = "J"; G3 = "K"; G4 = "L";
            }
            if (ddlLine.SelectedValue == "4")
            {
                G1 = "M"; G2 = "N"; G3 = "O"; G4 = "P";
            }
            if (ddlLine.SelectedValue == "5")
            {
                G1 = "Q"; G2 = "R"; G3 = "S"; G4 = "T";
            }
            if (ddlLine.SelectedValue == "6")
            {
                G1 = "U"; G2 = "V"; G3 = "W"; G4 = "X";
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = " ";
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group " + strgroup + G1;
                HeaderCell.ColumnSpan = 5;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group " + strgroup + G2;
                HeaderCell.ColumnSpan = 5;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group " + strgroup + G3;
                HeaderCell.ColumnSpan = 5;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Group " + strgroup + G4;
                HeaderCell.ColumnSpan = 5;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Total";
                HeaderCell.ColumnSpan = 5;



                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic1.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
        public enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }

        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
        {
            return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }

        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
        {
            if (interval == DateInterval.Year)
                return dt2.Year - dt1.Year;

            if (interval == DateInterval.Month)
                return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

            TimeSpan ts = dt2 - dt1;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
                return Round(ts.TotalDays);

            if (interval == DateInterval.Hour)
                return Round(ts.TotalHours);

            if (interval == DateInterval.Minute)
                return Round(ts.TotalMinutes);

            if (interval == DateInterval.Second)
                return Round(ts.TotalSeconds);

            if (interval == DateInterval.Weekday)
            {
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.WeekOfYear)
            {
                while (dt2.DayOfWeek != eFirstDayOfWeek)
                    dt2 = dt2.AddDays(-1);
                while (dt1.DayOfWeek != eFirstDayOfWeek)
                    dt1 = dt1.AddDays(-1);
                ts = dt2 - dt1;
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.Quarter)
            {
                double d1Quarter = GetQuarter(dt1.Month);
                double d2Quarter = GetQuarter(dt2.Month);
                double d1 = d2Quarter - d1Quarter;
                double d2 = (4 * (dt2.Year - dt1.Year));
                return Round(d1 + d2);
            }

            return 0;

        }

        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime tgl = DateTime.Parse("01-" + ddlBulan.SelectedValue + "-" + ddTahun.SelectedValue);
            txtdrtanggal.Text = "01-" + tgl.ToString("MMM-yyyy");
            txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (tgl.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") +
                "-" + tgl.ToString("MMM") + "-" + tgl.ToString("yyyy");
        }
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
                int i = 0;
                //if (e.Row.Cells[0].Text.Trim() == "Total3 (M3)")
                //{

                //    for (i = 1; i <= 34; i++)
                //    {
                //        if (e.Row.Cells[i].Text != "&nbsp;")
                //        e.Row.Cells[i].Text = decimal.Parse(e.Row.Cells[i].Text).ToString("N2");
                //    }
                //}
                //else
                //{
                //    //for (i = 1; i <= 33; i++)
                //    //{
                //    //    if (e.Row.Cells[i].Text !="&nbsp;")
                //    //    e.Row.Cells[i].Text = decimal.Parse(e.Row.Cells[i].Text).ToString("N0");
                //    //}
                //    if (e.Row.Cells[34].Text != "&nbsp;")
                //    e.Row.Cells[34].Text = decimal.Parse(e.Row.Cells[34].Text).ToString("N2");
                //}
                if (e.Row.Cells[0].Text.Trim() == "PRODUKTIVITAS")
                {
                    for (i = 1; i <= 32; i++)
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;")
                            e.Row.Cells[i].Text = decimal.Parse(e.Row.Cells[i].Text).ToString("N0") + "%";
                    }
                }
            }
        }
    }

    public class DomainEBB2
    {
        public int UnitKerjaID { get; set; }
        public int PlantValue { get; set; }
        public DateTime LastModifiedTime { get; set; }


        public string PlantName { get; set; }

        public string Line { get; set; }
        public string CreatedBy { get; set; }
        public string Jenis { get; set; }
        public string ThnBln { get; set; }
        public string GroupProduksi { get; set; }
        public Decimal QtyG1 { get; set; }
        public Decimal QtyG2 { get; set; }
        public Decimal QtyG3 { get; set; }
        public Decimal QtyG4 { get; set; }
        public Decimal Qty { get; set; }
    }

    public class FacadeDEBB2
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DomainEBB2 objBB2 = new DomainEBB2();

        public FacadeDEBB2()
            : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public DomainEBB2 RetrieveDataAdjustPressing(string ThnBln, string Line, string G1, string G2, string G3, string G4)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            //" select isnull(sum(Qty),0)Qty,GroupProduksi from Sarmut_EBB_Adjust where Line=3 and RowStatus>-1 and ThnBln='201909' group by GroupProduksi ";
            " select sum(QtyG1)QtyG1,sum(QtyG2)QtyG2,sum(QtyG3)QtyG3,sum(QtyG4)QtyG4,Jenis from (select isnull(sum(Qty),0)QtyG1,0'QtyG2',0'QtyG3',0'QtyG4' " +
            " ,Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and GroupProduksi='" + G1 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',isnull(sum(Qty),0)QtyG2,0'QtyG3',0'QtyG4',Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G2 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',0'QtyG2',isnull(sum(Qty),0)QtyG3,0'QtyG4',Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G3 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',0'QtyG2',0'QtyG3',isnull(sum(Qty),0)QtyG4,Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G4 + "' group by GroupProduksi,Jenis ) as xx where Jenis='Pressing' group by Jenis";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveQty(sqlDataReader);
                }
            }

            return new DomainEBB2();
        }

        public DomainEBB2 RetrieveDataAdjustNonPressing(string ThnBln, string Line, string G1, string G2, string G3, string G4)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            //" select isnull(sum(Qty),0)Qty,GroupProduksi from Sarmut_EBB_Adjust where Line=3 and RowStatus>-1 and ThnBln='201909' group by GroupProduksi ";
            " select sum(QtyG1)QtyG1,sum(QtyG2)QtyG2,sum(QtyG3)QtyG3,sum(QtyG4)QtyG4,Jenis from (select isnull(sum(Qty),0)QtyG1,0'QtyG2',0'QtyG3',0'QtyG4' " +
            " ,Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and GroupProduksi='" + G1 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',isnull(sum(Qty),0)QtyG2,0'QtyG3',0'QtyG4',Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G2 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',0'QtyG2',isnull(sum(Qty),0)QtyG3,0'QtyG4',Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G3 + "' group by GroupProduksi,Jenis " +

            " union all " +

            " select 0'QtyG1',0'QtyG2',0'QtyG3',isnull(sum(Qty),0)QtyG4,Jenis from Sarmut_EBB_Adjust where Line=" + Line + " and RowStatus>-1 and ThnBln='" + ThnBln + "' and " +
            " GroupProduksi='" + G4 + "' group by GroupProduksi,Jenis ) as xx where Jenis='Non Pressing' group by Jenis";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveQty(sqlDataReader);
                }
            }

            return new DomainEBB2();
        }

        public DomainEBB2 RetrieveQty(SqlDataReader sqlDataReader)
        {
            objBB2 = new DomainEBB2();
            objBB2.QtyG1 = Convert.ToDecimal(sqlDataReader["QtyG1"]);
            objBB2.QtyG2 = Convert.ToDecimal(sqlDataReader["QtyG2"]);
            objBB2.QtyG3 = Convert.ToDecimal(sqlDataReader["QtyG3"]);
            objBB2.QtyG4 = Convert.ToDecimal(sqlDataReader["QtyG4"]);
            objBB2.Jenis = sqlDataReader["Jenis"].ToString();
            return objBB2;
        }

    }
}