using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using DataAccessLayer;
using System.Data.SqlClient;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LKirim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }


            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            #region Tambahan 01
            KirimanDomain krm = new KirimanDomain();
            KirimanFacade krmF = new KirimanFacade();
            int TotalHari = krmF.CekTotalHari(periodeAkhir);
            #endregion

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            string frmtPrint = string.Empty;

            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            //string deptname = string.Empty;
            //if (dept.DeptName != string.Empty)
            //    deptname = dept.DeptName.Substring(0, 3).ToUpper();
            //else
            //    deptname = " ";

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            int tgltype = 0;
            if (RBTglProduksi.Checked == true)
                tgltype = 0;
            else
                tgltype = 1;
            string partno = string.Empty;
            if (txtPartno.Text.Trim().Length > 10)
                partno = " and C.partno='" + txtPartno.Text.Trim() + "' ";

            #region Tambahan 02
            int Tahun = Convert.ToInt32(periodeAwal.Substring(0, 4));
            int Bulan = Convert.ToInt32(periodeAwal.Substring(4, 2));
            int BulanMundur = Bulan - 1;
            int BulanMaju = Bulan + 1;

            if (BulanMundur < 10 || BulanMaju < 10)
            {
                string BulanMundurS = "0" + BulanMundur; Session["BulanFinal"] = BulanMundurS;
                string BulanMajuS = "0" + BulanMaju; Session["BulanMajuFinal"] = BulanMajuS;
            }
            else
            {
                string BulanMundurS = BulanMundur.ToString(); Session["BulanFinal"] = BulanMundurS;
                string BulanMajuS = BulanMaju.ToString(); Session["BulanMajuFinal"] = BulanMajuS;
            }

            if (Bulan == 1 || Bulan == 12)
            {
                int TahunFinal = Tahun - 1; Session["TahunFinal"] = TahunFinal;
                int TahunMajuFinal = Tahun + 1; Session["TahunMajuFinal"] = TahunMajuFinal;
            }
            else
            {
                int TahunFinal = Tahun; Session["TahunFinal"] = TahunFinal;
                int TahunMajuFinal = Tahun; Session["TahunMajuFinal"] = TahunMajuFinal;
            }

            string BulanMajuSF = Session["BulanMajuFinal"].ToString();
            string TahunPeriodeMajuSF = Session["TahunMajuFinal"].ToString();
            string PeriodeAkhirMajuSF = TahunPeriodeMajuSF + BulanMajuSF.ToString() + TotalHari.ToString();

            string BulanMundurSF = Session["BulanFinal"].ToString();
            string TahunPeriodeMundurSF = Session["TahunFinal"].ToString();
            string PeriodeAwalMundurSF = TahunPeriodeMundurSF + BulanMundurSF.ToString() + "01";

            #endregion
            if (ChkSJ.Checked == false)
                strQuery = reportFacade.ViewLKirim(periodeAwal, periodeAkhir, tgltype, partno, PeriodeAwalMundurSF, PeriodeAkhirMajuSF);
            else
                strQuery = reportFacade.ViewLKirim1(periodeAwal, periodeAkhir, tgltype, partno, PeriodeAwalMundurSF, PeriodeAkhirMajuSF);
            Session["Query"] = strQuery;
            Session["periode"] = "Periode : " + txPeriodeAwal + " s/d " + txPeriodeAkhir;
            Cetak(this);
            #region Quality Control Customer Complaint
            ///Add By Razib 15-05-2021
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strQuery;
            SqlDataReader sdr = zl.Retrieve();

            string sarmutPrs = "Customer complaint";
            string strDept = string.Empty;
            int deptid = getDeptID("QUALITY CONTROL");

            #endregion

            #region Proses Ke inputan Sarmut

            decimal Target = 0;
            string jam = DateTime.Now.ToString("yyMMddhhmmss");
            string tgl1 = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string tgl2 = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string tahun = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyy");
            string bulan = DateTime.Parse(txtToPostingPeriod.Text).ToString("MM");
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwalR" + jam + "]') AND type in (N'U')) DROP TABLE[dbo].DataAwalR" + jam + " " +
                            " SELECT (((AA.tebal*AA.Lebar*AA.Panjang)*(sum(AA.Qty)))/1000000000)as M3,AA.tebal,AA.Lebar,AA.Panjang,AA.KirimID,AA.CreatedTime ,AA.TglKirim,null  tglSJ,AA.Customer, AA.SJNo, C.PartNo, sum(AA.Qty) as Qty, " +
                            " ((AA.tebal/1000)*sum(AA.Qty) ) as Meter,D.Groups, AA.Cust,AA.pengiriman,AA.JenisPalet,AA.JmlPalet into DataAwalR" + jam + "  FROM " +
                            " (select (select tebal from fc_items where ID=b.itemid) as tebal,(select Lebar from FC_Items where ID=b.ItemID) as Lebar,(select Panjang from FC_Items where ID=b.ItemID) as Panjang, " +
                            " a.CreatedTime,a.Customer, a.ID as KirimID, a.SJNo,a.OPNo, a.TglKirim,b.ID as KirimDetailID,b.ItemIDSJ,b.ItemID,b.Qty,  case when  SUBSTRING(a.SJNo,14,4)='/SJ/' then 'CPD' when SUBSTRING(a.SJNo,10,4)='/TO/' " +
                            " then 'CPD' else 'EKS' end Cust,b.pengiriman,b.jenispalet,b.jmlpalet  from T3_Kirim as a, T3_KirimDetail as b where a.ID=b.KirimID and a.Status>-1 and a.RowStatus>-1 and b.RowStatus>-1 and b.Status>-1 ) as AA  " +
                            " INNER JOIN FC_Items AS C ON AA.ItemID = C.ID  left JOIN T3_GroupM as D ON D.ID = C.GroupID    WHERE convert(varchar,AA.TglKirim,112)>='" + tgl1 + "' and convert(varchar,AA.TglKirim,112)<='" + tgl2 + "'" +
                            " group by AA.KirimID,AA.KirimDetailID,AA.CreatedTime ,AA.TglKirim,AA.Customer, AA.SJNo, C.PartNo,D.Groups, AA.Cust,AA.tebal,AA.Lebar,AA.Panjang,AA.pengiriman,AA.JenisPalet,AA.JmlPalet,Qty " +
                            " select isnull([target],0) [target] from ( " +
                            " select M3, " +
                            " case when cast(M3 as int)>=0 and cast(M3 as int)<=4000 then '1'  " +
                                " when cast(M3 as int)>=4001 and cast(M3 as int)<=8000 then '2'  " +
                                " when cast(M3 as int)>=8001 and cast(M3 as int)<=12000 then '3'  " +
                                " when cast(M3 as int)>=12001 and cast(M3 as int)<=16000 then '4'  " +
                            " end [Target]  " +
                            " from (  " +
                            " select sum(M3)M3 From DataAwalR" + jam + " " +
                            " ) as Data1 " +
                            " ) as Data2 " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataAwalR" + jam + "]') AND type in (N'U')) DROP TABLE[dbo].DataAwalR" + jam + " ";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Target = Decimal.Parse(sdr["Target"].ToString());
                }
            }

            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + tahun +
                " set @bulan=" + bulan + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set Target=" + Target.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();


            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set Target= " + Target.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + tahun +
                 " and Bulan=" + bulan +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion
        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LKirim', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtFromPostingPeriod_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
        }
    }


    public class KirimanFacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private KirimanDomain krm = new KirimanDomain();

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int CekTotalHari(string tgl)
        {
            string StrSql = " Select day(dateadd(mm,DateDiff(mm, + 1, '" + tgl + "'),0) -1) Total ";
            //" where DeptID_Users=" + DeptID + " and RowStatus>-1 and ApvOP=0 and PlantID<>" + UnitKerjaID + " " +
            //" union all " +
            //" select '0'TotalShare from WorkOrder ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Total"]);
                }
            }

            return 0;
        }

    }

    public class KirimanDomain
    {
        public string No { get; set; }
        public string Plant { get; set; }
        public string NoPol { get; set; }
        public decimal MaxBudget { get; set; }
        public decimal Actual { get; set; }
        public decimal Persen { get; set; }
        public string JenisUnit { get; set; }
        public decimal TotalStd { get; set; }
        public decimal TotalPersen { get; set; }
        public decimal TotalActual { get; set; }

    }
}