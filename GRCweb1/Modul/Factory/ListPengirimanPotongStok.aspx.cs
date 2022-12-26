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
using System;
using System.Collections;
using Dapper;

namespace GRCweb1.Modul.Factory
{
    public partial class ListPengirimanPotongStok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string DariTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            string SampaiTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            int depoid = ((Users)Session["Users"]).UnitKerjaID;
            ArrayList arrSuratJalan = new ArrayList();
            Global2 cpdWebService = new Global2();
            DataSet dsarrSuratJalan = cpdWebService.Retrieve_ScheduleItems(DariTgl, SampaiTgl, depoid);

            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string delete = "TRUNCATE TABLE Temp_Schedule";
                connection.Execute(delete);
            }
            foreach (DataRow row in dsarrSuratJalan.Tables[0].Rows)
            {
                int val = 0; ;
                SqlConnection sConn = new SqlConnection(Global.ConnectionString());
                sConn.Open();
                try
                {
                    string strSQL = "INSERT INTO Temp_Schedule values ('" + row["ScheduleNo"].ToString() + "','" + row["SuratJalanNo"].ToString() + "','" + DateTime.Parse ( row["Scheduledate"].ToString()).ToString("yyyyMMdd") + "'," + Convert.ToInt32(row["Itemid"].ToString()) + ",'" + row["Item"].ToString() + "'," + Convert.ToInt32(row["Qty"].ToString()) + ")";
                    SqlCommand cmd = new SqlCommand(strSQL, sConn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                sConn.Close();
            }
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT * FROM Temp_Schedule WHERE itemid IN(SELECT DISTINCT itemidmkt FROM FC_LinkItemMkt) AND SuratJalanNo NOT IN(SELECT sjno FROM t3_kirim where Status > -1) AND ScheduleNo IN(SELECT Keterangan FROM LoadingTime) ORDER BY ScheduleDate asc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Schedule
                    {
                        ScheduleNo = sdr["ScheduleNo"].ToString(),
                        SuratJalanNo = sdr["SuratJalanNo"].ToString(),
                        Scheduledate = sdr["Scheduledate"].ToString(),
                        Item = sdr["Item"].ToString(),
                        Qty = Convert.ToInt32(sdr["Qty"].ToString())
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
        }

        public class Schedule : GRCBaseDomain
        {
            public string ScheduleNo { get; set; }
            public string SuratJalanNo { get; set; }
            public string Scheduledate { get; set; }
            public int Itemid { get; set; }
            public string Item { get; set; }
            public int Qty { get; set; }
            public string Keterangan { get; set; }
            
        }
    }
}