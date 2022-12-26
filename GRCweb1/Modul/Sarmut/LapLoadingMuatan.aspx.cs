using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.Modul.Sarmut
{
    public partial class LapLoadingMuatan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
               ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string DariTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            string SampaiTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            LoadSchedule(DariTgl, SampaiTgl);
        }

        private void LoadSchedule(string drtgl, string sdtgl)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT a.Keterangan ScheduleNo, (CASE WHEN MobilSendiri = 1 THEN '' WHEN MobilSendiri = 0 THEN 'BPAS' END ) Mobil, Convert(VARCHAR(10),TglIn,105) + ' ' + Convert(VARCHAR(10),TglIn,8) TglIn , Convert(VARCHAR(10),TimeIn,105) + ' ' + Convert(VARCHAR(10),TimeIn,8) TimeIn, Convert(VARCHAR(10),TimeOut,105) + ' ' + Convert(VARCHAR(10),TimeOut,8) TimeOut, (CASE WHEN b.JenisMobil IS NULL THEN '-' ELSE b.JenisMobil END) JenisKendaraan FROM LoadingTime a left JOIN MasterKendaraan b ON a.kendaraanid = b.id WHERE left(CONVERT(char,tglin,112),8) BETWEEN " + drtgl + " AND " + sdtgl + " order by tglin asc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Schedule
                    {
                        ScheduleNo = sdr["ScheduleNo"].ToString(),
                        Mobil = sdr["Mobil"].ToString(),
                        TglIn = sdr["TglIn"].ToString(),
                        TimeIn = sdr["TimeIn"].ToString(),
                        TimeOut = sdr["TimeOut"].ToString(),
                        JenisKendaraan = sdr["JenisKendaraan"].ToString()
                    });
                }
            }
            lstSch.DataSource = arrData;
            lstSch.DataBind();
        }

        protected void lstSch_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            Schedule sch = (Schedule)e.Item.DataItem;
            Repeater rsj = (Repeater)e.Item.FindControl("lstSj");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("Sj");

            LoadListSJ(sch.ScheduleNo, rsj);
        }

        protected void lstSj_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            Schedule ba = (Schedule)e.Item.DataItem;
            Repeater rsj = (Repeater)e.Item.FindControl("lstSj");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("Sj");

        }

        private void LoadListSJ(string ID, Repeater rsj)
        {
            //Users users = (Users)Session["Users"];
            //ArrayList arrData = new ArrayList();
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "SELECT d.ScheduleNo, a.SuratJalanNo, c.Description, b.Qty FROM [sql1.grcboard.com].GRCBOARD.dbo.suratjalan a, [sql1.grcboard.com].GRCBOARD.dbo.suratjalandetail b, [sql1.grcboard.com].GRCBOARD.dbo.items c, [sql1.grcboard.com].GRCBOARD.dbo.schedule d WHERE a.id = b.SuratJalanid AND b.itemid = c.id AND a.Scheduleid = d.id AND a.status > -1  AND d.ScheduleNo = '"+ID+"' ORDER BY suratjalanno ASC";
            //SqlDataReader sdr = zl.Retrieve();
            //if (sdr.HasRows)
            //{
            //    while (sdr.Read())
            //    {
            //        arrData.Add(new Schedule
            //        {
            //            SuratJalanNo = sdr["SuratJalanNo"].ToString(),
            //            Description = sdr["Description"].ToString(),
            //            Qty = Convert.ToInt32(sdr["Qty"].ToString())
            //        });
            //    }
            //}
            //rsj.DataSource = arrData;
            //rsj.DataBind();


            ArrayList arrSuratJalan = new ArrayList();
            Global2 cpdWebService = new Global2();
            DataSet dsarrSuratJalan = cpdWebService.Retrieve_SJ_ByID(ID);
            foreach (DataRow row in dsarrSuratJalan.Tables[0].Rows)
            {
                Schedule sJ = new Schedule();
                sJ.SuratJalanNo = row["SuratJalanNo"].ToString();
                sJ.Description = row["Description"].ToString();
                sJ.Qty = Convert.ToInt32(row["Qty"].ToString());
                arrSuratJalan.Add(sJ);
            }
            rsj.DataSource = arrSuratJalan;
            rsj.DataBind();
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LapPemantauanLoading.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            sr.Close();
            Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>LAPORAN PEMANTAUAN LOADING TIME</b>";
            Html += "<br>Periode : " + txtTgl1.Text + " s/d " + txtTgl2.Text;
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }

    public class Schedule : GRCBaseDomain
    {
        public string ScheduleNo { get; set; }
        public string Mobil { get; set; }
        public string TglIn { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string JenisKendaraan { get; set; }
        public string SuratJalanNo { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
    }
}