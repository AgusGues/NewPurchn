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
    public partial class LapLoadingTime : System.Web.UI.Page
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

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText(); string query1 = string.Empty;
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            string WaktuAwal = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartTime", "LoadingTime").ToString();
            query1 = " from data_600 order by MobilSendiri,UrutanNo ";

            Users users = (Users)Session["Users"];
            ReportFacade reportFacade = new ReportFacade();

            /** Pengaturan periode pengambilan data by Beny nnnnnn **/
            if (Convert.ToInt32(DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd").Substring(0, 6)) < 202109)
            {
                allQuery = reportFacade.ViewLapLoadingTime_asli(drTgl, sdTgl, 0, int.Parse(WaktuAwal));
            }
            else
            {
                allQuery = reportFacade.ViewLapLoadingTime(drTgl, sdTgl, 0, int.Parse(WaktuAwal), query1);
            }
            //allQuery = reportFacade.ViewLapLoadingTime2(drTgl, sdTgl, 0, int.Parse(WaktuAwal));
            strQuery = allQuery;
            Session["Query"] = strQuery;

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=LoadingTime', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LapLoadingTime1 lp = new LapLoadingTime1();
            ArrayList arrData = new ArrayList();
            lp.Pilihan = "Detail";
            lp.DariTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            lp.SampaiTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            lp.StartTime = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartTime", "LoadingTime").ToString();
            lp.ArmadaID = "0";
            //arrData = lp.Retrieve();
            //lstLoading.DataSource = arrData;
            //lstLoading.DataBind();

            /** Pengaturan periode pengambilan data by Beny n**/
            if (Convert.ToInt32(DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd").Substring(0, 6)) < 202109)
            {
                arrData = lp.Retrieve2();
                lstLoading.DataSource = arrData;
                lstLoading.DataBind();
            }
            else
            {
                arrData = lp.Retrieve();
                lstLoading.DataSource = arrData;
                lstLoading.DataBind();
            }
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

        protected void lstLoading_DataBound(object sender, RepeaterItemEventArgs e)
        {
            LoadingTime pm = (LoadingTime)e.Item.DataItem;
            //HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                for (int i = 1; i < tr.Cells.Count; i++)
                {
                    if (pm.Noted == "TimeIn=TimeOut" || pm.Noted.Contains("Lewat") || pm.Noted.Contains("Pinalti") || pm.Noted.Contains("Antaran Besok"))
                    {
                        tr.Cells[12].Attributes.Add("style", "background-color:Red; font-weight:bold; color:White;");
                        //tr.Cells[7].Attributes.Add("style", "font-color:Red; font-weight:bold; color:Red;");                    
                    }

                    if (pm.TimeOut.ToString().Trim().Contains("1900"))
                    {
                        tr.Cells[7].Attributes.Add("style", "background-color:Red; font-weight:bold; color:White;");

                    }
                }
            }
        }
    }
}

public class LapLoadingTime1
{
    ArrayList arrData = new ArrayList();
    LoadingTime ld = new LoadingTime();
    public string Criteria { get; set; }
    public string Pilihan { get; set; }
    public string DariTgl { get; set; }
    public string SampaiTgl { get; set; }
    public string StartTime { get; set; }
    public string ArmadaID { get; set; }

    private string Query()
    {
        string query = string.Empty; string query1 = string.Empty;
        switch (this.Pilihan)
        {
            case "Detail":
                query1 = " from data_600 order by MobilSendiri,UrutanNo ";
                query = new ReportFacade().ViewLapLoadingTime(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime), query1);
                break;
            case "Tahun":
                query = "Select Distinct Year(tglin) as Tahun from LoadingTime order by Year(tglin) desc";
                break;
            case "Rekap":
                query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; ";
                //"select * into laploadingtmp from ( ";
                query1 = " into laploadingtmp from data_600 order by MobilSendiri,UrutanNo ";
                query += new ReportFacade().ViewLapLoadingTime(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime), query1).Replace("order by MobilSendiri,TimeIn", "");
                //query += " )  as XX ";

                query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                      "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                      "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                      "  ( " +
                      "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                      "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                      "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                      "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                      "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                      "   from laploadingtmp  " +
                      "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                      "   ) as x Group By Tanggal " +
                      "   ) as xx " +
                      "   order by Tanggal";
                break;
        }
        return query;
    }
    private LoadingTime CreateObject(SqlDataReader sdr)
    {
        ld = new LoadingTime();
        switch (this.Pilihan)
        {
            case "Detail":
                ld.NoPolisi = sdr["NoPolisi"].ToString();
                ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                ld.Keterangan = sdr["Keterangan"].ToString();
                ld.JenisMobil = sdr["JenisMobil"].ToString();
                ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                ld.Status = Convert.ToInt32(sdr["Status"].ToString());
                ld.LoadingNo = sdr["StatusLoading"].ToString();
                //ld.LoadingNo = sdr["Status2"].ToString();
                ld.NoUrut = sdr["urutanno"].ToString();
                ld.Tujuan2 = sdr["Tujuan2"].ToString();

                ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                ld.Noted = sdr["Noted"].ToString();
                //ld.Target = Convert.ToDecimal(sdr["Target"]);
                ld.Target = sdr["Target"].ToString();
                ld.Status2 = Convert.ToInt32(sdr["Status2"].ToString());

                break;
            case "Tahun":
                ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                break;
            case "Rekap":
                ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());

                break;
        }
        return ld;
    }

    private string Query2()
    {
        string query = string.Empty; string query1 = string.Empty;
        switch (this.Pilihan)
        {
            case "Detail":
                query1 = "";
                query = new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime));
                break;
            case "Tahun":
                query = "Select Distinct Year(Timein) as Tahun from LoadingTime order by Year(Timein) desc";
                break;
            case "Rekap":
                query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; " +
                        "select * into laploadingtmp from ( ";
                query1 = "";
                query += new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime)).Replace("order by MobilSendiri,TimeIn", "");
                query += " )  as XX ";
                query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                      "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                      "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                      "  ( " +
                      "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                      "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                      "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                      "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                      "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                      "   from laploadingtmp  " +
                      "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                      "   ) as x Group By Tanggal " +
                      "   ) as xx " +
                      "   order by Tanggal";
                break;
        }
        return query;
    }
    private LoadingTime CreateObject2(SqlDataReader sdr)
    {
        ld = new LoadingTime();
        switch (this.Pilihan)
        {
            case "Detail":
                ld.NoPolisi = sdr["NoPolisi"].ToString();
                ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                ld.Keterangan = sdr["Ket"].ToString();
                ld.JenisMobil = sdr["JenisMobil"].ToString();
                ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                ld.Status = Convert.ToInt32(sdr["Status2"].ToString());
                ld.LoadingNo = sdr["StatusLoading"].ToString();
                ld.NoUrut = sdr["urutanno"].ToString();
                ld.Tujuan2 = sdr["Tujuan2"].ToString();

                ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                ld.Noted = sdr["Noted"].ToString();
                ld.Target = sdr["Target"].ToString();

                break;
            case "Tahun":
                ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                break;
            case "Rekap":
                ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());

                break;
        }
        return ld;
    }

    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(CreateObject(sdr));
            }
        }
        return arrData;
    }

    public ArrayList Retrieve2()
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query2());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(CreateObject2(sdr));
            }
        }
        return arrData;
    }
}