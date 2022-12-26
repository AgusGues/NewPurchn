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
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LProduktifitasOutput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                getYear();
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
                Button5.Visible = false;
            }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 30 ,false); </script>", false);
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
            //LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
            LblTgl1.Text = ddlBulan.SelectedItem.ToString() + " " + ddTahun.SelectedItem.ToString();
            LblLine.Text = ddlLine.SelectedValue;
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), false);
            //LoadPostingSarmut();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected int cekdataoutput(string thbln)
        {
            int jumlah = 0;
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select count(ID) jumlah from bm_bdtoutput where rowstatus>-1 and thnbln=" + thbln + " and line='line " + ddlLine.SelectedValue + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jumlah = Int32.Parse(sdr["jumlah"].ToString());
                }
            }
            return jumlah;
        }
        protected int cekdataproduktifitas(string thbln)
        {
            int jumlah = 0;
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select count(ID) jumlah from bm_bdtproduktifitas where rowstatus>-1 and thnbln=" + thbln + " and line='line " + ddlLine.SelectedValue + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jumlah = Int32.Parse(sdr["jumlah"].ToString());
                }
            }
            return jumlah;
        }

        private void loadDynamicGrid100(string tgl1, bool reset)
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
            {
                return;
            }
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string strSQL = string.Empty;
            string strSQL2 = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            if (reset == false && cekdataproduktifitas(thnbln) == 0)
                reset = true;
            if (reset == true)
                #region Logika Baru
                strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
                " " +
                "declare @thnbln  varchar(6) " +
                "declare @line varchar(max) " +
                "declare @unitkerjaID varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "set @line='line " + ddlLine.SelectedValue + "' " +
                "set @unitkerjaID='" + users.UnitKerjaID + "' " +

                 "if @unitkerjaID='1' begin " +
                "if @line='line 1' begin set @target='27' end " +
                "if @line='line 2' begin set @target='38' end " +
                "if @line='line 3' begin set @target='40' end " +
                "if @line='line 4' begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '7' begin " +
                "begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '13' begin " +
                "begin set @target='60' end " +
                "end " +


                " " +
                "update bm_bdtProduktifitas set rowstatus=-1 where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' " +
                "SELECT * into tempBreakBMPO From( " +

                "select line, left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
                "convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  " +
                "480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2  " +
                "and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  " +
                "where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  case when Pinalti=0  " +
                "then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  " +
                "then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U,  case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  " +
                "case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3,  " +
                "case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   " +
                "case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff  from (   " +
                "select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus,  1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))   " +
                "from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS,  StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, " +
                "0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  " +
                "then menit else 0 end  BDNPMS_E,  case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group]  " +
                "from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
                "select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end   " +
                "BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti, (select lokasiproblem  " +
                "from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP,  case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik'  " +
                "when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  " +
                "then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff  from(  select  isnull(xx.minutex,0) as menit,*  " +
                "from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  " +
                "Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) end finbd, " +
                "FinaltyBD,TglBreak  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B  " +
                "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line " +
                ") BM order by TglBreak " +

                "declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) " +
                "declare @K char " +
                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID) " +

                "if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end " +
                "if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end " +
                "if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
                "if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
                "if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
                "if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

                "SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
                "CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) " +
                "ELSE GP1 END GP1, " +

                "CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 " +
                "THEN (GP2-480) ELSE GP2  END GP2, " +

                "CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 " +
                "OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3  END GP3, " +

                "CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 " +
                "OR GroupOff=@gp4 OR GroupOff=@gp4 " +

                "THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1 " +
                "From tempBreakBMPO where RowStatus=0 order by TglBreak,StartBD,line  " +

                /** Baru **/
                /** Mulai **/
                "/** Mulai **/ " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt3]') AND type in (N'U')) DROP TABLE [dbo].[Bdt3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt4]') AND type in (N'U')) DROP TABLE [dbo].[Bdt4] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData0]') AND type in (N'U')) DROP TABLE [dbo].[LastData0] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData00]') AND type in (N'U')) DROP TABLE [dbo].[LastData00] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData000]') AND type in (N'U')) DROP TABLE [dbo].[LastData000] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData001]') AND type in (N'U')) DROP TABLE [dbo].[LastData001] " +

                /** Start temp table Bdt1 **/
                "select * into Bdt1 from ( " +
                "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,MenitMulai Prod_Mulai,case when MenitDone=0 then '480' else MenitDone end Prod_Done from ( " +
                "select *,  " +
                "case  " +
                "when waktu1 >= TglProduksi+' '+'23:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi)+1 as nchar)) +' '+'07:00:00' " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu1) " +
                "when waktu1 >= TglProduksi+' '+'15:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi) as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',waktu1) " +
                "when waktu1 >= TglProduksi+' '+'07:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi) as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',waktu1) end MenitMulai " +

                ",case " +
                "when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari=hari2 and Bulan<12 and  waktu2 < trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
                "when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari=hari2 and Bulan=12 and  waktu2 < trim(cast(YEAR(TglProduksi)+1 as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
                "when waktu2 >= TglProduksi+' '+'23:00:00' and hari2>TtlHari  and  waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
                "when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari>hari2 and Bulan<12 and  waktu2 < trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +

                "when waktu2 >= TglProduksi+' '+'15:00:00' and waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',waktu2) " +

                "when waktu2 >= TglProduksi+' '+'07:00:00' and waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',waktu2)   " +
                " end MenitDone  " +

                " from ( " +
                " select  *, " +

                "substring(TglProduksi,1,8)+trim(cast(Hari1 as nchar)) +' '+drJam waktu1, " +
                "case when Hari2>TtlHari and Bulan<12 then trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01' +' '+sdJam  " +
                "when Hari2>TtlHari and Bulan=12 then trim(cast(YEAR(TglProduksi)+1 as nchar))+'-01-01'+' '+sdJam else " +
                "substring(TglProduksi,1,8)+trim(cast(Hari2 as nchar)) +' '+sdJam " +
                "end waktu2 " +

                "from ( " +
                "select *,case when H1<>H2 then ((((DATEPART(HOUR,'23:59:59')+1)*60))- (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) )  else (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) end Prod_Mulai , case when H1<>H2 then  (DATEPART(HOUR,sdJam)*60)+(DATEPART(MINUTE,sdJam)) else (DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam)  end Prod_Done, (DATEPART(HOUR,drJam)*60)+(DATEPART(MINUTE,drJam)) Prod_Mulai2, (DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam) Prod_Done2, " +

                "case " +
                //"--when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1  then '0'+trim(cast(DAY(TglProduksi) as nchar)) "+
                //"--when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  then trim(cast(DAY(TglProduksi) as nchar)) "+
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  drJam>='00:00:00' and drJam<'07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)<9**/ and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)>=9**/ and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +

                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)<9**/  and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)>=9**/ and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar)) " +

                "end Hari1, " +
                "case " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)<9**/ and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)>=9**/ and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar)) " +

                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)<9**/  and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2 /**and day(TglProduksi)>=9**/ and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar)) " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar)) " +
                "end Hari2 " +


                "from ( " +
                "select left(convert(char,A.TglProduksi,23),10)TglProduksi,P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang, DATEPART(DAY,sdJam)H1,DATEPART(DAY,drJam)H2,left(convert(char,A.drJam,114),8)drJam,left(convert(char,A.sdJam,114),8)sdJam,DAY(EOMONTH(TglProduksi)) AS TtlHari,MONTH(TglProduksi)Bulan " +

                "from BM_Destacking A  inner join BM_Plant P on A.PlantID=P.ID  inner join BM_PlantGroup G on A.PlantGroupID =G.ID   inner join fc_items I on A.ItemID=I.ID  where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang,A.sdJam,A.drJam,A.TglProduksi " +

                ") as x ) as x1 ) as x2 ) as x3 ) as Final " +
                /** End temp table Bdt1 **/

                //"select * into Bdt2 from ( " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,@gp1 GP,right(syarat,2)GP2,@line Line,(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone,StartBD,FinishBD,GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1)BDNPMS_G1  from TempBreakDown1  " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,@gp2 GP,right(syarat,2)GP2,@line Line,(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone,StartBD,FinishBD,GP2,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G2)BDNPMS_G1  from TempBreakDown1 " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,@gp3 GP,right(syarat,2)GP2,@line Line,(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone,StartBD,FinishBD,GP3,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G3)BDNPMS_G1  from TempBreakDown1 " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,@gp4 GP,right(syarat,2)GP2,@line Line,(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone,StartBD,FinishBD,GP4,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1  from TempBreakDown1  ) as x  " +

                //"select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Mulai2,Prod_Done,Prod_Done2,selisihwaktu,isnull(GP1,0)GP1 into Bdt3 from ( " +
                //"select A.*,B.GP1 from Bdt1 A left join Bdt2 B ON A.TglProduksi=B.TglBreak and A.GP=B.GP ) as x /**where GP='KE'**/  group by  TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Done,selisihwaktu,GP1,Prod_Mulai2,Prod_Done2 " +

                //"select *,case when BDT_NS01=0 and TtlBDt=1 and TtlProd=1 then isnull((select A.BDNPMS_G1 from Bdt2 A where A.TglBreak=x2.TglProduksi and BDNPMS_G1>0 and A.GP2=x2.GP and A.GP=x2.GP),0)  else BDT_NS01 end BDT_NS into Bdt4 from ( " +
                //"select  *,case  " +
                //"when x1.TtlBDt>1 and x1.TtlProd=1 then isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=x1.TglProduksi and BDNPMS_G1>0 and A.GP2=x1.GP and A.GP=x1.GP),0)  " +

                //"else BDT_NS1  end BDT_NS01   from ( " +
                //"select *, " +

                //"(isnull((select count(A.BDNPMS_G1) from Bdt2 A where A.TglBreak=B.TglProduksi and BDNPMS_G1>0 and A.GP2=B.GP and A.GP=B.GP),0) )TtlBDt, " +
                //"isnull((select count(A1.TglProduksi) from Bdt3 A1 where A1.TglProduksi=B.TglProduksi and A1.GP=B.GP),0)TtlProd, " +
                //"case  " +
                //"when B.prod_mulai<B.prod_done and B.Prod_Mulai2<B.Prod_Done2 then isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=B.TglProduksi and BDNPMS_G1>0 and A.MenitMulai>=B.Prod_Mulai2 and A.MenitDone<=B.Prod_Done2 and A.GP2=B.GP and A.GP=B.GP),0)  " +

                //"when B.prod_mulai<B.prod_done and B.Prod_Mulai2>B.Prod_Done2 then isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=B.TglProduksi and BDNPMS_G1>0 and A.MenitMulai>=B.Prod_Mulai and A.MenitDone<=B.Prod_Done and A.GP2=B.GP and A.GP=B.GP),0) " +

                //"when  B.prod_mulai>B.prod_done then isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=B.TglProduksi and BDNPMS_G1>0 and A.MenitMulai>=B.Prod_Mulai2 and A.GP2=B.GP and A.GP=B.GP),0) end BDT_NS1 from Bdt3 B  " +

                //") as x1 ) as x2 " +

                /** Start temp table Bdt2 **/
                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S into Bdt2 from ( " +
                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
                "select *,case  " +
                "when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
                "case  " +
                "when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

                "from ( " +
                "select *,case " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                "case " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                "else (TglBreak)+' '+StartBD " +

                "end StartBD2, " +

                "case  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD      " +
                "else (TglBreak)+' '+FinishBD " +

                " end FinishBD2 " +
                " from ( " +
                " select left(convert(char,TglBreak,23),10)TglBreak,@gp1 GP,right(trim(syarat),2)GP2,@line Line, " +
                "StartBD,FinishBD,GP1 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +

                "union all " +

                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
                "select *,case  " +
                "when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00' " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
                "case  " +
                "when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00' " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

                "from ( " +
                " select *,case  " +
                " when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                " when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
                " when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                " when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                " when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                " case " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                " else (TglBreak)+' '+StartBD " +

                " end StartBD2, " +

                " case  " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                " else (TglBreak)+' '+FinishBD " +

                " end FinishBD2 " +
                " from ( " +
                "  select left(convert(char,TglBreak,23),10)TglBreak,@gp2 GP,right(trim(syarat),2)GP2,@line Line, " +
                "StartBD,FinishBD,GP2 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G2)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +

                "union all " +

                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
                "select *,case " +
                "when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
                "case " +
                "when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

                "from (" +
                "  select *,case " +
                " when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "  when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                " case " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "  when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "  when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "  else (TglBreak)+' '+StartBD " +

                " end StartBD2," +

                " case " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD   " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                " else (TglBreak)+' '+FinishBD " +

                " end FinishBD2 " +
                " from ( " +
                "  select left(convert(char,TglBreak,23),10)TglBreak,@gp3 GP,right(trim(syarat),2)GP2,@line Line, " +
                "StartBD,FinishBD,GP3 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G3)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +

                "union all " +

                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
                "select *,case " +
                "when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
                "case " +
                "when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

                "from ( " +
                "  select *,case  " +
                "  when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "  when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "  when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                "  case  " +
                "   when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "   when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
                "   when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "   when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "   when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "   else (TglBreak)+' '+StartBD " +

                "  end StartBD2, " +

                "   case  " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD   " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "  else (TglBreak)+' '+FinishBD    " +
                "  end FinishBD2 " +
                "  from ( " +
                "   select left(convert(char,TglBreak,23),10)TglBreak,@gp4 GP,right(trim(syarat),2)GP2,@line Line, " +
                "StartBD,FinishBD,GP4 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1 " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +
                ") as x " +
                /** End temp table Bdt2 **/

                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData03]') AND type in (N'U')) DROP TABLE [dbo].[LastData03] " +

                //"select *,case  " +
                //"when ttlNilai>0 and TtlBDt1=1 and BDT_NS=0 and BDT_NS1=0 then BDT_NS1  " +
                //"when ttlNilai>0 and TtlBDt1>1 and BDT_NS>0 then /**ttlNilai**/ BDT_NS " +
                //"when ttlNilai>0 and TtlBDt1>1 and BDT_NS=0 then 0 else BDT_NS " +
                //"end BDT_NS0 into LastData03 from ( " +

                //"select *,(select count(A.TglProduksi) from Bdt4 A where A.TglProduksi=x.TglProduksi and A.GP=x.GP and A.Tebal=x.Tebal)TtlBDt1,(select sum(selisihwaktu) from Bdt4 A where A.TglProduksi=x.TglProduksi and A.GP=x.GP and A.Tebal=x.Tebal)ttlNilai  from ( " +
                //"select * from Bdt4 ) as x " +

                //") as x1 " +

                //"select case when count(TglProduksi)>1 then 'Flag' end Flag,GP,TglProduksi Tgl into Flag from ( " +
                //"select TglProduksi,Line,GP,sum(Kubik)kubik,Deskripsi,sum(selisihwaktu)SelisihWaktu,GP1,sum(BDT_NS0)BDT_NS,TargetM3 from ( " +
                //"select A.*,B.Deskripsi,B.TargetM3 from /**Bdt4**/ LastData03 A inner join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang) as x group by TglProduksi,Line,GP,Deskripsi,GP1,Deskripsi,TargetM3) as x2  group by TglProduksi,GP " +

                //"select * into Bdt5 from ( " +
                //"select TglProduksi,Line,GP,sum(Kubik)kubik,Deskripsi,sum(selisihwaktu)SelisihWaktu,GP1,sum(BDT_NS0)BDT_NS,TargetM3 from ( " +
                //"select A.*,B.Deskripsi,B.TargetM3 from /**Bdt4**/  LastData03 A inner join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang) as x group by TglProduksi,Line,GP,Deskripsi,GP1,Deskripsi,TargetM3) as x2  " +

                //"select * into LastData from ( " +
                //"select *,case when Flag='' then GP1+BDT_NS when Flag='Flag' then SelisihWaktu end GP1_New from ( " +
                //"select A.*,isnull(B.Flag,'')Flag from Bdt5 A inner join Flag B ON A.TglProduksi=B.Tgl and A.GP=B.GP) as x) as x1 " +

                //"select * into LastData2 from ( " +
                //"select Line,GP,Deskripsi,sum(GP1_New)[Waktu(Menit)] from LastData where GP=@gp1  group by Line,GP,Deskripsi union all " +
                //"select Line,GP,Deskripsi,sum(GP1_New)[Waktu(Menit)] from LastData where GP=@gp2  group by Line,GP,Deskripsi union all " +
                //"select Line,GP,Deskripsi,sum(GP1_New)[Waktu(Menit)] from LastData where GP=@gp3  group by Line,GP,Deskripsi union all " +
                //"select Line,GP,Deskripsi,sum(GP1_New)[Waktu(Menit)] from LastData where GP=@gp4  group by Line,GP,Deskripsi ) as x " +

                /** Temp Table Bdt3 **/
                "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Done2 Prod_Done,isnull(GP1,0)GP1,isnull(sum(BDNPMS_G1),0)BDNPMS_G1, " +
                "case when x.MenitMulai>=x.Prod_Mulai and x.MenitMulai<x.Prod_Done2 then x.MenitDone when x.MenitDone=x.Prod_Mulai " +
                "then x.Prod_Done2-x.Prod_Mulai else 0 end GP1_Temp,BDNPMS_S into Bdt3 from ( select A.*,case when A.Prod_Done=479 then 480 " +
                "else A.Prod_Done end Prod_Done2,B.GP1,B.GP2,B.MenitMulai,B.MenitDone,B.BDNPMS_G1,B.BDNPMS_S from Bdt1 A " +
                "left join Bdt2 B ON A.TglProduksi=B.TglBreak and A.GP=B.GP and A.GP=B.GP2) as x /** where GP='KE' and GP2='KE' **/  " +
                "group by  TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Done2,GP1,MenitMulai,MenitDone,BDNPMS_S " +


                /** Temp Table Bdt4 **/
                "select *, isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=B.TglProduksi and A.GP2=B.GP and A.GP=B.GP),0) " +
                " BDT_NS into Bdt4 from Bdt3 B   " +


                /** Temp Table LastData0 **/
                " select A.Line,A.GP,B.Deskripsi,A.TglProduksi,GP1,A.BDT_NS,GP1+BDT_NS BDT_NS0,A.Prod_Mulai,A.Prod_Done, " +
                "isnull(sum(A.BDNPMS_G1),0)BDNPMS_G1,isnull(sum(BDNPMS_S),0)BDNPMS_S into LastData0 " +
                "from Bdt4 A " +
                "inner join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang where A.Kubik>0 and B.Rowstatus>-1 " +
                " group by A.Line,A.GP,B.Deskripsi,A.TglProduksi,GP1,A.BDT_NS,A.Prod_Mulai,A.Prod_Done,A.BDNPMS_S " +

                /** Temp Table LastData00 **/
                //"select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S,GP2,Prod_Done2,case  " +
                //"when Prod_Done2=0 then Prod_Done-Prod_Mulai  " +
                //"when Prod_Done2>0 and BDT_NS>0 then  Prod_Done2-Prod_Mulai " +
                //    //"when Prod_Done2>0 and BDT_NS=0 then Prod_Done2-Prod_Mulai  " +
                //"when Prod_Done2>0 and BDT_NS=0 then Prod_Done-Prod_Mulai " +
                //"end [Waktu/(Menit)]  " +
                //"into LastData00 from ( " +
                //"select *,case when GP2<Prod_Done then GP2 else Prod_Done end Prod_Done2 from ( " +
                //"select *,GP1+BDT_NS GP2 from LastData0 /**where GP='KE' and TglProduksi='2020-11-14'**/) as x ) as x2 " +
                //"group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,GP2,Prod_Done2 " +
                "select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S,GP2,Prod_Done2, " +
                "case " +
                "when ttl=1 and GP1+BDT_NS>0 then GP1+BDT_NS " +
                "when ttl=1 and GP1+BDT_NS=0 then Prod_Done-Prod_Mulai " +
                "when ttl>1 then Prod_Done-Prod_Mulai " +
                "end [Waktu/(Menit)]   into LastData00 " +
                "from ( " +
                "select Line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done,BDNPMS_G1,BDNPMS_S,GP2, " +
                "case when GP2<sum(Prod_Done) then GP2 else sum(Prod_Done) end Prod_Done2,(select count(A1.TglProduksi) from LastData0 A1 " +
                "where A1.TglProduksi=x.TglProduksi and A1.GP=x.GP)ttl  from ( " +
                "select *,GP1+BDT_NS GP2 from LastData0 /**where GP='KO' and TglProduksi='2021-02-07'**/ ) as x " +
                "group by Line,Deskripsi,TglProduksi,GP,GP1,BDT_NS,BDT_NS0,BDNPMS_G1,BDNPMS_S,GP2 " +
                ") as x2 " +
                "group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,GP2,Prod_Done2 ,BDNPMS_S,ttl  " +

                /** Temp Table LastData **/
                "select Line,GP,Deskripsi,case when BDT_NS0=0 then sum([Waktu/(Menit)]) when ttl>1 then [Waktu/(Menit)] else BDT_NS0 end [Waktu/(Menit)],TglProduksi " +
                //"case " +
                //"when BDNPMS_S>0 and Prod_Done>=479 and (ttlPD-ttlPM)>=479 then [Waktu/(Menit)]  " +
                //"when BDNPMS_S>0 and Prod_Done>=479 and (ttlPD-ttlPM)<480 then [Waktu/(Menit)]+BDNPMS_S " +
                //"else [Waktu/(Menit)]  end [Waktu/(Menit)],TglProduksi "+            
                "into LastData " +
                "from ( " +
                "select */**,(select sum(Prod_Done) from LastData00 A where A.TglProduksi=A1.TglProduksi and A1.GP=A.GP)ttlPD,(select sum(Prod_Mulai) " +
                "from LastData00 B where B.TglProduksi=A1.TglProduksi and A1.GP=B.GP)ttlPM **/ " +
                ",(select count(GP) from LastData00 A where A.TglProduksi=A1.TglProduksi and A1.GP=A.GP)ttl  from LastData00 A1 ) as x " +
                " group by Line,GP,Deskripsi,TglProduksi,BDT_NS0,[Waktu/(Menit)],ttl " +

                "select * into LastData000 from ( " +
                "select line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu/(Menit)],BDT_NS0,TglProduksi from ( " +
                "select Line,GP,Deskripsi,[Waktu/(Menit)],BDT_NS0,TglProduksi " +
                "from ( " +
                "select * from LastData00 A1 /**where TglProduksi>='2021-01-27' and TglProduksi<='2021-01-27' and GP='KF'**/ " +
                ") as x /**where TglProduksi>='2021-01-01' and TglProduksi<='2021-01-31' and GP='KF'**/ " +
                "group by Line,GP,Deskripsi,TglProduksi,BDT_NS0,[Waktu/(Menit)] " +
                ") as x1 group by line,GP,Deskripsi,TglProduksi,BDT_NS0 ) as x2 " +

                "select * into LastData001 from ( " +
                "select Line,GP,Deskripsi,case when BDT_NS0=0 then [Waktu/(Menit)] when ttl=1 then BDT_NS0 else [Waktu/(Menit)] end [Waktu/(Menit)],TglProduksi " +
                " from ( " +
                "select *,(select count(GP) from LastData000 A where A.TglProduksi=A1.TglProduksi and A1.GP=A.GP)ttl " +
                "from LastData000 A1 /** where tglproduksi>='2021-01-01' and tglproduksi<='2021-01-31' **/) as x ) as x1 " +

                /** Temp Table LastData2 **/
                "select * into LastData2 from ( " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData001 where GP=@gp1  group by Line,GP,Deskripsi  " +
                "union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData001 where GP=@gp2  group by Line,GP,Deskripsi  " +
                "union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData001 where GP=@gp3  group by Line,GP,Deskripsi " +
                "union all  " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData001 where GP=@gp4  group by Line,GP,Deskripsi ) as x " +
                /** End Baru **/

                "select distinct line,tglbreak,right(Syarat,2) Syarat,bdnpms_s into tempBDMS0 from tempBreakBMPO where bdnpms_s>0  " +
                "select line,tglbreak,Syarat,sum(bdnpms_s)bdnpms_s into tempBDMS from tempBDMS0 group by line,tglbreak,Syarat   " +
                "declare @query varchar(max) " +

                "set @query=' " +
                "select distinct line,tglbreak, " +
                "case when groupoff <>'''+@gp1+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp1 + ''' ),0) else 0 end gp10, " +
                "case when groupoff <>'''+@gp2+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp2 + ''' ),0) else 0 end gp20, " +
                "case when groupoff <>'''+@gp3+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp3 + ''' ),0) else 0 end gp30, " +
                "case when groupoff <>'''+@gp4+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp4 + ''' ),0) else 0 end gp40, " +
                "case when groupoff <>'''+@gp1+''' then gp1 else 0 end gp1,case when groupoff <>'''+@gp2+''' then gp2 else 0 end gp2, " +
                "case when groupoff <>'''+@gp3+''' then gp3 else 0 end gp3,case when groupoff <>'''+@gp4+''' then gp4 else 0 end gp4,groupoff into tempBDT1 from tempBreakBMPO A order by TglBreak' " +

                " " +

                "execute (@query) " +
                "set @query=  " +
                "'select * into tempBDT2 from  " +
                "(select line,'+@gp1 +' GP,sum(A.gp10) BDTProduktifitas,sum(A.gp1) BDTOutPut from (select line,''' + @gp1 + ''' '+  @gp1 + ',gp10,gp1 from tempBDT1)A group by A.line,A.' +  @gp1 + ' union all ' + " +
                "'select line,'+@gp2 +' GP,sum(B.gp20) BDTProduktifitas,sum(B.gp2) BDTOutPut from (select line,''' + @gp2 + ''' '+  @gp2 + ',gp20,gp2 from tempBDT1)B group by B.line,B.' +  @gp2 + ' union all ' + " +
                "'select line,'+@gp3 +' GP,sum(B.gp30) BDTProduktifitas,sum(B.gp3) BDTOutPut from (select line,''' + @gp3 + ''' '+  @gp3 + ',gp30,gp3 from tempBDT1)B group by B.line,B.' +  @gp3 + ' union all ' + " +
                "'select line,'+@gp4 +' GP,sum(B.gp40) BDTProduktifitas,sum(B.gp4) BDTOutPut from (select line,''' + @gp4 + ''' '+  @gp4 + ',gp40,gp4 from tempBDT1)B group by B.line,B.' +  @gp4 + ')D' " +

                "execute (@query) " +
                "select * into OuputProduksiPO from ( " +
                "select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang  " +
                "from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID " +
                "where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, " +
                "I.Lebar,I.Panjang )Z order by GP,tebal " +

                " " +

                "select Line,GP,Deskripsi,avg(BDTProduktifitas) BDTProduktifitas,avg(BDTOutPut) BDTOutPut,sum(kubik)kubik,TargetM3 into tempBDT3 from( " +
                "select A.Line,A.GP,A.BDTProduktifitas,A.BDTOutPut,Kubik " +
                ",(select top 1 Deskripsi from bm_stdtargetoutput where tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang) Deskripsi " +
                ",(select top 1 targetm3 from bm_stdtargetoutput where tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang) TargetM3 " +
                "from tempBDT2 A inner join OuputProduksiPO B on A.Line=B.line and A.GP=B.GP)A0 group by Line,GP,Deskripsi,TargetM3 order by Line,GP,Deskripsi " +

                " " +

                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen into tempBDTP from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '1'Urut,Line,GP," +
                //"(BDTProduktifitas * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)] "+
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D " +

                "union all " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                //"Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 Persen from( " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '1'Urut,Line,GP," +
                //"(BDTProduktifitas * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)]"+
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,GP,Urut " +

                "union all " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                //"avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], "+
                "cast(sum([Produktifitas(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) as decimal(18,3)) as decimal(18,1)) [Produktifitas(M3/Shift)], " +
                //"avg(konversi)konversi, " +           
                "cast(((sum([Produktifitas(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,5))/cast(60 as decimal(18,4))) as decimal(18,5)))*60/targetm3) as decimal(18,2))konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen " +
                "from (  " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen,TargetM3 from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen,TargetM3 from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '2'Urut,Line,GP," +
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Ketebalan,Urut,TargetM3 " +

                "union all " +

                //"select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''''Ketebalan, Sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                //"avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                ////"Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 Persen from( " +
                //"case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
                //"select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                //"select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                //"case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*40/targetm3 end konversi, " +
                //"case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                //"select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                //"case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], " +
                //"case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                //"select '3'Urut,Line,GP," +
                ////"(BDTProduktifitas * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)]"+
                //"(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                //",Deskripsi Ketebalan,TargetM3,kubik " +
                //"from tempBDT3 A)B)C)D)E group by  Line,Urut " +
                //")F order by Urut,line,GP " +

                "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)],sum(konversi)konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( " +
                "select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)], " +
                "sum([OutPut(M3)])[OutPut(M3)],   " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60)*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)],  " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3  " +
                "from( select '3'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3 ) as x group by  Line,Urut,GP)F " +
                "order by Urut,line,GP; " +

                " " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen into tempBDTO from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP," +
                //"(BDTOutPut * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)]"+
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D " +

                "union all " +

                "select Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi," +
                //"Sum([OutPut(M3)])/Sum([Target(M3)])*100 Persen from( " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP, " +
                //"(BDTOutPut * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)],"+
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,GP " +

                "union all " +

                "select Line,'Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi, " +
                 //"Sum([OutPut(M3)])/Sum([Target(M3)])*100 Persen from( " +
                 "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi," +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP," +
                //"(BDTOutPut * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)],"+
                "(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line)F order by line,GP; " +

                "if isnull((select count(id) from bm_bdtproduktifitas where rowstatus >-1 and thnbln=@thnbln and line=@line),0)=0 " +
                "begin " +
                "insert BM_BDTProduktifitas " +
                "select @thnbln ThnBln, Line, GP, [Waktu(Menit)], Ketebalan, [Target(M3)], [Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi, Persen,0 rowstatus from tempBDTP " +

                "end; " +
                " " +
                "with Produktifitas as( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen,stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when stdTarget=0 then 0 else [Produktifitas(M3/Shift)]*@target/stdTarget end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when stdTarget=0 then 0 else [Waktu(Menit)]*stdTarget/8/60 end [Target(M3)],[Produktifitas(M3)] [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then [Produktifitas(M3)]/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[Produktifitas(M3)],(select top 1 targetm3 from bm_stdTargetoutput where deskripsi=A.ketebalan)stdTarget " +
                "from BM_BDTProduktifitas A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +

                "ProduktifitasCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi,Persen from produktifitas " +

                "Union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when [Waktu(Menit)]=0 then 0 else [Produktifitas(M3)]/([Waktu(Menit)]/8/60) end [Produktifitas(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when stdTarget=0 then 0 else [Produktifitas(M3/Shift)] * @target/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas where GP=A.GP)) end konversi from Produktifitas A " +
                ")B group by Line,GP)C " +

                "union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)]," +
                "case when [Waktu(Menit)]=0 then 0 else [Produktifitas(M3)]/([Waktu(Menit)]/8/60) end  [Produktifitas(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when stdTarget=0 then 0 else [Produktifitas(M3/Shift)] * @target/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas ))  end konversi from Produktifitas A " +
                ")B group by Line)C ) " +

                "UPDATE BM_BDTProduktifitas  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[Produktifitas(M3)]=b.[Produktifitas(M3)], [Produktifitas(M3/Shift)]=b.[Produktifitas(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   produktifitasCalc b " +
                "WHERE  BM_BDTProduktifitas.Line = b.Line and BM_BDTProduktifitas.gp=b.GP and BM_BDTProduktifitas.ketebalan=b.Ketebalan  " +
                "and BM_BDTProduktifitas.thnbln=@thnbln and BM_BDTProduktifitas.Line=@Line and BM_BDTProduktifitas.rowstatus>-1 " +

                "select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)]," +
                "isnull([Produktifitas(M3)],0)[Produktifitas(M3)],isnull([Produktifitas(M3/Shift)],0)[Produktifitas(M3/Shift)],isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtProduktifitas  where rowstatus >-1 and ThnBln=@thnbln  and line=@line " +





                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO]" +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt3]') AND type in (N'U')) DROP TABLE [dbo].[Bdt3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt4]') AND type in (N'U')) DROP TABLE [dbo].[Bdt4] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt5]') AND type in (N'U')) DROP TABLE [dbo].[Bdt5] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flag]') AND type in (N'U')) DROP TABLE [dbo].[Flag] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData1]') AND type in (N'U')) DROP TABLE [dbo].[LastData1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData0]') AND type in (N'U')) DROP TABLE [dbo].[LastData0] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData00]') AND type in (N'U')) DROP TABLE [dbo].[LastData00] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData000]') AND type in (N'U')) DROP TABLE [dbo].[LastData000] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData001]') AND type in (N'U')) DROP TABLE [dbo].[LastData001] ";

            //"update SPD_TransPrs set actual= " +
            //"isnull((select round(([Produktifitas(M3)]/[Target(M3)])*100,2) Actual from ( " +
            //"select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum([Produktifitas(M3)]),0)[Produktifitas(M3)] from ( " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
            //"select * from BM_BDTProduktifitas where ThnBln==" + thnbln + "   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1),0) " +
            //"where SarMutPID in ( " +
            //"select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Tingkat Produktifitas' and OnSystem=1) " +
            //"and substring(Tahun,1,4)=" + thnbln + " and substring(Bulan,5,2)=" + thnbln + " ";

            else
                strSQL = "select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Produktifitas(M3)],0)[Produktifitas(M3)],isnull([Produktifitas(M3/Shift)],0)[Produktifitas(M3/Shift)], " +
                    " isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtProduktifitas A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            #endregion

            try
            {
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
                    if (bfield.HeaderText.Trim() != "Jenis")
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    else
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    if (bfield.HeaderText.Trim() == "Tanggal")
                        bfield.DataFormatString = "{0:d}";
                    if (bfield.HeaderText.Trim() == "Produktifitas(M3)")
                        bfield.DataFormatString = "{0:N1}";
                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();
                //LoadGridEdit();
                LoadListEdit();
                LoadPostingSarmut();
                LoadPostingBukuBesarISO();
            }
            catch
            {
                DisplayAJAXMessage(this, "Data tidak ada !! "); return;
            }
        }

        private void loadDynamicGrid(string tgl1, bool reset)
        {
            /** Perubahan 09 Oktober 2022 **/
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty; string logic1 = string.Empty; string logic2 = string.Empty; string logic3 = string.Empty;
            string logic4 = string.Empty;
            if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
            {
                return;
            }
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string strSQL = string.Empty;
            string strSQL2 = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            if (users.UnitKerjaID == 1)
            {
                logic1 = " and Line=B.Line and Rowstatus>-1 ";
                logic2 = " and B.Line=A.Line where B.Rowstatus>-1 ";
                logic3 = " and Line=A.Line and Rowstatus>-1 ";
                logic4 = " and A.Line=A1.Line ";
            }
            else
            {
                logic1 = ""; logic2 = ""; logic4 = "";
            }

            if (reset == false && cekdataproduktifitas(thnbln) == 0)
                reset = true;
            if (reset == true)
                #region Logika Baru
                strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0_P]   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO_P]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData2_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO_P]   " +

                " " +
                "declare @thnbln  varchar(6) " +
                "declare @line varchar(max) " +
                "declare @unitkerjaID varchar(6) " +
                "declare @target int " +

                "set @thnbln='" + thnbln + "' " +
                "set @line='line " + ddlLine.SelectedValue + "' " +
                "set @unitkerjaID='" + users.UnitKerjaID + "' " +

               "if @unitkerjaID='1' begin " +
                "if @line='line 1' begin set @target='27' end " +
                "if @line='line 2' begin set @target='38' end " +
                "if @line='line 3' begin set @target='40' end " +
                "if @line='line 4' begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '7' begin " +
                "begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '13' begin " +
                "begin set @target='60' end " +
                "end " +

                " " +

                "update bm_bdtProduktifitas set rowstatus=-1 where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' " +

                " /** Temp Table tempBreakBMPO **/ " +
                " SELECT * into tempBreakBMPO_P From( select  " +
                " case when StartBD>FinishBD then 'NextDay' else 'SameDay' end NotedDay , " +
                " case when StartBD<FinishBD or StartBD=FinishBD then TglBreak " +
                "  " +
                " when StartBD>FinishBD and H<Max_H and LEN(cast(DATEPART(DAY,TglBreak)+1 as char))=1  " +
                " then SUBSTRING(TglBreak,1,8)+'0'+cast(DATEPART(DAY,TglBreak)+1 as char)  " +
                "  " +
                " when StartBD>FinishBD and H<Max_H and LEN(cast(DATEPART(DAY,TglBreak)+1 as char))=2   " +
                " then SUBSTRING(TglBreak,1,8)+cast(DATEPART(DAY,TglBreak)+1 as char) " +
                "  " +
                " when StartBD>FinishBD and H=Max_H and LEN(cast(DATEPART(MONTH,TglBreak)+1 as char))=1 " +
                " then TRIM(cast(DATEPART(YEAR,TglBreak) as char)) +'-0'+TRIM(cast(DATEPART(MONTH,TglBreak)+1 as char))+'-'+'01' " +
                "  " +
                " when StartBD>FinishBD and H=Max_H and LEN(cast(DATEPART(MONTH,TglBreak)+1 as char))=2 and DATEPART(MONTH,TglBreak)<12 " +
                " then TRIM(cast(DATEPART(YEAR,TglBreak) as char)) +'-'+TRIM(cast(DATEPART(MONTH,TglBreak)+1 as char))+'-'+'01' " +
                "  " +
                " when StartBD>FinishBD and H=Max_H and LEN(cast(DATEPART(MONTH,TglBreak)+1 as char))=2 and DATEPART(MONTH,TglBreak)=12 " +
                " then TRIM(cast(DATEPART(YEAR,TglBreak)+1 as char)) +'-01-01' end TglBreak2 " +
                " ,* from ( select cast(DATEPART(DAY,TglBreak) as char)H,DAY(EOMONTH(TglBreak))Max_H,line,RIGHT(Syarat,1)GP, " +
                " left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
                " convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  " +
                " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , cast (FinaltyBD as datetime )))from BreakBM " +
                " where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  " +
                " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM " +
                " where RIGHT( rtrim (Syarat),2) = B.GPAT2  and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  " +
                " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  " +
                " where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  " +
                " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , cast (FinaltyBD as datetime )))from BreakBM " +
                " where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  " +
                " case when Pinalti=0  then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E " +
                " else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U, " +
                " case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  case when Pinalti=0 then BDNPMS_G2 " +
                " else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3, " +
                " case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L " +
                " else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) " +
                " end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff,Ketebalan " +
                " from (   select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
                " 1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))  " +
                " from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS, " +
                " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, 0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 " +
                " end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  then menit else 0 end  BDNPMS_E, " +
                " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] " +
                " from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr " +
                " order by [group]) and LEN(Syarat)=2 then menit else 0  end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] " +
                " from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                " order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, " +
                " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( select top 2 * from BM_PlantGroupbr " +
                " where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 " +
                " end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * " +
                " from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  order by [group] desc ) as Gr order by [group]) " +
                " and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 " +
                " end   BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
                " (select lokasiproblem  from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
                " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' " +
                " when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule' " +
                " when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC, " +
                " (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) " +
                " as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr " +
                " where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  " +
                " (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) " +
                " as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr " +
                " where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff,Ketebalan " +
                " from(  select  isnull(xx.minutex,0) as menit,*  from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd " +
                " from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  Case when Cast(startBD as int)>=23 " +
                " and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) " +
                " end finbd, FinaltyBD,TglBreak,Ketebalan,Pinalti  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B " +
                " where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line ) as x) BM order by TglBreak  " +


                "declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) " +
                "declare @K char " +
                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID) " +

                "if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end " +
                "if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end " +
                "if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
                "if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
                "if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
                "if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

                "SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
                "CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) " +
                "ELSE GP1 END GP1, " +

                "CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 " +
                "THEN (GP2-480) ELSE GP2  END GP2, " +

                "CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 " +
                "OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3  END GP3, " +

                "CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 " +
                "OR GroupOff=@gp4 OR GroupOff=@gp4 " +

                "THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1_P " +
                "From tempBreakBMPO_P where RowStatus=0 order by TglBreak,StartBD,line  " +

                " ;with " +
                " dataProduksi0 as (select left(convert(char,TglProduksi,112),8)Tgl,left(convert(char,TglProduksi,120),10)TglProduksi,PlantGroupID,PlantID,case when DATEDIFF(MINUTE,drJam,sdJam2)=479 then 480 else DATEDIFF(MINUTE,drJam,sdJam2) end Menit,ItemID from ( " +
                " select TglProduksi,PlantGroupID,PlantID,drJam,ItemID,case when left(convert(char,sdJam,108),8)='06:59:00' then left(convert(char,sdJam,112),8)+' '+'07:00:00' else sdJam end sdJam2 " +
                " from BM_Destacking where left(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from FC_Lokasi where lokasi like'%adj%') and PlantGroupID in (select ID from BM_PlantGroup) group by TglProduksi,PlantGroupID,PlantID,ItemID,sdJam,drJam ) as x), " +
                " dataProduksi1 as (select A.*,isnull(B.Kategori,'')Ketebalan from dataProduksi0 A left join FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +
                " dataProduksi2 as (select A.*,RIGHT(TRIM(B.[Group]),1)GP,RIGHT(TRIM(C.PlantName),1)Line from dataProduksi1 A inner join BM_PlantGroup B ON A.PlantGroupID=B.ID inner join BM_Plant C ON C.ID=A.PlantID), " +
                " dataProduksi20 as (select Tgl,TglProduksi,PlantGroupID,PlantID,sum(Menit)Menit,Ketebalan,GP,Line from dataProduksi2 group by  Tgl,TglProduksi,PlantGroupID,PlantID,Ketebalan,GP,Line), " +
                " dataProduksi3 as (select Tgl,TglProduksi/**,Waktu**/,Menit,GP,Line,Ketebalan from dataProduksi20 group by Tgl,TglProduksi/**,Waktu**/,GP,Line,Ketebalan,Menit), " +

                //" dataProduksi30 as (select TglBreak,sum(Selisih)NonSch,sum(BDNPMS_S)Sch,GP,Ketebalan,RIGHT(line,1)Line/**,isnull(Pinalti,0)Pinalti**/ from ( "+
                //" select case "+
                //" when StartBD<FinishBD /**and Pinalti>0**/ then DATEDIFF(MINUTE,StartBD,FinishBD)/** *(Pinalti/100)  as decimal(18,0)) **/ "+
                //" when StartBD<FinishBD /**and Pinalti>0**/ then DATEDIFF(MINUTE,StartBD,FinishBD) "+
                //" when StartBD>FinishBD /**and Pinalti>0**/ then DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD) /** *(Pinalti/100)  as decimal(18,0)) **/ "+
                //" when StartBD>FinishBD /**and Pinalti>0**/ then DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD) "+
                //" else 0 end Selisih,* from tempBreakBMPO_P where line=@line and left(convert(char,cast(TglBreak as datetime),112),6)=@thnbln) as x group by TglBreak,GP,Ketebalan,Line/**,Pinalti**/), "+

                " dataProduksi30 as (select TglBreak,sum(Selisih)NonSch,sum(BDNPMS_S)Sch,GP,Ketebalan,RIGHT(line,1)Line,isnull(Pinalti,0)Pinalti " +
                " from (select case when StartBD<FinishBD and Pinalti>0 and Noted='NonSch' then cast(DATEDIFF(MINUTE,StartBD,FinishBD)*(Pinalti/100) " +
                " as decimal(18,0)) when StartBD<FinishBD and Pinalti=0 and Noted='NonSch' then DATEDIFF(MINUTE,StartBD,FinishBD) " +
                " when StartBD>FinishBD and Pinalti>0 and Noted='NonSch' then " +
                " cast(DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD)*(Pinalti/100)  as decimal(18,0)) " +
                " when StartBD>FinishBD and Pinalti=0 and Noted='NonSch' then DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD) " +
                " else 0 end Selisih,* from (select case when Panjang>2 and left(syarat,2)<>@K+'H' then 'NonSch' when Panjang=2 " +
                " then 'Nonsch' when Panjang>2 and  left(syarat,2)=@K+'H' then 'Sch' end Noted ,* from (  select LEN(Syarat)Panjang,* " +
                " from tempBreakBMPO_P where line=@line and left(convert(char,cast(TglBreak as datetime),112),6)=@thnbln ) as x ) as xx ) as x " +
                " group by TglBreak,GP,Ketebalan,Line,Pinalti), " +

                " dataProduksi4 as (select *, " +
                " isnull((select sum(NonSch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan " + logic4 + "),0)NonSch, " +
                " isnull((select sum(Sch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan " + logic4 + "),0)Sch  " +
                " from dataProduksi3 A ), " +
                " dataProduksi5 as (select *,case when Menit=479 then 480-Sch else Menit-Sch end Waktu2 from dataProduksi4 ), " +
                " dataProduksi6 as (select TglProduksi Tgl,'Line'+ ' ' + Line Line,Ketebalan Deskripsi,@K+GP GP,Waktu2 [Waktu/(Menit)] from dataProduksi5 ) " +
                "  /** Temp data LastData **/ " +
                " select * into LastData_P from dataProduksi6 where Line=@line " +

                "select * into LastData2_P from ( " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData_P where GP=@gp1  group by Line,GP,Deskripsi  " +
                "union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData_P where GP=@gp2  group by Line,GP,Deskripsi  " +
                "union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData_P where GP=@gp3  group by Line,GP,Deskripsi " +
                "union all  " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)]  from LastData_P where GP=@gp4  group by Line,GP,Deskripsi ) as x " +

                "select distinct line,tglbreak,right(Syarat,2) Syarat,bdnpms_s into tempBDMS0_P from tempBreakBMPO_P where bdnpms_s>0  " +
                "select line,tglbreak,Syarat,sum(bdnpms_s)bdnpms_s into tempBDMS_P from tempBDMS0_P group by line,tglbreak,Syarat   " +
                "declare @query varchar(max) " +

                "set @query=' " +
                "select distinct line,tglbreak, " +
                "case when groupoff <>'''+@gp1+''' then 480 -isnull((select BDNPMS_S from tempbdms_P where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp1 + ''' ),0) else 0 end gp10, " +
                "case when groupoff <>'''+@gp2+''' then 480 -isnull((select BDNPMS_S from tempbdms_P where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp2 + ''' ),0) else 0 end gp20, " +
                "case when groupoff <>'''+@gp3+''' then 480 -isnull((select BDNPMS_S from tempbdms_P where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp3 + ''' ),0) else 0 end gp30, " +
                "case when groupoff <>'''+@gp4+''' then 480 -isnull((select BDNPMS_S from tempbdms_P where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp4 + ''' ),0) else 0 end gp40, " +
                "case when groupoff <>'''+@gp1+''' then gp1 else 0 end gp1,case when groupoff <>'''+@gp2+''' then gp2 else 0 end gp2, " +
                "case when groupoff <>'''+@gp3+''' then gp3 else 0 end gp3,case when groupoff <>'''+@gp4+''' then gp4 else 0 end gp4,groupoff into tempBDT1_P from tempBreakBMPO_P A order by TglBreak' " +

                " " +
                "execute (@query) " +

                "set @query=  " +
                "'select * into tempBDT2_P from  " +
                "(select line,'+@gp1 +' GP,sum(A.gp10) BDTProduktifitas,sum(A.gp1) BDTOutPut from (select line,''' + @gp1 + ''' '+  @gp1 + ',gp10,gp1 from tempBDT1_P)A group by A.line,A.' +  @gp1 + ' union all ' + " +
                "'select line,'+@gp2 +' GP,sum(B.gp20) BDTProduktifitas,sum(B.gp2) BDTOutPut from (select line,''' + @gp2 + ''' '+  @gp2 + ',gp20,gp2 from tempBDT1_P)B group by B.line,B.' +  @gp2 + ' union all ' + " +
                "'select line,'+@gp3 +' GP,sum(B.gp30) BDTProduktifitas,sum(B.gp3) BDTOutPut from (select line,''' + @gp3 + ''' '+  @gp3 + ',gp30,gp3 from tempBDT1_P)B group by B.line,B.' +  @gp3 + ' union all ' + " +
                "'select line,'+@gp4 +' GP,sum(B.gp40) BDTProduktifitas,sum(B.gp4) BDTOutPut from (select line,''' + @gp4 + ''' '+  @gp4 + ',gp40,gp4 from tempBDT1_P)B group by B.line,B.' +  @gp4 + ')D' " +

                "execute (@query) " +

                "select * into OuputProduksiPO_P from ( " +
                "select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang  " +
                "from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID " +
                "where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, " +
                "I.Lebar,I.Panjang )Z order by GP,tebal " +

                " " +
                "select Line,GP,Deskripsi,avg(BDTProduktifitas) BDTProduktifitas,avg(BDTOutPut) BDTOutPut,sum(kubik)kubik,TargetM3 into tempBDT3_P from( " +
                "select A.Line,A.GP,A.BDTProduktifitas,A.BDTOutPut,Kubik " +
                ",(select top 1 Deskripsi from bm_stdtargetoutput where rowstatus>-1 and tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") Deskripsi " +
                ",(select top 1 targetm3 from bm_stdtargetoutput where rowstatus>-1 and tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") TargetM3 " +
                "from tempBDT2_P A inner join OuputProduksiPO_P B on A.Line=B.line and A.GP=B.GP)A0 group by Line,GP,Deskripsi,TargetM3 order by Line,GP,Deskripsi " +

                " " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen into tempBDTP_P from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '1'Urut,Line,GP," +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D " +

                "union all " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '1'Urut,Line,GP," +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D)E group by  Line,GP,Urut " +

                "union all " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                //"avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                "cast(sum([Produktifitas(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) as decimal(18,3)) as decimal(18,1)) [Produktifitas(M3/Shift)], " +
                "cast(((sum([Produktifitas(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,5))/cast(60 as decimal(18,4))) as decimal(18,5)))*60/TargetM3) as decimal(18,2))konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen,TargetM3 from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen,TargetM3 from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [Produktifitas(M3/Shift)],TargetM3 from( " +
                "select '2'Urut,Line,GP," +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D)E group by  Line,Ketebalan,Urut,TargetM3 " +

                "union all " +

                "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) [OutPut(M3/Shift)],sum(konversi)konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( " +
                "select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, Sum([Target(M3)])[Target(M3)], " +
                "sum([OutPut(M3)])[OutPut(M3)],   " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)))*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3_P)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [OutPut(M3)],  " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [OutPut(M3/Shift)],TargetM3  " +
                "from( select '3'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2_P A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik from tempBDT3_P A)B)C)D)E group by  Line,Urut ,TargetM3 ) as x group by  Line,Urut,GP)F " +
                "order by Urut,line,GP; " +

                " " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen into tempBDTO_P from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP," +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D " +

                "union all " +

                "select Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi," +
                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP, " +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D)E group by  Line,GP " +

                "union all " +

                "select Line,'Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi, " +

                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi," +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],kubik [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) else 0 end [OutPut(M3/Shift)],TargetM3 from( " +
                "select Line,GP," +
                "(select  [Waktu(Menit)] from LastData2_P AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3_P A)B)C)D)E group by  Line)F order by line,GP; " +

                "if isnull((select count(id) from bm_bdtproduktifitas where rowstatus >-1 and thnbln=@thnbln and line=@line),0)=0 " +
                "begin " +
                "insert BM_BDTProduktifitas " +
                "select @thnbln ThnBln, Line, GP, [Waktu(Menit)], Ketebalan, [Target(M3)], [Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi, Persen,0 rowstatus from tempBDTP_P " +

                "end; " +
                " " +

                "with Produktifitas as( " +
                "select Line,GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Produktifitas(M3)],0)[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,isnull(Persen,0)Persen,stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when stdTarget=0 then 0 when [Produktifitas(M3/Shift)] =0 then 0 else [Produktifitas(M3/Shift)]*@target/stdTarget end konversi, " +
                "case when [Target(M3)]=0 then 0 when [Produktifitas(M3)] =0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, " +
                "case when stdTarget=0 then 0  when [Waktu(Menit)] =0 then 0 else [Waktu(Menit)]*stdTarget/cast(8 as decimal(18,2))/cast(60 as decimal(18,2)) end [Target(M3)],[Produktifitas(M3)] [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]=0 then 0 when [Produktifitas(M3)]=0 then 0 else [Produktifitas(M3)]/([Waktu(Menit)]/8/60) end [Produktifitas(M3/Shift)],stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[Produktifitas(M3)],(select top 1 targetm3 from bm_stdTargetoutput where rowstatus>-1 and deskripsi=A.ketebalan " + logic3 + ")stdTarget " +
                "from BM_BDTProduktifitas A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +

                "ProduktifitasCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi,Persen from produktifitas " +

                "Union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when [Waktu(Menit)]=0 then 0 when [Produktifitas(M3)]=0 then 0 else [Produktifitas(M3)]/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) end [Produktifitas(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 when [Produktifitas(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when stdTarget=0 then 0 when [Produktifitas(M3/Shift)]=0 then 0 else [Produktifitas(M3/Shift)] * @target/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas where GP=A.GP)) end konversi from Produktifitas A " +
                ")B group by Line,GP)C " +

                "union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)]," +
                "case when [Waktu(Menit)]=0 then 0 when [Produktifitas(M3)]=0 then 0 else [Produktifitas(M3)]/([Waktu(Menit)]/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) end  [Produktifitas(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 when [Produktifitas(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "case when stdTarget=0 then 0 when [Produktifitas(M3/Shift)] =0 then 0 else [Produktifitas(M3/Shift)] * @target/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas ))  end konversi from Produktifitas A " +
                ")B group by Line)C ) " +

                "UPDATE BM_BDTProduktifitas  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[Produktifitas(M3)]=b.[Produktifitas(M3)], [Produktifitas(M3/Shift)]=b.[Produktifitas(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   produktifitasCalc b " +
                "WHERE  BM_BDTProduktifitas.Line = b.Line and BM_BDTProduktifitas.gp=b.GP and BM_BDTProduktifitas.ketebalan=b.Ketebalan  " +
                "and BM_BDTProduktifitas.thnbln=@thnbln and BM_BDTProduktifitas.Line=@Line and BM_BDTProduktifitas.rowstatus>-1 " +

                " select Line,GP,[Waktu(Menit)],Ketebalan,[Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)],konversi,Persen from ( " +
                "select case when GP='SubTotal' then '2' when GP='GrandTotal' then '3' else '1' end Urut,isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)]," +
                "isnull([Produktifitas(M3)],0)[Produktifitas(M3)],isnull([Produktifitas(M3/Shift)],0)[Produktifitas(M3/Shift)],isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtProduktifitas  where rowstatus >-1 and ThnBln=@thnbln  and line=@line " +
                " ) as x order by Urut,line,GP " +

                #endregion

                #region Drop Temp Table
 "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0_P]   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO_P]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData2_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO_P]   ";




            #endregion

            else
                strSQL = "select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Produktifitas(M3)],0)[Produktifitas(M3)],isnull([Produktifitas(M3/Shift)],0)[Produktifitas(M3/Shift)], " +
                    " isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtProduktifitas A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);


            try
            {
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
                    if (bfield.HeaderText.Trim() != "Jenis")
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    else
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    if (bfield.HeaderText.Trim() == "Tanggal")
                        bfield.DataFormatString = "{0:d}";
                    if (bfield.HeaderText.Trim() == "Produktifitas(M3)")
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "Persen")
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "konversi")
                        bfield.DataFormatString = "{0:N1}";
                    if (bfield.HeaderText.Trim() == "Produktifitas(M3/Shift)")
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "Pro02}") ;

                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();
                //LoadGridEdit();
                LoadListEdit();
                LoadPostingSarmut();
                LoadPostingBukuBesarISO();
            }
            catch
            {
                DisplayAJAXMessage(this, "Data tidak ada !! "); return;
            }
        }

        private void loadDynamicGrid00(string tgl1, bool reset)
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty; string logic1 = string.Empty; string logic2 = string.Empty; string logic3 = string.Empty;
            if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
            {
                return;
            }
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string strSQL = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            if (users.UnitKerjaID == 1)
            {
                logic1 = " and Line=B.Line and Rowstatus>-1 ";
                logic2 = " and B.Line=A.Line where B.Rowstatus>-1 ";
                logic3 = " and Line=A.Line and Rowstatus>-1 ";
            }
            else
            {
                logic1 = ""; logic2 = "";
            }

            if (reset == false && cekdataoutput(thnbln) == 0)
                reset = true;
            if (reset == true)

                #region Ambil Data
                strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +


                " " +
                "declare @thnbln  varchar(6) " +
                "declare @line varchar(max) " +
                "declare @unitkerjaID varchar(6) " +
                "declare @target int " +

                "set @thnbln='" + thnbln + "' " +
                "set @line='line " + ddlLine.SelectedValue + "' " +
                "set @unitkerjaID='" + users.UnitKerjaID + "' " +

                "if @unitkerjaID='1' begin " +
                "if @line='line 1' begin set @target='27' end " +
                "if @line='line 2' begin set @target='38' end " +
                "if @line='line 3' begin set @target='40' end " +
                "if @line='line 4' begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '7' begin " +
                "begin set @target='40' end " +
                "end " +

                "if @unitkerjaID = '13' begin " +
                "begin set @target='60' end " +
                "end " +

                " " +

                "update bm_bdtoutput set rowstatus=-1 where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' " +

                /** Temp data tempBreakBMPO **/
                "SELECT * into tempBreakBMPO From( select case " +
                "when StartBD>FinishBD and LEN(cast(DATEPART(DAY,TglBreak)+1 as char))=2 then SUBSTRING(TglBreak,1,8)+cast(DATEPART(DAY,TglBreak)+1 as char) " +
                "when StartBD>FinishBD and LEN(cast(DATEPART(DAY,TglBreak)+1 as char))=1 then SUBSTRING(TglBreak,1,8)+'0'+cast(DATEPART(DAY,TglBreak)+1 as char) " +
                "when StartBD<FinishBD then TglBreak end TglBreak2,* from ( " +
                "select line,RIGHT(Syarat,1)GP, left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
                "convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  " +
                "480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2  " +
                "and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  " +
                "where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  case when Pinalti=0  " +
                "then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  " +
                "then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U,  case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  " +
                "case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3,  " +
                "case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   " +
                "case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff,Ketebalan  from (   " +
                "select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus,  1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))   " +
                "from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS,  StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, " +
                "0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  " +
                "then menit else 0 end  BDNPMS_E,  case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group]  " +
                "from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
                "select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end   " +
                "BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti, (select lokasiproblem  " +
                "from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP,  case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik'  " +
                "when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  " +
                "then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff,Ketebalan  from(  select  isnull(xx.minutex,0) as menit,*  " +
                "from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  " +
                "Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) end finbd, " +
                "FinaltyBD,TglBreak,Ketebalan  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B  " +
                "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line ) as x " +
                ") BM order by TglBreak " +

                "declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) " +
                "declare @K char " +
                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID) " +

                "if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end " +
                "if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end " +
                "if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
                "if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
                "if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
                "if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

                /** Temp data TempBreakDown1 **/
                "SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
                "CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) ELSE GP1 END GP1, " +
                "CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 THEN (GP2-480) ELSE GP2 END GP2, " +
                "CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3 END GP3, " +
                "CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 THEN (GP4-480) ELSE GP4 END GP4, " +
                "BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1 " +
                "From tempBreakBMPO where RowStatus=0 order by TglBreak,StartBD,line  " +

                ";with " +
                "dataProduksi0 as (select left(convert(char,TglProduksi,112),8)Tgl,left(convert(char,TglProduksi,120),10)TglProduksi,PlantGroupID,PlantID/**,480'Waktu'**/,DATEDIFF(MINUTE,drJam,sdJam)Menit,ItemID " +
                "from BM_Destacking where left(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from FC_Lokasi where lokasi like'%adj%') group by TglProduksi,PlantGroupID,PlantID,ItemID,sdJam,drJam), " +
                "dataProduksi1 as (select A.*,isnull(B.Kategori,'')Ketebalan from dataProduksi0 A left join FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +
                "dataProduksi2 as (select A.*,RIGHT(TRIM(B.[Group]),1)GP,RIGHT(TRIM(C.PlantName),1)Line from dataProduksi1 A inner join BM_PlantGroup B ON A.PlantGroupID=B.ID inner join BM_Plant C ON C.ID=A.PlantID), " +
                "dataProduksi20 as (select Tgl,TglProduksi,PlantGroupID,PlantID,sum(Menit)Menit,Ketebalan,GP,Line from dataProduksi2 group by  Tgl,TglProduksi,PlantGroupID,PlantID,Ketebalan,GP,Line), " +
                "dataProduksi3 as (select Tgl,TglProduksi/**,Waktu**/,Menit,GP,Line,Ketebalan from dataProduksi20 group by Tgl,TglProduksi/**,Waktu**/,GP,Line,Ketebalan,Menit), " +
                "dataProduksi30 as (select TglBreak,sum(Selisih)NonSch,sum(BDNPMS_S)Sch,GP,Ketebalan from ( " +
                "select case when StartBD<FinishBD then DATEDIFF(MINUTE,StartBD,FinishBD) when StartBD>FinishBD then DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD) else 0 end Selisih,* from tempBreakBMPO where line=@line and left(convert(char,cast(TglBreak as datetime),112),6)=@thnbln) as x group by TglBreak,GP,Ketebalan), " +
                "dataProduksi4 as (select *,isnull((select sum(NonSch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan),0)NonSch, " +
                "isnull((select sum(Sch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan),0)Sch from dataProduksi3 A )," +
                "dataProduksi5 as (select *,case when Menit=479 then 480-Sch else Menit-Sch end Waktu2 from dataProduksi4 ), " +
                "dataProduksi6 as (select TglProduksi Tgl,'Line'+ ' ' + Line Line,Ketebalan Deskripsi,'K'+GP GP,Waktu2 [Waktu/(Menit)] from dataProduksi5 ) " +
                /** Temp data LastData **/
                "select * into LastData from dataProduksi6 where Line=@line " +

                /** Temp data LastData2 **/
                "select * into LastData2 from ( " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp1  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp2  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp3  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp4  group by Line,GP,Deskripsi ) as x " +

                /** Temp data tempBDMS0 **/
                "select distinct line,tglbreak,right(Syarat,2) Syarat,bdnpms_s into tempBDMS0 from tempBreakBMPO where bdnpms_s>0  " +

                /** Temp data tempBDMS **/
                "select line,tglbreak,Syarat,sum(bdnpms_s)bdnpms_s into tempBDMS from tempBDMS0 group by line,tglbreak,Syarat   " +

                "declare @query varchar(max) " +
                "set @query=' " +
                "select distinct line,tglbreak, " +
                "case when groupoff <>'''+@gp1+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp1 + ''' ),0) else 0 end gp10, " +
                "case when groupoff <>'''+@gp2+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp2 + ''' ),0) else 0 end gp20, " +
                "case when groupoff <>'''+@gp3+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp3 + ''' ),0) else 0 end gp30, " +
                "case when groupoff <>'''+@gp4+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp4 + ''' ),0) else 0 end gp40, " +
                "case when groupoff <>'''+@gp1+''' then gp1 else 0 end gp1,case when groupoff <>'''+@gp2+''' then gp2 else 0 end gp2, " +
                "case when groupoff <>'''+@gp3+''' then gp3 else 0 end gp3,case when groupoff <>'''+@gp4+''' then gp4 else 0 end gp4,groupoff into tempBDT1 from tempBreakBMPO A order by TglBreak' " +
                " " +
                "execute (@query) " +
                "set @query=  " +
                "'select * into tempBDT2 from  " +
                "(select line,'+@gp1 +' GP,sum(A.gp10) BDTProduktifitas,sum(A.gp1) BDTOutPut from (select line,''' + @gp1 + ''' '+  @gp1 + ',gp10,gp1 from tempBDT1)A group by A.line,A.' +  @gp1 + ' union all ' + " +
                "'select line,'+@gp2 +' GP,sum(B.gp20) BDTProduktifitas,sum(B.gp2) BDTOutPut from (select line,''' + @gp2 + ''' '+  @gp2 + ',gp20,gp2 from tempBDT1)B group by B.line,B.' +  @gp2 + ' union all ' + " +
                "'select line,'+@gp3 +' GP,sum(B.gp30) BDTProduktifitas,sum(B.gp3) BDTOutPut from (select line,''' + @gp3 + ''' '+  @gp3 + ',gp30,gp3 from tempBDT1)B group by B.line,B.' +  @gp3 + ' union all ' + " +
                "'select line,'+@gp4 +' GP,sum(B.gp40) BDTProduktifitas,sum(B.gp4) BDTOutPut from (select line,''' + @gp4 + ''' '+  @gp4 + ',gp40,gp4 from tempBDT1)B group by B.line,B.' +  @gp4 + ')D' " +
                "execute (@query) " +

                "select * into OuputProduksiPO from ( " +
                "select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang  " +
                "from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID " +
                "where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, " +
                "I.Lebar,I.Panjang )Z order by GP,tebal " +
                " " +

                "select Line,GP,Deskripsi,avg(BDTProduktifitas) BDTProduktifitas,avg(BDTOutPut) BDTOutPut,sum(kubik)kubik,TargetM3 into tempBDT3 from( " +
                "select A.Line,A.GP,A.BDTProduktifitas,A.BDTOutPut,Kubik " +
                ",(select top 1 Deskripsi from bm_stdtargetoutput where tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") Deskripsi " +
                ",(select top 1 targetm3 from bm_stdtargetoutput where tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") TargetM3 " +
                "from tempBDT2 A inner join OuputProduksiPO B on A.Line=B.line and A.GP=B.GP)A0 group by Line,GP,Deskripsi,TargetM3 order by Line,GP,Deskripsi " +
                " " +

                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen into tempBDTP from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else[Produktifitas(M3/Shift)]*@target/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan,case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [Produktifitas(M3)], " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0) then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [Produktifitas(M3/Shift)], " +
                "TargetM3 from( " +
                "select '1'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D " +

                "union all " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen " +
                "from (  select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], case when targetm3=0 then 0 " +
                "else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], " +
                "kubik [Produktifitas(M3)],/** case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [Produktifitas(M3/Shift)] **/ " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0) then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) " +
                "else 0 end [Produktifitas(M3/Shift)] " +
                ",TargetM3 from( select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) " +
                " [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,GP,Urut " +

                "union all  " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "[Produktifitas(M3/Shift)], konversi,Persen from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi, case when [Target(M3)]=0 then 0 " +
                "else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 " +
                "else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [Produktifitas(M3)], case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 " +
                "when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0) then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [Produktifitas(M3/Shift)],TargetM3 " +
                "from( select '2'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,Ketebalan,Urut " +

                "union all  " +

                "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) [OutPut(M3/Shift)],sum(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*@target/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [OutPut(M3)], " +
                " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)],TargetM3  from( select '3'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3 ) as x group by  Line,Urut,GP)F order by Urut,line,GP; " +

                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen into tempBDTO " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*@target/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)], " +
                "kubik [OutPut(M3)],/** case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)] **/ " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0) " +
                "then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)],TargetM3 from( select '1'Urut,Line,GP,(select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik " +
                " from tempBDT3 A)B)C)D  " +

                "union all  " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi,case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 " +
                "end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*@target/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [OutPut(M3)], " +
                " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0) " +
                "then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)],TargetM3 from( select '1'Urut,Line,GP, (select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,GP,Urut " +

                "union all " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                "cast(sum([OutPut(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) as decimal(18,3)) as decimal(18,1)) [OutPut(M3/Shift)], " +
                "cast(((sum([OutPut(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,5))/cast(60 as decimal(18,4))) as decimal(18,5)))*60/targetm3) as decimal(18,2))konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 " +
                "end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*@target/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [OutPut(M3)], " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) " +
                " else 0 end [OutPut(M3/Shift)],TargetM3 from( select '2'Urut,Line,GP,(select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Ketebalan,Urut,TargetM3 " +

                "union all " +

                 "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) [OutPut(M3/Shift)],sum(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*@target/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],kubik [OutPut(M3)], " +
                " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)],TargetM3  from( select '3'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3 ) as x group by  Line,Urut,GP)F order by Urut,line,GP; " +


                "if isnull((select count(id) from bm_bdtoutput where rowstatus >-1 and thnbln=@thnbln and line=@line),0)=0 " +
                "begin " +
                "insert bm_bdtoutput " +
                "select @thnbln ThnBln, Line, GP, [Waktu(Menit)], Ketebalan, [Target(M3)], [Output(M3)], [OutPut(M3/Shift)], konversi, Persen,0 rowstatus from tempBDTO " +

                "end; " +
                " " +

                "with OutPut as( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen,stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "case when stdTarget=0 then 0 else [OutPut(M3/Shift)]*@target/stdTarget end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan," +
                "case when stdTarget=0 then 0 else [Waktu(Menit)]*stdTarget/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)) end [Target(M3)],[OutPut(M3)] [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0 " +
                "then [OutPut(M3)]/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)], " +
                "stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[OutPut(M3)],(select top 1 targetm3 from bm_stdTargetoutput where deskripsi=A.ketebalan " + logic3 + ")stdTarget " +
                "from BM_BDTOutPut A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +

                "OutPutCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3/Shift)], konversi,Persen from OutPut " +

                "Union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))>0 " +
                "then [OutPut(M3)]/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) else 0 end [OutPut(M3/Shift)], " +
                "konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * @target/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut where GP=A.GP)) " +
                "end konversi " +
                "from OutPut A " +
                ")B group by Line,GP)C " +

                "union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "case when [Waktu(Menit)]=0 then 0 when ([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3)))=0 then 0 else [OutPut(M3)]/([Waktu(Menit)]/cast(8 as decimal(18,3))/cast(60 as decimal(18,3))) end [OutPut(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * @target/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut )) end konversi " +
                "from OutPut A " +
                ")B group by Line)C ) " +

                "UPDATE BM_BDTOutPut  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[OutPut(M3)]=b.[OutPut(M3)], [OutPut(M3/Shift)]=b.[OutPut(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   OutPutCalc b " +
                "WHERE  BM_BDTOutPut.Line = b.Line and BM_BDTOutPut.gp=b.GP and BM_BDTOutPut.ketebalan=b.Ketebalan  " +
                "and BM_BDTOutPut.thnbln=@thnbln and BM_BDTOutPut.Line=@Line and BM_BDTOutPut.rowstatus>-1 " +

                " select Line,GP,[Waktu(Menit)],Ketebalan,[Target(M3)],[Output(M3)],[Output(M3/Shift)],konversi,Persen from ( " +
                " select case when GP='SubTotal' then '2' when GP='GrandTotal' then '3' else '1' end Urut,isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)],isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtoutput where rowstatus >-1 and ThnBln=@thnbln  and line=@line " +
                " ) as x order by Urut,line,GP " +


                #region Drop Temp Table
 "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] ";



            #endregion

            #endregion

            else
                strSQL =
                " select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)], " +
                " isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtOutput A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            try
            {
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
                    if (bfield.HeaderText.Trim() != "Jenis")
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    else
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    if (bfield.HeaderText.Trim() == "Tanggal")
                        bfield.DataFormatString = "{0:d}";
                    if (bfield.HeaderText.Trim() == "Produktifitas(M3)")
                        bfield.DataFormatString = "{0:N1}";
                    if (bfield.HeaderText.Trim() == "Persen")
                        bfield.DataFormatString = "{0:N2}";
                    if (bfield.HeaderText.Trim() == "konversi")
                        bfield.DataFormatString = "{0:N2}";
                    if (bfield.HeaderText.Trim() == "Output(M3/Shift)")
                        bfield.DataFormatString = "{0:N2}";
                    if (bfield.HeaderText.Trim() == "Output(M3)")
                        bfield.DataFormatString = "{0:N2}";

                    GrdDynamic.Columns.Add(bfield);
                }
                GrdDynamic.DataSource = dt;
                GrdDynamic.DataBind();

                //LoadGridEdit();
                LoadListEdit();
                LoadPostingSarmut();
                LoadPostingBukuBesarISO();
            }
            catch
            {
                DisplayAJAXMessage(this, "Data tidak ada !! "); return;
            }
        }

        protected void LoadPostingBukuBesarISO()
        {
            Users users = (Users)Session["Users"];

            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string thn = ddTahun.SelectedValue;
            string bln = ddlBulan.SelectedValue;
            string NamaBulan = string.Empty;
            string line = ddlLine.SelectedValue;
            string Value = string.Empty;
            string SubItemSarmut = string.Empty;

            BDT_Prd dprd = new BDT_Prd();
            BDT_PrdFacade fprd = new BDT_PrdFacade();
            decimal OutProduksi_All = fprd.RetrieveOutProduksi_Ttl(thnbln);

            BDT_Prd dprd01 = new BDT_Prd();
            BDT_PrdFacade fprd01 = new BDT_PrdFacade();
            decimal Produktifitas_All = fprd01.RetrieveProdukTifitas_All(thnbln);

            if (users.DeptID == 2)
            {
                if (bln == "1") { NamaBulan = "Jan"; }
                else if (bln == "2") { NamaBulan = "Feb"; }
                else if (bln == "3") { NamaBulan = "Mar"; }
                else if (bln == "4") { NamaBulan = "Apr"; }
                else if (bln == "5") { NamaBulan = "Mei"; }
                else if (bln == "6") { NamaBulan = "Jun"; }
                else if (bln == "7") { NamaBulan = "Jul"; }
                else if (bln == "8") { NamaBulan = "Agu"; }
                else if (bln == "9") { NamaBulan = "Sep"; }
                else if (bln == "10") { NamaBulan = "Okt"; }
                else if (bln == "11") { NamaBulan = "Nov"; }
                else if (bln == "12") { NamaBulan = "Des"; }

                /** Table BukuBesar_Flaging **/
                ArrayList arrBB0 = new ArrayList();
                BDT_PrdFacade F0 = new BDT_PrdFacade();
                arrBB0 = F0.RetrieveMapping0("Tingkat Produktifitas", "Line " + ddlLine.SelectedValue);
                if (arrBB0.Count > 0)
                {
                    int i0 = 0;
                    foreach (BDT_Prd List0 in arrBB0)
                    {
                        ArrayList arrBB = new ArrayList();
                        BDT_PrdFacade F = new BDT_PrdFacade();
                        arrBB = F.RetrieveMapping("Tingkat Produktifitas", List0.Noted, List0.ParameterTerukur);
                        if (arrBB.Count > 0)
                        {
                            int i = 0;
                            foreach (BDT_Prd List in arrBB)
                            {
                                if (List.Flag == "OutProduksi_All")
                                {
                                    Value = OutProduksi_All.ToString().Replace(",", ".");
                                }
                                else if (List.Flag == "Produktifitas_All")
                                {
                                    Value = Produktifitas_All.ToString().Replace(",", ".");
                                }
                                else if (List.Flag.Contains("OutProduksi"))
                                {
                                    BDT_Prd dprd04 = new BDT_Prd();
                                    BDT_PrdFacade fprd04 = new BDT_PrdFacade();
                                    dprd04 = fprd04.RetrieveOutProduksi_PerLine(thnbln, List.Noted);

                                    Value = dprd04.M3.ToString().Replace(",", ".");
                                }
                                else if (List.Flag.Contains("Produktifitas"))
                                {
                                    BDT_Prd dprd03 = new BDT_Prd();
                                    BDT_PrdFacade fprd03 = new BDT_PrdFacade();
                                    dprd03 = fprd03.RetrieveSemuaLine(thnbln, List.Noted);

                                    Value = dprd03.Actual.ToString().Replace(",", ".");
                                }

                                BDT_Prd domain1 = new BDT_Prd();
                                BDT_PrdFacade facade1 = new BDT_PrdFacade();
                                int ada = facade1.RetrieveAda(List.Flag, List.Noted);

                                /** Cek data Nilai di database Jika tidak ada akan di insert  **/
                                if (ada == 0)
                                {
                                    ZetroView z0 = new ZetroView();
                                    z0.QueryType = Operation.CUSTOM;
                                    z0.CustomQuery =

                                    "insert into BukuBesar_Mapping (Bulan,Tahun,Nilai,ParameterTerukur,InitialBulan,CreatedBy,CreatedTime,RowStatus,ItemSarmut,SubItemSarmut) " +
                                    "values " +
                                    "('" + ddlBulan.SelectedValue + "','" + ddTahun.SelectedValue + "','" + Value + "','" + List.ParameterTerukur + "','" + NamaBulan +
                                         "','" + users.UserName + "'," + "GetDate()" + "," + "0" + ",'" + List.ItemSarmut + "'" + ",'" + List.Noted + "')";

                                    SqlDataReader sd0 = z0.Retrieve();
                                }
                                else
                                {
                                    /** Cek Approval inputan SarMut Department, Jika blm di apv mgr dept (apv 2) maka di update **/
                                    BDT_Prd domain2 = new BDT_Prd();
                                    BDT_PrdFacade facade2 = new BDT_PrdFacade();
                                    int Apv = facade1.RetrieveApvSarmut(List.Flag, List.Noted);

                                    if (Apv < 2)
                                    {
                                        ZetroView z01 = new ZetroView();
                                        z01.QueryType = Operation.CUSTOM;
                                        z01.CustomQuery =

                                        "Update BukuBesar_Mapping set='" + Value + "' where Tahun='" + ddTahun.SelectedValue + "' and Bulan='" + ddlBulan.SelectedValue + "' " +
                                        "and ParameterTerukur='" + List.ParameterTerukur + "' and  SubItemSarmut='" + List.Noted + "'";

                                        SqlDataReader sd0 = z01.Retrieve();
                                    }
                                }
                            }
                            i = i + 1;
                        }
                    }
                    i0 = i0 + 1;
                }
            }
        }

        protected void LoadPostingSarmut()
        {
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ZetroView zl = new ZetroView();
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
               "update SPD_TransPrs set actual= " +
                "isnull((select round(([Produktifitas(M3)]/[Target(M3)])*100,2) Actual from ( " +
                "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum([Produktifitas(M3)]),0)[Produktifitas(M3)] from ( " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1),0) " +
                "where SarMutPID in ( " +
                "select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Tingkat Produktifitas' and OnSystem=1) " +
                "and Tahun=substring('" + thnbln + "',1,4) and Bulan=substring('" + thnbln + "',5,2) and Approval=0 and rowstatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
        }

        private void LoadGridEdit()
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string strSQL = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            strSQL = "select Line, GP, cast([Waktu(Menit)] as int)[Waktu(Menit)], Ketebalan, [Target(M3)], [Produktifitas(M3)], [Produktifitas(M3/Shift)], " +
                    "konversi, Persen,isnull((select top 1 targetm3 from BM_StdTargetOutPut where deskripsi=A.ketebalan),0) stdtarget " +
                    "from bm_bdtProduktifitas A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
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
            GridEdit.DataSource = dt;
            GridEdit.DataBind();
        }
        //private void loadDynamicGrid0(string tgl1)
        //{
        //    Users users = (Users)Session["Users"];
        //    string strStocker = string.Empty;
        //    if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
        //    {
        //        return;
        //    }
        //    DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
        //    DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
        //    string tglproses = string.Empty;
        //    string sqlselect = string.Empty;
        //    string sqlinpivot = string.Empty;

        //    string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
        //    string strSQL = "SELECT * FROM tempBDTP " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO] " + 
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO]";
        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //    sqlCon.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
        //    da.SelectCommand.CommandTimeout = 0;
        //    #region Code for preparing the DataTable
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
        //    dcol.AutoIncrement = true;
        //    #endregion
        //    GrdDynamic1.Columns.Clear();
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        BoundField bfield = new BoundField();
        //        bfield.DataField = col.ColumnName;
        //        bfield.HeaderText = col.ColumnName;
        //        bfield.DataFormatString = "{0:N0}";
        //        if (bfield.HeaderText.Trim() != "Jenis")
        //            bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        //        else
        //            bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        //        if (bfield.HeaderText.Trim() == "Tanggal")
        //            bfield.DataFormatString = "{0:d}";
        //        GrdDynamic1.Columns.Add(bfield);
        //    }
        //    GrdDynamic1.DataSource = dt;
        //    GrdDynamic1.DataBind();

        //}

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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ProduktifitasOutput_Line_" + ddlLine.SelectedValue + "_" + ddTahun.SelectedValue + ddlBulan.SelectedValue + ".xls"));
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

        }
        protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
            //    int i = 0;
            //    string test=string.Empty;
            //    if (e.Row.Cells[1].Text.Trim().Length>5)
            //        test = e.Row.Cells[1].Text.Trim().Substring(3, 5);
            //    if (e.Row.Cells[1].Text.Trim() == "Total" || test=="Total")
            //    {
            //        for (i = 1; i <= 8; i++)
            //        {
            //            e.Row.Cells[i].BackColor = Color.FromName("gray");
            //            e.Row.Cells[i].ForeColor = Color.FromName("white");
            //        }
            //    }
            //}
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel3.Visible = true;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Panel3.Visible = false;
            //LoadGridEdit();
        }
        protected void GridEdit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtWaktu = (TextBox)e.Row.FindControl("txtWaktu");
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                txtWaktu.Text = ((DataRowView)e.Row.DataItem).Row[2].ToString();
                if (((DataRowView)e.Row.DataItem).Row[1].ToString().Trim().Length >= 5)
                {
                    txtWaktu.Enabled = false;
                }
            }

        }
        protected void txtGridTo_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtWaktu = (TextBox)row.FindControl("txtWaktu");
            row.Cells[4].Text = (decimal.Parse(txtWaktu.Text) * decimal.Parse(row.Cells[9].Text) / 8 / 60).ToString("N2");
            row.Cells[6].Text = ((decimal.Parse(row.Cells[5].Text)) / (decimal.Parse(txtWaktu.Text) / 8 / 60)).ToString("N2");
            row.Cells[7].Text = (decimal.Parse(row.Cells[6].Text) * 40 / decimal.Parse(row.Cells[9].Text)).ToString("N2");
            row.Cells[8].Text = (decimal.Parse(row.Cells[5].Text) / decimal.Parse(row.Cells[4].Text) * 100).ToString("N2");
            txtWaktu.Focus();
            //foreach (DataGridViewRow row1 in GridEdit.Rows)
            //{
            //    // 0 is the column index
            //    if (row1.Cells[1].Text.Equals("Total"))
            //    {
            //        row1. = true;
            //        break;
            //    }
            //}
            //    }
            //    catch (Exception ex)
            //    {
            //    }
        }
        protected void Button5_Click(object sender, EventArgs e)
        { }
        protected void Button4_Click(object sender, EventArgs e)
        {
            //LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
            LblTgl1.Text = ddlBulan.SelectedItem.ToString() + " " + ddTahun.SelectedItem.ToString();
            LblLine.Text = ddlLine.SelectedValue;
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), true);
        }

        //protected void Button5_Click(object sender, EventArgs e)
        //protected void LoadPostingSarmut()
        //{
        //    string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
        //    //BDT_Prd bdt = new BDT_Prd();
        //    //BDT_PrdFacade bdt_f = new BDT_PrdFacade();
        //    //bdt = bdt_f.RetrieveDataProduktifitas(thnbln);
        //    //if (bdt.ID == 0)
        //    //{
        //    //    { DisplayAJAXMessage(this, " Pemantauan/Sarmut belum dibuat otomatis, hubungi IT !! "); return; }  
        //    //}

        //    //bdt = bdt_f.RetrieveDataInputanSarmut(thnbln);

        //    //if (bdt.Approval > 0)
        //    //{
        //    //    string sts = string.Empty;
        //    //    if (bdt.Approval == 1)
        //    //    {
        //    //        sts = " Head ";
        //    //    }
        //    //    else if (bdt.Approval == 2)
        //    //    {
        //    //        sts = " Manager ";
        //    //    }
        //    //    else if (bdt.Approval == 3)
        //    //    {
        //    //        sts = " Verifikasi ISO ";
        //    //    }

        //    //    { DisplayAJAXMessage(this, " Pemantauan/Sarmut sudah di approve oleh " +sts+ " "); return; }  
        //    //}

        //    ZetroView zl = new ZetroView();
        //    zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    zl.CustomQuery =
        //       "update SPD_TransPrs set actual= " +
        //        "isnull((select round(([Produktifitas(M3)]/[Target(M3)])*100,2) Actual from ( " +
        //        "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum([Produktifitas(M3)]),0)[Produktifitas(M3)] from ( " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
        //        "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1),0) " +
        //        "where SarMutPID in ( " +
        //        "select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Tingkat Produktifitas' and OnSystem=1) " +
        //        "and Tahun=substring('" + thnbln + "',1,4) and Bulan=substring('" + thnbln + "',5,2) and Approval=0 and rowstatus>-1 ";      
        //    SqlDataReader sdr = zl.Retrieve();
        //}

        protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            BDT_Prd ba = (BDT_Prd)e.Item.DataItem;
            TextBox txtWaktu = (TextBox)e.Item.FindControl("txtWaktu");
            Label lblID = (Label)e.Item.FindControl("lblID");
            if (ba.GP.ToString().Trim().Length >= 5)
                txtWaktu.Enabled = false;
            lblID.ToolTip = ba.ID.ToString();
            //LoadListSarmut(ba.TypeID, ba.SDeptID, rps);
        }
        private void LoadListEdit()
        {
            Users users = (Users)Session["Users"];
            string strStocker = string.Empty;
            DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
            DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
            string strSQL = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select ID,isnull(Line,'')Line,isnull(GP,'')GP,isnull(cast([Waktu(Menit)] as int),0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan," +
                "isnull([Target(M3)],0)[Target(M3)],isnull([Produktifitas(M3)],0)[Produktifitas(M3)],isnull([Produktifitas(M3/Shift)],0)[Produktifitas(M3/Shift)], " +
                "isnull(konversi,0)konversi,isnull(Persen,0)Persen,isnull((select top 1 targetm3 from BM_StdTargetOutPut where deskripsi=A.ketebalan),0) stdtarget " +
                "from bm_bdtProduktifitas A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BDT_Prd
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Line = sdr["Line"].ToString(),
                        GP = sdr["GP"].ToString(),
                        WaktuMenit = Convert.ToInt32(sdr["Waktu(Menit)"].ToString()),
                        Ketebalan = sdr["Ketebalan"].ToString(),
                        TargetM3 = Convert.ToDecimal(sdr["Target(M3)"].ToString()),
                        ProduktifitasM3 = Convert.ToDecimal(sdr["Produktifitas(M3)"].ToString()),
                        ProduktifitasM3Shift = Convert.ToDecimal(sdr["Produktifitas(M3/Shift)"].ToString()),
                        konversi = Convert.ToDecimal(sdr["konversi"].ToString()),
                        Persen = Convert.ToDecimal(sdr["Persen"].ToString()),
                        stdTarget = Convert.ToDecimal(sdr["stdTarget"].ToString())
                    });
                }
            }
            lstPrs.DataSource = arrData;
            lstPrs.DataBind();
        }
        protected void txWaktu_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            Regex pattern = new Regex("[.]");
            Regex pattern1 = new Regex("[.]");
            foreach (RepeaterItem objItem in lstPrs.Items)
            {
                Label lblID = (Label)lstPrs.Items[i].FindControl("lblID");
                TextBox txtWaktu = (TextBox)lstPrs.Items[i].FindControl("txtWaktu");
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "update bm_bdtProduktifitas set [waktu(menit)] =" + pattern.Replace(decimal.Parse(txtWaktu.Text).ToString(), "") + " where ID=" + lblID.ToolTip;
                SqlDataReader sdr = zl.Retrieve();
                i++;
            }
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @thnbln  varchar(6); " +
                "declare @line varchar(max); " +
                "set @thnbln='" + thnbln + "'; " +
                "set @line='line " + ddlLine.SelectedValue + "'; " +
                "with Produktifitas as( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen,stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "[Produktifitas(M3/Shift)]*40/stdTarget konversi,[Produktifitas(M3)]/[Target(M3)]*100 Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[Waktu(Menit)]*stdTarget/8/60 [Target(M3)],[Produktifitas(M3)] [Produktifitas(M3)], " +
                "case when [Waktu(Menit)]>0 then [Produktifitas(M3)]/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[Produktifitas(M3)],(select top 1 targetm3 from bm_stdTargetoutput where deskripsi=A.ketebalan)stdTarget " +
                "from BM_BDTProduktifitas A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +
                "ProduktifitasCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi,Persen from produktifitas " +
                "Union all " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3)]/([Waktu(Menit)]/8/60) [Produktifitas(M3/Shift)],  " +
                "konversi,[Produktifitas(M3)]/[Target(M3)]*100  Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "[Produktifitas(M3/Shift)] * 40/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas where GP=A.GP))  konversi from Produktifitas A " +
                ")B group by Line,GP)C " +
                "union all " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3)]/([Waktu(Menit)]/8/60) [Produktifitas(M3/Shift)],  " +
                "konversi,[Produktifitas(M3)]/[Target(M3)]*100  Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, sum([Target(M3)])[Target(M3)],sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "[Produktifitas(M3/Shift)] * 40/stdTarget * ([Produktifitas(M3)]/ (select sum([Produktifitas(M3)]) from Produktifitas ))  konversi from Produktifitas A " +
                ")B group by Line)C ) " +
                "UPDATE BM_BDTProduktifitas  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[Produktifitas(M3)]=b.[Produktifitas(M3)], [Produktifitas(M3/Shift)]=b.[Produktifitas(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   produktifitasCalc b " +
                "WHERE  BM_BDTProduktifitas.Line = b.Line and BM_BDTProduktifitas.gp=b.GP and BM_BDTProduktifitas.ketebalan=b.Ketebalan  " +
                "and BM_BDTProduktifitas.thnbln=@thnbln and BM_BDTProduktifitas.Line=@Line and BM_BDTProduktifitas.rowstatus>-1 ";

            SqlDataReader sdr1 = zl1.Retrieve();
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), false);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }

    public class BDT_Prd : GRCBaseDomain
    {
        public int ID { get; set; }
        public string Line { get; set; }
        public string GP { get; set; }
        public int WaktuMenit { get; set; }
        public string Ketebalan { get; set; }
        public decimal TargetM3 { get; set; }
        public decimal ProduktifitasM3 { get; set; }
        public decimal ProduktifitasM3Shift { get; set; }
        public decimal konversi { get; set; }
        public decimal Persen { get; set; }
        public decimal stdTarget { get; set; }
        public decimal Actual { get; set; }
        public int Approval { get; set; }

        public string ItemSarmut { get; set; }
        public string ParameterTerukur { get; set; }
        public string Prosen { get; set; }
        public string Flag { get; set; }
        public string Noted { get; set; }
        public decimal M3 { get; set; }
    }

    public class BDT_PrdFacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BDT_Prd bdt = new BDT_Prd();
        private List<SqlParameter> sqlListParam;

        public decimal RetrieveOutProduksi_Ttl(string thnbln)
        {
            string StrSql =
            " select sum(kubik)OutPut_AllLine from ( " +
            " select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang " +
            " from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  " +
            " inner join fc_items I on A.ItemID=I.ID where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)='" + thnbln + "'  " +
            " group by P.PlantName,G.[Group],I.tebal,I.Lebar,I.Panjang ) as x ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["OutPut_AllLine"]);
                }
            }

            return 0;
        }

        public BDT_Prd RetrieveOutProduksi_PerLine(string thnbln, string Line)
        {
            string StrSql =
            " select sum(kubik)M3,Line from ( " +
            " select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang " +
            " from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  " +
            " inner join fc_items I on A.ItemID=I.ID where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)='" + thnbln + "'  " +
            " group by P.PlantName,G.[Group],I.tebal,I.Lebar,I.Panjang ) as x where Line='" + Line + "' group by Line";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveDataPerLine(sqlDataReader);
                }
            }

            return new BDT_Prd();
        }

        public decimal RetrieveProdukTifitas_All(string thnbln)
        {
            string StrSql =
            "select round(([OutPut(M3)]/[Target(M3)])*100,2) Produktifitas_All from ( " +
            "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum([OutPut(M3)]),0)[OutPut(M3)] from ( " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
            "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Produktifitas_All"]);
                }
            }

            return 0;
        }

        public int RetrieveTableBB(string Bulan, string Tahun)
        {
            string StrSql =
            " select count(ID) Nilai from BukuBesar_Mapping where rowstatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " " +
            " and ItemSarMut='Tingkat Produktifitas' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Nilai"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveMapping(string A, string A1, string A2)
        {
            arrData = new ArrayList();
            string strSQL =
            " select * from BukuBesar_Flaging where ItemSarmut='" + A + "' and RowStatus>-1 and Noted='" + A1 + "' and ParameterTerukur='" + A2 + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BDT_Prd
                    {
                        ItemSarmut = sdr["ItemSarmut"].ToString(),
                        ParameterTerukur = sdr["ParameterTerukur"].ToString(),
                        Flag = sdr["Flag"].ToString(),
                        Noted = sdr["Noted"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveMapping0(string A, string Line)
        {
            arrData = new ArrayList();
            string strSQL =
            " select Noted,ParameterTerukur from BukuBesar_Flaging where ItemSarmut='" + A + "' and RowStatus>-1 and Noted='" + Line + "' " +
            " group by Noted,ParameterTerukur " +
            " union all " +
            " select Noted,ParameterTerukur from BukuBesar_Flaging where ItemSarmut='" + A + "' and RowStatus>-1 and Noted='All Line' " +
            " group by Noted,ParameterTerukur ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BDT_Prd
                    {
                        Noted = sdr["Noted"].ToString(),
                        ParameterTerukur = sdr["ParameterTerukur"].ToString()
                    });
                }
            }
            return arrData;
        }

        public BDT_Prd RetrieveDataProduktifitas(string ThnBln)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string query = string.Empty;
            string strSQL =
            " select sum(ID)ID from ( " +
            " select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Tingkat Produktifitas' and OnSystem=1  " +
            " union all select '0'ID ) as x";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveData(sqlDataReader);
                }
            }
            return new BDT_Prd();
        }

        public BDT_Prd RetrieveDataInputanSarmut(string ThnBln)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string query = string.Empty;
            string strSQL =
            " select sum(Approval)Approval from ( " +
            " select Approval from SPD_TransPrs " +
            " where SarmutPID in (select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Tingkat Produktifitas') " +
            " and tahun=substring('" + ThnBln + "',1,4) and bulan=substring('" + ThnBln + "',5,2) union all " +
            " select '0'Approval ) as x";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveDataSarmut(sqlDataReader);
                }
            }
            return new BDT_Prd();
        }

        public BDT_Prd RetrieveSemuaLine(string thnbln, string Line)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            "select case when [Target(M3)] > 0 then round(([Produktifitas(M3)]/[Target(M3)])*100,2) else 0 end Actual, Line from (  " +
            "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum([Produktifitas(M3)]),0)[Produktifitas(M3)],Line from (  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all  " +
            "select * from BM_BDTProduktifitas where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x " +
            "group by Line) as x1 where Line='" + Line + "' " +
            "group by Line,[Produktifitas(M3)],[Target(M3)] order by Line  ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveTotalAll(sqlDataReader);
                }
            }

            return new BDT_Prd();
        }

        public int RetrieveAda(string A, string B)
        {
            string StrSql =
            " select ID Nilai from BukuBesar_Mapping where SubItemSarmut='" + B + "' and ParameterTerukur in (select ParameterTerukur from BukuBesar_Flaging " +
            " where Flag='" + A + "' and RowStatus>-1) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Nilai"]);
                }
            }

            return 0;
        }

        public int RetrieveApvSarmut(string thn, string bln)
        {
            string StrSql =
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=1 and SarMutPerusahaan='Tingkat Produktifitas') " +
            " and RowStatus>-1 and Approval<2 and Bulan='" + bln + "' and Tahun='" + thn + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Approval"]);
                }
            }

            return 0;
        }

        public BDT_Prd RetrieveTotalAll(SqlDataReader sqlDataReader)
        {
            bdt = new BDT_Prd();
            bdt.Actual = Convert.ToDecimal(sqlDataReader["Actual"]);
            bdt.Line = sqlDataReader["Line"].ToString();
            return bdt;
        }

        public BDT_Prd RetrieveData(SqlDataReader sqlDataReader)
        {
            bdt = new BDT_Prd();
            bdt.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return bdt;
        }

        public BDT_Prd RetrieveDataSarmut(SqlDataReader sqlDataReader)
        {
            bdt = new BDT_Prd();
            bdt.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            return bdt;
        }

        public BDT_Prd RetrieveDataPerLine(SqlDataReader sqlDataReader)
        {
            bdt = new BDT_Prd();
            bdt.M3 = Convert.ToInt32(sqlDataReader["M3"]);
            bdt.Line = sqlDataReader["Line"].ToString();
            return bdt;
        }
    }
}