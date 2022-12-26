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

/** OUTPUT **/
namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LProduktifitasOutput1 : System.Web.UI.Page
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
            zl.CustomQuery = "Select count(ID) jumlah from bm_bdtOutput where rowstatus>-1 and thnbln=" + thbln + " and line='line " + ddlLine.SelectedValue + "'";
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
            string strSQL0 = string.Empty;
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            if (reset == false && cekdataoutput(thnbln) == 0)
                reset = true;
            if (reset == true)


                #region Ambil Data
                strSQL0 =
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
                /** Tambahan **/
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt3]') AND type in (N'U')) DROP TABLE [dbo].[Bdt3]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt4]') AND type in (N'U')) DROP TABLE [dbo].[Bdt4]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt5]') AND type in (N'U')) DROP TABLE [dbo].[Bdt5]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flag]') AND type in (N'U')) DROP TABLE [dbo].[Flag]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData1]') AND type in (N'U')) DROP TABLE [dbo].[LastData1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData0]') AND type in (N'U')) DROP TABLE [dbo].[LastData0] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData00]') AND type in (N'U')) DROP TABLE [dbo].[LastData00] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData000]') AND type in (N'U')) DROP TABLE [dbo].[LastData000] " +

                " " +
                "declare @thnbln  varchar(6) " +
                "declare @line varchar(max) " +
                "declare @unitkerjaID varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "set @line='line " + ddlLine.SelectedValue + "' " +
                "set @unitkerjaID='" + users.UnitKerjaID + "' " +
                " " +
                "update bm_bdtoutput set rowstatus=-1 where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' " +

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
                "OR GroupOff=@gp4 OR GroupOff=@gp4 THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1 " +
                "From tempBreakBMPO where RowStatus=0 order by TglBreak,StartBD,line  " +

                /** Baru **/
                /** Temp Table Bdt1 **/
                "select * into Bdt1 from ( " +
                "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,MenitMulai Prod_Mulai,case when MenitDone=0 then '480' else MenitDone end Prod_Done from ( " +
                "select *,  " +
                "case  " +
                "when waktu1 >= TglProduksi+' '+'23:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi)+1 as nchar)) +' '+'07:00:00'  " +
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
                " end MenitDone   " +

                " from ( " +
                "select  *, " +

                "substring(TglProduksi,1,8)+trim(cast(Hari1 as nchar)) +' '+drJam waktu1, " +
                "case  " +
                "when Hari2>TtlHari and Bulan<12 then trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01' +' '+sdJam  " +
                "when Hari2>TtlHari and Bulan=12 then trim(cast(YEAR(TglProduksi)+1 as nchar))+'-01-01'+' '+sdJam " +
                "else  " +
                "substring(TglProduksi,1,8)+trim(cast(Hari2 as nchar)) +' '+sdJam " +
                "end waktu2 " +

                "from ( " +
                "select *,case when H1<>H2 then ((((DATEPART(HOUR,'23:59:59')+1)*60))- (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) )  " +
                "else (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) end Prod_Mulai , case when H1<>H2 then  (DATEPART(HOUR,sdJam)*60)+(DATEPART(MINUTE,sdJam)) " +
                "else (DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam)  end Prod_Done, (DATEPART(HOUR,drJam)*60)+(DATEPART(MINUTE,drJam)) Prod_Mulai2, " +
                "(DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam) Prod_Done2, " +

                "case  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  drJam>='00:00:00' and drJam<'07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +

                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2   and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +

                "end Hari1, " +
                "case  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +

                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2   and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
                "when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
                "end Hari2 " +


                "from ( " +
                "select left(convert(char,A.TglProduksi,23),10)TglProduksi,P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang, DATEPART(DAY,sdJam)H1,DATEPART(DAY,drJam)H2,left(convert(char,A.drJam,114),8)drJam,left(convert(char,A.sdJam,114),8)sdJam,DAY(EOMONTH(TglProduksi)) AS TtlHari,MONTH(TglProduksi)Bulan " +

                "from BM_Destacking A  inner join BM_Plant P on A.PlantID=P.ID  inner join BM_PlantGroup G on A.PlantGroupID =G.ID   inner join fc_items I on A.ItemID=I.ID  where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang,A.sdJam,A.drJam,A.TglProduksi " +

                ") as x ) as x1 ) as x2 ) as x3 ) as Final " +
                /** End Temp Table Bdt1 **/

                //"select * into Bdt2 from ( " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,case when GP1>0 then @gp1 else '' end GP,right(syarat,2)GP2,@line Line, " +
                //"(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone, " +
                //"StartBD,FinishBD,GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1)BDNPMS_G1  from TempBreakDown1  where GP1>0 " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,case when GP2>0 then @gp2 else '' end GP,right(syarat,2)GP2,@line Line, " +
                //"(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone, " +
                //"StartBD,FinishBD,GP2,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G2)BDNPMS_G1  from TempBreakDown1  where GP2>0 " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,case when GP3>0 then @gp3 else '' end GP,right(syarat,2)GP2,@line Line, " +
                //"(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone, " +
                //"StartBD,FinishBD,GP3,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G3)BDNPMS_G1  from TempBreakDown1  where GP3>0 " +
                //"union all " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,case when GP4>0 then @gp4 else '' end GP,right(syarat,2)GP2,@line Line, " +
                //"(DATEPART(HOUR,startBD)*60)+(DATEPART(MINUTE,startBD))MenitMulai,(DATEPART(HOUR,finishBD)*60)+(DATEPART(MINUTE,finishBD))MenitDone, " +
                //"StartBD,FinishBD,GP4,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1  from TempBreakDown1  where GP4>0 ) as x " +

                /** Temp Table Bdt2 **/
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
                "select *,case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'    " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +
                "case  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                "else (TglBreak)+' '+StartBD " +
                "end StartBD2, " +
                "case  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "else (TglBreak)+' '+FinishBD " +
                "end FinishBD2 " +
                "from ( " +
                " select left(convert(char,TglBreak,23),10)TglBreak,@gp1 GP,right(syarat,2)GP2,@line Line, " +
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
                "select *,case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                "case " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                "else (TglBreak)+' '+StartBD " +

                "end StartBD2, " +

                "case  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
                "else (TglBreak)+' '+FinishBD " +

                "end FinishBD2 " +
                "from ( " +
                "select left(convert(char,TglBreak,23),10)TglBreak,@gp2 GP,right(syarat,2)GP2,@line Line, " +
                "StartBD,FinishBD,GP2 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G2)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +

                "union all " +

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
                " select *,case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                "case  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "else (TglBreak)+' '+StartBD " +

                " end StartBD2, " +

                "   case  " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
                "   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD       " +
                "  else (TglBreak)+' '+FinishBD " +

                " end FinishBD2 " +
                "  from ( " +
                "select left(convert(char,TglBreak,23),10)TglBreak,@gp3 GP,right(syarat,2)GP2,@line Line, " +
                "StartBD,FinishBD,GP3 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G3)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +

                "union all " +

                "select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
                "select *,case  " +
                "when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00' " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00' " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
                "case  " +
                "when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2  <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
                "then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

                "from ( " +
                "select *,case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                "case  " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD   " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
                " when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD " +
                " else (TglBreak)+' '+StartBD " +

                "end StartBD2, " +

                "case  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD     " +
                "else (TglBreak)+' '+FinishBD   " +
                "end FinishBD2 " +
                "from ( " +
                "select left(convert(char,TglBreak,23),10)TglBreak,@gp4 GP,right(syarat,2)GP2,@line Line, " +
                "StartBD,FinishBD,GP4 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
                "TglBreak+' 23:00:00' time1  " +
                "from TempBreakDown1) as x ) as x1 ) as x2 " +
                ") as x " +
                /** End Temp Table Bdt2 **/

                /** Temp Table Bdt3 **/
                "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Done2 Prod_Done,isnull(GP1,0)GP1,isnull(sum(BDNPMS_G1),0)BDNPMS_G1, " +
                "case when x.MenitMulai>=x.Prod_Mulai and x.MenitMulai<x.Prod_Done2 then x.MenitDone when x.MenitDone=x.Prod_Mulai then x.Prod_Done2-x.Prod_Mulai else 0 end GP1_Temp,BDNPMS_S into Bdt3 from (  " +
                "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,Prod_Mulai,Prod_Done,Prod_Done2,case when GP1 is null then 480 else GP1 end GP1, " +
                "case when GP2 is null then GP else GP end GP2,case when MenitMulai is null then '0' else MenitMulai end MenitMulai, " +
                "case when MenitDone is null then '0' else MenitDone end MenitDone,case when BDNPMS_G1 is null then '0' else BDNPMS_G1 end BDNPMS_G1, " +
                "case when BDNPMS_S is null then '0' else BDNPMS_S end BDNPMS_S from ( " +
                "select A.*,case when A.Prod_Done=479 then 480 else A.Prod_Done end Prod_Done2,B.GP1,B.GP2,B.MenitMulai,B.MenitDone,B.BDNPMS_G1,B.BDNPMS_S " +
                "from Bdt1 A " +
                "left join Bdt2 B ON A.TglProduksi=B.TglBreak and A.GP=B.GP and A.GP=B.GP2 ) as a " +
                ") as x /** where GP='KE' and GP2='KE' **/  " +
                "group by  TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,Prod_Mulai,Prod_Done2,GP1,MenitMulai,MenitDone,BDNPMS_S  " +
                /** End Temp Table Bdt3 **/

                /** Temp Table Bdt4 **/
                "select *, isnull((select sum(A.BDNPMS_G1)BDNPMS_G1 from Bdt2 A where A.TglBreak=B.TglProduksi and A.GP2=B.GP and A.GP=B.GP),0) BDT_NS " +
                "into Bdt4 from Bdt3 B " +
                /** End Temp Table Bdt4 **/

                /** Temp Table LastData0 **/
                "select A.Line,A.GP,B.Deskripsi,A.TglProduksi,GP1,A.BDT_NS,GP1+BDT_NS BDT_NS0,A.Prod_Mulai,A.Prod_Done,sum(A.BDNPMS_G1)BDNPMS_G1,BDNPMS_S " +
                "into LastData0 " +
                "from Bdt4 A  " +
                "inner join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang  " +
                "group by A.Line,A.GP,B.Deskripsi,A.TglProduksi,GP1,A.BDT_NS,A.Prod_Mulai,A.Prod_Done,A.BDNPMS_S " +
                /** End Temp Table LastData0 **/

                /** Temp Table LastData00 **/
                "select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S,GP2,Prod_Done2,GP1 [Waktu/(Menit)] " +
                "into LastData00 from ( " +
                "select *,case when GP2<Prod_Done then GP2 else Prod_Done end Prod_Done2 from ( " +
                "select *,GP1+BDT_NS GP2 from LastData0 /**where GP='KE' and TglProduksi='2020-11-14'**/) as x ) as x2 " +
                "group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDT_NS0,Prod_Mulai,Prod_Done,GP2,Prod_Done2 " +
                /** End Temp Table LastData00 **/

                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tanda0]') AND type in (N'U')) DROP TABLE [dbo].[tanda0] "+
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tanda]') AND type in (N'U')) DROP TABLE [dbo].[tanda] "+
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tanda1]') AND type in (N'U')) DROP TABLE [dbo].[tanda1]  "+
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tanda2]') AND type in (N'U')) DROP TABLE [dbo].[tanda2]  "+

                //"select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDNPMS_S,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done into tanda0 "+
                //"from LastData00 A1  group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDNPMS_S " +

                //"select *,case "+
                //"when Total>1 and ((480-(BDNPMS_S+BDT_NS))-Prod_Mulai)<GP1 then (((480-(BDNPMS_S+BDT_NS))-Prod_Mulai)) "+
                //"when Total=1 then GP1 end [Waktu/Menit] into tanda  "+
                //"from (select *,Prod_Done-Prod_Mulai Selisih,(select count(GP) from tanda0 A2 where A2.line=x.line and A2.GP=x.GP and A2.TglProduksi=x.TglProduksi)Total "+ 
                //"from (select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDNPMS_S,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done from tanda0 group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,BDNPMS_S ) as x "+
                //") as x  "+

                //"select *,case when total=1 then [Waktu/Menit] else (select A1.[Waktu/Menit] "+
                //"from tanda A1 where A1.GP=A.GP and A1.TglProduksi=A.TglProduksi and [Waktu/Menit]+Prod_Mulai=GP1) end xx into tanda1 from tanda A "+

                //"select line,GP,Deskripsi,TglProduksi,xxx [Waktu/(Menit)],GP1 into tanda2 from ( "+
                //"select *,case when total=1 then [Waktu/Menit]  when [Waktu/Menit]+Prod_Mulai=GP1 then [Waktu/Menit] else GP1-xx end xxx from tanda1 "+
                //") as x2 " +   

                "select * into LastData from ( " +
                "select line,GP,Deskripsi,TglProduksi,[Waktu/Menit] [Waktu/(Menit)] from ( " +
                "select *,case " +
                "when Total>1 and Selisih=480 then GP1 " +

                /** lagi dibawah **/

                "when (Total>1 and Prod_Done-Prod_Mulai>GP1) then GP1-((480+Prod_Mulai)-(Prod_Done)) " +
                "when (Total>1 and Prod_Done-Prod_Mulai=GP1) then GP1 " +

                "when Total>1 and Prod_Done>480 and Prod_Done-Prod_Mulai<GP1 then Prod_Done-Prod_Mulai " +
                "when (Total>1 and Prod_Done=480) and (Prod_Done-Prod_Mulai)<GP1 then Selisih  " +
                "when (Total>1 and Prod_Done=480 and ((Prod_Done-Prod_Mulai)>GP1)) then GP1 " +
                "when (Total>1 and Prod_Done<480 and (Prod_Done-Prod_Mulai)<GP1 and (480-Selisih)>GP1) then GP1-(GP1-(Selisih)) " +
                "when (Total>1 and Prod_Done<480 and (Prod_Done-Prod_Mulai)<GP1 and (480-Selisih)<GP1) then GP1-(480-(Selisih)) " +
                "when Total>1 and Prod_Done=480 and (Prod_Done-Prod_Mulai)<GP1 then GP1-(480-((Prod_Done-Prod_Mulai)-Selisih)) " +
                "when Total=1 then GP1 end [Waktu/Menit]  " +
                "from (select *,Prod_Done-Prod_Mulai Selisih  " +
                "from (select line,GP,Deskripsi,TglProduksi,GP1,BDT_NS,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done, " +
                "(select count(GP) from LastData00 A2 where A2.line=A1.line and A2.GP=A1.GP and A2.TglProduksi=A1.TglProduksi)Total " +
                " from LastData00 A1 /**where tglproduksi='2020-10-28' and GP='KM'**/ group by line,GP,Deskripsi,TglProduksi,GP1,BDT_NS) as x ) as x1 " +
                ") as x2 ) as x3 " +

                "select * into LastData2 from ( " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from /**tanda2**/ LastData where GP=@gp1  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from /**tanda2**/ LastData where GP=@gp2  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from /**tanda2**/ LastData where GP=@gp3  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from /**tanda2**/ LastData where GP=@gp4  group by Line,GP,Deskripsi ) as x " +
                /** Baru Selesai **/

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
                "case when targetm3=0 then 0 else[Produktifitas(M3/Shift)]*40/targetm3 end konversi, " +
                "case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( " +
                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan,case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], " +
                //"case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)], "+
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)], " +
                "TargetM3 from( " +
                "select '1'Urut,Line,GP, " +
                //"(BDTProduktifitas * kubik/(select sum(kubik) from tempBDT3 where line=A.Line and GP=A.GP)) [Waktu(Menit)], "+
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], " +
                "Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D " +

                "union all " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, " +
                "case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen " +
                "from (  select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], case when targetm3=0 then 0 " +
                "else [Produktifitas(M3/Shift)]*40/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], " +
                "kubik [Produktifitas(M3)],/** case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)] **/ " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) " +
                "else 0 end [Produktifitas(M3/Shift)] " +
                ",TargetM3 from( select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) " +
                " [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,GP,Urut " +

                "union all  " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], " +
                "avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], " +
                "[Produktifitas(M3/Shift)], konversi,Persen from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], " +
                "case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*40/targetm3 end konversi, case when [Target(M3)]=0 then 0 " +
                "else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 " +
                "else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)], case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 " +
                "when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 " +
                "from( select '2'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] " +
                ",Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,Ketebalan,Urut " +

                "union all  " +

                "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)],sum(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60)*40/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*40/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3  from( select '3'Urut,Line,GP, " +
                "(select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3 ) as x group by  Line,Urut,GP)F order by Urut,line,GP; " +

                "select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen into tempBDTO " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*40/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], " +
                "kubik [OutPut(M3)],/** case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)] **/ " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) " +
                "then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from( select '1'Urut,Line,GP,(select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik " +
                " from tempBDT3 A)B)C)D  " +

                "union all  " +

                "select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi,case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 " +
                "end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*40/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) " +
                "then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from( select '1'Urut,Line,GP, (select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,GP,Urut " +

                "union all " +

                "select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                "avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 " +
                "end Persen from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*40/targetm3 end konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                "case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) " +
                " else 0 end [OutPut(M3/Shift)],TargetM3 from( select '2'Urut,Line,GP,(select  [Waktu(Menit)] " +
                "from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik " +
                "from tempBDT3 A)B)C)D)E group by  Line,Ketebalan,Urut " +

                "union all " +

                 "select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , " +
                "sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)],sum(konversi)konversi, case when Sum([Target(M3)])=0 then 0 " +
                "else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from ( select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, " +
                "Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)], " +
                "(sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60)*40/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 " +
                "from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  case when targetm3=0 then 0 " +
                "else [OutPut(M3/Shift)]*40/targetm3 end konversi, case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 " +
                "from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)], " +
                " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3  from( select '3'Urut,Line,GP, " +
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
                "case when stdTarget=0 then 0 else [OutPut(M3/Shift)]*40/stdTarget end konversi, " +
                "case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan," +
                "case when stdTarget=0 then 0 else [Waktu(Menit)]*stdTarget/8/60 end [Target(M3)],[OutPut(M3)] [OutPut(M3)], " +
                //"case when [Waktu(Menit)]>0 then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)], "+
                "case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0 " +
                "then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)], " +
                "stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[OutPut(M3)],(select top 1 targetm3 from bm_stdTargetoutput where deskripsi=A.ketebalan)stdTarget " +
                "from BM_BDTOutPut A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +

                "OutPutCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3/Shift)], konversi,Persen from OutPut " +

                "Union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                //"case when [Waktu(Menit)]=0 then 0 else [OutPut(M3)]/([Waktu(Menit)]/8/60) end [OutPut(M3/Shift)],  " +
                "case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0 " +
                "then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)], " +
                "konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                //"case when stdTarget=0 then 0 else [OutPut(M3/Shift)] * 40/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut where GP=A.GP)) "+
                "case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * 40/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut where GP=A.GP)) " +
                "end konversi " +
                "from OutPut A " +
                ")B group by Line,GP)C " +

                "union all " +

                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                //"case when [Waktu(Menit)]=0 then 0 else [OutPut(M3)]/([Waktu(Menit)]/8/60) end [OutPut(M3/Shift)],  " +
                "case when [Waktu(Menit)]=0 then 0 when ([Waktu(Menit)]/8/60)=0 then 0 else [OutPut(M3)]/([Waktu(Menit)]/8/60) end [OutPut(M3/Shift)],  " +
                "konversi,case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * 40/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut )) end konversi " +
                "from OutPut A " +
                ")B group by Line)C ) " +

                "UPDATE BM_BDTOutPut  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[OutPut(M3)]=b.[OutPut(M3)], [OutPut(M3/Shift)]=b.[OutPut(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   OutPutCalc b " +
                "WHERE  BM_BDTOutPut.Line = b.Line and BM_BDTOutPut.gp=b.GP and BM_BDTOutPut.ketebalan=b.Ketebalan  " +
                "and BM_BDTOutPut.thnbln=@thnbln and BM_BDTOutPut.Line=@Line and BM_BDTOutPut.rowstatus>-1 " +

                "select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)],isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtoutput where rowstatus >-1 and ThnBln=@thnbln  and line=@line " +

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].OuputProduksiPO " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt3]') AND type in (N'U')) DROP TABLE [dbo].[Bdt3] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt4]') AND type in (N'U')) DROP TABLE [dbo].[Bdt4] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt5]') AND type in (N'U')) DROP TABLE [dbo].[Bdt5] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Flag]') AND type in (N'U')) DROP TABLE [dbo].[Flag] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] ";


            #endregion


            else
                strSQL0 =
                " select isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)],isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)], " +
                " isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtOutput A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
            /**   **/




            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL0, sqlCon);
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
        private void loadDynamicGrid200(string tgl1, bool reset)
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
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare1]') AND type in (N'U')) DROP TABLE [dbo].[compare1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare2]') AND type in (N'U')) DROP TABLE [dbo].[compare2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt1]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt01]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt01] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt3]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt3]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt4]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt4] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt5]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt5] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt05]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt05] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt6]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt6] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt06]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt06] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_OP]') AND type in (N'U')) DROP TABLE [dbo].[LastData_OP] " +


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
                "OR GroupOff=@gp4 OR GroupOff=@gp4 THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1 " +
                "From tempBreakBMPO where RowStatus=0 order by TglBreak,StartBD,line  " +

                #region Temp Table Bdt1 Baru
            " select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,Prod_Mulai,Prod_Done,[shift] into Bdt1 from ( " +
                " select *, " +
                " case " +
                " when shift=1 and drJam='07:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'07:00:00') " +
                " when shift=1 and drJam>'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',drJam2) " +
                " when shift=2 and drJam='15:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'15:00:00') " +
                " when shift=2 and drJam>'15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',drJam2) " +
                " when shift=3 and drJam='23:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'23:00:00') " +
                " when shift=3 and drJam>'23:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',drJam2) " +
                " when shift=3 and drJam>='00:00:00' then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'00:00:00', " +
                " SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+drJam) + 60 " +
                " end Prod_Mulai, " +

                " case " +
                " when shift=1 and drJam>='07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',sdJam2) " +
                " when shift=2 and drJam>='15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',sdJam2) " +
                " when shift=3 and drJam='23:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2)  " +
                " when shift=3 and drJam>'00:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'00:00:00', " +
                " SUBSTRING(TglProduksi,1,8)+trim(cast(DAY(TglProduksi) as nchar))+' '+sdJam) +60 " +
                " when shift=3 and drJam>'23:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2)  " +

                " end Prod_Done " +
                " from ( " +
                " select left(convert(char,A.TglProduksi,23),10)TglProduksi,P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) " +
                " Kubik,I.tebal,I.Lebar,I.Panjang,DATEPART(DAY,drJam)H1,DATEPART(DAY,sdJam)H2,left(convert(char,A.drJam,114),8)drJam, " +
                " left(convert(char,A.sdJam,114),8)sdJam,drJam drJam2,sdJam sdJam2,A.shift " +
                " from BM_Destacking A   " +
                " inner join BM_Plant P on A.PlantID=P.ID   " +
                " inner join BM_PlantGroup G on A.PlantGroupID =G.ID    " +
                " inner join fc_items I on A.ItemID=I.ID   " +
                " where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln and P.PlantName=@line " +
                " group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang,A.sdJam,A.drJam,A.TglProduksi,A.shift ) as x " +
                " ) as AA order by TglProduksi,[shift],Prod_Mulai " +
                #endregion

                #region Temp Table Bdt1 Lama
            //"select * into Bdt1 from ( " +
            //"select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,MenitMulai Prod_Mulai,case when MenitDone=0 then '480' else MenitDone end Prod_Done from ( " +
            //"select *,  " +
            //"case  " +
            //"when waktu1 >= TglProduksi+' '+'23:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu1) " +
            //"when waktu1 >= TglProduksi+' '+'15:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi) as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',waktu1) " +
            //"when waktu1 >= TglProduksi+' '+'07:00:00' and waktu1 < substring(TglProduksi,1,8)+trim(cast(DAY(TglProduksi) as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',waktu1) end MenitMulai " +

            //",case " +
            //"when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari=hari2 and Bulan<12 and  waktu2 < trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
            //"when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari=hari2 and Bulan=12 and  waktu2 < trim(cast(YEAR(TglProduksi)+1 as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
            //"when waktu2 >= TglProduksi+' '+'23:00:00' and hari2>TtlHari  and  waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +
            //"when waktu2 >= TglProduksi+' '+'23:00:00' and TtlHari>hari2 and Bulan<12 and  waktu2 < trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01'+' '+'07:00:00' " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',waktu2) " +

            //"when waktu2 >= TglProduksi+' '+'15:00:00' and waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',waktu2) " +

            //"when waktu2 >= TglProduksi+' '+'07:00:00' and waktu2 < substring(waktu2,1,8)+trim(cast(DAY(waktu2) as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',waktu2)   " +
            //" end MenitDone   " +

            //" from ( " +
            //"select  *, " +

            //"substring(TglProduksi,1,8)+trim(cast(Hari1 as nchar)) +' '+drJam waktu1, " +
            //"case  " +
            //"when Hari2>TtlHari and Bulan<12 then trim(cast(YEAR(TglProduksi) as nchar))+'-'+trim(cast(MONTH(TglProduksi)+1 as nchar))+'-'+'01' +' '+sdJam  " +
            //"when Hari2>TtlHari and Bulan=12 then trim(cast(YEAR(TglProduksi)+1 as nchar))+'-01-01'+' '+sdJam " +
            //"else  " +
            //"substring(TglProduksi,1,8)+trim(cast(Hari2 as nchar)) +' '+sdJam " +
            //"end waktu2 " +

            //"from ( " +
            //"select *,case when H1<>H2 then ((((DATEPART(HOUR,'23:59:59')+1)*60))- (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) )  " +
            //"else (DATEPART(HOUR,drJam)*60)+DATEPART(MINUTE,drJam) end Prod_Mulai , case when H1<>H2 then  (DATEPART(HOUR,sdJam)*60)+(DATEPART(MINUTE,sdJam)) " +
            //"else (DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam)  end Prod_Done, (DATEPART(HOUR,drJam)*60)+(DATEPART(MINUTE,drJam)) Prod_Mulai2, " +
            //"(DATEPART(HOUR,sdJam)*60)+DATEPART(MINUTE,sdJam) Prod_Done2, " +

            //"case  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  drJam>='00:00:00' and drJam<'07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='00:00:00' and drJam<'07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +

            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2   and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  drJam>='07:00:00' and drJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  drJam>='07:00:00' and drJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +

            //"end Hari1, " +
            //"case  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then '0'+trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='00:00:00' and sdJam<='07:00:00' then trim(cast(DAY(TglProduksi)+1 as nchar))  " +

            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)<9  and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2   and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=2  and  sdJam>='07:00:00' and sdJam<='23:59:00' then trim(cast(DAY(TglProduksi) as nchar))  " +
            //"when LEN(trim(cast(DAY(TglProduksi) as nchar)))=1 and day(TglProduksi)>=9 and  sdJam>='07:00:00' and sdJam<='23:59:00' then '0'+trim(cast(DAY(TglProduksi) as nchar))  " +
            //"end Hari2 " +


            //"from ( " +
            //"select left(convert(char,A.TglProduksi,23),10)TglProduksi,P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang, DATEPART(DAY,sdJam)H1,DATEPART(DAY,drJam)H2,left(convert(char,A.drJam,114),8)drJam,left(convert(char,A.sdJam,114),8)sdJam,DAY(EOMONTH(TglProduksi)) AS TtlHari,MONTH(TglProduksi)Bulan " +

            //"from BM_Destacking A  inner join BM_Plant P on A.PlantID=P.ID  inner join BM_PlantGroup G on A.PlantGroupID =G.ID   inner join fc_items I on A.ItemID=I.ID  where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang,A.sdJam,A.drJam,A.TglProduksi " +

            //") as x ) as x1 ) as x2 ) as x3 ) as Final " +
                #endregion

                #region Temp Table Bdt2 lama
            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S into Bdt2 from ( " +
            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
            //"select *,case  " +
            //"when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
            //"case  " +
            //"when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +
            //"from ( " +
            //"select *,case  " +
            //"when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
            //"when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'    " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +
            //"case  " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD    " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
            //"else (TglBreak)+' '+StartBD " +
            //"end StartBD2, " +
            //"case  " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
            //"else (TglBreak)+' '+FinishBD " +
            //"end FinishBD2 " +
            //"from ( " +
            //" select left(convert(char,TglBreak,23),10)TglBreak,@gp1 GP,right(syarat,2)GP2,@line Line, " +
            //"StartBD,FinishBD,GP1 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
            //"TglBreak+' 23:00:00' time1  " +
            //"from TempBreakDown1) as x ) as x1 ) as x2 " +
            //"union all " +
            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
            //"select *,case  " +
            //"when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
            //"case  " +
            //"when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +
            //"from ( " +
            //"select *,case  " +
            //"when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
            //"when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

            //"case " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
            //"else (TglBreak)+' '+StartBD " +

            //"end StartBD2, " +

            //"case  " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
            //"else (TglBreak)+' '+FinishBD " +

            //"end FinishBD2 " +
            //"from ( " +
            //"select left(convert(char,TglBreak,23),10)TglBreak,@gp2 GP,right(syarat,2)GP2,@line Line, " +
            //"StartBD,FinishBD,GP2 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G2)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
            //"TglBreak+' 23:00:00' time1  " +
            //"from TempBreakDown1) as x ) as x1 ) as x2 " +

            //"union all " +

            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
            //"select *,case  " +
            //"when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
            //"case  " +
            //"when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

            //"from ( " +
            //" select *,case  " +
            //"when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
            //"when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'   " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

            //"case  " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD   " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
            //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
            //"else (TglBreak)+' '+StartBD " +

            //" end StartBD2, " +

            //"   case  " +
            //"   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
            //"   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
            //"   when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD       " +
            //"  else (TglBreak)+' '+FinishBD " +

            //" end FinishBD2 " +
            //"  from ( " +
            //"select left(convert(char,TglBreak,23),10)TglBreak,@gp3 GP,right(syarat,2)GP2,@line Line, " +
            //"StartBD,FinishBD,GP3 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G3)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
            //"TglBreak+' 23:00:00' time1  " +
            //"from TempBreakDown1) as x ) as x1 ) as x2 " +

            //"union all " +

            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1,BDNPMS_S from ( " +
            //"select *,case  " +
            //"when StartBD2 >= TglBreak+' '+'23:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00' " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'15:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
            //"when StartBD2 >= TglBreak+' '+'07:00:00' and StartBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00' " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2) end MenitMulai, " +
            //"case  " +
            //"when FinishBD2 >= TglBreak+' '+'23:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'07:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'15:00:00' and FinishBD2  <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'23:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
            //"when FinishBD2 >= TglBreak+' '+'07:00:00' and FinishBD2 <= substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar)) +' '+'15:00:00'  " +
            //"then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2) end MenitDone " +

            //"from ( " +
            //"select *,case  " +
            //"when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
            //"when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
            //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

            //"case  " +
            //" when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
            //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD   " +
            //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
            //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD    " +
            //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD " +
            //" else (TglBreak)+' '+StartBD " +

            //"end StartBD2, " +

            //"case  " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD    " +
            //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD     " +
            //"else (TglBreak)+' '+FinishBD   " +
            //"end FinishBD2 " +
            //"from ( " +
            //"select left(convert(char,TglBreak,23),10)TglBreak,@gp4 GP,right(syarat,2)GP2,@line Line, " +
            //"StartBD,FinishBD,GP4 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan, " +
            //"TglBreak+' 23:00:00' time1  " +
            //"from TempBreakDown1) as x ) as x1 ) as x2 " +
            //") as x " +
                #endregion

                #region Temp Table Bdt2 Baru
            " select TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1 ,BDNPMS_S  into Bdt2  from ( " +
                " select *, " +
                " case   " +
                " when StartBD='07:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',TglBreak+' '+'07:00:00')  " +
                " when StartBD>'07:00:00' and StartBD<'15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2)  " +
                " when StartBD='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',TglBreak+' '+'15:00:00')   " +
                " when StartBD>'15:00:00' and StartBD<'23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                " when StartBD='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',TglBreak+' '+'23:00:00')   " +
                " when (StartBD>'23:00:00' and StartBD<='23:59:59') or  (StartBD>='00:00:00' and StartBD<'07:00:00') then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2)   " +
                " end MenitMulai, " +
                " case  " +
                " when FinishBD='07:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',TglBreak+' '+'15:00:00') " +
                " when FinishBD>'07:00:00' and FinishBD<='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2)  " +
                " when FinishBD='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',TglBreak+' '+'23:00:00')  " +
                " when FinishBD>'15:00:00' and FinishBD<='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                " when FinishBD='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',TglBreak+' '+'23:00:00')   " +
                " when (FinishBD>'23:00:00' and FinishBD<='23:59:59') or  (FinishBD>='00:00:00' and FinishBD<'07:00:00') then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2)  " +
                " end MenitDone " +
                " from (  " +
                //" select *, " +
                //" case " +
                //" when Bulan=12 and HariAsli=TtlHari " +
                //" then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                //" when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2  " +           
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
                //" when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9  " +
                //" then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'  " +
                //" when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  " +
                //" then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'  " +
                //" when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 "+
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' "+
                //" end time2, " +

                "select *, case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00'  " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=1 and month(TglBreak)<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=1 and month(TglBreak)>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +
                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2 then substring(TglBreak,1,8)+''+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+''+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9  then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "end time2,  " +

                //" case  " +
                //" when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari " +
                //" then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 " +           
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                //" then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD     " +
                //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                //" then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                //" when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 " +
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD "+
                //" else (TglBreak)+' '+StartBD end StartBD2, " +

                "case  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-0'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2  " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=2 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=1 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                "else (TglBreak)+' '+StartBD " +
                "end StartBD2, " +

                //" case  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari " +
                //" then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                //" when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 " +           
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD  " +
                //" when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                //" then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
                //" when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                //" then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                //" when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 " +
                //" then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
                //" else (TglBreak)+' '+FinishBD   end FinishBD2 from ( " +
                //" select left(convert(char,TglBreak,23),10)TglBreak,right(syarat,2)GP,Line, StartBD,FinishBD,GP4 GP1, " +
                //" (BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1+BDNPMS_G2+BDNPMS_G3+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) " +
                //" AS TtlHari,MONTH(TglBreak)Bulan  from TempBreakDown1)  as x ) "+

                "case " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-0'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2  " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=2 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=1 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
                "else (TglBreak)+' '+FinishBD  " +
                "end FinishBD2  " +
                "from ( select left(convert(char,TglBreak,23),10)TglBreak,right(syarat,2)GP,Line, StartBD,FinishBD,GP4 GP1, " +
                "(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari, " +
                "MONTH(TglBreak)Bulan  from TempBreakDown1)  as x ) " +

                " as x1 ) as x2 " +
                #endregion

                #region New Old
            //"select Prod_Mulai,case when Prod_Done=479 then 480 else Prod_Done end Prod_Done,TglProduksi Tgl,GP,Kubik,tebal,lebar,panjang,Line into compare1 " +
            //"from Bdt1  " +

            //"select MenitMulai,case when MenitDone=0 then 480 else MenitDone end MenitDone,TglBreak Tgl,GP,GP2,BDNPMS_G1 BDT_NonSch,BDNPMS_S BDT_Sch,GP1 BDTTime into compare2 from ( " +
            //"select TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S from Bdt2 group by TglBreak,GP,GP2,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1 ) as x " +

            //"select A.Tgl,A.GP,isnull(BDTTime,0)BDTTime,A.Prod_Mulai,A.Prod_Done,A.Kubik,Tebal,Lebar,Panjang,Line, " +
            //"case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done then B.MenitMulai else '0' end MulaiBDT, " +
            //"case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done then B.MenitDone else '0' end AkhirBDT,isnull(B.BDT_Sch,0)BDT_Sch,isnull(B.BDT_NonSch,0)BDT_NonSch into tempDataBdt1 " +
            //"from  compare1 A  " +
            //"left join compare2 B ON A.Tgl=B.Tgl and A.GP=B.GP and A.GP=B.GP2 where A.Kubik>0 " +

            //"select Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt01 from ( " +

            //"select Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,case when MulaiBDT>0 and AkhirBDT>0 then BDT_Sch else 0 end BDT_Sch, " +
            //"case when MulaiBDT=0 and AkhirBDT=0 then 0 " +
            //"else BDT_NonSch end BDT_NonSch,Tebal,Lebar,Panjang,Line from tempDataBdt1 ) as x group by Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line " +

            //"select Tgl,GP,BDTTime,Prod_Mulai,Prod_done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt2 " +
            //"from tempDataBdt01 " +
            //"group by Tgl,GP,BDTTime,Prod_Mulai,Prod_done,Tebal,Lebar,Panjang,Line " +

            //"select Tgl,GP,BDTTime,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt3 from tempDataBdt2 group by Tgl,GP,BDTTime,Tebal,Lebar,Panjang,Line " +
            //"select  Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line  into tempDataBdt4 from tempDataBdt3 group by Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,Tebal,Lebar,Panjang,Line  " +
            //"select *,Prod_Done-Prod_Mulai- BDT_NonSch-BDT_Sch WaktuPerMenit into tempDataBdt5 from tempDataBdt4 A " +
            //"select A.Line,B.Deskripsi,A.Tgl,A.GP,A.BDTTime,A.Prod_Mulai,A.Prod_Done,A.MulaiBDT,A.AkhirBDT,A.BDT_Sch,A.BDT_NonSch,A.WaktuPerMenit into tempDataBdt6 from tempDataBdt5  A left join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang " +
            //"select Tgl,Line,Deskripsi,GP,sum(WaktuPerMenit)[Waktu/(Menit)] into tempDataBdt06 from tempDataBdt6 group by Tgl,Line,Deskripsi,GP " +
            //"select * into LastData from tempDataBdt06 " +
                #endregion

                #region New
            " select Prod_Mulai,case when Prod_Done=479 then 480 else Prod_Done end Prod_Done,TglProduksi Tgl,GP,Kubik,tebal,lebar,panjang,Line into compare1 " +
                " from Bdt1 " +
                " select MenitMulai,case when MenitDone=0 then 480 else MenitDone end MenitDone,TglBreak Tgl,GP,BDNPMS_G1 BDT_NonSch,BDNPMS_S BDT_Sch,GP1 BDTTime " +
                " into compare2 from ( " +
                " select TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S from Bdt2 " +
                " group by TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1 ) as x " +
                " select A.Tgl,A.GP,isnull(BDTTime,0)BDTTime,A.Prod_Mulai,A.Prod_Done,A.Kubik,Tebal,Lebar,Panjang,Line, " +
                " case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done then B.MenitMulai else '0' end MulaiBDT, " +
                " case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done then B.MenitDone else '0' end AkhirBDT, " +
                " isnull(B.BDT_Sch,0)BDT_Sch,isnull(B.BDT_NonSch,0)BDT_NonSch into tempDataBdt1 " +
                " from  compare1 A " +
                " left join compare2 B ON A.Tgl=B.Tgl and A.GP=B.GP  where  A.Tgl in (select TglBreak from TempBreakDown1 where (GP1+GP2+GP3+GP4)>0) " +
                " select Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt01 from ( " +
                " select Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,case when (MulaiBDT>0 or AkhirBDT>0) then BDT_Sch else 0 end BDT_Sch, " +
                " case when MulaiBDT=0 and AkhirBDT=0 then 0 " +
                " else BDT_NonSch end BDT_NonSch,Tebal,Lebar,Panjang,Line from tempDataBdt1 ) as x " +
                " group by Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line " +
                " select Tgl,GP,BDTTime,Prod_Mulai,Prod_done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch," +
                " Tebal,Lebar,Panjang,Line into tempDataBdt2 " +
                " from tempDataBdt01 " +
                " group by Tgl,GP,BDTTime,Prod_Mulai,Prod_done,Tebal,Lebar,Panjang,Line " +
                " select Tgl,GP,BDTTime,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT, " +
                " sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line " +
                " into tempDataBdt3 from tempDataBdt2 group by Tgl,GP,BDTTime,Tebal,Lebar,Panjang,Line " +
                " select  Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch, " +
                " sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line  into tempDataBdt4 " +
                " from tempDataBdt3 group by Tgl,GP,BDTTime,Prod_Mulai,Prod_Done,Tebal,Lebar,Panjang,Line " +

                " select *,Prod_Done-Prod_Mulai/**-BDT_Sch-BDT_NonSch**/ WaktuPerMenit into tempDataBdt5 from tempDataBdt4 A " +

                " select A.Line,B.Deskripsi,A.Tgl,A.GP,A.BDTTime,A.Prod_Mulai,A.Prod_Done,A.MulaiBDT,A.AkhirBDT,A.BDT_Sch,A.BDT_NonSch,A.WaktuPerMenit " +
                " into tempDataBdt6 from tempDataBdt5  A left join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang " +
                " " + logic2 + " " +

                " select Tgl,Line,Deskripsi,GP,sum(WaktuPerMenit)[Waktu/(Menit)] into tempDataBdt06 from tempDataBdt6 group by Tgl,Line,Deskripsi,GP " +

                " select * into LastData from tempDataBdt06 " +

                " select Tgl,Line,Deskripsi,GP,[Waktu/(Menit)]-BDT_Sch [Waktu/(Menit)] into LastData_OP from ( " +
                " select *,isnull((select  BDT_Sch+isnull(BDT_NonSch,0) from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and " +
                " A.rowstatus>-1 and A.Noted='O'),0)BDT_Sch from LastData B ) as x " +


                #endregion

                "select * into LastData2 from ( " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData_OP where GP=@gp1  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData_OP where GP=@gp2  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData_OP where GP=@gp3  group by Line,GP,Deskripsi union all " +
                "select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData_OP where GP=@gp4  group by Line,GP,Deskripsi ) as x " +

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
                //"avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], "+
                "cast(sum([OutPut(M3)])/cast((sum([Waktu(Menit)])/cast(8 as decimal(18,2))/cast(60 as decimal(18,2))) as decimal(18,3)) as decimal(18,1)) [OutPut(M3/Shift)], " +
                //"avg(konversi)konversi, "+
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
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare1]') AND type in (N'U')) DROP TABLE [dbo].[compare1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare2]') AND type in (N'U')) DROP TABLE [dbo].[compare2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt1]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt1]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt01]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt01] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt2]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt2]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt3]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt3]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt4]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt4] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt5]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt5] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt05]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt05] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt6]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt6] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt06]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt06] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_OP]') AND type in (N'U')) DROP TABLE [dbo].[LastData_OP] ";



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
        private void loadDynamicGrid300(string tgl1, bool reset)
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
                "dataProduksi5 as (select *,case when Menit=479 then 480-Sch-NonSch else Menit-Sch-NonSch end Waktu2 from dataProduksi4 ), " +
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

            if (reset == false && cekdataoutput(thnbln) == 0)
                reset = true;
            if (reset == true)

                #region Ambil Data
                strSQL =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]   " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]   " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO]    " +

    " declare @thnbln  varchar(6) " +
    " declare @line varchar(max)  " +
    " declare @unitkerjaID varchar(6) " +
    " declare @target int  " +

    "set @thnbln='" + thnbln + "' " +
    "set @line='line " + ddlLine.SelectedValue + "' " +
    "set @unitkerjaID='" + users.UnitKerjaID + "' " +

    " if @unitkerjaID='1' begin " +
    " if @line='line 1' begin set @target='27' end " +
    " if @line='line 2' begin set @target='38' end " +
    " if @line='line 3' begin set @target='40' end  " +
    " if @line='line 4' begin set @target='40' end end  " +

    " if @unitkerjaID = '7' begin begin  " +
    " set @target='40' end end " +

    " if @unitkerjaID = '13' begin begin  " +
    " set @target='60' end end   " +
    "  " +
    " update bm_bdtoutput set rowstatus=-1 where rowstatus>-1 and ThnBln=@thnbln  and line=@line " +
    "  " +
    " /** Temp Table tempBreakBMPO **/ " +
    " SELECT * into tempBreakBMPO From( select  " +
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
    " ,* from ( select cast(DATEPART(DAY,TglBreak) as char)H,DAY(EOMONTH(TglBreak))Max_H,line,RIGHT(Syarat,1)GP, left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2  and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  case when Pinalti=0  then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U,  case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3,  case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff,Ketebalan  from (   select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus,  1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))   from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS,  StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, 0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  then menit else 0 end  BDNPMS_E,  case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group]  from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end   BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti, (select lokasiproblem  from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP,  case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff,Ketebalan  from(  select  isnull(xx.minutex,0) as menit,*  from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) end finbd, FinaltyBD,TglBreak,Ketebalan,Pinalti  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B  where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line ) as x) BM order by TglBreak  " +
    "  " +
    " declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) declare @K char  " +
    "  " +
    " set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID)  " +
    "  " +
    " if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end  " +
    " if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end  " +
    " if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
    " if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
    " if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
    " if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

    " /** Temp Table TempBreakDown1_P **/ " +
    " SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
    " CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) ELSE GP1 END GP1,  " +
    " CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 THEN (GP2-480) ELSE GP2  END GP2, " +
    " CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3  END GP3,  " +
    " CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1  " +
    " From tempBreakBMPO " +
    " where RowStatus=0 order by TglBreak,StartBD,line  " +
    "  " +
    " ;with  " +
    " dataProduksi0 as (select left(convert(char,TglProduksi,112),8)Tgl,left(convert(char,TglProduksi,120),10)TglProduksi,PlantGroupID,PlantID, " +
    " case when DATEDIFF(MINUTE,drJam,sdJam2)=479 then 480 else DATEDIFF(MINUTE,drJam,sdJam2) end Menit,ItemID from ( " +
    " select TglProduksi,PlantGroupID,PlantID,drJam,ItemID,case when left(convert(char,sdJam,108),8)='06:59:00' " +
    " then left(convert(char,sdJam,112),8)+' '+'07:00:00' else sdJam end sdJam2 " +
    " from BM_Destacking where left(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and " +
    " LokasiID not in (select ID from FC_Lokasi where lokasi like'%adj%') and PlantGroupID in (select ID from BM_PlantGroup) " +
    " group by TglProduksi,PlantGroupID,PlantID,ItemID,sdJam,drJam ) as x), " +
    " dataProduksi1 as (select A.*,isnull(B.Kategori,'')Ketebalan from dataProduksi0 A left join FC_Items B ON A.ItemID=B.ID where B.RowStatus>-1), " +
    " dataProduksi2 as (select A.*,RIGHT(TRIM(B.[Group]),1)GP,RIGHT(TRIM(C.PlantName),1)Line from dataProduksi1 A inner join BM_PlantGroup B ON A.PlantGroupID=B.ID inner join BM_Plant C ON C.ID=A.PlantID), " +
    " dataProduksi20 as (select Tgl,TglProduksi,PlantGroupID,PlantID,sum(Menit)Menit,Ketebalan,GP,Line from dataProduksi2 group by  Tgl,TglProduksi,PlantGroupID,PlantID,Ketebalan,GP,Line), " +
    " dataProduksi3 as (select Tgl,TglProduksi/**,Waktu**/,Menit,GP,Line,Ketebalan from dataProduksi20 group by Tgl,TglProduksi/**,Waktu**/,GP,Line,Ketebalan,Menit), " +

    " dataProduksi30 as (select TglBreak,sum(Selisih)NonSch,sum(BDNPMS_S)Sch,GP,Ketebalan,RIGHT(line,1)Line,isnull(Pinalti,0)Pinalti from ( " +
    " select case  " +
    " when StartBD<FinishBD and Pinalti>0 and Noted='NonSch' then cast(DATEDIFF(MINUTE,StartBD,FinishBD)*(Pinalti/100)  as decimal(18,0))  " +
    " when StartBD<FinishBD and Pinalti=0 and Noted='NonSch' then DATEDIFF(MINUTE,StartBD,FinishBD)   " +
    " when StartBD>FinishBD and Pinalti>0 and Noted='NonSch' then cast(DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD)*(Pinalti/100)  as decimal(18,0)) " +
    " when StartBD>FinishBD and Pinalti=0 and Noted='NonSch' then DATEDIFF(MINUTE,TglBreak+' '+StartBD,TglBreak2+' '+FinishBD)  else 0 end Selisih,* from ( " +
    " select case when Panjang>2 and left(syarat,2)<>@K+'H' then 'NonSch' when Panjang=2 then 'Nonsch' when Panjang>2 and  left(syarat,2)=@K+'H' then 'Sch' end Noted ,* from ( " +
    " select LEN(Syarat)Panjang,* from tempBreakBMPO where line=@line and left(convert(char,cast(TglBreak as datetime),112),6)=@thnbln ) as x ) as xx ) as x group by TglBreak,GP,Ketebalan,Line,Pinalti),  " +

    " dataProduksi4 as (select *, " +
    " isnull((select sum(NonSch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan " + logic4 + "),0)NonSch, " +
    " isnull((select sum(Sch) from dataProduksi30 A1 where A1.TglBreak=A.TglProduksi and A1.GP=A.GP and A1.Ketebalan=A.Ketebalan " + logic4 + "),0)Sch  " +
    " from dataProduksi3 A ), " +
    " dataProduksi5 as (select *,case when Menit=479 then 480-Sch-NonSch else Menit-Sch-NonSch end Waktu2 from dataProduksi4 ), " +
    " dataProduksi6 as (select TglProduksi Tgl,'Line'+ ' ' + Line Line,Ketebalan Deskripsi,@K+GP GP,Waktu2 [Waktu/(Menit)] from dataProduksi5 ) " +
    " /** Temp data LastData **/ " +
    " select * into LastData from dataProduksi6 where Line=@line " +
    "  " +

    "  " +
    " /** Temp data LastData2 **/ " +
    " select * into LastData2 from ( select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp1  group by Line,GP,Deskripsi  " +
    " union all  " +
    " select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp2  group by Line,GP,Deskripsi  " +
    " union all  " +
    " select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp3  group by Line,GP,Deskripsi  " +
    " union all  " +
    " select Line,GP,Deskripsi,sum([Waktu/(Menit)])[Waktu(Menit)] from LastData where GP=@gp4  group by Line,GP,Deskripsi ) as x  " +
    "  " +
    " /** Temp data tempBDMS0 **/ " +
    " select distinct line,tglbreak,right(Syarat,2) Syarat,bdnpms_s into tempBDMS0 from tempBreakBMPO where bdnpms_s>0   " +
    "  " +
    " /** Temp data tempBDMS **/ " +
    " select line,tglbreak,Syarat,sum(bdnpms_s)bdnpms_s into tempBDMS from tempBDMS0 group by line,tglbreak,Syarat  " +
    "  " +
    " declare @query varchar(max)  " +
    " set @query='  " +
    " select distinct line,tglbreak,  " +
    " case when groupoff <>'''+@gp1+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp1 + ''' ),0) else 0 end gp10,  " +
    " case when groupoff <>'''+@gp2+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp2 + ''' ),0) else 0 end gp20, " +
    " case when groupoff <>'''+@gp3+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp3 + ''' ),0) else 0 end gp30,  " +
    " case when groupoff <>'''+@gp4+''' then 480 -isnull((select BDNPMS_S from tempbdms where TglBreak=A.TglBreak and line='''+ @line + ''' and syarat='''+ @gp4 + ''' ),0) else 0 end gp40,  " +
    " case when groupoff <>'''+@gp1+''' then gp1 else 0 end gp1, " +
    " case when groupoff <>'''+@gp2+''' then gp2 else 0 end gp2,  " +
    " case when groupoff <>'''+@gp3+''' then gp3 else 0 end gp3, " +
    " case when groupoff <>'''+@gp4+''' then gp4 else 0 end gp4,groupoff into tempBDT1  " +
    " from tempBreakBMPO A order by TglBreak'   " +
    " execute (@query)  " +
    "  " +
    " set @query=  'select * into tempBDT2  " +
    " from  (select line,'+@gp1 +' GP,sum(A.gp10) BDTProduktifitas,sum(A.gp1) BDTOutPut from (select line,''' + @gp1 + ''' '+  @gp1 + ',gp10,gp1 from tempBDT1)A group by A.line,A.' +  @gp1 + ' union all ' +  " +
    " 'select line,'+@gp2 +' GP,sum(B.gp20) BDTProduktifitas,sum(B.gp2) BDTOutPut from (select line,''' + @gp2 + ''' '+  @gp2 + ',gp20,gp2 from tempBDT1)B group by B.line,B.' +  @gp2 + ' union all ' +  " +
    " 'select line,'+@gp3 +' GP,sum(B.gp30) BDTProduktifitas,sum(B.gp3) BDTOutPut from (select line,''' + @gp3 + ''' '+  @gp3 + ',gp30,gp3 from tempBDT1)B group by B.line,B.' +  @gp3 + ' union all ' +  " +
    " 'select line,'+@gp4 +' GP,sum(B.gp40) BDTProduktifitas,sum(B.gp4) BDTOutPut from (select line,''' + @gp4 + ''' '+  @gp4 + ',gp40,gp4 from tempBDT1)B group by B.line,B.' +  @gp4 + ')D'  " +
    " execute (@query) " +
    "  " +
    " select * into OuputProduksiPO from ( select P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang  from BM_Destacking A inner join BM_Plant P on A.PlantID=P.ID inner join BM_PlantGroup G on A.PlantGroupID =G.ID  inner join fc_items I on A.ItemID=I.ID where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln  and P.PlantName=@line group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang )Z order by GP,tebal " +
    "  " +
    " select Line,GP,Deskripsi,avg(BDTProduktifitas) BDTProduktifitas,avg(BDTOutPut) BDTOutPut,sum(kubik)kubik,TargetM3 into tempBDT3 from(  " +
    " select A.Line,A.GP,A.BDTProduktifitas,A.BDTOutPut,Kubik , " +
    " (select top 1 Deskripsi from bm_stdtargetoutput where rowstatus>-1 and tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") Deskripsi , " +
    " (select top 1 targetm3 from bm_stdtargetoutput where rowstatus>-1 and tebal=B.tebal and lebar=B.lebar and Panjang=B.Panjang " + logic1 + ") TargetM3  " +
    " from tempBDT2 A " +
    " inner join OuputProduksiPO B on A.Line=B.line and A.GP=B.GP)A0  " +
    " group by Line,GP,Deskripsi,TargetM3 order by Line,GP,Deskripsi   " +
    "  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen into tempBDTP from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from(  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)],  " +
    " case when targetm3=0 then 0 else[Produktifitas(M3/Shift)]*@target/targetm3 end konversi,  " +
    " case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
    " case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)],  " +
    " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)], TargetM3 from(  " +
    " select '1'Urut,Line,GP, (select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D  " +
    "  " +
    " union all  " +

    //" select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (   "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)], konversi,Persen from( "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)],  "+
    //" case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi,  "+
    //" case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from ( "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], kubik [Produktifitas(M3)],  "+
    //" case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)] ,TargetM3 from(  "+
    //" select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi)  [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E  "+
    //" group by  Line,GP,Urut  "+

    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan,[Target(M3)],[OutPut(M3)],[OutPut(M3)]/([Waktu(Menit)]/8/60)[OutPut(M3/Shift)],Konversi,Persen from (  " +
    " select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)],   " +
    " sum(konversi)Konversi,   " +
    " case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from (   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], konversi * kubik/(select sum(Kubik) from tempBDT3 D1 where D1.Line=D.Line and D1.GP=D.GP)Konversi,Persen from(   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],    " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,   case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,TargetM3,kubik from (   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], kubik [OutPut(M3)],   case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)] ,TargetM3,kubik from(   select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi)  [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E   group by  Line,GP,Urut ) as x  " +

    " union all  " +

    " select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([Produktifitas(M3)])[Produktifitas(M3)], avg([Produktifitas(M3/Shift)])[Produktifitas(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 else Sum([Produktifitas(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)], [Produktifitas(M3/Shift)], konversi,Persen from( " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[Produktifitas(M3)],[Produktifitas(M3/Shift)],  " +
    " case when targetm3=0 then 0 else [Produktifitas(M3/Shift)]*@target/targetm3 end konversi,  " +
    " case when [Target(M3)]=0 then 0 else [Produktifitas(M3)]/[Target(M3)]*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, " +
    " case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [Produktifitas(M3)],  " +
    " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [Produktifitas(M3/Shift)],TargetM3 from(  " +
    " select '2'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E  " +
    " group by  Line,Ketebalan,Urut  " +
    "  " +
    " union all  " +
    "  " +
    " select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)],sum(konversi)konversi, case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from (  " +
    " select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)], (sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60)*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,  " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan,  " +
    " case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)],   " +
    " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3  from(  " +
    " select '3'Urut,Line,GP, (select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3,ketebalan ) as x group by  Line,Urut,GP,ketebalan)F order by Urut,line,GP " +

    " ;select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen into tempBDTO from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from( " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan,  " +
    " case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], kubik [OutPut(M3)], " +
    " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from(  " +
    " select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik  from tempBDT3 A)B)C)D  " +

    " union all  " +

    //" select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''''Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)], avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi,case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from ( "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from(  "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  "+
    //" case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, "+
    //" case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from ( "+
    //" select Urut,Line,GP,[Waktu(Menit)],Ketebalan,  "+
    //" case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)],  "+
    //" case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3 from(  "+
    //" select '1'Urut,Line,GP, (select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E  "+
    //" group by  Line,GP,Urut  "+

    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan,[Target(M3)],[OutPut(M3)],[OutPut(M3)]/([Waktu(Menit)]/8/60)[OutPut(M3/Shift)],Konversi,Persen from (  " +
    " select Urut,Line,GP + '_Total'  GP,sum([Waktu(Menit)])[Waktu(Menit)],''  Ketebalan, Sum([Target(M3)])[Target(M3)],Sum([OutPut(M3)])[OutPut(M3)],   " +
    " sum(konversi)Konversi,   " +
    " case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from (   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], konversi * kubik/(select sum(Kubik) from tempBDT3 D1 where D1.Line=D.Line and D1.GP=D.GP)Konversi,Persen from(   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],    " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,   case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,TargetM3,kubik from (   " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)], kubik [OutPut(M3)],   case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)] ,TargetM3,kubik from(   select '1'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi)  [Waktu(Menit)] ,Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E   group by  Line,GP,Urut ) as x  " +


    " union all  " +

    " select Urut,Line,'SubTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], avg([OutPut(M3/Shift)])[OutPut(M3/Shift)], avg(konversi)konversi, case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen from(  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi, " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan, case when targetm3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)],  " +
    " case when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0) then 0 when ([Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0) then kubik/([Waktu(Menit)]/8/60)  else 0 end [OutPut(M3/Shift)],TargetM3 from(  " +
    " select '2'Urut,Line,GP,(select  [Waktu(Menit)] from LastData2 AA where AA.line=A.Line and AA.GP=A.GP and AA.Deskripsi=A.deskripsi) [Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E  " +
    " group by  Line,Ketebalan,Urut " +
    "  " +
    " union all  " +
    "  " +
    " select Urut,Line,GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)] , sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)],sum(konversi)konversi,  " +
    " case when Sum([Target(M3)])=0 then 0 else Sum([OutPut(M3)])/Sum([Target(M3)])*100 end Persen  from (  " +
    " select Urut,Line,'GrandTotal'  GP,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, Sum([Target(M3)])[Target(M3)], sum([OutPut(M3)])[OutPut(M3)],   sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60) [OutPut(M3/Shift)], (sum([OutPut(M3)])/(sum([Waktu(Menit)])/8/60)*@target/TargetM3) * Sum([OutPut(M3)])/(select sum(kubik) from tempBDT3)  konversi,TargetM3 from ( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen ,TargetM3 from( select Urut,Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
    " case when targetm3=0 then 0 else [OutPut(M3/Shift)]*@target/targetm3 end konversi,  " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen ,TargetM3 from (  " +
    " select Urut,Line,GP,[Waktu(Menit)],Ketebalan,  " +
    " case when TargetM3=0 then 0 else [Waktu(Menit)]*TargetM3/8/60 end [Target(M3)],kubik [OutPut(M3)],  " +
    " case when [Waktu(Menit)]>0 then kubik/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],TargetM3  from(  " +
    " select '3'Urut,Line,GP, (select [Waktu(Menit)] from LastData2 A1 where A1.Deskripsi=A.Deskripsi and A1.GP=A.GP)[Waktu(Menit)], Deskripsi Ketebalan,TargetM3,kubik from tempBDT3 A)B)C)D)E group by  Line,Urut ,TargetM3,ketebalan ) as x group by  Line,Urut,GP,ketebalan)F order by Urut,line,GP " +

    " ;if isnull((select count(id) from bm_bdtoutput where rowstatus >-1 and thnbln=@thnbln and line=@line),0)=0 begin  " +
    " insert bm_bdtoutput select @thnbln ThnBln, Line, GP, [Waktu(Menit)], Ketebalan, [Target(M3)], [Output(M3)], [OutPut(M3/Shift)], konversi, Persen,0 rowstatus from tempBDTO end " +

    " ;with OutPut as( select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen,stdTarget from(  " +
    " select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)],  " +
    //" case when stdTarget=0 then 0 else [OutPut(M3/Shift)]*@target/stdTarget end konversi,  "+
    " case when stdTarget=0 then 0 else konversi end konversi, " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen,stdTarget from ( " +
    " select Line,GP,[Waktu(Menit)],Ketebalan, " +
    " /**case when stdTarget=0 then 0 else [Waktu(Menit)]*stdTarget/8/60 end [Target(M3)]**/[Target(M3)],[OutPut(M3)] [OutPut(M3)], " +
    " case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0 then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)], stdTarget,konversi from( " +
    " select Line,GP,[Waktu(Menit)],Ketebalan,[OutPut(M3)],(select top 1 targetm3 from bm_stdTargetoutput where rowstatus>-1 and deskripsi=A.ketebalan " + logic3 + ")stdTarget,[Target(M3)],konversi from BM_BDTOutPut A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D ),  " +

    " OutPutCalc as ( select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3/Shift)], konversi,Persen from OutPut  " +

    " Union all  " +

    " select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],  " +
    " case when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)=0 then 0 when [Waktu(Menit)]>0 and ([Waktu(Menit)]/8/60)>0 then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)], konversi, " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from(  " +
    " select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)],  sum(konversi*konversi2)Konversi,''Persen from(  " +
    " select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
    //" case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * @target/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut where GP=A.GP)) end konversi "+
    " case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else ([OutPut(M3)]/(select sum([OutPut(M3)]) from OutPut A1 where A1.GP=A.GP)) end Konversi2,konversi " +
    " from OutPut A )B group by Line,GP)C  " +
    "  " +
    " union all  " +
    "  " +
    " select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],  " +
    " case when [Waktu(Menit)]=0 then 0 when ([Waktu(Menit)]/8/60)=0 then 0 else [OutPut(M3)]/([Waktu(Menit)]/8/60) end [OutPut(M3/Shift)],  konversi, " +
    " case when [Target(M3)]=0 then 0 else [OutPut(M3)]/[Target(M3)]*100 end Persen from(  " +
    " select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)],  sum(konversi)Konversi,''Persen from(  " +
    " select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], case when stdTarget=0 then 0 when [OutPut(M3)]=0 then 0 else [OutPut(M3/Shift)] * @target/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut )) end konversi from OutPut A )B group by Line)C )  " +
    "  " +
    " UPDATE BM_BDTOutPut   " +
    " SET Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], [OutPut(M3)]=b.[OutPut(M3)], [OutPut(M3/Shift)]=b.[OutPut(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
    " FROM   OutPutCalc b  " +
    " WHERE  BM_BDTOutPut.Line = b.Line and BM_BDTOutPut.gp=b.GP and BM_BDTOutPut.ketebalan=b.Ketebalan  and BM_BDTOutPut.thnbln=@thnbln and BM_BDTOutPut.Line=@Line and BM_BDTOutPut.rowstatus>-1  " +

    " select Line,GP,[Waktu(Menit)],Ketebalan,[Target(M3)],[Output(M3)],[Output(M3/Shift)],konversi,Persen from ( " +
    " select case when GP='SubTotal' then '2' when GP='GrandTotal' then '3' else '1' end Urut,isnull(Line,'')Line,isnull(GP,'')GP,isnull([Waktu(Menit)],0)[Waktu(Menit)],isnull(Ketebalan,'')Ketebalan,isnull([Target(M3)],0)[Target(M3)], " +
    " isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)],isnull(konversi,0)konversi,isnull(Persen,0)Persen from bm_bdtoutput  where rowstatus >-1 and ThnBln=@thnbln  and line=@line " +
    " ) as x order by Urut,line,GP " +


    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0]   " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1]   " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP]  " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData]') AND type in (N'U')) DROP TABLE [dbo].[LastData] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2]') AND type in (N'U')) DROP TABLE [dbo].[LastData2] " +
    " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO]  ";


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
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "Persen")
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "konversi")
                        bfield.DataFormatString = "{0:N1}";
                    if (bfield.HeaderText.Trim() == "Output(M3/Shift)")
                        bfield.DataFormatString = "{0:N0}";
                    if (bfield.HeaderText.Trim() == "Output(M3)")
                        bfield.DataFormatString = "{0:N0}";

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

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void LoadPostingSarmut()
        {
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ZetroView zl = new ZetroView();
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
               "update SPD_TransPrs set actual= " +
                "isnull((select round((OutPut(M3)/[Target(M3)])*100,2) Actual from ( " +
                "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum(OutPut(M3)),0)OutPut(M3) from ( " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
                "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1),0) " +
                "where SarMutPID in ( " +
                "select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Akumulasi line berjalan' and OnSystem=1) " +
                "and Tahun=substring('" + thnbln + "',1,4) and Bulan=substring('" + thnbln + "',5,2) and Approval=0 and rowstatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
        }

        protected void LoadPostingBukuBesarISO()
        {
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string line = ddlLine.SelectedValue;

            BDT_Out dbdt = new BDT_Out();
            FacadeBDT_OutPut fbdt = new FacadeBDT_OutPut();
            decimal OutProduksi_All = fbdt.RetrieveOutProduksi_All(thnbln);


            //ZetroView zl = new ZetroView();
            //zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery =
            //   "update SPD_TransPrs set actual= " +
            //    "isnull((select round((OutPut(M3)/[Target(M3)])*100,2) Actual from ( " +
            //    "select isnull(sum([Target(M3)]),0)[Target(M3)],isnull(sum(OutPut(M3)),0)OutPut(M3) from ( " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 1'  and RowStatus>-1 and GP='SubTotal' union all " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 2'  and RowStatus>-1 and GP='SubTotal' union all " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 3'  and RowStatus>-1 and GP='SubTotal' union all " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 4'  and RowStatus>-1 and GP='SubTotal' union all " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 5'  and RowStatus>-1 and GP='SubTotal' union all " +
            //    "select * from bm_bdtoutput where ThnBln='" + thnbln + "'   and line='Line 6'  and RowStatus>-1 and GP='SubTotal') as x ) as x1),0) " +
            //    "where SarMutPID in ( " +
            //    "select ID from SPD_Perusahaan where DeptID=1 and RowStatus>-1 and SarMutPerusahaan='Akumulasi line berjalan' and OnSystem=1) " +
            //    "and Tahun=substring('" + thnbln + "',1,4) and Bulan=substring('" + thnbln + "',5,2) and Approval=0 and rowstatus>-1 ";
            //SqlDataReader sdr = zl.Retrieve();
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
                    "konversi, Persen,isnull((select top 1 targetm3 from BM_StdTargetOutPut where rowstatus>-1 and deskripsi=A.ketebalan),0) stdtarget " +
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
        protected void Button4_Click(object sender, EventArgs e)
        {
            //LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
            LblTgl1.Text = ddlBulan.SelectedItem.ToString() + " " + ddTahun.SelectedItem.ToString();//LblTgl1.Text = ddlBulan.SelectedItem.ToString();
            LblLine.Text = ddlLine.SelectedValue;
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), true);
        }
        protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            BDT_Out ba = (BDT_Out)e.Item.DataItem;
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
                "isnull([Target(M3)],0)[Target(M3)],isnull([Output(M3)],0)[Output(M3)],isnull([Output(M3/Shift)],0)[Output(M3/Shift)], " +
                "isnull(konversi,0)konversi,isnull(Persen,0)Persen,isnull((select top 1 targetm3 from BM_StdTargetOutPut where rowstatus>-1 and deskripsi=A.ketebalan),0) stdtarget " +
                "from bm_bdtOutput A where rowstatus>-1 and ThnBln=" + thnbln + "  and line='line " + ddlLine.SelectedValue + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BDT_Out
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Line = sdr["Line"].ToString(),
                        GP = sdr["GP"].ToString(),
                        WaktuMenit = Convert.ToInt32(sdr["Waktu(Menit)"].ToString()),
                        Ketebalan = sdr["Ketebalan"].ToString(),
                        TargetM3 = Convert.ToDecimal(sdr["Target(M3)"].ToString()),
                        OutputM3 = Convert.ToDecimal(sdr["Output(M3)"].ToString()),
                        OutputM3Shift = Convert.ToDecimal(sdr["Output(M3/Shift)"].ToString()),
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
                zl.CustomQuery = "update bm_bdtOutput set [waktu(menit)] =" + pattern.Replace(decimal.Parse(txtWaktu.Text).ToString(), "") + " where ID=" + lblID.ToolTip;
                SqlDataReader sdr = zl.Retrieve();
                i++;
            }
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @thnbln  varchar(6); " +
                "declare @line varchar(max); " +
                "set @thnbln='" + thnbln + "' ; " +
                "set @line='line " + ddlLine.SelectedValue + "' ; " +
                "with OutPut as( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], konversi,Persen,stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)],[OutPut(M3/Shift)], " +
                "[OutPut(M3/Shift)]*40/stdTarget konversi,[OutPut(M3)]/[Target(M3)]*100 Persen,stdTarget from ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[Waktu(Menit)]*stdTarget/8/60 [Target(M3)],[OutPut(M3)] [OutPut(M3)], " +
                "case when [Waktu(Menit)]>0 then [OutPut(M3)]/([Waktu(Menit)]/8/60) else 0 end [OutPut(M3/Shift)],stdTarget from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan,[OutPut(M3)],(select top 1 targetm3 from bm_stdTargetoutput where rowstatus>-1 and deskripsi=A.ketebalan)stdTarget " +
                "from BM_BDTOutPut A where rowstatus>-1 and LEN(rtrim(GP))=2 and thnbln=@thnbln and line=@line)B)C)D " +
                "), " +
                "OutPutCalc as ( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3/Shift)], konversi,Persen from OutPut " +
                "Union all " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3)]/([Waktu(Menit)]/8/60) [OutPut(M3/Shift)],  " +
                "konversi,[OutPut(M3)]/[Target(M3)]*100  Persen from( " +
                "select Line,rtrim(GP) +'_Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "[OutPut(M3/Shift)] * 40/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut where GP=A.GP))  konversi from OutPut A " +
                ")B group by Line,GP)C " +
                "union all " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], [OutPut(M3)]/([Waktu(Menit)]/8/60) [OutPut(M3/Shift)],  " +
                "konversi,[OutPut(M3)]/[Target(M3)]*100  Persen from( " +
                "select Line,'Total' GP ,sum([Waktu(Menit)])[Waktu(Menit)],'''' Ketebalan, sum([Target(M3)])[Target(M3)],sum([OutPut(M3)])[OutPut(M3)], " +
                " sum(konversi)Konversi,''Persen from( " +
                "select Line,GP,[Waktu(Menit)],Ketebalan, [Target(M3)],[OutPut(M3)], " +
                "[OutPut(M3/Shift)] * 40/stdTarget * ([OutPut(M3)]/ (select sum([OutPut(M3)]) from OutPut ))  konversi from OutPut A " +
                ")B group by Line)C ) " +
                "UPDATE BM_BDTOutPut  " +
                "SET   Line=b.Line,GP =b.GP,[Waktu(Menit)]=b.[Waktu(Menit)],Ketebalan=b.Ketebalan, [Target(M3)]=b.[Target(M3)], " +
                "[OutPut(M3)]=b.[OutPut(M3)], [OutPut(M3/Shift)]=b.[OutPut(M3/Shift)], konversi=b.konversi,Persen=b.Persen " +
                "FROM   OutPutCalc b " +
                "WHERE  BM_BDTOutPut.Line = b.Line and BM_BDTOutPut.gp=b.GP and BM_BDTOutPut.ketebalan=b.Ketebalan  " +
                "and BM_BDTOutPut.thnbln=@thnbln and BM_BDTOutPut.Line=@Line and BM_BDTOutPut.rowstatus>-1";

            SqlDataReader sdr1 = zl1.Retrieve();
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), false);
        }
    }
    public class BDT_Out : GRCBaseDomain
    {
        public int ID { get; set; }
        public string Line { get; set; }
        public string GP { get; set; }
        public int WaktuMenit { get; set; }
        public string Ketebalan { get; set; }
        public decimal TargetM3 { get; set; }
        public decimal OutputM3 { get; set; }
        public decimal OutputM3Shift { get; set; }
        public decimal konversi { get; set; }
        public decimal Persen { get; set; }
        public decimal stdTarget { get; set; }
    }

    public class FacadeBDT_OutPut
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BDT_Out objOutPut = new BDT_Out();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public decimal RetrieveOutProduksi_All(string thnbln)
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

        public int RetrieveApv(string Bulan, string Tahun)
        {
            string StrSql =
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=2 and  " +
            " SarMutPerusahaan='Efisiensi Budget Finishing' and RowStatus>-1) and RowStatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " ";

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

        public int RetrieveTableBB(string Bulan, string Tahun)
        {
            string StrSql =
            " select count(ID) Nilai from BukuBesar_Mapping where rowstatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " " +
            " and ItemSarMut='Efisiensi Budget Finishing' ";

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
    }
}