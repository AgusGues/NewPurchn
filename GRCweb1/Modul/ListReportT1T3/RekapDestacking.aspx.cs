using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Factory;
using Domain;
using BusinessFacade;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class RekapDestacking : System.Web.UI.Page
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
            //string periodeAwal = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
            LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
            loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"));
            //string tgl = DateTime.Parse(txtdrtanggal.Text).ToString();
        }
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
            for (int i = 0; i < 31; i++)
            {
                if (i == 0)
                    tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" +
                        DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
                else
                    tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
                if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
                {
                    if (i < 30)
                    {
                        sqlselect = sqlselect + "cast(isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                            DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as int) as " +
                            "[" + (i + 1).ToString() + "],";
                        sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                            DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],";
                    }
                    else
                    {
                        sqlselect = sqlselect + "cast(isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                           DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as int) as " +
                           "[" + (i + 1).ToString() + "]";
                        sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                            DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "]";
                    }

                }
                else
                {
                    if (i < 30)
                        sqlselect = sqlselect + "cast(isnull([" + (i + 1).ToString() + "],0) as int) as " +
                        "[" + (i + 1).ToString() + "],";
                    else
                        sqlselect = sqlselect + "cast(isnull([" + (i + 1).ToString() + "],0) as int) as " +
                        "[" + (i + 1).ToString() + "]";
                    if (i < 30)
                        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
                    else
                        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
                }
            }
            string thnbln = ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string Target1 = string.Empty; string Target2 = string.Empty; string Target34 = string.Empty; string TempNilai = string.Empty;
            string Target = string.Empty; string query = string.Empty; string strSQL = string.Empty;

            if (users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                #region UnitKerja
                if (users.UnitKerjaID == 1)
                {
                    Target1 = "27"; Target2 = "38"; Target34 = "40";

                    TempNilai =

                    //" into tempNilai from (select  line,case when Line=1 then (" + Target1 + "*3*1) when Line=2 then (" + Target2 + "*3*1) " +
                    //" when Line in (3,4) then (" + Target34 + "*3*1) end Nilai ";

                    " select SUM(Nilai)Nilai,TglProduksi hari into tempNilai from (select  line,case when Line=1 then (" + Target1 + "*3*1) " +
                    " when Line=2 then (" + Target2 + "*3*1) when Line in (3,4) then (" + Target34 + "*3*1) end Nilai ,TglProduksi " +
                    " from ( " +
                    /** tambahan 02/07/2020 **/
                    " select Line,TglProduksi from ( " +
                    /** end **/
                    " select distinct(PlantID)Line,TglProduksi from BM_Destacking " +
                    " where LEFT(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') " +
                    " and TglProduksi not in (select tanggal from BM_LineAktif where Rowstatus>-1) " +
                    /** ambil data line dari breakdowntime 02/07/2020 **/
                    " union all " +
                    " select BM_PlantID Line,convert(char,StartBD,112) TglProduksi " +
                    " from BreakBM where LEFT(convert(char,StartBD,112),6)=@thnbln and SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)=4 ) as x0 " +
                    " group by Line,TglProduksi " +
                    /** end **/
                    " union all " +
                    " select LineAktif,Tanggal from BM_LineAktif where Rowstatus>-1 and LEFT(convert(char,Tanggal,112),6)=@thnbln) as Data1 ) as DataNilai " +
                    " group by TglProduksi order by TglProduksi ";

                    query =
                    /** 
                     * - Remark Logika lama -
                     * 
                     * "select count(line)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                       "from BM_Destacking where rowstatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                       "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";
                     * **/

                    /** - Logika Baru added 02 Jan 2020 By Beny For Citeureup - **/
                    " select jmlLine,tgl hari into TempJmlLine from (select count(line)jmlLine,tgl ,note from( " +
                    /** tambahan 02/07/2020 **/
                    " select Line,tgl,Note from ( " +
                    /** end **/
                    " select distinct PlantID Line , " +
                    " convert(char,tglproduksi,112) tgl, 'Auto'Note from BM_Destacking where rowstatus>-1 and LokasiID not in " +
                    " (select ID from FC_Lokasi where Lokasi like'%adj%') and  LEFT(convert(char,tglproduksi,112),6)=@thnbln and TglProduksi not in " +
                    " (select Tanggal from BM_LineAktif where RowStatus>-1) " +

                    /** ambil data line aktif diinputan breakdowntime 02/07/2020 **/
                    " union all " +
                    " select BM_PlantID Line,convert(char,StartBD,112) tgl,'Auto'Note " +
                    " from BreakBM where LEFT(convert(char,StartBD,112),6)=@thnbln and SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)=4 ) as x group by Line,tgl,Note " +
                    /** end **/

                    //" union all " +
                    //" select  LineAktif,convert(char,Tanggal,112) tgl,'Off'Note from BM_LineAktif where Rowstatus>-1 and LEFT(convert(char,Tanggal,112),6)=" +
                    //" @thnbln "+

                    " )A group by tgl,Note ) B group by jmlline,tgl order by tgl ";


                }
                else if (users.UnitKerjaID == 7)
                {
                    Target = "40";

                    TempNilai =
                    //" into tempNilai from (select  line,case when Line in (1,2,3,4,5,6) then (" + Target + "*3*1) end Nilai ";

                    " select SUM(Nilai)Nilai,TglProduksi hari into tempNilai from (select  line,case when Line in (1,2,3,4,5,6) then (" + Target + "*3*1) end Nilai " +
                    " ,TglProduksi from (select distinct(PlantID)Line,TglProduksi from BM_Destacking " +
                    " where LEFT(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') " +
                    " and TglProduksi not in (select tanggal from BM_LineAktif where Rowstatus>-1) " +
                    " union all " +
                    " select LineAktif,Tanggal from BM_LineAktif where Rowstatus>-1 and LEFT(convert(char,Tanggal,112),6)=@thnbln) as Data1 ) as DataNilai " +
                    " group by TglProduksi order by TglProduksi ";

                    if (Convert.ToInt32(thnbln) < Convert.ToInt32("201910"))
                    {
                        query =
                        "select (6)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                        "from BM_Destacking where rowstatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                        "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";
                    }
                    else if (Convert.ToInt32(thnbln) >= Convert.ToInt32("201910"))
                    {
                        query =
                        "select count(line)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                        "from BM_Destacking where rowstatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                        "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";
                    }

                }
                else if (users.UnitKerjaID == 13)
                {
                    Target = "60";

                    TempNilai =
                    //" into tempNilai from (select  line,case when Line in (1,2,3,4,5,6) then (" + Target + "*3*1) end Nilai ";

                    " select SUM(Nilai)Nilai,TglProduksi hari into tempNilai from (select  line,case when Line in (1,2,3,4,5,6) then (" + Target + "*3*1) end Nilai " +
                    " ,TglProduksi from (select distinct(PlantID)Line,TglProduksi from BM_Destacking " +
                    " where LEFT(convert(char,tglproduksi,112),6)=@thnbln and RowStatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') " +
                    " and TglProduksi not in (select tanggal from BM_LineAktif where Rowstatus>-1) " +
                    " union all " +
                    " select LineAktif,Tanggal from BM_LineAktif where Rowstatus>-1 and LEFT(convert(char,Tanggal,112),6)=@thnbln) as Data1 ) as DataNilai " +
                    " group by TglProduksi order by TglProduksi ";

                    query =
                        "select count(line)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                        "from BM_Destacking where rowstatus>-1 and qty>0 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                        "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";

                    //if (Convert.ToInt32(thnbln) < Convert.ToInt32("201910"))
                    //{
                    //    query =
                    //    "select (6)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                    //    "from BM_Destacking where rowstatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                    //    "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";
                    //}
                    //else if (Convert.ToInt32(thnbln) >= Convert.ToInt32("201910"))
                    //{
                    //    query =
                    //    "select count(line)jmlLine,tgl hari into TempJmlLine from(select distinct PlantID Line , convert(char,tglproduksi,112) tgl " +
                    //    "from BM_Destacking where rowstatus>-1 and LokasiID not in (select ID from FC_Lokasi where Lokasi like'%adj%') and  " +
                    //    "LEFT(convert(char,tglproduksi,112),6)=@thnbln )A group by tgl ";
                    //}

                }

                strSQL =

                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdest]') AND type in (N'U')) DROP TABLE [dbo].[tempdest] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBM]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBM] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempJmlLine]') AND type in (N'U')) DROP TABLE [dbo].[TempJmlLine] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempNilai]') AND type in (N'U')) DROP TABLE [dbo].[tempNilai] " +

                   "declare @thnbln  varchar(6) " +
                   "set @thnbln='" + thnbln + "' " +

                   "" + query + "" +
                   "" + TempNilai + "" +

                   /** Create Table temporary tempdest **/
                   "select I.partno,CONVERT(varchar,D.tglproduksi ,112)hari,sum(qty)qty, " +
                   "sum(qty)*((( cast(substring(partno,7,3) as decimal)/10) * cast(substring(partno,10,4) as decimal) * cast(substring(partno,14,4)as decimal))/(4*1220*2440)  ) qty4x8, " +
                   "sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000  qtym3,sum(qty)*(cast(substring(partno,7,3) as decimal)/10 ) qtymm into tempdest from bm_destacking D  " +
                   "inner join fc_items I on D.itemid=I.id where D.rowstatus>-1 and D.lokasiID not in (select ID from fc_lokasi where lokasi like '%adj%') and left(convert(char,D.tglproduksi,112),6)=@thnbln " +
                   "group by I.partno,D.tglproduksi " +
                   /** End Create Table temporary tempdest **/

                   /** Create Table temporary tempBreakBM **/
                   "SELECT CONVERT(varchar,TglBreak ,112)hari,sum(BDNPMS_S)BDNPMS_S into tempBreakBM From( " +
                   "select line, convert (varchar,TglBreak,112) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
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
                    "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
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
                    "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null) BM where RowStatus=0 group by TglBreak " +
                    /** End Create Table temporary tempBreakBM **/

                    "select  Jenis," + sqlselect +
                   ",cast((case when A1.partno ='X' then (select sum(qty) from tempdest )  " +
                   "    when A1.partno ='Y' then (select ROUND(sum(qty4x8),0) from tempdest )  " +
                   "    when A1.partno ='Z' then (select ROUND(sum(qtym3),0) from tempdest )  " +

                   //"    when A1.partno ='Z2' then (select sum(((480*3*6)-BDNPMS_S)/(480*3*6)) from tempBreakBM )  " +
                   "when A1.partno ='Z2' then (select SUM(xa) from (select ROUND(((((480*3)*(select (jmlLine) from TempJmlLine A1 " +
                   "where A1.hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select (jmlLine) from TempJmlLine A1 where A1.hari=tempBreakBM.hari))),0)xa " +
                   "from tempBreakBM) as xxa ) " +

                   //"    when A1.partno ='Z3' then (select sum((((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6)) from tempBreakBM )  " +
                   "when A1.partno ='Z3' then (select SUM(xa) from (select ROUND(((select Nilai from tempNilai where hari=tempBreakBM.hari)*" +
                   "(((480*3*(select (jmlLine) from TempJmlLine A1 where A1.hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select (jmlLine) from " +
                   "TempJmlLine A1 where A1.hari=tempBreakBM.hari)))),0)xa from tempBreakBM) as xxa ) " +

                   //"    when A1.partno ='Z4' then (select avg(qty) from ( " +
                   //"select  'Z4'partno, 'PRODUKTIVITAS'Jenis,hari,(((select sum(qty) from(select  'Z'partno, 'Total3 (M3)'Jenis,hari, " +
                   //"sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *   " +
                   //"cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest where hari=tempBreakBM.hari  group by partno, hari )A))/ " +
                   //"((((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6) ))*100  qty from tempBreakBM)B)  " +
                   //"else " +
                   //" (select sum(qty) from tempdest where partno=A1.partno) " +
                   //"end) as int) Total_Lbr,"+
                   "when A1.partno ='Z4' then (select avg(qty) from ( select  'Z4'partno, 'PRODUKTIVITAS'Jenis,hari, case when ((480*3*(select jmlline from TempJmlLine where hari=tempBreakBM.hari))-BDNPMS_S)=0 then 0 else " +
                   "(((select sum(qty) from(select  'Z'partno, 'Total3 (M3)'Jenis,hari, sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * " +
                   "cast(substring(partno,10,4) as decimal) *   cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest " +
                   "where hari=tempBreakBM.hari  group by partno, hari )A))/ ((((480*3*(select (jmlLine) from TempJmlLine A1 where A1.hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select jmlline from TempJmlLine " +
                   "where hari=tempBreakBM.hari)))*(select Nilai from tempNilai where hari=tempBreakBM.hari) ))*100 end qty from tempBreakBM)B)  " +
                   "else  (select sum(qty) from tempdest where partno=A1.partno) end) as int) Total_Lbr, " +

                   "cast((select sum(qty4x8) from tempdest where partno=A1.partno )as int) Total_4_4X8," +

                   "cast((select sum(qtym3) from tempdest where partno=A1.partno ) as decimal(8,1)) Total_M3 from ( " +
                   "select partno,substring(partno,1,3) + '   ' + rtrim(cast (cast(cast(substring(partno,7,3) as decimal(18,1) )/10 as decimal(18,1)) as char)) +  " +
                   "'   ' + substring(partno,10,4)+ '   ' +substring(partno,14,4)Jenis,hari,qty from tempdest " +

                   "union all " +

                   "select  'X'partno,'Total1 (Lembar)' Jenis,hari,sum(qty)qty from tempdest group by partno,hari " +

                   "union all " +

                   "select  'Y'partno, 'Total2 (Konv 4mmx4x8)' Jenis,hari,sum(qty)*(((cast(substring(partno,7,3) as decimal)/10) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))/(4*1220*2440)  ) qty from tempdest group by partno, hari " +

                   "union all " +

                   "select  'Z'partno, 'Total3 (M3)'Jenis,hari,sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest group by partno, hari " +

                   "union all " +

                   "select  'Z1'partno, 'BDT SCHEDULE'Jenis,hari,BDNPMS_S qty from tempBreakBM  " +

                   "union all " +

                   "select  'Z2'partno, 'WAKTU PRODUKSI'Jenis,hari,ROUND(((480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari)),0) qty from tempBreakBM  " +

                   "union all " +

                   "select  'Z3'partno, 'TARGET PRODUKSI'Jenis,hari,ROUND((((480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari)))*(select Nilai from tempNilai where hari=tempBreakBM.hari),0)  qty from tempBreakBM  " +

                   "union all " +

                   "select  'Z4'partno, 'PRODUKTIVITAS'Jenis,hari,case when ((480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari))-BDNPMS_S)=0 then 0 else ROUND((((select sum(qty) from(select  'Z'partno, 'Total3 (M3)'Jenis,hari,sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest where hari=tempBreakBM.hari  group by partno, hari )A))/((((480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari))-BDNPMS_S)/(480*3*(select jmlline from TempJmlLine  where hari=tempBreakBM.hari)))*(select Nilai from tempNilai where hari=tempBreakBM.hari) ))*100,0) end qty from tempBreakBM  " +
                   ") up pivot (sum(QTY) for hari in (" + sqlinpivot + "))A1 order by Partno ";
                #endregion
            }
            else if (users.UnitKerjaID == 7)
            {
                strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdest]') AND type in (N'U')) DROP TABLE [dbo].[tempdest] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBM]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBM] " +
                   "declare @thnbln  varchar(6) " +
                   "set @thnbln='" + thnbln + "' " +

                   "select I.partno,CONVERT(varchar,D.tglproduksi ,112)hari,sum(qty)qty, " +
                   "sum(qty)*((I.Tebal  * I.Lebar * I.Panjang)/(4*1220*2440)) qty4x8, " +
                   "sum(qty)*(I.Tebal  * I.Lebar * I.Panjang)/1000000000  qtym3,sum(qty)*(cast(substring(partno,7,3) as decimal)/10 ) qtymm into tempdest from bm_destacking D  " +
                   "inner join fc_items I on D.itemid=I.id where qty>0 and D.rowstatus>-1 and D.lokasiID not in (select ID from fc_lokasi where lokasi like '%adj%') and left(convert(char,D.tglproduksi,112),6)=@thnbln " +
                   "group by I.partno,I.Tebal  , I.Lebar , I.Panjang, I.partno,D.tglproduksi " +

                   "SELECT CONVERT(varchar,TglBreak ,112)hari,sum(BDNPMS_S)BDNPMS_S into tempBreakBM From( " +
                   "select line, convert (varchar,TglBreak,112) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
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
                    "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
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
                    "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null) BM where RowStatus=0 group by TglBreak " +

                    "select  Jenis," + sqlselect +
                   ",cast((case when A1.partno ='X' then (select sum(qty) from tempdest )  " +
                   "    when A1.partno ='Y' then (select sum(qty4x8) from tempdest )  " +
                   "    when A1.partno ='Z' then (select sum(qtym3) from tempdest )  " +
                   "    when A1.partno ='Z2' then (select sum(((480*3*6)-BDNPMS_S)/(480*3*6)) from tempBreakBM )  " +
                   "    when A1.partno ='Z3' then (select sum((((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6)) from tempBreakBM )  " +
                   "    when A1.partno ='Z4' then (select avg(qty) from ( " +
                   "select  'Z4'partno, 'PRODUKTIVITAS'Jenis,hari,(((select sum(qty) from(select  'Z'partno, 'Total3 (M3)'Jenis,hari, " +
                   "sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *   " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest where hari=tempBreakBM.hari  group by partno, hari )A))/ " +
                   "((((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6) ))*100  qty from tempBreakBM)B)  " +
                   "else " +
                   " (select sum(qty) from tempdest where partno=A1.partno) " +
                   "end) as int) Total_Lbr,cast((select sum(qty4x8) from tempdest where partno=A1.partno )as int) Total_4_4X8," +
                   "cast((select sum(qtym3) from tempdest where partno=A1.partno ) as decimal(8,1)) Total_M3 from ( " +
                   "select partno,substring(partno,1,3) + '   ' + rtrim(cast (cast(cast(substring(partno,7,3) as decimal(18,1) )/10 as decimal(18,1)) as char)) +  " +
                   "'   ' + substring(partno,10,4)+ '   ' +substring(partno,14,4)Jenis,hari,qty from tempdest " +
                   "union all " +
                   "select  'X'partno,'Total1 (Lembar)' Jenis,hari,sum(qty)qty from tempdest group by partno,hari " +
                   "union all " +
                   "select  'Y'partno, 'Total2 (Konv 4mmx4x8)' Jenis,hari,sum(qty)*(((cast(substring(partno,7,3) as decimal)/10) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))/(4*1220*2440)  ) qty from tempdest group by partno, hari " +
                   "union all " +
                   "select  'Z'partno, 'Total3 (M3)'Jenis,hari,sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest group by partno, hari " +
                   "union all " +
                   "select  'Z1'partno, 'BDT SCHEDULE'Jenis,hari,BDNPMS_S qty from tempBreakBM  " +
                   "union all " +
                   "select  'Z2'partno, 'WAKTU PRODUKSI'Jenis,hari,((480*3*6)-BDNPMS_S)/(480*3*6) qty from tempBreakBM  " +
                   "union all " +
                   "select  'Z3'partno, 'TARGET PRODUKSI'Jenis,hari,(((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6)  qty from tempBreakBM  " +
                   "union all " +
                   "select  'Z4'partno, 'PRODUKTIVITAS'Jenis,hari,(((select sum(qty) from(select  'Z'partno, 'Total3 (M3)'Jenis,hari,sum(qty)*(( (cast(substring(partno,7,3) as decimal)/10 ) * cast(substring(partno,10,4) as decimal) *  " +
                   "cast(substring(partno,14,4)as decimal))  )/1000000000 qty from tempdest where hari=tempBreakBM.hari  group by partno, hari )A))/((((480*3*6)-BDNPMS_S)/(480*3*6))*(40*3*6) ))*100  qty from tempBreakBM  " +
                   ") up pivot (sum(QTY) for hari in (" + sqlinpivot + "))A1 order by Partno ";
            }

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
                //bfield.DataFormatString = "{0:N0}";
                if (bfield.HeaderText.Trim() != "Jenis")
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                else
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
            LblTgl6.Text = DateTime.Now.ToString("dddd") + " " + DateTime.Now.ToString("dd MMMM yyyy");
            LblTgl2.Text = retrievetodest("select cast(sum(qtymm) as decimal(18,0)) result from tempdest").ToString("N0");
            LblTgl3.Text = retrievetodest("select count(hari) result  from(select distinct hari from tempdest where qty>0 )A").ToString("N0");
            LblTgl4.Text = (retrievetodest("select cast(sum(qtymm) as decimal(18,0)) result from tempdest") / Int32.Parse(LblTgl3.Text)).ToString("N0");
            LblTgl5.Text = retrievetodest("select cast(sum(qtym3) as decimal(18,2)) result from tempdest").ToString("N2");
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "drop table tempdest";
            //SqlDataReader sdr = zl.Retrieve();
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "rekap destacking.xls"));
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

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();
                HeaderCell.Text = "Tanggal";
                HeaderCell.ColumnSpan = 33;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);
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
}