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
using BusinessFacade;
using DataAccessLayer;
using System.Data.SqlClient;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonOntimePemantauan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadPic();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);

        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--Pilih Pulan--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            POPurchnFacade pd = new POPurchnFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadPic()
        {
            ArrayList arrKasbon = new ArrayList();
            KasbonFacade kasbonFacade = new KasbonFacade();
            arrKasbon = kasbonFacade.Retrieve();
            ddlPic.Items.Add(new ListItem("-- All PIC --", string.Empty));
            //ddlSupPurch.Items.Add(new ListItem(" ", string.Empty));
            foreach (Kasbon kasbon in arrKasbon)
            {
                ddlPic.Items.Add(new ListItem(kasbon.UserName, kasbon.ID.ToString()));
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string txtGroupID = string.Empty;
            //for (int i = 0; i < ddlGroupID.Items.Count; i++)
            //{
            //    if (ddlGroupID.Items[i].Selected == true)
            //    {
            //        txtGroupID += ddlGroupID.Items[i].Value + ",";
            //    }
            //}
            decimal OK = 0;
            decimal NO = 0;
            decimal BATAL = 0;
            decimal TotalData = 0;
            OntimeKasbon p = new OntimeKasbon();
            ArrayList arrData = new ArrayList();
            p.Bulan = ddlBulan.SelectedValue;
            p.Tahun = ddlTahun.SelectedValue;
            //p.GroupID = txtGroupID.Substring(0, txtGroupID.Length - 1);
            p.Criteria = " and Year(k.TglKasbon)=" + ddlTahun.SelectedValue.ToString();
            p.Criteria += " and Month(k.TglKasbon)=" + ddlBulan.SelectedValue.ToString();
            p.Criteria += (ddlPic.SelectedItem.ToString() == "-- All PIC --") ? "" : " and k.Pic='" + ddlPic.SelectedItem.ToString() + "' ";
            arrData = p.RetrieveKasbon();
            lstBiaya.DataSource = arrData;
            lstBiaya.DataBind();
            //hitung total prosentasi
            TotalData = arrData.Count;
            for (int i = 0; i < lstBiaya.Items.Count; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)lstBiaya.Items[i].FindControl("trs");
                OK += (tr.Cells[8].InnerHtml == "OK") ? 1 : 0;
                NO += (tr.Cells[8].InnerHtml == "NO") ? 1 : 0;
                BATAL += (tr.Cells[8].InnerHtml == "BATAL") ? 1 : 0;
            }
            ttNO.Cells[1].InnerHtml = NO.ToString();
            ttOK.Cells[1].InnerHtml = OK.ToString();
            ttBatal.Cells[1].InnerHtml = BATAL.ToString();
            tt.Cells[1].InnerHtml = (TotalData > 0) ? ((OK) / (TotalData - BATAL)).ToString("P2") : "";
            tt.Cells[1].Attributes["title"] = (TotalData > 0) ? "( " + OK.ToString("N0") + " / " + TotalData.ToString("N0") + ") x 100 =" + (OK / TotalData).ToString("P2") : "";
        }
        protected void lstBiaya_Databound(object sender, RepeaterItemEventArgs e)
        {
            Kasbon p = (Kasbon)e.Item.DataItem;
            int Bulan, Tahun = 0;
            string periode = string.Empty;
            if (int.Parse(ddlBulan.SelectedValue) == 12)
            {
                periode = "06/01/" + (int.Parse(ddlTahun.SelectedValue) + 1);
            }
            else
            {
                periode = "06/" + (int.Parse(ddlBulan.SelectedValue) + 1).ToString().PadLeft(2, '0') + "/" + ddlTahun.SelectedValue;
            }
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trs");
            DateTime date1 = Convert.ToDateTime(p.ApproveDate2);
            DateTime date2 = Convert.ToDateTime(p.ApproveDate4);
            int Apv1 = date1.Day;
            int Apv2 = date2.Day;
            //tr.Cells[6].InnerHtml = (p.KasbonDate < DateTime.Parse(periode)) ? "OK" : "NO";
            double libure = new OntimeKasbon().cekHariLibur(p.ApproveDate2.ToString("yyyyMMdd"), p.ApproveDate4.ToString("yyyyMMdd"));
            int RealisasiPO = Convert.ToInt32(p.POID);
            //double Selisih = Math.Floor((p.ApproveDate4 - p.ApproveDate2).TotalDays);
            double Selisih = Apv2 - Apv1;
            tr.Cells[8].InnerHtml = ((Selisih - libure) > 3 && tr.Cells[4].InnerHtml != string.Empty) ? "NO" : ((Selisih - libure) > 3 || (Selisih - libure) < 3 && tr.Cells[4].InnerHtml == string.Empty) ? "BATAL" : "OK";
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            string txtGroupID = string.Empty;
            //for (int i = 0; i < ddlGroupID.Items.Count; i++)
            //{
            //    if (ddlGroupID.Items[i].Selected == true)
            //    {
            //        txtGroupID += ddlGroupID.Items[i].Text + ",";
            //    }
            //}
            //btnPreview_Click(null, null);
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanOntimeKasbon.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>PEMANTAUAN ONTIME KASBON</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            Html += (txtGroupID != string.Empty) ? "<br>Material Group : " + txtGroupID.Substring(0, txtGroupID.Length - 1) : "";
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstNewP.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("xx\">", "\">\'");
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }


    public class OntimeKasbon
    {
        public string Bulan { get; set; }
        public string Tahun { get; set; }
        public string Pic { get; set; }
        public string Criteria { get; set; }
        private string Query()
        {
            string result = "SELECT k.KasbonNo,k.NoPengajuan,pd.DocumentNo,p.NoPO,k.TglKasbon,k.ApprovedDate2,k.ApprovedDate4,k.Approval,kd.ItemName,kd.Qty,kd.EstimasiKasbon " +
                            "FROM Kasbon AS k LEFT JOIN KasbonDetail AS kd ON k.ID=kd.KID LEFT JOIN POPurchnDetail AS pd ON pd.ID=kd.PODetailID " +
                            "LEFT JOIN POPurchn AS p ON p.ID=pd.POID WHERE k.Status>-1 AND kd.Status>-1 AND k.Approval=4 " + this.Criteria +
                            "";
            return result;
        }
        public ArrayList RetrieveKasbon()
        {
            ArrayList arrData = new ArrayList();
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            string strSQL = this.Query();
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjects(sdr));
                }
            }
            return arrData;
        }
        private Kasbon GenerateObjects(SqlDataReader sdr)
        {
            Kasbon p = new Kasbon();
            p.NoPengajuan = sdr["NoPengajuan"].ToString();
            p.NoKasbon = sdr["KasbonNo"].ToString();
            p.NoSPP = sdr["DocumentNo"].ToString();
            p.NoPo = sdr["NoPO"].ToString();
            p.KasbonDate = (sdr["TglKasbon"] == DBNull.Value) ? DateTime.MaxValue : DateTime.Parse(sdr["TglKasbon"].ToString());
            p.NamaBarang = sdr["ItemName"].ToString();
            p.ApproveDate2 = (sdr["ApprovedDate2"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["ApprovedDate2"].ToString());
            p.ApproveDate4 = (sdr["ApprovedDate4"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["ApprovedDate4"].ToString());
            return p;
        }

        public double cekHariLibur(string fromDate, string ToDate)
        {
            double result = 0;
            string strSQL = "set datefirst 1;select dbo.GetOFFDay('" + fromDate + "','" + ToDate + "') as Libur ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    result = double.Parse(sdr["Libur"].ToString());
                }
            }
            return result;
        }

        public double cekRealisasi(string fromDate, string ToDate)
        {
            double result = 0;
            string strSQL = "SELECT kd.PODetailID FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID WHERE " +
                            "left(CONVERT(CHAR,k.TglKasbon,112),8) BETWEEN '20200901' AND '20200921' AND k.Status>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    result = double.Parse(sdr["PODetailID"].ToString());
                }
            }
            return result;
        }
    }
}