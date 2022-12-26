using BusinessFacade;
using Cogs;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

using Dapper;
using System.Text;
using System.Threading.Tasks;
using GRCweb1.Modul.Sarmut;
using System.Web.UI.HtmlControls;
using GRCweb1.Modul.Factory;

namespace GRCweb1.Modul.Mtc
{
    public partial class BDTArmada : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                txtTglBA.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtStart.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
                txtFinish.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
                txtTotal.Text = "0";
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.Year.ToString().Trim();
                txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDataList();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        [WebMethod]
        public static List<DomainBDTArmada> GetArmada()
        {
            List<DomainBDTArmada> forklift = new List<DomainBDTArmada>();
            forklift = BDTArmadaFacade.GetArmada();
            return forklift;
        }
        [WebMethod]
        public static string GetUser()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            string createdby = user.UserName;

            return createdby;
        }
        [WebMethod]
        public static int Simpan(TransBDTArmada  obj)
        {
            
            int Intinsert=0;
            DateTime Tanggal = obj.Tanggal;
            string NamaUnit = obj.NamaUnit;
            DateTime TglStart = obj.TglStart;
            DateTime TglFinish = obj.TglFinish;
            int TotalTime = obj.TotalTime;
            string Kendala = obj.Kendala;
            string Perbaikan = obj.Perbaikan;
            string Keterangan = obj.Keterangan;
            string CreatedBy = obj.CreatedBy;

            BDTArmadaFacade bdff = new BDTArmadaFacade();
            Intinsert = bdff.Insertbreakdown(Tanggal, NamaUnit, TglStart, TglFinish, TotalTime, Kendala, Perbaikan, Keterangan, CreatedBy);
            return Intinsert;
        }
        protected void LoadDataList()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from BDTArmada where rowstatus>-1  " +
                "and convert(char,tanggal,112)>='" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd") + "' and convert(char,tanggal,112)<='" +
                DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd") + "' order by tglstart ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new TransBDTArmada
                        {
                            ID = int.Parse (sdr["ID"].ToString()),
                            Tanggal = DateTime.Parse(sdr["Tanggal"].ToString()),
                            TglStart = DateTime.Parse(sdr["TglStart"].ToString()),
                            TglFinish = DateTime.Parse(sdr["TglFinish"].ToString()),
                            TotalTime = Convert.ToInt32(sdr["TotalTime"].ToString()),
                            NamaUnit = (sdr["NamaUnit"].ToString()),
                            Kendala = sdr["Kendala"].ToString(),
                            Perbaikan = sdr["Perbaikan"].ToString(),
                            Keterangan = sdr["Keterangan"].ToString()
                        });
                    }
                }
            }

            lstBA.DataSource = arrData;
            lstBA.DataBind();
            lstExpBA.DataSource = arrData;
            lstExpBA.DataBind();
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("BDTArmada.aspx", false);
        }

        protected int GetBAID(string BANo)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ID from t3_BA where RowStatus>-1 and BANo='" + BANo + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            LoadDataList();
        }

        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            LoadDataList();
        }
        public decimal TotMenit = 0;
        public decimal TotUnit = 0;
        protected void lstBA_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TransBDTArmada p = (TransBDTArmada)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                if (tr != null)
                {
                    TotMenit += p.TotalTime;
                    TotUnit = TotUnit + 1;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ftr");
                tr.Cells[1].InnerText = TotUnit.ToString();
                tr.Cells[3].InnerText = TotMenit.ToString();
            }
        }
        public decimal TotMenitExp = 0;
        public decimal TotUnitExp = 0;
        protected void lstExpBA_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TransBDTArmada p = (TransBDTArmada)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                if (tr != null)
                {
                    TotMenitExp += p.TotalTime;
                    TotUnitExp = TotUnitExp + 1;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ftr");
                tr.Cells[1].InnerText = TotUnit.ToString();
                tr.Cells[3].InnerText = TotMenit.ToString();
            }
        }
        protected void lstBA_ItemCommand(object Source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Hps":
                    #region Verifikasi Closing Periode
                    ClosingFacade Closing = new ClosingFacade();
                    int Tahun = DateTime.Parse(txtdrtanggal.Text).Year;
                    int Bulan = DateTime.Parse(txtdrtanggal.Text).Month;
                    int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                    int clsStat = Closing.GetClosingStatus("SystemClosing");
                    if (status == 1 && clsStat == 1)
                    {
                        DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                        return;
                    }
                    else
                    {
                        ClosingFacade Closing1 = new ClosingFacade();
                        int Tahun1 = DateTime.Parse(txtsdtanggal.Text).Year;
                        int Bulan1 = DateTime.Parse(txtsdtanggal.Text).Month;
                        int status1 = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                        int clsStat1 = Closing.GetClosingStatus("SystemClosing");
                        if (status1 == 1 && clsStat1 == 1)
                        {
                            DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan1) + " " + Tahun1 + " sudah Closing. Transaksi Tidak bisa dilakukan");
                            return;
                        }
                    }
                    Image hps = (Image)e.Item.FindControl("Hapus");
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update BDTArmada set RowStatus=-1 where ID=" + hps.CssClass  ;
                    SqlDataReader sdr = zl.Retrieve();
                    LoadDataList();
                    #endregion
                    break;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            LblPlant.Text = getAlamat() + "," + DateTime.Now.ToString("dd MMM yyyy");
            lblTgl.Text =  DateTime.Parse(txtdrtanggal.Text).ToString("MMMM") + " " + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanBDTA.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string HtmlEnd = "";

            lst2.RenderControl(hw);
            //table.RenderControl(hw);
            string Contents = sw.ToString();
           // Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write( Contents );
            Response.Flush();
            Response.End();
        }
        private string getAlamat()
        {
            string Alamat = string.Empty;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select lokasi from company where depoid=" + users.UnitKerjaID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Alamat = sdr["lokasi"].ToString();
                }
            }
            return Alamat;
        }
    }
    public class BDTArmadaFacade : AbstractFacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DomainBDTArmada objBM = new DomainBDTArmada();

        public BDTArmadaFacade()
                : base()
        {

        }
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static List<DomainBDTArmada> GetArmada()
        {
            List<DomainBDTArmada> alldata = new List<DomainBDTArmada>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select ID,rtrim(NamaKendaraan)+' ('+rtrim(nopol)+')' NamaKendaraan from MTC_NamaArmada order by NamaKendaraan";
                    alldata = connection.Query<DomainBDTArmada>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
        public int Insertbreakdown(DateTime Tanggal, string NamaUnit, DateTime  TglStart, DateTime tglFinish, int TotalTime, string Kendala, string Perbaikan, string Keterangan,string createdby)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Tanggal", Tanggal));
                    sqlListParam.Add(new SqlParameter("@NamaUnit", NamaUnit));
                    sqlListParam.Add(new SqlParameter("@TglStart", TglStart));
                    sqlListParam.Add(new SqlParameter("@tglFinish", tglFinish));
                    sqlListParam.Add(new SqlParameter("@TotalTime", TotalTime));
                    sqlListParam.Add(new SqlParameter("@Kendala", Kendala));
                    sqlListParam.Add(new SqlParameter("@Perbaikan", Perbaikan));
                    sqlListParam.Add(new SqlParameter("@Keterangan", Keterangan));
                    sqlListParam.Add(new SqlParameter("@createdby", createdby));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spMTC_BdtArmada_Insert");
                    strError = dataAccess.Error;
                    return 1;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
        }
    }
    public class DomainBDTArmada : GRCBaseDomain
    {
        public string NamaKendaraan { get; set; }

    }
    public class TransBDTArmada : GRCBaseDomain
    {
        public DateTime Tanggal { get; set; }
        public DateTime TglStart { get; set; }
        public DateTime TglFinish { get; set; }
        public int TotalTime { get; set; }
        public string NamaUnit { get; set; }
        public string Kendala { get; set; }
        public string Perbaikan { get; set; }
        public string Keterangan { get; set; }
    }

}
